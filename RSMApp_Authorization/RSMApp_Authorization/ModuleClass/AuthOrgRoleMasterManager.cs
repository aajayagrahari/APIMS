
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
    public class AuthOrgRoleMasterManager
    {
        const string AuthOrgRoleMasterTable = "AuthOrgRoleMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        AuthORGRoleDetailsManager ObjAuthOrgroleDetailManager = new AuthORGRoleDetailsManager();

        public AuthOrgRoleMaster objGetAuthOrgRoleMaster(string argAuthOrgcode, string argClientCode)
        {
            AuthOrgRoleMaster argAuthOrgRoleMaster = new AuthOrgRoleMaster();
            DataSet DataSetToFill = new DataSet();

            if (argAuthOrgcode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetAuthOrgRoleMaster(argAuthOrgcode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argAuthOrgRoleMaster = this.objCreateAuthOrgRoleMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argAuthOrgRoleMaster;
        }
        
        public ICollection<AuthOrgRoleMaster> colGetAuthOrgRoleMaster(string argClientCode)
        {
            List<AuthOrgRoleMaster> lst = new List<AuthOrgRoleMaster>();
            DataSet DataSetToFill = new DataSet();
            AuthOrgRoleMaster tAuthOrgRoleMaster = new AuthOrgRoleMaster();

            DataSetToFill = this.GetAuthOrgRoleMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateAuthOrgRoleMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetAuthOrgRoleMaster(string argAuthOrgcode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AuthOrgcode", argAuthOrgcode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAuthOrgRoleMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetAuthOrgRoleMaster(string argAuthOrgcode, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AuthOrgcode", argAuthOrgcode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetAuthOrgRoleMaster4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetAuthOrgRoleMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAuthOrgRoleMaster", param);
            return DataSetToFill;
        }
        
        private AuthOrgRoleMaster objCreateAuthOrgRoleMaster(DataRow dr)
        {
            AuthOrgRoleMaster tAuthOrgRoleMaster = new AuthOrgRoleMaster();

            tAuthOrgRoleMaster.SetObjectInfo(dr);

            return tAuthOrgRoleMaster;

        }
        
        public ICollection<ErrorHandler> SaveAuthOrgRoleMaster(AuthOrgRoleMaster argAuthOrgRoleMaster, DataTable dtAuthORGRoleDetails)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            string strRetValue = "";
            AuthORGRoleDetails objAuthORGRoleDetail = null;
            try
            {
                
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsAuthOrgRoleMasterExists(argAuthOrgRoleMaster.AuthOrgcode, argAuthOrgRoleMaster.ClientCode, da) == false)
                {

                  strRetValue =   InsertAuthOrgRoleMaster(argAuthOrgRoleMaster, da, lstErr);               
                }
                else
                {
                   strRetValue =  UpdateAuthOrgRoleMaster(argAuthOrgRoleMaster, da, lstErr);                
                }

                foreach (ErrorHandler objerr in lstErr)
                {
                    if (objerr.Type == "E")
                    {
                        da.ROLLBACK_TRANSACTION();
                        return lstErr;
                    }
                }

                if (dtAuthORGRoleDetails.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtAuthORGRoleDetails.Rows)
                    {
                        objAuthORGRoleDetail = new AuthORGRoleDetails();
                        objAuthORGRoleDetail.AuthOrgcode = strRetValue;
                        objAuthORGRoleDetail.SerialNo = Convert.ToInt32(dr["SerialNo"]);
                        objAuthORGRoleDetail.FieldName = Convert.ToString(dr["FieldName"]);
                        objAuthORGRoleDetail.FieldValueFrom = Convert.ToString(dr["FieldValueFrom"]);
                        objAuthORGRoleDetail.FieldValueTo = Convert.ToString(dr["FieldValueTo"]);
                        objAuthORGRoleDetail.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                        objAuthORGRoleDetail.ClientCode = Convert.ToString(dr["ClientCode"]);
                        objAuthORGRoleDetail.CreatedBy = Convert.ToString(dr["CreatedBy"]);
                        objAuthORGRoleDetail.ModifiedBy = Convert.ToString(dr["ModifiedBy"]);

                        if (objAuthORGRoleDetail.IsDeleted == 0)
                        {
                            ObjAuthOrgroleDetailManager.SaveAuthORGRoleDetails(objAuthORGRoleDetail, da, lstErr);
                        }
                        else
                        {
                            ObjAuthOrgroleDetailManager.DeleteAuthORGRoleDetails(objAuthORGRoleDetail.AuthOrgcode, objAuthORGRoleDetail.FieldName, objAuthORGRoleDetail.ClientCode);
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
        
        public string InsertAuthOrgRoleMaster(AuthOrgRoleMaster argAuthOrgRoleMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@AuthOrgcode", argAuthOrgRoleMaster.AuthOrgcode);
            param[1] = new SqlParameter("@AuthORGDesc", argAuthOrgRoleMaster.AuthORGDesc);
            param[2] = new SqlParameter("@ClientCode", argAuthOrgRoleMaster.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argAuthOrgRoleMaster.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argAuthOrgRoleMaster.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertAuthOrgRoleMaster", param);


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
        
        public string UpdateAuthOrgRoleMaster(AuthOrgRoleMaster argAuthOrgRoleMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@AuthOrgcode", argAuthOrgRoleMaster.AuthOrgcode);
            param[1] = new SqlParameter("@AuthORGDesc", argAuthOrgRoleMaster.AuthORGDesc);
            param[2] = new SqlParameter("@ClientCode", argAuthOrgRoleMaster.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argAuthOrgRoleMaster.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argAuthOrgRoleMaster.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateAuthOrgRoleMaster", param);


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
        
        public ICollection<ErrorHandler> DeleteAuthOrgRoleMaster(string argAuthOrgcode, int IisDeleted, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@AuthOrgcode", argAuthOrgcode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteAuthOrgRoleMaster", param);


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
        
        public bool blnIsAuthOrgRoleMasterExists(string argAuthOrgcode, string argClientCode, DataAccess da)
        {
            bool IsAuthOrgRoleMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetAuthOrgRoleMaster(argAuthOrgcode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAuthOrgRoleMasterExists = true;
            }
            else
            {
                IsAuthOrgRoleMasterExists = false;
            }
            return IsAuthOrgRoleMasterExists;
        }

    }
}