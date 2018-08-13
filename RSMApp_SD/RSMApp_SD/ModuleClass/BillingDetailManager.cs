
//Created On :: 17, September, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class BillingDetailManager
    {
        const string BillingDetailTable = "BillingDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public BillingDetail objGetBillingDetail(string argBillingDocCode, int argItemNo, string argClientCode)
        {
            BillingDetail argBillingDetail = new BillingDetail();
            DataSet DataSetToFill = new DataSet();

            if (argBillingDocCode.Trim() == "")
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

            DataSetToFill = this.GetBillingDetail(argBillingDocCode, argItemNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argBillingDetail = this.objCreateBillingDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argBillingDetail;
        }

        public ICollection<BillingDetail> colGetBillingDetail(string argBillingDocCode, string argClientCode, List<BillingDetail> lst)
        {
            // List<DeliveryDetail> lst = new List<DeliveryDetail>();
            DataSet DataSetToFill = new DataSet();
            DeliveryDetail tDeliveryDetail = new DeliveryDetail();

            DataSetToFill = this.GetBillingDetail(argBillingDocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateBillingDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<BillingDetail> colGetBillingDetail(string argBillingDocCode,string argClientCode)
        {
            List<BillingDetail> lst = new List<BillingDetail>();
            DataSet DataSetToFill = new DataSet();
            BillingDetail tBillingDetail = new BillingDetail();

            DataSetToFill = this.GetBillingDetail(argBillingDocCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateBillingDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<BillingDetail> colGetBillingDetail4SO(string argSODocCode, string argClientCode, List<BillingDetail> lst)
        {
            DataSet DataSetToFill = new DataSet();
            SalesOrderDetailManager objSalesOrderDetailManager = new SalesOrderDetailManager();
            DataSetToFill = objSalesOrderDetailManager.GetSalesOrderDetail4BD(argSODocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateBillingDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<BillingDetail> colGetBillingDetail4DC(string argDeliveryDocCode, string argClientCode, List<BillingDetail> lst)
        {
            DataSet DataSetToFill = new DataSet();
            DeliveryDetail tDeliveryDetail = new DeliveryDetail();
            DeliveryDetailManager objDeliveryDetailManager = new DeliveryDetailManager();
            DataSetToFill = objDeliveryDetailManager.GetDeliveryDetail4BD(argDeliveryDocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateBillingDetail(dr));
                }
            }
            goto Finish;

            Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetBillingDetail(string argBillingDocCode, int argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BillingDocCode", argBillingDocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBillingDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetBillingDetail(string argBillingDocCode, int argItemNo, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BillingDocCode", argBillingDocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetBillingDetail4ID", param);

            return DataSetToFill;
        }
                
        public DataSet GetBillingDetail(string argBillingDocCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@BillingDocCode", argBillingDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBillingDetail",param);
            return DataSetToFill;
        }
        
        private BillingDetail objCreateBillingDetail(DataRow dr)
        {
            BillingDetail tBillingDetail = new BillingDetail();

            tBillingDetail.SetObjectInfo(dr);

            return tBillingDetail;

        }
        
        //public ICollection<ErrorHandler> SaveBillingDetail(BillingDetail argBillingDetail)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsBillingDetailExists(argBillingDetail.BillingDocCode, argBillingDetail.ItemNo, argBillingDetail.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertBillingDetail(argBillingDetail, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
        //            da.COMMIT_TRANSACTION();
        //        }
        //        else
        //        {
        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            UpdateBillingDetail(argBillingDetail, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
        //            da.COMMIT_TRANSACTION();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (da != null)
        //        {
        //            da.ROLLBACK_TRANSACTION();
        //        }
        //        objErrorHandler.Type = ErrorConstant.strAboartType;
        //        objErrorHandler.MsgId = 0;
        //        objErrorHandler.Module = ErrorConstant.strInsertModule;
        //        objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
        //        objErrorHandler.Message = ex.Message.ToString();
        //        objErrorHandler.RowNo = 0;
        //        objErrorHandler.FieldName = "";
        //        objErrorHandler.LogCode = "";
        //        lstErr.Add(objErrorHandler);
        //    }
        //    finally
        //    {
        //        if (da != null)
        //        {
        //            da.Close_Connection();
        //            da = null;
        //        }
        //    }
        //    return lstErr;
        //}

        public void SaveBillingDetail(BillingDetail argBillingDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsBillingDetailExists(argBillingDetail.BillingDocCode, argBillingDetail.ItemNo, argBillingDetail.ClientCode, da) == false)
                {
                    InsertBillingDetail(argBillingDetail, da, lstErr);
                }
                else
                {
                    UpdateBillingDetail(argBillingDetail, da, lstErr);
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

        public void InsertBillingDetail(BillingDetail argBillingDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[46];
            param[0] = new SqlParameter("@BillingDocCode", argBillingDetail.BillingDocCode);
            param[1] = new SqlParameter("@ItemNo", argBillingDetail.ItemNo);
            param[2] = new SqlParameter("@RefDocumentCode", argBillingDetail.RefDocumentCode);
            param[3] = new SqlParameter("@RefItemNo", argBillingDetail.RefItemNo);
            param[4] = new SqlParameter("@MaterialCode", argBillingDetail.MaterialCode);
            param[5] = new SqlParameter("@Batch", argBillingDetail.Batch);
            param[6] = new SqlParameter("@PlantCode", argBillingDetail.PlantCode);
            param[7] = new SqlParameter("@StoreCode", argBillingDetail.StoreCode);
            param[8] = new SqlParameter("@SalesOrganizationCode", argBillingDetail.SalesOrganizationCode);
            param[9] = new SqlParameter("@DistChannelCode", argBillingDetail.DistChannelCode);
            param[10] = new SqlParameter("@DivisionCode", argBillingDetail.DivisionCode);
            param[11] = new SqlParameter("@ShipingPointCode", argBillingDetail.ShipingPointCode);
            param[12] = new SqlParameter("@SalesofficeCode", argBillingDetail.SalesofficeCode);
            param[13] = new SqlParameter("@SalesGroupCode", argBillingDetail.SalesGroupCode);
            param[14] = new SqlParameter("@ValTypeCode", argBillingDetail.ValTypeCode);
            param[15] = new SqlParameter("@ProfitCenter", argBillingDetail.ProfitCenter);
            param[16] = new SqlParameter("@MatVolume", argBillingDetail.MatVolume);
            param[17] = new SqlParameter("@VolumeUOM", argBillingDetail.VolumeUOM);
            param[18] = new SqlParameter("@WeightUOM", argBillingDetail.WeightUOM);
            param[19] = new SqlParameter("@GrossWeight", argBillingDetail.GrossWeight);
            param[20] = new SqlParameter("@NetWeight", argBillingDetail.NetWeight);
            param[21] = new SqlParameter("@PriceDate", argBillingDetail.PriceDate);
            param[22] = new SqlParameter("@ItemCategoryCode", argBillingDetail.ItemCategoryCode);
            param[23] = new SqlParameter("@Quantity", argBillingDetail.Quantity);
            param[24] = new SqlParameter("@BillingQty", argBillingDetail.BillingQty);
            param[25] = new SqlParameter("@NetValue", argBillingDetail.NetValue);
            param[26] = new SqlParameter("@TaxAmt", argBillingDetail.TaxAmt);
            param[27] = new SqlParameter("@NetPricePerQty", argBillingDetail.NetPricePerQty);
            param[28] = new SqlParameter("@UOMCode", argBillingDetail.UOMCode);
            param[29] = new SqlParameter("@SalesUOMCode", argBillingDetail.SalesUOMCode);
            param[30] = new SqlParameter("@COACode", argBillingDetail.COACode);
            param[31] = new SqlParameter("@BusinessAreaCode", argBillingDetail.BusinessAreaCode);
            param[32] = new SqlParameter("@ProfitSegment", argBillingDetail.ProfitSegment);
            param[33] = new SqlParameter("@OrginatingDoc", argBillingDetail.OrginatingDoc);
            param[34] = new SqlParameter("@OrginatingItem", argBillingDetail.OrginatingItem);
            param[35] = new SqlParameter("@SalesDoc", argBillingDetail.SalesDoc);
            param[36] = new SqlParameter("@SalesItem", argBillingDetail.SalesItem);
            param[37] = new SqlParameter("@MatGroup1Code", argBillingDetail.MatGroup1Code);
            param[38] = new SqlParameter("@ServiceRenderDate", argBillingDetail.ServiceRenderDate);
            param[39] = new SqlParameter("@CostCenterCode", argBillingDetail.CostCenterCode);
            param[40] = new SqlParameter("@ClientCode", argBillingDetail.ClientCode);
            param[41] = new SqlParameter("@CreatedBy", argBillingDetail.CreatedBy);
            param[42] = new SqlParameter("@ModifiedBy", argBillingDetail.ModifiedBy);
       
            param[43] = new SqlParameter("@Type", SqlDbType.Char);
            param[43].Size = 1;
            param[43].Direction = ParameterDirection.Output;

            param[44] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[44].Size = 255;
            param[44].Direction = ParameterDirection.Output;

            param[45] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[45].Size = 20;
            param[45].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertBillingDetail", param);


            string strMessage = Convert.ToString(param[44].Value);
            string strType = Convert.ToString(param[43].Value);
            string strRetValue = Convert.ToString(param[45].Value);


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
        
        public void UpdateBillingDetail(BillingDetail argBillingDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[46];
            param[0] = new SqlParameter("@BillingDocCode", argBillingDetail.BillingDocCode);
            param[1] = new SqlParameter("@ItemNo", argBillingDetail.ItemNo);
            param[2] = new SqlParameter("@RefDocumentCode", argBillingDetail.RefDocumentCode);
            param[3] = new SqlParameter("@RefItemNo", argBillingDetail.RefItemNo);
            param[4] = new SqlParameter("@MaterialCode", argBillingDetail.MaterialCode);
            param[5] = new SqlParameter("@Batch", argBillingDetail.Batch);
            param[6] = new SqlParameter("@PlantCode", argBillingDetail.PlantCode);
            param[7] = new SqlParameter("@StoreCode", argBillingDetail.StoreCode);
            param[8] = new SqlParameter("@SalesOrganizationCode", argBillingDetail.SalesOrganizationCode);
            param[9] = new SqlParameter("@DistChannelCode", argBillingDetail.DistChannelCode);
            param[10] = new SqlParameter("@DivisionCode", argBillingDetail.DivisionCode);
            param[11] = new SqlParameter("@ShipingPointCode", argBillingDetail.ShipingPointCode);
            param[12] = new SqlParameter("@SalesofficeCode", argBillingDetail.SalesofficeCode);
            param[13] = new SqlParameter("@SalesGroupCode", argBillingDetail.SalesGroupCode);
            param[14] = new SqlParameter("@ValTypeCode", argBillingDetail.ValTypeCode);
            param[15] = new SqlParameter("@ProfitCenter", argBillingDetail.ProfitCenter);
            param[16] = new SqlParameter("@MatVolume", argBillingDetail.MatVolume);
            param[17] = new SqlParameter("@VolumeUOM", argBillingDetail.VolumeUOM);
            param[18] = new SqlParameter("@WeightUOM", argBillingDetail.WeightUOM);
            param[19] = new SqlParameter("@GrossWeight", argBillingDetail.GrossWeight);
            param[20] = new SqlParameter("@NetWeight", argBillingDetail.NetWeight);
            param[21] = new SqlParameter("@PriceDate", argBillingDetail.PriceDate);
            param[22] = new SqlParameter("@ItemCategoryCode", argBillingDetail.ItemCategoryCode);
            param[23] = new SqlParameter("@Quantity", argBillingDetail.Quantity);
            param[24] = new SqlParameter("@BillingQty", argBillingDetail.BillingQty);
            param[25] = new SqlParameter("@NetValue", argBillingDetail.NetValue);
            param[26] = new SqlParameter("@TaxAmt", argBillingDetail.TaxAmt);
            param[27] = new SqlParameter("@NetPricePerQty", argBillingDetail.NetPricePerQty);
            param[28] = new SqlParameter("@UOMCode", argBillingDetail.UOMCode);
            param[29] = new SqlParameter("@SalesUOMCode", argBillingDetail.SalesUOMCode);
            param[30] = new SqlParameter("@COACode", argBillingDetail.COACode);
            param[31] = new SqlParameter("@BusinessAreaCode", argBillingDetail.BusinessAreaCode);
            param[32] = new SqlParameter("@ProfitSegment", argBillingDetail.ProfitSegment);
            param[33] = new SqlParameter("@OrginatingDoc", argBillingDetail.OrginatingDoc);
            param[34] = new SqlParameter("@OrginatingItem", argBillingDetail.OrginatingItem);
            param[35] = new SqlParameter("@SalesDoc", argBillingDetail.SalesDoc);
            param[36] = new SqlParameter("@SalesItem", argBillingDetail.SalesItem);
            param[37] = new SqlParameter("@MatGroup1Code", argBillingDetail.MatGroup1Code);
            param[38] = new SqlParameter("@ServiceRenderDate", argBillingDetail.ServiceRenderDate);
            param[39] = new SqlParameter("@CostCenterCode", argBillingDetail.CostCenterCode);
            param[40] = new SqlParameter("@ClientCode", argBillingDetail.ClientCode);
            param[41] = new SqlParameter("@CreatedBy", argBillingDetail.CreatedBy);
            param[42] = new SqlParameter("@ModifiedBy", argBillingDetail.ModifiedBy);

            param[43] = new SqlParameter("@Type", SqlDbType.Char);
            param[43].Size = 1;
            param[43].Direction = ParameterDirection.Output;

            param[44] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[44].Size = 255;
            param[44].Direction = ParameterDirection.Output;

            param[45] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[45].Size = 20;
            param[45].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateBillingDetail", param);


            string strMessage = Convert.ToString(param[44].Value);
            string strType = Convert.ToString(param[43].Value);
            string strRetValue = Convert.ToString(param[45].Value);


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
        
        public ICollection<ErrorHandler> DeleteBillingDetail(string argBillingDocCode, int argItemNo, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@BillingDocCode", argBillingDocCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteBillingDetail", param);


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

        public void DeleteBillingDetail(string argBillingDocCode, int argItemNo, int IisDelete, string argClientCode, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@BillingDocCode", argBillingDocCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@IsDeleted", IisDelete);
                param[3] = new SqlParameter("@ClientCode", argClientCode);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.NExecuteNonQuery("Proc_DeleteBillingDetail", param);


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

        }

        public bool blnIsBillingDetailExists(string argBillingDocCode, int argItemNo, string argClientCode)
        {
            bool IsBillingDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetBillingDetail(argBillingDocCode, argItemNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsBillingDetailExists = true;
            }
            else
            {
                IsBillingDetailExists = false;
            }
            return IsBillingDetailExists;
        }

        public bool blnIsBillingDetailExists(string argBillingDocCode, int argItemNo, string argClientCode,DataAccess da)
        {
            bool IsBillingDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetBillingDetail(argBillingDocCode, argItemNo, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsBillingDetailExists = true;
            }
            else
            {
                IsBillingDetailExists = false;
            }
            return IsBillingDetailExists;
        }

    }
}