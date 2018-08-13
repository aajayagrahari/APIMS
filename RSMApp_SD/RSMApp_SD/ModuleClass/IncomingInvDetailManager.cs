
//Created On :: 26, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class IncomingInvDetailManager
    {
        const string IncomingInvDetailTable = "IncomingInvDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public IncomingInvDetail objGetIncomingInvDetail(string argIncomingInvDocCode, int argItemNo, string argClientCode)
        {
            IncomingInvDetail argIncomingInvDetail = new IncomingInvDetail();
            DataSet DataSetToFill = new DataSet();

            if (argIncomingInvDocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argItemNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetIncomingInvDetail(argIncomingInvDocCode, argItemNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argIncomingInvDetail = this.objCreateIncomingInvDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argIncomingInvDetail;
        }

        public ICollection<IncomingInvDetail> colGetIncomingInvDetail(string argIncomingInvDocCode, string argClientCode, List<IncomingInvDetail> lst)
        {
            DataSet DataSetToFill = new DataSet();
            IncomingInvDetail tIncomingInvDetail = new IncomingInvDetail();

            DataSetToFill = this.GetIncomingInvDetail(argIncomingInvDocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateIncomingInvDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<IncomingInvDetail> colGetIncomingInvDetail(string argItemNo, string argClientCode)
        {
            List<IncomingInvDetail> lst = new List<IncomingInvDetail>();
            DataSet DataSetToFill = new DataSet();
            IncomingInvDetail tIncomingInvDetail = new IncomingInvDetail();

            DataSetToFill = this.GetIncomingInvDetail(argItemNo,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateIncomingInvDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<IncomingInvDetail> colGetIncomingInvDetail4PO(string argPODocCode, string argClientCode, List<IncomingInvDetail> lst)
        {
            DataSet DataSetToFill = new DataSet();
            PurchaseOrderDetailManager objPurchaseOrderDetailManager = new PurchaseOrderDetailManager();
            DataSetToFill = objPurchaseOrderDetailManager.GetPurchaseOrderDetail4IncInv(argPODocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateIncomingInvDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<IncomingInvDetail> colGetIncomingInvDetail4InBDC(string argInBDeliveryDocCode, string argClientCode, List<IncomingInvDetail> lst)
        {
            DataSet DataSetToFill = new DataSet();
            InBDeliveryDetail tInBDeliveryDetail = new InBDeliveryDetail();
            InBDeliveryDetailManager obInBDeliveryDetailManager = new InBDeliveryDetailManager();
            DataSetToFill = obInBDeliveryDetailManager.GetInBDeliveryDetail4IncInv(argInBDeliveryDocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateIncomingInvDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetIncomingInvDetail(string argIncomingInvDocCode, int argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@IncomingInvDocCode", argIncomingInvDocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetIncomingInvDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetIncomingInvDetail(string argIncomingInvDocCode, int argItemNo, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@IncomingInvDocCode", argIncomingInvDocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetIncomingInvDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetInBDeliveryDetail4IncInv(string argInBDeliveryDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);


            DataSetToFill = da.FillDataSet("SP_GetInBDeliveryDetail4IncInv", param);
            return DataSetToFill;
        }

        public DataSet GetIncomingInvDetail(string argIncomingInvDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@IncomingInvDocCode", argIncomingInvDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetIncomingInvDetail",param);
            return DataSetToFill;
        }

        private IncomingInvDetail objCreateIncomingInvDetail(DataRow dr)
        {
            IncomingInvDetail tIncomingInvDetail = new IncomingInvDetail();

            tIncomingInvDetail.SetObjectInfo(dr);

            return tIncomingInvDetail;

        }

        public void SaveIncomingInvDetail(IncomingInvDetail argIncomingInvDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsIncomingInvDetailExists(argIncomingInvDetail.IncomingInvDocCode, argIncomingInvDetail.ItemNo, argIncomingInvDetail.ClientCode,da) == false)
                {
                    InsertIncomingInvDetail(argIncomingInvDetail, da, lstErr);
                }
                else
                {
                    UpdateIncomingInvDetail(argIncomingInvDetail, da, lstErr);
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

        public void InsertIncomingInvDetail(IncomingInvDetail argIncomingInvDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[39];
            param[0] = new SqlParameter("@IncomingInvDocCode", argIncomingInvDetail.IncomingInvDocCode);
            param[1] = new SqlParameter("@ItemNo", argIncomingInvDetail.ItemNo);
            param[2] = new SqlParameter("@RefDocumentCode", argIncomingInvDetail.RefDocumentCode);
            param[3] = new SqlParameter("@RefItemNo", argIncomingInvDetail.RefItemNo);
            param[4] = new SqlParameter("@MaterialCode", argIncomingInvDetail.MaterialCode);
            param[5] = new SqlParameter("@PlantCode", argIncomingInvDetail.PlantCode);
            param[6] = new SqlParameter("@StoreCode", argIncomingInvDetail.StoreCode);
            param[7] = new SqlParameter("@PurchaseOrgCode", argIncomingInvDetail.PurchaseOrgCode);
            param[8] = new SqlParameter("@CompanyCode", argIncomingInvDetail.CompanyCode);
            param[9] = new SqlParameter("@ValTypeCode", argIncomingInvDetail.ValTypeCode);
            param[10] = new SqlParameter("@ProfitCenter", argIncomingInvDetail.ProfitCenter);
            param[11] = new SqlParameter("@MatVolume", argIncomingInvDetail.MatVolume);
            param[12] = new SqlParameter("@VolumeUOM", argIncomingInvDetail.VolumeUOM);
            param[13] = new SqlParameter("@WeightUOM", argIncomingInvDetail.WeightUOM);
            param[14] = new SqlParameter("@GrossWeight", argIncomingInvDetail.GrossWeight);
            param[15] = new SqlParameter("@NetWeight", argIncomingInvDetail.NetWeight);
            param[16] = new SqlParameter("@PriceDate", argIncomingInvDetail.PriceDate);
            param[17] = new SqlParameter("@Quantity", argIncomingInvDetail.Quantity);
            param[18] = new SqlParameter("@PurchaseQty", argIncomingInvDetail.PurchaseQty);
            param[19] = new SqlParameter("@NetValue", argIncomingInvDetail.NetValue);
            param[20] = new SqlParameter("@TaxAmt", argIncomingInvDetail.TaxAmt);
            param[21] = new SqlParameter("@NetPricePerQty", argIncomingInvDetail.NetPricePerQty);
            param[22] = new SqlParameter("@ItemCategoryCode", argIncomingInvDetail.ItemCategoryCode);
            param[23] = new SqlParameter("@UOMCode", argIncomingInvDetail.UOMCode);
            param[24] = new SqlParameter("@PurchaseUOMCode", argIncomingInvDetail.PurchaseUOMCode);
            param[25] = new SqlParameter("@COACode", argIncomingInvDetail.COACode);
            param[26] = new SqlParameter("@OrginatingDoc", argIncomingInvDetail.OrginatingDoc);
            param[27] = new SqlParameter("@OrginatingItem", argIncomingInvDetail.OrginatingItem);
            param[28] = new SqlParameter("@PODoc", argIncomingInvDetail.PODoc);
            param[29] = new SqlParameter("@POItem", argIncomingInvDetail.POItem);
            param[30] = new SqlParameter("@MatGroup1Code", argIncomingInvDetail.MatGroup1Code);
            param[31] = new SqlParameter("@ServiceRenderDate", argIncomingInvDetail.ServiceRenderDate);
            param[32] = new SqlParameter("@CostCenterCode", argIncomingInvDetail.CostCenterCode);
            param[33] = new SqlParameter("@ClientCode", argIncomingInvDetail.ClientCode);
            param[34] = new SqlParameter("@CreatedBy", argIncomingInvDetail.CreatedBy);
            param[35] = new SqlParameter("@ModifiedBy", argIncomingInvDetail.ModifiedBy);

            param[36] = new SqlParameter("@Type", SqlDbType.Char);
            param[36].Size = 1;
            param[36].Direction = ParameterDirection.Output;

            param[37] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[37].Size = 255;
            param[37].Direction = ParameterDirection.Output;

            param[38] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[38].Size = 20;
            param[38].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertIncomingInvDetail", param);


            string strMessage = Convert.ToString(param[37].Value);
            string strType = Convert.ToString(param[36].Value);
            string strRetValue = Convert.ToString(param[38].Value);


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

        public void UpdateIncomingInvDetail(IncomingInvDetail argIncomingInvDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[39];
            param[0] = new SqlParameter("@IncomingInvDocCode", argIncomingInvDetail.IncomingInvDocCode);
            param[1] = new SqlParameter("@ItemNo", argIncomingInvDetail.ItemNo);
            param[2] = new SqlParameter("@RefDocumentCode", argIncomingInvDetail.RefDocumentCode);
            param[3] = new SqlParameter("@RefItemNo", argIncomingInvDetail.RefItemNo);
            param[4] = new SqlParameter("@MaterialCode", argIncomingInvDetail.MaterialCode);
            param[5] = new SqlParameter("@PlantCode", argIncomingInvDetail.PlantCode);
            param[6] = new SqlParameter("@StoreCode", argIncomingInvDetail.StoreCode);
            param[7] = new SqlParameter("@PurchaseOrgCode", argIncomingInvDetail.PurchaseOrgCode);
            param[8] = new SqlParameter("@CompanyCode", argIncomingInvDetail.CompanyCode);
            param[9] = new SqlParameter("@ValTypeCode", argIncomingInvDetail.ValTypeCode);
            param[10] = new SqlParameter("@ProfitCenter", argIncomingInvDetail.ProfitCenter);
            param[11] = new SqlParameter("@MatVolume", argIncomingInvDetail.MatVolume);
            param[12] = new SqlParameter("@VolumeUOM", argIncomingInvDetail.VolumeUOM);
            param[13] = new SqlParameter("@WeightUOM", argIncomingInvDetail.WeightUOM);
            param[14] = new SqlParameter("@GrossWeight", argIncomingInvDetail.GrossWeight);
            param[15] = new SqlParameter("@NetWeight", argIncomingInvDetail.NetWeight);
            param[16] = new SqlParameter("@PriceDate", argIncomingInvDetail.PriceDate);
            param[17] = new SqlParameter("@Quantity", argIncomingInvDetail.Quantity);
            param[18] = new SqlParameter("@PurchaseQty", argIncomingInvDetail.PurchaseQty);
            param[19] = new SqlParameter("@NetValue", argIncomingInvDetail.NetValue);
            param[20] = new SqlParameter("@TaxAmt", argIncomingInvDetail.TaxAmt);
            param[21] = new SqlParameter("@NetPricePerQty", argIncomingInvDetail.NetPricePerQty);
            param[22] = new SqlParameter("@ItemCategoryCode", argIncomingInvDetail.ItemCategoryCode);
            param[23] = new SqlParameter("@UOMCode", argIncomingInvDetail.UOMCode);
            param[24] = new SqlParameter("@PurchaseUOMCode", argIncomingInvDetail.PurchaseUOMCode);
            param[25] = new SqlParameter("@COACode", argIncomingInvDetail.COACode);
            param[26] = new SqlParameter("@OrginatingDoc", argIncomingInvDetail.OrginatingDoc);
            param[27] = new SqlParameter("@OrginatingItem", argIncomingInvDetail.OrginatingItem);
            param[28] = new SqlParameter("@PODoc", argIncomingInvDetail.PODoc);
            param[29] = new SqlParameter("@POItem", argIncomingInvDetail.POItem);
            param[30] = new SqlParameter("@MatGroup1Code", argIncomingInvDetail.MatGroup1Code);
            param[31] = new SqlParameter("@ServiceRenderDate", argIncomingInvDetail.ServiceRenderDate);
            param[32] = new SqlParameter("@CostCenterCode", argIncomingInvDetail.CostCenterCode);
            param[33] = new SqlParameter("@ClientCode", argIncomingInvDetail.ClientCode);
            param[34] = new SqlParameter("@CreatedBy", argIncomingInvDetail.CreatedBy);
            param[35] = new SqlParameter("@ModifiedBy", argIncomingInvDetail.ModifiedBy);

            param[36] = new SqlParameter("@Type", SqlDbType.Char);
            param[36].Size = 1;
            param[36].Direction = ParameterDirection.Output;

            param[37] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[37].Size = 255;
            param[37].Direction = ParameterDirection.Output;

            param[38] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[38].Size = 20;
            param[38].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateIncomingInvDetail", param);


            string strMessage = Convert.ToString(param[37].Value);
            string strType = Convert.ToString(param[36].Value);
            string strRetValue = Convert.ToString(param[38].Value);


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

        public ICollection<ErrorHandler> DeleteIncomingInvDetail(string argIncomingInvDocCode, int argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@IncomingInvDocCode", argIncomingInvDocCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteIncomingInvDetail", param);


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

        public ICollection<ErrorHandler> DeleteIncomingInvDetail(string argIncomingInvDocCode, int argItemNo, string argClientCode, int iIsDeleted, DataAccess da, List<ErrorHandler> lstErr)
        {
           try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@IncomingInvDocCode", argIncomingInvDocCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteIncomingInvDetail", param);


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

        public bool blnIsIncomingInvDetailExists(string argIncomingInvDocCode, int argItemNo, string argClientCode)
        {
            bool IsIncomingInvDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetIncomingInvDetail(argIncomingInvDocCode, argItemNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsIncomingInvDetailExists = true;
            }
            else
            {
                IsIncomingInvDetailExists = false;
            }
            return IsIncomingInvDetailExists;
        }

        public bool blnIsIncomingInvDetailExists(string argIncomingInvDocCode, int argItemNo, string argClientCode,DataAccess da)
        {
            bool IsIncomingInvDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetIncomingInvDetail(argIncomingInvDocCode, argItemNo, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsIncomingInvDetailExists = true;
            }
            else
            {
                IsIncomingInvDetailExists = false;
            }
            return IsIncomingInvDetailExists;
        }
    }
}