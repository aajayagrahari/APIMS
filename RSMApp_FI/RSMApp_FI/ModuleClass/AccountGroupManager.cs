
//Created On :: 05, September, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;


namespace RSMApp_FI
{
    public class AccountGroupManager
    {
        const string AccountGroupTable = "AccountGroup";
        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public AccountGroup objGetAccountGroup(string argAccGroupCode, string argClientCode)
        {
            AccountGroup argAccountGroup = new AccountGroup();
            DataSet DataSetToFill = new DataSet();

            if (argAccGroupCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetAccountGroup(argAccGroupCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argAccountGroup = this.objCreateAccountGroup((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argAccountGroup;
        }
        
        public ICollection<AccountGroup> colGetAccountGroup(string argClientCode)
        {
            List<AccountGroup> lst = new List<AccountGroup>();
            DataSet DataSetToFill = new DataSet();
            AccountGroup tAccountGroup = new AccountGroup();

            DataSetToFill = this.GetAccountGroup(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateAccountGroup(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetAccountGroup(string argAccGroupCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AccGroupCode", argAccGroupCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAccountGroup4ID", param);

            return DataSetToFill;
        }

       

        public DataSet GetAccountGroup(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAccountGroup",param);
            return DataSetToFill;
        }

        public DataSet GetAccountGroup(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + AccountGroupTable.ToString();

                if (iIsDeleted >= 0)
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
        
        private AccountGroup objCreateAccountGroup(DataRow dr)
        {
            AccountGroup tAccountGroup = new AccountGroup();

            tAccountGroup.SetObjectInfo(dr);

            return tAccountGroup;

        }
        
        public ICollection<ErrorHandler> SaveAccountGroup(AccountGroup argAccountGroup)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsAccountGroupExists(argAccountGroup.AccGroupCode, argAccountGroup.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertAccountGroup(argAccountGroup, da, lstErr);
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
                    UpdateAccountGroup(argAccountGroup, da, lstErr);
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
        
        public void InsertAccountGroup(AccountGroup argAccountGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@AccGroupCode", argAccountGroup.AccGroupCode);
            param[1] = new SqlParameter("@GroupDesc", argAccountGroup.GroupDesc);
            param[2] = new SqlParameter("@AccCategoryCode", argAccountGroup.AccCategoryCode);
            param[3] = new SqlParameter("@ClientCode", argAccountGroup.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argAccountGroup.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argAccountGroup.ModifiedBy);
            
            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertAccountGroup", param);
            
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
        
        public void UpdateAccountGroup(AccountGroup argAccountGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@AccGroupCode", argAccountGroup.AccGroupCode);
            param[1] = new SqlParameter("@GroupDesc", argAccountGroup.GroupDesc);
            param[2] = new SqlParameter("@AccCategoryCode", argAccountGroup.AccCategoryCode);
            param[3] = new SqlParameter("@ClientCode", argAccountGroup.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argAccountGroup.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argAccountGroup.ModifiedBy);

            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateAccountGroup", param);


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
        
        public ICollection<ErrorHandler> DeleteAccountGroup(string argAccGroupCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@AccGroupCode", argAccGroupCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteAccountGroup", param);
                
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
        
        public bool blnIsAccountGroupExists(string argAccGroupCode, string argClientCode)
        {
            bool IsAccountGroupExists = false;
            DataSet ds = new DataSet();
            ds = GetAccountGroup(argAccGroupCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAccountGroupExists = true;
            }
            else
            {
                IsAccountGroupExists = false;
            }
            return IsAccountGroupExists;
        }
    }
}