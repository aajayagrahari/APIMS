
//Created On :: 28, September, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;


namespace RSMApp_FI
{
    public class AccountDocTypeManager
    {
        const string AccountDocTypeTable = "AccountDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public AccountDocType objGetAccountDocType(string argAccountDocTypeCode, string argClientCode)
        {
            AccountDocType argAccountDocType = new AccountDocType();
            DataSet DataSetToFill = new DataSet();

            if (argAccountDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetAccountDocType(argAccountDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argAccountDocType = this.objCreateAccountDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argAccountDocType;
        }

        public ICollection<AccountDocType> colGetAccountDocType(string argClientCode)
        {
            List<AccountDocType> lst = new List<AccountDocType>();
            DataSet DataSetToFill = new DataSet();
            AccountDocType tAccountDocType = new AccountDocType();

            DataSetToFill = this.GetAccountDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateAccountDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

       

        public DataSet GetAccountDocType(string argAccountDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AccountDocTypeCode", argAccountDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAccountDocType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetAccountDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            
            SqlParameter[] param = new SqlParameter[1];
     
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAccountDocType",param);
            return DataSetToFill;
        }

        public DataSet GetAccountDocType(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + AccountDocTypeTable.ToString();

                if (iIsDeleted >= 0)
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " IsDeleted = " + iIsDeleted;
                }


                if (argClientCode != "")
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " ClientCode = '" + argClientCode + "'";
                }



                ds = da.FillDataSetWithSQL(tSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        private AccountDocType objCreateAccountDocType(DataRow dr)
        {
            AccountDocType tAccountDocType = new AccountDocType();

            tAccountDocType.SetObjectInfo(dr);

            return tAccountDocType;

        }

        public ICollection<ErrorHandler> SaveAccountDocType(AccountDocType argAccountDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsAccountDocTypeExists(argAccountDocType.AccountDocTypeCode, argAccountDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertAccountDocType(argAccountDocType, da, lstErr);
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
                    UpdateAccountDocType(argAccountDocType, da, lstErr);
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

        public void InsertAccountDocType(AccountDocType argAccountDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[21];
            param[0] = new SqlParameter("@AccountDocTypeCode", argAccountDocType.AccountDocTypeCode);
            param[1] = new SqlParameter("@AccountTypeDesc", argAccountDocType.AccountTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argAccountDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argAccountDocType.NumRange);
            param[4] = new SqlParameter("@SaveMode", argAccountDocType.SaveMode);
            param[5] = new SqlParameter("@RevAccountDocTypeCode", argAccountDocType.RevAccountDocTypeCode);
            param[6] = new SqlParameter("@AccTypeAllowedAsset", argAccountDocType.AccTypeAllowedAsset);
            param[7] = new SqlParameter("@AccTypeAllowedCustomer", argAccountDocType.AccTypeAllowedCustomer);
            param[8] = new SqlParameter("@AccTypeAllowedVendor", argAccountDocType.AccTypeAllowedVendor);
            param[9] = new SqlParameter("@AccTypeAllowedMaterial", argAccountDocType.AccTypeAllowedMaterial);
            param[10] = new SqlParameter("@AccTypeAllowedGL", argAccountDocType.AccTypeAllowedGL);
            param[11] = new SqlParameter("@IsNegativePosting", argAccountDocType.IsNegativePosting);
            param[12] = new SqlParameter("@IsDocHeaderAllowed", argAccountDocType.IsDocHeaderAllowed);
            param[13] = new SqlParameter("@IsReferenceAllowed", argAccountDocType.IsReferenceAllowed);
            param[14] = new SqlParameter("@ExchangeRateType", argAccountDocType.ExchangeRateType);
            param[15] = new SqlParameter("@ClientCode", argAccountDocType.ClientCode);
            param[16] = new SqlParameter("@CreatedBy", argAccountDocType.CreatedBy);
            param[17] = new SqlParameter("@ModifiedBy", argAccountDocType.ModifiedBy);
      
            param[18] = new SqlParameter("@Type", SqlDbType.Char);
            param[18].Size = 1;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[19].Size = 255;
            param[19].Direction = ParameterDirection.Output;

            param[20] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[20].Size = 20;
            param[20].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertAccountDocType", param);

            string strMessage = Convert.ToString(param[19].Value);
            string strType = Convert.ToString(param[18].Value);
            string strRetValue = Convert.ToString(param[20].Value);


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

        }

        public void UpdateAccountDocType(AccountDocType argAccountDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[21];
            param[0] = new SqlParameter("@AccountDocTypeCode", argAccountDocType.AccountDocTypeCode);
            param[1] = new SqlParameter("@AccountTypeDesc", argAccountDocType.AccountTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argAccountDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argAccountDocType.NumRange);
            param[4] = new SqlParameter("@SaveMode", argAccountDocType.SaveMode);
            param[5] = new SqlParameter("@RevAccountDocTypeCode", argAccountDocType.RevAccountDocTypeCode);
            param[6] = new SqlParameter("@AccTypeAllowedAsset", argAccountDocType.AccTypeAllowedAsset);
            param[7] = new SqlParameter("@AccTypeAllowedCustomer", argAccountDocType.AccTypeAllowedCustomer);
            param[8] = new SqlParameter("@AccTypeAllowedVendor", argAccountDocType.AccTypeAllowedVendor);
            param[9] = new SqlParameter("@AccTypeAllowedMaterial", argAccountDocType.AccTypeAllowedMaterial);
            param[10] = new SqlParameter("@AccTypeAllowedGL", argAccountDocType.AccTypeAllowedGL);
            param[11] = new SqlParameter("@IsNegativePosting", argAccountDocType.IsNegativePosting);
            param[12] = new SqlParameter("@IsDocHeaderAllowed", argAccountDocType.IsDocHeaderAllowed);
            param[13] = new SqlParameter("@IsReferenceAllowed", argAccountDocType.IsReferenceAllowed);
            param[14] = new SqlParameter("@ExchangeRateType", argAccountDocType.ExchangeRateType);
            param[15] = new SqlParameter("@ClientCode", argAccountDocType.ClientCode);
            param[16] = new SqlParameter("@CreatedBy", argAccountDocType.CreatedBy);
            param[17] = new SqlParameter("@ModifiedBy", argAccountDocType.ModifiedBy);

            param[18] = new SqlParameter("@Type", SqlDbType.Char);
            param[18].Size = 1;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[19].Size = 255;
            param[19].Direction = ParameterDirection.Output;

            param[20] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[20].Size = 20;
            param[20].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateAccountDocType", param);


            string strMessage = Convert.ToString(param[19].Value);
            string strType = Convert.ToString(param[18].Value);
            string strRetValue = Convert.ToString(param[20].Value);


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

        }

        public ICollection<ErrorHandler> DeleteAccountDocType(string argAccountDocTypeCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@AccountDocTypeCode", argAccountDocTypeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteAccountDocType", param);
                
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
                objErrorHandler.ReturnValue = strRetValue;
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

        public bool blnIsAccountDocTypeExists(string argAccountDocTypeCode, string argClientCode)
        {
            bool IsAccountDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetAccountDocType(argAccountDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAccountDocTypeExists = true;
            }
            else
            {
                IsAccountDocTypeExists = false;
            }
            return IsAccountDocTypeExists;
        }
    }
}