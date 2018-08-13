
//Created On :: 09, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Comman
{
    public class UserLoginManager
    {
        const string UserLoginTable = "UserLogin";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public UserLogin objGetUserLogin(int argidUserLogin)
        {
            UserLogin argUserLogin = new UserLogin();
            DataSet DataSetToFill = new DataSet();

            if (argidUserLogin <= 0)
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetUserLogin(argidUserLogin);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argUserLogin = this.objCreateUserLogin((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argUserLogin;
        }


        public ICollection<UserLogin> colGetUserLogin()
        {
            List<UserLogin> lst = new List<UserLogin>();
            DataSet DataSetToFill = new DataSet();
            UserLogin tUserLogin = new UserLogin();

            DataSetToFill = this.GetUserLogin();

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateUserLogin(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetUserLogin(int argidUserLogin)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@idUserLogin", argidUserLogin);
            DataSetToFill = da.FillDataSet("SP_GetUserLogin4ID", param);

            return DataSetToFill;
        }


        public DataSet GetUserLogin()
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            DataSetToFill = da.FillDataSet("SP_GetUserLogin");
            return DataSetToFill;
        }


        private UserLogin objCreateUserLogin(DataRow dr)
        {
            UserLogin tUserLogin = new UserLogin();

            tUserLogin.SetObjectInfo(dr);

            return tUserLogin;

        }


        public ICollection<ErrorHandler> SaveUserLogin(UserLogin argUserLogin)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsUserLoginExists(argUserLogin.idUserLogin) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertUserLogin(argUserLogin, da, lstErr);
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
                    UpdateUserLogin(argUserLogin, da, lstErr);
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


        public void InsertUserLogin(UserLogin argUserLogin, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@idUserLogin", argUserLogin.idUserLogin);
            param[1] = new SqlParameter("@UserName", argUserLogin.UserName);
            param[2] = new SqlParameter("@IP", argUserLogin.IP);
            param[3] = new SqlParameter("@UserAgent", argUserLogin.UserAgent);
            param[4] = new SqlParameter("@LoginDate", argUserLogin.LoginDate);
            param[5] = new SqlParameter("@LastRefreshedDate", argUserLogin.LastRefreshedDate);
            param[6] = new SqlParameter("@IsSuccessfull", argUserLogin.IsSuccessfull);
            param[7] = new SqlParameter("@Host", argUserLogin.Host);
            param[8] = new SqlParameter("@ClientCode", argUserLogin.ClientCode);
            param[9] = new SqlParameter("@PartnerCode", argUserLogin.PartnerCode);

            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;
            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;
            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertUserLogin", param);


            string strMessage = Convert.ToString(param[11].Value);
            string strType = Convert.ToString(param[10].Value);
            string strRetValue = Convert.ToString(param[12].Value);


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


        public void UpdateUserLogin(UserLogin argUserLogin, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@idUserLogin", argUserLogin.idUserLogin);
            param[1] = new SqlParameter("@UserName", argUserLogin.UserName);
            param[2] = new SqlParameter("@IP", argUserLogin.IP);
            param[3] = new SqlParameter("@UserAgent", argUserLogin.UserAgent);
            param[4] = new SqlParameter("@LoginDate", argUserLogin.LoginDate);
            param[5] = new SqlParameter("@LastRefreshedDate", argUserLogin.LastRefreshedDate);
            param[6] = new SqlParameter("@IsSuccessfull", argUserLogin.IsSuccessfull);
            param[7] = new SqlParameter("@Host", argUserLogin.Host);
            param[8] = new SqlParameter("@ClientCode", argUserLogin.ClientCode);
            param[9] = new SqlParameter("@PartnerCode", argUserLogin.PartnerCode);

            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;
            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;
            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateUserLogin", param);


            string strMessage = Convert.ToString(param[11].Value);
            string strType = Convert.ToString(param[10].Value);
            string strRetValue = Convert.ToString(param[12].Value);


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


        public ICollection<ErrorHandler> DeleteUserLogin(int argidUserLogin)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@idUserLogin", argidUserLogin);

                param[1] = new SqlParameter("@Type", SqlDbType.Char);
                param[1].Size = 1;
                param[1].Direction = ParameterDirection.Output;
                param[2] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[2].Size = 255;
                param[2].Direction = ParameterDirection.Output;
                param[3] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[3].Size = 20;
                param[3].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteUserLogin", param);


                string strMessage = Convert.ToString(param[2].Value);
                string strType = Convert.ToString(param[1].Value);
                string strRetValue = Convert.ToString(param[3].Value);


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


        public bool blnIsUserLoginExists(int argidUserLogin)
        {
            bool IsUserLoginExists = false;
            DataSet ds = new DataSet();
            ds = GetUserLogin(argidUserLogin);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsUserLoginExists = true;
            }
            else
            {
                IsUserLoginExists = false;
            }
            return IsUserLoginExists;
        }
    }
}