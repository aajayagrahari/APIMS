
//Created On :: 29, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class PartnerServiceTaxManager
    {
        const string PartnerServiceTaxTable = "PartnerServiceTax";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public PartnerServiceTax objGetPartnerServiceTax(int argServiceTaxID, string argClientCode)
        {
            PartnerServiceTax argPartnerServiceTax = new PartnerServiceTax();
            DataSet DataSetToFill = new DataSet();

            if (argServiceTaxID <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPartnerServiceTax(argServiceTaxID, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartnerServiceTax = this.objCreatePartnerServiceTax((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPartnerServiceTax;
        }
        
        public ICollection<PartnerServiceTax> colGetPartnerServiceTax(string argClientCode)
        {
            List<PartnerServiceTax> lst = new List<PartnerServiceTax>();
            DataSet DataSetToFill = new DataSet();
            PartnerServiceTax tPartnerServiceTax = new PartnerServiceTax();

            DataSetToFill = this.GetPartnerServiceTax(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePartnerServiceTax(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetPartnerServiceTax(int argServiceTaxID, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ServiceTaxID", argServiceTaxID);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerServiceTax4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetPartnerServiceTax(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerServiceTax", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerServiceTax4Repair(string argClientCode, DateTime argCurDate)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            param[1] = new SqlParameter("@CurDate", argCurDate);

            DataSetToFill = da.FillDataSet("SP_GetPartnerServiceTax4Repair",param);
            return DataSetToFill;
        }
        
        private PartnerServiceTax objCreatePartnerServiceTax(DataRow dr)
        {
            PartnerServiceTax tPartnerServiceTax = new PartnerServiceTax();

            tPartnerServiceTax.SetObjectInfo(dr);

            return tPartnerServiceTax;

        }
        
        public ICollection<ErrorHandler> SavePartnerServiceTax(PartnerServiceTax argPartnerServiceTax)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartnerServiceTaxExists(argPartnerServiceTax.ServiceTaxID, argPartnerServiceTax.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartnerServiceTax(argPartnerServiceTax, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
                    }
                }
                else
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    UpdatePartnerServiceTax(argPartnerServiceTax, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
                    }
                }
                da.COMMIT_TRANSACTION();
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

        public void InsertPartnerServiceTax(PartnerServiceTax argPartnerServiceTax, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@ServiceTaxID", argPartnerServiceTax.ServiceTaxID);
            param[1] = new SqlParameter("@ServiceTaxPer", argPartnerServiceTax.ServiceTaxPer);
            param[2] = new SqlParameter("@ValidFrom", argPartnerServiceTax.ValidFrom);
            param[3] = new SqlParameter("@ValidTo", argPartnerServiceTax.ValidTo);
            param[4] = new SqlParameter("@ProcedureType", argPartnerServiceTax.ProcedureType);
            param[5] = new SqlParameter("@ClientCode", argPartnerServiceTax.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argPartnerServiceTax.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argPartnerServiceTax.ModifiedBy);
         
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPartnerServiceTax", param);


            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
            string strRetValue = Convert.ToString(param[10].Value);


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

        public void UpdatePartnerServiceTax(PartnerServiceTax argPartnerServiceTax, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@ServiceTaxID", argPartnerServiceTax.ServiceTaxID);
            param[1] = new SqlParameter("@ServiceTaxPer", argPartnerServiceTax.ServiceTaxPer);
            param[2] = new SqlParameter("@ValidFrom", argPartnerServiceTax.ValidFrom);
            param[3] = new SqlParameter("@ValidTo", argPartnerServiceTax.ValidTo);
            param[4] = new SqlParameter("@ProcedureType", argPartnerServiceTax.ProcedureType);
            param[5] = new SqlParameter("@ClientCode", argPartnerServiceTax.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argPartnerServiceTax.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argPartnerServiceTax.ModifiedBy);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePartnerServiceTax", param);

            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
            string strRetValue = Convert.ToString(param[10].Value);


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

        public ICollection<ErrorHandler> DeletePartnerServiceTax(int argServiceTaxID, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@ServiceTaxID", argServiceTaxID);
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

                int i = da.ExecuteNonQuery("Proc_DeletePartnerServiceTax", param);


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

        public bool blnIsPartnerServiceTaxExists(int argServiceTaxID, string argClientCode)
        {
            bool IsPartnerServiceTaxExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerServiceTax(argServiceTaxID, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerServiceTaxExists = true;
            }
            else
            {
                IsPartnerServiceTaxExists = false;
            }
            return IsPartnerServiceTaxExists;
        }
    }
}