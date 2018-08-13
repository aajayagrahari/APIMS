
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
    public class CRContRiskCategoryManager
    {
        const string CRContRiskCategoryTable = "CRContRiskCategory";
        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CRContRiskCategory objGetCRContRiskCategory(string argRiskCategoryCode, string argClientCode)
        {
            CRContRiskCategory argCRContRiskCategory = new CRContRiskCategory();
            DataSet DataSetToFill = new DataSet();

            if (argRiskCategoryCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetCRContRiskCategory(argRiskCategoryCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argCRContRiskCategory = this.objCreateCRContRiskCategory((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argCRContRiskCategory;
        }
        
        public ICollection<CRContRiskCategory> colGetCRContRiskCategory(string argClientCode)
        {
            List<CRContRiskCategory> lst = new List<CRContRiskCategory>();
            DataSet DataSetToFill = new DataSet();
            CRContRiskCategory tCRContRiskCategory = new CRContRiskCategory();
            DataSetToFill = this.GetCRContRiskCategory(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCRContRiskCategory(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetCRContRiskCategory(string argRiskCategoryCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RiskCategoryCode", argRiskCategoryCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCRContRiskCategory4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCRContRiskCategory(string argRiskCategoryCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RiskCategoryCode", argRiskCategoryCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCRContRiskCategory4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCRContRiskCategory(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + CRContRiskCategoryTable.ToString();

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
        
        public DataSet GetCRContRiskCategory(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCRContRiskCategory", param);
            return DataSetToFill;
        }
        
        private CRContRiskCategory objCreateCRContRiskCategory(DataRow dr)
        {
            CRContRiskCategory tCRContRiskCategory = new CRContRiskCategory();
            tCRContRiskCategory.SetObjectInfo(dr);
            return tCRContRiskCategory;
        }
        
        public ICollection<ErrorHandler> SaveCRContRiskCategory(CRContRiskCategory argCRContRiskCategory)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCRContRiskCategoryExists(argCRContRiskCategory.RiskCategoryCode, argCRContRiskCategory.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCRContRiskCategory(argCRContRiskCategory, da, lstErr);
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
                    UpdateCRContRiskCategory(argCRContRiskCategory, da, lstErr);
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
        public void SaveCRContRiskCategory(CRContRiskCategory argCRContRiskCategory, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCRContRiskCategoryExists(argCRContRiskCategory.RiskCategoryCode, argCRContRiskCategory.ClientCode, da) == false)
                {
                    InsertCRContRiskCategory(argCRContRiskCategory, da, lstErr);
                }
                else
                {
                    UpdateCRContRiskCategory(argCRContRiskCategory, da, lstErr);
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
            CRContRiskCategory ObjCRContRiskCategory = null;
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
                        ObjCRContRiskCategory = new CRContRiskCategory();
                        ObjCRContRiskCategory.RiskCategoryCode = Convert.ToString(drExcel["RiskCategoryCode"]).Trim();
                        ObjCRContRiskCategory.RiskCategory = Convert.ToString(drExcel["RiskCategory"]).Trim();
                        ObjCRContRiskCategory.CreatedBy = Convert.ToString(argUserName);
                        ObjCRContRiskCategory.ModifiedBy = Convert.ToString(argUserName);
                        ObjCRContRiskCategory.ClientCode = Convert.ToString(argClientCode);
                        SaveCRContRiskCategory(ObjCRContRiskCategory, da, lstErr);

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
        
        public void InsertCRContRiskCategory(CRContRiskCategory argCRContRiskCategory, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@RiskCategoryCode", argCRContRiskCategory.RiskCategoryCode);
            param[1] = new SqlParameter("@RiskCategory", argCRContRiskCategory.RiskCategory);
            param[2] = new SqlParameter("@ClientCode", argCRContRiskCategory.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argCRContRiskCategory.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argCRContRiskCategory.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCRContRiskCategory", param);

            string strMessage = Convert.ToString(param[6].Value);
            string strType = Convert.ToString(param[5].Value);
            string strRetValue = Convert.ToString(param[7].Value);

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
        
        public void UpdateCRContRiskCategory(CRContRiskCategory argCRContRiskCategory, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@RiskCategoryCode", argCRContRiskCategory.RiskCategoryCode);
            param[1] = new SqlParameter("@RiskCategory", argCRContRiskCategory.RiskCategory);
            param[2] = new SqlParameter("@ClientCode", argCRContRiskCategory.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argCRContRiskCategory.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argCRContRiskCategory.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCRContRiskCategory", param);

            string strMessage = Convert.ToString(param[6].Value);
            string strType = Convert.ToString(param[5].Value);
            string strRetValue = Convert.ToString(param[7].Value);

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
        
        public ICollection<ErrorHandler> DeleteCRContRiskCategory(string argRiskCategoryCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@RiskCategoryCode", argRiskCategoryCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteCRContRiskCategory", param);

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
        
        public bool blnIsCRContRiskCategoryExists(string argRiskCategoryCode, string argClientCode)
        {
            bool IsCRContRiskCategoryExists = false;
            DataSet ds = new DataSet();
            ds = GetCRContRiskCategory(argRiskCategoryCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCRContRiskCategoryExists = true;
            }
            else
            {
                IsCRContRiskCategoryExists = false;
            }
            return IsCRContRiskCategoryExists;
        }

        public bool blnIsCRContRiskCategoryExists(string argRiskCategoryCode, string argClientCode, DataAccess da)
        {
            bool IsCRContRiskCategoryExists = false;
            DataSet ds = new DataSet();
            ds = GetCRContRiskCategory(argRiskCategoryCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCRContRiskCategoryExists = true;
            }
            else
            {
                IsCRContRiskCategoryExists = false;
            }
            return IsCRContRiskCategoryExists;
        }
    }
}