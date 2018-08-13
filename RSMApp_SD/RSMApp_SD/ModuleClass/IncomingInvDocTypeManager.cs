
//Created On :: 17, October, 2012
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
    public class IncomingInvDocTypeManager
    {
        const string IncomingInvDocTypeTable = "IncomingInvDocType";

        // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public IncomingInvDocType objGetIncomingInvDocType(string argIncomingInvDocTypeCode, string argClientCode)
        {
            IncomingInvDocType argIncomingInvDocType = new IncomingInvDocType();
            DataSet DataSetToFill = new DataSet();

            if (argIncomingInvDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetIncomingInvDocType(argIncomingInvDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argIncomingInvDocType = this.objCreateIncomingInvDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argIncomingInvDocType;
        }

        public ICollection<IncomingInvDocType> colGetIncomingInvDocType(string argClientCode)
        {
            List<IncomingInvDocType> lst = new List<IncomingInvDocType>();
            DataSet DataSetToFill = new DataSet();
            IncomingInvDocType tIncomingInvDocType = new IncomingInvDocType();

            DataSetToFill = this.GetIncomingInvDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateIncomingInvDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetIncomingInvDocType(string argIncomingInvDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@IncomingInvDocTypeCode", argIncomingInvDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetIncomingInvDocType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetIncomingInvDocType(string argIncomingInvDocTypeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@IncomingInvDocTypeCode", argIncomingInvDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetIncomingInvDocType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetIncomingInvDocType(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + IncomingInvDocTypeTable.ToString();

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

        public DataSet GetIncomingInvDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetIncomingInvDocType",param);
            return DataSetToFill;
        }
        
        private IncomingInvDocType objCreateIncomingInvDocType(DataRow dr)
        {
            IncomingInvDocType tIncomingInvDocType = new IncomingInvDocType();

            tIncomingInvDocType.SetObjectInfo(dr);

            return tIncomingInvDocType;

        }
        
        public ICollection<ErrorHandler> SaveIncomingInvDocType(IncomingInvDocType argIncomingInvDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsIncomingInvDocTypeExists(argIncomingInvDocType.IncomingInvDocTypeCode, argIncomingInvDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertIncomingInvDocType(argIncomingInvDocType, da, lstErr);
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
                    UpdateIncomingInvDocType(argIncomingInvDocType, da, lstErr);
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

        public void SaveIncomingInvDocType(IncomingInvDocType argIncomingInvDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsIncomingInvDocTypeExists(argIncomingInvDocType.IncomingInvDocTypeCode, argIncomingInvDocType.ClientCode, da) == false)
                {
                    InsertIncomingInvDocType(argIncomingInvDocType, da, lstErr);
                }
                else
                {
                    UpdateIncomingInvDocType(argIncomingInvDocType, da, lstErr);
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
            IncomingInvDocType ObjIncomingInvDocType = null;
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
                        ObjIncomingInvDocType = new IncomingInvDocType();
                        ObjIncomingInvDocType.IncomingInvDocTypeCode = Convert.ToString(drExcel["IncomingInvDocTypeCode"]).Trim();
                        ObjIncomingInvDocType.IncomingInvTypeDesc = Convert.ToString(drExcel["IncomingInvTypeDesc"]).Trim();
                        ObjIncomingInvDocType.ItemNoIncr = Convert.ToInt32(drExcel["ItemNoIncr"]);
                        ObjIncomingInvDocType.NumRange = Convert.ToString(drExcel["NumRange"]).Trim();
                        ObjIncomingInvDocType.BasedOn = Convert.ToInt32(drExcel["BasedOn"]);
                        ObjIncomingInvDocType.SaveMode = Convert.ToInt32(drExcel["SaveMode"]);
                        ObjIncomingInvDocType.CreatedBy = Convert.ToString(argUserName);
                        ObjIncomingInvDocType.ModifiedBy = Convert.ToString(argUserName);
                        ObjIncomingInvDocType.ClientCode = Convert.ToString(argClientCode);
                        SaveIncomingInvDocType(ObjIncomingInvDocType, da, lstErr);

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
        
        public void InsertIncomingInvDocType(IncomingInvDocType argIncomingInvDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@IncomingInvDocTypeCode", argIncomingInvDocType.IncomingInvDocTypeCode);
            param[1] = new SqlParameter("@IncomingInvTypeDesc", argIncomingInvDocType.IncomingInvTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argIncomingInvDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argIncomingInvDocType.NumRange);
            param[4] = new SqlParameter("@BasedOn", argIncomingInvDocType.BasedOn);
            param[5] = new SqlParameter("@SaveMode", argIncomingInvDocType.SaveMode);
            param[6] = new SqlParameter("@ClientCode", argIncomingInvDocType.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argIncomingInvDocType.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argIncomingInvDocType.ModifiedBy);
   

            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertIncomingInvDocType", param);


            string strMessage = Convert.ToString(param[10].Value);
            string strType = Convert.ToString(param[9].Value);
            string strRetValue = Convert.ToString(param[11].Value);


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

        public void UpdateIncomingInvDocType(IncomingInvDocType argIncomingInvDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@IncomingInvDocTypeCode", argIncomingInvDocType.IncomingInvDocTypeCode);
            param[1] = new SqlParameter("@IncomingInvTypeDesc", argIncomingInvDocType.IncomingInvTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argIncomingInvDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argIncomingInvDocType.NumRange);
            param[4] = new SqlParameter("@BasedOn", argIncomingInvDocType.BasedOn);
            param[5] = new SqlParameter("@SaveMode", argIncomingInvDocType.SaveMode);
            param[6] = new SqlParameter("@ClientCode", argIncomingInvDocType.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argIncomingInvDocType.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argIncomingInvDocType.ModifiedBy);


            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateIncomingInvDocType", param);


            string strMessage = Convert.ToString(param[10].Value);
            string strType = Convert.ToString(param[9].Value);
            string strRetValue = Convert.ToString(param[11].Value);


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
        
        public ICollection<ErrorHandler> DeleteIncomingInvDocType(string argIncomingInvDocTypeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@IncomingInvDocTypeCode", argIncomingInvDocTypeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteIncomingInvDocType", param);


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
        
        public bool blnIsIncomingInvDocTypeExists(string argIncomingInvDocTypeCode, string argClientCode)
        {
            bool IsIncomingInvDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetIncomingInvDocType(argIncomingInvDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsIncomingInvDocTypeExists = true;
            }
            else
            {
                IsIncomingInvDocTypeExists = false;
            }
            return IsIncomingInvDocTypeExists;
        }

        public bool blnIsIncomingInvDocTypeExists(string argIncomingInvDocTypeCode, string argClientCode, DataAccess da)
        {
            bool IsIncomingInvDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetIncomingInvDocType(argIncomingInvDocTypeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsIncomingInvDocTypeExists = true;
            }
            else
            {
                IsIncomingInvDocTypeExists = false;
            }
            return IsIncomingInvDocTypeExists;
        }
    }
}