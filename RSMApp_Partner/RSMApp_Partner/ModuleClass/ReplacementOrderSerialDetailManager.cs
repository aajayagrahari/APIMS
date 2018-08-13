
//Created On :: 07, January, 2013
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class ReplacementOrderSerialDetailManager
    {
        const string ReplacementOrderSerialDetailTable = "ReplacementOrderSerialDetail";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public ReplacementOrderSerialDetail objGetReplacementOrderSerialDetail(string argRepOrderCode, int argRepOrderItemNo, string argSerialNo1, string argClientCode)
        {
            ReplacementOrderSerialDetail argReplacementOrderSerialDetail = new ReplacementOrderSerialDetail();
            DataSet DataSetToFill = new DataSet();

            if (argRepOrderCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argRepOrderItemNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argSerialNo1.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetReplacementOrderSerialDetail(argRepOrderCode, argRepOrderItemNo, argSerialNo1, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argReplacementOrderSerialDetail = this.objCreateReplacementOrderSerialDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;
            
            return argReplacementOrderSerialDetail;
        }
        
        public ICollection<ReplacementOrderSerialDetail> colGetReplacementOrderSerialDetail(string argRepOrderCode, int argRepOrderItemNo, string argClientCode)
        {
            List<ReplacementOrderSerialDetail> lst = new List<ReplacementOrderSerialDetail>();
            DataSet DataSetToFill = new DataSet();
            ReplacementOrderSerialDetail tReplacementOrderSerialDetail = new ReplacementOrderSerialDetail();

            DataSetToFill = this.GetReplacementOrderSerialDetail(argRepOrderCode, argRepOrderItemNo, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateReplacementOrderSerialDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetReplacementOrderSerialDetail(string argRepOrderCode, int argRepOrderItemNo, string argSerialNo1, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@RepOrderCode", argRepOrderCode);
            param[1] = new SqlParameter("@RepOrderItemNo", argRepOrderItemNo);
            param[2] = new SqlParameter("@SerialNo1", argSerialNo1);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetReplacementOrderSerialDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetReplacementOrderSerialDetail(string argRepOrderCode, int argRepOrderItemNo, string argSerialNo1, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@RepOrderCode", argRepOrderCode);
            param[1] = new SqlParameter("@RepOrderItemNo", argRepOrderItemNo);
            param[2] = new SqlParameter("@SerialNo1", argSerialNo1);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetReplacementOrderSerialDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetReplacementOrderSerialDetail(string argRepOrderCode, int argRepOrderItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RepOrderCode", argRepOrderCode);
            param[1] = new SqlParameter("@RepOrderItemNo", argRepOrderItemNo);            
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetReplacementOrderSerialDetail", param);
            return DataSetToFill;
        }
        
        private ReplacementOrderSerialDetail objCreateReplacementOrderSerialDetail(DataRow dr)
        {
            ReplacementOrderSerialDetail tReplacementOrderSerialDetail = new ReplacementOrderSerialDetail();

            tReplacementOrderSerialDetail.SetObjectInfo(dr);

            return tReplacementOrderSerialDetail;

        }

        public ICollection<ErrorHandler> SaveReplacementOrderSerialDetail(ReplacementOrderSerialDetail argReplacementOrderSerialDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsReplacementOrderSerialDetailExists(argReplacementOrderSerialDetail.RepOrderCode, argReplacementOrderSerialDetail.RepOrderItemNo, argReplacementOrderSerialDetail.SerialNo1, argReplacementOrderSerialDetail.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertReplacementOrderSerialDetail(argReplacementOrderSerialDetail, da, lstErr);
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
                    UpdateReplacementOrderSerialDetail(argReplacementOrderSerialDetail, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
                    }
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

        public void SaveReplacementOrderSerialDetail(ReplacementOrderSerialDetail argReplacementOrderSerialDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsReplacementOrderSerialDetailExists(argReplacementOrderSerialDetail.RepOrderCode, argReplacementOrderSerialDetail.RepOrderItemNo, argReplacementOrderSerialDetail.SerialNo1, argReplacementOrderSerialDetail.ClientCode, da) == false)
                {
                    InsertReplacementOrderSerialDetail(argReplacementOrderSerialDetail, da, lstErr);
                }
                else
                {
                    UpdateReplacementOrderSerialDetail(argReplacementOrderSerialDetail, da,lstErr);
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
        
        public void InsertReplacementOrderSerialDetail(ReplacementOrderSerialDetail argReplacementOrderSerialDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[21];
            param[0] = new SqlParameter("@RepOrderCode", argReplacementOrderSerialDetail.RepOrderCode);
            param[1] = new SqlParameter("@RepOrderItemNo", argReplacementOrderSerialDetail.RepOrderItemNo);
            param[2] = new SqlParameter("@SerialNo1", argReplacementOrderSerialDetail.SerialNo1);
            param[3] = new SqlParameter("@SerialNo2", argReplacementOrderSerialDetail.SerialNo2);
            param[4] = new SqlParameter("@MaterialCode", argReplacementOrderSerialDetail.MaterialCode);
            param[5] = new SqlParameter("@MatGroup1Code", argReplacementOrderSerialDetail.MatGroup1Code);
            param[6] = new SqlParameter("@PartnerCode", argReplacementOrderSerialDetail.PartnerCode);
            param[7] = new SqlParameter("@PartnerEmployeeCode", argReplacementOrderSerialDetail.PartnerEmployeeCode);
            param[8] = new SqlParameter("@ToPartnerCode", argReplacementOrderSerialDetail.ToPartnerCode);
            param[9] = new SqlParameter("@OrderQty", argReplacementOrderSerialDetail.OrderQty);
            param[10] = new SqlParameter("@UOMCode", argReplacementOrderSerialDetail.UOMCode);
            param[11] = new SqlParameter("@ReceivedQty", argReplacementOrderSerialDetail.ReceivedQty);
            param[12] = new SqlParameter("@RepOrderStatus", argReplacementOrderSerialDetail.RepOrderStatus);
            param[13] = new SqlParameter("@IssueDocCode", argReplacementOrderSerialDetail.IssueDocCode);
            param[14] = new SqlParameter("@IssueDocItemNo", argReplacementOrderSerialDetail.IssueDocItemNo);
            param[15] = new SqlParameter("@ClientCode", argReplacementOrderSerialDetail.ClientCode);
            param[16] = new SqlParameter("@CreatedBy", argReplacementOrderSerialDetail.CreatedBy);
            param[17] = new SqlParameter("@ModifiedBy", argReplacementOrderSerialDetail.ModifiedBy);
        
            param[18] = new SqlParameter("@Type", SqlDbType.Char);
            param[18].Size = 1;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[19].Size = 255;
            param[19].Direction = ParameterDirection.Output;

            param[20] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[20].Size = 20;
            param[20].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertReplacementOrderSerialDetail", param);


            string strMessage = Convert.ToString(param[19].Value);
            string strType = Convert.ToString(param[18].Value);
            string strRetValue = Convert.ToString(param[20].Value);


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

        public void UpdateReplacementOrderSerialDetail(ReplacementOrderSerialDetail argReplacementOrderSerialDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[21];
            param[0] = new SqlParameter("@RepOrderCode", argReplacementOrderSerialDetail.RepOrderCode);
            param[1] = new SqlParameter("@RepOrderItemNo", argReplacementOrderSerialDetail.RepOrderItemNo);
            param[2] = new SqlParameter("@SerialNo1", argReplacementOrderSerialDetail.SerialNo1);
            param[3] = new SqlParameter("@SerialNo2", argReplacementOrderSerialDetail.SerialNo2);
            param[4] = new SqlParameter("@MaterialCode", argReplacementOrderSerialDetail.MaterialCode);
            param[5] = new SqlParameter("@MatGroup1Code", argReplacementOrderSerialDetail.MatGroup1Code);
            param[6] = new SqlParameter("@PartnerCode", argReplacementOrderSerialDetail.PartnerCode);
            param[7] = new SqlParameter("@PartnerEmployeeCode", argReplacementOrderSerialDetail.PartnerEmployeeCode);
            param[8] = new SqlParameter("@ToPartnerCode", argReplacementOrderSerialDetail.ToPartnerCode);
            param[9] = new SqlParameter("@OrderQty", argReplacementOrderSerialDetail.OrderQty);
            param[10] = new SqlParameter("@UOMCode", argReplacementOrderSerialDetail.UOMCode);
            param[11] = new SqlParameter("@ReceivedQty", argReplacementOrderSerialDetail.ReceivedQty);
            param[12] = new SqlParameter("@RepOrderStatus", argReplacementOrderSerialDetail.RepOrderStatus);
            param[13] = new SqlParameter("@IssueDocCode", argReplacementOrderSerialDetail.IssueDocCode);
            param[14] = new SqlParameter("@IssueDocItemNo", argReplacementOrderSerialDetail.IssueDocItemNo);
            param[15] = new SqlParameter("@ClientCode", argReplacementOrderSerialDetail.ClientCode);
            param[16] = new SqlParameter("@CreatedBy", argReplacementOrderSerialDetail.CreatedBy);
            param[17] = new SqlParameter("@ModifiedBy", argReplacementOrderSerialDetail.ModifiedBy);
           
            param[18] = new SqlParameter("@Type", SqlDbType.Char);
            param[18].Size = 1;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[19].Size = 255;
            param[19].Direction = ParameterDirection.Output;

            param[20] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[20].Size = 20;
            param[20].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateReplacementOrderSerialDetail", param);


            string strMessage = Convert.ToString(param[19].Value);
            string strType = Convert.ToString(param[18].Value);
            string strRetValue = Convert.ToString(param[20].Value);


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
        
        public ICollection<ErrorHandler> DeleteReplacementOrderSerialDetail(string argRepOrderCode, int argRepOrderItemNo, string argSerialNo1, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@RepOrderCode", argRepOrderCode);
                param[1] = new SqlParameter("@RepOrderItemNo", argRepOrderItemNo);
                param[2] = new SqlParameter("@SerialNo1", argSerialNo1);
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
                int i = da.ExecuteNonQuery("Proc_DeleteReplacementOrderSerialDetail", param);


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
            return lstErr;

        }

        public bool blnIsReplacementOrderSerialDetailExists(string argRepOrderCode, int argRepOrderItemNo, string argSerialNo1, string argClientCode)
        {
            bool IsReplacementOrderSerialDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetReplacementOrderSerialDetail(argRepOrderCode, argRepOrderItemNo, argSerialNo1, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsReplacementOrderSerialDetailExists = true;
            }
            else
            {
                IsReplacementOrderSerialDetailExists = false;
            }
            return IsReplacementOrderSerialDetailExists;
        }

        public bool blnIsReplacementOrderSerialDetailExists(string argRepOrderCode, int argRepOrderItemNo, string argSerialNo1, string argClientCode, DataAccess da)
        {
            bool IsReplacementOrderSerialDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetReplacementOrderSerialDetail(argRepOrderCode, argRepOrderItemNo, argSerialNo1, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsReplacementOrderSerialDetailExists = true;
            }
            else
            {
                IsReplacementOrderSerialDetailExists = false;
            }
            return IsReplacementOrderSerialDetailExists;
        }


    }
}