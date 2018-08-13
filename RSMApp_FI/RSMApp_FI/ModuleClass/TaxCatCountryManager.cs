
//Created On :: 03, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;


namespace RSMApp_FI
{
    public class TaxCatCountryManager
    {
        const string TaxCatCountryTable = "TaxCatCountry";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public TaxCatCountry objGetTaxCatCountry(string argTaxCountryCode, string argTaxCategoryCode, string argClientCode)
        {
            TaxCatCountry argTaxCatCountry = new TaxCatCountry();
            DataSet DataSetToFill = new DataSet();

            if (argTaxCountryCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argTaxCategoryCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetTaxCatCountry(argTaxCountryCode, argTaxCategoryCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argTaxCatCountry = this.objCreateTaxCatCountry((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argTaxCatCountry;
        }

        public ICollection<TaxCatCountry> colGetTaxCatCountry(string argClientCode)
        {
            List<TaxCatCountry> lst = new List<TaxCatCountry>();
            DataSet DataSetToFill = new DataSet();
            TaxCatCountry tTaxCatCountry = new TaxCatCountry();

            DataSetToFill = this.GetTaxCatCountry(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateTaxCatCountry(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetTaxCatCountry(string argTaxCountryCode, string argTaxCategoryCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@TaxCountryCode", argTaxCountryCode);
            param[1] = new SqlParameter("@TaxCategoryCode", argTaxCategoryCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetTaxCatCountry4ID", param);

            return DataSetToFill;
        }

        public DataSet GetTaxCatCountry(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetTaxCatCountry",param);
            return DataSetToFill;
        }

        private TaxCatCountry objCreateTaxCatCountry(DataRow dr)
        {
            TaxCatCountry tTaxCatCountry = new TaxCatCountry();

            tTaxCatCountry.SetObjectInfo(dr);

            return tTaxCatCountry;

        }

        public ICollection<ErrorHandler> SaveTaxCatCountry(TaxCatCountry argTaxCatCountry)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsTaxCatCountryExists(argTaxCatCountry.TaxCountryCode, argTaxCatCountry.TaxCategoryCode, argTaxCatCountry.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertTaxCatCountry(argTaxCatCountry, da, lstErr);
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
                    UpdateTaxCatCountry(argTaxCatCountry, da, lstErr);
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

        public void InsertTaxCatCountry(TaxCatCountry argTaxCatCountry, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@TaxCountryCode", argTaxCatCountry.TaxCountryCode);
            param[1] = new SqlParameter("@Sequence", argTaxCatCountry.Sequence);
            param[2] = new SqlParameter("@TaxCategoryCode", argTaxCatCountry.TaxCategoryCode);
            param[3] = new SqlParameter("@ClientCode", argTaxCatCountry.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argTaxCatCountry.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argTaxCatCountry.ModifiedBy);
  
            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertTaxCatCountry", param);


            string strMessage = Convert.ToString(param[7].Value);
            string strType = Convert.ToString(param[6].Value);
            string strRetValue = Convert.ToString(param[8].Value);


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

        public void UpdateTaxCatCountry(TaxCatCountry argTaxCatCountry, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@TaxCountryCode", argTaxCatCountry.TaxCountryCode);
            param[1] = new SqlParameter("@Sequence", argTaxCatCountry.Sequence);
            param[2] = new SqlParameter("@TaxCategoryCode", argTaxCatCountry.TaxCategoryCode);
            param[3] = new SqlParameter("@ClientCode", argTaxCatCountry.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argTaxCatCountry.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argTaxCatCountry.ModifiedBy);

            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateTaxCatCountry", param);


            string strMessage = Convert.ToString(param[7].Value);
            string strType = Convert.ToString(param[6].Value);
            string strRetValue = Convert.ToString(param[8].Value);


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

        public ICollection<ErrorHandler> DeleteTaxCatCountry(string argTaxCountryCode, string argTaxCategoryCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@TaxCountryCode", argTaxCountryCode);
                param[1] = new SqlParameter("@TaxCategoryCode", argTaxCategoryCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteTaxCatCountry", param);


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

        public bool blnIsTaxCatCountryExists(string argTaxCountryCode, string argTaxCategoryCode, string argClientCode)
        {
            bool IsTaxCatCountryExists = false;
            DataSet ds = new DataSet();
            ds = GetTaxCatCountry(argTaxCountryCode, argTaxCategoryCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsTaxCatCountryExists = true;
            }
            else
            {
                IsTaxCatCountryExists = false;
            }
            return IsTaxCatCountryExists;
        }
    }
}