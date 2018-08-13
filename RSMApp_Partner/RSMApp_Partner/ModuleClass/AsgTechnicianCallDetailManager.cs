
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
    public class AsgTechnicianCallDetailManager
    {
        const string AsgTechnicianCallDetailTable = "AsgTechnicianCallDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public AsgTechnicianCallDetail objGetAsgTechnicianCallDetail(string argAsgTechCallCode, int argItemNo, string argPartnerCode, string argClientCode)
        {
            AsgTechnicianCallDetail argAsgTechnicianCallDetail = new AsgTechnicianCallDetail();
            DataSet DataSetToFill = new DataSet();

            if (argAsgTechCallCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argItemNo <= 0)
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

            DataSetToFill = this.GetAsgTechnicianCallDetail(argAsgTechCallCode, argItemNo, argPartnerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argAsgTechnicianCallDetail = this.objCreateAsgTechnicianCallDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argAsgTechnicianCallDetail;
        }
        
        //public ICollection<AsgTechnicianCallDetail> colGetAsgTechnicianCallDetail(string argAsgTechCallCode, string  argPartnerCode, string argClientCode)
        //{
        //    List<AsgTechnicianCallDetail> lst = new List<AsgTechnicianCallDetail>();
        //    DataSet DataSetToFill = new DataSet();
        //    AsgTechnicianCallDetail tAsgTechnicianCallDetail = new AsgTechnicianCallDetail();

        //    DataSetToFill = this.GetAsgTechnicianCallDetail(argAsgTechCallCode, argPartnerCode , argClientCode);

        //    if (DataSetToFill != null)
        //    {
        //        foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
        //        {
        //            lst.Add(objCreateAsgTechnicianCallDetail(dr));
        //        }
        //    }
        //    goto Finish;

        //Finish:
        //    DataSetToFill = null;


        //    return lst;
        //}

        public AsgTechnicianCallDetailCol colGetAsgTechnicianCallDetail(string argAsgTechCallCode, string argPartnerCode, string argClientCode)
        {
            AsgTechnicianCallDetailCol AsgTechCallDetailCol = new AsgTechnicianCallDetailCol();            
            DataSet DataSetToFill = new DataSet();
            AsgTechnicianCallDetail tAsgTechnicianCallDetail = new AsgTechnicianCallDetail();

            DataSetToFill = this.GetAsgTechnicianCallDetail(argAsgTechCallCode, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                   AsgTechCallDetailCol.colAsgTechnicianCallDetail.Add(objCreateAsgTechnicianCallDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;           
            

             return AsgTechCallDetailCol;
        }
        
        public void colGetAsgTechnicianCallDetail(string argAsgTechCallCode, string argPartnerCode, string argClientCode, ref AsgTechnicianCallDetailCol AsgTechCallDetailCol)
        {
            
            DataSet DataSetToFill = new DataSet();
            AsgTechnicianCallDetail tAsgTechnicianCallDetail = new AsgTechnicianCallDetail();

            DataSetToFill = this.GetAsgTechnicianCallDetail(argAsgTechCallCode, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    AsgTechCallDetailCol.colAsgTechnicianCallDetail.Add(objCreateAsgTechnicianCallDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;
                                  
        }

        public DataSet GetAsgTechnicianCallDetail(string argAsgTechCallCode, int argItemNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@AsgTechCallCode", argAsgTechCallCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAsgTechnicianCallDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetAsgTechnicianCallDetail(string argAsgTechCallCode, int argItemNo, string argPartnerCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@AsgTechCallCode", argAsgTechCallCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetAsgTechnicianCallDetail4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetAsgTechnicianCallDetail(string argAsgTechCallCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@AsgTechCallCode", argAsgTechCallCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAsgTechnicianCallDetail", param);
            return DataSetToFill;
        }

        public DataSet GetAsgTechnicianCallDetail4Search(string argDateFrom, string argDateTo, string argAsgTechDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@DateFrom", argDateFrom);
            param[1] = new SqlParameter("@DateTo", argDateTo);
            param[2] = new SqlParameter("@AsgTechDocTypeCode", argAsgTechDocTypeCode);
            param[3] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAsgTechnicianCallDetail4Search", param);
            return DataSetToFill;
        }

        

        public DataSet GetAsgTechnicianCall4Repair(string argSerialNo, string argMatGroup1Code, string argRepairBy, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@RepairBY", argRepairBy);
            param[1] = new SqlParameter("@SerialNo", argSerialNo);
            param[2] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[3] = new SqlParameter("@PartnerCode", argPartnerCode);            
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_GetCallList4Repair", param);
            return DataSetToFill;
        }
        
        private AsgTechnicianCallDetail objCreateAsgTechnicianCallDetail(DataRow dr)
        {
            AsgTechnicianCallDetail tAsgTechnicianCallDetail = new AsgTechnicianCallDetail();

            tAsgTechnicianCallDetail.SetObjectInfo(dr);

            return tAsgTechnicianCallDetail;

        }
        
        public ICollection<ErrorHandler> SaveAsgTechnicianCallDetail(AsgTechnicianCallDetail argAsgTechnicianCallDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsAsgTechnicianCallDetailExists(argAsgTechnicianCallDetail.AsgTechCallCode, argAsgTechnicianCallDetail.ItemNo, argAsgTechnicianCallDetail.PartnerCode, argAsgTechnicianCallDetail.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertAsgTechnicianCallDetail(argAsgTechnicianCallDetail, da, lstErr);
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
                    UpdateAsgTechnicianCallDetail(argAsgTechnicianCallDetail, da, lstErr);
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

        public void SaveAsgTechnicianCallDetail(AsgTechnicianCallDetail argAsgTechnicianCallDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsAsgTechnicianCallDetailExists(argAsgTechnicianCallDetail.AsgTechCallCode, argAsgTechnicianCallDetail.ItemNo, argAsgTechnicianCallDetail.PartnerCode, argAsgTechnicianCallDetail.ClientCode, da) == false)
                {
                    InsertAsgTechnicianCallDetail(argAsgTechnicianCallDetail, da, lstErr);
                }
                else
                {
                    UpdateAsgTechnicianCallDetail(argAsgTechnicianCallDetail, da, lstErr);
                }
            }
            catch (Exception ex)
            {
                objErrorHandler.Type = ErrorConstant.strAboartType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strInsertModule;
                objErrorHandler.ModulePart = ErrorConstant.strDetailModule;
                objErrorHandler.Message = ex.Message.ToString();
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";
                lstErr.Add(objErrorHandler);
            }
           
        }

        public void InsertAsgTechnicianCallDetail(AsgTechnicianCallDetail argAsgTechnicianCallDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[29];
            param[0] = new SqlParameter("@AsgTechCallCode", argAsgTechnicianCallDetail.AsgTechCallCode);
            param[1] = new SqlParameter("@ItemNo", argAsgTechnicianCallDetail.ItemNo);
            param[2] = new SqlParameter("@SerialNo1", argAsgTechnicianCallDetail.SerialNo1);
            param[3] = new SqlParameter("@SerialNo2", argAsgTechnicianCallDetail.SerialNo2);
            param[4] = new SqlParameter("@MaterialCode", argAsgTechnicianCallDetail.MaterialCode);
            param[5] = new SqlParameter("@MatGroup1Code", argAsgTechnicianCallDetail.MatGroup1Code);
            param[6] = new SqlParameter("@StoreCode", argAsgTechnicianCallDetail.StoreCode);
            param[7] = new SqlParameter("@StockIndicator", argAsgTechnicianCallDetail.StockIndicator);
            param[8] = new SqlParameter("@RefDocCode", argAsgTechnicianCallDetail.RefDocCode);
            param[9] = new SqlParameter("@RefDocItemNo", argAsgTechnicianCallDetail.RefDocItemNo);
            param[10] = new SqlParameter("@RefDocTypeCode", argAsgTechnicianCallDetail.RefDocTypeCode);
            param[11] = new SqlParameter("@Quantity", argAsgTechnicianCallDetail.Quantity);
            param[12] = new SqlParameter("@UOMCode", argAsgTechnicianCallDetail.UOMCode);
            param[13] = new SqlParameter("@PartnerCode", argAsgTechnicianCallDetail.PartnerCode);

            param[14] = new SqlParameter("@FromPartnerEmployeeCode", argAsgTechnicianCallDetail.FromPartnerEmployeeCode);

            param[15] = new SqlParameter("@ToPartnerCode", argAsgTechnicianCallDetail.ToPartnerCode);
            param[16] = new SqlParameter("@ToPartnerEmployeeCode", argAsgTechnicianCallDetail.ToPartnerEmployeeCode);
            param[17] = new SqlParameter("@ToStoreCode", argAsgTechnicianCallDetail.ToStoreCode);
            param[18] = new SqlParameter("@ToMaterialCode", argAsgTechnicianCallDetail.ToMaterialCode);
            param[19] = new SqlParameter("@ToStockIndicator", argAsgTechnicianCallDetail.ToStockIndicator);
            param[20] = new SqlParameter("@MaterialDocTypeCode", argAsgTechnicianCallDetail.MaterialDocTypeCode);

            param[21] = new SqlParameter("@PGoodsMovementCode", argAsgTechnicianCallDetail.PGoodsMovementCode);
            param[22] = new SqlParameter("@GMItemNo", argAsgTechnicianCallDetail.GMItemNo);
            
            param[23] = new SqlParameter("@ClientCode", argAsgTechnicianCallDetail.ClientCode);
            param[24] = new SqlParameter("@CreatedBy", argAsgTechnicianCallDetail.CreatedBy);
            param[25] = new SqlParameter("@ModifiedBy", argAsgTechnicianCallDetail.ModifiedBy);
            
            param[26] = new SqlParameter("@Type", SqlDbType.Char);
            param[26].Size = 1;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[27].Size = 255;
            param[27].Direction = ParameterDirection.Output;

            param[28] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[28].Size = 20;
            param[28].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertAsgTechnicianCallDetail", param);


            string strMessage = Convert.ToString(param[27].Value);
            string strType = Convert.ToString(param[26].Value);
            string strRetValue = Convert.ToString(param[28].Value);


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
        
        public void UpdateAsgTechnicianCallDetail(AsgTechnicianCallDetail argAsgTechnicianCallDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[29];
            param[0] = new SqlParameter("@AsgTechCallCode", argAsgTechnicianCallDetail.AsgTechCallCode);
            param[1] = new SqlParameter("@ItemNo", argAsgTechnicianCallDetail.ItemNo);
            param[2] = new SqlParameter("@SerialNo1", argAsgTechnicianCallDetail.SerialNo1);
            param[3] = new SqlParameter("@SerialNo2", argAsgTechnicianCallDetail.SerialNo2);
            param[4] = new SqlParameter("@MaterialCode", argAsgTechnicianCallDetail.MaterialCode);
            param[5] = new SqlParameter("@MatGroup1Code", argAsgTechnicianCallDetail.MatGroup1Code);
            param[6] = new SqlParameter("@StoreCode", argAsgTechnicianCallDetail.StoreCode);
            param[7] = new SqlParameter("@StockIndicator", argAsgTechnicianCallDetail.StockIndicator);
            param[8] = new SqlParameter("@RefDocCode", argAsgTechnicianCallDetail.RefDocCode);
            param[9] = new SqlParameter("@RefDocItemNo", argAsgTechnicianCallDetail.RefDocItemNo);
            param[10] = new SqlParameter("@RefDocTypeCode", argAsgTechnicianCallDetail.RefDocTypeCode);
            param[11] = new SqlParameter("@Quantity", argAsgTechnicianCallDetail.Quantity);
            param[12] = new SqlParameter("@UOMCode", argAsgTechnicianCallDetail.UOMCode);
            param[13] = new SqlParameter("@PartnerCode", argAsgTechnicianCallDetail.PartnerCode);

            param[14] = new SqlParameter("@FromPartnerEmployeeCode", argAsgTechnicianCallDetail.FromPartnerEmployeeCode);

            param[15] = new SqlParameter("@ToPartnerCode", argAsgTechnicianCallDetail.ToPartnerCode);
            param[16] = new SqlParameter("@ToPartnerEmployeeCode", argAsgTechnicianCallDetail.ToPartnerEmployeeCode);
            param[17] = new SqlParameter("@ToStoreCode", argAsgTechnicianCallDetail.ToStoreCode);
            param[18] = new SqlParameter("@ToMaterialCode", argAsgTechnicianCallDetail.ToMaterialCode);
            param[19] = new SqlParameter("@ToStockIndicator", argAsgTechnicianCallDetail.ToStockIndicator);
            param[20] = new SqlParameter("@MaterialDocTypeCode", argAsgTechnicianCallDetail.MaterialDocTypeCode);

            param[21] = new SqlParameter("@PGoodsMovementCode", argAsgTechnicianCallDetail.PGoodsMovementCode);
            param[22] = new SqlParameter("@GMItemNo", argAsgTechnicianCallDetail.GMItemNo);

            param[23] = new SqlParameter("@ClientCode", argAsgTechnicianCallDetail.ClientCode);
            param[24] = new SqlParameter("@CreatedBy", argAsgTechnicianCallDetail.CreatedBy);
            param[25] = new SqlParameter("@ModifiedBy", argAsgTechnicianCallDetail.ModifiedBy);

            param[26] = new SqlParameter("@Type", SqlDbType.Char);
            param[26].Size = 1;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[27].Size = 255;
            param[27].Direction = ParameterDirection.Output;

            param[28] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[28].Size = 20;
            param[28].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateAsgTechnicianCallDetail", param);


            string strMessage = Convert.ToString(param[27].Value);
            string strType = Convert.ToString(param[26].Value);
            string strRetValue = Convert.ToString(param[28].Value);


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
        
        public ICollection<ErrorHandler> DeleteAsgTechnicianCallDetail(string argAsgTechCallCode, int argItemNo, string argSerialNo1, string argMaterialCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@AsgTechCallCode", argAsgTechCallCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@SerialNo1", argSerialNo1);
                param[3] = new SqlParameter("@MaterialCode", argMaterialCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteAsgTechnicianCallDetail", param);


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

        public void DeleteAsgTechnicianCallDetail(string argAsgTechCallCode, int argItemNo, string argSerialNo1, string argMaterialCode, string argClientCode, DataAccess da, List<ErrorHandler> lstErr)
        {
            
            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@AsgTechCallCode", argAsgTechCallCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@SerialNo1", argSerialNo1);
                param[3] = new SqlParameter("@MaterialCode", argMaterialCode);
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
                int i = da.NExecuteNonQuery("Proc_DeleteAsgTechnicianCallDetail", param);


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

        public bool blnIsAsgTechnicianCallDetailExists(string argAsgTechCallCode, int argItemNo, string argPartnerCode, string argClientCode)
        {
            bool IsAsgTechnicianCallDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetAsgTechnicianCallDetail(argAsgTechCallCode, argItemNo, argPartnerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAsgTechnicianCallDetailExists = true;
            }
            else
            {
                IsAsgTechnicianCallDetailExists = false;
            }
            return IsAsgTechnicianCallDetailExists;
        }

        public bool blnIsAsgTechnicianCallDetailExists(string argAsgTechCallCode, int argItemNo,  string argPartnerCode, string argClientCode, DataAccess da)
        {
            bool IsAsgTechnicianCallDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetAsgTechnicianCallDetail(argAsgTechCallCode, argItemNo, argPartnerCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAsgTechnicianCallDetailExists = true;
            }
            else
            {
                IsAsgTechnicianCallDetailExists = false;
            }
            return IsAsgTechnicianCallDetailExists;
        }
    }
}