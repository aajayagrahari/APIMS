
//Created On :: 23, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;


namespace RSMApp_FI
{
    public class GLAccountManager
    {
        const string GLAccountTable = "GLAccount";

//        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public GLAccount objGetGLAccount(string argGLCode, string argClientCode)
        {
            GLAccount argGLAccount = new GLAccount();
            DataSet DataSetToFill = new DataSet();

            if (argGLCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetGLAccount(argGLCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argGLAccount = this.objCreateGLAccount((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argGLAccount;
        }


        public ICollection<GLAccount> colGetGLAccount(string argClientCode)
        {
            List<GLAccount> lst = new List<GLAccount>();
            DataSet DataSetToFill = new DataSet();
            GLAccount tGLAccount = new GLAccount();

            DataSetToFill = this.GetGLAccount(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateGLAccount(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


      

        public DataSet GetGLAccount(string argGLCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@GLCode", argGLCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetGLAccount4ID", param);

            return DataSetToFill;
        }


        public DataSet GetGLAccount(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + GLAccountTable.ToString();

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


        public DataSet GetGLAccount(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetGLAccount",param);
            return DataSetToFill;
        }


        public DataSet GetGLAccount4Combo(string argPrefix, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetGLAccount4Combo", param);

            return DataSetToFill;
        }


        private GLAccount objCreateGLAccount(DataRow dr)
        {
            GLAccount tGLAccount = new GLAccount();

            tGLAccount.SetObjectInfo(dr);

            return tGLAccount;

        }


        public ICollection<ErrorHandler> SaveGLAccount(GLAccount argGLAccount)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsGLAccountExists(argGLAccount.GLCode, argGLAccount.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertGLAccount(argGLAccount, da, lstErr);
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
                    UpdateGLAccount(argGLAccount, da, lstErr);
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


        public void InsertGLAccount(GLAccount argGLAccount, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@GLCode", argGLAccount.GLCode);
            param[1] = new SqlParameter("@GLDesc", argGLAccount.GLDesc);
            param[2] = new SqlParameter("@GLType", argGLAccount.GLType);
            param[3] = new SqlParameter("@ChartACCode", argGLAccount.ChartACCode);
            param[4] = new SqlParameter("@CompanyCode", argGLAccount.CompanyCode);
            param[5] = new SqlParameter("@PostLocalCurrency", argGLAccount.PostLocalCurrency);
            param[6] = new SqlParameter("@AutoPost", argGLAccount.AutoPost);

            param[7] = new SqlParameter("@ClientCode", argGLAccount.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argGLAccount.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argGLAccount.ModifiedBy);
            
            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertGLAccount", param);
            
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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }


        public void UpdateGLAccount(GLAccount argGLAccount, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@GLCode", argGLAccount.GLCode);
            param[1] = new SqlParameter("@GLDesc", argGLAccount.GLDesc);
            param[2] = new SqlParameter("@GLType", argGLAccount.GLType);
            param[3] = new SqlParameter("@ChartACCode", argGLAccount.ChartACCode);
            param[4] = new SqlParameter("@CompanyCode", argGLAccount.CompanyCode);
            param[5] = new SqlParameter("@PostLocalCurrency", argGLAccount.PostLocalCurrency);
            param[6] = new SqlParameter("@AutoPost", argGLAccount.AutoPost);

            param[7] = new SqlParameter("@ClientCode", argGLAccount.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argGLAccount.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argGLAccount.ModifiedBy);

            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateGLAccount", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }


        public ICollection<ErrorHandler> DeleteGLAccount(string argGLCode, string argClientCode,int IisDeleted )
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@GLCode", argGLCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("IsDeleted",IisDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteGLAccount", param);


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


        public bool blnIsGLAccountExists(string argGLCode, string argClientCode)
        {
            bool IsGLAccountExists = false;
            DataSet ds = new DataSet();
            ds = GetGLAccount(argGLCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsGLAccountExists = true;
            }
            else
            {
                IsGLAccountExists = false;
            }
            return IsGLAccountExists;
        }
    }
}