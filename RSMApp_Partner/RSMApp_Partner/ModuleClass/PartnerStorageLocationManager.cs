
//Created On :: 16, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_Partner
{
    public class PartnerStorageLocationManager
    {
        const string PartnerStorageLocationTable = "PartnerStorageLocation";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public PartnerStorageLocation objGetPartnerStorageLocation(string argPartnerTypeCode, string argStoreCode, string argClientCode)
        {
            PartnerStorageLocation argPartnerStorageLocation = new PartnerStorageLocation();
            DataSet DataSetToFill = new DataSet();

            if (argPartnerTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argStoreCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPartnerStorageLocation(argPartnerTypeCode, argStoreCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartnerStorageLocation = this.objCreatePartnerStorageLocation((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPartnerStorageLocation;
        }

        public ICollection<PartnerStorageLocation> colGetPartnerStorageLocation(string argPartnerCode, string argClientCode)
        {
            List<PartnerStorageLocation> lst = new List<PartnerStorageLocation>();
            DataSet DataSetToFill = new DataSet();
            PartnerStorageLocation tPartnerStorageLocation = new PartnerStorageLocation();

            DataSetToFill = this.GetPartnerStorageLocation(argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePartnerStorageLocation(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetPartnerStorageLocation(string argPartnerTypeCode, string argStoreCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PartnerTypeCode", argPartnerTypeCode);
            param[1] = new SqlParameter("@StoreCode", argStoreCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerStorageLocation4ID", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerStorageLocation(string argPartnerTypeCode, string argStoreCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PartnerTypeCode", argPartnerTypeCode);
            param[1] = new SqlParameter("@StoreCode", argStoreCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPartnerStorageLocation4ID", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerStorageLocation4PartnerType(string argPartnerTypeCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerTypeCode", argPartnerTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerStorageLocation4PartnerType", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerStorageLocation(string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerStorageLocation", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerStorageLocation(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + PartnerStorageLocationTable.ToString();

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

        private PartnerStorageLocation objCreatePartnerStorageLocation(DataRow dr)
        {
            PartnerStorageLocation tPartnerStorageLocation = new PartnerStorageLocation();
            tPartnerStorageLocation.SetObjectInfo(dr);
            return tPartnerStorageLocation;
        }

        public ICollection<ErrorHandler> SavePartnerStorageLocation(PartnerStorageLocation argPartnerStorageLocation)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartnerStorageLocationExists(argPartnerStorageLocation.PartnerTypeCode, argPartnerStorageLocation.StoreCode, argPartnerStorageLocation.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartnerStorageLocation(argPartnerStorageLocation, da, lstErr);
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
                    UpdatePartnerStorageLocation(argPartnerStorageLocation, da, lstErr);
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

        public ICollection<ErrorHandler> SavePartnerStorageLocation(ICollection<PartnerStorageLocation> colPartnerStorageLocation)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (PartnerStorageLocation argPartnerStorageLocation in colPartnerStorageLocation)
                {

                    if (argPartnerStorageLocation.IsDeleted == 0)
                    {

                        if (blnIsPartnerStorageLocationExists(argPartnerStorageLocation.PartnerTypeCode,argPartnerStorageLocation.StoreCode ,argPartnerStorageLocation.ClientCode, da) == false)
                        {
                            InsertPartnerStorageLocation(argPartnerStorageLocation, da, lstErr);
                        }
                        else
                        {
                            UpdatePartnerStorageLocation(argPartnerStorageLocation, da, lstErr);
                        }
                    }
                    else
                    {
                        DeletePartnerStorageLocation(argPartnerStorageLocation.PartnerTypeCode, argPartnerStorageLocation.StoreCode, argPartnerStorageLocation.ClientCode, argPartnerStorageLocation.IsDeleted);

                    }

                }

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

        public void SavePartnerStorageLocation(PartnerStorageLocation argPartnerStorageLocation, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsPartnerStorageLocationExists(argPartnerStorageLocation.PartnerTypeCode, argPartnerStorageLocation.StoreCode, argPartnerStorageLocation.ClientCode, da) == false)
                {
                    InsertPartnerStorageLocation(argPartnerStorageLocation, da, lstErr);
                }
                else
                {
                    UpdatePartnerStorageLocation(argPartnerStorageLocation, da, lstErr);
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
            PartnerStorageLocation ObjPartnerStorageLocation = null;
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
                        ObjPartnerStorageLocation = new PartnerStorageLocation();
                        ObjPartnerStorageLocation.PartnerTypeCode = Convert.ToString(drExcel["PartnerTypeCode"]).Trim();
                        ObjPartnerStorageLocation.StoreCode = Convert.ToString(drExcel["StoreCode"]).Trim();
                        ObjPartnerStorageLocation.StoreName = Convert.ToString(drExcel["StoreName"]).Trim();
                        ObjPartnerStorageLocation.CreatedBy = Convert.ToString(argUserName);
                        ObjPartnerStorageLocation.ModifiedBy = Convert.ToString(argUserName);
                        ObjPartnerStorageLocation.ClientCode = Convert.ToString(argClientCode);
                        SavePartnerStorageLocation(ObjPartnerStorageLocation, da, lstErr);

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

        public void InsertPartnerStorageLocation(PartnerStorageLocation argPartnerStorageLocation, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@PartnerTypeCode", argPartnerStorageLocation.PartnerTypeCode);
            param[1] = new SqlParameter("@StoreCode", argPartnerStorageLocation.StoreCode);
            param[2] = new SqlParameter("@StoreName", argPartnerStorageLocation.StoreName);
            param[3] = new SqlParameter("@ClientCode", argPartnerStorageLocation.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argPartnerStorageLocation.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argPartnerStorageLocation.ModifiedBy);
 
            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPartnerStorageLocation", param);

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

        public void UpdatePartnerStorageLocation(PartnerStorageLocation argPartnerStorageLocation, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@PartnerTypeCode", argPartnerStorageLocation.PartnerTypeCode);
            param[1] = new SqlParameter("@StoreCode", argPartnerStorageLocation.StoreCode);
            param[2] = new SqlParameter("@StoreName", argPartnerStorageLocation.StoreName);
            param[3] = new SqlParameter("@ClientCode", argPartnerStorageLocation.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argPartnerStorageLocation.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argPartnerStorageLocation.ModifiedBy);

            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePartnerStorageLocation", param);

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

        public ICollection<ErrorHandler> DeletePartnerStorageLocation(string argPartnerTypeCode, string argStoreCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@PartnerTypeCode", argPartnerTypeCode);
                param[1] = new SqlParameter("@StoreCode", argStoreCode);
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

                int i = da.ExecuteNonQuery("Proc_DeletePartnerStorageLocation", param);

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

        public bool blnIsPartnerStorageLocationExists(string argPartnerTypeCode, string argStoreCode, string argClientCode)
        {
            bool IsPartnerStorageLocationExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerStorageLocation(argPartnerTypeCode, argStoreCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerStorageLocationExists = true;
            }
            else
            {
                IsPartnerStorageLocationExists = false;
            }
            return IsPartnerStorageLocationExists;
        }

        public bool blnIsPartnerStorageLocationExists(string argPartnerTypeCode, string argStoreCode, string argClientCode, DataAccess da)
        {
            bool IsPartnerStorageLocationExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerStorageLocation(argPartnerTypeCode, argStoreCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerStorageLocationExists = true;
            }
            else
            {
                IsPartnerStorageLocationExists = false;
            }
            return IsPartnerStorageLocationExists;
        }
    }
}