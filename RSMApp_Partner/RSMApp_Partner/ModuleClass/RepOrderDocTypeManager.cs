
//Created On :: 29, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class RepOrderDocTypeManager
    {
        const string RepOrderDocTypeTable = "RepOrderDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public RepOrderDocType objGetRepOrderDocType(string argRepOrderDocTypeCode, string argClientCode)
        {
            RepOrderDocType argRepOrderDocType = new RepOrderDocType();
            DataSet DataSetToFill = new DataSet();

            if (argRepOrderDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetRepOrderDocType(argRepOrderDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argRepOrderDocType = this.objCreateRepOrderDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argRepOrderDocType;
        }


        public ICollection<RepOrderDocType> colGetRepOrderDocType(string argClientCode)
        {
            List<RepOrderDocType> lst = new List<RepOrderDocType>();
            DataSet DataSetToFill = new DataSet();
            RepOrderDocType tRepOrderDocType = new RepOrderDocType();

            DataSetToFill = this.GetRepOrderDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateRepOrderDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetRepOrderDocType(string argRepOrderDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RepOrderDocTypeCode", argRepOrderDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepOrderDocType4ID", param);

            return DataSetToFill;
        }


        public DataSet GetRepOrderDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepOrderDocType",param);
            return DataSetToFill;
        }


        private RepOrderDocType objCreateRepOrderDocType(DataRow dr)
        {
            RepOrderDocType tRepOrderDocType = new RepOrderDocType();

            tRepOrderDocType.SetObjectInfo(dr);

            return tRepOrderDocType;

        }


        public ICollection<ErrorHandler> SaveRepOrderDocType(RepOrderDocType argRepOrderDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsRepOrderDocTypeExists(argRepOrderDocType.RepOrderDocTypeCode, argRepOrderDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertRepOrderDocType(argRepOrderDocType, da, lstErr);
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
                    UpdateRepOrderDocType(argRepOrderDocType, da, lstErr);
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


        public void InsertRepOrderDocType(RepOrderDocType argRepOrderDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@RepOrderDocTypeCode", argRepOrderDocType.RepOrderDocTypeCode);
            param[1] = new SqlParameter("@RepOrderDocTypeDesc", argRepOrderDocType.RepOrderDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argRepOrderDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argRepOrderDocType.NumRange);
            param[4] = new SqlParameter("@ClientCode", argRepOrderDocType.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argRepOrderDocType.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argRepOrderDocType.ModifiedBy);
        
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertRepOrderDocType", param);


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


        public void UpdateRepOrderDocType(RepOrderDocType argRepOrderDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@RepOrderDocTypeCode", argRepOrderDocType.RepOrderDocTypeCode);
            param[1] = new SqlParameter("@RepOrderDocTypeDesc", argRepOrderDocType.RepOrderDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argRepOrderDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argRepOrderDocType.NumRange);
            param[4] = new SqlParameter("@ClientCode", argRepOrderDocType.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argRepOrderDocType.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argRepOrderDocType.ModifiedBy);
          
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateRepOrderDocType", param);


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


        public ICollection<ErrorHandler> DeleteRepOrderDocType(string argRepOrderDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@RepOrderDocTypeCode", argRepOrderDocTypeCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteRepOrderDocType", param);


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


        public bool blnIsRepOrderDocTypeExists(string argRepOrderDocTypeCode, string argClientCode)
        {
            bool IsRepOrderDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetRepOrderDocType(argRepOrderDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsRepOrderDocTypeExists = true;
            }
            else
            {
                IsRepOrderDocTypeExists = false;
            }
            return IsRepOrderDocTypeExists;
        }
    }
}