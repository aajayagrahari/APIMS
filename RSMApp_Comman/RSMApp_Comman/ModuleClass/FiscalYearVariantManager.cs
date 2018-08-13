
//Created On :: 05, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;


namespace RSMApp_Comman
{
    public class FiscalYearVariantManager
    {
        const string FiscalYearVariantTable = "FiscalYearVariant";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public FiscalYearVariant objGetFiscalYearVariant(string argFiscalYearType)
        {
            FiscalYearVariant argFiscalYearVariant = new FiscalYearVariant();
            DataSet DataSetToFill = new DataSet();

            if (argFiscalYearType.Trim() == "")
            {
                goto ErrorHandler;
            }

            DataSetToFill = this.GetFiscalYearVariant(argFiscalYearType);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argFiscalYearVariant = this.objCreateFiscalYearVariant((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandler:

        Finish:
            DataSetToFill = null;


            return argFiscalYearVariant;
        }


        public ICollection<FiscalYearVariant> colGetFiscalYearVariant()
        {
            List<FiscalYearVariant> lst = new List<FiscalYearVariant>();
            DataSet DataSetToFill = new DataSet();
            FiscalYearVariant tFiscalYearVariant = new FiscalYearVariant();

            DataSetToFill = this.GetFiscalYearVariant();

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateFiscalYearVariant(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


       


        public DataSet GetFiscalYearVariant(int iIsDeleted)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + FiscalYearVariantTable.ToString();

                if (iIsDeleted >= 0)
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " IsDeleted = " + iIsDeleted;
                }
                ds = da.FillDataSetWithSQL(tSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }


        public DataSet GetFiscalYearVariant(string argFiscalYearType)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@FiscalYearType", argFiscalYearType);

            DataSetToFill = da.FillDataSet("SP_GetFiscalYearVariant4ID", param);

            return DataSetToFill;
        }


        public DataSet GetFiscalYearVariant()
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            DataSetToFill = da.FillDataSet("SP_GetFiscalYearVariant");
            return DataSetToFill;
        }


        private FiscalYearVariant objCreateFiscalYearVariant(DataRow dr)
        {
            FiscalYearVariant tFiscalYearVariant = new FiscalYearVariant();

            tFiscalYearVariant.SetObjectInfo(dr);

            return tFiscalYearVariant;

        }


        public ICollection<ErrorHandler> SaveFiscalYearVariant(FiscalYearVariant argFiscalYearVariant)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsFiscalYearVariantExists(argFiscalYearVariant.FiscalYearType) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertFiscalYearVariant(argFiscalYearVariant, da, lstErr);
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
                    UpdateFiscalYearVariant(argFiscalYearVariant, da, lstErr);
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


        public void InsertFiscalYearVariant(FiscalYearVariant argFiscalYearVariant, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@FiscalYearType", argFiscalYearVariant.FiscalYearType);
            param[1] = new SqlParameter("@FYDesc", argFiscalYearVariant.FYDesc);
            param[2] = new SqlParameter("@FYearStart", argFiscalYearVariant.FYearStart);
            param[3] = new SqlParameter("@FYearEnd", argFiscalYearVariant.FYearEnd);
            param[4] = new SqlParameter("@CreatedBy", argFiscalYearVariant.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argFiscalYearVariant.ModifiedBy);
                        

            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertFiscalYearVariant", param);

            string strType = Convert.ToString(param[6].Value);
            string strMessage = Convert.ToString(param[7].Value);            
            string strRetValue = Convert.ToString(param[8].Value);


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


        public void UpdateFiscalYearVariant(FiscalYearVariant argFiscalYearVariant, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@FiscalYearType", argFiscalYearVariant.FiscalYearType);
            param[1] = new SqlParameter("@FYDesc", argFiscalYearVariant.FYDesc);
            param[2] = new SqlParameter("@FYearStart", argFiscalYearVariant.FYearStart);
            param[3] = new SqlParameter("@FYearEnd", argFiscalYearVariant.FYearEnd);
            param[4] = new SqlParameter("@CreatedBy", argFiscalYearVariant.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argFiscalYearVariant.ModifiedBy);
            

            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateFiscalYearVariant", param);

            string strType = Convert.ToString(param[6].Value);
            string strMessage = Convert.ToString(param[7].Value);            
            string strRetValue = Convert.ToString(param[8].Value);


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


        public ICollection<ErrorHandler> DeleteFiscalYearVariant(string argFiscalYearType, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@FiscalYearType", argFiscalYearType);
                param[1] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[2] = new SqlParameter("@Type", SqlDbType.Char);
                param[2].Size = 1;
                param[2].Direction = ParameterDirection.Output;

                param[3] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[3].Size = 255;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[4].Size = 20;
                param[4].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteFiscalYearVariant", param);


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


        public bool blnIsFiscalYearVariantExists(string argFiscalYearType)
        {
            bool IsFiscalYearVariantExists = false;
            DataSet ds = new DataSet();
            ds = GetFiscalYearVariant(argFiscalYearType);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsFiscalYearVariantExists = true;
            }
            else
            {
                IsFiscalYearVariantExists = false;
            }
            return IsFiscalYearVariantExists;
        }
    }
}