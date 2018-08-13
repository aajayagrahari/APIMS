
//Created On :: 23, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallRepairProcessManager
    {
        const string CallRepairProcessTable = "CallRepairProcess";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        CallPartsConsumptionManager objCallPartsConsumptionMan = new CallPartsConsumptionManager();
        CallPartsOrderManager objCallPartsOrderMan = new CallPartsOrderManager();
        CallRepairTypeDetailManager objCallRepairTypeDetailMan = new CallRepairTypeDetailManager();
        CallDefectTypeDetailManager objCallDefectTypeDetailMan = new CallDefectTypeDetailManager();
        CallInvoiceDetailManager objCallInvoiceDetailMan = new CallInvoiceDetailManager();
        CallEstimationManager objCallEstimationManager = new CallEstimationManager();
        
        public CallRepairProcess objGetCallRepairProcess(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode)
        {
            CallRepairProcess argCallRepairProcess = new CallRepairProcess();
            DataSet DataSetToFill = new DataSet();

            if (argCallCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argCallItemNo <= 0)
            {
                goto ErrorHandler;
            }

            if (argRepairProcDocCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            DataSetToFill = this.GetCallRepairProcess(argCallCode, argCallItemNo, argRepairProcDocCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallRepairProcess = this.objCreateCallRepairProcess((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandler:

        Finish:
            DataSetToFill = null;


            return argCallRepairProcess;
        }
        
        public ICollection<CallRepairProcess> colGetCallRepairProcess(string argCallCode, int argCallItemNo, string argClientCode)
        {
            List<CallRepairProcess> lst = new List<CallRepairProcess>();
            DataSet DataSetToFill = new DataSet();
            CallRepairProcess tCallRepairProcess = new CallRepairProcess();

            DataSetToFill = this.GetCallRepairProcess(argCallCode, argCallItemNo, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallRepairProcess(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public void colGetCallRepairProcess(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode, ref CallRepairProcessCol argCallRepairProcessCol)
        {            
            DataSet DataSetToFill = new DataSet();
            CallRepairProcess tCallRepairProcess = new CallRepairProcess();

            DataSetToFill = this.GetCallRepairProcess(argCallCode, argCallItemNo, argRepairProcDocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argCallRepairProcessCol.colCallRepairProcess.Add(objCreateCallRepairProcess(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;
        }
        
        public DataSet GetCallRepairProcess(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallRepairProcess4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallRepairProcess(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode, DataAccess da)
        {
           // DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallRepairProcess4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCallRepairProcess(string argCallCode, int argCallItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallRepairProcess", param);
            return DataSetToFill;
        }
        
        private CallRepairProcess objCreateCallRepairProcess(DataRow dr)
        {
            CallRepairProcess tCallRepairProcess = new CallRepairProcess();

            tCallRepairProcess.SetObjectInfo(dr);

            return tCallRepairProcess;

        }
        
        public ICollection<ErrorHandler> SaveCallRepairProcess(CallRepairProcess argCallRepairProcess)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallRepairProcessExists(argCallRepairProcess.CallCode, argCallRepairProcess.CallItemNo, argCallRepairProcess.RepairProcDocCode, argCallRepairProcess.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallRepairProcess(argCallRepairProcess, da, lstErr);
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
                    UpdateCallRepairProcess(argCallRepairProcess, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
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

        public PartnerErrorResult SaveCallRepairProcess(CallRepairProcess argCallRepairProcess, CallPartConsumptionCol argCallPartsConsumptionCol, CallDefectTypeCol argCallDefectTypeCol, CallRepairTypeCol argCallRepairTypeCol, CallPartsOrderCol argCallPartsOrderCol, CallInvoiceDetail argCallInvoiceDetail, CallEstimation argCallEstimation)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            PartnerErrorResult errorcol = new PartnerErrorResult();
            PartnerGoodsMovementManager objPartnerGMManager = new PartnerGoodsMovementManager();
            PartnerMaterialDocTypeManager objPartnerMatDocTypeMan = new PartnerMaterialDocTypeManager();
            
            DataAccess da = new DataAccess();
            string strretValue = "";
            string strProductGoodsMovCode  = "";
            string strSpareGoodsMovCode = "";
            DataSet dsMatDocType = null;

            try
            {
               da.Open_Connection();
               da.BEGIN_TRANSACTION();

               if (blnIsCallRepairProcessExists(argCallRepairProcess.CallCode, argCallRepairProcess.CallItemNo, argCallRepairProcess.RepairProcDocCode, argCallRepairProcess.ClientCode, da) == false)
               {
                   if (argCallRepairProcess.PGoodsMovementCode == "NEW")
                   {
                       if (argCallRepairProcess.MaterialDocTypeCode != "")
                       {
                           strProductGoodsMovCode = objPartnerGMManager.GenerateGMCode("NEW", "GM01", argCallRepairProcess.PartnerCode, argCallRepairProcess.ClientCode, da);
                           argCallRepairProcess.PGoodsMovementCode = strProductGoodsMovCode;
                           argCallRepairProcess.GMItemNo = 1;
                       }
                       else
                       {
                           argCallRepairProcess.PGoodsMovementCode = "";
                           argCallRepairProcess.GMItemNo = 0;
                       }
                   }
                   
                   strretValue = InsertCallRepairProcess(argCallRepairProcess, da, lstErr);

               }
               else
               {
                   if (argCallRepairProcess.PGoodsMovementCode == "NEW")
                   {
                       if (argCallRepairProcess.MaterialDocTypeCode != "")
                       {
                           strProductGoodsMovCode = objPartnerGMManager.GenerateGMCode("NEW", "GM01", argCallRepairProcess.PartnerCode, argCallRepairProcess.ClientCode, da);
                           argCallRepairProcess.PGoodsMovementCode = strProductGoodsMovCode;
                           argCallRepairProcess.GMItemNo = 1;
                       }
                       else
                       {
                           argCallRepairProcess.PGoodsMovementCode = "";
                           argCallRepairProcess.GMItemNo = 0;
                       }
                   }

                  strretValue =  UpdateCallRepairProcess(argCallRepairProcess, da, lstErr);
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
                    if (argCallPartsConsumptionCol.colCallPartsConsumption.Count > 0)
                    {
                        foreach (CallPartsConsumption objCallPartsConsumption in argCallPartsConsumptionCol.colCallPartsConsumption)
                        {
                            if (objCallPartsConsumption.IsDeleted == 0)
                            {

                                if (objCallPartsConsumption.PGoodsMovementCode == "NEW")
                                {
                                    strSpareGoodsMovCode = objPartnerGMManager.GenerateGMCode("NEW", "GM01", argCallRepairProcess.PartnerCode, argCallRepairProcess.ClientCode, da);
                                    objCallPartsConsumption.PGoodsMovementCode = strSpareGoodsMovCode;
                                    objCallPartsConsumption.GMItemNo = objCallPartsConsumption.PCItemNo;
                                }
                                                                
                                objCallPartsConsumptionMan.SaveCallPartsConsumption(objCallPartsConsumption, da, lstErr);
                            }
                            else
                            {
                                /***************************/
                                
                                /*** Call Parts Consumption Delete Fucntion ***/

                                /***************************/

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


                    }

                    if (argCallPartsOrderCol.colCallRepairProcess.Count > 0)
                    {
                        foreach (CallPartsOrder objCallPartsOrder in argCallPartsOrderCol.colCallRepairProcess)
                        {
                            if (objCallPartsOrder.IsDeleted == 0)
                            {
                                objCallPartsOrderMan.SaveCallPartsOrder(objCallPartsOrder, da, lstErr);
                            }
                            else
                            {
                                /***************************/

                                /*** Call Parts Order Delete Fucntion ***/

                                /***************************/
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

                    }

                    if (argCallRepairTypeCol.colCallRepairTypeDetail.Count > 0)
                    {
                        foreach (CallRepairTypeDetail objCallRepairTypeDetail in argCallRepairTypeCol.colCallRepairTypeDetail)
                        {
                            if (objCallRepairTypeDetail.IsDeleted == 0)
                            {
                                objCallRepairTypeDetailMan.SaveCallRepairTypeDetail(objCallRepairTypeDetail, da, lstErr);
                            }
                            else
                            {
                                /***************************/

                                /*** Call Repair Type Delete Fucntion ***/
                                objCallRepairTypeDetailMan.DeleteCallRepairTypeDetail(objCallRepairTypeDetail.CallCode, objCallRepairTypeDetail.ItemNo, objCallRepairTypeDetail.RPItemNo, objCallRepairTypeDetail.IsDeleted, objCallRepairTypeDetail.ClientCode, da, lstErr);

                                /***************************/
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

                    }

                    if (argCallDefectTypeCol.colCallDefectTypeDetail.Count > 0)
                    {
                        foreach (CallDefectTypeDetail objCallDefectTypeDetail in argCallDefectTypeCol.colCallDefectTypeDetail)
                        {
                            if (objCallDefectTypeDetail.IsDeleted == 0)
                            {
                                objCallDefectTypeDetailMan.SaveCallDefectTypeDetail(objCallDefectTypeDetail, da, lstErr);
                            }
                            else
                            {
                                /***************************/

                                /*** Call Defect Type Delete Fucntion ***/
                                objCallDefectTypeDetailMan.DeleteCallDefectTypeDetail(objCallDefectTypeDetail.CallCode, objCallDefectTypeDetail.ItemNo, objCallDefectTypeDetail.DfItemNo, objCallDefectTypeDetail.PartnerCode, objCallDefectTypeDetail.ClientCode, objCallDefectTypeDetail.IsDeleted, da, lstErr);

                                /***************************/
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

                    }

                    if (argCallInvoiceDetail != null)
                    {
                        objCallInvoiceDetailMan.SaveCallInvoiceDetail(argCallInvoiceDetail, da, lstErr);
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

                    if (argCallEstimation != null)
                    {

                        objCallEstimationManager.SL_SaveCallEstimation(argCallEstimation, da, lstErr);
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


                    if (argCallPartsConsumptionCol.colCallPartsConsumption.Count > 0)
                    {

                        /* Partner Goods Movement Declared for Spares */
                        PartnerGoodsMovement objPartnerSpareGM = new PartnerGoodsMovement();

                        objPartnerSpareGM.PGoodsMovementCode = strSpareGoodsMovCode;
                        objPartnerSpareGM.PartnerGMDocTypeCode = "GM01";
                        objPartnerSpareGM.FromPlantCode = "";
                        objPartnerSpareGM.FromPartnerCode = Convert.ToString(argCallRepairProcess.PartnerCode);
                        objPartnerSpareGM.FromPartnerEmployeeCode = "";
                        objPartnerSpareGM.FromStoreCode = "";
                        objPartnerSpareGM.ToPartnerCode = Convert.ToString(argCallRepairProcess.PartnerCode);
                        objPartnerSpareGM.ToPlantCode = "";
                        objPartnerSpareGM.ToStoreCode = "";
                        objPartnerSpareGM.ToPartnerEmployeeCode = "";
                        objPartnerSpareGM.ClientCode = argCallRepairProcess.ClientCode;
                        objPartnerSpareGM.CreatedBy = argCallRepairProcess.CreatedBy;
                        objPartnerSpareGM.ModifiedBy = argCallRepairProcess.ModifiedBy;
                        objPartnerSpareGM.TotalQuantity = 0;
                        objPartnerSpareGM.PartnerCode = Convert.ToString(argCallRepairProcess.PartnerCode);
                        objPartnerSpareGM.GoodsMovDate = Convert.ToDateTime(argCallRepairProcess.RepairDate);

                        /*----------------------------------------------------------------------------------------*/
                        /* Partner Goods Movement Detail */
                       
                        PartnerGoodsMovementDetailCol objPartnerSpareGMCol = new PartnerGoodsMovementDetailCol();
                        PartnerGoodsMovSerialDetailCol objPartnerSpareGMSerialCol = new PartnerGoodsMovSerialDetailCol();

                        objPartnerSpareGMCol.colPartnerGMDetail = new List<PartnerGoodsMovementDetail>();
                        objPartnerSpareGMSerialCol.colPartnerGMSerialDetail = new List<PartnerGoodsMovSerialDetail>();

                        
                        bool IsNew = true;
                        int iCtr = 0;
                        int tmpItemNo = 0;
                        foreach (CallPartsConsumption objCallPartsConsumption in argCallPartsConsumptionCol.colCallPartsConsumption)
                        {
                            if (objCallPartsConsumption.IsDeleted == 0)
                            {
                                dsMatDocType = new DataSet();
                                iCtr = iCtr + 1;
                                tmpItemNo = iCtr;
                                if (IsNew == true)
                                {

                                    dsMatDocType = objPartnerMatDocTypeMan.GetPartnerMaterialDocType(objCallPartsConsumption.MaterialDocTypeCode.Trim(), objCallPartsConsumption.ClientCode, da);

                                    PartnerGoodsMovementDetail objPartnerGMDetailsnew = new PartnerGoodsMovementDetail();
                                    objPartnerGMDetailsnew.PGoodsMovementCode = strSpareGoodsMovCode;
                                    objPartnerGMDetailsnew.ItemNo = tmpItemNo;
                                    objPartnerGMDetailsnew.MaterialCode = Convert.ToString(objCallPartsConsumption.MaterialCode);
                                    objPartnerGMDetailsnew.MatGroup1Code = Convert.ToString(objCallPartsConsumption.MatGroup1Code);
                                    objPartnerGMDetailsnew.StockIndicator = Convert.ToString(objCallPartsConsumption.StockIndicator);
                                    objPartnerGMDetailsnew.ToStockIndicator = Convert.ToString(objCallPartsConsumption.ToStockIndicator);
                                    objPartnerGMDetailsnew.Quantity = Convert.ToInt32(objCallPartsConsumption.Quantity);
                                    objPartnerGMDetailsnew.UOMCode = Convert.ToString(objCallPartsConsumption.UOMCode);
                                    objPartnerGMDetailsnew.ClientCode = Convert.ToString(objCallPartsConsumption.ClientCode);
                                    objPartnerGMDetailsnew.CreatedBy = Convert.ToString(objCallPartsConsumption.CreatedBy);
                                    objPartnerGMDetailsnew.ModifiedBy = Convert.ToString(objCallPartsConsumption.ModifiedBy);
                                    objPartnerGMDetailsnew.TranRefDocCode = Convert.ToString(strretValue);
                                    objPartnerGMDetailsnew.TranRefDocItemNo = Convert.ToInt32(objCallPartsConsumption.PCItemNo);
                                    objPartnerGMDetailsnew.MaterialDocTypeCode = Convert.ToString(objCallPartsConsumption.MaterialDocTypeCode);
                                    objPartnerGMDetailsnew.PartnerCode = Convert.ToString(objCallPartsConsumption.PartnerCode);

                                    if (dsMatDocType != null)
                                    {
                                        if (dsMatDocType.Tables[0].Rows.Count > 0)
                                        {
                                            if (dsMatDocType.Tables[0].Rows[0]["FromPlant"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.FromPlantCode = "";
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.FromPlantCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["FromPartner"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(objCallPartsConsumption.PartnerCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.FromPartnerCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["FromStore"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(objCallPartsConsumption.StoreCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.FromStoreCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["FromEmployee"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.FromPartnerEmployeeCode = Convert.ToString(objCallPartsConsumption.PartnerEmployeeCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.FromPartnerEmployeeCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["ToPartner"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(objCallPartsConsumption.PartnerCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.ToPartnerCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["ToStore"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(objCallPartsConsumption.ToStoreCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.ToStoreCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["ToEmployee"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString(objCallPartsConsumption.ToPartnerEmployeeCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.ToPartnerEmployeeCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["ToPlant"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.ToPlantCode = "";
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.ToPlantCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["ToMaterialCode"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(objCallPartsConsumption.ToMaterialCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.ToMaterialCode = "";
                                            }


                                        }
                                        else
                                        {
                                            objPartnerGMDetailsnew.FromPlantCode = "";
                                            objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(objCallPartsConsumption.PartnerCode);
                                            objPartnerGMDetailsnew.FromPartnerEmployeeCode = Convert.ToString(objCallPartsConsumption.PartnerEmployeeCode);
                                            objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(objCallPartsConsumption.StoreCode);
                                            objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(objCallPartsConsumption.PartnerCode);
                                            objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString(objCallPartsConsumption.ToPartnerEmployeeCode);
                                            objPartnerGMDetailsnew.ToPlantCode = "";
                                            objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(objCallPartsConsumption.ToStoreCode);
                                            objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(objCallPartsConsumption.ToMaterialCode);

                                        }
                                    }

                                    objPartnerSpareGMCol.colPartnerGMDetail.Add(objPartnerGMDetailsnew);

                                    /*Partner Goods Movement Serial Detail*/
                                    if (objCallPartsConsumption.SerialNo1 != "")
                                    {
                                        /*Partner Goods Movement Serial Detail*/

                                        PartnerGoodsMovSerialDetail objPartnerGoodsMovSerialnew = new PartnerGoodsMovSerialDetail();
                                        objPartnerGoodsMovSerialnew.PGoodsMovementCode = strSpareGoodsMovCode;
                                        objPartnerGoodsMovSerialnew.ItemNo = tmpItemNo;
                                        objPartnerGoodsMovSerialnew.SerialNo1 = Convert.ToString(objCallPartsConsumption.SerialNo1);
                                        objPartnerGoodsMovSerialnew.SerialNo2 = Convert.ToString(objCallPartsConsumption.SerialNo2);
                                        objPartnerGoodsMovSerialnew.MaterialCode = Convert.ToString(objCallPartsConsumption.MaterialCode);
                                        objPartnerGoodsMovSerialnew.MatGroup1Code = Convert.ToString(objCallPartsConsumption.MatGroup1Code);
                                        objPartnerGoodsMovSerialnew.RefDocCode = Convert.ToString("");
                                        objPartnerGoodsMovSerialnew.RefDocItemNo = Convert.ToInt32("0");
                                        objPartnerGoodsMovSerialnew.RefDocType = Convert.ToString("");
                                        objPartnerGoodsMovSerialnew.TranRefDocCode = Convert.ToString(strretValue);
                                        objPartnerGoodsMovSerialnew.TranRefDocItemNo = Convert.ToInt32(objCallPartsConsumption.PCItemNo);
                                        objPartnerGoodsMovSerialnew.IsDeleted = 0;
                                        objPartnerGoodsMovSerialnew.ClientCode = Convert.ToString(objCallPartsConsumption.ClientCode);
                                        objPartnerGoodsMovSerialnew.CreatedBy = Convert.ToString(objCallPartsConsumption.CreatedBy);
                                        objPartnerGoodsMovSerialnew.ModifiedBy = Convert.ToString(objCallPartsConsumption.ModifiedBy);
                                        objPartnerGoodsMovSerialnew.StockIndicator = Convert.ToString(objCallPartsConsumption.StockIndicator);
                                        objPartnerGoodsMovSerialnew.ToStockIndicator = Convert.ToString(objCallPartsConsumption.ToStockIndicator);
                                        

                                        if (dsMatDocType != null)
                                        {
                                            if (dsMatDocType.Tables[0].Rows.Count > 0)
                                            {
                                                if (dsMatDocType.Tables[0].Rows[0]["FromPlant"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.PlantCode = "";
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.PlantCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["FromPartner"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.PartnerCode = Convert.ToString(objCallPartsConsumption.PartnerCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.PartnerCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["FromStore"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.StoreCode = Convert.ToString(objCallPartsConsumption.StoreCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.StoreCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["FromEmployee"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.PartnerEmployeeCode = Convert.ToString(objCallPartsConsumption.PartnerEmployeeCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.PartnerEmployeeCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["ToPartner"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.ToPartnerCode = Convert.ToString(objCallPartsConsumption.PartnerCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.ToPartnerCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["ToStore"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.ToStoreCode = Convert.ToString(objCallPartsConsumption.ToStoreCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.ToStoreCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["ToEmployee"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = Convert.ToString(objCallPartsConsumption.ToPartnerEmployeeCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["ToPlant"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.ToPlantCode = "";
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.ToPlantCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["ToMaterialCode"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.ToMaterialCode = Convert.ToString(objCallPartsConsumption.ToMaterialCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.ToMaterialCode = "";
                                                }


                                            }
                                            else
                                            {
                                                objPartnerGoodsMovSerialnew.PlantCode = "";
                                                objPartnerGoodsMovSerialnew.PartnerCode = Convert.ToString(objCallPartsConsumption.PartnerCode);
                                                objPartnerGoodsMovSerialnew.PartnerEmployeeCode = Convert.ToString(objCallPartsConsumption.PartnerEmployeeCode);
                                                objPartnerGoodsMovSerialnew.StoreCode = Convert.ToString(objCallPartsConsumption.StoreCode);
                                                objPartnerGoodsMovSerialnew.ToPartnerCode = Convert.ToString(objCallPartsConsumption.PartnerCode);
                                                objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = Convert.ToString(objCallPartsConsumption.ToPartnerEmployeeCode);
                                                objPartnerGoodsMovSerialnew.ToPlantCode = "";
                                                objPartnerGoodsMovSerialnew.ToStoreCode = Convert.ToString(objCallPartsConsumption.ToStoreCode);
                                                objPartnerGoodsMovSerialnew.ToMaterialCode = Convert.ToString(objCallPartsConsumption.ToMaterialCode);

                                            }
                                        }
                                        
                                        objPartnerSpareGMSerialCol.colPartnerGMSerialDetail.Add(objPartnerGoodsMovSerialnew);

                                    }

                                }
                            }
                        }
                        /*Partner Spare Goods Movement Save*/
                        if (objPartnerSpareGMCol.colPartnerGMDetail.Count > 0)
                        {
                            objPartnerGMManager.SavePartnerGoodsMovement(objPartnerSpareGM, objPartnerSpareGMCol, objPartnerSpareGMSerialCol, da, lstErr);

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
                    /************************************************************************************************/

                    if (argCallRepairProcess.MaterialDocTypeCode.Trim() != "")
                    {
                        /* Partner Goods Movement Declared for Product */

                        PartnerGoodsMovement objPartnerProductGM = new PartnerGoodsMovement();

                        objPartnerProductGM.PGoodsMovementCode = strProductGoodsMovCode;
                        objPartnerProductGM.PartnerGMDocTypeCode = "GM01";
                        objPartnerProductGM.FromPlantCode = "";
                        objPartnerProductGM.FromPartnerCode = Convert.ToString(argCallRepairProcess.PartnerCode);
                        objPartnerProductGM.FromPartnerEmployeeCode = Convert.ToString(argCallRepairProcess.RepairedBY);
                        objPartnerProductGM.FromStoreCode = "";
                        objPartnerProductGM.ToPartnerCode = Convert.ToString(argCallRepairProcess.PartnerCode);
                        objPartnerProductGM.ToPlantCode = "";
                        objPartnerProductGM.ToStoreCode = "";
                        objPartnerProductGM.ToPartnerEmployeeCode = Convert.ToString(argCallRepairProcess.RepairedBY);
                        objPartnerProductGM.ClientCode = argCallRepairProcess.ClientCode;
                        objPartnerProductGM.CreatedBy = argCallRepairProcess.CreatedBy;
                        objPartnerProductGM.ModifiedBy = argCallRepairProcess.ModifiedBy;
                        objPartnerProductGM.TotalQuantity = 0;
                        objPartnerProductGM.PartnerCode = Convert.ToString(argCallRepairProcess.PartnerCode);
                        objPartnerProductGM.GoodsMovDate = Convert.ToDateTime(DateTime.Now);

                        /*----------------------------------------------------------------------------------------*/
                        /* Partner Goods Movement Detail */
                     
                        PartnerGoodsMovementDetailCol objPartnerProductGMCol = new PartnerGoodsMovementDetailCol();
                        PartnerGoodsMovSerialDetailCol objPartnerProductGMSerialCol = new PartnerGoodsMovSerialDetailCol();

                        objPartnerProductGMCol.colPartnerGMDetail = new List<PartnerGoodsMovementDetail>();
                        objPartnerProductGMSerialCol.colPartnerGMSerialDetail = new List<PartnerGoodsMovSerialDetail>();

                        
                        dsMatDocType = new DataSet();
                        int tmpItemNo = 1;
                        dsMatDocType = objPartnerMatDocTypeMan.GetPartnerMaterialDocType(argCallRepairProcess.MaterialDocTypeCode, argCallRepairProcess.ClientCode, da);

                        PartnerGoodsMovementDetail objPartnerGMDetailsnew = new PartnerGoodsMovementDetail();
                        objPartnerGMDetailsnew.PGoodsMovementCode = strProductGoodsMovCode;
                        objPartnerGMDetailsnew.ItemNo = tmpItemNo;
                        objPartnerGMDetailsnew.MaterialCode = Convert.ToString(argCallRepairProcess.MaterialCode);
                        objPartnerGMDetailsnew.MatGroup1Code = Convert.ToString(argCallRepairProcess.MatGroup1Code);
                        objPartnerGMDetailsnew.StockIndicator = Convert.ToString(argCallRepairProcess.StockIndicator);
                        objPartnerGMDetailsnew.ToStockIndicator = Convert.ToString(argCallRepairProcess.ToStockIndicator);
                        objPartnerGMDetailsnew.Quantity = Convert.ToInt32("1");
                        objPartnerGMDetailsnew.UOMCode = Convert.ToString("EA");
                        objPartnerGMDetailsnew.ClientCode = Convert.ToString(argCallRepairProcess.ClientCode);
                        objPartnerGMDetailsnew.CreatedBy = Convert.ToString(argCallRepairProcess.CreatedBy);
                        objPartnerGMDetailsnew.ModifiedBy = Convert.ToString(argCallRepairProcess.ModifiedBy);
                        objPartnerGMDetailsnew.TranRefDocCode = Convert.ToString(strretValue);
                        objPartnerGMDetailsnew.TranRefDocItemNo = Convert.ToInt32("1");
                        objPartnerGMDetailsnew.MaterialDocTypeCode = Convert.ToString(argCallRepairProcess.MaterialDocTypeCode);
                        objPartnerGMDetailsnew.PartnerCode = Convert.ToString(argCallRepairProcess.PartnerCode);

                        if (dsMatDocType != null)
                        {
                            if (dsMatDocType.Tables[0].Rows.Count > 0)
                            {
                                if (dsMatDocType.Tables[0].Rows[0]["FromPlant"].ToString() != "HIDE")
                                {
                                    objPartnerGMDetailsnew.FromPlantCode = "";
                                }
                                else
                                {
                                    objPartnerGMDetailsnew.FromPlantCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["FromPartner"].ToString() != "HIDE")
                                {
                                    objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(argCallRepairProcess.PartnerCode);
                                }
                                else
                                {
                                    objPartnerGMDetailsnew.FromPartnerCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["FromStore"].ToString() != "HIDE")
                                {
                                    objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(argCallRepairProcess.StoreCode);
                                }
                                else
                                {
                                    objPartnerGMDetailsnew.FromStoreCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["FromEmployee"].ToString() != "HIDE")
                                {
                                    objPartnerGMDetailsnew.FromPartnerEmployeeCode = Convert.ToString(argCallRepairProcess.RepairedBY);
                                }
                                else
                                {
                                    objPartnerGMDetailsnew.FromPartnerEmployeeCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToPartner"].ToString() != "HIDE")
                                {
                                    objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(argCallRepairProcess.PartnerCode);
                                }
                                else
                                {
                                    objPartnerGMDetailsnew.ToPartnerCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToStore"].ToString() != "HIDE")
                                {
                                    objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(argCallRepairProcess.ToStoreCode);
                                }
                                else
                                {
                                    objPartnerGMDetailsnew.ToStoreCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToEmployee"].ToString() != "HIDE")
                                {
                                    objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString(argCallRepairProcess.RepairedBY);
                                }
                                else
                                {
                                    objPartnerGMDetailsnew.ToPartnerEmployeeCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToPlant"].ToString() != "HIDE")
                                {
                                    objPartnerGMDetailsnew.ToPlantCode = "";
                                }
                                else
                                {
                                    objPartnerGMDetailsnew.ToPlantCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToMaterialCode"].ToString() != "HIDE")
                                {
                                    objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(argCallRepairProcess.MaterialCode);
                                }
                                else
                                {
                                    objPartnerGMDetailsnew.ToMaterialCode = "";
                                }


                            }
                            else
                            {
                                objPartnerGMDetailsnew.FromPlantCode = "";
                                objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(argCallRepairProcess.PartnerCode);
                                objPartnerGMDetailsnew.FromPartnerEmployeeCode = Convert.ToString(argCallRepairProcess.RepairedBY);
                                objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(argCallRepairProcess.StoreCode);
                                objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(argCallRepairProcess.PartnerCode);
                                objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString(argCallRepairProcess.RepairedBY);
                                objPartnerGMDetailsnew.ToPlantCode = "";
                                objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(argCallRepairProcess.ToStoreCode);
                                objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(argCallRepairProcess.MaterialCode);

                            }
                        }

                        objPartnerProductGMCol.colPartnerGMDetail.Add(objPartnerGMDetailsnew);

                        /*******************************************************************/
                        PartnerGoodsMovSerialDetail objPartnerGoodsMovSerialnew = new PartnerGoodsMovSerialDetail();
                        objPartnerGoodsMovSerialnew.PGoodsMovementCode = strProductGoodsMovCode;
                        objPartnerGoodsMovSerialnew.ItemNo = tmpItemNo;
                        objPartnerGoodsMovSerialnew.SerialNo1 = Convert.ToString(argCallRepairProcess.SerialNo);
                        objPartnerGoodsMovSerialnew.SerialNo2 = "";
                        objPartnerGoodsMovSerialnew.MaterialCode = Convert.ToString(argCallRepairProcess.MaterialCode);
                        objPartnerGoodsMovSerialnew.MatGroup1Code = Convert.ToString(argCallRepairProcess.MatGroup1Code);
                        objPartnerGoodsMovSerialnew.RefDocCode = Convert.ToString(argCallRepairProcess.CallCode);
                        objPartnerGoodsMovSerialnew.RefDocItemNo = Convert.ToInt32(argCallRepairProcess.CallItemNo);
                        objPartnerGoodsMovSerialnew.RefDocType = Convert.ToString("");
                        objPartnerGoodsMovSerialnew.IsDeleted = 0;
                        objPartnerGoodsMovSerialnew.TranRefDocCode = Convert.ToString(strretValue);
                        objPartnerGoodsMovSerialnew.TranRefDocItemNo = Convert.ToInt32("1");
                        objPartnerGoodsMovSerialnew.ClientCode = Convert.ToString(argCallRepairProcess.ClientCode);
                        objPartnerGoodsMovSerialnew.CreatedBy = Convert.ToString(argCallRepairProcess.CreatedBy);
                        objPartnerGoodsMovSerialnew.ModifiedBy = Convert.ToString(argCallRepairProcess.ModifiedBy);
                        objPartnerGoodsMovSerialnew.StockIndicator = Convert.ToString(argCallRepairProcess.StockIndicator);
                        objPartnerGoodsMovSerialnew.ToStockIndicator = Convert.ToString(argCallRepairProcess.ToStockIndicator);
                        if (dsMatDocType != null)
                        {
                            if (dsMatDocType.Tables[0].Rows.Count > 0)
                            {
                                if (dsMatDocType.Tables[0].Rows[0]["FromPlant"].ToString() != "HIDE")
                                {
                                    objPartnerGoodsMovSerialnew.PlantCode = "";
                                }
                                else
                                {
                                    objPartnerGoodsMovSerialnew.PlantCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["FromPartner"].ToString() != "HIDE")
                                {
                                    objPartnerGoodsMovSerialnew.PartnerCode = Convert.ToString(argCallRepairProcess.PartnerCode);
                                }
                                else
                                {
                                    objPartnerGoodsMovSerialnew.PartnerCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["FromStore"].ToString() != "HIDE")
                                {
                                    objPartnerGoodsMovSerialnew.StoreCode = Convert.ToString(argCallRepairProcess.StoreCode);
                                }
                                else
                                {
                                    objPartnerGoodsMovSerialnew.StoreCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["FromEmployee"].ToString() != "HIDE")
                                {
                                    objPartnerGoodsMovSerialnew.PartnerEmployeeCode = Convert.ToString(argCallRepairProcess.RepairedBY);
                                }
                                else
                                {
                                    objPartnerGoodsMovSerialnew.PartnerEmployeeCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToPartner"].ToString() != "HIDE")
                                {
                                    objPartnerGoodsMovSerialnew.ToPartnerCode = Convert.ToString(argCallRepairProcess.PartnerCode);
                                }
                                else
                                {
                                    objPartnerGoodsMovSerialnew.ToPartnerCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToStore"].ToString() != "HIDE")
                                {
                                    objPartnerGoodsMovSerialnew.ToStoreCode = Convert.ToString(argCallRepairProcess.ToStoreCode);
                                }
                                else
                                {
                                    objPartnerGoodsMovSerialnew.ToStoreCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToEmployee"].ToString() != "HIDE")
                                {
                                    objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = Convert.ToString(argCallRepairProcess.RepairedBY);
                                }
                                else
                                {
                                    objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToPlant"].ToString() != "HIDE")
                                {
                                    objPartnerGoodsMovSerialnew.ToPlantCode = "";
                                }
                                else
                                {
                                    objPartnerGoodsMovSerialnew.ToPlantCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToMaterialCode"].ToString() != "HIDE")
                                {
                                    objPartnerGoodsMovSerialnew.ToMaterialCode = Convert.ToString(argCallRepairProcess.MaterialCode);
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


                        objPartnerProductGMSerialCol.colPartnerGMSerialDetail.Add(objPartnerGoodsMovSerialnew);

                        if (argCallRepairProcess.PGoodsMovementCode != "")
                        {
                            /*Partner Product Goods Movement Save*/
                            if (objPartnerProductGMCol.colPartnerGMDetail.Count > 0)
                            {
                                objPartnerGMManager.SavePartnerGoodsMovement(objPartnerProductGM, objPartnerProductGMCol, objPartnerProductGMSerialCol, da, lstErr);

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

        public string InsertCallRepairProcess(CallRepairProcess argCallRepairProcess, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[40];

            param[0] = new SqlParameter("@CallCode", argCallRepairProcess.CallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallRepairProcess.CallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argCallRepairProcess.RepairProcDocCode);
            param[3] = new SqlParameter("@RepairProcessDocTypeCode", argCallRepairProcess.RepairProcessDocTypeCode);
            param[4] = new SqlParameter("@RepairedBY", argCallRepairProcess.RepairedBY);
            param[5] = new SqlParameter("@CallDate", argCallRepairProcess.CallDate);
            param[6] = new SqlParameter("@TechReceivedDate", argCallRepairProcess.TechReceivedDate);
            param[7] = new SqlParameter("@RepairDate", argCallRepairProcess.RepairDate);
            param[8] = new SqlParameter("@SerialNo", argCallRepairProcess.SerialNo);
            param[9] = new SqlParameter("@MaterialCode", argCallRepairProcess.MaterialCode);
            param[10] = new SqlParameter("@MatGroup1Code", argCallRepairProcess.MatGroup1Code);
            param[11] = new SqlParameter("@ConditionCode", argCallRepairProcess.ConditionCode);
            param[12] = new SqlParameter("@CustComplaint", argCallRepairProcess.CustComplaint);
            param[13] = new SqlParameter("@WarrantyStatus", argCallRepairProcess.WarrantyStatus);
            param[14] = new SqlParameter("@WarrantyOn", argCallRepairProcess.WarrantyOn);
            param[15] = new SqlParameter("@ServiceLevel", argCallRepairProcess.ServiceLevel);
            param[16] = new SqlParameter("@OutWarrantyOn", argCallRepairProcess.OutWarrantyOn);
            param[17] = new SqlParameter("@DefectTypeDesc", argCallRepairProcess.DefectTypeDesc);
            param[18] = new SqlParameter("@RepairTypeDesc", argCallRepairProcess.RepairTypeDesc);
            param[19] = new SqlParameter("@RepairProcType", argCallRepairProcess.RepairProcType);
            param[20] = new SqlParameter("@WarrantyEndDate", argCallRepairProcess.WarrantyEndDate);
            param[21] = new SqlParameter("@EstTotal", argCallRepairProcess.EstTotal);
            param[22] = new SqlParameter("@TechRemark", argCallRepairProcess.TechRemark);
            param[23] = new SqlParameter("@IsRepairCompleted", argCallRepairProcess.IsRepairCompleted);
            param[24] = new SqlParameter("@RepairCompleteDate", argCallRepairProcess.RepairCompleteDate);
            param[25] = new SqlParameter("@PartnerCode", argCallRepairProcess.PartnerCode);

            param[26] = new SqlParameter("@StoreCode", argCallRepairProcess.StoreCode);
            param[27] = new SqlParameter("@StockIndicator", argCallRepairProcess.StockIndicator);
            param[28] = new SqlParameter("@ToStoreCode", argCallRepairProcess.ToStoreCode);
            param[29] = new SqlParameter("@ToStockIndicator", argCallRepairProcess.ToStockIndicator);
            param[30] = new SqlParameter("@MaterialDocTypeCode", argCallRepairProcess.MaterialDocTypeCode);
            param[31] = new SqlParameter("@PGoodsMovementCode", argCallRepairProcess.PGoodsMovementCode);
            param[32] = new SqlParameter("@GMItemNo", argCallRepairProcess.GMItemNo);

            param[33] = new SqlParameter("@MRevisionCode", argCallRepairProcess.MRevisionCode);

            param[34] = new SqlParameter("@ClientCode", argCallRepairProcess.ClientCode);
            param[35] = new SqlParameter("@CreatedBy", argCallRepairProcess.CreatedBy);
            param[36] = new SqlParameter("@ModifiedBy", argCallRepairProcess.ModifiedBy);
          
            param[37] = new SqlParameter("@Type", SqlDbType.Char);
            param[37].Size = 1;
            param[37].Direction = ParameterDirection.Output;

            param[38] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[38].Size = 255;
            param[38].Direction = ParameterDirection.Output;

            param[39] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[39].Size = 20;
            param[39].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallRepairProcess", param);


            string strMessage = Convert.ToString(param[38].Value);
            string strType = Convert.ToString(param[37].Value);
            string strRetValue = Convert.ToString(param[39].Value);


            objErrorHandler.Type = strType;
            objErrorHandler.MsgId = 0;
            objErrorHandler.Module = ErrorConstant.strInsertModule;
            objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
            objErrorHandler.Message = strMessage.ToString();
            objErrorHandler.RowNo = 0;
            objErrorHandler.FieldName = "";
            objErrorHandler.LogCode = "";
            lstErr.Add(objErrorHandler);

            return strRetValue;
        }

        public string UpdateCallRepairProcess(CallRepairProcess argCallRepairProcess, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[40];

            param[0] = new SqlParameter("@CallCode", argCallRepairProcess.CallCode);
            param[1] = new SqlParameter("@CallItemNo", argCallRepairProcess.CallItemNo);
            param[2] = new SqlParameter("@RepairProcDocCode", argCallRepairProcess.RepairProcDocCode);
            param[3] = new SqlParameter("@RepairProcessDocTypeCode", argCallRepairProcess.RepairProcessDocTypeCode);
            param[4] = new SqlParameter("@RepairedBY", argCallRepairProcess.RepairedBY);
            param[5] = new SqlParameter("@CallDate", argCallRepairProcess.CallDate);
            param[6] = new SqlParameter("@TechReceivedDate", argCallRepairProcess.TechReceivedDate);
            param[7] = new SqlParameter("@RepairDate", argCallRepairProcess.RepairDate);
            param[8] = new SqlParameter("@SerialNo", argCallRepairProcess.SerialNo);
            param[9] = new SqlParameter("@MaterialCode", argCallRepairProcess.MaterialCode);
            param[10] = new SqlParameter("@MatGroup1Code", argCallRepairProcess.MatGroup1Code);
            param[11] = new SqlParameter("@ConditionCode", argCallRepairProcess.ConditionCode);
            param[12] = new SqlParameter("@CustComplaint", argCallRepairProcess.CustComplaint);
            param[13] = new SqlParameter("@WarrantyStatus", argCallRepairProcess.WarrantyStatus);
            param[14] = new SqlParameter("@WarrantyOn", argCallRepairProcess.WarrantyOn);
            param[15] = new SqlParameter("@ServiceLevel", argCallRepairProcess.ServiceLevel);
            param[16] = new SqlParameter("@OutWarrantyOn", argCallRepairProcess.OutWarrantyOn);
            param[17] = new SqlParameter("@DefectTypeDesc", argCallRepairProcess.DefectTypeDesc);
            param[18] = new SqlParameter("@RepairTypeDesc", argCallRepairProcess.RepairTypeDesc);
            param[19] = new SqlParameter("@RepairProcType", argCallRepairProcess.RepairProcType);
            param[20] = new SqlParameter("@WarrantyEndDate", argCallRepairProcess.WarrantyEndDate);
            param[21] = new SqlParameter("@EstTotal", argCallRepairProcess.EstTotal);
            param[22] = new SqlParameter("@TechRemark", argCallRepairProcess.TechRemark);
            param[23] = new SqlParameter("@IsRepairCompleted", argCallRepairProcess.IsRepairCompleted);
            param[24] = new SqlParameter("@RepairCompleteDate", argCallRepairProcess.RepairCompleteDate);
            param[25] = new SqlParameter("@PartnerCode", argCallRepairProcess.PartnerCode);

            param[26] = new SqlParameter("@StoreCode", argCallRepairProcess.StoreCode);
            param[27] = new SqlParameter("@StockIndicator", argCallRepairProcess.StockIndicator);
            param[28] = new SqlParameter("@ToStoreCode", argCallRepairProcess.ToStoreCode);
            param[29] = new SqlParameter("@ToStockIndicator", argCallRepairProcess.ToStockIndicator);
            param[30] = new SqlParameter("@MaterialDocTypeCode", argCallRepairProcess.MaterialDocTypeCode);
            param[31] = new SqlParameter("@PGoodsMovementCode", argCallRepairProcess.PGoodsMovementCode);
            param[32] = new SqlParameter("@GMItemNo", argCallRepairProcess.GMItemNo);

            param[33] = new SqlParameter("@MRevisionCode", argCallRepairProcess.MRevisionCode);

            param[34] = new SqlParameter("@ClientCode", argCallRepairProcess.ClientCode);
            param[35] = new SqlParameter("@CreatedBy", argCallRepairProcess.CreatedBy);
            param[36] = new SqlParameter("@ModifiedBy", argCallRepairProcess.ModifiedBy);

            param[37] = new SqlParameter("@Type", SqlDbType.Char);
            param[37].Size = 1;
            param[37].Direction = ParameterDirection.Output;

            param[38] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[38].Size = 255;
            param[38].Direction = ParameterDirection.Output;

            param[39] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[39].Size = 20;
            param[39].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallRepairProcess", param);


            string strMessage = Convert.ToString(param[38].Value);
            string strType = Convert.ToString(param[37].Value);
            string strRetValue = Convert.ToString(param[39].Value);


            objErrorHandler.Type = strType;
            objErrorHandler.MsgId = 0;
            objErrorHandler.Module = ErrorConstant.strInsertModule;
            objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
            objErrorHandler.Message = strMessage.ToString();
            objErrorHandler.RowNo = 0;
            objErrorHandler.FieldName = "";
            objErrorHandler.LogCode = "";
            lstErr.Add(objErrorHandler);

            return strRetValue;

        }

        public ICollection<ErrorHandler> DeleteCallRepairProcess(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@CallCode", argCallCode);
                param[1] = new SqlParameter("@CallItemNo", argCallItemNo);
                param[2] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCallRepairProcess", param);


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
        
        public bool blnIsCallRepairProcessExists(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode)
        {
            bool IsCallRepairProcessExists = false;
            DataSet ds = new DataSet();
            ds = GetCallRepairProcess(argCallCode, argCallItemNo, argRepairProcDocCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallRepairProcessExists = true;
            }
            else
            {
                IsCallRepairProcessExists = false;
            }
            return IsCallRepairProcessExists;
        }

        public bool blnIsCallRepairProcessExists(string argCallCode, int argCallItemNo, string argRepairProcDocCode, string argClientCode, DataAccess da)
        {
            bool IsCallRepairProcessExists = false;
            DataSet ds = new DataSet();
            ds = GetCallRepairProcess(argCallCode, argCallItemNo, argRepairProcDocCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallRepairProcessExists = true;
            }
            else
            {
                IsCallRepairProcessExists = false;
            }
            return IsCallRepairProcessExists;
        }

        public string GenerateRepairProcCode(string argRepairProcDocCode, string argRepairProcessDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@RepairProcDocCode", argRepairProcDocCode);
            param[1] = new SqlParameter("@RepairProcessDocTypeCode", argRepairProcessDocTypeCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            param[4] = new SqlParameter("@GeneratedCallRepairProcCode", SqlDbType.VarChar);
            param[4].Size = 18;
            param[4].Direction = ParameterDirection.Output;

            int i = da.ExecuteNonQuery("SL_GetNewRepairProcessCode", param);

            string strMessage = Convert.ToString(param[4].Value);

            return strMessage;

        }


    }
}