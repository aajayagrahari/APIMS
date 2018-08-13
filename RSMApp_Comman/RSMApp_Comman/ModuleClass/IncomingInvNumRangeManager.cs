
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
    public class IncomingInvNumRangeManager
    {
        const string IncomingInvNumRangeTable = "IncomingInvNumRange";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public IncomingInvNumRange objGetIncomingInvNumRange(string argIncomingInvDocTypeCode, string argCompanyCode, string argNumRangeCode, string argFiscalYearType, string argClientCode)
        {
            IncomingInvNumRange argIncomingInvNumRange = new IncomingInvNumRange();
            DataSet DataSetToFill = new DataSet();

            if (argIncomingInvDocTypeCode.Trim() == "")
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

            DataSetToFill = this.GetIncomingInvNumRange(argIncomingInvDocTypeCode, argCompanyCode, argNumRangeCode, argFiscalYearType, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argIncomingInvNumRange = this.objCreateIncomingInvNumRange((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argIncomingInvNumRange;
        }

        public ICollection<IncomingInvNumRange> colGetIncomingInvNumRange(string argClientCode)
        {
            List<IncomingInvNumRange> lst = new List<IncomingInvNumRange>();
            DataSet DataSetToFill = new DataSet();
            IncomingInvNumRange tIncomingInvNumRange = new IncomingInvNumRange();

            DataSetToFill = this.GetIncomingInvNumRange(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateIncomingInvNumRange(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetIncomingInvNumRange(string argIncomingInvDocTypeCode, string argCompanyCode, string argNumRangeCode, string argFiscalYearType, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@IncomingInvDocTypeCode", argIncomingInvDocTypeCode);
            param[1] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[2] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[3] = new SqlParameter("@FiscalYearType", argFiscalYearType);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetIncomingInvNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetIncomingInvNumRange(string argIncomingInvDocTypeCode, string argCompanyCode, string argNumRangeCode, string argFiscalYearType, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@IncomingInvDocTypeCode", argIncomingInvDocTypeCode);
            param[1] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[2] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[3] = new SqlParameter("@FiscalYearType", argFiscalYearType);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetIncomingInvNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetIncomingInvNumRange(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetIncomingInvNumRange", param);

            return DataSetToFill;
        }

        private IncomingInvNumRange objCreateIncomingInvNumRange(DataRow dr)
        {
            IncomingInvNumRange tIncomingInvNumRange = new IncomingInvNumRange();

            tIncomingInvNumRange.SetObjectInfo(dr);

            return tIncomingInvNumRange;

        }

        public ICollection<ErrorHandler> SaveIncomingInvNumRange(IncomingInvNumRange argIncomingInvNumRange)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsIncomingInvNumRangeExists(argIncomingInvNumRange.IncomingInvDocTypeCode, argIncomingInvNumRange.CompanyCode, argIncomingInvNumRange.NumRangeCode, argIncomingInvNumRange.FiscalYearType, argIncomingInvNumRange.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertIncomingInvNumRange(argIncomingInvNumRange, da, lstErr);
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
                    UpdateIncomingInvNumRange(argIncomingInvNumRange, da, lstErr);
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

        public void SaveIncomingInvNumRange(IncomingInvNumRange argIncomingInvNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsIncomingInvNumRangeExists(argIncomingInvNumRange.IncomingInvDocTypeCode, argIncomingInvNumRange.CompanyCode, argIncomingInvNumRange.NumRangeCode, argIncomingInvNumRange.FiscalYearType, argIncomingInvNumRange.ClientCode, da) == false)
                {
                    InsertIncomingInvNumRange(argIncomingInvNumRange, da, lstErr);
                }
                else
                {
                    UpdateIncomingInvNumRange(argIncomingInvNumRange, da, lstErr);
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
            IncomingInvNumRange ObjIncomingInvNumRange = null;
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
                        ObjIncomingInvNumRange = new IncomingInvNumRange();

                        ObjIncomingInvNumRange.IncomingInvDocTypeCode = Convert.ToString(drExcel["IncomingInvDocTypeCode"]).Trim();
                        ObjIncomingInvNumRange.CompanyCode = Convert.ToString(drExcel["CompanyCode"]).Trim();
                        ObjIncomingInvNumRange.NumRangeCode = Convert.ToString(drExcel["NumRangeCode"]).Trim();
                        ObjIncomingInvNumRange.FiscalYearType = Convert.ToString(drExcel["FiscalYearType"]).Trim();
                        ObjIncomingInvNumRange.CreatedBy = Convert.ToString(argUserName);
                        ObjIncomingInvNumRange.ModifiedBy = Convert.ToString(argUserName);
                        ObjIncomingInvNumRange.ClientCode = Convert.ToString(argClientCode);

                        SaveIncomingInvNumRange(ObjIncomingInvNumRange, da, lstErr);

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

        public void InsertIncomingInvNumRange(IncomingInvNumRange argIncomingInvNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@IncomingInvDocTypeCode", argIncomingInvNumRange.IncomingInvDocTypeCode);
            param[1] = new SqlParameter("@CompanyCode", argIncomingInvNumRange.CompanyCode);
            param[2] = new SqlParameter("@FiscalYearType", argIncomingInvNumRange.FiscalYearType);
            param[3] = new SqlParameter("@NumRangeCode", argIncomingInvNumRange.NumRangeCode);
            param[4] = new SqlParameter("@ClientCode", argIncomingInvNumRange.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argIncomingInvNumRange.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argIncomingInvNumRange.ModifiedBy);


            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertIncomingInvNumRange", param);


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

        public void UpdateIncomingInvNumRange(IncomingInvNumRange argIncomingInvNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@IncomingInvDocTypeCode", argIncomingInvNumRange.IncomingInvDocTypeCode);
            param[1] = new SqlParameter("@CompanyCode", argIncomingInvNumRange.CompanyCode);
            param[2] = new SqlParameter("@FiscalYearType", argIncomingInvNumRange.FiscalYearType);
            param[3] = new SqlParameter("@NumRangeCode", argIncomingInvNumRange.NumRangeCode);
            param[4] = new SqlParameter("@ClientCode", argIncomingInvNumRange.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argIncomingInvNumRange.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argIncomingInvNumRange.ModifiedBy);


            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateIncomingInvNumRange", param);


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

        public ICollection<ErrorHandler> DeleteIncomingInvNumRange(string argIncomingInvDocTypeCode, string argCompanyCode, string argNumRangeCode, string argFiscalYearType, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@IncomingInvDocTypeCode", argIncomingInvDocTypeCode);
                param[1] = new SqlParameter("@CompanyCode", argCompanyCode);
                param[2] = new SqlParameter("@NumRangeCode", argNumRangeCode);
                param[3] = new SqlParameter("@FiscalYearType", argFiscalYearType);
                param[4] = new SqlParameter("@ClientCode", argClientCode);
                param[5] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[6] = new SqlParameter("@Type", SqlDbType.Char);
                param[6].Size = 1;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[7].Size = 255;
                param[7].Direction = ParameterDirection.Output;

                param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[8].Size = 20;
                param[8].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteIncomingInvNumRange", param);

                string strMessage = Convert.ToString(param[7].Value);
                string strType = Convert.ToString(param[6].Value);
                string strRetValue = Convert.ToString(param[8].Value);

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

        public bool blnIsIncomingInvNumRangeExists(string argIncomingInvDocTypeCode, string argCompanyCode, string argNumRangeCode, string argFiscalYearType, string argClientCode)
        {
            bool IsIncomingInvNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetIncomingInvNumRange(argIncomingInvDocTypeCode, argCompanyCode, argNumRangeCode, argFiscalYearType, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsIncomingInvNumRangeExists = true;
            }
            else
            {
                IsIncomingInvNumRangeExists = false;
            }
            return IsIncomingInvNumRangeExists;
        }

        public bool blnIsIncomingInvNumRangeExists(string argIncomingInvDocTypeCode, string argCompanyCode, string argNumRangeCode, string argFiscalYearType, string argClientCode, DataAccess da)
        {
            bool IsIncomingInvNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetIncomingInvNumRange(argIncomingInvDocTypeCode, argCompanyCode, argNumRangeCode, argFiscalYearType, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsIncomingInvNumRangeExists = true;
            }
            else
            {
                IsIncomingInvNumRangeExists = false;
            }
            return IsIncomingInvNumRangeExists;
        }
    }
}