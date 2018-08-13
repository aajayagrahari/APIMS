
//Created On :: 18, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class CallEstimationManager
    {
        const string CallEstimationTable = "CallEstimation";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public CallEstimation objGetCallEstimation(string argCallCode, int argItemNo, int argEstimateNo, string argClientCode)
        {
            CallEstimation argCallEstimation = new CallEstimation();
            DataSet DataSetToFill = new DataSet();

            if (argCallCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argItemNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argEstimateNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCallEstimation(argCallCode, argItemNo, argEstimateNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCallEstimation = this.objCreateCallEstimation((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCallEstimation;
        }

        //public ICollection<CallEstimation> colGetCallEstimation(string argCallCode, int argItemNo, string argClientCode, List<CallEstimation> lst)
        //{
        //    //List<CallEstimation> lst = new List<CallEstimation>();
        //    DataSet DataSetToFill = new DataSet();
        //    CallEstimation tCallEstimation = new CallEstimation();

        //    DataSetToFill = this.GetCallEstimation(argCallCode, argItemNo, argClientCode);

        //    if (DataSetToFill != null)
        //    {
        //        foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
        //        {
        //            lst.Add(objCreateCallEstimation(dr));
        //        }
        //    }
        //    goto Finish;

        //Finish:
        //    DataSetToFill = null;


        //    return lst;
        //}

        //public ICollection<CallEstimation> colGetCallEstimation4Call(string argCallCode, string argClientCode, List<CallEstimation> lst)
        //{
        //    //List<CallEstimation> lst = new List<CallEstimation>();
        //    DataSet DataSetToFill = new DataSet();
        //    CallEstimation tCallEstimation = new CallEstimation();

        //    DataSetToFill = this.GetCallEstimation4Call(argCallCode, argClientCode);

        //    if (DataSetToFill != null)
        //    {
        //        foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
        //        {
        //            lst.Add(objCreateCallEstimation(dr));
        //        }
        //    }
        //    goto Finish;

        //Finish:
        //    DataSetToFill = null;


        //    return lst;
        //}

        public void colGetCallEstimation(string argCallCode, string argClientCode, ref CallEstimationCol argCallEstimationCol)
        {
            DataSet DataSetToFill = new DataSet();
            CallEstimation tCallEstimation = new CallEstimation();

            DataSetToFill = this.GetCallEstimation4Call(argCallCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argCallEstimationCol.colCallEstimation.Add(objCreateCallEstimation(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;
        }

        public void colGetCallEstimation(string argCallCode, int argItemNo, string argClientCode, ref CallEstimationCol argCallEstimationCol)
        {
            DataSet DataSetToFill = new DataSet();
            CallEstimation tCallEstimation = new CallEstimation();

            DataSetToFill = this.GetCallEstimation(argCallCode, argItemNo, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    argCallEstimationCol.colCallEstimation.Add(objCreateCallEstimation(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;
        }

        public DataSet GetCallEstimation(string argCallCode, int argItemNo, int argEstimateNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@EstimateNo", argEstimateNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallEstimation4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallEstimation(string argCallCode, int argItemNo, int argEstimateNo, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@EstimateNo", argEstimateNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCallEstimation4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCallEstimation(string argCallCode, int argItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallEstimation",param);
            return DataSetToFill;
        }

        public DataSet GetCallEstimation4Call(string argCallCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CallCode", argCallCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCallEstimation4Call", param);
            return DataSetToFill;
        }

        private CallEstimation objCreateCallEstimation(DataRow dr)
        {
            CallEstimation tCallEstimation = new CallEstimation();

            tCallEstimation.SetObjectInfo(dr);

            return tCallEstimation;

        }

        public PartnerErrorResult SaveCallEstimation(CallEstimation argCallEstimation)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            PartnerErrorResult errorcol = new PartnerErrorResult();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCallEstimationExists(argCallEstimation.CallCode, argCallEstimation.ItemNo, argCallEstimation.EstimateNo, argCallEstimation.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCallEstimation(argCallEstimation, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            errorcol.colErrorHandler.Add(objerr);
                            da.ROLLBACK_TRANSACTION();
                            return errorcol;
                        }

                        if (objerr.Type == "A")
                        {
                            errorcol.colErrorHandler.Add(objerr);
                            da.ROLLBACK_TRANSACTION();
                            return errorcol;
                        }
                    }
                 
                }
                else
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    UpdateCallEstimation(argCallEstimation, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            errorcol.colErrorHandler.Add(objerr);
                            da.ROLLBACK_TRANSACTION();
                            return errorcol;
                        }

                        if (objerr.Type == "A")
                        {
                            errorcol.colErrorHandler.Add(objerr);
                            da.ROLLBACK_TRANSACTION();
                            return errorcol;
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
                errorcol.colErrorHandler.Add(objErrorHandler);
            }
            finally
            {
                if (da != null)
                {
                    da.Close_Connection();
                    da = null;
                }
            }
            return errorcol;
        }



        public void SaveCallEstimation(CallEstimation argCallEstimation, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCallEstimationExists(argCallEstimation.CallCode, argCallEstimation.ItemNo, argCallEstimation.EstimateNo, argCallEstimation.ClientCode, da) == false)
                {
                    InsertCallEstimation(argCallEstimation, da, lstErr);
                }
                else
                {
                    UpdateCallEstimation(argCallEstimation, da, lstErr);
                }
            }
            catch (Exception ex)
            {
                objErrorHandler.Type = ErrorConstant.strAboartType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strInsertModule;
                objErrorHandler.ModulePart = ErrorConstant.strDetailModule;
                objErrorHandler.Message = ex.Message.ToString();
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";
                lstErr.Add(objErrorHandler);
            }
        }

        public void SL_SaveCallEstimation(CallEstimation argCallEstimation, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCallEstimationExists(argCallEstimation.CallCode, argCallEstimation.ItemNo, argCallEstimation.EstimateNo, argCallEstimation.ClientCode, da) == false)
                {
                   SL_InsertCallEstimation(argCallEstimation, da, lstErr);
                }
                else
                {
                    SL_UpdateCallEstimation(argCallEstimation, da, lstErr);
                }
            }
            catch (Exception ex)
            {
                objErrorHandler.Type = ErrorConstant.strAboartType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strInsertModule;
                objErrorHandler.ModulePart = ErrorConstant.strDetailModule;
                objErrorHandler.Message = ex.Message.ToString();
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";
                lstErr.Add(objErrorHandler);
            }
        }

        public void InsertCallEstimation(CallEstimation argCallEstimation, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@CallCode", argCallEstimation.CallCode);
            param[1] = new SqlParameter("@ItemNo", argCallEstimation.ItemNo);
            param[2] = new SqlParameter("@EstimateNo", argCallEstimation.EstimateNo);
            param[3] = new SqlParameter("@EstPartsCost", argCallEstimation.EstPartsCost);
            param[4] = new SqlParameter("@EstLabourCost", argCallEstimation.EstLabourCost);
            param[5] = new SqlParameter("@EstShippingCost", argCallEstimation.EstShippingCost);
            param[6] = new SqlParameter("@EstHandlingCost", argCallEstimation.EstHandlingCost);
            param[7] = new SqlParameter("@EstTravelCost", argCallEstimation.EstTravelCost);
            param[8] = new SqlParameter("@EstTotal", argCallEstimation.EstTotal);
            param[9] = new SqlParameter("@EstAppStatus", argCallEstimation.EstAppStatus);
            param[10] = new SqlParameter("@AppDate", argCallEstimation.AppDate);
            param[11] = new SqlParameter("@PartnerCode", argCallEstimation.PartnerCode);
            param[12] = new SqlParameter("@ClientCode", argCallEstimation.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argCallEstimation.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argCallEstimation.ModifiedBy);
            
            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCallEstimation", param);


            string strMessage = Convert.ToString(param[16].Value);
            string strType = Convert.ToString(param[15].Value);
            string strRetValue = Convert.ToString(param[17].Value);


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

        public void UpdateCallEstimation(CallEstimation argCallEstimation, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@CallCode", argCallEstimation.CallCode);
            param[1] = new SqlParameter("@ItemNo", argCallEstimation.ItemNo);
            param[2] = new SqlParameter("@EstimateNo", argCallEstimation.EstimateNo);
            param[3] = new SqlParameter("@EstPartsCost", argCallEstimation.EstPartsCost);
            param[4] = new SqlParameter("@EstLabourCost", argCallEstimation.EstLabourCost);
            param[5] = new SqlParameter("@EstShippingCost", argCallEstimation.EstShippingCost);
            param[6] = new SqlParameter("@EstHandlingCost", argCallEstimation.EstHandlingCost);
            param[7] = new SqlParameter("@EstTravelCost", argCallEstimation.EstTravelCost);
            param[8] = new SqlParameter("@EstTotal", argCallEstimation.EstTotal);
            param[9] = new SqlParameter("@EstAppStatus", argCallEstimation.EstAppStatus);
            param[10] = new SqlParameter("@AppDate", argCallEstimation.AppDate);
            param[11] = new SqlParameter("@PartnerCode", argCallEstimation.PartnerCode);
            param[12] = new SqlParameter("@ClientCode", argCallEstimation.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argCallEstimation.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argCallEstimation.ModifiedBy);

            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCallEstimation", param);


            string strMessage = Convert.ToString(param[16].Value);
            string strType = Convert.ToString(param[15].Value);
            string strRetValue = Convert.ToString(param[17].Value);


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

        public void SL_InsertCallEstimation(CallEstimation argCallEstimation, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@CallCode", argCallEstimation.CallCode);
            param[1] = new SqlParameter("@ItemNo", argCallEstimation.ItemNo);
            param[2] = new SqlParameter("@EstimateNo", argCallEstimation.EstimateNo);
            param[3] = new SqlParameter("@EstPartsCost", argCallEstimation.EstPartsCost);
            param[4] = new SqlParameter("@EstLabourCost", argCallEstimation.EstLabourCost);
            param[5] = new SqlParameter("@EstShippingCost", argCallEstimation.EstShippingCost);
            param[6] = new SqlParameter("@EstHandlingCost", argCallEstimation.EstHandlingCost);
            param[7] = new SqlParameter("@EstTravelCost", argCallEstimation.EstTravelCost);
            param[8] = new SqlParameter("@EstTotal", argCallEstimation.EstTotal);
            param[9] = new SqlParameter("@EstAppStatus", argCallEstimation.EstAppStatus);
            param[10] = new SqlParameter("@AppDate", argCallEstimation.AppDate);
            param[11] = new SqlParameter("@PartnerCode", argCallEstimation.PartnerCode);
            param[12] = new SqlParameter("@ClientCode", argCallEstimation.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argCallEstimation.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argCallEstimation.ModifiedBy);

            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("SL_Proc_InsertCallEstimation", param);


            string strMessage = Convert.ToString(param[16].Value);
            string strType = Convert.ToString(param[15].Value);
            string strRetValue = Convert.ToString(param[17].Value);


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

        public void SL_UpdateCallEstimation(CallEstimation argCallEstimation, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@CallCode", argCallEstimation.CallCode);
            param[1] = new SqlParameter("@ItemNo", argCallEstimation.ItemNo);
            param[2] = new SqlParameter("@EstimateNo", argCallEstimation.EstimateNo);
            param[3] = new SqlParameter("@EstPartsCost", argCallEstimation.EstPartsCost);
            param[4] = new SqlParameter("@EstLabourCost", argCallEstimation.EstLabourCost);
            param[5] = new SqlParameter("@EstShippingCost", argCallEstimation.EstShippingCost);
            param[6] = new SqlParameter("@EstHandlingCost", argCallEstimation.EstHandlingCost);
            param[7] = new SqlParameter("@EstTravelCost", argCallEstimation.EstTravelCost);
            param[8] = new SqlParameter("@EstTotal", argCallEstimation.EstTotal);
            param[9] = new SqlParameter("@EstAppStatus", argCallEstimation.EstAppStatus);
            param[10] = new SqlParameter("@AppDate", argCallEstimation.AppDate);
            param[11] = new SqlParameter("@PartnerCode", argCallEstimation.PartnerCode);
            param[12] = new SqlParameter("@ClientCode", argCallEstimation.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argCallEstimation.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argCallEstimation.ModifiedBy);

            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("SL_Proc_UpdateCallEstimation", param);


            string strMessage = Convert.ToString(param[16].Value);
            string strType = Convert.ToString(param[15].Value);
            string strRetValue = Convert.ToString(param[17].Value);


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



        public ICollection<ErrorHandler> DeleteCallEstimation(string argCallCode, int argItemNo, int argEstimateNo, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@CallCode", argCallCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@EstimateNo", argEstimateNo);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteCallEstimation", param);


                string strMessage = Convert.ToString(param[6].Value);
                string strType = Convert.ToString(param[5].Value);
                string strRetValue = Convert.ToString(param[7].Value);


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
      
        public bool blnIsCallEstimationExists(string argCallCode, int argItemNo, int argEstimateNo, string argClientCode)
        {
            bool IsCallEstimationExists = false;
            DataSet ds = new DataSet();
            ds = GetCallEstimation(argCallCode, argItemNo, argEstimateNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallEstimationExists = true;
            }
            else
            {
                IsCallEstimationExists = false;
            }
            return IsCallEstimationExists;
        }

        public bool blnIsCallEstimationExists(string argCallCode, int argItemNo, int argEstimateNo, string argClientCode, DataAccess da)
        {
            bool IsCallEstimationExists = false;
            DataSet ds = new DataSet();
            ds = GetCallEstimation(argCallCode, argItemNo, argEstimateNo, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCallEstimationExists = true;
            }
            else
            {
                IsCallEstimationExists = false;
            }
            return IsCallEstimationExists;
        }

    }
}