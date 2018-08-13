
//Created On :: 08, December, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallType_CallClosingDocTypeManager
    {
        const string CallType_CallClosingDocTypeTable = "CallType_CallClosingDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public CallType_CallClosingDocType objGetCallType_CallClosingDocType(string argCallClosingDocTypeCode, string argCallTypeCode, int argIsRepairable, string argClientCode)
        {
            CallType_CallClosingDocType argCallType_CallClosingDocType = new CallType_CallClosingDocType();
            DataSet DataSetToFill = new DataSet();

            if (argCallClosingDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argCallTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argIsRepairable <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallType_CallClosingDocType(argCallClosingDocTypeCode, argCallTypeCode, argIsRepairable, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallType_CallClosingDocType = this.objCreateCallType_CallClosingDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallType_CallClosingDocType;
        }


        public ICollection<CallType_CallClosingDocType> colGetCallType_CallClosingDocType(string argClientCode)
        {
            List<CallType_CallClosingDocType> lst = new List<CallType_CallClosingDocType>();
            DataSet DataSetToFill = new DataSet();
            CallType_CallClosingDocType tCallType_CallClosingDocType = new CallType_CallClosingDocType();

            DataSetToFill = this.GetCallType_CallClosingDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallType_CallClosingDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetCallType_CallClosingDocType(string argCallClosingDocTypeCode, string argCallTypeCode, int argIsRepairable, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallClosingDocTypeCode", argCallClosingDocTypeCode);
            param[1] = new SqlParameter("@CallTypeCode", argCallTypeCode);
            param[2] = new SqlParameter("@IsRepairable", argIsRepairable);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallType_CallClosingDocType4ID", param);

            return DataSetToFill;
        }


        public DataSet GetCallType_CallClosingDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallType_CallClosingDocType", param);
            return DataSetToFill;
        }


        private CallType_CallClosingDocType objCreateCallType_CallClosingDocType(DataRow dr)
        {
            CallType_CallClosingDocType tCallType_CallClosingDocType = new CallType_CallClosingDocType();

            tCallType_CallClosingDocType.SetObjectInfo(dr);

            return tCallType_CallClosingDocType;

        }


        public ICollection<ErrorHandler> SaveCallType_CallClosingDocType(CallType_CallClosingDocType argCallType_CallClosingDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallType_CallClosingDocTypeExists(argCallType_CallClosingDocType.CallClosingDocTypeCode, argCallType_CallClosingDocType.CallTypeCode, argCallType_CallClosingDocType.IsRepairable, argCallType_CallClosingDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallType_CallClosingDocType(argCallType_CallClosingDocType, da, lstErr);
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
                    UpdateCallType_CallClosingDocType(argCallType_CallClosingDocType, da, lstErr);
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


        public void InsertCallType_CallClosingDocType(CallType_CallClosingDocType argCallType_CallClosingDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@CallClosingDocTypeCode", argCallType_CallClosingDocType.CallClosingDocTypeCode);
            param[1] = new SqlParameter("@CallTypeCode", argCallType_CallClosingDocType.CallTypeCode);
            param[2] = new SqlParameter("@GRStatus", argCallType_CallClosingDocType.GRStatus);
            param[3] = new SqlParameter("@IsRepairable", argCallType_CallClosingDocType.IsRepairable);
            param[4] = new SqlParameter("@MaterialDocTypeCode", argCallType_CallClosingDocType.MaterialDocTypeCode);
            param[5] = new SqlParameter("@RepairProcessCode", argCallType_CallClosingDocType.RepairProcessCode);
            param[6] = new SqlParameter("@DefaultStoreCode", argCallType_CallClosingDocType.DefaultStoreCode);
            param[7] = new SqlParameter("@DefaultStockIndicator", argCallType_CallClosingDocType.DefaultStockIndicator);
            param[8] = new SqlParameter("@ClientCode", argCallType_CallClosingDocType.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argCallType_CallClosingDocType.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argCallType_CallClosingDocType.ModifiedBy);
           
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallType_CallClosingDocType", param);


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


        public void UpdateCallType_CallClosingDocType(CallType_CallClosingDocType argCallType_CallClosingDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@CallClosingDocTypeCode", argCallType_CallClosingDocType.CallClosingDocTypeCode);
            param[1] = new SqlParameter("@CallTypeCode", argCallType_CallClosingDocType.CallTypeCode);
            param[2] = new SqlParameter("@GRStatus", argCallType_CallClosingDocType.GRStatus);
            param[3] = new SqlParameter("@IsRepairable", argCallType_CallClosingDocType.IsRepairable);
            param[4] = new SqlParameter("@MaterialDocTypeCode", argCallType_CallClosingDocType.MaterialDocTypeCode);
            param[5] = new SqlParameter("@RepairProcessCode", argCallType_CallClosingDocType.RepairProcessCode);
            param[6] = new SqlParameter("@DefaultStoreCode", argCallType_CallClosingDocType.DefaultStoreCode);
            param[7] = new SqlParameter("@DefaultStockIndicator", argCallType_CallClosingDocType.DefaultStockIndicator);
            param[8] = new SqlParameter("@ClientCode", argCallType_CallClosingDocType.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argCallType_CallClosingDocType.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argCallType_CallClosingDocType.ModifiedBy);
            
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;
            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;
            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateCallType_CallClosingDocType", param);


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


        public ICollection<ErrorHandler> DeleteCallType_CallClosingDocType(string argCallClosingDocTypeCode, string argCallTypeCode, int argIsRepairable, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@CallClosingDocTypeCode", argCallClosingDocTypeCode);
                param[1] = new SqlParameter("@CallTypeCode", argCallTypeCode);
                param[2] = new SqlParameter("@IsRepairable", argIsRepairable);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCallType_CallClosingDocType", param);


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


        public bool blnIsCallType_CallClosingDocTypeExists(string argCallClosingDocTypeCode, string argCallTypeCode, int argIsRepairable, string argClientCode)
        {
            bool IsCallType_CallClosingDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetCallType_CallClosingDocType(argCallClosingDocTypeCode, argCallTypeCode, argIsRepairable, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallType_CallClosingDocTypeExists = true;
            }
            else
            {
                IsCallType_CallClosingDocTypeExists = false;
            }
            return IsCallType_CallClosingDocTypeExists;
        }
    }
}