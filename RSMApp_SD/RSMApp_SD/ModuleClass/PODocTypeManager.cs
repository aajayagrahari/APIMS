
//Created On :: 15, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_SD
{
    public class PODocTypeManager
    {
        const string PODocTypeTable = "PODocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public PODocType objGetPODocType(string argPODocTypeCode, string argClientCode)
        {
            PODocType argPODocType = new PODocType();
            DataSet DataSetToFill = new DataSet();

            if (argPODocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPODocType(argPODocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPODocType = this.objCreatePODocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPODocType;
        }

        public ICollection<PODocType> colGetPODocType(string argClientCode)
        {
            List<PODocType> lst = new List<PODocType>();
            DataSet DataSetToFill = new DataSet();
            PODocType tPODocType = new PODocType();

            DataSetToFill = this.GetPODocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePODocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetPODocType(string argPODocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PODocTypeCode", argPODocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPODocType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPODocType(string argPODocTypeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PODocTypeCode", argPODocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPODocType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPODocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetPODocType",param);
            return DataSetToFill;
        }

        public DataSet GetPODocType(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + PODocTypeTable.ToString();

                if (iIsDeleted > -1)
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " IsDeleted = " + iIsDeleted;
                }

                if (argClientCode != "")
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " ClientCode = '" + argClientCode + "'";
                }

                ds = da.FillDataSetWithSQL(tSQL);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        private PODocType objCreatePODocType(DataRow dr)
        {
            PODocType tPODocType = new PODocType();

            tPODocType.SetObjectInfo(dr);

            return tPODocType;

        }

        public ICollection<ErrorHandler> SavePODocType(PODocType argPODocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPODocTypeExists(argPODocType.PODocTypeCode, argPODocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPODocType(argPODocType, da, lstErr);
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
                    UpdatePODocType(argPODocType, da, lstErr);
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

        public void SavePODocType(PODocType argPODocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsPODocTypeExists(argPODocType.PODocTypeCode, argPODocType.ClientCode, da) == false)
                {
                    InsertPODocType(argPODocType, da, lstErr);
                }
                else
                {
                    UpdatePODocType(argPODocType, da, lstErr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
            PODocType ObjPODocType = null;
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
                        ObjPODocType = new PODocType();
                        ObjPODocType.PODocTypeCode = Convert.ToString(drExcel["PODocTypeCode"]).Trim();
                        ObjPODocType.PODocTypeDesc = Convert.ToString(drExcel["PODocTypeDesc"]).Trim();
                        ObjPODocType.NumRange = Convert.ToString(drExcel["NumRange"]).Trim();
                        ObjPODocType.POType = Convert.ToString(drExcel["POType"]).Trim();
                        ObjPODocType.SaveMode = Convert.ToInt32(drExcel["SaveMode"]);
                        ObjPODocType.CreatedBy = Convert.ToString(argUserName);
                        ObjPODocType.ModifiedBy = Convert.ToString(argUserName);
                        ObjPODocType.ClientCode = Convert.ToString(argClientCode);
                        SavePODocType(ObjPODocType, da, lstErr);

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

        public void InsertPODocType(PODocType argPODocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@PODocTypeCode", argPODocType.PODocTypeCode);
            param[1] = new SqlParameter("@PODocTypeDesc", argPODocType.PODocTypeDesc);
            param[2] = new SqlParameter("@NumRange", argPODocType.NumRange);
            param[3] = new SqlParameter("@POType", argPODocType.POType);
            param[4] = new SqlParameter("@SaveMode", argPODocType.SaveMode);
            param[5] = new SqlParameter("@ClientCode", argPODocType.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argPODocType.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argPODocType.ModifiedBy);
    
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertPODocType", param);


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

        public void UpdatePODocType(PODocType argPODocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@PODocTypeCode", argPODocType.PODocTypeCode);
            param[1] = new SqlParameter("@PODocTypeDesc", argPODocType.PODocTypeDesc);
            param[2] = new SqlParameter("@NumRange", argPODocType.NumRange);
            param[3] = new SqlParameter("@POType", argPODocType.POType);
            param[4] = new SqlParameter("@SaveMode", argPODocType.SaveMode);
            param[5] = new SqlParameter("@ClientCode", argPODocType.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argPODocType.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argPODocType.ModifiedBy);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePODocType", param);


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

        public ICollection<ErrorHandler> DeletePODocType(string argPODocTypeCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@PODocTypeCode", argPODocTypeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeletePODocType", param);


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

        public bool blnIsPODocTypeExists(string argPODocTypeCode, string argClientCode)
        {
            bool IsPODocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetPODocType(argPODocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPODocTypeExists = true;
            }
            else
            {
                IsPODocTypeExists = false;
            }
            return IsPODocTypeExists;
        }

        public bool blnIsPODocTypeExists(string argPODocTypeCode, string argClientCode, DataAccess da)
        {
            bool IsPODocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetPODocType(argPODocTypeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPODocTypeExists = true;
            }
            else
            {
                IsPODocTypeExists = false;
            }
            return IsPODocTypeExists;
        }
    }
}