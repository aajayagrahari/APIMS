
//Created On :: 04, July, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class SalesOrderPartnerManager
    {
        const string SalesOrderPartnerTable = "SalesOrderPartner";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public SalesOrderPartner objGetSalesOrderPartner(string argSODocmentCode, string argPFunctionCode,  string argClientCode)
        {
            SalesOrderPartner argSalesOrderPartner = new SalesOrderPartner();
            DataSet DataSetToFill = new DataSet();

            if (argSODocmentCode.Trim() == "")
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

            DataSetToFill = this.GetSalesOrderPartner(argSODocmentCode, argClientCode, argPFunctionCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSalesOrderPartner = this.objCreateSalesOrderPartner((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSalesOrderPartner;
        }
        
        public ICollection<SalesOrderPartner> colGetSalesOrderPartner(string argSODocmentCode, string argClientCode)
        {
            List<SalesOrderPartner> lst = new List<SalesOrderPartner>();
            DataSet DataSetToFill = new DataSet();
            SalesOrderPartner tSalesOrderPartner = new SalesOrderPartner();

            DataSetToFill = this.GetSalesOrderPartner(argSODocmentCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSalesOrderPartner(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetSalesOrderPartner(string argSODocmentCode, string argClientCode, string argPFunctionCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SODocmentCode", argSODocmentCode);
            param[1] = new SqlParameter("@PFunctionCode", argPFunctionCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrderPartner4ID", param);

            return DataSetToFill;
        }

        public DataSet GetSalesOrderPartner(string argSODocmentCode, string argClientCode, string argPFunctionCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SODocmentCode", argSODocmentCode);
            param[1] = new SqlParameter("@PFunctionCode", argPFunctionCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSalesOrderPartner4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetSalesOrderPartner(string argSODocmentCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SODocmentCode", argSODocmentCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrderPartner",param);
            return DataSetToFill;
        }

        public DataSet GetSalesOrderPartner4DC(string argSODocmentCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SODocmentCode", argSODocmentCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrderPartner4DC", param);
            return DataSetToFill;
        }

        public DataSet GetSalesOrderPartner4BD(string argSODocmentCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SODocmentCode", argSODocmentCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesOrderPartner4BD", param);
            return DataSetToFill;
        }

        private SalesOrderPartner objCreateSalesOrderPartner(DataRow dr)
        {
            SalesOrderPartner tSalesOrderPartner = new SalesOrderPartner();

            tSalesOrderPartner.SetObjectInfo(dr);

            return tSalesOrderPartner;

        }

        public void SaveSalesOrderPartner(SalesOrderPartner argSalesOrderPartner, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsSalesOrderPartnerExists(argSalesOrderPartner.SODocmentCode, argSalesOrderPartner.PFunctionCode, argSalesOrderPartner.ClientCode, da) == false)
                {
                    InsertSalesOrderPartner(argSalesOrderPartner, da, lstErr);
                }
                else
                {
                    UpdateSalesOrderPartner(argSalesOrderPartner, da, lstErr);
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
        
        //public ICollection<ErrorHandler> SaveSalesOrderPartner(SalesOrderPartner argSalesOrderPartner)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsSalesOrderPartnerExists(argSalesOrderPartner.SODocmentCode, argSalesOrderPartner.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertSalesOrderPartner(argSalesOrderPartner, da, lstErr);
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
        //            UpdateSalesOrderPartner(argSalesOrderPartner, da, lstErr);
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
        
        public void InsertSalesOrderPartner(SalesOrderPartner argSalesOrderPartner, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@SODocmentCode", argSalesOrderPartner.SODocmentCode);
            param[1] = new SqlParameter("@PFunctionCode", argSalesOrderPartner.PFunctionCode);
            param[2] = new SqlParameter("@PartnerType", argSalesOrderPartner.PartnerType);
            param[3] = new SqlParameter("@CustomerCode", argSalesOrderPartner.CustomerCode);
            param[4] = new SqlParameter("@ClientCode", argSalesOrderPartner.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argSalesOrderPartner.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argSalesOrderPartner.ModifiedBy);
            
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSalesOrderPartner", param);


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
        
        public void UpdateSalesOrderPartner(SalesOrderPartner argSalesOrderPartner, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@SODocmentCode", argSalesOrderPartner.SODocmentCode);
            param[1] = new SqlParameter("@PFunctionCode", argSalesOrderPartner.PFunctionCode);
            param[2] = new SqlParameter("@PartnerType", argSalesOrderPartner.PartnerType);
            param[3] = new SqlParameter("@CustomerCode", argSalesOrderPartner.CustomerCode);
            param[4] = new SqlParameter("@ClientCode", argSalesOrderPartner.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argSalesOrderPartner.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argSalesOrderPartner.ModifiedBy);
            
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSalesOrderPartner", param);
            
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

        public ICollection<ErrorHandler> DeleteSalesOrderPartner(string argSODocmentCode,  string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@SODocmentCode", argSODocmentCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted",iIsDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteSalesOrderPartner", param);
                
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

        public bool blnIsSalesOrderPartnerExists(string argSODocmentCode, string argPFunctionCode, string argClientCode, DataAccess da)
        {
            bool IsSalesOrderPartnerExists = false;
            DataSet ds = new DataSet();
            ds = GetSalesOrderPartner(argSODocmentCode.Trim(), argClientCode, argPFunctionCode.Trim(), da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSalesOrderPartnerExists = true;
            }
            else
            {
                IsSalesOrderPartnerExists = false;
            }
            return IsSalesOrderPartnerExists;
        }
    }
}