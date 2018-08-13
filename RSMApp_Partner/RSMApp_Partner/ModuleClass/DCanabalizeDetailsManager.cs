
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
    public class DCanabalizeDetailsManager
    {
        const string DCanabalizeDetailsTable = "DCanabalizeDetails";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public DCanabalizeDetails objGetDCanabalizeDetails(string argDCanabalizeDocNo, int argDCanabalizeItemNo, string argPartnerCode, string argClientCode)
        {
            DCanabalizeDetails argDCanabalizeDetails = new DCanabalizeDetails();
            DataSet DataSetToFill = new DataSet();

            if (argDCanabalizeDocNo.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argDCanabalizeItemNo <= 0)
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

            DataSetToFill = this.GetDCanabalizeDetails(argDCanabalizeDocNo, argDCanabalizeItemNo, argPartnerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argDCanabalizeDetails = this.objCreateDCanabalizeDetails((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argDCanabalizeDetails;
        }


        public ICollection<DCanabalizeDetails> colGetDCanabalizeDetails(string argDCanabalizeDocNo, string argPartnerCode, string argClientCode)
        {
            List<DCanabalizeDetails> lst = new List<DCanabalizeDetails>();
            DataSet DataSetToFill = new DataSet();
            DCanabalizeDetails tDCanabalizeDetails = new DCanabalizeDetails();

            DataSetToFill = this.GetDCanabalizeDetails(argDCanabalizeDocNo, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDCanabalizeDetails(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public void colGetDCanabalizeDetails(string argDCanabalizeDocNo, string argPartnerCode, string argClientCode, ref DCanabalizeDetailsCol argDCanabalizeDetailsCol)
        {
            DataSet DataSetToFill = new DataSet();
            DCanabalizeDetails tDCanabalizeDetails = new DCanabalizeDetails();

            DataSetToFill = this.GetDCanabalizeDetails(argDCanabalizeDocNo, argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argDCanabalizeDetailsCol.colDCanabalizeDetails.Add(objCreateDCanabalizeDetails(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;
        }


        public DataSet GetDCanabalizeDetails(string argDCanabalizeDocNo, int argDCanabalizeItemNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@DCanabalizeDocNo", argDCanabalizeDocNo);
            param[1] = new SqlParameter("@DCanabalizeItemNo", argDCanabalizeItemNo);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDCanabalizeDetails4ID", param);

            return DataSetToFill;
        }

        public DataSet GetDCanabalizeDetails(string argDCanabalizeDocNo, int argDCanabalizeItemNo, string argPartnerCode, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@DCanabalizeDocNo", argDCanabalizeDocNo);
            param[1] = new SqlParameter("@DCanabalizeItemNo", argDCanabalizeItemNo);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetDCanabalizeDetails4ID", param);

            return DataSetToFill;
        }

        public DataSet GetDCanabalizeDetails4Search(string argDCanabalizeFromDate, string argDCanabalizeDateTo, string argDCanablizeDocTypeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@DCanablizeDateFrom", argDCanabalizeFromDate);
            param[1] = new SqlParameter("@DCanablizeDateTo", argDCanabalizeDateTo);
            param[2] = new SqlParameter("@DCanablizeDocTypeCode", argDCanablizeDocTypeCode);
            param[3] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDCanablizeDetail4Search", param);
            return DataSetToFill;
        }


        public DataSet GetDCanabalizeDetails(string argDCanabalizeDocNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@DCanabalizeDocNo", argDCanabalizeDocNo);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDCanabalizeDetails",param);
            return DataSetToFill;
        }


        private DCanabalizeDetails objCreateDCanabalizeDetails(DataRow dr)
        {
            DCanabalizeDetails tDCanabalizeDetails = new DCanabalizeDetails();

            tDCanabalizeDetails.SetObjectInfo(dr);

            return tDCanabalizeDetails;

        }


        public ICollection<ErrorHandler> SaveDCanabalizeDetails(DCanabalizeDetails argDCanabalizeDetails)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsDCanabalizeDetailsExists(argDCanabalizeDetails.DCanabalizeDocNo, argDCanabalizeDetails.DCanabalizeItemNo, argDCanabalizeDetails.PartnerCode, argDCanabalizeDetails.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertDCanabalizeDetails(argDCanabalizeDetails, da, lstErr);
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
                    UpdateDCanabalizeDetails(argDCanabalizeDetails, da, lstErr);
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

        public void SaveDCanabalizeDetails(DCanabalizeDetails argDCanabalizeDetails, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsDCanabalizeDetailsExists(argDCanabalizeDetails.DCanabalizeDocNo, argDCanabalizeDetails.DCanabalizeItemNo, argDCanabalizeDetails.PartnerCode, argDCanabalizeDetails.ClientCode, da) == false)
                {
                    InsertDCanabalizeDetails(argDCanabalizeDetails, da, lstErr);
                }
                else
                {
                    UpdateDCanabalizeDetails(argDCanabalizeDetails, da, lstErr);
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


        public void InsertDCanabalizeDetails(DCanabalizeDetails argDCanabalizeDetails, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[29];
            param[0] = new SqlParameter("@DCanabalizeDocNo", argDCanabalizeDetails.DCanabalizeDocNo);
            param[1] = new SqlParameter("@DCanabalizeItemNo", argDCanabalizeDetails.DCanabalizeItemNo);
            param[2] = new SqlParameter("@MaterialCode", argDCanabalizeDetails.MaterialCode);
            param[3] = new SqlParameter("@MatGroup1Code", argDCanabalizeDetails.MatGroup1Code);
            param[4] = new SqlParameter("@SerialNo1", argDCanabalizeDetails.SerialNo1);
            param[5] = new SqlParameter("@StoreCode", argDCanabalizeDetails.StoreCode);
            param[6] = new SqlParameter("@StockIndicator", argDCanabalizeDetails.StockIndicator);
            param[7] = new SqlParameter("@FromPartnerCode", argDCanabalizeDetails.FromPartnerCode);
            param[8] = new SqlParameter("@PartnerEmployeeCode", argDCanabalizeDetails.PartnerEmployeeCode);
            param[9] = new SqlParameter("@ToStoreCode", argDCanabalizeDetails.ToStoreCode);
            param[10] = new SqlParameter("@ToStockIndicator", argDCanabalizeDetails.ToStockIndicator);
            param[11] = new SqlParameter("@ToPartnerCode", argDCanabalizeDetails.ToPartnerCode);
            param[12] = new SqlParameter("@ToPartnerEmployeeCode", argDCanabalizeDetails.ToPartnerEmployeeCode);
            param[13] = new SqlParameter("@RefDocCode", argDCanabalizeDetails.RefDocCode);
            param[14] = new SqlParameter("@RefDocItemNo", argDCanabalizeDetails.RefDocItemNo);
            param[15] = new SqlParameter("@RefDocTypeCode", argDCanabalizeDetails.RefDocTypeCode);
            param[16] = new SqlParameter("@Quantity", argDCanabalizeDetails.Quantity);
            param[17] = new SqlParameter("@UOMCode", argDCanabalizeDetails.UOMCode);
            param[18] = new SqlParameter("@UnitPrice", argDCanabalizeDetails.UnitPrice);
            param[19] = new SqlParameter("@MaterialDocTypeCode", argDCanabalizeDetails.MaterialDocTypeCode);
            param[20] = new SqlParameter("@PGoodsMovementCode", argDCanabalizeDetails.PGoodsMovementCode);
            param[21] = new SqlParameter("@GMItemNo", argDCanabalizeDetails.GMItemNo);
            param[22] = new SqlParameter("@PartnerCode", argDCanabalizeDetails.PartnerCode);
            param[23] = new SqlParameter("@ClientCode", argDCanabalizeDetails.ClientCode);
            param[24] = new SqlParameter("@CreatedBy", argDCanabalizeDetails.CreatedBy);
            param[25] = new SqlParameter("@ModifiedBy", argDCanabalizeDetails.ModifiedBy);
      
            param[26] = new SqlParameter("@Type", SqlDbType.Char);
            param[26].Size = 1;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[27].Size = 255;
            param[27].Direction = ParameterDirection.Output;

            param[28] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[28].Size = 20;
            param[28].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertDCanabalizeDetails", param);


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


        public void UpdateDCanabalizeDetails(DCanabalizeDetails argDCanabalizeDetails, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[29];
            param[0] = new SqlParameter("@DCanabalizeDocNo", argDCanabalizeDetails.DCanabalizeDocNo);
            param[1] = new SqlParameter("@DCanabalizeItemNo", argDCanabalizeDetails.DCanabalizeItemNo);
            param[2] = new SqlParameter("@MaterialCode", argDCanabalizeDetails.MaterialCode);
            param[3] = new SqlParameter("@MatGroup1Code", argDCanabalizeDetails.MatGroup1Code);
            param[4] = new SqlParameter("@SerialNo1", argDCanabalizeDetails.SerialNo1);
            param[5] = new SqlParameter("@StoreCode", argDCanabalizeDetails.StoreCode);
            param[6] = new SqlParameter("@StockIndicator", argDCanabalizeDetails.StockIndicator);
            param[7] = new SqlParameter("@FromPartnerCode", argDCanabalizeDetails.FromPartnerCode);
            param[8] = new SqlParameter("@PartnerEmployeeCode", argDCanabalizeDetails.PartnerEmployeeCode);
            param[9] = new SqlParameter("@ToStoreCode", argDCanabalizeDetails.ToStoreCode);
            param[10] = new SqlParameter("@ToStockIndicator", argDCanabalizeDetails.ToStockIndicator);
            param[11] = new SqlParameter("@ToPartnerCode", argDCanabalizeDetails.ToPartnerCode);
            param[12] = new SqlParameter("@ToPartnerEmployeeCode", argDCanabalizeDetails.ToPartnerEmployeeCode);
            param[13] = new SqlParameter("@RefDocCode", argDCanabalizeDetails.RefDocCode);
            param[14] = new SqlParameter("@RefDocItemNo", argDCanabalizeDetails.RefDocItemNo);
            param[15] = new SqlParameter("@RefDocTypeCode", argDCanabalizeDetails.RefDocTypeCode);
            param[16] = new SqlParameter("@Quantity", argDCanabalizeDetails.Quantity);
            param[17] = new SqlParameter("@UOMCode", argDCanabalizeDetails.UOMCode);
            param[18] = new SqlParameter("@UnitPrice", argDCanabalizeDetails.UnitPrice);
            param[19] = new SqlParameter("@MaterialDocTypeCode", argDCanabalizeDetails.MaterialDocTypeCode);
            param[20] = new SqlParameter("@PGoodsMovementCode", argDCanabalizeDetails.PGoodsMovementCode);
            param[21] = new SqlParameter("@GMItemNo", argDCanabalizeDetails.GMItemNo);
            param[22] = new SqlParameter("@PartnerCode", argDCanabalizeDetails.PartnerCode);
            param[23] = new SqlParameter("@ClientCode", argDCanabalizeDetails.ClientCode);
            param[24] = new SqlParameter("@CreatedBy", argDCanabalizeDetails.CreatedBy);
            param[25] = new SqlParameter("@ModifiedBy", argDCanabalizeDetails.ModifiedBy);

            param[26] = new SqlParameter("@Type", SqlDbType.Char);
            param[26].Size = 1;
            param[26].Direction = ParameterDirection.Output;

            param[27] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[27].Size = 255;
            param[27].Direction = ParameterDirection.Output;

            param[28] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[28].Size = 20;
            param[28].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateDCanabalizeDetails", param);


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


        public ICollection<ErrorHandler> DeleteDCanabalizeDetails(string argDCanabalizeDocNo, int argDCanabalizeItemNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@DCanabalizeDocNo", argDCanabalizeDocNo);
                param[1] = new SqlParameter("@DCanabalizeItemNo", argDCanabalizeItemNo);
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
                int i = da.ExecuteNonQuery("Proc_DeleteDCanabalizeDetails", param);


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


        public bool blnIsDCanabalizeDetailsExists(string argDCanabalizeDocNo, int argDCanabalizeItemNo, string argPartnerCode, string argClientCode)
        {
            bool IsDCanabalizeDetailsExists = false;
            DataSet ds = new DataSet();
            ds = GetDCanabalizeDetails(argDCanabalizeDocNo, argDCanabalizeItemNo, argPartnerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDCanabalizeDetailsExists = true;
            }
            else
            {
                IsDCanabalizeDetailsExists = false;
            }
            return IsDCanabalizeDetailsExists;
        }
        public bool blnIsDCanabalizeDetailsExists(string argDCanabalizeDocNo, int argDCanabalizeItemNo, string argPartnerCode, string argClientCode,DataAccess da)
        {
            bool IsDCanabalizeDetailsExists = false;
            DataSet ds = new DataSet();
            ds = GetDCanabalizeDetails(argDCanabalizeDocNo, argDCanabalizeItemNo, argPartnerCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDCanabalizeDetailsExists = true;
            }
            else
            {
                IsDCanabalizeDetailsExists = false;
            }
            return IsDCanabalizeDetailsExists;
        }

        public DataSet GetBom4DCannibalization(string argMastMaterialCode, string argSerialNo, string argMRevisionCode, string argPartnerEmployeeCode, string argPartnerCode, string argClientCode)
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

            DataSetToFill = da.FillDataSet("SP_GetBOM4DCanabalize", param);

            return DataSetToFill;
        }

    }
}