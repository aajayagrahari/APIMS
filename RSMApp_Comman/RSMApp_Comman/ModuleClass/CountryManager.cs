
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
    public class CountryManager
    {
        const string CountryTable = "Country";
        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Country objGetCountry(string argCountryCode)
        {
            Country argCountry = new Country();
            DataSet DataSetToFill = new DataSet();

            if (argCountryCode.Trim() == "")
            {
                goto ErrorHandler;
            }
            DataSetToFill = this.GetCountry(argCountryCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argCountry = this.objCreateCountry((DataRow)DataSetToFill.Tables[0].Rows[0]);
            goto Finish;

        ErrorHandler:

        Finish:
            DataSetToFill = null;

            return argCountry;
        }

        public DataSet CheckCountryDuplication(string strCountryName)
        {
            DataSet dsCountry = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";
            try
            {
                tSQL = "SELECT * FROM " + CountryTable.ToString();

                if (strCountryName != "")
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;
                    tSQL = tSQL + " " + sWhereClauseSTR + " CountryName = '" + strCountryName + "'";
                }
                dsCountry = da.FillDataSetWithSQL(tSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dsCountry;
        }

        public DataSet GetCountry(string argCountryCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CountryCode", argCountryCode);
            DataSetToFill = da.FillDataSet("SP_GetCountry4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCountry(string argCountryCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CountryCode", argCountryCode);
            DataSetToFill = da.NFillDataSet("SP_GetCountry4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCountry()
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            DataSetToFill = da.FillDataSet("SP_GetCountry");
            return DataSetToFill;
        }

        public DataSet GetCountry(int iIsDeleted)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";
            try
            {
                tSQL = "SELECT * FROM " + CountryTable.ToString();

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

        private Country objCreateCountry(DataRow dr)
        {
            Country tCountry = new Country();
            tCountry.SetObjectInfo(dr);
            return tCountry;
        }

        public ICollection<Country> colGetCountry()
        {
            List<Country> lst = new List<Country>();
            DataSet DataSetToFill = new DataSet();
            Country tCountry = new Country();
            DataSetToFill = this.GetCountry();

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCountry(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<ErrorHandler> SaveCountry(Country argCountry)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCountryExists(argCountry.CountryCode) == false)
                {
                    if (blnSaveCountryBusinessrules(argCountry.CountryName) == true)
                    {
                        objErrorHandler.Type = ErrorConstant.strAboartType;
                        objErrorHandler.MsgId = 0;
                        objErrorHandler.Module = ErrorConstant.strInsertModule;
                        objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                        objErrorHandler.Message = "Country with same name exists.";
                        objErrorHandler.RowNo = 0;
                        objErrorHandler.FieldName = "";
                        objErrorHandler.LogCode = "";
                        lstErr.Add(objErrorHandler);
                        return lstErr;
                    }
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCountry(argCountry, da, lstErr);
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
                    UpdateCountry(argCountry, da, lstErr);
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

        public void SaveCountry(Country argCountry, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCountryExists(argCountry.CountryCode, da) == false)
                {
                    InsertCountry(argCountry, da, lstErr);
                }
                else
                {
                    UpdateCountry(argCountry, da, lstErr);
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
            Country ObjCountry = null;
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
                        ObjCountry = new Country();

                        ObjCountry.CountryCode = Convert.ToString(drExcel["CountryCode"]).Trim();
                        ObjCountry.CountryName = Convert.ToString(drExcel["CountryName"]).Trim();
                        ObjCountry.CreatedBy = Convert.ToString(argUserName);
                        ObjCountry.ModifiedBy = Convert.ToString(argUserName);

                        SaveCountry(ObjCountry, da, lstErr);

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
        
        public void InsertCountry(Country argCountry, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@CountryCode", argCountry.CountryCode);
            param[1] = new SqlParameter("@CountryName", argCountry.CountryName);
            param[2] = new SqlParameter("@CreatedBy", argCountry.CreatedBy);
            param[3] = new SqlParameter("@ModifiedBy", argCountry.ModifiedBy);
            
            param[4] = new SqlParameter("@Type", SqlDbType.Char);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[5].Size = 255;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCountry", param);

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
        
        public void UpdateCountry(Country argCountry, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@CountryCode", argCountry.CountryCode);
            param[1] = new SqlParameter("@CountryName", argCountry.CountryName);
            param[2] = new SqlParameter("@CreatedBy", argCountry.CreatedBy);
            param[3] = new SqlParameter("@ModifiedBy", argCountry.ModifiedBy);
            
            param[4] = new SqlParameter("@Type", SqlDbType.Char);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[5].Size = 255;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCountry", param);

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
        
        public ICollection<ErrorHandler> DeleteCountry(string argCountryCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@CountryCode", argCountryCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteCountry", param);

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
        
        public bool blnIsCountryExists(string argCountryCode)
        {
            bool IsCountryExists = false;
            DataSet ds = new DataSet();
            ds = GetCountry(argCountryCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCountryExists = true;
            }
            else
            {
                IsCountryExists = false;
            }
            return IsCountryExists;
        }

        public bool blnIsCountryExists(string argCountryCode, DataAccess da)
        {
            bool IsCountryExists = false;
            DataSet ds = new DataSet();
            ds = GetCountry(argCountryCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCountryExists = true;
            }
            else
            {
                IsCountryExists = false;
            }
            return IsCountryExists;
        }

        public bool blnSaveCountryBusinessrules(string strCountryName)
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