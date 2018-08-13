
//Created On :: 15, October, 2012
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
    public class NumRangeMasterManager
    {
        const string NumRangeMasterTable = "NumRangeMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public NumRangeMaster objGetNumRangeMaster(string argNumRangeCode, string argClientCode)
        {
            NumRangeMaster argNumRangeMaster = new NumRangeMaster();
            DataSet DataSetToFill = new DataSet();

            if (argNumRangeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetNumRangeMaster(argNumRangeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argNumRangeMaster = this.objCreateNumRangeMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argNumRangeMaster;
        }

        public ICollection<NumRangeMaster> colGetNumRangeMaster(string argClientCode)
        {
            List<NumRangeMaster> lst = new List<NumRangeMaster>();
            DataSet DataSetToFill = new DataSet();
            NumRangeMaster tNumRangeMaster = new NumRangeMaster();

            DataSetToFill = this.GetNumRangeMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateNumRangeMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetNumRangeMaster(string argNumRangeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetNumRangeMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetNumRangeMaster(string argNumRangeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetNumRangeMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetNumRangeMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            
            DataSetToFill = da.FillDataSet("SP_GetNumRangeMaster",param);
            return DataSetToFill;
        }

        public DataSet GetNumRangeMaster(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + NumRangeMasterTable.ToString();

                if (iIsDeleted >= 0)
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

        private NumRangeMaster objCreateNumRangeMaster(DataRow dr)
        {
            NumRangeMaster tNumRangeMaster = new NumRangeMaster();

            tNumRangeMaster.SetObjectInfo(dr);

            return tNumRangeMaster;

        }

        public ICollection<ErrorHandler> SaveNumRangeMaster(NumRangeMaster argNumRangeMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsNumRangeMasterExists(argNumRangeMaster.NumRangeCode, argNumRangeMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertNumRangeMaster(argNumRangeMaster, da, lstErr);
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
                    UpdateNumRangeMaster(argNumRangeMaster, da, lstErr);
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

        public void SaveNumRangeMaster(NumRangeMaster argNumRangeMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsNumRangeMasterExists(argNumRangeMaster.NumRangeCode, argNumRangeMaster.ClientCode, da) == false)
                {
                    InsertNumRangeMaster(argNumRangeMaster, da, lstErr);
                }
                else
                {
                    UpdateNumRangeMaster(argNumRangeMaster, da, lstErr);
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
            NumRangeMaster ObjNumRangeMaster = null;
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
                        ObjNumRangeMaster = new NumRangeMaster();

                        ObjNumRangeMaster.NumRangeCode = Convert.ToString(drExcel["NumRangeCode"]).Trim();
                        ObjNumRangeMaster.Prefix = Convert.ToString(drExcel["Prefix"]).Trim();
                        ObjNumRangeMaster.FromRange = Convert.ToString(drExcel["FromRange"]).Trim();
                        ObjNumRangeMaster.ToRange = Convert.ToString(drExcel["ToRange"]).Trim();
                        ObjNumRangeMaster.CurrentNo = Convert.ToString(drExcel["CurrentNo"]).Trim();
                        ObjNumRangeMaster.DocType = Convert.ToString(drExcel["DocType"]).Trim();                        
                        ObjNumRangeMaster.CreatedBy = Convert.ToString(argUserName);
                        ObjNumRangeMaster.ModifiedBy = Convert.ToString(argUserName);
                        ObjNumRangeMaster.ClientCode = Convert.ToString(argClientCode);

                        SaveNumRangeMaster(ObjNumRangeMaster, da, lstErr);

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

        public void InsertNumRangeMaster(NumRangeMaster argNumRangeMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@NumRangeCode", argNumRangeMaster.NumRangeCode);
            param[1] = new SqlParameter("@Prefix", argNumRangeMaster.Prefix);
            param[2] = new SqlParameter("@FromRange", argNumRangeMaster.FromRange);
            param[3] = new SqlParameter("@ToRange", argNumRangeMaster.ToRange);
            param[4] = new SqlParameter("@CurrentNo", argNumRangeMaster.CurrentNo);
            param[5] = new SqlParameter("@DocType", argNumRangeMaster.DocType);
            param[6] = new SqlParameter("@ClientCode", argNumRangeMaster.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argNumRangeMaster.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argNumRangeMaster.ModifiedBy);
      

            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertNumRangeMaster", param);


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

        public void UpdateNumRangeMaster(NumRangeMaster argNumRangeMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@NumRangeCode", argNumRangeMaster.NumRangeCode);
            param[1] = new SqlParameter("@Prefix", argNumRangeMaster.Prefix);
            param[2] = new SqlParameter("@FromRange", argNumRangeMaster.FromRange);
            param[3] = new SqlParameter("@ToRange", argNumRangeMaster.ToRange);
            param[4] = new SqlParameter("@CurrentNo", argNumRangeMaster.CurrentNo);
            param[5] = new SqlParameter("@DocType", argNumRangeMaster.DocType);
            param[6] = new SqlParameter("@ClientCode", argNumRangeMaster.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argNumRangeMaster.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argNumRangeMaster.ModifiedBy);


            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateNumRangeMaster", param);


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

        public ICollection<ErrorHandler> DeleteNumRangeMaster(string argNumRangeCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@NumRangeCode", argNumRangeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteNumRangeMaster", param);


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

        public bool blnIsNumRangeMasterExists(string argNumRangeCode, string argClientCode)
        {
            bool IsNumRangeMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetNumRangeMaster(argNumRangeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsNumRangeMasterExists = true;
            }
            else
            {
                IsNumRangeMasterExists = false;
            }
            return IsNumRangeMasterExists;
        }

        public bool blnIsNumRangeMasterExists(string argNumRangeCode, string argClientCode, DataAccess da)
        {
            bool IsNumRangeMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetNumRangeMaster(argNumRangeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsNumRangeMasterExists = true;
            }
            else
            {
                IsNumRangeMasterExists = false;
            }
            return IsNumRangeMasterExists;
        }
    }
}