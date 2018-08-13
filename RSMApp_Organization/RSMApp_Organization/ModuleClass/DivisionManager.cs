
//Created On :: 07, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using Telerik.Web.UI;
using System.Data.OleDb;

namespace RSMApp_Organization
{
    public class DivisionManager
    {
        const string DivisionTable = "Division";
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public Division objGetDivision(string argDivisionCode, string argClientCode)
        {
            Division argDivision = new Division();
            DataSet DataSetToFill = new DataSet();

            if (argDivisionCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetDivision(argDivisionCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argDivision = this.objCreateDivision((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argDivision;
        }
        
        public ICollection<Division> colGetDivision(string argClientCode)
        {
            List<Division> lst = new List<Division>();
            DataSet DataSetToFill = new DataSet();
            Division tDivision = new Division();
            DataSetToFill = this.GetDivision(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDivision(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        private Division objCreateDivision(DataRow dr)
        {
            Division tDivision = new Division();
            tDivision.SetObjectInfo(dr);
            return tDivision;
        }

        public DataSet GetDivision(string argDivisionCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            
            DataSetToFill = da.FillDataSet("SP_GetDivision4ID", param);
            return DataSetToFill;
        }

        public DataSet GetDivision(string argDivisionCode, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetDivision4ID", param);
            return DataSetToFill;
        }

        public DataSet GetDivision4SalesOrg(string argSalesOrganizationCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetDivision4SalesOrg", param);
            return DataSetToFill;
        }

        public DataSet GetDivision(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + DivisionTable.ToString();

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
                    tSQL = tSQL + " " + sWhereClauseSTR + " ClientCode = '" + argClientCode+"'";
                }
                ds = da.FillDataSetWithSQL(tSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetDivision(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDivision", param);
            return DataSetToFill;
        }
        
        public DataSet GetDivision4Combo(string argPrefix, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetDivision4Combo", param);
            return DataSetToFill;
        }
        
        
        
        public ICollection<ErrorHandler> SaveDivision(Division argDivision)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsDivisionExists(argDivision.DivisionCode, argDivision.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertDivision(argDivision, da, lstErr);
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
                    UpdateDivision(argDivision, da, lstErr);
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

        /*************/
        public void SaveDivision(Division argDivision, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsDivisionExists(argDivision.DivisionCode, argDivision.ClientCode, da) == false)
                {
                    InsertDivision(argDivision, da, lstErr);
                }
                else
                {
                    UpdateDivision(argDivision, da, lstErr);
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
            Division ObjDivision = null;
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
                        ObjDivision = new Division();
                        ObjDivision.DivisionCode = Convert.ToString(drExcel["DivisionCode"]);
                        ObjDivision.DivisionName = Convert.ToString(drExcel["DivisionName"]);
                        ObjDivision.CreatedBy = Convert.ToString(argUserName);
                        ObjDivision.ModifiedBy = Convert.ToString(argUserName);
                        ObjDivision.ClientCode = Convert.ToString(argClientCode);
                        SaveDivision(ObjDivision, da, lstErr);

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

        /**********/
        public void InsertDivision(Division argDivision, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@DivisionCode", argDivision.DivisionCode);
            param[1] = new SqlParameter("@DivisionName", argDivision.DivisionName);
            param[2] = new SqlParameter("@ClientCode", argDivision.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argDivision.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argDivision.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertDivision", param);

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
        
        public void UpdateDivision(Division argDivision, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@DivisionCode", argDivision.DivisionCode);
            param[1] = new SqlParameter("@DivisionName", argDivision.DivisionName);
            param[2] = new SqlParameter("@ClientCode", argDivision.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argDivision.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argDivision.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateDivision", param);

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
        
        public ICollection<ErrorHandler> DeleteDivision(string argDivisionCode, int iIsDeleted, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@DivisionCode", argDivisionCode);
                param[1] = new SqlParameter("@IsDeleted", iIsDeleted);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                
                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteDivision", param);

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
        
        public bool blnIsDivisionExists(string argDivisionCode, string argClientCode)
        {
            bool IsDivisionExists = false;
            DataSet ds = new DataSet();
            ds = GetDivision(argDivisionCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDivisionExists = true;
            }
            else
            {
                IsDivisionExists = false;
            }
            return IsDivisionExists;
        }

        public bool blnIsDivisionExists(string argDivisionCode, string argClientCode, DataAccess da)
        {
            bool IsDivisionExists = false;
            DataSet ds = new DataSet();
            ds = GetDivision(argDivisionCode, argClientCode, da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDivisionExists = true;
            }
            else
            {
                IsDivisionExists = false;
            }
            return IsDivisionExists;
        }
    }
}