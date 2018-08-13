
//Created On :: 15, May, 2012
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
    public class PlantMasterManager
    {
        const string PlantMasterTable = "PlantMaster";
        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public PlantMaster objGetPlantMaster(string argPlantCode, string argClientCode)
        {
            PlantMaster argPlantMaster = new PlantMaster();
            DataSet DataSetToFill = new DataSet();

            if (argPlantCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetPlantMaster(argPlantCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argPlantMaster = this.objCreatePlantMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argPlantMaster;
        }
        
        public ICollection<PlantMaster> colGetPlantMaster(string argClientCode)
        {
            List<PlantMaster> lst = new List<PlantMaster>();
            DataSet DataSetToFill = new DataSet();
            PlantMaster tPlantMaster = new PlantMaster();
            DataSetToFill = this.GetPlantMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePlantMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }
        
        public DataSet GetPlantMaster(string argPrefix, string argMaterialCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode.Trim());
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetPlantMaster4Combo", param);
            return DataSetToFill;
        }

        public DataSet GetPlant4Combo(string argPrefix, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetPlant4Combo", param);
            return DataSetToFill;
        }
        
        public DataSet GetPlantMaster(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + PlantMasterTable.ToString();

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

        public DataSet GetPlantMaster(string argPlantCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PlantCode", argPlantCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPlantMaster4ID", param);
            return DataSetToFill;
        }

        public DataSet GetPlant4ShippingPoint(string argShippingPointCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ShippingPointCode", argShippingPointCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetPlantMaster4ShippingPoint", param);
            return DataSetToFill;
        }

        public DataSet GetPlantMaster(string argPlantCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PlantCode", argPlantCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPlantMaster4ID", param);
            return DataSetToFill;
        }
        
        public DataSet GetPlantMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPlantMaster", param);
            return DataSetToFill;
        }
        
        public DataSet GetPlantState(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetPlantState4Combo", param);
            return DataSetToFill;
        }
        
        private PlantMaster objCreatePlantMaster(DataRow dr)
        {
            PlantMaster tPlantMaster = new PlantMaster();
            tPlantMaster.SetObjectInfo(dr);
            return tPlantMaster;
        }
        
        public ICollection<ErrorHandler> SavePlantMaster(PlantMaster argPlantMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPlantMasterExists(argPlantMaster.PlantCode, argPlantMaster.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPlantMaster(argPlantMaster, da, lstErr);
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
                    UpdatePlantMaster(argPlantMaster, da, lstErr);
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
        public void SavePlantMaster(PlantMaster argPlantMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsPlantMasterExists(argPlantMaster.PlantCode, argPlantMaster.ClientCode, da) == false)
                {
                    InsertPlantMaster(argPlantMaster, da, lstErr);
                }
                else
                {
                    UpdatePlantMaster(argPlantMaster, da, lstErr);
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
            PlantMaster ObjPlantMaster = null;
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
                        ObjPlantMaster = new PlantMaster();
                        ObjPlantMaster.PlantCode = Convert.ToString(drExcel["PlantCode"]).Trim();
                        ObjPlantMaster.PlantName = Convert.ToString(drExcel["PlantName"]).Trim();
                        ObjPlantMaster.PlantDesc = Convert.ToString(drExcel["PlantDesc"]).Trim();
                        ObjPlantMaster.Address1 = Convert.ToString(drExcel["Address1"]).Trim();
                        ObjPlantMaster.Address2 = Convert.ToString(drExcel["Address2"]).Trim();
                        ObjPlantMaster.CountryCode = Convert.ToString(drExcel["CountryCode"]).Trim();
                        ObjPlantMaster.StateCode = Convert.ToString(drExcel["StateCode"]).Trim();
                        ObjPlantMaster.City = Convert.ToString(drExcel["City"]).Trim();
                        ObjPlantMaster.PinCode = Convert.ToString(drExcel["PinCode"]).Trim();
                        ObjPlantMaster.TelNo = Convert.ToString(drExcel["TelNo"]).Trim();
                        ObjPlantMaster.MobileNo = Convert.ToString(drExcel["MobileNo"]).Trim();
                        ObjPlantMaster.LanguageCode = Convert.ToString(drExcel["LanguageCode"]).Trim();
                        ObjPlantMaster.CreatedBy = Convert.ToString(argUserName);
                        ObjPlantMaster.ModifiedBy = Convert.ToString(argUserName);
                        ObjPlantMaster.ClientCode = Convert.ToString(argClientCode);
                        SavePlantMaster(ObjPlantMaster, da, lstErr);

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

        public void InsertPlantMaster(PlantMaster argPlantMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@PlantCode", argPlantMaster.PlantCode);
            param[1] = new SqlParameter("@PlantName", argPlantMaster.PlantName);
            param[2] = new SqlParameter("@PlantDesc", argPlantMaster.PlantDesc);
            param[3] = new SqlParameter("@Address1", argPlantMaster.Address1);
            param[4] = new SqlParameter("@Address2", argPlantMaster.Address2);
            param[5] = new SqlParameter("@CountryCode", argPlantMaster.CountryCode);
            param[6] = new SqlParameter("@StateCode", argPlantMaster.StateCode);
            param[7] = new SqlParameter("@City", argPlantMaster.City);
            param[8] = new SqlParameter("@PinCode", argPlantMaster.PinCode);
            param[9] = new SqlParameter("@TelNo", argPlantMaster.TelNo);
            param[10] = new SqlParameter("@MobileNo", argPlantMaster.MobileNo);
            param[11] = new SqlParameter("@LanguageCode", argPlantMaster.LanguageCode);
            param[12] = new SqlParameter("@ClientCode", argPlantMaster.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argPlantMaster.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argPlantMaster.ModifiedBy);
           
            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPlantMaster", param);


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
            lstErr.Add(objErrorHandler);

        }
        
        public void UpdatePlantMaster(PlantMaster argPlantMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@PlantCode", argPlantMaster.PlantCode);
            param[1] = new SqlParameter("@PlantName", argPlantMaster.PlantName);
            param[2] = new SqlParameter("@PlantDesc", argPlantMaster.PlantDesc);
            param[3] = new SqlParameter("@Address1", argPlantMaster.Address1);
            param[4] = new SqlParameter("@Address2", argPlantMaster.Address2);
            param[5] = new SqlParameter("@CountryCode", argPlantMaster.CountryCode);
            param[6] = new SqlParameter("@StateCode", argPlantMaster.StateCode);
            param[7] = new SqlParameter("@City", argPlantMaster.City);
            param[8] = new SqlParameter("@PinCode", argPlantMaster.PinCode);
            param[9] = new SqlParameter("@TelNo", argPlantMaster.TelNo);
            param[10] = new SqlParameter("@MobileNo", argPlantMaster.MobileNo);
            param[11] = new SqlParameter("@LanguageCode", argPlantMaster.LanguageCode);
            param[12] = new SqlParameter("@ClientCode", argPlantMaster.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argPlantMaster.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argPlantMaster.ModifiedBy);
            
            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePlantMaster", param);


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
            lstErr.Add(objErrorHandler);

        }
        
        public ICollection<ErrorHandler> DeletePlantMaster(string argPlantCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@PlantCode", argPlantCode);
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

                int i = da.ExecuteNonQuery("Proc_DeletePlantMaster", param);


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
        
        public bool blnIsPlantMasterExists(string argPlantCode, string argClientCode)
        {
            bool IsPlantMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetPlantMaster(argPlantCode, argClientCode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPlantMasterExists = true;
            }
            else
            {
                IsPlantMasterExists = false;
            }
            return IsPlantMasterExists;
        }

        public bool blnIsPlantMasterExists(string argPlantCode, string argClientCode,DataAccess da)
        {
            bool IsPlantMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetPlantMaster(argPlantCode, argClientCode,da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPlantMasterExists = true;
            }
            else
            {
                IsPlantMasterExists = false;
            }
            return IsPlantMasterExists;
        }
    }
}