
//Created On :: 12, September, 2012
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
    public class ItemCategory_DeliveryDocTypeManager
    {
        const string ItemCategory_DeliveryDocTypeTable = "ItemCategory_DeliveryDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public ItemCategory_DeliveryDocType objGetItemCategory_DeliveryDocType(string argDeliveryDocTypeCode, string argDLItemCategoryCode, string argClientCode)
        {
            ItemCategory_DeliveryDocType argItemCategory_DeliveryDocType = new ItemCategory_DeliveryDocType();
            DataSet DataSetToFill = new DataSet();

            if (argDeliveryDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argDLItemCategoryCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetItemCategory_DeliveryDocType(argDeliveryDocTypeCode, argDLItemCategoryCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argItemCategory_DeliveryDocType = this.objCreateItemCategory_DeliveryDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argItemCategory_DeliveryDocType;
        }
        
        public ICollection<ItemCategory_DeliveryDocType> colGetItemCategory_DeliveryDocType(string argClientCode)
        {
            List<ItemCategory_DeliveryDocType> lst = new List<ItemCategory_DeliveryDocType>();
            DataSet DataSetToFill = new DataSet();
            ItemCategory_DeliveryDocType tItemCategory_DeliveryDocType = new ItemCategory_DeliveryDocType();
            DataSetToFill = this.GetItemCategory_DeliveryDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateItemCategory_DeliveryDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<ItemCategory_DeliveryDocType> colGetItemCategory_DeliveryDocType(DataTable dt, string argUserName, string clientCode)
        {
            List<ItemCategory_DeliveryDocType> lst = new List<ItemCategory_DeliveryDocType>();
            ItemCategory_DeliveryDocType objItemCategory_DeliveryDocType = null;
            foreach (DataRow dr in dt.Rows)
            {
                objItemCategory_DeliveryDocType = new ItemCategory_DeliveryDocType();
                objItemCategory_DeliveryDocType.DeliveryDocTypeCode = Convert.ToString(dr["DeliveryDocTypeCode"]).Trim();
                objItemCategory_DeliveryDocType.DeliveryTypeDesc = Convert.ToString(dr["DeliveryTypeDesc"]).Trim();
                objItemCategory_DeliveryDocType.ItemCatGroupCode = Convert.ToString(dr["ItemCatGroupCode"]).Trim();
                objItemCategory_DeliveryDocType.HLItemCategoryCode = Convert.ToString(dr["HLItemCategoryCode"]).Trim();
                objItemCategory_DeliveryDocType.DLItemCategoryCode = Convert.ToString(dr["DLItemCategoryCode"]).Trim();
                objItemCategory_DeliveryDocType.OptDLItemCategoryCode1 = Convert.ToString(dr["OptDLItemCategoryCode1"]).Trim();
                objItemCategory_DeliveryDocType.OptDLItemCategoryCode2 = Convert.ToString(dr["OptDLItemCategoryCode2"]).Trim();
                objItemCategory_DeliveryDocType.OptDLItemCategoryCode3 = Convert.ToString(dr["OptDLItemCategoryCode3"]).Trim();
                objItemCategory_DeliveryDocType.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objItemCategory_DeliveryDocType.ModifiedBy = Convert.ToString(argUserName).Trim();
                objItemCategory_DeliveryDocType.CreatedBy = Convert.ToString(argUserName).Trim();
                objItemCategory_DeliveryDocType.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objItemCategory_DeliveryDocType);
            }
            return lst;
        }
        
        public DataSet GetItemCategory_DeliveryDocType(string argDeliveryDocTypeCode, string argDLItemCategoryCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@DeliveryDocTypeCode", argDeliveryDocTypeCode);
            param[1] = new SqlParameter("@DLItemCategoryCode", argDLItemCategoryCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_DeliveryDocType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory_DeliveryDocType(string argDeliveryDocTypeCode, string argDLItemCategoryCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@DeliveryDocTypeCode", argDeliveryDocTypeCode);
            param[1] = new SqlParameter("@DLItemCategoryCode", argDLItemCategoryCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetItemCategory_DeliveryDocType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory_DeliveryDocType(string argDeliveryDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DeliveryDocTypeCode", argDeliveryDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_DeliveryDocType4DeliveryDoc", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory_DeliveryDocType4Combo(string argDeliveryDocTypeCode, string argItemCatGroupCode, string argHLItemCategoryCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@DeliveryDocTypeCode", argDeliveryDocTypeCode);
            param[1] = new SqlParameter("@ItemCatGroupCode", argItemCatGroupCode);
            param[2] = new SqlParameter("@HLItemCategoryCode", argHLItemCategoryCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_DeliveryDocType4Combo", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory_DeliveryDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_DeliveryDocType",param);
            return DataSetToFill;
        }
                       
        private ItemCategory_DeliveryDocType objCreateItemCategory_DeliveryDocType(DataRow dr)
        {
            ItemCategory_DeliveryDocType tItemCategory_DeliveryDocType = new ItemCategory_DeliveryDocType();
            tItemCategory_DeliveryDocType.SetObjectInfo(dr);
            return tItemCategory_DeliveryDocType;
        }

        public ICollection<ErrorHandler> SaveItemCategory_DeliveryDocType(ICollection<ItemCategory_DeliveryDocType> colGetItemCategory_DeliveryDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (ItemCategory_DeliveryDocType argItemCategory_DeliveryDocType in colGetItemCategory_DeliveryDocType)
                {
                    if (blnIsItemCategory_DeliveryDocTypeExists(argItemCategory_DeliveryDocType.DeliveryDocTypeCode, argItemCategory_DeliveryDocType.DLItemCategoryCode, argItemCategory_DeliveryDocType.ClientCode, da) == false)
                    {
                        InsertItemCategory_DeliveryDocType(argItemCategory_DeliveryDocType, da, lstErr);
                    }
                    else
                    {
                        UpdateItemCategory_DeliveryDocType(argItemCategory_DeliveryDocType, da, lstErr);
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

        public ICollection<ErrorHandler> SaveItemCategory_DeliveryDocType(ItemCategory_DeliveryDocType argItemCategory_DeliveryDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsItemCategory_DeliveryDocTypeExists(argItemCategory_DeliveryDocType.DeliveryDocTypeCode, argItemCategory_DeliveryDocType.DLItemCategoryCode, argItemCategory_DeliveryDocType.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertItemCategory_DeliveryDocType(argItemCategory_DeliveryDocType, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
                    }
                }
                else
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    UpdateItemCategory_DeliveryDocType(argItemCategory_DeliveryDocType, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
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
            return lstErr;
        }

        public ICollection<ErrorHandler> SaveItemCategory_DeliveryDocType(ICollection<ItemCategory_DeliveryDocType> colGetItemCategory_DeliveryDocType, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (ItemCategory_DeliveryDocType argItemCategory_DeliveryDocType in colGetItemCategory_DeliveryDocType)
                {
                    if (argItemCategory_DeliveryDocType.IsDeleted == 0)
                    {
                        if (blnIsItemCategory_DeliveryDocTypeExists(argItemCategory_DeliveryDocType.DeliveryDocTypeCode, argItemCategory_DeliveryDocType.DLItemCategoryCode, argItemCategory_DeliveryDocType.ClientCode, da) == false)
                        {
                            InsertItemCategory_DeliveryDocType(argItemCategory_DeliveryDocType, da, lstErr);
                        }
                        else
                        {
                            UpdateItemCategory_DeliveryDocType(argItemCategory_DeliveryDocType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteItemCategory_DeliveryDocType(argItemCategory_DeliveryDocType.DeliveryDocTypeCode, argItemCategory_DeliveryDocType.DLItemCategoryCode, argItemCategory_DeliveryDocType.ClientCode, argItemCategory_DeliveryDocType.IsDeleted);
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
                    SaveItemCategory_DeliveryDocType(colGetItemCategory_DeliveryDocType(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertItemCategory_DeliveryDocType(ItemCategory_DeliveryDocType argItemCategory_DeliveryDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];

            param[0] = new SqlParameter("@DeliveryDocTypeCode", argItemCategory_DeliveryDocType.DeliveryDocTypeCode);
            param[1] = new SqlParameter("@DeliveryTypeDesc", argItemCategory_DeliveryDocType.DeliveryTypeDesc);
            param[2] = new SqlParameter("@ItemCatGroupCode", argItemCategory_DeliveryDocType.ItemCatGroupCode);
            param[3] = new SqlParameter("@HLItemCategoryCode", argItemCategory_DeliveryDocType.HLItemCategoryCode);
            param[4] = new SqlParameter("@DLItemCategoryCode", argItemCategory_DeliveryDocType.DLItemCategoryCode);
            param[5] = new SqlParameter("@OptDLItemCategoryCode1", argItemCategory_DeliveryDocType.OptDLItemCategoryCode1);
            param[6] = new SqlParameter("@OptDLItemCategoryCode2", argItemCategory_DeliveryDocType.OptDLItemCategoryCode2);
            param[7] = new SqlParameter("@OptDLItemCategoryCode3", argItemCategory_DeliveryDocType.OptDLItemCategoryCode3);
            param[8] = new SqlParameter("@ClientCode", argItemCategory_DeliveryDocType.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argItemCategory_DeliveryDocType.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argItemCategory_DeliveryDocType.ModifiedBy);
          
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertItemCategory_DeliveryDocType", param);

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

        public void UpdateItemCategory_DeliveryDocType(ItemCategory_DeliveryDocType argItemCategory_DeliveryDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];

            param[0] = new SqlParameter("@DeliveryDocTypeCode", argItemCategory_DeliveryDocType.DeliveryDocTypeCode);
            param[1] = new SqlParameter("@DeliveryTypeDesc", argItemCategory_DeliveryDocType.DeliveryTypeDesc);
            param[2] = new SqlParameter("@ItemCatGroupCode", argItemCategory_DeliveryDocType.ItemCatGroupCode);
            param[3] = new SqlParameter("@HLItemCategoryCode", argItemCategory_DeliveryDocType.HLItemCategoryCode);
            param[4] = new SqlParameter("@DLItemCategoryCode", argItemCategory_DeliveryDocType.DLItemCategoryCode);
            param[5] = new SqlParameter("@OptDLItemCategoryCode1", argItemCategory_DeliveryDocType.OptDLItemCategoryCode1);
            param[6] = new SqlParameter("@OptDLItemCategoryCode2", argItemCategory_DeliveryDocType.OptDLItemCategoryCode2);
            param[7] = new SqlParameter("@OptDLItemCategoryCode3", argItemCategory_DeliveryDocType.OptDLItemCategoryCode3);
            param[8] = new SqlParameter("@ClientCode", argItemCategory_DeliveryDocType.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argItemCategory_DeliveryDocType.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argItemCategory_DeliveryDocType.ModifiedBy);
            
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateItemCategory_DeliveryDocType", param);

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

        public ICollection<ErrorHandler> DeleteItemCategory_DeliveryDocType(string argDeliveryDocTypeCode, string argDLItemCategoryCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@DeliveryDocTypeCode", argDeliveryDocTypeCode);
                param[1] = new SqlParameter("@DLItemCategoryCode", argDLItemCategoryCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteItemCategory_DeliveryDocType", param);

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
        
        public bool blnIsItemCategory_DeliveryDocTypeExists(string argDeliveryDocTypeCode, string argDLItemCategoryCode, string argClientCode)
        {
            bool IsItemCategory_DeliveryDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetItemCategory_DeliveryDocType(argDeliveryDocTypeCode, argDLItemCategoryCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemCategory_DeliveryDocTypeExists = true;
            }
            else
            {
                IsItemCategory_DeliveryDocTypeExists = false;
            }
            return IsItemCategory_DeliveryDocTypeExists;
        }

        public bool blnIsItemCategory_DeliveryDocTypeExists(string argDeliveryDocTypeCode, string argDLItemCategoryCode, string argClientCode, DataAccess da)
        {
            bool IsItemCategory_DeliveryDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetItemCategory_DeliveryDocType(argDeliveryDocTypeCode, argDLItemCategoryCode, argClientCode, da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemCategory_DeliveryDocTypeExists = true;
            }
            else
            {
                IsItemCategory_DeliveryDocTypeExists = false;
            }
            return IsItemCategory_DeliveryDocTypeExists;
        }
    }
}