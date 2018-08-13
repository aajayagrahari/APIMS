
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
    public class EmployeeMasterManager
    {
        const string EmployeeMasterTable = "EmployeeMaster";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public EmployeeMaster objGetEmployeeMaster(string argEmployeeCode, string argClientCode)
        {
            EmployeeMaster argEmployeeMaster = new EmployeeMaster();
            DataSet DataSetToFill = new DataSet();

            if (argEmployeeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetEmployeeMaster(argEmployeeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argEmployeeMaster = this.objCreateEmployeeMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argEmployeeMaster;
        }

        public ICollection<EmployeeMaster> colGetEmployeeMaster(string argClientCode)
        {
            List<EmployeeMaster> lst = new List<EmployeeMaster>();
            DataSet DataSetToFill = new DataSet();
            EmployeeMaster tEmployeeMaster = new EmployeeMaster();

            DataSetToFill = this.GetEmployeeMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateEmployeeMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetEmployeeMaster(string argEmployeeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@EmployeeCode", argEmployeeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetEmployeeMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetEmployeeMaster(string argEmployeeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@EmployeeCode", argEmployeeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetEmployeeMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetEmployeeMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            
            DataSetToFill = da.FillDataSet("SP_GetEmployeeMaster",param);
            return DataSetToFill;
        }

        public DataSet GetEmployeeMaster(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + EmployeeMasterTable.ToString();

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

        private EmployeeMaster objCreateEmployeeMaster(DataRow dr)
        {
            EmployeeMaster tEmployeeMaster = new EmployeeMaster();

            tEmployeeMaster.SetObjectInfo(dr);

            return tEmployeeMaster;

        }

        public ICollection<ErrorHandler> SaveEmployeeMaster(EmployeeMaster argEmployeeMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsEmployeeMasterExists(argEmployeeMaster.EmployeeCode, argEmployeeMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertEmployeeMaster(argEmployeeMaster, da, lstErr);
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
                    UpdateEmployeeMaster(argEmployeeMaster, da, lstErr);
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

        public void SaveEmployeeMaster(EmployeeMaster argEmployeeMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsEmployeeMasterExists(argEmployeeMaster.EmployeeCode, argEmployeeMaster.ClientCode, da) == false)
                {
                    InsertEmployeeMaster(argEmployeeMaster, da, lstErr);
                }
                else
                {
                    UpdateEmployeeMaster(argEmployeeMaster, da, lstErr);
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
            EmployeeMaster ObjEmployeeMaster = null;
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
                        ObjEmployeeMaster = new EmployeeMaster();

                        ObjEmployeeMaster.EmployeeCode = Convert.ToString(drExcel["EmployeeCode"]).Trim();
                        ObjEmployeeMaster.EmployeeName = Convert.ToString(drExcel["EmployeeName"]).Trim();
                        ObjEmployeeMaster.Address1 = Convert.ToString(drExcel["Address1"]).Trim();
                        ObjEmployeeMaster.Address2 = Convert.ToString(drExcel["Address2"]).Trim();
                        ObjEmployeeMaster.CountryCode = Convert.ToString(drExcel["CountryCode"]).Trim();
                        ObjEmployeeMaster.StateCode = Convert.ToString(drExcel["StateCode"]).Trim();
                        ObjEmployeeMaster.City = Convert.ToString(drExcel["City"]).Trim();
                        ObjEmployeeMaster.PinCode = Convert.ToString(drExcel["PinCode"]).Trim();
                        ObjEmployeeMaster.TelNo = Convert.ToString(drExcel["TelNo"]).Trim();
                        ObjEmployeeMaster.EmailID = Convert.ToString(drExcel["EmailID"]).Trim();
                        ObjEmployeeMaster.MobileNo = Convert.ToString(drExcel["MobileNo"]).Trim();
                        ObjEmployeeMaster.DepartmentCode = Convert.ToString(drExcel["DepartmentCode"]).Trim();
                        ObjEmployeeMaster.DesignationCode = Convert.ToString(drExcel["DesignationCode"]).Trim();
                        ObjEmployeeMaster.ClientCode = Convert.ToString(argClientCode);
                        ObjEmployeeMaster.CreatedBy = Convert.ToString(argUserName);
                        ObjEmployeeMaster.ModifiedBy = Convert.ToString(argUserName);

                        SaveEmployeeMaster(ObjEmployeeMaster, da, lstErr);

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

        public void InsertEmployeeMaster(EmployeeMaster argEmployeeMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@EmployeeCode", argEmployeeMaster.EmployeeCode);
            param[1] = new SqlParameter("@EmployeeName", argEmployeeMaster.EmployeeName);
            param[2] = new SqlParameter("@Address1", argEmployeeMaster.Address1);
            param[3] = new SqlParameter("@Address2", argEmployeeMaster.Address2);
            param[4] = new SqlParameter("@CountryCode", argEmployeeMaster.CountryCode);
            param[5] = new SqlParameter("@StateCode", argEmployeeMaster.StateCode);
            param[6] = new SqlParameter("@City", argEmployeeMaster.City);
            param[7] = new SqlParameter("@PinCode", argEmployeeMaster.PinCode);
            param[8] = new SqlParameter("@TelNo", argEmployeeMaster.TelNo);
            param[9] = new SqlParameter("@EmailID", argEmployeeMaster.EmailID);
            param[10] = new SqlParameter("@MobileNo", argEmployeeMaster.MobileNo);
            param[11] = new SqlParameter("@DepartmentCode", argEmployeeMaster.DepartmentCode);
            param[12] = new SqlParameter("@DesignationCode", argEmployeeMaster.DesignationCode);
            param[13] = new SqlParameter("@ClientCode", argEmployeeMaster.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argEmployeeMaster.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argEmployeeMaster.ModifiedBy);
    

            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertEmployeeMaster", param);


            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);


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

        public void UpdateEmployeeMaster(EmployeeMaster argEmployeeMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@EmployeeCode", argEmployeeMaster.EmployeeCode);
            param[1] = new SqlParameter("@EmployeeName", argEmployeeMaster.EmployeeName);
            param[2] = new SqlParameter("@Address1", argEmployeeMaster.Address1);
            param[3] = new SqlParameter("@Address2", argEmployeeMaster.Address2);
            param[4] = new SqlParameter("@CountryCode", argEmployeeMaster.CountryCode);
            param[5] = new SqlParameter("@StateCode", argEmployeeMaster.StateCode);
            param[6] = new SqlParameter("@City", argEmployeeMaster.City);
            param[7] = new SqlParameter("@PinCode", argEmployeeMaster.PinCode);
            param[8] = new SqlParameter("@TelNo", argEmployeeMaster.TelNo);
            param[9] = new SqlParameter("@EmailID", argEmployeeMaster.EmailID);
            param[10] = new SqlParameter("@MobileNo", argEmployeeMaster.MobileNo);
            param[11] = new SqlParameter("@DepartmentCode", argEmployeeMaster.DepartmentCode);
            param[12] = new SqlParameter("@DesignationCode", argEmployeeMaster.DesignationCode);
            param[13] = new SqlParameter("@ClientCode", argEmployeeMaster.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argEmployeeMaster.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argEmployeeMaster.ModifiedBy);


            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateEmployeeMaster", param);


            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);


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

        public ICollection<ErrorHandler> DeleteEmployeeMaster(string argEmployeeCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@EmployeeCode", argEmployeeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteEmployeeMaster", param);


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

        public bool blnIsEmployeeMasterExists(string argEmployeeCode, string argClientCode)
        {
            bool IsEmployeeMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetEmployeeMaster(argEmployeeCode, argClientCode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsEmployeeMasterExists = true;
            }
            else
            {
                IsEmployeeMasterExists = false;
            }
            return IsEmployeeMasterExists;
        }

        public bool blnIsEmployeeMasterExists(string argEmployeeCode, string argClientCode, DataAccess da)
        {
            bool IsEmployeeMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetEmployeeMaster(argEmployeeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsEmployeeMasterExists = true;
            }
            else
            {
                IsEmployeeMasterExists = false;
            }
            return IsEmployeeMasterExists;
        }
    }
}