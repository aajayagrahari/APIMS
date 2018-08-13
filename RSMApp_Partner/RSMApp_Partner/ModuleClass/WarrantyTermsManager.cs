
//Created On :: 12, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_Partner
{
    public class WarrantyTermsManager
    {
        const string WarrantyTermsTable = "WarrantyTerms";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public WarrantyTerms objGetWarrantyTerms(string argWarrantyTermsCode, string argClientCode)
        {
            WarrantyTerms argWarrantyTerms = new WarrantyTerms();
            DataSet DataSetToFill = new DataSet();

            if (argWarrantyTermsCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetWarrantyTerms(argWarrantyTermsCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argWarrantyTerms = this.objCreateWarrantyTerms((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argWarrantyTerms;
        }
        
        public ICollection<WarrantyTerms> colGetWarrantyTerms(string argClientCode)
        {
            List<WarrantyTerms> lst = new List<WarrantyTerms>();
            DataSet DataSetToFill = new DataSet();
            WarrantyTerms tWarrantyTerms = new WarrantyTerms();

            DataSetToFill = this.GetWarrantyTerms(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateWarrantyTerms(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetWarrantyTerms(string argWarrantyTermsCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@WarrantyTermsCode", argWarrantyTermsCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetWarrantyTerms4ID", param);
            return DataSetToFill;
        }

        public DataSet GetWarrantyTerms(string argWarrantyTermsCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@WarrantyTermsCode", argWarrantyTermsCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetWarrantyTerms4ID", param);
            return DataSetToFill;
        }

        public DataSet GetWarrantyTerms(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetWarrantyTerms",param);
            return DataSetToFill;
        }

        public DataSet GetWarrantyTerms(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + WarrantyTermsTable.ToString();

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

        private WarrantyTerms objCreateWarrantyTerms(DataRow dr)
        {
            WarrantyTerms tWarrantyTerms = new WarrantyTerms();
            tWarrantyTerms.SetObjectInfo(dr);
            return tWarrantyTerms;
        }

        public ICollection<ErrorHandler> SaveWarrantyTerms(WarrantyTerms argWarrantyTerms)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsWarrantyTermsExists(argWarrantyTerms.WarrantyTermsCode, argWarrantyTerms.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertWarrantyTerms(argWarrantyTerms, da, lstErr);
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
                    UpdateWarrantyTerms(argWarrantyTerms, da, lstErr);
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

        public void SaveWarrantyTerms(WarrantyTerms argWarrantyTerms, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsWarrantyTermsExists(argWarrantyTerms.WarrantyTermsCode, argWarrantyTerms.ClientCode, da) == false)
                {
                    InsertWarrantyTerms(argWarrantyTerms, da, lstErr);
                }
                else
                {
                    UpdateWarrantyTerms(argWarrantyTerms, da, lstErr);
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
            WarrantyTerms ObjWarrantyTerms = null;
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
                        ObjWarrantyTerms = new WarrantyTerms();
                        ObjWarrantyTerms.WarrantyTermsCode = Convert.ToString(drExcel["WarrantyTermsCode"]).Trim();
                        ObjWarrantyTerms.WarrantyTermsDesc = Convert.ToString(drExcel["WarrantyTermsDesc"]).Trim();
                        ObjWarrantyTerms.PeriodType = Convert.ToString(drExcel["PeriodType"]).Trim();
                        ObjWarrantyTerms.PeriodNo = Convert.ToString(drExcel["PeriodNo"]).Trim();
                        ObjWarrantyTerms.CreatedBy = Convert.ToString(argUserName);
                        ObjWarrantyTerms.ModifiedBy = Convert.ToString(argUserName);
                        ObjWarrantyTerms.ClientCode = Convert.ToString(argClientCode);
                        SaveWarrantyTerms(ObjWarrantyTerms, da, lstErr);

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

        public void InsertWarrantyTerms(WarrantyTerms argWarrantyTerms, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@WarrantyTermsCode", argWarrantyTerms.WarrantyTermsCode);
            param[1] = new SqlParameter("@WarrantyTermsDesc", argWarrantyTerms.WarrantyTermsDesc);
            param[2] = new SqlParameter("@PeriodType", argWarrantyTerms.PeriodType);
            param[3] = new SqlParameter("@PeriodNo", argWarrantyTerms.PeriodNo);
            param[4] = new SqlParameter("@ClientCode", argWarrantyTerms.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argWarrantyTerms.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argWarrantyTerms.ModifiedBy);
      
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertWarrantyTerms", param);

            string strMessage = Convert.ToString(param[8].Value);
            string strType = Convert.ToString(param[7].Value);
            string strRetValue = Convert.ToString(param[9].Value);

            objErrorHandler.Type = strType;
            objErrorHandler.MsgId = 0;
            objErrorHandler.Module = ErrorConstant.strInsertModule;
            objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
            objErrorHandler.Message = strMessage.ToString();
            objErrorHandler.RowNo = 0;
            objErrorHandler.FieldName = "";
            objErrorHandler.LogCode = "";
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);
        }

        public void UpdateWarrantyTerms(WarrantyTerms argWarrantyTerms, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@WarrantyTermsCode", argWarrantyTerms.WarrantyTermsCode);
            param[1] = new SqlParameter("@WarrantyTermsDesc", argWarrantyTerms.WarrantyTermsDesc);
            param[2] = new SqlParameter("@PeriodType", argWarrantyTerms.PeriodType);
            param[3] = new SqlParameter("@PeriodNo", argWarrantyTerms.PeriodNo);
            param[4] = new SqlParameter("@ClientCode", argWarrantyTerms.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argWarrantyTerms.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argWarrantyTerms.ModifiedBy);

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateWarrantyTerms", param);

            string strMessage = Convert.ToString(param[8].Value);
            string strType = Convert.ToString(param[7].Value);
            string strRetValue = Convert.ToString(param[9].Value);

            objErrorHandler.Type = strType;
            objErrorHandler.MsgId = 0;
            objErrorHandler.Module = ErrorConstant.strInsertModule;
            objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
            objErrorHandler.Message = strMessage.ToString();
            objErrorHandler.RowNo = 0;
            objErrorHandler.FieldName = "";
            objErrorHandler.LogCode = "";
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);
        }

        public ICollection<ErrorHandler> DeleteWarrantyTerms(string argWarrantyTermsCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@WarrantyTermsCode", argWarrantyTermsCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteWarrantyTerms", param);


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
                objErrorHandler.ReturnValue = strRetValue;
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

        public bool blnIsWarrantyTermsExists(string argWarrantyTermsCode, string argClientCode)
        {
            bool IsWarrantyTermsExists = false;
            DataSet ds = new DataSet();
            ds = GetWarrantyTerms(argWarrantyTermsCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsWarrantyTermsExists = true;
            }
            else
            {
                IsWarrantyTermsExists = false;
            }
            return IsWarrantyTermsExists;
        }

        public bool blnIsWarrantyTermsExists(string argWarrantyTermsCode, string argClientCode, DataAccess da)
        {
            bool IsWarrantyTermsExists = false;
            DataSet ds = new DataSet();
            ds = GetWarrantyTerms(argWarrantyTermsCode, argClientCode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsWarrantyTermsExists = true;
            }
            else
            {
                IsWarrantyTermsExists = false;
            }
            return IsWarrantyTermsExists;
        }
    }
}