
//Created On :: 16, May, 2012
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
    public class ItemCategoryManager
    {
        const string ItemCategoryTable = "ItemCategory";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public ItemCategory objGetItemCategory(string argItemCategoryCode, string argClientCode)
        {
            ItemCategory argItemCategory = new ItemCategory();
            DataSet DataSetToFill = new DataSet();

            if (argItemCategoryCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetItemCategory(argItemCategoryCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argItemCategory = this.objCreateItemCategory((DataRow)DataSetToFill.Tables[0].Rows[0]);
            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argItemCategory;
        }

        public ICollection<ItemCategory> colGetItemCategory(string argClientCode)
        {
            List<ItemCategory> lst = new List<ItemCategory>();
            DataSet DataSetToFill = new DataSet();
            ItemCategory tItemCategory = new ItemCategory();

            DataSetToFill = this.GetItemCategory(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateItemCategory(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetItemCategory(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + ItemCategoryTable.ToString();

                if (iIsDeleted > -1)
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;
                    tSQL = tSQL + " " + sWhereClauseSTR + " IsDeleted = " + iIsDeleted;
                }

                if (argClientCode != "")
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;
                    tSQL = tSQL + " " + sWhereClauseSTR + " ClientCode = '" + argClientCode + "'";
                }
                ds = da.FillDataSetWithSQL(tSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetItemCategory(string argItemCategoryCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ItemCategoryCode", argItemCategoryCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory4ID", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory(string argItemCategoryCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ItemCategoryCode", argItemCategoryCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetItemCategory4ID", param);
            return DataSetToFill;
        }

        public DataSet GetItemCategory(string argClientCode)
        {   
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory",param);
            return DataSetToFill;
        }

        private ItemCategory objCreateItemCategory(DataRow dr)
        {
            ItemCategory tItemCategory = new ItemCategory();
            tItemCategory.SetObjectInfo(dr);
            return tItemCategory;
        }

        public ICollection<ErrorHandler> SaveItemCategory(ItemCategory argItemCategory)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsItemCategoryExists(argItemCategory.ItemCategoryCode, argItemCategory.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertItemCategory(argItemCategory, da, lstErr);
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
                    UpdateItemCategory(argItemCategory, da, lstErr);
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

        public void SaveItemCategory(ItemCategory argItemCategory, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsItemCategoryExists(argItemCategory.ItemCategoryCode, argItemCategory.ClientCode, da) == false)
                {
                    InsertItemCategory(argItemCategory, da, lstErr);
                }
                else
                {
                    UpdateItemCategory(argItemCategory, da, lstErr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
            ItemCategory ObjItemCategory = null;
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
                    foreach (DataRow drExcel in dtExcel.Rows)
                    {
                        ObjItemCategory = new ItemCategory();
                        ObjItemCategory.ItemCategoryCode = Convert.ToString(drExcel["ItemCategoryCode"]).Trim();
                        ObjItemCategory.ItemCategoryDesc = Convert.ToString(drExcel["ItemCategoryDesc"]).Trim();
                        ObjItemCategory.ScheduleLineAllowed = Convert.ToInt32(drExcel["ScheduleLineAllowed"]);
                        ObjItemCategory.IsBusinessItem = Convert.ToInt32(drExcel["IsBusinessItem"]);
                        ObjItemCategory.ItemTypeCode = Convert.ToString(drExcel["ItemTypeCode"]).Trim();
                        ObjItemCategory.DeliveryRelevant = Convert.ToInt32(drExcel["DeliveryRelevant"]);
                        ObjItemCategory.BillingRelevantCode = Convert.ToString(drExcel["BillingRelevantCode"]).Trim();
                        ObjItemCategory.IsReturn = Convert.ToInt32(drExcel["IsReturn"]);
                        ObjItemCategory.CreatedBy = Convert.ToString(argUserName);
                        ObjItemCategory.ModifiedBy = Convert.ToString(argUserName);
                        ObjItemCategory.ClientCode = Convert.ToString(argClientCode);
                        SaveItemCategory(ObjItemCategory, da, lstErr);

                        foreach (ErrorHandler objerr in lstErr)
                        {
                            if (objerr.Type == "E")
                            {
                                da.ROLLBACK_TRANSACTION();
                                break;
                            }
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

        public void InsertItemCategory(ItemCategory argItemCategory, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@ItemCategoryCode", argItemCategory.ItemCategoryCode);
            param[1] = new SqlParameter("@ItemCategoryDesc", argItemCategory.ItemCategoryDesc);
            param[2] = new SqlParameter("@ScheduleLineAllowed", argItemCategory.ScheduleLineAllowed);
            param[3] = new SqlParameter("@IsBusinessItem", argItemCategory.IsBusinessItem);
            param[4] = new SqlParameter("@ItemTypeCode", argItemCategory.ItemTypeCode);
            param[5] = new SqlParameter("@DeliveryRelevant", argItemCategory.DeliveryRelevant);
            param[6] = new SqlParameter("@BillingRelevantCode", argItemCategory.BillingRelevantCode);
            param[7] = new SqlParameter("@IsReturn", argItemCategory.IsReturn);
            param[8] = new SqlParameter("@ClientCode", argItemCategory.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argItemCategory.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argItemCategory.ModifiedBy);
            
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertItemCategory", param);

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

        public void UpdateItemCategory(ItemCategory argItemCategory, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@ItemCategoryCode", argItemCategory.ItemCategoryCode);
            param[1] = new SqlParameter("@ItemCategoryDesc", argItemCategory.ItemCategoryDesc);
            param[2] = new SqlParameter("@ScheduleLineAllowed", argItemCategory.ScheduleLineAllowed);
            param[3] = new SqlParameter("@IsBusinessItem", argItemCategory.IsBusinessItem);
            param[4] = new SqlParameter("@ItemTypeCode", argItemCategory.ItemTypeCode);
            param[5] = new SqlParameter("@DeliveryRelevant", argItemCategory.DeliveryRelevant);
            param[6] = new SqlParameter("@BillingRelevantCode", argItemCategory.BillingRelevantCode);
            param[7] = new SqlParameter("@IsReturn", argItemCategory.IsReturn);
            param[8] = new SqlParameter("@ClientCode", argItemCategory.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argItemCategory.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argItemCategory.ModifiedBy);

            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateItemCategory", param);

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

        public ICollection<ErrorHandler> DeleteItemCategory(string argItemCategoryCode, string argClientCode,int IisDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@ItemCategoryCode", argItemCategoryCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted", IisDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteItemCategory", param);

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

        public bool blnIsItemCategoryExists(string argItemCategoryCode, string argClientCode)
        {
            bool IsItemCategoryExists = false;
            DataSet ds = new DataSet();
            ds = GetItemCategory(argItemCategoryCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemCategoryExists = true;
            }
            else
            {
                IsItemCategoryExists = false;
            }
            return IsItemCategoryExists;
        }

        public bool blnIsItemCategoryExists(string argItemCategoryCode, string argClientCode, DataAccess da)
        {
            bool IsItemCategoryExists = false;
            DataSet ds = new DataSet();
            ds = GetItemCategory(argItemCategoryCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemCategoryExists = true;
            }
            else
            {
                IsItemCategoryExists = false;
            }
            return IsItemCategoryExists;
        }
    }
}