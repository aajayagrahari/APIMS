
//Created On :: 08, June, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SAP
{
    public class SAPConnectionConfigManager
    {
        const string SAPConnectionConfigTable = "SAPConnectionConfig";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public SAPConnectionConfig objGetSAPConnectionConfig(string argSAPConCode, string argClientCode)
        {
            SAPConnectionConfig argSAPConnectionConfig = new SAPConnectionConfig();
            DataSet DataSetToFill = new DataSet();

            if (argSAPConCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetSAPConnectionConfig(argSAPConCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSAPConnectionConfig = this.objCreateSAPConnectionConfig((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSAPConnectionConfig;
        }

        public SAPConnectionConfig objGetActiveSAPConnectionConfig(string argClientCode, int iIsActive)
        {
            SAPConnectionConfig argSAPConnectionConfig = new SAPConnectionConfig();
            DataSet DataSetToFill = new DataSet();

            if (iIsActive == -1)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetActiveSAPConnectionConfig(argClientCode, iIsActive);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSAPConnectionConfig = this.objCreateSAPConnectionConfig((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSAPConnectionConfig;
        }

        public ICollection<SAPConnectionConfig> colGetSAPConnectionConfig(string argClientCode)
        {
            List<SAPConnectionConfig> lst = new List<SAPConnectionConfig>();
            DataSet DataSetToFill = new DataSet();
            SAPConnectionConfig tSAPConnectionConfig = new SAPConnectionConfig();

            DataSetToFill = this.GetSAPConnectionConfig(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSAPConnectionConfig(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetSAPConnectionConfig(string argSAPConCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SAPConCode", argSAPConCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSAPConnectionConfig4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetSAPConnectionConfig(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSAPConnectionConfig", param);
            return DataSetToFill;
        }

        public DataSet GetActiveSAPConnectionConfig(string argClientCode, int iIsActive)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            param[1] = new SqlParameter("@IsActive", iIsActive);

            DataSetToFill = da.FillDataSet("SP_GetActiveSAPConnectionConfig", param);
            return DataSetToFill;
        }
        
        private SAPConnectionConfig objCreateSAPConnectionConfig(DataRow dr)
        {
            SAPConnectionConfig tSAPConnectionConfig = new SAPConnectionConfig();

            tSAPConnectionConfig.SetObjectInfo(dr);

            return tSAPConnectionConfig;

        }        
        
        public ICollection<ErrorHandler> SaveSAPConnectionConfig(SAPConnectionConfig argSAPConnectionConfig)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsSAPConnectionConfigExists(argSAPConnectionConfig.SAPConCode, argSAPConnectionConfig.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertSAPConnectionConfig(argSAPConnectionConfig, da, lstErr);
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
                    UpdateSAPConnectionConfig(argSAPConnectionConfig, da, lstErr);
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
        
        public void InsertSAPConnectionConfig(SAPConnectionConfig argSAPConnectionConfig, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@SAPConCode", argSAPConnectionConfig.SAPConCode);
            param[1] = new SqlParameter("@SAPServer", argSAPConnectionConfig.SAPServer);
            param[2] = new SqlParameter("@SAPRouter", argSAPConnectionConfig.SAPRouter);
            param[3] = new SqlParameter("@AppServerHost", argSAPConnectionConfig.AppServerHost);
            param[4] = new SqlParameter("@SystemNumber", argSAPConnectionConfig.SystemNumber);
            param[5] = new SqlParameter("@SystemID", argSAPConnectionConfig.SystemID);
            param[6] = new SqlParameter("@SAPUser", argSAPConnectionConfig.SAPUser);
            param[7] = new SqlParameter("@SAPPassword", argSAPConnectionConfig.SAPPassword);
            param[8] = new SqlParameter("@SAPClient", argSAPConnectionConfig.SAPClient);
            param[9] = new SqlParameter("@SAPLanguage", argSAPConnectionConfig.SAPLanguage);
            param[10] = new SqlParameter("@PoolSize", argSAPConnectionConfig.PoolSize);
            param[11] = new SqlParameter("@MaxPoolSize", argSAPConnectionConfig.MaxPoolSize);
            param[12] = new SqlParameter("@IdleTimeout", argSAPConnectionConfig.IdleTimeout);
            param[13] = new SqlParameter("@IsActive", argSAPConnectionConfig.IsActive);
            param[14] = new SqlParameter("@ClientCode", argSAPConnectionConfig.ClientCode);
            param[15] = new SqlParameter("@CreatedBy", argSAPConnectionConfig.CreatedBy);
            param[16] = new SqlParameter("@ModifiedBy", argSAPConnectionConfig.ModifiedBy);
            
            param[17] = new SqlParameter("@Type", SqlDbType.Char);
            param[17].Size = 1;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[18].Size = 255;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[19].Size = 20;
            param[19].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSAPConnectionConfig", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }
        
        public void UpdateSAPConnectionConfig(SAPConnectionConfig argSAPConnectionConfig, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@SAPConCode", argSAPConnectionConfig.SAPConCode);
            param[1] = new SqlParameter("@SAPServer", argSAPConnectionConfig.SAPServer);
            param[2] = new SqlParameter("@SAPRouter", argSAPConnectionConfig.SAPRouter);
            param[3] = new SqlParameter("@AppServerHost", argSAPConnectionConfig.AppServerHost);
            param[4] = new SqlParameter("@SystemNumber", argSAPConnectionConfig.SystemNumber);
            param[5] = new SqlParameter("@SystemID", argSAPConnectionConfig.SystemID);
            param[6] = new SqlParameter("@SAPUser", argSAPConnectionConfig.SAPUser);
            param[7] = new SqlParameter("@SAPPassword", argSAPConnectionConfig.SAPPassword);
            param[8] = new SqlParameter("@SAPClient", argSAPConnectionConfig.SAPClient);
            param[9] = new SqlParameter("@SAPLanguage", argSAPConnectionConfig.SAPLanguage);
            param[10] = new SqlParameter("@PoolSize", argSAPConnectionConfig.PoolSize);
            param[11] = new SqlParameter("@MaxPoolSize", argSAPConnectionConfig.MaxPoolSize);
            param[12] = new SqlParameter("@IdleTimeout", argSAPConnectionConfig.IdleTimeout);
            param[13] = new SqlParameter("@IsActive", argSAPConnectionConfig.IsActive);
            param[14] = new SqlParameter("@ClientCode", argSAPConnectionConfig.ClientCode);
            param[15] = new SqlParameter("@CreatedBy", argSAPConnectionConfig.CreatedBy);
            param[16] = new SqlParameter("@ModifiedBy", argSAPConnectionConfig.ModifiedBy);
            
            param[17] = new SqlParameter("@Type", SqlDbType.Char);
            param[17].Size = 1;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[18].Size = 255;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[19].Size = 20;
            param[19].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSAPConnectionConfig", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }
        
        public ICollection<ErrorHandler> DeleteSAPConnectionConfig(string argSAPConCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@SAPConCode", argSAPConCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteSAPConnectionConfig", param);


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
        
        public bool blnIsSAPConnectionConfigExists(string argSAPConCode, string argClientCode)
        {
            bool IsSAPConnectionConfigExists = false;
            DataSet ds = new DataSet();
            ds = GetSAPConnectionConfig(argSAPConCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSAPConnectionConfigExists = true;
            }
            else
            {
                IsSAPConnectionConfigExists = false;
            }
            return IsSAPConnectionConfigExists;
        }


    }
}