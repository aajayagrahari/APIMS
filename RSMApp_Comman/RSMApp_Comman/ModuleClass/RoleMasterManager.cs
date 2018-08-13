
//Created On :: 12, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;


namespace RSMApp_Comman
{
    public class RoleMasterManager
    {
        const string RoleMasterTable = "RoleMaster";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        RoleDetailManager objRoleDetailManager = new RoleDetailManager();

        public RoleMaster objGetRoleMaster(string argRoleCode, string argClientCode)
        {
            RoleMaster argRoleMaster = new RoleMaster();
            DataSet DataSetToFill = new DataSet();

            if (argRoleCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetRoleMaster(argRoleCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argRoleMaster = this.objCreateRoleMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argRoleMaster;
        }
        
        public ICollection<RoleMaster> colGetRoleMaster(string argClientCode)
        {
            List<RoleMaster> lst = new List<RoleMaster>();
            DataSet DataSetToFill = new DataSet();
            RoleMaster tRoleMaster = new RoleMaster();

            DataSetToFill = this.GetRoleMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateRoleMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

      
        
        public DataSet GetRoleMaster(string argRoleCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RoleCode", argRoleCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRoleMaster4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetRoleMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRoleMaster", param);
            return DataSetToFill;
        }

        public DataSet GetRoleMaster(int iIsDeleted)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + RoleMasterTable.ToString();

                if (iIsDeleted >= 0)
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " IsDeleted = " + iIsDeleted;
                }


                ds = da.FillDataSetWithSQL(tSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }
        
        private RoleMaster objCreateRoleMaster(DataRow dr)
        {
            RoleMaster tRoleMaster = new RoleMaster();

            tRoleMaster.SetObjectInfo(dr);

            return tRoleMaster;

        }

        public ICollection<ErrorHandler> SaveRoleMaster(RoleMaster argRoleMaster, ICollection<RoleDetail> colRoleDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            string strretValue = "";
            try
            {

                da.Open_Connection();
                da.BEGIN_TRANSACTION();


                if (blnIsRoleMasterExists(argRoleMaster.RoleCode, argRoleMaster.ClientCode) == false)
                {

                   strretValue =  InsertRoleMaster(argRoleMaster, da, lstErr);
                   
                }
                else
                {
                   
                  strretValue =   UpdateRoleMaster(argRoleMaster, da, lstErr);
               
                }

                foreach (ErrorHandler objerr in lstErr)
                {
                    if (objerr.Type == "E")
                    {
                        da.ROLLBACK_TRANSACTION();
                        return lstErr;
                    }
                }

                if (strretValue == argRoleMaster.RoleCode)
                {
                    if (colRoleDetail.Count > 0)
                    {
                        foreach (RoleDetail argRoleDetail in colRoleDetail)
                        {
                            if (argRoleDetail.IsDeleted == 0)
                            {
                                objRoleDetailManager.SaveRoleDetail(argRoleDetail, da, lstErr);
                            }
                            else
                            {
                                objRoleDetailManager.DeleteRoleDetail(argRoleDetail.RoleCode, argRoleDetail.AuthJobRoleCode, argRoleDetail.AuthOrgCode, argRoleDetail.ClientCode, da, lstErr);
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
        
        
        public ICollection<ErrorHandler> SaveRoleMaster(RoleMaster argRoleMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsRoleMasterExists(argRoleMaster.RoleCode, argRoleMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertRoleMaster(argRoleMaster, da, lstErr);
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
                    UpdateRoleMaster(argRoleMaster, da, lstErr);
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
        
        public string InsertRoleMaster(RoleMaster argRoleMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@RoleCode", argRoleMaster.RoleCode);
            param[1] = new SqlParameter("@Role", argRoleMaster.Role);
            param[2] = new SqlParameter("@ClientCode", argRoleMaster.ClientCode);
            
            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;

            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertRoleMaster", param);


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

            return strRetValue;

        }
        
        public string UpdateRoleMaster(RoleMaster argRoleMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@RoleCode", argRoleMaster.RoleCode);
            param[1] = new SqlParameter("@Role", argRoleMaster.Role);
            param[2] = new SqlParameter("@ClientCode", argRoleMaster.ClientCode);
            
            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;

            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateRoleMaster", param);


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

            return strRetValue;
        }
        
        public ICollection<ErrorHandler> DeleteRoleMaster(string argRoleCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@RoleCode", argRoleCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteRoleMaster", param);


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
        
        public bool blnIsRoleMasterExists(string argRoleCode, string argClientCode)
        {
            bool IsRoleMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetRoleMaster(argRoleCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsRoleMasterExists = true;
            }
            else
            {
                IsRoleMasterExists = false;
            }
            return IsRoleMasterExists;
        }

    }
}