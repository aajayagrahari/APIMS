
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
    public class CallBillingDocTypeManager
    {
        const string CallBillingDocTypeTable = "CallBillingDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public CallBillingDocType objGetCallBillingDocType(string argCallBillingDocTypeCode, string argClientCode)
        {
            CallBillingDocType argCallBillingDocType = new CallBillingDocType();
            DataSet DataSetToFill = new DataSet();

            if (argCallBillingDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallBillingDocType(argCallBillingDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallBillingDocType = this.objCreateCallBillingDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallBillingDocType;
        }


        public ICollection<CallBillingDocType> colGetCallBillingDocType(string argClientCode)
        {
            List<CallBillingDocType> lst = new List<CallBillingDocType>();
            DataSet DataSetToFill = new DataSet();
            CallBillingDocType tCallBillingDocType = new CallBillingDocType();

            DataSetToFill = this.GetCallBillingDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallBillingDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetCallBillingDocType(string argCallBillingDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CallBillingDocTypeCode", argCallBillingDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallBillingDocType4ID", param);

            return DataSetToFill;
        }


        public DataSet GetCallBillingDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallBillingDocType",param);
            return DataSetToFill;
        }


        private CallBillingDocType objCreateCallBillingDocType(DataRow dr)
        {
            CallBillingDocType tCallBillingDocType = new CallBillingDocType();

            tCallBillingDocType.SetObjectInfo(dr);

            return tCallBillingDocType;

        }
        
        public ICollection<ErrorHandler> SaveCallBillingDocType(CallBillingDocType argCallBillingDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallBillingDocTypeExists(argCallBillingDocType.CallBillingDocTypeCode, argCallBillingDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallBillingDocType(argCallBillingDocType, da, lstErr);
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
                    UpdateCallBillingDocType(argCallBillingDocType, da, lstErr);
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
        
        public void InsertCallBillingDocType(CallBillingDocType argCallBillingDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@CallBillingDocTypeCode", argCallBillingDocType.CallBillingDocTypeCode);
            param[1] = new SqlParameter("@CallBillingDocTypeDesc", argCallBillingDocType.CallBillingDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argCallBillingDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argCallBillingDocType.NumRange);
            param[4] = new SqlParameter("@ClientCode", argCallBillingDocType.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argCallBillingDocType.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argCallBillingDocType.ModifiedBy);
      
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertCallBillingDocType", param);


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
            lstErr.Add(objErrorHandler);

        }


        public void UpdateCallBillingDocType(CallBillingDocType argCallBillingDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@CallBillingDocTypeCode", argCallBillingDocType.CallBillingDocTypeCode);
            param[1] = new SqlParameter("@CallBillingDocTypeDesc", argCallBillingDocType.CallBillingDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argCallBillingDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argCallBillingDocType.NumRange);
            param[4] = new SqlParameter("@ClientCode", argCallBillingDocType.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argCallBillingDocType.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argCallBillingDocType.ModifiedBy);
        
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallBillingDocType", param);


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
            lstErr.Add(objErrorHandler);

        }


        public ICollection<ErrorHandler> DeleteCallBillingDocType(string argCallBillingDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@CallBillingDocTypeCode", argCallBillingDocTypeCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCallBillingDocType", param);


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


        public bool blnIsCallBillingDocTypeExists(string argCallBillingDocTypeCode, string argClientCode)
        {
            bool IsCallBillingDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetCallBillingDocType(argCallBillingDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallBillingDocTypeExists = true;
            }
            else
            {
                IsCallBillingDocTypeExists = false;
            }
            return IsCallBillingDocTypeExists;
        }
    }
}