
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
    public class PartnerGoodsMovSerialDetailManager
    {
        const string PartnerGoodsMovSerialDetailTable = "PartnerGoodsMovSerialDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public PartnerGoodsMovSerialDetail objGetPartnerGoodsMovSerialDetail(string argPGoodsMovementCode, int argItemNo, string argMaterialCode, string argSerialNo1, string argClientCode)
        {
            PartnerGoodsMovSerialDetail argPartnerGoodsMovSerialDetail = new PartnerGoodsMovSerialDetail();
            DataSet DataSetToFill = new DataSet();

            if (argPGoodsMovementCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argItemNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argMaterialCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argSerialNo1.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPartnerGoodsMovSerialDetail(argPGoodsMovementCode, argItemNo, argMaterialCode, argSerialNo1, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartnerGoodsMovSerialDetail = this.objCreatePartnerGoodsMovSerialDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPartnerGoodsMovSerialDetail;
        }
        
        //public ICollection<PartnerGoodsMovSerialDetail> colGetPartnerGoodsMovSerialDetail(string argPGoodsMovementCode, int argItemNo, string argClientCode)
        //{
        //    List<PartnerGoodsMovSerialDetail> lst = new List<PartnerGoodsMovSerialDetail>();
        //    DataSet DataSetToFill = new DataSet();
        //    PartnerGoodsMovSerialDetail tPartnerGoodsMovSerialDetail = new PartnerGoodsMovSerialDetail();

        //    DataSetToFill = this.GetPartnerGoodsMovSerialDetail(argPGoodsMovementCode, argItemNo, argClientCode);

        //    if (DataSetToFill != null)
        //    {
        //        foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
        //        {
        //            lst.Add(objCreatePartnerGoodsMovSerialDetail(dr));
        //        }
        //    }
        //    goto Finish;

        //Finish:
        //    DataSetToFill = null;


        //    return lst;
        //}

        public void colGetPartnerGoodsMovSerialDetail(string argPGoodsMovementCode,  string argClientCode, ref PartnerGoodsMovSerialDetailCol argPartnerGoodsMovSerialDetailCol)
        {            
            DataSet DataSetToFill = new DataSet();
            PartnerGoodsMovSerialDetail tPartnerGoodsMovSerialDetail = new PartnerGoodsMovSerialDetail();

            DataSetToFill = this.GetPartnerGoodsMovSerialDetail(argPGoodsMovementCode,  argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argPartnerGoodsMovSerialDetailCol.colPartnerGMSerialDetail.Add(objCreatePartnerGoodsMovSerialDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;
        }

        
        public DataSet GetPartnerGoodsMovSerialDetail(string argPGoodsMovementCode, int argItemNo, string argMaterialCode, string argSerialNo1, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[3] = new SqlParameter("@SerialNo1", argSerialNo1);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGoodsMovSerialDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerGoodsMovSerialDetail(string argPGoodsMovementCode, int argItemNo, string argMaterialCode, string argSerialNo1, string argClientCode, DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[3] = new SqlParameter("@SerialNo1", argSerialNo1);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPartnerGoodsMovSerialDetail4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetPartnerGoodsMovSerialDetail(string argPGoodsMovementCode, int argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGoodsMovSerialDetail", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerGoodsMovSerialDetail(string argPGoodsMovementCode,  string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGoodsMovSerialDetail4GM", param);
            return DataSetToFill;
        }


        public DataSet GetPartnerGoodsMovSerialDetail4Rec(string argPGoodsMovementCode, string argFromPartnerCode, string argToPartnerCode,  string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[1] = new SqlParameter("@FromPartnerCode", argFromPartnerCode);
            param[2] = new SqlParameter("@ToPartnerCode", argToPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGMSerialDetail4Receipt", param);
            return DataSetToFill;
        }
        
        private PartnerGoodsMovSerialDetail objCreatePartnerGoodsMovSerialDetail(DataRow dr)
        {
            PartnerGoodsMovSerialDetail tPartnerGoodsMovSerialDetail = new PartnerGoodsMovSerialDetail();

            tPartnerGoodsMovSerialDetail.SetObjectInfo(dr);

            return tPartnerGoodsMovSerialDetail;

        }
        
        public ICollection<ErrorHandler> SavePartnerGoodsMovSerialDetail(PartnerGoodsMovSerialDetail argPartnerGoodsMovSerialDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartnerGoodsMovSerialDetailExists(argPartnerGoodsMovSerialDetail.PGoodsMovementCode, argPartnerGoodsMovSerialDetail.ItemNo, argPartnerGoodsMovSerialDetail.MaterialCode, argPartnerGoodsMovSerialDetail.SerialNo1, argPartnerGoodsMovSerialDetail.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartnerGoodsMovSerialDetail(argPartnerGoodsMovSerialDetail, da, lstErr);
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
                    UpdatePartnerGoodsMovSerialDetail(argPartnerGoodsMovSerialDetail, da, lstErr);
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

        public void SavePartnerGoodsMovSerialDetail(PartnerGoodsMovSerialDetail argPartnerGoodsMovSerialDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsPartnerGoodsMovSerialDetailExists(argPartnerGoodsMovSerialDetail.PGoodsMovementCode, argPartnerGoodsMovSerialDetail.ItemNo, argPartnerGoodsMovSerialDetail.MaterialCode, argPartnerGoodsMovSerialDetail.SerialNo1, argPartnerGoodsMovSerialDetail.ClientCode, da) == false)
                {
                    InsertPartnerGoodsMovSerialDetail(argPartnerGoodsMovSerialDetail, da, lstErr);
                }
                else
                {
                    //UpdatePartnerGoodsMovSerialDetail(argPartnerGoodsMovSerialDetail, da, lstErr);
                }
            }
            catch(Exception ex)
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

        public void InsertPartnerGoodsMovSerialDetail(PartnerGoodsMovSerialDetail argPartnerGoodsMovSerialDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[28];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPartnerGoodsMovSerialDetail.PGoodsMovementCode);
            param[1] = new SqlParameter("@ItemNo", argPartnerGoodsMovSerialDetail.ItemNo);
            param[2] = new SqlParameter("@MaterialCode", argPartnerGoodsMovSerialDetail.MaterialCode);
            param[3] = new SqlParameter("@MatGroup1Code", argPartnerGoodsMovSerialDetail.MatGroup1Code);
            param[4] = new SqlParameter("@SerialNo1", argPartnerGoodsMovSerialDetail.SerialNo1);
            param[5] = new SqlParameter("@SerialNo2", argPartnerGoodsMovSerialDetail.SerialNo2);
            param[6] = new SqlParameter("@PartnerCode", argPartnerGoodsMovSerialDetail.PartnerCode);
            param[7] = new SqlParameter("@StoreCode", argPartnerGoodsMovSerialDetail.StoreCode);
            param[8] = new SqlParameter("@PartnerEmployeeCode", argPartnerGoodsMovSerialDetail.PartnerEmployeeCode);
            param[9] = new SqlParameter("@PlantCode", argPartnerGoodsMovSerialDetail.PlantCode);
            param[10] = new SqlParameter("@StockIndicator", argPartnerGoodsMovSerialDetail.StockIndicator);

            param[11] = new SqlParameter("@ToPlantCode", argPartnerGoodsMovSerialDetail.ToPlantCode);
            param[12] = new SqlParameter("@ToPartnerCode", argPartnerGoodsMovSerialDetail.ToPartnerCode);
            param[13] = new SqlParameter("@ToStoreCode", argPartnerGoodsMovSerialDetail.ToStoreCode);
            param[14] = new SqlParameter("@ToStockIndicator", argPartnerGoodsMovSerialDetail.ToStockIndicator);
            param[15] = new SqlParameter("@ToPartnerEmployeeCode", argPartnerGoodsMovSerialDetail.ToPartnerEmployeeCode);
            param[16] = new SqlParameter("@ToMaterialCode", argPartnerGoodsMovSerialDetail.ToMaterialCode);


            param[17] = new SqlParameter("@RefDocCode", argPartnerGoodsMovSerialDetail.RefDocCode);
            param[18] = new SqlParameter("@RefDocItemNo", argPartnerGoodsMovSerialDetail.RefDocItemNo);
            param[19] = new SqlParameter("@RefDocType", argPartnerGoodsMovSerialDetail.RefDocType);

            param[20] = new SqlParameter("@TranRefDocCode", argPartnerGoodsMovSerialDetail.TranRefDocCode);
            param[21] = new SqlParameter("@TranRefDocItemNo", argPartnerGoodsMovSerialDetail.TranRefDocItemNo);

            param[22] = new SqlParameter("@ClientCode", argPartnerGoodsMovSerialDetail.ClientCode);
            param[23] = new SqlParameter("@CreatedBy", argPartnerGoodsMovSerialDetail.CreatedBy);
            param[24] = new SqlParameter("@ModifiedBy", argPartnerGoodsMovSerialDetail.ModifiedBy);
           
            param[25] = new SqlParameter("@Type", SqlDbType.Char);
            param[25].Size = 1;
            param[25].Direction = ParameterDirection.Output;

            param[26] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[26].Size = 255;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[27].Size = 20;
            param[27].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPartnerGoodsMovSerialDetail", param);


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
        
        public void UpdatePartnerGoodsMovSerialDetail(PartnerGoodsMovSerialDetail argPartnerGoodsMovSerialDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[28];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPartnerGoodsMovSerialDetail.PGoodsMovementCode);
            param[1] = new SqlParameter("@ItemNo", argPartnerGoodsMovSerialDetail.ItemNo);
            param[2] = new SqlParameter("@MaterialCode", argPartnerGoodsMovSerialDetail.MaterialCode);
            param[3] = new SqlParameter("@MatGroup1Code", argPartnerGoodsMovSerialDetail.MatGroup1Code);
            param[4] = new SqlParameter("@SerialNo1", argPartnerGoodsMovSerialDetail.SerialNo1);
            param[5] = new SqlParameter("@SerialNo2", argPartnerGoodsMovSerialDetail.SerialNo2);
            param[6] = new SqlParameter("@PartnerCode", argPartnerGoodsMovSerialDetail.PartnerCode);
            param[7] = new SqlParameter("@StoreCode", argPartnerGoodsMovSerialDetail.StoreCode);
            param[8] = new SqlParameter("@PartnerEmployeeCode", argPartnerGoodsMovSerialDetail.PartnerEmployeeCode);
            param[9] = new SqlParameter("@PlantCode", argPartnerGoodsMovSerialDetail.PlantCode);
            param[10] = new SqlParameter("@StockIndicator", argPartnerGoodsMovSerialDetail.StockIndicator);

            param[11] = new SqlParameter("@ToPlantCode", argPartnerGoodsMovSerialDetail.ToPlantCode);
            param[12] = new SqlParameter("@ToPartnerCode", argPartnerGoodsMovSerialDetail.ToPartnerCode);
            param[13] = new SqlParameter("@ToStoreCode", argPartnerGoodsMovSerialDetail.ToStoreCode);
            param[14] = new SqlParameter("@ToStockIndicator", argPartnerGoodsMovSerialDetail.ToStockIndicator);
            param[15] = new SqlParameter("@ToPartnerEmployeeCode", argPartnerGoodsMovSerialDetail.ToPartnerEmployeeCode);
            param[16] = new SqlParameter("@ToMaterialCode", argPartnerGoodsMovSerialDetail.ToMaterialCode);


            param[17] = new SqlParameter("@RefDocCode", argPartnerGoodsMovSerialDetail.RefDocCode);
            param[18] = new SqlParameter("@RefDocItemNo", argPartnerGoodsMovSerialDetail.RefDocItemNo);
            param[19] = new SqlParameter("@RefDocType", argPartnerGoodsMovSerialDetail.RefDocType);

            param[20] = new SqlParameter("@TranRefDocCode", argPartnerGoodsMovSerialDetail.TranRefDocCode);
            param[21] = new SqlParameter("@TranRefDocItemNo", argPartnerGoodsMovSerialDetail.TranRefDocItemNo);

            param[22] = new SqlParameter("@ClientCode", argPartnerGoodsMovSerialDetail.ClientCode);
            param[23] = new SqlParameter("@CreatedBy", argPartnerGoodsMovSerialDetail.CreatedBy);
            param[24] = new SqlParameter("@ModifiedBy", argPartnerGoodsMovSerialDetail.ModifiedBy);

            param[25] = new SqlParameter("@Type", SqlDbType.Char);
            param[25].Size = 1;
            param[25].Direction = ParameterDirection.Output;

            param[26] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[26].Size = 255;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[27].Size = 20;
            param[27].Direction = ParameterDirection.Output;


            int i = da.NExecuteNonQuery("Proc_UpdatePartnerGoodsMovSerialDetail", param);


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
        
        public ICollection<ErrorHandler> DeletePartnerGoodsMovSerialDetail(string argPGoodsMovementCode, int argItemNo, string argMaterialCode, string argSerialNo1, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@MaterialCode", argMaterialCode);
                param[3] = new SqlParameter("@SerialNo1", argSerialNo1);
                param[4] = new SqlParameter("@ClientCode", argClientCode);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;
                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;
                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeletePartnerGoodsMovSerialDetail", param);


                string strMessage = Convert.ToString(param[6].Value);
                string strType = Convert.ToString(param[5].Value);
                string strRetValue = Convert.ToString(param[7].Value);


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

        public void DeletePartnerGoodsMovSerialDetail(string argPGoodsMovementCode, int argItemNo, string argMaterialCode, string argSerialNo1, string argClientCode, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@MaterialCode", argMaterialCode);
                param[3] = new SqlParameter("@SerialNo1", argSerialNo1);
                param[4] = new SqlParameter("@ClientCode", argClientCode);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;
                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;
                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;
                int i = da.NExecuteNonQuery("Proc_DeletePartnerGoodsMovSerialDetail", param);


                string strMessage = Convert.ToString(param[6].Value);
                string strType = Convert.ToString(param[5].Value);
                string strRetValue = Convert.ToString(param[7].Value);


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

        public bool blnIsPartnerGoodsMovSerialDetailExists(string argPGoodsMovementCode, int argItemNo, string argMaterialCode, string argSerialNo1, string argClientCode)
        {
            bool IsPartnerGoodsMovSerialDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerGoodsMovSerialDetail(argPGoodsMovementCode, argItemNo, argMaterialCode, argSerialNo1, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerGoodsMovSerialDetailExists = true;
            }
            else
            {
                IsPartnerGoodsMovSerialDetailExists = false;
            }
            return IsPartnerGoodsMovSerialDetailExists;
        }

        public bool blnIsPartnerGoodsMovSerialDetailExists(string argPGoodsMovementCode, int argItemNo, string argMaterialCode, string argSerialNo1, string argClientCode, DataAccess da)
        {
            bool IsPartnerGoodsMovSerialDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerGoodsMovSerialDetail(argPGoodsMovementCode, argItemNo, argMaterialCode, argSerialNo1, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerGoodsMovSerialDetailExists = true;
            }
            else
            {
                IsPartnerGoodsMovSerialDetailExists = false;
            }
            return IsPartnerGoodsMovSerialDetailExists;
        }

        //----------------------------------------------------Report Section----------------------------------------------------//
        // For Issue For Replacement Report

        public DataSet GetPartnerGMDetail4SerilizeReport(string argPartnerGMDocTypeCode, string argFromPartnerCode, string argToPartnerCode, string argFromDate, string argToDate, string argMatGroup1Code, string argClientCode)
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

            DataSetToFill = da.FillDataSet("RP_GetPartnerGoodsMovement4SerializeReport", param);
            return DataSetToFill;
        }

        //---------------------------------------------------End Report Section---------------------------------------------------//
        

        //==========Ashutosh

        public void colGetPartnerGoodsMovTempSerialDetail(string argPGoodsMovementCode, string argPartnerCode, string argClientCode, ref PartnerGoodsMovSerialDetailCol argPartnerGoodsMovSerialDetailCol)
        {
            DataSet DataSetToFill = new DataSet();
            PartnerGoodsMovSerialDetail tPartnerGoodsMovSerialDetail = new PartnerGoodsMovSerialDetail();

            DataSetToFill = this.GetPartnerGoodsMovSerialTempDetail(argPGoodsMovementCode, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argPartnerGoodsMovSerialDetailCol.colPartnerGMSerialDetail.Add(objCreatePartnerGoodsMovSerialDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;
        }

        public DataSet GetPartnerGoodsMovSerialTempDetail(string argPGoodsMovementCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PGoodsMovementCode", argPGoodsMovementCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGoodsMovTempSerialDetail4GM", param);
            return DataSetToFill;
        }
        




        //----



    }
}