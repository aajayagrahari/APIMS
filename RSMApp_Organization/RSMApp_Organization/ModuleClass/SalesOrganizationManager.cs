    
//Created On :: 07, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Data.OleDb;

namespace RSMApp_Organization
{
    public class SalesOrganizationManager
    {
        const string SalesOrganizationTable = "SalesOrganization";
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public SalesOrganization objGetSalesOrganization(string argSalesOrganizationCode, string argClientCode)
        {
            SalesOrganization argSalesOrganization = new SalesOrganization();
            DataSet DataSetToFill = new DataSet();

            if (argSalesOrganizationCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetSalesOrganization(argSalesOrganizationCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argSalesOrganization = this.objCreateSalesOrganization((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argSalesOrganization;
        }
        
        public ICollection<SalesOrganization> colGetSalesOrganization(string argClientCode)
        {
            List<SalesOrganization> lst = new List<SalesOrganization>();
            DataSet DataSetToFill = new DataSet();
            SalesOrganization tSalesOrganization = new SalesOrganization();
            DataSetToFill = this.GetSalesOrganization(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSalesOrganization(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }
        
        public DataSet GetSalesOrganisation4Combo(string argPrefix,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetSalesOrganization4Combo", param);
            return DataSetToFill;
        }

        public DataSet GetSalesOrganization(string argSalesOrganizationCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            
            DataSetToFill = da.FillDataSet("SP_GetSalesOrganization4ID", param);
            return DataSetToFill;
        }

        /******/
        public DataSet GetSalesOrganization(string argSalesOrganizationCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSalesOrganization4ID", param);
            return DataSetToFill;
        }
        /*****/

        public DataSet GetSalesOrganization(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + SalesOrganizationTable.ToString();

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
        
        public DataSet GetSalesOrganization(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrganization", param);
            return DataSetToFill;
        }
        
        private SalesOrganization objCreateSalesOrganization(DataRow dr)
        {
            SalesOrganization tSalesOrganization = new SalesOrganization();
            tSalesOrganization.SetObjectInfo(dr);
            return tSalesOrganization;
        }
        
        public ICollection<ErrorHandler> SaveSalesOrganization(SalesOrganization argSalesOrganization)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsSalesOrganizationExists(argSalesOrganization.SalesOrganizationCode, argSalesOrganization.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertSalesOrganization(argSalesOrganization, da, lstErr);
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
                    UpdateSalesOrganization(argSalesOrganization, da, lstErr);
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
        public void SaveSalesOrganization(SalesOrganization argSalesOrganization, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsSalesOrganizationExists(argSalesOrganization.SalesOrganizationCode, argSalesOrganization.ClientCode, da) == false)
                {
                    InsertSalesOrganization(argSalesOrganization, da, lstErr);
                }
                else
                {
                    UpdateSalesOrganization(argSalesOrganization, da, lstErr);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
            SalesOrganization ObjSalesOrganization = null;
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
                        ObjSalesOrganization = new SalesOrganization();
                        ObjSalesOrganization.SalesOrganizationCode = Convert.ToString(drExcel["SalesOrganizationCode"]).Trim();
                        ObjSalesOrganization.SalesOrgName = Convert.ToString(drExcel["SalesOrgName"]).Trim();
                        ObjSalesOrganization.Address1 = Convert.ToString(drExcel["Address1"]).Trim();
                        ObjSalesOrganization.Address2 = Convert.ToString(drExcel["Address2"]).Trim();
                        ObjSalesOrganization.CountryCode = Convert.ToString(drExcel["CountryCode"]).Trim();
                        ObjSalesOrganization.StateCode = Convert.ToString(drExcel["StateCode"]).Trim();
                        ObjSalesOrganization.City = Convert.ToString(drExcel["City"]).Trim();
                        ObjSalesOrganization.PinCode = Convert.ToString(drExcel["PinCode"]).Trim();
                        ObjSalesOrganization.TelNO = Convert.ToString(drExcel["TelNO"]).Trim();
                        ObjSalesOrganization.EmailID = Convert.ToString(drExcel["EmailID"]).Trim();
                        ObjSalesOrganization.CurrencyCode = Convert.ToString(drExcel["CurrencyCode"]).Trim();
                        ObjSalesOrganization.CreatedBy = Convert.ToString(argUserName);
                        ObjSalesOrganization.ModifiedBy = Convert.ToString(argUserName);
                        ObjSalesOrganization.ClientCode = Convert.ToString(argClientCode);
                        SaveSalesOrganization(ObjSalesOrganization, da, lstErr);

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

        public void InsertSalesOrganization(SalesOrganization argSalesOrganization, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganization.SalesOrganizationCode);
            param[1] = new SqlParameter("@SalesOrgName", argSalesOrganization.SalesOrgName);
            param[2] = new SqlParameter("@Address1", argSalesOrganization.Address1);
            param[3] = new SqlParameter("@Address2", argSalesOrganization.Address2);
            param[4] = new SqlParameter("@CountryCode", argSalesOrganization.CountryCode);
            param[5] = new SqlParameter("@StateCode", argSalesOrganization.StateCode);
            param[6] = new SqlParameter("@CurrencyCode", argSalesOrganization.CurrencyCode);
            param[7] = new SqlParameter("@City", argSalesOrganization.City);
            param[8] = new SqlParameter("@PinCode", argSalesOrganization.PinCode);
            param[9] = new SqlParameter("@TelNO", argSalesOrganization.TelNO);
            param[10] = new SqlParameter("@EmailID", argSalesOrganization.EmailID);
            param[11] = new SqlParameter("@ClientCode", argSalesOrganization.ClientCode);
            param[12] = new SqlParameter("@CreatedBy", argSalesOrganization.CreatedBy);
            param[13] = new SqlParameter("@ModifiedBy", argSalesOrganization.ModifiedBy);
            
            param[14] = new SqlParameter("@Type", SqlDbType.Char);
            param[14].Size = 1;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[15].Size = 255;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[16].Size = 20;
            param[16].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSalesOrganization", param);

            string strMessage = Convert.ToString(param[15].Value);
            string strType = Convert.ToString(param[14].Value);
            string strRetValue = Convert.ToString(param[16].Value);

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
        
        public void UpdateSalesOrganization(SalesOrganization argSalesOrganization, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganization.SalesOrganizationCode);
            param[1] = new SqlParameter("@SalesOrgName", argSalesOrganization.SalesOrgName);
            param[2] = new SqlParameter("@Address1", argSalesOrganization.Address1);
            param[3] = new SqlParameter("@Address2", argSalesOrganization.Address2);
            param[4] = new SqlParameter("@CountryCode", argSalesOrganization.CountryCode);
            param[5] = new SqlParameter("@StateCode", argSalesOrganization.StateCode);
            param[6] = new SqlParameter("@CurrencyCode", argSalesOrganization.CurrencyCode);
            param[7] = new SqlParameter("@City", argSalesOrganization.City);
            param[8] = new SqlParameter("@PinCode", argSalesOrganization.PinCode);
            param[9] = new SqlParameter("@TelNO", argSalesOrganization.TelNO);
            param[10] = new SqlParameter("@EmailID", argSalesOrganization.EmailID);
            param[11] = new SqlParameter("@ClientCode", argSalesOrganization.ClientCode);
            param[12] = new SqlParameter("@CreatedBy", argSalesOrganization.CreatedBy);
            param[13] = new SqlParameter("@ModifiedBy", argSalesOrganization.ModifiedBy);
            
            param[14] = new SqlParameter("@Type", SqlDbType.Char);
            param[14].Size = 1;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[15].Size = 255;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[16].Size = 20;
            param[16].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSalesOrganization", param);

            string strMessage = Convert.ToString(param[15].Value);
            string strType = Convert.ToString(param[14].Value);
            string strRetValue = Convert.ToString(param[16].Value);

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

        public ICollection<ErrorHandler> DeleteSalesOrganization(string argSalesOrganizationCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteSalesOrganization", param);

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
        
        public bool blnIsSalesOrganizationExists(string argSalesOrganizationCode, string argClientCode)
        {
            bool IsSalesOrganizationExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesOrganization(argSalesOrganizationCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesOrganizationExists = true;
            }
            else
            {
                IsSalesOrganizationExists = false;
            }
            return IsSalesOrganizationExists;
        }

        /*****/
        public bool blnIsSalesOrganizationExists(string argSalesOrganizationCode, string argClientCode, DataAccess da)
        {
            bool IsSalesOrganizationExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesOrganization(argSalesOrganizationCode, argClientCode, da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesOrganizationExists = true;
            }
            else
            {
                IsSalesOrganizationExists = false;
            }
            return IsSalesOrganizationExists;
        }
    }
}