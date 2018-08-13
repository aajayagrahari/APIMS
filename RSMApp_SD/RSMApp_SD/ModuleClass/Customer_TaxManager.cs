
//Created On :: 04, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class Customer_TaxManager
    {
        const string Customer_TaxTable = "Customer_Tax";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Customer_Tax objGetCustomer_Tax(string argCustomerCode, string argCountryCode, string argTaxCategoryCode, string argTaxClassCode, string argClientCode)
        {
            Customer_Tax argCustomer_Tax = new Customer_Tax();
            DataSet DataSetToFill = new DataSet();

            if (argCustomerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argCountryCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argTaxCategoryCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argTaxClassCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCustomer_Tax(argCustomerCode, argCountryCode, argTaxCategoryCode, argTaxClassCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argCustomer_Tax = this.objCreateCustomer_Tax((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argCustomer_Tax;
        }

        public ICollection<Customer_Tax> colGetCustomer_Tax(string argCustomerCode, string argClientCode)
        {
            List<Customer_Tax> lst = new List<Customer_Tax>();
            DataSet DataSetToFill = new DataSet();
            Customer_Tax tCustomer_Tax = new Customer_Tax();
            DataSetToFill = this.GetCustomer_Tax(argCustomerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCustomer_Tax(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetCustomer_Tax(string argCustomerCode, string argCountryCode, string argTaxCategoryCode, string argTaxClassCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[1] = new SqlParameter("@CountryCode", argCountryCode);
            param[2] = new SqlParameter("@TaxCategoryCode", argTaxCategoryCode);
            param[3] = new SqlParameter("@TaxClassCode", argTaxClassCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCustomer_Tax", param);
            return DataSetToFill;
        }

        public DataSet GetCustomer_Tax(string argCustomerCode, string argCountryCode, string argTaxCategoryCode, string argTaxClassCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[1] = new SqlParameter("@CountryCode", argCountryCode);
            param[2] = new SqlParameter("@TaxCategoryCode", argTaxCategoryCode);
            param[3] = new SqlParameter("@TaxClassCode", argTaxClassCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCustomer_Tax4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCustomer_Tax(string argCustomerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCustomer_Tax", param);
            return DataSetToFill;
        }

        private Customer_Tax objCreateCustomer_Tax(DataRow dr)
        {
            Customer_Tax tCustomer_Tax = new Customer_Tax();
            tCustomer_Tax.SetObjectInfo(dr);
            return tCustomer_Tax;
        }

        //public ICollection<ErrorHandler> SaveCustomer_Tax(Customer_Tax argCustomer_Tax)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsCustomer_TaxExists(argCustomer_Tax.CustomerCode, argCustomer_Tax.CountryCode, argCustomer_Tax.TaxCategoryCode, argCustomer_Tax.TaxClassCode, argCustomer_Tax.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertCustomer_Tax(argCustomer_Tax, da, lstErr);
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
        //            UpdateCustomer_Tax(argCustomer_Tax, da, lstErr);
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

        public void SaveCustomer_Tax(Customer_Tax argCustomer_Tax, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCustomer_TaxExists(argCustomer_Tax.CustomerCode, argCustomer_Tax.CountryCode, argCustomer_Tax.TaxCategoryCode, argCustomer_Tax.TaxClassCode, argCustomer_Tax.ClientCode, da) == false)
                {
                    InsertCustomer_Tax(argCustomer_Tax, da, lstErr);
                }
                else
                {
                    UpdateCustomer_Tax(argCustomer_Tax, da, lstErr);
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

        public ICollection<ErrorHandler> SaveCustomer_Tax(ICollection<Customer_Tax> colGetCustomer_Tax, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                string strretValue = "";
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Customer_Tax argCustomer_Tax in colGetCustomer_Tax)
                {
                    if (argCustomer_Tax.IsDeleted == 0)
                    {
                        if (blnIsCustomer_TaxExists(argCustomer_Tax.CustomerCode, argCustomer_Tax.CountryCode, argCustomer_Tax.TaxCategoryCode, argCustomer_Tax.TaxClassCode, argCustomer_Tax.ClientCode, da) == false)
                        {
                            InsertCustomer_Tax(argCustomer_Tax, da, lstErr);
                        }
                        else
                        {
                            UpdateCustomer_Tax(argCustomer_Tax, da, lstErr);
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
                        DeleteCustomer_Tax(argCustomer_Tax.CustomerCode, argCustomer_Tax.CountryCode, argCustomer_Tax.TaxCategoryCode, argCustomer_Tax.TaxClassCode, argCustomer_Tax.ClientCode, argCustomer_Tax.IsDeleted);
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

        public void InsertCustomer_Tax(Customer_Tax argCustomer_Tax, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@CustomerCode", argCustomer_Tax.CustomerCode);
            param[1] = new SqlParameter("@CountryCode", argCustomer_Tax.CountryCode);
            param[2] = new SqlParameter("@TaxCategoryCode", argCustomer_Tax.TaxCategoryCode);
            param[3] = new SqlParameter("@TaxClassCode", argCustomer_Tax.TaxClassCode);
            param[4] = new SqlParameter("@ClientCode", argCustomer_Tax.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argCustomer_Tax.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argCustomer_Tax.ModifiedBy);

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCustomer_Tax", param);

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

        public void UpdateCustomer_Tax(Customer_Tax argCustomer_Tax, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@CustomerCode", argCustomer_Tax.CustomerCode);
            param[1] = new SqlParameter("@CountryCode", argCustomer_Tax.CountryCode);
            param[2] = new SqlParameter("@TaxCategoryCode", argCustomer_Tax.TaxCategoryCode);
            param[3] = new SqlParameter("@TaxClassCode", argCustomer_Tax.TaxClassCode);
            param[4] = new SqlParameter("@ClientCode", argCustomer_Tax.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argCustomer_Tax.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argCustomer_Tax.ModifiedBy);

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateCustomer_Tax", param);

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

        public ICollection<ErrorHandler> DeleteCustomer_Tax(string argCustomerCode, string argCountryCode, string argTaxCategoryCode, string argTaxClassCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
                param[1] = new SqlParameter("@CountryCode", argCountryCode);
                param[2] = new SqlParameter("@TaxCategoryCode", argTaxCategoryCode);
                param[3] = new SqlParameter("@TaxClassCode", argTaxClassCode);
                param[4] = new SqlParameter("@ClientCode", argClientCode);
                param[5] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[6] = new SqlParameter("@Type", SqlDbType.Char);
                param[6].Size = 1;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[7].Size = 255;
                param[7].Direction = ParameterDirection.Output;

                param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[8].Size = 20;
                param[8].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteCustomer_Tax", param);

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

        public ICollection<ErrorHandler> DeleteCustomer_Tax(string argCustomerCode, string argCountryCode, string argTaxCategoryCode, string argTaxClassCode, string argClientCode, int iIsDeleted, DataAccess da)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
                param[1] = new SqlParameter("@CountryCode", argCountryCode);
                param[2] = new SqlParameter("@TaxCategoryCode", argTaxCategoryCode);
                param[3] = new SqlParameter("@TaxClassCode", argTaxClassCode);
                param[4] = new SqlParameter("@ClientCode", argClientCode);
                param[5] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[6] = new SqlParameter("@Type", SqlDbType.Char);
                param[6].Size = 1;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[7].Size = 255;
                param[7].Direction = ParameterDirection.Output;

                param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[8].Size = 20;
                param[8].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteCustomer_Tax", param);

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

        public bool blnIsCustomer_TaxExists(string argCustomerCode, string argCountryCode, string argTaxCategoryCode, string argTaxClassCode, string argClientCode)
        {
            bool IsCustomer_TaxExists = false;
            DataSet ds = new DataSet();
            ds = GetCustomer_Tax(argCustomerCode, argCountryCode, argTaxCategoryCode, argTaxClassCode, argClientCode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCustomer_TaxExists = true;
            }
            else
            {
                IsCustomer_TaxExists = false;
            }
            return IsCustomer_TaxExists;
        }

        public bool blnIsCustomer_TaxExists(string argCustomerCode, string argCountryCode, string argTaxCategoryCode, string argTaxClassCode, string argClientCode, DataAccess da)
        {
            bool IsCustomer_TaxExists = false;
            DataSet ds = new DataSet();
            ds = GetCustomer_Tax(argCustomerCode, argCountryCode, argTaxCategoryCode, argTaxClassCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCustomer_TaxExists = true;
            }
            else
            {
                IsCustomer_TaxExists = false;
            }
            return IsCustomer_TaxExists;
        }
    }
}