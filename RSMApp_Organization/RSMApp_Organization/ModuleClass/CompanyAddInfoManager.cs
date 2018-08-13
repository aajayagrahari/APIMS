
//Created On :: 07, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Organization
{
    public class CompanyAddInfoManager
    {
        const string CompanyAddInfoTable = "CompanyAddInfo";
        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CompanyAddInfo objGetCompanyAddInfo(string argCompanyCode, string argParamName)
        {
            CompanyAddInfo argCompanyAddInfo = new CompanyAddInfo();
            DataSet DataSetToFill = new DataSet();

            if (argCompanyCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argParamName.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetCompanyAddInfo(argCompanyCode, argParamName);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argCompanyAddInfo = this.objCreateCompanyAddInfo((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argCompanyAddInfo;
        }
        
        public ICollection<CompanyAddInfo> colGetCompanyAddInfo(string argCompanyCode)
        {
            List<CompanyAddInfo> lst = new List<CompanyAddInfo>();
            DataSet DataSetToFill = new DataSet();
            CompanyAddInfo tCompanyAddInfo = new CompanyAddInfo();
            DataSetToFill = this.GetCompanyAddInfo(argCompanyCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCompanyAddInfo(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }
        
        public DataSet GetCompanyAddInfo(string argCompanyCode, string argParamName)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[1] = new SqlParameter("@ParamName", argParamName);

            DataSetToFill = da.FillDataSet("SP_GetCompanyAddInfo4ID", param);
            return DataSetToFill;
        }

        private DataSet GetCompanyAddInfoExists(string argCompanyCode, string argParamName, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[1] = new SqlParameter("@ParamName", argParamName);

            DataSetToFill = da.NFillDataSet("SP_GetCompanyAddInfo4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCompanyAddInfo(string argCompanyCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
         
            DataSetToFill = da.FillDataSet("SP_GetCompanyAddInfo",param);
            return DataSetToFill;
        }
        
        private CompanyAddInfo objCreateCompanyAddInfo(DataRow dr)
        {
            CompanyAddInfo tCompanyAddInfo = new CompanyAddInfo();
            tCompanyAddInfo.SetObjectInfo(dr);
            return tCompanyAddInfo;
        }

        public void SaveCompanyAddInfo(CompanyAddInfo argCompanyAddInfo, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCompanyAddInfoExists(argCompanyAddInfo.CompanyCode, argCompanyAddInfo.ParamName, da) == false)
                {
                    InsertCompanyAddInfo(argCompanyAddInfo, da, lstErr);
                }
                else
                {
                    UpdateCompanyAddInfo(argCompanyAddInfo, da, lstErr);
                }
            }
            catch(Exception ex)
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

        public ICollection<ErrorHandler> SaveCompanyAddInfo(CompanyAddInfo argCompanyAddInfo)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsCompanyAddInfoExists(argCompanyAddInfo.CompanyCode, argCompanyAddInfo.ParamName, da) == false)
                {
                    InsertCompanyAddInfo(argCompanyAddInfo, da, lstErr);
                }
                else
                {
                    UpdateCompanyAddInfo(argCompanyAddInfo, da, lstErr);
                }

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
        
        public void InsertCompanyAddInfo(CompanyAddInfo argCompanyAddInfo, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@CompanyCode", argCompanyAddInfo.CompanyCode);
            param[1] = new SqlParameter("@ParamName", argCompanyAddInfo.ParamName);
            param[2] = new SqlParameter("@ParamValue", argCompanyAddInfo.ParamValue);

            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertCompanyAddInfo", param);

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
        
        public void UpdateCompanyAddInfo(CompanyAddInfo argCompanyAddInfo, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@CompanyCode", argCompanyAddInfo.CompanyCode);
            param[1] = new SqlParameter("@ParamName", argCompanyAddInfo.ParamName);
            param[2] = new SqlParameter("@ParamValue", argCompanyAddInfo.ParamValue);

            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateCompanyAddInfo", param);

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
        
        public ICollection<ErrorHandler> DeleteCompanyAddInfo(string argCompanyCode, string argParamName)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
                param[1] = new SqlParameter("@ParamName", argParamName);

                param[2] = new SqlParameter("@Type", SqlDbType.Char);
                param[2].Size = 1;
                param[2].Direction = ParameterDirection.Output;
                param[3] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[3].Size = 255;
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[4].Size = 20;
                param[4].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteCompanyAddInfo", param);

                string strMessage = Convert.ToString(param[3].Value);
                string strType = Convert.ToString(param[2].Value);
                string strRetValue = Convert.ToString(param[4].Value);

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
        
        public bool blnIsCompanyAddInfoExists(string argCompanyCode, string argParamName, DataAccess da)
        {
            bool IsCompanyAddInfoExists = false;
            DataSet ds = new DataSet();
            ds = GetCompanyAddInfoExists(argCompanyCode, argParamName, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCompanyAddInfoExists = true;
            }
            else
            {
                IsCompanyAddInfoExists = false;
            }
            return IsCompanyAddInfoExists;
        }
    }
}