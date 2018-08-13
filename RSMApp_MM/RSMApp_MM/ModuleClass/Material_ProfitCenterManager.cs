
//Created On :: 26, May, 2012
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
    public class Material_ProfitCenterManager
    {
        const string Material_ProfitCenterTable = "Material_ProfitCenter";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public Material_ProfitCenter objGetMaterial_ProfitCenter(string argProfitCenterCode, string argMaterialCode, string argPlantCode, string argClientCode)
        {
            Material_ProfitCenter argMaterial_ProfitCenter = new Material_ProfitCenter();
            DataSet DataSetToFill = new DataSet();

            if (argProfitCenterCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

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

            DataSetToFill = this.GetMaterial_ProfitCenter(argProfitCenterCode, argMaterialCode, argPlantCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMaterial_ProfitCenter = this.objCreateMaterial_ProfitCenter((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argMaterial_ProfitCenter;
        }
        
        public ICollection<Material_ProfitCenter> colGetMaterial_ProfitCenter(string argMaterialCode,string argClientCode)
        {
            List<Material_ProfitCenter> lst = new List<Material_ProfitCenter>();
            DataSet DataSetToFill = new DataSet();
            Material_ProfitCenter tMaterial_ProfitCenter = new Material_ProfitCenter();

            DataSetToFill = this.GetMaterial_ProfitCenter(argMaterialCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMaterial_ProfitCenter(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<Material_ProfitCenter> colGetMaterial_ProfitCenter(DataTable dt, string argUserName, string clientCode)
        {
            List<Material_ProfitCenter> lst = new List<Material_ProfitCenter>();
            Material_ProfitCenter objMaterial_ProfitCenter = null;
            foreach (DataRow dr in dt.Rows)
            {
                objMaterial_ProfitCenter = new Material_ProfitCenter();
                objMaterial_ProfitCenter.MaterialCode = Convert.ToString(dr["MaterialCode"]).Trim();
                objMaterial_ProfitCenter.ProfitCenterCode = Convert.ToString(dr["ProfitCenterCode"]).Trim();
                objMaterial_ProfitCenter.PlantCode = Convert.ToString(dr["PlantCode"]).Trim();
                objMaterial_ProfitCenter.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objMaterial_ProfitCenter.ModifiedBy = Convert.ToString(argUserName).Trim();
                objMaterial_ProfitCenter.CreatedBy = Convert.ToString(argUserName).Trim();
                objMaterial_ProfitCenter.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objMaterial_ProfitCenter);
            }
            return lst;
        }
        
        public DataSet GetMaterial_ProfitCenter(string argProfitCenterCode, string argMaterialCode, string argPlantCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ProfitCenterCode", argProfitCenterCode);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@PlantCode", argPlantCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial_ProfitCenter4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMaterial_ProfitCenter(string argProfitCenterCode, string argMaterialCode, string argPlantCode, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ProfitCenterCode", argProfitCenterCode);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@PlantCode", argPlantCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMaterial_ProfitCenter4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetMaterial_ProfitCenter(string argMaterialCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetMaterial_ProfitCenter",param);
            return DataSetToFill;
        }

        public DataSet GetMaterial_ProfitCenter(string argMaterialCode, string argPlantCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@PlantCode", argPlantCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetProfitCenter4Material", param);
            return DataSetToFill;
        }       
        
        private Material_ProfitCenter objCreateMaterial_ProfitCenter(DataRow dr)
        {
            Material_ProfitCenter tMaterial_ProfitCenter = new Material_ProfitCenter();

            tMaterial_ProfitCenter.SetObjectInfo(dr);

            return tMaterial_ProfitCenter;

        }
        
        public ICollection<ErrorHandler> SaveMaterial_ProfitCenter(Material_ProfitCenter argMaterial_ProfitCenter)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsMaterial_ProfitCenterExists(argMaterial_ProfitCenter.ProfitCenterCode, argMaterial_ProfitCenter.MaterialCode, argMaterial_ProfitCenter.PlantCode, argMaterial_ProfitCenter.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertMaterial_ProfitCenter(argMaterial_ProfitCenter, da, lstErr);
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
                    UpdateMaterial_ProfitCenter(argMaterial_ProfitCenter, da, lstErr);
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

        public ICollection<ErrorHandler> SaveMaterial_ProfitCenter(ICollection<Material_ProfitCenter> colGetMaterial_ProfitCenter)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Material_ProfitCenter argMaterial_ProfitCenter in colGetMaterial_ProfitCenter)
                {

                    if (argMaterial_ProfitCenter.IsDeleted == 0)
                    {

                        if (blnIsMaterial_ProfitCenterExists(argMaterial_ProfitCenter.ProfitCenterCode, argMaterial_ProfitCenter.MaterialCode, argMaterial_ProfitCenter.PlantCode, argMaterial_ProfitCenter.ClientCode, da) == false)
                        {
                            InsertMaterial_ProfitCenter(argMaterial_ProfitCenter, da, lstErr);
                        }
                        else
                        {
                            UpdateMaterial_ProfitCenter(argMaterial_ProfitCenter, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMaterial_ProfitCenter(argMaterial_ProfitCenter.ProfitCenterCode, argMaterial_ProfitCenter.MaterialCode, argMaterial_ProfitCenter.PlantCode, argMaterial_ProfitCenter.ClientCode, argMaterial_ProfitCenter.IsDeleted);
                    
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

        public ICollection<ErrorHandler> SaveMaterial_ProfitCenter(ICollection<Material_ProfitCenter> colGetMaterial_ProfitCenter, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Material_ProfitCenter argMaterial_ProfitCenter in colGetMaterial_ProfitCenter)
                {
                    if (argMaterial_ProfitCenter.IsDeleted == 0)
                    {
                        if (blnIsMaterial_ProfitCenterExists(argMaterial_ProfitCenter.ProfitCenterCode, argMaterial_ProfitCenter.MaterialCode, argMaterial_ProfitCenter.PlantCode, argMaterial_ProfitCenter.ClientCode, da) == false)
                        {
                            InsertMaterial_ProfitCenter(argMaterial_ProfitCenter, da, lstErr);
                        }
                        else
                        {
                            UpdateMaterial_ProfitCenter(argMaterial_ProfitCenter, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMaterial_ProfitCenter(argMaterial_ProfitCenter.ProfitCenterCode, argMaterial_ProfitCenter.MaterialCode, argMaterial_ProfitCenter.PlantCode, argMaterial_ProfitCenter.ClientCode, argMaterial_ProfitCenter.IsDeleted);
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
                    SaveMaterial_ProfitCenter(colGetMaterial_ProfitCenter(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertMaterial_ProfitCenter(Material_ProfitCenter argMaterial_ProfitCenter, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@ProfitCenterCode", argMaterial_ProfitCenter.ProfitCenterCode);
            param[1] = new SqlParameter("@MaterialCode", argMaterial_ProfitCenter.MaterialCode);
            param[2] = new SqlParameter("@PlantCode", argMaterial_ProfitCenter.PlantCode);
            param[3] = new SqlParameter("@ClientCode", argMaterial_ProfitCenter.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argMaterial_ProfitCenter.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argMaterial_ProfitCenter.ModifiedBy);
            
            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertMaterial_ProfitCenter", param);


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

        public void UpdateMaterial_ProfitCenter(Material_ProfitCenter argMaterial_ProfitCenter, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@ProfitCenterCode", argMaterial_ProfitCenter.ProfitCenterCode);
            param[1] = new SqlParameter("@MaterialCode", argMaterial_ProfitCenter.MaterialCode);
            param[2] = new SqlParameter("@PlantCode", argMaterial_ProfitCenter.PlantCode);
            param[3] = new SqlParameter("@ClientCode", argMaterial_ProfitCenter.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argMaterial_ProfitCenter.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argMaterial_ProfitCenter.ModifiedBy);
            
            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateMaterial_ProfitCenter", param);


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
        
        public ICollection<ErrorHandler> DeleteMaterial_ProfitCenter(string argProfitCenterCode, string argMaterialCode, string argPlantCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@ProfitCenterCode", argProfitCenterCode);
                param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
                param[2] = new SqlParameter("@PlantCode", argPlantCode);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted",iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteMaterial_ProfitCenter", param);


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
        
        public bool blnIsMaterial_ProfitCenterExists(string argProfitCenterCode, string argMaterialCode, string argPlantCode, string argClientCode)
        {
            bool IsMaterial_ProfitCenterExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterial_ProfitCenter(argProfitCenterCode, argMaterialCode, argPlantCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterial_ProfitCenterExists = true;
            }
            else
            {
                IsMaterial_ProfitCenterExists = false;
            }
            return IsMaterial_ProfitCenterExists;
        }

        public bool blnIsMaterial_ProfitCenterExists(string argProfitCenterCode, string argMaterialCode, string argPlantCode, string argClientCode,DataAccess da)
        {
            bool IsMaterial_ProfitCenterExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterial_ProfitCenter(argProfitCenterCode, argMaterialCode, argPlantCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterial_ProfitCenterExists = true;
            }
            else
            {
                IsMaterial_ProfitCenterExists = false;
            }
            return IsMaterial_ProfitCenterExists;
        }
    }
}