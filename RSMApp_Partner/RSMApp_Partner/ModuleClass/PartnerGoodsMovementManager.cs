
//Created On :: 30, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class PartnerGoodsMovementManager
    {
        const string PartnerGoodsMovementTable = "PartnerGoodsMovement";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        ReplacementOrderManager objReplaceOrderManager = new ReplacementOrderManager();
                
        public PartnerGoodsMovement objGetPartnerGoodsMovement(string argPGoodsMovementCode, string argClientCode)
        {
            PartnerGoodsMovement argPartnerGoodsMovement = new PartnerGoodsMovement();
            DataSet DataSetToFill = new DataSet();

            if (argPGoodsMovementCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPartnerGoodsMovement(argPGoodsMovementCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartnerGoodsMovement = this.objCreatePartnerGoodsMovement((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argPartnerGoodsMovement;
        }
        
        public ICollection<PartnerGoodsMovement> colGetPartnerGoodsMovement(string argClientCode)
        {
            List<PartnerGoodsMovement> lst = new List<PartnerGoodsMovement>();
            DataSet DataSetToFill = new DataSet();
            PartnerGoodsMovement tPartnerGoodsMovement = new PartnerGoodsMovement();

            DataSetToFill = this.GetPartnerGoodsMovement(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePartnerGoodsMovement(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetPartnerGoodsMovement(string argPGoodsMovementCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGoodsMovement4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerGoodsMovement(string argPGoodsMovementCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPartnerGoodsMovement4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetPartnerGoodsMovement(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGoodsMovement", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerGoodsMov4Type(string argPartnerGMDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PartnerGMDocTypeCode", argPartnerGMDocTypeCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGoodsMov4Type", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerGoodsMov4Combo(string argFromPartnerCode, string argToPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@FromPartnerCode", argFromPartnerCode);
            param[1] = new SqlParameter("@ToPartnerCode", argToPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGM4Combo", param);
            return DataSetToFill;
        }
                       
        private PartnerGoodsMovement objCreatePartnerGoodsMovement(DataRow dr)
        {
            PartnerGoodsMovement tPartnerGoodsMovement = new PartnerGoodsMovement();

            tPartnerGoodsMovement.SetObjectInfo(dr);

            return tPartnerGoodsMovement;
        }
        
        public ICollection<ErrorHandler> SavePartnerGoodsMovement(PartnerGoodsMovement argPartnerGoodsMovement)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartnerGoodsMovementExists(argPartnerGoodsMovement.PGoodsMovementCode, argPartnerGoodsMovement.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartnerGoodsMovement(argPartnerGoodsMovement, da, lstErr);
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
                    UpdatePartnerGoodsMovement(argPartnerGoodsMovement, da, lstErr);
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

        public void SavePartnerGoodsMovement(PartnerGoodsMovement argPartnerGoodsMovement, PartnerGoodsMovementDetailCol argPartnerGNDetail, PartnerGoodsMovSerialDetailCol argPartnerGMSerialDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                PartnerGoodsMovementDetailManager objPartnerGMDetailManager = new PartnerGoodsMovementDetailManager();
                PartnerGoodsMovSerialDetailManager objPartnerGNSerialDetailManager = new PartnerGoodsMovSerialDetailManager();
                string strRetValue = "";
                if (blnIsPartnerGoodsMovementExists(argPartnerGoodsMovement.PGoodsMovementCode, argPartnerGoodsMovement.ClientCode, da) == false)
                {
                   strRetValue =  InsertPartnerGoodsMovement(argPartnerGoodsMovement, da, lstErr);
                }
                else
                {
                   //strRetValue =  UpdatePartnerGoodsMovement(argPartnerGoodsMovement, da, lstErr);
                }

                if (strRetValue != "")
                {
                    if (argPartnerGNDetail.colPartnerGMDetail.Count > 0)
                    {
                        foreach (PartnerGoodsMovementDetail objPartnerGMDetail in argPartnerGNDetail.colPartnerGMDetail)
                        {
                            objPartnerGMDetail.PGoodsMovementCode = strRetValue;

                            if (objPartnerGMDetail.IsDeleted == 0)
                            {
                                objPartnerGMDetailManager.InsertPartnerGoodsMovementDetail(objPartnerGMDetail, da, lstErr);
                            }
                            else
                            {
                                /*Delete Function Call*/
                            }                          

                        }
                    }

                    if (argPartnerGMSerialDetail.colPartnerGMSerialDetail.Count > 0)
                    {
                        foreach (PartnerGoodsMovSerialDetail objPartnerGMSerialDetail in argPartnerGMSerialDetail.colPartnerGMSerialDetail)
                        {
                            
                                objPartnerGMSerialDetail.PGoodsMovementCode = strRetValue;

                                if (objPartnerGMSerialDetail.IsDeleted == 0)
                                {
                                    objPartnerGNSerialDetailManager.InsertPartnerGoodsMovSerialDetail(objPartnerGMSerialDetail, da, lstErr);
                                }
                                else
                                {
                                    /*Call delete function*/
                                }
                            
                        }
                    }


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
        
        public PartnerErrorResult SavePartnerGoodsMovement(PartnerGoodsMovement argPartnerGoodsMovement, PartnerGoodsMovementDetailCol argPartnerGNDetail, PartnerGoodsMovSerialDetailCol argPartnerGMSerialDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            PartnerErrorResult errorcol = new PartnerErrorResult();
            PartnerGoodsMovementDetailManager objPartnerGMDetailManager = new PartnerGoodsMovementDetailManager();
            PartnerGoodsMovSerialDetailManager objPartnerGNSerialDetailManager = new PartnerGoodsMovSerialDetailManager();
            DataAccess da = new DataAccess();
            string strRetValue = "";

            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();
                
                if (blnIsPartnerGoodsMovementExists(argPartnerGoodsMovement.PGoodsMovementCode, argPartnerGoodsMovement.ClientCode, da) == false)
                {
                    strRetValue = InsertPartnerGoodsMovement(argPartnerGoodsMovement, da, lstErr);
                }
                else
                {
                    strRetValue = UpdatePartnerGoodsMovement(argPartnerGoodsMovement, da, lstErr);
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

                if (strRetValue != "")
                {
                    if (argPartnerGNDetail.colPartnerGMDetail.Count > 0)
                    {
                        foreach (PartnerGoodsMovementDetail objPartnerGMDetail in argPartnerGNDetail.colPartnerGMDetail)
                        {
                            objPartnerGMDetail.PGoodsMovementCode = strRetValue;

                            if (objPartnerGMDetail.IsDeleted == 0)
                            {
                                objPartnerGMDetailManager.SavePartnerGoodsMovementDetail(objPartnerGMDetail, da, lstErr);
                            }
                            else
                            {
                                /*Delete Function Call*/
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

                    if (argPartnerGMSerialDetail.colPartnerGMSerialDetail.Count > 0)
                    {
                        foreach (PartnerGoodsMovSerialDetail objPartnerGMSerialDetail in argPartnerGMSerialDetail.colPartnerGMSerialDetail)
                        {
                           
                                objPartnerGMSerialDetail.PGoodsMovementCode = strRetValue;

                                if (objPartnerGMSerialDetail.IsDeleted == 0)
                                {
                                    objPartnerGNSerialDetailManager.SavePartnerGoodsMovSerialDetail(objPartnerGMSerialDetail, da, lstErr);
                                }
                                else
                                {
                                    /*Call delete function*/
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

                    /******************************/

                    int IsCreateRepOrder = 0;
                    PartnerGMDocTypeManager objPartnerGMDocTypeMan = new PartnerGMDocTypeManager();
                    DataSet ds = objPartnerGMDocTypeMan.GetPartnerGMDocType(Convert.ToString(argPartnerGoodsMovement.PartnerGMDocTypeCode), Convert.ToString(argPartnerGoodsMovement.ClientCode), da);

                    if (ds != null)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (Convert.ToString(ds.Tables[0].Rows[0]["CreateRepOrder"]) != "")
                            {
                                IsCreateRepOrder = Convert.ToInt32(ds.Tables[0].Rows[0]["CreateRepOrder"]);
                            }
                        }
                    }


                    if (IsCreateRepOrder == 1)
                    {
                        ///* Call Replace Order Save Function */
                        ReplacementOrder objRepalcementOrder = new ReplacementOrder();
                        string strNewRepOrderCode = objReplaceOrderManager.GenerateRepOrderCode("NEW", "RO01", argPartnerGoodsMovement.PartnerCode, argPartnerGoodsMovement.ClientCode);

                        objRepalcementOrder.RepOrderCode = strNewRepOrderCode;
                        objRepalcementOrder.RepOrderDocTypeCode = Convert.ToString("RO01");
                        objRepalcementOrder.RepOrderDate = Convert.ToDateTime(argPartnerGoodsMovement.GoodsMovDate).ToString("yyyy-MM-dd");
                        objRepalcementOrder.TotalQuantity = 0;
                        objRepalcementOrder.RepOrderStatus = "OPEN";
                        objRepalcementOrder.OrderType = Convert.ToString(argPartnerGoodsMovement.AssignType);
                        objRepalcementOrder.IssueDocCode = Convert.ToString(argPartnerGoodsMovement.PGoodsMovementCode);
                        objRepalcementOrder.PartnerCode = Convert.ToString(argPartnerGoodsMovement.PartnerCode);
                        objRepalcementOrder.ToPartnerCode = Convert.ToString(argPartnerGoodsMovement.ToPartnerCode);
                        objRepalcementOrder.ClientCode = Convert.ToString(argPartnerGoodsMovement.ClientCode);
                        objRepalcementOrder.CreatedBy = Convert.ToString(argPartnerGoodsMovement.CreatedBy);
                        objRepalcementOrder.ModifiedBy = Convert.ToString(argPartnerGoodsMovement.ModifiedBy);

                        ReplacementOrderDetailCol argReplacementOrderDetailCol = new ReplacementOrderDetailCol();
                        ReplacementOrderDetail objReplacementOrderDetail = null;

                        ReplacementOrderSerialDetailCol argReplacementOrderSerialDetailCol = new ReplacementOrderSerialDetailCol();
                        ReplacementOrderSerialDetail objReplacementOrderSerialDetail = null;

                        int ictr = 0;

                        foreach (PartnerGoodsMovementDetail objPartnerGMDetail in argPartnerGNDetail.colPartnerGMDetail)
                        {
                            ictr = ictr + 1;
                            objReplacementOrderDetail = new ReplacementOrderDetail();
                            objReplacementOrderDetail.RepOrderCode = strNewRepOrderCode;
                            objReplacementOrderDetail.RepOrderItemNo = ictr;
                            objReplacementOrderDetail.MaterialCode = objPartnerGMDetail.MaterialCode;
                            objReplacementOrderDetail.MatGroup1Code = objPartnerGMDetail.MatGroup1Code;
                            objReplacementOrderDetail.PartnerCode = objPartnerGMDetail.PartnerCode;
                            objReplacementOrderDetail.PartnerEmployeeCode = objPartnerGMDetail.FromPartnerEmployeeCode;
                            objReplacementOrderDetail.OrderQty = objPartnerGMDetail.Quantity;
                            objReplacementOrderDetail.UOMCode = objPartnerGMDetail.UOMCode;
                            objReplacementOrderDetail.ReceivedQty = 0;
                            objReplacementOrderDetail.RepOrderStatus = "OPEN";
                            objReplacementOrderDetail.IssueDocCode = objPartnerGMDetail.PGoodsMovementCode;
                            objReplacementOrderDetail.IssueDocItemNo = objPartnerGMDetail.ItemNo;
                            objReplacementOrderDetail.ToPartnerCode = objPartnerGMDetail.ToPartnerCode;
                            objReplacementOrderDetail.ClientCode = argPartnerGoodsMovement.ClientCode;
                            objReplacementOrderDetail.CreatedBy = argPartnerGoodsMovement.CreatedBy;
                            objReplacementOrderDetail.ModifiedBy = argPartnerGoodsMovement.ModifiedBy;
                            objReplacementOrderDetail.IsDeleted = 0;

                            argReplacementOrderDetailCol.colReplacementOrderDetail.Add(objReplacementOrderDetail);

                            foreach (PartnerGoodsMovSerialDetail objPartnerGMSerialDetail in argPartnerGMSerialDetail.colPartnerGMSerialDetail)
                            {
                                if (objPartnerGMSerialDetail.ItemNo == objPartnerGMDetail.ItemNo)
                                {
                                    objReplacementOrderSerialDetail = new ReplacementOrderSerialDetail();
                                    objReplacementOrderSerialDetail.RepOrderCode = strNewRepOrderCode;
                                    objReplacementOrderSerialDetail.RepOrderItemNo = ictr;
                                    objReplacementOrderSerialDetail.SerialNo1 = objPartnerGMSerialDetail.SerialNo1;
                                    objReplacementOrderSerialDetail.SerialNo2 = objPartnerGMSerialDetail.SerialNo2;
                                    objReplacementOrderSerialDetail.MaterialCode = objPartnerGMSerialDetail.MaterialCode;
                                    objReplacementOrderSerialDetail.MatGroup1Code = objPartnerGMSerialDetail.MatGroup1Code;
                                    objReplacementOrderSerialDetail.PartnerCode = objPartnerGMSerialDetail.PartnerCode;
                                    objReplacementOrderSerialDetail.PartnerEmployeeCode = objPartnerGMSerialDetail.PartnerEmployeeCode;
                                    objReplacementOrderSerialDetail.OrderQty = 1;
                                    objReplacementOrderSerialDetail.UOMCode = objPartnerGMDetail.UOMCode;
                                    objReplacementOrderSerialDetail.ReceivedQty = 0;
                                    objReplacementOrderSerialDetail.RepOrderStatus = "OPEN";
                                    objReplacementOrderSerialDetail.IssueDocCode = objPartnerGMSerialDetail.PGoodsMovementCode;
                                    objReplacementOrderSerialDetail.IssueDocItemNo = objPartnerGMSerialDetail.ItemNo;
                                    objReplacementOrderSerialDetail.ToPartnerCode = objPartnerGMSerialDetail.ToPartnerCode;
                                    objReplacementOrderSerialDetail.ClientCode = argPartnerGoodsMovement.ClientCode;
                                    objReplacementOrderSerialDetail.CreatedBy = argPartnerGoodsMovement.CreatedBy;
                                    objReplacementOrderSerialDetail.ModifiedBy = argPartnerGoodsMovement.ModifiedBy;
                                    objReplacementOrderSerialDetail.IsDeleted = 0;

                                    argReplacementOrderSerialDetailCol.colReplacementOrderSerialDetail.Add(objReplacementOrderSerialDetail);
                                }
                            }

                        }

                        if (argReplacementOrderDetailCol.colReplacementOrderDetail.Count > 0)
                        {
                            objReplaceOrderManager.SaveReplacementOrder(objRepalcementOrder, argReplacementOrderDetailCol,argReplacementOrderSerialDetailCol, da, lstErr);

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

                    /******************************/


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
        
        public string InsertPartnerGoodsMovement(PartnerGoodsMovement argPartnerGoodsMovement, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPartnerGoodsMovement.PGoodsMovementCode);
            param[1] = new SqlParameter("@PartnerGMDocTypeCode", argPartnerGoodsMovement.PartnerGMDocTypeCode);
            param[2] = new SqlParameter("@FromPlantCode", argPartnerGoodsMovement.FromPlantCode);
            param[3] = new SqlParameter("@FromPartnerCode", argPartnerGoodsMovement.FromPartnerCode);
            param[4] = new SqlParameter("@FromStoreCode", argPartnerGoodsMovement.FromStoreCode);
            param[5] = new SqlParameter("@FromPartnerEmployeeCode", argPartnerGoodsMovement.FromPartnerEmployeeCode);
            param[6] = new SqlParameter("@ToPlantCode", argPartnerGoodsMovement.ToPlantCode);
            param[7] = new SqlParameter("@ToPartnerCode", argPartnerGoodsMovement.ToPartnerCode);
            param[8] = new SqlParameter("@ToStoreCode", argPartnerGoodsMovement.ToStoreCode);
            param[9] = new SqlParameter("@ToPartnerEmployeeCode", argPartnerGoodsMovement.ToPartnerEmployeeCode);
            param[10] = new SqlParameter("@TotalQuantity", argPartnerGoodsMovement.TotalQuantity);
            param[11] = new SqlParameter("@PartnerCode", argPartnerGoodsMovement.PartnerCode);

            param[12] = new SqlParameter("@GoodsMovDate", argPartnerGoodsMovement.GoodsMovDate);

            param[13] = new SqlParameter("@ClientCode", argPartnerGoodsMovement.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argPartnerGoodsMovement.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argPartnerGoodsMovement.ModifiedBy);
  
            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPartnerGoodsMovement", param);


            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);


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
        
        public string UpdatePartnerGoodsMovement(PartnerGoodsMovement argPartnerGoodsMovement, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPartnerGoodsMovement.PGoodsMovementCode);
            param[1] = new SqlParameter("@PartnerGMDocTypeCode", argPartnerGoodsMovement.PartnerGMDocTypeCode);
            param[2] = new SqlParameter("@FromPlantCode", argPartnerGoodsMovement.FromPlantCode);
            param[3] = new SqlParameter("@FromPartnerCode", argPartnerGoodsMovement.FromPartnerCode);
            param[4] = new SqlParameter("@FromStoreCode", argPartnerGoodsMovement.FromStoreCode);
            param[5] = new SqlParameter("@FromPartnerEmployeeCode", argPartnerGoodsMovement.FromPartnerEmployeeCode);
            param[6] = new SqlParameter("@ToPlantCode", argPartnerGoodsMovement.ToPlantCode);
            param[7] = new SqlParameter("@ToPartnerCode", argPartnerGoodsMovement.ToPartnerCode);
            param[8] = new SqlParameter("@ToStoreCode", argPartnerGoodsMovement.ToStoreCode);
            param[9] = new SqlParameter("@ToPartnerEmployeeCode", argPartnerGoodsMovement.ToPartnerEmployeeCode);
            param[10] = new SqlParameter("@TotalQuantity", argPartnerGoodsMovement.TotalQuantity);
            param[11] = new SqlParameter("@PartnerCode", argPartnerGoodsMovement.PartnerCode);

            param[12] = new SqlParameter("@GoodsMovDate", argPartnerGoodsMovement.GoodsMovDate);

            param[13] = new SqlParameter("@ClientCode", argPartnerGoodsMovement.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argPartnerGoodsMovement.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argPartnerGoodsMovement.ModifiedBy);

            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;


            int i = da.NExecuteNonQuery("Proc_UpdatePartnerGoodsMovement", param);


            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);


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
        
        public ICollection<ErrorHandler> DeletePartnerGoodsMovement(string argPGoodsMovementCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
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
                int i = da.ExecuteNonQuery("Proc_DeletePartnerGoodsMovement", param);


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

        public void DeletePartnerGoodsMovement(string argPGoodsMovementCode, string argClientCode, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
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
                int i = da.ExecuteNonQuery("Proc_DeletePartnerGoodsMovement", param);


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
        }

        public bool blnIsPartnerGoodsMovementExists(string argPGoodsMovementCode, string argClientCode)
        {
            bool IsPartnerGoodsMovementExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerGoodsMovement(argPGoodsMovementCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerGoodsMovementExists = true;
            }
            else
            {
                IsPartnerGoodsMovementExists = false;
            }
            return IsPartnerGoodsMovementExists;
        }

        public bool blnIsPartnerGoodsMovementExists(string argPGoodsMovementCode, string argClientCode, DataAccess da)
        {
            bool IsPartnerGoodsMovementExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerGoodsMovement(argPGoodsMovementCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerGoodsMovementExists = true;
            }
            else
            {
                IsPartnerGoodsMovementExists = false;
            }
            return IsPartnerGoodsMovementExists;
        }

        public string GenerateGMCode(string argGoodsMovementCode, string argPartnerGMDocTypeCode, string argPartnerCode, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@GoodsMovementCode", argGoodsMovementCode);
            param[1] = new SqlParameter("@PartnerGMDocTypeCode", argPartnerGMDocTypeCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            param[4] = new SqlParameter("@GeneratedCallCode", SqlDbType.VarChar);
            param[4].Size = 18;
            param[4].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("SL_GetNewPartnerGMCode", param);

            string strMessage = Convert.ToString(param[4].Value);


            return strMessage;

        }

        public string GenerateGMCode(string argGoodsMovementCode, string argPartnerGMDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@GoodsMovementCode", argGoodsMovementCode);
            param[1] = new SqlParameter("@PartnerGMDocTypeCode", argPartnerGMDocTypeCode);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            param[4] = new SqlParameter("@GeneratedCallCode", SqlDbType.VarChar);
            param[4].Size = 18;
            param[4].Direction = ParameterDirection.Output;

            int i = da.ExecuteNonQuery("SL_GetNewPartnerGMCode", param);

            string strMessage = Convert.ToString(param[4].Value);


            return strMessage;

        }
                
    }
}