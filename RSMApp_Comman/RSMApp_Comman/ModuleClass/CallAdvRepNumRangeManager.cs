
//Created On :: 21, February, 2013
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Comman
{
    public class CallAdvRepNumRangeManager
    {
        const string CallAdvRepNumRangeTable = "CallAdvRepNumRange";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public CallAdvRepNumRange objGetCallAdvRepNumRange(string argCallAdvRepDocTypeCode, string argNumRangeCode, string argClientCode)
        {
            CallAdvRepNumRange argCallAdvRepNumRange = new CallAdvRepNumRange();
            DataSet DataSetToFill = new DataSet();

            if (argCallAdvRepDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argNumRangeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallAdvRepNumRange(argCallAdvRepDocTypeCode, argNumRangeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallAdvRepNumRange = this.objCreateCallAdvRepNumRange((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallAdvRepNumRange;
        }


        public ICollection<CallAdvRepNumRange> colGetCallAdvRepNumRange(string argClientCode)
        {
            List<CallAdvRepNumRange> lst = new List<CallAdvRepNumRange>();
            DataSet DataSetToFill = new DataSet();
            CallAdvRepNumRange tCallAdvRepNumRange = new CallAdvRepNumRange();

            DataSetToFill = this.GetCallAdvRepNumRange(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallAdvRepNumRange(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetCallAdvRepNumRange(string argCallAdvRepDocTypeCode, string argNumRangeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvRepDocTypeCode);
            param[1] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallAdvRepNumRange4ID", param);

            return DataSetToFill;
        }


        public DataSet GetCallAdvRepNumRange(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallAdvRepNumRange",param);
            return DataSetToFill;
        }


        private CallAdvRepNumRange objCreateCallAdvRepNumRange(DataRow dr)
        {
            CallAdvRepNumRange tCallAdvRepNumRange = new CallAdvRepNumRange();

            tCallAdvRepNumRange.SetObjectInfo(dr);

            return tCallAdvRepNumRange;

        }


        public ICollection<ErrorHandler> SaveCallAdvRepNumRange(CallAdvRepNumRange argCallAdvRepNumRange)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallAdvRepNumRangeExists(argCallAdvRepNumRange.CallAdvRepDocTypeCode, argCallAdvRepNumRange.NumRangeCode, argCallAdvRepNumRange.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallAdvRepNumRange(argCallAdvRepNumRange, da, lstErr);
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
                    UpdateCallAdvRepNumRange(argCallAdvRepNumRange, da, lstErr);
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


        public void InsertCallAdvRepNumRange(CallAdvRepNumRange argCallAdvRepNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvRepNumRange.CallAdvRepDocTypeCode);
            param[1] = new SqlParameter("@NumRangeCode", argCallAdvRepNumRange.NumRangeCode);
            param[2] = new SqlParameter("@ClientCode", argCallAdvRepNumRange.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argCallAdvRepNumRange.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argCallAdvRepNumRange.ModifiedBy);
     

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertCallAdvRepNumRange", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }


        public void UpdateCallAdvRepNumRange(CallAdvRepNumRange argCallAdvRepNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvRepNumRange.CallAdvRepDocTypeCode);
            param[1] = new SqlParameter("@NumRangeCode", argCallAdvRepNumRange.NumRangeCode);
            param[2] = new SqlParameter("@ClientCode", argCallAdvRepNumRange.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argCallAdvRepNumRange.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argCallAdvRepNumRange.ModifiedBy);
       

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateCallAdvRepNumRange", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public ICollection<ErrorHandler> DeleteCallAdvRepNumRange(string argCallAdvRepDocTypeCode, string argNumRangeCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvRepDocTypeCode);
                param[1] = new SqlParameter("@NumRangeCode", argNumRangeCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCallAdvRepNumRange", param);


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


        public bool blnIsCallAdvRepNumRangeExists(string argCallAdvRepDocTypeCode, string argNumRangeCode, string argClientCode)
        {
            bool IsCallAdvRepNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetCallAdvRepNumRange(argCallAdvRepDocTypeCode, argNumRangeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallAdvRepNumRangeExists = true;
            }
            else
            {
                IsCallAdvRepNumRangeExists = false;
            }
            return IsCallAdvRepNumRangeExists;
        }
    }
}