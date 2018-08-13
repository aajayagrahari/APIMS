
//Created On :: 15, January, 2013
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallBillingDocManager
    {
        const string CallBillingDocTable = "CallBillingDoc";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public CallBillingDoc objGetCallBillingDoc(string argCallBillingDocCode, int argBillingItemNo, string argPartnerCode, string argClientCode)
        {
            CallBillingDoc argCallBillingDoc = new CallBillingDoc();
            DataSet DataSetToFill = new DataSet();

            if (argCallBillingDocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argBillingItemNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argPartnerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallBillingDoc(argCallBillingDocCode, argBillingItemNo, argPartnerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallBillingDoc = this.objCreateCallBillingDoc((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallBillingDoc;
        }
        
        public ICollection<CallBillingDoc> colGetCallBillingDoc(string argCallBillingDocCode, string argPartnerCode, string argClientCode)
        {
            List<CallBillingDoc> lst = new List<CallBillingDoc>();
            DataSet DataSetToFill = new DataSet();
            CallBillingDoc tCallBillingDoc = new CallBillingDoc();

            DataSetToFill = this.GetCallBillingDoc(argCallBillingDocCode,  argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallBillingDoc(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public void colGetCallBillingDoc(string argCallBillingDocCode, string argPartnerCode, string argClientCode, ref CallBillingDocCol argCallBillingDocCol)
        {
            DataSet DataSetToFill = new DataSet();
            CallBillingDoc tCallBillingDoc = new CallBillingDoc();

            DataSetToFill = this.GetCallBillingDoc(argCallBillingDocCode, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argCallBillingDocCol.colCallBillingDoc.Add(objCreateCallBillingDoc(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

        }

        public DataSet GetCallBillingDoc(string argCallBillingDocCode, int argBillingItemNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallBillingDocCode", argCallBillingDocCode);
            param[1] = new SqlParameter("@BillingItemNo", argBillingItemNo);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallBillingDoc4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallBillingDoc(string argCallBillingDocCode, int argBillingItemNo, string argPartnerCode, string argClientCode, DataAccess da)
        {
           // DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallBillingDocCode", argCallBillingDocCode);
            param[1] = new SqlParameter("@BillingItemNo", argBillingItemNo);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallBillingDoc4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallBillingDoc(string argCallBillingDocCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallBillingDocCode", argCallBillingDocCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallBillingDoc",param);
            return DataSetToFill;
        }

        public DataSet GetBillingDoc4Call(string argCallCode, int argCallItemNo, string argCallClosingCode, double argDiscPer, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[2] = new SqlParameter("@CallClosingCode", argCallClosingCode);
            param[3] = new SqlParameter("@DiscPer", argDiscPer);
            param[4] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[5] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_CreateBillingDoc4Call", param);
            return DataSetToFill;
        }

        
        private CallBillingDoc objCreateCallBillingDoc(DataRow dr)
        {
            CallBillingDoc tCallBillingDoc = new CallBillingDoc();

            tCallBillingDoc.SetObjectInfo(dr);

            return tCallBillingDoc;

        }
        
        public ICollection<ErrorHandler> SaveCallBillingDoc(CallBillingDoc argCallBillingDoc)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallBillingDocExists(argCallBillingDoc.CallBillingDocCode, argCallBillingDoc.BillingItemNo, argCallBillingDoc.PartnerCode, argCallBillingDoc.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallBillingDoc(argCallBillingDoc, da, lstErr);
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
                    UpdateCallBillingDoc(argCallBillingDoc, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
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
            return lstErr;
        }

        public void SaveCallBillingDoc(CallBillingDoc argCallBillingDoc, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCallBillingDocExists(argCallBillingDoc.CallBillingDocCode, argCallBillingDoc.BillingItemNo, argCallBillingDoc.PartnerCode, argCallBillingDoc.ClientCode, da) == false)
                {
                    InsertCallBillingDoc(argCallBillingDoc, da, lstErr);
                }
                else
                {
                    UpdateCallBillingDoc(argCallBillingDoc, da, lstErr);
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
        
        public void InsertCallBillingDoc(CallBillingDoc argCallBillingDoc, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[22];

            param[0] = new SqlParameter("@CallBillingDocCode", argCallBillingDoc.CallBillingDocCode);
            param[1] = new SqlParameter("@CallCode", argCallBillingDoc.CallCode);
            param[2] = new SqlParameter("@CallItemNo", argCallBillingDoc.CallItemNo);
            param[3] = new SqlParameter("@CallClosingCode", argCallBillingDoc.CallClosingCode);
            param[4] = new SqlParameter("@BillingItemNo", argCallBillingDoc.BillingItemNo);
            param[5] = new SqlParameter("@BillingDate", argCallBillingDoc.BillingDate);
            param[6] = new SqlParameter("@ProcedureType", argCallBillingDoc.ProcedureType);
            param[7] = new SqlParameter("@LineItemNo", argCallBillingDoc.LineItemNo);
            param[8] = new SqlParameter("@PricingDescription", argCallBillingDoc.PricingDescription);
            param[9] = new SqlParameter("@BaseAmount", argCallBillingDoc.BaseAmount);
            param[10] = new SqlParameter("@CalCulationOn", argCallBillingDoc.CalCulationOn);
            param[11] = new SqlParameter("@CalculationType", argCallBillingDoc.CalculationType);
            param[12] = new SqlParameter("@CalculationValue", argCallBillingDoc.CalculationValue);
            param[13] = new SqlParameter("@DiscountPer", argCallBillingDoc.DiscountPer);
            param[14] = new SqlParameter("@DiscountAmt", argCallBillingDoc.DiscountAmt);
            param[15] = new SqlParameter("@PartnerCode", argCallBillingDoc.PartnerCode);
            param[16] = new SqlParameter("@ClientCode", argCallBillingDoc.ClientCode);
            param[17] = new SqlParameter("@CreatedBy", argCallBillingDoc.CreatedBy);
            param[18] = new SqlParameter("@ModifiedBy", argCallBillingDoc.ModifiedBy);
           
            param[19] = new SqlParameter("@Type", SqlDbType.Char);
            param[19].Size = 1;
            param[19].Direction = ParameterDirection.Output;

            param[20] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[20].Size = 255;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[21].Size = 20;
            param[21].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallBillingDoc", param);
            
            string strMessage = Convert.ToString(param[20].Value);
            string strType = Convert.ToString(param[19].Value);
            string strRetValue = Convert.ToString(param[21].Value);


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
        
        public void UpdateCallBillingDoc(CallBillingDoc argCallBillingDoc, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[22];

            param[0] = new SqlParameter("@CallBillingDocCode", argCallBillingDoc.CallBillingDocCode);
            param[1] = new SqlParameter("@CallCode", argCallBillingDoc.CallCode);
            param[2] = new SqlParameter("@CallItemNo", argCallBillingDoc.CallItemNo);
            param[3] = new SqlParameter("@CallClosingCode", argCallBillingDoc.CallClosingCode);
            param[4] = new SqlParameter("@BillingItemNo", argCallBillingDoc.BillingItemNo);
            param[5] = new SqlParameter("@BillingDate", argCallBillingDoc.BillingDate);
            param[6] = new SqlParameter("@ProcedureType", argCallBillingDoc.ProcedureType);
            param[7] = new SqlParameter("@LineItemNo", argCallBillingDoc.LineItemNo);
            param[8] = new SqlParameter("@PricingDescription", argCallBillingDoc.PricingDescription);
            param[9] = new SqlParameter("@BaseAmount", argCallBillingDoc.BaseAmount);
            param[10] = new SqlParameter("@CalCulationOn", argCallBillingDoc.CalCulationOn);
            param[11] = new SqlParameter("@CalculationType", argCallBillingDoc.CalculationType);
            param[12] = new SqlParameter("@CalculationValue", argCallBillingDoc.CalculationValue);
            param[13] = new SqlParameter("@DiscountPer", argCallBillingDoc.DiscountPer);
            param[14] = new SqlParameter("@DiscountAmt", argCallBillingDoc.DiscountAmt);
            param[15] = new SqlParameter("@PartnerCode", argCallBillingDoc.PartnerCode);
            param[16] = new SqlParameter("@ClientCode", argCallBillingDoc.ClientCode);
            param[17] = new SqlParameter("@CreatedBy", argCallBillingDoc.CreatedBy);
            param[18] = new SqlParameter("@ModifiedBy", argCallBillingDoc.ModifiedBy);

            param[19] = new SqlParameter("@Type", SqlDbType.Char);
            param[19].Size = 1;
            param[19].Direction = ParameterDirection.Output;

            param[20] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[20].Size = 255;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[21].Size = 20;
            param[21].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallBillingDoc", param);
            
            string strMessage = Convert.ToString(param[20].Value);
            string strType = Convert.ToString(param[19].Value);
            string strRetValue = Convert.ToString(param[21].Value);


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
        
        public ICollection<ErrorHandler> DeleteCallBillingDoc(string argCallCode, int argCallItemNo, string argCallClosingCode, int argBillingItemNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            try
            {

                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@CallCode", argCallCode);
                param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
                param[2] = new SqlParameter("@CallClosingCode", argCallClosingCode);
                param[3] = new SqlParameter("@BillingItemNo", argBillingItemNo);
                param[4] = new SqlParameter("@PartnerCode", argPartnerCode);
                param[5] = new SqlParameter("@ClientCode", argClientCode);

                param[6] = new SqlParameter("@Type", SqlDbType.Char);
                param[6].Size = 1;
                param[6].Direction = ParameterDirection.Output;
                param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[7].Size = 255;
                param[7].Direction = ParameterDirection.Output;
                param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[8].Size = 20;
                param[8].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteCallBillingDoc", param);


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

        public bool blnIsCallBillingDocExists(string argCallBillingDocCode, int argBillingItemNo, string argPartnerCode, string argClientCode)
        {
            bool IsCallBillingDocExists = false;
            DataSet ds = new DataSet();
            ds = GetCallBillingDoc(argCallBillingDocCode, argBillingItemNo, argPartnerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallBillingDocExists = true;
            }
            else
            {
                IsCallBillingDocExists = false;
            }
            return IsCallBillingDocExists;
        }
        public bool blnIsCallBillingDocExists(string argCallBillingDocCode, int argBillingItemNo, string argPartnerCode, string argClientCode, DataAccess da)
        {
            bool IsCallBillingDocExists = false;
            DataSet ds = new DataSet();
            ds = GetCallBillingDoc(argCallBillingDocCode, argBillingItemNo, argPartnerCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallBillingDocExists = true;
            }
            else
            {
                IsCallBillingDocExists = false;
            }
            return IsCallBillingDocExists;
        }
    }
}