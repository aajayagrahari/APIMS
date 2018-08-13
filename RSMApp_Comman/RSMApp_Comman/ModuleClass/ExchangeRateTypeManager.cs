
//Created On :: 29, September, 2012
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
    public class ExchangeRateTypeManager
    {
        const string ExchangeRateTypeTable = "ExchangeRateType";
        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public ExchangeRateType objGetExchangeRateType(string argExChngRateTypeCode)
        {
            ExchangeRateType argExchangeRateType = new ExchangeRateType();
            DataSet DataSetToFill = new DataSet();

            if (argExChngRateTypeCode.Trim() == "")
            {
                goto ErrorHandler;
            }
            DataSetToFill = this.GetExchangeRateType(argExChngRateTypeCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argExchangeRateType = this.objCreateExchangeRateType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandler:

        Finish:
            DataSetToFill = null;

            return argExchangeRateType;
        }

        public ICollection<ExchangeRateType> colGetExchangeRateType()
        {
            List<ExchangeRateType> lst = new List<ExchangeRateType>();
            DataSet DataSetToFill = new DataSet();
            ExchangeRateType tExchangeRateType = new ExchangeRateType();
            DataSetToFill = this.GetExchangeRateType();

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateExchangeRateType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }
       
        public DataSet GetExchangeRateType(string argExChngRateTypeCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ExChngRateTypeCode", argExChngRateTypeCode);

            DataSetToFill = da.FillDataSet("SP_GetExchangeRateType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetExchangeRateType(string argExChngRateTypeCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ExChngRateTypeCode", argExChngRateTypeCode);

            DataSetToFill = da.NFillDataSet("SP_GetExchangeRateType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetExchangeRateType()
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            DataSetToFill = da.FillDataSet("SP_GetExchangeRateType");
            return DataSetToFill;
        }

        public DataSet GetExchangeRateType(int iIsDeleted)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";
            try
            {
                tSQL = "SELECT * FROM " + ExchangeRateTypeTable.ToString();

                if (iIsDeleted >= 0)
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " IsDeleted = " + iIsDeleted;
                }
                ds = da.FillDataSetWithSQL(tSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        private ExchangeRateType objCreateExchangeRateType(DataRow dr)
        {
            ExchangeRateType tExchangeRateType = new ExchangeRateType();
            tExchangeRateType.SetObjectInfo(dr);
            return tExchangeRateType;
        }

        public ICollection<ErrorHandler> SaveExchangeRateType(ExchangeRateType argExchangeRateType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsExchangeRateTypeExists(argExchangeRateType.ExChngRateTypeCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertExchangeRateType(argExchangeRateType, da, lstErr);
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
                    UpdateExchangeRateType(argExchangeRateType, da, lstErr);
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

        public void SaveExchangeRateType(ExchangeRateType argExchangeRateType, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsExchangeRateTypeExists(argExchangeRateType.ExChngRateTypeCode, da) == false)
                {
                    InsertExchangeRateType(argExchangeRateType, da, lstErr);
                }
                else
                {
                    UpdateExchangeRateType(argExchangeRateType, da, lstErr);
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
            ExchangeRateType ObjExchangeRateType = null;
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
                        ObjExchangeRateType = new ExchangeRateType();

                        ObjExchangeRateType.ExChngRateTypeCode = Convert.ToString(drExcel["ExChngRateTypeCode"]).Trim();
                        ObjExchangeRateType.ExChngRateTypeDesc = Convert.ToString(drExcel["ExChngRateTypeDesc"]).Trim();
                        ObjExchangeRateType.CreatedBy = Convert.ToString(argUserName);
                        ObjExchangeRateType.ModifiedBY = Convert.ToString(argUserName);

                        SaveExchangeRateType(ObjExchangeRateType, da, lstErr);

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

        public void InsertExchangeRateType(ExchangeRateType argExchangeRateType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@ExChngRateTypeCode", argExchangeRateType.ExChngRateTypeCode);
            param[1] = new SqlParameter("@ExChngRateTypeDesc", argExchangeRateType.ExChngRateTypeDesc);
            param[2] = new SqlParameter("@CreatedBy", argExchangeRateType.CreatedBy);
            param[3] = new SqlParameter("@ModifiedBY", argExchangeRateType.ModifiedBY);

            param[4] = new SqlParameter("@Type", SqlDbType.Char);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[5].Size = 255;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertExchangeRateType", param);

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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);
        }

        public void UpdateExchangeRateType(ExchangeRateType argExchangeRateType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@ExChngRateTypeCode", argExchangeRateType.ExChngRateTypeCode);
            param[1] = new SqlParameter("@ExChngRateTypeDesc", argExchangeRateType.ExChngRateTypeDesc);
            param[2] = new SqlParameter("@CreatedBy", argExchangeRateType.CreatedBy);
            param[3] = new SqlParameter("@ModifiedBY", argExchangeRateType.ModifiedBY);
            
            param[4] = new SqlParameter("@Type", SqlDbType.Char);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[5].Size = 255;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateExchangeRateType", param);

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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);
        }

        public ICollection<ErrorHandler> DeleteExchangeRateType(string argExChngRateTypeCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            try
            {
                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@ExChngRateTypeCode", argExChngRateTypeCode);
                param[1] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[2] = new SqlParameter("@Type", SqlDbType.Char);
                param[2].Size = 1;
                param[2].Direction = ParameterDirection.Output;

                param[3] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[3].Size = 255;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[4].Size = 20;
                param[4].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteExchangeRateType", param);

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

        public bool blnIsExchangeRateTypeExists(string argExChngRateTypeCode)
        {
            bool IsExchangeRateTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetExchangeRateType(argExChngRateTypeCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsExchangeRateTypeExists = true;
            }
            else
            {
                IsExchangeRateTypeExists = false;
            }
            return IsExchangeRateTypeExists;
        }

        public bool blnIsExchangeRateTypeExists(string argExChngRateTypeCode, DataAccess da)
        {
            bool IsExchangeRateTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetExchangeRateType(argExChngRateTypeCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsExchangeRateTypeExists = true;
            }
            else
            {
                IsExchangeRateTypeExists = false;
            }
            return IsExchangeRateTypeExists;
        }
    }
}