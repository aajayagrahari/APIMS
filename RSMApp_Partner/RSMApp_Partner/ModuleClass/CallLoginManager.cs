
//Created On :: 18, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using RSMApp_MM;

namespace RSMApp_Partner
{
    public class CallLoginManager
    {
        const string CallLoginTable = "CallLogin";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        CallLoginDetailManager ObjCallLoginDetailManager = new CallLoginDetailManager();
        CallDefectTypeDetailManager objCallDefectTypeDetailManager = new CallDefectTypeDetailManager();
        CallEstimationManager objCallEstimationManager = new CallEstimationManager();
        ProductMasterManager objProductMasterManager = new ProductMasterManager();
        SerializeStockMissingPartsManager objSerializeStockMissingPartsManager = new SerializeStockMissingPartsManager();
        
        public CallLogin objGetCallLogin(string argCallCode, string argClientCode)
        {
            CallLogin argCallLogin = new CallLogin();
            DataSet DataSetToFill = new DataSet();

            if (argCallCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallLogin(argCallCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallLogin = this.objCreateCallLogin((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallLogin;
        }

        public ICollection<CallLogin> colGetCallLogin(string argClientCode)
        {
            List<CallLogin> lst = new List<CallLogin>();
            DataSet DataSetToFill = new DataSet();
            CallLogin tCallLogin = new CallLogin();

            DataSetToFill = this.GetCallLogin(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallLogin(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
       
        public DataSet GetCallLogin(string argCallCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallLogin4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallLogin(string argCallCode, string argClientCode, DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallLogin4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallLogin(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallLogin",param);
            return DataSetToFill;
        }

        public DataSet GetCallLogin4Combo(string argCallReceivedFrm, string argCallRecName, string argCallRecMobile, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CallReceivedFrm", argCallReceivedFrm);
            param[1] = new SqlParameter("@CallRecName", argCallRecName);
            param[2] = new SqlParameter("@CallRecMobile", argCallRecMobile);            
            param[3] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetCallLogin4Combo", param);
            return DataSetToFill;
            
        }

        public DataSet GetClosingCalls(string argCallReceivedFrm, string argCallRecName,string argCallRecMobile, string argCallClosingDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@CallReceivedFrm", argCallReceivedFrm);
            param[1] = new SqlParameter("@CallRecName", argCallRecName);
            param[2] = new SqlParameter("@CallRecMobile", argCallRecMobile);  
            param[3] = new SqlParameter("@CallClosingDocTypeCode", argCallClosingDocTypeCode);
            param[4] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[5] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetClosingCalls", param);
            return DataSetToFill;

        }

        public DataSet CheckProductSerialNo(string argSerialNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_CheckSerialNo4Product", param);

            return DataSetToFill;
        }

        public DataSet GetCallReceiveFrom(string argPreFix, string argCallRecFrom, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Prefix", argPreFix);
            param[1] = new SqlParameter("@CallRecFrom", argCallRecFrom);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallRecivedFrom", param);

            return DataSetToFill;
        }

        public DataSet GetCallReceiveFromDetails(string argCallRecName, string argCallRecMobile, string argCallRecFrom, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CallRecName", argCallRecName);
            param[1] = new SqlParameter("@CallRecMobile", argCallRecMobile);
            param[2] = new SqlParameter("@CallRecFrom", argCallRecFrom);
            param[3] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallRecivedFromDetails", param);

            return DataSetToFill;

        }

        public DataSet GetProductHistory(string argSerialNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetProductHistory4Search", param);

            return DataSetToFill;
        }

        private CallLogin objCreateCallLogin(DataRow dr)
        {
            CallLogin tCallLogin = new CallLogin();

            tCallLogin.SetObjectInfo(dr);

            return tCallLogin;

        }

        public ICollection<ErrorHandler> SaveCallLogin(CallLogin argCallLogin)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallLoginExists(argCallLogin.CallCode, argCallLogin.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallLogin(argCallLogin, da, lstErr);
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
                    UpdateCallLogin(argCallLogin, da, lstErr);
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

        public PartnerErrorResult SaveCallLogin(CallLogin argCallLogin, CallLoginDetailCol colCallLoginDetail, CallDefectTypeCol colCallDefectTypeDetail, CallEstimationCol colCallEstimationDetail, SerializeStockMissingPartsCol argSerializeStockMissingPartsCol)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            PartnerErrorResult errorcol = new PartnerErrorResult();
            PartnerGoodsMovementManager objPartnerGMManager = new PartnerGoodsMovementManager();

            DataAccess da = new DataAccess();
            string strretValue = "";
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsCallLoginExists(argCallLogin.CallCode, argCallLogin.ClientCode,da) == false)
                {
                    strretValue = InsertCallLogin(argCallLogin, da, lstErr);
                }
                else
                {
                    strretValue = UpdateCallLogin(argCallLogin, da, lstErr);
                }

                foreach (ErrorHandler objerr in lstErr)
                {
                    if (objerr.Type == "E")
                    {
                        errorcol.colErrorHandler.Add(objerr);
                        da.ROLLBACK_TRANSACTION();
                        return errorcol;
                    }

                    if (objerr.Type == "A")
                    {
                        errorcol.colErrorHandler.Add(objerr);
                        da.ROLLBACK_TRANSACTION();
                        return errorcol;
                    }
                }

                if (strretValue != "")
                {
                    string strGoodsMovementCode = objPartnerGMManager.GenerateGMCode("NEW", "GM01", argCallLogin.PartnerCode, argCallLogin.ClientCode, da);
                    
                    if (colCallLoginDetail.colCallLoginDetail.Count > 0)
                    {
                        foreach (CallLoginDetail argCallLoginDetail in colCallLoginDetail.colCallLoginDetail)
                        {
                            argCallLoginDetail.CallCode = Convert.ToString(strretValue);

                            if (argCallLoginDetail.IsDeleted == 0)
                            {
                                ObjCallLoginDetailManager.SaveCallLoginDetail(argCallLoginDetail, da, lstErr);
                            }
                            else
                            {
                                //ObjCallLoginDetailManager.DeleteCallLoginDetail(argCallLoginDetail.CallCode, argCallLoginDetail.ItemNo, argCallLoginDetail.ClientCode,0, da, lstErr);
                            }

                            if (colCallDefectTypeDetail.colCallDefectTypeDetail.Count > 0)
                            {
                                foreach (CallDefectTypeDetail argCallDefectTypeDetail in colCallDefectTypeDetail.colCallDefectTypeDetail)
                                {
                                    if (argCallDefectTypeDetail.ItemNo == argCallLoginDetail.ItemNo)
                                    {
                                        argCallDefectTypeDetail.CallCode = Convert.ToString(strretValue);

                                        if (argCallDefectTypeDetail.IsDeleted == 0)
                                        {
                                            objCallDefectTypeDetailManager.SaveCallDefectTypeDetail(argCallDefectTypeDetail, da, lstErr);
                                        }
                                        else
                                        {
                                            //objCallDefectTypeDetailManager.DeleteCallDefectTypeDetail(argCallDefectTypeDetail.CallCode, argCallDefectTypeDetail.ItemNo, argCallDefectTypeDetail.ClientCode, 0, da, lstErr);
                                        }
                                    }
                                }
                            }

                            if (colCallEstimationDetail.colCallEstimation.Count > 0)
                            {
                                foreach (CallEstimation argCallEstimationDetail in colCallEstimationDetail.colCallEstimation)
                                {
                                    
                                    if (argCallEstimationDetail.ItemNo == argCallLoginDetail.ItemNo)
                                    {
                                        argCallEstimationDetail.CallCode = Convert.ToString(strretValue);

                                        if (argCallEstimationDetail.IsDeleted == 0)
                                        {
                                            objCallEstimationManager.SaveCallEstimation(argCallEstimationDetail, da, lstErr);
                                        }
                                        else
                                        {

                                        }
                                    }
                                }
                            }

                            if (argSerializeStockMissingPartsCol.colSerializeStockMissingParts.Count > 0)
                            {

                                foreach (SerializeStockMissingParts objSerializeStockMissingParts in argSerializeStockMissingPartsCol.colSerializeStockMissingParts)
                                {
                                    if (Convert.ToString(objSerializeStockMissingParts.SerialNo).Trim() == Convert.ToString(argCallLoginDetail.SerialNo).Trim())
                                    {
                                        objSerializeStockMissingPartsManager.SaveSerializeStockMissingParts(objSerializeStockMissingParts, da, lstErr);
                                    }
                                }

                            }
                            


                            
                            /******************Update Product Master************************/
                            ProductMaster argProductMaster = new ProductMaster();
                            argProductMaster.SerialNo = Convert.ToString(argCallLoginDetail.SerialNo);
                            argProductMaster.MaterialCode = Convert.ToString(argCallLoginDetail.MaterialCode);                            
                            argProductMaster.MatGroup1Code = Convert.ToString(argCallLoginDetail.MatGroup1Code);
                            argProductMaster.CustType = Convert.ToString(argCallLoginDetail.CallFrom);
                            argProductMaster.CustInvoiceDate = Convert.ToDateTime(argCallLoginDetail.CustInvoiceDate);
                            argProductMaster.CustInvoiceNo = Convert.ToString(argCallLoginDetail.CustInvoiceNo);
                            argProductMaster.CustName = Convert.ToString(argCallLoginDetail.CustName);
                            argProductMaster.CustAddress1 = Convert.ToString(argCallLoginDetail.CustAddress1);
                            argProductMaster.CustAddress2 = Convert.ToString(argCallLoginDetail.CustAddress2);
                            argProductMaster.CustPhone = Convert.ToString(argCallLoginDetail.CustPhone);
                            argProductMaster.CustMobile = Convert.ToString(argCallLoginDetail.CustMobile);
                            argProductMaster.CustEmail = Convert.ToString(argCallLoginDetail.CustEmail);
                            argProductMaster.CustGender = "";
                            argProductMaster.ValidFrom = "1900-01-01";
                            argProductMaster.ValidTo = Convert.ToString(argCallLoginDetail.WarrantyEndDate);

                            if (argCallLoginDetail.WarrantyStatus == "Out Warranty")
                            {
                                if (argCallLoginDetail.OutWarrantyOn == "Product")
                                {
                                    argProductMaster.IsWarrantyExp = 1;
                                }
                                else
                                {
                                    argProductMaster.IsWarrantyExp = 0;
                                }

                            }
                            else
                            {
                                argProductMaster.IsWarrantyExp = 0;
                            }
                            argProductMaster.ClientCode = Convert.ToString(argCallLoginDetail.ClientCode);
                            argProductMaster.CreatedBy = Convert.ToString(argCallLoginDetail.CreatedBy);
                            argProductMaster.ModifiedBy = Convert.ToString(argCallLoginDetail.ModifiedBy);

                            objProductMasterManager.UpdateProductMaster4Call(argProductMaster, da, lstErr);

                            /***********************************************************/
                        }

                        foreach (ErrorHandler objerr in lstErr)
                        {
                            if (objerr.Type == "E")
                            {
                                errorcol.colErrorHandler.Add(objerr);
                                da.ROLLBACK_TRANSACTION();
                                return errorcol;
                            }

                            if (objerr.Type == "A")
                            {
                                errorcol.colErrorHandler.Add(objerr);
                                da.ROLLBACK_TRANSACTION();
                                return errorcol;
                            }
                        }

                    }

                    
                    /* Partner Goods Movement Declared */
                    PartnerGoodsMovement objPartnerGM =new PartnerGoodsMovement();

                    objPartnerGM.PGoodsMovementCode = strGoodsMovementCode;
                    objPartnerGM.PartnerGMDocTypeCode = "GM01";
                    objPartnerGM.FromPlantCode = "";
                    objPartnerGM.FromPartnerCode = "";
                    objPartnerGM.FromPartnerEmployeeCode = "";
                    objPartnerGM.FromStoreCode = "";
                    objPartnerGM.ToPartnerCode = Convert.ToString(argCallLogin.PartnerCode);
                    objPartnerGM.ToPlantCode = "";
                    objPartnerGM.ToStoreCode = "";
                    objPartnerGM.ToPartnerEmployeeCode = "";
                    objPartnerGM.ClientCode = argCallLogin.ClientCode;
                    objPartnerGM.CreatedBy = argCallLogin.CreatedBy;
                    objPartnerGM.ModifiedBy = argCallLogin.ModifiedBy;
                    objPartnerGM.TotalQuantity = 0;
                    objPartnerGM.PartnerCode = Convert.ToString(argCallLogin.PartnerCode);
                    objPartnerGM.GoodsMovDate = Convert.ToDateTime(DateTime.Now);

                    /*----------------------------------------------------------------------------------------*/
                    /* Partner Goods Movement Detail */
                    PartnerMaterialDocTypeManager  objPartnerMatDocTypeMan = new PartnerMaterialDocTypeManager();

                    PartnerGoodsMovementDetailCol objPartnerGMCol = new PartnerGoodsMovementDetailCol();
                    PartnerGoodsMovSerialDetailCol objPartnerGMSerialCol = new PartnerGoodsMovSerialDetailCol();

                    objPartnerGMCol.colPartnerGMDetail = new List<PartnerGoodsMovementDetail>();
                    objPartnerGMSerialCol.colPartnerGMSerialDetail = new List<PartnerGoodsMovSerialDetail>();

                    DataSet dsMatDocType = null;
                    bool IsNew = true;
                    int iCtr =0;
                    int tmpItemNo = 0;

                    foreach (CallLoginDetail objCallLoginDetail in colCallLoginDetail.colCallLoginDetail)
                    {
                        if (objCallLoginDetail.IsDeleted == 0  && objCallLoginDetail.IsGoodsRec == 1 && objCallLoginDetail.Quantity  > 0)
                        {
                            dsMatDocType = new DataSet();
                            iCtr = iCtr + 1;
                            tmpItemNo = iCtr;
                            if (IsNew == true)
                            {
                                

                                dsMatDocType = objPartnerMatDocTypeMan.GetPartnerMaterialDocType(objCallLoginDetail.MaterialDocTypeCode, objCallLoginDetail.ClientCode, da);

                                PartnerGoodsMovementDetail objPartnerGMDetailsnew = new PartnerGoodsMovementDetail();
                                objPartnerGMDetailsnew.PGoodsMovementCode = strGoodsMovementCode;
                                objPartnerGMDetailsnew.ItemNo = tmpItemNo;                               
                                objPartnerGMDetailsnew.MaterialCode = Convert.ToString(objCallLoginDetail.MaterialCode);
                                objPartnerGMDetailsnew.MatGroup1Code = Convert.ToString(objCallLoginDetail.MatGroup1Code);                               
                                objPartnerGMDetailsnew.Quantity = Convert.ToInt32(objCallLoginDetail.Quantity);
                                objPartnerGMDetailsnew.UOMCode = Convert.ToString(objCallLoginDetail.UOMCode);
                                objPartnerGMDetailsnew.ClientCode = Convert.ToString(objCallLoginDetail.ClientCode);
                                objPartnerGMDetailsnew.CreatedBy = Convert.ToString(objCallLoginDetail.CreatedBy);
                                objPartnerGMDetailsnew.ModifiedBy = Convert.ToString(objCallLoginDetail.ModifiedBy);
                                objPartnerGMDetailsnew.TranRefDocCode = Convert.ToString(strretValue);
                                objPartnerGMDetailsnew.TranRefDocItemNo = Convert.ToInt32(objCallLoginDetail.ItemNo);
                                objPartnerGMDetailsnew.MaterialDocTypeCode = Convert.ToString(objCallLoginDetail.MaterialDocTypeCode);
                                objPartnerGMDetailsnew.PartnerCode = Convert.ToString(objCallLoginDetail.PartnerCode);

                                if (dsMatDocType != null)
                                {
                                    if (dsMatDocType.Tables[0].Rows.Count > 0)
                                    {
                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["AllowedFromStock"]).Trim() != "HIDE")
                                        {
                                            objPartnerGMDetailsnew.StockIndicator = Convert.ToString(objCallLoginDetail.StockIndicator);
                                        }
                                        else
                                        {
                                            objPartnerGMDetailsnew.StockIndicator = Convert.ToString("");
 
                                        }
                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["AllowedToStock"]).Trim() != "HIDE")
                                        {

                                            objPartnerGMDetailsnew.ToStockIndicator = Convert.ToString(objCallLoginDetail.StockIndicator);
                                        }
                                        else
                                        {
                                            objPartnerGMDetailsnew.ToStockIndicator = "";
                                        }


                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["FromPlant"]).Trim() != "HIDE")
                                        {
                                            objPartnerGMDetailsnew.FromPlantCode = "";
                                        }
                                        else
                                        {
                                            objPartnerGMDetailsnew.FromPlantCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["FromPartner"]).Trim() != "HIDE")
                                        {
                                            objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(objCallLoginDetail.PartnerCode);
                                        }
                                        else
                                        {
                                            objPartnerGMDetailsnew.FromPartnerCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["FromStore"]).Trim() != "HIDE")
                                        {
                                            objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(objCallLoginDetail.StoreCode);
                                        }
                                        else
                                        {
                                            objPartnerGMDetailsnew.FromStoreCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["FromEmployee"]).Trim() != "HIDE")
                                        {
                                            objPartnerGMDetailsnew.FromPartnerEmployeeCode = "";
                                        }
                                        else
                                        {
                                            objPartnerGMDetailsnew.FromPartnerEmployeeCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToPartner"]).Trim() != "HIDE")
                                        {
                                            objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(objCallLoginDetail.PartnerCode);
                                        }
                                        else
                                        {
                                            objPartnerGMDetailsnew.ToPartnerCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToStore"]).Trim() != "HIDE")
                                        {
                                            objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(objCallLoginDetail.StoreCode);
                                        }
                                        else
                                        {
                                            objPartnerGMDetailsnew.ToStoreCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToEmployee"]).Trim() != "HIDE")
                                        {
                                            objPartnerGMDetailsnew.ToPartnerEmployeeCode = "";
                                        }
                                        else
                                        {
                                            objPartnerGMDetailsnew.ToPartnerEmployeeCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToPlant"]).Trim() != "HIDE")
                                        {
                                            objPartnerGMDetailsnew.ToPlantCode = "";
                                        }
                                        else
                                        {
                                            objPartnerGMDetailsnew.ToPlantCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToMaterialCode"]).Trim() != "HIDE")
                                        {
                                            objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(objCallLoginDetail.MaterialCode);
                                        }
                                        else
                                        {
                                            objPartnerGMDetailsnew.ToMaterialCode = "";
                                        }
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.FromPlantCode = "";
                                        objPartnerGMDetailsnew.FromPartnerCode = "";
                                        objPartnerGMDetailsnew.FromPartnerEmployeeCode = "";
                                        objPartnerGMDetailsnew.FromStoreCode = "";
                                        objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(objCallLoginDetail.PartnerCode);
                                        objPartnerGMDetailsnew.ToPartnerEmployeeCode = "";
                                        objPartnerGMDetailsnew.ToPlantCode = "";
                                        objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(objCallLoginDetail.StoreCode);
                                        objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(objCallLoginDetail.MaterialCode);

                                    }
                                }

                                objPartnerGMCol.colPartnerGMDetail.Add(objPartnerGMDetailsnew);

                                /*Partner Goods Movement Serial Detail*/

                                PartnerGoodsMovSerialDetail objPartnerGoodsMovSerialnew = new PartnerGoodsMovSerialDetail();
                                objPartnerGoodsMovSerialnew.PGoodsMovementCode = strGoodsMovementCode;
                                objPartnerGoodsMovSerialnew.ItemNo = tmpItemNo;
                                objPartnerGoodsMovSerialnew.SerialNo1 = Convert.ToString(objCallLoginDetail.SerialNo);
                                objPartnerGoodsMovSerialnew.SerialNo2 = "";
                                objPartnerGoodsMovSerialnew.MaterialCode = Convert.ToString(objCallLoginDetail.MaterialCode);
                                objPartnerGoodsMovSerialnew.MatGroup1Code = Convert.ToString(objCallLoginDetail.MatGroup1Code);
                                objPartnerGoodsMovSerialnew.RefDocCode = Convert.ToString(objCallLoginDetail.CallCode);
                                objPartnerGoodsMovSerialnew.RefDocItemNo = Convert.ToInt32(objCallLoginDetail.ItemNo);
                                objPartnerGoodsMovSerialnew.RefDocType = Convert.ToString(objCallLoginDetail.CallTypeCode);
                                objPartnerGoodsMovSerialnew.IsDeleted = 0;
                                objPartnerGoodsMovSerialnew.TranRefDocCode = Convert.ToString(objCallLoginDetail.CallCode);
                                objPartnerGoodsMovSerialnew.TranRefDocItemNo = Convert.ToInt32(objCallLoginDetail.ItemNo);
                                objPartnerGoodsMovSerialnew.ClientCode = Convert.ToString(objCallLoginDetail.ClientCode);
                                objPartnerGoodsMovSerialnew.CreatedBy = Convert.ToString(objCallLoginDetail.CreatedBy);
                                objPartnerGoodsMovSerialnew.ModifiedBy = Convert.ToString(objCallLoginDetail.ModifiedBy);
                              
                                if (dsMatDocType != null)
                                {
                                    if (dsMatDocType.Tables[0].Rows.Count > 0)
                                    {
                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["AllowedFromStock"]).Trim() != "HIDE")
                                        {
                                            objPartnerGoodsMovSerialnew.StockIndicator = Convert.ToString(objCallLoginDetail.StockIndicator);
                                        }
                                        else
                                        {
                                            objPartnerGoodsMovSerialnew.StockIndicator = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["AllowedToStock"]).Trim() != "HIDE")
                                        {
                                            objPartnerGoodsMovSerialnew.ToStockIndicator = Convert.ToString(objCallLoginDetail.StockIndicator);
                                        }
                                        else
                                        {
                                            objPartnerGoodsMovSerialnew.ToStockIndicator = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["FromPlant"]).Trim() != "HIDE")
                                        {
                                            objPartnerGoodsMovSerialnew.PlantCode = "";
                                        }
                                        else
                                        {
                                            objPartnerGoodsMovSerialnew.PlantCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["FromPartner"]).Trim() != "HIDE")
                                        {
                                            objPartnerGoodsMovSerialnew.PartnerCode = Convert.ToString(objCallLoginDetail.PartnerCode);
                                        }
                                        else
                                        {
                                            objPartnerGoodsMovSerialnew.PartnerCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["FromStore"]).Trim() != "HIDE")
                                        {
                                            objPartnerGoodsMovSerialnew.StoreCode = Convert.ToString(objCallLoginDetail.StoreCode);
                                        }
                                        else
                                        {
                                            objPartnerGoodsMovSerialnew.StoreCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["FromEmployee"]).Trim() != "HIDE")
                                        {
                                            objPartnerGoodsMovSerialnew.PartnerEmployeeCode = "";
                                        }
                                        else
                                        {
                                            objPartnerGoodsMovSerialnew.PartnerEmployeeCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToPartner"]).Trim() != "HIDE")
                                        {
                                            objPartnerGoodsMovSerialnew.ToPartnerCode = Convert.ToString(objCallLoginDetail.PartnerCode);
                                        }
                                        else
                                        {
                                            objPartnerGoodsMovSerialnew.ToPartnerCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToStore"]).Trim() != "HIDE")
                                        {
                                            objPartnerGoodsMovSerialnew.ToStoreCode = Convert.ToString(objCallLoginDetail.StoreCode);
                                        }
                                        else
                                        {
                                            objPartnerGoodsMovSerialnew.ToStoreCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToEmployee"]).Trim() != "HIDE")
                                        {
                                            objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = "";
                                        }
                                        else
                                        {
                                            objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToPlant"]).Trim() != "HIDE")
                                        {
                                            objPartnerGoodsMovSerialnew.ToPlantCode = "";
                                        }
                                        else
                                        {
                                            objPartnerGoodsMovSerialnew.ToPlantCode = "";
                                        }

                                        if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToMaterialCode"]).Trim() != "HIDE")
                                        {
                                            objPartnerGoodsMovSerialnew.ToMaterialCode = Convert.ToString(objCallLoginDetail.MaterialCode);
                                        }
                                        else
                                        {
                                            objPartnerGoodsMovSerialnew.ToMaterialCode = "";
                                        }


                                    }
                                    else
                                    {
                                        //objPartnerGoodsMovSerialnew.FromPlantCode = "";
                                        //objPartnerGMDetailsnew.FromPartnerCode = "";
                                        //objPartnerGMDetailsnew.FromPartnerEmployeeCode = "";
                                        //objPartnerGMDetailsnew.FromStoreCode = "";
                                        //objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(objCallLoginDetail.PartnerCode);
                                        //objPartnerGMDetailsnew.ToPartnerEmployeeCode = "";
                                        //objPartnerGMDetailsnew.ToPlantCode = "";
                                        //objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(objCallLoginDetail.StoreCode);
                                        //objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(objCallLoginDetail.MaterialCode);

                                    }
                                }
                                                               
                               
                                objPartnerGMSerialCol.colPartnerGMSerialDetail.Add(objPartnerGoodsMovSerialnew);

                                /*----------------------------------------------------------------------------------------------------------*/
                            }

                            
                        }
                        /*-----------------------------------------------------------------------------------------------------------*/
                       

                    }
                    /*Partner Goods Movement Save*/
                    if (objPartnerGMCol.colPartnerGMDetail.Count > 0)
                    {

                        objPartnerGMManager.SavePartnerGoodsMovement(objPartnerGM, objPartnerGMCol, objPartnerGMSerialCol, da, lstErr);

                        foreach (ErrorHandler objerr in lstErr)
                        {
                            if (objerr.Type == "E")
                            {
                                errorcol.colErrorHandler.Add(objerr);
                                da.ROLLBACK_TRANSACTION();
                                return errorcol;
                            }

                            if (objerr.Type == "A")
                            {
                                errorcol.colErrorHandler.Add(objerr);
                                da.ROLLBACK_TRANSACTION();
                                return errorcol;
                            }
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
                errorcol.colErrorHandler.Add(objErrorHandler);
            }
            finally
            {
                if (da != null)
                {
                    da.Close_Connection();
                    da = null;
                }
            }
            return errorcol;


        }

        public string InsertCallLogin(CallLogin argCallLogin, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[26];
            param[0] = new SqlParameter("@CallCode", argCallLogin.CallCode);
            param[1] = new SqlParameter("@RepairDocTypeCode", argCallLogin.RepairDocTypeCode);
            param[2] = new SqlParameter("@CallDate", argCallLogin.CallDate);
            param[3] = new SqlParameter("@CallAttendedBY", argCallLogin.CallAttendedBY);
            param[4] = new SqlParameter("@ReceivingDate", argCallLogin.ReceivingDate);
            param[5] = new SqlParameter("@CallReceivedFrm", argCallLogin.CallReceivedFrm);
            param[6] = new SqlParameter("@CallRecName", argCallLogin.CallRecName);
            param[7] = new SqlParameter("@CallRecAddress1", argCallLogin.CallRecAddress1);
            param[8] = new SqlParameter("@CallRecAddress2", argCallLogin.CallRecAddress2);
            param[9] = new SqlParameter("@CallRecPhone", argCallLogin.CallRecPhone);
            param[10] = new SqlParameter("@CallRecMobile", argCallLogin.CallRecMobile);
            param[11] = new SqlParameter("@CallRecEmail", argCallLogin.CallRecEmail);
            param[12] = new SqlParameter("@CallRecGender", argCallLogin.CallRecGender);
            param[13] = new SqlParameter("@CallRecCountryCode", argCallLogin.CallRecCountryCode);
            param[14] = new SqlParameter("@CallRecStateCode", argCallLogin.CallRecStateCode);
            param[15] = new SqlParameter("@CallRecCity", argCallLogin.CallRecCity);
            param[16] = new SqlParameter("@CallStatus", argCallLogin.CallStatus);
            param[17] = new SqlParameter("@IsCallClosed", argCallLogin.IsCallClosed);
            param[18] = new SqlParameter("@IsInvGen", argCallLogin.IsInvGen);
            param[19] = new SqlParameter("@PartnerCode", argCallLogin.PartnerCode);
            param[20] = new SqlParameter("@ClientCode", argCallLogin.ClientCode);
            param[21] = new SqlParameter("@CreatedBy", argCallLogin.CreatedBy);
            param[22] = new SqlParameter("@ModifiedBy", argCallLogin.ModifiedBy);
      
            param[23] = new SqlParameter("@Type", SqlDbType.Char);
            param[23].Size = 1;
            param[23].Direction = ParameterDirection.Output;

            param[24] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[24].Size = 255;
            param[24].Direction = ParameterDirection.Output;

            param[25] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[25].Size = 20;
            param[25].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallLogin", param);


            string strMessage = Convert.ToString(param[24].Value);
            string strType = Convert.ToString(param[23].Value);
            string strRetValue = Convert.ToString(param[25].Value);


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

            return(strRetValue);

        }

        public string UpdateCallLogin(CallLogin argCallLogin, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[26];
            param[0] = new SqlParameter("@CallCode", argCallLogin.CallCode);
            param[1] = new SqlParameter("@RepairDocTypeCode", argCallLogin.RepairDocTypeCode);
            param[2] = new SqlParameter("@CallDate", argCallLogin.CallDate);
            param[3] = new SqlParameter("@CallAttendedBY", argCallLogin.CallAttendedBY);
            param[4] = new SqlParameter("@ReceivingDate", argCallLogin.ReceivingDate);
            param[5] = new SqlParameter("@CallReceivedFrm", argCallLogin.CallReceivedFrm);
            param[6] = new SqlParameter("@CallRecName", argCallLogin.CallRecName);
            param[7] = new SqlParameter("@CallRecAddress1", argCallLogin.CallRecAddress1);
            param[8] = new SqlParameter("@CallRecAddress2", argCallLogin.CallRecAddress2);
            param[9] = new SqlParameter("@CallRecPhone", argCallLogin.CallRecPhone);
            param[10] = new SqlParameter("@CallRecMobile", argCallLogin.CallRecMobile);
            param[11] = new SqlParameter("@CallRecEmail", argCallLogin.CallRecEmail);
            param[12] = new SqlParameter("@CallRecGender", argCallLogin.CallRecGender);
            param[13] = new SqlParameter("@CallRecCountryCode", argCallLogin.CallRecCountryCode);
            param[14] = new SqlParameter("@CallRecStateCode", argCallLogin.CallRecStateCode);
            param[15] = new SqlParameter("@CallRecCity", argCallLogin.CallRecCity);
            param[16] = new SqlParameter("@CallStatus", argCallLogin.CallStatus);
            param[17] = new SqlParameter("@IsCallClosed", argCallLogin.IsCallClosed);
            param[18] = new SqlParameter("@IsInvGen", argCallLogin.IsInvGen);
            param[19] = new SqlParameter("@PartnerCode", argCallLogin.PartnerCode);
            param[20] = new SqlParameter("@ClientCode", argCallLogin.ClientCode);
            param[21] = new SqlParameter("@CreatedBy", argCallLogin.CreatedBy);
            param[22] = new SqlParameter("@ModifiedBy", argCallLogin.ModifiedBy);

            param[23] = new SqlParameter("@Type", SqlDbType.Char);
            param[23].Size = 1;
            param[23].Direction = ParameterDirection.Output;

            param[24] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[24].Size = 255;
            param[24].Direction = ParameterDirection.Output;

            param[25] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[25].Size = 20;
            param[25].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallLogin", param);


            string strMessage = Convert.ToString(param[24].Value);
            string strType = Convert.ToString(param[23].Value);
            string strRetValue = Convert.ToString(param[25].Value);


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

            return (strRetValue);
        }

        public ICollection<ErrorHandler> DeleteCallLogin(string argCallCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CallCode", argCallCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCallLogin", param);


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

        public bool blnIsCallLoginExists(string argCallCode, string argClientCode)
        {
            bool IsCallLoginExists = false;
            DataSet ds = new DataSet();
            ds = GetCallLogin(argCallCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallLoginExists = true;
            }
            else
            {
                IsCallLoginExists = false;
            }
            return IsCallLoginExists;
        }

        public bool blnIsCallLoginExists(string argCallCode, string argClientCode, DataAccess da)
        {
            bool IsCallLoginExists = false;
            DataSet ds = new DataSet();
            ds = GetCallLogin(argCallCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallLoginExists = true;
            }
            else
            {
                IsCallLoginExists = false;
            }
            return IsCallLoginExists;
        }
        
        public string GenerateCallCode(string argCallCode, string argRepairDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@RepairDocTypeCode", argRepairDocTypeCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            param[4] = new SqlParameter("@GeneratedCallCode", SqlDbType.VarChar);
            param[4].Size = 18;
            param[4].Direction = ParameterDirection.Output;

            int i = da.ExecuteNonQuery("SL_GetNewCallCode", param);

            string strMessage = Convert.ToString(param[4].Value);


            return strMessage;

        }

        public PartnerErrorResult SaveProductMaster4NewBarcode(ProductMaster argProductMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            PartnerErrorResult errorcol = new PartnerErrorResult();
           ProductMasterManager objProductMasterManager = new ProductMasterManager();

            DataAccess da = new DataAccess();
            string strretValue = "";
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                objProductMasterManager.SaveProductMaster(argProductMaster, da, lstErr);

                foreach (ErrorHandler objerr in lstErr)
                {
                    if (objerr.Type == "E")
                    {
                        errorcol.colErrorHandler.Add(objerr);
                        da.ROLLBACK_TRANSACTION();
                        return errorcol;
                    }

                    if (objerr.Type == "A")
                    {
                        errorcol.colErrorHandler.Add(objerr);
                        da.ROLLBACK_TRANSACTION();
                        return errorcol;
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
                errorcol.colErrorHandler.Add(objErrorHandler);
            }
            finally
            {
                if (da != null)
                {
                    da.Close_Connection();
                    da = null;
                }
            }
            return errorcol;

        }

    }
}