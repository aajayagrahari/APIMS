
//Created On :: 19, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class InBDeliveryDetailManager
    {
        const string InBDeliveryDetailTable = "InBDeliveryDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public InBDeliveryDetail objGetInBDeliveryDetail(string argInBDeliveryDocCode, string argItemNo, string argClientCode)
        {
            InBDeliveryDetail argInBDeliveryDetail = new InBDeliveryDetail();
            DataSet DataSetToFill = new DataSet();

            if (argInBDeliveryDocCode.Trim() == "")
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

            DataSetToFill = this.GetInBDeliveryDetail(argInBDeliveryDocCode, argItemNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argInBDeliveryDetail = this.objCreateInBDeliveryDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argInBDeliveryDetail;
        }


        public ICollection<InBDeliveryDetail> colGetPODetail4InBDC(string argInBDeliveryDocTypeCode, string argPODocCode, string argClientCode, int argItemNoFrom, int argItemNoTo, List<InBDeliveryDetail> lst)
        {
            DataSet DataSetToFill = new DataSet();
            InBDeliveryDetail tInBDeliveryDetail = new InBDeliveryDetail();
            PurchaseOrderDetailManager objPurchaseOrderDetailManager = new PurchaseOrderDetailManager();
            DataSetToFill = objPurchaseOrderDetailManager.GetPurchaseOrderDetail(argInBDeliveryDocTypeCode, argPODocCode, argClientCode, argItemNoFrom, argItemNoTo);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateInBDeliveryDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }


        public ICollection<InBDeliveryDetail> colGetInBDeliveryDetail(string argInBDeliveryDocCode, string argClientCode, List<InBDeliveryDetail> lst)
        {
            //List<InBDeliveryDetail> lst = new List<InBDeliveryDetail>();
            DataSet DataSetToFill = new DataSet();
            InBDeliveryDetail tInBDeliveryDetail = new InBDeliveryDetail();

            DataSetToFill = this.GetInBDeliveryDetail(argInBDeliveryDocCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateInBDeliveryDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetInBDeliveryDetail(string argInBDeliveryDocCode, string argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetInBDeliveryDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetInBDeliveryDetail(string argInBDeliveryDocCode, string argItemNo, string argClientCode, DataAccess da )
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetInBDeliveryDetail4ID", param);

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

        public DataSet GetInBDeliveryDetail(string argInBDeliveryDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            
            param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetInBDeliveryDetail",param);
            return DataSetToFill;
        }


        private InBDeliveryDetail objCreateInBDeliveryDetail(DataRow dr)
        {
            InBDeliveryDetail tInBDeliveryDetail = new InBDeliveryDetail();

            tInBDeliveryDetail.SetObjectInfo(dr);

            return tInBDeliveryDetail;

        }


        public ICollection<ErrorHandler> SaveInBDeliveryDetail(InBDeliveryDetail argInBDeliveryDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsInBDeliveryDetailExists(argInBDeliveryDetail.InBDeliveryDocCode, argInBDeliveryDetail.ItemNo, argInBDeliveryDetail.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertInBDeliveryDetail(argInBDeliveryDetail, da, lstErr);
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
                    UpdateInBDeliveryDetail(argInBDeliveryDetail, da, lstErr);
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

        public void SaveInBDeliveryDetail(InBDeliveryDetail argInBDeliveryDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsInBDeliveryDetailExists(argInBDeliveryDetail.InBDeliveryDocCode, argInBDeliveryDetail.ItemNo, argInBDeliveryDetail.ClientCode,da) == false)
                {
                    InsertInBDeliveryDetail(argInBDeliveryDetail, da, lstErr);
                }
                else
                {
                    UpdateInBDeliveryDetail(argInBDeliveryDetail, da, lstErr);
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

        public void InsertInBDeliveryDetail(InBDeliveryDetail argInBDeliveryDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDetail.InBDeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argInBDeliveryDetail.ItemNo);
            param[2] = new SqlParameter("@PODocCode", argInBDeliveryDetail.PODocCode);
            param[3] = new SqlParameter("@POItemNo", argInBDeliveryDetail.POItemNo);
            param[4] = new SqlParameter("@DeliveryDocCode", argInBDeliveryDetail.DeliveryDocCode);
            param[5] = new SqlParameter("@DLItemNo", argInBDeliveryDetail.DLItemNo);
            param[6] = new SqlParameter("@MaterialCode", argInBDeliveryDetail.MaterialCode);
            param[7] = new SqlParameter("@Batch", argInBDeliveryDetail.Batch);
            param[8] = new SqlParameter("@PlantCode", argInBDeliveryDetail.PlantCode);
            param[9] = new SqlParameter("@StoreCode", argInBDeliveryDetail.StoreCode);
            param[10] = new SqlParameter("@ItemCategoryCode", argInBDeliveryDetail.ItemCategoryCode);
            param[11] = new SqlParameter("@MatMovementCode", argInBDeliveryDetail.MatMovementCode);
            param[12] = new SqlParameter("@Quantity", argInBDeliveryDetail.Quantity);
            param[13] = new SqlParameter("@PriceUnit", argInBDeliveryDetail.PriceUnit);
            param[14] = new SqlParameter("@UOMCode", argInBDeliveryDetail.UOMCode);
            param[15] = new SqlParameter("@RCStatus", argInBDeliveryDetail.RCStatus);
            param[16] = new SqlParameter("@ReceiptQty", argInBDeliveryDetail.ReceiptQty);
            param[17] = new SqlParameter("@ClientCode", argInBDeliveryDetail.ClientCode);
            param[18] = new SqlParameter("@CreatedBy", argInBDeliveryDetail.CreatedBy);
            param[19] = new SqlParameter("@ModifiedBy", argInBDeliveryDetail.ModifiedBy);

            param[20] = new SqlParameter("@Type", SqlDbType.Char);
            param[20].Size = 1;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[21].Size = 255;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[22].Size = 20;
            param[22].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertInBDeliveryDetail", param);


            string strMessage = Convert.ToString(param[21].Value);
            string strType = Convert.ToString(param[20].Value);
            string strRetValue = Convert.ToString(param[22].Value);


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

        public void UpdateInBDeliveryDetail(InBDeliveryDetail argInBDeliveryDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDetail.InBDeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argInBDeliveryDetail.ItemNo);
            param[2] = new SqlParameter("@PODocCode", argInBDeliveryDetail.PODocCode);
            param[3] = new SqlParameter("@POItemNo", argInBDeliveryDetail.POItemNo);
            param[4] = new SqlParameter("@DeliveryDocCode", argInBDeliveryDetail.DeliveryDocCode);
            param[5] = new SqlParameter("@DLItemNo", argInBDeliveryDetail.DLItemNo);
            param[6] = new SqlParameter("@MaterialCode", argInBDeliveryDetail.MaterialCode);
            param[7] = new SqlParameter("@Batch", argInBDeliveryDetail.Batch);
            param[8] = new SqlParameter("@PlantCode", argInBDeliveryDetail.PlantCode);
            param[9] = new SqlParameter("@StoreCode", argInBDeliveryDetail.StoreCode);
            param[10] = new SqlParameter("@ItemCategoryCode", argInBDeliveryDetail.ItemCategoryCode);
            param[11] = new SqlParameter("@MatMovementCode", argInBDeliveryDetail.MatMovementCode);
            param[12] = new SqlParameter("@Quantity", argInBDeliveryDetail.Quantity);
            param[13] = new SqlParameter("@PriceUnit", argInBDeliveryDetail.PriceUnit);
            param[14] = new SqlParameter("@UOMCode", argInBDeliveryDetail.UOMCode);
            param[15] = new SqlParameter("@RCStatus", argInBDeliveryDetail.RCStatus);
            param[16] = new SqlParameter("@ReceiptQty", argInBDeliveryDetail.ReceiptQty);
            param[17] = new SqlParameter("@ClientCode", argInBDeliveryDetail.ClientCode);
            param[18] = new SqlParameter("@CreatedBy", argInBDeliveryDetail.CreatedBy);
            param[19] = new SqlParameter("@ModifiedBy", argInBDeliveryDetail.ModifiedBy);

            param[20] = new SqlParameter("@Type", SqlDbType.Char);
            param[20].Size = 1;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[21].Size = 255;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[22].Size = 20;
            param[22].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateInBDeliveryDetail", param);


            string strMessage = Convert.ToString(param[21].Value);
            string strType = Convert.ToString(param[20].Value);
            string strRetValue = Convert.ToString(param[22].Value);


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


        public ICollection<ErrorHandler> DeleteInBDeliveryDetail(string argInBDeliveryDocCode, string argItemNo, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDocCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteInBDeliveryDetail", param);


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

        public ICollection<ErrorHandler> DeleteInBDeliveryDetail(string argInBDeliveryDocCode, string argItemNo, string argClientCode, int iIsDeleted, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDocCode);
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
                int i = da.NExecuteNonQuery("Proc_DeleteInBDeliveryDetail", param);


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


        public bool blnIsInBDeliveryDetailExists(string argInBDeliveryDocCode, string argItemNo, string argClientCode)
        {
            bool IsInBDeliveryDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetInBDeliveryDetail(argInBDeliveryDocCode, argItemNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsInBDeliveryDetailExists = true;
            }
            else
            {
                IsInBDeliveryDetailExists = false;
            }
            return IsInBDeliveryDetailExists;
        }
        public bool blnIsInBDeliveryDetailExists(string argInBDeliveryDocCode, string argItemNo, string argClientCode, DataAccess da)
        {
            bool IsInBDeliveryDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetInBDeliveryDetail(argInBDeliveryDocCode, argItemNo, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsInBDeliveryDetailExists = true;
            }
            else
            {
                IsInBDeliveryDetailExists = false;
            }
            return IsInBDeliveryDetailExists;
        }

    }
}