
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
    public class Material_PlantManager
    {
        const string Material_PlantTable = "Material_Plant";

        // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public Material_Plant objGetMaterial_Plant(string argMaterialCode, string argPlantCode, string argClientCode)
        {
            Material_Plant argMaterial_Plant = new Material_Plant();
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

            DataSetToFill = this.GetMaterial_Plant(argMaterialCode, argPlantCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMaterial_Plant = this.objCreateMaterial_Plant((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argMaterial_Plant;
        }
        
        public ICollection<Material_Plant> colGetMaterial_Plant(string argMaterialCode,string argClientCode)
        {
            List<Material_Plant> lst = new List<Material_Plant>();
            DataSet DataSetToFill = new DataSet();
            Material_Plant tMaterial_Plant = new Material_Plant();

            DataSetToFill = this.GetMaterial_Plant(argMaterialCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMaterial_Plant(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetMaterial_Plant(string argMaterialCode, string argPlantCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@PlantCode", argPlantCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial_Plant4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMaterial_Plant(string argMaterialCode, string argPlantCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@PlantCode", argPlantCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMaterial_Plant4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetMaterial_Plant(string argMaterialCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetMaterial_Plant",param);
            return DataSetToFill;
        }
        
        private Material_Plant objCreateMaterial_Plant(DataRow dr)
        {
            Material_Plant tMaterial_Plant = new Material_Plant();

            tMaterial_Plant.SetObjectInfo(dr);

            return tMaterial_Plant;

        }
        
        public ICollection<ErrorHandler> SaveMaterial_Plant(Material_Plant argMaterial_Plant)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsMaterial_PlantExists(argMaterial_Plant.MaterialCode, argMaterial_Plant.PlantCode, argMaterial_Plant.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertMaterial_Plant(argMaterial_Plant, da, lstErr);
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

                    objErrorHandler.Type = ErrorConstant.strErrType;
                    objErrorHandler.MsgId = 0;
                    objErrorHandler.Module = ErrorConstant.strInsertModule;
                    objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                    objErrorHandler.Message = "Material with this plant alerady exists.";
                    objErrorHandler.RowNo = 0;
                    objErrorHandler.FieldName = "";
                    objErrorHandler.LogCode = "";
                    lstErr.Add(objErrorHandler);

              
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

        public ICollection<ErrorHandler> SaveMaterial_Plant(ICollection<Material_Plant> colGetMaterial_Plant)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Material_Plant argMaterial_Plant in colGetMaterial_Plant)
                {
                    if (argMaterial_Plant.IsDeleted == 0)
                    {
                        if (blnIsMaterial_PlantExists(argMaterial_Plant.MaterialCode, argMaterial_Plant.PlantCode, argMaterial_Plant.ClientCode, da) == false)
                        {
                            InsertMaterial_Plant(argMaterial_Plant, da, lstErr);
                        }
                        else
                        {
                            UpdateMaterial_Plant(argMaterial_Plant, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMaterial_Plant(argMaterial_Plant.MaterialCode, argMaterial_Plant.PlantCode, argMaterial_Plant.ClientCode, argMaterial_Plant.IsDeleted);
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

        public ICollection<ErrorHandler> SaveMaterial_Plant(ICollection<Material_Plant> colGetMaterial_Plant, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Material_Plant argMaterial_Plant in colGetMaterial_Plant)
                {
                    if (argMaterial_Plant.IsDeleted == 0)
                    {
                        if (blnIsMaterial_PlantExists(argMaterial_Plant.MaterialCode, argMaterial_Plant.PlantCode, argMaterial_Plant.ClientCode, da) == false)
                        {
                            InsertMaterial_Plant(argMaterial_Plant, da, lstErr);
                        }
                        else
                        {
                            UpdateMaterial_Plant(argMaterial_Plant, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMaterial_Plant(argMaterial_Plant.MaterialCode, argMaterial_Plant.PlantCode, argMaterial_Plant.ClientCode, argMaterial_Plant.IsDeleted);
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
                    SaveMaterial_Plant(ColMaterial_plant(dtExcel,argUserName,argClientCode),lstErr);
                    
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

        public ICollection<Material_Plant> ColMaterial_plant(DataTable dt, string argUserName, string clientCode)
        {
            List<Material_Plant> lst = new List<Material_Plant>();
            Material_Plant objMaterial_Plant = null;
            foreach (DataRow dr in dt.Rows)
            {
                objMaterial_Plant = new Material_Plant();
                objMaterial_Plant.MaterialCode = Convert.ToString(dr["MaterialCode"]).Trim();
                objMaterial_Plant.PlantCode = Convert.ToString(dr["PlantCode"]).Trim();
                objMaterial_Plant.CompanyCode = Convert.ToString(dr["CompanyCode"]).Trim();
                objMaterial_Plant.ProfitCenterCode = Convert.ToString(dr["ProfitCenterCode"]).Trim();
                objMaterial_Plant.PurchaseGroupCode = Convert.ToString(dr["PurchaseGroupCode"]).Trim();
                objMaterial_Plant.SNProfileCode = Convert.ToString(dr["SNProfileCode"]).Trim();
                objMaterial_Plant.ValCatCode = Convert.ToString(dr["ValCatCode"]).Trim();
                objMaterial_Plant.IsAutoPO = Convert.ToInt32(dr["IsAutoPO"]);
                objMaterial_Plant.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objMaterial_Plant.ModifiedBy = Convert.ToString(argUserName).Trim();
                objMaterial_Plant.CreatedBy = Convert.ToString(argUserName).Trim();
                objMaterial_Plant.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objMaterial_Plant);
            }
            return lst;
        }

        public void InsertMaterial_Plant(Material_Plant argMaterial_Plant, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@MaterialCode", argMaterial_Plant.MaterialCode);
            param[1] = new SqlParameter("@PlantCode", argMaterial_Plant.PlantCode);
            param[2] = new SqlParameter("@ClientCode", argMaterial_Plant.ClientCode);
            param[3] = new SqlParameter("@ProfitCenterCode",argMaterial_Plant.ProfitCenterCode);
            param[4] = new SqlParameter("@CompanyCode",argMaterial_Plant.CompanyCode);
            param[5] = new SqlParameter("@IsAutoPO", argMaterial_Plant.IsAutoPO);
            param[6] = new SqlParameter("@PurchaseGroupCode", argMaterial_Plant.PurchaseGroupCode);
            param[7] = new SqlParameter("@SNProfileCode", argMaterial_Plant.SNProfileCode);
            param[8] = new SqlParameter("@ValCatCode", argMaterial_Plant.ValCatCode);
            param[9] = new SqlParameter("@CreatedBy", argMaterial_Plant.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argMaterial_Plant.ModifiedBy);
            
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertMaterial_Plant", param);

            string strMessage = Convert.ToString(param[12].Value);
            string strType = Convert.ToString(param[11].Value);
            string strRetValue = Convert.ToString(param[13].Value);
            
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
        
        public void UpdateMaterial_Plant(Material_Plant argMaterial_Plant, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@MaterialCode", argMaterial_Plant.MaterialCode);
            param[1] = new SqlParameter("@PlantCode", argMaterial_Plant.PlantCode);
            param[2] = new SqlParameter("@ClientCode", argMaterial_Plant.ClientCode);
            param[3] = new SqlParameter("@ProfitCenterCode", argMaterial_Plant.ProfitCenterCode);
            param[4] = new SqlParameter("@CompanyCode", argMaterial_Plant.CompanyCode);
            param[5] = new SqlParameter("@IsAutoPO", argMaterial_Plant.IsAutoPO);
            param[6] = new SqlParameter("@PurchaseGroupCode", argMaterial_Plant.PurchaseGroupCode);
            param[7] = new SqlParameter("@SNProfileCode", argMaterial_Plant.SNProfileCode);
            param[8] = new SqlParameter("@argValCatCode", argMaterial_Plant.ValCatCode);
            param[9] = new SqlParameter("@CreatedBy", argMaterial_Plant.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argMaterial_Plant.ModifiedBy);

            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateMaterial_Plant", param);


            string strMessage = Convert.ToString(param[12].Value);
            string strType = Convert.ToString(param[11].Value);
            string strRetValue = Convert.ToString(param[13].Value);


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
        
        public ICollection<ErrorHandler> DeleteMaterial_Plant(string argMaterialCode, string argPlantCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
                param[1] = new SqlParameter("@PlantCode", argPlantCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] =new SqlParameter("@IsDeleted",iIsDeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteMaterial_Plant", param);


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
        
        public bool blnIsMaterial_PlantExists(string argMaterialCode, string argPlantCode, string argClientCode)
        {
            bool IsMaterial_PlantExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterial_Plant(argMaterialCode, argPlantCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterial_PlantExists = true;
            }
            else
            {
                IsMaterial_PlantExists = false;
            }
            return IsMaterial_PlantExists;
        }

        public bool blnIsMaterial_PlantExists(string argMaterialCode, string argPlantCode, string argClientCode,DataAccess da)
        {
            bool IsMaterial_PlantExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterial_Plant(argMaterialCode, argPlantCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterial_PlantExists = true;
            }
            else
            {
                IsMaterial_PlantExists = false;
            }
            return IsMaterial_PlantExists;
        }
    }
}