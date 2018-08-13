
//Created On :: 24, July, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Authorization
{
    public class AuthJobRoleMasterManager
    {
        const string AuthJobRoleMasterTable = "AuthJobRoleMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        AuthJobRoleDetailsManager objAuthJobRoleDetailManager = new AuthJobRoleDetailsManager();

        public AuthJobRoleMaster objGetAuthJobRoleMaster(string argAuthJobRoleCode, string argClientCode)
        {
            AuthJobRoleMaster argAuthJobRoleMaster = new AuthJobRoleMaster();
            DataSet DataSetToFill = new DataSet();

            if (argAuthJobRoleCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetAuthJobRoleMaster(argAuthJobRoleCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argAuthJobRoleMaster = this.objCreateAuthJobRoleMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argAuthJobRoleMaster;
        }


        public ICollection<AuthJobRoleMaster> colGetAuthJobRoleMaster(string argClientCode)
        {
            List<AuthJobRoleMaster> lst = new List<AuthJobRoleMaster>();
            DataSet DataSetToFill = new DataSet();
            AuthJobRoleMaster tAuthJobRoleMaster = new AuthJobRoleMaster();

            DataSetToFill = this.GetAuthJobRoleMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateAuthJobRoleMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetAuthJobRoleMaster(string argAuthJobRoleCode, string argClientCode, DataAccess  da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AuthJobRoleCode", argAuthJobRoleCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetAuthJobRoleMaster4ID", param);

            return DataSetToFill;
        }


        public DataSet GetAuthJobRoleMaster(string argAuthJobRoleCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AuthJobRoleCode", argAuthJobRoleCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAuthJobRoleMaster4ID", param);

            return DataSetToFill;
        }


        public DataSet GetAuthJobRoleMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAuthJobRoleMaster", param);
            return DataSetToFill;
        }


        private AuthJobRoleMaster objCreateAuthJobRoleMaster(DataRow dr)
        {
            AuthJobRoleMaster tAuthJobRoleMaster = new AuthJobRoleMaster();

            tAuthJobRoleMaster.SetObjectInfo(dr);

            return tAuthJobRoleMaster;

        }


        public ICollection<ErrorHandler> SaveAuthJobRoleMaster(AuthJobRoleMaster argAuthJobRoleMaster, DataTable dtAuthJobRoleDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            string strRetValue = "";
            AuthJobRoleDetails objAuthJobRoleDetails = null;
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsAuthJobRoleMasterExists(argAuthJobRoleMaster.AuthJobRoleCode, argAuthJobRoleMaster.ClientCode, da) == false)
                {               
                  strRetValue =   InsertAuthJobRoleMaster(argAuthJobRoleMaster, da, lstErr);
                }
                else
                {                    
                  strRetValue =   UpdateAuthJobRoleMaster(argAuthJobRoleMaster, da, lstErr);
                }

                foreach (ErrorHandler objerr in lstErr)
                {
                    if (objerr.Type == "E")
                    {
                        da.ROLLBACK_TRANSACTION();
                        return lstErr;
                    }
                }

                if (strRetValue != "")
                {
                    foreach (DataRow dr in dtAuthJobRoleDetail.Rows)
                    {
                        objAuthJobRoleDetails = new AuthJobRoleDetails();

                        objAuthJobRoleDetails.AuthJobRoleCode = strRetValue;
                        objAuthJobRoleDetails.ModuleType = Convert.ToString(dr["ModuleType"]);
                        objAuthJobRoleDetails.Module = Convert.ToString(dr["Module"]);
                        objAuthJobRoleDetails.CreateAllowed = Convert.ToInt32(dr["CreateAllowed"]);
                        objAuthJobRoleDetails.ModifiedAllowed = Convert.ToInt32(dr["ModifiedAllowed"]);
                        objAuthJobRoleDetails.DeleteAllowed = Convert.ToInt32(dr["DeleteAllowed"]);
                        objAuthJobRoleDetails.DisplayAllowed = Convert.ToInt32(dr["DisplayAllowed"]);
                        objAuthJobRoleDetails.PrintAllowed = Convert.ToInt32(dr["PrintAllowed"]);
                        objAuthJobRoleDetails.ClientCode = Convert.ToString(dr["ClientCode"]);
                        objAuthJobRoleDetails.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                        objAuthJobRoleDetails.CreatedBy = Convert.ToString(dr["CreatedBy"]);
                        objAuthJobRoleDetails.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);

                        if (objAuthJobRoleDetails.IsDeleted == 0)
                        {
                            objAuthJobRoleDetailManager.SaveAuthJobRoleDetails(objAuthJobRoleDetails, da, lstErr);
                        }
                        else
                        {
                            objAuthJobRoleDetailManager.DeleteAuthJobRoleDetails(objAuthJobRoleDetails.AuthJobRoleCode, objAuthJobRoleDetails.Module, objAuthJobRoleDetails.ClientCode);
                        }
                    }

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


        public string  InsertAuthJobRoleMaster(AuthJobRoleMaster argAuthJobRoleMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@AuthJobRoleCode", argAuthJobRoleMaster.AuthJobRoleCode);
            param[1] = new SqlParameter("@AuthJobRoleDesc", argAuthJobRoleMaster.AuthJobRoleDesc);
            param[2] = new SqlParameter("@ClientCode", argAuthJobRoleMaster.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argAuthJobRoleMaster.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argAuthJobRoleMaster.ModifiedBy);
        
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertAuthJobRoleMaster", param);


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

            return strRetValue;
        }


        public string UpdateAuthJobRoleMaster(AuthJobRoleMaster argAuthJobRoleMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@AuthJobRoleCode", argAuthJobRoleMaster.AuthJobRoleCode);
            param[1] = new SqlParameter("@AuthJobRoleDesc", argAuthJobRoleMaster.AuthJobRoleDesc);
            param[2] = new SqlParameter("@ClientCode", argAuthJobRoleMaster.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argAuthJobRoleMaster.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argAuthJobRoleMaster.ModifiedBy);
 
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateAuthJobRoleMaster", param);


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

            return strRetValue;
        }


        public ICollection<ErrorHandler> DeleteAuthJobRoleMaster(string argAuthJobRoleCode, int IisDeleted, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@AuthJobRoleCode", argAuthJobRoleCode);
                param[1] = new SqlParameter("@IsDeleted", IisDeleted);
                param[2] = new SqlParameter("@ClientCode", argClientCode);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteAuthJobRoleMaster", param);
                
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


        public bool blnIsAuthJobRoleMasterExists(string argAuthJobRoleCode, string argClientCode, DataAccess da)
        {
            bool IsAuthJobRoleMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetAuthJobRoleMaster(argAuthJobRoleCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAuthJobRoleMasterExists = true;
            }
            else
            {
                IsAuthJobRoleMasterExists = false;
            }
            return IsAuthJobRoleMasterExists;
        }
    }
}