
//Created On :: 06, November, 2012
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
    public class RepairProcess_StatusMasterManager
    {
        const string RepairProcess_StatusMasterTable = "RepairProcess_StatusMaster";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public RepairProcess_StatusMaster objGetRepairProcess_StatusMaster(string argRepairProcessCode, string argRepairStatusCode, string argClientCode)
        {
            RepairProcess_StatusMaster argRepairProcess_StatusMaster = new RepairProcess_StatusMaster();
            DataSet DataSetToFill = new DataSet();

            if (argRepairProcessCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argRepairStatusCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetRepairProcess_StatusMaster(argRepairProcessCode, argRepairStatusCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argRepairProcess_StatusMaster = this.objCreateRepairProcess_StatusMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argRepairProcess_StatusMaster;
        }

        public ICollection<RepairProcess_StatusMaster> colGetRepairProcess_StatusMaster(string argRepairProcessCode, string argClientCode)
        {
            List<RepairProcess_StatusMaster> lst = new List<RepairProcess_StatusMaster>();
            DataSet DataSetToFill = new DataSet();
            RepairProcess_StatusMaster tRepairProcess_StatusMaster = new RepairProcess_StatusMaster();

            DataSetToFill = this.GetRepairProcess_StatusMaster(argRepairProcessCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateRepairProcess_StatusMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<RepairProcess_StatusMaster> colGetRepairProcess_StatusMaster(DataTable dt, string argUserName, string clientCode)
        {
            List<RepairProcess_StatusMaster> lst = new List<RepairProcess_StatusMaster>();
            RepairProcess_StatusMaster objRepairProcess_StatusMaster = null;
            foreach (DataRow dr in dt.Rows)
            {
                objRepairProcess_StatusMaster = new RepairProcess_StatusMaster();
                objRepairProcess_StatusMaster.RepairProcessCode = Convert.ToString(dr["RepairProcessCode"]).Trim();
                objRepairProcess_StatusMaster.RepairStatusCode = Convert.ToString(dr["RepairStatusCode"]).Trim();
                objRepairProcess_StatusMaster.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objRepairProcess_StatusMaster.ModifiedBy = Convert.ToString(argUserName).Trim();
                objRepairProcess_StatusMaster.CreatedBy = Convert.ToString(argUserName).Trim();
                objRepairProcess_StatusMaster.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objRepairProcess_StatusMaster);
            }
            return lst;
        }

        public DataSet GetRepairProcess_StatusMaster(string argRepairProcessCode, string argRepairStatusCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RepairProcessCode", argRepairProcessCode);
            param[1] = new SqlParameter("@RepairStatusCode", argRepairStatusCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepairProcess_StatusMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetRepairProcess_StatusMaster(string argRepairProcessCode, string argRepairStatusCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RepairProcessCode", argRepairProcessCode);
            param[1] = new SqlParameter("@RepairStatusCode", argRepairStatusCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetRepairProcess_StatusMaster4ID", param);
            return DataSetToFill;
        }

        public DataSet GetRepairProcess_StatusMaster(string argRepairProcessCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RepairProcessCode", argRepairProcessCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepairProcess_StatusMaster",param);
            return DataSetToFill;
        }

        private RepairProcess_StatusMaster objCreateRepairProcess_StatusMaster(DataRow dr)
        {
            RepairProcess_StatusMaster tRepairProcess_StatusMaster = new RepairProcess_StatusMaster();
            tRepairProcess_StatusMaster.SetObjectInfo(dr);
            return tRepairProcess_StatusMaster;
        }

        public ICollection<ErrorHandler> SaveRepairProcess_StatusMaster(RepairProcess_StatusMaster argRepairProcess_StatusMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsRepairProcess_StatusMasterExists(argRepairProcess_StatusMaster.RepairProcessCode, argRepairProcess_StatusMaster.RepairStatusCode, argRepairProcess_StatusMaster.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertRepairProcess_StatusMaster(argRepairProcess_StatusMaster, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
                    }
                }
                else
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    UpdateRepairProcess_StatusMaster(argRepairProcess_StatusMaster, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
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
            return lstErr;
        }

        public ICollection<ErrorHandler> SaveRepairProcess_StatusMaster(ICollection<RepairProcess_StatusMaster> colGetRepairProcess_StatusMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (RepairProcess_StatusMaster argRepairProcess_StatusMaster in colGetRepairProcess_StatusMaster)
                {

                    if (argRepairProcess_StatusMaster.IsDeleted == 0)
                    {

                        if (blnIsRepairProcess_StatusMasterExists(argRepairProcess_StatusMaster.RepairProcessCode, argRepairProcess_StatusMaster.RepairStatusCode, argRepairProcess_StatusMaster.ClientCode) == false)
                        {
                            InsertRepairProcess_StatusMaster(argRepairProcess_StatusMaster, da, lstErr);
                        }
                        else
                        {
                            UpdateRepairProcess_StatusMaster(argRepairProcess_StatusMaster, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteRepairProcess_StatusMaster(argRepairProcess_StatusMaster.RepairProcessCode, argRepairProcess_StatusMaster.RepairStatusCode, argRepairProcess_StatusMaster.ClientCode, argRepairProcess_StatusMaster.IsDeleted);

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

        public ICollection<ErrorHandler> SaveRepairProcess_StatusMaster(ICollection<RepairProcess_StatusMaster> colGetRepairProcess_StatusMaster, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (RepairProcess_StatusMaster argRepairProcess_StatusMaster in colGetRepairProcess_StatusMaster)
                {
                    if (argRepairProcess_StatusMaster.IsDeleted == 0)
                    {
                        if (blnIsRepairProcess_StatusMasterExists(argRepairProcess_StatusMaster.RepairProcessCode, argRepairProcess_StatusMaster.RepairStatusCode, argRepairProcess_StatusMaster.ClientCode, da) == false)
                        {
                            InsertRepairProcess_StatusMaster(argRepairProcess_StatusMaster, da, lstErr);
                        }
                        else
                        {
                            UpdateRepairProcess_StatusMaster(argRepairProcess_StatusMaster, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteRepairProcess_StatusMaster(argRepairProcess_StatusMaster.RepairProcessCode, argRepairProcess_StatusMaster.RepairStatusCode, argRepairProcess_StatusMaster.ClientCode, argRepairProcess_StatusMaster.IsDeleted);
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
                    SaveRepairProcess_StatusMaster(colGetRepairProcess_StatusMaster(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertRepairProcess_StatusMaster(RepairProcess_StatusMaster argRepairProcess_StatusMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@RepairProcessCode", argRepairProcess_StatusMaster.RepairProcessCode);
            param[1] = new SqlParameter("@RepairStatusCode", argRepairProcess_StatusMaster.RepairStatusCode);
            param[2] = new SqlParameter("@ClientCode", argRepairProcess_StatusMaster.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argRepairProcess_StatusMaster.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argRepairProcess_StatusMaster.ModifiedBy);
    
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertRepairProcess_StatusMaster", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public void UpdateRepairProcess_StatusMaster(RepairProcess_StatusMaster argRepairProcess_StatusMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@RepairProcessCode", argRepairProcess_StatusMaster.RepairProcessCode);
            param[1] = new SqlParameter("@RepairStatusCode", argRepairProcess_StatusMaster.RepairStatusCode);
            param[2] = new SqlParameter("@ClientCode", argRepairProcess_StatusMaster.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argRepairProcess_StatusMaster.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argRepairProcess_StatusMaster.ModifiedBy);

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateRepairProcess_StatusMaster", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public ICollection<ErrorHandler> DeleteRepairProcess_StatusMaster(string argRepairProcessCode, string argRepairStatusCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@RepairProcessCode", argRepairProcessCode);
                param[1] = new SqlParameter("@RepairStatusCode", argRepairStatusCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteRepairProcess_StatusMaster", param);


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

        public bool blnIsRepairProcess_StatusMasterExists(string argRepairProcessCode, string argRepairStatusCode, string argClientCode)
        {
            bool IsRepairProcess_StatusMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetRepairProcess_StatusMaster(argRepairProcessCode, argRepairStatusCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsRepairProcess_StatusMasterExists = true;
            }
            else
            {
                IsRepairProcess_StatusMasterExists = false;
            }
            return IsRepairProcess_StatusMasterExists;
        }

        public bool blnIsRepairProcess_StatusMasterExists(string argRepairProcessCode, string argRepairStatusCode, string argClientCode, DataAccess da)
        {
            bool IsRepairProcess_StatusMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetRepairProcess_StatusMaster(argRepairProcessCode, argRepairStatusCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsRepairProcess_StatusMasterExists = true;
            }
            else
            {
                IsRepairProcess_StatusMasterExists = false;
            }
            return IsRepairProcess_StatusMasterExists;
        }
    }
}