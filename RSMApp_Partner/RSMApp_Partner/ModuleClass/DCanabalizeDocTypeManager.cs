
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
    public class DCanabalizeDocTypeManager
    {
        const string DCanabalizeDocTypeTable = "DCanabalizeDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public DCanabalizeDocType objGetDCanabalizeDocType(string argDCanablizeDocTypeCode, string argClientCode)
        {
            DCanabalizeDocType argDCanabalizeDocType = new DCanabalizeDocType();
            DataSet DataSetToFill = new DataSet();

            if (argDCanablizeDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetDCanabalizeDocType(argDCanablizeDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argDCanabalizeDocType = this.objCreateDCanabalizeDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argDCanabalizeDocType;
        }


        public ICollection<DCanabalizeDocType> colGetDCanabalizeDocType(string argClientCode)
        {
            List<DCanabalizeDocType> lst = new List<DCanabalizeDocType>();
            DataSet DataSetToFill = new DataSet();
            DCanabalizeDocType tDCanabalizeDocType = new DCanabalizeDocType();

            DataSetToFill = this.GetDCanabalizeDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDCanabalizeDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetDCanabalizeDocType(string argDCanablizeDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DCanablizeDocTypeCode", argDCanablizeDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDCanabalizeDocType4ID", param);

            return DataSetToFill;
        }


        public DataSet GetDCanabalizeDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDCanabalizeDocType",param);
            return DataSetToFill;
        }


        private DCanabalizeDocType objCreateDCanabalizeDocType(DataRow dr)
        {
            DCanabalizeDocType tDCanabalizeDocType = new DCanabalizeDocType();

            tDCanabalizeDocType.SetObjectInfo(dr);

            return tDCanabalizeDocType;

        }


        public ICollection<ErrorHandler> SaveDCanabalizeDocType(DCanabalizeDocType argDCanabalizeDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsDCanabalizeDocTypeExists(argDCanabalizeDocType.DCanablizeDocTypeCode, argDCanabalizeDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertDCanabalizeDocType(argDCanabalizeDocType, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
                    }
                    da.COMMIT_TRANSACTION();
                }
                else
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    UpdateDCanabalizeDocType(argDCanabalizeDocType, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
                    }
                    da.COMMIT_TRANSACTION();
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


        public void InsertDCanabalizeDocType(DCanabalizeDocType argDCanabalizeDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@DCanablizeDocTypeCode", argDCanabalizeDocType.DCanablizeDocTypeCode);
            param[1] = new SqlParameter("@DCanablizeDocTypeDesc", argDCanabalizeDocType.DCanablizeDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argDCanabalizeDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argDCanabalizeDocType.NumRange);
            param[4] = new SqlParameter("@MatMaterialDocTypeCode", argDCanabalizeDocType.MatMaterialDocTypeCode);
            param[5] = new SqlParameter("@MatDefaultStoreCode", argDCanabalizeDocType.MatDefaultStoreCode);
            param[6] = new SqlParameter("@MatDefaultFromStockIndicator", argDCanabalizeDocType.MatDefaultFromStockIndicator);
            param[7] = new SqlParameter("@MatDefaultToStockIndicator", argDCanabalizeDocType.MatDefaultToStockIndicator);
            param[8] = new SqlParameter("@SpareMaterialDocTypeCode", argDCanabalizeDocType.SpareMaterialDocTypeCode);
            param[9] = new SqlParameter("@SpareDefaultStoreCode", argDCanabalizeDocType.SpareDefaultStoreCode);
            param[10] = new SqlParameter("@SpareDefaultFromStockIndicator", argDCanabalizeDocType.SpareDefaultFromStockIndicator);
            param[11] = new SqlParameter("@SpareDefaultToStockIndicator", argDCanabalizeDocType.SpareDefaultToStockIndicator);
            param[12] = new SqlParameter("@ClientCode", argDCanabalizeDocType.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argDCanabalizeDocType.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argDCanabalizeDocType.ModifiedBy);
    

            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertDCanabalizeDocType", param);


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


        public void UpdateDCanabalizeDocType(DCanabalizeDocType argDCanabalizeDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@DCanablizeDocTypeCode", argDCanabalizeDocType.DCanablizeDocTypeCode);
            param[1] = new SqlParameter("@DCanablizeDocTypeDesc", argDCanabalizeDocType.DCanablizeDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argDCanabalizeDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argDCanabalizeDocType.NumRange);
            param[4] = new SqlParameter("@MatMaterialDocTypeCode", argDCanabalizeDocType.MatMaterialDocTypeCode);
            param[5] = new SqlParameter("@MatDefaultStoreCode", argDCanabalizeDocType.MatDefaultStoreCode);
            param[6] = new SqlParameter("@MatDefaultFromStockIndicator", argDCanabalizeDocType.MatDefaultFromStockIndicator);
            param[7] = new SqlParameter("@MatDefaultToStockIndicator", argDCanabalizeDocType.MatDefaultToStockIndicator);
            param[8] = new SqlParameter("@SpareMaterialDocTypeCode", argDCanabalizeDocType.SpareMaterialDocTypeCode);
            param[9] = new SqlParameter("@SpareDefaultStoreCode", argDCanabalizeDocType.SpareDefaultStoreCode);
            param[10] = new SqlParameter("@SpareDefaultFromStockIndicator", argDCanabalizeDocType.SpareDefaultFromStockIndicator);
            param[11] = new SqlParameter("@SpareDefaultToStockIndicator", argDCanabalizeDocType.SpareDefaultToStockIndicator);
            param[12] = new SqlParameter("@ClientCode", argDCanabalizeDocType.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argDCanabalizeDocType.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argDCanabalizeDocType.ModifiedBy);


            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateDCanabalizeDocType", param);


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


        public ICollection<ErrorHandler> DeleteDCanabalizeDocType(string argDCanablizeDocTypeCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@DCanablizeDocTypeCode", argDCanablizeDocTypeCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteDCanabalizeDocType", param);


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


        public bool blnIsDCanabalizeDocTypeExists(string argDCanablizeDocTypeCode, string argClientCode)
        {
            bool IsDCanabalizeDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetDCanabalizeDocType(argDCanablizeDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDCanabalizeDocTypeExists = true;
            }
            else
            {
                IsDCanabalizeDocTypeExists = false;
            }
            return IsDCanabalizeDocTypeExists;
        }
    }
}