
//Created On :: 27, September, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_FI
{
    public class ProfitCenter_CompanyManager
    {
        const string ProfitCenter_CompanyTable = "ProfitCenter_Company";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public ProfitCenter_Company objGetProfitCenter_Company(string argProfitCenterCode, string argCompanyCode, string argClientCode)
        {
            ProfitCenter_Company argProfitCenter_Company = new ProfitCenter_Company();
            DataSet DataSetToFill = new DataSet();

            if (argProfitCenterCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argCompanyCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            DataSetToFill = this.GetProfitCenter_Company(argProfitCenterCode, argCompanyCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argProfitCenter_Company = this.objCreateProfitCenter_Company((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandler:

        Finish:
            DataSetToFill = null;


            return argProfitCenter_Company;
        }
        
        public ICollection<ProfitCenter_Company> colGetProfitCenter_Company(string argProfitCenterCode,string argClientCode)
        {
            List<ProfitCenter_Company> lst = new List<ProfitCenter_Company>();
            DataSet DataSetToFill = new DataSet();
            ProfitCenter_Company tProfitCenter_Company = new ProfitCenter_Company();

            DataSetToFill = this.GetProfitCenter_Company(argProfitCenterCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateProfitCenter_Company(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetProfitCenter_Company(string argProfitCenterCode, string argCompanyCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ProfitCenterCode", argProfitCenterCode);
            param[1] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetProfitCenter_Company4ID", param);

            return DataSetToFill;
        }

        public DataSet GetProfitCenter_Company(string argProfitCenterCode, string argCompanyCode, string argClientCode,DataAccess da)
        {
           
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ProfitCenterCode", argProfitCenterCode);
            param[1] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetProfitCenter_Company4ID", param);

            return DataSetToFill;
        }
       

        public DataSet GetProfitCenter_Company(string argProfitCenterCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ProfitCenterCode", argProfitCenterCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetProfitCenter_Company", param);
            return DataSetToFill;
        }
        
        private ProfitCenter_Company objCreateProfitCenter_Company(DataRow dr)
        {
            ProfitCenter_Company tProfitCenter_Company = new ProfitCenter_Company();

            tProfitCenter_Company.SetObjectInfo(dr);

            return tProfitCenter_Company;

        }
        
        public ICollection<ErrorHandler> SaveProfitCenter_Company(ProfitCenter_Company argProfitCenter_Company)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsProfitCenter_CompanyExists(argProfitCenter_Company.ProfitCenterCode, argProfitCenter_Company.CompanyCode, argProfitCenter_Company.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertProfitCenter_Company(argProfitCenter_Company, da, lstErr);
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
                    UpdateProfitCenter_Company(argProfitCenter_Company, da, lstErr);
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

        public ICollection<ErrorHandler> SaveProfitCenter_Company(ICollection<ProfitCenter_Company> colGetProfitCenter_Company)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (ProfitCenter_Company argProfitCenter_Company in colGetProfitCenter_Company)
                {
                    if (argProfitCenter_Company.IsDeleted == 0)
                    {

                        if (blnIsProfitCenter_CompanyExists(argProfitCenter_Company.ProfitCenterCode, argProfitCenter_Company.CompanyCode, argProfitCenter_Company.ClientCode, da) == false)
                        {
                            InsertProfitCenter_Company(argProfitCenter_Company, da, lstErr);
                        }
                        else
                        {
                            UpdateProfitCenter_Company(argProfitCenter_Company, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteProfitCenter_Company(argProfitCenter_Company.ProfitCenterCode, argProfitCenter_Company.CompanyCode, argProfitCenter_Company.ClientCode, argProfitCenter_Company.IsDeleted);
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

        public void InsertProfitCenter_Company(ProfitCenter_Company argProfitCenter_Company, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@ProfitCenterCode", argProfitCenter_Company.ProfitCenterCode);
            param[1] = new SqlParameter("@CompanyCode", argProfitCenter_Company.CompanyCode);
            param[2] = new SqlParameter("@ClientCode", argProfitCenter_Company.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argProfitCenter_Company.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argProfitCenter_Company.ModifiedBy);
         

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertProfitCenter_Company", param);


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
        
        public void UpdateProfitCenter_Company(ProfitCenter_Company argProfitCenter_Company, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@ProfitCenterCode", argProfitCenter_Company.ProfitCenterCode);
            param[1] = new SqlParameter("@CompanyCode", argProfitCenter_Company.CompanyCode);
            param[2] = new SqlParameter("@ClientCode", argProfitCenter_Company.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argProfitCenter_Company.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argProfitCenter_Company.ModifiedBy);


            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateProfitCenter_Company", param);


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

        public ICollection<ErrorHandler> DeleteProfitCenter_Company(string argProfitCenterCode, string argCompanyCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@ProfitCenterCode", argProfitCenterCode);
                param[1] = new SqlParameter("@CompanyCode", argCompanyCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteProfitCenter_Company", param);


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

        public bool blnIsProfitCenter_CompanyExists(string argProfitCenterCode, string argCompanyCode, string argClientCode)
        {
            bool IsProfitCenter_CompanyExists = false;
            DataSet ds = new DataSet();
            ds = GetProfitCenter_Company(argProfitCenterCode, argCompanyCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsProfitCenter_CompanyExists = true;
            }
            else
            {
                IsProfitCenter_CompanyExists = false;
            }
            return IsProfitCenter_CompanyExists;
        }

        public bool blnIsProfitCenter_CompanyExists(string argProfitCenterCode, string argCompanyCode, string argClientCode,DataAccess da)
        {
            bool IsProfitCenter_CompanyExists = false;
            DataSet ds = new DataSet();
            ds = GetProfitCenter_Company(argProfitCenterCode, argCompanyCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsProfitCenter_CompanyExists = true;
            }
            else
            {
                IsProfitCenter_CompanyExists = false;
            }
            return IsProfitCenter_CompanyExists;
        }
    }
}