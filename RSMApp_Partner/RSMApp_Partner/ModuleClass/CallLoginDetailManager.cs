
//Created On :: 20, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallLoginDetailManager
    {
        const string CallLoginDetailTable = "CallLoginDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public CallLoginDetail objGetCallLoginDetail(string argCallCode, int argItemNo, string argClientCode)
        {
            CallLoginDetail argCallLoginDetail = new CallLoginDetail();
            DataSet DataSetToFill = new DataSet();

            if (argCallCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argItemNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallLoginDetail(argCallCode, argItemNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallLoginDetail = this.objCreateCallLoginDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallLoginDetail;
        }

        public ICollection<CallLoginDetail> colGetCallLoginDetail(string argCallCode, string argClientCode)
        {
            List<CallLoginDetail> lst = new List<CallLoginDetail>();
            DataSet DataSetToFill = new DataSet();
            CallLoginDetail tCallLoginDetail = new CallLoginDetail();

            DataSetToFill = this.GetCallLoginDetail(argCallCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallLoginDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<CallLoginDetail> colGetCallLoginDetail(string argCallCode, string argClientCode, List<CallLoginDetail> lst)
        {
            //List<SalesOrderDetail> lst = new List<SalesOrderDetail>();
            DataSet DataSetToFill = new DataSet();
            CallLoginDetail tCallLoginDetail = new CallLoginDetail();

            DataSetToFill = this.GetCallLoginDetail(argCallCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallLoginDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public void colGetCallLoginDetail(string argCallCode, string argClientCode, ref CallLoginDetailCol argCallLoginDetailCol)
        {
            DataSet DataSetToFill = new DataSet();
            CallLoginDetail tCallLoginDetail = new CallLoginDetail();

            DataSetToFill = this.GetCallLoginDetail(argCallCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argCallLoginDetailCol.colCallLoginDetail.Add(objCreateCallLoginDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

                
        }

        public void colGetCallLoginDetail(string argCallCode, int argItemNo, string argClientCode, ref CallLoginDetailCol argCallLoginDetailCol)
        {
            DataSet DataSetToFill = new DataSet();
            CallLoginDetail tCallLoginDetail = new CallLoginDetail();

            DataSetToFill = this.GetCallLoginDetail(argCallCode, argItemNo, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argCallLoginDetailCol.colCallLoginDetail.Add(objCreateCallLoginDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;
        }

        public DataSet GetCallLoginDetail(string argCallCode, int argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallLoginDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallLoginDetail(string argCallCode, int argItemNo, string argClientCode, DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallLoginDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallLoginDetail(string argCallCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallLoginDetail",param);
            return DataSetToFill;
        }

        public DataSet GetCallLoginDetail(string argCallDateFrom, string argCallDateTo, string argRepairDocTypeCode, string argCallCode, string argSerialNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@CallDateFrom", argCallDateFrom);
            param[1] = new SqlParameter("@CallDateTo", argCallDateTo);
            param[2] = new SqlParameter("@RepairDocTypeCode", argRepairDocTypeCode);
            param[3] = new SqlParameter("@CallCode", argCallCode);
            param[4] = new SqlParameter("@SerialNo", argSerialNo);
            param[5] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[6] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallLoginDetail4Search", param);
            return DataSetToFill;
        }
        
        public DataSet GetMatGroupAgainstCall(string argCallCode, string argPartnerCode,  string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];            
            param[0] = new SqlParameter("@CallCode", argCallCode);            
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMatGroupAgainstCall", param);
            return DataSetToFill;
        }
        
        public int CheckCallLoginStatus(string argSerialNo, string argMaterialCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            param[4] = new SqlParameter("@RetValue", SqlDbType.Int);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            int i = da.ExecuteNonQuery("SL_CheckCallLoginSerialNo", param);


            int iRetValue = Convert.ToInt32(param[4].Value);

            return iRetValue;           
        }

        public DataSet GetCallToAsgTechnician(string argSerialNo, string argMatGroup1Code, string argMaterialDocTypeCode, string argAssignType, string argFrmStoreCode, string argFrmStockInd, string argFrmPartnerEmployeeCode, string argToPartnerEmployeeCode, string argToStoreCode, string argToStockInd, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[12];

            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@MaterialDocTypeCode", argMaterialDocTypeCode);
            param[3] = new SqlParameter("@AssignType", argAssignType);
            param[4] = new SqlParameter("@FrmStoreCode", argFrmStoreCode);
            param[5] = new SqlParameter("@FrmStockInd", argFrmStockInd);
            param[6] = new SqlParameter("@FrmPartnerEmployeeCode", argFrmPartnerEmployeeCode);
            param[7] = new SqlParameter("@ToPartnerEmployeeCode", argToPartnerEmployeeCode);
            param[8] = new SqlParameter("@ToStoreCode", argToStoreCode);
            param[9] = new SqlParameter("@ToStockInd", argToStockInd);            
            param[10] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[11] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAsgCallToTechnician", param);
            return DataSetToFill;
        }

        public DataSet GetCallLoginDetail4CL(string argCallCode, string argMatGroup1Code, string argSerialNo, string argCallRecName, string argCallRecMobile, string argWarrantyStatus,  string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@SerialNo", argSerialNo);
            param[3] = new SqlParameter("@CallRecName", argCallRecName);
            param[4] = new SqlParameter("@CallRecMobile", argCallRecMobile);
            param[5] = new SqlParameter("@WarrantyStatus", argWarrantyStatus);            
            param[6] = new SqlParameter("@PartnerCode", argPartnerCode);            
            param[7] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallLoginDetail4CL", param);
            return DataSetToFill;
        }

        public DataSet GetCallLoginDetail4AR(string argCallCode, string argMatGroup1Code, string argSerialNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@SerialNo", argSerialNo);
            param[3] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallLoginDetail4AR", param);
            return DataSetToFill;
        }

        public DataSet GetCallLoginDetail4ARRec(string argCallCode, string argMatGroup1Code, string argSerialNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@SerialNo", argSerialNo);
            param[3] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallLoginDetail4ARReceipt", param);
            return DataSetToFill;
        }

        public DataSet GetCallWiseStockAvailablity(string argCallCode, string argPartnerCode, string argClientCode)
        {
             DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallCode", argCallCode);            
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetCallWiseStockAvailable", param);
            return DataSetToFill;
            
        }

        public int CheckMaterialWarrantyStatus(string argMatGroup1Code, string argMaterialCode, string argMastMaterialCode, string argSerialNo, string argMRevisionCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            int iRetValue = 0;

            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@MastMaterialCode", argMastMaterialCode);
            param[3] = new SqlParameter("@SerialNo", argSerialNo);
            param[4] = new SqlParameter("@MRevisionCode", argMRevisionCode);
            param[5] = new SqlParameter("@ClientCode", argClientCode);

            param[6] = new SqlParameter("@RetValue", SqlDbType.Int);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;

            int i = da.ExecuteNonQuery("SL_GetMaterialWarrantyStatus", param);

            iRetValue = Convert.ToInt32(param[6].Value);

            return iRetValue;
        }


        // Function for DOA CallApproval by Field Staff	

        public DataSet GetCallLoginDetail4FieldStaffApp(string argCallTypeCode, string argRepairDocTypeCode, string argCallStatus, DateTime argStartDate, DateTime argEndDate, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@CallTypeCode", argCallTypeCode);
            param[1] = new SqlParameter("@RepairDocTypeCode", argRepairDocTypeCode);
            param[2] = new SqlParameter("@CallStatus", argCallStatus);
            param[3] = new SqlParameter("@StartDate", argStartDate);
            param[4] = new SqlParameter("@EndDate", argEndDate);
            param[5] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetCallLoginDetail4FieldStaffApp", param);
            return DataSetToFill;
        }


        private CallLoginDetail objCreateCallLoginDetail(DataRow dr)
        {
            CallLoginDetail tCallLoginDetail = new CallLoginDetail();

            tCallLoginDetail.SetObjectInfo(dr);

            return tCallLoginDetail;

        }

        public ICollection<ErrorHandler> SaveCallLoginDetail(CallLoginDetail argCallLoginDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallLoginDetailExists(argCallLoginDetail.CallCode, argCallLoginDetail.ItemNo, argCallLoginDetail.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallLoginDetail(argCallLoginDetail, da, lstErr);
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
                    UpdateCallLoginDetail(argCallLoginDetail, da, lstErr);
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

        public void SaveCallLoginDetail(CallLoginDetail argCallLoginDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCallLoginDetailExists(argCallLoginDetail.CallCode, argCallLoginDetail.ItemNo, argCallLoginDetail.ClientCode,da) == false)
                {
                    InsertCallLoginDetail(argCallLoginDetail, da, lstErr);
                }
                else
                {
                    UpdateCallLoginDetail(argCallLoginDetail, da, lstErr);
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

        public void InsertCallLoginDetail(CallLoginDetail argCallLoginDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[65];
            param[0] = new SqlParameter("@CallCode", argCallLoginDetail.CallCode);
            param[1] = new SqlParameter("@ItemNo", argCallLoginDetail.ItemNo);
            param[2] = new SqlParameter("@SerialNo", argCallLoginDetail.SerialNo);
            param[3] = new SqlParameter("@MaterialCode", argCallLoginDetail.MaterialCode);
            param[4] = new SqlParameter("@MaterialTypeCode", argCallLoginDetail.MaterialTypeCode);
            param[5] = new SqlParameter("@MatGroup1Code", argCallLoginDetail.MatGroup1Code);
            param[6] = new SqlParameter("@WarrantyOn", argCallLoginDetail.WarrantyOn);
            param[7] = new SqlParameter("@WarrantyEndDate", argCallLoginDetail.WarrantyEndDate);
            param[8] = new SqlParameter("@ConditionCode", argCallLoginDetail.ConditionCode);
            param[9] = new SqlParameter("@CustComplaint", argCallLoginDetail.CustComplaint);
            param[10] = new SqlParameter("@WarrantyStatus", argCallLoginDetail.WarrantyStatus);
            param[11] = new SqlParameter("@OutWarReason", argCallLoginDetail.OutWarReason);
            param[12] = new SqlParameter("@CallTypeCode", argCallLoginDetail.CallTypeCode);
            param[13] = new SqlParameter("@CallFrom", argCallLoginDetail.CallFrom);
            param[14] = new SqlParameter("@CustInvoiceDate", argCallLoginDetail.CustInvoiceDate);
            param[15] = new SqlParameter("@CustInvoiceNo", argCallLoginDetail.CustInvoiceNo);
            param[16] = new SqlParameter("@CustName", argCallLoginDetail.CustName);
            param[17] = new SqlParameter("@CustAddress1", argCallLoginDetail.CustAddress1);
            param[18] = new SqlParameter("@CustAddress2", argCallLoginDetail.CustAddress2);
            param[19] = new SqlParameter("@CustPhone", argCallLoginDetail.CustPhone);
            param[20] = new SqlParameter("@CustMobile", argCallLoginDetail.CustMobile);
            param[21] = new SqlParameter("@CustEmail", argCallLoginDetail.CustEmail);
            param[22] = new SqlParameter("@CustGender", argCallLoginDetail.CustGender);
            param[23] = new SqlParameter("@CustCountryCode", argCallLoginDetail.CustCountryCode);
            param[24] = new SqlParameter("@CustStateCode", argCallLoginDetail.CustStateCode);
            param[25] = new SqlParameter("@CustCity", argCallLoginDetail.CustCity);
            param[26] = new SqlParameter("@IsAssignToTechnician", argCallLoginDetail.IsAssignToTechnician);
            param[27] = new SqlParameter("@AssignTechNarration", argCallLoginDetail.AssignTechNarration);

            param[28] = new SqlParameter("@IsApproved", argCallLoginDetail.IsApproved);
            
            param[29] = new SqlParameter("@IsCallClosed", argCallLoginDetail.IsCallClosed);
            param[30] = new SqlParameter("@CallCloseDate", argCallLoginDetail.CallCloseDate);


            param[31] = new SqlParameter("@IsGoodsRec", argCallLoginDetail.IsGoodsRec);
            param[32] = new SqlParameter("@GRStatus", argCallLoginDetail.GRStatus);

            param[33] = new SqlParameter("@ReceivedDate", argCallLoginDetail.ReceivedDate);

            param[34] = new SqlParameter("@Quantity", argCallLoginDetail.Quantity);

            param[35] = new SqlParameter("@UOMCode", argCallLoginDetail.UOMCode);
            
            param[36] = new SqlParameter("@PartnerCode", argCallLoginDetail.PartnerCode);
            param[37] = new SqlParameter("@HLMaterialCode ", argCallLoginDetail.HLMaterialCode);
            param[38] = new SqlParameter("@HLItemNo", argCallLoginDetail.HLItemNo);
            param[39] = new SqlParameter("@Narration", argCallLoginDetail.Narration);
            param[40] = new SqlParameter("@RepairStatusCode", argCallLoginDetail.RepairStatusCode);

            param[41] = new SqlParameter("@DefectTypeDesc", argCallLoginDetail.DefectTypeDesc);
            param[42] = new SqlParameter("@EstTotal", argCallLoginDetail.EstTotal);
            param[43] = new SqlParameter("@EstAppStatus", argCallLoginDetail.EstAppStatus);
            param[44] = new SqlParameter("@EstAppDate", argCallLoginDetail.EstAppDate);

            param[45] = new SqlParameter("@AdvRefDocCode", argCallLoginDetail.AdvRefDocCode);
            param[46] = new SqlParameter("@AdvRefDocItemNo", argCallLoginDetail.AdvRefDocItemNo);
            param[47] = new SqlParameter("@AdvRecRefDocCode", argCallLoginDetail.AdvRecRefDocCode);
            param[48] = new SqlParameter("@AdvRecRefDocItemNo", argCallLoginDetail.AdvRecRefDocItemNo);
            param[49] = new SqlParameter("@MaterialDocTypeCode", argCallLoginDetail.MaterialDocTypeCode);
            param[50] = new SqlParameter("@StoreCode", argCallLoginDetail.StoreCode);
            param[51] = new SqlParameter("@StockIndicator", argCallLoginDetail.StockIndicator);

            param[52] = new SqlParameter("@ItemsReceived", argCallLoginDetail.ItemsReceived);
            param[53] = new SqlParameter("@IsCretificateIssued", argCallLoginDetail.IsCretificateIssued);
            param[54] = new SqlParameter("@CretificateIssueDate", argCallLoginDetail.CretificateIssueDate);

            param[55] = new SqlParameter("@OutWarrantyOn", argCallLoginDetail.OutWarrantyOn);

            param[56] = new SqlParameter("@RepairDocTypeCode", argCallLoginDetail.RepairDocTypeCode);
            param[57] = new SqlParameter("@MRevisionCode", argCallLoginDetail.MRevisionCode);
            param[58] = new SqlParameter("@IsRepairable", argCallLoginDetail.IsRepairable);

            param[59] = new SqlParameter("@ClientCode", argCallLoginDetail.ClientCode);
            param[60] = new SqlParameter("@CreatedBy", argCallLoginDetail.CreatedBy);
            param[61] = new SqlParameter("@ModifiedBy", argCallLoginDetail.ModifiedBy);
       
            param[62] = new SqlParameter("@Type", SqlDbType.Char);
            param[62].Size = 1;
            param[62].Direction = ParameterDirection.Output;

            param[63] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[63].Size = 255;
            param[63].Direction = ParameterDirection.Output;

            param[64] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[64].Size = 20;
            param[64].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallLoginDetail", param);
            
            string strMessage = Convert.ToString(param[63].Value);
            string strType = Convert.ToString(param[62].Value);
            string strRetValue = Convert.ToString(param[64].Value);
            
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

        public void UpdateCallLoginDetail(CallLoginDetail argCallLoginDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[65];
            param[0] = new SqlParameter("@CallCode", argCallLoginDetail.CallCode);
            param[1] = new SqlParameter("@ItemNo", argCallLoginDetail.ItemNo);
            param[2] = new SqlParameter("@SerialNo", argCallLoginDetail.SerialNo);
            param[3] = new SqlParameter("@MaterialCode", argCallLoginDetail.MaterialCode);
            param[4] = new SqlParameter("@MaterialTypeCode", argCallLoginDetail.MaterialTypeCode);
            param[5] = new SqlParameter("@MatGroup1Code", argCallLoginDetail.MatGroup1Code);
            param[6] = new SqlParameter("@WarrantyOn", argCallLoginDetail.WarrantyOn);
            param[7] = new SqlParameter("@WarrantyEndDate", argCallLoginDetail.WarrantyEndDate);
            param[8] = new SqlParameter("@ConditionCode", argCallLoginDetail.ConditionCode);
            param[9] = new SqlParameter("@CustComplaint", argCallLoginDetail.CustComplaint);
            param[10] = new SqlParameter("@WarrantyStatus", argCallLoginDetail.WarrantyStatus);
            param[11] = new SqlParameter("@OutWarReason", argCallLoginDetail.OutWarReason);
            param[12] = new SqlParameter("@CallTypeCode", argCallLoginDetail.CallTypeCode);
            param[13] = new SqlParameter("@CallFrom", argCallLoginDetail.CallFrom);
            param[14] = new SqlParameter("@CustInvoiceDate", argCallLoginDetail.CustInvoiceDate);
            param[15] = new SqlParameter("@CustInvoiceNo", argCallLoginDetail.CustInvoiceNo);
            param[16] = new SqlParameter("@CustName", argCallLoginDetail.CustName);
            param[17] = new SqlParameter("@CustAddress1", argCallLoginDetail.CustAddress1);
            param[18] = new SqlParameter("@CustAddress2", argCallLoginDetail.CustAddress2);
            param[19] = new SqlParameter("@CustPhone", argCallLoginDetail.CustPhone);
            param[20] = new SqlParameter("@CustMobile", argCallLoginDetail.CustMobile);
            param[21] = new SqlParameter("@CustEmail", argCallLoginDetail.CustEmail);
            param[22] = new SqlParameter("@CustGender", argCallLoginDetail.CustGender);
            param[23] = new SqlParameter("@CustCountryCode", argCallLoginDetail.CustCountryCode);
            param[24] = new SqlParameter("@CustStateCode", argCallLoginDetail.CustStateCode);
            param[25] = new SqlParameter("@CustCity", argCallLoginDetail.CustCity);
            param[26] = new SqlParameter("@IsAssignToTechnician", argCallLoginDetail.IsAssignToTechnician);
            param[27] = new SqlParameter("@AssignTechNarration", argCallLoginDetail.AssignTechNarration);

            param[28] = new SqlParameter("@IsApproved", argCallLoginDetail.IsApproved);


            param[29] = new SqlParameter("@IsCallClosed", argCallLoginDetail.IsCallClosed);
            param[30] = new SqlParameter("@CallCloseDate", argCallLoginDetail.CallCloseDate);


            param[31] = new SqlParameter("@IsGoodsRec", argCallLoginDetail.IsGoodsRec);
            param[32] = new SqlParameter("@GRStatus", argCallLoginDetail.GRStatus);

            param[33] = new SqlParameter("@ReceivedDate", argCallLoginDetail.ReceivedDate);

            param[34] = new SqlParameter("@Quantity", argCallLoginDetail.Quantity);

            param[35] = new SqlParameter("@UOMCode", argCallLoginDetail.UOMCode);

            param[36] = new SqlParameter("@PartnerCode", argCallLoginDetail.PartnerCode);
            param[37] = new SqlParameter("@HLMaterialCode ", argCallLoginDetail.HLMaterialCode);
            param[38] = new SqlParameter("@HLItemNo", argCallLoginDetail.HLItemNo);
            param[39] = new SqlParameter("@Narration", argCallLoginDetail.Narration);
            param[40] = new SqlParameter("@RepairStatusCode", argCallLoginDetail.RepairStatusCode);

            param[41] = new SqlParameter("@DefectTypeDesc", argCallLoginDetail.DefectTypeDesc);
            param[42] = new SqlParameter("@EstTotal", argCallLoginDetail.EstTotal);
            param[43] = new SqlParameter("@EstAppStatus", argCallLoginDetail.EstAppStatus);
            param[44] = new SqlParameter("@EstAppDate", argCallLoginDetail.EstAppDate);

            param[45] = new SqlParameter("@AdvRefDocCode", argCallLoginDetail.AdvRefDocCode);
            param[46] = new SqlParameter("@AdvRefDocItemNo", argCallLoginDetail.AdvRefDocItemNo);
            param[47] = new SqlParameter("@AdvRecRefDocCode", argCallLoginDetail.AdvRecRefDocCode);
            param[48] = new SqlParameter("@AdvRecRefDocItemNo", argCallLoginDetail.AdvRecRefDocItemNo);
            param[49] = new SqlParameter("@MaterialDocTypeCode", argCallLoginDetail.MaterialDocTypeCode);
            param[50] = new SqlParameter("@StoreCode", argCallLoginDetail.StoreCode);
            param[51] = new SqlParameter("@StockIndicator", argCallLoginDetail.StockIndicator);

            param[52] = new SqlParameter("@ItemsReceived", argCallLoginDetail.ItemsReceived);
            param[53] = new SqlParameter("@IsCretificateIssued", argCallLoginDetail.IsCretificateIssued);
            param[54] = new SqlParameter("@CretificateIssueDate", argCallLoginDetail.CretificateIssueDate);


            param[56] = new SqlParameter("@RepairDocTypeCode", argCallLoginDetail.RepairDocTypeCode);
            param[57] = new SqlParameter("@MRevisionCode", argCallLoginDetail.MRevisionCode);
            param[58] = new SqlParameter("@IsRepairable", argCallLoginDetail.IsRepairable);

            param[59] = new SqlParameter("@ClientCode", argCallLoginDetail.ClientCode);
            param[60] = new SqlParameter("@CreatedBy", argCallLoginDetail.CreatedBy);
            param[61] = new SqlParameter("@ModifiedBy", argCallLoginDetail.ModifiedBy);

            param[62] = new SqlParameter("@Type", SqlDbType.Char);
            param[62].Size = 1;
            param[62].Direction = ParameterDirection.Output;

            param[63] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[63].Size = 255;
            param[63].Direction = ParameterDirection.Output;

            param[64] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[64].Size = 20;
            param[64].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallLoginDetail", param);
            
            string strMessage = Convert.ToString(param[63].Value);
            string strType = Convert.ToString(param[62].Value);
            string strRetValue = Convert.ToString(param[64].Value);


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

        public ICollection<ErrorHandler> DeleteCallLoginDetail(string argCallCode, int argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CallCode", argCallCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@ClientCode", argClientCode);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;
                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteCallLoginDetail", param);


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

        public ICollection<ErrorHandler> DeleteCallLoginDetail(string argCallCode, int argItemNo, string argClientCode, int iIsDeleted, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@CallCode", argCallCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
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

                int i = da.NExecuteNonQuery("Proc_DeleteCallLoginDetail", param);


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

        public bool blnIsCallLoginDetailExists(string argCallCode, int argItemNo, string argClientCode)
        {
            bool IsCallLoginDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetCallLoginDetail(argCallCode, argItemNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallLoginDetailExists = true;
            }
            else
            {
                IsCallLoginDetailExists = false;
            }
            return IsCallLoginDetailExists;
        }

        public bool blnIsCallLoginDetailExists(string argCallCode, int argItemNo, string argClientCode, DataAccess da)
        {
            bool IsCallLoginDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetCallLoginDetail(argCallCode, argItemNo, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallLoginDetailExists = true;
            }
            else
            {
                IsCallLoginDetailExists = false;
            }
            return IsCallLoginDetailExists;
        }
        
        /*--------------------------------------------------------------------------------------------------------------------------
        * Report Section
        -------------------------------------------------------------------------------------------------------------------------------*/

        public DataSet GetCallLoginDetail4customerReceipt(string argPartnerCode, string argCallCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@CallCode", argCallCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetCallLogin4CustomerReceipt", param);
            return DataSetToFill;
        }
        
        public DataSet GetCallLoginDetail4TableReplacement(string argCallClosingDocTypeCode, string argPartnerCode, string argCallCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallClosingDocTypeCode", argCallClosingDocTypeCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@CallCode", argCallCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetCallLogIn4TableReplacement", param);
            return DataSetToFill;
        }
        
        public DataSet GetCallLoginDetail4CustomerIssue(string argCallClosingDocTypeCode, string argPartnerCode, string argCallCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallClosingDocTypeCode", argCallClosingDocTypeCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@CallCode", argCallCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetCallLogIn4CustomerIssue", param);
            return DataSetToFill;
        }
        
        public DataSet GetCallLoginDetail4CP(string argCallDateFrom, string argCallDateTo, string argRepairDocTypeCode, string argCallReceivedFrm, string argCallRecName, string argCallRecMobile, string argCallCode, string argSerialNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@CallDateFrom", argCallDateFrom);
            param[1] = new SqlParameter("@CallDateTo", argCallDateTo);
            param[2] = new SqlParameter("@RepairDocTypeCode", argRepairDocTypeCode);
            param[3] = new SqlParameter("@CallReceivedFrm", argCallReceivedFrm);
            param[4] = new SqlParameter("@CallRecName", argCallRecName);
            param[5] = new SqlParameter("@CallRecMobile", argCallRecMobile);
            param[6] = new SqlParameter("@CallCode", argCallCode);
            param[7] = new SqlParameter("@SerialNo", argSerialNo);
            param[8] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[9] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetCallLoginDetail4CP", param);
            return DataSetToFill;
        }
        
        /*--------------------------------------------------------------------------------------------------------------------------
         * Report Section ENDS
         -------------------------------------------------------------------------------------------------------------------------------*/



    }
}