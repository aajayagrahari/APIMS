
//Created On :: 23, August, 2012
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
    public class ShippingPointMasterManager
    {
        const string ShippingPointMasterTable = "ShippingPointMaster";
        // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public ShippingPointMaster objGetShippingPointMaster(string argShippingPointCode, string argClientCode)
        {
            ShippingPointMaster argShippingPointMaster = new ShippingPointMaster();
            DataSet DataSetToFill = new DataSet();

            if (argShippingPointCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetShippingPointMaster(argShippingPointCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argShippingPointMaster = this.objCreateShippingPointMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argShippingPointMaster;
        }

        public ICollection<ShippingPointMaster> colGetShippingPointMaster(string argClientCode)
        {
            List<ShippingPointMaster> lst = new List<ShippingPointMaster>();
            DataSet DataSetToFill = new DataSet();
            ShippingPointMaster tShippingPointMaster = new ShippingPointMaster();
            DataSetToFill = this.GetShippingPointMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateShippingPointMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetShippingPointMaster(string argShippingPointCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ShippingPointCode", argShippingPointCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetShippingPointMaster4ID", param);
            return DataSetToFill;
        }

        public DataSet GetShippingPointMaster(string argShippingPointCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ShippingPointCode", argShippingPointCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetShippingPointMaster4ID", param);
            return DataSetToFill;
        }

        public DataSet GetShippingPointMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetShippingPointMaster",param);
            return DataSetToFill;
        }

        public DataSet GetShippingPointMaster(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + ShippingPointMasterTable.ToString();

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

        private ShippingPointMaster objCreateShippingPointMaster(DataRow dr)
        {
            ShippingPointMaster tShippingPointMaster = new ShippingPointMaster();
            tShippingPointMaster.SetObjectInfo(dr);
            return tShippingPointMaster;
        }

        public ICollection<ErrorHandler> SaveShippingPointMaster(ShippingPointMaster argShippingPointMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsShippingPointMasterExists(argShippingPointMaster.ShippingPointCode, argShippingPointMaster.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertShippingPointMaster(argShippingPointMaster, da, lstErr);
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
                    UpdateShippingPointMaster(argShippingPointMaster, da, lstErr);
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
        public void SaveShippingPointMaster(ShippingPointMaster argShippingPointMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsShippingPointMasterExists(argShippingPointMaster.ShippingPointCode, argShippingPointMaster.ClientCode, da) == false)
                {
                    InsertShippingPointMaster(argShippingPointMaster, da, lstErr);
                }
                else
                {
                    UpdateShippingPointMaster(argShippingPointMaster, da, lstErr);
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
            ShippingPointMaster ObjShippingPointMaster = null;
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
                        ObjShippingPointMaster = new ShippingPointMaster();
                        ObjShippingPointMaster.ShippingPointCode = Convert.ToString(drExcel["ShippingPointCode"]).Trim();
                        ObjShippingPointMaster.ShippingPointName = Convert.ToString(drExcel["ShippingPointName"]).Trim();
                        ObjShippingPointMaster.ShippingDesc = Convert.ToString(drExcel["ShippingDesc"]).Trim();
                        ObjShippingPointMaster.Address1 = Convert.ToString(drExcel["Address1"]).Trim();
                        ObjShippingPointMaster.Address2 = Convert.ToString(drExcel["Address2"]).Trim();
                        ObjShippingPointMaster.CountryCode = Convert.ToString(drExcel["CountryCode"]).Trim();
                        ObjShippingPointMaster.StateCode = Convert.ToString(drExcel["StateCode"]).Trim();
                        ObjShippingPointMaster.City = Convert.ToString(drExcel["City"]).Trim();
                        ObjShippingPointMaster.PinCode = Convert.ToString(drExcel["PinCode"]).Trim();
                        ObjShippingPointMaster.TelNO = Convert.ToString(drExcel["TelNO"]).Trim();
                        ObjShippingPointMaster.MobileNo = Convert.ToString(drExcel["MobileNo"]).Trim();
                        ObjShippingPointMaster.LanguageCode = Convert.ToString(drExcel["LanguageCode"]).Trim();
                        ObjShippingPointMaster.CreatedBy = Convert.ToString(argUserName);
                        ObjShippingPointMaster.ModifiedBy = Convert.ToString(argUserName);
                        ObjShippingPointMaster.ClientCode = Convert.ToString(argClientCode);
                        SaveShippingPointMaster(ObjShippingPointMaster, da, lstErr);

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
        /************/  

        public void InsertShippingPointMaster(ShippingPointMaster argShippingPointMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@ShippingPointCode", argShippingPointMaster.ShippingPointCode);
            param[1] = new SqlParameter("@ShippingPointName", argShippingPointMaster.ShippingPointName);
            param[2] = new SqlParameter("@ShippingDesc", argShippingPointMaster.ShippingDesc);
            param[3] = new SqlParameter("@Address1", argShippingPointMaster.Address1);
            param[4] = new SqlParameter("@Address2", argShippingPointMaster.Address2);
            param[5] = new SqlParameter("@CountryCode", argShippingPointMaster.CountryCode);
            param[6] = new SqlParameter("@StateCode", argShippingPointMaster.StateCode);
            param[7] = new SqlParameter("@City", argShippingPointMaster.City);
            param[8] = new SqlParameter("@PinCode", argShippingPointMaster.PinCode);
            param[9] = new SqlParameter("@TelNO", argShippingPointMaster.TelNO);
            param[10] = new SqlParameter("@MobileNo", argShippingPointMaster.MobileNo);
            param[11] = new SqlParameter("@LanguageCode", argShippingPointMaster.LanguageCode);
            param[12] = new SqlParameter("@ClientCode", argShippingPointMaster.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argShippingPointMaster.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argShippingPointMaster.ModifiedBy);
   
            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertShippingPointMaster", param);
            
            string strMessage = Convert.ToString(param[16].Value);
            string strType = Convert.ToString(param[15].Value);
            string strRetValue = Convert.ToString(param[17].Value);

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

        public void UpdateShippingPointMaster(ShippingPointMaster argShippingPointMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@ShippingPointCode", argShippingPointMaster.ShippingPointCode);
            param[1] = new SqlParameter("@ShippingPointName", argShippingPointMaster.ShippingPointName);
            param[2] = new SqlParameter("@ShippingDesc", argShippingPointMaster.ShippingDesc);
            param[3] = new SqlParameter("@Address1", argShippingPointMaster.Address1);
            param[4] = new SqlParameter("@Address2", argShippingPointMaster.Address2);
            param[5] = new SqlParameter("@CountryCode", argShippingPointMaster.CountryCode);
            param[6] = new SqlParameter("@StateCode", argShippingPointMaster.StateCode);
            param[7] = new SqlParameter("@City", argShippingPointMaster.City);
            param[8] = new SqlParameter("@PinCode", argShippingPointMaster.PinCode);
            param[9] = new SqlParameter("@TelNO", argShippingPointMaster.TelNO);
            param[10] = new SqlParameter("@MobileNo", argShippingPointMaster.MobileNo);
            param[11] = new SqlParameter("@LanguageCode", argShippingPointMaster.LanguageCode);
            param[12] = new SqlParameter("@ClientCode", argShippingPointMaster.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argShippingPointMaster.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argShippingPointMaster.ModifiedBy);

            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateShippingPointMaster", param);

            string strMessage = Convert.ToString(param[16].Value);
            string strType = Convert.ToString(param[15].Value);
            string strRetValue = Convert.ToString(param[17].Value);

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

        public ICollection<ErrorHandler> DeleteShippingPointMaster(string argShippingPointCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@ShippingPointCode", argShippingPointCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteShippingPointMaster", param);
                
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

        public bool blnIsShippingPointMasterExists(string argShippingPointCode, string argClientCode)
        {
            bool IsShippingPointMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetShippingPointMaster(argShippingPointCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsShippingPointMasterExists = true;
            }
            else
            {
                IsShippingPointMasterExists = false;
            }
            return IsShippingPointMasterExists;
        }

        public bool blnIsShippingPointMasterExists(string argShippingPointCode, string argClientCode, DataAccess da)
        {
            bool IsShippingPointMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetShippingPointMaster(argShippingPointCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsShippingPointMasterExists = true;
            }
            else
            {
                IsShippingPointMasterExists = false;
            }
            return IsShippingPointMasterExists;
        }
    }
}