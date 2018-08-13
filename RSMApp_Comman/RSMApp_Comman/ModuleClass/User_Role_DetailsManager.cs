
//Created On :: 28, July, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Comman
{
    public class User_Role_DetailsManager
    {
        const string User_Role_DetailsTable = "User_Role_Details";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public User_Role_Details objGetUser_Role_Details(string argUserName, string argClientCode)
        {
            User_Role_Details argUser_Role_Details = new User_Role_Details();
            DataSet DataSetToFill = new DataSet();

            if (argUserName.Trim() == "")
            {
                goto ErrorHandlers;
            }


            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetUser_Role_Details(argUserName, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argUser_Role_Details = this.objCreateUser_Role_Details((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argUser_Role_Details;
        }

        public ICollection<User_Role_Details> colGetUser_Role_Details(string argUserName,string argClientCode)
        {
            List<User_Role_Details> lst = new List<User_Role_Details>();
            DataSet DataSetToFill = new DataSet();
            User_Role_Details tUser_Role_Details = new User_Role_Details();

            DataSetToFill = this.GetUser_Role_Details(argUserName,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateUser_Role_Details(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }


        public ICollection<User_Role_Details> colGetUser_Role_AuthJob_Details(string argUserName, string argClientCode, List<User_Role_Details> lst)
        {
            //List<User_Role_Details> lst = new List<User_Role_Details>();
            DataSet DataSetToFill = new DataSet();
            User_Role_Details tUser_Role_Details = new User_Role_Details();

            DataSetToFill = this.GetUser_Role_AuthJob_Details(argUserName, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateUser_Role_AuthJob_Details(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<User_Role_Details> colGetUser_Role_AuthOrg_Details(string argUserName, string argClientCode, List<User_Role_Details> lst)
        {
            //List<User_Role_Details> lst = new List<User_Role_Details>();
            DataSet DataSetToFill = new DataSet();
            User_Role_Details tUser_Role_Details = new User_Role_Details();

            DataSetToFill = this.GetUser_Role_AuthOrg_Details(argUserName, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateUser_Role_AuthOrg_Details(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }
        
        public ICollection<User_Role_Details> colGetUser_Role_Details(string argUserName, string argClientCode, List<User_Role_Details> lst)
        {

            DataSet DataSetToFill = new DataSet();
            User_Role_Details tUser_Role_Details = new User_Role_Details();

            DataSetToFill = this.GetUser_Role_Details(argUserName, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateUser_Role_Details(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetUser_Role_Details(string argUserName, string argAuthRoleCode, string argRoleType, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@UserName", argUserName);
            param[1] = new SqlParameter("@RoleType", argRoleType);
            param[2] = new SqlParameter("@AuthRoleCode", argAuthRoleCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetUser_Role_Details4ID", param);

            return DataSetToFill;
        }

        public DataSet GetUser_Role_Details(string argUserName, string argAuthRoleCode, string argRoleType, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();


            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@UserName", argUserName);
            param[1] = new SqlParameter("@RoleType", argRoleType);
            param[2] = new SqlParameter("@AuthRoleCode", argAuthRoleCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetUser_Role_Details4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetUser_Role_Details(string argUserName,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@UserName", argUserName);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetUser_Role_Details",param);
            return DataSetToFill;
        }

        public DataSet GetUser_Role_AuthJob_Details(string argUserName,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@UserName", argUserName);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetUser_Role_AuthJob_Detail", param);
            return DataSetToFill;
        }

        public DataSet GetUser_Role_AuthOrg_Details(string argUserName, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@UserName", argUserName);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetUser_Role_AuthOrg_Detail", param);
            return DataSetToFill;
        }
               
        private User_Role_Details objCreateUser_Role_Details(DataRow dr)
        {
            User_Role_Details tUser_Role_Details = new User_Role_Details();

            tUser_Role_Details.SetObjectInfo(dr);

            return tUser_Role_Details;

        }

        private User_Role_Details objCreateUser_Role_AuthJob_Details(DataRow dr)
        {
            User_Role_Details tUser_Role_Details = new User_Role_Details();

            tUser_Role_Details.SetObjectInfo2(dr);

            return tUser_Role_Details;

        }

        private User_Role_Details objCreateUser_Role_AuthOrg_Details(DataRow dr)
        {
            User_Role_Details tUser_Role_Details = new User_Role_Details();

            tUser_Role_Details.SetObjectInfo3(dr);

            return tUser_Role_Details;

        }
        
        public ICollection<ErrorHandler> SaveUser_Role_Details(ICollection<User_Role_Details> colUser_Role_details)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();
               foreach (User_Role_Details argUser_Role_Details in colUser_Role_details)
                {
                    if (blnIsUser_Role_DetailsExists(argUser_Role_Details.UserName, argUser_Role_Details.AuthRoleCode, argUser_Role_Details.RoleType,  argUser_Role_Details.ClientCode, da) == false)
                    {
                        InsertUser_Role_Details(argUser_Role_Details, da, lstErr);
                    }
                    else
                    {
                        UpdateUser_Role_Details(argUser_Role_Details, da, lstErr);
                    }
                }

                foreach (ErrorHandler objerr in lstErr)
                {
                    if (objerr.Type == "E")
                    {
                        da.ROLLBACK_TRANSACTION();
                        return lstErr;
                    }

                    if (objerr.Type == "A")
                    {
                        da.ROLLBACK_TRANSACTION();
                        return lstErr;
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

        public void InsertUser_Role_Details(User_Role_Details argUser_Role_Details, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@UserName", argUser_Role_Details.UserName);
            param[1] = new SqlParameter("@RoleType", argUser_Role_Details.RoleType);
            param[2] = new SqlParameter("@AuthRoleCode", argUser_Role_Details.AuthRoleCode);
            param[3] = new SqlParameter("@ClientCode", argUser_Role_Details.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argUser_Role_Details.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argUser_Role_Details.ModifiedBy);
            
            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertUser_Role_Details", param);
            
            string strMessage = Convert.ToString(param[7].Value);
            string strType = Convert.ToString(param[6].Value);
            string strRetValue = Convert.ToString(param[8].Value);


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

        public void UpdateUser_Role_Details(User_Role_Details argUser_Role_Details, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@UserName", argUser_Role_Details.UserName);
            param[1] = new SqlParameter("@RoleType", argUser_Role_Details.RoleType);
            param[2] = new SqlParameter("@AuthRoleCode", argUser_Role_Details.AuthRoleCode);
            param[3] = new SqlParameter("@ClientCode", argUser_Role_Details.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argUser_Role_Details.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argUser_Role_Details.ModifiedBy);
            
            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateUser_Role_Details", param);
            
            string strMessage = Convert.ToString(param[7].Value);
            string strType = Convert.ToString(param[6].Value);
            string strRetValue = Convert.ToString(param[8].Value);


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

        public ICollection<ErrorHandler> DeleteUser_Role_Details(string argUserName, string argAuthRoleCode, string argClientCode,int iIsdeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@UserName", argUserName);
                param[1] = new SqlParameter("@AuthRoleCode", argAuthRoleCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("@IsDeleted",iIsdeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteUser_Role_Details", param);
                
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

        public bool blnIsUser_Role_DetailsExists(string argUserName, string argAuthRoleCode, string argRoleType, string argClientCode)
        {
            bool IsUser_Role_DetailsExists = false;
            DataSet ds = new DataSet();
            ds = GetUser_Role_Details(argUserName, argAuthRoleCode, argRoleType, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsUser_Role_DetailsExists = true;
            }
            else
            {
                IsUser_Role_DetailsExists = false;
            }
            return IsUser_Role_DetailsExists;
        }

        public bool blnIsUser_Role_DetailsExists(string argUserName, string argAuthRoleCode, string argRoleType, string argClientCode, DataAccess da)
        {
            bool IsUser_Role_DetailsExists = false;
            DataSet ds = new DataSet();
            ds = GetUser_Role_Details(argUserName, argAuthRoleCode, argRoleType, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsUser_Role_DetailsExists = true;
            }
            else
            {
                IsUser_Role_DetailsExists = false;
            }
            return IsUser_Role_DetailsExists;
        }
    }
}