
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
    public class RoleDetailManager
    {
        const string RoleDetailTable = "RoleDetail";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public RoleDetail objGetRoleDetail(string argRoleCode, string argAuthJobRoleCode, string argAuthOrgcode, string argClientCode)
        {
            RoleDetail argRoleDetail = new RoleDetail();
            DataSet DataSetToFill = new DataSet();

            if (argRoleCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argAuthJobRoleCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argAuthOrgcode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetRoleDetail(argRoleCode, argAuthJobRoleCode, argAuthOrgcode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argRoleDetail = this.objCreateRoleDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argRoleDetail;
        }
        
        public ICollection<RoleDetail> colGetRoleDetail(string argRoldeCode, string argClientCode)
        {
            List<RoleDetail> lst = new List<RoleDetail>();
            DataSet DataSetToFill = new DataSet();
            RoleDetail tRoleDetail = new RoleDetail();

            DataSetToFill = this.GetRoleDetail(argRoldeCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateRoleDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetRoleDetail(string argRoleCode, string argAuthJobRoleCode, string argAuthOrgcode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@RoleCode", argRoleCode);
            param[1] = new SqlParameter("@AuthJobRoleCode", argAuthJobRoleCode);
            param[2] = new SqlParameter("@AuthOrgcode", argAuthOrgcode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRoleDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetRoleDetail(string argRoleCode, string argAuthJobRoleCode, string argAuthOrgcode, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@RoleCode", argRoleCode);
            param[1] = new SqlParameter("@AuthJobRoleCode", argAuthJobRoleCode);
            param[2] = new SqlParameter("@AuthOrgcode", argAuthOrgcode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetRoleDetail4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetRoleDetail(string argRoleCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RoleCode", argRoleCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRoleDetail", param);
            return DataSetToFill;
        }
        
        private RoleDetail objCreateRoleDetail(DataRow dr)
        {
            RoleDetail tRoleDetail = new RoleDetail();

            tRoleDetail.SetObjectInfo(dr);

            return tRoleDetail;

        }

        public void SaveRoleDetail(RoleDetail argRoleDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
         
            try
            {
             
                if (blnIsRoleDetailExists(argRoleDetail.RoleCode, argRoleDetail.AuthJobRoleCode, argRoleDetail.AuthOrgCode, argRoleDetail.ClientCode, da) == false)
                {

                     InsertRoleDetail(argRoleDetail, da, lstErr);
                  
                }
                else
                {
                     UpdateRoleDetail(argRoleDetail, da, lstErr);
                  
                }

            }
            catch (Exception ex)
            {
               
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
       
        }

        //public ICollection<ErrorHandler> SaveRoleDetail(RoleDetail argRoleDetail)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsRoleDetailExists(argRoleDetail.RoleCode, argRoleDetail.ProfileCode, argRoleDetail.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertRoleDetail(argRoleDetail, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }

        //            da.COMMIT_TRANSACTION();
        //        }
        //        else
        //        {
        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            UpdateRoleDetail(argRoleDetail, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }

        //            da.COMMIT_TRANSACTION();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (da != null)
        //        {
        //            da.ROLLBACK_TRANSACTION();
        //        }
        //        objErrorHandler.Type = ErrorConstant.strAboartType;
        //        objErrorHandler.MsgId = 0;
        //        objErrorHandler.Module = ErrorConstant.strInsertModule;
        //        objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
        //        objErrorHandler.Message = ex.Message.ToString();
        //        objErrorHandler.RowNo = 0;
        //        objErrorHandler.FieldName = "";
        //        objErrorHandler.LogCode = "";
        //        lstErr.Add(objErrorHandler);
        //    }
        //    finally
        //    {
        //        if (da != null)
        //        {
        //            da.Close_Connection();
        //            da = null;
        //        }
        //    }
        //    return lstErr;
        //}
        
        public string InsertRoleDetail(RoleDetail argRoleDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@RoleCode", argRoleDetail.RoleCode);
            param[1] = new SqlParameter("@AuthJobRoleCode", argRoleDetail.AuthJobRoleCode);
            param[2] = new SqlParameter("@AuthOrgcode", argRoleDetail.AuthOrgCode);
            param[3] = new SqlParameter("@ClientCode", argRoleDetail.ClientCode);
            
            param[4] = new SqlParameter("@Type", SqlDbType.Char);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[5].Size = 255;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertRoleDetail", param);


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

            return strRetValue;
        }
        
        public string UpdateRoleDetail(RoleDetail argRoleDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@RoleCode", argRoleDetail.RoleCode);
            param[1] = new SqlParameter("@AuthJobRoleCode", argRoleDetail.AuthJobRoleCode);
            param[2] = new SqlParameter("@AuthOrgcode", argRoleDetail.AuthOrgCode);
            param[3] = new SqlParameter("@ClientCode", argRoleDetail.ClientCode);
            
            param[4] = new SqlParameter("@Type", SqlDbType.Char);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[5].Size = 255;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateRoleDetail", param);


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

            return strRetValue;
        }

        public void DeleteRoleDetail(string argRoleCode,string argAuthJobRoleCode,string argAuthOrgcode,string argClientCode, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@RoleCode", argRoleCode);
                param[1] = new SqlParameter("@AuthJobRoleCode", argAuthJobRoleCode);
                param[2] = new SqlParameter("@AuthOrgcode", argAuthOrgcode);
                param[3] = new SqlParameter("@ClientCode", argClientCode);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.NExecuteNonQuery("Proc_DeleteRoleDetail", param);
                
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
        }

        public ICollection<ErrorHandler> DeleteRoleDetail(string argRoleCode, string argProfileCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@RoleCode", argRoleCode);
                param[1] = new SqlParameter("@ProfileCode", argProfileCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteRoleDetail", param);


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

        public bool blnIsRoleDetailExists(string argRoleCode, string argAuthJobRoleCode, string argAuthOrgcode, string argClientCode, DataAccess da)
        {
            bool IsRoleDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetRoleDetail(argRoleCode, argAuthJobRoleCode, argAuthOrgcode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsRoleDetailExists = true;
            }
            else
            {
                IsRoleDetailExists = false;
            }
            return IsRoleDetailExists;
        }
    }
}