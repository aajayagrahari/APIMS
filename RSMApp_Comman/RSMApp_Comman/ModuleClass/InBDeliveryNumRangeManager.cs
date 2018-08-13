
//Created On :: 15, October, 2012
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
    public class InBDeliveryNumRangeManager
    {
        const string InBDeliveryNumRangeTable = "InBDeliveryNumRange";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public InBDeliveryNumRange objGetInBDeliveryNumRange(string argInBDeliveryDocTypeCode, string argPlantCode, string argNumRangeCode, string argClientCode)
        {
            InBDeliveryNumRange argInBDeliveryNumRange = new InBDeliveryNumRange();
            DataSet DataSetToFill = new DataSet();

            if (argInBDeliveryDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argPlantCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argNumRangeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetInBDeliveryNumRange(argInBDeliveryDocTypeCode, argPlantCode, argNumRangeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argInBDeliveryNumRange = this.objCreateInBDeliveryNumRange((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argInBDeliveryNumRange;
        }

        public ICollection<InBDeliveryNumRange> colGetInBDeliveryNumRange(string argClientCode)
        {
            List<InBDeliveryNumRange> lst = new List<InBDeliveryNumRange>();
            DataSet DataSetToFill = new DataSet();
            InBDeliveryNumRange tInBDeliveryNumRange = new InBDeliveryNumRange();

            DataSetToFill = this.GetInBDeliveryNumRange(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateInBDeliveryNumRange(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetInBDeliveryNumRange(string argInBDeliveryDocTypeCode, string argPlantCode, string argNumRangeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@InBDeliveryDocTypeCode", argInBDeliveryDocTypeCode);
            param[1] = new SqlParameter("@PlantCode", argPlantCode);
            param[2] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetInBDeliveryNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetInBDeliveryNumRange(string argInBDeliveryDocTypeCode, string argPlantCode, string argNumRangeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@InBDeliveryDocTypeCode", argInBDeliveryDocTypeCode);
            param[1] = new SqlParameter("@PlantCode", argPlantCode);
            param[2] = new SqlParameter("@NumRangeCode", argNumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetInBDeliveryNumRange4ID", param);

            return DataSetToFill;
        }

        public DataSet GetInBDeliveryNumRange(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
     
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetInBDeliveryNumRange", param);

            return DataSetToFill;
        }

        private InBDeliveryNumRange objCreateInBDeliveryNumRange(DataRow dr)
        {
            InBDeliveryNumRange tInBDeliveryNumRange = new InBDeliveryNumRange();

            tInBDeliveryNumRange.SetObjectInfo(dr);

            return tInBDeliveryNumRange;

        }

        public ICollection<ErrorHandler> SaveInBDeliveryNumRange(InBDeliveryNumRange argInBDeliveryNumRange)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsInBDeliveryNumRangeExists(argInBDeliveryNumRange.InBDeliveryDocTypeCode, argInBDeliveryNumRange.PlantCode, argInBDeliveryNumRange.NumRangeCode, argInBDeliveryNumRange.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertInBDeliveryNumRange(argInBDeliveryNumRange, da, lstErr);
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
                    UpdateInBDeliveryNumRange(argInBDeliveryNumRange, da, lstErr);
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

        public void SaveInBDeliveryNumRange(InBDeliveryNumRange argInBDeliveryNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsInBDeliveryNumRangeExists(argInBDeliveryNumRange.InBDeliveryDocTypeCode, argInBDeliveryNumRange.PlantCode, argInBDeliveryNumRange.NumRangeCode, argInBDeliveryNumRange.ClientCode, da) == false)
                {
                    InsertInBDeliveryNumRange(argInBDeliveryNumRange, da, lstErr);
                }
                else
                {
                    UpdateInBDeliveryNumRange(argInBDeliveryNumRange, da, lstErr);
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
            InBDeliveryNumRange ObjInBDeliveryNumRange = null;
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
                        ObjInBDeliveryNumRange = new InBDeliveryNumRange();

                        ObjInBDeliveryNumRange.InBDeliveryDocTypeCode = Convert.ToString(drExcel["InBDeliveryDocTypeCode"]).Trim();
                        ObjInBDeliveryNumRange.PlantCode = Convert.ToString(drExcel["PlantCode"]).Trim();
                        ObjInBDeliveryNumRange.NumRangeCode = Convert.ToString(drExcel["NumRangeCode"]).Trim();
                        ObjInBDeliveryNumRange.CreatedBy = Convert.ToString(argUserName);
                        ObjInBDeliveryNumRange.ModifiedBy = Convert.ToString(argUserName);
                        ObjInBDeliveryNumRange.ClientCode = Convert.ToString(argClientCode);

                        SaveInBDeliveryNumRange(ObjInBDeliveryNumRange, da, lstErr);

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

        public void InsertInBDeliveryNumRange(InBDeliveryNumRange argInBDeliveryNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@InBDeliveryDocTypeCode", argInBDeliveryNumRange.InBDeliveryDocTypeCode);
            param[1] = new SqlParameter("@PlantCode", argInBDeliveryNumRange.PlantCode);
            param[2] = new SqlParameter("@NumRangeCode", argInBDeliveryNumRange.NumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argInBDeliveryNumRange.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argInBDeliveryNumRange.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argInBDeliveryNumRange.ModifiedBy);
           

            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertInBDeliveryNumRange", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public void UpdateInBDeliveryNumRange(InBDeliveryNumRange argInBDeliveryNumRange, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@InBDeliveryDocTypeCode", argInBDeliveryNumRange.InBDeliveryDocTypeCode);
            param[1] = new SqlParameter("@PlantCode", argInBDeliveryNumRange.PlantCode);
            param[2] = new SqlParameter("@NumRangeCode", argInBDeliveryNumRange.NumRangeCode);
            param[3] = new SqlParameter("@ClientCode", argInBDeliveryNumRange.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argInBDeliveryNumRange.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argInBDeliveryNumRange.ModifiedBy);


            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateInBDeliveryNumRange", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public ICollection<ErrorHandler> DeleteInBDeliveryNumRange(string argInBDeliveryDocTypeCode, string argPlantCode, string argNumRangeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@InBDeliveryDocTypeCode", argInBDeliveryDocTypeCode);
                param[1] = new SqlParameter("@PlantCode", argPlantCode);
                param[2] = new SqlParameter("@NumRangeCode", argNumRangeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteInBDeliveryNumRange", param);


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

        public bool blnIsInBDeliveryNumRangeExists(string argInBDeliveryDocTypeCode, string argPlantCode, string argNumRangeCode, string argClientCode)
        {
            bool IsInBDeliveryNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetInBDeliveryNumRange(argInBDeliveryDocTypeCode, argPlantCode, argNumRangeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsInBDeliveryNumRangeExists = true;
            }
            else
            {
                IsInBDeliveryNumRangeExists = false;
            }
            return IsInBDeliveryNumRangeExists;
        }

        public bool blnIsInBDeliveryNumRangeExists(string argInBDeliveryDocTypeCode, string argPlantCode, string argNumRangeCode, string argClientCode, DataAccess da)
        {
            bool IsInBDeliveryNumRangeExists = false;
            DataSet ds = new DataSet();
            ds = GetInBDeliveryNumRange(argInBDeliveryDocTypeCode, argPlantCode, argNumRangeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsInBDeliveryNumRangeExists = true;
            }
            else
            {
                IsInBDeliveryNumRangeExists = false;
            }
            return IsInBDeliveryNumRangeExists;
        }
    }
}