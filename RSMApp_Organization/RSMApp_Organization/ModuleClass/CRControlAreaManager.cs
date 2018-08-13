
//Created On :: 10, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using Telerik.Web.UI;
using System.Data.OleDb;

namespace RSMApp_Organization
{
    public class CRControlAreaManager
    {
        const string CRControlAreaTable = "CRControlArea";
        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CRControlArea objGetCRControlArea(string argCRContAreaCode, string argClientCode)
        {
            CRControlArea argCRControlArea = new CRControlArea();
            DataSet DataSetToFill = new DataSet();

            if (argCRContAreaCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetCRControlArea(argCRContAreaCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argCRControlArea = this.objCreateCRControlArea((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argCRControlArea;
        }
        
        public ICollection<CRControlArea> colGetCRControlArea(string argClientCode)
        {
            List<CRControlArea> lst = new List<CRControlArea>();
            DataSet DataSetToFill = new DataSet();
            CRControlArea tCRControlArea = new CRControlArea();
            DataSetToFill = this.GetCRControlArea(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCRControlArea(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetCRControlArea(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + CRControlAreaTable.ToString();

                if (iIsDeleted > -1)
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;
                    tSQL = tSQL + " " + sWhereClauseSTR + " IsDeleted = " + iIsDeleted;
                }

                if (argClientCode != "")
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;
                    tSQL = tSQL + " " + sWhereClauseSTR + " ClientCode = '" + argClientCode + "'";
                }
                ds = da.FillDataSetWithSQL(tSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetCRControlArea(string argCRContAreaCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CRContAreaCode", argCRContAreaCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCRControlArea4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCRControlArea(string argCRContAreaCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CRContAreaCode", argCRContAreaCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCRControlArea4ID", param);
            return DataSetToFill;
        }
        
        public DataSet GetCRControlArea(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCRControlArea", param);
            return DataSetToFill;
        }
        
        private CRControlArea objCreateCRControlArea(DataRow dr)
        {
            CRControlArea tCRControlArea = new CRControlArea();
            tCRControlArea.SetObjectInfo(dr);
            return tCRControlArea;
        }
        
        public ICollection<ErrorHandler> SaveCRControlArea(CRControlArea argCRControlArea)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCRControlAreaExists(argCRControlArea.CRContAreaCode, argCRControlArea.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCRControlArea(argCRControlArea, da, lstErr);
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
                    UpdateCRControlArea(argCRControlArea, da, lstErr);
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

        #region Bulk Insert
        public void SaveCRControlArea(CRControlArea argCRControlArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCRControlAreaExists(argCRControlArea.CRContAreaCode, argCRControlArea.ClientCode, da) == false)
                {
                    InsertCRControlArea(argCRControlArea, da, lstErr);
                }
                else
                {
                    UpdateCRControlArea(argCRControlArea, da, lstErr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
            CRControlArea ObjCRControlArea = null;
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
                        ObjCRControlArea = new CRControlArea();
                        ObjCRControlArea.CRContAreaCode = Convert.ToString(drExcel["CRContAreaCode"]).Trim();
                        ObjCRControlArea.CRContArea = Convert.ToString(drExcel["CRContArea"]).Trim();
                        ObjCRControlArea.CurrencyCode = Convert.ToString(drExcel["CurrencyCode"]).Trim();
                        ObjCRControlArea.CRUpdateCode = Convert.ToString(drExcel["CRUpdateCode"]).Trim();
                        ObjCRControlArea.FiscalYearType = Convert.ToString(drExcel["FiscalYearType"]).Trim();
                        ObjCRControlArea.RiskCategoryCode = Convert.ToString(drExcel["RiskCategoryCode"]).Trim();
                        ObjCRControlArea.CreditLimit = Convert.ToInt32(drExcel["CreditLimit"]);
                        ObjCRControlArea.CreatedBy = Convert.ToString(argUserName);
                        ObjCRControlArea.ModifiedBy = Convert.ToString(argUserName);
                        ObjCRControlArea.ClientCode = Convert.ToString(argClientCode);
                        SaveCRControlArea(ObjCRControlArea, da, lstErr);

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
        
        public void InsertCRControlArea(CRControlArea argCRControlArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@CRContAreaCode", argCRControlArea.CRContAreaCode);
            param[1] = new SqlParameter("@CRContArea", argCRControlArea.CRContArea);
            param[2] = new SqlParameter("@CurrencyCode", argCRControlArea.CurrencyCode);
            param[3] = new SqlParameter("@CRUpdateCode", argCRControlArea.CRUpdateCode);
            param[4] = new SqlParameter("@FiscalYearType", argCRControlArea.FiscalYearType);
            param[5] = new SqlParameter("@RiskCategoryCode", argCRControlArea.RiskCategoryCode);
            param[6] = new SqlParameter("@CreditLimit", argCRControlArea.CreditLimit);
            param[7] = new SqlParameter("@ClientCode", argCRControlArea.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argCRControlArea.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argCRControlArea.ModifiedBy);            

            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCRControlArea", param);

            string strMessage = Convert.ToString(param[11].Value);
            string strType = Convert.ToString(param[10].Value);
            string strRetValue = Convert.ToString(param[12].Value);

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
        
        public void UpdateCRControlArea(CRControlArea argCRControlArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@CRContAreaCode", argCRControlArea.CRContAreaCode);
            param[1] = new SqlParameter("@CRContArea", argCRControlArea.CRContArea);
            param[2] = new SqlParameter("@CurrencyCode", argCRControlArea.CurrencyCode);
            param[3] = new SqlParameter("@CRUpdateCode", argCRControlArea.CRUpdateCode);
            param[4] = new SqlParameter("@FiscalYearType", argCRControlArea.FiscalYearType);
            param[5] = new SqlParameter("@RiskCategoryCode", argCRControlArea.RiskCategoryCode);
            param[6] = new SqlParameter("@CreditLimit", argCRControlArea.CreditLimit);
            param[7] = new SqlParameter("@ClientCode", argCRControlArea.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argCRControlArea.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argCRControlArea.ModifiedBy);
            
            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCRControlArea", param);

            string strMessage = Convert.ToString(param[11].Value);
            string strType = Convert.ToString(param[10].Value);
            string strRetValue = Convert.ToString(param[12].Value);

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
        
        public ICollection<ErrorHandler> DeleteCRControlArea(string argCRContAreaCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CRContAreaCode", argCRContAreaCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteCRControlArea", param);

                string strMessage = Convert.ToString(param[4].Value);
                string strType = Convert.ToString(param[3].Value);
                string strRetValue = Convert.ToString(param[5].Value);

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
        
        public bool blnIsCRControlAreaExists(string argCRContAreaCode, string argClientCode)
        {
            bool IsCRControlAreaExists = false;
            DataSet ds = new DataSet();
            ds = GetCRControlArea(argCRContAreaCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCRControlAreaExists = true;
            }
            else
            {
                IsCRControlAreaExists = false;
            }
            return IsCRControlAreaExists;
        }

        public bool blnIsCRControlAreaExists(string argCRContAreaCode, string argClientCode, DataAccess da)
        {
            bool IsCRControlAreaExists = false;
            DataSet ds = new DataSet();
            ds = GetCRControlArea(argCRContAreaCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCRControlAreaExists = true;
            }
            else
            {
                IsCRControlAreaExists = false;
            }
            return IsCRControlAreaExists;
        }
    }
}