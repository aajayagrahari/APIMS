
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
    public class CanabalizeDocTypeManager
    {
        const string CanabalizeDocTypeTable = "CanabalizeDocType";
        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CanabalizeDocType objGetCanabalizeDocType(string argCanablizeDocTypeCode, string argClientCode)
        {
            CanabalizeDocType argCanabalizeDocType = new CanabalizeDocType();
            DataSet DataSetToFill = new DataSet();

            if (argCanablizeDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCanabalizeDocType(argCanablizeDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCanabalizeDocType = this.objCreateCanabalizeDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCanabalizeDocType;
        }
        
        public ICollection<CanabalizeDocType> colGetCanabalizeDocType(string argClientCode)
        {
            List<CanabalizeDocType> lst = new List<CanabalizeDocType>();
            DataSet DataSetToFill = new DataSet();
            CanabalizeDocType tCanabalizeDocType = new CanabalizeDocType();

            DataSetToFill = this.GetCanabalizeDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCanabalizeDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetCanabalizeDocType(string argCanablizeDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CanablizeDocTypeCode", argCanablizeDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCanabalizeDocType4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCanabalizeDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];            
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCanabalizeDocType", param);
            return DataSetToFill;
        }

        private CanabalizeDocType objCreateCanabalizeDocType(DataRow dr)
        {
            CanabalizeDocType tCanabalizeDocType = new CanabalizeDocType();

            tCanabalizeDocType.SetObjectInfo(dr);

            return tCanabalizeDocType;

        }
        
        public ICollection<ErrorHandler> SaveCanabalizeDocType(CanabalizeDocType argCanabalizeDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCanabalizeDocTypeExists(argCanabalizeDocType.CanablizeDocTypeCode, argCanabalizeDocType.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCanabalizeDocType(argCanabalizeDocType, da, lstErr);
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
                    UpdateCanabalizeDocType(argCanabalizeDocType, da, lstErr);
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
        
        public void InsertCanabalizeDocType(CanabalizeDocType argCanabalizeDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];

            param[0] = new SqlParameter("@CanablizeDocTypeCode", argCanabalizeDocType.CanablizeDocTypeCode);
            param[1] = new SqlParameter("@CanablizeDocTypeDesc", argCanabalizeDocType.CanablizeDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argCanabalizeDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argCanabalizeDocType.NumRange);
            param[4] = new SqlParameter("@MatMaterialDocTypeCode", argCanabalizeDocType.MatMaterialDocTypeCode);
            param[5] = new SqlParameter("@MatDefaultStoreCode", argCanabalizeDocType.MatDefaultStoreCode);
            param[6] = new SqlParameter("@MatDefaultFromStockIndicator", argCanabalizeDocType.MatDefaultFromStockIndicator);
            param[7] = new SqlParameter("@MatDefaultToStockIndicator", argCanabalizeDocType.MatDefaultToStockIndicator);
            param[8] = new SqlParameter("@SpareMaterialDocTypeCode", argCanabalizeDocType.SpareMaterialDocTypeCode);
            param[9] = new SqlParameter("@SpareDefaultStoreCode", argCanabalizeDocType.SpareDefaultStoreCode);
            param[10] = new SqlParameter("@SpareDefaultFromStockIndicator", argCanabalizeDocType.SpareDefaultFromStockIndicator);
            param[11] = new SqlParameter("@SpareDefaultToStockIndicator", argCanabalizeDocType.SpareDefaultToStockIndicator);
            param[12] = new SqlParameter("@ClientCode", argCanabalizeDocType.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argCanabalizeDocType.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argCanabalizeDocType.ModifiedBy);
         
            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCanabalizeDocType", param);
            
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


        public void UpdateCanabalizeDocType(CanabalizeDocType argCanabalizeDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@CanablizeDocTypeCode", argCanabalizeDocType.CanablizeDocTypeCode);
            param[1] = new SqlParameter("@CanablizeDocTypeDesc", argCanabalizeDocType.CanablizeDocTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argCanabalizeDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argCanabalizeDocType.NumRange);
            param[4] = new SqlParameter("@MatMaterialDocTypeCode", argCanabalizeDocType.MatMaterialDocTypeCode);
            param[5] = new SqlParameter("@MatDefaultStoreCode", argCanabalizeDocType.MatDefaultStoreCode);
            param[6] = new SqlParameter("@MatDefaultFromStockIndicator", argCanabalizeDocType.MatDefaultFromStockIndicator);
            param[7] = new SqlParameter("@MatDefaultToStockIndicator", argCanabalizeDocType.MatDefaultToStockIndicator);
            param[8] = new SqlParameter("@SpareMaterialDocTypeCode", argCanabalizeDocType.SpareMaterialDocTypeCode);
            param[9] = new SqlParameter("@SpareDefaultStoreCode", argCanabalizeDocType.SpareDefaultStoreCode);
            param[10] = new SqlParameter("@SpareDefaultFromStockIndicator", argCanabalizeDocType.SpareDefaultFromStockIndicator);
            param[11] = new SqlParameter("@SpareDefaultToStockIndicator", argCanabalizeDocType.SpareDefaultToStockIndicator);
            param[12] = new SqlParameter("@ClientCode", argCanabalizeDocType.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argCanabalizeDocType.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argCanabalizeDocType.ModifiedBy);
           
            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCanabalizeDocType", param);
            
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


        public ICollection<ErrorHandler> DeleteCanabalizeDocType(string argCanablizeDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@CanablizeDocTypeCode", argCanablizeDocTypeCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCanabalizeDocType", param);


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


        public bool blnIsCanabalizeDocTypeExists(string argCanablizeDocTypeCode, string argClientCode)
        {
            bool IsCanabalizeDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetCanabalizeDocType(argCanablizeDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCanabalizeDocTypeExists = true;
            }
            else
            {
                IsCanabalizeDocTypeExists = false;
            }
            return IsCanabalizeDocTypeExists;
        }
    }
}