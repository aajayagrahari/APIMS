
//Created On :: 01, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class AsgTechnicianDocTypeManager
    {
        const string AsgTechnicianDocTypeTable = "AsgTechnicianDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public AsgTechnicianDocType objGetAsgTechnicianDocType(string argAsgTechDocTypeCode, string argClientCode)
        {
            AsgTechnicianDocType argAsgTechnicianDocType = new AsgTechnicianDocType();
            DataSet DataSetToFill = new DataSet();

            if (argAsgTechDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetAsgTechnicianDocType(argAsgTechDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argAsgTechnicianDocType = this.objCreateAsgTechnicianDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argAsgTechnicianDocType;
        }
        
        public ICollection<AsgTechnicianDocType> colGetAsgTechnicianDocType(string argClientCode)
        {
            List<AsgTechnicianDocType> lst = new List<AsgTechnicianDocType>();
            DataSet DataSetToFill = new DataSet();
            AsgTechnicianDocType tAsgTechnicianDocType = new AsgTechnicianDocType();

            DataSetToFill = this.GetAsgTechnicianDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateAsgTechnicianDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetAsgTechnicianDocType(string argAsgTechDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AsgTechDocTypeCode", argAsgTechDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAsgTechnicianDocType4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetAsgTechnicianDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAsgTechnicianDocType", param);
            return DataSetToFill;
        }        
        
        private AsgTechnicianDocType objCreateAsgTechnicianDocType(DataRow dr)
        {
            AsgTechnicianDocType tAsgTechnicianDocType = new AsgTechnicianDocType();

            tAsgTechnicianDocType.SetObjectInfo(dr);

            return tAsgTechnicianDocType;

        }
        
        public ICollection<ErrorHandler> SaveAsgTechnicianDocType(AsgTechnicianDocType argAsgTechnicianDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsAsgTechnicianDocTypeExists(argAsgTechnicianDocType.AsgTechDocTypeCode, argAsgTechnicianDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertAsgTechnicianDocType(argAsgTechnicianDocType, da, lstErr);
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
                    UpdateAsgTechnicianDocType(argAsgTechnicianDocType, da, lstErr);
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
        
        public void InsertAsgTechnicianDocType(AsgTechnicianDocType argAsgTechnicianDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@AsgTechDocTypeCode", argAsgTechnicianDocType.AsgTechDocTypeCode);
            param[1] = new SqlParameter("@AsgTechDocTypeDesc", argAsgTechnicianDocType.AsgTechDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argAsgTechnicianDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argAsgTechnicianDocType.NumRange);
            param[4] = new SqlParameter("@AssignType", argAsgTechnicianDocType.AssignType);
            param[5] = new SqlParameter("@MaterialDocTypeCode", argAsgTechnicianDocType.MaterialDocTypeCode);
            param[6] = new SqlParameter("@ClientCode", argAsgTechnicianDocType.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argAsgTechnicianDocType.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argAsgTechnicianDocType.ModifiedBy);
           
            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertAsgTechnicianDocType", param);


            string strMessage = Convert.ToString(param[10].Value);
            string strType = Convert.ToString(param[9].Value);
            string strRetValue = Convert.ToString(param[11].Value);


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
        
        public void UpdateAsgTechnicianDocType(AsgTechnicianDocType argAsgTechnicianDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];

            param[0] = new SqlParameter("@AsgTechDocTypeCode", argAsgTechnicianDocType.AsgTechDocTypeCode);
            param[1] = new SqlParameter("@AsgTechDocTypeDesc", argAsgTechnicianDocType.AsgTechDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argAsgTechnicianDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argAsgTechnicianDocType.NumRange);
            param[4] = new SqlParameter("@AssignType", argAsgTechnicianDocType.AssignType);
            param[5] = new SqlParameter("@MaterialDocTypeCode", argAsgTechnicianDocType.MaterialDocTypeCode);
            param[6] = new SqlParameter("@ClientCode", argAsgTechnicianDocType.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argAsgTechnicianDocType.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argAsgTechnicianDocType.ModifiedBy);

            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateAsgTechnicianDocType", param);


            string strMessage = Convert.ToString(param[10].Value);
            string strType = Convert.ToString(param[9].Value);
            string strRetValue = Convert.ToString(param[11].Value);


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
        
        public ICollection<ErrorHandler> DeleteAsgTechnicianDocType(string argAsgTechDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@AsgTechDocTypeCode", argAsgTechDocTypeCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteAsgTechnicianDocType", param);


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
        
        public bool blnIsAsgTechnicianDocTypeExists(string argAsgTechDocTypeCode, string argClientCode)
        {
            bool IsAsgTechnicianDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetAsgTechnicianDocType(argAsgTechDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAsgTechnicianDocTypeExists = true;
            }
            else
            {
                IsAsgTechnicianDocTypeExists = false;
            }
            return IsAsgTechnicianDocTypeExists;
        }

    }
}