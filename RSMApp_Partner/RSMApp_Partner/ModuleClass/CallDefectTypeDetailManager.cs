
//Created On :: 20, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallDefectTypeDetailManager
    {
        const string CallDefectTypeDetailTable = "CallDefectTypeDetail";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public CallDefectTypeDetail objGetCallDefectTypeDetail(string argCallCode, int argItemNo, string argClientCode)
        {
            CallDefectTypeDetail argCallDefectTypeDetail = new CallDefectTypeDetail();
            DataSet DataSetToFill = new DataSet();

            if (argCallCode.Trim() == "")
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

            DataSetToFill = this.GetCallDefectTypeDetail(argCallCode, argItemNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallDefectTypeDetail = this.objCreateCallDefectTypeDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallDefectTypeDetail;
        }
        
        public ICollection<CallDefectTypeDetail> colGetCallDefectTypeDetail(string argCallCode, int argItemNo, string argClientCode)
        {
            List<CallDefectTypeDetail> lst = new List<CallDefectTypeDetail>();
            DataSet DataSetToFill = new DataSet();
            CallDefectTypeDetail tCallDefectTypeDetail = new CallDefectTypeDetail();

            DataSetToFill = this.GetCallDefectTypeDetail(argCallCode, argItemNo, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallDefectTypeDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        //public ICollection<CallDefectTypeDetail> colGetCallDefectTypeDetail(string argCallCode, int argItemNo, string argClientCode, List<CallDefectTypeDetail> lst)
        //{
        //    // List<SOPriceCondition> lst = new List<SOPriceCondition>();
        //    DataSet DataSetToFill = new DataSet();
        //    CallDefectTypeDetail tCallDefectTypeDetail = new CallDefectTypeDetail();

        //    DataSetToFill = this.GetCallDefectTypeDetail(argCallCode, argItemNo, argClientCode);

        //    if (DataSetToFill != null)
        //    {
        //        foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
        //        {
        //            lst.Add(objCreateCallDefectTypeDetail(dr));
        //        }
        //    }
        //    goto Finish;

        //Finish:
        //    DataSetToFill = null;


        //    return lst;
        //}
        
        //public ICollection<CallDefectTypeDetail> colGetCallDefectTypeDetail(string argCallCode, string argClientCode, List<CallDefectTypeDetail> lst)
        //{
        //    // List<SOPriceCondition> lst = new List<SOPriceCondition>();
        //    DataSet DataSetToFill = new DataSet();
        //    CallDefectTypeDetail tCallDefectTypeDetail = new CallDefectTypeDetail();

        //    DataSetToFill = this.GetCallDefectTypeDetail(argCallCode, argClientCode);

        //    if (DataSetToFill != null)
        //    {
        //        foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
        //        {
        //            lst.Add(objCreateCallDefectTypeDetail(dr));
        //        }
        //    }
        //    goto Finish;

        //Finish:
        //    DataSetToFill = null;


        //    return lst;
        //}

        public void colGetCallDefectTypeDetail(string argCallCode, string argClientCode, ref CallDefectTypeCol argCallDefectTypeCol)
        {
            DataSet DataSetToFill = new DataSet();
            CallDefectTypeDetail tCallDefectTypeDetail = new CallDefectTypeDetail();

            DataSetToFill = this.GetCallDefectTypeDetail(argCallCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argCallDefectTypeCol.colCallDefectTypeDetail.Add(objCreateCallDefectTypeDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

        }

        public void colGetCallDefectTypeDetail(string argCallCode, int argItemNo, string argClientCode, ref CallDefectTypeCol argCallDefectTypeCol)
        {
            DataSet DataSetToFill = new DataSet();
            CallDefectTypeDetail tCallDefectTypeDetail = new CallDefectTypeDetail();

            DataSetToFill = this.GetCallDefectTypeDetail(argCallCode, argItemNo, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argCallDefectTypeCol.colCallDefectTypeDetail.Add(objCreateCallDefectTypeDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

        }


        public DataSet GetCallDefectTypeDetail(string argCallCode, int argItemNo, int argDfItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@DfItemNo", argDfItemNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallDefectTypeDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallDefectTypeDetail(string argCallCode, int argItemNo,  int argDfItemNo, string argClientCode, DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@DfItemNo", argDfItemNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallDefectTypeDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallDefectTypeDetail(string argCallCode, int argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallDefectTypeDetail",param);
            return DataSetToFill;
        }

        public DataSet GetCallDefectTypeDetail(string argCallCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetCallDefectTypeDetail4Call", param);
            return DataSetToFill;
        }
           
        private CallDefectTypeDetail objCreateCallDefectTypeDetail(DataRow dr)
        {
            CallDefectTypeDetail tCallDefectTypeDetail = new CallDefectTypeDetail();

            tCallDefectTypeDetail.SetObjectInfo(dr);

            return tCallDefectTypeDetail;

        }

        public ICollection<ErrorHandler> SaveCallDefectTypeDetail(CallDefectTypeDetail argCallDefectTypeDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallDefectTypeDetailExists(argCallDefectTypeDetail.CallCode, argCallDefectTypeDetail.ItemNo, argCallDefectTypeDetail.DfItemNo, argCallDefectTypeDetail.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallDefectTypeDetail(argCallDefectTypeDetail, da, lstErr);
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
                    UpdateCallDefectTypeDetail(argCallDefectTypeDetail, da, lstErr);
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

        public void SaveCallDefectTypeDetail(CallDefectTypeDetail argCallDefectTypeDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCallDefectTypeDetailExists(argCallDefectTypeDetail.CallCode, argCallDefectTypeDetail.ItemNo, argCallDefectTypeDetail.DfItemNo, argCallDefectTypeDetail.ClientCode,da) == false)
                {
                    InsertCallDefectTypeDetail(argCallDefectTypeDetail, da, lstErr);
                }
                else
                {
                    UpdateCallDefectTypeDetail(argCallDefectTypeDetail, da, lstErr);
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

        public void InsertCallDefectTypeDetail(CallDefectTypeDetail argCallDefectTypeDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];

            param[0] = new SqlParameter("@CallCode", argCallDefectTypeDetail.CallCode);
            param[1] = new SqlParameter("@ItemNo", argCallDefectTypeDetail.ItemNo);
            param[2] = new SqlParameter("@DfItemNo", argCallDefectTypeDetail.DfItemNo);
            param[3] = new SqlParameter("@DefectTypeCode", argCallDefectTypeDetail.DefectTypeCode);
            param[4] = new SqlParameter("@DefectTypeDesc", argCallDefectTypeDetail.DefectTypeDesc);

            param[5] = new SqlParameter("@DMarkInd", argCallDefectTypeDetail.DMarkInd);
            param[6] = new SqlParameter("@RepairProcDocCode", argCallDefectTypeDetail.RepairProcDocCode);


            param[7] = new SqlParameter("@PartnerCode", argCallDefectTypeDetail.PartnerCode);
            param[8] = new SqlParameter("@ClientCode", argCallDefectTypeDetail.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argCallDefectTypeDetail.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argCallDefectTypeDetail.ModifiedBy);
       
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallDefectTypeDetail", param);


            string strMessage = Convert.ToString(param[12].Value);
            string strType = Convert.ToString(param[11].Value);
            string strRetValue = Convert.ToString(param[13].Value);


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

        public void UpdateCallDefectTypeDetail(CallDefectTypeDetail argCallDefectTypeDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@CallCode", argCallDefectTypeDetail.CallCode);
            param[1] = new SqlParameter("@ItemNo", argCallDefectTypeDetail.ItemNo);
            param[2] = new SqlParameter("@DfItemNo", argCallDefectTypeDetail.DfItemNo);
            param[3] = new SqlParameter("@DefectTypeCode", argCallDefectTypeDetail.DefectTypeCode);
            param[4] = new SqlParameter("@DefectTypeDesc", argCallDefectTypeDetail.DefectTypeDesc);

            param[5] = new SqlParameter("@DMarkInd", argCallDefectTypeDetail.DMarkInd);
            param[6] = new SqlParameter("@RepairProcDocCode", argCallDefectTypeDetail.RepairProcDocCode);

            param[7] = new SqlParameter("@PartnerCode", argCallDefectTypeDetail.PartnerCode);
            param[8] = new SqlParameter("@ClientCode", argCallDefectTypeDetail.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argCallDefectTypeDetail.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argCallDefectTypeDetail.ModifiedBy);

            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallDefectTypeDetail", param);


            string strMessage = Convert.ToString(param[12].Value);
            string strType = Convert.ToString(param[11].Value);
            string strRetValue = Convert.ToString(param[13].Value);


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

        public ICollection<ErrorHandler> DeleteCallDefectTypeDetail(string argCallCode, int argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CallCode", argCallCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCallDefectTypeDetail", param);


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

        public void DeleteCallDefectTypeDetail(string argCallCode, int argItemNo, int argDfItemNo, string argPartnerCode,  string argClientCode, int iIsDeleted, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@CallCode", argCallCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@DfItemNo", argDfItemNo);
                param[3] = new SqlParameter("@PartnerCode", argPartnerCode);                
                param[4] = new SqlParameter("@ClientCode", argClientCode);
                param[5] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[6] = new SqlParameter("@Type", SqlDbType.Char);
                param[6].Size = 1;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[7].Size = 255;
                param[7].Direction = ParameterDirection.Output;

                param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[8].Size = 20;
                param[8].Direction = ParameterDirection.Output;

                int i = da.NExecuteNonQuery("Proc_DeleteCallDefectTypeDetail", param);


                string strMessage = Convert.ToString(param[7].Value);
                string strType = Convert.ToString(param[6].Value);
                string strRetValue = Convert.ToString(param[8].Value);


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
        
        public bool blnIsCallDefectTypeDetailExists(string argCallCode, int argItemNo, int argDfItemNo, string argClientCode)
        {
            bool IsCallDefectTypeDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetCallDefectTypeDetail(argCallCode, argItemNo, argDfItemNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallDefectTypeDetailExists = true;
            }
            else
            {
                IsCallDefectTypeDetailExists = false;
            }
            return IsCallDefectTypeDetailExists;
        }

        public bool blnIsCallDefectTypeDetailExists(string argCallCode, int argItemNo, int argDfItemNo, string argClientCode, DataAccess da)
        {
            bool IsCallDefectTypeDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetCallDefectTypeDetail(argCallCode, argItemNo, argDfItemNo, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallDefectTypeDetailExists = true;
            }
            else
            {
                IsCallDefectTypeDetailExists = false;
            }
            return IsCallDefectTypeDetailExists;
        }
    }
}