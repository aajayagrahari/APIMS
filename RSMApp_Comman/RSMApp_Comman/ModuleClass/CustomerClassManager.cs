
//Created On :: 03, October, 2012
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
    public class CustomerClassManager
    {
        const string CustomerClassTable = "CustomerClass";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public CustomerClass objGetCustomerClass(string argCustomerClassCode,string argClientCode)
        {
            CustomerClass argCustomerClass = new CustomerClass();
            DataSet DataSetToFill = new DataSet();

            if (argCustomerClassCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCustomerClass(argCustomerClassCode,argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCustomerClass = this.objCreateCustomerClass((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCustomerClass;
        }

        public ICollection<CustomerClass> colGetCustomerClass(string argClientCode)
        {
            List<CustomerClass> lst = new List<CustomerClass>();
            DataSet DataSetToFill = new DataSet();
            CustomerClass tCustomerClass = new CustomerClass();

            DataSetToFill = this.GetCustomerClass(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCustomerClass(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetCustomerClass(string argCustomerClassCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@CustomerClassCode", argCustomerClassCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCustomerClass4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCustomerClass(string argCustomerClassCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@CustomerClassCode", argCustomerClassCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCustomerClass4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCustomerClass(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCustomerClass",param);
            return DataSetToFill;
        }

        public DataSet GetCustomerClass(int iIsDeleted,string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + CustomerClassTable.ToString();

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

        private CustomerClass objCreateCustomerClass(DataRow dr)
        {
            CustomerClass tCustomerClass = new CustomerClass();

            tCustomerClass.SetObjectInfo(dr);

            return tCustomerClass;

        }

        public ICollection<ErrorHandler> SaveCustomerClass(CustomerClass argCustomerClass)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCustomerClassExists(argCustomerClass.CustomerClassCode,argCustomerClass.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCustomerClass(argCustomerClass, da, lstErr);
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
                    UpdateCustomerClass(argCustomerClass, da, lstErr);
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

        public void SaveCustomerClass(CustomerClass argCustomerClass, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCustomerClassExists(argCustomerClass.CustomerClassCode, argCustomerClass.ClientCode, da) == false)
                {
                    InsertCustomerClass(argCustomerClass, da, lstErr);
                }
                else
                {
                    UpdateCustomerClass(argCustomerClass, da, lstErr);
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
            CustomerClass ObjCustomerClass = null;
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
                        ObjCustomerClass = new CustomerClass();

                        ObjCustomerClass.CustomerClassCode = Convert.ToString(drExcel["CustomerClassCode"]).Trim();
                        ObjCustomerClass.CustomerClassDesc = Convert.ToString(drExcel["CustomerClassDesc"]).Trim();
                        ObjCustomerClass.ClientCode = Convert.ToString(argClientCode);

                        SaveCustomerClass(ObjCustomerClass, da, lstErr);

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

        public void InsertCustomerClass(CustomerClass argCustomerClass, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@CustomerClassCode", argCustomerClass.CustomerClassCode);
            param[1] = new SqlParameter("@CustomerClassDesc", argCustomerClass.CustomerClassDesc);
            param[2] = new SqlParameter("@ClientCode", argCustomerClass.ClientCode);
        
            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;

            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertCustomerClass", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public void UpdateCustomerClass(CustomerClass argCustomerClass, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@CustomerClassCode", argCustomerClass.CustomerClassCode);
            param[1] = new SqlParameter("@CustomerClassDesc", argCustomerClass.CustomerClassDesc);
            param[2] = new SqlParameter("@ClientCode", argCustomerClass.ClientCode);

            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;

            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCustomerClass", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public ICollection<ErrorHandler> DeleteCustomerClass(string argCustomerClassCode,string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CustomerClassCode", argCustomerClassCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteCustomerClass", param);


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

        public bool blnIsCustomerClassExists(string argCustomerClassCode,string argClientCode)
        {
            bool IsCustomerClassExists = false;
            DataSet ds = new DataSet();
            ds = GetCustomerClass(argCustomerClassCode,argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCustomerClassExists = true;
            }
            else
            {
                IsCustomerClassExists = false;
            }
            return IsCustomerClassExists;
        }

        public bool blnIsCustomerClassExists(string argCustomerClassCode, string argClientCode, DataAccess da)
        {
            bool IsCustomerClassExists = false;
            DataSet ds = new DataSet();
            ds = GetCustomerClass(argCustomerClassCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCustomerClassExists = true;
            }
            else
            {
                IsCustomerClassExists = false;
            }
            return IsCustomerClassExists;
        }
    }
}