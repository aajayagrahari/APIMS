
//Created On :: 06, September, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_Comman
{
    public class ItemTypeManager
    {
        const string ItemTypeTable = "ItemType";

       ///GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public ItemType objGetItemType(string argItemTypeCode)
        {
            ItemType argItemType = new ItemType();
            DataSet DataSetToFill = new DataSet();

            if (argItemTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetItemType(argItemTypeCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argItemType = this.objCreateItemType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argItemType;
        }
        
        public ICollection<ItemType> colGetItemType()
        {
            List<ItemType> lst = new List<ItemType>();
            DataSet DataSetToFill = new DataSet();
            ItemType tItemType = new ItemType();

            DataSetToFill = this.GetItemType();

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateItemType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
      
        public DataSet GetItemType(string argItemTypeCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ItemTypeCode", argItemTypeCode);

            DataSetToFill = da.FillDataSet("SP_GetItemType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetItemType(string argItemTypeCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ItemTypeCode", argItemTypeCode);

            DataSetToFill = da.NFillDataSet("SP_GetItemType4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetItemType()
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            DataSetToFill = da.FillDataSet("SP_GetItemType");
            return DataSetToFill;
        }
        
        private ItemType objCreateItemType(DataRow dr)
        {
            ItemType tItemType = new ItemType();

            tItemType.SetObjectInfo(dr);

            return tItemType;

        }

        public ICollection<ErrorHandler> SaveItemType(ItemType argItemType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsItemTypeExists(argItemType.ItemTypeCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertItemType(argItemType, da, lstErr);
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
                    UpdateItemType(argItemType, da, lstErr);
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

        public void SaveItemType(ItemType argItemType, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsItemTypeExists(argItemType.ItemTypeCode, da) == false)
                {
                    InsertItemType(argItemType, da, lstErr);
                }
                else
                {
                    UpdateItemType(argItemType, da, lstErr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Bulk Insert
        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
            ItemType ObjItemType = null;
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
                        ObjItemType = new ItemType();

                        ObjItemType.ItemTypeCode = Convert.ToString(drExcel["ItemTypeCode"]).Trim();
                        ObjItemType.ItemTypeDesc = Convert.ToString(drExcel["ItemTypeDesc"]).Trim();

                        SaveItemType(ObjItemType, da, lstErr);

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
        #endregion
        
        public void InsertItemType(ItemType argItemType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@ItemTypeCode", argItemType.ItemTypeCode);
            param[1] = new SqlParameter("@ItemTypeDesc", argItemType.ItemTypeDesc);
            param[2] = new SqlParameter("@IsDeleted", argItemType.IsDeleted);

            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertItemType", param);


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

        public void UpdateItemType(ItemType argItemType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@ItemTypeCode", argItemType.ItemTypeCode);
            param[1] = new SqlParameter("@ItemTypeDesc", argItemType.ItemTypeDesc);
            param[2] = new SqlParameter("@IsDeleted", argItemType.IsDeleted);

            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateItemType", param);


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
        
        public ICollection<ErrorHandler> DeleteItemType(string argItemTypeCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@ItemTypeCode", argItemTypeCode);

                param[1] = new SqlParameter("@Type", SqlDbType.Char);
                param[1].Size = 1;
                param[1].Direction = ParameterDirection.Output;
                param[2] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[2].Size = 255;
                param[2].Direction = ParameterDirection.Output;
                param[3] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[3].Size = 20;
                param[3].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteItemType", param);


                string strMessage = Convert.ToString(param[2].Value);
                string strType = Convert.ToString(param[1].Value);
                string strRetValue = Convert.ToString(param[3].Value);


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
        
        public bool blnIsItemTypeExists(string argItemTypeCode)
        {
            bool IsItemTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetItemType(argItemTypeCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemTypeExists = true;
            }
            else
            {
                IsItemTypeExists = false;
            }
            return IsItemTypeExists;
        }

        public bool blnIsItemTypeExists(string argItemTypeCode, DataAccess da)
        {
            bool IsItemTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetItemType(argItemTypeCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemTypeExists = true;
            }
            else
            {
                IsItemTypeExists = false;
            }
            return IsItemTypeExists;
        }
    }
}