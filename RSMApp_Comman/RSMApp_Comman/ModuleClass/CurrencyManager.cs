
//Created On :: 05, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_Comman
{
    public class CurrencyManager
    {
        const string CurrencyTable = "Currency";
        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Currency objGetCurrency(string argCurrencyCode)
        {
            Currency argCurrency = new Currency();
            DataSet DataSetToFill = new DataSet();

            if (argCurrencyCode.Trim() == "")
            {
                goto ErrorHandler;
            }
            DataSetToFill = this.GetCurrency(argCurrencyCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argCurrency = this.objCreateCurrency((DataRow)DataSetToFill.Tables[0].Rows[0]);
            goto Finish;

        ErrorHandler:

        Finish:
            DataSetToFill = null;

            return argCurrency;
        }

        public ICollection<Currency> colGetCurrency()
        {
            List<Currency> lst = new List<Currency>();
            DataSet DataSetToFill = new DataSet();
            Currency tCurrency = new Currency();
            DataSetToFill = this.GetCurrency();

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCurrency(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetCurrency(int iIsDeleted)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";
            try
            {
                tSQL = "SELECT * FROM " + CurrencyTable.ToString();

                if (iIsDeleted >= 0)
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;
                    tSQL = tSQL + " " + sWhereClauseSTR + " IsDeleted = " + iIsDeleted;
                }
                ds = da.FillDataSetWithSQL(tSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetCurrency(string argCurrencyCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CurrencyCode", argCurrencyCode);
            DataSetToFill = da.FillDataSet("SP_GetCurrency4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCurrency(string argCurrencyCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CurrencyCode", argCurrencyCode);
            DataSetToFill = da.NFillDataSet("SP_GetCurrency4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCurrency()
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            DataSetToFill = da.FillDataSet("SP_GetCurrency");
            return DataSetToFill;
        }

        public DataSet CheckCountryDuplication(string strCurrencyName)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";
            try
            {
                tSQL = "SELECT * FROM " + CurrencyTable.ToString();

                if (strCurrencyName != "")
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;
                    tSQL = tSQL + " " + sWhereClauseSTR + " CurrencyName = '" + strCurrencyName + "'";
                }
                ds = da.FillDataSetWithSQL(tSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        private Currency objCreateCurrency(DataRow dr)
        {
            Currency tCurrency = new Currency();
            tCurrency.SetObjectInfo(dr);
            return tCurrency;
        }

        public ICollection<ErrorHandler> SaveCurrency(Currency argCurrency)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCurrencyExists(argCurrency.CurrencyCode) == false)
                {
                    if (blnSaveCurrencyBusinessrules(argCurrency.CurrencyName) == true)
                    {
                        objErrorHandler.Type = ErrorConstant.strAboartType;
                        objErrorHandler.MsgId = 0;
                        objErrorHandler.Module = ErrorConstant.strInsertModule;
                        objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                        objErrorHandler.Message = "Currency Name already Exists";
                        objErrorHandler.RowNo = 0;
                        objErrorHandler.FieldName = "";
                        objErrorHandler.LogCode = "";
                        lstErr.Add(objErrorHandler);
                    }
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCurrency(argCurrency, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
                    }
                    da.COMMIT_TRANSACTION();
                }
                else
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    UpdateCurrency(argCurrency, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
                    }
                    da.COMMIT_TRANSACTION();
                }
            }
            catch (Exception ex)
            {
                if (da != null)
                {
                    da.ROLLBACK_TRANSACTION();
                }
                objErrorHandler.Type = ErrorConstant.strAboartType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strInsertModule;
                objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                objErrorHandler.Message = ex.Message.ToString();
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";
                lstErr.Add(objErrorHandler);
            }
            finally
            {
                if (da != null)
                {
                    da.Close_Connection();
                    da = null;
                }
            }
            return lstErr;
        }

        public void SaveCurrency(Currency argCurrency, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCurrencyExists(argCurrency.CurrencyCode, da) == false)
                {
                    InsertCurrency(argCurrency, da, lstErr);
                }
                else
                {
                    UpdateCurrency(argCurrency, da, lstErr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Bulk Insert
        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
            Currency ObjCurrency = null;
            string xConnStr = "";
            string strSheetName = "";
            DataSet dsExcel = new DataSet();
            DataTable dtTableSchema = new DataTable();
            OleDbConnection objXConn = null;
            OleDbDataAdapter objDataAdapter = new OleDbDataAdapter();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            if (argFileExt.ToString() == ".xls")
            {
                xConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" +
               "Data Source=" + argExcelPath.Trim() + ";" +
               "Extended Properties=Excel 8.0";
            }
            else
            {
                xConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
               "Data Source=" + argExcelPath.Trim() + ";" +
               "Extended Properties=Excel 12.0";
            }

            try
            {
                objXConn = new OleDbConnection(xConnStr);
                objXConn.Open();
                dtTableSchema = objXConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (argFileExt.ToString() == ".xls")
                {
                    strSheetName = Convert.ToString(dtTableSchema.Rows[0]["TABLE_NAME"]);
                }
                else
                {
                    strSheetName = Convert.ToString(dtTableSchema.Rows[0]["TABLE_NAME"]);

                    if (strSheetName.IndexOf(@"_xlnm#_FilterDatabase") >= 0)
                    {
                        strSheetName = Convert.ToString(dtTableSchema.Rows[1]["TABLE_NAME"]);
                    }
                }
                argQuery = argQuery + " [" + strSheetName + "]";
                OleDbCommand objCommand = new OleDbCommand(argQuery, objXConn);
                objDataAdapter.SelectCommand = objCommand;
                objDataAdapter.Fill(dsExcel);
                dtExcel = dsExcel.Tables[0];
                /*****************************************/
                DataAccess da = new DataAccess();
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                try
                {
                    foreach (DataRow drExcel in dtExcel.Rows)
                    {
                        ObjCurrency = new Currency();

                        ObjCurrency.CurrencyCode = Convert.ToString(drExcel["CurrencyCode"]).Trim();
                        ObjCurrency.CurrencyName = Convert.ToString(drExcel["CurrencyName"]).Trim();
                        ObjCurrency.CreatedBy = Convert.ToString(argUserName);
                        ObjCurrency.ModifiedBy = Convert.ToString(argUserName);

                        SaveCurrency(ObjCurrency, da, lstErr);

                        foreach (ErrorHandler objerr in lstErr)
                        {
                            if (objerr.Type == "E")
                            {
                                da.ROLLBACK_TRANSACTION();
                                break;
                            }
                        }
                    }
                    da.COMMIT_TRANSACTION();
                }
                catch (Exception ex)
                {
                    if (da != null)
                    {
                        da.ROLLBACK_TRANSACTION();
                    }
                    objErrorHandler.Type = ErrorConstant.strAboartType;
                    objErrorHandler.MsgId = 0;
                    objErrorHandler.Module = ErrorConstant.strInsertModule;
                    objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                    objErrorHandler.Message = ex.Message.ToString();
                    objErrorHandler.RowNo = 0;
                    objErrorHandler.FieldName = "";
                    objErrorHandler.LogCode = "";
                    lstErr.Add(objErrorHandler);
                }
                finally
                {
                    if (da != null)
                    {
                        da.Close_Connection();
                        da = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objXConn.Close();
            }
            return lstErr;
        }
        #endregion

        public void InsertCurrency(Currency argCurrency, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@CurrencyCode", argCurrency.CurrencyCode);
            param[1] = new SqlParameter("@CurrencyName", argCurrency.CurrencyName);
            param[2] = new SqlParameter("@CreatedBy", argCurrency.CreatedBy);
            param[3] = new SqlParameter("@ModifiedBy", argCurrency.ModifiedBy);
            
            param[4] = new SqlParameter("@Type", SqlDbType.Char);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[5].Size = 255;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCurrency", param);

            string strType = Convert.ToString(param[4].Value);
            string strMessage = Convert.ToString(param[5].Value);            
            string strRetValue = Convert.ToString(param[6].Value);

            objErrorHandler.Type = strType;
            objErrorHandler.MsgId = 0;
            objErrorHandler.Module = ErrorConstant.strInsertModule;
            objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
            objErrorHandler.Message = strMessage.ToString();
            objErrorHandler.RowNo = 0;
            objErrorHandler.FieldName = "";
            objErrorHandler.LogCode = "";
            lstErr.Add(objErrorHandler);
        }

        public void UpdateCurrency(Currency argCurrency, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@CurrencyCode", argCurrency.CurrencyCode);
            param[1] = new SqlParameter("@CurrencyName", argCurrency.CurrencyName);
            param[2] = new SqlParameter("@CreatedBy", argCurrency.CreatedBy);
            param[3] = new SqlParameter("@ModifiedBy", argCurrency.ModifiedBy);
            
            param[4] = new SqlParameter("@Type", SqlDbType.Char);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[5].Size = 255;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCurrency", param);

            string strType = Convert.ToString(param[4].Value);
            string strMessage = Convert.ToString(param[5].Value);            
            string strRetValue = Convert.ToString(param[6].Value);

            objErrorHandler.Type = strType;
            objErrorHandler.MsgId = 0;
            objErrorHandler.Module = ErrorConstant.strInsertModule;
            objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
            objErrorHandler.Message = strMessage.ToString();
            objErrorHandler.RowNo = 0;
            objErrorHandler.FieldName = "";
            objErrorHandler.LogCode = "";
            lstErr.Add(objErrorHandler);
        }

        public ICollection<ErrorHandler> DeleteCurrency(string argCurrencyCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@CurrencyCode", argCurrencyCode);
                param[1] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[2] = new SqlParameter("@Type", SqlDbType.Char);
                param[2].Size = 1;
                param[2].Direction = ParameterDirection.Output;

                param[3] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[3].Size = 255;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[4].Size = 20;
                param[4].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteCurrency", param);
                
                string strMessage = Convert.ToString(param[3].Value);
                string strType = Convert.ToString(param[2].Value);
                string strRetValue = Convert.ToString(param[4].Value);

                objErrorHandler.Type = strType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strInsertModule;
                objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                objErrorHandler.Message = strMessage.ToString();
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";
                lstErr.Add(objErrorHandler);
            }
            catch (Exception ex)
            {
                objErrorHandler.Type = ErrorConstant.strAboartType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strDeleteModule;
                objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                objErrorHandler.Message = ex.Message.ToString();
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";
                lstErr.Add(objErrorHandler);
            }
            return lstErr;
        }

        public bool blnIsCurrencyExists(string argCurrencyCode)
        {
            bool IsCurrencyExists = false;
            DataSet ds = new DataSet();
            ds = GetCurrency(argCurrencyCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCurrencyExists = true;
            }
            else
            {
                IsCurrencyExists = false;
            }
            return IsCurrencyExists;
        }

        public bool blnIsCurrencyExists(string argCurrencyCode, DataAccess da)
        {
            bool IsCurrencyExists = false;
            DataSet ds = new DataSet();
            ds = GetCurrency(argCurrencyCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCurrencyExists = true;
            }
            else
            {
                IsCurrencyExists = false;
            }
            return IsCurrencyExists;
        }

        public bool blnSaveCurrencyBusinessrules(string strCountryName)
        {
            bool IsDuplicate = false;
            DataSet ds = new DataSet();
            ds = CheckCountryDuplication(strCountryName);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDuplicate = true;
            }
            else
            {
                IsDuplicate = false;
            }
            return IsDuplicate;
        }
    }
}