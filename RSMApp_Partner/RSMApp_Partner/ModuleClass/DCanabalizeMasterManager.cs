
//Created On :: 12, January, 2013
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class DCanabalizeMasterManager
    {
        const string DCanabalizeMasterTable = "DCanabalizeMaster";

       //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        DCanabalizeDetailsManager objDCanabalizeDetailsManager = new DCanabalizeDetailsManager();
        SerializeStockMissingPartsManager objSerializeStockMissingPartsManager = new SerializeStockMissingPartsManager();

        public DCanabalizeMaster objGetDCanabalizeMaster(string argDCanabalizeDocNo, string argPartnerCode, string argClientCode)
        {
            DCanabalizeMaster argDCanabalizeMaster = new DCanabalizeMaster();
            DataSet DataSetToFill = new DataSet();

            if (argDCanabalizeDocNo.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argPartnerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetDCanabalizeMaster(argDCanabalizeDocNo, argPartnerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argDCanabalizeMaster = this.objCreateDCanabalizeMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argDCanabalizeMaster;
        }


        public ICollection<DCanabalizeMaster> colGetDCanabalizeMaster(string argPartnerCode, string argClientCode)
        {
            List<DCanabalizeMaster> lst = new List<DCanabalizeMaster>();
            DataSet DataSetToFill = new DataSet();
            DCanabalizeMaster tDCanabalizeMaster = new DCanabalizeMaster();

            DataSetToFill = this.GetDCanabalizeMaster(argPartnerCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDCanabalizeMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetDCanabalizeMaster(string argDCanabalizeDocNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@DCanabalizeDocNo", argDCanabalizeDocNo);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDCanabalizeMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetDCanabalizeMaster(string argDCanabalizeDocNo, string argPartnerCode, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@DCanabalizeDocNo", argDCanabalizeDocNo);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetDCanabalizeMaster4ID", param);

            return DataSetToFill;
        }



        public DataSet GetDCanabalizeMaster(string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetDCanabalizeMaster",param);
            return DataSetToFill;
        }


        private DCanabalizeMaster objCreateDCanabalizeMaster(DataRow dr)
        {
            DCanabalizeMaster tDCanabalizeMaster = new DCanabalizeMaster();

            tDCanabalizeMaster.SetObjectInfo(dr);

            return tDCanabalizeMaster;

        }


        public ICollection<ErrorHandler> SaveDCanabalizeMaster(DCanabalizeMaster argDCanabalizeMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsDCanabalizeMasterExists(argDCanabalizeMaster.DCanabalizeDocNo, argDCanabalizeMaster.PartnerCode, argDCanabalizeMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertDCanabalizeMaster(argDCanabalizeMaster, da, lstErr);
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
                    UpdateDCanabalizeMaster(argDCanabalizeMaster, da, lstErr);
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

        public PartnerErrorResult SaveDCanabalizeMaster(DCanabalizeMaster argDCanabalizeMaster, DCanabalizeDetailsCol argDCanabalizeDetailsCol)
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
                if (blnIsDCanabalizeMasterExists(argDCanabalizeMaster.DCanabalizeDocNo, argDCanabalizeMaster.PartnerCode, argDCanabalizeMaster.ClientCode, da) == false)
                {
                    if (argDCanabalizeMaster.PGoodsMovementCode == "NEW")
                    {
                        if (argDCanabalizeMaster.MaterialDocTypeCode != "")
                        {
                            strProductGoodsMovCode = objPartnerGMManager.GenerateGMCode("NEW", "GM01", argDCanabalizeMaster.PartnerCode, argDCanabalizeMaster.ClientCode, da);
                            argDCanabalizeMaster.PGoodsMovementCode = strProductGoodsMovCode;
                            argDCanabalizeMaster.GMItemNo = 1;
                        }
                        else
                        {
                            argDCanabalizeMaster.PGoodsMovementCode = "";
                            argDCanabalizeMaster.GMItemNo = 0;
                        }
                    }


                    strretValue = InsertDCanabalizeMaster(argDCanabalizeMaster, da, lstErr);

                }
                else
                {
                    strretValue = UpdateDCanabalizeMaster(argDCanabalizeMaster, da, lstErr);

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

                    //if (argSerializeStockMissingPartsCol.colSerializeStockMissingParts.Count > 0)
                    //{

                    //    foreach (SerializeStockMissingParts objSerializeStockMissingParts in argSerializeStockMissingPartsCol.colSerializeStockMissingParts)
                    //    {
                    //        if (Convert.ToString(objSerializeStockMissingParts.SerialNo).Trim() == Convert.ToString(argDCanabalizeMaster.SerialNo).Trim())
                    //        {
                    //            objSerializeStockMissingPartsManager.SaveSerializeStockMissingParts(objSerializeStockMissingParts, da, lstErr);
                    //        }
                    //    }

                    //}



                    if (argDCanabalizeDetailsCol.colDCanabalizeDetails.Count > 0)
                    {
                        foreach (DCanabalizeDetails objDCanabalizeDetails in argDCanabalizeDetailsCol.colDCanabalizeDetails)
                        {
                            if (objDCanabalizeDetails.IsDeleted == 0)
                            {
                                if (objDCanabalizeDetails.PGoodsMovementCode == "NEW")
                                {
                                    strSpareGoodsMovCode = objPartnerGMManager.GenerateGMCode("NEW", "GM01", argDCanabalizeMaster.PartnerCode, argDCanabalizeMaster.ClientCode, da);
                                    objDCanabalizeDetails.PGoodsMovementCode = strSpareGoodsMovCode;
                                    objDCanabalizeDetails.GMItemNo = objDCanabalizeDetails.DCanabalizeItemNo;
                                }

                                objDCanabalizeDetails.DCanabalizeDocNo = strretValue;
                                objDCanabalizeDetailsManager.SaveDCanabalizeDetails(objDCanabalizeDetails, da, lstErr);
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
                    if (argDCanabalizeDetailsCol.colDCanabalizeDetails.Count > 0)
                    {
                        PartnerGoodsMovement objPartnerSpareGM = new PartnerGoodsMovement();
                        objPartnerSpareGM.PGoodsMovementCode = strSpareGoodsMovCode;
                        objPartnerSpareGM.PartnerGMDocTypeCode = "GM01";
                        objPartnerSpareGM.FromPlantCode = "";
                        objPartnerSpareGM.FromPartnerCode = Convert.ToString(argDCanabalizeMaster.PartnerCode);
                        objPartnerSpareGM.FromPartnerEmployeeCode = "";
                        objPartnerSpareGM.FromStoreCode = "";
                        objPartnerSpareGM.ToPartnerCode = Convert.ToString(argDCanabalizeMaster.PartnerCode);
                        objPartnerSpareGM.ToPlantCode = "";
                        objPartnerSpareGM.ToStoreCode = "";
                        objPartnerSpareGM.ToPartnerEmployeeCode = "";
                        objPartnerSpareGM.ClientCode = argDCanabalizeMaster.ClientCode;
                        objPartnerSpareGM.CreatedBy = argDCanabalizeMaster.CreatedBy;
                        objPartnerSpareGM.ModifiedBy = argDCanabalizeMaster.ModifiedBy;
                        objPartnerSpareGM.TotalQuantity = 0;
                        objPartnerSpareGM.PartnerCode = Convert.ToString(argDCanabalizeMaster.PartnerCode);
                        objPartnerSpareGM.GoodsMovDate = Convert.ToDateTime(argDCanabalizeMaster.DCanablizeDate);

                        /*----------------------------------------------------------------------------------------*/
                        /* Partner Goods Movement Detail */
                        PartnerGoodsMovementDetailCol objPartnerSpareGMCol = new PartnerGoodsMovementDetailCol();
                        PartnerGoodsMovSerialDetailCol objPartnerSpareGMSerialCol = new PartnerGoodsMovSerialDetailCol();

                        objPartnerSpareGMCol.colPartnerGMDetail = new List<PartnerGoodsMovementDetail>();
                        objPartnerSpareGMSerialCol.colPartnerGMSerialDetail = new List<PartnerGoodsMovSerialDetail>();
                        bool IsNew = true;
                        int iCtr = 0;
                        int tmpItemNo = 0;
                        foreach (DCanabalizeDetails objDCanabalizeDetails in argDCanabalizeDetailsCol.colDCanabalizeDetails)
                        {
                            if (objDCanabalizeDetails.IsDeleted == 0)
                            {
                                dsMatDocType = new DataSet();
                                iCtr = iCtr + 1;
                                tmpItemNo = iCtr;
                                if (IsNew == true)
                                {
                                    dsMatDocType = objPartnerMatDocTypeMan.GetPartnerMaterialDocType(objDCanabalizeDetails.MaterialDocTypeCode, objDCanabalizeDetails.ClientCode, da);

                                    PartnerGoodsMovementDetail objPartnerGMDetailsnew = new PartnerGoodsMovementDetail();
                                    objPartnerGMDetailsnew.PGoodsMovementCode = strSpareGoodsMovCode;
                                    objPartnerGMDetailsnew.ItemNo = tmpItemNo;
                                    objPartnerGMDetailsnew.MaterialCode = Convert.ToString(objDCanabalizeDetails.MaterialCode);
                                    objPartnerGMDetailsnew.MatGroup1Code = Convert.ToString(objDCanabalizeDetails.MatGroup1Code);
                                    objPartnerGMDetailsnew.StockIndicator = Convert.ToString(objDCanabalizeDetails.StockIndicator);
                                    objPartnerGMDetailsnew.ToStockIndicator = Convert.ToString(objDCanabalizeDetails.ToStockIndicator);
                                    objPartnerGMDetailsnew.Quantity = Convert.ToInt32(objDCanabalizeDetails.Quantity);
                                    objPartnerGMDetailsnew.UOMCode = Convert.ToString(objDCanabalizeDetails.UOMCode);
                                    objPartnerGMDetailsnew.ClientCode = Convert.ToString(objDCanabalizeDetails.ClientCode);
                                    objPartnerGMDetailsnew.CreatedBy = Convert.ToString(objDCanabalizeDetails.CreatedBy);
                                    objPartnerGMDetailsnew.ModifiedBy = Convert.ToString(objDCanabalizeDetails.ModifiedBy);
                                    objPartnerGMDetailsnew.TranRefDocCode = Convert.ToString(strretValue);
                                    objPartnerGMDetailsnew.TranRefDocItemNo = Convert.ToInt32(objDCanabalizeDetails.DCanabalizeItemNo);
                                    objPartnerGMDetailsnew.MaterialDocTypeCode = Convert.ToString(objDCanabalizeDetails.MaterialDocTypeCode);
                                    objPartnerGMDetailsnew.PartnerCode = Convert.ToString(objDCanabalizeDetails.PartnerCode);

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
                                                objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(objDCanabalizeDetails.PartnerCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.FromPartnerCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["FromStore"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(objDCanabalizeDetails.StoreCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.FromStoreCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["FromEmployee"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.FromPartnerEmployeeCode = Convert.ToString(objDCanabalizeDetails.PartnerEmployeeCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.FromPartnerEmployeeCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["ToPartner"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(objDCanabalizeDetails.PartnerCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.ToPartnerCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["ToStore"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(objDCanabalizeDetails.ToStoreCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.ToStoreCode = "";
                                            }

                                            if (dsMatDocType.Tables[0].Rows[0]["ToEmployee"].ToString() != "HIDE")
                                            {
                                                objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString(objDCanabalizeDetails.ToPartnerEmployeeCode);
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
                                                objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(objDCanabalizeDetails.MaterialCode);
                                            }
                                            else
                                            {
                                                objPartnerGMDetailsnew.ToMaterialCode = "";
                                            }
                                        }
                                        else
                                        {
                                            objPartnerGMDetailsnew.FromPlantCode = "";
                                            objPartnerGMDetailsnew.FromPartnerCode = Convert.ToString(objDCanabalizeDetails.PartnerCode);
                                            objPartnerGMDetailsnew.FromPartnerEmployeeCode = Convert.ToString(objDCanabalizeDetails.PartnerEmployeeCode);
                                            objPartnerGMDetailsnew.FromStoreCode = Convert.ToString(objDCanabalizeDetails.StoreCode);
                                            objPartnerGMDetailsnew.ToPartnerCode = Convert.ToString(objDCanabalizeDetails.PartnerCode);
                                            objPartnerGMDetailsnew.ToPartnerEmployeeCode = Convert.ToString(objDCanabalizeDetails.ToPartnerEmployeeCode);
                                            objPartnerGMDetailsnew.ToPlantCode = "";
                                            objPartnerGMDetailsnew.ToStoreCode = Convert.ToString(objDCanabalizeDetails.ToStoreCode);
                                            objPartnerGMDetailsnew.ToMaterialCode = Convert.ToString(objDCanabalizeDetails.MaterialCode);
                                        }
                                    }

                                    objPartnerSpareGMCol.colPartnerGMDetail.Add(objPartnerGMDetailsnew);
                                    /*Partner Goods Movement Serial Detail*/
                                    if (objDCanabalizeDetails.SerialNo1 != "")
                                    {
                                        /*Partner Goods Movement Serial Detail*/

                                        PartnerGoodsMovSerialDetail objPartnerGoodsMovSerialnew = new PartnerGoodsMovSerialDetail();
                                        objPartnerGoodsMovSerialnew.PGoodsMovementCode = strSpareGoodsMovCode;
                                        objPartnerGoodsMovSerialnew.ItemNo = tmpItemNo;
                                        objPartnerGoodsMovSerialnew.SerialNo1 = Convert.ToString(objDCanabalizeDetails.SerialNo1);
                                        objPartnerGoodsMovSerialnew.SerialNo2 = Convert.ToString("");
                                        objPartnerGoodsMovSerialnew.MaterialCode = Convert.ToString(objDCanabalizeDetails.MaterialCode);
                                        objPartnerGoodsMovSerialnew.MatGroup1Code = Convert.ToString(objDCanabalizeDetails.MatGroup1Code);
                                        objPartnerGoodsMovSerialnew.RefDocCode = Convert.ToString(objDCanabalizeDetails.RefDocCode);
                                        objPartnerGoodsMovSerialnew.RefDocItemNo = Convert.ToInt32(objDCanabalizeDetails.RefDocItemNo);
                                        objPartnerGoodsMovSerialnew.RefDocType = Convert.ToString(objDCanabalizeDetails.RefDocTypeCode);
                                        objPartnerGoodsMovSerialnew.TranRefDocCode = Convert.ToString(strretValue);
                                        objPartnerGoodsMovSerialnew.TranRefDocItemNo = Convert.ToInt32(objDCanabalizeDetails.DCanabalizeItemNo);
                                        objPartnerGoodsMovSerialnew.IsDeleted = 0;
                                        objPartnerGoodsMovSerialnew.ClientCode = Convert.ToString(objDCanabalizeDetails.ClientCode);
                                        objPartnerGoodsMovSerialnew.CreatedBy = Convert.ToString(objDCanabalizeDetails.CreatedBy);
                                        objPartnerGoodsMovSerialnew.ModifiedBy = Convert.ToString(objDCanabalizeDetails.ModifiedBy);
                                        objPartnerGoodsMovSerialnew.StockIndicator = Convert.ToString(objDCanabalizeDetails.StockIndicator);
                                        objPartnerGoodsMovSerialnew.ToStockIndicator = Convert.ToString(objDCanabalizeDetails.ToStockIndicator);


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
                                                    objPartnerGoodsMovSerialnew.PartnerCode = Convert.ToString(objDCanabalizeDetails.PartnerCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.PartnerCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["FromStore"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.StoreCode = Convert.ToString(objDCanabalizeDetails.StoreCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.StoreCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["FromEmployee"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.PartnerEmployeeCode = Convert.ToString(objDCanabalizeDetails.PartnerEmployeeCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.PartnerEmployeeCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["ToPartner"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.ToPartnerCode = Convert.ToString(objDCanabalizeDetails.PartnerCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.ToPartnerCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["ToStore"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.ToStoreCode = Convert.ToString(objDCanabalizeDetails.ToStoreCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.ToStoreCode = "";
                                                }

                                                if (dsMatDocType.Tables[0].Rows[0]["ToEmployee"].ToString() != "HIDE")
                                                {
                                                    objPartnerGoodsMovSerialnew.ToPartnerEmployeeCode = Convert.ToString(objDCanabalizeDetails.ToPartnerEmployeeCode);
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
                                                    objPartnerGoodsMovSerialnew.ToMaterialCode = Convert.ToString(objDCanabalizeDetails.MaterialCode);
                                                }
                                                else
                                                {
                                                    objPartnerGoodsMovSerialnew.ToMaterialCode = "";
                                                }


                                            }
                                            else
                                            {
                                               

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


        public string  InsertDCanabalizeMaster(DCanabalizeMaster argDCanabalizeMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@DCanabalizeDocNo", argDCanabalizeMaster.DCanabalizeDocNo);
            param[1] = new SqlParameter("@DCanablizeDocTypeCode", argDCanabalizeMaster.DCanablizeDocTypeCode);
            param[2] = new SqlParameter("@DCanablizeDate", argDCanabalizeMaster.DCanablizeDate);
            param[3] = new SqlParameter("@SerialNo", argDCanabalizeMaster.SerialNo);
            param[4] = new SqlParameter("@MaterialCode", argDCanabalizeMaster.MaterialCode);
            param[5] = new SqlParameter("@MatGroup1Code", argDCanabalizeMaster.MatGroup1Code);
            param[6] = new SqlParameter("@PartnerCode", argDCanabalizeMaster.PartnerCode);
            param[7] = new SqlParameter("@StoreCode", argDCanabalizeMaster.StoreCode);
            param[8] = new SqlParameter("@StockIndicator", argDCanabalizeMaster.StockIndicator);
            param[9] = new SqlParameter("@PartnerEmployeeCode", argDCanabalizeMaster.PartnerEmployeeCode);
            param[10] = new SqlParameter("@ToPartnerCode", argDCanabalizeMaster.ToPartnerCode);
            param[11] = new SqlParameter("@ToStoreCode", argDCanabalizeMaster.ToStoreCode);
            param[12] = new SqlParameter("@ToStockIndicator", argDCanabalizeMaster.ToStockIndicator);
            param[13] = new SqlParameter("@ToPartnerEmployeeCode", argDCanabalizeMaster.ToPartnerEmployeeCode);
            param[14] = new SqlParameter("@MaterialDocTypeCode", argDCanabalizeMaster.MaterialDocTypeCode);
            param[15] = new SqlParameter("@PGoodsMovementCode", argDCanabalizeMaster.PGoodsMovementCode);
            param[16] = new SqlParameter("@GMItemNo", argDCanabalizeMaster.GMItemNo);
            param[17] = new SqlParameter("@ClientCode", argDCanabalizeMaster.ClientCode);
            param[18] = new SqlParameter("@CreatedBy", argDCanabalizeMaster.CreatedBy);
            param[19] = new SqlParameter("@ModifiedBy", argDCanabalizeMaster.ModifiedBy);
   

            param[20] = new SqlParameter("@Type", SqlDbType.Char);
            param[20].Size = 1;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[21].Size = 255;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[22].Size = 20;
            param[22].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertDCanabalizeMaster", param);


            string strMessage = Convert.ToString(param[21].Value);
            string strType = Convert.ToString(param[20].Value);
            string strRetValue = Convert.ToString(param[22].Value);


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


        public string UpdateDCanabalizeMaster(DCanabalizeMaster argDCanabalizeMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@DCanabalizeDocNo", argDCanabalizeMaster.DCanabalizeDocNo);
            param[1] = new SqlParameter("@DCanablizeDocTypeCode", argDCanabalizeMaster.DCanablizeDocTypeCode);
            param[2] = new SqlParameter("@DCanablizeDate", argDCanabalizeMaster.DCanablizeDate);
            param[3] = new SqlParameter("@SerialNo", argDCanabalizeMaster.SerialNo);
            param[4] = new SqlParameter("@MaterialCode", argDCanabalizeMaster.MaterialCode);
            param[5] = new SqlParameter("@MatGroup1Code", argDCanabalizeMaster.MatGroup1Code);
            param[6] = new SqlParameter("@PartnerCode", argDCanabalizeMaster.PartnerCode);
            param[7] = new SqlParameter("@StoreCode", argDCanabalizeMaster.StoreCode);
            param[8] = new SqlParameter("@StockIndicator", argDCanabalizeMaster.StockIndicator);
            param[9] = new SqlParameter("@PartnerEmployeeCode", argDCanabalizeMaster.PartnerEmployeeCode);
            param[10] = new SqlParameter("@ToPartnerCode", argDCanabalizeMaster.ToPartnerCode);
            param[11] = new SqlParameter("@ToStoreCode", argDCanabalizeMaster.ToStoreCode);
            param[12] = new SqlParameter("@ToStockIndicator", argDCanabalizeMaster.ToStockIndicator);
            param[13] = new SqlParameter("@ToPartnerEmployeeCode", argDCanabalizeMaster.ToPartnerEmployeeCode);
            param[14] = new SqlParameter("@MaterialDocTypeCode", argDCanabalizeMaster.MaterialDocTypeCode);
            param[15] = new SqlParameter("@PGoodsMovementCode", argDCanabalizeMaster.PGoodsMovementCode);
            param[16] = new SqlParameter("@GMItemNo", argDCanabalizeMaster.GMItemNo);
            param[17] = new SqlParameter("@ClientCode", argDCanabalizeMaster.ClientCode);
            param[18] = new SqlParameter("@CreatedBy", argDCanabalizeMaster.CreatedBy);
            param[19] = new SqlParameter("@ModifiedBy", argDCanabalizeMaster.ModifiedBy);


            param[20] = new SqlParameter("@Type", SqlDbType.Char);
            param[20].Size = 1;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[21].Size = 255;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[22].Size = 20;
            param[22].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateDCanabalizeMaster", param);


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
            lstErr.Add(objErrorHandler);
            return strRetValue;

        }


        public ICollection<ErrorHandler> DeleteDCanabalizeMaster(string argDCanabalizeDocNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@DCanabalizeDocNo", argDCanabalizeDocNo);
                param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteDCanabalizeMaster", param);


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


        public bool blnIsDCanabalizeMasterExists(string argDCanabalizeDocNo, string argPartnerCode, string argClientCode)
        {
            bool IsDCanabalizeMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetDCanabalizeMaster(argDCanabalizeDocNo, argPartnerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDCanabalizeMasterExists = true;
            }
            else
            {
                IsDCanabalizeMasterExists = false;
            }
            return IsDCanabalizeMasterExists;
        }

        public bool blnIsDCanabalizeMasterExists(string argDCanabalizeDocNo, string argPartnerCode, string argClientCode,DataAccess da)
        {
            bool IsDCanabalizeMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetDCanabalizeMaster(argDCanabalizeDocNo, argPartnerCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDCanabalizeMasterExists = true;
            }
            else
            {
                IsDCanabalizeMasterExists = false;
            }
            return IsDCanabalizeMasterExists;
        }

        public string GenerateDCanabalizeCode(string argDCanabalizeDocNo, string argDCanabalizeDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@DCanabalizeDocNo", argDCanabalizeDocNo);
            param[1] = new SqlParameter("@DCanabalizeDocTypeCode", argDCanabalizeDocTypeCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            param[4] = new SqlParameter("@GeneratedDCanabalizeDocNo", SqlDbType.VarChar);
            param[4].Size = 18;
            param[4].Direction = ParameterDirection.Output;

            int i = da.ExecuteNonQuery("SL_GetNewDCanabalizeCode", param);

            string strMessage = Convert.ToString(param[4].Value);

            return strMessage;

        }
    }
}