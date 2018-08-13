
//Created On :: 17, September, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class BillingPartnerManager
    {
        const string BillingPartnerTable = "BillingPartner";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public BillingPartner objGetBillingPartner(string argBillingDocCode, string argPFunctionCode, string argClientCode)
        {
            BillingPartner argBillingPartner = new BillingPartner();
            DataSet DataSetToFill = new DataSet();

            if (argBillingDocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argPFunctionCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetBillingPartner(argBillingDocCode, argPFunctionCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argBillingPartner = this.objCreateBillingPartner((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argBillingPartner;
        }
        
        public ICollection<BillingPartner> colGetBillingPartner(string argBillingDocCode,string argClientCode)
        {
            List<BillingPartner> lst = new List<BillingPartner>();
            DataSet DataSetToFill = new DataSet();
            BillingPartner tBillingPartner = new BillingPartner();

            DataSetToFill = this.GetBillingPartner(argBillingDocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateBillingPartner(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetBillingPartner(string argBillingDocCode, string argPFunctionCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BillingDocCode", argBillingDocCode);
            param[1] = new SqlParameter("@PFunctionCode", argPFunctionCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBillingPartner4ID", param);

            return DataSetToFill;
        }

        public DataSet GetBillingPartner(string argBillingDocCode, string argPFunctionCode, string argClientCode,DataAccess da)
        {
        
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BillingDocCode", argBillingDocCode);
            param[1] = new SqlParameter("@PFunctionCode", argPFunctionCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetBillingPartner4ID", param);

            return DataSetToFill;
        }

        public DataSet GetBillingPartner(string argBillingDocCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@BillingDocCode", argBillingDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBillingPartner",param);
            return DataSetToFill;
        }
        
        private BillingPartner objCreateBillingPartner(DataRow dr)
        {
            BillingPartner tBillingPartner = new BillingPartner();

            tBillingPartner.SetObjectInfo(dr);

            return tBillingPartner;

        }
        
        //public ICollection<ErrorHandler> SaveBillingPartner(BillingPartner argBillingPartner)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsBillingPartnerExists(argBillingPartner.BillingDocCode, argBillingPartner.PFunctionCode, argBillingPartner.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertBillingPartner(argBillingPartner, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
        //            da.COMMIT_TRANSACTION();
        //        }
        //        else
        //        {
        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            UpdateBillingPartner(argBillingPartner, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
        //            da.COMMIT_TRANSACTION();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (da != null)
        //        {
        //            da.ROLLBACK_TRANSACTION();
        //        }
        //        objErrorHandler.Type = ErrorConstant.strAboartType;
        //        objErrorHandler.MsgId = 0;
        //        objErrorHandler.Module = ErrorConstant.strInsertModule;
        //        objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
        //        objErrorHandler.Message = ex.Message.ToString();
        //        objErrorHandler.RowNo = 0;
        //        objErrorHandler.FieldName = "";
        //        objErrorHandler.LogCode = "";
        //        lstErr.Add(objErrorHandler);
        //    }
        //    finally
        //    {
        //        if (da != null)
        //        {
        //            da.Close_Connection();
        //            da = null;
        //        }
        //    }
        //    return lstErr;
        //}

        public void SaveBillingPartner(BillingPartner argBillingPartner, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsBillingPartnerExists(argBillingPartner.BillingDocCode.Trim(), argBillingPartner.PFunctionCode.Trim(), argBillingPartner.ClientCode.Trim(), da) == false)
                {
                    InsertBillingPartner(argBillingPartner, da, lstErr);
                }
                else
                {
                    UpdateBillingPartner(argBillingPartner, da, lstErr);
                }
            }
            catch (Exception ex)
            {
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
        }

        public void InsertBillingPartner(BillingPartner argBillingPartner, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@BillingDocCode", argBillingPartner.BillingDocCode);
            param[1] = new SqlParameter("@PFunctionCode", argBillingPartner.PFunctionCode);
            param[2] = new SqlParameter("@PartnerType", argBillingPartner.PartnerType);
            param[3] = new SqlParameter("@CustomerCode", argBillingPartner.CustomerCode);
            param[4] = new SqlParameter("@ClientCode", argBillingPartner.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argBillingPartner.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argBillingPartner.ModifiedBy);
     

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertBillingPartner", param);


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
        
        public void UpdateBillingPartner(BillingPartner argBillingPartner, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@BillingDocCode", argBillingPartner.BillingDocCode);
            param[1] = new SqlParameter("@PFunctionCode", argBillingPartner.PFunctionCode);
            param[2] = new SqlParameter("@PartnerType", argBillingPartner.PartnerType);
            param[3] = new SqlParameter("@CustomerCode", argBillingPartner.CustomerCode);
            param[4] = new SqlParameter("@ClientCode", argBillingPartner.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argBillingPartner.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argBillingPartner.ModifiedBy);


            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateBillingPartner", param);
            
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
        
        public ICollection<ErrorHandler> DeleteBillingPartner(string argBillingDocCode, string argPFunctionCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@BillingDocCode", argBillingDocCode);
                param[1] = new SqlParameter("@PFunctionCode", argPFunctionCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteBillingPartner", param);


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

        public void DeleteBillingPartner(string argBillingDocCode, string argPFunctionCode, string argClientCode, int iIsDeleted, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[7];

                param[0] = new SqlParameter("@BillingDocCode", argBillingDocCode);
                param[1] = new SqlParameter("@PFunctionCode", argPFunctionCode);
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

                int i = da.NExecuteNonQuery("Proc_DeleteBillingPartner", param);

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
        }

        public bool blnIsBillingPartnerExists(string argBillingDocCode, string argPFunctionCode, string argClientCode)
        {
            bool IsBillingPartnerExists = false;
            DataSet ds = new DataSet();
            ds = GetBillingPartner(argBillingDocCode, argPFunctionCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsBillingPartnerExists = true;
            }
            else
            {
                IsBillingPartnerExists = false;
            }
            return IsBillingPartnerExists;
        }

        public bool blnIsBillingPartnerExists(string argBillingDocCode, string argPFunctionCode, string argClientCode,DataAccess da)
        {
            bool IsBillingPartnerExists = false;
            DataSet ds = new DataSet();
            ds = GetBillingPartner(argBillingDocCode, argPFunctionCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsBillingPartnerExists = true;
            }
            else
            {
                IsBillingPartnerExists = false;
            }
            return IsBillingPartnerExists;
        }
    }
}