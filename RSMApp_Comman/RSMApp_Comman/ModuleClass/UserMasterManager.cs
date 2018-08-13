
//Created On :: 07, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_Comman
{
    public class UserMasterManager
    {
        const string UserMasterTable = "UserMaster";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public UserMaster objGetUserMaster(string argUserName, string argClientCode)
        {
            UserMaster argUserMaster = new UserMaster();
            DataSet DataSetToFill = new DataSet();

            if (argUserName.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            DataSetToFill = this.GetUserMaster(argUserName, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argUserMaster = this.objCreateUserMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandler:

        Finish:
            DataSetToFill = null;


            return argUserMaster;
        }
        
        public ICollection<UserMaster> colGetUserMaster(string argClientCode)
        {
            List<UserMaster> lst = new List<UserMaster>();
            DataSet DataSetToFill = new DataSet();
            UserMaster tUserMaster = new UserMaster();

            DataSetToFill = this.GetUserMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateUserMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetUser(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + UserMasterTable.ToString();

                if (iIsDeleted > -1)
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " IsDeleted = " + iIsDeleted;
                }

                if (argClientCode != "")
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " ClientCode = '" + argClientCode + "'";
                }

                ds = da.FillDataSetWithSQL(tSQL);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet GetUserMaster(string argUserName, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserName", argUserName);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetUserMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetUserMaster(string argUserName, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@UserName", argUserName);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetUserMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet CheckUserLogin(string argUserName, string argUserPassword, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@UserName", argUserName);
            param[1] = new SqlParameter("@UserPassword", argUserPassword);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_CheckUserLogin", param);

            return DataSetToFill;
        }

        public DataSet GetUserExists(string argUserName, string argUserPassword)
        {
            DataSet dsCountry = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + UserMasterTable.ToString();

                if (argUserName != "")
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " UserName = '" + argUserName + "'";
                }

                if (argUserPassword != "")
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " UserPassword = '" + argUserPassword + "'";
                }


                dsCountry = da.FillDataSetWithSQL(tSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dsCountry;
        }
        
        public DataSet GetUserMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetUserMaster", param);
            return DataSetToFill;
        }

        public DataSet GetUserMaster4Combo(string argPrefix, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetUserMaster4Combo", param);

            return DataSetToFill;
        }
        
        private UserMaster objCreateUserMaster(DataRow dr)
        {
            UserMaster tUserMaster = new UserMaster();

            tUserMaster.SetObjectInfo(dr);

            return tUserMaster;

        }
        
        public ICollection<ErrorHandler> SaveUserMaster(UserMaster argUserMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsUserMasterExists(argUserMaster.UserName, argUserMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertUserMaster(argUserMaster, da, lstErr);
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
                    UpdateUserMaster(argUserMaster, da, lstErr);
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

        public void SaveUserMaster(UserMaster argUserMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsUserMasterExists(argUserMaster.UserName, argUserMaster.ClientCode, da) == false)
                {
                    InsertUserMaster(argUserMaster, da, lstErr);
                }
                else
                {
                    UpdateUserMaster(argUserMaster, da, lstErr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Bulk Insert
        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
            UserMaster ObjUserMaster = null;
            string xConnStr = "";
            string strSheetName = "";
            DataSet dsExcel = new DataSet();
            DataTable dtTableSchema = new DataTable();
            OleDbConnection objXConn = null;
            OleDbDataAdapter objDataAdapter = new OleDbDataAdapter();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            if (argFileExt.ToString() == ".xls")
            {
                xConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" +
               "Data Source=" + argExcelPath.Trim() + ";" +
               "Extended Properties=Excel 8.0";
            }
            else
            {
                xConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
               "Data Source=" + argExcelPath.Trim() + ";" +
               "Extended Properties=Excel 12.0";
            }

            try
            {
                objXConn = new OleDbConnection(xConnStr);
                objXConn.Open();
                dtTableSchema = objXConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                if (argFileExt.ToString() == ".xls")
                {
                    strSheetName = Convert.ToString(dtTableSchema.Rows[0]["TABLE_NAME"]);
                }
                else
                {
                    strSheetName = Convert.ToString(dtTableSchema.Rows[0]["TABLE_NAME"]);

                    if (strSheetName.IndexOf(@"_xlnm#_FilterDatabase") >= 0)
                    {
                        strSheetName = Convert.ToString(dtTableSchema.Rows[1]["TABLE_NAME"]);
                    }
                }
                argQuery = argQuery + " [" + strSheetName + "]";
                OleDbCommand objCommand = new OleDbCommand(argQuery, objXConn);
                objDataAdapter.SelectCommand = objCommand;
                objDataAdapter.Fill(dsExcel);
                dtExcel = dsExcel.Tables[0];
                /*****************************************/
                DataAccess da = new DataAccess();
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                try
                {
                    foreach (DataRow drExcel in dtExcel.Rows)
                    {
                        ObjUserMaster = new UserMaster();

                        ObjUserMaster.UserName = Convert.ToString(drExcel["UserName"]).Trim();
                        ObjUserMaster.UserPassword = Convert.ToString(drExcel["UserPassword"]).Trim();
                        ObjUserMaster.FirstName = Convert.ToString(drExcel["FirstName"]).Trim();
                        ObjUserMaster.LastName = Convert.ToString(drExcel["LastName"]).Trim();
                        ObjUserMaster.Gender = Convert.ToString(drExcel["Gender"]).Trim();
                        ObjUserMaster.EMPCode = Convert.ToString(drExcel["EMPCode"]).Trim();
                        ObjUserMaster.Address1 = Convert.ToString(drExcel["Address1"]).Trim();
                        ObjUserMaster.Address2 = Convert.ToString(drExcel["Address2"]).Trim();
                        ObjUserMaster.Phone1 = Convert.ToString(drExcel["Phone1"]).Trim();
                        ObjUserMaster.Phone2 = Convert.ToString(drExcel["Phone2"]).Trim();
                        ObjUserMaster.EmailID = Convert.ToString(drExcel["EmailID"]).Trim();
                        ObjUserMaster.City = Convert.ToString(drExcel["City"]).Trim();
                        ObjUserMaster.StateCode = Convert.ToString(drExcel["StateCode"]).Trim();
                        ObjUserMaster.CountryCode = Convert.ToString(drExcel["CountryCode"]).Trim();
                        ObjUserMaster.ClientCode = Convert.ToString(argClientCode);

                        SaveUserMaster(ObjUserMaster, da, lstErr);

                        foreach (ErrorHandler objerr in lstErr)
                        {
                            if (objerr.Type == "E")
                            {
                                da.ROLLBACK_TRANSACTION();
                                break;
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objXConn.Close();
            }
            return lstErr;
        }
        #endregion
        
        public void InsertUserMaster(UserMaster argUserMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@UserName", argUserMaster.UserName);
            param[1] = new SqlParameter("@UserPassword", argUserMaster.UserPassword);
            param[2] = new SqlParameter("@FirstName", argUserMaster.FirstName);
            param[3] = new SqlParameter("@LastName", argUserMaster.LastName);
            param[4] = new SqlParameter("@Gender", argUserMaster.Gender);
            param[5] = new SqlParameter("@EMPCode", argUserMaster.EMPCode);
            param[6] = new SqlParameter("@Address1", argUserMaster.Address1);
            param[7] = new SqlParameter("@Address2", argUserMaster.Address2);
            param[8] = new SqlParameter("@Phone1", argUserMaster.Phone1);
            param[9] = new SqlParameter("@Phone2", argUserMaster.Phone2);
            param[10] = new SqlParameter("@EmailID", argUserMaster.EmailID);
            param[11] = new SqlParameter("@City", argUserMaster.City);
            param[12] = new SqlParameter("@StateCode", argUserMaster.StateCode);
            param[13] = new SqlParameter("@CountryCode", argUserMaster.CountryCode);
            param[14] = new SqlParameter("@ClientCode", argUserMaster.ClientCode);
            

            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertUserMaster", param);


            string strMessage = Convert.ToString(param[16].Value);
            string strType = Convert.ToString(param[15].Value);
            string strRetValue = Convert.ToString(param[17].Value);


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
        
        public void UpdateUserMaster(UserMaster argUserMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@UserName", argUserMaster.UserName);
            param[1] = new SqlParameter("@UserPassword", argUserMaster.UserPassword);
            param[2] = new SqlParameter("@FirstName", argUserMaster.FirstName);
            param[3] = new SqlParameter("@LastName", argUserMaster.LastName);
            param[4] = new SqlParameter("@Gender", argUserMaster.Gender);
            param[5] = new SqlParameter("@EMPCode", argUserMaster.EMPCode);
            param[6] = new SqlParameter("@Address1", argUserMaster.Address1);
            param[7] = new SqlParameter("@Address2", argUserMaster.Address2);
            param[8] = new SqlParameter("@Phone1", argUserMaster.Phone1);
            param[9] = new SqlParameter("@Phone2", argUserMaster.Phone2);
            param[10] = new SqlParameter("@EmailID", argUserMaster.EmailID);
            param[11] = new SqlParameter("@City", argUserMaster.City);
            param[12] = new SqlParameter("@StateCode", argUserMaster.StateCode);
            param[13] = new SqlParameter("@CountryCode", argUserMaster.CountryCode);
            param[14] = new SqlParameter("@ClientCode", argUserMaster.ClientCode);
            

            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateUserMaster", param);


            string strMessage = Convert.ToString(param[16].Value);
            string strType = Convert.ToString(param[15].Value);
            string strRetValue = Convert.ToString(param[17].Value);


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
        
        public ICollection<ErrorHandler> DeleteUserMaster(string argUserName, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@UserName", argUserName);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteUserMaster", param);


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
        
        public ICollection<ErrorHandler> UpdateUserPassword(string argUserName, string argClientCode, string argUserPassword, string argNewPassword)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@UserName", argUserName);
                param[1] = new SqlParameter("@UserPassword", argUserPassword);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("@NewPassword", argNewPassword);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("SL_Proc_UpdateUserPassword", param);


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

        //SL_Proc_UpdateUserPassword

        public bool blnIsUserMasterExists(string argUserName, string argClientCode)
        {
            bool IsUserMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetUserMaster(argUserName, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsUserMasterExists = true;
            }
            else
            {
                IsUserMasterExists = false;
            }
            return IsUserMasterExists;
        }

        public bool blnIsUserMasterExists(string argUserName, string argClientCode, DataAccess da)
        {
            bool IsUserMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetUserMaster(argUserName, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsUserMasterExists = true;
            }
            else
            {
                IsUserMasterExists = false;
            }
            return IsUserMasterExists;
        }
    }
}