
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
    public class Vendor_AccViewWHTaxManager
    {
        const string Vendor_AccViewWHTaxTable = "Vendor_AccViewWHTax";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Vendor_AccViewWHTax objGetVendor_AccViewWHTax(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode)
        {
            Vendor_AccViewWHTax argVendor_AccViewWHTax = new Vendor_AccViewWHTax();
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

            DataSetToFill = this.GetVendor_AccViewWHTax(argVendorCode, argPurchaseOrgCode, argCompanyCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argVendor_AccViewWHTax = this.objCreateVendor_AccViewWHTax((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argVendor_AccViewWHTax;
        }

        public ICollection<Vendor_AccViewWHTax> colGetVendor_AccViewWHTax(string argVendorCode,string argClientCode)
        {
            List<Vendor_AccViewWHTax> lst = new List<Vendor_AccViewWHTax>();
            DataSet DataSetToFill = new DataSet();
            Vendor_AccViewWHTax tVendor_AccViewWHTax = new Vendor_AccViewWHTax();

            DataSetToFill = this.GetVendor_AccViewWHTax(argVendorCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateVendor_AccViewWHTax(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetVendor_AccViewWHTax(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@VendorCode", argVendorCode);
            param[1] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrgCode);
            param[2] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetVendor_AccViewWHTax4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetVendor_AccViewWHTax(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@VendorCode", argVendorCode);
            param[1] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrgCode);
            param[2] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetVendor_AccViewWHTax4ID", param);

            return DataSetToFill;
        }

        public DataSet GetVendor_AccViewWHTax(string argVendorCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
         
            param[0] = new SqlParameter("@VendorCode", argVendorCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetVendor_AccViewWHTax",param);
            return DataSetToFill;
        }

        private Vendor_AccViewWHTax objCreateVendor_AccViewWHTax(DataRow dr)
        {
            Vendor_AccViewWHTax tVendor_AccViewWHTax = new Vendor_AccViewWHTax();

            tVendor_AccViewWHTax.SetObjectInfo(dr);

            return tVendor_AccViewWHTax;

        }

        public ICollection<ErrorHandler> SaveVendor_AccViewWHTax(Vendor_AccViewWHTax argVendor_AccViewWHTax)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsVendor_AccViewWHTaxExists(argVendor_AccViewWHTax.VendorCode, argVendor_AccViewWHTax.PurchaseOrgCode, argVendor_AccViewWHTax.CompanyCode, argVendor_AccViewWHTax.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertVendor_AccViewWHTax(argVendor_AccViewWHTax, da, lstErr);
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
                    UpdateVendor_AccViewWHTax(argVendor_AccViewWHTax, da, lstErr);
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

        public void SaveVendor_AccViewWHTax(Vendor_AccViewWHTax argVendor_AccViewWHTax, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsVendor_AccViewWHTaxExists(argVendor_AccViewWHTax.VendorCode, argVendor_AccViewWHTax.PurchaseOrgCode, argVendor_AccViewWHTax.CompanyCode, argVendor_AccViewWHTax.ClientCode,da) == false)
                {
                    InsertVendor_AccViewWHTax(argVendor_AccViewWHTax, da, lstErr);
                }
                else
                {
                    UpdateVendor_AccViewWHTax(argVendor_AccViewWHTax, da, lstErr);
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

        public void InsertVendor_AccViewWHTax(Vendor_AccViewWHTax argVendor_AccViewWHTax, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@VendorCode", argVendor_AccViewWHTax.VendorCode);
            param[1] = new SqlParameter("@PurchaseOrgCode", argVendor_AccViewWHTax.PurchaseOrgCode);
            param[2] = new SqlParameter("@CompanyCode", argVendor_AccViewWHTax.CompanyCode);
            param[3] = new SqlParameter("@WHTaxCountryCode", argVendor_AccViewWHTax.WHTaxCountryCode);
            param[4] = new SqlParameter("@WHTaxType", argVendor_AccViewWHTax.WHTaxType);
            param[5] = new SqlParameter("@WHTaxCode", argVendor_AccViewWHTax.WHTaxCode);
            param[6] = new SqlParameter("@WHTaxID", argVendor_AccViewWHTax.WHTaxID);
            param[7] = new SqlParameter("@TypeOfRecCode", argVendor_AccViewWHTax.TypeOfRecCode);
            param[8] = new SqlParameter("@ExemptNo", argVendor_AccViewWHTax.ExemptNo);
            param[9] = new SqlParameter("@ExemptPercentage", argVendor_AccViewWHTax.ExemptPercentage);
            param[10] = new SqlParameter("@ExemptReason", argVendor_AccViewWHTax.ExemptReason);
            param[11] = new SqlParameter("@ExemptFrom", argVendor_AccViewWHTax.ExemptFrom);
            param[12] = new SqlParameter("@ExemptTo", argVendor_AccViewWHTax.ExemptTo);
            param[13] = new SqlParameter("@ClientCode", argVendor_AccViewWHTax.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argVendor_AccViewWHTax.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argVendor_AccViewWHTax.ModifiedBy);
         
            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertVendor_AccViewWHTax", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public void UpdateVendor_AccViewWHTax(Vendor_AccViewWHTax argVendor_AccViewWHTax, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@VendorCode", argVendor_AccViewWHTax.VendorCode);
            param[1] = new SqlParameter("@PurchaseOrgCode", argVendor_AccViewWHTax.PurchaseOrgCode);
            param[2] = new SqlParameter("@CompanyCode", argVendor_AccViewWHTax.CompanyCode);
            param[3] = new SqlParameter("@WHTaxCountryCode", argVendor_AccViewWHTax.WHTaxCountryCode);
            param[4] = new SqlParameter("@WHTaxType", argVendor_AccViewWHTax.WHTaxType);
            param[5] = new SqlParameter("@WHTaxCode", argVendor_AccViewWHTax.WHTaxCode);
            param[6] = new SqlParameter("@WHTaxID", argVendor_AccViewWHTax.WHTaxID);
            param[7] = new SqlParameter("@TypeOfRecCode", argVendor_AccViewWHTax.TypeOfRecCode);
            param[8] = new SqlParameter("@ExemptNo", argVendor_AccViewWHTax.ExemptNo);
            param[9] = new SqlParameter("@ExemptPercentage", argVendor_AccViewWHTax.ExemptPercentage);
            param[10] = new SqlParameter("@ExemptReason", argVendor_AccViewWHTax.ExemptReason);
            param[11] = new SqlParameter("@ExemptFrom", argVendor_AccViewWHTax.ExemptFrom);
            param[12] = new SqlParameter("@ExemptTo", argVendor_AccViewWHTax.ExemptTo);
            param[13] = new SqlParameter("@ClientCode", argVendor_AccViewWHTax.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argVendor_AccViewWHTax.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argVendor_AccViewWHTax.ModifiedBy);

            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateVendor_AccViewWHTax", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public ICollection<ErrorHandler> DeleteVendor_AccViewWHTax(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@VendorCode", argVendorCode);
                param[1] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrgCode);
                param[2] = new SqlParameter("@CompanyCode", argCompanyCode);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteVendor_AccViewWHTax", param);
                
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

        public ICollection<ErrorHandler> DeleteVendor_AccViewWHTax(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode, int iIsDeleted,DataAccess da)
        {
            
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@VendorCode", argVendorCode);
                param[1] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrgCode);
                param[2] = new SqlParameter("@CompanyCode", argCompanyCode);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.NExecuteNonQuery("Proc_DeleteVendor_AccViewWHTax", param);

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

        public bool blnIsVendor_AccViewWHTaxExists(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode)
        {
            bool IsVendor_AccViewWHTaxExists = false;
            DataSet ds = new DataSet();
            ds = GetVendor_AccViewWHTax(argVendorCode, argPurchaseOrgCode, argCompanyCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsVendor_AccViewWHTaxExists = true;
            }
            else
            {
                IsVendor_AccViewWHTaxExists = false;
            }
            return IsVendor_AccViewWHTaxExists;
        }

        public bool blnIsVendor_AccViewWHTaxExists(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode,DataAccess da)
        {
            bool IsVendor_AccViewWHTaxExists = false;
            DataSet ds = new DataSet();
            ds = GetVendor_AccViewWHTax(argVendorCode, argPurchaseOrgCode, argCompanyCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsVendor_AccViewWHTaxExists = true;
            }
            else
            {
                IsVendor_AccViewWHTaxExists = false;
            }
            return IsVendor_AccViewWHTaxExists;
        }
    }
}