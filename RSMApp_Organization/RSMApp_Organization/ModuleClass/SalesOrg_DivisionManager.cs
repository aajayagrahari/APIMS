
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
    public class SalesOrg_DivisionManager
    {
        const string SalesOrg_DivisionTable = "SalesOrg_Division";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public SalesOrg_Division objGetSalesOrg_Division(string argDivisionCode, string argSalesOrganizationCode, string argClientCode)
        {
            SalesOrg_Division argSalesOrg_Division = new SalesOrg_Division();
            DataSet DataSetToFill = new DataSet();

            if (argDivisionCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argSalesOrganizationCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetSalesOrg_Division(argDivisionCode, argSalesOrganizationCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSalesOrg_Division = this.objCreateSalesOrg_Division((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSalesOrg_Division;
        }

        public ICollection<SalesOrg_Division> colGetSalesOrg_Division(DataTable dt, string argUserName, string clientCode)
        {
            List<SalesOrg_Division> lst = new List<SalesOrg_Division>();
            SalesOrg_Division objSalesOrg_Division = null;
            foreach (DataRow dr in dt.Rows)
            {
                objSalesOrg_Division = new SalesOrg_Division();
                objSalesOrg_Division.DivisionCode = Convert.ToString(dr["DivisionCode"]).Trim();
                objSalesOrg_Division.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]).Trim();
                objSalesOrg_Division.CreatedBy = Convert.ToString(argUserName);
                objSalesOrg_Division.ModifiedBy = Convert.ToString(argUserName);
                objSalesOrg_Division.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objSalesOrg_Division);
            }
            return lst;
        }
        
        public DataSet GetSalesOrg_Division(string argDivisionCode, string argSalesOrganizationCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrg_Division4ID", param);

            return DataSetToFill;
        }

        public DataSet GetSalesOrg_Division(string argDivisionCode, string argSalesOrganizationCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSalesOrg_Division4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetSalesOrg_Division(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetSalesOrg_Division", param);
            return DataSetToFill;
        }
        
        private SalesOrg_Division objCreateSalesOrg_Division(DataRow dr)
        {
            SalesOrg_Division tSalesOrg_Division = new SalesOrg_Division();

            tSalesOrg_Division.SetObjectInfo(dr);

            return tSalesOrg_Division;

        }
        
        public ICollection<ErrorHandler> SaveSalesOrg_Division(SalesOrg_Division argSalesOrg_Division)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsSalesOrg_DivisionExists(argSalesOrg_Division.DivisionCode, argSalesOrg_Division.SalesOrganizationCode, argSalesOrg_Division.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertSalesOrg_Division(argSalesOrg_Division, da, lstErr);
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
                    UpdateSalesOrg_Division(argSalesOrg_Division, da, lstErr);
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
        public ICollection<ErrorHandler> SaveSalesOrg_Division(ICollection<SalesOrg_Division> colGetSalesOrg_Division, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (SalesOrg_Division argSalesOrg_Division in colGetSalesOrg_Division)
                {
                    if (argSalesOrg_Division.IsDeleted == 0)
                    {
                        if (blnIsSalesOrg_DivisionExists(argSalesOrg_Division.DivisionCode, argSalesOrg_Division.SalesOrganizationCode, argSalesOrg_Division.ClientCode, da) == false)
                        {
                            InsertSalesOrg_Division(argSalesOrg_Division, da, lstErr);
                        }
                        else
                        {
                            UpdateSalesOrg_Division(argSalesOrg_Division, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteSalesOrg_Division(argSalesOrg_Division.DivisionCode, argSalesOrg_Division.SalesOrganizationCode, argSalesOrg_Division.ClientCode, argSalesOrg_Division.IsDeleted);
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
                    SaveSalesOrg_Division(colGetSalesOrg_Division(dtExcel, argUserName, argClientCode), lstErr);

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
                
        public void InsertSalesOrg_Division(SalesOrg_Division argSalesOrg_Division, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@DivisionCode", argSalesOrg_Division.DivisionCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrg_Division.SalesOrganizationCode);
            param[2] = new SqlParameter("@ClientCode", argSalesOrg_Division.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argSalesOrg_Division.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argSalesOrg_Division.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSalesOrg_Division", param);


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
        
        public void UpdateSalesOrg_Division(SalesOrg_Division argSalesOrg_Division, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@DivisionCode", argSalesOrg_Division.DivisionCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrg_Division.SalesOrganizationCode);
            param[2] = new SqlParameter("@ClientCode", argSalesOrg_Division.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argSalesOrg_Division.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argSalesOrg_Division.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateSalesOrg_Division", param);


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
        
        public ICollection<ErrorHandler> DeleteSalesOrg_Division(string argDivisionCode, string argSalesOrganizationCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@DivisionCode", argDivisionCode);
                param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteSalesOrg_Division", param);


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
        
        public bool blnIsSalesOrg_DivisionExists(string argDivisionCode, string argSalesOrganizationCode, string argClientCode)
        {
            bool IsSalesOrg_DivisionExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesOrg_Division(argDivisionCode, argSalesOrganizationCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesOrg_DivisionExists = true;
            }
            else
            {
                IsSalesOrg_DivisionExists = false;
            }
            return IsSalesOrg_DivisionExists;
        }

        public bool blnIsSalesOrg_DivisionExists(string argDivisionCode, string argSalesOrganizationCode, string argClientCode,DataAccess da)
        {
            bool IsSalesOrg_DivisionExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesOrg_Division(argDivisionCode, argSalesOrganizationCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesOrg_DivisionExists = true;
            }
            else
            {
                IsSalesOrg_DivisionExists = false;
            }
            return IsSalesOrg_DivisionExists;
        }
    }
}