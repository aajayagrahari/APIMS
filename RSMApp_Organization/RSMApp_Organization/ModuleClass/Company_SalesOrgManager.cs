
//Created On :: 08, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;


namespace RSMApp_Organization
{
    public class Company_SalesOrgManager
    {
        const string Company_SalesOrgTable = "Company_SalesOrg";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public Company_SalesOrg objGetCompany_SalesOrg(string argCompanyCode, string argSalesOrganizationCode, string argClientCode)
        {
            Company_SalesOrg argCompany_SalesOrg = new Company_SalesOrg();
            DataSet DataSetToFill = new DataSet();

            if (argCompanyCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argSalesOrganizationCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCompany_SalesOrg(argCompanyCode, argSalesOrganizationCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCompany_SalesOrg = this.objCreateCompany_SalesOrg((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCompany_SalesOrg;
        }

        public ICollection<Company_SalesOrg> colGetCompany_SalesOrg(DataTable dt, string argUserName, string clientCode)
        {
            List<Company_SalesOrg> lst = new List<Company_SalesOrg>();
            Company_SalesOrg objCompany_SalesOrg = null;
            foreach (DataRow dr in dt.Rows)
            {
                objCompany_SalesOrg = new Company_SalesOrg();
                objCompany_SalesOrg.CompanyCode = Convert.ToString(dr["CompanyCode"]).Trim();
                objCompany_SalesOrg.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]).Trim();
                objCompany_SalesOrg.CreatedBy = Convert.ToString(argUserName);
                objCompany_SalesOrg.ModifiedBy = Convert.ToString(argUserName);
                objCompany_SalesOrg.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objCompany_SalesOrg);
            }
            return lst;
        }
        
        public DataSet GetCompany_SalesOrg(string argCompanyCode, string argSalesOrganizationCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCompany_SalesOrg4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCompany_SalesOrg(string argCompanyCode, string argSalesOrganizationCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCompany_SalesOrg4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCompany_SalesOrg(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetCompany_SalesOrg", param);
            return DataSetToFill;
        }
        
        private Company_SalesOrg objCreateCompany_SalesOrg(DataRow dr)
        {
            Company_SalesOrg tCompany_SalesOrg = new Company_SalesOrg();

            tCompany_SalesOrg.SetObjectInfo(dr);

            return tCompany_SalesOrg;

        }
        
        public ICollection<ErrorHandler> SaveCompany_SalesOrg(Company_SalesOrg argCompany_SalesOrg)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCompany_SalesOrgExists(argCompany_SalesOrg.CompanyCode, argCompany_SalesOrg.SalesOrganizationCode, argCompany_SalesOrg.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCompany_SalesOrg(argCompany_SalesOrg, da, lstErr);
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
                    UpdateCompany_SalesOrg(argCompany_SalesOrg, da, lstErr);
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

        /*************/
        public ICollection<ErrorHandler> SaveCompany_SalesOrg(ICollection<Company_SalesOrg> colGetCompany_SalesOrg, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Company_SalesOrg argCompany_SalesOrg in colGetCompany_SalesOrg)
                {
                    if (argCompany_SalesOrg.IsDeleted == 0)
                    {
                        if (blnIsCompany_SalesOrgExists(argCompany_SalesOrg.CompanyCode, argCompany_SalesOrg.SalesOrganizationCode, argCompany_SalesOrg.ClientCode, da) == false)
                        {
                            InsertCompany_SalesOrg(argCompany_SalesOrg, da, lstErr);
                        }
                        else
                        {
                            UpdateCompany_SalesOrg(argCompany_SalesOrg, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteCompany_SalesOrg(argCompany_SalesOrg.CompanyCode, argCompany_SalesOrg.SalesOrganizationCode, argCompany_SalesOrg.ClientCode, argCompany_SalesOrg.IsDeleted);
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

        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
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
                    SaveCompany_SalesOrg(colGetCompany_SalesOrg(dtExcel, argUserName, argClientCode), lstErr);

                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            break;

                        }
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
        /**********/
        
        public void InsertCompany_SalesOrg(Company_SalesOrg argCompany_SalesOrg, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@CompanyCode", argCompany_SalesOrg.CompanyCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argCompany_SalesOrg.SalesOrganizationCode);
            param[2] = new SqlParameter("@ClientCode", argCompany_SalesOrg.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argCompany_SalesOrg.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argCompany_SalesOrg.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertCompany_SalesOrg", param);


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
            lstErr.Add(objErrorHandler);

        }
        
        public void UpdateCompany_SalesOrg(Company_SalesOrg argCompany_SalesOrg, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@CompanyCode", argCompany_SalesOrg.CompanyCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argCompany_SalesOrg.SalesOrganizationCode);
            param[2] = new SqlParameter("@ClientCode", argCompany_SalesOrg.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argCompany_SalesOrg.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argCompany_SalesOrg.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCompany_SalesOrg", param);


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
            lstErr.Add(objErrorHandler);

        }
        
        public ICollection<ErrorHandler> DeleteCompany_SalesOrg(string argCompanyCode, string argSalesOrganizationCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
                param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteCompany_SalesOrg", param);


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
        
        public bool blnIsCompany_SalesOrgExists(string argCompanyCode, string argSalesOrganizationCode, string argClientCode)
        {
            bool IsCompany_SalesOrgExists = false;
            DataSet ds = new DataSet();
            ds = GetCompany_SalesOrg(argCompanyCode, argSalesOrganizationCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCompany_SalesOrgExists = true;
            }
            else
            {
                IsCompany_SalesOrgExists = false;
            }
            return IsCompany_SalesOrgExists;
        }

        public bool blnIsCompany_SalesOrgExists(string argCompanyCode, string argSalesOrganizationCode, string argClientCode,DataAccess da)
        {
            bool IsCompany_SalesOrgExists = false;
            DataSet ds = new DataSet();
            ds = GetCompany_SalesOrg(argCompanyCode, argSalesOrganizationCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCompany_SalesOrgExists = true;
            }
            else
            {
                IsCompany_SalesOrgExists = false;
            }
            return IsCompany_SalesOrgExists;
        }
    }
}