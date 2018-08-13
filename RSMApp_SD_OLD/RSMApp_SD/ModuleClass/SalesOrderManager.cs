
//Created On :: 23, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using Telerik.Web.UI;

namespace RSMApp_SD
{
    public class SalesOrderManager
    {
        const string SalesOrderTable = "SalesOrder";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        SalesOrderDetailManager objSalesOrderDetailManager = new SalesOrderDetailManager();

        
        public SalesOrder objGetSalesOrder(string argSODocCode, string argClientCode)
        {
            SalesOrder argSalesOrder = new SalesOrder();
            DataSet DataSetToFill = new DataSet();

            if (argSODocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetSalesOrder(argSODocCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSalesOrder = this.objCreateSalesOrder((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSalesOrder;
        }
        
        public ICollection<SalesOrder> colGetSalesOrder(string argClientCode)
        {
            List<SalesOrder> lst = new List<SalesOrder>();
            DataSet DataSetToFill = new DataSet();
            SalesOrder tSalesOrder = new SalesOrder();

            DataSetToFill = this.GetSalesOrder(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSalesOrder(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetSalesOrder(string argSODocCode, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SODocCode", argSODocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSalesOrder4ID", param);

            return DataSetToFill;
        }

        public DataSet GetSalesOrder(string argSODocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SODocCode", argSODocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrder4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetSalesOrder(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrder",param);
            return DataSetToFill;
        }
        
        private SalesOrder objCreateSalesOrder(DataRow dr)
        {
            SalesOrder tSalesOrder = new SalesOrder();

            tSalesOrder.SetObjectInfo(dr);

            return tSalesOrder;

        }

        public ICollection<ErrorHandler> SaveSalesOrder(SalesOrder argSalesOrder, ICollection<SalesOrderDetail> colSalesOrderDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            string strretValue = "";
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsSalesOrderExists(argSalesOrder.SODocCode, argSalesOrder.ClientCode, da) == false)
                {
                   strretValue =  InsertSalesOrder(argSalesOrder, da, lstErr);
                }
                else
                {
                   strretValue =  UpdateSalesOrder(argSalesOrder, da, lstErr);
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
                    if (colSalesOrderDetail.Count > 0)
                    {
                        foreach (SalesOrderDetail argSalesOrderDetail in colSalesOrderDetail)
                        {
                            if (argSalesOrderDetail.IsDeleted == 0)
                            {
                                objSalesOrderDetailManager.SaveSalesOrderDetail(argSalesOrderDetail, da, lstErr);
                            }
                            else
                            {
                                objSalesOrderDetailManager.DeleteSalesOrderDetail(argSalesOrderDetail.SODocCode,argSalesOrderDetail.ItemNo, argSalesOrderDetail.ClientCode, da, lstErr);
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
            catch(Exception ex)
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

        public ICollection<ErrorHandler> SaveSalesOrder(SalesOrder argSalesOrder)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsSalesOrderExists(argSalesOrder.SODocCode, argSalesOrder.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertSalesOrder(argSalesOrder, da, lstErr);
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
                    UpdateSalesOrder(argSalesOrder, da, lstErr);
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
        
        public string InsertSalesOrder(SalesOrder argSalesOrder, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[24];
            param[0] = new SqlParameter("@SODocCode", argSalesOrder.SODocCode);
            param[1] = new SqlParameter("@SODocDate", argSalesOrder.SODocDate);
            param[2] = new SqlParameter("@SOTypeCode", argSalesOrder.SOTypeCode);
            param[3] = new SqlParameter("@SalesofficeCode", argSalesOrder.SalesofficeCode);
            param[4] = new SqlParameter("@CustomerCode", argSalesOrder.CustomerCode);
            param[5] = new SqlParameter("@SalesOrganizationCode", argSalesOrder.SalesOrganizationCode);
            param[6] = new SqlParameter("@DistChannelCode", argSalesOrder.DistChannelCode);
            param[7] = new SqlParameter("@DivisionCode", argSalesOrder.DivisionCode);
            param[8] = new SqlParameter("@SalesGroupCode", argSalesOrder.SalesGroupCode);
            param[9] = new SqlParameter("@SalesRegionCode", argSalesOrder.SalesRegionCode);
            param[10] = new SqlParameter("@SalesDistrictCode", argSalesOrder.SalesDistrictCode);
            param[11] = new SqlParameter("@RefPODocCode", argSalesOrder.RefPODocCode);
            param[12] = new SqlParameter("@RefPODate", argSalesOrder.RefPODate);
            param[13] = new SqlParameter("@PostingDate", argSalesOrder.PostingDate);
            param[14] = new SqlParameter("@ReqDeliveryDate", argSalesOrder.ReqDeliveryDate);
            param[15] = new SqlParameter("@PriceDate", argSalesOrder.PriceDate);
            param[16] = new SqlParameter("@DeliveryBlocked", argSalesOrder.DeliveryBlocked);
            param[17] = new SqlParameter("@BillingBlocked", argSalesOrder.BillingBlocked);
            param[18] = new SqlParameter("@ClientCode", argSalesOrder.ClientCode);
            param[19] = new SqlParameter("@CreatedBy", argSalesOrder.CreatedBy);
            param[20] = new SqlParameter("@ModifiedBy", argSalesOrder.ModifiedBy);
            
            param[21] = new SqlParameter("@Type", SqlDbType.Char);
            param[21].Size = 1;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[22].Size = 255;
            param[22].Direction = ParameterDirection.Output;

            param[23] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[23].Size = 20;
            param[23].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSalesOrder", param);


            string strMessage = Convert.ToString(param[22].Value);
            string strType = Convert.ToString(param[21].Value);
            string strRetValue = Convert.ToString(param[23].Value);


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
        
        public string UpdateSalesOrder(SalesOrder argSalesOrder, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[24];
            param[0] = new SqlParameter("@SODocCode", argSalesOrder.SODocCode);
            param[1] = new SqlParameter("@SODocDate", argSalesOrder.SODocDate);
            param[2] = new SqlParameter("@SOTypeCode", argSalesOrder.SOTypeCode);
            param[3] = new SqlParameter("@SalesofficeCode", argSalesOrder.SalesofficeCode);
            param[4] = new SqlParameter("@CustomerCode", argSalesOrder.CustomerCode);
            param[5] = new SqlParameter("@SalesOrganizationCode", argSalesOrder.SalesOrganizationCode);
            param[6] = new SqlParameter("@DistChannelCode", argSalesOrder.DistChannelCode);
            param[7] = new SqlParameter("@DivisionCode", argSalesOrder.DivisionCode);
            param[8] = new SqlParameter("@SalesGroupCode", argSalesOrder.SalesGroupCode);
            param[9] = new SqlParameter("@SalesRegionCode", argSalesOrder.SalesRegionCode);
            param[10] = new SqlParameter("@SalesDistrictCode", argSalesOrder.SalesDistrictCode);
            param[11] = new SqlParameter("@RefPODocCode", argSalesOrder.RefPODocCode);
            param[12] = new SqlParameter("@RefPODate", argSalesOrder.RefPODate);
            param[13] = new SqlParameter("@PostingDate", argSalesOrder.PostingDate);
            param[14] = new SqlParameter("@ReqDeliveryDate", argSalesOrder.ReqDeliveryDate);
            param[15] = new SqlParameter("@PriceDate", argSalesOrder.PriceDate);
            param[16] = new SqlParameter("@DeliveryBlocked", argSalesOrder.DeliveryBlocked);
            param[17] = new SqlParameter("@BillingBlocked", argSalesOrder.BillingBlocked);
            param[18] = new SqlParameter("@ClientCode", argSalesOrder.ClientCode);
            param[19] = new SqlParameter("@CreatedBy", argSalesOrder.CreatedBy);
            param[20] = new SqlParameter("@ModifiedBy", argSalesOrder.ModifiedBy);
           
            param[21] = new SqlParameter("@Type", SqlDbType.Char);
            param[21].Size = 1;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[22].Size = 255;
            param[22].Direction = ParameterDirection.Output;

            param[23] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[23].Size = 20;
            param[23].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSalesOrder", param);


            string strMessage = Convert.ToString(param[22].Value);
            string strType = Convert.ToString(param[21].Value);
            string strRetValue = Convert.ToString(param[23].Value);


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
        
        public ICollection<ErrorHandler> DeleteSalesOrder(string argSODocCode, string argClientCode,int IisDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@SODocCode", argSODocCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted",IisDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteSalesOrder", param);


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
        
        public bool blnIsSalesOrderExists(string argSODocCode, string argClientCode)
        {
            bool IsSalesOrderExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesOrder(argSODocCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesOrderExists = true;
            }
            else
            {
                IsSalesOrderExists = false;
            }
            return IsSalesOrderExists;
        }

        public bool blnIsSalesOrderExists(string argSODocCode, string argClientCode, DataAccess da)
        {
            bool IsSalesOrderExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesOrder(argSODocCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesOrderExists = true;
            }
            else
            {
                IsSalesOrderExists = false;
            }
            return IsSalesOrderExists;
        }

    }
}