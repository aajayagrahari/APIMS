
//Created On :: 17, November, 2012
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
    public class RepairDocType_CallTypeManager
    {
        const string RepairDocType_CallTypeTable = "RepairDocType_CallType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public RepairDocType_CallType objGetRepairDocType_CallType(string argRepairDocTypeCode, string argCallTypeCode, string argClientCode)
        {
            RepairDocType_CallType argRepairDocType_CallType = new RepairDocType_CallType();
            DataSet DataSetToFill = new DataSet();

            if (argRepairDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argCallTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetRepairDocType_CallType(argRepairDocTypeCode, argCallTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argRepairDocType_CallType = this.objCreateRepairDocType_CallType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argRepairDocType_CallType;
        }

        public ICollection<RepairDocType_CallType> colGetRepairDocType_CallType(string argRepairDocTypeCode, string argClientCode)
        {
            List<RepairDocType_CallType> lst = new List<RepairDocType_CallType>();
            DataSet DataSetToFill = new DataSet();
            RepairDocType_CallType tRepairDocType_CallType = new RepairDocType_CallType();

            DataSetToFill = this.GetRepairDocType_CallType(argRepairDocTypeCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateRepairDocType_CallType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<RepairDocType_CallType> colGetRepairDocType_CallType(DataTable dt, string argUserName, string clientCode)
        {
            List<RepairDocType_CallType> lst = new List<RepairDocType_CallType>();
            RepairDocType_CallType objRepairDocType_CallType = null;
            foreach (DataRow dr in dt.Rows)
            {
                objRepairDocType_CallType = new RepairDocType_CallType();
                objRepairDocType_CallType.RepairDocTypeCode = Convert.ToString(dr["RepairDocTypeCode"]).Trim();
                objRepairDocType_CallType.CallTypeCode = Convert.ToString(dr["CallTypeCode"]).Trim();
                objRepairDocType_CallType.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objRepairDocType_CallType.ModifiedBy = Convert.ToString(argUserName).Trim();
                objRepairDocType_CallType.CreatedBy = Convert.ToString(argUserName).Trim();
                objRepairDocType_CallType.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objRepairDocType_CallType);
            }
            return lst;
        }

        public DataSet GetRepairDocType_CallType(string argRepairDocTypeCode, string argCallTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RepairDocTypeCode", argRepairDocTypeCode);
            param[1] = new SqlParameter("@CallTypeCode", argCallTypeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepairDocType_CallType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetRepairDocType_CallType(string argRepairDocTypeCode, string argCallTypeCode, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RepairDocTypeCode", argRepairDocTypeCode);
            param[1] = new SqlParameter("@CallTypeCode", argCallTypeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetRepairDocType_CallType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetRepairDocType_CallType(string argRepairDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RepairDocTypeCode", argRepairDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetRepairDocType_CallType",param);
            return DataSetToFill;
        }

        private RepairDocType_CallType objCreateRepairDocType_CallType(DataRow dr)
        {
            RepairDocType_CallType tRepairDocType_CallType = new RepairDocType_CallType();

            tRepairDocType_CallType.SetObjectInfo(dr);

            return tRepairDocType_CallType;

        }

        public ICollection<ErrorHandler> SaveRepairDocType_CallType(RepairDocType_CallType argRepairDocType_CallType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsRepairDocType_CallTypeExists(argRepairDocType_CallType.RepairDocTypeCode, argRepairDocType_CallType.CallTypeCode, argRepairDocType_CallType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertRepairDocType_CallType(argRepairDocType_CallType, da, lstErr);
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
                    UpdateRepairDocType_CallType(argRepairDocType_CallType, da, lstErr);
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

        public ICollection<ErrorHandler> SaveRepairDocType_CallType(ICollection<RepairDocType_CallType> colGetRepairDocType_CallType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (RepairDocType_CallType argRepairDocType_CallType in colGetRepairDocType_CallType)
                {

                    if (argRepairDocType_CallType.IsDeleted == 0)
                    {

                        if (blnIsRepairDocType_CallTypeExists(argRepairDocType_CallType.RepairDocTypeCode, argRepairDocType_CallType.CallTypeCode, argRepairDocType_CallType.ClientCode) == false)
                        {
                            InsertRepairDocType_CallType(argRepairDocType_CallType, da, lstErr);
                        }
                        else
                        {
                            UpdateRepairDocType_CallType(argRepairDocType_CallType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteRepairDocType_CallType(argRepairDocType_CallType.RepairDocTypeCode, argRepairDocType_CallType.CallTypeCode, argRepairDocType_CallType.ClientCode, argRepairDocType_CallType.IsDeleted);

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

        public ICollection<ErrorHandler> SaveRepairDocType_CallType(ICollection<RepairDocType_CallType> colGetRepairDocType_CallType, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (RepairDocType_CallType argRepairDocType_CallType in colGetRepairDocType_CallType)
                {
                    if (argRepairDocType_CallType.IsDeleted == 0)
                    {
                        if (blnIsRepairDocType_CallTypeExists(argRepairDocType_CallType.RepairDocTypeCode, argRepairDocType_CallType.CallTypeCode, argRepairDocType_CallType.ClientCode, da) == false)
                        {
                            InsertRepairDocType_CallType(argRepairDocType_CallType, da, lstErr);
                        }
                        else
                        {
                            UpdateRepairDocType_CallType(argRepairDocType_CallType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteRepairDocType_CallType(argRepairDocType_CallType.RepairDocTypeCode, argRepairDocType_CallType.CallTypeCode, argRepairDocType_CallType.ClientCode, argRepairDocType_CallType.IsDeleted);
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
                    SaveRepairDocType_CallType(colGetRepairDocType_CallType(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertRepairDocType_CallType(RepairDocType_CallType argRepairDocType_CallType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@RepairDocTypeCode", argRepairDocType_CallType.RepairDocTypeCode);
            param[1] = new SqlParameter("@CallTypeCode", argRepairDocType_CallType.CallTypeCode);
            param[2] = new SqlParameter("@ClientCode", argRepairDocType_CallType.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argRepairDocType_CallType.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argRepairDocType_CallType.ModifiedBy);
      
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertRepairDocType_CallType", param);


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

        public void UpdateRepairDocType_CallType(RepairDocType_CallType argRepairDocType_CallType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@RepairDocTypeCode", argRepairDocType_CallType.RepairDocTypeCode);
            param[1] = new SqlParameter("@CallTypeCode", argRepairDocType_CallType.CallTypeCode);
            param[2] = new SqlParameter("@ClientCode", argRepairDocType_CallType.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argRepairDocType_CallType.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argRepairDocType_CallType.ModifiedBy);

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateRepairDocType_CallType", param);


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

        public ICollection<ErrorHandler> DeleteRepairDocType_CallType(string argRepairDocTypeCode, string argCallTypeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@RepairDocTypeCode", argRepairDocTypeCode);
                param[1] = new SqlParameter("@CallTypeCode", argCallTypeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteRepairDocType_CallType", param);


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

        public bool blnIsRepairDocType_CallTypeExists(string argRepairDocTypeCode, string argCallTypeCode, string argClientCode)
        {
            bool IsRepairDocType_CallTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetRepairDocType_CallType(argRepairDocTypeCode, argCallTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsRepairDocType_CallTypeExists = true;
            }
            else
            {
                IsRepairDocType_CallTypeExists = false;
            }
            return IsRepairDocType_CallTypeExists;
        }

        public bool blnIsRepairDocType_CallTypeExists(string argRepairDocTypeCode, string argCallTypeCode, string argClientCode, DataAccess da)
        {
            bool IsRepairDocType_CallTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetRepairDocType_CallType(argRepairDocTypeCode, argCallTypeCode, argClientCode, da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsRepairDocType_CallTypeExists = true;
            }
            else
            {
                IsRepairDocType_CallTypeExists = false;
            }
            return IsRepairDocType_CallTypeExists;
        }
    }
}