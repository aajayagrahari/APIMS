
//Created On :: 20, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class RepairProcDocTypeManager
    {
        const string RepairProcDocTypeTable = "RepairProcDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public RepairProcDocType objGetRepairProcDocType(string argRepairProcDocTypeCode, string argClientCode)
        {
            RepairProcDocType argRepairProcDocType = new RepairProcDocType();
            DataSet DataSetToFill = new DataSet();

            if (argRepairProcDocTypeCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            DataSetToFill = this.GetRepairProcDocType(argRepairProcDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argRepairProcDocType = this.objCreateRepairProcDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandler:

        Finish:
            DataSetToFill = null;


            return argRepairProcDocType;
        }


        public ICollection<RepairProcDocType> colGetRepairProcDocType(string argClientCode)
        {
            List<RepairProcDocType> lst = new List<RepairProcDocType>();
            DataSet DataSetToFill = new DataSet();
            RepairProcDocType tRepairProcDocType = new RepairProcDocType();

            DataSetToFill = this.GetRepairProcDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateRepairProcDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetRepairProcDocType(string argRepairProcDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RepairProcDocTypeCode", argRepairProcDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepairProcDocType4ID", param);

            return DataSetToFill;
        }


        public DataSet GetRepairProcDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepairProcDocType", param);
            return DataSetToFill;
        }


        private RepairProcDocType objCreateRepairProcDocType(DataRow dr)
        {
            RepairProcDocType tRepairProcDocType = new RepairProcDocType();

            tRepairProcDocType.SetObjectInfo(dr);

            return tRepairProcDocType;

        }


        public ICollection<ErrorHandler> SaveRepairProcDocType(RepairProcDocType argRepairProcDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsRepairProcDocTypeExists(argRepairProcDocType.RepairProcDocTypeCode, argRepairProcDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertRepairProcDocType(argRepairProcDocType, da, lstErr);
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
                    UpdateRepairProcDocType(argRepairProcDocType, da, lstErr);
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


        public void InsertRepairProcDocType(RepairProcDocType argRepairProcDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@RepairProcDocTypeCode", argRepairProcDocType.RepairProcDocTypeCode);
            param[1] = new SqlParameter("@RepairProcDocTypeDesc", argRepairProcDocType.RepairProcDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argRepairProcDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argRepairProcDocType.NumRange);
            param[4] = new SqlParameter("@ClientCode", argRepairProcDocType.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argRepairProcDocType.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argRepairProcDocType.ModifiedBy);
            

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertRepairProcDocType", param);


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


        public void UpdateRepairProcDocType(RepairProcDocType argRepairProcDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@RepairProcDocTypeCode", argRepairProcDocType.RepairProcDocTypeCode);
            param[1] = new SqlParameter("@RepairProcDocTypeDesc", argRepairProcDocType.RepairProcDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argRepairProcDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argRepairProcDocType.NumRange);
            param[4] = new SqlParameter("@ClientCode", argRepairProcDocType.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argRepairProcDocType.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argRepairProcDocType.ModifiedBy);
           
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateRepairProcDocType", param);


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


        public ICollection<ErrorHandler> DeleteRepairProcDocType(string argRepairProcDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@RepairProcDocTypeCode", argRepairProcDocTypeCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteRepairProcDocType", param);


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


        public bool blnIsRepairProcDocTypeExists(string argRepairProcDocTypeCode, string argClientCode)
        {
            bool IsRepairProcDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetRepairProcDocType(argRepairProcDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsRepairProcDocTypeExists = true;
            }
            else
            {
                IsRepairProcDocTypeExists = false;
            }
            return IsRepairProcDocTypeExists;
        }
    }
}