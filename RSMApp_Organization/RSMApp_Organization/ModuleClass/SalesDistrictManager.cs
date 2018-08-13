
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
    public class SalesDistrictManager
    {
        const string SalesDistrictTable = "SalesDistrict";
        ErrorHandler objErrorHandler = new ErrorHandler();

        public SalesDistrict objGetSalesDistrict(string argSalesDistrictCode, string argClientCode)
        {
            SalesDistrict argSalesDistrict = new SalesDistrict();
            DataSet DataSetToFill = new DataSet();

            if (argSalesDistrictCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetSalesDistrict(argSalesDistrictCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argSalesDistrict = this.objCreateSalesDistrict((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argSalesDistrict;
        }

        public ICollection<SalesDistrict> colGetSalesDistrict(string argClientCode)
        {
            List<SalesDistrict> lst = new List<SalesDistrict>();
            DataSet DataSetToFill = new DataSet();
            SalesDistrict tSalesDistrict = new SalesDistrict();
            DataSetToFill = this.GetSalesDistrict(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSalesDistrict(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }
        
        public DataSet GetSalesDistrict(string argSalesDistrictCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SalesDistrictCode", argSalesDistrictCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesDistrict4ID", param);
            return DataSetToFill;
        }

        public DataSet GetSalesDistrict(string argSalesDistrictCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();
            
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SalesDistrictCode", argSalesDistrictCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSalesDistrict4ID", param);
            return DataSetToFill;
        }

        public DataSet GetSalesDistrict(int iIsDeleted)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + SalesDistrictTable.ToString();

                if (iIsDeleted > -1)
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

        public DataSet GetSalesDistrict(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesDistrict", param);
            return DataSetToFill;
        }

        public DataSet GetSalesDistrict(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + SalesDistrictTable.ToString();

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
        
        public DataSet GetSalesDistrict4Combo(string argPrefix, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetSalesDistrict4Combo", param);
            return DataSetToFill;
        }
        
        private SalesDistrict objCreateSalesDistrict(DataRow dr)
        {
            SalesDistrict tSalesDistrict = new SalesDistrict();
            tSalesDistrict.SetObjectInfo(dr);
            return tSalesDistrict;
        }
        
        public ICollection<ErrorHandler> SaveSalesDistrict(SalesDistrict argSalesDistrict)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsSalesDistrictExists(argSalesDistrict.SalesDistrictCode, argSalesDistrict.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertSalesDistrict(argSalesDistrict, da, lstErr);
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
                    UpdateSalesDistrict(argSalesDistrict, da, lstErr);
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

        #region Bulk Insert
        public void SaveSalesDistrict(SalesDistrict argSalesDistrict, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsSalesDistrictExists(argSalesDistrict.SalesDistrictCode, argSalesDistrict.ClientCode, da) == false)
                {
                    InsertSalesDistrict(argSalesDistrict, da, lstErr);
                }
                else
                {
                    UpdateSalesDistrict(argSalesDistrict, da, lstErr);
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
            SalesDistrict ObjSalesDistrict = null;
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
                        ObjSalesDistrict = new SalesDistrict();
                        ObjSalesDistrict.SalesDistrictCode = Convert.ToString(drExcel["SalesDistrictCode"]).Trim();
                        ObjSalesDistrict.SalesDistrictName = Convert.ToString(drExcel["SalesDistrictName"]).Trim();
                        ObjSalesDistrict.Address1 = Convert.ToString(drExcel["Address1"]).Trim();
                        ObjSalesDistrict.Address2 = Convert.ToString(drExcel["Address2"]).Trim();
                        ObjSalesDistrict.CountryCode = Convert.ToString(drExcel["CountryCode"]).Trim();
                        ObjSalesDistrict.StateCode = Convert.ToString(drExcel["StateCode"]).Trim();
                        ObjSalesDistrict.City = Convert.ToString(drExcel["City"]).Trim();
                        ObjSalesDistrict.PinCode = Convert.ToString(drExcel["PinCode"]).Trim();
                        ObjSalesDistrict.TelNO = Convert.ToString(drExcel["TelNO"]).Trim();
                        ObjSalesDistrict.EmailID = Convert.ToString(drExcel["EmailID"]).Trim();
                        ObjSalesDistrict.CreatedBy = Convert.ToString(argUserName);
                        ObjSalesDistrict.ModifiedBy = Convert.ToString(argUserName);
                        ObjSalesDistrict.ClientCode = Convert.ToString(argClientCode);
                        SaveSalesDistrict(ObjSalesDistrict, da, lstErr);

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

        public void InsertSalesDistrict(SalesDistrict argSalesDistrict, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@SalesDistrictCode", argSalesDistrict.SalesDistrictCode);
            param[1] = new SqlParameter("@SalesDistrictName", argSalesDistrict.SalesDistrictName);
            param[2] = new SqlParameter("@Address1", argSalesDistrict.Address1);
            param[3] = new SqlParameter("@Address2", argSalesDistrict.Address2);
            param[4] = new SqlParameter("@CountryCode", argSalesDistrict.CountryCode);
            param[5] = new SqlParameter("@StateCode", argSalesDistrict.StateCode);
            param[6] = new SqlParameter("@City", argSalesDistrict.City);
            param[7] = new SqlParameter("@PinCode", argSalesDistrict.PinCode);
            param[8] = new SqlParameter("@TelNO", argSalesDistrict.TelNO);
            param[9] = new SqlParameter("@EmailID", argSalesDistrict.EmailID);
            param[10] = new SqlParameter("@ClientCode", argSalesDistrict.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argSalesDistrict.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argSalesDistrict.ModifiedBy);
            
            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSalesDistrict", param);

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
        
        public void UpdateSalesDistrict(SalesDistrict argSalesDistrict, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@SalesDistrictCode", argSalesDistrict.SalesDistrictCode);
            param[1] = new SqlParameter("@SalesDistrictName", argSalesDistrict.SalesDistrictName);
            param[2] = new SqlParameter("@Address1", argSalesDistrict.Address1);
            param[3] = new SqlParameter("@Address2", argSalesDistrict.Address2);
            param[4] = new SqlParameter("@CountryCode", argSalesDistrict.CountryCode);
            param[5] = new SqlParameter("@StateCode", argSalesDistrict.StateCode);
            param[6] = new SqlParameter("@City", argSalesDistrict.City);
            param[7] = new SqlParameter("@PinCode", argSalesDistrict.PinCode);
            param[8] = new SqlParameter("@TelNO", argSalesDistrict.TelNO);
            param[9] = new SqlParameter("@EmailID", argSalesDistrict.EmailID);
            param[10] = new SqlParameter("@ClientCode", argSalesDistrict.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argSalesDistrict.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argSalesDistrict.ModifiedBy);
            
            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSalesDistrict", param);

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
        
        public ICollection<ErrorHandler> DeleteSalesDistrict(string argSalesDistrictCode, int iIsDeleted, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@SalesDistrictCode", argSalesDistrictCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteSalesDistrict", param);

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
        
        public bool blnIsSalesDistrictExists(string argSalesDistrictCode, string argClientcode)
        {
            bool IsSalesDistrictExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesDistrict(argSalesDistrictCode, argClientcode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesDistrictExists = true;
            }
            else
            {
                IsSalesDistrictExists = false;
            }
            return IsSalesDistrictExists;
        }

        public bool blnIsSalesDistrictExists(string argSalesDistrictCode, string argClientcode,DataAccess da)
        {
            bool IsSalesDistrictExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesDistrict(argSalesDistrictCode, argClientcode,da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesDistrictExists = true;
            }
            else
            {
                IsSalesDistrictExists = false;
            }
            return IsSalesDistrictExists;
        }
    }
}