
//Created On :: 26, December, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallClosingMasterManager
    {
        const string CallClosingMasterTable = "CallClosingMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CallClosingMaster objGetCallClosingMaster(string argCallClosingCode, string argCallCode, string argClientCode)
        {
            CallClosingMaster argCallClosingMaster = new CallClosingMaster();
            DataSet DataSetToFill = new DataSet();

            if (argCallClosingCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argCallCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallClosingMaster(argCallClosingCode, argCallCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallClosingMaster = this.objCreateCallClosingMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallClosingMaster;
        }


        public ICollection<CallClosingMaster> colGetCallClosingMaster(string argCallCode, string argClientCode)
        {
            List<CallClosingMaster> lst = new List<CallClosingMaster>();
            DataSet DataSetToFill = new DataSet();
            CallClosingMaster tCallClosingMaster = new CallClosingMaster();

            DataSetToFill = this.GetCallClosingMaster(argCallCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCallClosingMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetCallClosingMaster(string argCallClosingCode, string argCallCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallClosingCode", argCallClosingCode);
            param[1] = new SqlParameter("@CallCode", argCallCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallClosingMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallClosingMaster(string argCallClosingCode, string argCallCode, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallClosingCode", argCallClosingCode);
            param[1] = new SqlParameter("@CallCode", argCallCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallClosingMaster4ID", param);

            return DataSetToFill;
        }



        public DataSet GetCallClosingMaster(string argCallCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];            
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetCallClosingMaster",param);
            return DataSetToFill;
        }


        private CallClosingMaster objCreateCallClosingMaster(DataRow dr)
        {
            CallClosingMaster tCallClosingMaster = new CallClosingMaster();

            tCallClosingMaster.SetObjectInfo(dr);

            return tCallClosingMaster;

        }


        public ICollection<ErrorHandler> SaveCallClosingMaster(CallClosingMaster argCallClosingMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallClosingMasterExists(argCallClosingMaster.CallClosingCode, argCallClosingMaster.CallCode, argCallClosingMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallClosingMaster(argCallClosingMaster, da, lstErr);
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
                    UpdateCallClosingMaster(argCallClosingMaster, da, lstErr);
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
        
        public void SaveCallClosingMaster(CallClosingMaster argCallClosingMaster, DataAccess da,  List<ErrorHandler> lstErr )
        {
            try
            {
                if (blnIsCallClosingMasterExists(argCallClosingMaster.CallClosingCode, argCallClosingMaster.CallCode, argCallClosingMaster.ClientCode, da) == false)
                {
                     InsertCallClosingMaster(argCallClosingMaster, da, lstErr);

                }
                else
                {
                       UpdateCallClosingMaster(argCallClosingMaster, da, lstErr);
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

        public void InsertCallClosingMaster(CallClosingMaster argCallClosingMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];

            param[0] = new SqlParameter("@CallClosingCode", argCallClosingMaster.CallClosingCode);
            param[1] = new SqlParameter("@CallClosingDocTypeCode", argCallClosingMaster.CallClosingDocTypeCode);
            param[2] = new SqlParameter("@CallCode", argCallClosingMaster.CallCode);
            param[3] = new SqlParameter("@CallClosingDate", argCallClosingMaster.CallClosingDate);
            param[4] = new SqlParameter("@NetPayable", argCallClosingMaster.NetPayable);
            param[5] = new SqlParameter("@PaidAmt", argCallClosingMaster.PaidAmt);
            param[6] = new SqlParameter("@PartnerCode", argCallClosingMaster.PartnerCode);
            param[7] = new SqlParameter("@ClientCode", argCallClosingMaster.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argCallClosingMaster.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argCallClosingMaster.ModifiedBy);       

            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallClosingMaster", param);
            
            string strMessage = Convert.ToString(param[11].Value);
            string strType = Convert.ToString(param[10].Value);
            string strRetValue = Convert.ToString(param[12].Value);


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

        public void UpdateCallClosingMaster(CallClosingMaster argCallClosingMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];

            param[0] = new SqlParameter("@CallClosingCode", argCallClosingMaster.CallClosingCode);
            param[1] = new SqlParameter("@CallClosingDocTypeCode", argCallClosingMaster.CallClosingDocTypeCode);
            param[2] = new SqlParameter("@CallCode", argCallClosingMaster.CallCode);
            param[3] = new SqlParameter("@CallClosingDate", argCallClosingMaster.CallClosingDate);
            param[4] = new SqlParameter("@NetPayable", argCallClosingMaster.NetPayable);
            param[5] = new SqlParameter("@PaidAmt", argCallClosingMaster.PaidAmt);
            param[6] = new SqlParameter("@PartnerCode", argCallClosingMaster.PartnerCode);
            param[7] = new SqlParameter("@ClientCode", argCallClosingMaster.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argCallClosingMaster.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argCallClosingMaster.ModifiedBy);            

            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallClosingMaster", param);
            
            string strMessage = Convert.ToString(param[11].Value);
            string strType = Convert.ToString(param[10].Value);
            string strRetValue = Convert.ToString(param[12].Value);
            
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

        public ICollection<ErrorHandler> DeleteCallClosingMaster(string argCallClosingCode, string argCallCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CallClosingCode", argCallClosingCode);
                param[1] = new SqlParameter("@CallCode", argCallCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;
                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteCallClosingMaster", param);


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

        public bool blnIsCallClosingMasterExists(string argCallClosingCode, string argCallCode, string argClientCode)
        {
            bool IsCallClosingMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetCallClosingMaster(argCallClosingCode, argCallCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallClosingMasterExists = true;
            }
            else
            {
                IsCallClosingMasterExists = false;
            }
            return IsCallClosingMasterExists;
        }

        public bool blnIsCallClosingMasterExists(string argCallClosingCode, string argCallCode, string argClientCode, DataAccess da)
        {
            bool IsCallClosingMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetCallClosingMaster(argCallClosingCode, argCallCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallClosingMasterExists = true;
            }
            else
            {
                IsCallClosingMasterExists = false;
            }
            return IsCallClosingMasterExists;
        }


    }
}