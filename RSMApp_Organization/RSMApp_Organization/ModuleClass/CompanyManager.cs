
//Created On :: 07, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using Telerik.Web.UI;

namespace RSMApp_Organization
{
    public class CompanyManager
    {
        const string CompanyTable = "Company";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        CompanyAddInfoManager objCompanyAddInfoManager = new CompanyAddInfoManager(); 

        public Company objGetCompany(string argCompanyCode, string argClientCode)
        {
            Company argCompany = new Company();
            DataSet DataSetToFill = new DataSet();

            if (argCompanyCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetCompany(argCompanyCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argCompany = this.objCreateCompany((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argCompany;
        }
        
        public ICollection<Company> colGetCompany(string argClientCode)
        {
            List<Company> lst = new List<Company>();
            DataSet DataSetToFill = new DataSet();
            Company tCompany = new Company();
            DataSetToFill = this.GetCompany(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCompany(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }
        
        public DataSet GetCompany(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + CompanyTable.ToString();

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

        public DataSet GetCompany(string argCompanyCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            
            DataSetToFill = da.FillDataSet("SP_GetCompany4ID", param);
            return DataSetToFill;
        }
        
        public DataSet GetCompanyExists(string argCompanyCode, string argClientCode,  DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCompany4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCompany(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCompany", param);
            return DataSetToFill;
        }
        
        private Company objCreateCompany(DataRow dr)
        {
            Company tCompany = new Company();
            tCompany.SetObjectInfo(dr);
            return tCompany;
        }
        
        public ICollection<ErrorHandler> SaveCompany(Company argCompany, ICollection<CompanyAddInfo> colCompanyAddInfo)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            string strretValue = "";
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsCompanyExists(argCompany.CompanyCode, argCompany.ClientCode , da) == false)
                {
                   strretValue = InsertCompany(argCompany, da, lstErr);
                }
                else
                {
                    strretValue = UpdateCompany(argCompany, da, lstErr);
                    
                }

                foreach (ErrorHandler objerr in lstErr)
                {
                   if (objerr.Type == "E")
                   {
                        da.ROLLBACK_TRANSACTION();
                        return lstErr;
                   }
                }

                if (strretValue == argCompany.CompanyCode)
                {

                    if (colCompanyAddInfo.Count > 0)
                    {
                        foreach (CompanyAddInfo argCompanyAddInfo in colCompanyAddInfo)
                        {
                            objCompanyAddInfoManager.SaveCompanyAddInfo(argCompanyAddInfo, da, lstErr);
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
        
        public string InsertCompany(Company argCompany, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@ClientCode", argCompany.ClientCode);
            param[1] = new SqlParameter("@CompanyCode", argCompany.CompanyCode);
            param[2] = new SqlParameter("@CompanyName", argCompany.CompanyName);
            param[3] = new SqlParameter("@Address1", argCompany.Address1);
            param[4] = new SqlParameter("@Address2", argCompany.Address2);
            param[5] = new SqlParameter("@CountryCode", argCompany.CountryCode);
            param[6] = new SqlParameter("@CurrencyCode", argCompany.CurrencyCode);
            param[7] = new SqlParameter("@LanguageCode", argCompany.LanguageCode);
            param[8] = new SqlParameter("@ChartACCode", argCompany.ChartACCode);
            param[9] = new SqlParameter("@CrContAreaCode", argCompany.CrContAreaCode);
            param[10] = new SqlParameter("@FiscalYearType", argCompany.FiscalYearType);
            param[11] = new SqlParameter("@PostingPeriodType", argCompany.PostingPeriodType);
            param[12] = new SqlParameter("@VatRegistrationNo", argCompany.VatRegistrationNo);
            param[13] = new SqlParameter("@CreatedBy", argCompany.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argCompany.ModifiedBy);
         
            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertCompany", param);

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

            return strRetValue;
        }
        
        public string UpdateCompany(Company argCompany, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@ClientCode", argCompany.ClientCode);
            param[1] = new SqlParameter("@CompanyCode", argCompany.CompanyCode);
            param[2] = new SqlParameter("@CompanyName", argCompany.CompanyName);
            param[3] = new SqlParameter("@Address1", argCompany.Address1);
            param[4] = new SqlParameter("@Address2", argCompany.Address2);
            param[5] = new SqlParameter("@CountryCode", argCompany.CountryCode);
            param[6] = new SqlParameter("@CurrencyCode", argCompany.CurrencyCode);
            param[7] = new SqlParameter("@LanguageCode", argCompany.LanguageCode);
            param[8] = new SqlParameter("@ChartACCode", argCompany.ChartACCode);
            param[9] = new SqlParameter("@CrContAreaCode", argCompany.CrContAreaCode);
            param[10] = new SqlParameter("@FiscalYearType", argCompany.FiscalYearType);
            param[11] = new SqlParameter("@PostingPeriodType", argCompany.PostingPeriodType);
            param[12] = new SqlParameter("@VatRegistrationNo", argCompany.VatRegistrationNo);
            param[13] = new SqlParameter("@CreatedBy", argCompany.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argCompany.ModifiedBy);
            
            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCompany", param);
            
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

            return strRetValue;
        }
        
        public ICollection<ErrorHandler> DeleteCompany(string argCompanyCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
                param[1] = new SqlParameter("@IsDeleted", iIsDeleted);
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

                int i = da.ExecuteNonQuery("Proc_DeleteCompany", param);

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
        
        public bool blnIsCompanyExists(string argCompanyCode, string argClientCode, DataAccess da)
        {
            bool IsCompanyExists = false;
            DataSet ds = new DataSet();
            ds = GetCompanyExists(argCompanyCode, argClientCode, da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCompanyExists = true;
            }
            else
            {
                IsCompanyExists = false;
            }
            return IsCompanyExists;
        }
    }
}