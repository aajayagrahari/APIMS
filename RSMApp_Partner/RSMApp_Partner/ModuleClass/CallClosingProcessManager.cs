
//Created On :: 18, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallClosingProcessManager
    {
        const string CallClosingProcessTable = "CallClosingProcess";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public CallClosingProcess objGetCallClosingProcess(string argCallCode, int argCallItemNo, string argSerialNo, string argClientCode)
        {
            CallClosingProcess argCallClosingProcess = new CallClosingProcess();
            DataSet DataSetToFill = new DataSet();

            if (argCallCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argCallItemNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argSerialNo.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallClosingProcess(argCallCode, argCallItemNo, argSerialNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallClosingProcess = this.objCreateCallClosingProcess((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallClosingProcess;
        }


        public ICollection<CallClosingProcess> colGetCallClosingProcess(string argCallCode, string argClientCode)
        {
            List<CallClosingProcess> lst = new List<CallClosingProcess>();
            DataSet DataSetToFill = new DataSet();
            CallClosingProcess tCallClosingProcess = new CallClosingProcess();

            DataSetToFill = this.GetCallClosingProcess(argCallCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallClosingProcess(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetCallClosingProcess(string argCallCode, int argCallItemNo, string argSerialNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[2] = new SqlParameter("@SerialNo", argSerialNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallClosingProcess4ID", param);

            return DataSetToFill;
        }


        public DataSet GetCallClosingProcess(string argCallCode, int argCallItemNo, string argSerialNo, string argClientCode, DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[2] = new SqlParameter("@SerialNo", argSerialNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallClosingProcess4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallClosingProcess(string argCallCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallClosingProcess", param);

            return DataSetToFill;
        }

        private CallClosingProcess objCreateCallClosingProcess(DataRow dr)
        {
            CallClosingProcess tCallClosingProcess = new CallClosingProcess();

            tCallClosingProcess.SetObjectInfo(dr);

            return tCallClosingProcess;

        }


        public ICollection<ErrorHandler> SaveCallClosingProcess(CallClosingProcess argCallClosingProcess)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallClosingProcessExists(argCallClosingProcess.CallCode, argCallClosingProcess.CallItemNo, argCallClosingProcess.SerialNo, argCallClosingProcess.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallClosingProcess(argCallClosingProcess, da, lstErr);
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
                    UpdateCallClosingProcess(argCallClosingProcess, da, lstErr);
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


        public void InsertCallClosingProcess(CallClosingProcess argCallClosingProcess, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@CallCode", argCallClosingProcess.CallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallClosingProcess.CallItemNo);
            param[2] = new SqlParameter("@SerialNo", argCallClosingProcess.SerialNo);
            param[3] = new SqlParameter("@MaterialCode", argCallClosingProcess.MaterialCode);
            param[4] = new SqlParameter("@MaterialTypeCode", argCallClosingProcess.MaterialTypeCode);
            param[5] = new SqlParameter("@MatGroup1Code", argCallClosingProcess.MatGroup1Code);
            param[6] = new SqlParameter("@StoreCode", argCallClosingProcess.StoreCode);
            param[7] = new SqlParameter("@MaterialDocTypeCode", argCallClosingProcess.MaterialDocTypeCode);
            param[8] = new SqlParameter("@GoodsMovementCode", argCallClosingProcess.GoodsMovementCode);
            param[9] = new SqlParameter("@GoodsMMItemNo", argCallClosingProcess.GoodsMMItemNo);
            param[10] = new SqlParameter("@Quantity", argCallClosingProcess.Quantity);
            param[11] = new SqlParameter("@UOMCode", argCallClosingProcess.UOMCode);
            param[12] = new SqlParameter("@PartnerCode", argCallClosingProcess.PartnerCode);
            param[13] = new SqlParameter("@ClientCode", argCallClosingProcess.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argCallClosingProcess.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argCallClosingProcess.ModifiedBy);
 
            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertCallClosingProcess", param);


            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);


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


        public void UpdateCallClosingProcess(CallClosingProcess argCallClosingProcess, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@CallCode", argCallClosingProcess.CallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallClosingProcess.CallItemNo);
            param[2] = new SqlParameter("@SerialNo", argCallClosingProcess.SerialNo);
            param[3] = new SqlParameter("@MaterialCode", argCallClosingProcess.MaterialCode);
            param[4] = new SqlParameter("@MaterialTypeCode", argCallClosingProcess.MaterialTypeCode);
            param[5] = new SqlParameter("@MatGroup1Code", argCallClosingProcess.MatGroup1Code);
            param[6] = new SqlParameter("@StoreCode", argCallClosingProcess.StoreCode);
            param[7] = new SqlParameter("@MaterialDocTypeCode", argCallClosingProcess.MaterialDocTypeCode);
            param[8] = new SqlParameter("@GoodsMovementCode", argCallClosingProcess.GoodsMovementCode);
            param[9] = new SqlParameter("@GoodsMMItemNo", argCallClosingProcess.GoodsMMItemNo);
            param[10] = new SqlParameter("@Quantity", argCallClosingProcess.Quantity);
            param[11] = new SqlParameter("@UOMCode", argCallClosingProcess.UOMCode);
            param[12] = new SqlParameter("@PartnerCode", argCallClosingProcess.PartnerCode);
            param[13] = new SqlParameter("@ClientCode", argCallClosingProcess.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argCallClosingProcess.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argCallClosingProcess.ModifiedBy);

            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallClosingProcess", param);


            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);


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


        public ICollection<ErrorHandler> DeleteCallClosingProcess(string argCallCode, int argCallItemNo, string argSerialNo, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@CallCode", argCallCode);
                param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
                param[2] = new SqlParameter("@SerialNo", argSerialNo);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteCallClosingProcess", param);


                string strMessage = Convert.ToString(param[6].Value);
                string strType = Convert.ToString(param[5].Value);
                string strRetValue = Convert.ToString(param[7].Value);


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


        public bool blnIsCallClosingProcessExists(string argCallCode, int argCallItemNo, string argSerialNo, string argClientCode)
        {
            bool IsCallClosingProcessExists = false;
            DataSet ds = new DataSet();
            ds = GetCallClosingProcess(argCallCode, argCallItemNo, argSerialNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallClosingProcessExists = true;
            }
            else
            {
                IsCallClosingProcessExists = false;
            }
            return IsCallClosingProcessExists;
        }
        public bool blnIsCallClosingProcessExists(string argCallCode, int argCallItemNo, string argSerialNo, string argClientCode, DataAccess da)
        {
            bool IsCallClosingProcessExists = false;
            DataSet ds = new DataSet();
            ds = GetCallClosingProcess(argCallCode, argCallItemNo, argSerialNo, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallClosingProcessExists = true;
            }
            else
            {
                IsCallClosingProcessExists = false;
            }
            return IsCallClosingProcessExists;
        }
    }
}