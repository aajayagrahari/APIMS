
//Created On :: 28, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallPartsOrderManager
    {
        const string CallPartsOrderTable = "CallPartsOrder";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public CallPartsOrder objGetCallPartsOrder(string argCallCode, int argCallItemNo, string argRepairProcDocCode, int argPOItemNo, string argClientCode)
        {
            CallPartsOrder argCallPartsOrder = new CallPartsOrder();
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

            if (argPOItemNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallPartsOrder(argCallCode, argCallItemNo, argRepairProcDocCode, argPOItemNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallPartsOrder = this.objCreateCallPartsOrder((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallPartsOrder;
        }

        public ICollection<CallPartsOrder> colGetCallPartsOrder(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode)
        {
            List<CallPartsOrder> lst = new List<CallPartsOrder>();
            DataSet DataSetToFill = new DataSet();
            CallPartsOrder tCallPartsOrder = new CallPartsOrder();

            DataSetToFill = this.GetCallPartsOrder(argCallCode, argCallItemNo, argRepairProcDocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallPartsOrder(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public void colGetCallPartsOrder(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode, ref CallPartsOrderCol argCallPartsOrderCol)
        {
            DataSet DataSetToFill = new DataSet();
            CallPartsOrder tCallPartsOrder = new CallPartsOrder();

            DataSetToFill = this.GetCallPartsOrder(argCallCode, argCallItemNo, argRepairProcDocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argCallPartsOrderCol.colCallRepairProcess.Add(objCreateCallPartsOrder(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


        }

        public DataSet GetCallPartsOrder(string argCallCode, int argCallItemNo, string argRepairProcDocCode, int argPOItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
            param[3] = new SqlParameter("@POItemNo", argPOItemNo);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallPartsOrder4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallPartsOrder(string argCallCode, int argCallItemNo, string argRepairProcDocCode, int argPOItemNo, string argClientCode, DataAccess da)
        {
           //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
            param[3] = new SqlParameter("@POItemNo", argPOItemNo);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallPartsOrder4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCallPartsOrder(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallPartsOrder", param);
            return DataSetToFill;
        }
        
        private CallPartsOrder objCreateCallPartsOrder(DataRow dr)
        {
            CallPartsOrder tCallPartsOrder = new CallPartsOrder();

            tCallPartsOrder.SetObjectInfo(dr);

            return tCallPartsOrder;

        }
        
        public ICollection<ErrorHandler> SaveCallPartsOrder(CallPartsOrder argCallPartsOrder)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallPartsOrderExists(argCallPartsOrder.CallCode, argCallPartsOrder.CallItemNo, argCallPartsOrder.RepairProcDocCode, argCallPartsOrder.POItemNo, argCallPartsOrder.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallPartsOrder(argCallPartsOrder, da, lstErr);
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
                    UpdateCallPartsOrder(argCallPartsOrder, da, lstErr);
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

        public void SaveCallPartsOrder(CallPartsOrder argCallPartsOrder, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCallPartsOrderExists(argCallPartsOrder.CallCode, argCallPartsOrder.CallItemNo, argCallPartsOrder.RepairProcDocCode, argCallPartsOrder.POItemNo, argCallPartsOrder.ClientCode, da) == false)
                {
                    InsertCallPartsOrder(argCallPartsOrder, da, lstErr);
                }
                else
                {
                    UpdateCallPartsOrder(argCallPartsOrder, da, lstErr);
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

        public void InsertCallPartsOrder(CallPartsOrder argCallPartsOrder, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];

            param[0] = new SqlParameter("@CallCode", argCallPartsOrder.CallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallPartsOrder.CallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argCallPartsOrder.RepairProcDocCode);
            param[3] = new SqlParameter("@POItemNo", argCallPartsOrder.POItemNo);
            param[4] = new SqlParameter("@MaterialCode", argCallPartsOrder.MaterialCode);
            param[5] = new SqlParameter("@MatGroup1Code", argCallPartsOrder.MatGroup1Code);
            param[6] = new SqlParameter("@PartnerCode", argCallPartsOrder.PartnerCode);
            param[7] = new SqlParameter("@PartnerEmployeeCode", argCallPartsOrder.PartnerEmployeeCode);
            param[8] = new SqlParameter("@OrderQty", argCallPartsOrder.OrderQty);
            param[9] = new SqlParameter("@UOMCode", argCallPartsOrder.UOMCode);
            param[10] = new SqlParameter("@ReceivedQty", argCallPartsOrder.ReceivedQty);
            param[11] = new SqlParameter("@IsOrdered", argCallPartsOrder.IsOrdered);
            param[12] = new SqlParameter("@OrderStatus", argCallPartsOrder.OrderStatus);
            param[13] = new SqlParameter("@ClientCode", argCallPartsOrder.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argCallPartsOrder.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argCallPartsOrder.ModifiedBy);
            
            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallPartsOrder", param);


            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);


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
        
        public void UpdateCallPartsOrder(CallPartsOrder argCallPartsOrder, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@CallCode", argCallPartsOrder.CallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallPartsOrder.CallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argCallPartsOrder.RepairProcDocCode);
            param[3] = new SqlParameter("@POItemNo", argCallPartsOrder.POItemNo);
            param[4] = new SqlParameter("@MaterialCode", argCallPartsOrder.MaterialCode);
            param[5] = new SqlParameter("@MatGroup1Code", argCallPartsOrder.MatGroup1Code);
            param[6] = new SqlParameter("@PartnerCode", argCallPartsOrder.PartnerCode);
            param[7] = new SqlParameter("@PartnerEmployeeCode", argCallPartsOrder.PartnerEmployeeCode);
            param[8] = new SqlParameter("@OrderQty", argCallPartsOrder.OrderQty);
            param[9] = new SqlParameter("@UOMCode", argCallPartsOrder.UOMCode);
            param[10] = new SqlParameter("@ReceivedQty", argCallPartsOrder.ReceivedQty);
            param[11] = new SqlParameter("@IsOrdered", argCallPartsOrder.IsOrdered);
            param[12] = new SqlParameter("@OrderStatus", argCallPartsOrder.OrderStatus);
            param[13] = new SqlParameter("@ClientCode", argCallPartsOrder.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argCallPartsOrder.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argCallPartsOrder.ModifiedBy);

            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallPartsOrder", param);


            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);


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
        
        public ICollection<ErrorHandler> DeleteCallPartsOrder(string argCallCode, int argCallItemNo, string argRepairProcDocCode, int argPOItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@CallCode", argCallCode);
                param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
                param[2] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
                param[3] = new SqlParameter("@POItemNo", argPOItemNo);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCallPartsOrder", param);


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
        
        public bool blnIsCallPartsOrderExists(string argCallCode, int argCallItemNo, string argRepairProcDocCode, int argPOItemNo, string argClientCode)
        {
            bool IsCallPartsOrderExists = false;
            DataSet ds = new DataSet();
            ds = GetCallPartsOrder(argCallCode, argCallItemNo, argRepairProcDocCode, argPOItemNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallPartsOrderExists = true;
            }
            else
            {
                IsCallPartsOrderExists = false;
            }
            return IsCallPartsOrderExists;
        }

        public bool blnIsCallPartsOrderExists(string argCallCode, int argCallItemNo, string argRepairProcDocCode, int argPOItemNo, string argClientCode, DataAccess da)
        {
            bool IsCallPartsOrderExists = false;
            DataSet ds = new DataSet();
            ds = GetCallPartsOrder(argCallCode, argCallItemNo, argRepairProcDocCode, argPOItemNo, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallPartsOrderExists = true;
            }
            else
            {
                IsCallPartsOrderExists = false;
            }
            return IsCallPartsOrderExists;
        }
    }
}