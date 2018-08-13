
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
    public class PartnerGoodsMovementDetailManager
    {
        const string PartnerGoodsMovementDetailTable = "PartnerGoodsMovementDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public PartnerGoodsMovementDetail objGetPartnerGoodsMovementDetail(string argPGoodsMovementCode, int argItemNo, string argClientCode)
        {
            PartnerGoodsMovementDetail argPartnerGoodsMovementDetail = new PartnerGoodsMovementDetail();
            DataSet DataSetToFill = new DataSet();

            if (argPGoodsMovementCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argItemNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPartnerGoodsMovementDetail(argPGoodsMovementCode, argItemNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartnerGoodsMovementDetail = this.objCreatePartnerGoodsMovementDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPartnerGoodsMovementDetail;
        }
        
        //public ICollection<PartnerGoodsMovementDetail> colGetPartnerGoodsMovementDetail(string argPGoodsMovementCode, string argClientCode)
        //{
        //    List<PartnerGoodsMovementDetail> lst = new List<PartnerGoodsMovementDetail>();
        //    DataSet DataSetToFill = new DataSet();
        //    PartnerGoodsMovementDetail tPartnerGoodsMovementDetail = new PartnerGoodsMovementDetail();

        //    DataSetToFill = this.GetPartnerGoodsMovementDetail(argPGoodsMovementCode, argClientCode);

        //    if (DataSetToFill != null)
        //    {
        //        foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
        //        {
        //            lst.Add(objCreatePartnerGoodsMovementDetail(dr));
        //        }
        //    }
        //    goto Finish;

        //Finish:
        //    DataSetToFill = null;


        //    return lst;
        //}

        public void colGetPartnerGoodsMovementDetail(string argPGoodsMovementCode, string argClientCode, ref PartnerGoodsMovementDetailCol argPartnerGoodsMovDetailCol)
        {
            
            DataSet DataSetToFill = new DataSet();
            PartnerGoodsMovementDetail tPartnerGoodsMovementDetail = new PartnerGoodsMovementDetail();

            DataSetToFill = this.GetPartnerGoodsMovementDetail(argPGoodsMovementCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argPartnerGoodsMovDetailCol.colPartnerGMDetail.Add(objCreatePartnerGoodsMovementDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


        }

        public DataSet GetPartnerGoodsMovementDetail(string argPGoodsMovementCode, int argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGoodsMovementDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerGoodsMovementDetail(string argPGoodsMovementCode, int argItemNo, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPartnerGoodsMovementDetail4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetPartnerGoodsMovementDetail(string argPGoodsMovementCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGoodsMovementDetail", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerGoodsMovementDetail4Rec(string argPGoodsMovementCode, string argFromPartnerCode, string argToPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[1] = new SqlParameter("@FromPartnerCode", argFromPartnerCode);
            param[2] = new SqlParameter("@ToPartnerCode", argToPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGMDetail4Receipt", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerGoodsMovDetail4Search(string argDateFrom, string argDateTo, string argPartnerGMDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@DateFrom", argDateFrom);
            param[1] = new SqlParameter("@DateTo", argDateTo);
            param[2] = new SqlParameter("@PartnerGMDocTypeCode", argPartnerGMDocTypeCode);
            param[3] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGoodsMovementDetail4Search", param);
            return DataSetToFill;
        }

        

        private PartnerGoodsMovementDetail objCreatePartnerGoodsMovementDetail(DataRow dr)
        {
            PartnerGoodsMovementDetail tPartnerGoodsMovementDetail = new PartnerGoodsMovementDetail();

            tPartnerGoodsMovementDetail.SetObjectInfo(dr);

            return tPartnerGoodsMovementDetail;

        }
        
        public ICollection<ErrorHandler> SavePartnerGoodsMovementDetail(PartnerGoodsMovementDetail argPartnerGoodsMovementDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartnerGoodsMovementDetailExists(argPartnerGoodsMovementDetail.PGoodsMovementCode, argPartnerGoodsMovementDetail.ItemNo, argPartnerGoodsMovementDetail.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartnerGoodsMovementDetail(argPartnerGoodsMovementDetail, da, lstErr);
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
                    UpdatePartnerGoodsMovementDetail(argPartnerGoodsMovementDetail, da, lstErr);
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

        public void SavePartnerGoodsMovementDetail(PartnerGoodsMovementDetail argPartnerGoodsMovementDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsPartnerGoodsMovementDetailExists(argPartnerGoodsMovementDetail.PGoodsMovementCode, argPartnerGoodsMovementDetail.ItemNo, argPartnerGoodsMovementDetail.ClientCode, da) == false)
                {
                    InsertPartnerGoodsMovementDetail(argPartnerGoodsMovementDetail, da, lstErr);
                }
                else
                {
                   // UpdatePartnerGoodsMovementDetail(argPartnerGoodsMovementDetail, da, lstErr);
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

        public void InsertPartnerGoodsMovementDetail(PartnerGoodsMovementDetail argPartnerGoodsMovementDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[28];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPartnerGoodsMovementDetail.PGoodsMovementCode);
            param[1] = new SqlParameter("@ItemNo", argPartnerGoodsMovementDetail.ItemNo);
            param[2] = new SqlParameter("@MaterialCode", argPartnerGoodsMovementDetail.MaterialCode);
            param[3] = new SqlParameter("@MatGroup1Code", argPartnerGoodsMovementDetail.MatGroup1Code);
            param[4] = new SqlParameter("@FromPlantCode", argPartnerGoodsMovementDetail.FromPlantCode);
            param[5] = new SqlParameter("@FromPartnerCode", argPartnerGoodsMovementDetail.FromPartnerCode);
            param[6] = new SqlParameter("@FromStoreCode", argPartnerGoodsMovementDetail.FromStoreCode);
            param[7] = new SqlParameter("@FromPartnerEmployeeCode", argPartnerGoodsMovementDetail.FromPartnerEmployeeCode);
            param[8] = new SqlParameter("@ToPlantCode", argPartnerGoodsMovementDetail.ToPlantCode);
            param[9] = new SqlParameter("@ToPartnerCode", argPartnerGoodsMovementDetail.ToPartnerCode);
            param[10] = new SqlParameter("@ToStoreCode", argPartnerGoodsMovementDetail.ToStoreCode);
            param[11] = new SqlParameter("@ToPartnerEmployeeCode", argPartnerGoodsMovementDetail.ToPartnerEmployeeCode);
            param[12] = new SqlParameter("@ToMaterialCode", argPartnerGoodsMovementDetail.ToMaterialCode);
            param[13] = new SqlParameter("@Quantity", argPartnerGoodsMovementDetail.Quantity);
            param[14] = new SqlParameter("@UOMCode", argPartnerGoodsMovementDetail.UOMCode);

            param[15] = new SqlParameter("@UnitPrice", argPartnerGoodsMovementDetail.UnitPrice);

            param[16] = new SqlParameter("@StockIndicator", argPartnerGoodsMovementDetail.StockIndicator);
            param[17] = new SqlParameter("@ToStockIndicator", argPartnerGoodsMovementDetail.ToStockIndicator);
            param[18] = new SqlParameter("@TranRefDocCode", argPartnerGoodsMovementDetail.TranRefDocCode);
            param[19] = new SqlParameter("@TranRefDocItemNo", argPartnerGoodsMovementDetail.TranRefDocItemNo);
            param[20] = new SqlParameter("@MaterialDocTypeCode", argPartnerGoodsMovementDetail.MaterialDocTypeCode);
            param[21] = new SqlParameter("@PartnerCode", argPartnerGoodsMovementDetail.PartnerCode);
            param[22] = new SqlParameter("@ClientCode", argPartnerGoodsMovementDetail.ClientCode);
            param[23] = new SqlParameter("@CreatedBy", argPartnerGoodsMovementDetail.CreatedBy);
            param[24] = new SqlParameter("@ModifiedBy", argPartnerGoodsMovementDetail.ModifiedBy);
       
            param[25] = new SqlParameter("@Type", SqlDbType.Char);
            param[25].Size = 1;
            param[25].Direction = ParameterDirection.Output;

            param[26] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[26].Size = 255;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[27].Size = 20;
            param[27].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPartnerGoodsMovementDetail", param);


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

        }
        
        public void UpdatePartnerGoodsMovementDetail(PartnerGoodsMovementDetail argPartnerGoodsMovementDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[28];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPartnerGoodsMovementDetail.PGoodsMovementCode);
            param[1] = new SqlParameter("@ItemNo", argPartnerGoodsMovementDetail.ItemNo);
            param[2] = new SqlParameter("@MaterialCode", argPartnerGoodsMovementDetail.MaterialCode);
            param[3] = new SqlParameter("@MatGroup1Code", argPartnerGoodsMovementDetail.MatGroup1Code);
            param[4] = new SqlParameter("@FromPlantCode", argPartnerGoodsMovementDetail.FromPlantCode);
            param[5] = new SqlParameter("@FromPartnerCode", argPartnerGoodsMovementDetail.FromPartnerCode);
            param[6] = new SqlParameter("@FromStoreCode", argPartnerGoodsMovementDetail.FromStoreCode);
            param[7] = new SqlParameter("@FromPartnerEmployeeCode", argPartnerGoodsMovementDetail.FromPartnerEmployeeCode);
            param[8] = new SqlParameter("@ToPlantCode", argPartnerGoodsMovementDetail.ToPlantCode);
            param[9] = new SqlParameter("@ToPartnerCode", argPartnerGoodsMovementDetail.ToPartnerCode);
            param[10] = new SqlParameter("@ToStoreCode", argPartnerGoodsMovementDetail.ToStoreCode);
            param[11] = new SqlParameter("@ToPartnerEmployeeCode", argPartnerGoodsMovementDetail.ToPartnerEmployeeCode);
            param[12] = new SqlParameter("@ToMaterialCode", argPartnerGoodsMovementDetail.ToMaterialCode);
            param[13] = new SqlParameter("@Quantity", argPartnerGoodsMovementDetail.Quantity);
            param[14] = new SqlParameter("@UOMCode", argPartnerGoodsMovementDetail.UOMCode);

            param[15] = new SqlParameter("@UnitPrice", argPartnerGoodsMovementDetail.UnitPrice);

            param[16] = new SqlParameter("@StockIndicator", argPartnerGoodsMovementDetail.StockIndicator);
            param[17] = new SqlParameter("@ToStockIndicator", argPartnerGoodsMovementDetail.ToStockIndicator);
            param[18] = new SqlParameter("@TranRefDocCode", argPartnerGoodsMovementDetail.TranRefDocCode);
            param[19] = new SqlParameter("@TranRefDocItemNo", argPartnerGoodsMovementDetail.TranRefDocItemNo);
            param[20] = new SqlParameter("@MaterialDocTypeCode", argPartnerGoodsMovementDetail.MaterialDocTypeCode);
            param[21] = new SqlParameter("@PartnerCode", argPartnerGoodsMovementDetail.PartnerCode);
            param[22] = new SqlParameter("@ClientCode", argPartnerGoodsMovementDetail.ClientCode);
            param[23] = new SqlParameter("@CreatedBy", argPartnerGoodsMovementDetail.CreatedBy);
            param[24] = new SqlParameter("@ModifiedBy", argPartnerGoodsMovementDetail.ModifiedBy);

            param[25] = new SqlParameter("@Type", SqlDbType.Char);
            param[25].Size = 1;
            param[25].Direction = ParameterDirection.Output;

            param[26] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[26].Size = 255;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[27].Size = 20;
            param[27].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePartnerGoodsMovementDetail", param);
            
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

        }
        
        public ICollection<ErrorHandler> DeletePartnerGoodsMovementDetail(string argPGoodsMovementCode, int argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
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
                int i = da.ExecuteNonQuery("Proc_DeletePartnerGoodsMovementDetail", param);


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

        public void DeletePartnerGoodsMovementDetail(string argPGoodsMovementCode, int argItemNo, string argClientCode, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[6];
                
                param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
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

                int i = da.NExecuteNonQuery("Proc_DeletePartnerGoodsMovementDetail", param);
                
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
        }

        public bool blnIsPartnerGoodsMovementDetailExists(string argPGoodsMovementCode, int argItemNo, string argClientCode)
        {
            bool IsPartnerGoodsMovementDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerGoodsMovementDetail(argPGoodsMovementCode, argItemNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerGoodsMovementDetailExists = true;
            }
            else
            {
                IsPartnerGoodsMovementDetailExists = false;
            }
            return IsPartnerGoodsMovementDetailExists;
        }

        public bool blnIsPartnerGoodsMovementDetailExists(string argPGoodsMovementCode, int argItemNo, string argClientCode, DataAccess da)
        {
            bool IsPartnerGoodsMovementDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerGoodsMovementDetail(argPGoodsMovementCode, argItemNo, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerGoodsMovementDetailExists = true;
            }
            else
            {
                IsPartnerGoodsMovementDetailExists = false;
            }
            return IsPartnerGoodsMovementDetailExists;
        }

        //------------------------------------------------Report Section------------------------------------------//

        // For Goods Movement Report - Details Report

        public DataSet GetPartnerGMDetail4Report(string argPartnerGMDocTypeCode, string argFromPartnerCode, string argToPartnerCode, string argFromDate, string argToDate, string argMatGroup1Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@PartnerGMDocTypeCode", argPartnerGMDocTypeCode);
            param[1] = new SqlParameter("@FromPartnerCode", argFromPartnerCode);
            param[2] = new SqlParameter("@ToPartnerCode", argToPartnerCode);
            param[3] = new SqlParameter("@FromDate", argFromDate);
            param[4] = new SqlParameter("@ToDate", argToDate);
            param[5] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[6] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetPartnerGoodsMovement4Report", param);
            return DataSetToFill;
        }


        // -----------------------------------------------End Report----------------------------------------------//


        //---Ashutosh


        //  For Binding Temporary stock Opening serailized Data

        public void colGetPartnerGoodsMovementTempDetail(string argPGoodsMovementCode, string argPartnerCode, string argClientCode, ref PartnerGoodsMovementDetailCol argPartnerGoodsMovDetailCol)
        {

            DataSet DataSetToFill = new DataSet();
            PartnerGoodsMovementDetail tPartnerGoodsMovementDetail = new PartnerGoodsMovementDetail();

            DataSetToFill = this.GetPartnerGoodsMovementTempDetail(argPGoodsMovementCode, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argPartnerGoodsMovDetailCol.colPartnerGMDetail.Add(objCreatePartnerGoodsMovementDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


        }

        public DataSet GetPartnerGoodsMovementTempDetail(string argPGoodsMovementCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGoodsMovementTempDetail", param);
            return DataSetToFill;
        }
        
        // For Binding Temporary stock Opening Non serailized Data
        
        public void colGetPartnerGoodsMovementTempNSDetail(string argPGoodsMovementCode, string argPartnerCode, string argClientCode, ref PartnerGoodsMovementDetailCol argPartnerGoodsMovDetailCol)
        {
            DataSet DataSetToFill = new DataSet();
            PartnerGoodsMovementDetail tPartnerGoodsMovementDetail = new PartnerGoodsMovementDetail();

            DataSetToFill = this.GetPartnerGoodsMovementTempNSDetail(argPGoodsMovementCode, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argPartnerGoodsMovDetailCol.colPartnerGMDetail.Add(objCreatePartnerGoodsMovementDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

        }

        public DataSet GetPartnerGoodsMovementTempNSDetail(string argPGoodsMovementCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGoodsMovementTempNonserialDetail", param);
            return DataSetToFill;
        }

        // For Deleting Temp stock Opening For Partner

        public void DeleteTempStockOpeningData4Partner(string argTableName, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@TableName", argTableName);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_DeletetmpStockOpeningTables", param);

        }

        public DataSet GetPartnerGMDetail4IssueReport(string argPartnerGMDocTypeCode, string argPGoodsMovementCode, string argFromPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@PartnerGMDocTypeCode", argPartnerGMDocTypeCode);
            param[1] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[2] = new SqlParameter("@PartnerCode", argFromPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetPartnerGoodsIssue4Report", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerGMDetail4PurchaseReport(string argPartnerGMDocTypeCode, string argPGoodsMovementCode, string argFromPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@PartnerGMDocTypeCode", argPartnerGMDocTypeCode);
            param[1] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[2] = new SqlParameter("@PartnerCode", argFromPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetPartnerGoods4PurchaseReport", param);
            return DataSetToFill;
        }

        public DataSet GetErrorDataInNonSerialize(string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetErrorDataInNonSerialize", param);
            return DataSetToFill;
        }

        public DataSet GetErrorDataInSerialize(string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetErrorDataInSerialize", param);
            return DataSetToFill;
        }


       //---------------------------

    }
}