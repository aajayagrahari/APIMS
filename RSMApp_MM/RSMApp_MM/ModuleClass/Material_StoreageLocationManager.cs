
//Created On :: 17, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_MM
{
    public class Material_StoreageLocationManager
    {
        const string Material_StoreageLocationTable = "Material_StoreageLocation";

        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public Material_StoreageLocation objGetMaterial_StoreageLocation(string argMaterialCode, string argPlantCode, string argStoreCode, string argClientCode)
        {
            Material_StoreageLocation argMaterial_StoreageLocation = new Material_StoreageLocation();
            DataSet DataSetToFill = new DataSet();

            if (argMaterialCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argPlantCode.Trim() == "")
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

            DataSetToFill = this.GetMaterial_StoreageLocation(argMaterialCode, argPlantCode, argStoreCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMaterial_StoreageLocation = this.objCreateMaterial_StoreageLocation((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argMaterial_StoreageLocation;
        }

        public ICollection<Material_StoreageLocation> colGetMaterial_StoreageLocation(string argMaterialCode,string argClientCode)
        {   
            List<Material_StoreageLocation> lst = new List<Material_StoreageLocation>();
            DataSet DataSetToFill = new DataSet();
            Material_StoreageLocation tMaterial_StoreageLocation = new Material_StoreageLocation();

            DataSetToFill = this.GetMaterial_StoreageLocation(argMaterialCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMaterial_StoreageLocation(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<Material_StoreageLocation> colGetMaterial_StoreageLocation(DataTable dt, string argUserName, string clientCode)
        {
            List<Material_StoreageLocation> lst = new List<Material_StoreageLocation>();
            Material_StoreageLocation objMaterial_StoreageLocation = null;
            foreach (DataRow dr in dt.Rows)
            {
                objMaterial_StoreageLocation = new Material_StoreageLocation();
                objMaterial_StoreageLocation.MaterialCode = Convert.ToString(dr["MaterialCode"]).Trim();
                objMaterial_StoreageLocation.PlantCode = Convert.ToString(dr["PlantCode"]).Trim();
                objMaterial_StoreageLocation.StoreCode = Convert.ToString(dr["StoreCode"]).Trim();
                objMaterial_StoreageLocation.UnrestrictedStock = Convert.ToInt32(dr["UnrestrictedStock"]);
                objMaterial_StoreageLocation.RestrictedStock = Convert.ToInt32(dr["RestrictedStock"]);
                objMaterial_StoreageLocation.InQltyInspection = Convert.ToInt32(dr["InQltyInspection"]);
                objMaterial_StoreageLocation.Blocked = Convert.ToInt32(dr["Blocked"]);
                objMaterial_StoreageLocation.StockInTransit = Convert.ToInt32(dr["StockInTransit"]);
                objMaterial_StoreageLocation.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objMaterial_StoreageLocation.ModifiedBy = Convert.ToString(argUserName).Trim();
                objMaterial_StoreageLocation.CreatedBy = Convert.ToString(argUserName).Trim();
                objMaterial_StoreageLocation.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objMaterial_StoreageLocation);
            }
            return lst;
        }

        public DataSet GetMaterial_StoreageLocation(string argMaterialCode, string argPlantCode, string argStoreCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@PlantCode", argPlantCode);
            param[2] = new SqlParameter("@StoreCode", argStoreCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial_StoreageLocation4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMaterial_StoreageLocation(string argMaterialCode, string argPlantCode, string argStoreCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@PlantCode", argPlantCode);
            param[2] = new SqlParameter("@StoreCode", argStoreCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMaterial_StoreageLocation4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMaterial_StoreageLocation(string argMaterialCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetMaterial_StoreageLocation",param);
            return DataSetToFill;
        }

        private Material_StoreageLocation objCreateMaterial_StoreageLocation(DataRow dr)
        {
            Material_StoreageLocation tMaterial_StoreageLocation = new Material_StoreageLocation();

            tMaterial_StoreageLocation.SetObjectInfo(dr);

            return tMaterial_StoreageLocation;

        }

        public ICollection<ErrorHandler> SaveMaterial_StoreageLocation(Material_StoreageLocation argMaterial_StoreageLocation)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsMaterial_StoreageLocationExists(argMaterial_StoreageLocation.MaterialCode, argMaterial_StoreageLocation.PlantCode, argMaterial_StoreageLocation.StoreCode, argMaterial_StoreageLocation.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertMaterial_StoreageLocation(argMaterial_StoreageLocation, da, lstErr);
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
                    UpdateMaterial_StoreageLocation(argMaterial_StoreageLocation, da, lstErr);
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

        public ICollection<ErrorHandler> SaveMaterial_StoreageLocation(ICollection<Material_StoreageLocation> colGetMaterial_StoreageLocation)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Material_StoreageLocation argMaterial_StoreageLocation in colGetMaterial_StoreageLocation)
                {
                    if (argMaterial_StoreageLocation.IsDeleted == 0)
                    {

                        if (blnIsMaterial_StoreageLocationExists(argMaterial_StoreageLocation.MaterialCode, argMaterial_StoreageLocation.PlantCode, argMaterial_StoreageLocation.StoreCode, argMaterial_StoreageLocation.ClientCode, da) == false)
                        {
                            InsertMaterial_StoreageLocation(argMaterial_StoreageLocation, da, lstErr);
                        }
                        else
                        {
                            UpdateMaterial_StoreageLocation(argMaterial_StoreageLocation, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMaterial_StoreageLocation(argMaterial_StoreageLocation.MaterialCode, argMaterial_StoreageLocation.PlantCode, argMaterial_StoreageLocation.StoreCode, argMaterial_StoreageLocation.ClientCode, argMaterial_StoreageLocation.IsDeleted);
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

        public ICollection<ErrorHandler> SaveMaterial_StoreageLocation(ICollection<Material_StoreageLocation> colGetMaterial_StoreageLocation, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Material_StoreageLocation argMaterial_StoreageLocation in colGetMaterial_StoreageLocation)
                {
                    if (argMaterial_StoreageLocation.IsDeleted == 0)
                    {
                        if (blnIsMaterial_StoreageLocationExists(argMaterial_StoreageLocation.MaterialCode, argMaterial_StoreageLocation.PlantCode, argMaterial_StoreageLocation.StoreCode, argMaterial_StoreageLocation.ClientCode, da) == false)
                        {
                            InsertMaterial_StoreageLocation(argMaterial_StoreageLocation, da, lstErr);
                        }
                        else
                        {
                            UpdateMaterial_StoreageLocation(argMaterial_StoreageLocation, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMaterial_StoreageLocation(argMaterial_StoreageLocation.MaterialCode, argMaterial_StoreageLocation.PlantCode, argMaterial_StoreageLocation.StoreCode, argMaterial_StoreageLocation.ClientCode, argMaterial_StoreageLocation.IsDeleted);
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

        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
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
                    SaveMaterial_StoreageLocation(colGetMaterial_StoreageLocation(dtExcel, argUserName, argClientCode), lstErr);

                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            break;

                        }
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

        public void InsertMaterial_StoreageLocation(Material_StoreageLocation argMaterial_StoreageLocation, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@MaterialCode", argMaterial_StoreageLocation.MaterialCode);
            param[1] = new SqlParameter("@PlantCode", argMaterial_StoreageLocation.PlantCode);
            param[2] = new SqlParameter("@StoreCode", argMaterial_StoreageLocation.StoreCode);
            param[3] = new SqlParameter("@ClientCode", argMaterial_StoreageLocation.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argMaterial_StoreageLocation.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argMaterial_StoreageLocation.ModifiedBy);
            
            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertMaterial_StoreageLocation", param);


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

        public void UpdateMaterial_StoreageLocation(Material_StoreageLocation argMaterial_StoreageLocation, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@MaterialCode", argMaterial_StoreageLocation.MaterialCode);
            param[1] = new SqlParameter("@PlantCode", argMaterial_StoreageLocation.PlantCode);
            param[2] = new SqlParameter("@StoreCode", argMaterial_StoreageLocation.StoreCode);
            param[3] = new SqlParameter("@ClientCode", argMaterial_StoreageLocation.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argMaterial_StoreageLocation.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argMaterial_StoreageLocation.ModifiedBy);

            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateMaterial_StoreageLocation", param);
            
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

        public ICollection<ErrorHandler> DeleteMaterial_StoreageLocation(string argMaterialCode, string argPlantCode, string argStoreCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
                param[1] = new SqlParameter("@PlantCode", argPlantCode);
                param[2] = new SqlParameter("@StoreCode", argStoreCode);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("IsDeleted", iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteMaterial_StoreageLocation", param);


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

        public bool blnIsMaterial_StoreageLocationExists(string argMaterialCode, string argPlantCode, string argStoreCode, string argClientCode)
        {
            bool IsMaterial_StoreageLocationExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterial_StoreageLocation(argMaterialCode, argPlantCode, argStoreCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterial_StoreageLocationExists = true;
            }
            else
            {
                IsMaterial_StoreageLocationExists = false;
            }
            return IsMaterial_StoreageLocationExists;
        }

        public bool blnIsMaterial_StoreageLocationExists(string argMaterialCode, string argPlantCode, string argStoreCode, string argClientCode,DataAccess da)
        {
            bool IsMaterial_StoreageLocationExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterial_StoreageLocation(argMaterialCode, argPlantCode, argStoreCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterial_StoreageLocationExists = true;
            }
            else
            {
                IsMaterial_StoreageLocationExists = false;
            }
            return IsMaterial_StoreageLocationExists;
        }
    }
}