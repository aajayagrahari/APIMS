
//Created On :: 26, September, 2012
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
    public class PricingConditionTypeManager
    {
        const string PricingConditionTypeTable = "PricingConditionType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public PricingConditionType objGetPricingConditionType(string argConditionTypeCode, string argClientCode)
        {
            PricingConditionType argPricingConditionType = new PricingConditionType();
            DataSet DataSetToFill = new DataSet();

            if (argConditionTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPricingConditionType(argConditionTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPricingConditionType = this.objCreatePricingConditionType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPricingConditionType;
        }
        
        public ICollection<PricingConditionType> colGetPricingConditionType(string argClientCode)
        {
            List<PricingConditionType> lst = new List<PricingConditionType>();
            DataSet DataSetToFill = new DataSet();
            PricingConditionType tPricingConditionType = new PricingConditionType();

            DataSetToFill = this.GetPricingConditionType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePricingConditionType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetPricingConditionType(string argConditionTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ConditionTypeCode", argConditionTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPricingConditionType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPricingConditionType(string argConditionTypeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ConditionTypeCode", argConditionTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPricingConditionType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPricingConditionType4SO(string argClientCode, string argSOTypeCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPricingConditionType4SO", param);

            return DataSetToFill;
        }

        public DataSet GetPricingConditionType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPricingConditionType", param);

            return DataSetToFill;
        }

        public DataSet GetPricingConditionType(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + PricingConditionTypeTable.ToString();

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
        
        private PricingConditionType objCreatePricingConditionType(DataRow dr)
        {
            PricingConditionType tPricingConditionType = new PricingConditionType();

            tPricingConditionType.SetObjectInfo(dr);

            return tPricingConditionType;

        }
        
        public ICollection<ErrorHandler> SavePricingConditionType(PricingConditionType argPricingConditionType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPricingConditionTypeExists(argPricingConditionType.ConditionTypeCode, argPricingConditionType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPricingConditionType(argPricingConditionType, da, lstErr);
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
                    UpdatePricingConditionType(argPricingConditionType, da, lstErr);
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

        public void SavePricingConditionType(PricingConditionType argPricingConditionType, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsPricingConditionTypeExists(argPricingConditionType.ConditionTypeCode, argPricingConditionType.ClientCode, da) == false)
                {
                    InsertPricingConditionType(argPricingConditionType, da, lstErr);
                }
                else
                {
                    UpdatePricingConditionType(argPricingConditionType, da, lstErr);
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
            PricingConditionType ObjPricingConditionType = null;
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
                        ObjPricingConditionType = new PricingConditionType();
                        ObjPricingConditionType.ConditionTypeCode = Convert.ToString(drExcel["ConditionTypeCode"]).Trim();
                        ObjPricingConditionType.ConditionTypeDesc = Convert.ToString(drExcel["ConditionTypeDesc"]).Trim();
                        ObjPricingConditionType.ConditionClass = Convert.ToString(drExcel["ConditionClass"]).Trim();
                        ObjPricingConditionType.CalculationTypeCode = Convert.ToString(drExcel["CalculationTypeCode"]).Trim();
                        ObjPricingConditionType.ManualEntryAllowed = Convert.ToInt32(drExcel["ManualEntryAllowed"]);
                        ObjPricingConditionType.CreatedBy = Convert.ToString(argUserName);
                        ObjPricingConditionType.ModifiedBy = Convert.ToString(argUserName);
                        ObjPricingConditionType.ClientCode = Convert.ToString(argClientCode);
                        SavePricingConditionType(ObjPricingConditionType, da, lstErr);

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

        public void InsertPricingConditionType(PricingConditionType argPricingConditionType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@ConditionTypeCode", argPricingConditionType.ConditionTypeCode);
            param[1] = new SqlParameter("@ConditionTypeDesc", argPricingConditionType.ConditionTypeDesc);
            param[2] = new SqlParameter("@ConditionClass", argPricingConditionType.ConditionClass);
            param[3] = new SqlParameter("@CalculationTypeCode", argPricingConditionType.CalculationTypeCode);
            param[4] = new SqlParameter("@ManualEntryAllowed", argPricingConditionType.ManualEntryAllowed);
            param[5] = new SqlParameter("@ClientCode", argPricingConditionType.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argPricingConditionType.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argPricingConditionType.ModifiedBy);
            
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPricingConditionType", param);


            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
            string strRetValue = Convert.ToString(param[10].Value);
            
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
        
        public void UpdatePricingConditionType(PricingConditionType argPricingConditionType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@ConditionTypeCode", argPricingConditionType.ConditionTypeCode);
            param[1] = new SqlParameter("@ConditionTypeDesc", argPricingConditionType.ConditionTypeDesc);
            param[2] = new SqlParameter("@ConditionClass", argPricingConditionType.ConditionClass);
            param[3] = new SqlParameter("@CalculationTypeCode", argPricingConditionType.CalculationTypeCode);
            param[4] = new SqlParameter("@ManualEntryAllowed", argPricingConditionType.ManualEntryAllowed);
            param[5] = new SqlParameter("@ClientCode", argPricingConditionType.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argPricingConditionType.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argPricingConditionType.ModifiedBy);
            
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePricingConditionType", param);


            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
            string strRetValue = Convert.ToString(param[10].Value);


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
        
        public ICollection<ErrorHandler> DeletePricingConditionType(string argConditionTypeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@ConditionTypeCode", argConditionTypeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeletePricingConditionType", param);


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
        
        public bool blnIsPricingConditionTypeExists(string argConditionTypeCode, string argClientCode)
        {
            bool IsPricingConditionTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetPricingConditionType(argConditionTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPricingConditionTypeExists = true;
            }
            else
            {
                IsPricingConditionTypeExists = false;
            }
            return IsPricingConditionTypeExists;
        }

        public bool blnIsPricingConditionTypeExists(string argConditionTypeCode, string argClientCode, DataAccess da)
        {
            bool IsPricingConditionTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetPricingConditionType(argConditionTypeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPricingConditionTypeExists = true;
            }
            else
            {
                IsPricingConditionTypeExists = false;
            }
            return IsPricingConditionTypeExists;
        }
    }
}