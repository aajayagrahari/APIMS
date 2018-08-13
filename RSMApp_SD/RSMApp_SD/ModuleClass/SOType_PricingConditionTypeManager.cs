
//Created On :: 28, September, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_SD
{
    public class SOType_PricingConditionTypeManager
    {
        const string SOType_PricingConditionTypeTable = "SOType_PricingConditionType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public SOType_PricingConditionType objGetSOType_PricingConditionType(string argSOTypeCode, string argConditionTypeCode, string argClientCode)
        {
            SOType_PricingConditionType argSOType_PricingConditionType = new SOType_PricingConditionType();
            DataSet DataSetToFill = new DataSet();

            if (argSOTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argConditionTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetSOType_PricingConditionType(argSOTypeCode, argConditionTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argSOType_PricingConditionType = this.objCreateSOType_PricingConditionType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argSOType_PricingConditionType;
        }

        public ICollection<SOType_PricingConditionType> colGetSOType_PricingConditionType(string argSOTypeCode,string argClientCode)
        {
            List<SOType_PricingConditionType> lst = new List<SOType_PricingConditionType>();
            DataSet DataSetToFill = new DataSet();
            SOType_PricingConditionType tSOType_PricingConditionType = new SOType_PricingConditionType();
            DataSetToFill = this.GetSOType_PricingConditionType(argSOTypeCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSOType_PricingConditionType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<SOType_PricingConditionType> colGetSOType_PricingConditionType(DataTable dt, string argUserName, string clientCode)
        {
            List<SOType_PricingConditionType> lst = new List<SOType_PricingConditionType>();
            SOType_PricingConditionType objSOType_PricingConditionType = null;
            foreach (DataRow dr in dt.Rows)
            {
                objSOType_PricingConditionType = new SOType_PricingConditionType();
                objSOType_PricingConditionType.SOTypeCode = Convert.ToString(dr["SOTypeCode"]).Trim();
                objSOType_PricingConditionType.ConditionTypeCode = Convert.ToString(dr["ConditionTypeCode"]).Trim();
                objSOType_PricingConditionType.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objSOType_PricingConditionType.ModifiedBy = Convert.ToString(argUserName).Trim();
                objSOType_PricingConditionType.CreatedBy = Convert.ToString(argUserName).Trim();
                objSOType_PricingConditionType.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objSOType_PricingConditionType);
            }
            return lst;
        }

        public DataSet GetSOType_PricingConditionType(string argSOTypeCode, string argConditionTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
            param[1] = new SqlParameter("@ConditionTypeCode", argConditionTypeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSOType_PricingConditionType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetSOType_PricingConditionType(string argSOTypeCode, string argConditionTypeCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
            param[1] = new SqlParameter("@ConditionTypeCode", argConditionTypeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSOType_PricingConditionType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetSOType_PricingConditionType(string argSOTypeCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSOType_PricingConditionType",param);
            return DataSetToFill;
        }

        private SOType_PricingConditionType objCreateSOType_PricingConditionType(DataRow dr)
        {
            SOType_PricingConditionType tSOType_PricingConditionType = new SOType_PricingConditionType();
            tSOType_PricingConditionType.SetObjectInfo(dr);
            return tSOType_PricingConditionType;
        }

        public ICollection<ErrorHandler> SaveSOType_PricingConditionType(SOType_PricingConditionType argSOType_PricingConditionType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsSOType_PricingConditionTypeExists(argSOType_PricingConditionType.SOTypeCode, argSOType_PricingConditionType.ConditionTypeCode, argSOType_PricingConditionType.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertSOType_PricingConditionType(argSOType_PricingConditionType, da, lstErr);
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
                    UpdateSOType_PricingConditionType(argSOType_PricingConditionType, da, lstErr);
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

        public ICollection<ErrorHandler> SaveSOType_PricingConditionType(ICollection<SOType_PricingConditionType> colGetSOType_PricingConditionType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (SOType_PricingConditionType argSOType_PricingConditionType in colGetSOType_PricingConditionType)
                {
                    if (argSOType_PricingConditionType.IsDeleted == 0)
                    {
                        if (blnIsSOType_PricingConditionTypeExists(argSOType_PricingConditionType.SOTypeCode, argSOType_PricingConditionType.ConditionTypeCode, argSOType_PricingConditionType.ClientCode, da) == false)
                        {
                            InsertSOType_PricingConditionType(argSOType_PricingConditionType, da, lstErr);
                        }
                        else
                        {
                            UpdateSOType_PricingConditionType(argSOType_PricingConditionType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteSOType_PricingConditionType(argSOType_PricingConditionType.SOTypeCode, argSOType_PricingConditionType.ConditionTypeCode, argSOType_PricingConditionType.ClientCode, argSOType_PricingConditionType.IsDeleted);
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

        public ICollection<ErrorHandler> SaveSOType_PricingConditionType(ICollection<SOType_PricingConditionType> colGetSOType_PricingConditionType, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (SOType_PricingConditionType argSOType_PricingConditionType in colGetSOType_PricingConditionType)
                {
                    if (argSOType_PricingConditionType.IsDeleted == 0)
                    {
                        if (blnIsSOType_PricingConditionTypeExists(argSOType_PricingConditionType.SOTypeCode, argSOType_PricingConditionType.ConditionTypeCode, argSOType_PricingConditionType.ClientCode, da) == false)
                        {
                            InsertSOType_PricingConditionType(argSOType_PricingConditionType, da, lstErr);
                        }
                        else
                        {
                            UpdateSOType_PricingConditionType(argSOType_PricingConditionType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteSOType_PricingConditionType(argSOType_PricingConditionType.SOTypeCode, argSOType_PricingConditionType.ConditionTypeCode, argSOType_PricingConditionType.ClientCode, argSOType_PricingConditionType.IsDeleted);
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
                    SaveSOType_PricingConditionType(colGetSOType_PricingConditionType(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertSOType_PricingConditionType(SOType_PricingConditionType argSOType_PricingConditionType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@SOTypeCode", argSOType_PricingConditionType.SOTypeCode);
            param[1] = new SqlParameter("@ConditionTypeCode", argSOType_PricingConditionType.ConditionTypeCode);
            param[2] = new SqlParameter("@ClientCode", argSOType_PricingConditionType.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argSOType_PricingConditionType.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argSOType_PricingConditionType.ModifiedBy);
         
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSOType_PricingConditionType", param);

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

        public void UpdateSOType_PricingConditionType(SOType_PricingConditionType argSOType_PricingConditionType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@SOTypeCode", argSOType_PricingConditionType.SOTypeCode);
            param[1] = new SqlParameter("@ConditionTypeCode", argSOType_PricingConditionType.ConditionTypeCode);
            param[2] = new SqlParameter("@ClientCode", argSOType_PricingConditionType.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argSOType_PricingConditionType.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argSOType_PricingConditionType.ModifiedBy);

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateSOType_PricingConditionType", param);

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

        public ICollection<ErrorHandler> DeleteSOType_PricingConditionType(string argSOTypeCode, string argConditionTypeCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
                param[1] = new SqlParameter("@ConditionTypeCode", argConditionTypeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteSOType_PricingConditionType", param);
                
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

        public bool blnIsSOType_PricingConditionTypeExists(string argSOTypeCode, string argConditionTypeCode, string argClientCode)
        {
            bool IsSOType_PricingConditionTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetSOType_PricingConditionType(argSOTypeCode, argConditionTypeCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSOType_PricingConditionTypeExists = true;
            }
            else
            {
                IsSOType_PricingConditionTypeExists = false;
            }
            return IsSOType_PricingConditionTypeExists;
        }

        public bool blnIsSOType_PricingConditionTypeExists(string argSOTypeCode, string argConditionTypeCode, string argClientCode,DataAccess da)
        {
            bool IsSOType_PricingConditionTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetSOType_PricingConditionType(argSOTypeCode, argConditionTypeCode, argClientCode,da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSOType_PricingConditionTypeExists = true;
            }
            else
            {
                IsSOType_PricingConditionTypeExists = false;
            }
            return IsSOType_PricingConditionTypeExists;
        }
    }
}