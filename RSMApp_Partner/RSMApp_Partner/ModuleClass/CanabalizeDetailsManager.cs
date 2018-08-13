
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
    public class CanabalizeDetailsManager
    {
        const string CanabalizeDetailsTable = "CanabalizeDetails";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public CanabalizeDetails objGetCanabalizeDetails(string argCanabalizeDocCode, int argCanabalizeItemNo, string argPartnerCode, string argClientCode)
        {
            CanabalizeDetails argCanabalizeDetails = new CanabalizeDetails();
            DataSet DataSetToFill = new DataSet();

            if (argCanabalizeDocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argCanabalizeItemNo <= 0)
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

            DataSetToFill = this.GetCanabalizeDetails(argCanabalizeDocCode, argCanabalizeItemNo, argPartnerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCanabalizeDetails = this.objCreateCanabalizeDetails((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCanabalizeDetails;
        }

        public ICollection<CanabalizeDetails> colGetCanabalizeDetails(string argCanabalizeDocCode, string argPartnerCode, string argClientCode)
        {
            List<CanabalizeDetails> lst = new List<CanabalizeDetails>();
            DataSet DataSetToFill = new DataSet();
            CanabalizeDetails tCanabalizeDetails = new CanabalizeDetails();

            DataSetToFill = this.GetCanabalizeDetails(argCanabalizeDocCode, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCanabalizeDetails(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public void colGetCanabalizeDetails(string argCanabalizeDocCode, string argPartnerCode, string argClientCode, ref CanabalizeDetailsCol argCanabalizeDetailsCol) 
        {           
            DataSet DataSetToFill = new DataSet();
            CanabalizeDetails tCanabalizeDetails = new CanabalizeDetails();

            DataSetToFill = this.GetCanabalizeDetails(argCanabalizeDocCode, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argCanabalizeDetailsCol.colCanabalizeDetails.Add(objCreateCanabalizeDetails(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;          
        }
        
        public DataSet GetCanabalizeDetails(string argCanabalizeDocCode, int argCanabalizeItemNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CanabalizeDocCode", argCanabalizeDocCode);
            param[1] = new SqlParameter("@CanabalizeItemNo", argCanabalizeItemNo);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCanabalizeDetails4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCanabalizeDetails(string argCanabalizeDocCode, int argCanabalizeItemNo, string argPartnerCode, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();

            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CanabalizeDocCode", argCanabalizeDocCode);
            param[1] = new SqlParameter("@CanabalizeItemNo", argCanabalizeItemNo);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCanabalizeDetails4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCanabalizeDetails(string argCanabalizeDocCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
         
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CanabalizeDocCode", argCanabalizeDocCode);            
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCanabalizeDetails", param);
            return DataSetToFill;
        }

        public DataSet GetCanabalizeDetails4Search(string argCanabalizeFrom, string argCanabalizeDateTo, string argCanablizeDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CanablizeDateFrom", argCanabalizeFrom);
            param[1] = new SqlParameter("@CanablizeDateTo", argCanabalizeDateTo);
            param[2] = new SqlParameter("@CanablizeDocTypeCode", argCanablizeDocTypeCode);
            param[3] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCanablizeDetail4Search", param);
            return DataSetToFill;
        }
        
        private CanabalizeDetails objCreateCanabalizeDetails(DataRow dr)
        {
            CanabalizeDetails tCanabalizeDetails = new CanabalizeDetails();

            tCanabalizeDetails.SetObjectInfo(dr);

            return tCanabalizeDetails;

        }

        public ICollection<ErrorHandler> SaveCanabalizeDetails(CanabalizeDetails argCanabalizeDetails)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCanabalizeDetailsExists(argCanabalizeDetails.CanabalizeDocCode, argCanabalizeDetails.CanabalizeItemNo, argCanabalizeDetails.PartnerCode, argCanabalizeDetails.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCanabalizeDetails(argCanabalizeDetails, da, lstErr);
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
                    UpdateCanabalizeDetails(argCanabalizeDetails, da, lstErr);
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

        public void SaveCanabalizeDetails(CanabalizeDetails argCanabalizeDetails, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCanabalizeDetailsExists(argCanabalizeDetails.CanabalizeDocCode, argCanabalizeDetails.CanabalizeItemNo, argCanabalizeDetails.PartnerCode, argCanabalizeDetails.ClientCode, da) == false)
                {
                    InsertCanabalizeDetails(argCanabalizeDetails, da, lstErr);
                }
                else
                {
                    UpdateCanabalizeDetails(argCanabalizeDetails, da, lstErr);
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
        
        public void InsertCanabalizeDetails(CanabalizeDetails argCanabalizeDetails, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[28];
            param[0] = new SqlParameter("@CanabalizeDocCode", argCanabalizeDetails.CanabalizeDocCode);
            param[1] = new SqlParameter("@CanabalizeItemNo", argCanabalizeDetails.CanabalizeItemNo);
            param[2] = new SqlParameter("@MaterialCode", argCanabalizeDetails.MaterialCode);
            param[3] = new SqlParameter("@MatGroup1Code", argCanabalizeDetails.MatGroup1Code);
            param[4] = new SqlParameter("@SerialNo1", argCanabalizeDetails.SerialNo1);
            param[5] = new SqlParameter("@StoreCode", argCanabalizeDetails.StoreCode);
            param[6] = new SqlParameter("@StockIndicator", argCanabalizeDetails.StockIndicator);
            param[7] = new SqlParameter("@PartnerCode", argCanabalizeDetails.PartnerCode);
            param[8] = new SqlParameter("@PartnerEmployeeCode", argCanabalizeDetails.PartnerEmployeeCode);
            param[9] = new SqlParameter("@ToStoreCode", argCanabalizeDetails.ToStoreCode);
            param[10] = new SqlParameter("@ToStockIndicator", argCanabalizeDetails.ToStockIndicator);
            param[11] = new SqlParameter("@ToPartnerCode", argCanabalizeDetails.ToPartnerCode);
            param[12] = new SqlParameter("@ToPartnerEmployeeCode", argCanabalizeDetails.ToPartnerEmployeeCode);
            param[13] = new SqlParameter("@RefDocCode", argCanabalizeDetails.RefDocCode);
            param[14] = new SqlParameter("@RefDocItemNo", argCanabalizeDetails.RefDocItemNo);
            param[15] = new SqlParameter("@RefDocTypeCode", argCanabalizeDetails.RefDocTypeCode);
            param[16] = new SqlParameter("@Quantity", argCanabalizeDetails.Quantity);
            param[17] = new SqlParameter("@UOMCode", argCanabalizeDetails.UOMCode);
            param[18] = new SqlParameter("@UnitPrice", argCanabalizeDetails.UnitPrice);
            param[19] = new SqlParameter("@MaterialDocTypeCode", argCanabalizeDetails.MaterialDocTypeCode);
            param[20] = new SqlParameter("@PGoodsMovementCode", argCanabalizeDetails.PGoodsMovementCode);
            param[21] = new SqlParameter("@GMItemNo", argCanabalizeDetails.GMItemNo);
            param[22] = new SqlParameter("@ClientCode", argCanabalizeDetails.ClientCode);
            param[23] = new SqlParameter("@CreatedBy", argCanabalizeDetails.CreatedBy);
            param[24] = new SqlParameter("@ModifiedBy", argCanabalizeDetails.ModifiedBy);

            param[25] = new SqlParameter("@Type", SqlDbType.Char);
            param[25].Size = 1;
            param[25].Direction = ParameterDirection.Output;

            param[26] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[26].Size = 255;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[27].Size = 20;
            param[27].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCanabalizeDetails", param);


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
        
        public void UpdateCanabalizeDetails(CanabalizeDetails argCanabalizeDetails, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[28];
            param[0] = new SqlParameter("@CanabalizeDocCode", argCanabalizeDetails.CanabalizeDocCode);
            param[1] = new SqlParameter("@CanabalizeItemNo", argCanabalizeDetails.CanabalizeItemNo);
            param[2] = new SqlParameter("@MaterialCode", argCanabalizeDetails.MaterialCode);
            param[3] = new SqlParameter("@MatGroup1Code", argCanabalizeDetails.MatGroup1Code);
            param[4] = new SqlParameter("@SerialNo1", argCanabalizeDetails.SerialNo1);
            param[5] = new SqlParameter("@StoreCode", argCanabalizeDetails.StoreCode);
            param[6] = new SqlParameter("@StockIndicator", argCanabalizeDetails.StockIndicator);
            param[7] = new SqlParameter("@PartnerCode", argCanabalizeDetails.PartnerCode);
            param[8] = new SqlParameter("@PartnerEmployeeCode", argCanabalizeDetails.PartnerEmployeeCode);
            param[9] = new SqlParameter("@ToStoreCode", argCanabalizeDetails.ToStoreCode);
            param[10] = new SqlParameter("@ToStockIndicator", argCanabalizeDetails.ToStockIndicator);
            param[11] = new SqlParameter("@ToPartnerCode", argCanabalizeDetails.ToPartnerCode);
            param[12] = new SqlParameter("@ToPartnerEmployeeCode", argCanabalizeDetails.ToPartnerEmployeeCode);
            param[13] = new SqlParameter("@RefDocCode", argCanabalizeDetails.RefDocCode);
            param[14] = new SqlParameter("@RefDocItemNo", argCanabalizeDetails.RefDocItemNo);
            param[15] = new SqlParameter("@RefDocTypeCode", argCanabalizeDetails.RefDocTypeCode);
            param[16] = new SqlParameter("@Quantity", argCanabalizeDetails.Quantity);
            param[17] = new SqlParameter("@UOMCode", argCanabalizeDetails.UOMCode);
            param[18] = new SqlParameter("@UnitPrice", argCanabalizeDetails.UnitPrice);
            param[19] = new SqlParameter("@MaterialDocTypeCode", argCanabalizeDetails.MaterialDocTypeCode);
            param[20] = new SqlParameter("@PGoodsMovementCode", argCanabalizeDetails.PGoodsMovementCode);
            param[21] = new SqlParameter("@GMItemNo", argCanabalizeDetails.GMItemNo);
            param[22] = new SqlParameter("@ClientCode", argCanabalizeDetails.ClientCode);
            param[23] = new SqlParameter("@CreatedBy", argCanabalizeDetails.CreatedBy);
            param[24] = new SqlParameter("@ModifiedBy", argCanabalizeDetails.ModifiedBy);


            param[25] = new SqlParameter("@Type", SqlDbType.Char);
            param[25].Size = 1;
            param[25].Direction = ParameterDirection.Output;

            param[26] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[26].Size = 255;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[27].Size = 20;
            param[27].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCanabalizeDetails", param);


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
        
        public ICollection<ErrorHandler> DeleteCanabalizeDetails(string argCanabalizeDocCode, int argCanabalizeItemNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@CanabalizeDocCode", argCanabalizeDocCode);
                param[1] = new SqlParameter("@CanabalizeItemNo", argCanabalizeItemNo);
                param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
                param[3] = new SqlParameter("@ClientCode", argClientCode);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;
                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;
                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteCanabalizeDetails", param);


                string strMessage = Convert.ToString(param[5].Value);
                string strType = Convert.ToString(param[4].Value);
                string strRetValue = Convert.ToString(param[6].Value);


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
        
        public bool blnIsCanabalizeDetailsExists(string argCanabalizeDocCode, int argCanabalizeItemNo, string argPartnerCode, string argClientCode)
        {
            bool IsCanabalizeDetailsExists = false;
            DataSet ds = new DataSet();
            ds = GetCanabalizeDetails(argCanabalizeDocCode, argCanabalizeItemNo, argPartnerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCanabalizeDetailsExists = true;
            }
            else
            {
                IsCanabalizeDetailsExists = false;
            }
            return IsCanabalizeDetailsExists;
        }

        public bool blnIsCanabalizeDetailsExists(string argCanabalizeDocCode, int argCanabalizeItemNo, string argPartnerCode, string argClientCode, DataAccess da)
        {
            bool IsCanabalizeDetailsExists = false;
            DataSet ds = new DataSet();
            ds = GetCanabalizeDetails(argCanabalizeDocCode, argCanabalizeItemNo, argPartnerCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCanabalizeDetailsExists = true;
            }
            else
            {
                IsCanabalizeDetailsExists = false;
            }
            return IsCanabalizeDetailsExists;
        }


        public DataSet GetBom4Cannibalization(string argMastMaterialCode, string argSerialNo, string argMRevisionCode, string argPartnerEmployeeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[6];

            param[0] = new SqlParameter("@MastMaterialCode", argMastMaterialCode);
            param[1] = new SqlParameter("@SerialNo", argSerialNo);
            param[2] = new SqlParameter("@MRevisionCode", argMRevisionCode);
            param[3] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeCode);
            param[4] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[5] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBOM4Canabalize", param);

            return DataSetToFill;
        }

    }
}