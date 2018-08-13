
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
    public class RepairProcessMasterManager
    {
        const string RepairProcessMasterTable = "RepairProcessMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public RepairProcessMaster objGetRepairProcessMaster(string argRepairProcessCode, string argClientCode)
        {
            RepairProcessMaster argRepairProcessMaster = new RepairProcessMaster();
            DataSet DataSetToFill = new DataSet();

            if (argRepairProcessCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetRepairProcessMaster(argRepairProcessCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argRepairProcessMaster = this.objCreateRepairProcessMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argRepairProcessMaster;
        }

        public ICollection<RepairProcessMaster> colGetRepairProcessMaster(string argClientCode)
        {
            List<RepairProcessMaster> lst = new List<RepairProcessMaster>();
            DataSet DataSetToFill = new DataSet();
            RepairProcessMaster tRepairProcessMaster = new RepairProcessMaster();

            DataSetToFill = this.GetRepairProcessMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateRepairProcessMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetRepairProcessMaster(string argRepairProcessCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RepairProcessCode", argRepairProcessCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepairProcessMaster4ID", param);
            return DataSetToFill;
        }

        public DataSet GetRepairProcessMaster(string argRepairProcessCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RepairProcessCode", argRepairProcessCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetRepairProcessMaster4ID", param);
            return DataSetToFill;
        }

        public DataSet GetRepairProcessMaster(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + RepairProcessMasterTable.ToString();

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

        public DataSet GetRepairProcessMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepairProcessMaster",param);
            return DataSetToFill;
        }

        private RepairProcessMaster objCreateRepairProcessMaster(DataRow dr)
        {
            RepairProcessMaster tRepairProcessMaster = new RepairProcessMaster();
            tRepairProcessMaster.SetObjectInfo(dr);
            return tRepairProcessMaster;
        }

        public ICollection<ErrorHandler> SaveRepairProcessMaster(RepairProcessMaster argRepairProcessMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsRepairProcessMasterExists(argRepairProcessMaster.RepairProcessCode, argRepairProcessMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertRepairProcessMaster(argRepairProcessMaster, da, lstErr);
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
                    UpdateRepairProcessMaster(argRepairProcessMaster, da, lstErr);
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

        public void SaveRepairProcessMaster(RepairProcessMaster argRepairProcessMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsRepairProcessMasterExists(argRepairProcessMaster.RepairProcessCode, argRepairProcessMaster.ClientCode, da) == false)
                {
                    InsertRepairProcessMaster(argRepairProcessMaster, da, lstErr);
                }
                else
                {
                    UpdateRepairProcessMaster(argRepairProcessMaster, da, lstErr);
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
            RepairProcessMaster ObjRepairProcessMaster = null;
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
                        ObjRepairProcessMaster = new RepairProcessMaster();
                        ObjRepairProcessMaster.RepairProcessCode = Convert.ToString(drExcel["RepairProcessCode"]).Trim();
                        ObjRepairProcessMaster.RepairProcessName = Convert.ToString(drExcel["RepairProcessName"]).Trim();
                        ObjRepairProcessMaster.IsAssignToTechnician = Convert.ToInt32(drExcel["IsAssignToTechnician"]);
                        ObjRepairProcessMaster.IsCallClosed = Convert.ToInt32(drExcel["IsCallClosed"]);
                        ObjRepairProcessMaster.IsRepairCompleted = Convert.ToInt32(drExcel["IsRepairCompleted"]);
                        ObjRepairProcessMaster.IsApproved = Convert.ToInt32(drExcel["IsApproved"]);
                        ObjRepairProcessMaster.CreatedBy = Convert.ToString(argUserName);
                        ObjRepairProcessMaster.ModifiedBy = Convert.ToString(argUserName);
                        ObjRepairProcessMaster.ClientCode = Convert.ToString(argClientCode);
                        SaveRepairProcessMaster(ObjRepairProcessMaster, da, lstErr);

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

        public void InsertRepairProcessMaster(RepairProcessMaster argRepairProcessMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@RepairProcessCode", argRepairProcessMaster.RepairProcessCode);
            param[1] = new SqlParameter("@RepairProcessName", argRepairProcessMaster.RepairProcessName);
            param[2] = new SqlParameter("@IsAssignToTechnician", argRepairProcessMaster.IsAssignToTechnician);
            param[3] = new SqlParameter("@IsCallClosed", argRepairProcessMaster.IsCallClosed);
            param[4] = new SqlParameter("@IsRepairCompleted", argRepairProcessMaster.IsRepairCompleted);
            param[5] = new SqlParameter("@IsApproved", argRepairProcessMaster.IsApproved);
            param[6] = new SqlParameter("@ClientCode", argRepairProcessMaster.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argRepairProcessMaster.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argRepairProcessMaster.ModifiedBy);
      
            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertRepairProcessMaster", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public void UpdateRepairProcessMaster(RepairProcessMaster argRepairProcessMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@RepairProcessCode", argRepairProcessMaster.RepairProcessCode);
            param[1] = new SqlParameter("@RepairProcessName", argRepairProcessMaster.RepairProcessName);
            param[2] = new SqlParameter("@IsAssignToTechnician", argRepairProcessMaster.IsAssignToTechnician);
            param[3] = new SqlParameter("@IsCallClosed", argRepairProcessMaster.IsCallClosed);
            param[4] = new SqlParameter("@IsRepairCompleted", argRepairProcessMaster.IsRepairCompleted);
            param[5] = new SqlParameter("@IsApproved", argRepairProcessMaster.IsApproved);
            param[6] = new SqlParameter("@ClientCode", argRepairProcessMaster.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argRepairProcessMaster.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argRepairProcessMaster.ModifiedBy);

            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateRepairProcessMaster", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public ICollection<ErrorHandler> DeleteRepairProcessMaster(string argRepairProcessCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@RepairProcessCode", argRepairProcessCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteRepairProcessMaster", param);


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

        public bool blnIsRepairProcessMasterExists(string argRepairProcessCode, string argClientCode)
        {
            bool IsRepairProcessMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetRepairProcessMaster(argRepairProcessCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsRepairProcessMasterExists = true;
            }
            else
            {
                IsRepairProcessMasterExists = false;
            }
            return IsRepairProcessMasterExists;
        }

        public bool blnIsRepairProcessMasterExists(string argRepairProcessCode, string argClientCode, DataAccess da)
        {
            bool IsRepairProcessMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetRepairProcessMaster(argRepairProcessCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsRepairProcessMasterExists = true;
            }
            else
            {
                IsRepairProcessMasterExists = false;
            }
            return IsRepairProcessMasterExists;
        }
    }
}