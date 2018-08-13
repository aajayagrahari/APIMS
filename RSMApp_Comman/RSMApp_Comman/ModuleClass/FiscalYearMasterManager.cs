
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
    public class FiscalYearMasterManager
    {
        const string FiscalYearMasterTable = "FiscalYearMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public FiscalYearMaster objGetFiscalYearMaster(string argFiscalYearType, string argFiscalYear, string argClientCode)
        {
            FiscalYearMaster argFiscalYearMaster = new FiscalYearMaster();
            DataSet DataSetToFill = new DataSet();

            if (argFiscalYearType.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argFiscalYear.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetFiscalYearMaster(argFiscalYearType, argFiscalYear, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argFiscalYearMaster = this.objCreateFiscalYearMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argFiscalYearMaster;
        }

        public ICollection<FiscalYearMaster> colGetFiscalYearMaster(string argFiscalYearType, string argClientCode)
        {
            List<FiscalYearMaster> lst = new List<FiscalYearMaster>();
            DataSet DataSetToFill = new DataSet();
            FiscalYearMaster tFiscalYearMaster = new FiscalYearMaster();

            DataSetToFill = this.GetFiscalYearMaster(argFiscalYearType, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateFiscalYearMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetFiscalYearMaster(string argFiscalYearType, string argFiscalYear, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@FiscalYearType", argFiscalYearType);
            param[1] = new SqlParameter("@FiscalYear", argFiscalYear);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetFiscalYearMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetFiscalYearMaster(string argFiscalYearType, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@FiscalYearType", argFiscalYearType);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetFiscalYearMaster",param);
            return DataSetToFill;
        }

        public DataSet GetFiscalYearMaster(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + FiscalYearMasterTable.ToString();

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

        private FiscalYearMaster objCreateFiscalYearMaster(DataRow dr)
        {
            FiscalYearMaster tFiscalYearMaster = new FiscalYearMaster();

            tFiscalYearMaster.SetObjectInfo(dr);

            return tFiscalYearMaster;

        }

        public ICollection<ErrorHandler> SaveFiscalYearMaster(FiscalYearMaster argFiscalYearMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsFiscalYearMasterExists(argFiscalYearMaster.FiscalYearType, argFiscalYearMaster.FiscalYear, argFiscalYearMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertFiscalYearMaster(argFiscalYearMaster, da, lstErr);
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
                    UpdateFiscalYearMaster(argFiscalYearMaster, da, lstErr);
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

        public void InsertFiscalYearMaster(FiscalYearMaster argFiscalYearMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@FiscalYearType", argFiscalYearMaster.FiscalYearType);
            param[1] = new SqlParameter("@FiscalYear", argFiscalYearMaster.FiscalYear);
            param[2] = new SqlParameter("@FYStartDate", argFiscalYearMaster.FYStartDate);
            param[3] = new SqlParameter("@FYEndDate", argFiscalYearMaster.FYEndDate);
            param[4] = new SqlParameter("@CompanyCode", argFiscalYearMaster.CompanyCode);
            param[5] = new SqlParameter("@IsOpen", argFiscalYearMaster.IsOpen);
            param[6] = new SqlParameter("@ClientCode", argFiscalYearMaster.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argFiscalYearMaster.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argFiscalYearMaster.ModifiedBy);


            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertFiscalYearMaster", param);


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

        public void UpdateFiscalYearMaster(FiscalYearMaster argFiscalYearMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@FiscalYearType", argFiscalYearMaster.FiscalYearType);
            param[1] = new SqlParameter("@FiscalYear", argFiscalYearMaster.FiscalYear);
            param[2] = new SqlParameter("@FYStartDate", argFiscalYearMaster.FYStartDate);
            param[3] = new SqlParameter("@FYEndDate", argFiscalYearMaster.FYEndDate);
            param[4] = new SqlParameter("@CompanyCode", argFiscalYearMaster.CompanyCode);
            param[5] = new SqlParameter("@IsOpen", argFiscalYearMaster.IsOpen);
            param[6] = new SqlParameter("@ClientCode", argFiscalYearMaster.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argFiscalYearMaster.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argFiscalYearMaster.ModifiedBy);


            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateFiscalYearMaster", param);


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

        public ICollection<ErrorHandler> DeleteFiscalYearMaster(string argFiscalYearType, string argFiscalYear, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@FiscalYearType", argFiscalYearType);
                param[1] = new SqlParameter("@FiscalYear", argFiscalYear);
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

                int i = da.ExecuteNonQuery("Proc_DeleteFiscalYearMaster", param);


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

        public bool blnIsFiscalYearMasterExists(string argFiscalYearType, string argFiscalYear, string argClientCode)
        {
            bool IsFiscalYearMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetFiscalYearMaster(argFiscalYearType, argFiscalYear, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsFiscalYearMasterExists = true;
            }
            else
            {
                IsFiscalYearMasterExists = false;
            }
            return IsFiscalYearMasterExists;
        }
    }
}