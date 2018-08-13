
//Created On :: 11, October, 2012
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
    public class DepartmentMasterManager
    {
        const string DepartmentMasterTable = "DepartmentMaster";
        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public DepartmentMaster objGetDepartmentMaster(string argDepartmentCode, string argClientCode)
        {
            DepartmentMaster argDepartmentMaster = new DepartmentMaster();
            DataSet DataSetToFill = new DataSet();

            if (argDepartmentCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetDepartmentMaster(argDepartmentCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argDepartmentMaster = this.objCreateDepartmentMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argDepartmentMaster;
        }

        public ICollection<DepartmentMaster> colGetDepartmentMaster(string argClientCode)
        {
            List<DepartmentMaster> lst = new List<DepartmentMaster>();
            DataSet DataSetToFill = new DataSet();
            DepartmentMaster tDepartmentMaster = new DepartmentMaster();
            DataSetToFill = this.GetDepartmentMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDepartmentMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetDepartmentMaster(string argDepartmentCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DepartmentCode", argDepartmentCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDepartmentMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetDepartmentMaster(string argDepartmentCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DepartmentCode", argDepartmentCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetDepartmentMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetDepartmentMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDepartmentMaster",param);
            return DataSetToFill;
        }

        public DataSet GetDepartmentMaster(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";
            try
            {
                tSQL = "SELECT * FROM " + DepartmentMasterTable.ToString();

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

        private DepartmentMaster objCreateDepartmentMaster(DataRow dr)
        {
            DepartmentMaster tDepartmentMaster = new DepartmentMaster();
            tDepartmentMaster.SetObjectInfo(dr);
            return tDepartmentMaster;
        }

        public ICollection<ErrorHandler> SaveDepartmentMaster(DepartmentMaster argDepartmentMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsDepartmentMasterExists(argDepartmentMaster.DepartmentCode, argDepartmentMaster.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertDepartmentMaster(argDepartmentMaster, da, lstErr);
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
                    UpdateDepartmentMaster(argDepartmentMaster, da, lstErr);
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

        public void SaveDepartmentMaster(DepartmentMaster argDepartmentMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsDepartmentMasterExists(argDepartmentMaster.DepartmentCode, argDepartmentMaster.ClientCode, da) == false)
                {
                    InsertDepartmentMaster(argDepartmentMaster, da, lstErr);
                }
                else
                {
                    UpdateDepartmentMaster(argDepartmentMaster, da, lstErr);
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
            DepartmentMaster ObjDepartmentMaster = null;
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
                        ObjDepartmentMaster = new DepartmentMaster();

                        ObjDepartmentMaster.DepartmentCode = Convert.ToString(drExcel["DepartmentCode"]).Trim();
                        ObjDepartmentMaster.Department = Convert.ToString(drExcel["Department"]).Trim();
                        ObjDepartmentMaster.CreatedBy = Convert.ToString(argUserName);
                        ObjDepartmentMaster.ModifiedBy = Convert.ToString(argUserName);
                        ObjDepartmentMaster.ClientCode = Convert.ToString(argClientCode);

                        SaveDepartmentMaster(ObjDepartmentMaster, da, lstErr);

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

        public void InsertDepartmentMaster(DepartmentMaster argDepartmentMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@DepartmentCode", argDepartmentMaster.DepartmentCode);
            param[1] = new SqlParameter("@Department", argDepartmentMaster.Department);
            param[2] = new SqlParameter("@ClientCode", argDepartmentMaster.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argDepartmentMaster.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argDepartmentMaster.ModifiedBy);

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertDepartmentMaster", param);

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

        public void UpdateDepartmentMaster(DepartmentMaster argDepartmentMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@DepartmentCode", argDepartmentMaster.DepartmentCode);
            param[1] = new SqlParameter("@Department", argDepartmentMaster.Department);
            param[2] = new SqlParameter("@ClientCode", argDepartmentMaster.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argDepartmentMaster.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argDepartmentMaster.ModifiedBy);

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateDepartmentMaster", param);

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

        public ICollection<ErrorHandler> DeleteDepartmentMaster(string argDepartmentCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@DepartmentCode", argDepartmentCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteDepartmentMaster", param);

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

        public bool blnIsDepartmentMasterExists(string argDepartmentCode, string argClientCode)
        {
            bool IsDepartmentMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetDepartmentMaster(argDepartmentCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDepartmentMasterExists = true;
            }
            else
            {
                IsDepartmentMasterExists = false;
            }
            return IsDepartmentMasterExists;
        }

        public bool blnIsDepartmentMasterExists(string argDepartmentCode, string argClientCode, DataAccess da)
        {
            bool IsDepartmentMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetDepartmentMaster(argDepartmentCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDepartmentMasterExists = true;
            }
            else
            {
                IsDepartmentMasterExists = false;
            }
            return IsDepartmentMasterExists;
        }
    }
}