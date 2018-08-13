
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
    public class RepairCallNumRangeManager
    {
        const string RepairCallNumRangeTable = "RepairCallNumRange";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public RepairCallNumRange objGetRepairCallNumRange(string argRepairDocTypeCode, string argNumRangeCode, string argClientCode)
        {
            RepairCallNumRange argRepairCallNumRange = new RepairCallNumRange();
            DataSet DataSetToFill = new DataSet();

            if (argRepairDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetRepairCallNumRange(argRepairDocTypeCode, argNumRangeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argRepairCallNumRange = this.objCreateRepairCallNumRange((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argRepairCallNumRange;
        }

        public ICollection<RepairCallNumRange> colGetRepairCallNumRange(string argClientCode)
        {
            List<RepairCallNumRange> lst = new List<RepairCallNumRange>();
            DataSet DataSetToFill = new DataSet();
            RepairCallNumRange tRepairCallNumRange = new RepairCallNumRange();

            DataSetToFill = this.GetRepairCallNumRange(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateRepairCallNumRange(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetRepairCallNumRange(string argRepairDocTypeCode, string argNumRangeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RepairDocTypeCode", argRepairDocTypeCode);
            param[1] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepairCallNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetRepairCallNumRange(string argRepairDocTypeCode, string argNumRangeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RepairDocTypeCode", argRepairDocTypeCode);
            param[1] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetRepairCallNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetRepairCallNumRange(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepairCallNumRange",param);
            return DataSetToFill;
        }

        private RepairCallNumRange objCreateRepairCallNumRange(DataRow dr)
        {
            RepairCallNumRange tRepairCallNumRange = new RepairCallNumRange();

            tRepairCallNumRange.SetObjectInfo(dr);

            return tRepairCallNumRange;

        }

        public ICollection<ErrorHandler> SaveRepairCallNumRange(RepairCallNumRange argRepairCallNumRange)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsRepairCallNumRangeExists(argRepairCallNumRange.RepairDocTypeCode, argRepairCallNumRange.NumRangeCode, argRepairCallNumRange.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertRepairCallNumRange(argRepairCallNumRange, da, lstErr);
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
                    UpdateRepairCallNumRange(argRepairCallNumRange, da, lstErr);
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

        public void SaveRepairCallNumRange(RepairCallNumRange argRepairCallNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsRepairCallNumRangeExists(argRepairCallNumRange.RepairDocTypeCode, argRepairCallNumRange.NumRangeCode, argRepairCallNumRange.ClientCode, da) == false)
                {
                    InsertRepairCallNumRange(argRepairCallNumRange, da, lstErr);
                }
                else
                {
                    UpdateRepairCallNumRange(argRepairCallNumRange, da, lstErr);
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
            RepairCallNumRange ObjRepairCallNumRange = null;
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
                        ObjRepairCallNumRange = new RepairCallNumRange();

                        ObjRepairCallNumRange.RepairDocTypeCode = Convert.ToString(drExcel["RepairDocTypeCode"]).Trim();
                        ObjRepairCallNumRange.NumRangeCode = Convert.ToString(drExcel["NumRangeCode"]).Trim();
                        ObjRepairCallNumRange.CreatedBy = Convert.ToString(argUserName);
                        ObjRepairCallNumRange.ModifiedBy = Convert.ToString(argUserName);
                        ObjRepairCallNumRange.ClientCode = Convert.ToString(argClientCode);

                        SaveRepairCallNumRange(ObjRepairCallNumRange, da, lstErr);

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

        public void InsertRepairCallNumRange(RepairCallNumRange argRepairCallNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@RepairDocTypeCode", argRepairCallNumRange.RepairDocTypeCode);
            param[1] = new SqlParameter("@NumRangeCode", argRepairCallNumRange.NumRangeCode);
            param[2] = new SqlParameter("@ClientCode", argRepairCallNumRange.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argRepairCallNumRange.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argRepairCallNumRange.ModifiedBy);
       

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertRepairCallNumRange", param);


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

        public void UpdateRepairCallNumRange(RepairCallNumRange argRepairCallNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@RepairDocTypeCode", argRepairCallNumRange.RepairDocTypeCode);
            param[1] = new SqlParameter("@NumRangeCode", argRepairCallNumRange.NumRangeCode);
            param[2] = new SqlParameter("@ClientCode", argRepairCallNumRange.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argRepairCallNumRange.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argRepairCallNumRange.ModifiedBy);


            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateRepairCallNumRange", param);


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

        public ICollection<ErrorHandler> DeleteRepairCallNumRange(string argRepairDocTypeCode, string argNumRangeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@RepairDocTypeCode", argRepairDocTypeCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteRepairCallNumRange", param);

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

        public bool blnIsRepairCallNumRangeExists(string argRepairDocTypeCode, string argNumRangeCode, string argClientCode)
        {
            bool IsRepairCallNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetRepairCallNumRange(argRepairDocTypeCode, argNumRangeCode, argClientCode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsRepairCallNumRangeExists = true;
            }
            else
            {
                IsRepairCallNumRangeExists = false;
            }
            return IsRepairCallNumRangeExists;
        }

        public bool blnIsRepairCallNumRangeExists(string argRepairDocTypeCode, string argNumRangeCode, string argClientCode, DataAccess da)
        {
            bool IsRepairCallNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetRepairCallNumRange(argRepairDocTypeCode, argNumRangeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsRepairCallNumRangeExists = true;
            }
            else
            {
                IsRepairCallNumRangeExists = false;
            }
            return IsRepairCallNumRangeExists;
        }
    }
}