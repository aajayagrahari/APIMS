
//Created On :: 15, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using RSMApp_MM;

namespace RSMApp_SD
{
    public class PurchaseOrderManager
    {
        const string PurchaseOrderTable = "PurchaseOrder";

        // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        PurchaseOrderDetailManager objPurchaseOrderDetailManager = new PurchaseOrderDetailManager();
        POPriceConditionManager objPOPriceConditionManager = new POPriceConditionManager();
        CharactersticsValueMasterManager objCharValueMasterManager = new CharactersticsValueMasterManager();

        public PurchaseOrder objGetPurchaseOrder(string argPODocCode, string argClientCode)
        {
            PurchaseOrder argPurchaseOrder = new PurchaseOrder();
            DataSet DataSetToFill = new DataSet();

            if (argPODocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPurchaseOrder(argPODocCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPurchaseOrder = this.objCreatePurchaseOrder((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPurchaseOrder;
        }


        public ICollection<PurchaseOrder> colGetPurchaseOrder(string argPOTypeCode, string argClientCode)
        {
            List<PurchaseOrder> lst = new List<PurchaseOrder>();
            DataSet DataSetToFill = new DataSet();
            PurchaseOrder tPurchaseOrder = new PurchaseOrder();

            DataSetToFill = this.GetPurchaseOrderList(argPOTypeCode, argClientCode, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"));

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePurchaseOrder(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetPurchaseOrder(string argPODocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PODocCode", argPODocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPurchaseOrder4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPurchaseOrder(string argPODocCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PODocCode", argPODocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPurchaseOrder4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPurchaseOrderList(string argPOTypeCode, string argClientCode, DateTime argStartDate, DateTime argEndDate)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@POTypeCode", argPOTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            param[2] = new SqlParameter("@StartDate", argStartDate);
            param[3] = new SqlParameter("@EndDate", argEndDate);

            DataSetToFill = da.FillDataSet("SP_GetPurchaseOrder", param);
            return DataSetToFill;
        }

        public DataSet GetPurchaseOrder4InBDC(string argClientCode, string argInBDeliveryDocTypeCode, string argToDocType)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@InBDeliveryDocTypeCode", argInBDeliveryDocTypeCode);
            param[1] = new SqlParameter("@ToDocType", argToDocType);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPurchaseOrder4InBDC", param);

            return DataSetToFill;
        }

        private PurchaseOrder objCreatePurchaseOrder(DataRow dr)
        {
            PurchaseOrder tPurchaseOrder = new PurchaseOrder();

            tPurchaseOrder.SetObjectInfo(dr);

            return tPurchaseOrder;

        }


        public ICollection<ErrorHandler> SavePurchaseOrder(PurchaseOrder argPurchaseOrder, ICollection<PurchaseOrderDetail> colGetPurchaseOrderDetail, ICollection<POPriceCondition> colPOPriceCondition)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            string strretValue = "";
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsPurchaseOrderExists(argPurchaseOrder.PODocCode, argPurchaseOrder.ClientCode, da) == false)
                {
                    strretValue = InsertPurchaseOrder(argPurchaseOrder, da, lstErr);
                }
                else
                {
                    strretValue = UpdatePurchaseOrder(argPurchaseOrder, da, lstErr);
                }

                foreach (ErrorHandler objerr in lstErr)
                {
                    if (objerr.Type == "E")
                    {
                        da.ROLLBACK_TRANSACTION();
                        return lstErr;
                    }

                    if (objerr.Type == "A")
                    {
                        da.ROLLBACK_TRANSACTION();
                        return lstErr;
                    }
                }

                if (strretValue != "")
                {
                    if (colGetPurchaseOrderDetail.Count > 0)
                    {
                        foreach (PurchaseOrderDetail argPurchaseOrderDetail in colGetPurchaseOrderDetail)
                        {
                            argPurchaseOrderDetail.PODocCode = Convert.ToString(strretValue);

                            if (argPurchaseOrderDetail.IsDeleted == 0)
                            {
                                objPurchaseOrderDetailManager.SavePurchaseOrderDetail(argPurchaseOrderDetail, da, lstErr);
                            }
                            else
                            {
                                objPurchaseOrderDetailManager.DeletePurchaseOrderDetail(argPurchaseOrderDetail.PODocCode, argPurchaseOrderDetail.ItemNo, argPurchaseOrderDetail.ClientCode, da, lstErr);
                            }

                            if (colPOPriceCondition.Count > 0)
                            {
                                foreach (POPriceCondition argPOPriceCon in colPOPriceCondition)
                                {
                                    if (argPOPriceCon.ItemNo == argPurchaseOrderDetail.ItemNo)
                                    {
                                        argPOPriceCon.PODocCode = Convert.ToString(strretValue);

                                        if (argPOPriceCon.IsDeleted == 0)
                                        {
                                            objPOPriceConditionManager.SavePOPriceCondition(argPOPriceCon, da, lstErr);
                                        }
                                        else
                                        {
                                            objPOPriceConditionManager.DeletePOPriceCondition(argPOPriceCon.PODocCode, argPOPriceCon.ItemNo, argPOPriceCon.ConditionType, argPOPriceCon.ClientCode, argPOPriceCon.IsDeleted);
                                        }
                                    }
                                }
                            }
                        }

                        foreach (ErrorHandler objerr in lstErr)
                        {
                            if (objerr.Type == "E")
                            {
                                da.ROLLBACK_TRANSACTION();
                                return lstErr;
                            }

                            if (objerr.Type == "A")
                            {
                                da.ROLLBACK_TRANSACTION();
                                return lstErr;
                            }
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

        public string InsertPurchaseOrder(PurchaseOrder argPurchaseOrder, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@PODocCode", argPurchaseOrder.PODocCode);
            param[1] = new SqlParameter("@POTypeCode", argPurchaseOrder.POTypeCode);
            param[2] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrder.PurchaseOrgCode);
            param[3] = new SqlParameter("@CompanyCode", argPurchaseOrder.CompanyCode);
            param[4] = new SqlParameter("@SourcePlantCode", argPurchaseOrder.SourcePlantCode);
            param[5] = new SqlParameter("@VendorCode", argPurchaseOrder.VendorCode);
            param[6] = new SqlParameter("@ReqDeliveryDate", argPurchaseOrder.ReqDeliveryDate);
            param[7] = new SqlParameter("@PriceDate", argPurchaseOrder.PriceDate);
            param[8] = new SqlParameter("@PODocumentDate", argPurchaseOrder.PODocumentDate);
            param[9] = new SqlParameter("@PostingDate", argPurchaseOrder.PostingDate);
            param[10] = new SqlParameter("@POStatus", argPurchaseOrder.POStatus);
            param[11] = new SqlParameter("@ClientCode", argPurchaseOrder.ClientCode);
            param[12] = new SqlParameter("@CreatedBy", argPurchaseOrder.CreatedBy);
            param[13] = new SqlParameter("@ModifiedBy", argPurchaseOrder.ModifiedBy);

            param[14] = new SqlParameter("@Type", SqlDbType.Char);
            param[14].Size = 1;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[15].Size = 255;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[16].Size = 20;
            param[16].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPurchaseOrder", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);
            return strRetValue;

        }


        public string UpdatePurchaseOrder(PurchaseOrder argPurchaseOrder, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[17];
            param[0] = new SqlParameter("@PODocCode", argPurchaseOrder.PODocCode);
            param[1] = new SqlParameter("@POTypeCode", argPurchaseOrder.POTypeCode);
            param[2] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrder.PurchaseOrgCode);
            param[3] = new SqlParameter("@CompanyCode", argPurchaseOrder.CompanyCode);
            param[4] = new SqlParameter("@SourcePlantCode", argPurchaseOrder.SourcePlantCode);
            param[5] = new SqlParameter("@VendorCode", argPurchaseOrder.VendorCode);
            param[6] = new SqlParameter("@ReqDeliveryDate", argPurchaseOrder.ReqDeliveryDate);
            param[7] = new SqlParameter("@PriceDate", argPurchaseOrder.PriceDate);
            param[8] = new SqlParameter("@PODocumentDate", argPurchaseOrder.PODocumentDate);
            param[9] = new SqlParameter("@PostingDate", argPurchaseOrder.PostingDate);
            param[10] = new SqlParameter("@POStatus", argPurchaseOrder.POStatus);
            param[11] = new SqlParameter("@ClientCode", argPurchaseOrder.ClientCode);
            param[12] = new SqlParameter("@CreatedBy", argPurchaseOrder.CreatedBy);
            param[13] = new SqlParameter("@ModifiedBy", argPurchaseOrder.ModifiedBy);

            param[14] = new SqlParameter("@Type", SqlDbType.Char);
            param[14].Size = 1;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[15].Size = 255;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[16].Size = 20;
            param[16].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdatePurchaseOrder", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);
            return strRetValue;

        }


        public ICollection<ErrorHandler> DeletePurchaseOrder(string argPODocCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@PODocCode", argPODocCode);
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

                int i = da.ExecuteNonQuery("Proc_DeletePurchaseOrder", param);


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

        public ICollection<ErrorHandler> DeletePurchaseOrder(string argPODocCode, string argClientCode, int iIsDeleted, DataAccess da)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@PODocCode", argPODocCode);
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

                int i = da.NExecuteNonQuery("Proc_DeletePurchaseOrder", param);


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

        public bool blnIsPurchaseOrderExists(string argPODocCode, string argClientCode)
        {
            bool IsPurchaseOrderExists = false;
            DataSet ds = new DataSet();
            ds = GetPurchaseOrder(argPODocCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPurchaseOrderExists = true;
            }
            else
            {
                IsPurchaseOrderExists = false;
            }
            return IsPurchaseOrderExists;
        }

        public bool blnIsPurchaseOrderExists(string argPODocCode, string argClientCode, DataAccess da)
        {
            bool IsPurchaseOrderExists = false;
            DataSet ds = new DataSet();
            ds = GetPurchaseOrder(argPODocCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPurchaseOrderExists = true;
            }
            else
            {
                IsPurchaseOrderExists = false;
            }
            return IsPurchaseOrderExists;
        }
    }
}