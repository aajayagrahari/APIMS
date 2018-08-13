
//Created On :: 08, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_Organization
{
    public class SalesAreaManager
    {
        const string SalesAreaTable = "SalesArea";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public SalesArea objGetSalesArea(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argClientCode)
        {
            SalesArea argSalesArea = new SalesArea();
            DataSet DataSetToFill = new DataSet();

            if (argSalesOrganizationCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argDistChannelCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argDivisionCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetSalesArea(argSalesOrganizationCode, argDistChannelCode, argDivisionCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argSalesArea = this.objCreateSalesArea((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argSalesArea;
        }
        
        public ICollection<SalesArea> colGetSalesArea(string argClientCode)
        {
            List<SalesArea> lst = new List<SalesArea>();
            DataSet DataSetToFill = new DataSet();
            SalesArea tSalesArea = new SalesArea();
            DataSetToFill = this.GetSalesArea(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSalesArea(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }
        
        public DataSet GetSalesArea(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[1] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesArea4ID", param);
            return DataSetToFill;
        }

        public DataSet GetSalesArea(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[1] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSalesArea4ID", param);
            return DataSetToFill;
        }

        public DataSet GetSalesArea(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + SalesAreaTable.ToString();

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

        public DataSet GetSalesArea(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesArea", param);
            return DataSetToFill;
        }
        
        private SalesArea objCreateSalesArea(DataRow dr)
        {
            SalesArea tSalesArea = new SalesArea();
            tSalesArea.SetObjectInfo(dr);
            return tSalesArea;
        }
        
        public ICollection<ErrorHandler> SaveSalesArea(SalesArea argSalesArea)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsSalesAreaExists(argSalesArea.SalesOrganizationCode, argSalesArea.DistChannelCode, argSalesArea.DivisionCode, argSalesArea.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertSalesArea(argSalesArea, da, lstErr);
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
                    UpdateSalesArea(argSalesArea, da, lstErr);
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

        #region Bulk Insert
        public void SaveSalesArea(SalesArea argSalesArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsSalesAreaExists(argSalesArea.SalesOrganizationCode,argSalesArea.DistChannelCode,argSalesArea.DivisionCode,argSalesArea.ClientCode, da) == false)
                {
                    InsertSalesArea(argSalesArea, da, lstErr);
                }
                else
                {
                    UpdateSalesArea(argSalesArea, da, lstErr);
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
            SalesArea ObjSalesArea = null;
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
                        ObjSalesArea = new SalesArea();
                        ObjSalesArea.SalesOrganizationCode = Convert.ToString(drExcel["SalesOrganizationCode"]).Trim();
                        ObjSalesArea.DistChannelCode = Convert.ToString(drExcel["DistChannelCode"]).Trim();
                        ObjSalesArea.DivisionCode = Convert.ToString(drExcel["DivisionCode"]).Trim();
                        ObjSalesArea.CreatedBy = Convert.ToString(argUserName);
                        ObjSalesArea.ModifiedBy = Convert.ToString(argUserName);
                        ObjSalesArea.ClientCode = Convert.ToString(argClientCode);
                        SaveSalesArea(ObjSalesArea, da, lstErr);

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

        public void InsertSalesArea(SalesArea argSalesArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesArea.SalesOrganizationCode);
            param[1] = new SqlParameter("@DistChannelCode", argSalesArea.DistChannelCode);
            param[2] = new SqlParameter("@DivisionCode", argSalesArea.DivisionCode);
            param[3] = new SqlParameter("@ClientCode", argSalesArea.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argSalesArea.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argSalesArea.ModifiedBy);
            
            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSalesArea", param);

            string strMessage = Convert.ToString(param[7].Value);
            string strType = Convert.ToString(param[6].Value);
            string strRetValue = Convert.ToString(param[8].Value);

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
        
        public void UpdateSalesArea(SalesArea argSalesArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesArea.SalesOrganizationCode);
            param[1] = new SqlParameter("@DistChannelCode", argSalesArea.DistChannelCode);
            param[2] = new SqlParameter("@DivisionCode", argSalesArea.DivisionCode);
            param[3] = new SqlParameter("@ClientCode", argSalesArea.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argSalesArea.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argSalesArea.ModifiedBy);
            
            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSalesArea", param);

            string strMessage = Convert.ToString(param[7].Value);
            string strType = Convert.ToString(param[6].Value);
            string strRetValue = Convert.ToString(param[8].Value);

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
        
        public ICollection<ErrorHandler> DeleteSalesArea(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
                param[1] = new SqlParameter("@DistChannelCode", argDistChannelCode);
                param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteSalesArea", param);

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
        
        public bool blnIsSalesAreaExists(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argClientCode)
        {
            bool IsSalesAreaExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesArea(argSalesOrganizationCode, argDistChannelCode, argDivisionCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesAreaExists = true;
            }
            else
            {
                IsSalesAreaExists = false;
            }
            return IsSalesAreaExists;
        }
        
        public bool blnIsSalesAreaExists(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argClientCode,DataAccess da)
        {
            bool IsSalesAreaExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesArea(argSalesOrganizationCode, argDistChannelCode, argDivisionCode, argClientCode,da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesAreaExists = true;
            }
            else
            {
                IsSalesAreaExists = false;
            }
            return IsSalesAreaExists;
        }
    }
}