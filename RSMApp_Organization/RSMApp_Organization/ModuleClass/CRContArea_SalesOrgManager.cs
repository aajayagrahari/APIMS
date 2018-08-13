
//Created On :: 14, May, 2012
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
    public class CRContArea_SalesOrgManager
    {
        const string CRContArea_SalesOrgTable = "CRContArea_SalesOrg";
        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public CRContArea_SalesOrg objGetCRContArea_SalesOrg(string argCrContAreaCode, string argSalesOrganizationCode, string argClientCode)
        {
            CRContArea_SalesOrg argCRContArea_SalesOrg = new CRContArea_SalesOrg();
            DataSet DataSetToFill = new DataSet();

            if (argCrContAreaCode.Trim() == "")
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
            DataSetToFill = this.GetCRContArea_SalesOrg(argCrContAreaCode, argSalesOrganizationCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argCRContArea_SalesOrg = this.objCreateCRContArea_SalesOrg((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argCRContArea_SalesOrg;
        }

        public ICollection<CRContArea_SalesOrg> colGetCRContArea_SalesOrg(string argClientCode)
        {
            List<CRContArea_SalesOrg> lst = new List<CRContArea_SalesOrg>();
            DataSet DataSetToFill = new DataSet();
            CRContArea_SalesOrg tCRContArea_SalesOrg = new CRContArea_SalesOrg();
            DataSetToFill = this.GetCRContArea_SalesOrg(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCRContArea_SalesOrg(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<CRContArea_SalesOrg> colGetCRContArea_SalesOrg(DataTable dt, string argUserName, string clientCode)
        {
            List<CRContArea_SalesOrg> lst = new List<CRContArea_SalesOrg>();
            CRContArea_SalesOrg objCRContArea_SalesOrg = null;
            foreach (DataRow dr in dt.Rows)
            {
                objCRContArea_SalesOrg = new CRContArea_SalesOrg();
                objCRContArea_SalesOrg.CrContAreaCode = Convert.ToString(dr["CrContAreaCode"]).Trim();
                objCRContArea_SalesOrg.CurrencyCode = Convert.ToString(dr["CurrencyCode"]).Trim();
                objCRContArea_SalesOrg.CRUpdateCode = Convert.ToString(dr["CRUpdateCode"]).Trim();
                objCRContArea_SalesOrg.FiscalYearType = Convert.ToString(dr["FiscalYearType"]).Trim();
                objCRContArea_SalesOrg.RiskCategoryCode = Convert.ToString(dr["RiskCategoryCode"]).Trim();
                objCRContArea_SalesOrg.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]).Trim();
                objCRContArea_SalesOrg.CreditLimit = Convert.ToInt32(dr["CreditLimit"]);
                objCRContArea_SalesOrg.CreatedBy = Convert.ToString(argUserName);
                objCRContArea_SalesOrg.ModifiedBy = Convert.ToString(argUserName);
                objCRContArea_SalesOrg.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objCRContArea_SalesOrg);
            }
            return lst;
        }

        public DataSet GetCRContArea_SalesOrg(string argCrContAreaCode, string argSalesOrganizationCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CrContAreaCode", argCrContAreaCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCRContArea_SalesOrg4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCRContArea_SalesOrg(string argCrContAreaCode, string argSalesOrganizationCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CrContAreaCode", argCrContAreaCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCRContArea_SalesOrg4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCRContArea_SalesOrg(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCRContArea_SalesOrg", param);
            return DataSetToFill;
        }

        private CRContArea_SalesOrg objCreateCRContArea_SalesOrg(DataRow dr)
        {
            CRContArea_SalesOrg tCRContArea_SalesOrg = new CRContArea_SalesOrg();
            tCRContArea_SalesOrg.SetObjectInfo(dr);
            return tCRContArea_SalesOrg;
        }

        public ICollection<ErrorHandler> SaveCRContArea_SalesOrg(CRContArea_SalesOrg argCRContArea_SalesOrg)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCRContArea_SalesOrgExists(argCRContArea_SalesOrg.CrContAreaCode, argCRContArea_SalesOrg.SalesOrganizationCode, argCRContArea_SalesOrg.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCRContArea_SalesOrg(argCRContArea_SalesOrg, da, lstErr);
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
                    UpdateCRContArea_SalesOrg(argCRContArea_SalesOrg, da, lstErr);
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
        public ICollection<ErrorHandler> SaveCRContArea_SalesOrg(ICollection<CRContArea_SalesOrg> colGetCRContArea_SalesOrg, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (CRContArea_SalesOrg argCRContArea_SalesOrg in colGetCRContArea_SalesOrg)
                {
                    if (argCRContArea_SalesOrg.IsDeleted == 0)
                    {
                        if (blnIsCRContArea_SalesOrgExists(argCRContArea_SalesOrg.CrContAreaCode, argCRContArea_SalesOrg.SalesOrganizationCode, argCRContArea_SalesOrg.ClientCode, da) == false)
                        {
                            InsertCRContArea_SalesOrg(argCRContArea_SalesOrg, da, lstErr);
                        }
                        else
                        {
                            UpdateCRContArea_SalesOrg(argCRContArea_SalesOrg, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteCRContArea_SalesOrg(argCRContArea_SalesOrg.CrContAreaCode, argCRContArea_SalesOrg.SalesOrganizationCode, argCRContArea_SalesOrg.ClientCode, argCRContArea_SalesOrg.IsDeleted);
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
                    SaveCRContArea_SalesOrg(colGetCRContArea_SalesOrg(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertCRContArea_SalesOrg(CRContArea_SalesOrg argCRContArea_SalesOrg, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@CrContAreaCode", argCRContArea_SalesOrg.CrContAreaCode);
            param[1] = new SqlParameter("@CurrencyCode", argCRContArea_SalesOrg.CurrencyCode);
            param[2] = new SqlParameter("@CRUpdateCode", argCRContArea_SalesOrg.CRUpdateCode);
            param[3] = new SqlParameter("@FiscalYearType", argCRContArea_SalesOrg.FiscalYearType);
            param[4] = new SqlParameter("@RiskCategoryCode", argCRContArea_SalesOrg.RiskCategoryCode);
            param[5] = new SqlParameter("@CreditLimit", argCRContArea_SalesOrg.CreditLimit);
            param[6] = new SqlParameter("@SalesOrganizationCode", argCRContArea_SalesOrg.SalesOrganizationCode);
            param[7] = new SqlParameter("@ClientCode", argCRContArea_SalesOrg.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argCRContArea_SalesOrg.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argCRContArea_SalesOrg.ModifiedBy);
            
            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCRContArea_SalesOrg", param);

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
            lstErr.Add(objErrorHandler);
        }

        public void UpdateCRContArea_SalesOrg(CRContArea_SalesOrg argCRContArea_SalesOrg, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@CrContAreaCode", argCRContArea_SalesOrg.CrContAreaCode);
            param[1] = new SqlParameter("@CurrencyCode", argCRContArea_SalesOrg.CurrencyCode);
            param[2] = new SqlParameter("@CRUpdateCode", argCRContArea_SalesOrg.CRUpdateCode);
            param[3] = new SqlParameter("@FiscalYearType", argCRContArea_SalesOrg.FiscalYearType);
            param[4] = new SqlParameter("@RiskCategoryCode", argCRContArea_SalesOrg.RiskCategoryCode);
            param[5] = new SqlParameter("@CreditLimit", argCRContArea_SalesOrg.CreditLimit);
            param[6] = new SqlParameter("@SalesOrganizationCode", argCRContArea_SalesOrg.SalesOrganizationCode);
            param[7] = new SqlParameter("@ClientCode", argCRContArea_SalesOrg.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argCRContArea_SalesOrg.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argCRContArea_SalesOrg.ModifiedBy);
            
            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCRContArea_SalesOrg", param);

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
            lstErr.Add(objErrorHandler);
        }

        public ICollection<ErrorHandler> DeleteCRContArea_SalesOrg(string argCrContAreaCode, string argSalesOrganizationCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@CrContAreaCode", argCrContAreaCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteCRContArea_SalesOrg", param);

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

        public bool blnIsCRContArea_SalesOrgExists(string argCrContAreaCode, string argSalesOrganizationCode, string argClientCode)
        {
            bool IsCRContArea_SalesOrgExists = false;
            DataSet ds = new DataSet();
            ds = GetCRContArea_SalesOrg(argCrContAreaCode, argSalesOrganizationCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCRContArea_SalesOrgExists = true;
            }
            else
            {
                IsCRContArea_SalesOrgExists = false;
            }
            return IsCRContArea_SalesOrgExists;
        }

        public bool blnIsCRContArea_SalesOrgExists(string argCrContAreaCode, string argSalesOrganizationCode, string argClientCode, DataAccess da)
        {
            bool IsCRContArea_SalesOrgExists = false;
            DataSet ds = new DataSet();
            ds = GetCRContArea_SalesOrg(argCrContAreaCode, argSalesOrganizationCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCRContArea_SalesOrgExists = true;
            }
            else
            {
                IsCRContArea_SalesOrgExists = false;
            }
            return IsCRContArea_SalesOrgExists;
        }
    }
}