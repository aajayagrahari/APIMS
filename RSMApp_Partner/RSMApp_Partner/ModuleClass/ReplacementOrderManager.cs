
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
    public class ReplacementOrderManager
    {
        const string ReplacementOrderTable = "ReplacementOrder";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        ReplacementOrderDetailManager objReplacementOrderDetailMan = new ReplacementOrderDetailManager();
        ReplacementOrderSerialDetailManager objReplacementOrderSerialDetailManager = new ReplacementOrderSerialDetailManager();

        public ReplacementOrder objGetReplacementOrder(string argRepOrderCode, string argClientCode)
        {
            ReplacementOrder argReplacementOrder = new ReplacementOrder();
            DataSet DataSetToFill = new DataSet();

            if (argRepOrderCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetReplacementOrder(argRepOrderCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argReplacementOrder = this.objCreateReplacementOrder((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argReplacementOrder;
        }
        
        public ICollection<ReplacementOrder> colGetReplacementOrder(string argClientCode)
        {
            List<ReplacementOrder> lst = new List<ReplacementOrder>();
            DataSet DataSetToFill = new DataSet();
            ReplacementOrder tReplacementOrder = new ReplacementOrder();

            DataSetToFill = this.GetReplacementOrder(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateReplacementOrder(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetReplacementOrder(string argRepOrderCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RepOrderCode", argRepOrderCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetReplacementOrder4ID", param);

            return DataSetToFill;
        }

        public DataSet GetReplacementOrder(string argRepOrderCode, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RepOrderCode", argRepOrderCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetReplacementOrder4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetReplacementOrder(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetReplacementOrder", param);
            return DataSetToFill;
        }

        public DataSet GetRepOrder4Issue(string argToPartnerCode, string argPartnerCode, string argOrderType, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ToPartnerCode", argToPartnerCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@OrderType", argOrderType);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepOrder4Issue", param);
            return DataSetToFill;
        }
        
        private ReplacementOrder objCreateReplacementOrder(DataRow dr)
        {
            ReplacementOrder tReplacementOrder = new ReplacementOrder();

            tReplacementOrder.SetObjectInfo(dr);

            return tReplacementOrder;

        }
        
        public ICollection<ErrorHandler> SaveReplacementOrder(ReplacementOrder argReplacementOrder)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsReplacementOrderExists(argReplacementOrder.RepOrderCode, argReplacementOrder.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertReplacementOrder(argReplacementOrder, da, lstErr);
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
                    UpdateReplacementOrder(argReplacementOrder, da, lstErr);
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

        public void SaveReplacementOrder(ReplacementOrder argReplacementOrder, ReplacementOrderDetailCol argReplacementOrderDetailCol, ReplacementOrderSerialDetailCol argReplacementOrderSerialDetailCol, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                string strRetValue = "";
                if (blnIsReplacementOrderExists(argReplacementOrder.RepOrderCode, argReplacementOrder.ClientCode, da) == false)
                {
                    strRetValue = InsertReplacementOrder(argReplacementOrder, da, lstErr);

                }
                else
                {
                    strRetValue = UpdateReplacementOrder(argReplacementOrder, da, lstErr);
                }

                if (strRetValue != "")
                {
                    if (argReplacementOrderDetailCol.colReplacementOrderDetail.Count > 0)
                    {
                        foreach (ReplacementOrderDetail objReplacementOrderDetail in argReplacementOrderDetailCol.colReplacementOrderDetail)
                        {
                            objReplacementOrderDetail.RepOrderCode = strRetValue;

                            if (objReplacementOrderDetail.IsDeleted == 0)
                            {
                                objReplacementOrderDetailMan.SaveReplacementOrderDetail(objReplacementOrderDetail, da, lstErr);

                            }
                            else
                            {
                                /*******************/
                                /** Call Delete Function **/
                            }

                            if (argReplacementOrderSerialDetailCol.colReplacementOrderSerialDetail.Count > 0)
                            {
                                foreach (ReplacementOrderSerialDetail objReplacementOrderSerialDetail in argReplacementOrderSerialDetailCol.colReplacementOrderSerialDetail)
                                {
                                    if (objReplacementOrderSerialDetail.RepOrderItemNo == objReplacementOrderDetail.RepOrderItemNo)
                                    {
                                        objReplacementOrderSerialDetail.RepOrderCode = strRetValue;
                                        if (objReplacementOrderSerialDetail.IsDeleted == 0)
                                        {
                                            objReplacementOrderSerialDetailManager.SaveReplacementOrderSerialDetail(objReplacementOrderSerialDetail, da, lstErr);
                                        }
                                        else
                                        {

                                        }
                                    }
                                }
                            }

                        }
                    }
                }

            }
            catch (Exception ex)
            {
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
            
        }

        public string InsertReplacementOrder(ReplacementOrder argReplacementOrder, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@RepOrderCode", argReplacementOrder.RepOrderCode);
            param[1] = new SqlParameter("@RepOrderDocTypeCode", argReplacementOrder.RepOrderDocTypeCode);
            param[2] = new SqlParameter("@RepOrderDate", argReplacementOrder.RepOrderDate);
            param[3] = new SqlParameter("@TotalQuantity", argReplacementOrder.TotalQuantity);
            param[4] = new SqlParameter("@RepOrderStatus", argReplacementOrder.RepOrderStatus);
            param[5] = new SqlParameter("@IssueDocCode", argReplacementOrder.IssueDocCode);
            param[6] = new SqlParameter("@PartnerCode", argReplacementOrder.PartnerCode);
            param[7] = new SqlParameter("@ToPartnerCode", argReplacementOrder.ToPartnerCode);
            param[8] = new SqlParameter("@OrderType", argReplacementOrder.OrderType);
            param[9] = new SqlParameter("@ClientCode", argReplacementOrder.ClientCode);
            param[10] = new SqlParameter("@CreatedBy", argReplacementOrder.CreatedBy);
            param[11] = new SqlParameter("@ModifiedBy", argReplacementOrder.ModifiedBy);
            
            param[12] = new SqlParameter("@Type", SqlDbType.Char);
            param[12].Size = 1;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[13].Size = 255;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[14].Size = 20;
            param[14].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertReplacementOrder", param);


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
            lstErr.Add(objErrorHandler);

            return strRetValue;

        }
        
        public string UpdateReplacementOrder(ReplacementOrder argReplacementOrder, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@RepOrderCode", argReplacementOrder.RepOrderCode);
            param[1] = new SqlParameter("@RepOrderDocTypeCode", argReplacementOrder.RepOrderDocTypeCode);
            param[2] = new SqlParameter("@RepOrderDate", argReplacementOrder.RepOrderDate);
            param[3] = new SqlParameter("@TotalQuantity", argReplacementOrder.TotalQuantity);
            param[4] = new SqlParameter("@RepOrderStatus", argReplacementOrder.RepOrderStatus);
            param[5] = new SqlParameter("@IssueDocCode", argReplacementOrder.IssueDocCode);
            param[6] = new SqlParameter("@PartnerCode", argReplacementOrder.PartnerCode);
            param[7] = new SqlParameter("@ToPartnerCode", argReplacementOrder.ToPartnerCode);
            param[8] = new SqlParameter("@OrderType", argReplacementOrder.OrderType);
            param[9] = new SqlParameter("@ClientCode", argReplacementOrder.ClientCode);
            param[10] = new SqlParameter("@CreatedBy", argReplacementOrder.CreatedBy);
            param[11] = new SqlParameter("@ModifiedBy", argReplacementOrder.ModifiedBy);

            param[12] = new SqlParameter("@Type", SqlDbType.Char);
            param[12].Size = 1;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[13].Size = 255;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[14].Size = 20;
            param[14].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateReplacementOrder", param);


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
            lstErr.Add(objErrorHandler);

            return strRetValue;
        }
        
        public ICollection<ErrorHandler> DeleteReplacementOrder(string argRepOrderCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@RepOrderCode", argRepOrderCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteReplacementOrder", param);


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
        
        public bool blnIsReplacementOrderExists(string argRepOrderCode, string argClientCode)
        {
            bool IsReplacementOrderExists = false;
            DataSet ds = new DataSet();
            ds = GetReplacementOrder(argRepOrderCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsReplacementOrderExists = true;
            }
            else
            {
                IsReplacementOrderExists = false;
            }
            return IsReplacementOrderExists;
        }

        public bool blnIsReplacementOrderExists(string argRepOrderCode, string argClientCode, DataAccess da)
        {
            bool IsReplacementOrderExists = false;
            DataSet ds = new DataSet();
            ds = GetReplacementOrder(argRepOrderCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsReplacementOrderExists = true;
            }
            else
            {
                IsReplacementOrderExists = false;
            }
            return IsReplacementOrderExists;
        }

        public string GenerateRepOrderCode(string argRepOrderCode, string argRepOrderDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@RepOrderCode", argRepOrderCode);
            param[1] = new SqlParameter("@RepOrderDocTypeCode", argRepOrderDocTypeCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            param[4] = new SqlParameter("@GeneratedRepOrderCode", SqlDbType.VarChar);
            param[4].Size = 18;
            param[4].Direction = ParameterDirection.Output;

            int i = da.ExecuteNonQuery("SL_GetNewRepOrderCode", param);

            string strMessage = Convert.ToString(param[4].Value);

            return strMessage;

        }

        //----------------------------------Report Section-----------------------------------------------------------------//
        public DataSet GetReplacementOrder4Report(string argRepOrderDocTypeCode, string argFromPartnerCode, string argToPartnerCode, string argRepOrderStatus, string argFromDate, string argDateTo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@RepOrderDocTypeCode", argRepOrderDocTypeCode);
            param[1] = new SqlParameter("@FromPartnerCode", argFromPartnerCode);
            param[2] = new SqlParameter("@ToPartnerCode", argToPartnerCode);
            param[3] = new SqlParameter("@RepOrderStatus", argRepOrderStatus);
            param[4] = new SqlParameter("@FromDate", argFromDate);
            param[5] = new SqlParameter("@DateTo", argDateTo);
            param[6] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetReplacementOrder4Report", param);
            return DataSetToFill;
        }

        public DataSet GetReplacementOrderReceived4Report(string argRepOrderDocTypeCode, string argFromPartnerCode, string argToPartnerCode, string argRepOrderStatus, string argFromDate, string argDateTo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@RepOrderDocTypeCode", argRepOrderDocTypeCode);
            param[1] = new SqlParameter("@FromPartnerCode", argFromPartnerCode);
            param[2] = new SqlParameter("@ToPartnerCode", argToPartnerCode);
            param[3] = new SqlParameter("@RepOrderStatus", argRepOrderStatus);
            param[4] = new SqlParameter("@FromDate", argFromDate);
            param[5] = new SqlParameter("@DateTo", argDateTo);
            param[6] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetReplacementOrderRec4Report", param);
            return DataSetToFill;
        }
        // ---------------------------------END Report---------------------------------------------------------------------//

    }
}