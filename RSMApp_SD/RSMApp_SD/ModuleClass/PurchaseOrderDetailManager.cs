
//Created On :: 15, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class PurchaseOrderDetailManager
    {
        const string PurchaseOrderDetailTable = "PurchaseOrderDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public PurchaseOrderDetail objGetPurchaseOrderDetail(string argPODocCode, string argItemNo, string argClientCode)
        {
            PurchaseOrderDetail argPurchaseOrderDetail = new PurchaseOrderDetail();
            DataSet DataSetToFill = new DataSet();

            if (argPODocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argItemNo.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPurchaseOrderDetail(argPODocCode, argItemNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPurchaseOrderDetail = this.objCreatePurchaseOrderDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPurchaseOrderDetail;
        }


        public ICollection<PurchaseOrderDetail> colGetPurchaseOrderDetail(string argPODocCode, string argClientCode)
        {
            List<PurchaseOrderDetail> lst = new List<PurchaseOrderDetail>();
            DataSet DataSetToFill = new DataSet();
            PurchaseOrderDetail tPurchaseOrderDetail = new PurchaseOrderDetail();

            DataSetToFill = this.GetPurchaseOrderDetail(argPODocCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePurchaseOrderDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<PurchaseOrderDetail> colGetPurchaseOrderDetail(string argPODocCode, string argClientCode, List<PurchaseOrderDetail> lst)
        {
            //List<SalesOrderDetail> lst = new List<SalesOrderDetail>();
            DataSet DataSetToFill = new DataSet();
            PurchaseOrderDetail tPurchaseOrderDetail = new PurchaseOrderDetail();

            DataSetToFill = this.GetPurchaseOrderDetail(argPODocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePurchaseOrderDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetPurchaseOrderDetail(string argInBDeliveryDocTypeCode, string argPODocCode, string argClientCode, int argItemNoFrom, int argItemNoTo)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@InBDeliveryDocTypeCode", argInBDeliveryDocTypeCode);
            param[1] = new SqlParameter("@PODocCode", argPODocCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);
            param[3] = new SqlParameter("@ItemNoFrom", argItemNoFrom);
            param[4] = new SqlParameter("@ItemNoTo", argItemNoTo);


            DataSetToFill = da.FillDataSet("SP_GetPurchaseOrderDetail4InBDC", param);
            return DataSetToFill;
        }


        public DataSet GetPurchaseOrderDetail(string argPODocCode, string argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PODocCode", argPODocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPurchaseOrderDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPurchaseOrderDetail(string argPODocCode, string argItemNo, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PODocCode", argPODocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPurchaseOrderDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPurchaseOrderDetail4IncInv(string argPODocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PODocCode", argPODocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetPurchaseOrderDetail4IncInv", param);
            return DataSetToFill;
        }


        public DataSet GetPurchaseOrderDetail(string argPODocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            
            param[0] = new SqlParameter("@PODocCode", argPODocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPurchaseOrderDetail",param);
            return DataSetToFill;
        }

        public DataSet GetPurchaseOrderDetailsItemNo(string argPODocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PODocCode", argPODocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetPurchaseOrderDetailItemNo", param);

            return DataSetToFill;
        }


        private PurchaseOrderDetail objCreatePurchaseOrderDetail(DataRow dr)
        {
            PurchaseOrderDetail tPurchaseOrderDetail = new PurchaseOrderDetail();

            tPurchaseOrderDetail.SetObjectInfo(dr);

            return tPurchaseOrderDetail;

        }


        public ICollection<ErrorHandler> SavePurchaseOrderDetail(PurchaseOrderDetail argPurchaseOrderDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPurchaseOrderDetailExists(argPurchaseOrderDetail.PODocCode, argPurchaseOrderDetail.ItemNo, argPurchaseOrderDetail.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPurchaseOrderDetail(argPurchaseOrderDetail, da, lstErr);
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
                    UpdatePurchaseOrderDetail(argPurchaseOrderDetail, da, lstErr);
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

        public void SavePurchaseOrderDetail(PurchaseOrderDetail argPurchaseOrderDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsPurchaseOrderDetailExists(argPurchaseOrderDetail.PODocCode, argPurchaseOrderDetail.ItemNo, argPurchaseOrderDetail.ClientCode, da) == false)
                {
                    InsertPurchaseOrderDetail(argPurchaseOrderDetail, da, lstErr);
                }
                else
                {
                    UpdatePurchaseOrderDetail(argPurchaseOrderDetail, da, lstErr);
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

        public void InsertPurchaseOrderDetail(PurchaseOrderDetail argPurchaseOrderDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[30];
            param[0] = new SqlParameter("@PODocCode", argPurchaseOrderDetail.PODocCode);
            param[1] = new SqlParameter("@ItemNo", argPurchaseOrderDetail.ItemNo);
            param[2] = new SqlParameter("@PlantCode", argPurchaseOrderDetail.PlantCode);
            param[3] = new SqlParameter("@StoreCode", argPurchaseOrderDetail.StoreCode);
            param[4] = new SqlParameter("@MaterialCode", argPurchaseOrderDetail.MaterialCode);
            param[5] = new SqlParameter("@Batch", argPurchaseOrderDetail.Batch);
            param[6] = new SqlParameter("@OrderQuantity", argPurchaseOrderDetail.OrderQuantity);
            param[7] = new SqlParameter("@ReceivedQuantity", argPurchaseOrderDetail.ReceivedQuantity);
            param[8] = new SqlParameter("@PriceDate", argPurchaseOrderDetail.PriceDate);
            param[9] = new SqlParameter("@ValClassType", argPurchaseOrderDetail.ValClassType);
            param[10] = new SqlParameter("@CurrencyCode", argPurchaseOrderDetail.CurrencyCode);
            param[11] = new SqlParameter("@UOMCode", argPurchaseOrderDetail.UOMCode);
            param[12] = new SqlParameter("@NetWeight", argPurchaseOrderDetail.NetWeight);
            param[13] = new SqlParameter("@GrossWeight", argPurchaseOrderDetail.GrossWeight);
            param[14] = new SqlParameter("@ItemCategoryCode", argPurchaseOrderDetail.ItemCategoryCode);
            param[15] = new SqlParameter("@HighLevelItem", argPurchaseOrderDetail.HighLevelItem);
            param[16] = new SqlParameter("@ProfitCenter", argPurchaseOrderDetail.ProfitCenter);
            param[17] = new SqlParameter("@ReceiptBlock", argPurchaseOrderDetail.ReceiptBlock);
            param[18] = new SqlParameter("@PurchaseBlock", argPurchaseOrderDetail.PurchaseBlock);
            param[19] = new SqlParameter("@NetValue", argPurchaseOrderDetail.NetValue);
            param[20] = new SqlParameter("@TaxAmt", argPurchaseOrderDetail.TaxAmt);
            param[21] = new SqlParameter("@NetPricePerQty", argPurchaseOrderDetail.NetPricePerQty);
            param[22] = new SqlParameter("@POStatus", argPurchaseOrderDetail.POStatus);
            param[23] = new SqlParameter("@PurchaseQty", argPurchaseOrderDetail.PurchaseQty);
            param[24] = new SqlParameter("@ClientCode", argPurchaseOrderDetail.ClientCode);
            param[25] = new SqlParameter("@CreatedBy", argPurchaseOrderDetail.CreatedBy);
            param[26] = new SqlParameter("@ModifiedBy", argPurchaseOrderDetail.ModifiedBy);

            param[27] = new SqlParameter("@Type", SqlDbType.Char);
            param[27].Size = 1;
            param[27].Direction = ParameterDirection.Output;

            param[28] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[28].Size = 255;
            param[28].Direction = ParameterDirection.Output;

            param[29] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[29].Size = 20;
            param[29].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertPurchaseOrderDetail", param);


            string strMessage = Convert.ToString(param[28].Value);
            string strType = Convert.ToString(param[27].Value);
            string strRetValue = Convert.ToString(param[29].Value);


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


        public void UpdatePurchaseOrderDetail(PurchaseOrderDetail argPurchaseOrderDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[30];
            param[0] = new SqlParameter("@PODocCode", argPurchaseOrderDetail.PODocCode);
            param[1] = new SqlParameter("@ItemNo", argPurchaseOrderDetail.ItemNo);
            param[2] = new SqlParameter("@PlantCode", argPurchaseOrderDetail.PlantCode);
            param[3] = new SqlParameter("@StoreCode", argPurchaseOrderDetail.StoreCode);
            param[4] = new SqlParameter("@MaterialCode", argPurchaseOrderDetail.MaterialCode);
            param[5] = new SqlParameter("@Batch", argPurchaseOrderDetail.Batch);
            param[6] = new SqlParameter("@OrderQuantity", argPurchaseOrderDetail.OrderQuantity);
            param[7] = new SqlParameter("@ReceivedQuantity", argPurchaseOrderDetail.ReceivedQuantity);
            param[8] = new SqlParameter("@PriceDate", argPurchaseOrderDetail.PriceDate);
            param[9] = new SqlParameter("@ValClassType", argPurchaseOrderDetail.ValClassType);
            param[10] = new SqlParameter("@CurrencyCode", argPurchaseOrderDetail.CurrencyCode);
            param[11] = new SqlParameter("@UOMCode", argPurchaseOrderDetail.UOMCode);
            param[12] = new SqlParameter("@NetWeight", argPurchaseOrderDetail.NetWeight);
            param[13] = new SqlParameter("@GrossWeight", argPurchaseOrderDetail.GrossWeight);
            param[14] = new SqlParameter("@ItemCategoryCode", argPurchaseOrderDetail.ItemCategoryCode);
            param[15] = new SqlParameter("@HighLevelItem", argPurchaseOrderDetail.HighLevelItem);
            param[16] = new SqlParameter("@ProfitCenter", argPurchaseOrderDetail.ProfitCenter);
            param[17] = new SqlParameter("@ReceiptBlock", argPurchaseOrderDetail.ReceiptBlock);
            param[18] = new SqlParameter("@PurchaseBlock", argPurchaseOrderDetail.PurchaseBlock);
            param[19] = new SqlParameter("@NetValue", argPurchaseOrderDetail.NetValue);
            param[20] = new SqlParameter("@TaxAmt", argPurchaseOrderDetail.TaxAmt);
            param[21] = new SqlParameter("@NetPricePerQty", argPurchaseOrderDetail.NetPricePerQty);
            param[22] = new SqlParameter("@POStatus", argPurchaseOrderDetail.POStatus);
            param[23] = new SqlParameter("@PurchaseQty", argPurchaseOrderDetail.PurchaseQty);
            param[24] = new SqlParameter("@ClientCode", argPurchaseOrderDetail.ClientCode);
            param[25] = new SqlParameter("@CreatedBy", argPurchaseOrderDetail.CreatedBy);
            param[26] = new SqlParameter("@ModifiedBy", argPurchaseOrderDetail.ModifiedBy);

            param[27] = new SqlParameter("@Type", SqlDbType.Char);
            param[27].Size = 1;
            param[27].Direction = ParameterDirection.Output;

            param[28] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[28].Size = 255;
            param[28].Direction = ParameterDirection.Output;

            param[29] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[29].Size = 20;
            param[29].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePurchaseOrderDetail", param);


            string strMessage = Convert.ToString(param[28].Value);
            string strType = Convert.ToString(param[27].Value);
            string strRetValue = Convert.ToString(param[29].Value);


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


        public ICollection<ErrorHandler> DeletePurchaseOrderDetail(string argPODocCode, string argItemNo, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@PODocCode", argPODocCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
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

                int i = da.ExecuteNonQuery("Proc_DeletePurchaseOrderDetail", param);


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

        public ICollection<ErrorHandler> DeletePurchaseOrderDetail(string argPODocCode, string argItemNo, string argClientCode, DataAccess da, List<ErrorHandler> lstErr)
        {
           try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@PODocCode", argPODocCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
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

                int i = da.NExecuteNonQuery("Proc_DeletePurchaseOrderDetail", param);


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

        public bool blnIsPurchaseOrderDetailExists(string argPODocCode, string argItemNo, string argClientCode)
        {
            bool IsPurchaseOrderDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetPurchaseOrderDetail(argPODocCode, argItemNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPurchaseOrderDetailExists = true;
            }
            else
            {
                IsPurchaseOrderDetailExists = false;
            }
            return IsPurchaseOrderDetailExists;
        }
        public bool blnIsPurchaseOrderDetailExists(string argPODocCode, string argItemNo, string argClientCode,DataAccess da)
        {
            bool IsPurchaseOrderDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetPurchaseOrderDetail(argPODocCode, argItemNo, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPurchaseOrderDetailExists = true;
            }
            else
            {
                IsPurchaseOrderDetailExists = false;
            }
            return IsPurchaseOrderDetailExists;
        }
    }
}