
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
    public class GoodsMMNumRangeManager
    {
        const string GoodsMMNumRangeTable = "GoodsMMNumRange";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public GoodsMMNumRange objGetGoodsMMNumRange(string argCompanyCode, string argNumRangeCode, string argClientCode)
        {
            GoodsMMNumRange argGoodsMMNumRange = new GoodsMMNumRange();
            DataSet DataSetToFill = new DataSet();

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

            DataSetToFill = this.GetGoodsMMNumRange(argCompanyCode, argNumRangeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argGoodsMMNumRange = this.objCreateGoodsMMNumRange((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argGoodsMMNumRange;
        }

        public ICollection<GoodsMMNumRange> colGetGoodsMMNumRange( string argClientCode)
        {
            List<GoodsMMNumRange> lst = new List<GoodsMMNumRange>();
            DataSet DataSetToFill = new DataSet();
            GoodsMMNumRange tGoodsMMNumRange = new GoodsMMNumRange();

            DataSetToFill = this.GetGoodsMMNumRange( argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateGoodsMMNumRange(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetGoodsMMNumRange(string argCompanyCode, string argNumRangeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[1] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetGoodsMMNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetGoodsMMNumRange(string argCompanyCode, string argNumRangeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[1] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetGoodsMMNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetGoodsMMNumRange(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetGoodsMMNumRange",param);
            return DataSetToFill;
        }

        private GoodsMMNumRange objCreateGoodsMMNumRange(DataRow dr)
        {
            GoodsMMNumRange tGoodsMMNumRange = new GoodsMMNumRange();

            tGoodsMMNumRange.SetObjectInfo(dr);

            return tGoodsMMNumRange;

        }

        public ICollection<ErrorHandler> SaveGoodsMMNumRange(GoodsMMNumRange argGoodsMMNumRange)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsGoodsMMNumRangeExists(argGoodsMMNumRange.CompanyCode, argGoodsMMNumRange.NumRangeCode, argGoodsMMNumRange.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertGoodsMMNumRange(argGoodsMMNumRange, da, lstErr);
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
                    UpdateGoodsMMNumRange(argGoodsMMNumRange, da, lstErr);
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

        public void SaveGoodsMMNumRange(GoodsMMNumRange argGoodsMMNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsGoodsMMNumRangeExists(argGoodsMMNumRange.CompanyCode, argGoodsMMNumRange.NumRangeCode, argGoodsMMNumRange.ClientCode, da) == false)
                {
                    InsertGoodsMMNumRange(argGoodsMMNumRange, da, lstErr);
                }
                else
                {
                    UpdateGoodsMMNumRange(argGoodsMMNumRange, da, lstErr);
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
            GoodsMMNumRange ObjGoodsMMNumRange = null;
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
                        ObjGoodsMMNumRange = new GoodsMMNumRange();

                        ObjGoodsMMNumRange.CompanyCode = Convert.ToString(drExcel["CompanyCode"]).Trim();
                        ObjGoodsMMNumRange.NumRangeCode = Convert.ToString(drExcel["NumRangeCode"]).Trim();
                        ObjGoodsMMNumRange.FiscalYearType = Convert.ToString(drExcel["FiscalYearType"]).Trim();
                        ObjGoodsMMNumRange.CreatedBy = Convert.ToString(argUserName);
                        ObjGoodsMMNumRange.ModifiedBy = Convert.ToString(argUserName);
                        ObjGoodsMMNumRange.ClientCode = Convert.ToString(argClientCode);

                        SaveGoodsMMNumRange(ObjGoodsMMNumRange, da, lstErr);

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

        public void InsertGoodsMMNumRange(GoodsMMNumRange argGoodsMMNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@CompanyCode", argGoodsMMNumRange.CompanyCode);
            param[1] = new SqlParameter("@FiscalYearType", argGoodsMMNumRange.FiscalYearType);
            param[2] = new SqlParameter("@NumRangeCode", argGoodsMMNumRange.NumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argGoodsMMNumRange.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argGoodsMMNumRange.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argGoodsMMNumRange.ModifiedBy);
         

            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertGoodsMMNumRange", param);


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

        public void UpdateGoodsMMNumRange(GoodsMMNumRange argGoodsMMNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@CompanyCode", argGoodsMMNumRange.CompanyCode);
            param[1] = new SqlParameter("@FiscalYearType", argGoodsMMNumRange.FiscalYearType);
            param[2] = new SqlParameter("@NumRangeCode", argGoodsMMNumRange.NumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argGoodsMMNumRange.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argGoodsMMNumRange.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argGoodsMMNumRange.ModifiedBy);
    
            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;
            
            int i = da.NExecuteNonQuery("Proc_UpdateGoodsMMNumRange", param);


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
            lstErr.Add(objErrorHandler);

        }

        public ICollection<ErrorHandler> DeleteGoodsMMNumRange(string argCompanyCode, string argNumRangeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
                param[1] = new SqlParameter("@NumRangeCode", argNumRangeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteGoodsMMNumRange", param);


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

        public bool blnIsGoodsMMNumRangeExists(string argCompanyCode, string argNumRangeCode, string argClientCode)
        {
            bool IsGoodsMMNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetGoodsMMNumRange(argCompanyCode, argNumRangeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsGoodsMMNumRangeExists = true;
            }
            else
            {
                IsGoodsMMNumRangeExists = false;
            }
            return IsGoodsMMNumRangeExists;
        }

        public bool blnIsGoodsMMNumRangeExists(string argCompanyCode, string argNumRangeCode, string argClientCode, DataAccess da)
        {
            bool IsGoodsMMNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetGoodsMMNumRange(argCompanyCode, argNumRangeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsGoodsMMNumRangeExists = true;
            }
            else
            {
                IsGoodsMMNumRangeExists = false;
            }
            return IsGoodsMMNumRangeExists;
        }
    }
}