
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
    public class ItemCategory_MatMovementTypeManager
    {
        const string ItemCategory_MatMovementTypeTable = "ItemCategory_MatMovementType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public ItemCategory_MatMovementType objGetItemCategory_MatMovementType(string argItemCategoryCode, string argMatMovementCode, string argClientCode)
        {
            ItemCategory_MatMovementType argItemCategory_MatMovementType = new ItemCategory_MatMovementType();
            DataSet DataSetToFill = new DataSet();

            if (argItemCategoryCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argMatMovementCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetItemCategory_MatMovementType(argItemCategoryCode, argMatMovementCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argItemCategory_MatMovementType = this.objCreateItemCategory_MatMovementType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argItemCategory_MatMovementType;
        }
        
        public ICollection<ItemCategory_MatMovementType> colGetItemCategory_MatMovementType(string argClientCode)
        {
            List<ItemCategory_MatMovementType> lst = new List<ItemCategory_MatMovementType>();
            DataSet DataSetToFill = new DataSet();
            ItemCategory_MatMovementType tItemCategory_MatMovementType = new ItemCategory_MatMovementType();

            DataSetToFill = this.GetItemCategory_MatMovementType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateItemCategory_MatMovementType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<ItemCategory_MatMovementType> colGetItemCategory_MatMovementType(DataTable dt, string argUserName, string clientCode)
        {
            List<ItemCategory_MatMovementType> lst = new List<ItemCategory_MatMovementType>();
            ItemCategory_MatMovementType objItemCategory_MatMovementType = null;
            foreach (DataRow dr in dt.Rows)
            {
                objItemCategory_MatMovementType = new ItemCategory_MatMovementType();
                objItemCategory_MatMovementType.ItemCategoryCode = Convert.ToString(dr["ItemCategoryCode"]).Trim();
                objItemCategory_MatMovementType.MatMovementCode = Convert.ToString(dr["MatMovementCode"]).Trim();
                objItemCategory_MatMovementType.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objItemCategory_MatMovementType.ModifiedBy = Convert.ToString(argUserName).Trim();
                objItemCategory_MatMovementType.CreatedBy = Convert.ToString(argUserName).Trim();
                objItemCategory_MatMovementType.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objItemCategory_MatMovementType);
            }
            return lst;
        }

        public DataSet GetItemCategory_MatMovementType4Combo(string argItemCategoryCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ItemCategoryCode", argItemCategoryCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_MatMovementType4Combo", param);

            return DataSetToFill;
        }

        public DataSet GetItemCategory_MatMovementType(string argItemCategoryCode, string argMatMovementCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ItemCategoryCode", argItemCategoryCode);
            param[1] = new SqlParameter("@MatMovementCode", argMatMovementCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategory_MatMovementType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetItemCategory_MatMovementType(string argItemCategoryCode, string argMatMovementCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ItemCategoryCode", argItemCategoryCode);
            param[1] = new SqlParameter("@MatMovementCode", argMatMovementCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetItemCategory_MatMovementType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetItemCategory_MatMovementType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetItemCategory_MatMovementType",param);
            return DataSetToFill;
        }

        private ItemCategory_MatMovementType objCreateItemCategory_MatMovementType(DataRow dr)
        {
            ItemCategory_MatMovementType tItemCategory_MatMovementType = new ItemCategory_MatMovementType();

            tItemCategory_MatMovementType.SetObjectInfo(dr);

            return tItemCategory_MatMovementType;

        }

        public ICollection<ErrorHandler> SaveItemCategory_MatMovementType(ItemCategory_MatMovementType argItemCategory_MatMovementType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsItemCategory_MatMovementTypeExists(argItemCategory_MatMovementType.ItemCategoryCode, argItemCategory_MatMovementType.MatMovementCode, argItemCategory_MatMovementType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertItemCategory_MatMovementType(argItemCategory_MatMovementType, da, lstErr);
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
                    UpdateItemCategory_MatMovementType(argItemCategory_MatMovementType, da, lstErr);
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

        public ICollection<ErrorHandler> SaveItemCategory_MatMovementType(ICollection<ItemCategory_MatMovementType> colGetItemCategory_MatMovementType, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (ItemCategory_MatMovementType argItemCategory_MatMovementType in colGetItemCategory_MatMovementType)
                {
                    if (argItemCategory_MatMovementType.IsDeleted == 0)
                    {
                        if (blnIsItemCategory_MatMovementTypeExists(argItemCategory_MatMovementType.ItemCategoryCode, argItemCategory_MatMovementType.MatMovementCode, argItemCategory_MatMovementType.ClientCode, da) == false)
                        {
                            InsertItemCategory_MatMovementType(argItemCategory_MatMovementType, da, lstErr);
                        }
                        else
                        {
                            UpdateItemCategory_MatMovementType(argItemCategory_MatMovementType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteItemCategory_MatMovementType(argItemCategory_MatMovementType.ItemCategoryCode, argItemCategory_MatMovementType.MatMovementCode, argItemCategory_MatMovementType.ClientCode, argItemCategory_MatMovementType.IsDeleted);
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
                    SaveItemCategory_MatMovementType(colGetItemCategory_MatMovementType(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertItemCategory_MatMovementType(ItemCategory_MatMovementType argItemCategory_MatMovementType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@ItemCategoryCode", argItemCategory_MatMovementType.ItemCategoryCode);
            param[1] = new SqlParameter("@MatMovementCode", argItemCategory_MatMovementType.MatMovementCode);
            param[2] = new SqlParameter("@ClientCode", argItemCategory_MatMovementType.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argItemCategory_MatMovementType.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argItemCategory_MatMovementType.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertItemCategory_MatMovementType", param);


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

        public void UpdateItemCategory_MatMovementType(ItemCategory_MatMovementType argItemCategory_MatMovementType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@ItemCategoryCode", argItemCategory_MatMovementType.ItemCategoryCode);
            param[1] = new SqlParameter("@MatMovementCode", argItemCategory_MatMovementType.MatMovementCode);
            param[2] = new SqlParameter("@ClientCode", argItemCategory_MatMovementType.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argItemCategory_MatMovementType.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argItemCategory_MatMovementType.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateItemCategory_MatMovementType", param);


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

        public ICollection<ErrorHandler> DeleteItemCategory_MatMovementType(string argItemCategoryCode, string argMatMovementCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@ItemCategoryCode", argItemCategoryCode);
                param[1] = new SqlParameter("@MatMovementCode", argMatMovementCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteItemCategory_MatMovementType", param);


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

        public bool blnIsItemCategory_MatMovementTypeExists(string argItemCategoryCode, string argMatMovementCode, string argClientCode)
        {
            bool IsItemCategory_MatMovementTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetItemCategory_MatMovementType(argItemCategoryCode, argMatMovementCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemCategory_MatMovementTypeExists = true;
            }
            else
            {
                IsItemCategory_MatMovementTypeExists = false;
            }
            return IsItemCategory_MatMovementTypeExists;
        }

        public bool blnIsItemCategory_MatMovementTypeExists(string argItemCategoryCode, string argMatMovementCode, string argClientCode, DataAccess da)
        {
            bool IsItemCategory_MatMovementTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetItemCategory_MatMovementType(argItemCategoryCode, argMatMovementCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemCategory_MatMovementTypeExists = true;
            }
            else
            {
                IsItemCategory_MatMovementTypeExists = false;
            }
            return IsItemCategory_MatMovementTypeExists;
        }
    }
}