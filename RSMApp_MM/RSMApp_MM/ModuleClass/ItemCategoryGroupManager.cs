
//Created On :: 10, September, 2012
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
    public class ItemCategoryGroupManager
    {
        const string ItemCategoryGroupTable = "ItemCategoryGroup";

        // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public ItemCategoryGroup objGetItemCategoryGroup(string argItemCatGroupCode, string argClientCode)
        {
            ItemCategoryGroup argItemCategoryGroup = new ItemCategoryGroup();
            DataSet DataSetToFill = new DataSet();

            if (argItemCatGroupCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetItemCategoryGroup(argItemCatGroupCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argItemCategoryGroup = this.objCreateItemCategoryGroup((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argItemCategoryGroup;
        }

        public ICollection<ItemCategoryGroup> colGetItemCategoryGroup(string argClientCode)
        {
            List<ItemCategoryGroup> lst = new List<ItemCategoryGroup>();
            DataSet DataSetToFill = new DataSet();
            ItemCategoryGroup tItemCategoryGroup = new ItemCategoryGroup();

            DataSetToFill = this.GetItemCategoryGroup(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateItemCategoryGroup(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetItemCategoryGroup(string argItemCatGroupCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ItemCatGroupCode", argItemCatGroupCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetItemCategoryGroup4ID", param);

            return DataSetToFill;
        }

        public DataSet GetItemCategoryGroup(string argItemCatGroupCode, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ItemCatGroupCode", argItemCatGroupCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetItemCategoryGroup4ID", param);

            return DataSetToFill;
        }

        public DataSet GetItemCategoryGroup(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetItemCategoryGroup",param);
            return DataSetToFill;
        }

        public DataSet GetItemCategoryGroup(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + ItemCategoryGroupTable.ToString();

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

        private ItemCategoryGroup objCreateItemCategoryGroup(DataRow dr)
        {
            ItemCategoryGroup tItemCategoryGroup = new ItemCategoryGroup();

            tItemCategoryGroup.SetObjectInfo(dr);

            return tItemCategoryGroup;

        }

        public ICollection<ErrorHandler> SaveItemCategoryGroup(ItemCategoryGroup argItemCategoryGroup)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsItemCategoryGroupExists(argItemCategoryGroup.ItemCatGroupCode, argItemCategoryGroup.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertItemCategoryGroup(argItemCategoryGroup, da, lstErr);
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
                    UpdateItemCategoryGroup(argItemCategoryGroup, da, lstErr);
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

        public void SaveItemCategoryGroup(ItemCategoryGroup argItemCategoryGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsItemCategoryGroupExists(argItemCategoryGroup.ItemCatGroupCode, argItemCategoryGroup.ClientCode, da) == false)
                {
                    InsertItemCategoryGroup(argItemCategoryGroup, da, lstErr);
                }
                else
                {
                    UpdateItemCategoryGroup(argItemCategoryGroup, da, lstErr);
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
            ItemCategoryGroup ObjItemCategoryGroup = null;
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


                        ObjItemCategoryGroup = new ItemCategoryGroup();

                        ObjItemCategoryGroup.ItemCatGroupCode = Convert.ToString(drExcel["ItemCatGroupCode"]).Trim();
                        ObjItemCategoryGroup.ItemCatGroupDesc = Convert.ToString(drExcel["ItemCatGroupDesc"]).Trim();
                        ObjItemCategoryGroup.CreatedBy = Convert.ToString(argUserName);
                        ObjItemCategoryGroup.ModifiedBy = Convert.ToString(argUserName);
                        ObjItemCategoryGroup.ClientCode = Convert.ToString(argClientCode);

                        SaveItemCategoryGroup(ObjItemCategoryGroup, da, lstErr);

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

        public void InsertItemCategoryGroup(ItemCategoryGroup argItemCategoryGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@ItemCatGroupCode", argItemCategoryGroup.ItemCatGroupCode);
            param[1] = new SqlParameter("@ItemCatGroupDesc", argItemCategoryGroup.ItemCatGroupDesc);
            param[2] = new SqlParameter("@ClientCode", argItemCategoryGroup.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argItemCategoryGroup.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argItemCategoryGroup.ModifiedBy);
        

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertItemCategoryGroup", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public void UpdateItemCategoryGroup(ItemCategoryGroup argItemCategoryGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@ItemCatGroupCode", argItemCategoryGroup.ItemCatGroupCode);
            param[1] = new SqlParameter("@ItemCatGroupDesc", argItemCategoryGroup.ItemCatGroupDesc);
            param[2] = new SqlParameter("@ClientCode", argItemCategoryGroup.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argItemCategoryGroup.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argItemCategoryGroup.ModifiedBy);


            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateItemCategoryGroup", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public ICollection<ErrorHandler> DeleteItemCategoryGroup(string argItemCatGroupCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@ItemCatGroupCode", argItemCatGroupCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteItemCategoryGroup", param);


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

        public bool blnIsItemCategoryGroupExists(string argItemCatGroupCode, string argClientCode)
        {
            bool IsItemCategoryGroupExists = false;
            DataSet ds = new DataSet();
            ds = GetItemCategoryGroup(argItemCatGroupCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemCategoryGroupExists = true;
            }
            else
            {
                IsItemCategoryGroupExists = false;
            }
            return IsItemCategoryGroupExists;
        }

        public bool blnIsItemCategoryGroupExists(string argItemCatGroupCode, string argClientCode,DataAccess da)
        {
            bool IsItemCategoryGroupExists = false;
            DataSet ds = new DataSet();
            ds = GetItemCategoryGroup(argItemCatGroupCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsItemCategoryGroupExists = true;
            }
            else
            {
                IsItemCategoryGroupExists = false;
            }
            return IsItemCategoryGroupExists;
        }
    }
}