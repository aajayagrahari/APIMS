
//Created On :: 11, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_Partner
{
    public class PartnerEmployeeMasterManager
    {
        const string PartnerEmployeeMasterTable = "PartnerEmployeeMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public PartnerEmployeeMaster objGetPartnerEmployeeMaster(string argPartnerEmployeeCode, string argPartnerCode, string argClientCode)
        {
            PartnerEmployeeMaster argPartnerEmployeeMaster = new PartnerEmployeeMaster();
            DataSet DataSetToFill = new DataSet();

            if (argPartnerEmployeeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argPartnerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPartnerEmployeeMaster(argPartnerEmployeeCode, argPartnerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartnerEmployeeMaster = this.objCreatePartnerEmployeeMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPartnerEmployeeMaster;
        }

        public ICollection<PartnerEmployeeMaster> colGetPartnerEmployeeMaster(string argClientCode)
        {
            List<PartnerEmployeeMaster> lst = new List<PartnerEmployeeMaster>();
            DataSet DataSetToFill = new DataSet();
            PartnerEmployeeMaster tPartnerEmployeeMaster = new PartnerEmployeeMaster();

            DataSetToFill = this.GetPartnerEmployeeMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePartnerEmployeeMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetPartnerEmployeeMaster(string argPartnerEmployeeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerEmployeeMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerEmployeeMaster(string argPartnerEmployeeCode, string argPartnerCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPartnerEmployeeMaster4ID", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerEmployee4Combo(string argPartnerCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            param[2] = new SqlParameter("@IsDeleted", iIsDeleted);

            DataSetToFill = da.FillDataSet("SP_GetPartnerEmployee4Combo", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerEmployeeMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerEmployeeMaster",param);
            return DataSetToFill;
        }

        public DataSet GetPartnerEmployee4Desg(string argDesignationCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@DesignationCode", argDesignationCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPtnEmployee4Desg", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerEmployeeMaster(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + PartnerEmployeeMasterTable.ToString();

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

        private PartnerEmployeeMaster objCreatePartnerEmployeeMaster(DataRow dr)
        {
            PartnerEmployeeMaster tPartnerEmployeeMaster = new PartnerEmployeeMaster();
            tPartnerEmployeeMaster.SetObjectInfo(dr);
            return tPartnerEmployeeMaster;
        }

        public ICollection<ErrorHandler> SavePartnerEmployeeMaster(PartnerEmployeeMaster argPartnerEmployeeMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartnerEmployeeMasterExists(argPartnerEmployeeMaster.PartnerEmployeeCode, argPartnerEmployeeMaster.PartnerCode, argPartnerEmployeeMaster.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartnerEmployeeMaster(argPartnerEmployeeMaster, da, lstErr);
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
                    UpdatePartnerEmployeeMaster(argPartnerEmployeeMaster, da, lstErr);
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

        public void SavePartnerEmployeeMaster(PartnerEmployeeMaster argPartnerEmployeeMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsPartnerEmployeeMasterExists(argPartnerEmployeeMaster.PartnerEmployeeCode, argPartnerEmployeeMaster.PartnerCode, argPartnerEmployeeMaster.ClientCode, da) == false)
                {
                    InsertPartnerEmployeeMaster(argPartnerEmployeeMaster, da, lstErr);
                }
                else
                {
                    UpdatePartnerEmployeeMaster(argPartnerEmployeeMaster, da, lstErr);
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
            PartnerEmployeeMaster ObjPartnerEmployeeMaster = null;
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
                        ObjPartnerEmployeeMaster = new PartnerEmployeeMaster();
                        ObjPartnerEmployeeMaster.PartnerEmployeeCode = Convert.ToString(drExcel["PartnerEmployeeCode"]).Trim();
                        ObjPartnerEmployeeMaster.EmployeeName = Convert.ToString(drExcel["EmployeeName"]).Trim();
                        ObjPartnerEmployeeMaster.Address1 = Convert.ToString(drExcel["Address1"]).Trim();
                        ObjPartnerEmployeeMaster.Address2 = Convert.ToString(drExcel["Address2"]).Trim();
                        ObjPartnerEmployeeMaster.StateCode = Convert.ToString(drExcel["StateCode"]).Trim();
                        ObjPartnerEmployeeMaster.City = Convert.ToString(drExcel["City"]).Trim();
                        ObjPartnerEmployeeMaster.PinCode = Convert.ToString(drExcel["PinCode"]).Trim();
                        ObjPartnerEmployeeMaster.TelNo = Convert.ToString(drExcel["TelNo"]).Trim();
                        ObjPartnerEmployeeMaster.EmailID = Convert.ToString(drExcel["EmailID"]).Trim();
                        ObjPartnerEmployeeMaster.MobileNo = Convert.ToString(drExcel["MobileNo"]).Trim();
                        ObjPartnerEmployeeMaster.DepartmentCode = Convert.ToString(drExcel["DepartmentCode"]).Trim();
                        ObjPartnerEmployeeMaster.DesignationCode = Convert.ToString(drExcel["DesignationCode"]).Trim();
                        ObjPartnerEmployeeMaster.PartnerCode = Convert.ToString(drExcel["PartnerCode"]).Trim();
                        ObjPartnerEmployeeMaster.CountryCode = Convert.ToString(drExcel["CountryCode"]).Trim();
                        ObjPartnerEmployeeMaster.CreatedBy = Convert.ToString(argUserName);
                        ObjPartnerEmployeeMaster.ModifiedBy = Convert.ToString(argUserName);
                        ObjPartnerEmployeeMaster.ClientCode = Convert.ToString(argClientCode);
                        SavePartnerEmployeeMaster(ObjPartnerEmployeeMaster, da, lstErr);

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

        public void InsertPartnerEmployeeMaster(PartnerEmployeeMaster argPartnerEmployeeMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeMaster.PartnerEmployeeCode);
            param[1] = new SqlParameter("@EmployeeName", argPartnerEmployeeMaster.EmployeeName);
            param[2] = new SqlParameter("@Address1", argPartnerEmployeeMaster.Address1);
            param[3] = new SqlParameter("@Address2", argPartnerEmployeeMaster.Address2);
            param[4] = new SqlParameter("@StateCode", argPartnerEmployeeMaster.StateCode);
            param[5] = new SqlParameter("@City", argPartnerEmployeeMaster.City);
            param[6] = new SqlParameter("@PinCode", argPartnerEmployeeMaster.PinCode);
            param[7] = new SqlParameter("@TelNo", argPartnerEmployeeMaster.TelNo);
            param[8] = new SqlParameter("@EmailID", argPartnerEmployeeMaster.EmailID);
            param[9] = new SqlParameter("@MobileNo", argPartnerEmployeeMaster.MobileNo);
            param[10] = new SqlParameter("@DepartmentCode", argPartnerEmployeeMaster.DepartmentCode);
            param[11] = new SqlParameter("@DesignationCode", argPartnerEmployeeMaster.DesignationCode);
            param[12] = new SqlParameter("@PartnerCode", argPartnerEmployeeMaster.PartnerCode);
            param[13] = new SqlParameter("@CountryCode", argPartnerEmployeeMaster.CountryCode);
            param[14] = new SqlParameter("@ClientCode", argPartnerEmployeeMaster.ClientCode);
            param[15] = new SqlParameter("@CreatedBy", argPartnerEmployeeMaster.CreatedBy);
            param[16] = new SqlParameter("@ModifiedBy", argPartnerEmployeeMaster.ModifiedBy);
    
            param[17] = new SqlParameter("@Type", SqlDbType.Char);
            param[17].Size = 1;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[18].Size = 255;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[19].Size = 20;
            param[19].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPartnerEmployeeMaster", param);

            string strMessage = Convert.ToString(param[18].Value);
            string strType = Convert.ToString(param[17].Value);
            string strRetValue = Convert.ToString(param[19].Value);

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

        public void UpdatePartnerEmployeeMaster(PartnerEmployeeMaster argPartnerEmployeeMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeMaster.PartnerEmployeeCode);
            param[1] = new SqlParameter("@EmployeeName", argPartnerEmployeeMaster.EmployeeName);
            param[2] = new SqlParameter("@Address1", argPartnerEmployeeMaster.Address1);
            param[3] = new SqlParameter("@Address2", argPartnerEmployeeMaster.Address2);
            param[4] = new SqlParameter("@StateCode", argPartnerEmployeeMaster.StateCode);
            param[5] = new SqlParameter("@City", argPartnerEmployeeMaster.City);
            param[6] = new SqlParameter("@PinCode", argPartnerEmployeeMaster.PinCode);
            param[7] = new SqlParameter("@TelNo", argPartnerEmployeeMaster.TelNo);
            param[8] = new SqlParameter("@EmailID", argPartnerEmployeeMaster.EmailID);
            param[9] = new SqlParameter("@MobileNo", argPartnerEmployeeMaster.MobileNo);
            param[10] = new SqlParameter("@DepartmentCode", argPartnerEmployeeMaster.DepartmentCode);
            param[11] = new SqlParameter("@DesignationCode", argPartnerEmployeeMaster.DesignationCode);
            param[12] = new SqlParameter("@PartnerCode", argPartnerEmployeeMaster.PartnerCode);
            param[13] = new SqlParameter("@CountryCode", argPartnerEmployeeMaster.CountryCode);
            param[14] = new SqlParameter("@ClientCode", argPartnerEmployeeMaster.ClientCode);
            param[15] = new SqlParameter("@CreatedBy", argPartnerEmployeeMaster.CreatedBy);
            param[16] = new SqlParameter("@ModifiedBy", argPartnerEmployeeMaster.ModifiedBy);

            param[17] = new SqlParameter("@Type", SqlDbType.Char);
            param[17].Size = 1;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[18].Size = 255;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[19].Size = 20;
            param[19].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdatePartnerEmployeeMaster", param);

            string strMessage = Convert.ToString(param[18].Value);
            string strType = Convert.ToString(param[17].Value);
            string strRetValue = Convert.ToString(param[19].Value);

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

        public ICollection<ErrorHandler> DeletePartnerEmployeeMaster(string argPartnerEmployeeCode, string argPartnerCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeCode);
                param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
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

                int i = da.ExecuteNonQuery("Proc_DeletePartnerEmployeeMaster", param);

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

        public bool blnIsPartnerEmployeeMasterExists(string argPartnerEmployeeCode, string argPartnerCode, string argClientCode)
        {
            bool IsPartnerEmployeeMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerEmployeeMaster(argPartnerEmployeeCode, argPartnerCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerEmployeeMasterExists = true;
            }
            else
            {
                IsPartnerEmployeeMasterExists = false;
            }
            return IsPartnerEmployeeMasterExists;
        }

        public bool blnIsPartnerEmployeeMasterExists(string argPartnerEmployeeCode, string argPartnerCode, string argClientCode, DataAccess da)
        {
            bool IsPartnerEmployeeMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerEmployeeMaster(argPartnerEmployeeCode, argPartnerCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerEmployeeMasterExists = true;
            }
            else
            {
                IsPartnerEmployeeMasterExists = false;
            }
            return IsPartnerEmployeeMasterExists;
        }
    }
}