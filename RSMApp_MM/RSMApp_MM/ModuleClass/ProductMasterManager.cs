
//Created On :: 19, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_MM
{
    public class ProductMasterManager
    {
        const string ProductMasterTable = "ProductMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public ProductMaster objGetProductMaster(string argSerialNo, string argMatGroup1Code, string argClientCode)
        {
            ProductMaster argProductMaster = new ProductMaster();
            DataSet DataSetToFill = new DataSet();

            if (argSerialNo.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argMatGroup1Code.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetProductMaster(argSerialNo, argMatGroup1Code, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argProductMaster = this.objCreateProductMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argProductMaster;
        }

        public DataSet GetProduct4Call(string argSerialNo, string argMatGroup1Code, string argCallTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@CallTypeCode", argCallTypeCode);
            param[3] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_GetMaterialList4Call", param);
            return DataSetToFill;
        }
        
        public ProductMaster objGetProductMaster(string argSerialNo,string argMaterialCode ,string argMatGroup1Code, string argClientCode)
        {
            ProductMaster argProductMaster = new ProductMaster();
            DataSet DataSetToFill = new DataSet();

            if (argSerialNo.Trim() == "")
            {
                goto ErrorHandlers;
            }
            if (argMaterialCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            if (argMatGroup1Code.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetProductMaster(argSerialNo,argMaterialCode, argMatGroup1Code, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argProductMaster = this.objCreateProductMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argProductMaster;
        }

        public ICollection<ProductMaster> colGetProductMaster(string argClientCode)
        {
            List<ProductMaster> lst = new List<ProductMaster>();
            DataSet DataSetToFill = new DataSet();
            ProductMaster tProductMaster = new ProductMaster();

            DataSetToFill = this.GetProductMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateProductMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<ProductMaster> colGetProductMaster(string argSerialNo, string argMaterialCode, string argMatGroup1Code, string argClientCode, List<ProductMaster> lst)
        {
            DataSet DataSetToFill = new DataSet();
            ProductMaster tProductMaster = new ProductMaster();

            DataSetToFill = this.GetProductMaster(argSerialNo,argMaterialCode, argMatGroup1Code, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateProductMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetProductMaster(string argSerialNo, string argMatGroup1Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetProductMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetProductMaster(string argSerialNo, string argMatGroup1Code, string argClientCode, DataAccess da)
        {
           
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetProductMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetProductMaster(string argSerialNo,string argMastMaterialCode, string argMatGroup1Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@MastMaterialCode", argMastMaterialCode);
            param[2] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetProductMaster4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetProductMaster(string argSerialNo, string argMastMaterialCode, string argMatGroup1Code, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@MastMaterialCode", argMastMaterialCode);
            param[2] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetProductMaster4ID", param);

            return DataSetToFill;
        }
       
        public DataSet GetProductMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);


            DataSetToFill = da.FillDataSet("SP_GetProductMaster", param);
            return DataSetToFill;
        }

        public DataSet GetProductSerialNo(string argMaterialCode,string argMatGroup1Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetProductSerialNo", param);

            return DataSetToFill;
        }

        private ProductMaster objCreateProductMaster(DataRow dr)
        {
            ProductMaster tProductMaster = new ProductMaster();

            tProductMaster.SetObjectInfo(dr);

            return tProductMaster;

        }

        public ICollection<ErrorHandler> SaveProductMaster(ProductMaster argProductMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsProductMasterExists(argProductMaster.SerialNo, argProductMaster.MatGroup1Code, argProductMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertProductMaster(argProductMaster, ref da, ref lstErr);
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
                    UpdateProductMaster(argProductMaster, da, lstErr);
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

        public ICollection<ErrorHandler> SaveProductMaster(ICollection<ProductMaster> colGetProductMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();
                foreach (ProductMaster argProductMaster in colGetProductMaster)
                {
                    if (blnIsProductMasterExists(argProductMaster.SerialNo, argProductMaster.MastMaterialCode,argProductMaster.MatGroup1Code, argProductMaster.ClientCode,da) == false)
                    {
                        InsertProductMaster(argProductMaster, ref da, ref lstErr);
                    }
                    else
                    {
                        UpdateProductMaster(argProductMaster, da, lstErr);
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

        public void SaveProductMaster(ProductMaster argProductMaster, ref DataAccess da, ref List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsProductMasterExists(argProductMaster.SerialNo, argProductMaster.MatGroup1Code, argProductMaster.ClientCode) == false)
                {
                    InsertProductMaster(argProductMaster, ref da, ref lstErr);
                }
                else
                {

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

        public void  InsertProductMaster(ProductMaster argProductMaster, ref DataAccess da, ref List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[37];
            param[0] = new SqlParameter("@SerialNo", argProductMaster.SerialNo);
            param[1] = new SqlParameter("@MastSerialNo", argProductMaster.MastSerialNo);
            param[2] = new SqlParameter("@MastMaterialCode", argProductMaster.MastMaterialCode);
            param[3] = new SqlParameter("@MRevisionCode", argProductMaster.MRevisionCode);
            param[4] = new SqlParameter("@CompanyCode", argProductMaster.CompanyCode);
            param[5] = new SqlParameter("@MaterialCode", argProductMaster.MaterialCode);
            param[6] = new SqlParameter("@MatGroup1Code", argProductMaster.MatGroup1Code);
            param[7] = new SqlParameter("@WarrantyCode", argProductMaster.WarrantyCode);
            param[8] = new SqlParameter("@ManufacturingDate", argProductMaster.ManufacturingDate);

            param[9] = new SqlParameter("@PrimarySalesDate", argProductMaster.PrimarySalesDate);

            param[10] = new SqlParameter("@CustInvoiceDate", argProductMaster.CustInvoiceDate);
            param[11] = new SqlParameter("@CustInvoiceNo", argProductMaster.CustInvoiceNo);
            param[12] = new SqlParameter("@CustType", argProductMaster.CustType);
            param[13] = new SqlParameter("@CustName", argProductMaster.CustName);
            param[14] = new SqlParameter("@CustAddress1", argProductMaster.CustAddress1);
            param[15] = new SqlParameter("@CustAddress2", argProductMaster.CustAddress2);
            param[16] = new SqlParameter("@CustPhone", argProductMaster.CustPhone);
            param[17] = new SqlParameter("@CustMobile", argProductMaster.CustMobile);
            param[18] = new SqlParameter("@CustEmail", argProductMaster.CustEmail);
            param[19] = new SqlParameter("@CustGender", argProductMaster.CustGender);
            param[20] = new SqlParameter("@WarrantyOn", argProductMaster.WarrantyOn);
            param[21] = new SqlParameter("@ExtWarrantyOn", argProductMaster.ExtWarrantyOn);
            param[22] = new SqlParameter("@ExtWarrantyReason", argProductMaster.ExtWarrantyReason);
            param[23] = new SqlParameter("@ValidFrom", argProductMaster.ValidFrom);
            param[24] = new SqlParameter("@ValidTo", argProductMaster.ValidTo);
            param[25] = new SqlParameter("@ExtValidFrom", argProductMaster.ExtValidFrom);
            param[26] = new SqlParameter("@ExtValidTo", argProductMaster.ExtValidTo);
            param[27] = new SqlParameter("@IsExtWarrantyApp", argProductMaster.IsExtWarrantyApp);
            param[28] = new SqlParameter("@IsWarrantyExp", argProductMaster.IsWarrantyExp);
            param[29] = new SqlParameter("@IsAbandoned", argProductMaster.IsAbandoned);
            param[30] = new SqlParameter("@IsExploded", argProductMaster.IsExploded);
            param[31] = new SqlParameter("@ClientCode", argProductMaster.ClientCode);
            param[32] = new SqlParameter("@CreatedBy", argProductMaster.CreatedBy);
            param[33] = new SqlParameter("@ModifiedBy", argProductMaster.ModifiedBy);
      
            param[34] = new SqlParameter("@Type", SqlDbType.Char);
            param[34].Size = 1;
            param[34].Direction = ParameterDirection.Output;

            param[35] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[35].Size = 255;
            param[35].Direction = ParameterDirection.Output;

            param[36] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[36].Size = 20;
            param[36].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertProductMaster", param);


            string strMessage = Convert.ToString(param[35].Value);
            string strType = Convert.ToString(param[34].Value);
            string strRetValue = Convert.ToString(param[36].Value);

            objErrorHandler = new ErrorHandler();
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

        public void UpdateProductMaster(ProductMaster argProductMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[36];
            param[0] = new SqlParameter("@SerialNo", argProductMaster.SerialNo);
            param[1] = new SqlParameter("@MastSerialNo", argProductMaster.MastSerialNo);
            param[2] = new SqlParameter("@MastMaterialCode", argProductMaster.MastMaterialCode);
            param[3] = new SqlParameter("@MRevisionCode", argProductMaster.MRevisionCode);
            param[4] = new SqlParameter("@CompanyCode", argProductMaster.CompanyCode);
            param[5] = new SqlParameter("@MaterialCode", argProductMaster.MaterialCode);
            param[6] = new SqlParameter("@MatGroup1Code", argProductMaster.MatGroup1Code);
            param[7] = new SqlParameter("@WarrantyCode", argProductMaster.WarrantyCode);
            param[8] = new SqlParameter("@ManufacturingDate", argProductMaster.ManufacturingDate);

            param[9] = new SqlParameter("@PrimarySalesDate", argProductMaster.PrimarySalesDate);

            param[10] = new SqlParameter("@CustInvoiceDate", argProductMaster.CustInvoiceDate);
            param[11] = new SqlParameter("@CustInvoiceNo", argProductMaster.CustInvoiceNo);
            param[12] = new SqlParameter("@CustType", argProductMaster.CustType);
            param[13] = new SqlParameter("@CustName", argProductMaster.CustName);
            param[14] = new SqlParameter("@CustAddress1", argProductMaster.CustAddress1);
            param[15] = new SqlParameter("@CustAddress2", argProductMaster.CustAddress2);
            param[16] = new SqlParameter("@CustPhone", argProductMaster.CustPhone);
            param[17] = new SqlParameter("@CustMobile", argProductMaster.CustMobile);
            param[18] = new SqlParameter("@CustEmail", argProductMaster.CustEmail);
            param[19] = new SqlParameter("@CustGender", argProductMaster.CustGender);
            param[20] = new SqlParameter("@WarrantyOn", argProductMaster.WarrantyOn);
            param[21] = new SqlParameter("@ExtWarrantyOn", argProductMaster.ExtWarrantyOn);
            param[22] = new SqlParameter("@ExtWarrantyReason", argProductMaster.ExtWarrantyReason);
            param[23] = new SqlParameter("@ValidFrom", argProductMaster.ValidFrom);
            param[24] = new SqlParameter("@ValidTo", argProductMaster.ValidTo);
            param[25] = new SqlParameter("@ExtValidFrom", argProductMaster.ExtValidFrom);
            param[26] = new SqlParameter("@ExtValidTo", argProductMaster.ExtValidTo);
            param[27] = new SqlParameter("@IsExtWarrantyApp", argProductMaster.IsExtWarrantyApp);
            param[28] = new SqlParameter("@IsWarrantyExp", argProductMaster.IsWarrantyExp);
            param[29] = new SqlParameter("@IsAbandoned", argProductMaster.IsAbandoned);
            param[30] = new SqlParameter("@ClientCode", argProductMaster.ClientCode);
            param[31] = new SqlParameter("@CreatedBy", argProductMaster.CreatedBy);
            param[32] = new SqlParameter("@ModifiedBy", argProductMaster.ModifiedBy);

            param[33] = new SqlParameter("@Type", SqlDbType.Char);
            param[33].Size = 1;
            param[33].Direction = ParameterDirection.Output;

            param[34] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[34].Size = 255;
            param[34].Direction = ParameterDirection.Output;

            param[35] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[35].Size = 20;
            param[35].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateProductMaster", param);


            string strMessage = Convert.ToString(param[34].Value);
            string strType = Convert.ToString(param[33].Value);
            string strRetValue = Convert.ToString(param[35].Value);


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

        public void UpdateProductMaster4Call(ProductMaster argProductMaster, ref DataAccess da, ref List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[22];

            param[0] = new SqlParameter("@SerialNo", argProductMaster.SerialNo);
            param[1] = new SqlParameter("@MaterialCode", argProductMaster.MaterialCode);
            param[2] = new SqlParameter("@MatGroup1Code", argProductMaster.MatGroup1Code);
            param[3] = new SqlParameter("@CustInvoiceDate", argProductMaster.CustInvoiceDate);
            param[4] = new SqlParameter("@CustInvoiceNo", argProductMaster.CustInvoiceNo);
            param[5] = new SqlParameter("@CustType", argProductMaster.CustType);
            param[6] = new SqlParameter("@CustName", argProductMaster.CustName);
            param[7] = new SqlParameter("@CustAddress1", argProductMaster.CustAddress1);
            param[8] = new SqlParameter("@CustAddress2", argProductMaster.CustAddress2);
            param[9] = new SqlParameter("@CustPhone", argProductMaster.CustPhone);
            param[10] = new SqlParameter("@CustMobile", argProductMaster.CustMobile);
            param[11] = new SqlParameter("@CustEmail", argProductMaster.CustEmail);
            param[12] = new SqlParameter("@CustGender", argProductMaster.CustGender);
            param[13] = new SqlParameter("@ValidFrom", argProductMaster.ValidFrom);
            param[14] = new SqlParameter("@ValidTo", argProductMaster.ValidTo);
            param[15] = new SqlParameter("@IsWarrantyExp", argProductMaster.IsWarrantyExp);
            param[16] = new SqlParameter("@ClientCode", argProductMaster.ClientCode);
            param[17] = new SqlParameter("@CreatedBy", argProductMaster.CreatedBy);
            param[18] = new SqlParameter("@ModifiedBy", argProductMaster.ModifiedBy);

            param[19] = new SqlParameter("@Type", SqlDbType.Char);
            param[19].Size = 1;
            param[19].Direction = ParameterDirection.Output;

            param[20] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[20].Size = 255;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[21].Size = 20;
            param[21].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("SL_Proc_UpdateProductMaster", param);


            string strMessage = Convert.ToString(param[20].Value);
            string strType = Convert.ToString(param[19].Value);
            string strRetValue = Convert.ToString(param[21].Value);

            objErrorHandler = new ErrorHandler();
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

        public ICollection<ErrorHandler> DeleteProductMaster(string argSerialNo, string argMatGroup1Code, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@SerialNo", argSerialNo);
                param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
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

                int i = da.ExecuteNonQuery("Proc_DeleteProductMaster", param);


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

        public bool blnIsProductMasterExists(string argSerialNo, string argMatGroup1Code, string argClientCode)
        {
            bool IsProductMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetProductMaster(argSerialNo, argMatGroup1Code, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsProductMasterExists = true;
            }
            else
            {
                IsProductMasterExists = false;
            }
            return IsProductMasterExists;
        }
       
        public bool blnIsProductMasterExists(string argSerialNo, string argMastMaterialCode,string argMatGroup1Code, string argClientCode, DataAccess da)
        {
            bool IsProductMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetProductMaster(argSerialNo,argMastMaterialCode,argMatGroup1Code, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsProductMasterExists = true;
            }
            else
            {
                IsProductMasterExists = false;
            }
            return IsProductMasterExists;
        }
    }
}