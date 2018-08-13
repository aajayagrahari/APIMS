
//Created On :: 21, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallRepairTypeDetailManager
    {
        const string CallRepairTypeDetailTable = "CallRepairTypeDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CallRepairTypeDetail objGetCallRepairTypeDetail(string argCallCode, int argItemNo, int argRPItemNo, string argClientCode)
        {
            CallRepairTypeDetail argCallRepairTypeDetail = new CallRepairTypeDetail();
            DataSet DataSetToFill = new DataSet();

            if (argCallCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argItemNo <= 0)
            {
                goto ErrorHandler;
            }

            if (argRPItemNo <= 0)
            {
                goto ErrorHandler;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            DataSetToFill = this.GetCallRepairTypeDetail(argCallCode, argItemNo, argRPItemNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallRepairTypeDetail = this.objCreateCallRepairTypeDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandler:

        Finish:
            DataSetToFill = null;


            return argCallRepairTypeDetail;
        }
        
        //public ICollection<CallRepairTypeDetail> colGetCallRepairTypeDetail(string argCallCode, int argItemNo, string argClientCode)
        //{
        //    List<CallRepairTypeDetail> lst = new List<CallRepairTypeDetail>();
        //    DataSet DataSetToFill = new DataSet();
        //    CallRepairTypeDetail tCallRepairTypeDetail = new CallRepairTypeDetail();

        //    DataSetToFill = this.GetCallRepairTypeDetail(argCallCode, argItemNo, argClientCode);

        //    if (DataSetToFill != null)
        //    {
        //        foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
        //        {
        //            lst.Add(objCreateCallRepairTypeDetail(dr));
        //        }
        //    }
        //    goto Finish;

        //Finish:
        //    DataSetToFill = null;


        //    return lst;
        //}

        public void colGetCallRepairTypeDetail(string argCallCode, int argItemNo, string argClientCode, ref CallRepairTypeCol argCallRepairTypeCol)
        {
            DataSet DataSetToFill = new DataSet();
            CallRepairTypeDetail tCallRepairTypeDetail = new CallRepairTypeDetail();
            DataSetToFill = this.GetCallRepairTypeDetail(argCallCode, argItemNo, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argCallRepairTypeCol.colCallRepairTypeDetail.Add(objCreateCallRepairTypeDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

        }

        public DataSet GetCallRepairTypeDetail(string argCallCode, int argItemNo, int argRPItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@RPItemNo", argRPItemNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallRepairTypeDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallRepairTypeDetail(string argCallCode, int argItemNo, int argRPItemNo, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@RPItemNo", argRPItemNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallRepairTypeDetail4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCallRepairTypeDetail(string argCallCode, int argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallRepairTypeDetail", param);
            return DataSetToFill;
        }
        
        private CallRepairTypeDetail objCreateCallRepairTypeDetail(DataRow dr)
        {
            CallRepairTypeDetail tCallRepairTypeDetail = new CallRepairTypeDetail();

            tCallRepairTypeDetail.SetObjectInfo(dr);

            return tCallRepairTypeDetail;

        }
        
        public ICollection<ErrorHandler> SaveCallRepairTypeDetail(CallRepairTypeDetail argCallRepairTypeDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallRepairTypeDetailExists(argCallRepairTypeDetail.CallCode, argCallRepairTypeDetail.ItemNo, argCallRepairTypeDetail.RPItemNo, argCallRepairTypeDetail.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallRepairTypeDetail(argCallRepairTypeDetail, da, lstErr);
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
                    UpdateCallRepairTypeDetail(argCallRepairTypeDetail, da, lstErr);
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

        public void SaveCallRepairTypeDetail(CallRepairTypeDetail argCallRepairTypeDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCallRepairTypeDetailExists(argCallRepairTypeDetail.CallCode, argCallRepairTypeDetail.ItemNo, argCallRepairTypeDetail.RPItemNo, argCallRepairTypeDetail.ClientCode, da) == false)
                {
                    InsertCallRepairTypeDetail(argCallRepairTypeDetail, da, lstErr);
                }
                else
                {
                    UpdateCallRepairTypeDetail(argCallRepairTypeDetail, da, lstErr);
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

        public void InsertCallRepairTypeDetail(CallRepairTypeDetail argCallRepairTypeDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@CallCode", argCallRepairTypeDetail.CallCode);
            param[1] = new SqlParameter("@ItemNo", argCallRepairTypeDetail.ItemNo);
            param[2] = new SqlParameter("@RPItemNo", argCallRepairTypeDetail.RPItemNo);
            param[3] = new SqlParameter("@RepairTypeCode", argCallRepairTypeDetail.RepairTypeCode);
            param[4] = new SqlParameter("@RepairTypeDesc", argCallRepairTypeDetail.RepairTypeDesc);

            param[5] = new SqlParameter("@DefectTypeCode", argCallRepairTypeDetail.DefectTypeCode);
            param[6] = new SqlParameter("@ServiceLevel", argCallRepairTypeDetail.ServiceLevel);

            param[7] = new SqlParameter("@RMarkInd", argCallRepairTypeDetail.RMarkInd);
            param[8] = new SqlParameter("@RepairProcDocCode", argCallRepairTypeDetail.RepairProcDocCode);

            param[9] = new SqlParameter("@PartnerCode", argCallRepairTypeDetail.PartnerCode);
            param[10] = new SqlParameter("@ClientCode", argCallRepairTypeDetail.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argCallRepairTypeDetail.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argCallRepairTypeDetail.ModifiedBy);
           
            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallRepairTypeDetail", param);


            string strMessage = Convert.ToString(param[14].Value);
            string strType = Convert.ToString(param[13].Value);
            string strRetValue = Convert.ToString(param[15].Value);


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
        
        public void UpdateCallRepairTypeDetail(CallRepairTypeDetail argCallRepairTypeDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@CallCode", argCallRepairTypeDetail.CallCode);
            param[1] = new SqlParameter("@ItemNo", argCallRepairTypeDetail.ItemNo);
            param[2] = new SqlParameter("@RPItemNo", argCallRepairTypeDetail.RPItemNo);
            param[3] = new SqlParameter("@RepairTypeCode", argCallRepairTypeDetail.RepairTypeCode);
            param[4] = new SqlParameter("@RepairTypeDesc", argCallRepairTypeDetail.RepairTypeDesc);
            param[5] = new SqlParameter("@DefectTypeCode", argCallRepairTypeDetail.DefectTypeCode);
            param[6] = new SqlParameter("@ServiceLevel", argCallRepairTypeDetail.ServiceLevel);

            param[7] = new SqlParameter("@RMarkInd", argCallRepairTypeDetail.RMarkInd);
            param[8] = new SqlParameter("@RepairProcDocCode", argCallRepairTypeDetail.RepairProcDocCode);

            param[9] = new SqlParameter("@PartnerCode", argCallRepairTypeDetail.PartnerCode);
            param[10] = new SqlParameter("@ClientCode", argCallRepairTypeDetail.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argCallRepairTypeDetail.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argCallRepairTypeDetail.ModifiedBy);

            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallRepairTypeDetail", param);


            string strMessage = Convert.ToString(param[14].Value);
            string strType = Convert.ToString(param[13].Value);
            string strRetValue = Convert.ToString(param[15].Value);
            
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
        
        public ICollection<ErrorHandler> DeleteCallRepairTypeDetail(string argCallCode, int argItemNo, int argRPItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@CallCode", argCallCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@RPItemNo", argRPItemNo);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCallRepairTypeDetail", param);


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

        public void DeleteCallRepairTypeDetail(string argCallCode, int argItemNo, int argRPItemNo, int argIsDeleted, string argClientCode, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@CallCode", argCallCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@RPItemNo", argRPItemNo);
                param[3] = new SqlParameter("@IsDeleted", argIsDeleted);
                param[4] = new SqlParameter("@ClientCode", argClientCode);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.NExecuteNonQuery("Proc_DeleteCallRepairTypeDetail", param);


                string strMessage = Convert.ToString(param[6].Value);
                string strType = Convert.ToString(param[5].Value);
                string strRetValue = Convert.ToString(param[7].Value);


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

        public bool blnIsCallRepairTypeDetailExists(string argCallCode, int argItemNo, int argRPItemNo, string argClientCode)
        {
            bool IsCallRepairTypeDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetCallRepairTypeDetail(argCallCode, argItemNo, argRPItemNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallRepairTypeDetailExists = true;
            }
            else
            {
                IsCallRepairTypeDetailExists = false;
            }
            return IsCallRepairTypeDetailExists;
        }

        public bool blnIsCallRepairTypeDetailExists(string argCallCode, int argItemNo, int argRPItemNo, string argClientCode, DataAccess da)
        {
            bool IsCallRepairTypeDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetCallRepairTypeDetail(argCallCode, argItemNo, argRPItemNo, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallRepairTypeDetailExists = true;
            }
            else
            {
                IsCallRepairTypeDetailExists = false;
            }
            return IsCallRepairTypeDetailExists;
        }

    }
}