
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
    public class ItemCategory_BillingDocTypeManager
    {
        const string ItemCategory_BillingDocTypeTable = "ItemCategory_BillingDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public ItemCategory_BillingDocType objGetItemCategory_BillingDocType(string argBillingDocTypeCode, string argBLItemCategoryCode, string argClientCode)
        {
            ItemCategory_BillingDocType argItemCategory_BillingDocType = new ItemCategory_BillingDocType();
            DataSet DataSetToFill = new DataSet();

            if (argBillingDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argBLItemCategoryCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetItemCategory_BillingDocType(argBillingDocTypeCode, argBLItemCategoryCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argItemCategory_BillingDocType = this.objCreateItemCategory_BillingDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argItemCategory_BillingDocType;
        }
        
        public ICollection<ItemCategory_BillingDocType> colGetItemCategory_BillingDocType(string argClientCode)
        {
            List<ItemCategory_BillingDocType> lst = new List<ItemCategory_BillingDocType>();
            DataSet DataSetToFill = new DataSet();
            ItemCategory_BillingDocType tItemCategory_BillingDocType = new ItemCategory_BillingDocType();
            DataSetToFill = this.GetItemCategory_BillingDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateItemCategory_BillingDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<ItemCategory_BillingDocType> colGetItemCategory_BillingDocType(DataTable dt, string argUserName, string clientCode)
        {
            List<ItemCategory_BillingDocType> lst = new List<ItemCategory_BillingDocType>();
            ItemCategory_BillingDocType objItemCategory_BillingDocType = null;
            foreach (DataRow dr in dt.Rows)
            {
                objItemCategory_BillingDocType = new ItemCategory_BillingDocType();
                objItemCategory_BillingDocType.BillingDocTypeCode = Convert.ToString(dr["BillingDocTypeCode"]).Trim();
                objItemCategory_BillingDocType.BillingTypeDesc = Convert.ToString(dr["BillingTypeDesc"]).Trim();
                objItemCategory_BillingDocType.ItemCatGroupCode = Convert.ToString(dr["ItemCatGroupCode"]).Trim();
                objItemCategory_BillingDocType.HLItemCategoryCode = Convert.ToString(dr["HLItemCategoryCode"]).Trim();
                objItemCategory_BillingDocType.BLItemCategoryCode = Convert.ToString(dr["BLItemCategoryCode"]).Trim();
                objItemCategory_BillingDocType.OptBLItemCategoryCode1 = Convert.ToString(dr["OptBLItemCategoryCode1"]).Trim();
                objItemCategory_BillingDocType.OptBLItemCategoryCode2 = Convert.ToString(dr["OptBLItemCategoryCode2"]).Trim();
                objItemCategory_BillingDocType.OptBLItemCategoryCode3 = Convert.ToString(dr["OptBLItemCategoryCode3"]).Trim();
                objItemCategory_BillingDocType.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objItemCategory_BillingDocType.ModifiedBy = Convert.ToString(argUserName).Trim();
                objItemCategory_BillingDocType.CreatedBy = Convert.ToString(argUserName).Trim();
                objItemCategory_BillingDocType.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objItemCategory_BillingDocType);
            }
            return lst;
        }
        
        public DataSet GetItemCategory_BillingDocType(string argBillingDocTypeCode, string argBLItemCategoryCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BillingDocTypeCode", argBillingDocTypeCode);
            param[1] = new SqlParameter("@BLItemCategoryCode", argBLItemCategoryCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_BillingDocType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory_BillingDocType(string argBillingDocTypeCode, string argBLItemCategoryCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BillingDocTypeCode", argBillingDocTypeCode);
            param[1] = new SqlParameter("@BLItemCategoryCode", argBLItemCategoryCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetItemCategory_BillingDocType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory_BillingDocType(string argBillingDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@BillingDocTypeCode", argBillingDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_BillingDocType4BillingDoc", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory_BillingDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_BillingDocType",param);
            return DataSetToFill;
        }
        
        private ItemCategory_BillingDocType objCreateItemCategory_BillingDocType(DataRow dr)
        {
            ItemCategory_BillingDocType tItemCategory_BillingDocType = new ItemCategory_BillingDocType();
            tItemCategory_BillingDocType.SetObjectInfo(dr);
            return tItemCategory_BillingDocType;
        }
        
        public ICollection<ErrorHandler> SaveItemCategory_BillingDocType(ItemCategory_BillingDocType argItemCategory_BillingDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsItemCategory_BillingDocTypeExists(argItemCategory_BillingDocType.BillingDocTypeCode, argItemCategory_BillingDocType.BLItemCategoryCode, argItemCategory_BillingDocType.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertItemCategory_BillingDocType(argItemCategory_BillingDocType, da, lstErr);
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
                    UpdateItemCategory_BillingDocType(argItemCategory_BillingDocType, da, lstErr);
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
        
        public ICollection<ErrorHandler> SaveItemCategory_BillingDocType(ICollection<ItemCategory_BillingDocType> colGetItemCategory_BillingDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (ItemCategory_BillingDocType argItemCategory_BillingDocType in colGetItemCategory_BillingDocType)
                {
                    if (argItemCategory_BillingDocType.IsDeleted == 0)
                    {
                        if (blnIsItemCategory_BillingDocTypeExists(argItemCategory_BillingDocType.BillingDocTypeCode, argItemCategory_BillingDocType.BLItemCategoryCode, argItemCategory_BillingDocType.ClientCode, da) == false)
                        {
                            InsertItemCategory_BillingDocType(argItemCategory_BillingDocType, da, lstErr);
                        }
                        else
                        {
                            UpdateItemCategory_BillingDocType(argItemCategory_BillingDocType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteItemCategory_BillingDocType(argItemCategory_BillingDocType.BillingDocTypeCode, argItemCategory_BillingDocType.BLItemCategoryCode, argItemCategory_BillingDocType.ClientCode, argItemCategory_BillingDocType.IsDeleted);
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

        public ICollection<ErrorHandler> SaveItemCategory_BillingDocType(ICollection<ItemCategory_BillingDocType> colGetItemCategory_BillingDocType, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (ItemCategory_BillingDocType argItemCategory_BillingDocType in colGetItemCategory_BillingDocType)
                {
                    if (argItemCategory_BillingDocType.IsDeleted == 0)
                    {
                        if (blnIsItemCategory_BillingDocTypeExists(argItemCategory_BillingDocType.BillingDocTypeCode, argItemCategory_BillingDocType.BLItemCategoryCode, argItemCategory_BillingDocType.ClientCode, da) == false)
                        {
                            InsertItemCategory_BillingDocType(argItemCategory_BillingDocType, da, lstErr);
                        }
                        else
                        {
                            UpdateItemCategory_BillingDocType(argItemCategory_BillingDocType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteItemCategory_BillingDocType(argItemCategory_BillingDocType.BillingDocTypeCode, argItemCategory_BillingDocType.BLItemCategoryCode, argItemCategory_BillingDocType.ClientCode, argItemCategory_BillingDocType.IsDeleted);
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
                    SaveItemCategory_BillingDocType(colGetItemCategory_BillingDocType(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertItemCategory_BillingDocType(ItemCategory_BillingDocType argItemCategory_BillingDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];

            param[0] = new SqlParameter("@BillingDocTypeCode", argItemCategory_BillingDocType.BillingDocTypeCode);
            param[1] = new SqlParameter("@BillingTypeDesc", argItemCategory_BillingDocType.BillingTypeDesc);
            param[2] = new SqlParameter("@ItemCatGroupCode", argItemCategory_BillingDocType.ItemCatGroupCode);
            param[3] = new SqlParameter("@HLItemCategoryCode", argItemCategory_BillingDocType.HLItemCategoryCode);
            param[4] = new SqlParameter("@BLItemCategoryCode", argItemCategory_BillingDocType.BLItemCategoryCode);
            param[5] = new SqlParameter("@OptBLItemCategoryCode1", argItemCategory_BillingDocType.OptBLItemCategoryCode1);
            param[6] = new SqlParameter("@OptBLItemCategoryCode2", argItemCategory_BillingDocType.OptBLItemCategoryCode2);
            param[7] = new SqlParameter("@OptBLItemCategoryCode3", argItemCategory_BillingDocType.OptBLItemCategoryCode3);
            param[8] = new SqlParameter("@ClientCode", argItemCategory_BillingDocType.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argItemCategory_BillingDocType.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argItemCategory_BillingDocType.ModifiedBy);
            
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertItemCategory_BillingDocType", param);
            
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
        
        public void UpdateItemCategory_BillingDocType(ItemCategory_BillingDocType argItemCategory_BillingDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];

            param[0] = new SqlParameter("@BillingDocTypeCode", argItemCategory_BillingDocType.BillingDocTypeCode);
            param[1] = new SqlParameter("@BillingTypeDesc", argItemCategory_BillingDocType.BillingTypeDesc);
            param[2] = new SqlParameter("@ItemCatGroupCode", argItemCategory_BillingDocType.ItemCatGroupCode);
            param[3] = new SqlParameter("@HLItemCategoryCode", argItemCategory_BillingDocType.HLItemCategoryCode);
            param[4] = new SqlParameter("@BLItemCategoryCode", argItemCategory_BillingDocType.BLItemCategoryCode);
            param[5] = new SqlParameter("@OptBLItemCategoryCode1", argItemCategory_BillingDocType.OptBLItemCategoryCode1);
            param[6] = new SqlParameter("@OptBLItemCategoryCode2", argItemCategory_BillingDocType.OptBLItemCategoryCode2);
            param[7] = new SqlParameter("@OptBLItemCategoryCode3", argItemCategory_BillingDocType.OptBLItemCategoryCode3);
            param[8] = new SqlParameter("@ClientCode", argItemCategory_BillingDocType.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argItemCategory_BillingDocType.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argItemCategory_BillingDocType.ModifiedBy);
            
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateItemCategory_BillingDocType", param);
            
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
        
        public ICollection<ErrorHandler> DeleteItemCategory_BillingDocType(string argBillingDocTypeCode, string argBLItemCategoryCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[7];

                param[0] = new SqlParameter("@BillingDocTypeCode", argBillingDocTypeCode);
                param[1] = new SqlParameter("@BLItemCategoryCode", argBLItemCategoryCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteItemCategory_BillingDocType", param);

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
        
        public bool blnIsItemCategory_BillingDocTypeExists(string argBillingDocTypeCode, string argBLItemCategoryCode, string argClientCode)
        {
            bool IsItemCategory_BillingDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetItemCategory_BillingDocType(argBillingDocTypeCode, argBLItemCategoryCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemCategory_BillingDocTypeExists = true;
            }
            else
            {
                IsItemCategory_BillingDocTypeExists = false;
            }
            return IsItemCategory_BillingDocTypeExists;
        }

        public bool blnIsItemCategory_BillingDocTypeExists(string argBillingDocTypeCode, string argBLItemCategoryCode, string argClientCode, DataAccess da)
        {
            bool IsItemCategory_BillingDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetItemCategory_BillingDocType(argBillingDocTypeCode, argBLItemCategoryCode, argClientCode, da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemCategory_BillingDocTypeExists = true;
            }
            else
            {
                IsItemCategory_BillingDocTypeExists = false;
            }
            return IsItemCategory_BillingDocTypeExists;
        }
    }
}