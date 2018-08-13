
//Created On :: 16, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class WarrantyDetailManager
    {
        const string WarrantyDetailTable = "WarrantyDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public WarrantyDetail objGetWarrantyDetail(string argWarrantyCode, int argItemNo, string argClientCode)
        {
            WarrantyDetail argWarrantyDetail = new WarrantyDetail();
            DataSet DataSetToFill = new DataSet();

            if (argWarrantyCode.Trim() == "")
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

            DataSetToFill = this.GetWarrantyDetail(argWarrantyCode, argItemNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argWarrantyDetail = this.objCreateWarrantyDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argWarrantyDetail;
        }

        public ICollection<WarrantyDetail> colGetWarrantyDetail(string argWarrantyCode, string argClientCode)
        {
            List<WarrantyDetail> lst = new List<WarrantyDetail>();
            DataSet DataSetToFill = new DataSet();
            WarrantyDetail tWarrantyDetail = new WarrantyDetail();

            DataSetToFill = this.GetWarrantyDetail(argWarrantyCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateWarrantyDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetWarrantyDetail(string argWarrantyCode, int argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@WarrantyCode", argWarrantyCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetWarrantyDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetWarrantyDetail(string argWarrantyCode, int argItemNo, string argClientCode, DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@WarrantyCode", argWarrantyCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetWarrantyDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetWarrantyDetail(string argWarrantyCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@WarrantyCode", argWarrantyCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetWarrantyDetail",param);

            return DataSetToFill;
        }

        public DateTime GetWarrantyEndDate(string argWarrantyCode, string argMatGroup1Code, DateTime argValidFrom, string argClientCode)
        {
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@WarrantyCode", argWarrantyCode);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@ValidFrom",argValidFrom);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            param[4] = new SqlParameter("@ValidTo", SqlDbType.DateTime);
            param[4].Size = 25;
            param[4].Direction = ParameterDirection.Output;

            int i = da.ExecuteNonQuery("SL_GetWarrantyEndDate", param);

            DateTime strRetValue = Convert.ToDateTime(param[4].Value);

            return strRetValue;

        }


        private WarrantyDetail objCreateWarrantyDetail(DataRow dr)
        {
            WarrantyDetail tWarrantyDetail = new WarrantyDetail();

            tWarrantyDetail.SetObjectInfo(dr);

            return tWarrantyDetail;

        }

        public ICollection<ErrorHandler> SaveWarrantyDetail(WarrantyDetail argWarrantyDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsWarrantyDetailExists(argWarrantyDetail.WarrantyCode, argWarrantyDetail.ItemNo, argWarrantyDetail.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertWarrantyDetail(argWarrantyDetail, da, lstErr);
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
                    UpdateWarrantyDetail(argWarrantyDetail, da, lstErr);
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

        public void SaveWarrantyDetail(WarrantyDetail argWarrantyDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsWarrantyDetailExists(argWarrantyDetail.WarrantyCode, argWarrantyDetail.ItemNo, argWarrantyDetail.ClientCode,da) == false)
                {
                    InsertWarrantyDetail(argWarrantyDetail, da, lstErr);
                }
                else
                {
                    UpdateWarrantyDetail(argWarrantyDetail, da, lstErr);
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

        public void InsertWarrantyDetail(WarrantyDetail argWarrantyDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@WarrantyCode", argWarrantyDetail.WarrantyCode);
            param[1] = new SqlParameter("@ItemNo", argWarrantyDetail.ItemNo);
            param[2] = new SqlParameter("@MatGroup1Code", argWarrantyDetail.MatGroup1Code);
            param[3] = new SqlParameter("@VendorWarTermsCode", argWarrantyDetail.VendorWarTermsCode);
            param[4] = new SqlParameter("@CustWarTermsCode", argWarrantyDetail.CustWarTermsCode);
            param[5] = new SqlParameter("@ClientCode", argWarrantyDetail.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argWarrantyDetail.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argWarrantyDetail.ModifiedBy);
        
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertWarrantyDetail", param);


            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
            string strRetValue = Convert.ToString(param[10].Value);


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

        public void UpdateWarrantyDetail(WarrantyDetail argWarrantyDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@WarrantyCode", argWarrantyDetail.WarrantyCode);
            param[1] = new SqlParameter("@ItemNo", argWarrantyDetail.ItemNo);
            param[2] = new SqlParameter("@MatGroup1Code", argWarrantyDetail.MatGroup1Code);
            param[3] = new SqlParameter("@VendorWarTermsCode", argWarrantyDetail.VendorWarTermsCode);
            param[4] = new SqlParameter("@CustWarTermsCode", argWarrantyDetail.CustWarTermsCode);
            param[5] = new SqlParameter("@ClientCode", argWarrantyDetail.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argWarrantyDetail.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argWarrantyDetail.ModifiedBy);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateWarrantyDetail", param);


            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
            string strRetValue = Convert.ToString(param[10].Value);


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

        public ICollection<ErrorHandler> DeleteWarrantyDetail(string argWarrantyCode, int argItemNo, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@WarrantyCode", argWarrantyCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteWarrantyDetail", param);


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

        public ICollection<ErrorHandler> DeleteWarrantyDetail(string argWarrantyCode, int argItemNo, string argClientCode, int iIsDeleted, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[7];
                
                param[0] = new SqlParameter("@WarrantyCode", argWarrantyCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteWarrantyDetail", param);


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

        public bool blnIsWarrantyDetailExists(string argWarrantyCode, int argItemNo, string argClientCode)
        {
            bool IsWarrantyDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetWarrantyDetail(argWarrantyCode, argItemNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsWarrantyDetailExists = true;
            }
            else
            {
                IsWarrantyDetailExists = false;
            }
            return IsWarrantyDetailExists;
        }

        public bool blnIsWarrantyDetailExists(string argWarrantyCode, int argItemNo, string argClientCode, DataAccess da)
        {
            bool IsWarrantyDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetWarrantyDetail(argWarrantyCode, argItemNo, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsWarrantyDetailExists = true;
            }
            else
            {
                IsWarrantyDetailExists = false;
            }
            return IsWarrantyDetailExists;
        }
    }
}