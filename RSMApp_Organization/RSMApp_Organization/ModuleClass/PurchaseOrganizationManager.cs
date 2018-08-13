
//Created On :: 15, May, 2012
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
    public class PurchaseOrganizationManager
    {
        const string PurchaseOrganizationTable = "PurchaseOrganization";
        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public PurchaseOrganization objGetPurchaseOrganization(string argPurchaseOrgCode, string argClientCode)
        {
            PurchaseOrganization argPurchaseOrganization = new PurchaseOrganization();
            DataSet DataSetToFill = new DataSet();

            if (argPurchaseOrgCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetPurchaseOrganization(argPurchaseOrgCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argPurchaseOrganization = this.objCreatePurchaseOrganization((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argPurchaseOrganization;
        }

        public ICollection<PurchaseOrganization> colGetPurchaseOrganization(string argClientCode)
        {
            List<PurchaseOrganization> lst = new List<PurchaseOrganization>();
            DataSet DataSetToFill = new DataSet();
            PurchaseOrganization tPurchaseOrganization = new PurchaseOrganization();
            DataSetToFill = this.GetPurchaseOrganization(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePurchaseOrganization(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetPurchaseOrganization(string argPurchaseOrgCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrgCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPurchaseOrganization4ID", param);
            return DataSetToFill;
        }

        public DataSet GetPurchaseOrganization(string argPurchaseOrgCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrgCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPurchaseOrganization4ID", param);
            return DataSetToFill;
        }

        public DataSet GetPurchaseOrganization(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPurchaseOrganization",param);
            return DataSetToFill;
        }

        public DataSet GetPurchaseOrganization(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + PurchaseOrganizationTable.ToString();

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

        private PurchaseOrganization objCreatePurchaseOrganization(DataRow dr)
        {
            PurchaseOrganization tPurchaseOrganization = new PurchaseOrganization();
            tPurchaseOrganization.SetObjectInfo(dr);
            return tPurchaseOrganization;
        }
        
        public ICollection<ErrorHandler> SavePurchaseOrganization(PurchaseOrganization argPurchaseOrganization)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPurchaseOrganizationExists(argPurchaseOrganization.PurchaseOrgCode, argPurchaseOrganization.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPurchaseOrganization(argPurchaseOrganization, da, lstErr);
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
                    UpdatePurchaseOrganization(argPurchaseOrganization, da, lstErr);
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
        public void SavePurchaseOrganization(PurchaseOrganization argPurchaseOrganization, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsPurchaseOrganizationExists(argPurchaseOrganization.PurchaseOrgCode, argPurchaseOrganization.ClientCode, da) == false)
                {
                    InsertPurchaseOrganization(argPurchaseOrganization, da, lstErr);
                }
                else
                {
                    UpdatePurchaseOrganization(argPurchaseOrganization, da, lstErr);
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
            PurchaseOrganization ObjPurchaseOrganization = null;
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
                        ObjPurchaseOrganization = new PurchaseOrganization();
                        ObjPurchaseOrganization.PurchaseOrgCode = Convert.ToString(drExcel["PurchaseOrgCode"]).Trim();
                        ObjPurchaseOrganization.PurchaseOrgName = Convert.ToString(drExcel["PurchaseOrgName"]).Trim();
                        ObjPurchaseOrganization.Address1 = Convert.ToString(drExcel["Address1"]).Trim();
                        ObjPurchaseOrganization.Address2 = Convert.ToString(drExcel["Address2"]).Trim();
                        ObjPurchaseOrganization.CountryCode = Convert.ToString(drExcel["CountryCode"]).Trim();
                        ObjPurchaseOrganization.StateCode = Convert.ToString(drExcel["StateCode"]).Trim();
                        ObjPurchaseOrganization.City = Convert.ToString(drExcel["City"]).Trim();
                        ObjPurchaseOrganization.PinCode = Convert.ToString(drExcel["PinCode"]).Trim();
                        ObjPurchaseOrganization.TelNO = Convert.ToString(drExcel["TelNO"]).Trim();
                        ObjPurchaseOrganization.EmailID = Convert.ToString(drExcel["EmailID"]).Trim();
                        ObjPurchaseOrganization.CurrencyCode = Convert.ToString(drExcel["CurrencyCode"]).Trim();
                        ObjPurchaseOrganization.CreatedBy = Convert.ToString(argUserName);
                        ObjPurchaseOrganization.ModifiedBy = Convert.ToString(argUserName);
                        ObjPurchaseOrganization.ClientCode = Convert.ToString(argClientCode);
                        SavePurchaseOrganization(ObjPurchaseOrganization, da, lstErr);

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

        public void InsertPurchaseOrganization(PurchaseOrganization argPurchaseOrganization, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrganization.PurchaseOrgCode);
            param[1] = new SqlParameter("@PurchaseOrgName", argPurchaseOrganization.PurchaseOrgName);
            param[2] = new SqlParameter("@Address1", argPurchaseOrganization.Address1);
            param[3] = new SqlParameter("@Address2", argPurchaseOrganization.Address2);
            param[4] = new SqlParameter("@CountryCode", argPurchaseOrganization.CountryCode);
            param[5] = new SqlParameter("@StateCode", argPurchaseOrganization.StateCode);
            param[6] = new SqlParameter("@City", argPurchaseOrganization.City);
            param[7] = new SqlParameter("@PinCode", argPurchaseOrganization.PinCode);
            param[8] = new SqlParameter("@TelNO", argPurchaseOrganization.TelNO);
            param[9] = new SqlParameter("@EmailID", argPurchaseOrganization.EmailID);
            param[10] = new SqlParameter("@ClientCode", argPurchaseOrganization.ClientCode);
            param[11] = new SqlParameter("@CurrencyCode", argPurchaseOrganization.CurrencyCode);
            param[12] = new SqlParameter("@CreatedBy", argPurchaseOrganization.CreatedBy);
            param[13] = new SqlParameter("@ModifiedBy", argPurchaseOrganization.ModifiedBy);
            
            param[14] = new SqlParameter("@Type", SqlDbType.Char);
            param[14].Size = 1;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[15].Size = 255;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[16].Size = 20;
            param[16].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPurchaseOrganization", param);

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

        public void UpdatePurchaseOrganization(PurchaseOrganization argPurchaseOrganization, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrganization.PurchaseOrgCode);
            param[1] = new SqlParameter("@PurchaseOrgName", argPurchaseOrganization.PurchaseOrgName);
            param[2] = new SqlParameter("@Address1", argPurchaseOrganization.Address1);
            param[3] = new SqlParameter("@Address2", argPurchaseOrganization.Address2);
            param[4] = new SqlParameter("@CountryCode", argPurchaseOrganization.CountryCode);
            param[5] = new SqlParameter("@StateCode", argPurchaseOrganization.StateCode);
            param[6] = new SqlParameter("@City", argPurchaseOrganization.City);
            param[7] = new SqlParameter("@PinCode", argPurchaseOrganization.PinCode);
            param[8] = new SqlParameter("@TelNO", argPurchaseOrganization.TelNO);
            param[9] = new SqlParameter("@EmailID", argPurchaseOrganization.EmailID);
            param[10] = new SqlParameter("@ClientCode", argPurchaseOrganization.ClientCode);
            param[11] = new SqlParameter("@CurrencyCode", argPurchaseOrganization.CurrencyCode);
            param[12] = new SqlParameter("@CreatedBy", argPurchaseOrganization.CreatedBy);
            param[13] = new SqlParameter("@ModifiedBy", argPurchaseOrganization.ModifiedBy);
           
            param[14] = new SqlParameter("@Type", SqlDbType.Char);
            param[14].Size = 1;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[15].Size = 255;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[16].Size = 20;
            param[16].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePurchaseOrganization", param);

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
        
        public ICollection<ErrorHandler> DeletePurchaseOrganization(string argPurchaseOrgCode, string argClientCode,int IisDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrgCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted", IisDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeletePurchaseOrganization", param);

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

        public bool blnIsPurchaseOrganizationExists(string argPurchaseOrgCode, string argClientCode)
        {
            bool IsPurchaseOrganizationExists = false;
            DataSet ds = new DataSet();
            ds = GetPurchaseOrganization(argPurchaseOrgCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPurchaseOrganizationExists = true;
            }
            else
            {
                IsPurchaseOrganizationExists = false;
            }
            return IsPurchaseOrganizationExists;
        }

        public bool blnIsPurchaseOrganizationExists(string argPurchaseOrgCode, string argClientCode, DataAccess da)
        {
            bool IsPurchaseOrganizationExists = false;
            DataSet ds = new DataSet();
            ds = GetPurchaseOrganization(argPurchaseOrgCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPurchaseOrganizationExists = true;
            }
            else
            {
                IsPurchaseOrganizationExists = false;
            }
            return IsPurchaseOrganizationExists;
        }
    }
}