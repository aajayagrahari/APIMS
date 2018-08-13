
//Created On :: 07, May, 2012
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
    public class ControllingAreaManager
    {
        const string ControllingAreaTable = "ControllingArea";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public ControllingArea objGetControllingArea(string argCOACode, string argClientCode)
        {
            ControllingArea argControllingArea = new ControllingArea();
            DataSet DataSetToFill = new DataSet();

            if (argCOACode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetControllingArea(argCOACode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argControllingArea = this.objCreateControllingArea((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argControllingArea;
        }
        
        public ICollection<ControllingArea> colGetControllingArea(string argClientCode)
        {
            List<ControllingArea> lst = new List<ControllingArea>();
            DataSet DataSetToFill = new DataSet();
            ControllingArea tControllingArea = new ControllingArea();

            DataSetToFill = this.GetControllingArea(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateControllingArea(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetControllingArea(string argCOACode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@COACode", argCOACode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);            

            DataSetToFill = da.FillDataSet("SP_GetControllingArea4ID", param);

            return DataSetToFill;
        }

        public DataSet GetControllingArea(string argCOACode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@COACode", argCOACode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetControllingArea4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetControllingArea(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);            
            DataSetToFill = da.FillDataSet("SP_GetControllingArea", param);
            return DataSetToFill;
        }
        
        private ControllingArea objCreateControllingArea(DataRow dr)
        {
            ControllingArea tControllingArea = new ControllingArea();

            tControllingArea.SetObjectInfo(dr);

            return tControllingArea;

        }
        
        public ICollection<ErrorHandler> SaveControllingArea(ControllingArea argControllingArea)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsControllingAreaExists(argControllingArea.COACode, argControllingArea.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertControllingArea(argControllingArea, da, lstErr);
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
                    UpdateControllingArea(argControllingArea, da, lstErr);
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

        public void SaveControllingArea(ControllingArea argControllingArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsControllingAreaExists(argControllingArea.COACode, argControllingArea.ClientCode, da) == false)
                {
                    InsertControllingArea(argControllingArea, da, lstErr);
                }
                else
                {
                    UpdateControllingArea(argControllingArea, da, lstErr);
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
            ControllingArea ObjControllingArea = null;
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
                        ObjControllingArea = new ControllingArea();

                        ObjControllingArea.COACode = Convert.ToString(drExcel["COACode"]).Trim();
                        ObjControllingArea.COAName = Convert.ToString(drExcel["COAName"]).Trim();
                        ObjControllingArea.ChartACCode = Convert.ToString(drExcel["ChartACCode"]).Trim();
                        ObjControllingArea.FiscalYearType = Convert.ToString(drExcel["FiscalYearType"]).Trim();
                        ObjControllingArea.CurrencyCode = Convert.ToString(drExcel["CurrencyCode"]).Trim();
                        ObjControllingArea.ClientCode = Convert.ToString(argClientCode);
                        ObjControllingArea.CreatedBy = Convert.ToString(argUserName);
                        ObjControllingArea.ModifiedBy = Convert.ToString(argUserName);

                        SaveControllingArea(ObjControllingArea, da, lstErr);

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
        
        public void InsertControllingArea(ControllingArea argControllingArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@COACode", argControllingArea.COACode);
            param[1] = new SqlParameter("@COAName", argControllingArea.COAName);
            param[2] = new SqlParameter("@ChartACCode", argControllingArea.ChartACCode);
            param[3] = new SqlParameter("@FiscalYearType", argControllingArea.FiscalYearType);
            param[4] = new SqlParameter("@CurrencyCode", argControllingArea.CurrencyCode);
            param[5] = new SqlParameter("@ClientCode", argControllingArea.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argControllingArea.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argControllingArea.ModifiedBy);
            
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertControllingArea", param);


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
        
        public void UpdateControllingArea(ControllingArea argControllingArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@COACode", argControllingArea.COACode);
            param[1] = new SqlParameter("@COAName", argControllingArea.COAName);
            param[2] = new SqlParameter("@ChartACCode", argControllingArea.ChartACCode);
            param[3] = new SqlParameter("@FiscalYearType", argControllingArea.FiscalYearType);
            param[4] = new SqlParameter("@CurrencyCode", argControllingArea.CurrencyCode);
            param[5] = new SqlParameter("@ClientCode", argControllingArea.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argControllingArea.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argControllingArea.ModifiedBy);
            

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateControllingArea", param);


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
        
        public ICollection<ErrorHandler> DeleteControllingArea(string argCOACode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@COACode", argCOACode);
                param[1] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[2] = new SqlParameter("@Type", SqlDbType.Char);
                param[2].Size = 1;
                param[2].Direction = ParameterDirection.Output;

                param[3] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[3].Size = 255;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[4].Size = 20;
                param[4].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteControllingArea", param);


                string strMessage = Convert.ToString(param[3].Value);
                string strType = Convert.ToString(param[2].Value);
                string strRetValue = Convert.ToString(param[4].Value);


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
        
        public bool blnIsControllingAreaExists(string argCOACode, string argClientCode)
        {
            bool IsControllingAreaExists = false;
            DataSet ds = new DataSet();
            ds = GetControllingArea(argCOACode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsControllingAreaExists = true;
            }
            else
            {
                IsControllingAreaExists = false;
            }
            return IsControllingAreaExists;
        }

        public bool blnIsControllingAreaExists(string argCOACode, string argClientCode, DataAccess da)
        {
            bool IsControllingAreaExists = false;
            DataSet ds = new DataSet();
            ds = GetControllingArea(argCOACode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsControllingAreaExists = true;
            }
            else
            {
                IsControllingAreaExists = false;
            }
            return IsControllingAreaExists;
        }
    }
}