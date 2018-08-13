
//Created On :: 18, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using RSMApp_Organization;
using Telerik.Web.UI;

namespace RSMApp_SD
{
    public class Customer_SalesAreaManager
    {
        const string Customer_SalesAreaTable = "Customer_SalesArea";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public Customer_SalesArea objGetCustomer_SalesArea(string argCustomerCode, string argSalesOrganizationCode, string argDivisionCode, string argDistChannelCode, string argClientCode)
        {
            Customer_SalesArea argCustomer_SalesArea = new Customer_SalesArea();
            DataSet DataSetToFill = new DataSet();

            if (argCustomerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argSalesOrganizationCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argDivisionCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argDistChannelCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCustomer_SalesArea(argCustomerCode, argSalesOrganizationCode, argDivisionCode, argDistChannelCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCustomer_SalesArea = this.objCreateCustomer_SalesArea((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCustomer_SalesArea;
        }
        
        public ICollection<Customer_SalesArea> colGetCustomer_SalesArea(string argClientCode)
        {
            List<Customer_SalesArea> lst = new List<Customer_SalesArea>();
            DataSet DataSetToFill = new DataSet();
            Customer_SalesArea tCustomer_SalesArea = new Customer_SalesArea();

            DataSetToFill = this.GetCustomer_SalesArea(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCustomer_SalesArea(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetCustomer_SalesArea(string argCustomerCode, string argSalesOrganizationCode, string argDivisionCode, string argDistChannelCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[3] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCustomer_SalesArea4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCustomer_SalesArea(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetCustomer_SalesArea",param);
            return DataSetToFill;
        }
        
        private Customer_SalesArea objCreateCustomer_SalesArea(DataRow dr)
        {
            Customer_SalesArea tCustomer_SalesArea = new Customer_SalesArea();

            tCustomer_SalesArea.SetObjectInfo(dr);

            return tCustomer_SalesArea;

        }
        
        public ICollection<ErrorHandler> SaveCustomer_SalesArea(Customer_SalesArea argCustomer_SalesArea)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            SalesAreaManager ObjSalesAreaManager = new SalesAreaManager();
            try
            {
                if (ObjSalesAreaManager.blnIsSalesAreaExists(argCustomer_SalesArea.SalesOrganizationCode, argCustomer_SalesArea.DistChannelCode, argCustomer_SalesArea.DivisionCode, argCustomer_SalesArea.ClientCode) == true)
                {

                    if (blnIsCustomer_SalesAreaExists(argCustomer_SalesArea.CustomerCode, argCustomer_SalesArea.SalesOrganizationCode, argCustomer_SalesArea.DivisionCode, argCustomer_SalesArea.DistChannelCode, argCustomer_SalesArea.ClientCode) == false)
                    {

                        da.Open_Connection();
                        da.BEGIN_TRANSACTION();
                        InsertCustomer_SalesArea(argCustomer_SalesArea, da, lstErr);
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
                        UpdateCustomer_SalesArea(argCustomer_SalesArea, da, lstErr);
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
                else
                {
                    objErrorHandler.Type = "E";
                    objErrorHandler.MsgId = 0;
                    objErrorHandler.Module = ErrorConstant.strInsertModule;
                    objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                    objErrorHandler.Message = "Sales Area does not exists.";
                    objErrorHandler.RowNo = 0;
                    objErrorHandler.FieldName = "";
                    objErrorHandler.LogCode = "";
                    lstErr.Add(objErrorHandler);
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
        
        public void InsertCustomer_SalesArea(Customer_SalesArea argCustomer_SalesArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@CustomerCode", argCustomer_SalesArea.CustomerCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argCustomer_SalesArea.SalesOrganizationCode);
            param[2] = new SqlParameter("@DivisionCode", argCustomer_SalesArea.DivisionCode);
            param[3] = new SqlParameter("@DistChannelCode", argCustomer_SalesArea.DistChannelCode);
            param[4] = new SqlParameter("@ClientCode", argCustomer_SalesArea.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argCustomer_SalesArea.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argCustomer_SalesArea.ModifiedBy);
            
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCustomer_SalesArea", param);
            
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
            lstErr.Add(objErrorHandler);

        }
        
        public void UpdateCustomer_SalesArea(Customer_SalesArea argCustomer_SalesArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@CustomerCode", argCustomer_SalesArea.CustomerCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argCustomer_SalesArea.SalesOrganizationCode);
            param[2] = new SqlParameter("@DivisionCode", argCustomer_SalesArea.DivisionCode);
            param[3] = new SqlParameter("@DistChannelCode", argCustomer_SalesArea.DistChannelCode);
            param[4] = new SqlParameter("@ClientCode", argCustomer_SalesArea.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argCustomer_SalesArea.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argCustomer_SalesArea.ModifiedBy);
            
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCustomer_SalesArea", param);
            
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
            lstErr.Add(objErrorHandler);

        }
        
        public ICollection<ErrorHandler> DeleteCustomer_SalesArea(string argCustomerCode, string argSalesOrganizationCode, string argDivisionCode, string argDistChannelCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
                param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
                param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
                param[3] = new SqlParameter("@DistChannelCode", argDistChannelCode);
                param[4] = new SqlParameter("@ClientCode", argClientCode);
                param[5] = new SqlParameter("@IsDeleted",iIsDeleted);

                param[6] = new SqlParameter("@Type", SqlDbType.Char);
                param[6].Size = 1;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[7].Size = 255;
                param[7].Direction = ParameterDirection.Output;

                param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[8].Size = 20;
                param[8].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteCustomer_SalesArea", param);

                string strMessage = Convert.ToString(param[7].Value);
                string strType = Convert.ToString(param[6].Value);
                string strRetValue = Convert.ToString(param[8].Value);


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
        
        public bool blnIsCustomer_SalesAreaExists(string argCustomerCode, string argSalesOrganizationCode, string argDivisionCode, string argDistChannelCode, string argClientCode)
        {
            bool IsCustomer_SalesAreaExists = false;
            DataSet ds = new DataSet();
            ds = GetCustomer_SalesArea(argCustomerCode, argSalesOrganizationCode, argDivisionCode, argDistChannelCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCustomer_SalesAreaExists = true;
            }
            else
            {
                IsCustomer_SalesAreaExists = false;
            }
            return IsCustomer_SalesAreaExists;
        }
    }
}