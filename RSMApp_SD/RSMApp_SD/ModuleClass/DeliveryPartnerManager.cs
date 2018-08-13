
//Created On :: 12, July, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class DeliveryPartnerManager
    {
        const string DeliveryPartnerTable = "DeliveryPartner";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public DeliveryPartner objGetDeliveryPartner(string argDeliveryDocCode, string argPFunctionCode, string argClientCode)
        {
            DeliveryPartner argDeliveryPartner = new DeliveryPartner();
            DataSet DataSetToFill = new DataSet();

            if (argDeliveryDocCode.Trim() == "")
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

            DataSetToFill = this.GetDeliveryPartner(argDeliveryDocCode, argPFunctionCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argDeliveryPartner = this.objCreateDeliveryPartner((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argDeliveryPartner;
        }
        
        public ICollection<DeliveryPartner> colGetDeliveryPartner(string argDeliveryDocCode, string argClientCode)
        {
            List<DeliveryPartner> lst = new List<DeliveryPartner>();
            DataSet DataSetToFill = new DataSet();
            DeliveryPartner tDeliveryPartner = new DeliveryPartner();

            DataSetToFill = this.GetDeliveryPartner(argDeliveryDocCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDeliveryPartner(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetDeliveryPartner(string argDeliveryDocCode, string argPFunctionCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@PFunctionCode", argPFunctionCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDeliveryPartner4ID", param);

            return DataSetToFill;
        }

        public DataSet GetDeliveryPartner(string argDeliveryDocCode, string argPFunctionCode, string argClientCode,DataAccess da)
        {
           DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@PFunctionCode", argPFunctionCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetDeliveryPartner4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetDeliveryPartner(string argDeliveryDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            
            DataSetToFill = da.FillDataSet("SP_GetDeliveryPartner",param);
            return DataSetToFill;
        }

        public DataSet GetDeliveryPartner4BD(string argDeliveryDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDeliveryPartner4BD", param);
            return DataSetToFill;
        }
        
        private DeliveryPartner objCreateDeliveryPartner(DataRow dr)
        {
            DeliveryPartner tDeliveryPartner = new DeliveryPartner();

            tDeliveryPartner.SetObjectInfo(dr);

            return tDeliveryPartner;
        }

        public void SaveDeliveryPartner(DeliveryPartner argDeliveryPartner, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsDeliveryPartnerExists(argDeliveryPartner.DeliveryDocCode.Trim(), argDeliveryPartner.PFunctionCode.Trim(), argDeliveryPartner.ClientCode.Trim(), da) == false)
                {
                    InsertDeliveryPartner(argDeliveryPartner, da, lstErr);
                }
                else
                {
                    UpdateDeliveryPartner(argDeliveryPartner, da, lstErr);
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
        
        //public ICollection<ErrorHandler> SaveDeliveryPartner(DeliveryPartner argDeliveryPartner)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsDeliveryPartnerExists(argDeliveryPartner.DeliveryDocCode, argDeliveryPartner.PFunctionCode, argDeliveryPartner.ClientCode,da) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertDeliveryPartner(argDeliveryPartner, da, lstErr);
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
        //            UpdateDeliveryPartner(argDeliveryPartner, da, lstErr);
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
        
        public void InsertDeliveryPartner(DeliveryPartner argDeliveryPartner, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryPartner.DeliveryDocCode);
            param[1] = new SqlParameter("@PFunctionCode", argDeliveryPartner.PFunctionCode);
            param[2] = new SqlParameter("@PartnerType", argDeliveryPartner.PartnerType);
            param[3] = new SqlParameter("@CustomerCode", argDeliveryPartner.CustomerCode);
            param[4] = new SqlParameter("@ClientCode", argDeliveryPartner.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argDeliveryPartner.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argDeliveryPartner.ModifiedBy);
            
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertDeliveryPartner", param);
            
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
        
        public void UpdateDeliveryPartner(DeliveryPartner argDeliveryPartner, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryPartner.DeliveryDocCode);
            param[1] = new SqlParameter("@PFunctionCode", argDeliveryPartner.PFunctionCode);
            param[2] = new SqlParameter("@PartnerType", argDeliveryPartner.PartnerType);
            param[3] = new SqlParameter("@CustomerCode", argDeliveryPartner.CustomerCode);
            param[4] = new SqlParameter("@ClientCode", argDeliveryPartner.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argDeliveryPartner.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argDeliveryPartner.ModifiedBy);
            
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateDeliveryPartner", param);
            
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

        public void DeleteDeliveryPartner(string argDeliveryDocCode, string argPFunctionCode, string argClientCode, int iIsDeleted, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[7];

                param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
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

                int i = da.NExecuteNonQuery("Proc_DeleteDeliveryPartner", param);

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

        public ICollection<ErrorHandler> DeleteDeliveryPartner(string argDeliveryDocCode, string argPFunctionCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
                param[1] = new SqlParameter("@PFunctionCode", argPFunctionCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("@IsDeleted",iIsDeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteDeliveryPartner", param);
                
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
        
        public bool blnIsDeliveryPartnerExists(string argDeliveryDocCode, string argPFunctionCode, string argClientCode, DataAccess da)
        {
            bool IsDeliveryPartnerExists = false;
            DataSet ds = new DataSet();
            ds = GetDeliveryPartner(argDeliveryDocCode.Trim(), argPFunctionCode.Trim(), argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDeliveryPartnerExists = true;
            }
            else
            {
                IsDeliveryPartnerExists = false;
            }
            return IsDeliveryPartnerExists;
        }

    }
}