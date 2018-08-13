
//Created On :: 15, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Comman
{
    public class MMPostingPeriodManager
    {
        const string MMPostingPeriodTable = "MMPostingPeriod";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public MMPostingPeriod objGetMMPostingPeriod(string argFiscalYear, string argPostingPeriod, string argClientCode)
        {
            MMPostingPeriod argMMPostingPeriod = new MMPostingPeriod();
            DataSet DataSetToFill = new DataSet();

            if (argFiscalYear.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argPostingPeriod.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetMMPostingPeriod(argFiscalYear, argPostingPeriod, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMMPostingPeriod = this.objCreateMMPostingPeriod((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argMMPostingPeriod;
        }

        public ICollection<MMPostingPeriod> colGetMMPostingPeriod(string argFiscalYear, string argClientCode)
        {
            List<MMPostingPeriod> lst = new List<MMPostingPeriod>();
            DataSet DataSetToFill = new DataSet();
            MMPostingPeriod tMMPostingPeriod = new MMPostingPeriod();

            DataSetToFill = this.GetMMPostingPeriod(argFiscalYear,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMMPostingPeriod(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetMMPostingPeriod(string argFiscalYear, string argPostingPeriod, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@FiscalYear", argFiscalYear);
            param[1] = new SqlParameter("@PostingPeriod", argPostingPeriod);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMMPostingPeriod4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMMPostingPeriod(string argFiscalYear, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@FiscalYear", argFiscalYear);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMMPostingPeriod", param);

            return DataSetToFill;
        }

        private MMPostingPeriod objCreateMMPostingPeriod(DataRow dr)
        {
            MMPostingPeriod tMMPostingPeriod = new MMPostingPeriod();

            tMMPostingPeriod.SetObjectInfo(dr);

            return tMMPostingPeriod;

        }

        public ICollection<ErrorHandler> SaveMMPostingPeriod(MMPostingPeriod argMMPostingPeriod)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsMMPostingPeriodExists(argMMPostingPeriod.FiscalYear, argMMPostingPeriod.PostingPeriod, argMMPostingPeriod.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertMMPostingPeriod(argMMPostingPeriod, da, lstErr);
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
                    UpdateMMPostingPeriod(argMMPostingPeriod, da, lstErr);
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

        public void InsertMMPostingPeriod(MMPostingPeriod argMMPostingPeriod, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@FiscalYear", argMMPostingPeriod.FiscalYear);
            param[1] = new SqlParameter("@PostingPeriod", argMMPostingPeriod.PostingPeriod);
            param[2] = new SqlParameter("@PPStartDate", argMMPostingPeriod.PPStartDate);
            param[3] = new SqlParameter("@PPEndDate", argMMPostingPeriod.PPEndDate);
            param[4] = new SqlParameter("@CompanyCode", argMMPostingPeriod.CompanyCode);
            param[5] = new SqlParameter("@IsOpen", argMMPostingPeriod.IsOpen);
            param[6] = new SqlParameter("@ClientCode", argMMPostingPeriod.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argMMPostingPeriod.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argMMPostingPeriod.ModifiedBy);
     

            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertMMPostingPeriod", param);


            string strMessage = Convert.ToString(param[10].Value);
            string strType = Convert.ToString(param[9].Value);
            string strRetValue = Convert.ToString(param[11].Value);


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

        public void UpdateMMPostingPeriod(MMPostingPeriod argMMPostingPeriod, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@FiscalYear", argMMPostingPeriod.FiscalYear);
            param[1] = new SqlParameter("@PostingPeriod", argMMPostingPeriod.PostingPeriod);
            param[2] = new SqlParameter("@PPStartDate", argMMPostingPeriod.PPStartDate);
            param[3] = new SqlParameter("@PPEndDate", argMMPostingPeriod.PPEndDate);
            param[4] = new SqlParameter("@CompanyCode", argMMPostingPeriod.CompanyCode);
            param[5] = new SqlParameter("@IsOpen", argMMPostingPeriod.IsOpen);
            param[6] = new SqlParameter("@ClientCode", argMMPostingPeriod.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argMMPostingPeriod.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argMMPostingPeriod.ModifiedBy);
   
            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateMMPostingPeriod", param);


            string strMessage = Convert.ToString(param[10].Value);
            string strType = Convert.ToString(param[9].Value);
            string strRetValue = Convert.ToString(param[11].Value);


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

        public ICollection<ErrorHandler> DeleteMMPostingPeriod(string argFiscalYear, string argPostingPeriod, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@FiscalYear", argFiscalYear);
                param[1] = new SqlParameter("@PostingPeriod", argPostingPeriod);
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

                int i = da.ExecuteNonQuery("Proc_DeleteMMPostingPeriod", param);


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

        public bool blnIsMMPostingPeriodExists(string argFiscalYear, string argPostingPeriod, string argClientCode)
        {
            bool IsMMPostingPeriodExists = false;
            DataSet ds = new DataSet();
            ds = GetMMPostingPeriod(argFiscalYear, argPostingPeriod, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMMPostingPeriodExists = true;
            }
            else
            {
                IsMMPostingPeriodExists = false;
            }
            return IsMMPostingPeriodExists;
        }
    }
}