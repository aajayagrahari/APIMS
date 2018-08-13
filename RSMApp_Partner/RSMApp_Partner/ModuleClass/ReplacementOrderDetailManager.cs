
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
    public class ReplacementOrderDetailManager
    {
        const string ReplacementOrderDetailTable = "ReplacementOrderDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public ReplacementOrderDetail objGetReplacementOrderDetail(string argRepOrderCode, int argRepOrderItemNo, string argClientCode)
        {
            ReplacementOrderDetail argReplacementOrderDetail = new ReplacementOrderDetail();
            DataSet DataSetToFill = new DataSet();

            if (argRepOrderCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argRepOrderItemNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetReplacementOrderDetail(argRepOrderCode, argRepOrderItemNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argReplacementOrderDetail = this.objCreateReplacementOrderDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argReplacementOrderDetail;
        }
        
        public ICollection<ReplacementOrderDetail> colGetReplacementOrderDetail(string argRepOrderCode, string argClientCode)
        {
            List<ReplacementOrderDetail> lst = new List<ReplacementOrderDetail>();
            DataSet DataSetToFill = new DataSet();
            ReplacementOrderDetail tReplacementOrderDetail = new ReplacementOrderDetail();

            DataSetToFill = this.GetReplacementOrderDetail(argRepOrderCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateReplacementOrderDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetReplacementOrderDetail(string argRepOrderCode, int argRepOrderItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RepOrderCode", argRepOrderCode);
            param[1] = new SqlParameter("@RepOrderItemNo", argRepOrderItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetReplacementOrderDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetReplacementOrderDetail(string argRepOrderCode, int argRepOrderItemNo, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RepOrderCode", argRepOrderCode);
            param[1] = new SqlParameter("@RepOrderItemNo", argRepOrderItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetReplacementOrderDetail4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetReplacementOrderDetail(string argRepOrderCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RepOrderCode", argRepOrderCode);            
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetReplacementOrderDetail", param);
            return DataSetToFill;
        }

        public DataSet GetRepOrderMatGroup4Issue(string argRepOrderCode, string argToPartnerCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@RepOrderCode", argRepOrderCode);
            param[1] = new SqlParameter("@ToPartnerCode", argToPartnerCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepOrderMatGroup4Issue", param);
            return DataSetToFill;
        }

        public DataSet GetRepOrderMaterial4Issue(string argMatGroup1Code, string argRepOrderCode, string argToPartnerCode, string argPartnerCode, string argStoreCode, string argStockIndicator, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@RepOrderCode", argRepOrderCode);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@ToPartnerCode", argToPartnerCode);
            param[3] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[4] = new SqlParameter("@StoreCode", argStoreCode);
            param[5] = new SqlParameter("@StockIndicator", argStockIndicator);
            param[6] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepOrderMaterial4Issue", param);
            return DataSetToFill;
        }

        private ReplacementOrderDetail objCreateReplacementOrderDetail(DataRow dr)
        {
            ReplacementOrderDetail tReplacementOrderDetail = new ReplacementOrderDetail();

            tReplacementOrderDetail.SetObjectInfo(dr);

            return tReplacementOrderDetail;

        }
        
        public ICollection<ErrorHandler> SaveReplacementOrderDetail(ReplacementOrderDetail argReplacementOrderDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsReplacementOrderDetailExists(argReplacementOrderDetail.RepOrderCode, argReplacementOrderDetail.RepOrderItemNo, argReplacementOrderDetail.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertReplacementOrderDetail(argReplacementOrderDetail, da, lstErr);
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
                    UpdateReplacementOrderDetail(argReplacementOrderDetail, da, lstErr);
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

        public void SaveReplacementOrderDetail(ReplacementOrderDetail argReplacementOrderDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsReplacementOrderDetailExists(argReplacementOrderDetail.RepOrderCode, argReplacementOrderDetail.RepOrderItemNo, argReplacementOrderDetail.ClientCode, da) == false)
                {
                    InsertReplacementOrderDetail(argReplacementOrderDetail, da, lstErr);
                }
                else
                {
                    UpdateReplacementOrderDetail(argReplacementOrderDetail, da, lstErr);
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

        public void InsertReplacementOrderDetail(ReplacementOrderDetail argReplacementOrderDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@RepOrderCode", argReplacementOrderDetail.RepOrderCode);
            param[1] = new SqlParameter("@RepOrderItemNo", argReplacementOrderDetail.RepOrderItemNo);
            param[2] = new SqlParameter("@MaterialCode", argReplacementOrderDetail.MaterialCode);
            param[3] = new SqlParameter("@MatGroup1Code", argReplacementOrderDetail.MatGroup1Code);
            param[4] = new SqlParameter("@PartnerCode", argReplacementOrderDetail.PartnerCode);
            param[5] = new SqlParameter("@PartnerEmployeeCode", argReplacementOrderDetail.PartnerEmployeeCode);
            param[6] = new SqlParameter("@OrderQty", argReplacementOrderDetail.OrderQty);
            param[7] = new SqlParameter("@UOMCode", argReplacementOrderDetail.UOMCode);
            param[8] = new SqlParameter("@ReceivedQty", argReplacementOrderDetail.ReceivedQty);
            param[9] = new SqlParameter("@RepOrderStatus", argReplacementOrderDetail.RepOrderStatus);
            param[10] = new SqlParameter("@IssueDocCode", argReplacementOrderDetail.IssueDocCode);
            param[11] = new SqlParameter("@IssueDocItemNo", argReplacementOrderDetail.IssueDocItemNo);

            param[12] = new SqlParameter("@ToPartnerCode", argReplacementOrderDetail.ToPartnerCode);

            param[13] = new SqlParameter("@ClientCode", argReplacementOrderDetail.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argReplacementOrderDetail.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argReplacementOrderDetail.ModifiedBy);
          
            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertReplacementOrderDetail", param);


            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);


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
        
        public void UpdateReplacementOrderDetail(ReplacementOrderDetail argReplacementOrderDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@RepOrderCode", argReplacementOrderDetail.RepOrderCode);
            param[1] = new SqlParameter("@RepOrderItemNo", argReplacementOrderDetail.RepOrderItemNo);
            param[2] = new SqlParameter("@MaterialCode", argReplacementOrderDetail.MaterialCode);
            param[3] = new SqlParameter("@MatGroup1Code", argReplacementOrderDetail.MatGroup1Code);
            param[4] = new SqlParameter("@PartnerCode", argReplacementOrderDetail.PartnerCode);
            param[5] = new SqlParameter("@PartnerEmployeeCode", argReplacementOrderDetail.PartnerEmployeeCode);
            param[6] = new SqlParameter("@OrderQty", argReplacementOrderDetail.OrderQty);
            param[7] = new SqlParameter("@UOMCode", argReplacementOrderDetail.UOMCode);
            param[8] = new SqlParameter("@ReceivedQty", argReplacementOrderDetail.ReceivedQty);
            param[9] = new SqlParameter("@RepOrderStatus", argReplacementOrderDetail.RepOrderStatus);
            param[10] = new SqlParameter("@IssueDocCode", argReplacementOrderDetail.IssueDocCode);
            param[11] = new SqlParameter("@IssueDocItemNo", argReplacementOrderDetail.IssueDocItemNo);

            param[12] = new SqlParameter("@ToPartnerCode", argReplacementOrderDetail.ToPartnerCode);

            param[13] = new SqlParameter("@ClientCode", argReplacementOrderDetail.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argReplacementOrderDetail.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argReplacementOrderDetail.ModifiedBy);
           
            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateReplacementOrderDetail", param);


            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);


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
        
        public ICollection<ErrorHandler> DeleteReplacementOrderDetail(string argRepOrderCode, int argRepOrderItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@RepOrderCode", argRepOrderCode);
                param[1] = new SqlParameter("@RepOrderItemNo", argRepOrderItemNo);
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
                int i = da.ExecuteNonQuery("Proc_DeleteReplacementOrderDetail", param);


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
        
        public bool blnIsReplacementOrderDetailExists(string argRepOrderCode, int argRepOrderItemNo, string argClientCode)
        {
            bool IsReplacementOrderDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetReplacementOrderDetail(argRepOrderCode, argRepOrderItemNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsReplacementOrderDetailExists = true;
            }
            else
            {
                IsReplacementOrderDetailExists = false;
            }
            return IsReplacementOrderDetailExists;
        }

        public bool blnIsReplacementOrderDetailExists(string argRepOrderCode, int argRepOrderItemNo, string argClientCode, DataAccess da)
        {
            bool IsReplacementOrderDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetReplacementOrderDetail(argRepOrderCode, argRepOrderItemNo, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsReplacementOrderDetailExists = true;
            }
            else
            {
                IsReplacementOrderDetailExists = false;
            }
            return IsReplacementOrderDetailExists;
        }
    }
}