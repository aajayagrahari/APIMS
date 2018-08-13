
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
    public class GLAccount_AccountGroupManager
    {
        const string GLAccount_AccountGroupTable = "GLAccount_AccountGroup";

       //  GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public GLAccount_AccountGroup objGetGLAccount_AccountGroup(string argGLCode, string argAccGroupCode, string argClientCode)
        {
            GLAccount_AccountGroup argGLAccount_AccountGroup = new GLAccount_AccountGroup();
            DataSet DataSetToFill = new DataSet();

            if (argGLCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argAccGroupCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetGLAccount_AccountGroup(argGLCode, argAccGroupCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argGLAccount_AccountGroup = this.objCreateGLAccount_AccountGroup((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argGLAccount_AccountGroup;
        }


        public ICollection<GLAccount_AccountGroup> colGetGLAccount_AccountGroup(string argClientCode)
        {
            List<GLAccount_AccountGroup> lst = new List<GLAccount_AccountGroup>();
            DataSet DataSetToFill = new DataSet();
            GLAccount_AccountGroup tGLAccount_AccountGroup = new GLAccount_AccountGroup();

            DataSetToFill = this.GetGLAccount_AccountGroup(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateGLAccount_AccountGroup(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetGLAccount_AccountGroup(string argGLCode, string argAccGroupCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@GLCode", argGLCode);
            param[1] = new SqlParameter("@AccGroupCode", argAccGroupCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetGLAccount_AccountGroup4ID", param);

            return DataSetToFill;
        }


        public DataSet GetGLAccount_AccountGroup( string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetGLAccount_AccountGroup",param);
            return DataSetToFill;
        }


        private GLAccount_AccountGroup objCreateGLAccount_AccountGroup(DataRow dr)
        {
            GLAccount_AccountGroup tGLAccount_AccountGroup = new GLAccount_AccountGroup();

            tGLAccount_AccountGroup.SetObjectInfo(dr);

            return tGLAccount_AccountGroup;

        }


        public ICollection<ErrorHandler> SaveGLAccount_AccountGroup(GLAccount_AccountGroup argGLAccount_AccountGroup)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsGLAccount_AccountGroupExists(argGLAccount_AccountGroup.GLCode, argGLAccount_AccountGroup.AccGroupCode, argGLAccount_AccountGroup.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertGLAccount_AccountGroup(argGLAccount_AccountGroup, da, lstErr);
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
                    UpdateGLAccount_AccountGroup(argGLAccount_AccountGroup, da, lstErr);
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


        public void InsertGLAccount_AccountGroup(GLAccount_AccountGroup argGLAccount_AccountGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@GLCode", argGLAccount_AccountGroup.GLCode);
            param[1] = new SqlParameter("@AccGroupCode", argGLAccount_AccountGroup.AccGroupCode);
            param[2] = new SqlParameter("@ClientCode", argGLAccount_AccountGroup.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argGLAccount_AccountGroup.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argGLAccount_AccountGroup.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertGLAccount_AccountGroup", param);
            
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

        }


        public void UpdateGLAccount_AccountGroup(GLAccount_AccountGroup argGLAccount_AccountGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@GLCode", argGLAccount_AccountGroup.GLCode);
            param[1] = new SqlParameter("@AccGroupCode", argGLAccount_AccountGroup.AccGroupCode);
            param[2] = new SqlParameter("@ClientCode", argGLAccount_AccountGroup.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argGLAccount_AccountGroup.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argGLAccount_AccountGroup.ModifiedBy);

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateGLAccount_AccountGroup", param);


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

        }


        public ICollection<ErrorHandler> DeleteGLAccount_AccountGroup(string argGLCode, string argAccGroupCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@GLCode", argGLCode);
                param[1] = new SqlParameter("@AccGroupCode", argAccGroupCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("IsDeleted",iIsDeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteGLAccount_AccountGroup", param);
                
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


        public bool blnIsGLAccount_AccountGroupExists(string argGLCode, string argAccGroupCode, string argClientCode)
        {
            bool IsGLAccount_AccountGroupExists = false;
            DataSet ds = new DataSet();
            ds = GetGLAccount_AccountGroup(argGLCode, argAccGroupCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsGLAccount_AccountGroupExists = true;
            }
            else
            {
                IsGLAccount_AccountGroupExists = false;
            }
            return IsGLAccount_AccountGroupExists;
        }
    }
}