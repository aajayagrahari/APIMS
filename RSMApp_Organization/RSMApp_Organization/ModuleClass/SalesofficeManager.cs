
//Created On :: 07, May, 2012
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
    public class SalesofficeManager
    {
        const string SalesofficeTable = "Salesoffice";
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public Salesoffice objGetSalesoffice(string argSalesofficeCode, string argClientCode)
        {
            Salesoffice argSalesoffice = new Salesoffice();
            DataSet DataSetToFill = new DataSet();

            if (argSalesofficeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetSalesoffice(argSalesofficeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argSalesoffice = this.objCreateSalesoffice((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argSalesoffice;
        }
        
        public ICollection<Salesoffice> colGetSalesoffice(string argClientCode)
        {
            List<Salesoffice> lst = new List<Salesoffice>();
            DataSet DataSetToFill = new DataSet();
            Salesoffice tSalesoffice = new Salesoffice();
            DataSetToFill = this.GetSalesoffice(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSalesoffice(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetSalesOffice4Combo(string argPrefix, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetSalesOffice4Combo", param);
            return DataSetToFill;
        }

        public DataSet GetSalesoffice(string argSalesofficeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SalesofficeCode", argSalesofficeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesoffice4ID", param);
            return DataSetToFill;
        }

        public DataSet GetSalesoffice(string argSalesofficeCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SalesofficeCode", argSalesofficeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSalesoffice4ID", param);
            return DataSetToFill;
        }

        public DataSet GetSalesoffice(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + SalesofficeTable.ToString();

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
                    tSQL = tSQL + " " + sWhereClauseSTR + " ClientCode = '" + argClientCode +"'";
                }
                ds = da.FillDataSetWithSQL(tSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        
        public DataSet GetSalesoffice(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesoffice", param);
            return DataSetToFill;
        }
        
        private Salesoffice objCreateSalesoffice(DataRow dr)
        {
            Salesoffice tSalesoffice = new Salesoffice();
            tSalesoffice.SetObjectInfo(dr);
            return tSalesoffice;
        }
        
        public ICollection<ErrorHandler> SaveSalesoffice(Salesoffice argSalesoffice)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsSalesofficeExists(argSalesoffice.SalesofficeCode, argSalesoffice.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertSalesoffice(argSalesoffice, da, lstErr);
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
                    UpdateSalesoffice(argSalesoffice, da, lstErr);
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

        /*************/
        public void SaveSalesoffice(Salesoffice argSalesoffice, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsSalesofficeExists(argSalesoffice.SalesofficeCode, argSalesoffice.ClientCode, da) == false)
                {
                    InsertSalesoffice(argSalesoffice, da, lstErr);
                }
                else
                {
                    UpdateSalesoffice(argSalesoffice, da, lstErr);
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
            Salesoffice ObjSalesOffice = null;
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
                        ObjSalesOffice = new Salesoffice();
                        ObjSalesOffice.SalesofficeCode = Convert.ToString(drExcel["SalesOfficeCode"]).Trim();
                        ObjSalesOffice.SalesofficeName = Convert.ToString(drExcel["SalesofficeName"]).Trim();
                        ObjSalesOffice.Address1 = Convert.ToString(drExcel["Address1"]).Trim();
                        ObjSalesOffice.Address2 = Convert.ToString(drExcel["Address2"]).Trim();
                        ObjSalesOffice.CountryCode = Convert.ToString(drExcel["CountryCode"]).Trim();
                        ObjSalesOffice.StateCode = Convert.ToString(drExcel["StateCode"]).Trim();
                        ObjSalesOffice.City = Convert.ToString(drExcel["City"]).Trim();
                        ObjSalesOffice.PinCode = Convert.ToString(drExcel["PinCode"]).Trim();
                        ObjSalesOffice.TelNO = Convert.ToString(drExcel["TelNO"]).Trim();
                        ObjSalesOffice.EmailID = Convert.ToString(drExcel["EmailID"]).Trim();
                        ObjSalesOffice.CreatedBy = Convert.ToString(argUserName);
                        ObjSalesOffice.ModifiedBy = Convert.ToString(argUserName);
                        ObjSalesOffice.ClientCode = Convert.ToString(argClientCode);
                        SaveSalesoffice(ObjSalesOffice, da, lstErr);
                        
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
        /*************/        

        public void InsertSalesoffice(Salesoffice argSalesoffice, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@SalesofficeCode", argSalesoffice.SalesofficeCode);
            param[1] = new SqlParameter("@SalesofficeName", argSalesoffice.SalesofficeName);
            param[2] = new SqlParameter("@Address1", argSalesoffice.Address1);
            param[3] = new SqlParameter("@Address2", argSalesoffice.Address2);
            param[4] = new SqlParameter("@CountryCode", argSalesoffice.CountryCode);
            param[5] = new SqlParameter("@StateCode", argSalesoffice.StateCode);
            param[6] = new SqlParameter("@City", argSalesoffice.City);
            param[7] = new SqlParameter("@PinCode", argSalesoffice.PinCode);
            param[8] = new SqlParameter("@TelNO", argSalesoffice.TelNO);
            param[9] = new SqlParameter("@EmailID", argSalesoffice.EmailID);
            param[10] = new SqlParameter("@ClientCode", argSalesoffice.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argSalesoffice.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argSalesoffice.ModifiedBy);
            
            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSalesoffice", param);

            string strMessage = Convert.ToString(param[14].Value);
            string strType = Convert.ToString(param[13].Value);
            string strRetValue = Convert.ToString(param[15].Value);

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
        
        public void UpdateSalesoffice(Salesoffice argSalesoffice, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@SalesofficeCode", argSalesoffice.SalesofficeCode);
            param[1] = new SqlParameter("@SalesofficeName", argSalesoffice.SalesofficeName);
            param[2] = new SqlParameter("@Address1", argSalesoffice.Address1);
            param[3] = new SqlParameter("@Address2", argSalesoffice.Address2);
            param[4] = new SqlParameter("@CountryCode", argSalesoffice.CountryCode);
            param[5] = new SqlParameter("@StateCode", argSalesoffice.StateCode);
            param[6] = new SqlParameter("@City", argSalesoffice.City);
            param[7] = new SqlParameter("@PinCode", argSalesoffice.PinCode);
            param[8] = new SqlParameter("@TelNO", argSalesoffice.TelNO);
            param[9] = new SqlParameter("@EmailID", argSalesoffice.EmailID);
            param[10] = new SqlParameter("@ClientCode", argSalesoffice.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argSalesoffice.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argSalesoffice.ModifiedBy);
            
            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSalesoffice", param);

            string strMessage = Convert.ToString(param[14].Value);
            string strType = Convert.ToString(param[13].Value);
            string strRetValue = Convert.ToString(param[15].Value);

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
        
        public ICollection<ErrorHandler> DeleteSalesoffice(string argSalesofficeCode, int iIsDeleted, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@SalesofficeCode", argSalesofficeCode);
                param[1] = new SqlParameter("@IsDeleted", iIsDeleted);
                param[2] = new SqlParameter("@ClientCode", argClientCode);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteSalesoffice", param);

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
        
        public bool blnIsSalesofficeExists(string argSalesofficeCode, string argClientCode)
        {
            bool IsSalesofficeExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesoffice(argSalesofficeCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesofficeExists = true;
            }
            else
            {
                IsSalesofficeExists = false;
            }
            return IsSalesofficeExists;
        }

        public bool blnIsSalesofficeExists(string argSalesofficeCode, string argClientCode,DataAccess da)
        {
            bool IsSalesofficeExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesoffice(argSalesofficeCode, argClientCode,da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesofficeExists = true;
            }
            else
            {
                IsSalesofficeExists = false;
            }
            return IsSalesofficeExists;
        }
    }
}