
//Created On :: 08, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallClosingDetailsManager
    {
        const string CallClosingDetailsTable = "CallClosingDetails";
        
        ErrorHandler objErrorHandler = new ErrorHandler();
        CallBillingDocMasterManager objCallBillingDocMasterManager = new CallBillingDocMasterManager();
        CallBillingDocManager objCallBillingDocManager = new CallBillingDocManager();
        
        public CallClosingDetails objGetCallClosingDetails(string argCallClosingCode, string argCallCode, int argCallItemNo, string argPartnerCode, string argClientCode)
        {
            CallClosingDetails argCallClosingDetails = new CallClosingDetails();
            DataSet DataSetToFill = new DataSet();

            if (argCallClosingCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argCallCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argCallItemNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallClosingDetails(argCallClosingCode, argCallCode, argCallItemNo, argPartnerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallClosingDetails = this.objCreateCallClosingDetails((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallClosingDetails;
        }
        
        public ICollection<CallClosingDetails> colGetCallClosingDetails(string argCallClosingCode, string argCallCode, string argPartnerCode, string argClientCode)
        {
            List<CallClosingDetails> lst = new List<CallClosingDetails>();
            DataSet DataSetToFill = new DataSet();
            CallClosingDetails tCallClosingDetails = new CallClosingDetails();

            DataSetToFill = this.GetCallClosingDetails(argCallClosingCode, argCallCode, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallClosingDetails(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public void colGetCallClosingDetails(string argCallClosingCode, string argCallCode, string argPartnerCode, string argClientCode, ref CallClosingDetailCol argCallClosingDetailCol)
        {
            DataSet DataSetToFill = new DataSet();
            CallClosingDetails tCallClosingDetails = new CallClosingDetails();

            DataSetToFill = this.GetCallClosingDetails(argCallClosingCode, argCallCode, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argCallClosingDetailCol.colCallClosingDetail.Add(objCreateCallClosingDetails(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;
        
        }
        
        public DataSet GetCallClosingDetails(string argCallClosingCode, string argCallCode, int argCallItemNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CallClosingCode", argCallClosingCode);
            param[1] = new SqlParameter("@CallCode", argCallCode);
            param[2] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[3] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallClosingDetails4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallClosingDetails(string argCallClosingCode, string argCallCode, int argCallItemNo, string argPartnerCode, string argClientCode, DataAccess da)
        {            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CallClosingCode", argCallClosingCode);
            param[1] = new SqlParameter("@CallCode", argCallCode);
            param[2] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[3] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallClosingDetails4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCallClosingDetails(string argCallClosingCode, string argCallCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@CallClosingCode", argCallClosingCode);
            param[1] = new SqlParameter("@CallCode", argCallCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallClosingDetails", param);
            return DataSetToFill;
        }

        public DataSet GetCallClosingDetails4Search(string argDateFrom, string argDateTo, string argCallClosingDocTypeCode, string argCallCode, string argSerialNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[7];

            param[0] = new SqlParameter("@DateFrom ", argDateFrom);
            param[1] = new SqlParameter("@DateTo ", argDateTo);
            param[2] = new SqlParameter("@CallClosingDocTypeCode ", argCallClosingDocTypeCode);
            param[3] = new SqlParameter("@CallCode", argCallCode);
            param[4] = new SqlParameter("@SerialNo", argSerialNo);
            param[5] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[6] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallClosingDetail4Search", param);
            return DataSetToFill;


        }
        
        private CallClosingDetails objCreateCallClosingDetails(DataRow dr)
        {
            CallClosingDetails tCallClosingDetails = new CallClosingDetails();

            tCallClosingDetails.SetObjectInfo(dr);

            return tCallClosingDetails;
        }
        
        public ICollection<ErrorHandler> SaveCallClosingDetails(CallClosingDetails argCallClosingDetails)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallClosingDetailsExists(argCallClosingDetails.CallClosingCode, argCallClosingDetails.CallCode, argCallClosingDetails.CallItemNo, argCallClosingDetails.PartnerCode, argCallClosingDetails.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallClosingDetails(argCallClosingDetails, da, lstErr);
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
                    UpdateCallClosingDetails(argCallClosingDetails, da, lstErr);
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

        public PartnerErrorResult SaveCallClosingDetails(CallClosingMaster argCallClosingMaster,  CallClosingDetailCol argCallClosingDetailCol, CallBillingDocMaster argCallBillingDocMaster,  CallBillingDocCol argCallBillingDocCol, string argPartnerCode, string argClientCode, string argUserName)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            PartnerErrorResult errorcol = new PartnerErrorResult();
            PartnerGoodsMovementManager objPartnerGMManager = new PartnerGoodsMovementManager();
            CallClosingMasterManager objCallClosingMasterManager = new CallClosingMasterManager();
            DataAccess da = new DataAccess();
            string strRetValue = "";
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                string strGoodsMovementCode = "";
                int ictr = 0;
                strGoodsMovementCode = objPartnerGMManager.GenerateGMCode("NEW", "GM01", argPartnerCode, argClientCode, da);

                if (argCallClosingMaster != null)
                {
                    objCallClosingMasterManager.SaveCallClosingMaster(argCallClosingMaster, da, lstErr);
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

                foreach (CallClosingDetails argCallClosingDetails in argCallClosingDetailCol.colCallClosingDetail)
                {
                    ictr = ictr + 1;

                    if (blnIsCallClosingDetailsExists(argCallClosingDetails.CallClosingCode, argCallClosingDetails.CallCode, argCallClosingDetails.CallItemNo, argCallClosingDetails.PartnerCode, argCallClosingDetails.ClientCode, da) == false)
                    {
                        argCallClosingDetails.PGoodsMovementCode = strGoodsMovementCode;
                        argCallClosingDetails.GMItemNo = ictr;

                        InsertCallClosingDetails(argCallClosingDetails, da, lstErr);
                    }
                    else
                    {
                        UpdateCallClosingDetails(argCallClosingDetails, da, lstErr);
                    }                
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

                /*--------------------Billing Document---------------------------------------------------------------*/

                if (argCallBillingDocMaster != null)
                {
                    if (argCallBillingDocCol.colCallBillingDoc.Count > 0)
                    {

                        string srtNewCallBillingDocCode = "";
                        srtNewCallBillingDocCode = objCallBillingDocMasterManager.GenerateCallBillingDocCode(argCallBillingDocMaster.CallBillingDocCode, argCallBillingDocMaster.CallBillingDocTypeCode, argCallBillingDocMaster.PartnerCode, argCallBillingDocMaster.ClientCode, da);

                        argCallBillingDocMaster.CallBillingDocCode = srtNewCallBillingDocCode;
                        strRetValue = objCallBillingDocMasterManager.SaveCallBillingDocMaster(argCallBillingDocMaster, da, lstErr);

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

                        if (strRetValue != "")
                        {
                            foreach (CallBillingDoc objCallBillingDoc in argCallBillingDocCol.colCallBillingDoc)
                            {
                                objCallBillingDoc.CallBillingDocCode = strRetValue;
                                objCallBillingDocManager.SaveCallBillingDoc(objCallBillingDoc, da, lstErr);
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
                    }
                }
                
                /*--------------------------------------------------------------------------------------------*/




                /* Partner Goods Movement Declared */
                PartnerGoodsMovement objPartnerGM = new PartnerGoodsMovement();

                objPartnerGM.PGoodsMovementCode = strGoodsMovementCode;
                objPartnerGM.PartnerGMDocTypeCode = "GM01";
                objPartnerGM.FromPlantCode = "";
                objPartnerGM.FromPartnerCode = argPartnerCode;
                objPartnerGM.FromPartnerEmployeeCode = "";
                objPartnerGM.FromStoreCode = "";
                objPartnerGM.ToPartnerCode = "";
                objPartnerGM.ToPlantCode = "";
                objPartnerGM.ToStoreCode = "";
                objPartnerGM.ToPartnerEmployeeCode = "";
                objPartnerGM.ClientCode = argClientCode;
                objPartnerGM.CreatedBy = argUserName;
                objPartnerGM.ModifiedBy = argUserName;
                objPartnerGM.TotalQuantity = 0;
                objPartnerGM.PartnerCode = Convert.ToString(argPartnerCode);
                objPartnerGM.GoodsMovDate = Convert.ToDateTime(DateTime.Now);
                /*----------------------------------------------------------------------------------------*/
                /* Partner Goods Movement Detail */
                PartnerMaterialDocTypeManager objPartnerMatDocTypeMan = new PartnerMaterialDocTypeManager();

                PartnerGoodsMovementDetailCol objPartnerGMCol = new PartnerGoodsMovementDetailCol();
                PartnerGoodsMovSerialDetailCol objPartnerGMSerialCol = new PartnerGoodsMovSerialDetailCol();

                objPartnerGMCol.colPartnerGMDetail = new List<PartnerGoodsMovementDetail>();
                objPartnerGMSerialCol.colPartnerGMSerialDetail = new List<PartnerGoodsMovSerialDetail>();

                DataSet dsMatDocType = null;
                bool IsNew = true;              
                int tmpItemNo = 0;

                ictr = 0;
                foreach (CallClosingDetails argCallClosingDetails in argCallClosingDetailCol.colCallClosingDetail)
                {
                    if (argCallClosingDetails.IsDeleted == 0)
                    {
                        dsMatDocType = new DataSet();
                        if (IsNew == true)
                        {
                            ictr = ictr + 1;
                            dsMatDocType = objPartnerMatDocTypeMan.GetPartnerMaterialDocType(argCallClosingDetails.MaterialDocTypeCode, argCallClosingDetails.ClientCode, da);

                            PartnerGoodsMovementDetail objPartnerGMDetailsnew = new PartnerGoodsMovementDetail();
                            objPartnerGMDetailsnew.PGoodsMovementCode = strGoodsMovementCode;
                            objPartnerGMDetailsnew.ItemNo = ictr;
                            tmpItemNo = ictr;
                            objPartnerGMDetailsnew.MaterialCode = Convert.ToString(argCallClosingDetails.MaterialCode);
                            objPartnerGMDetailsnew.MatGroup1Code = Convert.ToString(argCallClosingDetails.MatGroup1Code);
                            objPartnerGMDetailsnew.StockIndicator = Convert.ToString(argCallClosingDetails.StockIndicator);
                            objPartnerGMDetailsnew.ToStockIndicator = Convert.ToString("");
                            objPartnerGMDetailsnew.Quantity = Convert.ToInt32(argCallClosingDetails.Quantity);
                            objPartnerGMDetailsnew.UOMCode = Convert.ToString(argCallClosingDetails.UOMCode);
                            objPartnerGMDetailsnew.ClientCode = Convert.ToString(argCallClosingDetails.ClientCode);
                            objPartnerGMDetailsnew.CreatedBy = Convert.ToString(argCallClosingDetails.CreatedBy);
                            objPartnerGMDetailsnew.ModifiedBy = Convert.ToString(argCallClosingDetails.ModifiedBy);
                            objPartnerGMDetailsnew.TranRefDocCode = Convert.ToString(argCallClosingDetails.CallClosingCode);
                            objPartnerGMDetailsnew.TranRefDocItemNo = Convert.ToInt32(ictr);
                            objPartnerGMDetailsnew.MaterialDocTypeCode = Convert.ToString(argCallClosingDetails.MaterialDocTypeCode);
                            objPartnerGMDetailsnew.PartnerCode = Convert.ToString(argPartnerCode);


                            if (dsMatDocType != null)
                            {
                                if (dsMatDocType.Tables[0].Rows.Count > 0)
                                {
                                    if (dsMatDocType.Tables[0].Rows[0]["FromPlant"].ToString() != "Hide")
                                    {
                                        objPartnerGMDetailsnew.FromPlantCode = "";
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.FromPlantCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["FromPartner"].ToString() != "Hide")
                                    {
                                        objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(argCallClosingDetails.PartnerCode);
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.FromPartnerCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["FromStore"].ToString() != "Hide")
                                    {
                                        objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(argCallClosingDetails.StoreCode);
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.FromStoreCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["FromEmployee"].ToString() != "Hide")
                                    {
                                        objPartnerGMDetailsnew.FromPartnerEmployeeCode = "";
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.FromPartnerEmployeeCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["ToPartner"].ToString() != "Hide")
                                    {
                                        objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString("");
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.ToPartnerCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["ToStore"].ToString() != "Hide")
                                    {
                                        objPartnerGMDetailsnew.ToStoreCode = Convert.ToString("");
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.ToStoreCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["ToEmployee"].ToString() != "Hide")
                                    {
                                        objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString("");
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.ToPartnerEmployeeCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["ToPlant"].ToString() != "Hide")
                                    {
                                        objPartnerGMDetailsnew.ToPlantCode = "";
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.ToPlantCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["ToMaterialCode"].ToString() != "Hide")
                                    {
                                        objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString("");
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.ToMaterialCode = "";
                                    }


                                }
                                else
                                {
                                    objPartnerGMDetailsnew.FromPlantCode = "";
                                    objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(argCallClosingDetails.PartnerCode);
                                    objPartnerGMDetailsnew.FromPartnerEmployeeCode = "";
                                    objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(argCallClosingDetails.StoreCode);
                                    objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString("");
                                    objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString("");
                                    objPartnerGMDetailsnew.ToPlantCode = "";
                                    objPartnerGMDetailsnew.ToStoreCode = Convert.ToString("");
                                    objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString("");
                                }
                            }
                            objPartnerGMCol.colPartnerGMDetail.Add(objPartnerGMDetailsnew);

                            /*Partner Goods Movement Serial Detail*/

                            PartnerGoodsMovSerialDetail objPartnerGoodsMovSerialnew = new PartnerGoodsMovSerialDetail();
                            objPartnerGoodsMovSerialnew.PGoodsMovementCode = strGoodsMovementCode;
                            objPartnerGoodsMovSerialnew.ItemNo = tmpItemNo;
                            objPartnerGoodsMovSerialnew.SerialNo1 = Convert.ToString(argCallClosingDetails.SerialNo1);
                            objPartnerGoodsMovSerialnew.SerialNo2 = Convert.ToString(argCallClosingDetails.SerialNo2);
                            objPartnerGoodsMovSerialnew.MaterialCode = Convert.ToString(argCallClosingDetails.MaterialCode);
                            objPartnerGoodsMovSerialnew.MatGroup1Code = Convert.ToString(argCallClosingDetails.MatGroup1Code);
                            objPartnerGoodsMovSerialnew.RefDocCode = Convert.ToString(argCallClosingDetails.CallCode);
                            objPartnerGoodsMovSerialnew.RefDocItemNo = Convert.ToInt32(argCallClosingDetails.CallItemNo);
                            objPartnerGoodsMovSerialnew.RefDocType = Convert.ToString("");
                            objPartnerGoodsMovSerialnew.TranRefDocCode = Convert.ToString(argCallClosingDetails.CallClosingCode);
                            objPartnerGoodsMovSerialnew.TranRefDocItemNo = Convert.ToInt32(ictr);
                            objPartnerGoodsMovSerialnew.IsDeleted = 0;
                            objPartnerGoodsMovSerialnew.ClientCode = Convert.ToString(argCallClosingDetails.ClientCode);
                            objPartnerGoodsMovSerialnew.CreatedBy = Convert.ToString(argCallClosingDetails.CreatedBy);
                            objPartnerGoodsMovSerialnew.ModifiedBy = Convert.ToString(argCallClosingDetails.ModifiedBy);
                            objPartnerGoodsMovSerialnew.StockIndicator = Convert.ToString(argCallClosingDetails.StockIndicator);
                            objPartnerGoodsMovSerialnew.ToStockIndicator = Convert.ToString(argCallClosingDetails.StockIndicator);

                            if (dsMatDocType != null)
                            {
                                if (dsMatDocType.Tables[0].Rows.Count > 0)
                                {
                                    if (dsMatDocType.Tables[0].Rows[0]["FromPlant"].ToString() != "Hide")
                                    {
                                        objPartnerGoodsMovSerialnew.PlantCode = "";
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.PlantCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["FromPartner"].ToString() != "Hide")
                                    {
                                        objPartnerGoodsMovSerialnew.PartnerCode = Convert.ToString(argCallClosingDetails.PartnerCode);
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.PartnerCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["FromStore"].ToString() != "Hide")
                                    {
                                        objPartnerGoodsMovSerialnew.StoreCode = Convert.ToString(argCallClosingDetails.StoreCode);
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.StoreCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["FromEmployee"].ToString() != "Hide")
                                    {
                                        objPartnerGoodsMovSerialnew.PartnerEmployeeCode = "";
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.PartnerEmployeeCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["ToPartner"].ToString() != "Hide")
                                    {
                                        objPartnerGoodsMovSerialnew.ToPartnerCode = Convert.ToString("");
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.ToPartnerCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["ToStore"].ToString() != "Hide")
                                    {
                                        objPartnerGoodsMovSerialnew.ToStoreCode = Convert.ToString("");
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.ToStoreCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["ToEmployee"].ToString() != "Hide")
                                    {
                                        objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = Convert.ToString("");
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["ToPlant"].ToString() != "Hide")
                                    {
                                        objPartnerGoodsMovSerialnew.ToPlantCode = "";
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.ToPlantCode = "";
                                    }

                                    if (dsMatDocType.Tables[0].Rows[0]["ToMaterialCode"].ToString() != "Hide")
                                    {
                                        objPartnerGoodsMovSerialnew.ToMaterialCode = Convert.ToString("");
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.ToMaterialCode = "";
                                    }


                                }
                                else
                                {
                                    objPartnerGoodsMovSerialnew.PlantCode = "";
                                    objPartnerGoodsMovSerialnew.PartnerCode = Convert.ToString(argCallClosingDetails.PartnerCode);
                                    objPartnerGoodsMovSerialnew.PartnerEmployeeCode = "";
                                    objPartnerGoodsMovSerialnew.StoreCode = Convert.ToString(argCallClosingDetails.StoreCode);
                                    objPartnerGoodsMovSerialnew.ToPartnerCode = Convert.ToString("");
                                    objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = Convert.ToString("");
                                    objPartnerGoodsMovSerialnew.ToPlantCode = "";
                                    objPartnerGoodsMovSerialnew.ToStoreCode = Convert.ToString("");
                                    objPartnerGoodsMovSerialnew.ToMaterialCode = Convert.ToString("");
                                }
                            }
                            
                            objPartnerGMSerialCol.colPartnerGMSerialDetail.Add(objPartnerGoodsMovSerialnew);

                            /*----------------------------------------------------------------------------------------------------------*/
                        }
                    }
                }
                /*Partner Goods Movement Save*/
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
        
        public void InsertCallClosingDetails(CallClosingDetails argCallClosingDetails, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[25];
            param[0] = new SqlParameter("@CallClosingCode", argCallClosingDetails.CallClosingCode);
            param[1] = new SqlParameter("@CallClosingDocTypeCode", argCallClosingDetails.CallClosingDocTypeCode);
            param[2] = new SqlParameter("@CallCode", argCallClosingDetails.CallCode);
            param[3] = new SqlParameter("@CallItemNo", argCallClosingDetails.CallItemNo);
            param[4] = new SqlParameter("@CallClosingDate", argCallClosingDetails.CallClosingDate);
            param[5] = new SqlParameter("@SerialNo1", argCallClosingDetails.SerialNo1);
            param[6] = new SqlParameter("@SerialNo2", argCallClosingDetails.SerialNo2);
            param[7] = new SqlParameter("@MaterialCode", argCallClosingDetails.MaterialCode);
            param[8] = new SqlParameter("@MatGroup1Code", argCallClosingDetails.MatGroup1Code);
            param[9] = new SqlParameter("@PartnerCode", argCallClosingDetails.PartnerCode);
            param[10] = new SqlParameter("@StoreCode", argCallClosingDetails.StoreCode);
            param[11] = new SqlParameter("@StockIndicator", argCallClosingDetails.StockIndicator);
            param[12] = new SqlParameter("@PartnerEmployeeCode", argCallClosingDetails.PartnerEmployeeCode);
            param[13] = new SqlParameter("@Quantity", argCallClosingDetails.Quantity);
            param[14] = new SqlParameter("@UOMCode", argCallClosingDetails.UOMCode);
            param[15] = new SqlParameter("@MaterialDocTypeCode", argCallClosingDetails.MaterialDocTypeCode);
            param[16] = new SqlParameter("@PGoodsMovementCode", argCallClosingDetails.PGoodsMovementCode);
            param[17] = new SqlParameter("@GMItemNo", argCallClosingDetails.GMItemNo);

            param[18] = new SqlParameter("@PayableAmt", argCallClosingDetails.PayableAmt);

            param[19] = new SqlParameter("@ClientCode", argCallClosingDetails.ClientCode);
            param[20] = new SqlParameter("@CreatedBy", argCallClosingDetails.CreatedBy);
            param[21] = new SqlParameter("@ModifiedBy", argCallClosingDetails.ModifiedBy);
            
            param[22] = new SqlParameter("@Type", SqlDbType.Char);
            param[22].Size = 1;
            param[22].Direction = ParameterDirection.Output;

            param[23] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[23].Size = 255;
            param[23].Direction = ParameterDirection.Output;

            param[24] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[24].Size = 20;
            param[24].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallClosingDetails", param);


            string strMessage = Convert.ToString(param[23].Value);
            string strType = Convert.ToString(param[22].Value);
            string strRetValue = Convert.ToString(param[24].Value);


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
        
        public void UpdateCallClosingDetails(CallClosingDetails argCallClosingDetails, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[25];
            param[0] = new SqlParameter("@CallClosingCode", argCallClosingDetails.CallClosingCode);
            param[1] = new SqlParameter("@CallClosingDocTypeCode", argCallClosingDetails.CallClosingDocTypeCode);
            param[2] = new SqlParameter("@CallCode", argCallClosingDetails.CallCode);
            param[3] = new SqlParameter("@CallItemNo", argCallClosingDetails.CallItemNo);
            param[4] = new SqlParameter("@CallClosingDate", argCallClosingDetails.CallClosingDate);
            param[5] = new SqlParameter("@SerialNo1", argCallClosingDetails.SerialNo1);
            param[6] = new SqlParameter("@SerialNo2", argCallClosingDetails.SerialNo2);
            param[7] = new SqlParameter("@MaterialCode", argCallClosingDetails.MaterialCode);
            param[8] = new SqlParameter("@MatGroup1Code", argCallClosingDetails.MatGroup1Code);
            param[9] = new SqlParameter("@PartnerCode", argCallClosingDetails.PartnerCode);
            param[10] = new SqlParameter("@StoreCode", argCallClosingDetails.StoreCode);
            param[11] = new SqlParameter("@StockIndicator", argCallClosingDetails.StockIndicator);
            param[12] = new SqlParameter("@PartnerEmployeeCode", argCallClosingDetails.PartnerEmployeeCode);
            param[13] = new SqlParameter("@Quantity", argCallClosingDetails.Quantity);
            param[14] = new SqlParameter("@UOMCode", argCallClosingDetails.UOMCode);
            param[15] = new SqlParameter("@MaterialDocTypeCode", argCallClosingDetails.MaterialDocTypeCode);
            param[16] = new SqlParameter("@PGoodsMovementCode", argCallClosingDetails.PGoodsMovementCode);
            param[17] = new SqlParameter("@GMItemNo", argCallClosingDetails.GMItemNo);

            param[18] = new SqlParameter("@PayableAmt", argCallClosingDetails.PayableAmt);

            param[19] = new SqlParameter("@ClientCode", argCallClosingDetails.ClientCode);
            param[20] = new SqlParameter("@CreatedBy", argCallClosingDetails.CreatedBy);
            param[21] = new SqlParameter("@ModifiedBy", argCallClosingDetails.ModifiedBy);

            param[22] = new SqlParameter("@Type", SqlDbType.Char);
            param[22].Size = 1;
            param[22].Direction = ParameterDirection.Output;

            param[23] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[23].Size = 255;
            param[23].Direction = ParameterDirection.Output;

            param[24] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[24].Size = 20;
            param[24].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallClosingDetails", param);
            
            string strMessage = Convert.ToString(param[23].Value);
            string strType = Convert.ToString(param[22].Value);
            string strRetValue = Convert.ToString(param[24].Value);


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
        
        public ICollection<ErrorHandler> DeleteCallClosingDetails(string argCallClosingCode, string argCallCode, int argCallItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@CallClosingCode", argCallClosingCode);
                param[1] = new SqlParameter("@CallCode", argCallCode);
                param[2] = new SqlParameter("@CallItemNo", argCallItemNo);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCallClosingDetails", param);


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

        public bool blnIsCallClosingDetailsExists(string argCallClosingCode, string argCallCode, int argCallItemNo, string argPartnerCode, string argClientCode)
        {
            bool IsCallClosingDetailsExists = false;
            DataSet ds = new DataSet();
            ds = GetCallClosingDetails(argCallClosingCode, argCallCode, argCallItemNo, argPartnerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallClosingDetailsExists = true;
            }
            else
            {
                IsCallClosingDetailsExists = false;
            }
            return IsCallClosingDetailsExists;
        }

        public bool blnIsCallClosingDetailsExists(string argCallClosingCode, string argCallCode, int argCallItemNo, string argPartnerCode, string argClientCode, DataAccess da)
        {
            bool IsCallClosingDetailsExists = false;
            DataSet ds = new DataSet();
            ds = GetCallClosingDetails(argCallClosingCode, argCallCode, argCallItemNo, argPartnerCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallClosingDetailsExists = true;
            }
            else
            {
                IsCallClosingDetailsExists = false;
            }
            return IsCallClosingDetailsExists;
        }
        
        public string GenerateCallClosingCode(string argCallClosingCode, string argCallClosingDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CallClosingCode", argCallClosingCode);
            param[1] = new SqlParameter("@CallClosingDocTypeCode", argCallClosingDocTypeCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            param[4] = new SqlParameter("@GeneratedCallClosingCode", SqlDbType.VarChar);
            param[4].Size = 18;
            param[4].Direction = ParameterDirection.Output;

            int i = da.ExecuteNonQuery("SL_GetNewCallClosingCode", param);

            string strMessage = Convert.ToString(param[4].Value);
            
            return strMessage;

        }


        public DataSet GetCallClosingDetail4OWPrint(string argCallClosingCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallClosingCode", argCallClosingCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetchargeReceipt4Print", param);
            return DataSetToFill;
        }

        public DataSet GetCallClosedOnSitePJPDetails4Search(string argDateFrom, string argDateTo, string argRepairDocTypeCode, string argCallCode, string argSerialNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[7];

            param[0] = new SqlParameter("@CallDateFrom ", argDateFrom);
            param[1] = new SqlParameter("@CallDateTo ", argDateTo);
            param[2] = new SqlParameter("@RepairDocTypeCode ", argRepairDocTypeCode);
            param[3] = new SqlParameter("@CallCode", argCallCode);
            param[4] = new SqlParameter("@SerialNo", argSerialNo);
            param[5] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[6] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallClosedOnsitePJPDetail4Search", param);
            return DataSetToFill;
        }

        
    }
}