
//Created On :: 01, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class AsgTechnicianCallMasterManager
    {
        const string AsgTechnicianCallMasterTable = "AsgTechnicianCallMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        AsgTechnicianCallDetailManager objAsgTechnicianCallDetailMan = new AsgTechnicianCallDetailManager();
        
        public AsgTechnicianCallMaster objGetAsgTechnicianCallMaster(string argAsgTechCallCode, string argPartnerCode, string argClientCode)
        {
            AsgTechnicianCallMaster argAsgTechnicianCallMaster = new AsgTechnicianCallMaster();
            DataSet DataSetToFill = new DataSet();

            if (argAsgTechCallCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetAsgTechnicianCallMaster(argAsgTechCallCode, argPartnerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argAsgTechnicianCallMaster = this.objCreateAsgTechnicianCallMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argAsgTechnicianCallMaster;
        }

        public ICollection<AsgTechnicianCallMaster> colGetAsgTechnicianCallMaster(string argPartnerCode, string argClientCode)
        {
            List<AsgTechnicianCallMaster> lst = new List<AsgTechnicianCallMaster>();
            DataSet DataSetToFill = new DataSet();
            AsgTechnicianCallMaster tAsgTechnicianCallMaster = new AsgTechnicianCallMaster();

            DataSetToFill = this.GetAsgTechnicianCallMaster(argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateAsgTechnicianCallMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetAsgTechnicianCallMaster(string argAsgTechCallCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@AsgTechCallCode", argAsgTechCallCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAsgTechnicianCallMaster4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetAsgTechnicianCallMaster(string argAsgTechCallCode, string argPartnerCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@AsgTechCallCode", argAsgTechCallCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);            
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetAsgTechnicianCallMaster4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetAsgTechnicianCallMaster(string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAsgTechnicianCallMaster", param);
            return DataSetToFill;
        }
        
        public DataSet GetAsgTechCallMaster4Type(string argAsgTechDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@AsgTechDocTypeCode", argAsgTechDocTypeCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAsgTechCallMaster4Type", param);
            return DataSetToFill;
        }        

        private AsgTechnicianCallMaster objCreateAsgTechnicianCallMaster(DataRow dr)
        {
            AsgTechnicianCallMaster tAsgTechnicianCallMaster = new AsgTechnicianCallMaster();

            tAsgTechnicianCallMaster.SetObjectInfo(dr);

            return tAsgTechnicianCallMaster;

        }
        
        public ICollection<ErrorHandler> SaveAsgTechnicianCallMaster(AsgTechnicianCallMaster argAsgTechnicianCallMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsAsgTechnicianCallMasterExists(argAsgTechnicianCallMaster.AsgTechCallCode, argAsgTechnicianCallMaster.PartnerCode, argAsgTechnicianCallMaster.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertAsgTechnicianCallMaster(argAsgTechnicianCallMaster, da, lstErr);
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
                    UpdateAsgTechnicianCallMaster(argAsgTechnicianCallMaster, da, lstErr);
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

        public PartnerErrorResult SaveAsgTechnicianCallMaster(AsgTechnicianCallMaster argAsgTechnicianCallMaster, AsgTechnicianCallDetailCol argAsgTechCallDetailCol)
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

                if (blnIsAsgTechnicianCallMasterExists(argAsgTechnicianCallMaster.AsgTechCallCode, argAsgTechnicianCallMaster.PartnerCode, argAsgTechnicianCallMaster.ClientCode, da) == false)
                {
                    strretValue = InsertAsgTechnicianCallMaster(argAsgTechnicianCallMaster, da, lstErr);
                }
                else
                {
                    strretValue = UpdateAsgTechnicianCallMaster(argAsgTechnicianCallMaster, da, lstErr);
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
                    string strGoodsMovementCode = "";
                    if(argAsgTechnicianCallMaster.AssignType.Trim() != "CALL")
                    {
                        strGoodsMovementCode = objPartnerGMManager.GenerateGMCode("NEW", "GM01", argAsgTechnicianCallMaster.PartnerCode, argAsgTechnicianCallMaster.ClientCode, da);
                    }

                    if (argAsgTechCallDetailCol.colAsgTechnicianCallDetail.Count > 0)
                    {
                        foreach (AsgTechnicianCallDetail objAsgTechnicianCallDetail in argAsgTechCallDetailCol.colAsgTechnicianCallDetail)
                        {
                            objAsgTechnicianCallDetail.AsgTechCallCode = strretValue;

                            if (argAsgTechnicianCallMaster.AssignType.Trim() != "CALL")
                            {
                                objAsgTechnicianCallDetail.PGoodsMovementCode = strGoodsMovementCode;
                                objAsgTechnicianCallDetail.GMItemNo = objAsgTechnicianCallDetail.ItemNo;
                            }

                            if (objAsgTechnicianCallDetail.IsDeleted == 0)
                            {
                                objAsgTechnicianCallDetailMan.SaveAsgTechnicianCallDetail(objAsgTechnicianCallDetail, da, lstErr);
                            }
                            else
                            {
                                /*Delete function*/
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

                    if (argAsgTechnicianCallMaster.AssignType.Trim().ToUpper() != "CALL")
                    {
                        /* Partner Goods Movement Declared */
                        PartnerGoodsMovement objPartnerGM = new PartnerGoodsMovement();

                        objPartnerGM.PGoodsMovementCode = strGoodsMovementCode;
                        objPartnerGM.PartnerGMDocTypeCode = "GM01";
                        objPartnerGM.FromPlantCode = "";
                        objPartnerGM.FromPartnerCode = "";
                        objPartnerGM.FromPartnerEmployeeCode = "";
                        objPartnerGM.FromStoreCode = "";
                        objPartnerGM.ToPartnerCode = Convert.ToString(argAsgTechnicianCallMaster.PartnerCode);
                        objPartnerGM.ToPlantCode = "";
                        objPartnerGM.ToStoreCode = "";
                        objPartnerGM.ToPartnerEmployeeCode = Convert.ToString(argAsgTechnicianCallMaster.PartnerEmployeeCode);
                        objPartnerGM.GoodsMovDate = Convert.ToDateTime(argAsgTechnicianCallMaster.AssignDate);
                        objPartnerGM.ClientCode = argAsgTechnicianCallMaster.ClientCode;
                        objPartnerGM.CreatedBy = argAsgTechnicianCallMaster.CreatedBy;
                        objPartnerGM.ModifiedBy = argAsgTechnicianCallMaster.ModifiedBy;
                        objPartnerGM.TotalQuantity = 0;
                        objPartnerGM.PartnerCode = argAsgTechnicianCallMaster.PartnerCode;


                        /*----------------------------------------------------------------------------------------*/

                        /* Partner Goods Movement Detail */
                        PartnerMaterialDocTypeManager objPartnerMatDocTypeMan = new PartnerMaterialDocTypeManager();

                        PartnerGoodsMovementDetailCol objPartnerGMCol = new PartnerGoodsMovementDetailCol();
                        PartnerGoodsMovSerialDetailCol objPartnerGMSerialCol = new PartnerGoodsMovSerialDetailCol();

                        objPartnerGMCol.colPartnerGMDetail = new List<PartnerGoodsMovementDetail>();
                        objPartnerGMSerialCol.colPartnerGMSerialDetail = new List<PartnerGoodsMovSerialDetail>();

                        DataSet dsMatDocType = null;
                        bool IsNew = true;
                        int iCtr = 0;
                        int tmpItemNo = 0;

                        foreach (AsgTechnicianCallDetail objAsgTechCallDetail in argAsgTechCallDetailCol.colAsgTechnicianCallDetail)
                        {
                            if (objAsgTechCallDetail.IsDeleted == 0)
                            {
                                dsMatDocType = new DataSet();

                                if (IsNew == true)
                                {
                                    iCtr = iCtr + 1;

                                    dsMatDocType = objPartnerMatDocTypeMan.GetPartnerMaterialDocType(objAsgTechCallDetail.MaterialDocTypeCode, objAsgTechCallDetail.ClientCode, da);

                                    PartnerGoodsMovementDetail objPartnerGMDetailsnew = new PartnerGoodsMovementDetail();
                                    objPartnerGMDetailsnew.PGoodsMovementCode = strGoodsMovementCode;
                                    objPartnerGMDetailsnew.ItemNo = iCtr;
                                    tmpItemNo = iCtr;
                                    objPartnerGMDetailsnew.MaterialCode = Convert.ToString(objAsgTechCallDetail.MaterialCode);
                                    objPartnerGMDetailsnew.MatGroup1Code = Convert.ToString(objAsgTechCallDetail.MaterialCode);                                   
                                    objPartnerGMDetailsnew.Quantity = Convert.ToInt32(objAsgTechCallDetail.Quantity);
                                    objPartnerGMDetailsnew.UOMCode = Convert.ToString(objAsgTechCallDetail.UOMCode);
                                    objPartnerGMDetailsnew.ClientCode = Convert.ToString(objAsgTechCallDetail.ClientCode);
                                    objPartnerGMDetailsnew.CreatedBy = Convert.ToString(objAsgTechCallDetail.CreatedBy);
                                    objPartnerGMDetailsnew.ModifiedBy = Convert.ToString(objAsgTechCallDetail.ModifiedBy);
                                    objPartnerGMDetailsnew.TranRefDocCode = Convert.ToString(strretValue);
                                    objPartnerGMDetailsnew.TranRefDocItemNo = Convert.ToInt32(objAsgTechCallDetail.ItemNo);
                                    objPartnerGMDetailsnew.MaterialDocTypeCode = Convert.ToString(objAsgTechCallDetail.MaterialDocTypeCode);
                                    objPartnerGMDetailsnew.PartnerCode = Convert.ToString(objAsgTechCallDetail.PartnerCode);
                                    if (dsMatDocType != null)
                                    {
                                        if (dsMatDocType.Tables[0].Rows.Count > 0)
                                        {

                                            if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["AllowedFromStock"]).Trim() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.StockIndicator = Convert.ToString(objAsgTechCallDetail.StockIndicator);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.StockIndicator = "";
                                            }

                                            if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["AllowedToStock"]).Trim() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.ToStockIndicator = Convert.ToString(objAsgTechCallDetail.ToStockIndicator);
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
                                                objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(objAsgTechCallDetail.PartnerCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.FromPartnerCode = "";
                                            }

                                            if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["FromStore"]).Trim() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(objAsgTechCallDetail.StoreCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.FromStoreCode = "";
                                            }

                                            if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["FromEmployee"]).Trim() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.FromPartnerEmployeeCode = Convert.ToString(objAsgTechCallDetail.FromPartnerEmployeeCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.FromPartnerEmployeeCode = "";
                                            }

                                            if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToPartner"]).Trim() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(objAsgTechCallDetail.ToPartnerCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.ToPartnerCode = "";
                                            }

                                            if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToStore"]).Trim() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(objAsgTechCallDetail.ToStoreCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.ToStoreCode = "";
                                            }

                                            if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["ToEmployee"]).Trim() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString(objAsgTechCallDetail.ToPartnerEmployeeCode);
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
                                                objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(objAsgTechCallDetail.ToMaterialCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.ToMaterialCode = "";
                                            }
                                        }
                                        else
                                        {
                                            objPartnerGMDetailsnew.FromPlantCode = "";
                                            objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(objAsgTechCallDetail.PartnerCode);
                                            objPartnerGMDetailsnew.FromPartnerEmployeeCode = "";
                                            objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(objAsgTechCallDetail.StoreCode);
                                            objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(objAsgTechCallDetail.ToPartnerCode);
                                            objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString(objAsgTechCallDetail.ToPartnerEmployeeCode);
                                            objPartnerGMDetailsnew.ToPlantCode = "";
                                            objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(objAsgTechCallDetail.ToStoreCode);
                                            objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(objAsgTechCallDetail.ToMaterialCode);
                                        }
                                    }

                                    objPartnerGMCol.colPartnerGMDetail.Add(objPartnerGMDetailsnew);


                                    if (objAsgTechCallDetail.SerialNo1 != "" )
                                    {

                                        /*Partner Goods Movement Serial Detail*/

                                        PartnerGoodsMovSerialDetail objPartnerGoodsMovSerialnew = new PartnerGoodsMovSerialDetail();
                                        objPartnerGoodsMovSerialnew.PGoodsMovementCode = strGoodsMovementCode;
                                        objPartnerGoodsMovSerialnew.ItemNo = tmpItemNo;
                                        objPartnerGoodsMovSerialnew.SerialNo1 = Convert.ToString(objAsgTechCallDetail.SerialNo1);
                                        objPartnerGoodsMovSerialnew.SerialNo2 = Convert.ToString(objAsgTechCallDetail.SerialNo2);
                                        objPartnerGoodsMovSerialnew.MaterialCode = Convert.ToString(objAsgTechCallDetail.MaterialCode);
                                        objPartnerGoodsMovSerialnew.MatGroup1Code = Convert.ToString(objAsgTechCallDetail.MatGroup1Code);
                                        objPartnerGoodsMovSerialnew.RefDocCode = Convert.ToString(objAsgTechCallDetail.RefDocCode);
                                        objPartnerGoodsMovSerialnew.RefDocItemNo = Convert.ToInt32(objAsgTechCallDetail.RefDocItemNo);
                                        objPartnerGoodsMovSerialnew.RefDocType = Convert.ToString(objAsgTechCallDetail.RefDocTypeCode);
                                        objPartnerGoodsMovSerialnew.TranRefDocCode = Convert.ToString(strretValue);
                                        objPartnerGoodsMovSerialnew.TranRefDocItemNo = Convert.ToInt32(objAsgTechCallDetail.ItemNo);
                                        objPartnerGoodsMovSerialnew.IsDeleted = 0;
                                        objPartnerGoodsMovSerialnew.ClientCode = Convert.ToString(objAsgTechCallDetail.ClientCode);
                                        objPartnerGoodsMovSerialnew.CreatedBy = Convert.ToString(objAsgTechCallDetail.CreatedBy);
                                        objPartnerGoodsMovSerialnew.ModifiedBy = Convert.ToString(objAsgTechCallDetail.ModifiedBy);
                                        

                                        if (dsMatDocType != null)
                                        {
                                            if (dsMatDocType.Tables[0].Rows.Count > 0)
                                            {
                                                if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["AllowedFromStock"]).Trim() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.StockIndicator = Convert.ToString(objAsgTechCallDetail.StockIndicator);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.StockIndicator =  "";
                                                }

                                                if (Convert.ToString(dsMatDocType.Tables[0].Rows[0]["AllowedToStock"]).Trim() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.ToStockIndicator = Convert.ToString(objAsgTechCallDetail.ToStockIndicator);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.ToStockIndicator = "";

                                                }

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
                                                    objPartnerGoodsMovSerialnew.PartnerCode = Convert.ToString(objAsgTechCallDetail.PartnerCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.PartnerCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["FromStore"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.StoreCode = Convert.ToString(objAsgTechCallDetail.StoreCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.StoreCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["FromEmployee"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.PartnerEmployeeCode = Convert.ToString(objAsgTechCallDetail.FromPartnerEmployeeCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.PartnerEmployeeCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["ToPartner"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.ToPartnerCode = Convert.ToString(objAsgTechCallDetail.ToPartnerCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.ToPartnerCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["ToStore"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.ToStoreCode = Convert.ToString(objAsgTechCallDetail.ToStoreCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.ToStoreCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["ToEmployee"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = Convert.ToString(objAsgTechCallDetail.ToPartnerEmployeeCode);
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
                                                    objPartnerGoodsMovSerialnew.ToMaterialCode = Convert.ToString(objAsgTechCallDetail.ToMaterialCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.ToMaterialCode = "";
                                                }


                                            }
                                            else
                                            {
                                                objPartnerGoodsMovSerialnew.PlantCode = "";
                                                objPartnerGoodsMovSerialnew.PartnerCode = Convert.ToString(objAsgTechCallDetail.PartnerCode);
                                                objPartnerGoodsMovSerialnew.PartnerEmployeeCode = "";
                                                objPartnerGoodsMovSerialnew.StoreCode = Convert.ToString(objAsgTechCallDetail.StoreCode);
                                                objPartnerGoodsMovSerialnew.ToPartnerCode = Convert.ToString(objAsgTechCallDetail.ToPartnerCode);
                                                objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = Convert.ToString(objAsgTechCallDetail.ToPartnerEmployeeCode);
                                                objPartnerGoodsMovSerialnew.ToPlantCode = "";
                                                objPartnerGoodsMovSerialnew.ToStoreCode = Convert.ToString(objAsgTechCallDetail.ToStoreCode);
                                                objPartnerGoodsMovSerialnew.ToMaterialCode = Convert.ToString(objAsgTechCallDetail.ToMaterialCode);
                                            }
                                        }


                                        objPartnerGMSerialCol.colPartnerGMSerialDetail.Add(objPartnerGoodsMovSerialnew);

                                        /*----------------------------------------------------------------------------------------------------------*/
                                    }

                                }

                                
                            }
                            /*-----------------------------------------------------------------------------------------------------------*/
                            

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

        public string InsertAsgTechnicianCallMaster(AsgTechnicianCallMaster argAsgTechnicianCallMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];

            param[0] = new SqlParameter("@AsgTechCallCode", argAsgTechnicianCallMaster.AsgTechCallCode);
            param[1] = new SqlParameter("@AsgTechDocTypeCode", argAsgTechnicianCallMaster.AsgTechDocTypeCode);
            param[2] = new SqlParameter("@PartnerEmployeeCode", argAsgTechnicianCallMaster.PartnerEmployeeCode);
            param[3] = new SqlParameter("@AssignDate", argAsgTechnicianCallMaster.AssignDate);
            param[4] = new SqlParameter("@PartnerCode", argAsgTechnicianCallMaster.PartnerCode);
            param[5] = new SqlParameter("@ClientCode", argAsgTechnicianCallMaster.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argAsgTechnicianCallMaster.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argAsgTechnicianCallMaster.ModifiedBy);
            
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertAsgTechnicianCallMaster", param);
            
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

            return strRetValue;
        }
        
        public string UpdateAsgTechnicianCallMaster(AsgTechnicianCallMaster argAsgTechnicianCallMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@AsgTechCallCode", argAsgTechnicianCallMaster.AsgTechCallCode);
            param[1] = new SqlParameter("@AsgTechDocTypeCode", argAsgTechnicianCallMaster.AsgTechDocTypeCode);
            param[2] = new SqlParameter("@PartnerEmployeeCode", argAsgTechnicianCallMaster.PartnerEmployeeCode);
            param[3] = new SqlParameter("@AssignDate", argAsgTechnicianCallMaster.AssignDate);
            param[4] = new SqlParameter("@PartnerCode", argAsgTechnicianCallMaster.PartnerCode);
            param[5] = new SqlParameter("@ClientCode", argAsgTechnicianCallMaster.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argAsgTechnicianCallMaster.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argAsgTechnicianCallMaster.ModifiedBy);
          
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateAsgTechnicianCallMaster", param);


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

            return strRetValue;

        }
        
        public ICollection<ErrorHandler> DeleteAsgTechnicianCallMaster(string argAsgTechCallCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@AsgTechCallCode", argAsgTechCallCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);

                param[2] = new SqlParameter("@Type", SqlDbType.Char);
                param[2].Size = 1;
                param[2].Direction = ParameterDirection.Output;
                param[3] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[3].Size = 255;
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[4].Size = 20;
                param[4].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteAsgTechnicianCallMaster", param);


                string strMessage = Convert.ToString(param[3].Value);
                string strType = Convert.ToString(param[2].Value);
                string strRetValue = Convert.ToString(param[4].Value);


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

        public bool blnIsAsgTechnicianCallMasterExists(string argAsgTechCallCode, string argPartnerCode, string argClientCode)
        {
            bool IsAsgTechnicianCallMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetAsgTechnicianCallMaster(argAsgTechCallCode, argPartnerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAsgTechnicianCallMasterExists = true;
            }
            else
            {
                IsAsgTechnicianCallMasterExists = false;
            }
            return IsAsgTechnicianCallMasterExists;
        }

        public bool blnIsAsgTechnicianCallMasterExists(string argAsgTechCallCode, string argPartnerCode, string argClientCode, DataAccess da)
        {
            bool IsAsgTechnicianCallMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetAsgTechnicianCallMaster(argAsgTechCallCode, argPartnerCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAsgTechnicianCallMasterExists = true;
            }
            else
            {
                IsAsgTechnicianCallMasterExists = false;
            }
            return IsAsgTechnicianCallMasterExists;
        }
        
        // -------------------------------------For Report------------------------------------------------------------------//

        public DataSet GetAsgTechCallMaster4Report(string argAsgTechDocTypeCode, string argPartnerCode, string argPartnerEmployeeCode, string argFromDate, string argToDate, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@AsgTechDocTypeCode", argAsgTechDocTypeCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@FromPartnerEmployeeCode", argPartnerEmployeeCode);
            param[3] = new SqlParameter("@FromDate", argFromDate);
            param[4] = new SqlParameter("@ToDate", argToDate);
            param[5] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetCallAssignToTech4Report", param);
            return DataSetToFill;
        }
        
        public DataSet GetAsgTechCallMaster4Print(string argAsgTechDocTypeCode, string argAsgTechCallCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@AsgTechDocTypeCode", argAsgTechDocTypeCode);
            param[1] = new SqlParameter("@AsgTechCallCode", argAsgTechCallCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);


            DataSetToFill = da.FillDataSet("RP_GetCallAssignToTech4Print", param);
            return DataSetToFill;
        }
        
        //--------------------------------------End Report---------------------------------------------------------------------//


    }
}