
//Created On :: 29, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class PartnerGMDocTypeManager
    {
        const string PartnerGMDocTypeTable = "PartnerGMDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public PartnerGMDocType objGetPartnerGMDocType(string argPartnerGMDocTypeCode, string argClientCode)
        {
            PartnerGMDocType argPartnerGMDocType = new PartnerGMDocType();
            DataSet DataSetToFill = new DataSet();

            if (argPartnerGMDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPartnerGMDocType(argPartnerGMDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartnerGMDocType = this.objCreatePartnerGMDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPartnerGMDocType;
        }


        public ICollection<PartnerGMDocType> colGetPartnerGMDocType(string argClientCode)
        {
            List<PartnerGMDocType> lst = new List<PartnerGMDocType>();
            DataSet DataSetToFill = new DataSet();
            PartnerGMDocType tPartnerGMDocType = new PartnerGMDocType();

            DataSetToFill = this.GetPartnerGMDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePartnerGMDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetPartnerGMDocType(string argPartnerGMDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerGMDocTypeCode", argPartnerGMDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGMDocType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerGMDocType(string argPartnerGMDocTypeCode, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerGMDocTypeCode", argPartnerGMDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPartnerGMDocType4ID", param);

            return DataSetToFill;
        }


        public DataSet GetPartnerGMDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerGMDocType", param);
            return DataSetToFill;
        }


        private PartnerGMDocType objCreatePartnerGMDocType(DataRow dr)
        {
            PartnerGMDocType tPartnerGMDocType = new PartnerGMDocType();

            tPartnerGMDocType.SetObjectInfo(dr);

            return tPartnerGMDocType;

        }


        public ICollection<ErrorHandler> SavePartnerGMDocType(PartnerGMDocType argPartnerGMDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartnerGMDocTypeExists(argPartnerGMDocType.PartnerGMDocTypeCode, argPartnerGMDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartnerGMDocType(argPartnerGMDocType, da, lstErr);
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
                    UpdatePartnerGMDocType(argPartnerGMDocType, da, lstErr);
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


        public void InsertPartnerGMDocType(PartnerGMDocType argPartnerGMDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[20];

            param[0] = new SqlParameter("@PartnerGMDocTypeCode", argPartnerGMDocType.PartnerGMDocTypeCode);
            param[1] = new SqlParameter("@PartnerGMDocTypeDesc", argPartnerGMDocType.PartnerGMDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argPartnerGMDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argPartnerGMDocType.NumRange);
            param[4] = new SqlParameter("@AssignType", argPartnerGMDocType.AssignType);
            param[5] = new SqlParameter("@MaterialDocTypeCode", argPartnerGMDocType.MaterialDocTypeCode);
            param[6] = new SqlParameter("@RepairProcessCode", argPartnerGMDocType.RepairProcessCode);
            param[7] = new SqlParameter("@DefaultFromStoreCode", argPartnerGMDocType.DefaultFromStoreCode);
            param[8] = new SqlParameter("@DefaultFromStockIndicator", argPartnerGMDocType.DefaultFromStockIndicator);
            param[9] = new SqlParameter("@DefaultToStoreCode", argPartnerGMDocType.DefaultToStoreCode);
            param[10] = new SqlParameter("@DefaultToStockIndicator", argPartnerGMDocType.DefaultToStockIndicator);
            param[11] = new SqlParameter("@AgainstRO", argPartnerGMDocType.AgainstRO);
            param[12] = new SqlParameter("@CreateRepOrder", argPartnerGMDocType.CreateRepOrder);
            param[13] = new SqlParameter("@RecAgainstIssueDoc", argPartnerGMDocType.RecAgainstIssueDoc);
            param[14] = new SqlParameter("@ClientCode", argPartnerGMDocType.ClientCode);
            param[15] = new SqlParameter("@CreatedBy", argPartnerGMDocType.CreatedBy);
            param[16] = new SqlParameter("@ModifiedBy", argPartnerGMDocType.ModifiedBy);
           
            param[17] = new SqlParameter("@Type", SqlDbType.Char);
            param[17].Size = 1;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[18].Size = 255;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[19].Size = 20;
            param[19].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPartnerGMDocType", param);


            string strMessage = Convert.ToString(param[18].Value);
            string strType = Convert.ToString(param[17].Value);
            string strRetValue = Convert.ToString(param[19].Value);


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


        public void UpdatePartnerGMDocType(PartnerGMDocType argPartnerGMDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@PartnerGMDocTypeCode", argPartnerGMDocType.PartnerGMDocTypeCode);
            param[1] = new SqlParameter("@PartnerGMDocTypeDesc", argPartnerGMDocType.PartnerGMDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argPartnerGMDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argPartnerGMDocType.NumRange);
            param[4] = new SqlParameter("@AssignType", argPartnerGMDocType.AssignType);
            param[5] = new SqlParameter("@MaterialDocTypeCode", argPartnerGMDocType.MaterialDocTypeCode);
            param[6] = new SqlParameter("@RepairProcessCode", argPartnerGMDocType.RepairProcessCode);
            param[7] = new SqlParameter("@DefaultFromStoreCode", argPartnerGMDocType.DefaultFromStoreCode);
            param[8] = new SqlParameter("@DefaultFromStockIndicator", argPartnerGMDocType.DefaultFromStockIndicator);
            param[9] = new SqlParameter("@DefaultToStoreCode", argPartnerGMDocType.DefaultToStoreCode);
            param[10] = new SqlParameter("@DefaultToStockIndicator", argPartnerGMDocType.DefaultToStockIndicator);
            param[11] = new SqlParameter("@AgainstRO", argPartnerGMDocType.AgainstRO);
            param[12] = new SqlParameter("@CreateRepOrder", argPartnerGMDocType.CreateRepOrder);
            param[13] = new SqlParameter("@RecAgainstIssueDoc", argPartnerGMDocType.RecAgainstIssueDoc);
            param[14] = new SqlParameter("@ClientCode", argPartnerGMDocType.ClientCode);
            param[15] = new SqlParameter("@CreatedBy", argPartnerGMDocType.CreatedBy);
            param[16] = new SqlParameter("@ModifiedBy", argPartnerGMDocType.ModifiedBy);
 
            param[17] = new SqlParameter("@Type", SqlDbType.Char);
            param[17].Size = 1;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[18].Size = 255;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[19].Size = 20;
            param[19].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePartnerGMDocType", param);


            string strMessage = Convert.ToString(param[18].Value);
            string strType = Convert.ToString(param[17].Value);
            string strRetValue = Convert.ToString(param[19].Value);


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


        public ICollection<ErrorHandler> DeletePartnerGMDocType(string argPartnerGMDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@PartnerGMDocTypeCode", argPartnerGMDocTypeCode);
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
                int i = da.ExecuteNonQuery("Proc_DeletePartnerGMDocType", param);


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


        public bool blnIsPartnerGMDocTypeExists(string argPartnerGMDocTypeCode, string argClientCode)
        {
            bool IsPartnerGMDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerGMDocType(argPartnerGMDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerGMDocTypeExists = true;
            }
            else
            {
                IsPartnerGMDocTypeExists = false;
            }
            return IsPartnerGMDocTypeExists;
        }
    }
}