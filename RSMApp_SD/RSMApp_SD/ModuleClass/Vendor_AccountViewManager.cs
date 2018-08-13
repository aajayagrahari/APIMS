
//Created On :: 08, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class Vendor_AccountViewManager
    {
        const string Vendor_AccountViewTable = "Vendor_AccountView";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Vendor_AccountView objGetVendor_AccountView(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode)
        {
            Vendor_AccountView argVendor_AccountView = new Vendor_AccountView();
            DataSet DataSetToFill = new DataSet();

            if (argVendorCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argPurchaseOrgCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argCompanyCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetVendor_AccountView(argVendorCode, argPurchaseOrgCode, argCompanyCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argVendor_AccountView = this.objCreateVendor_AccountView((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argVendor_AccountView;
        }


        public ICollection<Vendor_AccountView> colGetVendor_AccountView(string argVendorCode, string argClientCode)
        {
            List<Vendor_AccountView> lst = new List<Vendor_AccountView>();
            DataSet DataSetToFill = new DataSet();
            Vendor_AccountView tVendor_AccountView = new Vendor_AccountView();

            DataSetToFill = this.GetVendor_AccountView(argVendorCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateVendor_AccountView(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetVendor_AccountView(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@VendorCode", argVendorCode);
            param[1] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrgCode);
            param[2] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetVendor_AccountView4ID", param);

            return DataSetToFill;
        }

        public DataSet GetVendor_AccountView(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@VendorCode", argVendorCode);
            param[1] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrgCode);
            param[2] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetVendor_AccountView4ID", param);

            return DataSetToFill;
        }



        public DataSet GetVendor_AccountView(string argVendorCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@VendorCode", argVendorCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetVendor_AccountView",param);
            return DataSetToFill;
        }


        private Vendor_AccountView objCreateVendor_AccountView(DataRow dr)
        {
            Vendor_AccountView tVendor_AccountView = new Vendor_AccountView();

            tVendor_AccountView.SetObjectInfo(dr);

            return tVendor_AccountView;

        }


        public ICollection<ErrorHandler> SaveVendor_AccountView(Vendor_AccountView argVendor_AccountView)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsVendor_AccountViewExists(argVendor_AccountView.VendorCode, argVendor_AccountView.PurchaseOrgCode, argVendor_AccountView.CompanyCode, argVendor_AccountView.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertVendor_AccountView(argVendor_AccountView, da, lstErr);
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
                    UpdateVendor_AccountView(argVendor_AccountView, da, lstErr);
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

        public void SaveVendor_AccountView(Vendor_AccountView argVendor_AccountView, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsVendor_AccountViewExists(argVendor_AccountView.VendorCode, argVendor_AccountView.PurchaseOrgCode, argVendor_AccountView.CompanyCode, argVendor_AccountView.ClientCode,da) == false)
                {
                    InsertVendor_AccountView(argVendor_AccountView, da, lstErr);
                }
                else
                {
                    UpdateVendor_AccountView(argVendor_AccountView, da, lstErr);
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

        public void InsertVendor_AccountView(Vendor_AccountView argVendor_AccountView, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@VendorCode", argVendor_AccountView.VendorCode);
            param[1] = new SqlParameter("@PurchaseOrgCode", argVendor_AccountView.PurchaseOrgCode);
            param[2] = new SqlParameter("@CompanyCode", argVendor_AccountView.CompanyCode);
            param[3] = new SqlParameter("@ReconsilationAccount", argVendor_AccountView.ReconsilationAccount);
            param[4] = new SqlParameter("@PaymentTermsCode", argVendor_AccountView.PaymentTermsCode);
            param[5] = new SqlParameter("@ClientCode", argVendor_AccountView.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argVendor_AccountView.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argVendor_AccountView.ModifiedBy);
      
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertVendor_AccountView", param);


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

        public void UpdateVendor_AccountView(Vendor_AccountView argVendor_AccountView, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@VendorCode", argVendor_AccountView.VendorCode);
            param[1] = new SqlParameter("@PurchaseOrgCode", argVendor_AccountView.PurchaseOrgCode);
            param[2] = new SqlParameter("@CompanyCode", argVendor_AccountView.CompanyCode);
            param[3] = new SqlParameter("@ReconsilationAccount", argVendor_AccountView.ReconsilationAccount);
            param[4] = new SqlParameter("@PaymentTermsCode", argVendor_AccountView.PaymentTermsCode);
            param[5] = new SqlParameter("@ClientCode", argVendor_AccountView.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argVendor_AccountView.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argVendor_AccountView.ModifiedBy);
      

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateVendor_AccountView", param);


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
            lstErr.Add(objErrorHandler);

        }

        public ICollection<ErrorHandler> DeleteVendor_AccountView(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@VendorCode", argVendorCode);
                param[1] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrgCode);
                param[2] = new SqlParameter("@CompanyCode", argCompanyCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteVendor_AccountView", param);


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

        public ICollection<ErrorHandler> DeleteVendor_AccountView(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode,DataAccess da)
        {
  
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@VendorCode", argVendorCode);
                param[1] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrgCode);
                param[2] = new SqlParameter("@CompanyCode", argCompanyCode);
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
                int i = da.NExecuteNonQuery("Proc_DeleteVendor_AccountView", param);


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


        public bool blnIsVendor_AccountViewExists(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode)
        {
            bool IsVendor_AccountViewExists = false;
            DataSet ds = new DataSet();
            ds = GetVendor_AccountView(argVendorCode, argPurchaseOrgCode, argCompanyCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsVendor_AccountViewExists = true;
            }
            else
            {
                IsVendor_AccountViewExists = false;
            }
            return IsVendor_AccountViewExists;
        }

        public bool blnIsVendor_AccountViewExists(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode,DataAccess da)
        {
            bool IsVendor_AccountViewExists = false;
            DataSet ds = new DataSet();
            ds = GetVendor_AccountView(argVendorCode, argPurchaseOrgCode, argCompanyCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsVendor_AccountViewExists = true;
            }
            else
            {
                IsVendor_AccountViewExists = false;
            }
            return IsVendor_AccountViewExists;
        }
    }
}