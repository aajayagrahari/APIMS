
//Created On :: 11, December, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallAdvRepDocTypeManager
    {
        const string CallAdvRepDocTypeTable = "CallAdvRepDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CallAdvRepDocType objGetCallAdvRepDocType(string argCallAdvRepDocTypeCode, string argClientCode)
        {
            CallAdvRepDocType argCallAdvRepDocType = new CallAdvRepDocType();
            DataSet DataSetToFill = new DataSet();

            if (argCallAdvRepDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallAdvRepDocType(argCallAdvRepDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallAdvRepDocType = this.objCreateCallAdvRepDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;
            
            return argCallAdvRepDocType;

        }
        
        public ICollection<CallAdvRepDocType> colGetCallAdvRepDocType(string argClientCode)
        {
            List<CallAdvRepDocType> lst = new List<CallAdvRepDocType>();
            DataSet DataSetToFill = new DataSet();
            CallAdvRepDocType tCallAdvRepDocType = new CallAdvRepDocType();

            DataSetToFill = this.GetCallAdvRepDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallAdvRepDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetCallAdvRepDocType(string argCallAdvRepDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvRepDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallAdvRepDocType4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCallAdvRepDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallAdvRepDocType", param);
            return DataSetToFill;
        }

        public DataSet GetCallAdvRepDocType4RepType(string argReplacementType, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            param[1] = new SqlParameter("@ReplacementType", argReplacementType);

            DataSetToFill = da.FillDataSet("SP_GetCallAdvRepDocType4RepType", param);
            return DataSetToFill;
        }
        
        private CallAdvRepDocType objCreateCallAdvRepDocType(DataRow dr)
        {
            CallAdvRepDocType tCallAdvRepDocType = new CallAdvRepDocType();

            tCallAdvRepDocType.SetObjectInfo(dr);

            return tCallAdvRepDocType;
        }
        
        public ICollection<ErrorHandler> SaveCallAdvRepDocType(CallAdvRepDocType argCallAdvRepDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallAdvRepDocTypeExists(argCallAdvRepDocType.CallAdvRepDocTypeCode, argCallAdvRepDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallAdvRepDocType(argCallAdvRepDocType, da, lstErr);
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
                    UpdateCallAdvRepDocType(argCallAdvRepDocType, da, lstErr);
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
        
        public void InsertCallAdvRepDocType(CallAdvRepDocType argCallAdvRepDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvRepDocType.CallAdvRepDocTypeCode);
            param[1] = new SqlParameter("@CallAdvRepDocTypeDesc", argCallAdvRepDocType.CallAdvRepDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argCallAdvRepDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argCallAdvRepDocType.NumRange);
            param[4] = new SqlParameter("@ReplacementType", argCallAdvRepDocType.ReplacementType);
            param[5] = new SqlParameter("@ClientCode", argCallAdvRepDocType.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argCallAdvRepDocType.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argCallAdvRepDocType.ModifiedBy);
            
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallAdvRepDocType", param);


            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
            string strRetValue = Convert.ToString(param[10].Value);


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
        
        public void UpdateCallAdvRepDocType(CallAdvRepDocType argCallAdvRepDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvRepDocType.CallAdvRepDocTypeCode);
            param[1] = new SqlParameter("@CallAdvRepDocTypeDesc", argCallAdvRepDocType.CallAdvRepDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argCallAdvRepDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argCallAdvRepDocType.NumRange);
            param[4] = new SqlParameter("@ReplacementType", argCallAdvRepDocType.ReplacementType);
            param[5] = new SqlParameter("@ClientCode", argCallAdvRepDocType.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argCallAdvRepDocType.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argCallAdvRepDocType.ModifiedBy);
            
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;
            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;
            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateCallAdvRepDocType", param);


            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
            string strRetValue = Convert.ToString(param[10].Value);


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
        
        public ICollection<ErrorHandler> DeleteCallAdvRepDocType(string argCallAdvRepDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvRepDocTypeCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCallAdvRepDocType", param);


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
        
        public bool blnIsCallAdvRepDocTypeExists(string argCallAdvRepDocTypeCode, string argClientCode)
        {
            bool IsCallAdvRepDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetCallAdvRepDocType(argCallAdvRepDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallAdvRepDocTypeExists = true;
            }
            else
            {
                IsCallAdvRepDocTypeExists = false;
            }
            return IsCallAdvRepDocTypeExists;
        }
    }
}