
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
    public class CallTypeManager
    {
        const string CallTypeTable = "CallType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public CallType objGetCallType(string argCallTypeCode, string argClientCode)
        {
            CallType argCallType = new CallType();
            DataSet DataSetToFill = new DataSet();

            if (argCallTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallType(argCallTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallType = this.objCreateCallType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallType;
        }

        public ICollection<CallType> colGetCallType(string argClientCode)
        {
            List<CallType> lst = new List<CallType>();
            DataSet DataSetToFill = new DataSet();
            CallType tCallType = new CallType();

            DataSetToFill = this.GetCallType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetCallType(string argCallTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CallTypeCode", argCallTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallType(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + CallTypeTable.ToString();

                if (iIsDeleted > -1)
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " IsDeleted = " + iIsDeleted;
                }

                if (argClientCode != "")
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " ClientCode = '" + argClientCode + "'";
                }

                ds = da.FillDataSetWithSQL(tSQL);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }
        
        public DataSet GetCallType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            
            SqlParameter[] param = new SqlParameter[1];            
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetCallType",param);
            
            return DataSetToFill;
        }

        public DataSet GetCallType4Call(string argRepairDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RepairDocTypeCode", argRepairDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetCallType4Call", param);
            
            return DataSetToFill;
        }
               


        private CallType objCreateCallType(DataRow dr)
        {
            CallType tCallType = new CallType();

            tCallType.SetObjectInfo(dr);

            return tCallType;

        }

        public ICollection<ErrorHandler> SaveCallType(CallType argCallType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallTypeExists(argCallType.CallTypeCode, argCallType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallType(argCallType, da, lstErr);
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
                    UpdateCallType(argCallType, da, lstErr);
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

        public void InsertCallType(CallType argCallType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@CallTypeCode", argCallType.CallTypeCode);
            param[1] = new SqlParameter("@CallTypeDesc", argCallType.CallTypeDesc);
            param[2] = new SqlParameter("@IsReceivable", argCallType.IsReceivable);
            param[3] = new SqlParameter("@IsApprovalReq", argCallType.IsApprovalReq);
            param[4] = new SqlParameter("@IsAdvReplacement", argCallType.IsAdvReplacement);
            param[5] = new SqlParameter("@IsTopLevelItem", argCallType.IsTopLevelItem);
            param[6] = new SqlParameter("@IsBomExplodAllowed", argCallType.IsBomExplodAllowed);
            param[7] = new SqlParameter("@IsBOMItemReceivedChk", argCallType.IsBOMItemReceivedChk);
            param[8] = new SqlParameter("@CallCloseByCertify", argCallType.CallCloseByCertify);
            param[9] = new SqlParameter("@DefaultStoreCode", argCallType.DefaultStoreCode);
            param[10] = new SqlParameter("@DefaultStockIndicator", argCallType.DefaultStockIndicator);
            param[11] = new SqlParameter("@MaterialDocTypeCode", argCallType.MaterialDocTypeCode);
            param[12] = new SqlParameter("@DefaultNRStoreCode", argCallType.DefaultNRStoreCode);
            param[13] = new SqlParameter("@DefaultNRStockIndicator", argCallType.DefaultNRStockIndicator);
            param[14] = new SqlParameter("@ClientCode", argCallType.ClientCode);
            param[15] = new SqlParameter("@CreatedBy", argCallType.CreatedBy);
            param[16] = new SqlParameter("@ModifiedBy", argCallType.ModifiedBy);

            param[17] = new SqlParameter("@Type", SqlDbType.Char);
            param[17].Size = 1;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[18].Size = 255;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[19].Size = 20;
            param[19].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallType", param);


            string strMessage = Convert.ToString(param[18].Value);
            string strType = Convert.ToString(param[17].Value);
            string strRetValue = Convert.ToString(param[19].Value);


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

        public void UpdateCallType(CallType argCallType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@CallTypeCode", argCallType.CallTypeCode);
            param[1] = new SqlParameter("@CallTypeDesc", argCallType.CallTypeDesc);
            param[2] = new SqlParameter("@IsReceivable", argCallType.IsReceivable);
            param[3] = new SqlParameter("@IsApprovalReq", argCallType.IsApprovalReq);
            param[4] = new SqlParameter("@IsAdvReplacement", argCallType.IsAdvReplacement);
            param[5] = new SqlParameter("@IsTopLevelItem", argCallType.IsTopLevelItem);
            param[6] = new SqlParameter("@IsBomExplodAllowed", argCallType.IsBomExplodAllowed);
            param[7] = new SqlParameter("@IsBOMItemReceivedChk", argCallType.IsBOMItemReceivedChk);
            param[8] = new SqlParameter("@CallCloseByCertify", argCallType.CallCloseByCertify);
            param[9] = new SqlParameter("@DefaultStoreCode", argCallType.DefaultStoreCode);
            param[10] = new SqlParameter("@DefaultStockIndicator", argCallType.DefaultStockIndicator);
            param[11] = new SqlParameter("@MaterialDocTypeCode", argCallType.MaterialDocTypeCode);
            param[12] = new SqlParameter("@DefaultNRStoreCode", argCallType.DefaultNRStoreCode);
            param[13] = new SqlParameter("@DefaultNRStockIndicator", argCallType.DefaultNRStockIndicator);
            param[14] = new SqlParameter("@ClientCode", argCallType.ClientCode);
            param[15] = new SqlParameter("@CreatedBy", argCallType.CreatedBy);
            param[16] = new SqlParameter("@ModifiedBy", argCallType.ModifiedBy);

            param[17] = new SqlParameter("@Type", SqlDbType.Char);
            param[17].Size = 1;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[18].Size = 255;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[19].Size = 20;
            param[19].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallType", param);


            string strMessage = Convert.ToString(param[18].Value);
            string strType = Convert.ToString(param[17].Value);
            string strRetValue = Convert.ToString(param[19].Value);


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

        public ICollection<ErrorHandler> DeleteCallType(string argCallTypeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CallTypeCode", argCallTypeCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteCallType", param);


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

        public bool blnIsCallTypeExists(string argCallTypeCode, string argClientCode)
        {
            bool IsCallTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetCallType(argCallTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallTypeExists = true;
            }
            else
            {
                IsCallTypeExists = false;
            }
            return IsCallTypeExists;
        }
    }
}