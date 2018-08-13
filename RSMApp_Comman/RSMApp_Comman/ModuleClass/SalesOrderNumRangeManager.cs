
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
    public class SalesOrderNumRangeManager
    {
        const string SalesOrderNumRangeTable = "SalesOrderNumRange";

       //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public SalesOrderNumRange objGetSalesOrderNumRange(string argSOTypeCode, string argSalesofficeCode, string argNumRangeCode, string argClientCode)
        {
            SalesOrderNumRange argSalesOrderNumRange = new SalesOrderNumRange();
            DataSet DataSetToFill = new DataSet();

            if (argSOTypeCode.Trim() == "")
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

            DataSetToFill = this.GetSalesOrderNumRange(argSOTypeCode, argSalesofficeCode, argNumRangeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSalesOrderNumRange = this.objCreateSalesOrderNumRange((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSalesOrderNumRange;
        }

        public ICollection<SalesOrderNumRange> colGetSalesOrderNumRange(string argSOTypeCode, string argClientCode)
        {
            List<SalesOrderNumRange> lst = new List<SalesOrderNumRange>();
            DataSet DataSetToFill = new DataSet();
            SalesOrderNumRange tSalesOrderNumRange = new SalesOrderNumRange();

            DataSetToFill = this.GetSalesOrderNumRange(argSOTypeCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSalesOrderNumRange(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetSalesOrderNumRange(string argSOTypeCode, string argSalesofficeCode, string argNumRangeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
            param[1] = new SqlParameter("@SalesofficeCode", argSalesofficeCode);
            param[2] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrderNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetSalesOrderNumRange(string argSOTypeCode, string argSalesofficeCode, string argNumRangeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
            param[1] = new SqlParameter("@SalesofficeCode", argSalesofficeCode);
            param[2] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSalesOrderNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetSalesOrderNumRange(string argSOTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrderNumRange", param);

            return DataSetToFill;
        }

        private SalesOrderNumRange objCreateSalesOrderNumRange(DataRow dr)
        {
            SalesOrderNumRange tSalesOrderNumRange = new SalesOrderNumRange();

            tSalesOrderNumRange.SetObjectInfo(dr);

            return tSalesOrderNumRange;

        }

        public ICollection<ErrorHandler> SaveSalesOrderNumRange(SalesOrderNumRange argSalesOrderNumRange)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsSalesOrderNumRangeExists(argSalesOrderNumRange.SOTypeCode, argSalesOrderNumRange.SalesofficeCode, argSalesOrderNumRange.NumRangeCode, argSalesOrderNumRange.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertSalesOrderNumRange(argSalesOrderNumRange, da, lstErr);
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
                    UpdateSalesOrderNumRange(argSalesOrderNumRange, da, lstErr);
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

        public void SaveSalesOrderNumRange(SalesOrderNumRange argSalesOrderNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsSalesOrderNumRangeExists(argSalesOrderNumRange.SOTypeCode, argSalesOrderNumRange.SalesofficeCode, argSalesOrderNumRange.NumRangeCode, argSalesOrderNumRange.ClientCode, da) == false)
                {
                    InsertSalesOrderNumRange(argSalesOrderNumRange, da, lstErr);
                }
                else
                {
                    UpdateSalesOrderNumRange(argSalesOrderNumRange, da, lstErr);
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
            SalesOrderNumRange ObjSalesOrderNumRange = null;
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
                        ObjSalesOrderNumRange = new SalesOrderNumRange();

                        ObjSalesOrderNumRange.SOTypeCode = Convert.ToString(drExcel["SOTypeCode"]).Trim();
                        ObjSalesOrderNumRange.SalesofficeCode = Convert.ToString(drExcel["SalesofficeCode"]).Trim();
                        ObjSalesOrderNumRange.NumRangeCode = Convert.ToString(drExcel["NumRangeCode"]).Trim();
                        ObjSalesOrderNumRange.CreatedBy = Convert.ToString(argUserName);
                        ObjSalesOrderNumRange.ModifiedBy = Convert.ToString(argUserName);
                        ObjSalesOrderNumRange.ClientCode = Convert.ToString(argClientCode);

                        SaveSalesOrderNumRange(ObjSalesOrderNumRange, da, lstErr);

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

        public void InsertSalesOrderNumRange(SalesOrderNumRange argSalesOrderNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@SOTypeCode", argSalesOrderNumRange.SOTypeCode);
            param[1] = new SqlParameter("@SalesofficeCode", argSalesOrderNumRange.SalesofficeCode);
            param[2] = new SqlParameter("@NumRangeCode", argSalesOrderNumRange.NumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argSalesOrderNumRange.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argSalesOrderNumRange.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argSalesOrderNumRange.ModifiedBy);
  
            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSalesOrderNumRange", param);


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

        public void UpdateSalesOrderNumRange(SalesOrderNumRange argSalesOrderNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@SOTypeCode", argSalesOrderNumRange.SOTypeCode);
            param[1] = new SqlParameter("@SalesofficeCode", argSalesOrderNumRange.SalesofficeCode);
            param[2] = new SqlParameter("@NumRangeCode", argSalesOrderNumRange.NumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argSalesOrderNumRange.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argSalesOrderNumRange.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argSalesOrderNumRange.ModifiedBy);

            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSalesOrderNumRange", param);


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

        public ICollection<ErrorHandler> DeleteSalesOrderNumRange(string argSOTypeCode, string argSalesofficeCode, string argNumRangeCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteSalesOrderNumRange", param);


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

        public bool blnIsSalesOrderNumRangeExists(string argSOTypeCode, string argSalesofficeCode, string argNumRangeCode, string argClientCode)
        {
            bool IsSalesOrderNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesOrderNumRange(argSOTypeCode, argSalesofficeCode, argNumRangeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesOrderNumRangeExists = true;
            }
            else
            {
                IsSalesOrderNumRangeExists = false;
            }
            return IsSalesOrderNumRangeExists;
        }

        public bool blnIsSalesOrderNumRangeExists(string argSOTypeCode, string argSalesofficeCode, string argNumRangeCode, string argClientCode, DataAccess da)
        {
            bool IsSalesOrderNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesOrderNumRange(argSOTypeCode, argSalesofficeCode, argNumRangeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesOrderNumRangeExists = true;
            }
            else
            {
                IsSalesOrderNumRangeExists = false;
            }
            return IsSalesOrderNumRangeExists;
        }
    }
}