
//Created On :: 06, July, 2012
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
    public class DeliveryDocTypeManager
    {
        const string DeliveryDocTypeTable = "DeliveryDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public DeliveryDocType objGetDeliveryDocType(string argDeliveryDocTypeCode, string argClientCode)
        {
            DeliveryDocType argDeliveryDocType = new DeliveryDocType();
            DataSet DataSetToFill = new DataSet();

            if (argDeliveryDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetDeliveryDocType(argDeliveryDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argDeliveryDocType = this.objCreateDeliveryDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argDeliveryDocType;
        }
        
        public ICollection<DeliveryDocType> colGetDeliveryDocType(string argClientCode)
        {
            List<DeliveryDocType> lst = new List<DeliveryDocType>();
            DataSet DataSetToFill = new DataSet();
            DeliveryDocType tDeliveryDocType = new DeliveryDocType();

            DataSetToFill = this.GetDeliveryDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDeliveryDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetDeliveryDocType(string argDeliveryDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DeliveryDocTypeCode", argDeliveryDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDeliveryDocType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetDeliveryDocType(string argDeliveryDocTypeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DeliveryDocTypeCode", argDeliveryDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetDeliveryDocType4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetDeliveryDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDeliveryDocType", param);
            return DataSetToFill;
        }
        
        private DeliveryDocType objCreateDeliveryDocType(DataRow dr)
        {
            DeliveryDocType tDeliveryDocType = new DeliveryDocType();

            tDeliveryDocType.SetObjectInfo(dr);

            return tDeliveryDocType;

        }
        
        public ICollection<ErrorHandler> SaveDeliveryDocType(DeliveryDocType argDeliveryDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsDeliveryDocTypeExists(argDeliveryDocType.DeliveryDocTypeCode, argDeliveryDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertDeliveryDocType(argDeliveryDocType, da, lstErr);
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
                    UpdateDeliveryDocType(argDeliveryDocType, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
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
            return lstErr;
        }

        public void SaveDeliveryDocType(DeliveryDocType argDeliveryDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsDeliveryDocTypeExists(argDeliveryDocType.DeliveryDocTypeCode, argDeliveryDocType.ClientCode, da) == false)
                {
                    InsertDeliveryDocType(argDeliveryDocType, da, lstErr);
                }
                else
                {
                    UpdateDeliveryDocType(argDeliveryDocType, da, lstErr);
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
            DeliveryDocType ObjDeliveryDocType = null;
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
                        ObjDeliveryDocType = new DeliveryDocType();
                        ObjDeliveryDocType.DeliveryDocTypeCode = Convert.ToString(drExcel["DeliveryDocTypeCode"]).Trim();
                        ObjDeliveryDocType.DeliveryTypeDesc = Convert.ToString(drExcel["DeliveryTypeDesc"]).Trim();
                        ObjDeliveryDocType.CRLimitCheckType = Convert.ToString(drExcel["CRLimitCheckType"]).Trim();
                        ObjDeliveryDocType.ItemNoIncr = Convert.ToInt32(drExcel["ItemNoIncr"]);
                        ObjDeliveryDocType.NumRange = Convert.ToString(drExcel["NumRange"]).Trim();
                        ObjDeliveryDocType.RangeFrom = Convert.ToString(drExcel["RangeFrom"]).Trim();
                        ObjDeliveryDocType.RangeTo = Convert.ToString(drExcel["RangeTo"]).Trim();
                        ObjDeliveryDocType.ItemCategoryCode = Convert.ToString(drExcel["ItemCategoryCode"]).Trim();
                        ObjDeliveryDocType.SaveMode = Convert.ToInt32(drExcel["SaveMode"]);
                        ObjDeliveryDocType.CreatedBy = Convert.ToString(argUserName);
                        ObjDeliveryDocType.ModifiedBy = Convert.ToString(argUserName);
                        ObjDeliveryDocType.ClientCode = Convert.ToString(argClientCode);
                        SaveDeliveryDocType(ObjDeliveryDocType, da, lstErr);

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
        
        public void InsertDeliveryDocType(DeliveryDocType argDeliveryDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@DeliveryDocTypeCode", argDeliveryDocType.DeliveryDocTypeCode);
            param[1] = new SqlParameter("@DeliveryTypeDesc", argDeliveryDocType.DeliveryTypeDesc);
            param[2] = new SqlParameter("@CRLimitCheckType", argDeliveryDocType.CRLimitCheckType);
            param[3] = new SqlParameter("@ItemNoIncr", argDeliveryDocType.ItemNoIncr);
            param[4] = new SqlParameter("@NumRange", argDeliveryDocType.NumRange);
            param[5] = new SqlParameter("@RangeFrom", argDeliveryDocType.RangeFrom);
            param[6] = new SqlParameter("@RangeTo", argDeliveryDocType.RangeTo);
            param[7] = new SqlParameter("@ItemCategoryCode", argDeliveryDocType.ItemCategoryCode.Trim());
            param[8] = new SqlParameter("@SaveMode", argDeliveryDocType.SaveMode);
            param[9] = new SqlParameter("@ClientCode", argDeliveryDocType.ClientCode);
            param[10] = new SqlParameter("@CreatedBy", argDeliveryDocType.CreatedBy);
            param[11] = new SqlParameter("@ModifiedBy", argDeliveryDocType.ModifiedBy);
            
            param[12] = new SqlParameter("@Type", SqlDbType.Char);
            param[12].Size = 1;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[13].Size = 255;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[14].Size = 20;
            param[14].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertDeliveryDocType", param);


            string strMessage = Convert.ToString(param[13].Value);
            string strType = Convert.ToString(param[12].Value);
            string strRetValue = Convert.ToString(param[14].Value);


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
        
        public void UpdateDeliveryDocType(DeliveryDocType argDeliveryDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@DeliveryDocTypeCode", argDeliveryDocType.DeliveryDocTypeCode);
            param[1] = new SqlParameter("@DeliveryTypeDesc", argDeliveryDocType.DeliveryTypeDesc);
            param[2] = new SqlParameter("@CRLimitCheckType", argDeliveryDocType.CRLimitCheckType);
            param[3] = new SqlParameter("@ItemNoIncr", argDeliveryDocType.ItemNoIncr);
            param[4] = new SqlParameter("@NumRange", argDeliveryDocType.NumRange);
            param[5] = new SqlParameter("@RangeFrom", argDeliveryDocType.RangeFrom);
            param[6] = new SqlParameter("@RangeTo", argDeliveryDocType.RangeTo);
            param[7] = new SqlParameter("@ItemCategoryCode", argDeliveryDocType.ItemCategoryCode);
            param[8] = new SqlParameter("@SaveMode", argDeliveryDocType.SaveMode);
            param[9] = new SqlParameter("@ClientCode", argDeliveryDocType.ClientCode);
            param[10] = new SqlParameter("@CreatedBy", argDeliveryDocType.CreatedBy);
            param[11] = new SqlParameter("@ModifiedBy", argDeliveryDocType.ModifiedBy);
            
            param[12] = new SqlParameter("@Type", SqlDbType.Char);
            param[12].Size = 1;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[13].Size = 255;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[14].Size = 20;
            param[14].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateDeliveryDocType", param);
            
            string strMessage = Convert.ToString(param[13].Value);
            string strType = Convert.ToString(param[12].Value);
            string strRetValue = Convert.ToString(param[14].Value);


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
        
        public ICollection<ErrorHandler> DeleteDeliveryDocType(string argDeliveryDocTypeCode, string argClientCode, int IisDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@DeliveryDocTypeCode", argDeliveryDocTypeCode);
                param[1] = new SqlParameter("@IsDeleted", IisDeleted);
                param[2] = new SqlParameter("@ClientCode", argClientCode);


                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteDeliveryDocType", param);


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
        
        public bool blnIsDeliveryDocTypeExists(string argDeliveryDocTypeCode, string argClientCode)
        {
            bool IsDeliveryDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetDeliveryDocType(argDeliveryDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDeliveryDocTypeExists = true;
            }
            else
            {
                IsDeliveryDocTypeExists = false;
            }
            return IsDeliveryDocTypeExists;
        }

        public bool blnIsDeliveryDocTypeExists(string argDeliveryDocTypeCode, string argClientCode, DataAccess da)
        {
            bool IsDeliveryDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetDeliveryDocType(argDeliveryDocTypeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDeliveryDocTypeExists = true;
            }
            else
            {
                IsDeliveryDocTypeExists = false;
            }
            return IsDeliveryDocTypeExists;
        }
    }
}