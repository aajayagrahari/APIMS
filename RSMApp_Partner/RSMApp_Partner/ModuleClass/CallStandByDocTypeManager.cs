
//Created On :: 15, December, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallStandByDocTypeManager
    {
        const string CallStandByDocTypeTable = "CallStandByDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public CallStandByDocType objGetCallStandByDocType(string argCallStandByDocTypeCode, string argClientCode)
        {
            CallStandByDocType argCallStandByDocType = new CallStandByDocType();
            DataSet DataSetToFill = new DataSet();

            if (argCallStandByDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallStandByDocType(argCallStandByDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallStandByDocType = this.objCreateCallStandByDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallStandByDocType;
        }


        public ICollection<CallStandByDocType> colGetCallStandByDocType(string argClientCode)
        {
            List<CallStandByDocType> lst = new List<CallStandByDocType>();
            DataSet DataSetToFill = new DataSet();
            CallStandByDocType tCallStandByDocType = new CallStandByDocType();

            DataSetToFill = this.GetCallStandByDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallStandByDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetCallStandByDocType(string argCallStandByDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CallStandByDocTypeCode", argCallStandByDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallStandByDocType4ID", param);

            return DataSetToFill;
        }


        public DataSet GetCallStandByDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallStandByDocType",param);
            return DataSetToFill;
        }

        public DataSet GetCallStandByDocType4StandByType(string argStandByType, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            param[1] = new SqlParameter("@StandByType", argStandByType);

            DataSetToFill = da.FillDataSet("SP_GetCallStandByDocType4StandByType", param);
            return DataSetToFill;
        }


        private CallStandByDocType objCreateCallStandByDocType(DataRow dr)
        {
            CallStandByDocType tCallStandByDocType = new CallStandByDocType();

            tCallStandByDocType.SetObjectInfo(dr);

            return tCallStandByDocType;

        }


        public ICollection<ErrorHandler> SaveCallStandByDocType(CallStandByDocType argCallStandByDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallStandByDocTypeExists(argCallStandByDocType.CallStandByDocTypeCode, argCallStandByDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallStandByDocType(argCallStandByDocType, da, lstErr);
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
                    UpdateCallStandByDocType(argCallStandByDocType, da, lstErr);
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


        public void InsertCallStandByDocType(CallStandByDocType argCallStandByDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@CallStandByDocTypeCode", argCallStandByDocType.CallStandByDocTypeCode);
            param[1] = new SqlParameter("@CallStandByDocTypeDesc", argCallStandByDocType.CallStandByDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argCallStandByDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argCallStandByDocType.NumRange);
            param[4] = new SqlParameter("@StandByType", argCallStandByDocType.StandByType);
            param[5] = new SqlParameter("@MaterialDocTypeCode", argCallStandByDocType.MaterialDocTypeCode);
            param[6] = new SqlParameter("@RepairProcessCode", argCallStandByDocType.RepairProcessCode);
            param[7] = new SqlParameter("@DefaultStoreCode", argCallStandByDocType.DefaultStoreCode);
            param[8] = new SqlParameter("@DefaultStockIndicator", argCallStandByDocType.DefaultStockIndicator);
            param[9] = new SqlParameter("@ClientCode", argCallStandByDocType.ClientCode);
            param[10] = new SqlParameter("@CreatedBy", argCallStandByDocType.CreatedBy);
            param[11] = new SqlParameter("@ModifiedBy", argCallStandByDocType.ModifiedBy);
      

            param[12] = new SqlParameter("@Type", SqlDbType.Char);
            param[12].Size = 1;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[13].Size = 255;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[14].Size = 20;
            param[14].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertCallStandByDocType", param);


            string strMessage = Convert.ToString(param[13].Value);
            string strType = Convert.ToString(param[12].Value);
            string strRetValue = Convert.ToString(param[14].Value);


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


        public void UpdateCallStandByDocType(CallStandByDocType argCallStandByDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@CallStandByDocTypeCode", argCallStandByDocType.CallStandByDocTypeCode);
            param[1] = new SqlParameter("@CallStandByDocTypeDesc", argCallStandByDocType.CallStandByDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argCallStandByDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argCallStandByDocType.NumRange);
            param[4] = new SqlParameter("@StandByType", argCallStandByDocType.StandByType);
            param[5] = new SqlParameter("@MaterialDocTypeCode", argCallStandByDocType.MaterialDocTypeCode);
            param[6] = new SqlParameter("@RepairProcessCode", argCallStandByDocType.RepairProcessCode);
            param[7] = new SqlParameter("@DefaultStoreCode", argCallStandByDocType.DefaultStoreCode);
            param[8] = new SqlParameter("@DefaultStockIndicator", argCallStandByDocType.DefaultStockIndicator);
            param[9] = new SqlParameter("@ClientCode", argCallStandByDocType.ClientCode);
            param[10] = new SqlParameter("@CreatedBy", argCallStandByDocType.CreatedBy);
            param[11] = new SqlParameter("@ModifiedBy", argCallStandByDocType.ModifiedBy);


            param[12] = new SqlParameter("@Type", SqlDbType.Char);
            param[12].Size = 1;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[13].Size = 255;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[14].Size = 20;
            param[14].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateCallStandByDocType", param);


            string strMessage = Convert.ToString(param[13].Value);
            string strType = Convert.ToString(param[12].Value);
            string strRetValue = Convert.ToString(param[14].Value);


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


        public ICollection<ErrorHandler> DeleteCallStandByDocType(string argCallStandByDocTypeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CallStandByDocTypeCode", argCallStandByDocTypeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteCallStandByDocType", param);


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


        public bool blnIsCallStandByDocTypeExists(string argCallStandByDocTypeCode, string argClientCode)
        {
            bool IsCallStandByDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetCallStandByDocType(argCallStandByDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallStandByDocTypeExists = true;
            }
            else
            {
                IsCallStandByDocTypeExists = false;
            }
            return IsCallStandByDocTypeExists;
        }
    }
}