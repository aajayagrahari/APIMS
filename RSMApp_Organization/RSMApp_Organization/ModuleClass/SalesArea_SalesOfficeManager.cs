
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
    public class SalesArea_SalesOfficeManager
    {
        const string SalesArea_SalesOfficeTable = "SalesArea_SalesOffice";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public SalesArea_SalesOffice objGetSalesArea_SalesOffice(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argSalesofficeCode, string argClientCode)
        {
            SalesArea_SalesOffice argSalesArea_SalesOffice = new SalesArea_SalesOffice();
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

            if (argSalesofficeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetSalesArea_SalesOffice(argSalesOrganizationCode, argDistChannelCode, argDivisionCode, argSalesofficeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSalesArea_SalesOffice = this.objCreateSalesArea_SalesOffice((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSalesArea_SalesOffice;
        }

        public ICollection<SalesArea_SalesOffice> colGetSalesArea_SalesOffice(DataTable dt, string argUserName, string clientCode)
        {
            List<SalesArea_SalesOffice> lst = new List<SalesArea_SalesOffice>();
            SalesArea_SalesOffice objSalesArea_SalesOffice = null;
            foreach (DataRow dr in dt.Rows)
            {
                objSalesArea_SalesOffice = new SalesArea_SalesOffice();
                objSalesArea_SalesOffice.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]).Trim();
                objSalesArea_SalesOffice.DistChannelCode = Convert.ToString(dr["DistChannelCode"]).Trim();
                objSalesArea_SalesOffice.DivisionCode = Convert.ToString(dr["DivisionCode"]).Trim();
                objSalesArea_SalesOffice.SalesofficeCode = Convert.ToString(dr["SalesOfficeCode"]).Trim();
                objSalesArea_SalesOffice.CreatedBy = Convert.ToString(argUserName);
                objSalesArea_SalesOffice.ModifiedBy = Convert.ToString(argUserName);
                objSalesArea_SalesOffice.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objSalesArea_SalesOffice);
            }
            return lst;
        }
        
        public DataSet GetSalesArea_SalesOffice(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argSalesofficeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[1] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[3] = new SqlParameter("@SalesofficeCode", argSalesofficeCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesArea_SalesOffice4ID", param);

            return DataSetToFill;
        }

        public DataSet GetSalesArea_SalesOffice(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argSalesofficeCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[1] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[3] = new SqlParameter("@SalesofficeCode", argSalesofficeCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSalesArea_SalesOffice4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetSalesArea_SalesOffice(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesArea_SalesOffice", param);
            return DataSetToFill;
        }
        
        private SalesArea_SalesOffice objCreateSalesArea_SalesOffice(DataRow dr)
        {
            SalesArea_SalesOffice tSalesArea_SalesOffice = new SalesArea_SalesOffice();

            tSalesArea_SalesOffice.SetObjectInfo(dr);

            return tSalesArea_SalesOffice;

        }
        
        public ICollection<ErrorHandler> SaveSalesArea_SalesOffice(SalesArea_SalesOffice argSalesArea_SalesOffice)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            SalesAreaManager ObjSalesAreaManager = new SalesAreaManager();
            try
            {
                if (ObjSalesAreaManager.blnIsSalesAreaExists(argSalesArea_SalesOffice.SalesOrganizationCode, argSalesArea_SalesOffice.DistChannelCode, argSalesArea_SalesOffice.DivisionCode, argSalesArea_SalesOffice.ClientCode) == true)
                {
                    if (blnIsSalesArea_SalesOfficeExists(argSalesArea_SalesOffice.SalesOrganizationCode, argSalesArea_SalesOffice.DistChannelCode, argSalesArea_SalesOffice.DivisionCode, argSalesArea_SalesOffice.SalesofficeCode, argSalesArea_SalesOffice.ClientCode) == false)
                    {

                        da.Open_Connection();
                        da.BEGIN_TRANSACTION();
                        InsertSalesArea_SalesOffice(argSalesArea_SalesOffice, da, lstErr);
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
                        UpdateSalesArea_SalesOffice(argSalesArea_SalesOffice, da, lstErr);
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
                else
                {
                    objErrorHandler.Type = "E";
                    objErrorHandler.MsgId = 0;
                    objErrorHandler.Module = ErrorConstant.strInsertModule;
                    objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                    objErrorHandler.Message = "Sales Area does not exists.";
                    objErrorHandler.RowNo = 0;
                    objErrorHandler.FieldName = "";
                    objErrorHandler.LogCode = "";
                    lstErr.Add(objErrorHandler);
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
        public ICollection<ErrorHandler> SaveSalesArea_SalesOffice(ICollection<SalesArea_SalesOffice> colGetSalesArea_SalesOffice, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (SalesArea_SalesOffice argSalesArea_SalesOffice in colGetSalesArea_SalesOffice)
                {
                    if (argSalesArea_SalesOffice.IsDeleted == 0)
                    {
                        if (blnIsSalesArea_SalesOfficeExists(argSalesArea_SalesOffice.SalesOrganizationCode, argSalesArea_SalesOffice.DistChannelCode, argSalesArea_SalesOffice.DivisionCode, argSalesArea_SalesOffice.SalesofficeCode, argSalesArea_SalesOffice.ClientCode, da) == false)
                        {
                            InsertSalesArea_SalesOffice(argSalesArea_SalesOffice, da, lstErr);
                        }
                        else
                        {
                            UpdateSalesArea_SalesOffice(argSalesArea_SalesOffice, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteSalesArea_SalesOffice(argSalesArea_SalesOffice.SalesOrganizationCode, argSalesArea_SalesOffice.DistChannelCode, argSalesArea_SalesOffice.DivisionCode, argSalesArea_SalesOffice.SalesofficeCode, argSalesArea_SalesOffice.ClientCode, argSalesArea_SalesOffice.IsDeleted);
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
                    SaveSalesArea_SalesOffice(colGetSalesArea_SalesOffice(dtExcel, argUserName, argClientCode), lstErr);

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
        
        public void InsertSalesArea_SalesOffice(SalesArea_SalesOffice argSalesArea_SalesOffice, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesArea_SalesOffice.SalesOrganizationCode);
            param[1] = new SqlParameter("@DistChannelCode", argSalesArea_SalesOffice.DistChannelCode);
            param[2] = new SqlParameter("@DivisionCode", argSalesArea_SalesOffice.DivisionCode);
            param[3] = new SqlParameter("@SalesofficeCode", argSalesArea_SalesOffice.SalesofficeCode);
            param[4] = new SqlParameter("@ClientCode", argSalesArea_SalesOffice.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argSalesArea_SalesOffice.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argSalesArea_SalesOffice.ModifiedBy);
            
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSalesArea_SalesOffice", param);


            string strMessage = Convert.ToString(param[8].Value);
            string strType = Convert.ToString(param[7].Value);
            string strRetValue = Convert.ToString(param[9].Value);


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
        
        public void UpdateSalesArea_SalesOffice(SalesArea_SalesOffice argSalesArea_SalesOffice, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesArea_SalesOffice.SalesOrganizationCode);
            param[1] = new SqlParameter("@DistChannelCode", argSalesArea_SalesOffice.DistChannelCode);
            param[2] = new SqlParameter("@DivisionCode", argSalesArea_SalesOffice.DivisionCode);
            param[3] = new SqlParameter("@SalesofficeCode", argSalesArea_SalesOffice.SalesofficeCode);
            param[4] = new SqlParameter("@ClientCode", argSalesArea_SalesOffice.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argSalesArea_SalesOffice.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argSalesArea_SalesOffice.ModifiedBy);
            
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateSalesArea_SalesOffice", param);


            string strMessage = Convert.ToString(param[8].Value);
            string strType = Convert.ToString(param[7].Value);
            string strRetValue = Convert.ToString(param[9].Value);


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
        
        public ICollection<ErrorHandler> DeleteSalesArea_SalesOffice(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argSalesofficeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
                param[1] = new SqlParameter("@DistChannelCode", argDistChannelCode);
                param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
                param[3] = new SqlParameter("@SalesofficeCode", argSalesofficeCode);
                param[4] = new SqlParameter("@ClientCode", argClientCode);
                param[5] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[6] = new SqlParameter("@Type", SqlDbType.Char);
                param[6].Size = 1;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[7].Size = 255;
                param[7].Direction = ParameterDirection.Output;

                param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[8].Size = 20;
                param[8].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteSalesArea_SalesOffice", param);


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
        
        public bool blnIsSalesArea_SalesOfficeExists(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argSalesofficeCode, string argClientCode)
        {
            bool IsSalesArea_SalesOfficeExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesArea_SalesOffice(argSalesOrganizationCode, argDistChannelCode, argDivisionCode, argSalesofficeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesArea_SalesOfficeExists = true;
            }
            else
            {
                IsSalesArea_SalesOfficeExists = false;
            }
            return IsSalesArea_SalesOfficeExists;
        }
        
        public bool blnIsSalesArea_SalesOfficeExists(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argSalesofficeCode, string argClientCode,DataAccess da)
        {
            bool IsSalesArea_SalesOfficeExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesArea_SalesOffice(argSalesOrganizationCode, argDistChannelCode, argDivisionCode, argSalesofficeCode, argClientCode,da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesArea_SalesOfficeExists = true;
            }
            else
            {
                IsSalesArea_SalesOfficeExists = false;
            }
            return IsSalesArea_SalesOfficeExists;
        }
    }
}