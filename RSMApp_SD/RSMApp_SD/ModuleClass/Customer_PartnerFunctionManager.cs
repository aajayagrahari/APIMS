
//Created On :: 28, July, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class Customer_PartnerFunctionManager
    {
        const string Customer_PartnerFunctionTable = "Customer_PartnerFunction";
        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Customer_PartnerFunction objGetCustomer_PartnerFunction(string argCustomerCode, string argPFunctionCode, string argClientCode)
        {
            Customer_PartnerFunction argCustomer_PartnerFunction = new Customer_PartnerFunction();
            DataSet DataSetToFill = new DataSet();

            if (argCustomerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argPFunctionCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCustomer_PartnerFunction(argCustomerCode, argPFunctionCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argCustomer_PartnerFunction = this.objCreateCustomer_PartnerFunction((DataRow)DataSetToFill.Tables[0].Rows[0]);
            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argCustomer_PartnerFunction;
        }

        public ICollection<Customer_PartnerFunction> colGetCustomer_PartnerFunction(string argCustomerCode, string argClientCode)
        {
            List<Customer_PartnerFunction> lst = new List<Customer_PartnerFunction>();
            DataSet DataSetToFill = new DataSet();
            Customer_PartnerFunction tCustomer_PartnerFunction = new Customer_PartnerFunction();
            DataSetToFill = this.GetCustomer_PartnerFunction(argCustomerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCustomer_PartnerFunction(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetCustomer_PartnerFunction(string argCustomerCode, string argPFunctionCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[1] = new SqlParameter("@PFunctionCode", argPFunctionCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCustomer_PartnerFunction4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCustomer_PartnerFunction(string argCustomerCode, string argPFunctionCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[1] = new SqlParameter("@PFunctionCode", argPFunctionCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCustomer_PartnerFunction4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCustomer_PartnerFunction(string argCustomerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCustomer_PartnerFunction", param);
            return DataSetToFill;
        }

        public DataSet GetCustomer_PartnerFunction4SO(string argCustomerCode, string argSalesOrganisationCode, string argDivisionCode, string argDistChannelCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganisationCode);
            param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[3] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetCustomer_PartnerFunction4SO", param);
            return DataSetToFill;
        }

        // For Customer Sales area Page
        public DataSet GetCustomer_PartnerFunction4SA(string argCustomerCode, string argSalesOrganisationCode, string argDivisionCode, string argDistChannelCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganisationCode);
            param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[3] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetCustomer_PartnerFunction4SA", param);
            return DataSetToFill;
        }

        private Customer_PartnerFunction objCreateCustomer_PartnerFunction(DataRow dr)
        {
            Customer_PartnerFunction tCustomer_PartnerFunction = new Customer_PartnerFunction();
            tCustomer_PartnerFunction.SetObjectInfo(dr);
            return tCustomer_PartnerFunction;
        }

        //public ICollection<ErrorHandler> SaveCustomer_PartnerFunction(Customer_PartnerFunction argCustomer_PartnerFunction)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsCustomer_PartnerFunctionExists(argCustomer_PartnerFunction.CustomerCode, argCustomer_PartnerFunction.PFunctionCode, argCustomer_PartnerFunction.ClientCode) == false)
        //        {
        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertCustomer_PartnerFunction(argCustomer_PartnerFunction, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            UpdateCustomer_PartnerFunction(argCustomer_PartnerFunction, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (da != null)
        //        {
        //            da.ROLLBACK_TRANSACTION();
        //        }
        //        objErrorHandler.Type = ErrorConstant.strAboartType;
        //        objErrorHandler.MsgId = 0;
        //        objErrorHandler.Module = ErrorConstant.strInsertModule;
        //        objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
        //        objErrorHandler.Message = ex.Message.ToString();
        //        objErrorHandler.RowNo = 0;
        //        objErrorHandler.FieldName = "";
        //        objErrorHandler.LogCode = "";
        //        lstErr.Add(objErrorHandler);
        //    }
        //    finally
        //    {
        //        if (da != null)
        //        {
        //            da.Close_Connection();
        //            da = null;
        //        }
        //    }
        //    return lstErr;
        //}

        public void SaveCustomer_PartnerFunction(Customer_PartnerFunction argCustomer_PartnerFunction, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCustomer_PartnerFunctionExists(argCustomer_PartnerFunction.CustomerCode, argCustomer_PartnerFunction.PFunctionCode, argCustomer_PartnerFunction.ClientCode, da) == false)
                {
                    InsertCustomer_PartnerFunction(argCustomer_PartnerFunction, da, lstErr);
                }
                else
                {
                    UpdateCustomer_PartnerFunction(argCustomer_PartnerFunction, da, lstErr);
                }
            }
            catch (Exception ex)
            {
                objErrorHandler.Type = ErrorConstant.strAboartType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strInsertModule;
                objErrorHandler.ModulePart = ErrorConstant.strDetailModule;
                objErrorHandler.Message = ex.Message.ToString();
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";
                lstErr.Add(objErrorHandler);
            }
        }

        public ICollection<ErrorHandler> SaveCustomer_PartnerFunction(ICollection<Customer_PartnerFunction> colGetCustomer_PartnerFunction, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                string strretValue = "";
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Customer_PartnerFunction argCustomer_PartnerFunction in colGetCustomer_PartnerFunction)
                {
                    if (argCustomer_PartnerFunction.IsDeleted == 0)
                    {
                        if (blnIsCustomer_PartnerFunctionExists(argCustomer_PartnerFunction.CustomerCode, argCustomer_PartnerFunction.PFunctionCode, argCustomer_PartnerFunction.ClientCode, da) == false)
                        {
                            InsertCustomer_PartnerFunction(argCustomer_PartnerFunction, da, lstErr);
                        }
                        else
                        {
                            UpdateCustomer_PartnerFunction(argCustomer_PartnerFunction, da, lstErr);
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

                        }
                    }
                    else
                    {
                        DeleteCustomer_PartnerFunction(argCustomer_PartnerFunction.CustomerCode, argCustomer_PartnerFunction.PFunctionCode, argCustomer_PartnerFunction.ClientCode, argCustomer_PartnerFunction.IsDeleted);
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

        public void InsertCustomer_PartnerFunction(Customer_PartnerFunction argCustomer_PartnerFunction, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@CustomerCode", argCustomer_PartnerFunction.CustomerCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argCustomer_PartnerFunction.SalesOrganizationCode);
            param[2] = new SqlParameter("@DivisionCode", argCustomer_PartnerFunction.DivisionCode);
            param[3] = new SqlParameter("@DistChannelCode", argCustomer_PartnerFunction.DistChannelCode);
            param[4] = new SqlParameter("@PFunctionCode", argCustomer_PartnerFunction.PFunctionCode);
            param[5] = new SqlParameter("@PartnerCounter", argCustomer_PartnerFunction.PartnerCounter);
            param[6] = new SqlParameter("@PartnerTable", argCustomer_PartnerFunction.PartnerTable);
            param[7] = new SqlParameter("@PartnerCode", argCustomer_PartnerFunction.PartnerCode);
            param[8] = new SqlParameter("@ClientCode", argCustomer_PartnerFunction.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argCustomer_PartnerFunction.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argCustomer_PartnerFunction.ModifiedBy);

            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCustomer_PartnerFunction", param);

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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);
        }

        public void UpdateCustomer_PartnerFunction(Customer_PartnerFunction argCustomer_PartnerFunction, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@CustomerCode", argCustomer_PartnerFunction.CustomerCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argCustomer_PartnerFunction.SalesOrganizationCode);
            param[2] = new SqlParameter("@DivisionCode", argCustomer_PartnerFunction.DivisionCode);
            param[3] = new SqlParameter("@DistChannelCode", argCustomer_PartnerFunction.DistChannelCode);
            param[4] = new SqlParameter("@PFunctionCode", argCustomer_PartnerFunction.PFunctionCode);
            param[5] = new SqlParameter("@PartnerCounter", argCustomer_PartnerFunction.PartnerCounter);
            param[6] = new SqlParameter("@PartnerTable", argCustomer_PartnerFunction.PartnerTable);
            param[7] = new SqlParameter("@PartnerCode", argCustomer_PartnerFunction.PartnerCode);
            param[8] = new SqlParameter("@ClientCode", argCustomer_PartnerFunction.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argCustomer_PartnerFunction.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argCustomer_PartnerFunction.ModifiedBy);

            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCustomer_PartnerFunction", param);

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

        public ICollection<ErrorHandler> DeleteCustomer_PartnerFunction(string argCustomerCode, string argPFunctionCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
                param[1] = new SqlParameter("@PFunctionCode", argPFunctionCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteCustomer_PartnerFunction", param);

                string strMessage = Convert.ToString(param[5].Value);
                string strType = Convert.ToString(param[4].Value);
                string strRetValue = Convert.ToString(param[6].Value);

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

        public ICollection<ErrorHandler> DeleteCustomer_PartnerFunction(string argCustomerCode, string argPFunctionCode, string argClientCode, int iIsDeleted, DataAccess da)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
                param[1] = new SqlParameter("@PFunctionCode", argPFunctionCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;
                int i = da.NExecuteNonQuery("Proc_DeleteCustomer_PartnerFunction", param);

                string strMessage = Convert.ToString(param[5].Value);
                string strType = Convert.ToString(param[4].Value);
                string strRetValue = Convert.ToString(param[6].Value);

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

        public bool blnIsCustomer_PartnerFunctionExists(string argCustomerCode, string argPFunctionCode, string argClientCode)
        {
            bool IsCustomer_PartnerFunctionExists = false;
            DataSet ds = new DataSet();
            ds = GetCustomer_PartnerFunction(argCustomerCode, argPFunctionCode, argClientCode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCustomer_PartnerFunctionExists = true;
            }
            else
            {
                IsCustomer_PartnerFunctionExists = false;
            }
            return IsCustomer_PartnerFunctionExists;
        }

        public bool blnIsCustomer_PartnerFunctionExists(string argCustomerCode, string argPFunctionCode, string argClientCode, DataAccess da)
        {
            bool IsCustomer_PartnerFunctionExists = false;
            DataSet ds = new DataSet();
            ds = GetCustomer_PartnerFunction(argCustomerCode, argPFunctionCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCustomer_PartnerFunctionExists = true;
            }
            else
            {
                IsCustomer_PartnerFunctionExists = false;
            }
            return IsCustomer_PartnerFunctionExists;
        }
    }
}