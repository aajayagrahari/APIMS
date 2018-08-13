
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
    public class CallAdvReplacementIssueManager
    {
        const string CallAdvReplacementIssueTable = "CallAdvReplacementIssue";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CallAdvReplacementIssue objGetCallAdvReplacementIssue(string argCallAdvRepIssueCode, string argCallCode, int argCallItemNo, string argPartnerCOde, string argClientCode)
        {
            CallAdvReplacementIssue argCallAdvReplacementIssue = new CallAdvReplacementIssue();
            DataSet DataSetToFill = new DataSet();

            if (argCallAdvRepIssueCode.Trim() == "")
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

            DataSetToFill = this.GetCallAdvReplacementIssue(argCallAdvRepIssueCode, argCallCode, argCallItemNo,argPartnerCOde, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallAdvReplacementIssue = this.objCreateCallAdvReplacementIssue((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;
            
            return argCallAdvReplacementIssue;
        }

        public ICollection<CallAdvReplacementIssue> colGetCallAdvReplacementIssue(string argCallAdvRepIssueCode,string argCallCode, string argPartnerCode, string argClientCode)
        {
            List<CallAdvReplacementIssue> lst = new List<CallAdvReplacementIssue>();
            DataSet DataSetToFill = new DataSet();
            CallAdvReplacementIssue tCallAdvReplacementIssue = new CallAdvReplacementIssue();

            DataSetToFill = this.GetCallAdvReplacementIssue(argCallAdvRepIssueCode, argCallCode, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallAdvReplacementIssue(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public void colGetCallAdvReplacementIssue(string argCallAdvRepIssueCode, string argCallCode, string argPartnerCode, string argClientCode, ref CallAdvRepIssueCol argCallAdvRepIssueCol)
        {
            DataSet DataSetToFill = new DataSet();
            CallAdvReplacementIssue tCallAdvReplacementIssue = new CallAdvReplacementIssue();

            DataSetToFill = this.GetCallAdvReplacementIssue(argCallAdvRepIssueCode, argCallCode, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argCallAdvRepIssueCol.colCallAdvReplacementIssue.Add(objCreateCallAdvReplacementIssue(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;
        }

        public DataSet GetCallAdvReplacementIssue(string argCallAdvRepIssueCode, string argCallCode, int argCallItemNo, string argPartnerCode,  string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallAdvRepIssueCode", argCallAdvRepIssueCode);
            param[1] = new SqlParameter("@CallCode", argCallCode);
            param[2] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallAdvReplacementIssue4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallAdvReplacementIssue(string argCallAdvRepIssueCode, string argCallCode, int argCallItemNo, string argPartnerCode, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallAdvRepIssueCode", argCallAdvRepIssueCode);
            param[1] = new SqlParameter("@CallCode", argCallCode);
            param[2] = new SqlParameter("@CallItemNo", argCallItemNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallAdvReplacementIssue4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCallAdvReplacementIssue(string argCallAdvRepIssueCode, string argCallCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallAdvRepIssueCode", argCallAdvRepIssueCode);
            param[1] = new SqlParameter("@CallCode", argCallCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);            
            param[3] = new SqlParameter("@ClientCode", argClientCode);
            
            DataSetToFill = da.FillDataSet("SP_GetCallAdvReplacementIssue", param);
            return DataSetToFill;
        }

        public DataSet GetCallAdvRepIssue4Search(string argCallAdvRepDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvRepDocTypeCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallAdvRepIssue4Search", param);
            return DataSetToFill;
        }

        public DataSet GetAdvRepIssueCalls(string argCallAdvRepDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvRepDocTypeCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAdvRepIssueCalls", param);
            return DataSetToFill;
        }
        
        private CallAdvReplacementIssue objCreateCallAdvReplacementIssue(DataRow dr)
        {
            CallAdvReplacementIssue tCallAdvReplacementIssue = new CallAdvReplacementIssue();

            tCallAdvReplacementIssue.SetObjectInfo(dr);

            return tCallAdvReplacementIssue;
        }
        
        public ICollection<ErrorHandler> SaveCallAdvReplacementIssue(CallAdvReplacementIssue argCallAdvReplacementIssue)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallAdvReplacementIssueExists(argCallAdvReplacementIssue.CallAdvRepIssueCode, argCallAdvReplacementIssue.CallCode, argCallAdvReplacementIssue.CallItemNo, argCallAdvReplacementIssue.PartnerCode, argCallAdvReplacementIssue.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallAdvReplacementIssue(argCallAdvReplacementIssue, da, lstErr);
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
                    UpdateCallAdvReplacementIssue(argCallAdvReplacementIssue, da, lstErr);
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

        public PartnerErrorResult SaveCallAdvReplacementIssue(CallAdvRepIssueCol argCallAdvRepIssueCol, string argPartnerCode, string argClientCode, string argUserName)
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
                foreach (CallAdvReplacementIssue objCallAdvReplacementIssue in argCallAdvRepIssueCol.colCallAdvReplacementIssue)
                {
                    if (objCallAdvReplacementIssue.IsDeleted == 0)
                    {
                        ictr = ictr + 1;

                        if (blnIsCallAdvReplacementIssueExists(objCallAdvReplacementIssue.CallAdvRepIssueCode, objCallAdvReplacementIssue.CallCode, objCallAdvReplacementIssue.CallItemNo, objCallAdvReplacementIssue.PartnerCode, objCallAdvReplacementIssue.ClientCode, da) == false)
                        {
                            objCallAdvReplacementIssue.PGoodsMovementCode = strGoodsMovementCode;
                            objCallAdvReplacementIssue.GMItemNo = ictr;

                            InsertCallAdvReplacementIssue(objCallAdvReplacementIssue, da, lstErr);

                        }
                        else
                        {
                            UpdateCallAdvReplacementIssue(objCallAdvReplacementIssue, da, lstErr);
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
                objPartnerGM.PartnerGMDocTypeCode = "GM08";
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
                foreach (CallAdvReplacementIssue objCallAdvReplacementIssue in argCallAdvRepIssueCol.colCallAdvReplacementIssue)
                {
                    if (objCallAdvReplacementIssue.IsDeleted == 0)
                    {
                        dsMatDocType = new DataSet();
                        if (IsNew == true)
                        {
                            ictr = ictr + 1;
                            dsMatDocType = objPartnerMatDocTypeMan.GetPartnerMaterialDocType(objCallAdvReplacementIssue.MaterialDocTypeCode, objCallAdvReplacementIssue.ClientCode, da);

                            PartnerGoodsMovementDetail objPartnerGMDetailsnew = new PartnerGoodsMovementDetail();
                            objPartnerGMDetailsnew.PGoodsMovementCode = strGoodsMovementCode;
                            objPartnerGMDetailsnew.ItemNo = ictr;
                            tmpItemNo = ictr;
                            objPartnerGMDetailsnew.MaterialCode = Convert.ToString(objCallAdvReplacementIssue.MaterialCode);
                            objPartnerGMDetailsnew.MatGroup1Code = Convert.ToString(objCallAdvReplacementIssue.MatGroup1Code);
                            
                            objPartnerGMDetailsnew.Quantity = Convert.ToInt32(objCallAdvReplacementIssue.Quantity);
                            objPartnerGMDetailsnew.UOMCode = Convert.ToString(objCallAdvReplacementIssue.UOMCode);
                            objPartnerGMDetailsnew.UnitPrice = Convert.ToDouble(objCallAdvReplacementIssue.UnitPrice);
                            objPartnerGMDetailsnew.ClientCode = Convert.ToString(objCallAdvReplacementIssue.ClientCode);
                            objPartnerGMDetailsnew.CreatedBy = Convert.ToString(objCallAdvReplacementIssue.CreatedBy);
                            objPartnerGMDetailsnew.ModifiedBy = Convert.ToString(objCallAdvReplacementIssue.ModifiedBy);
                            objPartnerGMDetailsnew.TranRefDocCode = Convert.ToString(objCallAdvReplacementIssue.CallAdvRepIssueCode);
                            objPartnerGMDetailsnew.TranRefDocItemNo = Convert.ToInt32(ictr);
                            objPartnerGMDetailsnew.MaterialDocTypeCode = Convert.ToString(objCallAdvReplacementIssue.MaterialDocTypeCode);
                            objPartnerGMDetailsnew.PartnerCode = Convert.ToString(argPartnerCode);
                            
                            if (dsMatDocType != null)
                            {
                                if (dsMatDocType.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["AllowedFromStock"]).Trim() != "HIDE")
                                    {
                                        objPartnerGMDetailsnew.StockIndicator = Convert.ToString(objCallAdvReplacementIssue.StockIndicator);
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.StockIndicator = "";
                                    }

                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["AllowedToStock"]).Trim() != "HIDE")
                                    {
                                        objPartnerGMDetailsnew.ToStockIndicator = Convert.ToString(objCallAdvReplacementIssue.StockIndicator);
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.ToStockIndicator = Convert.ToString("");
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
                                        objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(objCallAdvReplacementIssue.PartnerCode);
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.FromPartnerCode = "";
                                    }

                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["FromStore"]).Trim() != "HIDE")
                                    {
                                        objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(objCallAdvReplacementIssue.StoreCode);
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
                                        objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString("");
                                    }
                                    else
                                    {
                                        objPartnerGMDetailsnew.ToPartnerCode = "";
                                    }

                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToStore"]).Trim() != "HIDE")
                                    {
                                        objPartnerGMDetailsnew.ToStoreCode = Convert.ToString("");
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
                                    objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(objCallAdvReplacementIssue.PartnerCode);
                                    objPartnerGMDetailsnew.FromPartnerEmployeeCode = "";
                                    objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(objCallAdvReplacementIssue.StoreCode);
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
                            objPartnerGoodsMovSerialnew.SerialNo1 = Convert.ToString(objCallAdvReplacementIssue.SerialNo1);
                            objPartnerGoodsMovSerialnew.SerialNo2 = Convert.ToString(objCallAdvReplacementIssue.SerialNo2);
                            objPartnerGoodsMovSerialnew.MaterialCode = Convert.ToString(objCallAdvReplacementIssue.MaterialCode);
                            objPartnerGoodsMovSerialnew.MatGroup1Code = Convert.ToString(objCallAdvReplacementIssue.MatGroup1Code);
                            objPartnerGoodsMovSerialnew.RefDocCode = Convert.ToString(objCallAdvReplacementIssue.CallCode);
                            objPartnerGoodsMovSerialnew.RefDocItemNo = Convert.ToInt32(objCallAdvReplacementIssue.CallItemNo);
                            objPartnerGoodsMovSerialnew.RefDocType = Convert.ToString("");
                            objPartnerGoodsMovSerialnew.TranRefDocCode = Convert.ToString(objCallAdvReplacementIssue.CallAdvRepIssueCode);
                            objPartnerGoodsMovSerialnew.TranRefDocItemNo = Convert.ToInt32(ictr);
                            objPartnerGoodsMovSerialnew.IsDeleted = 0;
                            objPartnerGoodsMovSerialnew.ClientCode = Convert.ToString(objCallAdvReplacementIssue.ClientCode);
                            objPartnerGoodsMovSerialnew.CreatedBy = Convert.ToString(objCallAdvReplacementIssue.CreatedBy);
                            objPartnerGoodsMovSerialnew.ModifiedBy = Convert.ToString(objCallAdvReplacementIssue.ModifiedBy);
                            
                           

                            if (dsMatDocType != null)
                            {
                                if (dsMatDocType.Tables[0].Rows.Count > 0)
                                {
                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["AllowedFromStock"]).Trim() != "HIDE")
                                    {
                                        objPartnerGoodsMovSerialnew.StockIndicator = Convert.ToString(objCallAdvReplacementIssue.StockIndicator);
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.StockIndicator = "";
                                    }

                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["AllowedToStock"]).Trim() != "HIDE")
                                    {
                                        objPartnerGoodsMovSerialnew.ToStockIndicator = Convert.ToString(objCallAdvReplacementIssue.StockIndicator);
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
                                        objPartnerGoodsMovSerialnew.PartnerCode = Convert.ToString(objCallAdvReplacementIssue.PartnerCode);
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.PartnerCode = "";
                                    }

                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["FromStore"]).Trim() != "HIDE")
                                    {
                                        objPartnerGoodsMovSerialnew.StoreCode = Convert.ToString(objCallAdvReplacementIssue.StoreCode);
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
                                        objPartnerGoodsMovSerialnew.ToPartnerCode = Convert.ToString("");
                                    }
                                    else
                                    {
                                        objPartnerGoodsMovSerialnew.ToPartnerCode = "";
                                    }

                                    if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToStore"]).Trim() != "HIDE")
                                    {
                                        objPartnerGoodsMovSerialnew.ToStoreCode = Convert.ToString("");
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
                                    objPartnerGoodsMovSerialnew.PartnerCode = Convert.ToString(objCallAdvReplacementIssue.PartnerCode);
                                    objPartnerGoodsMovSerialnew.PartnerEmployeeCode = "";
                                    objPartnerGoodsMovSerialnew.StoreCode = Convert.ToString(objCallAdvReplacementIssue.StoreCode);
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
        
        public void InsertCallAdvReplacementIssue(CallAdvReplacementIssue argCallAdvReplacementIssue, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[24];
            param[0] = new SqlParameter("@CallAdvRepIssueCode", argCallAdvReplacementIssue.CallAdvRepIssueCode);
            param[1] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvReplacementIssue.CallAdvRepDocTypeCode);
            param[2] = new SqlParameter("@CallCode", argCallAdvReplacementIssue.CallCode);
            param[3] = new SqlParameter("@CallItemNo", argCallAdvReplacementIssue.CallItemNo);
            param[4] = new SqlParameter("@SerialNo1", argCallAdvReplacementIssue.SerialNo1);
            param[5] = new SqlParameter("@SerialNo2", argCallAdvReplacementIssue.SerialNo2);
            param[6] = new SqlParameter("@MaterialCode", argCallAdvReplacementIssue.MaterialCode);
            param[7] = new SqlParameter("@MatGroup1Code", argCallAdvReplacementIssue.MatGroup1Code);
            param[8] = new SqlParameter("@Quantity", argCallAdvReplacementIssue.Quantity);
            param[9] = new SqlParameter("@UOMCode", argCallAdvReplacementIssue.UOMCode);
            param[10] = new SqlParameter("@UnitPrice", argCallAdvReplacementIssue.UnitPrice);
            param[11] = new SqlParameter("@PartnerCode", argCallAdvReplacementIssue.PartnerCode);
            param[12] = new SqlParameter("@StoreCode", argCallAdvReplacementIssue.StoreCode);
            param[13] = new SqlParameter("@StockIndicator", argCallAdvReplacementIssue.StockIndicator);
            param[14] = new SqlParameter("@PartnerEmployeeCode", argCallAdvReplacementIssue.PartnerEmployeeCode);
            param[15] = new SqlParameter("@MaterialDocTypeCode", argCallAdvReplacementIssue.MaterialDocTypeCode);
            param[16] = new SqlParameter("@PGoodsMovementCode", argCallAdvReplacementIssue.PGoodsMovementCode);
            param[17] = new SqlParameter("@GMItemNo", argCallAdvReplacementIssue.GMItemNo);
            param[18] = new SqlParameter("@ClientCode", argCallAdvReplacementIssue.ClientCode);
            param[19] = new SqlParameter("@CreatedBy", argCallAdvReplacementIssue.CreatedBy);
            param[20] = new SqlParameter("@ModifiedBy", argCallAdvReplacementIssue.ModifiedBy);
            
            param[21] = new SqlParameter("@Type", SqlDbType.Char);
            param[21].Size = 1;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[22].Size = 255;
            param[22].Direction = ParameterDirection.Output;

            param[23] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[23].Size = 20;
            param[23].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallAdvReplacementIssue", param);


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
        
        public void UpdateCallAdvReplacementIssue(CallAdvReplacementIssue argCallAdvReplacementIssue, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[24];
            param[0] = new SqlParameter("@CallAdvRepIssueCode", argCallAdvReplacementIssue.CallAdvRepIssueCode);
            param[1] = new SqlParameter("@CallAdvRepDocTypeCode", argCallAdvReplacementIssue.CallAdvRepDocTypeCode);
            param[2] = new SqlParameter("@CallCode", argCallAdvReplacementIssue.CallCode);
            param[3] = new SqlParameter("@CallItemNo", argCallAdvReplacementIssue.CallItemNo);
            param[4] = new SqlParameter("@SerialNo1", argCallAdvReplacementIssue.SerialNo1);
            param[5] = new SqlParameter("@SerialNo2", argCallAdvReplacementIssue.SerialNo2);
            param[6] = new SqlParameter("@MaterialCode", argCallAdvReplacementIssue.MaterialCode);
            param[7] = new SqlParameter("@MatGroup1Code", argCallAdvReplacementIssue.MatGroup1Code);
            param[8] = new SqlParameter("@Quantity", argCallAdvReplacementIssue.Quantity);
            param[9] = new SqlParameter("@UOMCode", argCallAdvReplacementIssue.UOMCode);
            param[10] = new SqlParameter("@UnitPrice", argCallAdvReplacementIssue.UnitPrice);
            param[11] = new SqlParameter("@PartnerCode", argCallAdvReplacementIssue.PartnerCode);
            param[12] = new SqlParameter("@StoreCode", argCallAdvReplacementIssue.StoreCode);
            param[13] = new SqlParameter("@StockIndicator", argCallAdvReplacementIssue.StockIndicator);
            param[14] = new SqlParameter("@PartnerEmployeeCode", argCallAdvReplacementIssue.PartnerEmployeeCode);
            param[15] = new SqlParameter("@MaterialDocTypeCode", argCallAdvReplacementIssue.MaterialDocTypeCode);
            param[16] = new SqlParameter("@PGoodsMovementCode", argCallAdvReplacementIssue.PGoodsMovementCode);
            param[17] = new SqlParameter("@GMItemNo", argCallAdvReplacementIssue.GMItemNo);
            param[18] = new SqlParameter("@ClientCode", argCallAdvReplacementIssue.ClientCode);
            param[19] = new SqlParameter("@CreatedBy", argCallAdvReplacementIssue.CreatedBy);
            param[20] = new SqlParameter("@ModifiedBy", argCallAdvReplacementIssue.ModifiedBy);
          
            param[21] = new SqlParameter("@Type", SqlDbType.Char);
            param[21].Size = 1;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[22].Size = 255;
            param[22].Direction = ParameterDirection.Output;

            param[23] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[23].Size = 20;
            param[23].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallAdvReplacementIssue", param);


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
        
        public ICollection<ErrorHandler> DeleteCallAdvReplacementIssue(string argCallAdvRepIssueCode, string argCallCode, int argCallItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@CallAdvRepIssueCode", argCallAdvRepIssueCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCallAdvReplacementIssue", param);


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
        
        public bool blnIsCallAdvReplacementIssueExists(string argCallAdvRepIssueCode, string argCallCode, int argCallItemNo, string argPartnerCode, string argClientCode)
        {
            bool IsCallAdvReplacementIssueExists = false;
            DataSet ds = new DataSet();
            ds = GetCallAdvReplacementIssue(argCallAdvRepIssueCode, argCallCode, argCallItemNo,argPartnerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallAdvReplacementIssueExists = true;
            }
            else
            {
                IsCallAdvReplacementIssueExists = false;
            }
            return IsCallAdvReplacementIssueExists;
        }

        public bool blnIsCallAdvReplacementIssueExists(string argCallAdvRepIssueCode, string argCallCode, int argCallItemNo, string argPartnerCode, string argClientCode, DataAccess da)
        {
            bool IsCallAdvReplacementIssueExists = false;
            DataSet ds = new DataSet();
            ds = GetCallAdvReplacementIssue(argCallAdvRepIssueCode, argCallCode, argCallItemNo,argPartnerCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallAdvReplacementIssueExists = true;
            }
            else
            {
                IsCallAdvReplacementIssueExists = false;
            }
            return IsCallAdvReplacementIssueExists;
        }

        public string GenerateCallAdvRepIssue(string argCallAdvRepIssueCode, string argCallAdvRepDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CallAdvRepCode", argCallAdvRepIssueCode);
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