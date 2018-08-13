
//Created On :: 10, May, 2012
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
    public class CRContUpdateTypeManager
    {
        const string CRContUpdateTypeTable = "CRContUpdateType";
        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public CRContUpdateType objGetCRContUpdateType(string argCRUpdateCode, string argClientCode)
        {
            CRContUpdateType argCRContUpdateType = new CRContUpdateType();
            DataSet DataSetToFill = new DataSet();

            if (argCRUpdateCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetCRContUpdateType(argCRUpdateCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argCRContUpdateType = this.objCreateCRContUpdateType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argCRContUpdateType;
        }

        public ICollection<CRContUpdateType> colGetCRContUpdateType(string argClientCode)
        {
            List<CRContUpdateType> lst = new List<CRContUpdateType>();
            DataSet DataSetToFill = new DataSet();
            CRContUpdateType tCRContUpdateType = new CRContUpdateType();
            DataSetToFill = this.GetCRContUpdateType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCRContUpdateType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetCRContUpdateType(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + CRContUpdateTypeTable.ToString();

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

        public DataSet GetCRContUpdateType(string argCRUpdateCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CRUpdateCode", argCRUpdateCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCRContUpdateType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCRContUpdateType(string argCRUpdateCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CRUpdateCode", argCRUpdateCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCRContUpdateType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCRContUpdateType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCRContUpdateType", param);
            return DataSetToFill;
        }

        private CRContUpdateType objCreateCRContUpdateType(DataRow dr)
        {
            CRContUpdateType tCRContUpdateType = new CRContUpdateType();
            tCRContUpdateType.SetObjectInfo(dr);
            return tCRContUpdateType;
        }

        public ICollection<ErrorHandler> SaveCRContUpdateType(CRContUpdateType argCRContUpdateType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCRContUpdateTypeExists(argCRContUpdateType.CRUpdateCode, argCRContUpdateType.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCRContUpdateType(argCRContUpdateType, da, lstErr);
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
                    UpdateCRContUpdateType(argCRContUpdateType, da, lstErr);
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
        public void SaveCRContUpdateType(CRContUpdateType argCRContUpdateType, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCRContUpdateTypeExists(argCRContUpdateType.CRUpdateCode, argCRContUpdateType.ClientCode, da) == false)
                {
                    InsertCRContUpdateType(argCRContUpdateType, da, lstErr);
                }
                else
                {
                    UpdateCRContUpdateType(argCRContUpdateType, da, lstErr);
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
            CRContUpdateType ObjCRContUpdateType = null;
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
                        ObjCRContUpdateType = new CRContUpdateType();
                        ObjCRContUpdateType.CRUpdateCode = Convert.ToString(drExcel["CRUpdateCode"]).Trim();
                        ObjCRContUpdateType.UpdateType = Convert.ToString(drExcel["UpdateType"]).Trim();
                        ObjCRContUpdateType.UpdateDesc = Convert.ToString(drExcel["UpdateDesc"]).Trim();
                        ObjCRContUpdateType.OpenSalesOrder = Convert.ToInt32(drExcel["OpenSalesOrder"]);
                        ObjCRContUpdateType.OpenDelivery = Convert.ToInt32(drExcel["OpenDelivery"]);
                        ObjCRContUpdateType.OpenBilling = Convert.ToInt32(drExcel["OpenBilling"]);
                        ObjCRContUpdateType.CreatedBy = Convert.ToString(argUserName);
                        ObjCRContUpdateType.ModifiedBy = Convert.ToString(argUserName);
                        ObjCRContUpdateType.ClientCode = Convert.ToString(argClientCode);
                        SaveCRContUpdateType(ObjCRContUpdateType, da, lstErr);

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

        public void InsertCRContUpdateType(CRContUpdateType argCRContUpdateType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@CRUpdateCode", argCRContUpdateType.CRUpdateCode);
            param[1] = new SqlParameter("@UpdateType", argCRContUpdateType.UpdateType);
            param[2] = new SqlParameter("@UpdateDesc", argCRContUpdateType.UpdateDesc);
            param[3] = new SqlParameter("@OpenSalesOrder", argCRContUpdateType.OpenSalesOrder);
            param[4] = new SqlParameter("@OpenDelivery", argCRContUpdateType.OpenDelivery);
            param[5] = new SqlParameter("@OpenBilling", argCRContUpdateType.OpenBilling);
            param[6] = new SqlParameter("@ClientCode", argCRContUpdateType.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argCRContUpdateType.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argCRContUpdateType.ModifiedBy);            

            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertCRContUpdateType", param);

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
            lstErr.Add(objErrorHandler);
        }

        public void UpdateCRContUpdateType(CRContUpdateType argCRContUpdateType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@CRUpdateCode", argCRContUpdateType.CRUpdateCode);
            param[1] = new SqlParameter("@UpdateType", argCRContUpdateType.UpdateType);
            param[2] = new SqlParameter("@UpdateDesc", argCRContUpdateType.UpdateDesc);
            param[3] = new SqlParameter("@OpenSalesOrder", argCRContUpdateType.OpenSalesOrder);
            param[4] = new SqlParameter("@OpenDelivery", argCRContUpdateType.OpenDelivery);
            param[5] = new SqlParameter("@OpenBilling", argCRContUpdateType.OpenBilling);
            param[6] = new SqlParameter("@ClientCode", argCRContUpdateType.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argCRContUpdateType.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argCRContUpdateType.ModifiedBy);
            
            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;
            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;
            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateCRContUpdateType", param);

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
            lstErr.Add(objErrorHandler);
        }

        public ICollection<ErrorHandler> DeleteCRContUpdateType(string argCRUpdateCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CRUpdateCode", argCRUpdateCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCRContUpdateType", param);

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

        public bool blnIsCRContUpdateTypeExists(string argCRUpdateCode, string argClientCode)
        {
            bool IsCRContUpdateTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetCRContUpdateType(argCRUpdateCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCRContUpdateTypeExists = true;
            }
            else
            {
                IsCRContUpdateTypeExists = false;
            }
            return IsCRContUpdateTypeExists;
        }

        public bool blnIsCRContUpdateTypeExists(string argCRUpdateCode, string argClientCode, DataAccess da)
        {
            bool IsCRContUpdateTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetCRContUpdateType(argCRUpdateCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCRContUpdateTypeExists = true;
            }
            else
            {
                IsCRContUpdateTypeExists = false;
            }
            return IsCRContUpdateTypeExists;
        }
    }
}