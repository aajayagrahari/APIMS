
//Created On :: 07, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using Telerik.Web.UI;
using System.Data.OleDb;

namespace RSMApp_Organization
{
    public class SalesRegionManager
    {
        const string SalesRegionTable = "SalesRegion";
        ErrorHandler objErrorHandler = new ErrorHandler();

        public SalesRegion objGetSalesRegion(string argSalesRegionCode, string argClientCode)
        {
            SalesRegion argSalesRegion = new SalesRegion();
            DataSet DataSetToFill = new DataSet();

            if (argSalesRegionCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetSalesRegion(argSalesRegionCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argSalesRegion = this.objCreateSalesRegion((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argSalesRegion;
        }
        
        public ICollection<SalesRegion> colGetSalesRegion(string argClientCode)
        {
            List<SalesRegion> lst = new List<SalesRegion>();
            DataSet DataSetToFill = new DataSet();
            SalesRegion tSalesRegion = new SalesRegion();
            DataSetToFill = this.GetSalesRegion(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSalesRegion(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }
        
        public DataSet GetSalesRegion(string argSalesRegionCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SalesRegionCode", argSalesRegionCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesRegion4ID", param);
            return DataSetToFill;
        }

        public DataSet GetSalesRegion(string argSalesRegionCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SalesRegionCode", argSalesRegionCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSalesRegion4ID", param);
            return DataSetToFill;
        }

        public DataSet GetSalesRegion(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + SalesRegionTable.ToString();

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
        
        public DataSet GetSalesRegion(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesRegion", param);
            return DataSetToFill;
        }
        
        public DataSet GetSalesRegion4Combo(string argPrefix, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetSalesRegion4Combo", param);
            return DataSetToFill;
        }
                
        private SalesRegion objCreateSalesRegion(DataRow dr)
        {
            SalesRegion tSalesRegion = new SalesRegion();
            tSalesRegion.SetObjectInfo(dr);
            return tSalesRegion;
        }
        
        public ICollection<ErrorHandler> SaveSalesRegion(SalesRegion argSalesRegion)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsSalesRegionExists(argSalesRegion.SalesRegionCode, argSalesRegion.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertSalesRegion(argSalesRegion, da, lstErr);
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
                    UpdateSalesRegion(argSalesRegion, da, lstErr);
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

        /*************/
        public void SaveSalesRegion(SalesRegion argSalesRegion, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsSalesRegionExists(argSalesRegion.SalesRegionCode, argSalesRegion.ClientCode, da) == false)
                {
                    InsertSalesRegion(argSalesRegion, da, lstErr);
                }
                else
                {
                    UpdateSalesRegion(argSalesRegion, da, lstErr);
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
            SalesRegion ObjSalesRegion = null;
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
                        ObjSalesRegion = new SalesRegion();
                        ObjSalesRegion.SalesRegionCode = Convert.ToString(drExcel["SalesRegionCode"]).Trim();
                        ObjSalesRegion.SalesRegionName = Convert.ToString(drExcel["SalesRegionName"]).Trim();
                        ObjSalesRegion.Address1 = Convert.ToString(drExcel["Address1"]).Trim();
                        ObjSalesRegion.Address2 = Convert.ToString(drExcel["Address2"]).Trim();
                        ObjSalesRegion.CountryCode = Convert.ToString(drExcel["CountryCode"]).Trim();
                        ObjSalesRegion.StateCode = Convert.ToString(drExcel["StateCode"]).Trim();
                        ObjSalesRegion.City = Convert.ToString(drExcel["City"]).Trim();
                        ObjSalesRegion.PinCode = Convert.ToString(drExcel["PinCode"]).Trim();
                        ObjSalesRegion.TelNO = Convert.ToString(drExcel["TelNO"]).Trim();
                        ObjSalesRegion.EmailID = Convert.ToString(drExcel["EmailID"]).Trim();
                        ObjSalesRegion.CreatedBy = Convert.ToString(argUserName);
                        ObjSalesRegion.ModifiedBy = Convert.ToString(argUserName);
                        ObjSalesRegion.ClientCode = Convert.ToString(argClientCode);
                        SaveSalesRegion(ObjSalesRegion, da, lstErr);

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
        /*************/ 
        
        public void InsertSalesRegion(SalesRegion argSalesRegion, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@SalesRegionCode", argSalesRegion.SalesRegionCode);
            param[1] = new SqlParameter("@SalesRegionName", argSalesRegion.SalesRegionName);
            param[2] = new SqlParameter("@Address1", argSalesRegion.Address1);
            param[3] = new SqlParameter("@Address2", argSalesRegion.Address2);
            param[4] = new SqlParameter("@CountryCode", argSalesRegion.CountryCode);
            param[5] = new SqlParameter("@StateCode", argSalesRegion.StateCode);
            param[6] = new SqlParameter("@City", argSalesRegion.City);
            param[7] = new SqlParameter("@PinCode", argSalesRegion.PinCode);
            param[8] = new SqlParameter("@TelNO", argSalesRegion.TelNO);
            param[9] = new SqlParameter("@EmailID", argSalesRegion.EmailID);
            param[10] = new SqlParameter("@ClientCode", argSalesRegion.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argSalesRegion.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argSalesRegion.ModifiedBy);
            
            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSalesRegion", param);

            string strMessage = Convert.ToString(param[14].Value);
            string strType = Convert.ToString(param[13].Value);
            string strRetValue = Convert.ToString(param[15].Value);

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
        
        public void UpdateSalesRegion(SalesRegion argSalesRegion, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@SalesRegionCode", argSalesRegion.SalesRegionCode);
            param[1] = new SqlParameter("@SalesRegionName", argSalesRegion.SalesRegionName);
            param[2] = new SqlParameter("@Address1", argSalesRegion.Address1);
            param[3] = new SqlParameter("@Address2", argSalesRegion.Address2);
            param[4] = new SqlParameter("@CountryCode", argSalesRegion.CountryCode);
            param[5] = new SqlParameter("@StateCode", argSalesRegion.StateCode);
            param[6] = new SqlParameter("@City", argSalesRegion.City);
            param[7] = new SqlParameter("@PinCode", argSalesRegion.PinCode);
            param[8] = new SqlParameter("@TelNO", argSalesRegion.TelNO);
            param[9] = new SqlParameter("@EmailID", argSalesRegion.EmailID);
            param[10] = new SqlParameter("@ClientCode", argSalesRegion.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argSalesRegion.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argSalesRegion.ModifiedBy);
            
            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSalesRegion", param);

            string strMessage = Convert.ToString(param[14].Value);
            string strType = Convert.ToString(param[13].Value);
            string strRetValue = Convert.ToString(param[15].Value);

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
        
        public ICollection<ErrorHandler> DeleteSalesRegion(string argSalesRegionCode, int iIsDeleted, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@SalesRegionCode", argSalesRegionCode);
                param[1] = new SqlParameter("@IsDeleted", iIsDeleted);
                param[2] = new SqlParameter("@ClientCode", argClientCode);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteSalesRegion", param);
                
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
        
        public bool blnIsSalesRegionExists(string argSalesRegionCode, string argClientCode)
        {
            bool IsSalesRegionExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesRegion(argSalesRegionCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesRegionExists = true;
            }
            else
            {
                IsSalesRegionExists = false;
            }
            return IsSalesRegionExists;
        }

        public bool blnIsSalesRegionExists(string argSalesRegionCode, string argClientCode,DataAccess da)
        {
            bool IsSalesRegionExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesRegion(argSalesRegionCode, argClientCode,da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesRegionExists = true;
            }
            else
            {
                IsSalesRegionExists = false;
            }
            return IsSalesRegionExists;
        }
    }
}