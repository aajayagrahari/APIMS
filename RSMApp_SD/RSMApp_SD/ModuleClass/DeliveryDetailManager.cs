
//Created On :: 19, July, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class DeliveryDetailManager
    {
        const string DeliveryDetailTable = "DeliveryDetail";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public DeliveryDetail objGetDeliveryDetail(string argDeliveryDocCode, string argItemNo, string argClientCode)
        {
            DeliveryDetail argDeliveryDetail = new DeliveryDetail();
            DataSet DataSetToFill = new DataSet();

            if (argDeliveryDocCode.Trim() == "")
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

            DataSetToFill = this.GetDeliveryDetail(argDeliveryDocCode, argItemNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argDeliveryDetail = this.objCreateDeliveryDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argDeliveryDetail;
        }
        
        public ICollection<DeliveryDetail> colGetDeliveryDetail(string argDeliveryDocCode, string argClientCode, List<DeliveryDetail> lst)
        {
           // List<DeliveryDetail> lst = new List<DeliveryDetail>();
            DataSet DataSetToFill = new DataSet();
            DeliveryDetail tDeliveryDetail = new DeliveryDetail();

            DataSetToFill = this.GetDeliveryDetail(argDeliveryDocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDeliveryDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;
            
            return lst;
        }

        public ICollection<DeliveryDetail> colGetSODetail4DC(string argDeliveryDocTypeCode,string argSODocCode, string argClientCode, int argItemNoFrom, int argItemNoTo, List<DeliveryDetail> lst)
        {
            // List<DeliveryDetail> lst = new List<DeliveryDetail>();
            DataSet DataSetToFill = new DataSet();
            DeliveryDetail tDeliveryDetail = new DeliveryDetail();
            SalesOrderDetailManager objSalesOrderDetailManager = new SalesOrderDetailManager();
            DataSetToFill = objSalesOrderDetailManager.GetSalesOrderDetail(argDeliveryDocTypeCode, argSODocCode, argClientCode, argItemNoFrom, argItemNoTo);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDeliveryDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<DeliveryDetail> colGetDeliveryDetail(string argDeliveryDocCode, string argClientCode)
        {
            List<DeliveryDetail> lst = new List<DeliveryDetail>();
            DataSet DataSetToFill = new DataSet();
            DeliveryDetail tDeliveryDetail = new DeliveryDetail();

            DataSetToFill = this.GetDeliveryDetail(argDeliveryDocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDeliveryDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetDeliveryDetail(string argDeliveryDocCode, string argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDeliveryDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetDeliveryDetail(string argDeliveryDocCode, string argItemNo, string argClientCode, DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetDeliveryDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetDeliveryDetail4BD(string argDeliveryDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);


            DataSetToFill = da.FillDataSet("SP_GetDeliveryDetail4BD", param);
            return DataSetToFill;
        }

        public DataSet GetDeliveryDetail(string argDeliveryDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);


            DataSetToFill = da.FillDataSet("SP_GetDeliveryDetail", param);
            return DataSetToFill;
        }
        
        private DeliveryDetail objCreateDeliveryDetail(DataRow dr)
        {
            DeliveryDetail tDeliveryDetail = new DeliveryDetail();

            tDeliveryDetail.SetObjectInfo(dr);

            return tDeliveryDetail;

        }

        public void SaveDeliveryDetail(DeliveryDetail argDeliveryDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsDeliveryDetailExists(argDeliveryDetail.DeliveryDocCode, argDeliveryDetail.ItemNo, argDeliveryDetail.ClientCode, da) == false)
                {
                    InsertDeliveryDetail(argDeliveryDetail, da, lstErr);
                }
                else
                {
                    UpdateDeliveryDetail(argDeliveryDetail, da, lstErr);
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

        //public ICollection<ErrorHandler> SaveDeliveryDetail(DeliveryDetail argDeliveryDetail)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsDeliveryDetailExists(argDeliveryDetail.DeliveryDocCode, argDeliveryDetail.ItemNo, argDeliveryDetail.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertDeliveryDetail(argDeliveryDetail, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            UpdateDeliveryDetail(argDeliveryDetail, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
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
        
        public void InsertDeliveryDetail(DeliveryDetail argDeliveryDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDetail.DeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argDeliveryDetail.ItemNo);
            param[2] = new SqlParameter("@SODocCode", argDeliveryDetail.SODocCode);
            param[3] = new SqlParameter("@SOItemNo", argDeliveryDetail.SOItemNo);
            param[4] = new SqlParameter("@MaterialCode", argDeliveryDetail.MaterialCode);
            param[5] = new SqlParameter("@Batch", argDeliveryDetail.Batch);
            param[6] = new SqlParameter("@PlantCode", argDeliveryDetail.PlantCode);
            param[7] = new SqlParameter("@StoreCode", argDeliveryDetail.StoreCode);
            param[8] = new SqlParameter("@ItemCategoryCode", argDeliveryDetail.ItemCategoryCode);
            param[9] = new SqlParameter("@MatMovementCode", argDeliveryDetail.MatMovementCode);
            param[10] = new SqlParameter("@Quantity", argDeliveryDetail.Quantity);
            param[11] = new SqlParameter("@PriceUnit", argDeliveryDetail.PriceUnit);
            param[12] = new SqlParameter("@UOMCode", argDeliveryDetail.UOMCode);
           
            param[13] = new SqlParameter("@OrderQty", argDeliveryDetail.OrderQty);
            param[14] = new SqlParameter("@OrderUOMCode", argDeliveryDetail.OrderUOMCode);
            param[15] = new SqlParameter("@GrossWeight", argDeliveryDetail.GrossWeight);
            param[16] = new SqlParameter("@NetWeight", argDeliveryDetail.NetWeight);

            param[17] = new SqlParameter("@ClientCode", argDeliveryDetail.ClientCode);
            param[18] = new SqlParameter("@CreatedBy", argDeliveryDetail.CreatedBy);
            param[19] = new SqlParameter("@ModifiedBy", argDeliveryDetail.ModifiedBy);
           
            param[20] = new SqlParameter("@Type", SqlDbType.Char);
            param[20].Size = 1;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[21].Size = 255;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[22].Size = 20;
            param[22].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertDeliveryDetail", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }
        
        public void UpdateDeliveryDetail(DeliveryDetail argDeliveryDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDetail.DeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argDeliveryDetail.ItemNo);
            param[2] = new SqlParameter("@SODocCode", argDeliveryDetail.SODocCode);
            param[3] = new SqlParameter("@SOItemNo", argDeliveryDetail.SOItemNo);
            param[4] = new SqlParameter("@MaterialCode", argDeliveryDetail.MaterialCode);
            param[5] = new SqlParameter("@Batch", argDeliveryDetail.Batch);
            param[6] = new SqlParameter("@PlantCode", argDeliveryDetail.PlantCode);
            param[7] = new SqlParameter("@StoreCode", argDeliveryDetail.StoreCode);
            param[8] = new SqlParameter("@ItemCategoryCode", argDeliveryDetail.ItemCategoryCode);
            param[9] = new SqlParameter("@MatMovementCode", argDeliveryDetail.MatMovementCode);
            param[10] = new SqlParameter("@Quantity", argDeliveryDetail.Quantity);
            param[11] = new SqlParameter("@PriceUnit", argDeliveryDetail.PriceUnit);
            param[12] = new SqlParameter("@UOMCode", argDeliveryDetail.UOMCode);
            param[13] = new SqlParameter("@OrderQty", argDeliveryDetail.OrderQty);
            param[14] = new SqlParameter("@OrderUOMCode", argDeliveryDetail.OrderUOMCode);
            param[15] = new SqlParameter("@GrossWeight", argDeliveryDetail.GrossWeight);
            param[16] = new SqlParameter("@NetWeight", argDeliveryDetail.NetWeight);

            param[17] = new SqlParameter("@ClientCode", argDeliveryDetail.ClientCode);
            param[18] = new SqlParameter("@CreatedBy", argDeliveryDetail.CreatedBy);
            param[19] = new SqlParameter("@ModifiedBy", argDeliveryDetail.ModifiedBy);

            param[20] = new SqlParameter("@Type", SqlDbType.Char);
            param[20].Size = 1;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[21].Size = 255;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[22].Size = 20;
            param[22].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateDeliveryDetail", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public void DeleteDeliveryDetail(string argDeliveryDocCode, string argItemNo, int IisDelete, string argClientCode, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
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

                int i = da.NExecuteNonQuery("Proc_DeleteDeliveryDetail", param);


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

        public ICollection<ErrorHandler> DeleteDeliveryDetail(string argDeliveryDocCode, string argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteDeliveryDetail", param);


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
        
        public bool blnIsDeliveryDetailExists(string argDeliveryDocCode, string argItemNo, string argClientCode, DataAccess da)
        {
            bool IsDeliveryDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetDeliveryDetail(argDeliveryDocCode, argItemNo, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDeliveryDetailExists = true;
            }
            else
            {
                IsDeliveryDetailExists = false;
            }
            return IsDeliveryDetailExists;
        }

        public bool blnIsDeliveryDetailExists(string argDeliveryDocCode, string argItemNo, string argClientCode)
        {
            bool IsDeliveryDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetDeliveryDetail(argDeliveryDocCode, argItemNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDeliveryDetailExists = true;
            }
            else
            {
                IsDeliveryDetailExists = false;
            }
            return IsDeliveryDetailExists;
        }
    }
}