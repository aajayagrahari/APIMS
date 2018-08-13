
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
    public class SalesOffice_SalesGroupManager
    {
        const string SalesOffice_SalesGroupTable = "SalesOffice_SalesGroup";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public SalesOffice_SalesGroup objGetSalesOffice_SalesGroup(string argSalesGroupCode, string argSalesofficeCode, string argClientCode)
        {
            SalesOffice_SalesGroup argSalesOffice_SalesGroup = new SalesOffice_SalesGroup();
            DataSet DataSetToFill = new DataSet();

            if (argSalesGroupCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argSalesofficeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetSalesOffice_SalesGroup(argSalesGroupCode, argSalesofficeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSalesOffice_SalesGroup = this.objCreateSalesOffice_SalesGroup((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSalesOffice_SalesGroup;
        }

        public ICollection<SalesOffice_SalesGroup> colGetSalesOffice_SalesGroup(DataTable dt, string argUserName, string clientCode)
        {
            List<SalesOffice_SalesGroup> lst = new List<SalesOffice_SalesGroup>();
            SalesOffice_SalesGroup objSalesOffice_SalesGroup = null;
            foreach (DataRow dr in dt.Rows)
            {
                objSalesOffice_SalesGroup = new SalesOffice_SalesGroup();
                objSalesOffice_SalesGroup.SalesGroupCode = Convert.ToString(dr["SalesGroupCode"]).Trim();
                objSalesOffice_SalesGroup.SalesofficeCode = Convert.ToString(dr["SalesofficeCode"]).Trim();
                objSalesOffice_SalesGroup.CreatedBy = Convert.ToString(argUserName);
                objSalesOffice_SalesGroup.ModifiedBy = Convert.ToString(argUserName);
                objSalesOffice_SalesGroup.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objSalesOffice_SalesGroup);
            }
            return lst;
        }
        
        public DataSet GetSalesOffice_SalesGroup(string argSalesGroupCode, string argSalesofficeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SalesGroupCode", argSalesGroupCode);
            param[1] = new SqlParameter("@SalesofficeCode", argSalesofficeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOffice_SalesGroup4ID", param);

            return DataSetToFill;
        }

        public DataSet GetSalesOffice_SalesGroup(string argSalesGroupCode, string argSalesofficeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SalesGroupCode", argSalesGroupCode);
            param[1] = new SqlParameter("@SalesofficeCode", argSalesofficeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSalesOffice_SalesGroup4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetSalesOffice_SalesGroup(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOffice_SalesGroup", param);
            return DataSetToFill;
        }
        
        private SalesOffice_SalesGroup objCreateSalesOffice_SalesGroup(DataRow dr)
        {
            SalesOffice_SalesGroup tSalesOffice_SalesGroup = new SalesOffice_SalesGroup();

            tSalesOffice_SalesGroup.SetObjectInfo(dr);

            return tSalesOffice_SalesGroup;

        }
        
        public ICollection<ErrorHandler> SaveSalesOffice_SalesGroup(SalesOffice_SalesGroup argSalesOffice_SalesGroup)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsSalesOffice_SalesGroupExists(argSalesOffice_SalesGroup.SalesGroupCode, argSalesOffice_SalesGroup.SalesofficeCode, argSalesOffice_SalesGroup.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertSalesOffice_SalesGroup(argSalesOffice_SalesGroup, da, lstErr);
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
                    UpdateSalesOffice_SalesGroup(argSalesOffice_SalesGroup, da, lstErr);
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
        public ICollection<ErrorHandler> SaveSalesOffice_SalesGroup(ICollection<SalesOffice_SalesGroup> colGetSalesOffice_SalesGroup, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (SalesOffice_SalesGroup argSalesOffice_SalesGroup in colGetSalesOffice_SalesGroup)
                {
                    if (argSalesOffice_SalesGroup.IsDeleted == 0)
                    {
                        if (blnIsSalesOffice_SalesGroupExists(argSalesOffice_SalesGroup.SalesGroupCode, argSalesOffice_SalesGroup.SalesofficeCode, argSalesOffice_SalesGroup.ClientCode, da) == false)
                        {
                            InsertSalesOffice_SalesGroup(argSalesOffice_SalesGroup, da, lstErr);
                        }
                        else
                        {
                            UpdateSalesOffice_SalesGroup(argSalesOffice_SalesGroup, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteSalesOffice_SalesGroup(argSalesOffice_SalesGroup.SalesGroupCode, argSalesOffice_SalesGroup.SalesofficeCode, argSalesOffice_SalesGroup.ClientCode, argSalesOffice_SalesGroup.IsDeleted);
                    }
                }

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

        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
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
                    SaveSalesOffice_SalesGroup(colGetSalesOffice_SalesGroup(dtExcel, argUserName, argClientCode), lstErr);

                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            break;

                        }
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
        
        public void InsertSalesOffice_SalesGroup(SalesOffice_SalesGroup argSalesOffice_SalesGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@SalesGroupCode", argSalesOffice_SalesGroup.SalesGroupCode);
            param[1] = new SqlParameter("@SalesofficeCode", argSalesOffice_SalesGroup.SalesofficeCode);
            param[2] = new SqlParameter("@ClientCode", argSalesOffice_SalesGroup.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argSalesOffice_SalesGroup.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argSalesOffice_SalesGroup.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSalesOffice_SalesGroup", param);


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
        
        public void UpdateSalesOffice_SalesGroup(SalesOffice_SalesGroup argSalesOffice_SalesGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@SalesGroupCode", argSalesOffice_SalesGroup.SalesGroupCode);
            param[1] = new SqlParameter("@SalesofficeCode", argSalesOffice_SalesGroup.SalesofficeCode);
            param[2] = new SqlParameter("@ClientCode", argSalesOffice_SalesGroup.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argSalesOffice_SalesGroup.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argSalesOffice_SalesGroup.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateSalesOffice_SalesGroup", param);


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
        
        public ICollection<ErrorHandler> DeleteSalesOffice_SalesGroup(string argSalesGroupCode, string argSalesofficeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@SalesGroupCode", argSalesGroupCode);
                param[1] = new SqlParameter("@SalesofficeCode", argSalesofficeCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteSalesOffice_SalesGroup", param);


                string strMessage = Convert.ToString(param[5].Value);
                string strType = Convert.ToString(param[4].Value);
                string strRetValue = Convert.ToString(param[6].Value);


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
        
        public bool blnIsSalesOffice_SalesGroupExists(string argSalesGroupCode, string argSalesofficeCode, string argClientCode)
        {
            bool IsSalesOffice_SalesGroupExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesOffice_SalesGroup(argSalesGroupCode, argSalesofficeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesOffice_SalesGroupExists = true;
            }
            else
            {
                IsSalesOffice_SalesGroupExists = false;
            }
            return IsSalesOffice_SalesGroupExists;
        }

        public bool blnIsSalesOffice_SalesGroupExists(string argSalesGroupCode, string argSalesofficeCode, string argClientCode, DataAccess da)
        {
            bool IsSalesOffice_SalesGroupExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesOffice_SalesGroup(argSalesGroupCode, argSalesofficeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesOffice_SalesGroupExists = true;
            }
            else
            {
                IsSalesOffice_SalesGroupExists = false;
            }
            return IsSalesOffice_SalesGroupExists;
        }
    }
}