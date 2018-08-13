
//Created On :: 18, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_SD
{
    public class CustomerManager
    {
        const string CustomerTable = "Customer";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        Customer_PartnerFunctionManager objCustomer_PartnerFunctionManager =new Customer_PartnerFunctionManager();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public Customer objGetCustomer(string argCustomerCode, string argClientCode)
        {
            Customer argCustomer = new Customer();
            DataSet DataSetToFill = new DataSet();

            if (argCustomerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            
            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCustomer(argCustomerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCustomer = this.objCreateCustomer((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCustomer;
        }
        
        public ICollection<Customer> colGetCustomer(string argClientCode)
        {
            List<Customer> lst = new List<Customer>();
            DataSet DataSetToFill = new DataSet();
            Customer tCustomer = new Customer();

            DataSetToFill = this.GetCustomer(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCustomer(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetCustomer4Combo(string argPrefix, string argSalesOfficeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@SalesOfficeCode", argSalesOfficeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCustomer4Combo", param);

            return DataSetToFill;
        }

        public DataSet GetCustomer4Combo(string argPrefix, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCustomer4Payment", param);

            return DataSetToFill;
        }

        public DataSet GetCustomer(string argCustomerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCustomer4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCustomerDetail4SO(string argCustomerCode, string argSalesOrganizationCode,string argDistChannelCode,string argDivisionCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[2] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[3] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCustomerDetail4SO", param);

            return DataSetToFill;
        }

        public DataSet GetCustomer(string argCustomerCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCustomer4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCustomer(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetCustomer",param);
            return DataSetToFill;
        }

        public DataSet GetCustomer(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT top(100)CustomerCode,(Name1) as CustomerName,CurrencyCode, City FROM " + CustomerTable.ToString();

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

        private Customer objCreateCustomer(DataRow dr)
        {
            Customer tCustomer = new Customer();

            tCustomer.SetObjectInfo(dr);

            return tCustomer;

        }

        public ICollection<ErrorHandler> SaveCustomer(Customer argCustomer)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            
            try
            {
                
                if (blnIsCustomerExists(argCustomer.CustomerCode, argCustomer.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCustomer(argCustomer, da, lstErr);
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
                    UpdateCustomer(argCustomer, da, lstErr);
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

        public ICollection<ErrorHandler> SaveCustomer(Customer argCustomer, DataTable dtCustomer_Partner)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            string strretValue = "";
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsCustomerExists(argCustomer.CustomerCode, argCustomer.ClientCode, da) == false)
                {
                    strretValue = InsertCustomer(argCustomer, da, lstErr);
                }
                else
                {
                    strretValue = UpdateCustomer(argCustomer, da, lstErr);
                }

                foreach (ErrorHandler objerr in lstErr)
                {
                    if (objerr.Type == "E")
                    {
                        da.ROLLBACK_TRANSACTION();
                        return lstErr;
                    }

                    if (objerr.Type == "A")
                    {
                        da.ROLLBACK_TRANSACTION();
                        return lstErr;
                    }
                }

                if (strretValue != "")
                {
                    if (dtCustomer_Partner.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCustomer_Partner.Rows)
                        {
                            if (Convert.ToInt32(dr["IsDeleted"]) == 0)
                            {

                                Customer_PartnerFunction objCustomer_Partner = new Customer_PartnerFunction();

                                objCustomer_Partner.CustomerCode = strretValue.Trim();
                                objCustomer_Partner.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]).Trim();
                                objCustomer_Partner.DivisionCode = Convert.ToString(dr["DivisionCode"]).Trim();
                                objCustomer_Partner.DistChannelCode = Convert.ToString(dr["DistChannelCode"]).Trim();
                                objCustomer_Partner.PFunctionCode = Convert.ToString(dr["PFunctionCode"]).Trim();
                                objCustomer_Partner.PartnerCounter = Convert.ToInt32(dr["PartnerCounter"]);
                                objCustomer_Partner.PartnerTable = Convert.ToString(dr["PartnerTable"]).Trim();
                                objCustomer_Partner.PartnerCode = Convert.ToString(dr["PartnerCode"]).Trim();
                                objCustomer_Partner.ClientCode = Convert.ToString(dr["ClientCode"]).Trim();
                                objCustomer_Partner.CreatedBy = Convert.ToString(dr["CreatedBy"]).Trim();
                                objCustomer_Partner.ModifiedBy = Convert.ToString(dr["ModifiedBy"]).Trim();

                                objCustomer_PartnerFunctionManager.SaveCustomer_PartnerFunction(objCustomer_Partner, da, lstErr);
                            }
                            else
                            {
                                objCustomer_PartnerFunctionManager.DeleteCustomer_PartnerFunction(strretValue.ToString().Trim(), Convert.ToString(dr["PFunctionCode"]).Trim(), Convert.ToString(dr["ClientCode"]).Trim(), 1,da);
                            }
                           
                        }

                        foreach (ErrorHandler objerr in lstErr)
                        {
                            if (objerr.Type == "E")
                            {
                                da.ROLLBACK_TRANSACTION();
                                return lstErr;
                            }

                            if (objerr.Type == "A")
                            {
                                da.ROLLBACK_TRANSACTION();
                                return lstErr;
                            }
                        }
                    }

                   
             
                //}
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

        public void SaveCustomer(Customer argCustomer, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCustomerExists(argCustomer.CustomerCode, argCustomer.ClientCode, da) == false)
                {
                    InsertCustomer(argCustomer, da, lstErr);
                }
                else
                {
                    UpdateCustomer(argCustomer, da, lstErr);
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
            Customer ObjCustomer = null;
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
                        ObjCustomer = new Customer();
                        ObjCustomer.CustomerCode = Convert.ToString(drExcel["CustomerCode"]).Trim();
                        ObjCustomer.Name1 = Convert.ToString(drExcel["Name1"]).Trim();
                        ObjCustomer.Name2 = Convert.ToString(drExcel["Name2"]).Trim();
                        ObjCustomer.CustomerAccTypeCode = Convert.ToString(drExcel["CustomerAccTypeCode"]).Trim();
                        ObjCustomer.Address1 = Convert.ToString(drExcel["Address1"]).Trim();
                        ObjCustomer.Address2 = Convert.ToString(drExcel["Address2"]).Trim();
                        ObjCustomer.CountryCode = Convert.ToString(drExcel["CountryCode"]).Trim();
                        ObjCustomer.StateCode = Convert.ToString(drExcel["StateCode"]).Trim();
                        ObjCustomer.City = Convert.ToString(drExcel["City"]).Trim();
                        ObjCustomer.PinCode = Convert.ToString(drExcel["PinCode"]).Trim();
                        ObjCustomer.TelNo = Convert.ToString(drExcel["TelNo"]).Trim();
                        ObjCustomer.FaxNo = Convert.ToString(drExcel["FaxNo"]).Trim();
                        ObjCustomer.EmailID = Convert.ToString(drExcel["EmailID"]).Trim();
                        ObjCustomer.Title = Convert.ToString(drExcel["Title"]).Trim();
                        ObjCustomer.ContactPerson = Convert.ToString(drExcel["ContactPerson"]).Trim();
                        ObjCustomer.VATNo = Convert.ToString(drExcel["VATNo"]).Trim();
                        ObjCustomer.TransportZoneCode = Convert.ToString(drExcel["TransportZoneCode"]).Trim();
                        ObjCustomer.CustomerClassCode = Convert.ToString(drExcel["CustomerClassCode"]).Trim();
                        ObjCustomer.IndustryName = Convert.ToString(drExcel["IndustryName"]).Trim();
                        ObjCustomer.AnnualSales = Convert.ToInt32(drExcel["AnnualSales"]);
                        ObjCustomer.AnnualSalesCurrency = Convert.ToString(drExcel["AnnualSalesCurrency"]).Trim();
                        ObjCustomer.TotalEmployees = Convert.ToInt32(drExcel["TotalEmployees"]);
                        ObjCustomer.CurrencyCode = Convert.ToString(drExcel["CurrencyCode"]).Trim();
                        ObjCustomer.CreatedBy = Convert.ToString(argUserName);
                        ObjCustomer.ModifiedBy = Convert.ToString(argUserName);
                        ObjCustomer.ClientCode = Convert.ToString(argClientCode);
                        SaveCustomer(ObjCustomer, da, lstErr);

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

        public string InsertCustomer(Customer argCustomer, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[29];
            param[0] = new SqlParameter("@CustomerCode", argCustomer.CustomerCode);
            param[1] = new SqlParameter("@Name1", argCustomer.Name1);
            param[2] = new SqlParameter("@Name2", argCustomer.Name2);
            param[3] = new SqlParameter("@CustomerAccTypeCode", argCustomer.CustomerAccTypeCode);
            param[4] = new SqlParameter("@Address1", argCustomer.Address1);
            param[5] = new SqlParameter("@Address2", argCustomer.Address2);
            param[6] = new SqlParameter("@CountryCode", argCustomer.CountryCode);
            param[7] = new SqlParameter("@StateCode", argCustomer.StateCode);
            param[8] = new SqlParameter("@City", argCustomer.City);
            param[9] = new SqlParameter("@PinCode", argCustomer.PinCode);
            param[10] = new SqlParameter("@TelNo", argCustomer.TelNo);
            param[11] = new SqlParameter("@FaxNo", argCustomer.FaxNo);
            param[12] = new SqlParameter("@EmailID", argCustomer.EmailID);
            param[13] = new SqlParameter("@Title", argCustomer.Title);
            param[14] = new SqlParameter("@ContactPerson", argCustomer.ContactPerson);
            param[15] = new SqlParameter("@VATNo", argCustomer.VATNo);
            param[16] = new SqlParameter("@TransportZoneCode", argCustomer.TransportZoneCode);
            param[17] = new SqlParameter("@CustomerClassCode", argCustomer.CustomerClassCode);
            param[18] = new SqlParameter("@IndustryName", argCustomer.IndustryName);
            param[19] = new SqlParameter("@AnnualSales", argCustomer.AnnualSales);
            param[20] = new SqlParameter("@AnnualSalesCurrency", argCustomer.AnnualSalesCurrency);
            param[21] = new SqlParameter("@TotalEmployees", argCustomer.TotalEmployees);
            param[22] = new SqlParameter("@CurrencyCode", argCustomer.CurrencyCode);
            param[23] = new SqlParameter("@ClientCode", argCustomer.ClientCode);
            param[24] = new SqlParameter("@CreatedBy", argCustomer.CreatedBy);
            param[25] = new SqlParameter("@ModifiedBy", argCustomer.ModifiedBy);

            param[26] = new SqlParameter("@Type", SqlDbType.Char);
            param[26].Size = 1;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[27].Size = 255;
            param[27].Direction = ParameterDirection.Output;

            param[28] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[28].Size = 20;
            param[28].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCustomer", param);

            string strMessage = Convert.ToString(param[27].Value);
            string strType = Convert.ToString(param[26].Value);
            string strRetValue = Convert.ToString(param[28].Value);
            
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

            return strRetValue;
        }

        public string UpdateCustomer(Customer argCustomer, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[29];
            param[0] = new SqlParameter("@CustomerCode", argCustomer.CustomerCode);
            param[1] = new SqlParameter("@Name1", argCustomer.Name1);
            param[2] = new SqlParameter("@Name2", argCustomer.Name2);
            param[3] = new SqlParameter("@CustomerAccTypeCode", argCustomer.CustomerAccTypeCode);
            param[4] = new SqlParameter("@Address1", argCustomer.Address1);
            param[5] = new SqlParameter("@Address2", argCustomer.Address2);
            param[6] = new SqlParameter("@CountryCode", argCustomer.CountryCode);
            param[7] = new SqlParameter("@StateCode", argCustomer.StateCode);
            param[8] = new SqlParameter("@City", argCustomer.City);
            param[9] = new SqlParameter("@PinCode", argCustomer.PinCode);
            param[10] = new SqlParameter("@TelNo", argCustomer.TelNo);
            param[11] = new SqlParameter("@FaxNo", argCustomer.FaxNo);
            param[12] = new SqlParameter("@EmailID", argCustomer.EmailID);
            param[13] = new SqlParameter("@Title", argCustomer.Title);
            param[14] = new SqlParameter("@ContactPerson", argCustomer.ContactPerson);
            param[15] = new SqlParameter("@VATNo", argCustomer.VATNo);
            param[16] = new SqlParameter("@TransportZoneCode",argCustomer.TransportZoneCode);
            param[17] = new SqlParameter("@CustomerClassCode", argCustomer.CustomerClassCode);
            param[18] = new SqlParameter("@IndustryName", argCustomer.IndustryName);
            param[19] = new SqlParameter("@AnnualSales", argCustomer.AnnualSales);
            param[20] = new SqlParameter("@AnnualSalesCurrency", argCustomer.AnnualSalesCurrency);
            param[21] = new SqlParameter("@TotalEmployees", argCustomer.TotalEmployees);
            param[22] = new SqlParameter("@CurrencyCode", argCustomer.CurrencyCode);
            param[23] = new SqlParameter("@ClientCode", argCustomer.ClientCode);
            param[24] = new SqlParameter("@CreatedBy", argCustomer.CreatedBy);
            param[25] = new SqlParameter("@ModifiedBy", argCustomer.ModifiedBy);

            param[26] = new SqlParameter("@Type", SqlDbType.Char);
            param[26].Size = 1;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[27].Size = 255;
            param[27].Direction = ParameterDirection.Output;

            param[28] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[28].Size = 20;
            param[28].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCustomer", param);

            string strMessage = Convert.ToString(param[27].Value);
            string strType = Convert.ToString(param[26].Value);
            string strRetValue = Convert.ToString(param[28].Value);

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

            return strRetValue;
        }

        public ICollection<ErrorHandler> DeleteCustomer(string argCustomerCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted",iIsDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteCustomer", param);

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

        public bool blnIsCustomerExists(string argCustomerCode, string argClientCode)
        {
            bool IsCustomerExists = false;
            DataSet ds = new DataSet();
            ds = GetCustomer(argCustomerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCustomerExists = true;
            }
            else
            {
                IsCustomerExists = false;
            }
            return IsCustomerExists;
        }

        public bool blnIsCustomerExists(string argCustomerCode, string argClientCode,DataAccess da)
        {
            bool IsCustomerExists = false;
            DataSet ds = new DataSet();
            ds = GetCustomer(argCustomerCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCustomerExists = true;
            }
            else
            {
                IsCustomerExists = false;
            }
            return IsCustomerExists;
        }
    }
}