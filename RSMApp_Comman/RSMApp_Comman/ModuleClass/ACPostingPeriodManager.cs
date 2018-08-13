
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
    public class ACPostingPeriodManager
    {
        const string ACPostingPeriodTable = "ACPostingPeriod";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public ACPostingPeriod objGetACPostingPeriod(string argFiscalYear, string argPostingPeriod, string argClientCode)
        {
            ACPostingPeriod argACPostingPeriod = new ACPostingPeriod();
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

            DataSetToFill = this.GetACPostingPeriod(argFiscalYear, argPostingPeriod, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argACPostingPeriod = this.objCreateACPostingPeriod((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argACPostingPeriod;
        }

        public ICollection<ACPostingPeriod> colGetACPostingPeriod(string argFiscalYear, string argClientCode)
        {
            List<ACPostingPeriod> lst = new List<ACPostingPeriod>();
            DataSet DataSetToFill = new DataSet();
            ACPostingPeriod tACPostingPeriod = new ACPostingPeriod();

            DataSetToFill = this.GetACPostingPeriod(argFiscalYear, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateACPostingPeriod(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetACPostingPeriod(string argFiscalYear, string argPostingPeriod, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@FiscalYear", argFiscalYear);
            param[1] = new SqlParameter("@PostingPeriod", argPostingPeriod);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetACPostingPeriod4ID", param);

            return DataSetToFill;
        }

        public DataSet GetACPostingPeriod(string argFiscalYear, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@FiscalYear", argFiscalYear);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetACPostingPeriod", param);

            return DataSetToFill;
        }

        private ACPostingPeriod objCreateACPostingPeriod(DataRow dr)
        {
            ACPostingPeriod tACPostingPeriod = new ACPostingPeriod();

            tACPostingPeriod.SetObjectInfo(dr);

            return tACPostingPeriod;

        }

        public ICollection<ErrorHandler> SaveACPostingPeriod(ACPostingPeriod argACPostingPeriod)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsACPostingPeriodExists(argACPostingPeriod.FiscalYear, argACPostingPeriod.PostingPeriod, argACPostingPeriod.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertACPostingPeriod(argACPostingPeriod, da, lstErr);
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
                    UpdateACPostingPeriod(argACPostingPeriod, da, lstErr);
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

        public void InsertACPostingPeriod(ACPostingPeriod argACPostingPeriod, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@FiscalYear", argACPostingPeriod.FiscalYear);
            param[1] = new SqlParameter("@PostingPeriod", argACPostingPeriod.PostingPeriod);
            param[2] = new SqlParameter("@PPStartDate", argACPostingPeriod.PPStartDate);
            param[3] = new SqlParameter("@PPEndDate", argACPostingPeriod.PPEndDate);
            param[4] = new SqlParameter("@CompanyCode", argACPostingPeriod.CompanyCode);
            param[5] = new SqlParameter("@IsOpen", argACPostingPeriod.IsOpen);
            param[6] = new SqlParameter("@ClientCode", argACPostingPeriod.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argACPostingPeriod.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argACPostingPeriod.ModifiedBy);
  
            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertACPostingPeriod", param);


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

        public void UpdateACPostingPeriod(ACPostingPeriod argACPostingPeriod, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@FiscalYear", argACPostingPeriod.FiscalYear);
            param[1] = new SqlParameter("@PostingPeriod", argACPostingPeriod.PostingPeriod);
            param[2] = new SqlParameter("@PPStartDate", argACPostingPeriod.PPStartDate);
            param[3] = new SqlParameter("@PPEndDate", argACPostingPeriod.PPEndDate);
            param[4] = new SqlParameter("@CompanyCode", argACPostingPeriod.CompanyCode);
            param[5] = new SqlParameter("@IsOpen", argACPostingPeriod.IsOpen);
            param[6] = new SqlParameter("@ClientCode", argACPostingPeriod.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argACPostingPeriod.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argACPostingPeriod.ModifiedBy);

            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateACPostingPeriod", param);


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

        public ICollection<ErrorHandler> DeleteACPostingPeriod(string argFiscalYear, string argPostingPeriod, string argClientCode,int iIsDeleted)
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

                int i = da.ExecuteNonQuery("Proc_DeleteACPostingPeriod", param);


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

        public bool blnIsACPostingPeriodExists(string argFiscalYear, string argPostingPeriod, string argClientCode)
        {
            bool IsACPostingPeriodExists = false;
            DataSet ds = new DataSet();
            ds = GetACPostingPeriod(argFiscalYear, argPostingPeriod, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsACPostingPeriodExists = true;
            }
            else
            {
                IsACPostingPeriodExists = false;
            }
            return IsACPostingPeriodExists;
        }
    }
}