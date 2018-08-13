
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
    public class BillingNumRangeManager
    {
        const string BillingNumRangeTable = "BillingNumRange";

       //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public BillingNumRange objGetBillingNumRange(string argBillingDocTypeCode, string argSalesofficeCode, string argNumRangeCode, string argClientCode)
        {
            BillingNumRange argBillingNumRange = new BillingNumRange();
            DataSet DataSetToFill = new DataSet();

            if (argBillingDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argSalesofficeCode.Trim() == "")
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

            DataSetToFill = this.GetBillingNumRange(argBillingDocTypeCode, argSalesofficeCode, argNumRangeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argBillingNumRange = this.objCreateBillingNumRange((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argBillingNumRange;
        }

        public ICollection<BillingNumRange> colGetBillingNumRange(string argBillingDocTypeCode, string argClientCode)
        {
            List<BillingNumRange> lst = new List<BillingNumRange>();
            DataSet DataSetToFill = new DataSet();
            BillingNumRange tBillingNumRange = new BillingNumRange();

            DataSetToFill = this.GetBillingNumRange(argBillingDocTypeCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateBillingNumRange(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetBillingNumRange(string argBillingDocTypeCode, string argSalesofficeCode, string argNumRangeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@BillingDocTypeCode", argBillingDocTypeCode);
            param[1] = new SqlParameter("@SalesofficeCode", argSalesofficeCode);
            param[2] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBillingNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetBillingNumRange(string argBillingDocTypeCode, string argSalesofficeCode, string argNumRangeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@BillingDocTypeCode", argBillingDocTypeCode);
            param[1] = new SqlParameter("@SalesofficeCode", argSalesofficeCode);
            param[2] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetBillingNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetBillingNumRange(string argBillingDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@BillingDocTypeCode", argBillingDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBillingNumRange", param);

            return DataSetToFill;
        }

        private BillingNumRange objCreateBillingNumRange(DataRow dr)
        {
            BillingNumRange tBillingNumRange = new BillingNumRange();

            tBillingNumRange.SetObjectInfo(dr);

            return tBillingNumRange;

        }

        public ICollection<ErrorHandler> SaveBillingNumRange(BillingNumRange argBillingNumRange)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsBillingNumRangeExists(argBillingNumRange.BillingDocTypeCode, argBillingNumRange.SalesofficeCode, argBillingNumRange.NumRangeCode, argBillingNumRange.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertBillingNumRange(argBillingNumRange, da, lstErr);
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
                    UpdateBillingNumRange(argBillingNumRange, da, lstErr);
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

        public void SaveBillingNumRange(BillingNumRange argBillingNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsBillingNumRangeExists(argBillingNumRange.BillingDocTypeCode,argBillingNumRange.SalesofficeCode, argBillingNumRange.NumRangeCode, argBillingNumRange.ClientCode, da) == false)
                {
                    InsertBillingNumRange(argBillingNumRange, da, lstErr);
                }
                else
                {
                    UpdateBillingNumRange(argBillingNumRange, da, lstErr);
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
            BillingNumRange ObjBillingNumRange = null;
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
                        ObjBillingNumRange = new BillingNumRange();

                        ObjBillingNumRange.BillingDocTypeCode = Convert.ToString(drExcel["BillingDocTypeCode"]).Trim();
                        ObjBillingNumRange.SalesofficeCode = Convert.ToString(drExcel["SalesofficeCode"]).Trim();
                        ObjBillingNumRange.NumRangeCode = Convert.ToString(drExcel["NumRangeCode"]).Trim();
                        ObjBillingNumRange.CompanyCode = Convert.ToString(drExcel["CompanyCode"]).Trim();
                        ObjBillingNumRange.FiscalYearType = Convert.ToString(drExcel["FiscalYearType"]).Trim();
                        ObjBillingNumRange.CreatedBy = Convert.ToString(argUserName);
                        ObjBillingNumRange.ModifiedBy = Convert.ToString(argUserName);
                        ObjBillingNumRange.ClientCode = Convert.ToString(argClientCode);

                        SaveBillingNumRange(ObjBillingNumRange, da, lstErr);

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

        public void InsertBillingNumRange(BillingNumRange argBillingNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@BillingDocTypeCode", argBillingNumRange.BillingDocTypeCode);
            param[1] = new SqlParameter("@SalesofficeCode", argBillingNumRange.SalesofficeCode);
            param[2] = new SqlParameter("@CompanyCode", argBillingNumRange.CompanyCode);
            param[3] = new SqlParameter("@FiscalYearType", argBillingNumRange.FiscalYearType);
            param[4] = new SqlParameter("@NumRangeCode", argBillingNumRange.NumRangeCode);
            param[5] = new SqlParameter("@ClientCode", argBillingNumRange.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argBillingNumRange.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argBillingNumRange.ModifiedBy);
      
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertBillingNumRange", param);


            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
            string strRetValue = Convert.ToString(param[10].Value);


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

        public void UpdateBillingNumRange(BillingNumRange argBillingNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@BillingDocTypeCode", argBillingNumRange.BillingDocTypeCode);
            param[1] = new SqlParameter("@SalesofficeCode", argBillingNumRange.SalesofficeCode);
            param[2] = new SqlParameter("@CompanyCode", argBillingNumRange.CompanyCode);
            param[3] = new SqlParameter("@FiscalYearType", argBillingNumRange.FiscalYearType);
            param[4] = new SqlParameter("@NumRangeCode", argBillingNumRange.NumRangeCode);
            param[5] = new SqlParameter("@ClientCode", argBillingNumRange.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argBillingNumRange.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argBillingNumRange.ModifiedBy);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateBillingNumRange", param);


            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
            string strRetValue = Convert.ToString(param[10].Value);


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

        public ICollection<ErrorHandler> DeleteBillingNumRange(string argBillingDocTypeCode, string argSalesofficeCode, string argNumRangeCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@BillingDocTypeCode", argBillingDocTypeCode);
                param[1] = new SqlParameter("@SalesofficeCode", argSalesofficeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteBillingNumRange", param);


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

        public bool blnIsBillingNumRangeExists(string argBillingDocTypeCode, string argSalesofficeCode, string argNumRangeCode, string argClientCode)
        {
            bool IsBillingNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetBillingNumRange(argBillingDocTypeCode, argSalesofficeCode, argNumRangeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsBillingNumRangeExists = true;
            }
            else
            {
                IsBillingNumRangeExists = false;
            }
            return IsBillingNumRangeExists;
        }

        public bool blnIsBillingNumRangeExists(string argBillingDocTypeCode, string argSalesofficeCode, string argNumRangeCode, string argClientCode, DataAccess da)
        {
            bool IsBillingNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetBillingNumRange(argBillingDocTypeCode, argSalesofficeCode, argNumRangeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsBillingNumRangeExists = true;
            }
            else
            {
                IsBillingNumRangeExists = false;
            }
            return IsBillingNumRangeExists;
        }
    }
}