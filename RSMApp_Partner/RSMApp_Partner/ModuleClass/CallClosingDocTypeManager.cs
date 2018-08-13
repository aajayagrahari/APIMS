
//Created On :: 08, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallClosingDocTypeManager
    {
        const string CallClosingDocTypeTable = "CallClosingDocType";
                
        ErrorHandler objErrorHandler = new ErrorHandler();


        public CallClosingDocType objGetCallClosingDocType(string argCallClosingDocTypeCode, string argClientCode)
        {
            CallClosingDocType argCallClosingDocType = new CallClosingDocType();
            DataSet DataSetToFill = new DataSet();

            if (argCallClosingDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallClosingDocType(argCallClosingDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallClosingDocType = this.objCreateCallClosingDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallClosingDocType;
        }


        public ICollection<CallClosingDocType> colGetCallClosingDocType(string argClientCode)
        {
            List<CallClosingDocType> lst = new List<CallClosingDocType>();
            DataSet DataSetToFill = new DataSet();
            CallClosingDocType tCallClosingDocType = new CallClosingDocType();

            DataSetToFill = this.GetCallClosingDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallClosingDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetCallClosingDocType(string argCallClosingDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CallClosingDocTypeCode", argCallClosingDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallClosingDocType4ID", param);

            return DataSetToFill;
        }


        public DataSet GetCallClosingDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallClosingDocType", param);
            return DataSetToFill;
        }


        private CallClosingDocType objCreateCallClosingDocType(DataRow dr)
        {
            CallClosingDocType tCallClosingDocType = new CallClosingDocType();

            tCallClosingDocType.SetObjectInfo(dr);

            return tCallClosingDocType;

        }


        public ICollection<ErrorHandler> SaveCallClosingDocType(CallClosingDocType argCallClosingDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallClosingDocTypeExists(argCallClosingDocType.CallClosingDocTypeCode, argCallClosingDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallClosingDocType(argCallClosingDocType, da, lstErr);
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
                    UpdateCallClosingDocType(argCallClosingDocType, da, lstErr);
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


        public void InsertCallClosingDocType(CallClosingDocType argCallClosingDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@CallClosingDocTypeCode", argCallClosingDocType.CallClosingDocTypeCode);
            param[1] = new SqlParameter("@CallClosingDocTypeDesc", argCallClosingDocType.CallClosingDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argCallClosingDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argCallClosingDocType.NumRange);
            param[4] = new SqlParameter("@MaterialDocTypeCode", argCallClosingDocType.MaterialDocTypeCode);
            param[5] = new SqlParameter("@RepairProcessCode", argCallClosingDocType.RepairProcessCode);
            param[6] = new SqlParameter("@DefaultStoreCode", argCallClosingDocType.DefaultStoreCode);
            param[7] = new SqlParameter("@DefaultStockIndicator", argCallClosingDocType.DefaultStockIndicator);
            param[8] = new SqlParameter("@ClientCode", argCallClosingDocType.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argCallClosingDocType.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argCallClosingDocType.ModifiedBy);
            
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallClosingDocType", param);
            
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


        public void UpdateCallClosingDocType(CallClosingDocType argCallClosingDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@CallClosingDocTypeCode", argCallClosingDocType.CallClosingDocTypeCode);
            param[1] = new SqlParameter("@CallClosingDocTypeDesc", argCallClosingDocType.CallClosingDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argCallClosingDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argCallClosingDocType.NumRange);
            param[4] = new SqlParameter("@MaterialDocTypeCode", argCallClosingDocType.MaterialDocTypeCode);
            param[5] = new SqlParameter("@RepairProcessCode", argCallClosingDocType.RepairProcessCode);
            param[6] = new SqlParameter("@DefaultStoreCode", argCallClosingDocType.DefaultStoreCode);
            param[7] = new SqlParameter("@DefaultStockIndicator", argCallClosingDocType.DefaultStockIndicator);
            param[8] = new SqlParameter("@ClientCode", argCallClosingDocType.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argCallClosingDocType.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argCallClosingDocType.ModifiedBy);
       
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallClosingDocType", param);


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


        public ICollection<ErrorHandler> DeleteCallClosingDocType(string argCallClosingDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@CallClosingDocTypeCode", argCallClosingDocTypeCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCallClosingDocType", param);


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


        public bool blnIsCallClosingDocTypeExists(string argCallClosingDocTypeCode, string argClientCode)
        {
            bool IsCallClosingDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetCallClosingDocType(argCallClosingDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallClosingDocTypeExists = true;
            }
            else
            {
                IsCallClosingDocTypeExists = false;
            }
            return IsCallClosingDocTypeExists;
        }
    }
}