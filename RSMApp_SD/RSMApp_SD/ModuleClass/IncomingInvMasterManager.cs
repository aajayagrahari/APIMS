
//Created On :: 26, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class IncomingInvMasterManager
    {
        const string IncomingInvMasterTable = "IncomingInvMaster";
        IncomingInvDetailManager objIncomingInvDetailManager = new IncomingInvDetailManager();

       //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public IncomingInvMaster objGetIncomingInvMaster(string argIncomingInvDocCode, string argClientCode)
        {
            IncomingInvMaster argIncomingInvMaster = new IncomingInvMaster();
            DataSet DataSetToFill = new DataSet();

            if (argIncomingInvDocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetIncomingInvMaster(argIncomingInvDocCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argIncomingInvMaster = this.objCreateIncomingInvMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argIncomingInvMaster;
        }

        public ICollection<IncomingInvMaster> colGetIncomingInvMaster(string argClientCode)
        {
            List<IncomingInvMaster> lst = new List<IncomingInvMaster>();
            DataSet DataSetToFill = new DataSet();
            IncomingInvMaster tIncomingInvMaster = new IncomingInvMaster();

            DataSetToFill = this.GetIncomingInvMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateIncomingInvMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetIncomingInvMaster(string argIncomingInvDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@IncomingInvDocCode", argIncomingInvDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetIncomingInvMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetIncomingInvMaster(string argIncomingInvDocCode, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@IncomingInvDocCode", argIncomingInvDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetIncomingInvMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetIncomingInvMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetIncomingInvMaster",param);
            return DataSetToFill;
        }

        private IncomingInvMaster objCreateIncomingInvMaster(DataRow dr)
        {
            IncomingInvMaster tIncomingInvMaster = new IncomingInvMaster();

            tIncomingInvMaster.SetObjectInfo(dr);

            return tIncomingInvMaster;

        }

        public ICollection<ErrorHandler> SaveIncomingInvMaster(IncomingInvMaster argIncomingInvMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsIncomingInvMasterExists(argIncomingInvMaster.IncomingInvDocCode, argIncomingInvMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertIncomingInvMaster(argIncomingInvMaster, da, lstErr);
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
                    UpdateIncomingInvMaster(argIncomingInvMaster, da, lstErr);
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

        public ICollection<ErrorHandler> SaveIncomingInvMaster(IncomingInvMaster argIncomingInvMaster, ICollection<IncomingInvDetail> colGetIncomingInvDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();

            string strretValue = "";
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsIncomingInvMasterExists(argIncomingInvMaster.IncomingInvDocCode, argIncomingInvMaster.ClientCode,da) == false)
                {
                    strretValue = InsertIncomingInvMaster(argIncomingInvMaster, da, lstErr);
                }
                else
                {
                    strretValue = UpdateIncomingInvMaster(argIncomingInvMaster, da, lstErr);
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

                    if (colGetIncomingInvDetail.Count > 0)
                    {
                        foreach (IncomingInvDetail argIncomingInvDetail in colGetIncomingInvDetail)
                        {
                            argIncomingInvDetail.IncomingInvDocCode = Convert.ToString(strretValue);
                            if (argIncomingInvDetail.IsDeleted == 0)
                            {
                                objIncomingInvDetailManager.SaveIncomingInvDetail(argIncomingInvDetail, da, lstErr);
                            }
                            else
                            {
                                objIncomingInvDetailManager.DeleteIncomingInvDetail(argIncomingInvDetail.IncomingInvDocCode, argIncomingInvDetail.ItemNo,argIncomingInvDetail.ClientCode,1, da, lstErr);
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
                //}
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

        public string InsertIncomingInvMaster(IncomingInvMaster argIncomingInvMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@IncomingInvDocCode", argIncomingInvMaster.IncomingInvDocCode);
            param[1] = new SqlParameter("@IncomingInvDocTypeCode", argIncomingInvMaster.IncomingInvDocTypeCode);
            param[2] = new SqlParameter("@InvoiceDate", argIncomingInvMaster.InvoiceDate);
            param[3] = new SqlParameter("@VendorCode", argIncomingInvMaster.VendorCode);
            param[4] = new SqlParameter("@TotalQty", argIncomingInvMaster.TotalQty);
            param[5] = new SqlParameter("@GrossAmt",argIncomingInvMaster.GrossAmt);
            param[6] = new SqlParameter("@NetAmt", argIncomingInvMaster.NetAmt);
            param[7] = new SqlParameter("@SAPTranID", argIncomingInvMaster.SAPTranID);
            param[8] = new SqlParameter("@IsSAPPosted", argIncomingInvMaster.IsSAPPosted);
            param[9] = new SqlParameter("@ClientCode", argIncomingInvMaster.ClientCode);
            param[10] = new SqlParameter("@CreatedBy", argIncomingInvMaster.CreatedBy);
            param[11] = new SqlParameter("@ModifiedBy", argIncomingInvMaster.ModifiedBy);

            param[12] = new SqlParameter("@Type", SqlDbType.Char);
            param[12].Size = 1;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[13].Size = 255;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[14].Size = 20;
            param[14].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertIncomingInvMaster", param);


            string strMessage = Convert.ToString(param[13].Value);
            string strType = Convert.ToString(param[12].Value);
            string strRetValue = Convert.ToString(param[14].Value);


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

        public string UpdateIncomingInvMaster(IncomingInvMaster argIncomingInvMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@IncomingInvDocCode", argIncomingInvMaster.IncomingInvDocCode);
            param[1] = new SqlParameter("@IncomingInvDocTypeCode", argIncomingInvMaster.IncomingInvDocTypeCode);
            param[2] = new SqlParameter("@InvoiceDate", argIncomingInvMaster.InvoiceDate);
            param[3] = new SqlParameter("@VendorCode", argIncomingInvMaster.VendorCode);
            param[4] = new SqlParameter("@TotalQty", argIncomingInvMaster.TotalQty);
            param[5] = new SqlParameter("@GrossAmt", argIncomingInvMaster.GrossAmt);
            param[6] = new SqlParameter("@NetAmt", argIncomingInvMaster.NetAmt);
            param[7] = new SqlParameter("@SAPTranID", argIncomingInvMaster.SAPTranID);
            param[8] = new SqlParameter("@IsSAPPosted", argIncomingInvMaster.IsSAPPosted);
            param[9] = new SqlParameter("@ClientCode", argIncomingInvMaster.ClientCode);
            param[10] = new SqlParameter("@CreatedBy", argIncomingInvMaster.CreatedBy);
            param[11] = new SqlParameter("@ModifiedBy", argIncomingInvMaster.ModifiedBy);

            param[12] = new SqlParameter("@Type", SqlDbType.Char);
            param[12].Size = 1;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[13].Size = 255;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[14].Size = 20;
            param[14].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateIncomingInvMaster", param);


            string strMessage = Convert.ToString(param[13].Value);
            string strType = Convert.ToString(param[12].Value);
            string strRetValue = Convert.ToString(param[14].Value);


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

        public ICollection<ErrorHandler> DeleteIncomingInvMaster(string argIncomingInvDocCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@IncomingInvDocCode", argIncomingInvDocCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteIncomingInvMaster", param);


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
            return lstErr;

        }

        public ICollection<ErrorHandler> DeleteIncomingInvMaster(string argIncomingInvDocCode, string argClientCode, int iIsDeleted, DataAccess da, List<ErrorHandler> lstErr)
        {
  
            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@IncomingInvDocCode", argIncomingInvDocCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.NExecuteNonQuery("Proc_DeleteIncomingInvMaster", param);


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
            return lstErr;

        }

        public bool blnIsIncomingInvMasterExists(string argIncomingInvDocCode, string argClientCode)
        {
            bool IsIncomingInvMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetIncomingInvMaster(argIncomingInvDocCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsIncomingInvMasterExists = true;
            }
            else
            {
                IsIncomingInvMasterExists = false;
            }
            return IsIncomingInvMasterExists;
        }

        public bool blnIsIncomingInvMasterExists(string argIncomingInvDocCode, string argClientCode, DataAccess da)
        {
            bool IsIncomingInvMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetIncomingInvMaster(argIncomingInvDocCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsIncomingInvMasterExists = true;
            }
            else
            {
                IsIncomingInvMasterExists = false;
            }
            return IsIncomingInvMasterExists;
        }
    }
}