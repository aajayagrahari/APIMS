
//Created On :: 06, September, 2012
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
    public class DeliveryItemCategoryManager
    {
        const string DeliveryItemCategoryTable = "DeliveryItemCategory";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public DeliveryItemCategory objGetDeliveryItemCategory(string argDItemCategoryCode, string argClientCode)
        {
            DeliveryItemCategory argDeliveryItemCategory = new DeliveryItemCategory();
            DataSet DataSetToFill = new DataSet();

            if (argDItemCategoryCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetDeliveryItemCategory(argDItemCategoryCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argDeliveryItemCategory = this.objCreateDeliveryItemCategory((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argDeliveryItemCategory;
        }

        public ICollection<DeliveryItemCategory> colGetDeliveryItemCategory(string argClientCode)
        {
            List<DeliveryItemCategory> lst = new List<DeliveryItemCategory>();
            DataSet DataSetToFill = new DataSet();
            DeliveryItemCategory tDeliveryItemCategory = new DeliveryItemCategory();

            DataSetToFill = this.GetDeliveryItemCategory(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDeliveryItemCategory(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetDeliveryItemCategory(string argDItemCategoryCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DItemCategoryCode", argDItemCategoryCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDeliveryItemCategory4ID", param);

            return DataSetToFill;
        }

        public DataSet GetDeliveryItemCategory(string argDItemCategoryCode, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DItemCategoryCode", argDItemCategoryCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetDeliveryItemCategory4ID", param);

            return DataSetToFill;
        }

        public DataSet GetDeliveryItemCategory(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@ClientCode", argClientCode);
            
            DataSetToFill = da.FillDataSet("SP_GetDeliveryItemCategory",param);
            return DataSetToFill;
        }

        private DeliveryItemCategory objCreateDeliveryItemCategory(DataRow dr)
        {
            DeliveryItemCategory tDeliveryItemCategory = new DeliveryItemCategory();

            tDeliveryItemCategory.SetObjectInfo(dr);

            return tDeliveryItemCategory;

        }

        public DataSet GetDeliveryItemCategory(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + DeliveryItemCategoryTable.ToString();

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

        public ICollection<ErrorHandler> SaveDeliveryItemCategory(DeliveryItemCategory argDeliveryItemCategory)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsDeliveryItemCategoryExists(argDeliveryItemCategory.DItemCategoryCode, argDeliveryItemCategory.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertDeliveryItemCategory(argDeliveryItemCategory, da, lstErr);
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
                    UpdateDeliveryItemCategory(argDeliveryItemCategory, da, lstErr);
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

        public void SaveDeliveryItemCategory(DeliveryItemCategory argDeliveryItemCategory, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsDeliveryItemCategoryExists(argDeliveryItemCategory.DItemCategoryCode, argDeliveryItemCategory.ClientCode, da) == false)
                {
                    InsertDeliveryItemCategory(argDeliveryItemCategory, da, lstErr);
                }
                else
                {
                    UpdateDeliveryItemCategory(argDeliveryItemCategory, da, lstErr);
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
            DeliveryItemCategory ObjDeliveryItemCategory = null;
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


                        ObjDeliveryItemCategory = new DeliveryItemCategory();

                        ObjDeliveryItemCategory.DItemCategoryCode = Convert.ToString(drExcel["DItemCategoryCode"]).Trim();
                        ObjDeliveryItemCategory.DItemCategoryDesc = Convert.ToString(drExcel["DItemCategoryDesc"]).Trim();
                        ObjDeliveryItemCategory.BillingRelevant = Convert.ToInt32(drExcel["BillingRelevant"]);
                        ObjDeliveryItemCategory.IsReturn = Convert.ToInt32(drExcel["IsReturn"]);
                        ObjDeliveryItemCategory.CreatedBy = Convert.ToString(argUserName);
                        ObjDeliveryItemCategory.ModifiedBy = Convert.ToString(argUserName);
                        ObjDeliveryItemCategory.ClientCode = Convert.ToString(argClientCode);

                        SaveDeliveryItemCategory(ObjDeliveryItemCategory, da, lstErr);

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

        public void InsertDeliveryItemCategory(DeliveryItemCategory argDeliveryItemCategory, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@DItemCategoryCode", argDeliveryItemCategory.DItemCategoryCode);
            param[1] = new SqlParameter("@DItemCategoryDesc", argDeliveryItemCategory.DItemCategoryDesc);
            param[2] = new SqlParameter("@BillingRelevant", argDeliveryItemCategory.BillingRelevant);
            param[3] = new SqlParameter("@IsReturn", argDeliveryItemCategory.IsReturn);
            param[4] = new SqlParameter("@ClientCode", argDeliveryItemCategory.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argDeliveryItemCategory.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argDeliveryItemCategory.ModifiedBy);
           

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertDeliveryItemCategory", param);
            
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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public void UpdateDeliveryItemCategory(DeliveryItemCategory argDeliveryItemCategory, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@DItemCategoryCode", argDeliveryItemCategory.DItemCategoryCode);
            param[1] = new SqlParameter("@DItemCategoryDesc", argDeliveryItemCategory.DItemCategoryDesc);
            param[2] = new SqlParameter("@BillingRelevant", argDeliveryItemCategory.BillingRelevant);
            param[3] = new SqlParameter("@IsReturn", argDeliveryItemCategory.IsReturn);
            param[4] = new SqlParameter("@ClientCode", argDeliveryItemCategory.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argDeliveryItemCategory.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argDeliveryItemCategory.ModifiedBy);
      

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateDeliveryItemCategory", param);
            
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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public ICollection<ErrorHandler> DeleteDeliveryItemCategory(string argDItemCategoryCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@DItemCategoryCode", argDItemCategoryCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteDeliveryItemCategory", param);


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

        public bool blnIsDeliveryItemCategoryExists(string argDItemCategoryCode, string argClientCode)
        {
            bool IsDeliveryItemCategoryExists = false;
            DataSet ds = new DataSet();
            ds = GetDeliveryItemCategory(argDItemCategoryCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDeliveryItemCategoryExists = true;
            }
            else
            {
                IsDeliveryItemCategoryExists = false;
            }
            return IsDeliveryItemCategoryExists;
        }

        public bool blnIsDeliveryItemCategoryExists(string argDItemCategoryCode, string argClientCode,DataAccess da)
        {
            bool IsDeliveryItemCategoryExists = false;
            DataSet ds = new DataSet();
            ds = GetDeliveryItemCategory(argDItemCategoryCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDeliveryItemCategoryExists = true;
            }
            else
            {
                IsDeliveryItemCategoryExists = false;
            }
            return IsDeliveryItemCategoryExists;
        }
    }
}