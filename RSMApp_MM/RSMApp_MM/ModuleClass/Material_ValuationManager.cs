
//Created On :: 11, September, 2012
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
    public class Material_ValuationManager
    {
        const string Material_ValuationTable = "Material_Valuation";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public Material_Valuation objGetMaterial_Valuation(string argMaterialCode, string argPlantCode, string argValTypeCode, string argValCatCode, string argClientCode)
        {
            Material_Valuation argMaterial_Valuation = new Material_Valuation();
            DataSet DataSetToFill = new DataSet();

            if (argMaterialCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argPlantCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argValTypeCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argValCatCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            DataSetToFill = this.GetMaterial_Valuation(argMaterialCode, argPlantCode, argValTypeCode, argValCatCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMaterial_Valuation = this.objCreateMaterial_Valuation((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandler:

        Finish:
            DataSetToFill = null;


            return argMaterial_Valuation;
        }

        public ICollection<Material_Valuation> colGetMaterial_Valuation(string argMaterialCode,string argClientCode)
        {
            List<Material_Valuation> lst = new List<Material_Valuation>();
            DataSet DataSetToFill = new DataSet();
            Material_Valuation tMaterial_Valuation = new Material_Valuation();

            DataSetToFill = this.GetMaterial_Valuation(argMaterialCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMaterial_Valuation(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<Material_Valuation> colGetMaterial_Valuation(DataTable dt, string argUserName, string clientCode)
        {
            List<Material_Valuation> lst = new List<Material_Valuation>();
            Material_Valuation objMaterial_Valuation = null;
            foreach (DataRow dr in dt.Rows)
            {
                objMaterial_Valuation = new Material_Valuation();
                objMaterial_Valuation.MaterialCode = Convert.ToString(dr["MaterialCode"]).Trim();
                objMaterial_Valuation.PlantCode = Convert.ToString(dr["PlantCode"]).Trim();
                objMaterial_Valuation.ValTypeCode = Convert.ToString(dr["ValTypeCode"]).Trim();
                objMaterial_Valuation.ValCatCode = Convert.ToString(dr["ValCatCode"]).Trim();
                objMaterial_Valuation.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objMaterial_Valuation.ModifiedBy = Convert.ToString(argUserName).Trim();
                objMaterial_Valuation.CreatedBy = Convert.ToString(argUserName).Trim();
                objMaterial_Valuation.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objMaterial_Valuation);
            }
            return lst;
        }

        public DataSet GetMaterial_Valuation(string argMaterialCode, string argPlantCode, string argValTypeCode, string argValCatCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@PlantCode", argPlantCode);
            param[2] = new SqlParameter("@ValTypeCode", argValTypeCode);
            param[3] = new SqlParameter("@ValCatCode", argValCatCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial_Valuation4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMaterial_Valuation(string argMaterialCode, string argPlantCode, string argValTypeCode, string argValCatCode, string argClientCode,DataAccess da)
        {
           
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@PlantCode", argPlantCode);
            param[2] = new SqlParameter("@ValTypeCode", argValTypeCode);
            param[3] = new SqlParameter("@ValCatCode", argValCatCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMaterial_Valuation4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMaterial_Valuation(string argMaterialCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetMaterial_Valuation", param);
            return DataSetToFill;
        }

        private Material_Valuation objCreateMaterial_Valuation(DataRow dr)
        {
            Material_Valuation tMaterial_Valuation = new Material_Valuation();

            tMaterial_Valuation.SetObjectInfo(dr);

            return tMaterial_Valuation;

        }

        public ICollection<ErrorHandler> SaveMaterial_Valuation(Material_Valuation argMaterial_Valuation)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsMaterial_ValuationExists(argMaterial_Valuation.MaterialCode, argMaterial_Valuation.PlantCode, argMaterial_Valuation.ValTypeCode, argMaterial_Valuation.ValCatCode, argMaterial_Valuation.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertMaterial_Valuation(argMaterial_Valuation, da, lstErr);
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
                    UpdateMaterial_Valuation(argMaterial_Valuation, da, lstErr);
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

        public ICollection<ErrorHandler> SaveMaterial_Valuation(ICollection<Material_Valuation> colGetMaterial_Valuation)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Material_Valuation argMaterial_Valuation in colGetMaterial_Valuation)
                {
                    if (argMaterial_Valuation.IsDeleted == 0)
                    {

                        if (blnIsMaterial_ValuationExists(argMaterial_Valuation.MaterialCode, argMaterial_Valuation.PlantCode, argMaterial_Valuation.ValTypeCode, argMaterial_Valuation.ValCatCode, argMaterial_Valuation.ClientCode, da) == false)
                        {
                            InsertMaterial_Valuation(argMaterial_Valuation, da, lstErr);
                        }
                        else
                        {
                            UpdateMaterial_Valuation(argMaterial_Valuation, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMaterial_Valuation(argMaterial_Valuation.MaterialCode, argMaterial_Valuation.PlantCode, argMaterial_Valuation.ValTypeCode, argMaterial_Valuation.ValCatCode, argMaterial_Valuation.ClientCode, argMaterial_Valuation.IsDeleted);
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

        public ICollection<ErrorHandler> SaveMaterial_Valuation(ICollection<Material_Valuation> colGetMaterial_Valuation, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Material_Valuation argMaterial_Valuation in colGetMaterial_Valuation)
                {
                    if (argMaterial_Valuation.IsDeleted == 0)
                    {
                        if (blnIsMaterial_ValuationExists(argMaterial_Valuation.MaterialCode, argMaterial_Valuation.PlantCode, argMaterial_Valuation.ValTypeCode, argMaterial_Valuation.ValCatCode, argMaterial_Valuation.ClientCode, da) == false)
                        {
                            InsertMaterial_Valuation(argMaterial_Valuation, da, lstErr);
                        }
                        else
                        {
                            UpdateMaterial_Valuation(argMaterial_Valuation, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMaterial_Valuation(argMaterial_Valuation.MaterialCode, argMaterial_Valuation.PlantCode, argMaterial_Valuation.ValTypeCode, argMaterial_Valuation.ValCatCode, argMaterial_Valuation.ClientCode, argMaterial_Valuation.IsDeleted);
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
                    SaveMaterial_Valuation(colGetMaterial_Valuation(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertMaterial_Valuation(Material_Valuation argMaterial_Valuation, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];

            param[0] = new SqlParameter("@MaterialCode", argMaterial_Valuation.MaterialCode);
            param[1] = new SqlParameter("@PlantCode", argMaterial_Valuation.PlantCode);
            param[2] = new SqlParameter("@ValTypeCode", argMaterial_Valuation.ValTypeCode);
            param[3] = new SqlParameter("@ValCatCode", argMaterial_Valuation.ValCatCode);
            param[4] = new SqlParameter("@ClientCode", argMaterial_Valuation.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argMaterial_Valuation.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argMaterial_Valuation.ModifiedBy);

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertMaterial_Valuation", param);


            string strMessage = Convert.ToString(param[8].Value);
            string strType = Convert.ToString(param[7].Value);
            string strRetValue = Convert.ToString(param[9].Value);
            
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

        public void UpdateMaterial_Valuation(Material_Valuation argMaterial_Valuation, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];

            param[0] = new SqlParameter("@MaterialCode", argMaterial_Valuation.MaterialCode);
            param[1] = new SqlParameter("@PlantCode", argMaterial_Valuation.PlantCode);
            param[2] = new SqlParameter("@ValTypeCode", argMaterial_Valuation.ValTypeCode);
            param[3] = new SqlParameter("@ValCatCode", argMaterial_Valuation.ValCatCode);
            param[4] = new SqlParameter("@ClientCode", argMaterial_Valuation.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argMaterial_Valuation.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argMaterial_Valuation.ModifiedBy);
            
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateMaterial_Valuation", param);


            string strMessage = Convert.ToString(param[8].Value);
            string strType = Convert.ToString(param[7].Value);
            string strRetValue = Convert.ToString(param[9].Value);


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

        public ICollection<ErrorHandler> DeleteMaterial_Valuation(string argMaterialCode, string argPlantCode, string argValTypeCode, string argValCatCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
                param[1] = new SqlParameter("@PlantCode", argPlantCode);
                param[2] = new SqlParameter("@ValTypeCode", argValTypeCode);
                param[3] = new SqlParameter("@ValCatCode", argValCatCode);
                param[4] = new SqlParameter("@ClientCode", argClientCode);
                param[5] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[6] = new SqlParameter("@Type", SqlDbType.Char);
                param[6].Size = 1;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[7].Size = 255;
                param[7].Direction = ParameterDirection.Output;

                param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[8].Size = 20;
                param[8].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteMaterial_Valuation", param);
                
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

        public bool blnIsMaterial_ValuationExists(string argMaterialCode, string argPlantCode, string argValTypeCode, string argValCatCode, string argClientCode)
        {
            bool IsMaterial_ValuationExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterial_Valuation(argMaterialCode, argPlantCode, argValTypeCode, argValCatCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterial_ValuationExists = true;
            }
            else
            {
                IsMaterial_ValuationExists = false;
            }
            return IsMaterial_ValuationExists;
        }

        public bool blnIsMaterial_ValuationExists(string argMaterialCode, string argPlantCode, string argValTypeCode, string argValCatCode, string argClientCode,DataAccess da)
        {
            bool IsMaterial_ValuationExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterial_Valuation(argMaterialCode, argPlantCode, argValTypeCode, argValCatCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterial_ValuationExists = true;
            }
            else
            {
                IsMaterial_ValuationExists = false;
            }
            return IsMaterial_ValuationExists;
        }
    }
}