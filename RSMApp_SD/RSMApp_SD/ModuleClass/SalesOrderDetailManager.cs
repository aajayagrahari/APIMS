
//Created On :: 24, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class SalesOrderDetailManager
    {
        const string SalesOrderDetailTable = "SalesOrderDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public SalesOrderDetail objGetSalesOrderDetail(string argSODocCode, string argItemNo, string argClientCode)
        {
            SalesOrderDetail argSalesOrderDetail = new SalesOrderDetail();
            DataSet DataSetToFill = new DataSet();

            if (argSODocCode.Trim() == "")
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

            DataSetToFill = this.GetSalesOrderDetail(argSODocCode, argItemNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSalesOrderDetail = this.objCreateSalesOrderDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSalesOrderDetail;
        }
        
        public ICollection<SalesOrderDetail> colGetSalesOrderDetail(string argSODocCode, string argClientCode, List<SalesOrderDetail> lst)
        {
            //List<SalesOrderDetail> lst = new List<SalesOrderDetail>();
            DataSet DataSetToFill = new DataSet();
            SalesOrderDetail tSalesOrderDetail = new SalesOrderDetail();

            DataSetToFill = this.GetSalesOrderDetail(argSODocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSalesOrderDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public ICollection<SalesOrderDetail> colGetSalesOrderDetail(string argSODocCode, string argClientCode)
        {
            List<SalesOrderDetail> lst = new List<SalesOrderDetail>();
            DataSet DataSetToFill = new DataSet();
            SalesOrderDetail tSalesOrderDetail = new SalesOrderDetail();

            DataSetToFill = this.GetSalesOrderDetail(argSODocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSalesOrderDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }        

        public DataSet GetSalesOrderDetail4Combo(string argPrefix, string argSODocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@SODocCode", argSODocCode);            
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetSalesOrderDetail4Combo", param);

            return DataSetToFill;
        }

        public DataSet GetSalesOrderDetail(string argSODocCode, string argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SODocCode", argSODocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrderDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetSalesOrderDetail(string argSODocCode, string argItemNo, string argClientCode, DataAccess da)
        {
           // DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SODocCode", argSODocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSalesOrderDetail4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetSalesOrderDetail(string argSODocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SODocCode", argSODocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrderDetail", param);
            return DataSetToFill;
        }

        public DataSet GetSalesOrderDetail(string argDeliveryDocTypeCode, string argSODocCode, string argClientCode,int argItemNoFrom,int argItemNoTo)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@DeliveryDocTypeCode", argDeliveryDocTypeCode);
            param[1] = new SqlParameter("@SODocCode", argSODocCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);
            param[3] = new SqlParameter("@ItemNoFrom", argItemNoFrom);
            param[4] = new SqlParameter("@ItemNoTo", argItemNoTo);


            DataSetToFill = da.FillDataSet("SP_GetSalesOrderDetail4DC", param);
            return DataSetToFill;
        }

        public DataSet GetSalesOrderDetail4BD(string argSODocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SODocCode", argSODocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetSalesOrderDetail4BD", param);
            return DataSetToFill;
        }
        

        public DataSet GetSalesOrderDetailsItemNo(string argSODocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SODocCode", argSODocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetSalesOrderDetailItemNo", param);

            return DataSetToFill;
        }

        private SalesOrderDetail objCreateSalesOrderDetail(DataRow dr)
        {
            SalesOrderDetail tSalesOrderDetail = new SalesOrderDetail();

            tSalesOrderDetail.SetObjectInfo(dr);

            return tSalesOrderDetail;

        }
        
        public void SaveSalesOrderDetail(SalesOrderDetail argSalesOrderDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsSalesOrderDetailExists(argSalesOrderDetail.SODocCode, argSalesOrderDetail.ItemNo, argSalesOrderDetail.ClientCode, da) == false)
                {
                    InsertSalesOrderDetail(argSalesOrderDetail, da, lstErr);
                }
                else
                {
                    UpdateSalesOrderDetail(argSalesOrderDetail, da, lstErr);
                }
            }
            catch(Exception ex)
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

        //public ICollection<ErrorHandler> SaveSalesOrderDetail(SalesOrderDetail argSalesOrderDetail)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsSalesOrderDetailExists(argSalesOrderDetail.SODocCode, argSalesOrderDetail.ItemNo, argSalesOrderDetail.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertSalesOrderDetail(argSalesOrderDetail, da, lstErr);
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
        //            UpdateSalesOrderDetail(argSalesOrderDetail, da, lstErr);
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
        
        public void InsertSalesOrderDetail(SalesOrderDetail argSalesOrderDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[29];
            param[0] = new SqlParameter("@SODocCode", argSalesOrderDetail.SODocCode);
            param[1] = new SqlParameter("@ItemNo", argSalesOrderDetail.ItemNo);
            param[2] = new SqlParameter("@POItemNo", argSalesOrderDetail.POItemNo);
            param[3] = new SqlParameter("@PlantCode", argSalesOrderDetail.PlantCode);
            param[4] = new SqlParameter("@StoreCode", argSalesOrderDetail.StoreCode);
            param[5] = new SqlParameter("@MaterialCode", argSalesOrderDetail.MaterialCode);
            param[6] = new SqlParameter("@Batch", argSalesOrderDetail.Batch);
            param[7] = new SqlParameter("@OrderQuantity", argSalesOrderDetail.OrderQuantity);
            param[8] = new SqlParameter("@DeliveryQuantity", argSalesOrderDetail.DeliveryQuantity);
            param[9] = new SqlParameter("@PriceDate", argSalesOrderDetail.PriceDate);
            param[10] = new SqlParameter("@ValClassType", argSalesOrderDetail.ValClassType);
            param[11] = new SqlParameter("@CurrencyCode", argSalesOrderDetail.CurrencyCode);
            param[12] = new SqlParameter("@UOMCode", argSalesOrderDetail.UOMCode);
            param[13] = new SqlParameter("@NetWeight", argSalesOrderDetail.NetWeight);
            param[14] = new SqlParameter("@GrossWeight", argSalesOrderDetail.GrossWeight);
            param[15] = new SqlParameter("@ItemCategoryCode", argSalesOrderDetail.ItemCategoryCode);
            param[16] = new SqlParameter("@HighLevelItem", argSalesOrderDetail.HighLevelItem);
            param[17] = new SqlParameter("@ProfitCenter", argSalesOrderDetail.ProfitCenter);
            param[18] = new SqlParameter("@DeliveryBlock", argSalesOrderDetail.DeliveryBlock);
            param[19] = new SqlParameter("@BillingBlock", argSalesOrderDetail.BillingBlock);
            param[20] = new SqlParameter("@NetValue",argSalesOrderDetail.NetValue);
            param[21] = new SqlParameter("@TaxAmt",argSalesOrderDetail.TaxAmt);
            param[22] = new SqlParameter("@NetPricePerQty",argSalesOrderDetail.NetPricePerQty);
            param[23] = new SqlParameter("@ClientCode", argSalesOrderDetail.ClientCode);
            param[24] = new SqlParameter("@CreatedBy", argSalesOrderDetail.CreatedBy);
            param[25] = new SqlParameter("@ModifiedBy", argSalesOrderDetail.ModifiedBy);
            
            param[26] = new SqlParameter("@Type", SqlDbType.Char);
            param[26].Size = 1;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[27].Size = 255;
            param[27].Direction = ParameterDirection.Output;

            param[28] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[28].Size = 20;
            param[28].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSalesOrderDetail", param);


            string strMessage = Convert.ToString(param[27].Value);
            string strType = Convert.ToString(param[26].Value);
            string strRetValue = Convert.ToString(param[28].Value);


            objErrorHandler.Type = strType;
            objErrorHandler.MsgId = 0;
            objErrorHandler.Module = ErrorConstant.strInsertModule;
            objErrorHandler.ModulePart = ErrorConstant.strDetailModule;
            objErrorHandler.Message = strMessage.ToString();
            objErrorHandler.RowNo = 0;
            objErrorHandler.FieldName = "";
            objErrorHandler.LogCode = "";
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }
        
        public void UpdateSalesOrderDetail(SalesOrderDetail argSalesOrderDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[29];
            param[0] = new SqlParameter("@SODocCode", argSalesOrderDetail.SODocCode);
            param[1] = new SqlParameter("@ItemNo", argSalesOrderDetail.ItemNo);
            param[2] = new SqlParameter("@POItemNo", argSalesOrderDetail.POItemNo);
            param[3] = new SqlParameter("@PlantCode", argSalesOrderDetail.PlantCode);
            param[4] = new SqlParameter("@StoreCode", argSalesOrderDetail.StoreCode);
            param[5] = new SqlParameter("@MaterialCode", argSalesOrderDetail.MaterialCode);
            param[6] = new SqlParameter("@Batch", argSalesOrderDetail.Batch);
            param[7] = new SqlParameter("@OrderQuantity", argSalesOrderDetail.OrderQuantity);
            param[8] = new SqlParameter("@DeliveryQuantity", argSalesOrderDetail.DeliveryQuantity);
            param[9] = new SqlParameter("@PriceDate", argSalesOrderDetail.PriceDate);
            param[10] = new SqlParameter("@ValClassType", argSalesOrderDetail.ValClassType);
            param[11] = new SqlParameter("@CurrencyCode", argSalesOrderDetail.CurrencyCode);
            param[12] = new SqlParameter("@UOMCode", argSalesOrderDetail.UOMCode);
            param[13] = new SqlParameter("@NetWeight", argSalesOrderDetail.NetWeight);
            param[14] = new SqlParameter("@GrossWeight", argSalesOrderDetail.GrossWeight);
            param[15] = new SqlParameter("@ItemCategoryCode", argSalesOrderDetail.ItemCategoryCode);
            param[16] = new SqlParameter("@HighLevelItem", argSalesOrderDetail.HighLevelItem);
            param[17] = new SqlParameter("@ProfitCenter", argSalesOrderDetail.ProfitCenter);
            param[18] = new SqlParameter("@DeliveryBlock", argSalesOrderDetail.DeliveryBlock);
            param[19] = new SqlParameter("@BillingBlock", argSalesOrderDetail.BillingBlock);
            param[20] = new SqlParameter("@NetValue",argSalesOrderDetail.NetValue);
            param[21] = new SqlParameter("@Taxamt",argSalesOrderDetail.TaxAmt);
            param[22] = new SqlParameter("@NetPricePerQty", argSalesOrderDetail.NetPricePerQty);
            param[23] = new SqlParameter("@ClientCode", argSalesOrderDetail.ClientCode);
            param[24] = new SqlParameter("@CreatedBy", argSalesOrderDetail.CreatedBy);
            param[25] = new SqlParameter("@ModifiedBy", argSalesOrderDetail.ModifiedBy);

            param[26] = new SqlParameter("@Type", SqlDbType.Char);
            param[26].Size = 1;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[27].Size = 255;
            param[27].Direction = ParameterDirection.Output;

            param[28] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[28].Size = 20;
            param[28].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateSalesOrderDetail", param);


            string strMessage = Convert.ToString(param[27].Value);
            string strType = Convert.ToString(param[26].Value);
            string strRetValue = Convert.ToString(param[28].Value);


            objErrorHandler.Type = strType;
            objErrorHandler.MsgId = 0;
            objErrorHandler.Module = ErrorConstant.strUpdateModule;
            objErrorHandler.ModulePart = ErrorConstant.strDetailModule;
            objErrorHandler.Message = strMessage.ToString();
            objErrorHandler.RowNo = 0;
            objErrorHandler.FieldName = "";
            objErrorHandler.LogCode = "";
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public void DeleteSalesOrderDetail(string argSODocCode, string argItemNo, string argClientCode, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@SODocCode", argSODocCode);
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

                int i = da.NExecuteNonQuery("Proc_DeleteSalesOrderDetail", param);


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
            
        }

        public ICollection<ErrorHandler> DeleteSalesOrderDetail(string argSODocCode, string argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@SODocCode", argSODocCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteSalesOrderDetail", param);


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
        
        public bool blnIsSalesOrderDetailExists(string argSODocCode, string argItemNo, string argClientCode, DataAccess da)
        {
            bool IsSalesOrderDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesOrderDetail(argSODocCode, argItemNo, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesOrderDetailExists = true;
            }
            else
            {
                IsSalesOrderDetailExists = false;
            }
            return IsSalesOrderDetailExists;
        }

        public bool blnIsSalesOrderDetailExists(string argSODocCode, string argItemNo, string argClientCode)
        {
            bool IsSalesOrderDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesOrderDetail(argSODocCode, argItemNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesOrderDetailExists = true;
            }
            else
            {
                IsSalesOrderDetailExists = false;
            }
            return IsSalesOrderDetailExists;
        }


    }
}