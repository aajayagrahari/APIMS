
//Created On :: 19, October, 2012
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
    public class ConAgst_RepairTypeManager
    {
        const string ConAgst_RepairTypeTable = "ConAgst_RepairType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public ConAgst_RepairType objGetConAgst_RepairType(string argMatGroup1Code, string argRepairTypeCode, string argConMatGroup1Code, string argClientCode)
        {
            ConAgst_RepairType argConAgst_RepairType = new ConAgst_RepairType();
            DataSet DataSetToFill = new DataSet();

            if (argMatGroup1Code.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argRepairTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argConMatGroup1Code.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetConAgst_RepairType(argMatGroup1Code, argRepairTypeCode, argConMatGroup1Code, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argConAgst_RepairType = this.objCreateConAgst_RepairType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argConAgst_RepairType;
        }

        public ICollection<ConAgst_RepairType> colGetConAgst_RepairType(string argMatGroup1Code, string argClientCode)
        {
            List<ConAgst_RepairType> lst = new List<ConAgst_RepairType>();
            DataSet DataSetToFill = new DataSet();
            ConAgst_RepairType tConAgst_RepairType = new ConAgst_RepairType();

            DataSetToFill = this.GetConAgst_RepairType(argMatGroup1Code, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateConAgst_RepairType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<ConAgst_RepairType> colGetConAgst_RepairType(DataTable dt, string argUserName, string clientCode)
        {
            List<ConAgst_RepairType> lst = new List<ConAgst_RepairType>();
            ConAgst_RepairType objConAgst_RepairType = null;
            foreach (DataRow dr in dt.Rows)
            {
                objConAgst_RepairType = new ConAgst_RepairType();
                objConAgst_RepairType.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]).Trim();
                objConAgst_RepairType.RepairTypeCode = Convert.ToString(dr["RepairTypeCode"]).Trim();
                objConAgst_RepairType.ConMatGroup1Code = Convert.ToString(dr["ConMatGroup1Code"]).Trim();
                objConAgst_RepairType.MaterialCode = Convert.ToString(dr["MaterialCode"]).Trim();
                objConAgst_RepairType.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objConAgst_RepairType.ModifiedBy = Convert.ToString(argUserName).Trim();
                objConAgst_RepairType.CreatedBy = Convert.ToString(argUserName).Trim();
                objConAgst_RepairType.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objConAgst_RepairType);
            }
            return lst;
        }

        public DataSet GetConAgst_RepairType(string argMatGroup1Code, string argRepairTypeCode, string argConMatGroup1Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@RepairTypeCode", argRepairTypeCode);
            param[2] = new SqlParameter("@ConMatGroup1Code", argConMatGroup1Code);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetConAgst_RepairType4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetConAgst_RepairType(string argMatGroup1Code, string argRepairTypeCode, string argConMatGroup1Code, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@RepairTypeCode", argRepairTypeCode);
            param[2] = new SqlParameter("@ConMatGroup1Code", argConMatGroup1Code);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetConAgst_RepairType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetConAgst_RepairType(string argMatGroup1Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetConAgst_RepairType", param);
            return DataSetToFill;
        }

        public DataSet GetConAgst_RepairType4Repair(string argMatGroup1Code, string argMaterialCode, string argRepairTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@RepairTypeCode", argRepairTypeCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetConAgst_RepairType4Repair", param);
            return DataSetToFill;
        }

        public DataSet GetConAgst_RepairType(string argMatGroup1Code, string argRepairTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@RepairTypeCode", argRepairTypeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetConAgst_RepairType4RepairType", param);
            return DataSetToFill;
        }

        private ConAgst_RepairType objCreateConAgst_RepairType(DataRow dr)
        {
            ConAgst_RepairType tConAgst_RepairType = new ConAgst_RepairType();
            tConAgst_RepairType.SetObjectInfo(dr);
            return tConAgst_RepairType;
        }

        public ICollection<ErrorHandler> SaveConAgst_RepairType(ConAgst_RepairType argConAgst_RepairType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsConAgst_RepairTypeExists(argConAgst_RepairType.MatGroup1Code, argConAgst_RepairType.RepairTypeCode, argConAgst_RepairType.ConMatGroup1Code, argConAgst_RepairType.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertConAgst_RepairType(argConAgst_RepairType, da, lstErr);
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
                    UpdateConAgst_RepairType(argConAgst_RepairType, da, lstErr);
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

        public ICollection<ErrorHandler> SaveConAgst_RepairType(ICollection<ConAgst_RepairType> colGetConAgst_RepairType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (ConAgst_RepairType argConAgst_RepairType in colGetConAgst_RepairType)
                {
                    if (argConAgst_RepairType.IsDeleted == 0)
                    {
                        if (blnIsConAgst_RepairTypeExists(argConAgst_RepairType.MatGroup1Code, argConAgst_RepairType.RepairTypeCode, argConAgst_RepairType.ConMatGroup1Code, argConAgst_RepairType.ClientCode, da) == false)
                        {
                            InsertConAgst_RepairType(argConAgst_RepairType, da, lstErr);
                        }
                        else
                        {
                            UpdateConAgst_RepairType(argConAgst_RepairType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteConAgst_RepairType(argConAgst_RepairType.MatGroup1Code, argConAgst_RepairType.RepairTypeCode, argConAgst_RepairType.ConMatGroup1Code, argConAgst_RepairType.ClientCode, argConAgst_RepairType.IsDeleted);
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

        public ICollection<ErrorHandler> SaveConAgst_RepairType(ICollection<ConAgst_RepairType> colGetConAgst_RepairType, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (ConAgst_RepairType argConAgst_RepairType in colGetConAgst_RepairType)
                {
                    if (argConAgst_RepairType.IsDeleted == 0)
                    {
                        if (blnIsConAgst_RepairTypeExists(argConAgst_RepairType.MatGroup1Code, argConAgst_RepairType.RepairTypeCode, argConAgst_RepairType.ConMatGroup1Code, argConAgst_RepairType.ClientCode, da) == false)
                        {
                            InsertConAgst_RepairType(argConAgst_RepairType, da, lstErr);
                        }
                        else
                        {
                            UpdateConAgst_RepairType(argConAgst_RepairType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteConAgst_RepairType(argConAgst_RepairType.MatGroup1Code, argConAgst_RepairType.RepairTypeCode, argConAgst_RepairType.ConMatGroup1Code, argConAgst_RepairType.ClientCode, argConAgst_RepairType.IsDeleted);
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
                    SaveConAgst_RepairType(colGetConAgst_RepairType(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertConAgst_RepairType(ConAgst_RepairType argConAgst_RepairType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@MatGroup1Code", argConAgst_RepairType.MatGroup1Code);
            param[1] = new SqlParameter("@RepairTypeCode", argConAgst_RepairType.RepairTypeCode);
            param[2] = new SqlParameter("@ConMatGroup1Code", argConAgst_RepairType.ConMatGroup1Code);
            param[3] = new SqlParameter("@MaterialCode", argConAgst_RepairType.MaterialCode);
            param[4] = new SqlParameter("@ClientCode", argConAgst_RepairType.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argConAgst_RepairType.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argConAgst_RepairType.ModifiedBy);
    
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertConAgst_RepairType", param);

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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);
        }

        public void UpdateConAgst_RepairType(ConAgst_RepairType argConAgst_RepairType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@MatGroup1Code", argConAgst_RepairType.MatGroup1Code);
            param[1] = new SqlParameter("@RepairTypeCode", argConAgst_RepairType.RepairTypeCode);
            param[2] = new SqlParameter("@ConMatGroup1Code", argConAgst_RepairType.ConMatGroup1Code);
            param[3] = new SqlParameter("@MaterialCode", argConAgst_RepairType.MaterialCode);
            param[4] = new SqlParameter("@ClientCode", argConAgst_RepairType.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argConAgst_RepairType.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argConAgst_RepairType.ModifiedBy);

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateConAgst_RepairType", param);

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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);
        }

        public ICollection<ErrorHandler> DeleteConAgst_RepairType(string argMatGroup1Code, string argRepairTypeCode, string argConMatGroup1Code, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
                param[1] = new SqlParameter("@RepairTypeCode", argRepairTypeCode);
                param[2] = new SqlParameter("@ConMatGroup1Code", argConMatGroup1Code);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteConAgst_RepairType", param);

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

        public bool blnIsConAgst_RepairTypeExists(string argMatGroup1Code, string argRepairTypeCode, string argConMatGroup1Code, string argClientCode)
        {
            bool IsConAgst_RepairTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetConAgst_RepairType(argMatGroup1Code, argRepairTypeCode, argConMatGroup1Code, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsConAgst_RepairTypeExists = true;
            }
            else
            {
                IsConAgst_RepairTypeExists = false;
            }
            return IsConAgst_RepairTypeExists;
        }
       
        public bool blnIsConAgst_RepairTypeExists(string argMatGroup1Code, string argRepairTypeCode, string argConMatGroup1Code, string argClientCode, DataAccess da)
        {
            bool IsConAgst_RepairTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetConAgst_RepairType(argMatGroup1Code, argRepairTypeCode, argConMatGroup1Code, argClientCode, da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsConAgst_RepairTypeExists = true;
            }
            else
            {
                IsConAgst_RepairTypeExists = false;
            }
            return IsConAgst_RepairTypeExists;
        }
    }
}