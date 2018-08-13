
//Created On :: 09, November, 2012
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
    public class AsgTechCallNumRangeManager
    {
        const string AsgTechCallNumRangeTable = "AsgTechCallNumRange";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public AsgTechCallNumRange objGetAsgTechCallNumRange(string argAsgTechDocTypeCode, string argClientCode)
        {
            AsgTechCallNumRange argAsgTechCallNumRange = new AsgTechCallNumRange();
            DataSet DataSetToFill = new DataSet();

            if (argAsgTechDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetAsgTechCallNumRange(argAsgTechDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argAsgTechCallNumRange = this.objCreateAsgTechCallNumRange((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argAsgTechCallNumRange;
        }

        public ICollection<AsgTechCallNumRange> colGetAsgTechCallNumRange(string argClientCode)
        {
            List<AsgTechCallNumRange> lst = new List<AsgTechCallNumRange>();
            DataSet DataSetToFill = new DataSet();
            AsgTechCallNumRange tAsgTechCallNumRange = new AsgTechCallNumRange();

            DataSetToFill = this.GetAsgTechCallNumRange(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateAsgTechCallNumRange(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetAsgTechCallNumRange(string argAsgTechDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AsgTechDocTypeCode", argAsgTechDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAsgTechCallNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetAsgTechCallNumRange(string argAsgTechDocTypeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AsgTechDocTypeCode", argAsgTechDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetAsgTechCallNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetAsgTechCallNumRange(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
        
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAsgTechCallNumRange",param);
            return DataSetToFill;
        }

        private AsgTechCallNumRange objCreateAsgTechCallNumRange(DataRow dr)
        {
            AsgTechCallNumRange tAsgTechCallNumRange = new AsgTechCallNumRange();

            tAsgTechCallNumRange.SetObjectInfo(dr);

            return tAsgTechCallNumRange;

        }

        public ICollection<ErrorHandler> SaveAsgTechCallNumRange(AsgTechCallNumRange argAsgTechCallNumRange)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsAsgTechCallNumRangeExists(argAsgTechCallNumRange.AsgTechDocTypeCode, argAsgTechCallNumRange.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertAsgTechCallNumRange(argAsgTechCallNumRange, da, lstErr);
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
                    UpdateAsgTechCallNumRange(argAsgTechCallNumRange, da, lstErr);
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

        public void SaveAsgTechCallNumRange(AsgTechCallNumRange argAsgTechCallNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsAsgTechCallNumRangeExists(argAsgTechCallNumRange.AsgTechDocTypeCode, argAsgTechCallNumRange.ClientCode, da) == false)
                {
                    InsertAsgTechCallNumRange(argAsgTechCallNumRange, da, lstErr);
                }
                else
                {
                    UpdateAsgTechCallNumRange(argAsgTechCallNumRange, da, lstErr);
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
            AsgTechCallNumRange ObjAsgTechCallNumRange = null;
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
                        ObjAsgTechCallNumRange = new AsgTechCallNumRange();

                        ObjAsgTechCallNumRange.AsgTechDocTypeCode = Convert.ToString(drExcel["AsgTechDocTypeCode"]).Trim();
                        ObjAsgTechCallNumRange.NumRangeCode = Convert.ToString(drExcel["NumRangeCode"]).Trim();
                        ObjAsgTechCallNumRange.CreatedBy = Convert.ToString(argUserName);
                        ObjAsgTechCallNumRange.ModifiedBy = Convert.ToString(argUserName);
                        ObjAsgTechCallNumRange.ClientCode = Convert.ToString(argClientCode);

                        SaveAsgTechCallNumRange(ObjAsgTechCallNumRange, da, lstErr);

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

        public void InsertAsgTechCallNumRange(AsgTechCallNumRange argAsgTechCallNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@AsgTechDocTypeCode", argAsgTechCallNumRange.AsgTechDocTypeCode);
            param[1] = new SqlParameter("@NumRangeCode", argAsgTechCallNumRange.NumRangeCode);
            param[2] = new SqlParameter("@ClientCode", argAsgTechCallNumRange.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argAsgTechCallNumRange.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argAsgTechCallNumRange.ModifiedBy);


            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertAsgTechCallNumRange", param);


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

        public void UpdateAsgTechCallNumRange(AsgTechCallNumRange argAsgTechCallNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@AsgTechDocTypeCode", argAsgTechCallNumRange.AsgTechDocTypeCode);
            param[1] = new SqlParameter("@NumRangeCode", argAsgTechCallNumRange.NumRangeCode);
            param[2] = new SqlParameter("@ClientCode", argAsgTechCallNumRange.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argAsgTechCallNumRange.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argAsgTechCallNumRange.ModifiedBy);


            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateAsgTechCallNumRange", param);


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

        public ICollection<ErrorHandler> DeleteAsgTechCallNumRange(string argAsgTechDocTypeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@AsgTechDocTypeCode", argAsgTechDocTypeCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteAsgTechCallNumRange", param);


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

        public bool blnIsAsgTechCallNumRangeExists(string argAsgTechDocTypeCode, string argClientCode)
        {
            bool IsAsgTechCallNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetAsgTechCallNumRange(argAsgTechDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAsgTechCallNumRangeExists = true;
            }
            else
            {
                IsAsgTechCallNumRangeExists = false;
            }
            return IsAsgTechCallNumRangeExists;
        }

        public bool blnIsAsgTechCallNumRangeExists(string argAsgTechDocTypeCode, string argClientCode, DataAccess da)
        {
            bool IsAsgTechCallNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetAsgTechCallNumRange(argAsgTechDocTypeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAsgTechCallNumRangeExists = true;
            }
            else
            {
                IsAsgTechCallNumRangeExists = false;
            }
            return IsAsgTechCallNumRangeExists;
        }
    }
}