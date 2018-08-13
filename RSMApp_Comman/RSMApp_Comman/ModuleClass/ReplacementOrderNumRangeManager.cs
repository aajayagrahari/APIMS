
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
    public class ReplacementOrderNumRangeManager
    {
        const string ReplacementOrderNumRangeTable = "ReplacementOrderNumRange";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public ReplacementOrderNumRange objGetReplacementOrderNumRange(string argRepOrderDocTypeCode, string argNumRangeCode, string argClientCode)
        {
            ReplacementOrderNumRange argReplacementOrderNumRange = new ReplacementOrderNumRange();
            DataSet DataSetToFill = new DataSet();

            if (argRepOrderDocTypeCode.Trim() == "")
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

            DataSetToFill = this.GetReplacementOrderNumRange(argRepOrderDocTypeCode, argNumRangeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argReplacementOrderNumRange = this.objCreateReplacementOrderNumRange((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argReplacementOrderNumRange;
        }


        public ICollection<ReplacementOrderNumRange> colGetReplacementOrderNumRange(string argClientCode)
        {
            List<ReplacementOrderNumRange> lst = new List<ReplacementOrderNumRange>();
            DataSet DataSetToFill = new DataSet();
            ReplacementOrderNumRange tReplacementOrderNumRange = new ReplacementOrderNumRange();

            DataSetToFill = this.GetReplacementOrderNumRange(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateReplacementOrderNumRange(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetReplacementOrderNumRange(string argRepOrderDocTypeCode, string argNumRangeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RepOrderDocTypeCode", argRepOrderDocTypeCode);
            param[1] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetReplacementOrderNumRange4ID", param);

            return DataSetToFill;
        }


        public DataSet GetReplacementOrderNumRange(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
          
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetReplacementOrderNumRange",param);
            return DataSetToFill;
        }


        private ReplacementOrderNumRange objCreateReplacementOrderNumRange(DataRow dr)
        {
            ReplacementOrderNumRange tReplacementOrderNumRange = new ReplacementOrderNumRange();

            tReplacementOrderNumRange.SetObjectInfo(dr);

            return tReplacementOrderNumRange;

        }


        public ICollection<ErrorHandler> SaveReplacementOrderNumRange(ReplacementOrderNumRange argReplacementOrderNumRange)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsReplacementOrderNumRangeExists(argReplacementOrderNumRange.RepOrderDocTypeCode, argReplacementOrderNumRange.NumRangeCode, argReplacementOrderNumRange.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertReplacementOrderNumRange(argReplacementOrderNumRange, da, lstErr);
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
                    UpdateReplacementOrderNumRange(argReplacementOrderNumRange, da, lstErr);
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


        public void InsertReplacementOrderNumRange(ReplacementOrderNumRange argReplacementOrderNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@RepOrderDocTypeCode", argReplacementOrderNumRange.RepOrderDocTypeCode);
            param[1] = new SqlParameter("@NumRangeCode", argReplacementOrderNumRange.NumRangeCode);
            param[2] = new SqlParameter("@ClientCode", argReplacementOrderNumRange.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argReplacementOrderNumRange.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argReplacementOrderNumRange.ModifiedBy);
          

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertReplacementOrderNumRange", param);


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


        public void UpdateReplacementOrderNumRange(ReplacementOrderNumRange argReplacementOrderNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@RepOrderDocTypeCode", argReplacementOrderNumRange.RepOrderDocTypeCode);
            param[1] = new SqlParameter("@NumRangeCode", argReplacementOrderNumRange.NumRangeCode);
            param[2] = new SqlParameter("@ClientCode", argReplacementOrderNumRange.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argReplacementOrderNumRange.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argReplacementOrderNumRange.ModifiedBy);


            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;
            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateReplacementOrderNumRange", param);


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


        public ICollection<ErrorHandler> DeleteReplacementOrderNumRange(string argRepOrderDocTypeCode, string argNumRangeCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@RepOrderDocTypeCode", argRepOrderDocTypeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteReplacementOrderNumRange", param);


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


        public bool blnIsReplacementOrderNumRangeExists(string argRepOrderDocTypeCode, string argNumRangeCode, string argClientCode)
        {
            bool IsReplacementOrderNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetReplacementOrderNumRange(argRepOrderDocTypeCode, argNumRangeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsReplacementOrderNumRangeExists = true;
            }
            else
            {
                IsReplacementOrderNumRangeExists = false;
            }
            return IsReplacementOrderNumRangeExists;
        }
    }
}