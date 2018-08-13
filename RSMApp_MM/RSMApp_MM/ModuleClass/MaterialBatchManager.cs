
//Created On :: 10, September, 2012
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
    public class MaterialBatchManager
    {
        const string MaterialBatchTable = "MaterialBatch";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public MaterialBatch objGetMaterialBatch(string argMaterialCode, string argPlantCode, string argClientCode)
        {
            MaterialBatch argMaterialBatch = new MaterialBatch();
            DataSet DataSetToFill = new DataSet();

            if (argMaterialCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argPlantCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetMaterialBatch(argMaterialCode, argPlantCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argMaterialBatch = this.objCreateMaterialBatch((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argMaterialBatch;
        }

        public ICollection<MaterialBatch> colGetMaterialBatch(string argClientCode)
        {
            List<MaterialBatch> lst = new List<MaterialBatch>();
            DataSet DataSetToFill = new DataSet();
            MaterialBatch tMaterialBatch = new MaterialBatch();

            DataSetToFill = this.GetMaterialBatch(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMaterialBatch(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<MaterialBatch> colGetMaterialBatch(DataTable dt, string argUserName, string clientCode)
        {
            List<MaterialBatch> lst = new List<MaterialBatch>();
            MaterialBatch objMaterialBatch = null;
            foreach (DataRow dr in dt.Rows)
            {
                objMaterialBatch = new MaterialBatch();
                objMaterialBatch.MaterialCode = Convert.ToString(dr["MaterialCode"]).Trim();
                objMaterialBatch.PlantCode = Convert.ToString(dr["PlantCode"]).Trim();
                objMaterialBatch.BatchNumber = Convert.ToString(dr["BatchNumber"]).Trim();
                objMaterialBatch.ExpiryDate = Convert.ToString(dr["ExpiryDate"]).Trim();
                objMaterialBatch.AvailabilityDate = Convert.ToString(dr["AvailabilityDate"]).Trim();
                objMaterialBatch.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objMaterialBatch.ModifiedBy = Convert.ToString(argUserName).Trim();
                objMaterialBatch.CreatedBy = Convert.ToString(argUserName).Trim();
                objMaterialBatch.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objMaterialBatch);
            }
            return lst;
        }

        public DataSet GetMaterialBatch(string argMaterialCode, string argPlantCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@PlantCode", argPlantCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialBatch4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMaterialBatch(string argMaterialCode, string argPlantCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@PlantCode", argPlantCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMaterialBatch4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMaterialBatch(string argMaterialCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);            
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialBatch4Material", param);

            return DataSetToFill;
        }

        public DataSet GetMaterialBatch(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialBatch",param);
            return DataSetToFill;
        }

        private MaterialBatch objCreateMaterialBatch(DataRow dr)
        {
            MaterialBatch tMaterialBatch = new MaterialBatch();
            tMaterialBatch.SetObjectInfo(dr);
            return tMaterialBatch;
        }

        public ICollection<ErrorHandler> SaveMaterialBatch(MaterialBatch argMaterialBatch)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsMaterialBatchExists(argMaterialBatch.MaterialCode, argMaterialBatch.PlantCode, argMaterialBatch.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertMaterialBatch(argMaterialBatch, da, lstErr);
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
                    UpdateMaterialBatch(argMaterialBatch, da, lstErr);
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

        public ICollection<ErrorHandler> SaveMaterialBatch(ICollection<MaterialBatch> colGetMaterialBatch)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (MaterialBatch argMaterialBatch in colGetMaterialBatch)
                {
                    if (argMaterialBatch.IsDeleted == 0)
                    {
                        if (blnIsMaterialBatchExists(argMaterialBatch.MaterialCode, argMaterialBatch.PlantCode, argMaterialBatch.ClientCode, da) == false)
                        {
                            InsertMaterialBatch(argMaterialBatch, da, lstErr);
                        }
                        else
                        {
                            UpdateMaterialBatch(argMaterialBatch, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMaterialBatch(argMaterialBatch.MaterialCode, argMaterialBatch.PlantCode, argMaterialBatch.ClientCode, argMaterialBatch.IsDeleted);
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

        public ICollection<ErrorHandler> SaveMaterialBatch(ICollection<MaterialBatch> colGetMaterialBatch, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (MaterialBatch argMaterialBatch in colGetMaterialBatch)
                {
                    if (argMaterialBatch.IsDeleted == 0)
                    {
                        if (blnIsMaterialBatchExists(argMaterialBatch.MaterialCode, argMaterialBatch.PlantCode, argMaterialBatch.ClientCode, da) == false)
                        {
                            InsertMaterialBatch(argMaterialBatch, da, lstErr);
                        }
                        else
                        {
                            UpdateMaterialBatch(argMaterialBatch, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMaterialBatch(argMaterialBatch.MaterialCode, argMaterialBatch.PlantCode, argMaterialBatch.ClientCode, argMaterialBatch.IsDeleted);
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
                    SaveMaterialBatch(colGetMaterialBatch(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertMaterialBatch(MaterialBatch argMaterialBatch, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@MaterialCode", argMaterialBatch.MaterialCode);
            param[1] = new SqlParameter("@PlantCode", argMaterialBatch.PlantCode);
            param[2] = new SqlParameter("@BatchNumber", argMaterialBatch.BatchNumber);
            param[3] = new SqlParameter("@ExpiryDate", argMaterialBatch.ExpiryDate);
            param[4] = new SqlParameter("@AvailabilityDate", argMaterialBatch.AvailabilityDate);
            param[5] = new SqlParameter("@ClientCode", argMaterialBatch.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argMaterialBatch.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argMaterialBatch.ModifiedBy);
    
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertMaterialBatch", param);

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

        public void UpdateMaterialBatch(MaterialBatch argMaterialBatch, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@MaterialCode", argMaterialBatch.MaterialCode);
            param[1] = new SqlParameter("@PlantCode", argMaterialBatch.PlantCode);
            param[2] = new SqlParameter("@BatchNumber", argMaterialBatch.BatchNumber);
            param[3] = new SqlParameter("@ExpiryDate", argMaterialBatch.ExpiryDate);
            param[4] = new SqlParameter("@AvailabilityDate", argMaterialBatch.AvailabilityDate);
            param[5] = new SqlParameter("@ClientCode", argMaterialBatch.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argMaterialBatch.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argMaterialBatch.ModifiedBy);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateMaterialBatch", param);

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

        public ICollection<ErrorHandler> DeleteMaterialBatch(string argMaterialCode, string argPlantCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
                param[1] = new SqlParameter("@PlantCode", argPlantCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("IsDeleted",iIsDeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteMaterialBatch", param);

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

        public bool blnIsMaterialBatchExists(string argMaterialCode, string argPlantCode, string argClientCode)
        {
            bool IsMaterialBatchExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterialBatch(argMaterialCode, argPlantCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterialBatchExists = true;
            }
            else
            {
                IsMaterialBatchExists = false;
            }
            return IsMaterialBatchExists;
        }

        public bool blnIsMaterialBatchExists(string argMaterialCode, string argPlantCode, string argClientCode, DataAccess da)
        {
            bool IsMaterialBatchExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterialBatch(argMaterialCode, argPlantCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterialBatchExists = true;
            }
            else
            {
                IsMaterialBatchExists = false;
            }
            return IsMaterialBatchExists;
        }
    }
}