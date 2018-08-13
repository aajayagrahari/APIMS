
//Created On :: 18, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_Organization
{
    public class CreditControl_CustomerManager
    {
        const string CreditControl_CustomerTable = "CreditControl_Customer";
        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CreditControl_Customer objGetCreditControl_Customer(string argCrContAreaCode, string argCustomerCode, string argSalesOrganizationCode, string argClientCode)
        {
            CreditControl_Customer argCreditControl_Customer = new CreditControl_Customer();
            DataSet DataSetToFill = new DataSet();

            if (argCrContAreaCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argCustomerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argSalesOrganizationCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetCreditControl_Customer(argCrContAreaCode, argCustomerCode, argSalesOrganizationCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argCreditControl_Customer = this.objCreateCreditControl_Customer((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argCreditControl_Customer;
        }

        public ICollection<CreditControl_Customer> colGetCreditControl_Customer(DataTable dt, string argUserName, string clientCode)
        {
            List<CreditControl_Customer> lst = new List<CreditControl_Customer>();
            CreditControl_Customer objCreditControl_Customer = null;
            foreach (DataRow dr in dt.Rows)
            {
                objCreditControl_Customer = new CreditControl_Customer();
                objCreditControl_Customer.CrContAreaCode = Convert.ToString(dr["CrContAreaCode"]).Trim();
                objCreditControl_Customer.CustomerCode = Convert.ToString(dr["CustomerCode"]).Trim();
                objCreditControl_Customer.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]).Trim();
                objCreditControl_Customer.CurrencyCode = Convert.ToString(dr["CurrencyCode"]).Trim();
                objCreditControl_Customer.CRUpdateCode = Convert.ToString(dr["CRUpdateCode"]).Trim();
                objCreditControl_Customer.FiscalYearType = Convert.ToString(dr["FiscalYearType"]).Trim();
                objCreditControl_Customer.RiskCategoryCode = Convert.ToString(dr["RiskCategoryCode"]).Trim();
                objCreditControl_Customer.CreditLimit = Convert.ToInt32(dr["CreditLimit"]);
                objCreditControl_Customer.CreatedBy = Convert.ToString(argUserName);
                objCreditControl_Customer.ModifiedBy = Convert.ToString(argUserName);
                objCreditControl_Customer.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objCreditControl_Customer);
            }
            return lst;
        }

        public DataSet GetCreditControl_Customer(string argCrContAreaCode, string argCustomerCode, string argSalesOrganizationCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CrContAreaCode", argCrContAreaCode);
            param[1] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[2] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCreditControl_Customer4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCreditControl_Customer(string argCrContAreaCode, string argCustomerCode, string argSalesOrganizationCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CrContAreaCode", argCrContAreaCode);
            param[1] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[2] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCreditControl_Customer4ID", param);
            return DataSetToFill;
        }
        
        public DataSet GetCreditControl_Customer(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCreditControl_Customer",param);
            return DataSetToFill;
        }

        private CreditControl_Customer objCreateCreditControl_Customer(DataRow dr)
        {
            CreditControl_Customer tCreditControl_Customer = new CreditControl_Customer();
            tCreditControl_Customer.SetObjectInfo(dr);
            return tCreditControl_Customer;
        }

        public ICollection<ErrorHandler> SaveCreditControl_Customer(CreditControl_Customer argCreditControl_Customer)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCreditControl_CustomerExists(argCreditControl_Customer.CrContAreaCode, argCreditControl_Customer.CustomerCode, argCreditControl_Customer.SalesOrganizationCode, argCreditControl_Customer.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCreditControl_Customer(argCreditControl_Customer, da, lstErr);
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
                    UpdateCreditControl_Customer(argCreditControl_Customer, da, lstErr);
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
        public ICollection<ErrorHandler> SaveCreditControl_Customer(ICollection<CreditControl_Customer> colGetCreditControl_Customer, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (CreditControl_Customer argCreditControl_Customer in colGetCreditControl_Customer)
                {
                    if (argCreditControl_Customer.IsDeleted == 0)
                    {
                        if (blnIsCreditControl_CustomerExists(argCreditControl_Customer.CrContAreaCode, argCreditControl_Customer.CustomerCode, argCreditControl_Customer.SalesOrganizationCode, argCreditControl_Customer.ClientCode, da) == false)
                        {
                            InsertCreditControl_Customer(argCreditControl_Customer, da, lstErr);
                        }
                        else
                        {
                            UpdateCreditControl_Customer(argCreditControl_Customer, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteCreditControl_Customer(argCreditControl_Customer.CrContAreaCode, argCreditControl_Customer.CustomerCode, argCreditControl_Customer.SalesOrganizationCode, argCreditControl_Customer.ClientCode, argCreditControl_Customer.IsDeleted);
                    }
                }

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

        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
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
                    SaveCreditControl_Customer(colGetCreditControl_Customer(dtExcel, argUserName, argClientCode), lstErr);

                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            break;

                        }
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
        /**********/

        public void InsertCreditControl_Customer(CreditControl_Customer argCreditControl_Customer, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@CrContAreaCode", argCreditControl_Customer.CrContAreaCode);
            param[1] = new SqlParameter("@CustomerCode", argCreditControl_Customer.CustomerCode);
            param[2] = new SqlParameter("@SalesOrganizationCode", argCreditControl_Customer.SalesOrganizationCode);
            param[3] = new SqlParameter("@CurrencyCode", argCreditControl_Customer.CurrencyCode);
            param[4] = new SqlParameter("@CRUpdateCode", argCreditControl_Customer.CRUpdateCode);
            param[5] = new SqlParameter("@FiscalYearType", argCreditControl_Customer.FiscalYearType);
            param[6] = new SqlParameter("@RiskCategoryCode", argCreditControl_Customer.RiskCategoryCode);
            param[7] = new SqlParameter("@CreditLimit", argCreditControl_Customer.CreditLimit);
            param[8] = new SqlParameter("@ClientCode", argCreditControl_Customer.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argCreditControl_Customer.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argCreditControl_Customer.ModifiedBy);
            
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCreditControl_Customer", param);

            string strMessage = Convert.ToString(param[12].Value);
            string strType = Convert.ToString(param[11].Value);
            string strRetValue = Convert.ToString(param[13].Value);

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

        public void UpdateCreditControl_Customer(CreditControl_Customer argCreditControl_Customer, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@CrContAreaCode", argCreditControl_Customer.CrContAreaCode);
            param[1] = new SqlParameter("@CustomerCode", argCreditControl_Customer.CustomerCode);
            param[2] = new SqlParameter("@SalesOrganizationCode", argCreditControl_Customer.SalesOrganizationCode);
            param[3] = new SqlParameter("@CurrencyCode", argCreditControl_Customer.CurrencyCode);
            param[4] = new SqlParameter("@CRUpdateCode", argCreditControl_Customer.CRUpdateCode);
            param[5] = new SqlParameter("@FiscalYearType", argCreditControl_Customer.FiscalYearType);
            param[6] = new SqlParameter("@RiskCategoryCode", argCreditControl_Customer.RiskCategoryCode);
            param[7] = new SqlParameter("@CreditLimit", argCreditControl_Customer.CreditLimit);
            param[8] = new SqlParameter("@ClientCode", argCreditControl_Customer.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argCreditControl_Customer.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argCreditControl_Customer.ModifiedBy);
            
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCreditControl_Customer", param);

            string strMessage = Convert.ToString(param[12].Value);
            string strType = Convert.ToString(param[11].Value);
            string strRetValue = Convert.ToString(param[13].Value);

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

        public ICollection<ErrorHandler> DeleteCreditControl_Customer(string argCrContAreaCode, string argCustomerCode, string argSalesOrganizationCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@CrContAreaCode", argCrContAreaCode);
                param[1] = new SqlParameter("@CustomerCode", argCustomerCode);
                param[2] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted",iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteCreditControl_Customer", param);
                
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

        public bool blnIsCreditControl_CustomerExists(string argCrContAreaCode, string argCustomerCode, string argSalesOrganizationCode, string argClientCode)
        {
            bool IsCreditControl_CustomerExists = false;
            DataSet ds = new DataSet();
            ds = GetCreditControl_Customer(argCrContAreaCode, argCustomerCode, argSalesOrganizationCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCreditControl_CustomerExists = true;
            }
            else
            {
                IsCreditControl_CustomerExists = false;
            }
            return IsCreditControl_CustomerExists;
        }

        public bool blnIsCreditControl_CustomerExists(string argCrContAreaCode, string argCustomerCode, string argSalesOrganizationCode, string argClientCode, DataAccess da)
        {
            bool IsCreditControl_CustomerExists = false;
            DataSet ds = new DataSet();
            ds = GetCreditControl_Customer(argCrContAreaCode, argCustomerCode, argSalesOrganizationCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCreditControl_CustomerExists = true;
            }
            else
            {
                IsCreditControl_CustomerExists = false;
            }
            return IsCreditControl_CustomerExists;
        }
    }
}