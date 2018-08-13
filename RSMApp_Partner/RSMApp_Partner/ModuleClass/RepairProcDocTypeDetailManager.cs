
//Created On :: 20, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class RepairProcDocTypeDetailManager
    {
        const string RepairProcDocTypeDetailTable = "RepairProcDocTypeDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public RepairProcDocTypeDetail objGetRepairProcDocTypeDetail(string argRepairProcDocTypeCode, string argRepairProcType, string argClientCode)
        {
            RepairProcDocTypeDetail argRepairProcDocTypeDetail = new RepairProcDocTypeDetail();
            DataSet DataSetToFill = new DataSet();

            if (argRepairProcDocTypeCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argRepairProcType.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            DataSetToFill = this.GetRepairProcDocTypeDetail(argRepairProcDocTypeCode, argRepairProcType, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argRepairProcDocTypeDetail = this.objCreateRepairProcDocTypeDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandler:

        Finish:
            DataSetToFill = null;


            return argRepairProcDocTypeDetail;
        }

        public ICollection<RepairProcDocTypeDetail> colGetRepairProcDocTypeDetail(string argRepairProcDocTypeCode,  string argClientCode)
        {
            List<RepairProcDocTypeDetail> lst = new List<RepairProcDocTypeDetail>();
            DataSet DataSetToFill = new DataSet();
            RepairProcDocTypeDetail tRepairProcDocTypeDetail = new RepairProcDocTypeDetail();

            DataSetToFill = this.GetRepairProcDocTypeDetail(argRepairProcDocTypeCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateRepairProcDocTypeDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetRepairProcDocTypeDetail(string argRepairProcDocTypeCode, string argRepairProcType, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@RepairProcDocTypeCode", argRepairProcDocTypeCode);
            param[1] = new SqlParameter("@RepairProcType", argRepairProcType);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepairProcDocTypeDetail4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetRepairProcDocTypeDetail(string argRepairProcDocTypeCode,  string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@RepairProcDocTypeCode", argRepairProcDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRepairProcDocTypeDetail", param);
            return DataSetToFill;
        }
        
        private RepairProcDocTypeDetail objCreateRepairProcDocTypeDetail(DataRow dr)
        {
            RepairProcDocTypeDetail tRepairProcDocTypeDetail = new RepairProcDocTypeDetail();

            tRepairProcDocTypeDetail.SetObjectInfo(dr);

            return tRepairProcDocTypeDetail;

        }
        
        public ICollection<ErrorHandler> SaveRepairProcDocTypeDetail(RepairProcDocTypeDetail argRepairProcDocTypeDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsRepairProcDocTypeDetailExists(argRepairProcDocTypeDetail.RepairProcDocTypeCode, argRepairProcDocTypeDetail.RepairProcType, argRepairProcDocTypeDetail.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertRepairProcDocTypeDetail(argRepairProcDocTypeDetail, da, lstErr);
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
                    UpdateRepairProcDocTypeDetail(argRepairProcDocTypeDetail, da, lstErr);
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

        public void InsertRepairProcDocTypeDetail(RepairProcDocTypeDetail argRepairProcDocTypeDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@RepairProcDocTypeCode", argRepairProcDocTypeDetail.RepairProcDocTypeCode);
            param[1] = new SqlParameter("@RepairProcType", argRepairProcDocTypeDetail.RepairProcType);
            param[2] = new SqlParameter("@RepairProcessCode", argRepairProcDocTypeDetail.RepairProcessCode);
            param[3] = new SqlParameter("@MatMaterialDocTypeCode", argRepairProcDocTypeDetail.MatMaterialDocTypeCode);
            param[4] = new SqlParameter("@MatDefaultStoreCode", argRepairProcDocTypeDetail.MatDefaultStoreCode);
            param[5] = new SqlParameter("@MatDefaultStockIndicator", argRepairProcDocTypeDetail.MatDefaultStockIndicator);
            param[6] = new SqlParameter("@SpareMaterialDocTypeCode", argRepairProcDocTypeDetail.SpareMaterialDocTypeCode);
            param[7] = new SqlParameter("@SpareDefaultStoreCode", argRepairProcDocTypeDetail.SpareDefaultStoreCode);
            param[8] = new SqlParameter("@SpareDefaultStockIndicator", argRepairProcDocTypeDetail.SpareDefaultStockIndicator);
            param[9] = new SqlParameter("@DefectMaterialDocTypeCode", argRepairProcDocTypeDetail.DefectMaterialDocTypeCode);
            param[10] = new SqlParameter("@DefectDefaultStoreCode", argRepairProcDocTypeDetail.DefectDefaultStoreCode);
            param[11] = new SqlParameter("@DefectDefaultStockIndicator", argRepairProcDocTypeDetail.DefectDefaultStockIndicator);
            param[12] = new SqlParameter("@ClientCode", argRepairProcDocTypeDetail.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argRepairProcDocTypeDetail.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argRepairProcDocTypeDetail.ModifiedBy);
          
            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertRepairProcDocTypeDetail", param);


            string strMessage = Convert.ToString(param[16].Value);
            string strType = Convert.ToString(param[15].Value);
            string strRetValue = Convert.ToString(param[17].Value);


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

        public void UpdateRepairProcDocTypeDetail(RepairProcDocTypeDetail argRepairProcDocTypeDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@RepairProcDocTypeCode", argRepairProcDocTypeDetail.RepairProcDocTypeCode);
            param[1] = new SqlParameter("@RepairProcType", argRepairProcDocTypeDetail.RepairProcType);
            param[2] = new SqlParameter("@RepairProcessCode", argRepairProcDocTypeDetail.RepairProcessCode);
            param[3] = new SqlParameter("@MatMaterialDocTypeCode", argRepairProcDocTypeDetail.MatMaterialDocTypeCode);
            param[4] = new SqlParameter("@MatDefaultStoreCode", argRepairProcDocTypeDetail.MatDefaultStoreCode);
            param[5] = new SqlParameter("@MatDefaultStockIndicator", argRepairProcDocTypeDetail.MatDefaultStockIndicator);
            param[6] = new SqlParameter("@SpareMaterialDocTypeCode", argRepairProcDocTypeDetail.SpareMaterialDocTypeCode);
            param[7] = new SqlParameter("@SpareDefaultStoreCode", argRepairProcDocTypeDetail.SpareDefaultStoreCode);
            param[8] = new SqlParameter("@SpareDefaultStockIndicator", argRepairProcDocTypeDetail.SpareDefaultStockIndicator);
            param[9] = new SqlParameter("@DefectMaterialDocTypeCode", argRepairProcDocTypeDetail.DefectMaterialDocTypeCode);
            param[10] = new SqlParameter("@DefectDefaultStoreCode", argRepairProcDocTypeDetail.DefectDefaultStoreCode);
            param[11] = new SqlParameter("@DefectDefaultStockIndicator", argRepairProcDocTypeDetail.DefectDefaultStockIndicator);
            param[12] = new SqlParameter("@ClientCode", argRepairProcDocTypeDetail.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argRepairProcDocTypeDetail.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argRepairProcDocTypeDetail.ModifiedBy);
         
            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateRepairProcDocTypeDetail", param);


            string strMessage = Convert.ToString(param[16].Value);
            string strType = Convert.ToString(param[15].Value);
            string strRetValue = Convert.ToString(param[17].Value);


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

        public ICollection<ErrorHandler> DeleteRepairProcDocTypeDetail(string argRepairProcDocTypeCode, string argRepairProcType, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@RepairProcDocTypeCode", argRepairProcDocTypeCode);
                param[1] = new SqlParameter("@RepairProcType", argRepairProcType);
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
                int i = da.ExecuteNonQuery("Proc_DeleteRepairProcDocTypeDetail", param);


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


        public bool blnIsRepairProcDocTypeDetailExists(string argRepairProcDocTypeCode, string argRepairProcType, string argClientCode)
        {
            bool IsRepairProcDocTypeDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetRepairProcDocTypeDetail(argRepairProcDocTypeCode, argRepairProcType, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsRepairProcDocTypeDetailExists = true;
            }
            else
            {
                IsRepairProcDocTypeDetailExists = false;
            }
            return IsRepairProcDocTypeDetailExists;
        }
    }
}