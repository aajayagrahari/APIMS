
//Created On :: 15, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_Comman
{
    public class AccountNumRangeManager
    {
        const string AccountNumRangeTable = "AccountNumRange";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public AccountNumRange objGetAccountNumRange(string argAccountDocTypeCode, string argCompanyCode, string argNumRangeCode, string argClientCode)
        {
            AccountNumRange argAccountNumRange = new AccountNumRange();
            DataSet DataSetToFill = new DataSet();

            if (argAccountDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argCompanyCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argNumRangeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetAccountNumRange(argAccountDocTypeCode, argCompanyCode, argNumRangeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argAccountNumRange = this.objCreateAccountNumRange((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argAccountNumRange;
        }

        public ICollection<AccountNumRange> colGetAccountNumRange(string argAccountDocTypeCode, string argClientCode)
        {
            List<AccountNumRange> lst = new List<AccountNumRange>();
            DataSet DataSetToFill = new DataSet();
            AccountNumRange tAccountNumRange = new AccountNumRange();

            DataSetToFill = this.GetAccountNumRange(argAccountDocTypeCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateAccountNumRange(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetAccountNumRange(string argAccountDocTypeCode, string argCompanyCode, string argNumRangeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@AccountDocTypeCode", argAccountDocTypeCode);
            param[1] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[2] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAccountNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetAccountNumRange(string argAccountDocTypeCode, string argCompanyCode, string argNumRangeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@AccountDocTypeCode", argAccountDocTypeCode);
            param[1] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[2] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetAccountNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetAccountNumRange(string argAccountDocTypeCode , string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AccountDocTypeCode", argAccountDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAccountNumRange", param);

            return DataSetToFill;
        }

        private AccountNumRange objCreateAccountNumRange(DataRow dr)
        {
            AccountNumRange tAccountNumRange = new AccountNumRange();

            tAccountNumRange.SetObjectInfo(dr);

            return tAccountNumRange;

        }

        public ICollection<ErrorHandler> SaveAccountNumRange(AccountNumRange argAccountNumRange)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsAccountNumRangeExists(argAccountNumRange.AccountDocTypeCode, argAccountNumRange.CompanyCode, argAccountNumRange.NumRangeCode, argAccountNumRange.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertAccountNumRange(argAccountNumRange, da, lstErr);
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
                    UpdateAccountNumRange(argAccountNumRange, da, lstErr);
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

        public void SaveAccountNumRange(AccountNumRange argAccountNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsAccountNumRangeExists(argAccountNumRange.AccountDocTypeCode, argAccountNumRange.CompanyCode, argAccountNumRange.NumRangeCode, argAccountNumRange.ClientCode, da) == false)
                {
                    InsertAccountNumRange(argAccountNumRange, da, lstErr);
                }
                else
                {
                    UpdateAccountNumRange(argAccountNumRange, da, lstErr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Bulk Insert
        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
            AccountNumRange ObjAccountNumRange = null;
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
                    foreach (DataRow drExcel in dtExcel.Rows)
                    {
                        ObjAccountNumRange = new AccountNumRange();

                        ObjAccountNumRange.AccountDocTypeCode = Convert.ToString(drExcel["AccountDocTypeCode"]).Trim();
                        ObjAccountNumRange.CompanyCode = Convert.ToString(drExcel["CompanyCode"]).Trim();
                        ObjAccountNumRange.NumRangeCode = Convert.ToString(drExcel["NumRangeCode"]).Trim();
                        ObjAccountNumRange.FiscalYearType = Convert.ToString(drExcel["FiscalYearType"]).Trim();
                        ObjAccountNumRange.CreatedBy = Convert.ToString(argUserName);
                        ObjAccountNumRange.ModifiedBy = Convert.ToString(argUserName);
                        ObjAccountNumRange.ClientCode = Convert.ToString(argClientCode);

                        SaveAccountNumRange(ObjAccountNumRange, da, lstErr);

                        foreach (ErrorHandler objerr in lstErr)
                        {
                            if (objerr.Type == "E")
                            {
                                da.ROLLBACK_TRANSACTION();
                                break;
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
        #endregion

        public void InsertAccountNumRange(AccountNumRange argAccountNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@AccountDocTypeCode", argAccountNumRange.AccountDocTypeCode);
            param[1] = new SqlParameter("@CompanyCode", argAccountNumRange.CompanyCode);
            param[2] = new SqlParameter("@FiscalYearType", argAccountNumRange.FiscalYearType);
            param[3] = new SqlParameter("@NumRangeCode", argAccountNumRange.NumRangeCode);
            param[4] = new SqlParameter("@ClientCode", argAccountNumRange.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argAccountNumRange.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argAccountNumRange.ModifiedBy);

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertAccountNumRange", param);


            string strMessage = Convert.ToString(param[8].Value);
            string strType = Convert.ToString(param[7].Value);
            string strRetValue = Convert.ToString(param[9].Value);


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

        public void UpdateAccountNumRange(AccountNumRange argAccountNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@AccountDocTypeCode", argAccountNumRange.AccountDocTypeCode);
            param[1] = new SqlParameter("@CompanyCode", argAccountNumRange.CompanyCode);
            param[2] = new SqlParameter("@FiscalYearType", argAccountNumRange.FiscalYearType);
            param[3] = new SqlParameter("@NumRangeCode", argAccountNumRange.NumRangeCode);
            param[4] = new SqlParameter("@ClientCode", argAccountNumRange.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argAccountNumRange.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argAccountNumRange.ModifiedBy);

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateAccountNumRange", param);


            string strMessage = Convert.ToString(param[8].Value);
            string strType = Convert.ToString(param[7].Value);
            string strRetValue = Convert.ToString(param[9].Value);


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

        public ICollection<ErrorHandler> DeleteAccountNumRange(string argAccountDocTypeCode, string argCompanyCode, string argNumRangeCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@AccountDocTypeCode", argAccountDocTypeCode);
                param[1] = new SqlParameter("@CompanyCode", argCompanyCode);
                param[2] = new SqlParameter("@NumRangeCode", argNumRangeCode);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteAccountNumRange", param);


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

        public bool blnIsAccountNumRangeExists(string argAccountDocTypeCode, string argCompanyCode, string argNumRangeCode, string argClientCode)
        {
            bool IsAccountNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetAccountNumRange(argAccountDocTypeCode, argCompanyCode, argNumRangeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAccountNumRangeExists = true;
            }
            else
            {
                IsAccountNumRangeExists = false;
            }
            return IsAccountNumRangeExists;
        }

        public bool blnIsAccountNumRangeExists(string argAccountDocTypeCode, string argCompanyCode, string argNumRangeCode, string argClientCode, DataAccess da)
        {
            bool IsAccountNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetAccountNumRange(argAccountDocTypeCode, argCompanyCode, argNumRangeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAccountNumRangeExists = true;
            }
            else
            {
                IsAccountNumRangeExists = false;
            }
            return IsAccountNumRangeExists;
        }
    }
}