
//Created On :: 20, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using RSMApp_Organization;
using System.Data.OleDb;

namespace RSMApp_MM
{
    public class Material_SalesAreaManager
    {
        const string Material_SalesAreaTable = "Material_SalesArea";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Material_SalesArea objGetMaterial_SalesArea(string argMaterialCode, string argSalesOrganizationCode, string argDistChannelCode, string argClientCode)
        {
            Material_SalesArea argMaterial_SalesArea = new Material_SalesArea();
            DataSet DataSetToFill = new DataSet();

            if (argMaterialCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argSalesOrganizationCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argDistChannelCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            
            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetMaterial_SalesArea(argMaterialCode, argSalesOrganizationCode, argDistChannelCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMaterial_SalesArea = this.objCreateMaterial_SalesArea((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argMaterial_SalesArea;
        }
        
        public ICollection<Material_SalesArea> colGetMaterial_SalesArea(string argClientCode)
        {
            List<Material_SalesArea> lst = new List<Material_SalesArea>();
            DataSet DataSetToFill = new DataSet();
            Material_SalesArea tMaterial_SalesArea = new Material_SalesArea();

            DataSetToFill = this.GetMaterial_SalesArea(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMaterial_SalesArea(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<Material_SalesArea> colGetMaterial_SalesArea(DataTable dt, string argUserName, string clientCode)
        {
            List<Material_SalesArea> lst = new List<Material_SalesArea>();
            Material_SalesArea objMaterial_SalesArea = null;
            foreach (DataRow dr in dt.Rows)
            {
                objMaterial_SalesArea = new Material_SalesArea();
                objMaterial_SalesArea.MaterialCode = Convert.ToString(dr["MaterialCode"]).Trim();
                objMaterial_SalesArea.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]).Trim();
                objMaterial_SalesArea.DistChannelCode = Convert.ToString(dr["DistChannelCode"]).Trim();
                objMaterial_SalesArea.MinOrderQty = Convert.ToInt32(dr["MinOrderQty"]);
                objMaterial_SalesArea.MinDeliveryQty = Convert.ToInt32(dr["MinDeliveryQty"]);
                objMaterial_SalesArea.SalesUOMCode = Convert.ToString(dr["SalesUOMCode"]).Trim();
                objMaterial_SalesArea.DeliveringPlantCode = Convert.ToString(dr["DeliveringPlantCode"]).Trim();
                objMaterial_SalesArea.MaterialHierarchy = Convert.ToString(dr["MaterialHierarchy"]).Trim();
                objMaterial_SalesArea.PFMaterialCode = Convert.ToString(dr["PFMaterialCode"]).Trim();
                objMaterial_SalesArea.IsVariableSalesUOM = Convert.ToInt32(dr["IsVariableSalesUOM"]);
                objMaterial_SalesArea.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objMaterial_SalesArea.ModifiedBy = Convert.ToString(argUserName).Trim();
                objMaterial_SalesArea.CreatedBy = Convert.ToString(argUserName).Trim();
                objMaterial_SalesArea.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objMaterial_SalesArea);
            }
            return lst;
        }
        
        public DataSet GetMaterial_SalesArea(string argMaterialCode, string argSalesOrganizationCode, string argDistChannelCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[2] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial_SalesArea4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMaterial_SalesArea(string argMaterialCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial_SalesArea4Material", param);

            return DataSetToFill;
        }

        public DataSet GetMaterial_SalesArea(string argMaterialCode, string argSalesOrganizationCode, string argDistChannelCode, string argClientCode, DataAccess da)
        {            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[2] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMaterial_SalesArea4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetMaterial_SalesArea(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetMaterial_SalesArea", param);
            return DataSetToFill;
        }
        
        private Material_SalesArea objCreateMaterial_SalesArea(DataRow dr)
        {
            Material_SalesArea tMaterial_SalesArea = new Material_SalesArea();

            tMaterial_SalesArea.SetObjectInfo(dr);

            return tMaterial_SalesArea;

        }
        
        public ICollection<ErrorHandler> SaveMaterial_SalesArea(Material_SalesArea argMaterial_SalesArea)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();

            SalesAreaManager objSalesAreaManager = new SalesAreaManager();
            try
            {
                  if (blnIsMaterial_SalesAreaExists(argMaterial_SalesArea.MaterialCode, argMaterial_SalesArea.SalesOrganizationCode, argMaterial_SalesArea.DistChannelCode,argMaterial_SalesArea.ClientCode) == false)
                    {
                        da.Open_Connection();
                        da.BEGIN_TRANSACTION();
                        InsertMaterial_SalesArea(argMaterial_SalesArea, da, lstErr);
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
                        UpdateMaterial_SalesArea(argMaterial_SalesArea, da, lstErr);
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

        public ICollection<ErrorHandler> SaveMaterial_SalesArea(string argClientCode, string argSalesOrganizationCode, string argDistChannelCode, int argMinOrderQty, int argMinDeliveryQty,string argSalesUOMCode,string argDeliveringPlantCode, string argMaterialHierarchy,string argPFMaterialCode, int argIsVariableSalesUOM, string argCreatedBy)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();
                InsertMaterial_SalesArea(argSalesOrganizationCode, argDistChannelCode, argMinOrderQty, argMinDeliveryQty,argSalesUOMCode,argDeliveringPlantCode, argMaterialHierarchy,argPFMaterialCode,argIsVariableSalesUOM, argClientCode, argCreatedBy, da, lstErr);
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

        public ICollection<ErrorHandler> SaveMaterial_SalesArea(ICollection<Material_SalesArea> colGetMaterial_SalesArea)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Material_SalesArea argMaterial_SalesArea in colGetMaterial_SalesArea)
                {
                    if (argMaterial_SalesArea.IsDeleted == 0)
                    {

                        if (blnIsMaterial_SalesAreaExists(argMaterial_SalesArea.MaterialCode, argMaterial_SalesArea.SalesOrganizationCode, argMaterial_SalesArea.DistChannelCode, argMaterial_SalesArea.ClientCode, da) == false)
                        {
                            InsertMaterial_SalesArea(argMaterial_SalesArea, da, lstErr);
                        }
                        else
                        {
                            UpdateMaterial_SalesArea(argMaterial_SalesArea, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMaterial_SalesArea(argMaterial_SalesArea.MaterialCode, argMaterial_SalesArea.SalesOrganizationCode, argMaterial_SalesArea.DistChannelCode, argMaterial_SalesArea.ClientCode, argMaterial_SalesArea.IsDeleted);
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

        public ICollection<ErrorHandler> SaveMaterial_SalesArea(ICollection<Material_SalesArea> colGetMaterial_SalesArea, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Material_SalesArea argMaterial_SalesArea in colGetMaterial_SalesArea)
                {
                    if (argMaterial_SalesArea.IsDeleted == 0)
                    {
                        if (blnIsMaterial_SalesAreaExists(argMaterial_SalesArea.MaterialCode, argMaterial_SalesArea.SalesOrganizationCode, argMaterial_SalesArea.DistChannelCode, argMaterial_SalesArea.ClientCode, da) == false)
                        {
                            InsertMaterial_SalesArea(argMaterial_SalesArea, da, lstErr);
                        }
                        else
                        {
                            UpdateMaterial_SalesArea(argMaterial_SalesArea, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMaterial_SalesArea(argMaterial_SalesArea.MaterialCode, argMaterial_SalesArea.SalesOrganizationCode, argMaterial_SalesArea.DistChannelCode, argMaterial_SalesArea.ClientCode, argMaterial_SalesArea.IsDeleted);
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
                    SaveMaterial_SalesArea(colGetMaterial_SalesArea(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertMaterial_SalesArea(Material_SalesArea argMaterial_SalesArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@MaterialCode", argMaterial_SalesArea.MaterialCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argMaterial_SalesArea.SalesOrganizationCode);
            param[2] = new SqlParameter("@DistChannelCode", argMaterial_SalesArea.DistChannelCode);
            param[3] = new SqlParameter("@MinOrderQty", argMaterial_SalesArea.MinOrderQty);
            param[4] = new SqlParameter("@MinDeliveryQty", argMaterial_SalesArea.MinDeliveryQty);
            param[5] = new SqlParameter("@SalesUOMCode", argMaterial_SalesArea.SalesUOMCode);
            param[6] = new SqlParameter("@DeliveringPlantCode", argMaterial_SalesArea.DeliveringPlantCode);
            param[7] = new SqlParameter("@MaterialHierarchy", argMaterial_SalesArea.MaterialHierarchy);
            param[8] = new SqlParameter("@PFMaterialCode", argMaterial_SalesArea.PFMaterialCode);
            param[9] = new SqlParameter("@IsVariableSalesUOM", argMaterial_SalesArea.IsVariableSalesUOM);
            param[10] = new SqlParameter("@ClientCode", argMaterial_SalesArea.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argMaterial_SalesArea.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argMaterial_SalesArea.ModifiedBy);
            
            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertMaterial_SalesArea", param);
            
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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public void InsertMaterial_SalesArea(string argSalesOrganizationCode, string argDistChannelCode,int argMinOrderQty, int argMinDeliveryQty,string argSalesUOMCode,string argDeliveringPlantCode, string argMaterialHierarchy,string argPFMaterialCode, int argIsVariableSalesUOM, string argClientCode, string argCreatedBy, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[1] = new SqlParameter("@DistChannelCode",argDistChannelCode);
            param[2] = new SqlParameter("@MinOrderQty", argMinOrderQty);
            param[3] = new SqlParameter("@MinDeliveryQty", argMinDeliveryQty);
            param[4] = new SqlParameter("@SalesUOMCode", argSalesUOMCode);
            param[5] = new SqlParameter("@DeliveringPlantCode", argDeliveringPlantCode);
            param[6] = new SqlParameter("@MaterialHierarchy", argMaterialHierarchy);
            param[7] = new SqlParameter("@PFMaterialCode", argPFMaterialCode);
            param[8] = new SqlParameter("@IsVariableSalesUOM", argIsVariableSalesUOM);
            param[9] = new SqlParameter("@ClientCode", argClientCode);
            param[10] = new SqlParameter("@CreatedBy", argCreatedBy);

            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("SL_BulkInsertMaterial_SalesArea", param);

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

        public void UpdateMaterial_SalesArea(Material_SalesArea argMaterial_SalesArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@MaterialCode", argMaterial_SalesArea.MaterialCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argMaterial_SalesArea.SalesOrganizationCode);
            param[2] = new SqlParameter("@DistChannelCode", argMaterial_SalesArea.DistChannelCode);
            param[3] = new SqlParameter("@MinOrderQty", argMaterial_SalesArea.MinOrderQty);
            param[4] = new SqlParameter("@MinDeliveryQty", argMaterial_SalesArea.MinDeliveryQty);
            param[5] = new SqlParameter("@SalesUOMCode", argMaterial_SalesArea.SalesUOMCode);
            param[6] = new SqlParameter("@DeliveringPlantCode", argMaterial_SalesArea.DeliveringPlantCode);
            param[7] = new SqlParameter("@MaterialHierarchy", argMaterial_SalesArea.MaterialHierarchy);
            param[8] = new SqlParameter("@PFMaterialCode", argMaterial_SalesArea.PFMaterialCode);
            param[9] = new SqlParameter("@IsVariableSalesUOM", argMaterial_SalesArea.IsVariableSalesUOM);
            param[10] = new SqlParameter("@ClientCode", argMaterial_SalesArea.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argMaterial_SalesArea.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argMaterial_SalesArea.ModifiedBy);

            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateMaterial_SalesArea", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public ICollection<ErrorHandler> DeleteMaterial_SalesArea(string argMaterialCode, string argSalesOrganizationCode, string argDistChannelCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
                param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
                param[2] = new SqlParameter("@DistChannelCode", argDistChannelCode);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteMaterial_SalesArea", param);


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
        
        public bool blnIsMaterial_SalesAreaExists(string argMaterialCode, string argSalesOrganizationCode, string argDistChannelCode, string argClientCode)
        {
            bool IsMaterial_SalesAreaExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterial_SalesArea(argMaterialCode, argSalesOrganizationCode, argDistChannelCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterial_SalesAreaExists = true;
            }
            else
            {
                IsMaterial_SalesAreaExists = false;
            }
            return IsMaterial_SalesAreaExists;
        }

        public bool blnIsMaterial_SalesAreaExists(string argMaterialCode, string argSalesOrganizationCode, string argDistChannelCode, string argClientCode, DataAccess da)
        {
            bool IsMaterial_SalesAreaExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterial_SalesArea(argMaterialCode, argSalesOrganizationCode, argDistChannelCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterial_SalesAreaExists = true;
            }
            else
            {
                IsMaterial_SalesAreaExists = false;
            }
            return IsMaterial_SalesAreaExists;
        }
    }
}