
//Created On :: 12, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_Partner
{
    public class RepairDocTypeManager
    {
        const string RepairDocTypeTable = "RepairDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public RepairDocType objGetRepairDocType(string argRepairDocTypeCode, string argClientCode)
        {
            RepairDocType argRepairDocType = new RepairDocType();
            DataSet DataSetToFill = new DataSet();

            if (argRepairDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetRepairDocType(argRepairDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argRepairDocType = this.objCreateRepairDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argRepairDocType;
        }

        public ICollection<RepairDocType> colGetRepairDocType(string argClientCode)
        {
            List<RepairDocType> lst = new List<RepairDocType>();
            DataSet DataSetToFill = new DataSet();
            RepairDocType tRepairDocType = new RepairDocType();

            DataSetToFill = this.GetRepairDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateRepairDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetRepairDocType(string argRepairDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RepairDocTypeCode", argRepairDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepairDocType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetRepairDocType(string argRepairDocTypeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RepairDocTypeCode", argRepairDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetRepairDocType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetRepairDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepairDocType",param);
            return DataSetToFill;
        }

        public DataSet GetRepairDocType(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + RepairDocTypeTable.ToString();

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

        private RepairDocType objCreateRepairDocType(DataRow dr)
        {
            RepairDocType tRepairDocType = new RepairDocType();
            tRepairDocType.SetObjectInfo(dr);
            return tRepairDocType;
        }

        public ICollection<ErrorHandler> SaveRepairDocType(RepairDocType argRepairDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsRepairDocTypeExists(argRepairDocType.RepairDocTypeCode, argRepairDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertRepairDocType(argRepairDocType, da, lstErr);
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
                    UpdateRepairDocType(argRepairDocType, da, lstErr);
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

        public void SaveRepairDocType(RepairDocType argRepairDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsRepairDocTypeExists(argRepairDocType.RepairDocTypeCode, argRepairDocType.ClientCode, da) == false)
                {
                    InsertRepairDocType(argRepairDocType, da, lstErr);
                }
                else
                {
                    UpdateRepairDocType(argRepairDocType, da, lstErr);
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
            RepairDocType ObjRepairDocType = null;
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
                        ObjRepairDocType = new RepairDocType();
                        ObjRepairDocType.RepairDocTypeCode = Convert.ToString(drExcel["RepairDocTypeCode"]).Trim();
                        ObjRepairDocType.RepairTypeDesc = Convert.ToString(drExcel["RepairTypeDesc"]).Trim();
                        ObjRepairDocType.ItemNoIncr = Convert.ToInt32(drExcel["ItemNoIncr"]);
                        ObjRepairDocType.NumRange = Convert.ToString(drExcel["NumRange"]).Trim();
                        ObjRepairDocType.SaveMode = Convert.ToInt32(drExcel["SaveMode"]);
                        ObjRepairDocType.RepairType = Convert.ToString(drExcel["RepairType"]).Trim();
                        ObjRepairDocType.RepairProcessCode = Convert.ToString(drExcel["RepairProcessCode"]).Trim();
                        ObjRepairDocType.CreatedBy = Convert.ToString(argUserName);
                        ObjRepairDocType.ModifiedBy = Convert.ToString(argUserName);
                        ObjRepairDocType.ClientCode = Convert.ToString(argClientCode);
                        SaveRepairDocType(ObjRepairDocType, da, lstErr);

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

        public void InsertRepairDocType(RepairDocType argRepairDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@RepairDocTypeCode", argRepairDocType.RepairDocTypeCode);
            param[1] = new SqlParameter("@RepairTypeDesc", argRepairDocType.RepairTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argRepairDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argRepairDocType.NumRange);
            param[4] = new SqlParameter("@SaveMode", argRepairDocType.SaveMode);
            param[5] = new SqlParameter("@RepairType", argRepairDocType.RepairType);
            param[6] = new SqlParameter("@RepairProcessCode", argRepairDocType.RepairProcessCode);
            param[7] = new SqlParameter("@ClientCode", argRepairDocType.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argRepairDocType.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argRepairDocType.ModifiedBy);
        
            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertRepairDocType", param);


            string strMessage = Convert.ToString(param[11].Value);
            string strType = Convert.ToString(param[10].Value);
            string strRetValue = Convert.ToString(param[12].Value);


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

        public void UpdateRepairDocType(RepairDocType argRepairDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@RepairDocTypeCode", argRepairDocType.RepairDocTypeCode);
            param[1] = new SqlParameter("@RepairTypeDesc", argRepairDocType.RepairTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argRepairDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argRepairDocType.NumRange);
            param[4] = new SqlParameter("@SaveMode", argRepairDocType.SaveMode);
            param[5] = new SqlParameter("@RepairType", argRepairDocType.RepairType);
            param[6] = new SqlParameter("@RepairProcessCode", argRepairDocType.RepairProcessCode);
            param[7] = new SqlParameter("@ClientCode", argRepairDocType.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argRepairDocType.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argRepairDocType.ModifiedBy);

            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateRepairDocType", param);

            string strMessage = Convert.ToString(param[11].Value);
            string strType = Convert.ToString(param[10].Value);
            string strRetValue = Convert.ToString(param[12].Value);

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

        public ICollection<ErrorHandler> DeleteRepairDocType(string argRepairDocTypeCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@RepairDocTypeCode", argRepairDocTypeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteRepairDocType", param);


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

        public bool blnIsRepairDocTypeExists(string argRepairDocTypeCode, string argClientCode)
        {
            bool IsRepairDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetRepairDocType(argRepairDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsRepairDocTypeExists = true;
            }
            else
            {
                IsRepairDocTypeExists = false;
            }
            return IsRepairDocTypeExists;
        }

        public bool blnIsRepairDocTypeExists(string argRepairDocTypeCode, string argClientCode, DataAccess da)
        {
            bool IsRepairDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetRepairDocType(argRepairDocTypeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsRepairDocTypeExists = true;
            }
            else
            {
                IsRepairDocTypeExists = false;
            }
            return IsRepairDocTypeExists;
        }
    }
}