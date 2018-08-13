
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
    public class Vendor_PurchaseOrgManager
    {
        const string Vendor_PurchaseOrgTable = "Vendor_PurchaseOrg";
        Vendor_AccountViewManager objVendor_AccountViewManager=new Vendor_AccountViewManager();
        Vendor_AccountView objVendor_AccountView=new Vendor_AccountView();
        Vendor_AccViewWHTax objVendor_AccViewWHTax=new Vendor_AccViewWHTax();
        Vendor_AccViewWHTaxManager objVendor_AccViewWHTaxManager=new Vendor_AccViewWHTaxManager();


        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Vendor_PurchaseOrg objGetVendor_PurchaseOrg(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode)
        {
            Vendor_PurchaseOrg argVendor_PurchaseOrg = new Vendor_PurchaseOrg();
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

            DataSetToFill = this.GetVendor_PurchaseOrg(argVendorCode, argPurchaseOrgCode, argCompanyCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argVendor_PurchaseOrg = this.objCreateVendor_PurchaseOrg((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argVendor_PurchaseOrg;
        }

        public ICollection<Vendor_PurchaseOrg> colGetVendor_PurchaseOrg(string argVendorCode, string argClientCode)
        {
            List<Vendor_PurchaseOrg> lst = new List<Vendor_PurchaseOrg>();
            DataSet DataSetToFill = new DataSet();
            Vendor_PurchaseOrg tVendor_PurchaseOrg = new Vendor_PurchaseOrg();

            DataSetToFill = this.GetVendor_PurchaseOrg(argVendorCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateVendor_PurchaseOrg(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetVendor_PurchaseOrg(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@VendorCode", argVendorCode);
            param[1] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrgCode);
            param[2] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetVendor_PurchaseOrg4ID", param);

            return DataSetToFill;
        }

        public DataSet GetVendor_PurchaseOrg(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@VendorCode", argVendorCode);
            param[1] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrgCode);
            param[2] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetVendor_PurchaseOrg4ID", param);

            return DataSetToFill;
        }

        public DataSet GetVendor_PurchaseOrg(string argVendorCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
           
            param[0] = new SqlParameter("@VendorCode", argVendorCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetVendor_PurchaseOrg",param);
            return DataSetToFill;
        }

        private Vendor_PurchaseOrg objCreateVendor_PurchaseOrg(DataRow dr)
        {
            Vendor_PurchaseOrg tVendor_PurchaseOrg = new Vendor_PurchaseOrg();

            tVendor_PurchaseOrg.SetObjectInfo(dr);

            return tVendor_PurchaseOrg;

        }

        public ICollection<ErrorHandler> SaveVendor_PurchaseOrg(Vendor_PurchaseOrg argVendor_PurchaseOrg, Vendor_AccountView argVendor_AccountView, ICollection<Vendor_AccViewWHTax> colGetVendor_AccViewWHTax)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
               da.Open_Connection();
               da.BEGIN_TRANSACTION();

               if (blnIsVendor_PurchaseOrgExists(argVendor_PurchaseOrg.VendorCode, argVendor_PurchaseOrg.PurchaseOrgCode, argVendor_PurchaseOrg.CompanyCode, argVendor_PurchaseOrg.ClientCode, da) == false)
               {
                   InsertVendor_PurchaseOrg(argVendor_PurchaseOrg, da, lstErr);
               }
               else
               {
                   UpdateVendor_PurchaseOrg(argVendor_PurchaseOrg, da, lstErr);
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

// For Account View
               objVendor_AccountViewManager.SaveVendor_AccountView(argVendor_AccountView, da, lstErr);

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

// For Account_ViewWH Tax
               if (colGetVendor_AccViewWHTax.Count > 0)
               {
                   foreach (Vendor_AccViewWHTax argVendor_AccViewWHTax in colGetVendor_AccViewWHTax)
                   {
                       if (argVendor_AccViewWHTax.IsDeleted == 0)
                       {
                          objVendor_AccViewWHTaxManager.SaveVendor_AccViewWHTax(argVendor_AccViewWHTax, da, lstErr);
                       }
                       else
                       {
                           objVendor_AccViewWHTaxManager.DeleteVendor_AccViewWHTax(argVendor_AccViewWHTax.VendorCode, argVendor_AccViewWHTax.PurchaseOrgCode, argVendor_AccViewWHTax.CompanyCode, argVendor_AccViewWHTax.ClientCode, argVendor_AccViewWHTax.IsDeleted);
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

        public ICollection<ErrorHandler> SaveVendor_PurchaseOrg(ICollection<Vendor_PurchaseOrg> colGetVendor_PurchaseOrg)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Vendor_PurchaseOrg argVendor_PurchaseOrg in colGetVendor_PurchaseOrg)
                {
                    if (argVendor_PurchaseOrg.IsDeleted == 0)
                    {
                        if (blnIsVendor_PurchaseOrgExists(argVendor_PurchaseOrg.VendorCode, argVendor_PurchaseOrg.PurchaseOrgCode, argVendor_PurchaseOrg.CompanyCode, argVendor_PurchaseOrg.ClientCode,da) == false)
                        {
                            InsertVendor_PurchaseOrg(argVendor_PurchaseOrg, da, lstErr);
                        }
                        else
                        {
                            UpdateVendor_PurchaseOrg(argVendor_PurchaseOrg, da, lstErr);
                        }
                    }
                    else
                    {

                        DeleteVendor_PurchaseOrg(argVendor_PurchaseOrg.VendorCode, argVendor_PurchaseOrg.PurchaseOrgCode, argVendor_PurchaseOrg.CompanyCode, argVendor_PurchaseOrg.ClientCode, argVendor_PurchaseOrg.IsDeleted);

                    }

                }

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

        public void InsertVendor_PurchaseOrg(Vendor_PurchaseOrg argVendor_PurchaseOrg, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@VendorCode", argVendor_PurchaseOrg.VendorCode);
            param[1] = new SqlParameter("@PurchaseOrgCode", argVendor_PurchaseOrg.PurchaseOrgCode);
            param[2] = new SqlParameter("@CompanyCode", argVendor_PurchaseOrg.CompanyCode);
            param[3] = new SqlParameter("@OrderCurrencyCode", argVendor_PurchaseOrg.OrderCurrencyCode);
            param[4] = new SqlParameter("@PaymentTermsCode", argVendor_PurchaseOrg.PaymentTermsCode);
            param[5] = new SqlParameter("@IncoTermsCode", argVendor_PurchaseOrg.IncoTermsCode);
            param[6] = new SqlParameter("@MinOrderValue", argVendor_PurchaseOrg.MinOrderValue);
            param[7] = new SqlParameter("@VendGroupCode", argVendor_PurchaseOrg.VendGroupCode);
            param[8] = new SqlParameter("@PurchaseGroupCode", argVendor_PurchaseOrg.PurchaseGroupCode);
            param[9] = new SqlParameter("@PlannedDeliverytime", argVendor_PurchaseOrg.PlannedDeliverytime);
            param[10] = new SqlParameter("@ClientCode", argVendor_PurchaseOrg.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argVendor_PurchaseOrg.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argVendor_PurchaseOrg.ModifiedBy);


            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertVendor_PurchaseOrg", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public void UpdateVendor_PurchaseOrg(Vendor_PurchaseOrg argVendor_PurchaseOrg, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@VendorCode", argVendor_PurchaseOrg.VendorCode);
            param[1] = new SqlParameter("@PurchaseOrgCode", argVendor_PurchaseOrg.PurchaseOrgCode);
            param[2] = new SqlParameter("@CompanyCode", argVendor_PurchaseOrg.CompanyCode);
            param[3] = new SqlParameter("@OrderCurrencyCode", argVendor_PurchaseOrg.OrderCurrencyCode);
            param[4] = new SqlParameter("@PaymentTermsCode", argVendor_PurchaseOrg.PaymentTermsCode);
            param[5] = new SqlParameter("@IncoTermsCode", argVendor_PurchaseOrg.IncoTermsCode);
            param[6] = new SqlParameter("@MinOrderValue", argVendor_PurchaseOrg.MinOrderValue);
            param[7] = new SqlParameter("@VendGroupCode", argVendor_PurchaseOrg.VendGroupCode);
            param[8] = new SqlParameter("@PurchaseGroupCode", argVendor_PurchaseOrg.PurchaseGroupCode);
            param[9] = new SqlParameter("@PlannedDeliverytime", argVendor_PurchaseOrg.PlannedDeliverytime);
            param[10] = new SqlParameter("@ClientCode", argVendor_PurchaseOrg.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argVendor_PurchaseOrg.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argVendor_PurchaseOrg.ModifiedBy);


            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateVendor_PurchaseOrg", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public ICollection<ErrorHandler> DeleteVendor_PurchaseOrg(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode,int iIsDeleted)
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

                int i = da.ExecuteNonQuery("Proc_DeleteVendor_PurchaseOrg", param);


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

        public bool blnIsVendor_PurchaseOrgExists(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode)
        {
            bool IsVendor_PurchaseOrgExists = false;
            DataSet ds = new DataSet();
            ds = GetVendor_PurchaseOrg(argVendorCode, argPurchaseOrgCode, argCompanyCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsVendor_PurchaseOrgExists = true;
            }
            else
            {
                IsVendor_PurchaseOrgExists = false;
            }
            return IsVendor_PurchaseOrgExists;
        }

        public bool blnIsVendor_PurchaseOrgExists(string argVendorCode, string argPurchaseOrgCode, string argCompanyCode, string argClientCode,DataAccess da)
        {
            bool IsVendor_PurchaseOrgExists = false;
            DataSet ds = new DataSet();
            ds = GetVendor_PurchaseOrg(argVendorCode, argPurchaseOrgCode, argCompanyCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsVendor_PurchaseOrgExists = true;
            }
            else
            {
                IsVendor_PurchaseOrgExists = false;
            }
            return IsVendor_PurchaseOrgExists;
        }
    }
}