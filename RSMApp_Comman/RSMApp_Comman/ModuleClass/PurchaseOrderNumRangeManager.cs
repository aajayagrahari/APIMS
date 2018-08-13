
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
    public class PurchaseOrderNumRangeManager
    {
        const string PurchaseOrderNumRangeTable = "PurchaseOrderNumRange";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public PurchaseOrderNumRange objGetPurchaseOrderNumRange(string argPODocTypeCode, string argCompanyCode, string argNumRangeCode, string argClientCode)
        {
            PurchaseOrderNumRange argPurchaseOrderNumRange = new PurchaseOrderNumRange();
            DataSet DataSetToFill = new DataSet();

            if (argPODocTypeCode.Trim() == "")
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

            DataSetToFill = this.GetPurchaseOrderNumRange(argPODocTypeCode, argCompanyCode, argNumRangeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPurchaseOrderNumRange = this.objCreatePurchaseOrderNumRange((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPurchaseOrderNumRange;
        }

        public ICollection<PurchaseOrderNumRange> colGetPurchaseOrderNumRange(string argPODocTypeCode,string argClientCode)
        {
            List<PurchaseOrderNumRange> lst = new List<PurchaseOrderNumRange>();
            DataSet DataSetToFill = new DataSet();
            PurchaseOrderNumRange tPurchaseOrderNumRange = new PurchaseOrderNumRange();

            DataSetToFill = this.GetPurchaseOrderNumRange(argPODocTypeCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePurchaseOrderNumRange(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetPurchaseOrderNumRange(string argPODocTypeCode, string argCompanyCode, string argNumRangeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@PODocTypeCode", argPODocTypeCode);
            param[1] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[2] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPurchaseOrderNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPurchaseOrderNumRange(string argPODocTypeCode, string argCompanyCode, string argNumRangeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@PODocTypeCode", argPODocTypeCode);
            param[1] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[2] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPurchaseOrderNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPurchaseOrderNumRange(string argPODocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@PODocTypeCode", argPODocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPurchaseOrderNumRange", param);

            return DataSetToFill;
        }

        private PurchaseOrderNumRange objCreatePurchaseOrderNumRange(DataRow dr)
        {
            PurchaseOrderNumRange tPurchaseOrderNumRange = new PurchaseOrderNumRange();

            tPurchaseOrderNumRange.SetObjectInfo(dr);

            return tPurchaseOrderNumRange;

        }

        public ICollection<ErrorHandler> SavePurchaseOrderNumRange(PurchaseOrderNumRange argPurchaseOrderNumRange)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPurchaseOrderNumRangeExists(argPurchaseOrderNumRange.PODocTypeCode, argPurchaseOrderNumRange.CompanyCode, argPurchaseOrderNumRange.NumRangeCode, argPurchaseOrderNumRange.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPurchaseOrderNumRange(argPurchaseOrderNumRange, da, lstErr);
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
                    UpdatePurchaseOrderNumRange(argPurchaseOrderNumRange, da, lstErr);
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

        public void SavePurchaseOrderNumRange(PurchaseOrderNumRange argPurchaseOrderNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsPurchaseOrderNumRangeExists(argPurchaseOrderNumRange.PODocTypeCode, argPurchaseOrderNumRange.CompanyCode, argPurchaseOrderNumRange.NumRangeCode, argPurchaseOrderNumRange.ClientCode, da) == false)
                {
                    InsertPurchaseOrderNumRange(argPurchaseOrderNumRange, da, lstErr);
                }
                else
                {
                    UpdatePurchaseOrderNumRange(argPurchaseOrderNumRange, da, lstErr);
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
            PurchaseOrderNumRange ObjPurchaseOrderNumRange = null;
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
                        ObjPurchaseOrderNumRange = new PurchaseOrderNumRange();

                        ObjPurchaseOrderNumRange.PODocTypeCode = Convert.ToString(drExcel["PODocTypeCode"]).Trim();
                        ObjPurchaseOrderNumRange.CompanyCode = Convert.ToString(drExcel["CompanyCode"]).Trim();
                        ObjPurchaseOrderNumRange.NumRangeCode = Convert.ToString(drExcel["NumRangeCode"]).Trim();
                        ObjPurchaseOrderNumRange.CreatedBy = Convert.ToString(argUserName);
                        ObjPurchaseOrderNumRange.ModifiedBy = Convert.ToString(argUserName);
                        ObjPurchaseOrderNumRange.ClientCode = Convert.ToString(argClientCode);

                        SavePurchaseOrderNumRange(ObjPurchaseOrderNumRange, da, lstErr);

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

        public void InsertPurchaseOrderNumRange(PurchaseOrderNumRange argPurchaseOrderNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@PODocTypeCode", argPurchaseOrderNumRange.PODocTypeCode);
            param[1] = new SqlParameter("@CompanyCode", argPurchaseOrderNumRange.CompanyCode);
            param[2] = new SqlParameter("@NumRangeCode", argPurchaseOrderNumRange.NumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argPurchaseOrderNumRange.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argPurchaseOrderNumRange.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argPurchaseOrderNumRange.ModifiedBy);

            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPurchaseOrderNumRange", param);


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

        public void UpdatePurchaseOrderNumRange(PurchaseOrderNumRange argPurchaseOrderNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@PODocTypeCode", argPurchaseOrderNumRange.PODocTypeCode);
            param[1] = new SqlParameter("@CompanyCode", argPurchaseOrderNumRange.CompanyCode);
            param[2] = new SqlParameter("@NumRangeCode", argPurchaseOrderNumRange.NumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argPurchaseOrderNumRange.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argPurchaseOrderNumRange.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argPurchaseOrderNumRange.ModifiedBy);

            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePurchaseOrderNumRange", param);


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

        public ICollection<ErrorHandler> DeletePurchaseOrderNumRange(string argPODocTypeCode, string argCompanyCode, string argNumRangeCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@PODocTypeCode", argPODocTypeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeletePurchaseOrderNumRange", param);


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

        public bool blnIsPurchaseOrderNumRangeExists(string argPODocTypeCode, string argCompanyCode, string argNumRangeCode, string argClientCode)
        {
            bool IsPurchaseOrderNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetPurchaseOrderNumRange(argPODocTypeCode, argCompanyCode, argNumRangeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPurchaseOrderNumRangeExists = true;
            }
            else
            {
                IsPurchaseOrderNumRangeExists = false;
            }
            return IsPurchaseOrderNumRangeExists;
        }

        public bool blnIsPurchaseOrderNumRangeExists(string argPODocTypeCode, string argCompanyCode, string argNumRangeCode, string argClientCode, DataAccess da)
        {
            bool IsPurchaseOrderNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetPurchaseOrderNumRange(argPODocTypeCode, argCompanyCode, argNumRangeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPurchaseOrderNumRangeExists = true;
            }
            else
            {
                IsPurchaseOrderNumRangeExists = false;
            }
            return IsPurchaseOrderNumRangeExists;
        }
    }
}