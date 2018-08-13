
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
    public class ItemCategory_SODocTypeManager
    {
        const string ItemCategory_SODocTypeTable = "ItemCategory_SODocType";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public ItemCategory_SODocType objGetItemCategory_SODocType(string argSOTypeCode, string argItemCategoryCode, string argClientCode)
        {
            ItemCategory_SODocType argItemCategory_SODocType = new ItemCategory_SODocType();
            DataSet DataSetToFill = new DataSet();

            if (argSOTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argItemCategoryCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetItemCategory_SODocType(argSOTypeCode, argItemCategoryCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argItemCategory_SODocType = this.objCreateItemCategory_SODocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argItemCategory_SODocType;
        }
        
        public ICollection<ItemCategory_SODocType> colGetItemCategory_SODocType(string argClientCode)
        {
            List<ItemCategory_SODocType> lst = new List<ItemCategory_SODocType>();
            DataSet DataSetToFill = new DataSet();
            ItemCategory_SODocType tItemCategory_SODocType = new ItemCategory_SODocType();

            DataSetToFill = this.GetItemCategory_SODocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateItemCategory_SODocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<ItemCategory_SODocType> colGetItemCategory_SODocType(DataTable dt, string argUserName, string clientCode)
        {
            List<ItemCategory_SODocType> lst = new List<ItemCategory_SODocType>();
            ItemCategory_SODocType objItemCategory_SODocType = null;
            foreach (DataRow dr in dt.Rows)
            {
                objItemCategory_SODocType = new ItemCategory_SODocType();
                objItemCategory_SODocType.SOTypeCode = Convert.ToString(dr["SOTypeCode"]).Trim();
                objItemCategory_SODocType.SOTypeDesc = Convert.ToString(dr["SOTypeDesc"]).Trim();
                objItemCategory_SODocType.ItemCatGroupCode = Convert.ToString(dr["ItemCatGroupCode"]).Trim();
                objItemCategory_SODocType.HLItemCategoryCode = Convert.ToString(dr["HLItemCategoryCode"]).Trim();
                objItemCategory_SODocType.ItemCategoryCode = Convert.ToString(dr["ItemCategoryCode"]).Trim();
                objItemCategory_SODocType.OptItemCategoryCode1 = Convert.ToString(dr["OptItemCategoryCode1"]).Trim();
                objItemCategory_SODocType.OptItemCategoryCode2 = Convert.ToString(dr["OptItemCategoryCode2"]).Trim();
                objItemCategory_SODocType.OptItemCategoryCode3 = Convert.ToString(dr["OptItemCategoryCode3"]).Trim();
                objItemCategory_SODocType.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objItemCategory_SODocType.ModifiedBy = Convert.ToString(argUserName).Trim();
                objItemCategory_SODocType.CreatedBy = Convert.ToString(argUserName).Trim();
                objItemCategory_SODocType.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objItemCategory_SODocType);
            }
            return lst;
        }

        public DataSet GetItemCategory_SODocType(string argSOTypeCode, string argItemCategoryCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
            param[1] = new SqlParameter("@ItemCategoryCode", argItemCategoryCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_SODocType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory_SODocType(string argSOTypeCode, string argItemCategoryCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
            param[1] = new SqlParameter("@ItemCategoryCode", argItemCategoryCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetItemCategory_SODocType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory_SODocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_SODocType", param);
            return DataSetToFill;
        }
        
        public DataSet GetItemCategory_SODocType(string argSOTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_SODocType4SODoc", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory_SODocType4Combo(string argSOTypeCode, string argItemCatGroupCode, string argHLItemCategoryCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
            param[1] = new SqlParameter("@ItemCatGroupCode", argItemCatGroupCode);
            param[2] = new SqlParameter("@HLItemCategoryCode", argHLItemCategoryCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_SODocType4Combo", param);
            return DataSetToFill;
        }
                
        private ItemCategory_SODocType objCreateItemCategory_SODocType(DataRow dr)
        {
            ItemCategory_SODocType tItemCategory_SODocType = new ItemCategory_SODocType();
            tItemCategory_SODocType.SetObjectInfo(dr);
            return tItemCategory_SODocType;
        }
        
        public ICollection<ErrorHandler> SaveItemCategory_SODocType(ItemCategory_SODocType argItemCategory_SODocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsItemCategory_SODocTypeExists(argItemCategory_SODocType.SOTypeCode, argItemCategory_SODocType.ItemCategoryCode, argItemCategory_SODocType.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertItemCategory_SODocType(argItemCategory_SODocType, da, lstErr);
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
                    UpdateItemCategory_SODocType(argItemCategory_SODocType, da, lstErr);
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

        public ICollection<ErrorHandler> SaveItemCategory_SODocType(ICollection<ItemCategory_SODocType> colGetItemCategory_SODocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (ItemCategory_SODocType argItemCategory_SODocType in colGetItemCategory_SODocType)
                {
                    if (blnIsItemCategory_SODocTypeExists(argItemCategory_SODocType.SOTypeCode, argItemCategory_SODocType.ItemCategoryCode, argItemCategory_SODocType.ClientCode, da) == false)
                    {
                        InsertItemCategory_SODocType(argItemCategory_SODocType, da, lstErr);
                    }
                    else
                    {
                        UpdateItemCategory_SODocType(argItemCategory_SODocType, da, lstErr); 
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

        public ICollection<ErrorHandler> SaveItemCategory_SODocType(ICollection<ItemCategory_SODocType> colGetItemCategory_SODocType, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (ItemCategory_SODocType argItemCategory_SODocType in colGetItemCategory_SODocType)
                {
                    if (argItemCategory_SODocType.IsDeleted == 0)
                    {
                        if (blnIsItemCategory_SODocTypeExists(argItemCategory_SODocType.SOTypeCode, argItemCategory_SODocType.ItemCategoryCode, argItemCategory_SODocType.ClientCode, da) == false)
                        {
                            InsertItemCategory_SODocType(argItemCategory_SODocType, da, lstErr);
                        }
                        else
                        {
                            UpdateItemCategory_SODocType(argItemCategory_SODocType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteItemCategory_SODocType(argItemCategory_SODocType.SOTypeCode, argItemCategory_SODocType.ItemCategoryCode, argItemCategory_SODocType.ClientCode);
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
                    SaveItemCategory_SODocType(colGetItemCategory_SODocType(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertItemCategory_SODocType(ItemCategory_SODocType argItemCategory_SODocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@SOTypeCode", argItemCategory_SODocType.SOTypeCode);
            param[1] = new SqlParameter("@SOTypeDesc", argItemCategory_SODocType.SOTypeDesc);
            param[2] = new SqlParameter("@ItemCatGroupCode", argItemCategory_SODocType.ItemCatGroupCode);
            param[3] = new SqlParameter("@HLItemCategoryCode", argItemCategory_SODocType.HLItemCategoryCode);
            param[4] = new SqlParameter("@ItemCategoryCode", argItemCategory_SODocType.ItemCategoryCode);
            param[5] = new SqlParameter("@OptItemCategoryCode1", argItemCategory_SODocType.OptItemCategoryCode1);
            param[6] = new SqlParameter("@OptItemCategoryCode2", argItemCategory_SODocType.OptItemCategoryCode2);
            param[7] = new SqlParameter("@OptItemCategoryCode3", argItemCategory_SODocType.OptItemCategoryCode3);
            param[8] = new SqlParameter("@ClientCode", argItemCategory_SODocType.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argItemCategory_SODocType.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argItemCategory_SODocType.ModifiedBy);
            
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertItemCategory_SODocType", param);

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
        
        public void UpdateItemCategory_SODocType(ItemCategory_SODocType argItemCategory_SODocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@SOTypeCode", argItemCategory_SODocType.SOTypeCode);
            param[1] = new SqlParameter("@SOTypeDesc", argItemCategory_SODocType.SOTypeDesc);
            param[2] = new SqlParameter("@ItemCatGroupCode", argItemCategory_SODocType.ItemCatGroupCode);
            param[3] = new SqlParameter("@HLItemCategoryCode", argItemCategory_SODocType.HLItemCategoryCode);
            param[4] = new SqlParameter("@ItemCategoryCode", argItemCategory_SODocType.ItemCategoryCode);
            param[5] = new SqlParameter("@OptItemCategoryCode1", argItemCategory_SODocType.OptItemCategoryCode1);
            param[6] = new SqlParameter("@OptItemCategoryCode2", argItemCategory_SODocType.OptItemCategoryCode2);
            param[7] = new SqlParameter("@OptItemCategoryCode3", argItemCategory_SODocType.OptItemCategoryCode3);
            param[8] = new SqlParameter("@ClientCode", argItemCategory_SODocType.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argItemCategory_SODocType.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argItemCategory_SODocType.ModifiedBy);
            
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateItemCategory_SODocType", param);

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
        
        public ICollection<ErrorHandler> DeleteItemCategory_SODocType(string argSOTypeCode, string argItemCategoryCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
                param[1] = new SqlParameter("@ItemCategoryCode", argItemCategoryCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;
                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteItemCategory_SODocType", param);

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
        
        public bool blnIsItemCategory_SODocTypeExists(string argSOTypeCode, string argItemCategoryCode, string argClientCode)
        {
            bool IsItemCategory_SODocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetItemCategory_SODocType(argSOTypeCode, argItemCategoryCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemCategory_SODocTypeExists = true;
            }
            else
            {
                IsItemCategory_SODocTypeExists = false;
            }
            return IsItemCategory_SODocTypeExists;
        }

        public bool blnIsItemCategory_SODocTypeExists(string argSOTypeCode, string argItemCategoryCode, string argClientCode,DataAccess da)
        {
            bool IsItemCategory_SODocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetItemCategory_SODocType(argSOTypeCode, argItemCategoryCode, argClientCode,da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemCategory_SODocTypeExists = true;
            }
            else
            {
                IsItemCategory_SODocTypeExists = false;
            }
            return IsItemCategory_SODocTypeExists;
        }
    }
}