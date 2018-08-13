
//Created On :: 29, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class StockTransferDocTypeManager
    {
        const string StockTransferDocTypeTable = "StockTransferDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public StockTransferDocType objGetStockTransferDocType(string argStockTranDocTypeCode, string argClientCode)
        {
            StockTransferDocType argStockTransferDocType = new StockTransferDocType();
            DataSet DataSetToFill = new DataSet();

            if (argStockTranDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetStockTransferDocType(argStockTranDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argStockTransferDocType = this.objCreateStockTransferDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argStockTransferDocType;
        }


        public ICollection<StockTransferDocType> colGetStockTransferDocType(string argClientCode)
        {
            List<StockTransferDocType> lst = new List<StockTransferDocType>();
            DataSet DataSetToFill = new DataSet();
            StockTransferDocType tStockTransferDocType = new StockTransferDocType();

            DataSetToFill = this.GetStockTransferDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateStockTransferDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetStockTransferDocType(string argStockTranDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@StockTranDocTypeCode", argStockTranDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetStockTransferDocType4ID", param);

            return DataSetToFill;
        }


        public DataSet GetStockTransferDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetStockTransferDocType", param);
            return DataSetToFill;
        }


        private StockTransferDocType objCreateStockTransferDocType(DataRow dr)
        {
            StockTransferDocType tStockTransferDocType = new StockTransferDocType();

            tStockTransferDocType.SetObjectInfo(dr);

            return tStockTransferDocType;

        }


        public ICollection<ErrorHandler> SaveStockTransferDocType(StockTransferDocType argStockTransferDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsStockTransferDocTypeExists(argStockTransferDocType.StockTranDocTypeCode, argStockTransferDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertStockTransferDocType(argStockTransferDocType, da, lstErr);
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
                    UpdateStockTransferDocType(argStockTransferDocType, da, lstErr);
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


        public void InsertStockTransferDocType(StockTransferDocType argStockTransferDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@StockTranDocTypeCode", argStockTransferDocType.StockTranDocTypeCode);
            param[1] = new SqlParameter("@StockTranDocTypeDesc", argStockTransferDocType.StockTranDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argStockTransferDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argStockTransferDocType.NumRange);
            param[4] = new SqlParameter("@AssignType", argStockTransferDocType.AssignType);
            param[5] = new SqlParameter("@MaterialDocTypeCode", argStockTransferDocType.MaterialDocTypeCode);
            param[6] = new SqlParameter("@RepairProcessCode", argStockTransferDocType.RepairProcessCode);
            param[7] = new SqlParameter("@DefaultFromStoreCode", argStockTransferDocType.DefaultFromStoreCode);
            param[8] = new SqlParameter("@DefaultFromStockIndicator", argStockTransferDocType.DefaultFromStockIndicator);
            param[9] = new SqlParameter("@DefaultToStoreCode", argStockTransferDocType.DefaultToStoreCode);
            param[10] = new SqlParameter("@DefaultToStockIndicator", argStockTransferDocType.DefaultToStockIndicator);
            param[11] = new SqlParameter("@ClientCode", argStockTransferDocType.ClientCode);
            param[12] = new SqlParameter("@CreatedBy", argStockTransferDocType.CreatedBy);
            param[13] = new SqlParameter("@ModifiedBy", argStockTransferDocType.ModifiedBy);
           
            param[14] = new SqlParameter("@Type", SqlDbType.Char);
            param[14].Size = 1;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[15].Size = 255;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[16].Size = 20;
            param[16].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertStockTransferDocType", param);


            string strMessage = Convert.ToString(param[15].Value);
            string strType = Convert.ToString(param[14].Value);
            string strRetValue = Convert.ToString(param[16].Value);


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


        public void UpdateStockTransferDocType(StockTransferDocType argStockTransferDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@StockTranDocTypeCode", argStockTransferDocType.StockTranDocTypeCode);
            param[1] = new SqlParameter("@StockTranDocTypeDesc", argStockTransferDocType.StockTranDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argStockTransferDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argStockTransferDocType.NumRange);
            param[4] = new SqlParameter("@AssignType", argStockTransferDocType.AssignType);
            param[5] = new SqlParameter("@MaterialDocTypeCode", argStockTransferDocType.MaterialDocTypeCode);
            param[6] = new SqlParameter("@RepairProcessCode", argStockTransferDocType.RepairProcessCode);
            param[7] = new SqlParameter("@DefaultFromStoreCode", argStockTransferDocType.DefaultFromStoreCode);
            param[8] = new SqlParameter("@DefaultFromStockIndicator", argStockTransferDocType.DefaultFromStockIndicator);
            param[9] = new SqlParameter("@DefaultToStoreCode", argStockTransferDocType.DefaultToStoreCode);
            param[10] = new SqlParameter("@DefaultToStockIndicator", argStockTransferDocType.DefaultToStockIndicator);
            param[11] = new SqlParameter("@ClientCode", argStockTransferDocType.ClientCode);
            param[12] = new SqlParameter("@CreatedBy", argStockTransferDocType.CreatedBy);
            param[13] = new SqlParameter("@ModifiedBy", argStockTransferDocType.ModifiedBy);
          
            param[14] = new SqlParameter("@Type", SqlDbType.Char);
            param[14].Size = 1;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[15].Size = 255;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[16].Size = 20;
            param[16].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateStockTransferDocType", param);


            string strMessage = Convert.ToString(param[15].Value);
            string strType = Convert.ToString(param[14].Value);
            string strRetValue = Convert.ToString(param[16].Value);


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


        public ICollection<ErrorHandler> DeleteStockTransferDocType(string argStockTranDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@StockTranDocTypeCode", argStockTranDocTypeCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);

                param[2] = new SqlParameter("@Type", SqlDbType.Char);
                param[2].Size = 1;
                param[2].Direction = ParameterDirection.Output;
                param[3] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[3].Size = 255;
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[4].Size = 20;
                param[4].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteStockTransferDocType", param);


                string strMessage = Convert.ToString(param[3].Value);
                string strType = Convert.ToString(param[2].Value);
                string strRetValue = Convert.ToString(param[4].Value);


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


        public bool blnIsStockTransferDocTypeExists(string argStockTranDocTypeCode, string argClientCode)
        {
            bool IsStockTransferDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetStockTransferDocType(argStockTranDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsStockTransferDocTypeExists = true;
            }
            else
            {
                IsStockTransferDocTypeExists = false;
            }
            return IsStockTransferDocTypeExists;
        }
    }
}