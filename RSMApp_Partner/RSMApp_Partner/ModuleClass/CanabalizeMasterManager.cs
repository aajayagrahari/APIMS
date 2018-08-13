
//Created On :: 09, January, 2013
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CanabalizeMasterManager
    {
        const string CanabalizeMasterTable = "CanabalizeMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        CanabalizeDetailsManager objCanabalizeDetailsManager = new CanabalizeDetailsManager();
        SerializeStockMissingPartsManager objSerializeStockMissingPartsManager = new SerializeStockMissingPartsManager();

        public CanabalizeMaster objGetCanabalizeMaster(string argCanabalizeDocCode, string argParnerCode, string argClientCode)
        {
            CanabalizeMaster argCanabalizeMaster = new CanabalizeMaster();
            DataSet DataSetToFill = new DataSet();

            if (argCanabalizeDocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argParnerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCanabalizeMaster(argCanabalizeDocCode, argParnerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCanabalizeMaster = this.objCreateCanabalizeMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCanabalizeMaster;
        }
        
        public ICollection<CanabalizeMaster> colGetCanabalizeMaster(string argPartnerCode, string argClientCode)
        {
            List<CanabalizeMaster> lst = new List<CanabalizeMaster>();
            DataSet DataSetToFill = new DataSet();
            CanabalizeMaster tCanabalizeMaster = new CanabalizeMaster();

            DataSetToFill = this.GetCanabalizeMaster(argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCanabalizeMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetCanabalizeMaster(string argCanabalizeDocCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CanabalizeDocCode", argCanabalizeDocCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCanabalizeMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCanabalizeMaster(string argCanabalizeDocCode, string argPartnerCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CanabalizeDocCode", argCanabalizeDocCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCanabalizeMaster4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCanabalizeMaster(string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];            
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCanabalizeMaster", param);
            return DataSetToFill;
        }
        
        private CanabalizeMaster objCreateCanabalizeMaster(DataRow dr)
        {
            CanabalizeMaster tCanabalizeMaster = new CanabalizeMaster();

            tCanabalizeMaster.SetObjectInfo(dr);

            return tCanabalizeMaster;
        }
        
        public ICollection<ErrorHandler> SaveCanabalizeMaster(CanabalizeMaster argCanabalizeMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCanabalizeMasterExists(argCanabalizeMaster.CanabalizeDocCode, argCanabalizeMaster.PartnerCode, argCanabalizeMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCanabalizeMaster(argCanabalizeMaster, da, lstErr);
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
                    UpdateCanabalizeMaster(argCanabalizeMaster, da, lstErr);
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

        public PartnerErrorResult SaveCanabalizeMaster(CanabalizeMaster argCanabalizeMaster, CanabalizeDetailsCol argCanabalizeDetailsCol, SerializeStockMissingPartsCol argSerializeStockMissingPartsCol)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            PartnerErrorResult errorcol = new PartnerErrorResult();
            PartnerGoodsMovementManager objPartnerGMManager = new PartnerGoodsMovementManager();
            PartnerMaterialDocTypeManager objPartnerMatDocTypeMan = new PartnerMaterialDocTypeManager();

            DataAccess da = new DataAccess();

            string strretValue = "";
            string strProductGoodsMovCode = "";
            string strSpareGoodsMovCode = "";

            DataSet dsMatDocType = null;

            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();
                if (blnIsCanabalizeMasterExists(argCanabalizeMaster.CanabalizeDocCode, argCanabalizeMaster.PartnerCode, argCanabalizeMaster.ClientCode, da) == false)
                {
                    if (argCanabalizeMaster.PGoodsMovementCode == "NEW")
                    {
                        if (argCanabalizeMaster.MaterialDocTypeCode != "")
                        {
                            strProductGoodsMovCode = objPartnerGMManager.GenerateGMCode("NEW", "GM01", argCanabalizeMaster.PartnerCode, argCanabalizeMaster.ClientCode, da);
                            argCanabalizeMaster.PGoodsMovementCode = strProductGoodsMovCode;
                            argCanabalizeMaster.GMItemNo = 1;
                        }
                        else
                        {
                            argCanabalizeMaster.PGoodsMovementCode = "";
                            argCanabalizeMaster.GMItemNo = 0;
                        }
                    }
                   

                    strretValue = InsertCanabalizeMaster(argCanabalizeMaster, da, lstErr);
                  
                }
                else
                {
                    strretValue = UpdateCanabalizeMaster(argCanabalizeMaster, da, lstErr);
                  
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

                    if (argSerializeStockMissingPartsCol.colSerializeStockMissingParts.Count > 0)
                    {

                        foreach (SerializeStockMissingParts objSerializeStockMissingParts in argSerializeStockMissingPartsCol.colSerializeStockMissingParts)
                        {
                            if (Convert.ToString(objSerializeStockMissingParts.SerialNo).Trim() == Convert.ToString(argCanabalizeMaster.SerialNo).Trim())
                            {
                                objSerializeStockMissingPartsManager.SaveSerializeStockMissingParts(objSerializeStockMissingParts, da, lstErr);
                            }
                        }

                    }



                    if (argCanabalizeDetailsCol.colCanabalizeDetails.Count > 0)
                    {
                        foreach (CanabalizeDetails objCanabalizeDetails in argCanabalizeDetailsCol.colCanabalizeDetails)
                        {
                            if (objCanabalizeDetails.IsDeleted == 0)
                            {
                                if (objCanabalizeDetails.PGoodsMovementCode == "NEW")
                                {
                                    strSpareGoodsMovCode = objPartnerGMManager.GenerateGMCode("NEW", "GM01", argCanabalizeMaster.PartnerCode, argCanabalizeMaster.ClientCode, da);
                                    objCanabalizeDetails.PGoodsMovementCode = strSpareGoodsMovCode;
                                    objCanabalizeDetails.GMItemNo = objCanabalizeDetails.CanabalizeItemNo;
                                }

                                objCanabalizeDetails.CanabalizeDocCode = strretValue;
                                objCanabalizeDetailsManager.SaveCanabalizeDetails(objCanabalizeDetails, da, lstErr);
                            }
                            else
                            {
                                /***************************/

                                /*** Delete Fucntion ***/

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


                    /* Partner Goods Movement Declared for Spares */
                    if (argCanabalizeDetailsCol.colCanabalizeDetails.Count > 0)
                    {
                        PartnerGoodsMovement objPartnerSpareGM = new PartnerGoodsMovement();
                        objPartnerSpareGM.PGoodsMovementCode = strSpareGoodsMovCode;
                        objPartnerSpareGM.PartnerGMDocTypeCode = "GM01";
                        objPartnerSpareGM.FromPlantCode = "";
                        objPartnerSpareGM.FromPartnerCode = Convert.ToString(argCanabalizeMaster.PartnerCode);
                        objPartnerSpareGM.FromPartnerEmployeeCode = "";
                        objPartnerSpareGM.FromStoreCode = "";
                        objPartnerSpareGM.ToPartnerCode = Convert.ToString(argCanabalizeMaster.PartnerCode);
                        objPartnerSpareGM.ToPlantCode = "";
                        objPartnerSpareGM.ToStoreCode = "";
                        objPartnerSpareGM.ToPartnerEmployeeCode = "";
                        objPartnerSpareGM.ClientCode = argCanabalizeMaster.ClientCode;
                        objPartnerSpareGM.CreatedBy = argCanabalizeMaster.CreatedBy;
                        objPartnerSpareGM.ModifiedBy = argCanabalizeMaster.ModifiedBy;
                        objPartnerSpareGM.TotalQuantity = 0;
                        objPartnerSpareGM.PartnerCode = Convert.ToString(argCanabalizeMaster.PartnerCode);
                        objPartnerSpareGM.GoodsMovDate = Convert.ToDateTime(argCanabalizeMaster.CanablizeDate);

                        /*----------------------------------------------------------------------------------------*/
                        /* Partner Goods Movement Detail */
                        PartnerGoodsMovementDetailCol objPartnerSpareGMCol = new PartnerGoodsMovementDetailCol();
                        PartnerGoodsMovSerialDetailCol objPartnerSpareGMSerialCol = new PartnerGoodsMovSerialDetailCol();

                        objPartnerSpareGMCol.colPartnerGMDetail = new List<PartnerGoodsMovementDetail>();
                        objPartnerSpareGMSerialCol.colPartnerGMSerialDetail = new List<PartnerGoodsMovSerialDetail>();
                        bool IsNew = true;
                        int iCtr = 0;
                        int tmpItemNo = 0;
                        foreach (CanabalizeDetails objCanabalizeDetails in argCanabalizeDetailsCol.colCanabalizeDetails)
                        {
                            if (objCanabalizeDetails.IsDeleted == 0)
                            {
                                dsMatDocType = new DataSet();
                                iCtr = iCtr + 1;
                                tmpItemNo = iCtr;
                                if (IsNew == true)
                                {
                                    dsMatDocType = objPartnerMatDocTypeMan.GetPartnerMaterialDocType(objCanabalizeDetails.MaterialDocTypeCode, objCanabalizeDetails.ClientCode, da);
                                                                        
                                    PartnerGoodsMovementDetail objPartnerGMDetailsnew = new PartnerGoodsMovementDetail();
                                    objPartnerGMDetailsnew.PGoodsMovementCode = strSpareGoodsMovCode;
                                    objPartnerGMDetailsnew.ItemNo = tmpItemNo;
                                    objPartnerGMDetailsnew.MaterialCode = Convert.ToString(objCanabalizeDetails.MaterialCode);
                                    objPartnerGMDetailsnew.MatGroup1Code = Convert.ToString(objCanabalizeDetails.MatGroup1Code);
                                    objPartnerGMDetailsnew.StockIndicator = Convert.ToString(objCanabalizeDetails.StockIndicator);
                                    objPartnerGMDetailsnew.ToStockIndicator = Convert.ToString(objCanabalizeDetails.ToStockIndicator);
                                    objPartnerGMDetailsnew.Quantity = Convert.ToInt32(objCanabalizeDetails.Quantity);
                                    objPartnerGMDetailsnew.UOMCode = Convert.ToString(objCanabalizeDetails.UOMCode);
                                    objPartnerGMDetailsnew.ClientCode = Convert.ToString(objCanabalizeDetails.ClientCode);
                                    objPartnerGMDetailsnew.CreatedBy = Convert.ToString(objCanabalizeDetails.CreatedBy);
                                    objPartnerGMDetailsnew.ModifiedBy = Convert.ToString(objCanabalizeDetails.ModifiedBy);
                                    objPartnerGMDetailsnew.TranRefDocCode = Convert.ToString(strretValue);
                                    objPartnerGMDetailsnew.TranRefDocItemNo = Convert.ToInt32(objCanabalizeDetails.CanabalizeItemNo);
                                    objPartnerGMDetailsnew.MaterialDocTypeCode = Convert.ToString(objCanabalizeDetails.MaterialDocTypeCode);
                                    objPartnerGMDetailsnew.PartnerCode = Convert.ToString(objCanabalizeDetails.PartnerCode);

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
                                                objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(objCanabalizeDetails.PartnerCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.FromPartnerCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["FromStore"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(objCanabalizeDetails.StoreCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.FromStoreCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["FromEmployee"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.FromPartnerEmployeeCode = Convert.ToString(objCanabalizeDetails.PartnerEmployeeCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.FromPartnerEmployeeCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["ToPartner"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(objCanabalizeDetails.PartnerCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.ToPartnerCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["ToStore"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(objCanabalizeDetails.ToStoreCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.ToStoreCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["ToEmployee"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString(objCanabalizeDetails.ToPartnerEmployeeCode);
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
                                                objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(objCanabalizeDetails.MaterialCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.ToMaterialCode = "";
                                            }
                                        }
                                        else
                                        {
                                            objPartnerGMDetailsnew.FromPlantCode = "";
                                            objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(objCanabalizeDetails.PartnerCode);
                                            objPartnerGMDetailsnew.FromPartnerEmployeeCode = Convert.ToString(objCanabalizeDetails.PartnerEmployeeCode);
                                            objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(objCanabalizeDetails.StoreCode);
                                            objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(objCanabalizeDetails.PartnerCode);
                                            objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString(objCanabalizeDetails.ToPartnerEmployeeCode);
                                            objPartnerGMDetailsnew.ToPlantCode = "";
                                            objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(objCanabalizeDetails.ToStoreCode);
                                            objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(objCanabalizeDetails.MaterialCode);
                                        }
                                    }

                                    objPartnerSpareGMCol.colPartnerGMDetail.Add(objPartnerGMDetailsnew);
                                    /*Partner Goods Movement Serial Detail*/
                                    if (objCanabalizeDetails.SerialNo1 != "")
                                    {
                                        /*Partner Goods Movement Serial Detail*/

                                        PartnerGoodsMovSerialDetail objPartnerGoodsMovSerialnew = new PartnerGoodsMovSerialDetail();
                                        objPartnerGoodsMovSerialnew.PGoodsMovementCode = strSpareGoodsMovCode;
                                        objPartnerGoodsMovSerialnew.ItemNo = tmpItemNo;
                                        objPartnerGoodsMovSerialnew.SerialNo1 = Convert.ToString(objCanabalizeDetails.SerialNo1);
                                        objPartnerGoodsMovSerialnew.SerialNo2 = Convert.ToString("");
                                        objPartnerGoodsMovSerialnew.MaterialCode = Convert.ToString(objCanabalizeDetails.MaterialCode);
                                        objPartnerGoodsMovSerialnew.MatGroup1Code = Convert.ToString(objCanabalizeDetails.MatGroup1Code);
                                        objPartnerGoodsMovSerialnew.RefDocCode = Convert.ToString(objCanabalizeDetails.RefDocCode);
                                        objPartnerGoodsMovSerialnew.RefDocItemNo = Convert.ToInt32(objCanabalizeDetails.RefDocItemNo);
                                        objPartnerGoodsMovSerialnew.RefDocType = Convert.ToString(objCanabalizeDetails.RefDocTypeCode);
                                        objPartnerGoodsMovSerialnew.TranRefDocCode = Convert.ToString(strretValue);
                                        objPartnerGoodsMovSerialnew.TranRefDocItemNo = Convert.ToInt32(objCanabalizeDetails.CanabalizeItemNo);
                                        objPartnerGoodsMovSerialnew.IsDeleted = 0;
                                        objPartnerGoodsMovSerialnew.ClientCode = Convert.ToString(objCanabalizeDetails.ClientCode);
                                        objPartnerGoodsMovSerialnew.CreatedBy = Convert.ToString(objCanabalizeDetails.CreatedBy);
                                        objPartnerGoodsMovSerialnew.ModifiedBy = Convert.ToString(objCanabalizeDetails.ModifiedBy);
                                        objPartnerGoodsMovSerialnew.StockIndicator = Convert.ToString(objCanabalizeDetails.StockIndicator);
                                        objPartnerGoodsMovSerialnew.ToStockIndicator = Convert.ToString(objCanabalizeDetails.ToStockIndicator);
                        

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
                                                    objPartnerGoodsMovSerialnew.PartnerCode = Convert.ToString(objCanabalizeDetails.PartnerCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.PartnerCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["FromStore"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.StoreCode = Convert.ToString(objCanabalizeDetails.StoreCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.StoreCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["FromEmployee"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.PartnerEmployeeCode = Convert.ToString(objCanabalizeDetails.PartnerEmployeeCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.PartnerEmployeeCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["ToPartner"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.ToPartnerCode = Convert.ToString(objCanabalizeDetails.PartnerCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.ToPartnerCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["ToStore"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.ToStoreCode = Convert.ToString(objCanabalizeDetails.ToStoreCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.ToStoreCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["ToEmployee"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = Convert.ToString(objCanabalizeDetails.ToPartnerEmployeeCode);
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
                                                    objPartnerGoodsMovSerialnew.ToMaterialCode = Convert.ToString(objCanabalizeDetails.MaterialCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.ToMaterialCode = "";
                                                }


                                            }
                                            else
                                            {
                                                //objPartnerGoodsMovSerialnew.FromPlantCode = "";
                                                //objPartnerGoodsMovSerialnew.FromPartnerCode = Convert.ToString(objCanabalizeDetails.PartnerCode);
                                                //objPartnerGoodsMovSerialnew.FromPartnerEmployeeCode = Convert.ToString(objCanabalizeDetails.PartnerEmployeeCode);
                                                //objPartnerGoodsMovSerialnew.FromStoreCode = Convert.ToString(objCanabalizeDetails.StoreCode);
                                                //objPartnerGoodsMovSerialnew.ToPartnerCode = Convert.ToString(objCanabalizeDetails.PartnerCode);
                                                //objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = Convert.ToString(objCanabalizeDetails.ToPartnerEmployeeCode);
                                                //objPartnerGoodsMovSerialnew.ToPlantCode = "";
                                                //objPartnerGoodsMovSerialnew.ToStoreCode = Convert.ToString(objCanabalizeDetails.ToStoreCode);
                                                //objPartnerGoodsMovSerialnew.ToMaterialCode = Convert.ToString(objCanabalizeDetails.MaterialCode);

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
                    /*-------------------------------------------------------------------------------------------------------*/
                    if (argCanabalizeMaster.IsPartialCanibalize == 0)
                    {
                        /* Partner Goods Movement Declared for Product */

                        PartnerGoodsMovement objPartnerProductGM = new PartnerGoodsMovement();

                        objPartnerProductGM.PGoodsMovementCode = strProductGoodsMovCode;
                        objPartnerProductGM.PartnerGMDocTypeCode = "GM01";
                        objPartnerProductGM.FromPlantCode = "";
                        objPartnerProductGM.FromPartnerCode = Convert.ToString(argCanabalizeMaster.PartnerCode);
                        objPartnerProductGM.FromPartnerEmployeeCode = Convert.ToString(argCanabalizeMaster.PartnerEmployeeCode);
                        objPartnerProductGM.FromStoreCode = "";
                        objPartnerProductGM.ToPartnerCode = Convert.ToString(argCanabalizeMaster.PartnerCode);
                        objPartnerProductGM.ToPlantCode = "";
                        objPartnerProductGM.ToStoreCode = "";
                        objPartnerProductGM.ToPartnerEmployeeCode = Convert.ToString(argCanabalizeMaster.ToPartnerEmployeeCode);
                        objPartnerProductGM.ClientCode = argCanabalizeMaster.ClientCode;
                        objPartnerProductGM.CreatedBy = argCanabalizeMaster.CreatedBy;
                        objPartnerProductGM.ModifiedBy = argCanabalizeMaster.ModifiedBy;
                        objPartnerProductGM.TotalQuantity = 0;
                        objPartnerProductGM.PartnerCode = Convert.ToString(argCanabalizeMaster.PartnerCode);
                        objPartnerProductGM.GoodsMovDate = Convert.ToDateTime(DateTime.Now);

                        /*----------------------------------------------------------------------------------------*/
                        /* Partner Goods Movement Detail */

                        PartnerGoodsMovementDetailCol objPartnerProductGMCol = new PartnerGoodsMovementDetailCol();
                        PartnerGoodsMovSerialDetailCol objPartnerProductGMSerialCol = new PartnerGoodsMovSerialDetailCol();

                        objPartnerProductGMCol.colPartnerGMDetail = new List<PartnerGoodsMovementDetail>();
                        objPartnerProductGMSerialCol.colPartnerGMSerialDetail = new List<PartnerGoodsMovSerialDetail>();
                        dsMatDocType = new DataSet();
                        int tmpItemNo = 1;
                        dsMatDocType = objPartnerMatDocTypeMan.GetPartnerMaterialDocType(argCanabalizeMaster.MaterialDocTypeCode, argCanabalizeMaster.ClientCode, da);

                        PartnerGoodsMovementDetail objPartnerGMDetailsnew = new PartnerGoodsMovementDetail();
                        objPartnerGMDetailsnew.PGoodsMovementCode = strProductGoodsMovCode;
                        objPartnerGMDetailsnew.ItemNo = tmpItemNo;
                        objPartnerGMDetailsnew.MaterialCode = Convert.ToString(argCanabalizeMaster.MaterialCode);
                        objPartnerGMDetailsnew.MatGroup1Code = Convert.ToString(argCanabalizeMaster.MatGroup1Code);
                        objPartnerGMDetailsnew.StockIndicator = Convert.ToString(argCanabalizeMaster.StockIndicator);
                        objPartnerGMDetailsnew.ToStockIndicator = Convert.ToString(argCanabalizeMaster.ToStockIndicator);
                        objPartnerGMDetailsnew.Quantity = Convert.ToInt32("1");
                        objPartnerGMDetailsnew.UOMCode = Convert.ToString("EA");
                        objPartnerGMDetailsnew.ClientCode = Convert.ToString(argCanabalizeMaster.ClientCode);
                        objPartnerGMDetailsnew.CreatedBy = Convert.ToString(argCanabalizeMaster.CreatedBy);
                        objPartnerGMDetailsnew.ModifiedBy = Convert.ToString(argCanabalizeMaster.ModifiedBy);
                        objPartnerGMDetailsnew.TranRefDocCode = Convert.ToString(strretValue);
                        objPartnerGMDetailsnew.TranRefDocItemNo = Convert.ToInt32("1");
                        objPartnerGMDetailsnew.MaterialDocTypeCode = Convert.ToString(argCanabalizeMaster.MaterialDocTypeCode);
                        objPartnerGMDetailsnew.PartnerCode = Convert.ToString(argCanabalizeMaster.PartnerCode);

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
                                    objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(argCanabalizeMaster.PartnerCode);
                                }
                                else
                                {
                                    objPartnerGMDetailsnew.FromPartnerCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["FromStore"].ToString() != "HIDE")
                                {
                                    objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(argCanabalizeMaster.StoreCode);
                                }
                                else
                                {
                                    objPartnerGMDetailsnew.FromStoreCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["FromEmployee"].ToString() != "HIDE")
                                {
                                    objPartnerGMDetailsnew.FromPartnerEmployeeCode = Convert.ToString(argCanabalizeMaster.PartnerEmployeeCode);
                                }
                                else
                                {
                                    objPartnerGMDetailsnew.FromPartnerEmployeeCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToPartner"].ToString() != "HIDE")
                                {
                                    objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(argCanabalizeMaster.PartnerCode);
                                }
                                else
                                {
                                    objPartnerGMDetailsnew.ToPartnerCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToStore"].ToString() != "HIDE")
                                {
                                    objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(argCanabalizeMaster.ToStoreCode);
                                }
                                else
                                {
                                    objPartnerGMDetailsnew.ToStoreCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToEmployee"].ToString() != "HIDE")
                                {
                                    objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString(argCanabalizeMaster.ToPartnerEmployeeCode);
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
                                    objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(argCanabalizeMaster.MaterialCode);
                                }
                                else
                                {
                                    objPartnerGMDetailsnew.ToMaterialCode = "";
                                }


                            }
                            else
                            {
                                objPartnerGMDetailsnew.FromPlantCode = "";
                                objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(argCanabalizeMaster.PartnerCode);
                                objPartnerGMDetailsnew.FromPartnerEmployeeCode = Convert.ToString(argCanabalizeMaster.PartnerEmployeeCode);
                                objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(argCanabalizeMaster.StoreCode);
                                objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(argCanabalizeMaster.PartnerCode);
                                objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString(argCanabalizeMaster.ToPartnerEmployeeCode);
                                objPartnerGMDetailsnew.ToPlantCode = "";
                                objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(argCanabalizeMaster.ToStoreCode);
                                objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(argCanabalizeMaster.MaterialCode);

                            }
                        }

                        objPartnerProductGMCol.colPartnerGMDetail.Add(objPartnerGMDetailsnew);

                        /*******************************************************************/
                        PartnerGoodsMovSerialDetail objPartnerGoodsMovSerialnew = new PartnerGoodsMovSerialDetail();
                        objPartnerGoodsMovSerialnew.PGoodsMovementCode = strProductGoodsMovCode;
                        objPartnerGoodsMovSerialnew.ItemNo = tmpItemNo;
                        objPartnerGoodsMovSerialnew.SerialNo1 = Convert.ToString(argCanabalizeMaster.SerialNo);
                        objPartnerGoodsMovSerialnew.SerialNo2 = "";
                        objPartnerGoodsMovSerialnew.MaterialCode = Convert.ToString(argCanabalizeMaster.MaterialCode);
                        objPartnerGoodsMovSerialnew.MatGroup1Code = Convert.ToString(argCanabalizeMaster.MatGroup1Code);
                        objPartnerGoodsMovSerialnew.RefDocCode = Convert.ToString(argCanabalizeMaster.RefDocCode);
                        objPartnerGoodsMovSerialnew.RefDocItemNo = Convert.ToInt32(argCanabalizeMaster.RefDocItemNo);
                        objPartnerGoodsMovSerialnew.RefDocType = Convert.ToString(argCanabalizeMaster.RefDocType);
                        objPartnerGoodsMovSerialnew.IsDeleted = 0;
                        objPartnerGoodsMovSerialnew.TranRefDocCode = Convert.ToString(strretValue);
                        objPartnerGoodsMovSerialnew.TranRefDocItemNo = Convert.ToInt32("1");
                        objPartnerGoodsMovSerialnew.ClientCode = Convert.ToString(argCanabalizeMaster.ClientCode);
                        objPartnerGoodsMovSerialnew.CreatedBy = Convert.ToString(argCanabalizeMaster.CreatedBy);
                        objPartnerGoodsMovSerialnew.ModifiedBy = Convert.ToString(argCanabalizeMaster.ModifiedBy);
                        objPartnerGoodsMovSerialnew.StockIndicator = Convert.ToString(argCanabalizeMaster.StockIndicator);
                        objPartnerGoodsMovSerialnew.ToStockIndicator = Convert.ToString(argCanabalizeMaster.ToStockIndicator);
                        

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
                                    objPartnerGoodsMovSerialnew.PartnerCode = Convert.ToString(argCanabalizeMaster.PartnerCode);
                                }
                                else
                                {
                                    objPartnerGoodsMovSerialnew.PartnerCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["FromStore"].ToString() != "HIDE")
                                {
                                    objPartnerGoodsMovSerialnew.StoreCode = Convert.ToString(argCanabalizeMaster.StoreCode);
                                }
                                else
                                {
                                    objPartnerGoodsMovSerialnew.StoreCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["FromEmployee"].ToString() != "HIDE")
                                {
                                    objPartnerGoodsMovSerialnew.PartnerEmployeeCode = Convert.ToString(argCanabalizeMaster.PartnerEmployeeCode   );
                                }
                                else
                                {
                                    objPartnerGoodsMovSerialnew.PartnerEmployeeCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToPartner"].ToString() != "HIDE")
                                {
                                    objPartnerGoodsMovSerialnew.ToPartnerCode = Convert.ToString(argCanabalizeMaster.PartnerCode);
                                }
                                else
                                {
                                    objPartnerGoodsMovSerialnew.ToPartnerCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToStore"].ToString() != "HIDE")
                                {
                                    objPartnerGoodsMovSerialnew.ToStoreCode = Convert.ToString(argCanabalizeMaster.ToStoreCode);
                                }
                                else
                                {
                                    objPartnerGoodsMovSerialnew.ToStoreCode = "";
                                }

                                if (dsMatDocType.Tables[0].Rows[0]["ToEmployee"].ToString() != "HIDE")
                                {
                                    objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = Convert.ToString(argCanabalizeMaster.ToPartnerEmployeeCode);
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
                                    objPartnerGoodsMovSerialnew.ToMaterialCode = Convert.ToString(argCanabalizeMaster.MaterialCode);
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

                        if (argCanabalizeMaster.PGoodsMovementCode != "")
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
        
        public string InsertCanabalizeMaster(CanabalizeMaster argCanabalizeMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[28];

            param[0] = new SqlParameter("@CanabalizeDocCode", argCanabalizeMaster.CanabalizeDocCode);
            param[1] = new SqlParameter("@CanablizeDocTypeCode", argCanabalizeMaster.CanablizeDocTypeCode);
            param[2] = new SqlParameter("@CanablizeDate", argCanabalizeMaster.CanablizeDate);
            param[3] = new SqlParameter("@SerialNo", argCanabalizeMaster.SerialNo);
            param[4] = new SqlParameter("@MaterialCode", argCanabalizeMaster.MaterialCode);
            param[5] = new SqlParameter("@MatGroup1Code", argCanabalizeMaster.MatGroup1Code);
            param[6] = new SqlParameter("@PartnerCode", argCanabalizeMaster.PartnerCode);
            param[7] = new SqlParameter("@StoreCode", argCanabalizeMaster.StoreCode);
            param[8] = new SqlParameter("@StockIndicator", argCanabalizeMaster.StockIndicator);
            param[9] = new SqlParameter("@PartnerEmployeeCode", argCanabalizeMaster.PartnerEmployeeCode);
            param[10] = new SqlParameter("@ToPartnerCode", argCanabalizeMaster.ToPartnerCode);
            param[11] = new SqlParameter("@ToStoreCode", argCanabalizeMaster.ToStoreCode);
            param[12] = new SqlParameter("@ToStockIndicator", argCanabalizeMaster.ToStockIndicator);
            param[13] = new SqlParameter("@ToPartnerEmployeeCode", argCanabalizeMaster.ToPartnerEmployeeCode);
            param[14] = new SqlParameter("@ToMaterialCode", argCanabalizeMaster.ToMaterialCode);
            param[15] = new SqlParameter("@RefDocCode", argCanabalizeMaster.RefDocCode);
            param[16] = new SqlParameter("@RefDocItemNo", argCanabalizeMaster.RefDocItemNo);
            param[17] = new SqlParameter("@RefDocType", argCanabalizeMaster.RefDocType);
            param[18] = new SqlParameter("@MaterialDocTypeCode", argCanabalizeMaster.MaterialDocTypeCode);
            param[19] = new SqlParameter("@PGoodsMovementCode", argCanabalizeMaster.PGoodsMovementCode);
            param[20] = new SqlParameter("@GMItemNo", argCanabalizeMaster.GMItemNo);
            param[21] = new SqlParameter("@IsPartialCanibalize", argCanabalizeMaster.IsPartialCanibalize);
            param[22] = new SqlParameter("@ClientCode", argCanabalizeMaster.ClientCode);
            param[23] = new SqlParameter("@CreatedBy", argCanabalizeMaster.CreatedBy);
            param[24] = new SqlParameter("@ModifiedBy", argCanabalizeMaster.ModifiedBy);
         
            param[25] = new SqlParameter("@Type", SqlDbType.Char);
            param[25].Size = 1;
            param[25].Direction = ParameterDirection.Output;

            param[26] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[26].Size = 255;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[27].Size = 20;
            param[27].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCanabalizeMaster", param);


            string strMessage = Convert.ToString(param[26].Value);
            string strType = Convert.ToString(param[25].Value);
            string strRetValue = Convert.ToString(param[27].Value);


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
        
        public string UpdateCanabalizeMaster(CanabalizeMaster argCanabalizeMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[28];

            param[0] = new SqlParameter("@CanabalizeDocCode", argCanabalizeMaster.CanabalizeDocCode);
            param[1] = new SqlParameter("@CanablizeDocTypeCode", argCanabalizeMaster.CanablizeDocTypeCode);
            param[2] = new SqlParameter("@CanablizeDate", argCanabalizeMaster.CanablizeDate);
            param[3] = new SqlParameter("@SerialNo", argCanabalizeMaster.SerialNo);
            param[4] = new SqlParameter("@MaterialCode", argCanabalizeMaster.MaterialCode);
            param[5] = new SqlParameter("@MatGroup1Code", argCanabalizeMaster.MatGroup1Code);
            param[6] = new SqlParameter("@ParnerCode", argCanabalizeMaster.PartnerCode);
            param[7] = new SqlParameter("@StoreCode", argCanabalizeMaster.StoreCode);
            param[8] = new SqlParameter("@StockIndicator", argCanabalizeMaster.StockIndicator);
            param[9] = new SqlParameter("@PartnerEmployeeCode", argCanabalizeMaster.PartnerEmployeeCode);
            param[10] = new SqlParameter("@ToPartnerCode", argCanabalizeMaster.ToPartnerCode);
            param[11] = new SqlParameter("@ToStoreCode", argCanabalizeMaster.ToStoreCode);
            param[12] = new SqlParameter("@ToStockIndicator", argCanabalizeMaster.ToStockIndicator);
            param[13] = new SqlParameter("@ToPartnerEmployeeCode", argCanabalizeMaster.ToPartnerEmployeeCode);
            param[14] = new SqlParameter("@ToMaterialCode", argCanabalizeMaster.ToMaterialCode);
            param[15] = new SqlParameter("@RefDocCode", argCanabalizeMaster.RefDocCode);
            param[16] = new SqlParameter("@RefDocItemNo", argCanabalizeMaster.RefDocItemNo);
            param[17] = new SqlParameter("@RefDocType", argCanabalizeMaster.RefDocType);
            param[18] = new SqlParameter("@MaterialDocTypeCode", argCanabalizeMaster.MaterialDocTypeCode);
            param[19] = new SqlParameter("@PGoodsMovementCode", argCanabalizeMaster.PGoodsMovementCode);
            param[20] = new SqlParameter("@GMItemNo", argCanabalizeMaster.GMItemNo);
            param[21] = new SqlParameter("@IsPartialCanibalize", argCanabalizeMaster.IsPartialCanibalize);
            param[22] = new SqlParameter("@ClientCode", argCanabalizeMaster.ClientCode);
            param[23] = new SqlParameter("@CreatedBy", argCanabalizeMaster.CreatedBy);
            param[24] = new SqlParameter("@ModifiedBy", argCanabalizeMaster.ModifiedBy);

            param[25] = new SqlParameter("@Type", SqlDbType.Char);
            param[25].Size = 1;
            param[25].Direction = ParameterDirection.Output;

            param[26] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[26].Size = 255;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[27].Size = 20;
            param[27].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCanabalizeMaster", param);

            string strMessage = Convert.ToString(param[26].Value);
            string strType = Convert.ToString(param[25].Value);
            string strRetValue = Convert.ToString(param[27].Value);


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
        
        public ICollection<ErrorHandler> DeleteCanabalizeMaster(string argCanabalizeDocCode, string argParnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CanabalizeDocCode", argCanabalizeDocCode);
                param[1] = new SqlParameter("@ParnerCode", argParnerCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;
                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteCanabalizeMaster", param);


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

        public bool blnIsCanabalizeMasterExists(string argCanabalizeDocCode, string argParnerCode, string argClientCode)
        {
            bool IsCanabalizeMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetCanabalizeMaster(argCanabalizeDocCode, argParnerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCanabalizeMasterExists = true;
            }
            else
            {
                IsCanabalizeMasterExists = false;
            }
            return IsCanabalizeMasterExists;
        }

        public bool blnIsCanabalizeMasterExists(string argCanabalizeDocCode, string argParnerCode, string argClientCode, DataAccess da)
        {
            bool IsCanabalizeMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetCanabalizeMaster(argCanabalizeDocCode, argParnerCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCanabalizeMasterExists = true;
            }
            else
            {
                IsCanabalizeMasterExists = false;
            }
            return IsCanabalizeMasterExists;
        }

        public string GenerateCanabalizeCode(string argCanabalizeDocCode, string argCanabalizeDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CanabalizeDocCode", argCanabalizeDocCode);
            param[1] = new SqlParameter("@CanabalizeDocTypeCode", argCanabalizeDocTypeCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            param[4] = new SqlParameter("@GeneratedCanabalizeDocCode", SqlDbType.VarChar);
            param[4].Size = 18;
            param[4].Direction = ParameterDirection.Output;

            int i = da.ExecuteNonQuery("SL_GetNewCanabalizeCode", param);

            string strMessage = Convert.ToString(param[4].Value);

            return strMessage;

        }


    }
}