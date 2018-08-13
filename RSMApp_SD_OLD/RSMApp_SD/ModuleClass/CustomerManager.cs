
//Created On :: 18, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using Telerik.Web.UI;

namespace RSMApp_SD
{
    public class CustomerManager
    {
        const string CustomerTable = "Customer";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
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

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBCustomer, string argClientCode, int iIsDeleted)
        {
            RCBCustomer.Items.Clear();
            foreach (DataRow dr in GetCustomer(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["CustomerCode"].ToString() + " " + dr["CustomerName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["CustomerCode"].ToString()
                };

                itemCollection.Attributes.Add("CustomerCode", dr["CustomerCode"].ToString());
                itemCollection.Attributes.Add("CustomerName", dr["CustomerName"].ToString());
               

                RCBCustomer.Items.Add(itemCollection);
                itemCollection.DataBind();
            }

        }

        public RadComboBoxData FillRadComboData(RadComboBoxContext context, string strClientCode)
        {
            RadComboBoxData comboData = new RadComboBoxData();

            DataTable dtCustData = GetCustomer4Combo(context.Text.ToString(), context["SalesOfficeCode"].ToString().Trim(), strClientCode).Tables[0];

            if (dtCustData != null)
            {

                List<RadComboBoxItemData> result = new List<RadComboBoxItemData>(context.NumberOfItems);
                try
                {
                    int itemsPerRequest = 10;
                    int itemOffset = context.NumberOfItems;
                    int endOffset = itemOffset + itemsPerRequest;
                    if (endOffset > dtCustData.Rows.Count)
                    {
                        endOffset = dtCustData.Rows.Count;
                    }
                    if (endOffset == dtCustData.Rows.Count)
                    {
                        comboData.EndOfItems = true;
                    }
                    else
                    {
                        comboData.EndOfItems = false;
                    }
                    result = new List<RadComboBoxItemData>();

                    for (int i = itemOffset; i < endOffset; i++)
                    {
                        RadComboBoxItemData itemData = new RadComboBoxItemData();

                        String itemText = dtCustData.Rows[i]["CustomerCode"].ToString() + " " + dtCustData.Rows[i]["Name1"].ToString();
                        itemData.Text = itemText;
                        itemData.Value = dtCustData.Rows[i]["CustomerCode"].ToString();
                        itemData.Attributes["CustomerCode"] = dtCustData.Rows[i]["CustomerCode"].ToString();
                        itemData.Attributes["Name1"] = dtCustData.Rows[i]["Name1"].ToString();
                        itemData.Attributes["StateCode"] = dtCustData.Rows[i]["StateCode"].ToString();
                        itemData.Attributes["SalesDistrictCode"] = dtCustData.Rows[i]["SalesDistrictCode"].ToString();
                        itemData.Attributes["SalesRegionCode"] = dtCustData.Rows[i]["SalesRegionCode"].ToString();
                        itemData.Attributes["SalesOfficeCode"] = dtCustData.Rows[i]["SalesOfficeCode"].ToString();
                        itemData.Attributes["SalesGroupCode"] = dtCustData.Rows[i]["SalesGroupCode"].ToString();
                        itemData.Attributes["VATNo"] = dtCustData.Rows[i]["VATNo"].ToString();
                        itemData.Attributes["IsTaxExempted"] = dtCustData.Rows[i]["IsTaxExempted"].ToString();
                        itemData.Attributes["SalesOrganizationCode"] = dtCustData.Rows[i]["SalesOrganizationCode"].ToString();
                        itemData.Attributes["DistChannelCode"] = dtCustData.Rows[i]["DistChannelCode"].ToString();
                        itemData.Attributes["DivisionCode"] = dtCustData.Rows[i]["DivisionCode"].ToString();
                        itemData.Attributes["CurrencyCode"] = dtCustData.Rows[i]["CurrencyCode"].ToString();
                        result.Add(itemData);
                    }

                    if (dtCustData.Rows.Count > 0)
                    {
                        comboData.Message = String.Format("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>", endOffset.ToString(), dtCustData.Rows.Count.ToString());
                    }
                    else
                    {
                        comboData.Message = "No matches";
                    }

                }
                catch (Exception e)
                {
                    comboData.Message = e.Message;
                }

                comboData.Items = result.ToArray();
            }
            return comboData;
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

                tSQL = "SELECT CustomerCode,(Name1+ +Name2) as CustomerName FROM " + CustomerTable.ToString();

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

        public void InsertCustomer(Customer argCustomer, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[28];
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
            param[16] = new SqlParameter("@SalesDistrictCode", argCustomer.SalesDistrictCode);
            param[17] = new SqlParameter("@SalesRegionCode", argCustomer.SalesRegionCode);
            param[18] = new SqlParameter("@SalesOfficeCode", argCustomer.SalesOfficeCode);
            param[19] = new SqlParameter("@IsTaxExempted", argCustomer.IsTaxExempted);
            param[20] = new SqlParameter("@SalesGroupCode", argCustomer.SalesGroupCode);
            param[21] = new SqlParameter("@CurrencyCode",argCustomer.CurrencyCode);
            param[22] = new SqlParameter("@ClientCode", argCustomer.ClientCode);
            param[23] = new SqlParameter("@CreatedBy", argCustomer.CreatedBy);
            param[24] = new SqlParameter("@ModifiedBy", argCustomer.ModifiedBy);
            
            param[25] = new SqlParameter("@Type", SqlDbType.Char);
            param[25].Size = 1;
            param[25].Direction = ParameterDirection.Output;

            param[26] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[26].Size = 255;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[27].Size = 20;
            param[27].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCustomer", param);

            string strMessage = Convert.ToString(param[26].Value);
            string strType = Convert.ToString(param[25].Value);
            string strRetValue = Convert.ToString(param[27].Value);
            
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

        public void UpdateCustomer(Customer argCustomer, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[28];
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
            param[16] = new SqlParameter("@SalesDistrictCode", argCustomer.SalesDistrictCode);
            param[17] = new SqlParameter("@SalesRegionCode", argCustomer.SalesRegionCode);
            param[18] = new SqlParameter("@SalesOfficeCode", argCustomer.SalesOfficeCode);
            param[19] = new SqlParameter("@IsTaxExempted", argCustomer.IsTaxExempted);
            param[20] = new SqlParameter("@SalesGroupCode", argCustomer.SalesGroupCode);
            param[21] = new SqlParameter("@CurrencyCode", argCustomer.CurrencyCode);
            param[22] = new SqlParameter("@ClientCode", argCustomer.ClientCode);
            param[23] = new SqlParameter("@CreatedBy", argCustomer.CreatedBy);
            param[24] = new SqlParameter("@ModifiedBy", argCustomer.ModifiedBy);

            param[25] = new SqlParameter("@Type", SqlDbType.Char);
            param[25].Size = 1;
            param[25].Direction = ParameterDirection.Output;

            param[26] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[26].Size = 255;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[27].Size = 20;
            param[27].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCustomer", param);

            string strMessage = Convert.ToString(param[26].Value);
            string strType = Convert.ToString(param[25].Value);
            string strRetValue = Convert.ToString(param[27].Value);


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
    }
}