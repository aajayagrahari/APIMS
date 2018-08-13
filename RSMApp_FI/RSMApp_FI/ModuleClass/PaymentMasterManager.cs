
//Created On :: 28, September, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_FI
{
    public class PaymentMasterManager
    {
        const string PaymentMasterTable = "PaymentMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public PaymentMaster objGetPaymentMaster(string argPaymentDocCode, string argClientCode)
        {
            PaymentMaster argPaymentMaster = new PaymentMaster();
            DataSet DataSetToFill = new DataSet();

            if (argPaymentDocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPaymentMaster(argPaymentDocCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPaymentMaster = this.objCreatePaymentMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPaymentMaster;
        }

        public ICollection<PaymentMaster> colGetPaymentMaster(string argClientCode)
        {
            List<PaymentMaster> lst = new List<PaymentMaster>();
            DataSet DataSetToFill = new DataSet();
            PaymentMaster tPaymentMaster = new PaymentMaster();

            DataSetToFill = this.GetPaymentMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePaymentMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetPaymentMaster(string argPaymentDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PaymentDocCode", argPaymentDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPaymentMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPaymentMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
 
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPaymentMaster");
            return DataSetToFill;
        }

        public DataSet GetPaymentMaster(string argAccountDocTypeCode, string argClientCode, DateTime argStartDate, DateTime argEndDate)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@AccountDocTypeCode", argAccountDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            param[2] = new SqlParameter("@StartDate", argStartDate);
            param[3] = new SqlParameter("@EndDate", argEndDate);

            DataSetToFill = da.FillDataSet("SP_GetPaymentMaster", param);
            return DataSetToFill;
        }


        private PaymentMaster objCreatePaymentMaster(DataRow dr)
        {
            PaymentMaster tPaymentMaster = new PaymentMaster();

            tPaymentMaster.SetObjectInfo(dr);

            return tPaymentMaster;

        }

        public ICollection<ErrorHandler> SavePaymentMaster(PaymentMaster argPaymentMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPaymentMasterExists(argPaymentMaster.PaymentDocCode, argPaymentMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPaymentMaster(argPaymentMaster, da, lstErr);
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
                    UpdatePaymentMaster(argPaymentMaster, da, lstErr);
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

        public void InsertPaymentMaster(PaymentMaster argPaymentMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[27];
            param[0] = new SqlParameter("@PaymentDocCode", argPaymentMaster.PaymentDocCode);
            param[1] = new SqlParameter("@AccountDocTypeCode", argPaymentMaster.AccountDocTypeCode);
            param[2] = new SqlParameter("@DocumentDate", argPaymentMaster.DocumentDate);
            param[3] = new SqlParameter("@PostingDate", argPaymentMaster.PostingDate);
            param[4] = new SqlParameter("@PostingPeriod", argPaymentMaster.PostingPeriod);
            param[5] = new SqlParameter("@CompanyCode", argPaymentMaster.CompanyCode);
            param[6] = new SqlParameter("@BusinessAreaCode", argPaymentMaster.BusinessAreaCode);
            param[7] = new SqlParameter("@ProfitCenterCode", argPaymentMaster.ProfitCenterCode);
            param[8] = new SqlParameter("@BankAccount", argPaymentMaster.BankAccount);
            param[9] = new SqlParameter("@SpecialGLIndCode", argPaymentMaster.SpecialGLIndCode);
            param[10] = new SqlParameter("@CurrencyCode", argPaymentMaster.CurrencyCode);
            param[11] = new SqlParameter("@Reference", argPaymentMaster.Reference);
            param[12] = new SqlParameter("@ValueDate", argPaymentMaster.ValueDate);
            param[13] = new SqlParameter("@AccountType", argPaymentMaster.AccountType);
            param[14] = new SqlParameter("@CustomerCode", argPaymentMaster.CustomerCode);
            param[15] = new SqlParameter("@Amount", argPaymentMaster.Amount);
            param[16] = new SqlParameter("@AmountInLc", argPaymentMaster.AmountInLc);
            param[17] = new SqlParameter("@DocHeaderText", argPaymentMaster.DocHeaderText);
            param[18] = new SqlParameter("@ClearingText", argPaymentMaster.ClearingText);
            param[19] = new SqlParameter("@TradingPartnerBA", argPaymentMaster.TradingPartnerBA);
            param[20] = new SqlParameter("@ClientCode", argPaymentMaster.ClientCode);
            param[21] = new SqlParameter("@CreatedBy", argPaymentMaster.CreatedBy);
            param[22] = new SqlParameter("@ModifiedBy", argPaymentMaster.ModifiedBy);
            param[23] = new SqlParameter("@IsCancel", argPaymentMaster.IsCancel);

            param[24] = new SqlParameter("@Type", SqlDbType.Char);
            param[24].Size = 1;
            param[24].Direction = ParameterDirection.Output;

            param[25] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[25].Size = 255;
            param[25].Direction = ParameterDirection.Output;

            param[26] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[26].Size = 20;
            param[26].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPaymentMaster", param);


            string strMessage = Convert.ToString(param[25].Value);
            string strType = Convert.ToString(param[24].Value);
            string strRetValue = Convert.ToString(param[26].Value);


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

        public void UpdatePaymentMaster(PaymentMaster argPaymentMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[27];
            param[0] = new SqlParameter("@PaymentDocCode", argPaymentMaster.PaymentDocCode);
            param[1] = new SqlParameter("@AccountDocTypeCode", argPaymentMaster.AccountDocTypeCode);
            param[2] = new SqlParameter("@DocumentDate", argPaymentMaster.DocumentDate);
            param[3] = new SqlParameter("@PostingDate", argPaymentMaster.PostingDate);
            param[4] = new SqlParameter("@PostingPeriod", argPaymentMaster.PostingPeriod);
            param[5] = new SqlParameter("@CompanyCode", argPaymentMaster.CompanyCode);
            param[6] = new SqlParameter("@BusinessAreaCode", argPaymentMaster.BusinessAreaCode);
            param[7] = new SqlParameter("@ProfitCenterCode", argPaymentMaster.ProfitCenterCode);
            param[8] = new SqlParameter("@BankAccount", argPaymentMaster.BankAccount);
            param[9] = new SqlParameter("@SpecialGLIndCode", argPaymentMaster.SpecialGLIndCode);
            param[10] = new SqlParameter("@CurrencyCode", argPaymentMaster.CurrencyCode);
            param[11] = new SqlParameter("@Reference", argPaymentMaster.Reference);
            param[12] = new SqlParameter("@ValueDate", argPaymentMaster.ValueDate);
            param[13] = new SqlParameter("@AccountType", argPaymentMaster.AccountType);
            param[14] = new SqlParameter("@CustomerCode", argPaymentMaster.CustomerCode);
            param[15] = new SqlParameter("@Amount", argPaymentMaster.Amount);
            param[16] = new SqlParameter("@AmountInLc", argPaymentMaster.AmountInLc);
            param[17] = new SqlParameter("@DocHeaderText", argPaymentMaster.DocHeaderText);
            param[18] = new SqlParameter("@ClearingText", argPaymentMaster.ClearingText);
            param[19] = new SqlParameter("@TradingPartnerBA", argPaymentMaster.TradingPartnerBA);
            param[20] = new SqlParameter("@ClientCode", argPaymentMaster.ClientCode);
            param[21] = new SqlParameter("@CreatedBy", argPaymentMaster.CreatedBy);
            param[22] = new SqlParameter("@ModifiedBy", argPaymentMaster.ModifiedBy);
            param[23] = new SqlParameter("@IsCancel", argPaymentMaster.IsCancel);

            param[24] = new SqlParameter("@Type", SqlDbType.Char);
            param[24].Size = 1;
            param[24].Direction = ParameterDirection.Output;

            param[25] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[25].Size = 255;
            param[25].Direction = ParameterDirection.Output;

            param[26] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[26].Size = 20;
            param[26].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePaymentMaster", param);


            string strMessage = Convert.ToString(param[25].Value);
            string strType = Convert.ToString(param[24].Value);
            string strRetValue = Convert.ToString(param[26].Value);


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

        public ICollection<ErrorHandler> DeletePaymentMaster(string argPaymentDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@PaymentDocCode", argPaymentDocCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);

                param[2] = new SqlParameter("@Type", SqlDbType.Char);
                param[2].Size = 1;
                param[2].Direction = ParameterDirection.Output;
                param[3] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[3].Size = 255;
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[4].Size = 20;
                param[4].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeletePaymentMaster", param);


                string strMessage = Convert.ToString(param[3].Value);
                string strType = Convert.ToString(param[2].Value);
                string strRetValue = Convert.ToString(param[4].Value);


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

        public bool blnIsPaymentMasterExists(string argPaymentDocCode, string argClientCode)
        {
            bool IsPaymentMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetPaymentMaster(argPaymentDocCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPaymentMasterExists = true;
            }
            else
            {
                IsPaymentMasterExists = false;
            }
            return IsPaymentMasterExists;
        }
    }
}