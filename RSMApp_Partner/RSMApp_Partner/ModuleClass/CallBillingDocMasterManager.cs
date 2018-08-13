    
//Created On :: 16, January, 2013
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallBillingDocMasterManager
    {
        const string CallBillingDocMasterTable = "CallBillingDocMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public CallBillingDocMaster objGetCallBillingDocMaster(string argCallBillingDocCode, string argPartnerCode, string argClientCode)
        {
            CallBillingDocMaster argCallBillingDocMaster = new CallBillingDocMaster();
            DataSet DataSetToFill = new DataSet();

            if (argCallBillingDocCode.Trim() == "")
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

            DataSetToFill = this.GetCallBillingDocMaster(argCallBillingDocCode, argPartnerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallBillingDocMaster = this.objCreateCallBillingDocMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallBillingDocMaster;
        }


        public ICollection<CallBillingDocMaster> colGetCallBillingDocMaster( string argPartnerCode, string argClientCode)
        {
            List<CallBillingDocMaster> lst = new List<CallBillingDocMaster>();
            DataSet DataSetToFill = new DataSet();
            CallBillingDocMaster tCallBillingDocMaster = new CallBillingDocMaster();

            DataSetToFill = this.GetCallBillingDocMaster(argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallBillingDocMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetCallBillingDocMaster(string argCallBillingDocCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallBillingDocCode", argCallBillingDocCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallBillingDocMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallBillingDocMaster(string argCallBillingDocCode, string argPartnerCode, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallBillingDocCode", argCallBillingDocCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallBillingDocMaster4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCallBillingDocMaster(string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallBillingDocMaster", param);
            return DataSetToFill;
        }
        
        private CallBillingDocMaster objCreateCallBillingDocMaster(DataRow dr)
        {
            CallBillingDocMaster tCallBillingDocMaster = new CallBillingDocMaster();

            tCallBillingDocMaster.SetObjectInfo(dr);

            return tCallBillingDocMaster;

        }
        
        public ICollection<ErrorHandler> SaveCallBillingDocMaster(CallBillingDocMaster argCallBillingDocMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallBillingDocMasterExists(argCallBillingDocMaster.CallBillingDocCode, argCallBillingDocMaster.PartnerCode, argCallBillingDocMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallBillingDocMaster(argCallBillingDocMaster, da, lstErr);
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
                    UpdateCallBillingDocMaster(argCallBillingDocMaster, da, lstErr);
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

        public string SaveCallBillingDocMaster(CallBillingDocMaster argCallBillingDocMaster,  DataAccess da, List<ErrorHandler> lstErr)
        {
            string strRetValue = "";
            try
            {
                if (blnIsCallBillingDocMasterExists(argCallBillingDocMaster.CallBillingDocCode, argCallBillingDocMaster.PartnerCode, argCallBillingDocMaster.ClientCode, da) == false)
                {
                   strRetValue =  InsertCallBillingDocMaster(argCallBillingDocMaster, da, lstErr);
                }
                else
                {
                   strRetValue =  UpdateCallBillingDocMaster(argCallBillingDocMaster, da, lstErr);
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

            return strRetValue;
        }
        
        public string InsertCallBillingDocMaster(CallBillingDocMaster argCallBillingDocMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@CallBillingDocCode", argCallBillingDocMaster.CallBillingDocCode);
            param[1] = new SqlParameter("@CallBillingDocTypeCode", argCallBillingDocMaster.CallBillingDocTypeCode);
            param[2] = new SqlParameter("@BillingDate", argCallBillingDocMaster.BillingDate);
            param[3] = new SqlParameter("@CallClosingCode", argCallBillingDocMaster.CallClosingCode);
            param[4] = new SqlParameter("@GrossValue", argCallBillingDocMaster.GrossValue);
            param[5] = new SqlParameter("@DiscPer", argCallBillingDocMaster.DiscPer);
            param[6] = new SqlParameter("@DiscValue", argCallBillingDocMaster.DiscValue);
            param[7] = new SqlParameter("@NetValue", argCallBillingDocMaster.NetValue);
            param[8] = new SqlParameter("@PaidValue", argCallBillingDocMaster.PaidValue);
            param[9] = new SqlParameter("@PartnerCode", argCallBillingDocMaster.PartnerCode);
            param[10] = new SqlParameter("@ClientCode", argCallBillingDocMaster.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argCallBillingDocMaster.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argCallBillingDocMaster.ModifiedBy);
          
            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallBillingDocMaster", param);


            string strMessage = Convert.ToString(param[14].Value);
            string strType = Convert.ToString(param[13].Value);
            string strRetValue = Convert.ToString(param[15].Value);


            objErrorHandler.Type = strType;
            objErrorHandler.MsgId = 0;
            objErrorHandler.Module = ErrorConstant.strInsertModule;
            objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
            objErrorHandler.Message = strMessage.ToString();
            objErrorHandler.RowNo = 0;
            objErrorHandler.FieldName = "";
            objErrorHandler.LogCode = "";
            lstErr.Add(objErrorHandler);

            return strRetValue;

        }
        
        public string UpdateCallBillingDocMaster(CallBillingDocMaster argCallBillingDocMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@CallBillingDocCode", argCallBillingDocMaster.CallBillingDocCode);
            param[1] = new SqlParameter("@CallBillingDocTypeCode", argCallBillingDocMaster.CallBillingDocTypeCode);
            param[2] = new SqlParameter("@BillingDate", argCallBillingDocMaster.BillingDate);
            param[3] = new SqlParameter("@CallClosingCode", argCallBillingDocMaster.CallClosingCode);
            param[4] = new SqlParameter("@GrossValue", argCallBillingDocMaster.GrossValue);
            param[5] = new SqlParameter("@DiscPer", argCallBillingDocMaster.DiscPer);
            param[6] = new SqlParameter("@DiscValue", argCallBillingDocMaster.DiscValue);
            param[7] = new SqlParameter("@NetValue", argCallBillingDocMaster.NetValue);
            param[8] = new SqlParameter("@PaidValue", argCallBillingDocMaster.PaidValue);
            param[9] = new SqlParameter("@PartnerCode", argCallBillingDocMaster.PartnerCode);
            param[10] = new SqlParameter("@ClientCode", argCallBillingDocMaster.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argCallBillingDocMaster.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argCallBillingDocMaster.ModifiedBy);
          
            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallBillingDocMaster", param);


            string strMessage = Convert.ToString(param[14].Value);
            string strType = Convert.ToString(param[13].Value);
            string strRetValue = Convert.ToString(param[15].Value);


            objErrorHandler.Type = strType;
            objErrorHandler.MsgId = 0;
            objErrorHandler.Module = ErrorConstant.strInsertModule;
            objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
            objErrorHandler.Message = strMessage.ToString();
            objErrorHandler.RowNo = 0;
            objErrorHandler.FieldName = "";
            objErrorHandler.LogCode = "";
            lstErr.Add(objErrorHandler);

            return strRetValue;
        }
        
        public ICollection<ErrorHandler> DeleteCallBillingDocMaster(string argCallBillingDocCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CallBillingDocCode", argCallBillingDocCode);
                param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCallBillingDocMaster", param);


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
        
        public bool blnIsCallBillingDocMasterExists(string argCallBillingDocCode, string argPartnerCode, string argClientCode)
        {
            bool IsCallBillingDocMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetCallBillingDocMaster(argCallBillingDocCode, argPartnerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallBillingDocMasterExists = true;
            }
            else
            {
                IsCallBillingDocMasterExists = false;
            }
            return IsCallBillingDocMasterExists;
        }

        public bool blnIsCallBillingDocMasterExists(string argCallBillingDocCode, string argPartnerCode, string argClientCode, DataAccess da)
        {
            bool IsCallBillingDocMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetCallBillingDocMaster(argCallBillingDocCode, argPartnerCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallBillingDocMasterExists = true;
            }
            else
            {
                IsCallBillingDocMasterExists = false;
            }
            return IsCallBillingDocMasterExists;
        }


        public string GenerateCallBillingDocCode(string argCallBillingDocCode, string argCallBillingDocTypeCode, string argPartnerCode, string argClientCode, DataAccess da)
        {
          // DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CallBillingDocCode", argCallBillingDocCode);
            param[1] = new SqlParameter("@CallBillingDocTypeCode", argCallBillingDocTypeCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            param[4] = new SqlParameter("@GeneratedCallBillingDocCode", SqlDbType.VarChar);
            param[4].Size = 18;
            param[4].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("SL_GetNewCallBillingDocCode", param);

            string strMessage = Convert.ToString(param[4].Value);

            return strMessage;

        }


    }
}