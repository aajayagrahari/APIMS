
//Created On :: 11, December, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallAdvReplacementReceiptManager
    {
        const string CallAdvReplacementReceiptTable = "CallAdvReplacementReceipt";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CallAdvReplacementReceipt objGetCallAdvReplacementReceipt(string argCallAdvRepReceiptCode, string argCallCode, int argCallItemNo, string argPartnerCode, string argClientCode)
        {
            CallAdvReplacementReceipt argCallAdvReplacementReceipt = new CallAdvReplacementReceipt();
            DataSet DataSetToFill = new DataSet();

            if (argCallAdvRepReceiptCode.Trim() == "")
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

            DataSetToFill = this.GetCallAdvReplacementReceipt(argCallAdvRepReceiptCode, argCallCode, argCallItemNo,argPartnerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallAdvReplacementReceipt = this.objCreateCallAdvReplacementReceipt((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallAdvReplacementReceipt;
        }


        public ICollection<CallAdvReplacementReceipt> colGetCallAdvReplacementReceipt(string argCallAdvRepReceiptCode, string argCallCode, string argPartnerCode, string argClientCode)
        {
            List<CallAdvReplacementReceipt> lst = new List<CallAdvReplacementReceipt>();
            DataSet DataSetToFill = new DataSet();
            CallAdvReplacementReceipt tCallAdvReplacementReceipt = new CallAdvReplacementReceipt();

            DataSetToFill = this.GetCallAdvReplacementReceipt(argCallAdvRepReceiptCode,argCallCode, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallAdvReplacementReceipt(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public void colGetCallAdvReplacementReceipt(string argCallAdvRepReceiptCode, string argCallCode, string argPartnerCode, string argClientCode, ref CallAdvRepReceiptCol argCallAdvRepReceiptCol)
        {
            DataSet DataSetToFill = new DataSet();
            CallAdvReplacementReceipt tCallAdvReplacementReceipt = new CallAdvReplacementReceipt();

            DataSetToFill = this.GetCallAdvReplacementReceipt(argCallAdvRepReceiptCode, argCallCode, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argCallAdvRepReceiptCol.colCallAdvReplacementReceipt.Add(objCreateCallAdvReplacementReceipt(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;
        }

        public DataSet GetCallAdvReplacementReceipt(string argCallAdvRepReceiptCode, string argCallCode, int argCallItemNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallAdvRepReceiptCode", argCallAdvRepReceiptCode);
            param[1] = new SqlParameter("@CallCode", argCallCode);
            param[2] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallAdvReplacementReceipt4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallAdvReplacementReceipt(string argCallAdvRepReceiptCode, string argCallCode, int argCallItemNo, string argPartnerCode, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallAdvRepReceiptCode", argCallAdvRepReceiptCode);
            param[1] = new SqlParameter("@CallCode", argCallCode);
            param[2] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallAdvReplacementReceipt4ID", param);

            return DataSetToFill;
        }



        public DataSet GetCallAdvReplacementReceipt(string argCallAdvRepReceiptCode, string argCallCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallAdvRepReceiptCode", argCallAdvRepReceiptCode);
            param[1] = new SqlParameter("@CallCode", argCallCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);


            DataSetToFill = da.FillDataSet("SP_GetCallAdvReplacementReceipt", param);
            return DataSetToFill;
        }

        public DataSet GetAdvRepReceiptCalls(string argCallAdvRepReceiptCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvRepReceiptCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAdvRepReceiptCalls", param);
            return DataSetToFill;
        }


        public DataSet GetCallAdvRepReceipt4Search(string argCallAdvRepDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvRepDocTypeCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallAdvRepReceipt4Search", param);
            return DataSetToFill;
        }

        private CallAdvReplacementReceipt objCreateCallAdvReplacementReceipt(DataRow dr)
        {
            CallAdvReplacementReceipt tCallAdvReplacementReceipt = new CallAdvReplacementReceipt();

            tCallAdvReplacementReceipt.SetObjectInfo(dr);

            return tCallAdvReplacementReceipt;

        }


        public ICollection<ErrorHandler> SaveCallAdvReplacementReceipt(CallAdvReplacementReceipt argCallAdvReplacementReceipt)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallAdvReplacementReceiptExists(argCallAdvReplacementReceipt.CallAdvRepReceiptCode, argCallAdvReplacementReceipt.CallCode, argCallAdvReplacementReceipt.CallItemNo, argCallAdvReplacementReceipt.PartnerCode, argCallAdvReplacementReceipt.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallAdvReplacementReceipt(argCallAdvReplacementReceipt, da, lstErr);
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
                    UpdateCallAdvReplacementReceipt(argCallAdvReplacementReceipt, da, lstErr);
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

        public PartnerErrorResult SaveCallAdvReplacementReceipt(CallAdvRepReceiptCol argCallAdvRepReceiptCol, string argPartnerCode, string argClientCode, string argUserName)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            PartnerErrorResult errorcol = new PartnerErrorResult();
            PartnerGoodsMovementManager objPartnerGMManager = new PartnerGoodsMovementManager();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                string strGoodsMovementCode = "";
                int ictr = 0;
                strGoodsMovementCode = objPartnerGMManager.GenerateGMCode("NEW", "GM01", argPartnerCode, argClientCode, da);
                foreach (CallAdvReplacementReceipt objCallAdvReplacementReceipt in argCallAdvRepReceiptCol.colCallAdvReplacementReceipt)
                {
                    if (objCallAdvReplacementReceipt.IsDeleted == 0)
                    {
                        ictr = ictr + 1;

                        if (blnIsCallAdvReplacementReceiptExists(objCallAdvReplacementReceipt.CallAdvRepReceiptCode, objCallAdvReplacementReceipt.CallCode, objCallAdvReplacementReceipt.CallItemNo, objCallAdvReplacementReceipt.PartnerCode, objCallAdvReplacementReceipt.ClientCode, da) == false)
                        {
                            objCallAdvReplacementReceipt.PGoodsMovementCode = strGoodsMovementCode;
                            objCallAdvReplacementReceipt.GMItemNo = ictr;

                            InsertCallAdvReplacementReceipt(objCallAdvReplacementReceipt, da, lstErr);

                        }
                        else
                        {
                            UpdateCallAdvReplacementReceipt(objCallAdvReplacementReceipt, da, lstErr);
                        }
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

                /* Partner Goods Movement Declared */
                PartnerGoodsMovement objPartnerGM = new PartnerGoodsMovement();

                objPartnerGM.PGoodsMovementCode = strGoodsMovementCode;
                objPartnerGM.PartnerGMDocTypeCode = "GM09";
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
                foreach (CallAdvReplacementReceipt objCallAdvReplacementReceipt in argCallAdvRepReceiptCol.colCallAdvReplacementReceipt)
                {
                    if (objCallAdvReplacementReceipt.IsDeleted == 0)
                    {
                        dsMatDocType = new DataSet();
                        if (IsNew == true)
                        {
                            ictr = ictr + 1;
                            dsMatDocType = objPartnerMatDocTypeMan.GetPartnerMaterialDocType(objCallAdvReplacementReceipt.MaterialDocTypeCode, objCallAdvReplacementReceipt.ClientCode, da);

                            PartnerGoodsMovementDetail objPartnerGMDetailsnew = new PartnerGoodsMovementDetail();
                            objPartnerGMDetailsnew.PGoodsMovementCode = strGoodsMovementCode;
                            objPartnerGMDetailsnew.ItemNo = ictr;
                            tmpItemNo = ictr;
                            objPartnerGMDetailsnew.MaterialCode = Convert.ToString(objCallAdvReplacementReceipt.MaterialCode);
                            objPartnerGMDetailsnew.MatGroup1Code = Convert.ToString(objCallAdvReplacementReceipt.MatGroup1Code);
                           
                            
                            objPartnerGMDetailsnew.Quantity = Convert.ToInt32(objCallAdvReplacementReceipt.Quantity);
                            objPartnerGMDetailsnew.UOMCode = Convert.ToString(objCallAdvReplacementReceipt.UOMCode);
                            objPartnerGMDetailsnew.UnitPrice = Convert.ToDouble(objCallAdvReplacementReceipt.UnitPrice);
                            objPartnerGMDetailsnew.ClientCode = Convert.ToString(objCallAdvReplacementReceipt.ClientCode);
                            objPartnerGMDetailsnew.CreatedBy = Convert.ToString(objCallAdvReplacementReceipt.CreatedBy);
                            objPartnerGMDetailsnew.ModifiedBy = Convert.ToString(objCallAdvReplacementReceipt.ModifiedBy);
                            objPartnerGMDetailsnew.TranRefDocCode = Convert.ToString(objCallAdvReplacementReceipt.CallAdvRepReceiptCode);
                            objPartnerGMDetailsnew.TranRefDocItemNo = Convert.ToInt32(ictr);
                            objPartnerGMDetailsnew.MaterialDocTypeCode = Convert.ToString(objCallAdvReplacementReceipt.MaterialDocTypeCode);
                            objPartnerGMDetailsnew.PartnerCode = Convert.ToString(argPartnerCode);

                            if (dsMatDocType != null)
                            {
                                if (dsMatDocType.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["AllowedFromStock"]).Trim() != "HIDE")
                                    {
                                        objPartnerGMDetailsnew.StockIndicator = Convert.ToString(objCallAdvReplacementReceipt.StockIndicator);
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.StockIndicator = "";
                                    }

                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["AllowedToStock"]).Trim() != "HIDE")
                                    {
                                        objPartnerGMDetailsnew.ToStockIndicator = Convert.ToString(objCallAdvReplacementReceipt.StockIndicator);
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
                                        objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(objCallAdvReplacementReceipt.PartnerCode);
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.FromPartnerCode = "";
                                    }

                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["FromStore"]).Trim() != "HIDE")
                                    {
                                        objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(objCallAdvReplacementReceipt.StoreCode);
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
                                        objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(objCallAdvReplacementReceipt.PartnerCode);
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.ToPartnerCode = "";
                                    }

                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToStore"]).Trim() != "HIDE")
                                    {
                                        objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(objCallAdvReplacementReceipt.StoreCode);
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.ToStoreCode = "";
                                    }

                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToEmployee"]).Trim() != "HIDE")
                                    {
                                        objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString("");
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
                                        objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(objCallAdvReplacementReceipt.MaterialCode);
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
                                    objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(objCallAdvReplacementReceipt.PartnerCode);
                                    objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString("");
                                    objPartnerGMDetailsnew.ToPlantCode = "";
                                    objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(objCallAdvReplacementReceipt.StoreCode);
                                    objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(objCallAdvReplacementReceipt.MaterialCode);
                                }
                            }
                            objPartnerGMCol.colPartnerGMDetail.Add(objPartnerGMDetailsnew);

                            /*Partner Goods Movement Serial Detail*/

                            PartnerGoodsMovSerialDetail objPartnerGoodsMovSerialnew = new PartnerGoodsMovSerialDetail();
                            objPartnerGoodsMovSerialnew.PGoodsMovementCode = strGoodsMovementCode;
                            objPartnerGoodsMovSerialnew.ItemNo = tmpItemNo;
                            objPartnerGoodsMovSerialnew.SerialNo1 = Convert.ToString(objCallAdvReplacementReceipt.SerialNo1);
                            objPartnerGoodsMovSerialnew.SerialNo2 = Convert.ToString(objCallAdvReplacementReceipt.SerialNo2);
                            objPartnerGoodsMovSerialnew.MaterialCode = Convert.ToString(objCallAdvReplacementReceipt.MaterialCode);
                            objPartnerGoodsMovSerialnew.MatGroup1Code = Convert.ToString(objCallAdvReplacementReceipt.MatGroup1Code);
                            objPartnerGoodsMovSerialnew.RefDocCode = Convert.ToString(objCallAdvReplacementReceipt.CallCode);
                            objPartnerGoodsMovSerialnew.RefDocItemNo = Convert.ToInt32(objCallAdvReplacementReceipt.CallItemNo);
                            objPartnerGoodsMovSerialnew.RefDocType = Convert.ToString("");
                            objPartnerGoodsMovSerialnew.TranRefDocCode = Convert.ToString(objCallAdvReplacementReceipt.CallAdvRepReceiptCode);
                            objPartnerGoodsMovSerialnew.TranRefDocItemNo = Convert.ToInt32(ictr);
                            objPartnerGoodsMovSerialnew.IsDeleted = 0;
                            objPartnerGoodsMovSerialnew.ClientCode = Convert.ToString(objCallAdvReplacementReceipt.ClientCode);
                            objPartnerGoodsMovSerialnew.CreatedBy = Convert.ToString(objCallAdvReplacementReceipt.CreatedBy);
                            objPartnerGoodsMovSerialnew.ModifiedBy = Convert.ToString(objCallAdvReplacementReceipt.ModifiedBy);
                            objPartnerGoodsMovSerialnew.StockIndicator = Convert.ToString(objCallAdvReplacementReceipt.StockIndicator);
                            objPartnerGoodsMovSerialnew.ToStockIndicator = Convert.ToString(objCallAdvReplacementReceipt.StockIndicator);

                            if (dsMatDocType != null)
                            {
                                if (dsMatDocType.Tables[0].Rows.Count > 0)
                                {
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
                                        objPartnerGoodsMovSerialnew.PartnerCode = Convert.ToString(objCallAdvReplacementReceipt.PartnerCode);
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.PartnerCode = "";
                                    }

                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["FromStore"]).Trim() != "HIDE")
                                    {
                                        objPartnerGoodsMovSerialnew.StoreCode = Convert.ToString(objCallAdvReplacementReceipt.StoreCode);
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
                                        objPartnerGoodsMovSerialnew.ToPartnerCode = Convert.ToString(objCallAdvReplacementReceipt.PartnerCode);
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.ToPartnerCode = "";
                                    }

                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToStore"]).Trim() != "HIDE")
                                    {
                                        objPartnerGoodsMovSerialnew.ToStoreCode = Convert.ToString(objCallAdvReplacementReceipt.StoreCode);
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.ToStoreCode = "";
                                    }

                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToEmployee"]).Trim() != "HIDE")
                                    {
                                        objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = Convert.ToString("");
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
                                        objPartnerGoodsMovSerialnew.ToMaterialCode = Convert.ToString(objCallAdvReplacementReceipt.MaterialCode);
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.ToMaterialCode = "";
                                    }


                                }
                                else
                                {
                                    objPartnerGoodsMovSerialnew.PlantCode = "";
                                    objPartnerGoodsMovSerialnew.PartnerCode = "";
                                    objPartnerGoodsMovSerialnew.PartnerEmployeeCode = "";
                                    objPartnerGoodsMovSerialnew.StoreCode = "";
                                    objPartnerGoodsMovSerialnew.ToPartnerCode = Convert.ToString(objCallAdvReplacementReceipt.PartnerCode);
                                    objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = Convert.ToString("");
                                    objPartnerGoodsMovSerialnew.ToPlantCode = "";
                                    objPartnerGoodsMovSerialnew.ToStoreCode = Convert.ToString(objCallAdvReplacementReceipt.StoreCode);
                                    objPartnerGoodsMovSerialnew.ToMaterialCode = Convert.ToString(objCallAdvReplacementReceipt.MaterialCode);
                                }
                            }

                            objPartnerGMSerialCol.colPartnerGMSerialDetail.Add(objPartnerGoodsMovSerialnew);

                            /*----------------------------------------------------------------------------------------------------------*/

                        }
                    }
                }
                if (objPartnerGMCol.colPartnerGMDetail.Count > 0)
                {
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


        public void InsertCallAdvReplacementReceipt(CallAdvReplacementReceipt argCallAdvReplacementReceipt, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[24];
            param[0] = new SqlParameter("@CallAdvRepReceiptCode", argCallAdvReplacementReceipt.CallAdvRepReceiptCode);
            param[1] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvReplacementReceipt.CallAdvRepDocTypeCode);
            param[2] = new SqlParameter("@CallCode", argCallAdvReplacementReceipt.CallCode);
            param[3] = new SqlParameter("@CallItemNo", argCallAdvReplacementReceipt.CallItemNo);
            param[4] = new SqlParameter("@SerialNo1", argCallAdvReplacementReceipt.SerialNo1);
            param[5] = new SqlParameter("@SerialNo2", argCallAdvReplacementReceipt.SerialNo2);
            param[6] = new SqlParameter("@MaterialCode", argCallAdvReplacementReceipt.MaterialCode);
            param[7] = new SqlParameter("@MatGroup1Code", argCallAdvReplacementReceipt.MatGroup1Code);
            param[8] = new SqlParameter("@Quantity", argCallAdvReplacementReceipt.Quantity);
            param[9] = new SqlParameter("@UOMCode", argCallAdvReplacementReceipt.UOMCode);
            param[10] = new SqlParameter("@UnitPrice", argCallAdvReplacementReceipt.UnitPrice);
            param[11] = new SqlParameter("@PartnerCode", argCallAdvReplacementReceipt.PartnerCode);
            param[12] = new SqlParameter("@StoreCode", argCallAdvReplacementReceipt.StoreCode);
            param[13] = new SqlParameter("@StockIndicator", argCallAdvReplacementReceipt.StockIndicator);
            param[14] = new SqlParameter("@PartnerEmployeeCode", argCallAdvReplacementReceipt.PartnerEmployeeCode);
            param[15] = new SqlParameter("@MaterialDocTypeCode", argCallAdvReplacementReceipt.MaterialDocTypeCode);
            param[16] = new SqlParameter("@PGoodsMovementCode", argCallAdvReplacementReceipt.PGoodsMovementCode);
            param[17] = new SqlParameter("@GMItemNo", argCallAdvReplacementReceipt.GMItemNo);
            param[18] = new SqlParameter("@ClientCode", argCallAdvReplacementReceipt.ClientCode);
            param[19] = new SqlParameter("@CreatedBy", argCallAdvReplacementReceipt.CreatedBy);
            param[20] = new SqlParameter("@ModifiedBy", argCallAdvReplacementReceipt.ModifiedBy);
        
            param[21] = new SqlParameter("@Type", SqlDbType.Char);
            param[21].Size = 1;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[22].Size = 255;
            param[22].Direction = ParameterDirection.Output;

            param[23] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[23].Size = 20;
            param[23].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallAdvReplacementReceipt", param);


            string strMessage = Convert.ToString(param[22].Value);
            string strType = Convert.ToString(param[21].Value);
            string strRetValue = Convert.ToString(param[23].Value);


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


        public void UpdateCallAdvReplacementReceipt(CallAdvReplacementReceipt argCallAdvReplacementReceipt, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[24];
            param[0] = new SqlParameter("@CallAdvRepReceiptCode", argCallAdvReplacementReceipt.CallAdvRepReceiptCode);
            param[1] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvReplacementReceipt.CallAdvRepDocTypeCode);
            param[2] = new SqlParameter("@CallCode", argCallAdvReplacementReceipt.CallCode);
            param[3] = new SqlParameter("@CallItemNo", argCallAdvReplacementReceipt.CallItemNo);
            param[4] = new SqlParameter("@SerialNo1", argCallAdvReplacementReceipt.SerialNo1);
            param[5] = new SqlParameter("@SerialNo2", argCallAdvReplacementReceipt.SerialNo2);
            param[6] = new SqlParameter("@MaterialCode", argCallAdvReplacementReceipt.MaterialCode);
            param[7] = new SqlParameter("@MatGroup1Code", argCallAdvReplacementReceipt.MatGroup1Code);
            param[8] = new SqlParameter("@Quantity", argCallAdvReplacementReceipt.Quantity);
            param[9] = new SqlParameter("@UOMCode", argCallAdvReplacementReceipt.UOMCode);
            param[10] = new SqlParameter("@UnitPrice", argCallAdvReplacementReceipt.UnitPrice);
            param[11] = new SqlParameter("@PartnerCode", argCallAdvReplacementReceipt.PartnerCode);
            param[12] = new SqlParameter("@StoreCode", argCallAdvReplacementReceipt.StoreCode);
            param[13] = new SqlParameter("@StockIndicator", argCallAdvReplacementReceipt.StockIndicator);
            param[14] = new SqlParameter("@PartnerEmployeeCode", argCallAdvReplacementReceipt.PartnerEmployeeCode);
            param[15] = new SqlParameter("@MaterialDocTypeCode", argCallAdvReplacementReceipt.MaterialDocTypeCode);
            param[16] = new SqlParameter("@PGoodsMovementCode", argCallAdvReplacementReceipt.PGoodsMovementCode);
            param[17] = new SqlParameter("@GMItemNo", argCallAdvReplacementReceipt.GMItemNo);
            param[18] = new SqlParameter("@ClientCode", argCallAdvReplacementReceipt.ClientCode);
            param[19] = new SqlParameter("@CreatedBy", argCallAdvReplacementReceipt.CreatedBy);
            param[20] = new SqlParameter("@ModifiedBy", argCallAdvReplacementReceipt.ModifiedBy);
         
            param[21] = new SqlParameter("@Type", SqlDbType.Char);
            param[21].Size = 1;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[22].Size = 255;
            param[22].Direction = ParameterDirection.Output;

            param[23] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[23].Size = 20;
            param[23].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallAdvReplacementReceipt", param);


            string strMessage = Convert.ToString(param[22].Value);
            string strType = Convert.ToString(param[21].Value);
            string strRetValue = Convert.ToString(param[23].Value);


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


        public ICollection<ErrorHandler> DeleteCallAdvReplacementReceipt(string argCallAdvRepReceiptCode, string argCallCode, int argCallItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@CallAdvRepReceiptCode", argCallAdvRepReceiptCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCallAdvReplacementReceipt", param);


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


        public bool blnIsCallAdvReplacementReceiptExists(string argCallAdvRepReceiptCode, string argCallCode, int argCallItemNo, string argPartnerCode, string argClientCode)
        {
            bool IsCallAdvReplacementReceiptExists = false;
            DataSet ds = new DataSet();
            ds = GetCallAdvReplacementReceipt(argCallAdvRepReceiptCode, argCallCode, argCallItemNo,argPartnerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallAdvReplacementReceiptExists = true;
            }
            else
            {
                IsCallAdvReplacementReceiptExists = false;
            }
            return IsCallAdvReplacementReceiptExists;
        }

        public bool blnIsCallAdvReplacementReceiptExists(string argCallAdvRepReceiptCode, string argCallCode, int argCallItemNo,string argPartnerCode, string argClientCode, DataAccess da)
        {
            bool IsCallAdvReplacementReceiptExists = false;
            DataSet ds = new DataSet();
            ds = GetCallAdvReplacementReceipt(argCallAdvRepReceiptCode, argCallCode, argCallItemNo,argPartnerCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallAdvReplacementReceiptExists = true;
            }
            else
            {
                IsCallAdvReplacementReceiptExists = false;
            }
            return IsCallAdvReplacementReceiptExists;
        }


        public string GenerateCallAdvRepReceipt(string argCallAdvRepReceiptCode, string argCallAdvRepDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CallAdvRepCode", argCallAdvRepReceiptCode);
            param[1] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvRepDocTypeCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            param[4] = new SqlParameter("@GeneratedCallAdvRepCode", SqlDbType.VarChar);
            param[4].Size = 18;
            param[4].Direction = ParameterDirection.Output;

            int i = da.ExecuteNonQuery("SL_GetNewCallAdvRepCode", param);

            string strMessage = Convert.ToString(param[4].Value);

            return strMessage;
        }

    }
}