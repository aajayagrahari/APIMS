
//Created On :: 03, October, 2012
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
    public class PaymentTermsManager
    {
        const string PaymentTermsTable = "PaymentTerms";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public PaymentTerms objGetPaymentTerms(string argPaymentTermsCode, string argClientCode)
        {
            PaymentTerms argPaymentTerms = new PaymentTerms();
            DataSet DataSetToFill = new DataSet();

            if (argPaymentTermsCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPaymentTerms(argPaymentTermsCode,argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPaymentTerms = this.objCreatePaymentTerms((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPaymentTerms;
        }

        public ICollection<PaymentTerms> colGetPaymentTerms(string argClientCode)
        {
            List<PaymentTerms> lst = new List<PaymentTerms>();
            DataSet DataSetToFill = new DataSet();
            PaymentTerms tPaymentTerms = new PaymentTerms();

            DataSetToFill = this.GetPaymentTerms(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePaymentTerms(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetPaymentTerms(string argPaymentTermsCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PaymentTermsCode", argPaymentTermsCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPaymentTerms4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPaymentTerms(string argPaymentTermsCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PaymentTermsCode", argPaymentTermsCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPaymentTerms4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPaymentTerms(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPaymentTerms",param);
            return DataSetToFill;
        }

        public DataSet GetPaymentTerms(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + PaymentTermsTable.ToString();

                if (iIsDeleted >= 0)
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

        private PaymentTerms objCreatePaymentTerms(DataRow dr)
        {
            PaymentTerms tPaymentTerms = new PaymentTerms();

            tPaymentTerms.SetObjectInfo(dr);

            return tPaymentTerms;

        }

        public ICollection<ErrorHandler> SavePaymentTerms(PaymentTerms argPaymentTerms)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPaymentTermsExists(argPaymentTerms.PaymentTermsCode,argPaymentTerms.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPaymentTerms(argPaymentTerms, da, lstErr);
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
                    UpdatePaymentTerms(argPaymentTerms, da, lstErr);
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

        public void SavePaymentTerms(PaymentTerms argPaymentTerms, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsPaymentTermsExists(argPaymentTerms.PaymentTermsCode, argPaymentTerms.ClientCode, da) == false)
                {
                    InsertPaymentTerms(argPaymentTerms, da, lstErr);
                }
                else
                {
                    UpdatePaymentTerms(argPaymentTerms, da, lstErr);
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
            PaymentTerms ObjPaymentTerms = null;
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
                        ObjPaymentTerms = new PaymentTerms();

                        ObjPaymentTerms.PaymentTermsCode = Convert.ToString(drExcel["PaymentTermsCode"]).Trim();
                        ObjPaymentTerms.PaymentTermsDesc1 = Convert.ToString(drExcel["PaymentTermsDesc1"]).Trim();
                        ObjPaymentTerms.PaymentTermsDesc2 = Convert.ToString(drExcel["PaymentTermsDesc2"]).Trim();
                        ObjPaymentTerms.PaymentTermsDesc3 = Convert.ToString(drExcel["PaymentTermsDesc3"]).Trim();
                        ObjPaymentTerms.PaymentTermsDesc4 = Convert.ToString(drExcel["PaymentTermsDesc4"]).Trim();
                        ObjPaymentTerms.PaymentTermsDesc5 = Convert.ToString(drExcel["PaymentTermsDesc5"]).Trim();
                        ObjPaymentTerms.DayLimit = Convert.ToString(drExcel["DayLimit"]).Trim();
                        ObjPaymentTerms.ClientCode = Convert.ToString(argClientCode);

                        SavePaymentTerms(ObjPaymentTerms, da, lstErr);

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

        public void InsertPaymentTerms(PaymentTerms argPaymentTerms, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@PaymentTermsCode", argPaymentTerms.PaymentTermsCode);
            param[1] = new SqlParameter("@PaymentTermsDesc1", argPaymentTerms.PaymentTermsDesc1);
            param[2] = new SqlParameter("@PaymentTermsDesc2", argPaymentTerms.PaymentTermsDesc2);
            param[3] = new SqlParameter("@PaymentTermsDesc3", argPaymentTerms.PaymentTermsDesc3);
            param[4] = new SqlParameter("@PaymentTermsDesc4", argPaymentTerms.PaymentTermsDesc4);
            param[5] = new SqlParameter("@PaymentTermsDesc5", argPaymentTerms.PaymentTermsDesc5);
            param[6] = new SqlParameter("@DayLimit", argPaymentTerms.DayLimit);
            param[7] = new SqlParameter("@ClientCode", argPaymentTerms.ClientCode);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPaymentTerms", param);


            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
            string strRetValue = Convert.ToString(param[10].Value);


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

        public void UpdatePaymentTerms(PaymentTerms argPaymentTerms, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@PaymentTermsCode", argPaymentTerms.PaymentTermsCode);
            param[1] = new SqlParameter("@PaymentTermsDesc1", argPaymentTerms.PaymentTermsDesc1);
            param[2] = new SqlParameter("@PaymentTermsDesc2", argPaymentTerms.PaymentTermsDesc2);
            param[3] = new SqlParameter("@PaymentTermsDesc3", argPaymentTerms.PaymentTermsDesc3);
            param[4] = new SqlParameter("@PaymentTermsDesc4", argPaymentTerms.PaymentTermsDesc4);
            param[5] = new SqlParameter("@PaymentTermsDesc5", argPaymentTerms.PaymentTermsDesc5);
            param[6] = new SqlParameter("@DayLimit", argPaymentTerms.DayLimit);
            param[7] = new SqlParameter("@ClientCode", argPaymentTerms.ClientCode);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePaymentTerms", param);


            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
            string strRetValue = Convert.ToString(param[10].Value);


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

        public ICollection<ErrorHandler> DeletePaymentTerms(string argPaymentTermsCode,string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@PaymentTermsCode", argPaymentTermsCode);
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

                int i = da.ExecuteNonQuery("Proc_DeletePaymentTerms", param);


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

        public bool blnIsPaymentTermsExists(string argPaymentTermsCode,string argClientCode)
        {
            bool IsPaymentTermsExists = false;
            DataSet ds = new DataSet();
            ds = GetPaymentTerms(argPaymentTermsCode,argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPaymentTermsExists = true;
            }
            else
            {
                IsPaymentTermsExists = false;
            }
            return IsPaymentTermsExists;
        }

        public bool blnIsPaymentTermsExists(string argPaymentTermsCode, string argClientCode, DataAccess da)
        {
            bool IsPaymentTermsExists = false;
            DataSet ds = new DataSet();
            ds = GetPaymentTerms(argPaymentTermsCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPaymentTermsExists = true;
            }
            else
            {
                IsPaymentTermsExists = false;
            }
            return IsPaymentTermsExists;
        }
    }
}