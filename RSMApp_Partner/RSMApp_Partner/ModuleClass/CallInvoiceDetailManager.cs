
//Created On :: 28, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallInvoiceDetailManager
    {
        const string CallInvoiceDetailTable = "CallInvoiceDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CallInvoiceDetail objGetCallInvoiceDetail(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode)
        {
            CallInvoiceDetail argCallInvoiceDetail = new CallInvoiceDetail();
            DataSet DataSetToFill = new DataSet();

            if (argCallCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argCallItemNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argRepairProcDocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallInvoiceDetail(argCallCode, argCallItemNo, argRepairProcDocCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallInvoiceDetail = this.objCreateCallInvoiceDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallInvoiceDetail;
        }

        public ICollection<CallInvoiceDetail> colGetCallInvoiceDetail(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode)
        {
            List<CallInvoiceDetail> lst = new List<CallInvoiceDetail>();
            DataSet DataSetToFill = new DataSet();
            CallInvoiceDetail tCallInvoiceDetail = new CallInvoiceDetail();

            DataSetToFill = this.GetCallInvoiceDetail(argCallCode, argCallItemNo, argRepairProcDocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallInvoiceDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        //public DataSet GetCallInvoiceDetail(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode)
        //{
        //    DataAccess da = new DataAccess();
        //    DataSet DataSetToFill = new DataSet();

        //    SqlParameter[] param = new SqlParameter[4];
        //    param[0] = new SqlParameter("@CallCode", argCallCode);
        //    param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
        //    param[2] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
        //    param[3] = new SqlParameter("@ClientCode", argClientCode);

        //    DataSetToFill = da.FillDataSet("SP_GetCallInvoiceDetail4ID", param);

        //    return DataSetToFill;
        //}
        
        public DataSet GetCallInvoiceDetail(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallInvoiceDetail", param);
            return DataSetToFill;
        }

        public DataSet GetCallInvoiceDetail(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallInvoiceDetail", param);
            return DataSetToFill;
        }

        public DataSet GetLabourCharges4Repair(string argMatGroup1Code, string argServiceLevel, DateTime dtCurDate, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@ServiceLevel", argServiceLevel);
            param[2] = new SqlParameter("@CurDate", dtCurDate);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetLabourCharges4Repair", param);
            return DataSetToFill;
        }

        public DataSet GetCallInvoiceDetail4CL(string argCallCode, string argCallItemNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetCallInvoiceDetail4CL", param);
            return DataSetToFill;            
        }

        private CallInvoiceDetail objCreateCallInvoiceDetail(DataRow dr)
        {
            CallInvoiceDetail tCallInvoiceDetail = new CallInvoiceDetail();

            tCallInvoiceDetail.SetObjectInfo(dr);

            return tCallInvoiceDetail;

        }
        
        public ICollection<ErrorHandler> SaveCallInvoiceDetail(CallInvoiceDetail argCallInvoiceDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallInvoiceDetailExists(argCallInvoiceDetail.CallCode, argCallInvoiceDetail.CallItemNo, argCallInvoiceDetail.RepairProcDocCode, argCallInvoiceDetail.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallInvoiceDetail(argCallInvoiceDetail, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
                    }
                }
                else
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    UpdateCallInvoiceDetail(argCallInvoiceDetail, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
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
            return lstErr;
        }

        public void SaveCallInvoiceDetail(CallInvoiceDetail argCallInvoiceDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCallInvoiceDetailExists(argCallInvoiceDetail.CallCode, argCallInvoiceDetail.CallItemNo, argCallInvoiceDetail.RepairProcDocCode, argCallInvoiceDetail.ClientCode, da) == false)
                {
                    InsertCallInvoiceDetail(argCallInvoiceDetail, da, lstErr);
                }
                else
                {
                    UpdateCallInvoiceDetail(argCallInvoiceDetail, da, lstErr);
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
        
        public void InsertCallInvoiceDetail(CallInvoiceDetail argCallInvoiceDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@CallCode", argCallInvoiceDetail.CallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallInvoiceDetail.CallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argCallInvoiceDetail.RepairProcDocCode);
            param[3] = new SqlParameter("@PartsCost", argCallInvoiceDetail.PartsCost);
            param[4] = new SqlParameter("@LabourCost", argCallInvoiceDetail.LabourCost);
            param[5] = new SqlParameter("@ShippingCost", argCallInvoiceDetail.ShippingCost);
            param[6] = new SqlParameter("@HandlingCost", argCallInvoiceDetail.HandlingCost);
            param[7] = new SqlParameter("@TravelCost", argCallInvoiceDetail.TravelCost);
            param[8] = new SqlParameter("@SubTotal", argCallInvoiceDetail.SubTotal);
            param[9] = new SqlParameter("@SalesVatPer", argCallInvoiceDetail.SalesVatPer);
            param[10] = new SqlParameter("@SalesVatAmt", argCallInvoiceDetail.SalesVatAmt);
            param[11] = new SqlParameter("@ServiceTaxPer", argCallInvoiceDetail.ServiceTaxPer);
            param[12] = new SqlParameter("@ServiceTaxAmt", argCallInvoiceDetail.ServiceTaxAmt);
            param[13] = new SqlParameter("@NetAmt", argCallInvoiceDetail.NetAmt);
            param[14] = new SqlParameter("@IsPaid", argCallInvoiceDetail.IsPaid);
            param[15] = new SqlParameter("@IsExempted", argCallInvoiceDetail.IsExempted);
            param[16] = new SqlParameter("@PartnerCode", argCallInvoiceDetail.PartnerCode);
            param[17] = new SqlParameter("@ClientCode", argCallInvoiceDetail.ClientCode);
            param[18] = new SqlParameter("@CreatedBy", argCallInvoiceDetail.CreatedBy);
            param[19] = new SqlParameter("@ModifiedBy", argCallInvoiceDetail.ModifiedBy);
          
            param[20] = new SqlParameter("@Type", SqlDbType.Char);
            param[20].Size = 1;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[21].Size = 255;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[22].Size = 20;
            param[22].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallInvoiceDetail", param);


            string strMessage = Convert.ToString(param[21].Value);
            string strType = Convert.ToString(param[20].Value);
            string strRetValue = Convert.ToString(param[22].Value);


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
        
        public void UpdateCallInvoiceDetail(CallInvoiceDetail argCallInvoiceDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@CallCode", argCallInvoiceDetail.CallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallInvoiceDetail.CallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argCallInvoiceDetail.RepairProcDocCode);
            param[3] = new SqlParameter("@PartsCost", argCallInvoiceDetail.PartsCost);
            param[4] = new SqlParameter("@LabourCost", argCallInvoiceDetail.LabourCost);
            param[5] = new SqlParameter("@ShippingCost", argCallInvoiceDetail.ShippingCost);
            param[6] = new SqlParameter("@HandlingCost", argCallInvoiceDetail.HandlingCost);
            param[7] = new SqlParameter("@TravelCost", argCallInvoiceDetail.TravelCost);
            param[8] = new SqlParameter("@SubTotal", argCallInvoiceDetail.SubTotal);
            param[9] = new SqlParameter("@SalesVatPer", argCallInvoiceDetail.SalesVatPer);
            param[10] = new SqlParameter("@SalesVatAmt", argCallInvoiceDetail.SalesVatAmt);
            param[11] = new SqlParameter("@ServiceTaxPer", argCallInvoiceDetail.ServiceTaxPer);
            param[12] = new SqlParameter("@ServiceTaxAmt", argCallInvoiceDetail.ServiceTaxAmt);
            param[13] = new SqlParameter("@NetAmt", argCallInvoiceDetail.NetAmt);
            param[14] = new SqlParameter("@IsPaid", argCallInvoiceDetail.IsPaid);
            param[15] = new SqlParameter("@IsExempted", argCallInvoiceDetail.IsExempted);
            param[16] = new SqlParameter("@PartnerCode", argCallInvoiceDetail.PartnerCode);
            param[17] = new SqlParameter("@ClientCode", argCallInvoiceDetail.ClientCode);
            param[18] = new SqlParameter("@CreatedBy", argCallInvoiceDetail.CreatedBy);
            param[19] = new SqlParameter("@ModifiedBy", argCallInvoiceDetail.ModifiedBy);
           
            param[20] = new SqlParameter("@Type", SqlDbType.Char);
            param[20].Size = 1;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[21].Size = 255;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[22].Size = 20;
            param[22].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallInvoiceDetail", param);
            
            string strMessage = Convert.ToString(param[21].Value);
            string strType = Convert.ToString(param[20].Value);
            string strRetValue = Convert.ToString(param[22].Value);


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
        
        public ICollection<ErrorHandler> DeleteCallInvoiceDetail(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@CallCode", argCallCode);
                param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
                param[2] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
                param[3] = new SqlParameter("@ClientCode", argClientCode);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;
                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;
                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteCallInvoiceDetail", param);


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
        
        public bool blnIsCallInvoiceDetailExists(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode)
        {
            bool IsCallInvoiceDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetCallInvoiceDetail(argCallCode, argCallItemNo, argRepairProcDocCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallInvoiceDetailExists = true;
            }
            else
            {
                IsCallInvoiceDetailExists = false;
            }
            return IsCallInvoiceDetailExists;
        }

        public bool blnIsCallInvoiceDetailExists(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode, DataAccess da)
        {
            bool IsCallInvoiceDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetCallInvoiceDetail(argCallCode, argCallItemNo, argRepairProcDocCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallInvoiceDetailExists = true;
            }
            else
            {
                IsCallInvoiceDetailExists = false;
            }
            return IsCallInvoiceDetailExists;
        }

    }
}