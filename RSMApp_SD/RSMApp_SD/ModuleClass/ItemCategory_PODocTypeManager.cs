
//Created On :: 15, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_SD
{
    public class ItemCategory_PODocTypeManager
    {
        const string ItemCategory_PODocTypeTable = "ItemCategory_PODocType";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public ItemCategory_PODocType objGetItemCategory_PODocType(string argPODocTypeCode, string argPOItemCategoryCode, string argClientCode)
        {
            ItemCategory_PODocType argItemCategory_PODocType = new ItemCategory_PODocType();
            DataSet DataSetToFill = new DataSet();

            if (argPODocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argPOItemCategoryCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetItemCategory_PODocType(argPODocTypeCode, argPOItemCategoryCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argItemCategory_PODocType = this.objCreateItemCategory_PODocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argItemCategory_PODocType;
        }

        public ICollection<ItemCategory_PODocType> colGetItemCategory_PODocType(string argClientCode)
        {
            List<ItemCategory_PODocType> lst = new List<ItemCategory_PODocType>();
            DataSet DataSetToFill = new DataSet();
            ItemCategory_PODocType tItemCategory_PODocType = new ItemCategory_PODocType();
            DataSetToFill = this.GetItemCategory_PODocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateItemCategory_PODocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<ItemCategory_PODocType> colGetItemCategory_PODocType(DataTable dt, string argUserName, string clientCode)
        {
            List<ItemCategory_PODocType> lst = new List<ItemCategory_PODocType>();
            ItemCategory_PODocType objItemCategory_PODocType = null;
            foreach (DataRow dr in dt.Rows)
            {
                objItemCategory_PODocType = new ItemCategory_PODocType();
                objItemCategory_PODocType.PODocTypeCode = Convert.ToString(dr["PODocTypeCode"]).Trim();
                objItemCategory_PODocType.PODocTypeDesc = Convert.ToString(dr["PODocTypeDesc"]).Trim();
                objItemCategory_PODocType.ItemCatGroupCode = Convert.ToString(dr["ItemCatGroupCode"]).Trim();
                objItemCategory_PODocType.HLPOItemCategoryCode = Convert.ToString(dr["HLPOItemCategoryCode"]).Trim();
                objItemCategory_PODocType.POItemCategoryCode = Convert.ToString(dr["POItemCategoryCode"]).Trim();
                objItemCategory_PODocType.OptPOItemCategoryCode1 = Convert.ToString(dr["OptPOItemCategoryCode1"]).Trim();
                objItemCategory_PODocType.OptPOItemCategoryCode2 = Convert.ToString(dr["OptPOItemCategoryCode2"]).Trim();
                objItemCategory_PODocType.OptPOItemCategoryCode3 = Convert.ToString(dr["OptPOItemCategoryCode3"]).Trim();
                objItemCategory_PODocType.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objItemCategory_PODocType.ModifiedBy = Convert.ToString(argUserName).Trim();
                objItemCategory_PODocType.CreatedBy = Convert.ToString(argUserName).Trim();
                objItemCategory_PODocType.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objItemCategory_PODocType);
            }
            return lst;
        }

        public DataSet GetItemCategory_PODocType(string argPODocTypeCode, string argPOItemCategoryCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PODocTypeCode", argPODocTypeCode);
            param[1] = new SqlParameter("@POItemCategoryCode", argPOItemCategoryCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_PODocType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory_PODocType(string argPODocTypeCode, string argPOItemCategoryCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PODocTypeCode", argPODocTypeCode);
            param[1] = new SqlParameter("@POItemCategoryCode", argPOItemCategoryCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetItemCategory_PODocType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory_PODocType(string argPODocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PODocTypeCode", argPODocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_PODocType4PODoc", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory_PODocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_PODocType", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory_PODocType4Combo(string argPODocTypeCode, string argItemCatGroupCode, string argHLPOItemCategoryCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@PODocTypeCode", argPODocTypeCode);
            param[1] = new SqlParameter("@ItemCatGroupCode", argItemCatGroupCode);
            param[2] = new SqlParameter("@HLPOItemCategoryCode", argHLPOItemCategoryCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_PODocType4Combo", param);
            return DataSetToFill;
        }

        private ItemCategory_PODocType objCreateItemCategory_PODocType(DataRow dr)
        {
            ItemCategory_PODocType tItemCategory_PODocType = new ItemCategory_PODocType();
            tItemCategory_PODocType.SetObjectInfo(dr);
            return tItemCategory_PODocType;
        }

        public ICollection<ErrorHandler> SaveItemCategory_PODocType(ICollection<ItemCategory_PODocType> colGetItemCategory_PODocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (ItemCategory_PODocType argItemCategory_PODocType in colGetItemCategory_PODocType)
                {
                    if (argItemCategory_PODocType.IsDeleted == 0)
                    {
                        if (blnIsItemCategory_PODocTypeExists(argItemCategory_PODocType.PODocTypeCode, argItemCategory_PODocType.POItemCategoryCode, argItemCategory_PODocType.ClientCode, da) == false)
                        {
                            InsertItemCategory_PODocType(argItemCategory_PODocType, da, lstErr);
                        }
                        else
                        {
                            UpdateItemCategory_PODocType(argItemCategory_PODocType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteItemCategory_PODocType(argItemCategory_PODocType.PODocTypeCode, argItemCategory_PODocType.POItemCategoryCode, argItemCategory_PODocType.ClientCode, argItemCategory_PODocType.IsDeleted);
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

        public ICollection<ErrorHandler> SaveItemCategory_PODocType(ItemCategory_PODocType argItemCategory_PODocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsItemCategory_PODocTypeExists(argItemCategory_PODocType.PODocTypeCode, argItemCategory_PODocType.POItemCategoryCode, argItemCategory_PODocType.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertItemCategory_PODocType(argItemCategory_PODocType, da, lstErr);
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
                    UpdateItemCategory_PODocType(argItemCategory_PODocType, da, lstErr);
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

        public ICollection<ErrorHandler> SaveItemCategory_PODocType(ICollection<ItemCategory_PODocType> colGetItemCategory_PODocType, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (ItemCategory_PODocType argItemCategory_PODocType in colGetItemCategory_PODocType)
                {
                    if (argItemCategory_PODocType.IsDeleted == 0)
                    {
                        if (blnIsItemCategory_PODocTypeExists(argItemCategory_PODocType.PODocTypeCode, argItemCategory_PODocType.POItemCategoryCode, argItemCategory_PODocType.ClientCode, da) == false)
                        {
                            InsertItemCategory_PODocType(argItemCategory_PODocType, da, lstErr);
                        }
                        else
                        {
                            UpdateItemCategory_PODocType(argItemCategory_PODocType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteItemCategory_PODocType(argItemCategory_PODocType.PODocTypeCode, argItemCategory_PODocType.POItemCategoryCode, argItemCategory_PODocType.ClientCode, argItemCategory_PODocType.IsDeleted);
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
                    SaveItemCategory_PODocType(colGetItemCategory_PODocType(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertItemCategory_PODocType(ItemCategory_PODocType argItemCategory_PODocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@PODocTypeCode", argItemCategory_PODocType.PODocTypeCode);
            param[1] = new SqlParameter("@PODocTypeDesc", argItemCategory_PODocType.PODocTypeDesc);
            param[2] = new SqlParameter("@ItemCatGroupCode", argItemCategory_PODocType.ItemCatGroupCode);
            param[3] = new SqlParameter("@HLPOItemCategoryCode", argItemCategory_PODocType.HLPOItemCategoryCode);
            param[4] = new SqlParameter("@POItemCategoryCode", argItemCategory_PODocType.POItemCategoryCode);
            param[5] = new SqlParameter("@OptPOItemCategoryCode1", argItemCategory_PODocType.OptPOItemCategoryCode1);
            param[6] = new SqlParameter("@OptPOItemCategoryCode2", argItemCategory_PODocType.OptPOItemCategoryCode2);
            param[7] = new SqlParameter("@OptPOItemCategoryCode3", argItemCategory_PODocType.OptPOItemCategoryCode3);
            param[8] = new SqlParameter("@ClientCode", argItemCategory_PODocType.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argItemCategory_PODocType.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argItemCategory_PODocType.ModifiedBy);
        
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertItemCategory_PODocType", param);

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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);
        }

        public void UpdateItemCategory_PODocType(ItemCategory_PODocType argItemCategory_PODocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@PODocTypeCode", argItemCategory_PODocType.PODocTypeCode);
            param[1] = new SqlParameter("@PODocTypeDesc", argItemCategory_PODocType.PODocTypeDesc);
            param[2] = new SqlParameter("@ItemCatGroupCode", argItemCategory_PODocType.ItemCatGroupCode);
            param[3] = new SqlParameter("@HLPOItemCategoryCode", argItemCategory_PODocType.HLPOItemCategoryCode);
            param[4] = new SqlParameter("@POItemCategoryCode", argItemCategory_PODocType.POItemCategoryCode);
            param[5] = new SqlParameter("@OptPOItemCategoryCode1", argItemCategory_PODocType.OptPOItemCategoryCode1);
            param[6] = new SqlParameter("@OptPOItemCategoryCode2", argItemCategory_PODocType.OptPOItemCategoryCode2);
            param[7] = new SqlParameter("@OptPOItemCategoryCode3", argItemCategory_PODocType.OptPOItemCategoryCode3);
            param[8] = new SqlParameter("@ClientCode", argItemCategory_PODocType.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argItemCategory_PODocType.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argItemCategory_PODocType.ModifiedBy);

            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateItemCategory_PODocType", param);

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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);
        }

        public ICollection<ErrorHandler> DeleteItemCategory_PODocType(string argPODocTypeCode, string argPOItemCategoryCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@PODocTypeCode", argPODocTypeCode);
                param[1] = new SqlParameter("@POItemCategoryCode", argPOItemCategoryCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteItemCategory_PODocType", param);

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
                objErrorHandler.ReturnValue = strRetValue;
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

        public ICollection<ErrorHandler> DeleteItemCategory_PODocType(string argPODocTypeCode, string argPOItemCategoryCode, string argClientCode, int iIsDeleted, DataAccess da)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@PODocTypeCode", argPODocTypeCode);
                param[1] = new SqlParameter("@POItemCategoryCode", argPOItemCategoryCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;
                int i = da.NExecuteNonQuery("Proc_DeleteItemCategory_PODocType", param);

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
                objErrorHandler.ReturnValue = strRetValue;
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

        public bool blnIsItemCategory_PODocTypeExists(string argPODocTypeCode, string argPOItemCategoryCode, string argClientCode)
        {
            bool IsItemCategory_PODocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetItemCategory_PODocType(argPODocTypeCode, argPOItemCategoryCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemCategory_PODocTypeExists = true;
            }
            else
            {
                IsItemCategory_PODocTypeExists = false;
            }
            return IsItemCategory_PODocTypeExists;
        }

        public bool blnIsItemCategory_PODocTypeExists(string argPODocTypeCode, string argPOItemCategoryCode, string argClientCode, DataAccess da)
        {
            bool IsItemCategory_PODocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetItemCategory_PODocType(argPODocTypeCode, argPOItemCategoryCode, argClientCode,da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemCategory_PODocTypeExists = true;
            }
            else
            {
                IsItemCategory_PODocTypeExists = false;
            }
            return IsItemCategory_PODocTypeExists;
        }
    }
}