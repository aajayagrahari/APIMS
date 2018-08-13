
//Created On :: 29, August, 2012
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
    public class BillingDocTypeManager
    {
        const string BillingDocTypeTable = "BillingDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public BillingDocType objGetBillingDocType(string argBillingDocTypeCode, string argClientCode)
        {
            BillingDocType argBillingDocType = new BillingDocType();
            DataSet DataSetToFill = new DataSet();

            if (argBillingDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetBillingDocType(argBillingDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argBillingDocType = this.objCreateBillingDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argBillingDocType;
        }
        
        public ICollection<BillingDocType> colGetBillingDocType(string argClientCode)
        {
            List<BillingDocType> lst = new List<BillingDocType>();
            DataSet DataSetToFill = new DataSet();
            BillingDocType tBillingDocType = new BillingDocType();

            DataSetToFill = this.GetBillingDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateBillingDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetBillingDocType(string argBillingDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@BillingDocTypeCode", argBillingDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBillingDocType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetBillingDocType(string argBillingDocTypeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@BillingDocTypeCode", argBillingDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetBillingDocType4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetBillingDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBillingDocType",param);
            return DataSetToFill;
        }

        public DataSet GetBillingDocType(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + BillingDocTypeTable.ToString();

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
        
        private BillingDocType objCreateBillingDocType(DataRow dr)
        {
            BillingDocType tBillingDocType = new BillingDocType();

            tBillingDocType.SetObjectInfo(dr);

            return tBillingDocType;

        }
        
        public ICollection<ErrorHandler> SaveBillingDocType(BillingDocType argBillingDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsBillingDocTypeExists(argBillingDocType.BillingDocTypeCode, argBillingDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertBillingDocType(argBillingDocType, da, lstErr);
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
                    UpdateBillingDocType(argBillingDocType, da, lstErr);
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

        public void SaveBillingDocType(BillingDocType argBillingDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsBillingDocTypeExists(argBillingDocType.BillingDocTypeCode, argBillingDocType.ClientCode, da) == false)
                {
                    InsertBillingDocType(argBillingDocType, da, lstErr);
                }
                else
                {
                    UpdateBillingDocType(argBillingDocType, da, lstErr);
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
            BillingDocType ObjBillingDocType = null;
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
                        ObjBillingDocType = new BillingDocType();
                        ObjBillingDocType.BillingDocTypeCode = Convert.ToString(drExcel["BillingDocTypeCode"]).Trim();
                        ObjBillingDocType.BillingTypeDesc = Convert.ToString(drExcel["BillingTypeDesc"]).Trim();
                        ObjBillingDocType.CRLimitCheckType = Convert.ToString(drExcel["CRLimitCheckType"]).Trim();
                        ObjBillingDocType.ItemNoIncr = Convert.ToInt32(drExcel["ItemNoIncr"]);
                        ObjBillingDocType.NumRange = Convert.ToString(drExcel["NumRange"]).Trim();
                        ObjBillingDocType.RangeFrom = Convert.ToString(drExcel["RangeFrom"]).Trim();
                        ObjBillingDocType.RangeTo = Convert.ToString(drExcel["RangeTo"]).Trim();
                        ObjBillingDocType.BasedOn = Convert.ToInt32(drExcel["BasedOn"]);
                        ObjBillingDocType.SaveMode = Convert.ToInt32(drExcel["SaveMode"]);
                        ObjBillingDocType.CreatedBy = Convert.ToString(argUserName);
                        ObjBillingDocType.ModifiedBy = Convert.ToString(argUserName);
                        ObjBillingDocType.ClientCode = Convert.ToString(argClientCode);
                        SaveBillingDocType(ObjBillingDocType, da, lstErr);

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
        
        public void InsertBillingDocType(BillingDocType argBillingDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@BillingDocTypeCode", argBillingDocType.BillingDocTypeCode);
            param[1] = new SqlParameter("@BillingTypeDesc", argBillingDocType.BillingTypeDesc);
            param[2] = new SqlParameter("@CRLimitCheckType", argBillingDocType.CRLimitCheckType);
            param[3] = new SqlParameter("@ItemNoIncr", argBillingDocType.ItemNoIncr);
            param[4] = new SqlParameter("@NumRange", argBillingDocType.NumRange);
            param[5] = new SqlParameter("@RangeFrom", argBillingDocType.RangeFrom);
            param[6] = new SqlParameter("@RangeTo", argBillingDocType.RangeTo);
            param[7] = new SqlParameter("@BasedOn", argBillingDocType.BasedOn);
            param[8] = new SqlParameter("@SaveMode",argBillingDocType.SaveMode);
            param[9] = new SqlParameter("@ClientCode", argBillingDocType.ClientCode);
            param[10] = new SqlParameter("@CreatedBy", argBillingDocType.CreatedBy);
            param[11] = new SqlParameter("@ModifiedBy", argBillingDocType.ModifiedBy);

            param[12] = new SqlParameter("@Type", SqlDbType.Char);
            param[12].Size = 1;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[13].Size = 255;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[14].Size = 20;
            param[14].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertBillingDocType", param);
            
            string strMessage = Convert.ToString(param[13].Value);
            string strType = Convert.ToString(param[12].Value);
            string strRetValue = Convert.ToString(param[14].Value);


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

        public void UpdateBillingDocType(BillingDocType argBillingDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@BillingDocTypeCode", argBillingDocType.BillingDocTypeCode);
            param[1] = new SqlParameter("@BillingTypeDesc", argBillingDocType.BillingTypeDesc);
            param[2] = new SqlParameter("@CRLimitCheckType", argBillingDocType.CRLimitCheckType);
            param[3] = new SqlParameter("@ItemNoIncr", argBillingDocType.ItemNoIncr);
            param[4] = new SqlParameter("@NumRange", argBillingDocType.NumRange);
            param[5] = new SqlParameter("@RangeFrom", argBillingDocType.RangeFrom);
            param[6] = new SqlParameter("@RangeTo", argBillingDocType.RangeTo);
            param[7] = new SqlParameter("@BasedOn", argBillingDocType.BasedOn);
            param[8] = new SqlParameter("@SaveMode",argBillingDocType.SaveMode);
            param[9] = new SqlParameter("@ClientCode", argBillingDocType.ClientCode);
            param[10] = new SqlParameter("@CreatedBy", argBillingDocType.CreatedBy);
            param[11] = new SqlParameter("@ModifiedBy", argBillingDocType.ModifiedBy);

            param[12] = new SqlParameter("@Type", SqlDbType.Char);
            param[12].Size = 1;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[13].Size = 255;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[14].Size = 20;
            param[14].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateBillingDocType", param);


            string strMessage = Convert.ToString(param[13].Value);
            string strType = Convert.ToString(param[12].Value);
            string strRetValue = Convert.ToString(param[14].Value);


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
        
        public ICollection<ErrorHandler> DeleteBillingDocType(string argBillingDocTypeCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@BillingDocTypeCode", argBillingDocTypeCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted",iIsDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteBillingDocType", param);
                
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

        public bool blnIsBillingDocTypeExists(string argBillingDocTypeCode, string argClientCode)
        {
            bool IsBillingDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetBillingDocType(argBillingDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsBillingDocTypeExists = true;
            }
            else
            {
                IsBillingDocTypeExists = false;
            }
            return IsBillingDocTypeExists;
        }

        public bool blnIsBillingDocTypeExists(string argBillingDocTypeCode, string argClientCode, DataAccess da)
        {
            bool IsBillingDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetBillingDocType(argBillingDocTypeCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsBillingDocTypeExists = true;
            }
            else
            {
                IsBillingDocTypeExists = false;
            }
            return IsBillingDocTypeExists;
        }
    }
}