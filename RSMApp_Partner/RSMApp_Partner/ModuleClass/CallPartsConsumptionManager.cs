
//Created On :: 27, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallPartsConsumptionManager
    {
        const string CallPartsConsumptionTable = "CallPartsConsumption";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CallPartsConsumption objGetCallPartsConsumption(string argCallCode, int argCallItemNo, string argRepairProcDocCode, int argPCItemNo, string argClientCode)
        {
            CallPartsConsumption argCallPartsConsumption = new CallPartsConsumption();
            DataSet DataSetToFill = new DataSet();

            if (argCallCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argCallItemNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argRepairProcDocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argPCItemNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallPartsConsumption(argCallCode, argCallItemNo, argRepairProcDocCode, argPCItemNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallPartsConsumption = this.objCreateCallPartsConsumption((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallPartsConsumption;
        }

        public ICollection<CallPartsConsumption> colGetCallPartsConsumption(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode)
        {
            List<CallPartsConsumption> lst = new List<CallPartsConsumption>();
            DataSet DataSetToFill = new DataSet();
            CallPartsConsumption tCallPartsConsumption = new CallPartsConsumption();

            DataSetToFill = this.GetCallPartsConsumption(argCallCode, argCallItemNo, argRepairProcDocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallPartsConsumption(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public void colGetCallPartsConsumption(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode, ref CallPartConsumptionCol argCallPartConsumptionCol)
        {
            DataSet DataSetToFill = new DataSet();
            CallPartsConsumption tCallPartsConsumption = new CallPartsConsumption();

            DataSetToFill = this.GetCallPartsConsumption(argCallCode, argCallItemNo, argRepairProcDocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argCallPartConsumptionCol.colCallPartsConsumption.Add(objCreateCallPartsConsumption(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;
        }

        public DataSet GetCallPartsConsumption(string argCallCode, int argCallItemNo, string argRepairProcDocCode, int argPCItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
            param[3] = new SqlParameter("@PCItemNo", argPCItemNo);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallPartsConsumption4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallPartsConsumption(string argCallCode, int argCallItemNo, string argRepairProcDocCode, int argPCItemNo, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
            param[3] = new SqlParameter("@PCItemNo", argPCItemNo);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallPartsConsumption4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallPartsConsumption(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallPartsConsumption", param);
            return DataSetToFill;
        }

        private CallPartsConsumption objCreateCallPartsConsumption(DataRow dr)
        {
            CallPartsConsumption tCallPartsConsumption = new CallPartsConsumption();

            tCallPartsConsumption.SetObjectInfo(dr);

            return tCallPartsConsumption;

        }
        
        public ICollection<ErrorHandler> SaveCallPartsConsumption(CallPartsConsumption argCallPartsConsumption)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallPartsConsumptionExists(argCallPartsConsumption.CallCode, argCallPartsConsumption.CallItemNo, argCallPartsConsumption.RepairProcDocCode, argCallPartsConsumption.PCItemNo, argCallPartsConsumption.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallPartsConsumption(argCallPartsConsumption, da, lstErr);
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
                    UpdateCallPartsConsumption(argCallPartsConsumption, da, lstErr);
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

        public void SaveCallPartsConsumption(CallPartsConsumption argCallPartsConsumption, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCallPartsConsumptionExists(argCallPartsConsumption.CallCode, argCallPartsConsumption.CallItemNo, argCallPartsConsumption.RepairProcDocCode, argCallPartsConsumption.PCItemNo, argCallPartsConsumption.ClientCode, da) == false)
                {
                    InsertCallPartsConsumption(argCallPartsConsumption, da, lstErr);
                }
                else
                {
                    UpdateCallPartsConsumption(argCallPartsConsumption, da, lstErr);
                }
            }
            catch (Exception ex)
            {
                objErrorHandler.Type = ErrorConstant.strAboartType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strInsertModule;
                objErrorHandler.ModulePart = ErrorConstant.strDetailModule;
                objErrorHandler.Message = ex.Message.ToString();
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";
                lstErr.Add(objErrorHandler);
            }
        }

        public void InsertCallPartsConsumption(CallPartsConsumption argCallPartsConsumption, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[32];

            param[0] = new SqlParameter("@CallCode", argCallPartsConsumption.CallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallPartsConsumption.CallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argCallPartsConsumption.RepairProcDocCode);
            param[3] = new SqlParameter("@PCItemNo", argCallPartsConsumption.PCItemNo);
            param[4] = new SqlParameter("@SerialNo1", argCallPartsConsumption.SerialNo1);
            param[5] = new SqlParameter("@SerialNo2", argCallPartsConsumption.SerialNo2);
            param[6] = new SqlParameter("@MaterialCode", argCallPartsConsumption.MaterialCode);
            param[7] = new SqlParameter("@MatGroup1Code", argCallPartsConsumption.MatGroup1Code);
            param[8] = new SqlParameter("@PartnerCode", argCallPartsConsumption.PartnerCode);
            param[9] = new SqlParameter("@PartnerEmployeeCode", argCallPartsConsumption.PartnerEmployeeCode);
            param[10] = new SqlParameter("@StoreCode", argCallPartsConsumption.StoreCode);
            param[11] = new SqlParameter("@StockIndicator", argCallPartsConsumption.StockIndicator);
            param[12] = new SqlParameter("@ToPartnerEmployeeCode", argCallPartsConsumption.ToPartnerEmployeeCode);
            param[13] = new SqlParameter("@ToStoreCode", argCallPartsConsumption.ToStoreCode);
            param[14] = new SqlParameter("@ToStockIndicator", argCallPartsConsumption.ToStockIndicator);
            param[15] = new SqlParameter("@ToMaterialCode", argCallPartsConsumption.ToMaterialCode);
            param[16] = new SqlParameter("@Quantity", argCallPartsConsumption.Quantity);
            param[17] = new SqlParameter("@UOMCode", argCallPartsConsumption.UOMCode);

            param[18] = new SqlParameter("@UnitPrice", argCallPartsConsumption.UnitPrice);

            param[19] = new SqlParameter("@MaterialDocTypeCode", argCallPartsConsumption.MaterialDocTypeCode);
            param[20] = new SqlParameter("@HigherLevel", argCallPartsConsumption.HigherLevel);


            param[21] = new SqlParameter("@PGoodsMovementCode", argCallPartsConsumption.PGoodsMovementCode);
            param[22] = new SqlParameter("@GMItemNo", argCallPartsConsumption.GMItemNo);

            param[23] = new SqlParameter("@IsUnderWarranty", argCallPartsConsumption.IsUnderWarranty);

            param[24] = new SqlParameter("@SalesVatPer", argCallPartsConsumption.SalesVatPer);
            param[25] = new SqlParameter("@SalesVatAmt", argCallPartsConsumption.SalesVatAmt);


            param[26] = new SqlParameter("@ClientCode", argCallPartsConsumption.ClientCode);
            param[27] = new SqlParameter("@CreatedBy", argCallPartsConsumption.CreatedBy);
            param[28] = new SqlParameter("@ModifiedBy", argCallPartsConsumption.ModifiedBy);
        
            param[29] = new SqlParameter("@Type", SqlDbType.Char);
            param[29].Size = 1;
            param[29].Direction = ParameterDirection.Output;

            param[30] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[30].Size = 255;
            param[30].Direction = ParameterDirection.Output;

            param[31] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[31].Size = 20;
            param[31].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallPartsConsumption", param);


            string strMessage = Convert.ToString(param[30].Value);
            string strType = Convert.ToString(param[29].Value);
            string strRetValue = Convert.ToString(param[31].Value);


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
        
        public void UpdateCallPartsConsumption(CallPartsConsumption argCallPartsConsumption, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[32];

            param[0] = new SqlParameter("@CallCode", argCallPartsConsumption.CallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallPartsConsumption.CallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argCallPartsConsumption.RepairProcDocCode);
            param[3] = new SqlParameter("@PCItemNo", argCallPartsConsumption.PCItemNo);
            param[4] = new SqlParameter("@SerialNo1", argCallPartsConsumption.SerialNo1);
            param[5] = new SqlParameter("@SerialNo2", argCallPartsConsumption.SerialNo2);
            param[6] = new SqlParameter("@MaterialCode", argCallPartsConsumption.MaterialCode);
            param[7] = new SqlParameter("@MatGroup1Code", argCallPartsConsumption.MatGroup1Code);
            param[8] = new SqlParameter("@PartnerCode", argCallPartsConsumption.PartnerCode);
            param[9] = new SqlParameter("@PartnerEmployeeCode", argCallPartsConsumption.PartnerEmployeeCode);
            param[10] = new SqlParameter("@StoreCode", argCallPartsConsumption.StoreCode);
            param[11] = new SqlParameter("@StockIndicator", argCallPartsConsumption.StockIndicator);
            param[12] = new SqlParameter("@ToPartnerEmployeeCode", argCallPartsConsumption.ToPartnerEmployeeCode);
            param[13] = new SqlParameter("@ToStoreCode", argCallPartsConsumption.ToStoreCode);
            param[14] = new SqlParameter("@ToStockIndicator", argCallPartsConsumption.ToStockIndicator);
            param[15] = new SqlParameter("@ToMaterialCode", argCallPartsConsumption.ToMaterialCode);
            param[16] = new SqlParameter("@Quantity", argCallPartsConsumption.Quantity);
            param[17] = new SqlParameter("@UOMCode", argCallPartsConsumption.UOMCode);

            param[18] = new SqlParameter("@UnitPrice", argCallPartsConsumption.UnitPrice);

            param[19] = new SqlParameter("@MaterialDocTypeCode", argCallPartsConsumption.MaterialDocTypeCode);
            param[20] = new SqlParameter("@HigherLevel", argCallPartsConsumption.HigherLevel);

            param[21] = new SqlParameter("@PGoodsMovementCode", argCallPartsConsumption.PGoodsMovementCode);
            param[22] = new SqlParameter("@GMItemNo", argCallPartsConsumption.GMItemNo);

            param[23] = new SqlParameter("@IsUnderWarranty", argCallPartsConsumption.IsUnderWarranty);

            param[24] = new SqlParameter("@SalesVatPer", argCallPartsConsumption.SalesVatPer);
            param[25] = new SqlParameter("@SalesVatAmt", argCallPartsConsumption.SalesVatAmt);


            param[26] = new SqlParameter("@ClientCode", argCallPartsConsumption.ClientCode);
            param[27] = new SqlParameter("@CreatedBy", argCallPartsConsumption.CreatedBy);
            param[28] = new SqlParameter("@ModifiedBy", argCallPartsConsumption.ModifiedBy);

            param[29] = new SqlParameter("@Type", SqlDbType.Char);
            param[29].Size = 1;
            param[29].Direction = ParameterDirection.Output;

            param[30] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[30].Size = 255;
            param[30].Direction = ParameterDirection.Output;

            param[31] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[31].Size = 20;
            param[31].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallPartsConsumption", param);


            string strMessage = Convert.ToString(param[30].Value);
            string strType = Convert.ToString(param[29].Value);
            string strRetValue = Convert.ToString(param[31].Value);


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
        
        public ICollection<ErrorHandler> DeleteCallPartsConsumption(string argCallCode, int argCallItemNo, string argRepairProcDocCode, int argPCItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@CallCode", argCallCode);
                param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
                param[2] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
                param[3] = new SqlParameter("@PCItemNo", argPCItemNo);
                param[4] = new SqlParameter("@ClientCode", argClientCode);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;
                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;
                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteCallPartsConsumption", param);


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
        
        public bool blnIsCallPartsConsumptionExists(string argCallCode, int argCallItemNo, string argRepairProcDocCode, int argPCItemNo, string argClientCode)
        {
            bool IsCallPartsConsumptionExists = false;
            DataSet ds = new DataSet();
            ds = GetCallPartsConsumption(argCallCode, argCallItemNo, argRepairProcDocCode, argPCItemNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallPartsConsumptionExists = true;
            }
            else
            {
                IsCallPartsConsumptionExists = false;
            }
            return IsCallPartsConsumptionExists;
        }

        public bool blnIsCallPartsConsumptionExists(string argCallCode, int argCallItemNo, string argRepairProcDocCode, int argPCItemNo, string argClientCode, DataAccess da)
        {
            bool IsCallPartsConsumptionExists = false;
            DataSet ds = new DataSet();
            ds = GetCallPartsConsumption(argCallCode, argCallItemNo, argRepairProcDocCode, argPCItemNo, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallPartsConsumptionExists = true;
            }
            else
            {
                IsCallPartsConsumptionExists = false;
            }
            return IsCallPartsConsumptionExists;
        }

    }
}