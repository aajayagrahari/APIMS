
//Created On :: 23, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using RSMApp_Organization;
using RSMApp_MM;

namespace RSMApp_SD
{
    public class SalesOrderManager
    {
        const string SalesOrderTable = "SalesOrder";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        SalesOrderDetailManager objSalesOrderDetailManager = new SalesOrderDetailManager();
        SOPriceConditionManager objSOPriceConditionManager = new SOPriceConditionManager();
        CharactersticsValueMasterManager objCharValueMasterManager = new CharactersticsValueMasterManager();
        SalesOrderPartnerManager objOrderPartnerManager = new SalesOrderPartnerManager();
        SOScheduleManager objSalesOrderScheduleManager = new SOScheduleManager();

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
        
        public ICollection<SalesOrder> colGetSalesOrder(string argSOTypeCode, string argClientCode)
        {
            List<SalesOrder> lst = new List<SalesOrder>();
            DataSet DataSetToFill = new DataSet();
            SalesOrder tSalesOrder = new SalesOrder();

            DataSetToFill = this.GetSalesOrderList(argSOTypeCode, argClientCode, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"));

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
               
        public DataSet GetSalesOrder4Combo(string argShipToParty, string argDeliveryDocTypeCode, string argToDocType, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@ShipToParty", argShipToParty);
            param[1] = new SqlParameter("@DeliveryDocTypeCode", argDeliveryDocTypeCode);
            param[2] = new SqlParameter("@ToDocType", argToDocType);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrder4Combo", param);

            return DataSetToFill;
        }

        public DataSet GetSalesOrder4DC(string argClientCode, string argDeliveryDocTypeCode, string argToDocType)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@DeliveryDocTypeCode", argDeliveryDocTypeCode);
            param[1] = new SqlParameter("@ToDocType", argToDocType);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrder4DC", param);

            return DataSetToFill;
        }

        public DataSet GetSalesOrder4BD(string argClientCode, string argBillingDocTypeCode, string argToDocType)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BillingDocTypeCode", argBillingDocTypeCode);
            param[1] = new SqlParameter("@ToDocType", argToDocType);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrder4BD", param);

            return DataSetToFill;
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

        public DataSet GetSalesOrderList(string argSOTypeCode, string argClientCode, DateTime argStartDate, DateTime argEndDate)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            param[2] = new SqlParameter("@StartDate", argStartDate);
            param[3] = new SqlParameter("@EndDate", argEndDate);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrder",param);
            return DataSetToFill;
        }
        
        private SalesOrder objCreateSalesOrder(DataRow dr)
        {
            SalesOrder tSalesOrder = new SalesOrder();

            tSalesOrder.SetObjectInfo(dr);

            return tSalesOrder;

        }

        public ICollection<ErrorHandler> SaveSalesOrder(SalesOrder argSalesOrder, ICollection<SalesOrderDetail> colSalesOrderDetail, ICollection<SOPriceCondition> colSOPriceCondition, ICollection<SOSchedule> colSOSchedule,  ICollection<CharactersticsValueMaster> colCharactersticsValueMaster, DataTable dtOrderPartner)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
                       
            string strretValue = "";
            try
            {
               // if (blnCheckBasicRules(argSalesOrder, lstErr) == true)
               // {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();

                    if (blnIsSalesOrderExists(argSalesOrder.SODocCode, argSalesOrder.ClientCode, da) == false)
                    {
                        strretValue = InsertSalesOrder(argSalesOrder, da, lstErr);
                    }
                    else
                    {
                        strretValue = UpdateSalesOrder(argSalesOrder, da, lstErr);
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
                                argSalesOrderDetail.SODocCode = Convert.ToString(strretValue);

                                if (argSalesOrderDetail.ErrFlag == 0)
                                {

                                    if (argSalesOrderDetail.IsDeleted == 0)
                                    {

                                        objSalesOrderDetailManager.SaveSalesOrderDetail(argSalesOrderDetail, da, lstErr);
                                    }
                                    else
                                    {
                                        objSalesOrderDetailManager.DeleteSalesOrderDetail(argSalesOrderDetail.SODocCode, argSalesOrderDetail.ItemNo, argSalesOrderDetail.ClientCode, da, lstErr);
                                    }

                                    if (colSOPriceCondition.Count > 0)
                                    {
                                        foreach (SOPriceCondition argSOPriceCon in colSOPriceCondition)
                                        {
                                            if (argSOPriceCon.ItemNo == argSalesOrderDetail.ItemNo)
                                            {
                                                argSOPriceCon.SODocCode = Convert.ToString(strretValue);

                                                if (argSOPriceCon.IsDeleted == 0)
                                                {
                                                    objSOPriceConditionManager.SaveSOPriceCondition(argSOPriceCon, da, lstErr);
                                                }
                                                else
                                                {
                                                    objSOPriceConditionManager.DeleteSOPriceCondition(argSOPriceCon.SODocCode, argSOPriceCon.ItemNo, argSOPriceCon.ConditionType, argSOPriceCon.ClientCode, argSOPriceCon.IsDeleted);
                                                }
                                            }
                                        }
                                    }

                                    if (colSOSchedule.Count > 0)
                                    {
                                        foreach (SOSchedule argSOSchedule in colSOSchedule)
                                        {
                                            if (argSOSchedule.ItemNo == argSalesOrderDetail.ItemNo)
                                            {
                                                argSOSchedule.SODocCode = Convert.ToString(strretValue);

                                                if (argSOSchedule.IsDeleted == 0)
                                                {
                                                    objSalesOrderScheduleManager.SaveSOSchedule(argSOSchedule, da, lstErr);
                                                }
                                                //else
                                                //{
                                                //    objSOPriceConditionManager.DeleteSOPriceCondition(argSOPriceCon.SODocCode, argSOPriceCon.ItemNo, argSOPriceCon.ConditionType, argSOPriceCon.ClientCode, argSOPriceCon.IsDeleted);
                                                //}
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

                        if (colCharactersticsValueMaster.Count > 0)
                        {
                            foreach (CharactersticsValueMaster argCharactersticsValueMaster in colCharactersticsValueMaster)
                            {
                                if (argCharactersticsValueMaster.ObjectKey.Contains("NEW") == true )
                                {
                                    argCharactersticsValueMaster.ObjectKey = Convert.ToString(strretValue).Trim() + argCharactersticsValueMaster.ObjectKey.Substring(3).Trim();
                                    argCharactersticsValueMaster.ObjectKey = argCharactersticsValueMaster.ObjectKey.Trim();
                                }

                                objCharValueMasterManager.SaveCharactersticsValueMaster(argCharactersticsValueMaster, da, lstErr);
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


                        foreach (DataRow dr in dtOrderPartner.Rows)
                        {
                            SalesOrderPartner objSalesOrderPartner = new SalesOrderPartner();

                            objSalesOrderPartner.SODocmentCode = strretValue;
                            objSalesOrderPartner.PFunctionCode = Convert.ToString(dr["PFunctionCode"]).Trim();
                            objSalesOrderPartner.CustomerCode = Convert.ToString(dr["CustomerCode"]).Trim();
                            objSalesOrderPartner.PartnerType = Convert.ToString(dr["PartnerType"]).Trim();
                            objSalesOrderPartner.ClientCode = Convert.ToString(dr["ClientCode"]).Trim();
                            objSalesOrderPartner.CreatedBy = Convert.ToString(dr["CreatedBy"]).Trim();
                            objSalesOrderPartner.ModifiedBy = Convert.ToString(dr["ModifiedBy"]).Trim();

                            objOrderPartnerManager.SaveSalesOrderPartner(objSalesOrderPartner, da, lstErr);

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



                    da.COMMIT_TRANSACTION();
               // }
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
            SqlParameter[] param = new SqlParameter[31];
            param[0] = new SqlParameter("@SODocCode", argSalesOrder.SODocCode);
            param[1] = new SqlParameter("@SODocDate", argSalesOrder.SODocDate);
            param[2] = new SqlParameter("@SOTypeCode", argSalesOrder.SOTypeCode);
            param[3] = new SqlParameter("@SalesofficeCode", argSalesOrder.SalesofficeCode);
            param[4] = new SqlParameter("@CustomerCode", argSalesOrder.CustomerCode);

            param[5] = new SqlParameter("@SoldToParty", argSalesOrder.SoldToParty);
            param[6] = new SqlParameter("@BillToParty", argSalesOrder.BillToParty);
            param[7] = new SqlParameter("@ShipToParty", argSalesOrder.ShipToParty);
            param[8] = new SqlParameter("@PayToParty", argSalesOrder.PayToParty);

            param[9] = new SqlParameter("@SalesOrganizationCode", argSalesOrder.SalesOrganizationCode);
            param[10] = new SqlParameter("@DistChannelCode", argSalesOrder.DistChannelCode);
            param[11] = new SqlParameter("@DivisionCode", argSalesOrder.DivisionCode);
            param[12] = new SqlParameter("@SalesGroupCode", argSalesOrder.SalesGroupCode);
            param[13] = new SqlParameter("@SalesRegionCode", argSalesOrder.SalesRegionCode);
            param[14] = new SqlParameter("@SalesDistrictCode", argSalesOrder.SalesDistrictCode);
            param[15] = new SqlParameter("@RefPODocCode", argSalesOrder.RefPODocCode);
            param[16] = new SqlParameter("@RefPODate", argSalesOrder.RefPODate);
            param[17] = new SqlParameter("@PostingDate", argSalesOrder.PostingDate);
            param[18] = new SqlParameter("@ReqDeliveryDate", argSalesOrder.ReqDeliveryDate);
            param[19] = new SqlParameter("@PriceDate", argSalesOrder.PriceDate);
            param[20] = new SqlParameter("@DeliveryBlocked", argSalesOrder.DeliveryBlocked);
            param[21] = new SqlParameter("@BillingBlocked", argSalesOrder.BillingBlocked);
            param[22] = new SqlParameter("@ReasonCode", argSalesOrder.ReasonCode);

            param[23] = new SqlParameter("@SAPTranID", argSalesOrder.SAPTranID);
            param[24] = new SqlParameter("@IsSAPPosted", argSalesOrder.IsSAPPosted);

            param[25] = new SqlParameter("@ClientCode", argSalesOrder.ClientCode);
            param[26] = new SqlParameter("@CreatedBy", argSalesOrder.CreatedBy);
            param[27] = new SqlParameter("@ModifiedBy", argSalesOrder.ModifiedBy);
            
            param[28] = new SqlParameter("@Type", SqlDbType.Char);
            param[28].Size = 1;
            param[28].Direction = ParameterDirection.Output;

            param[29] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[29].Size = 255;
            param[29].Direction = ParameterDirection.Output;

            param[30] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[30].Size = 20;
            param[30].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSalesOrder", param);


            string strMessage = Convert.ToString(param[29].Value);
            string strType = Convert.ToString(param[28].Value);
            string strRetValue = Convert.ToString(param[30].Value);


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
            SqlParameter[] param = new SqlParameter[31];
            param[0] = new SqlParameter("@SODocCode", argSalesOrder.SODocCode);
            param[1] = new SqlParameter("@SODocDate", argSalesOrder.SODocDate);
            param[2] = new SqlParameter("@SOTypeCode", argSalesOrder.SOTypeCode);
            param[3] = new SqlParameter("@SalesofficeCode", argSalesOrder.SalesofficeCode);
            param[4] = new SqlParameter("@CustomerCode", argSalesOrder.CustomerCode);

            param[5] = new SqlParameter("@SoldToParty", argSalesOrder.SoldToParty);
            param[6] = new SqlParameter("@BillToParty", argSalesOrder.BillToParty);
            param[7] = new SqlParameter("@ShipToParty", argSalesOrder.ShipToParty);
            param[8] = new SqlParameter("@PayToParty", argSalesOrder.PayToParty);


            param[9] = new SqlParameter("@SalesOrganizationCode", argSalesOrder.SalesOrganizationCode);
            param[10] = new SqlParameter("@DistChannelCode", argSalesOrder.DistChannelCode);
            param[11] = new SqlParameter("@DivisionCode", argSalesOrder.DivisionCode);
            param[12] = new SqlParameter("@SalesGroupCode", argSalesOrder.SalesGroupCode);
            param[13] = new SqlParameter("@SalesRegionCode", argSalesOrder.SalesRegionCode);
            param[14] = new SqlParameter("@SalesDistrictCode", argSalesOrder.SalesDistrictCode);
            param[15] = new SqlParameter("@RefPODocCode", argSalesOrder.RefPODocCode);
            param[16] = new SqlParameter("@RefPODate", argSalesOrder.RefPODate);
            param[17] = new SqlParameter("@PostingDate", argSalesOrder.PostingDate);
            param[18] = new SqlParameter("@ReqDeliveryDate", argSalesOrder.ReqDeliveryDate);
            param[19] = new SqlParameter("@PriceDate", argSalesOrder.PriceDate);
            param[20] = new SqlParameter("@DeliveryBlocked", argSalesOrder.DeliveryBlocked);
            param[21] = new SqlParameter("@BillingBlocked", argSalesOrder.BillingBlocked);
            param[22] = new SqlParameter("@ReasonCode", argSalesOrder.ReasonCode);

            param[23] = new SqlParameter("@SAPTranID", argSalesOrder.SAPTranID);
            param[24] = new SqlParameter("@IsSAPPosted", argSalesOrder.IsSAPPosted);


            param[25] = new SqlParameter("@ClientCode", argSalesOrder.ClientCode);
            param[26] = new SqlParameter("@CreatedBy", argSalesOrder.CreatedBy);
            param[27] = new SqlParameter("@ModifiedBy", argSalesOrder.ModifiedBy);
           
            param[28] = new SqlParameter("@Type", SqlDbType.Char);
            param[28].Size = 1;
            param[28].Direction = ParameterDirection.Output;

            param[29] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[29].Size = 255;
            param[29].Direction = ParameterDirection.Output;

            param[30] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[30].Size = 20;
            param[30].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSalesOrder", param);


            string strMessage = Convert.ToString(param[29].Value);
            string strType = Convert.ToString(param[28].Value);
            string strRetValue = Convert.ToString(param[30].Value);


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
        
        public string UpdateSalesOrder(string argSODocCode, string argClientCode, string argIsSAPPosted, string argSAPTranID)
          { 
              DataAccess da = new DataAccess();
              
              SqlParameter[] param = new SqlParameter[5];
              param[0] = new SqlParameter("@SODocCode", argSODocCode);
              param[1] = new SqlParameter("@ClientCode", argClientCode);
              param[2] = new SqlParameter("@IsSAPPosted", argIsSAPPosted);
              param[3] = new SqlParameter("@SAPTranID", argSAPTranID);
              
              param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
              param[4].Direction = ParameterDirection.ReturnValue;

              int i = da.ExecuteNonQuery("SAP_UpdateSalesOrder", param);

              string strRetValue = Convert.ToString(param[4].Value);

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

        public bool blnCheckBasicRules(SalesOrder argSalesOrder, List<ErrorHandler> lstErr)
        {
            bool retValue = true;
            SalesAreaManager objSalesAreaManager = new SalesAreaManager();
            if (objSalesAreaManager.blnIsSalesAreaExists(argSalesOrder.SalesOrganizationCode, argSalesOrder.DistChannelCode, argSalesOrder.DivisionCode, argSalesOrder.ClientCode) == false)
            {

                objErrorHandler.Type = ErrorConstant.strErrType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strInsertModule;
                objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                objErrorHandler.Message = "Sales Area does not exists.";
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";
                objErrorHandler.ReturnValue = "";
                lstErr.Add(objErrorHandler);

                retValue = false;
                objSalesAreaManager = null;
            }

            Customer_SalesAreaManager objCustSalesArea = new Customer_SalesAreaManager();

            if (objCustSalesArea.blnIsCustomer_SalesAreaExists(argSalesOrder.CustomerCode, argSalesOrder.SalesOrganizationCode, argSalesOrder.DivisionCode, argSalesOrder.DistChannelCode, argSalesOrder.ClientCode) == false)
            {
                objErrorHandler.Type = ErrorConstant.strErrType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strInsertModule;
                objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                objErrorHandler.Message = "Map Customer to Sales Area.";
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";
                objErrorHandler.ReturnValue = "";
                lstErr.Add(objErrorHandler);


                retValue = false;
                objCustSalesArea = null;
            }

            //SalesArea_SalesOfficeManager objSalesAreaoffice = new SalesArea_SalesOfficeManager();
            //if (objSalesAreaoffice.blnIsSalesArea_SalesOfficeExists(argSalesOrder.SalesOrganizationCode, argSalesOrder.DistChannelCode, argSalesOrder.DivisionCode, argSalesOrder.SalesofficeCode, argSalesOrder.ClientCode) == false)
            //{
            //    objErrorHandler.Type = ErrorConstant.strErrType;
            //    objErrorHandler.MsgId = 0;
            //    objErrorHandler.Module = ErrorConstant.strInsertModule;
            //    objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
            //    objErrorHandler.Message = "Map Sales Office to Sales Area.";
            //    objErrorHandler.RowNo = 0;
            //    objErrorHandler.FieldName = "";
            //    objErrorHandler.LogCode = "";
            //    objErrorHandler.ReturnValue = "";
            //    lstErr.Add(objErrorHandler);


            //    retValue = false;
            //    objSalesAreaoffice = null;
            //}

            //SalesOffice_SalesGroupManager objSalesOfficeGroup = new SalesOffice_SalesGroupManager();
            //if (objSalesOfficeGroup.blnIsSalesOffice_SalesGroupExists(argSalesOrder.SalesGroupCode, argSalesOrder.SalesofficeCode, argSalesOrder.ClientCode) == false)
            //{
            //    objErrorHandler.Type = ErrorConstant.strErrType;
            //    objErrorHandler.MsgId = 0;
            //    objErrorHandler.Module = ErrorConstant.strInsertModule;
            //    objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
            //    objErrorHandler.Message = "Map Sales Office to Sales Group.";
            //    objErrorHandler.RowNo = 0;
            //    objErrorHandler.FieldName = "";
            //    objErrorHandler.LogCode = "";
            //    objErrorHandler.ReturnValue = "";
            //    lstErr.Add(objErrorHandler);


            //    retValue = false;
            //    objSalesOfficeGroup = null;
            //}


            return retValue;
        }


    }
}