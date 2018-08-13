
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
    public class TaxCatCustomerManager
    {
        const string TaxCatCustomerTable = "TaxCatCustomer";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public TaxCatCustomer objGetTaxCatCustomer(string argTaxCategoryCode, string argTaxClassCode, string argClientCode)
        {
            TaxCatCustomer argTaxCatCustomer = new TaxCatCustomer();
            DataSet DataSetToFill = new DataSet();

            if (argTaxCategoryCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argTaxClassCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetTaxCatCustomer(argTaxCategoryCode, argTaxClassCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argTaxCatCustomer = this.objCreateTaxCatCustomer((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argTaxCatCustomer;
        }


        public ICollection<TaxCatCustomer> colGetTaxCatCustomer(string argClientCode)
        {
            List<TaxCatCustomer> lst = new List<TaxCatCustomer>();
            DataSet DataSetToFill = new DataSet();
            TaxCatCustomer tTaxCatCustomer = new TaxCatCustomer();

            DataSetToFill = this.GetTaxCatCustomer(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateTaxCatCustomer(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetTaxCatCustomer(string argTaxCategoryCode, string argTaxClassCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@TaxCategoryCode", argTaxCategoryCode);
            param[1] = new SqlParameter("@TaxClassCode", argTaxClassCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetTaxCatCustomer4ID", param);

            return DataSetToFill;
        }


        public DataSet GetTaxCatCustomer(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
         
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetTaxCatCustomer",param);
            return DataSetToFill;
        }

        public DataSet GetTaxCatCustomer(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + TaxCatCustomerTable.ToString();

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

        private TaxCatCustomer objCreateTaxCatCustomer(DataRow dr)
        {
            TaxCatCustomer tTaxCatCustomer = new TaxCatCustomer();

            tTaxCatCustomer.SetObjectInfo(dr);

            return tTaxCatCustomer;

        }


        public ICollection<ErrorHandler> SaveTaxCatCustomer(TaxCatCustomer argTaxCatCustomer)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsTaxCatCustomerExists(argTaxCatCustomer.TaxCategoryCode, argTaxCatCustomer.TaxClassCode, argTaxCatCustomer.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertTaxCatCustomer(argTaxCatCustomer, da, lstErr);
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
                    UpdateTaxCatCustomer(argTaxCatCustomer, da, lstErr);
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


        public void InsertTaxCatCustomer(TaxCatCustomer argTaxCatCustomer, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@TaxCategoryCode", argTaxCatCustomer.TaxCategoryCode);
            param[1] = new SqlParameter("@TaxCategoryDesc", argTaxCatCustomer.TaxCategoryDesc);
            param[2] = new SqlParameter("@TaxClassCode", argTaxCatCustomer.TaxClassCode);
            param[3] = new SqlParameter("@TaxClassDesc", argTaxCatCustomer.TaxClassDesc);
            param[4] = new SqlParameter("@ClientCode", argTaxCatCustomer.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argTaxCatCustomer.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argTaxCatCustomer.ModifiedBy);
      
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertTaxCatCustomer", param);


            string strMessage = Convert.ToString(param[8].Value);
            string strType = Convert.ToString(param[7].Value);
            string strRetValue = Convert.ToString(param[9].Value);


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


        public void UpdateTaxCatCustomer(TaxCatCustomer argTaxCatCustomer, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@TaxCategoryCode", argTaxCatCustomer.TaxCategoryCode);
            param[1] = new SqlParameter("@TaxCategoryDesc", argTaxCatCustomer.TaxCategoryDesc);
            param[2] = new SqlParameter("@TaxClassCode", argTaxCatCustomer.TaxClassCode);
            param[3] = new SqlParameter("@TaxClassDesc", argTaxCatCustomer.TaxClassDesc);
            param[4] = new SqlParameter("@ClientCode", argTaxCatCustomer.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argTaxCatCustomer.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argTaxCatCustomer.ModifiedBy);

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateTaxCatCustomer", param);


            string strMessage = Convert.ToString(param[8].Value);
            string strType = Convert.ToString(param[7].Value);
            string strRetValue = Convert.ToString(param[9].Value);


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


        public ICollection<ErrorHandler> DeleteTaxCatCustomer(string argTaxCategoryCode, string argTaxClassCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@TaxCategoryCode", argTaxCategoryCode);
                param[1] = new SqlParameter("@TaxClassCode", argTaxClassCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteTaxCatCustomer", param);


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


        public bool blnIsTaxCatCustomerExists(string argTaxCategoryCode, string argTaxClassCode, string argClientCode)
        {
            bool IsTaxCatCustomerExists = false;
            DataSet ds = new DataSet();
            ds = GetTaxCatCustomer(argTaxCategoryCode, argTaxClassCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsTaxCatCustomerExists = true;
            }
            else
            {
                IsTaxCatCustomerExists = false;
            }
            return IsTaxCatCustomerExists;
        }
    }
}