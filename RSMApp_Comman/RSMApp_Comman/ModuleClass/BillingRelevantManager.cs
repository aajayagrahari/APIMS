
//Created On :: 06, September, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Comman
{
    public class BillingRelevantManager
    {
        const string BillingRelevantTable = "BillingRelevant";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public BillingRelevant objGetBillingRelevant(string argBillingRelevantCode)
        {
            BillingRelevant argBillingRelevant = new BillingRelevant();
            DataSet DataSetToFill = new DataSet();

            if (argBillingRelevantCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetBillingRelevant(argBillingRelevantCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argBillingRelevant = this.objCreateBillingRelevant((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argBillingRelevant;
        }
        
        public ICollection<BillingRelevant> colGetBillingRelevant()
        {
            List<BillingRelevant> lst = new List<BillingRelevant>();
            DataSet DataSetToFill = new DataSet();
            BillingRelevant tBillingRelevant = new BillingRelevant();

            DataSetToFill = this.GetBillingRelevant();

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateBillingRelevant(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

       
        
        public DataSet GetBillingRelevant(string argBillingRelevantCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@BillingRelevantCode", argBillingRelevantCode);

            DataSetToFill = da.FillDataSet("SP_GetBillingRelevant4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetBillingRelevant()
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            DataSetToFill = da.FillDataSet("SP_GetBillingRelevant");
            return DataSetToFill;
        }
        
        private BillingRelevant objCreateBillingRelevant(DataRow dr)
        {
            BillingRelevant tBillingRelevant = new BillingRelevant();

            tBillingRelevant.SetObjectInfo(dr);

            return tBillingRelevant;

        }
        
        public ICollection<ErrorHandler> SaveBillingRelevant(BillingRelevant argBillingRelevant)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsBillingRelevantExists(argBillingRelevant.BillingRelevantCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertBillingRelevant(argBillingRelevant, da, lstErr);
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
                    UpdateBillingRelevant(argBillingRelevant, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
                    }
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
        
        public void InsertBillingRelevant(BillingRelevant argBillingRelevant, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@BillingRelevantCode", argBillingRelevant.BillingRelevantCode);
            param[1] = new SqlParameter("@BRelevantDesc", argBillingRelevant.BRelevantDesc);
            param[2] = new SqlParameter("@IsDeleted", argBillingRelevant.IsDeleted);

            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertBillingRelevant", param);


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
            lstErr.Add(objErrorHandler);

        }
        
        public void UpdateBillingRelevant(BillingRelevant argBillingRelevant, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@BillingRelevantCode", argBillingRelevant.BillingRelevantCode);
            param[1] = new SqlParameter("@BRelevantDesc", argBillingRelevant.BRelevantDesc);
            param[2] = new SqlParameter("@IsDeleted", argBillingRelevant.IsDeleted);

            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;
            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;
            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateBillingRelevant", param);


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
            lstErr.Add(objErrorHandler);

        }
        
        public ICollection<ErrorHandler> DeleteBillingRelevant(string argBillingRelevantCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@BillingRelevantCode", argBillingRelevantCode);

                param[1] = new SqlParameter("@Type", SqlDbType.Char);
                param[1].Size = 1;
                param[1].Direction = ParameterDirection.Output;
                param[2] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[2].Size = 255;
                param[2].Direction = ParameterDirection.Output;
                param[3] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[3].Size = 20;
                param[3].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteBillingRelevant", param);


                string strMessage = Convert.ToString(param[2].Value);
                string strType = Convert.ToString(param[1].Value);
                string strRetValue = Convert.ToString(param[3].Value);


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
        
        public bool blnIsBillingRelevantExists(string argBillingRelevantCode)
        {
            bool IsBillingRelevantExists = false;
            DataSet ds = new DataSet();
            ds = GetBillingRelevant(argBillingRelevantCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsBillingRelevantExists = true;
            }
            else
            {
                IsBillingRelevantExists = false;
            }
            return IsBillingRelevantExists;
        }
    }
}