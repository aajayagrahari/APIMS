
//Created On :: 06, June, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SAP
{
    public class SAPParamManager
    {
        const string SAPParamTable = "SAPParam";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public SAPParam objGetSAPParam(string argProcessCode, string argParamNo, string argClientCode)
        {
            SAPParam argSAPParam = new SAPParam();
            DataSet DataSetToFill = new DataSet();

            if (argProcessCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argParamNo.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetSAPParam(argProcessCode, argParamNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSAPParam = this.objCreateSAPParam((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSAPParam;
        }

        public ICollection<SAPParam> colGetSAPParam(string argProcessCode, string argClientCode, List<SAPParam> lst)
        {
            //List<SalesOrderDetail> lst = new List<SalesOrderDetail>();
            DataSet DataSetToFill = new DataSet();
            SAPParam tSAPParam = new SAPParam();

            DataSetToFill = this.GetSAPParam(argProcessCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSAPParam(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public ICollection<SAPParam> colGetSAPParam(string argProcessCode,string argClientCode)
        {
            List<SAPParam> lst = new List<SAPParam>();
            DataSet DataSetToFill = new DataSet();
            SAPParam tSAPParam = new SAPParam();

            DataSetToFill = this.GetSAPParam(argProcessCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSAPParam(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetSAPParam(string argProcessCode, string argParamNo, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ProcessCode", argProcessCode);
            param[1] = new SqlParameter("@ParamNo", argParamNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSAPParam4ID", param);

            return DataSetToFill;
        }

        public DataSet GetSAPParam(string argProcessCode, string argParamNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ProcessCode", argProcessCode);
            param[1] = new SqlParameter("@ParamNo", argParamNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSAPParam4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetSAPParam(string argProcessCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ProcessCode", argProcessCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetSAPParam",param);
            return DataSetToFill;
        }
        
        private SAPParam objCreateSAPParam(DataRow dr)
        {
            SAPParam tSAPParam = new SAPParam();

            tSAPParam.SetObjectInfo(dr);

            return tSAPParam;

        }
        
        //public ICollection<ErrorHandler> SaveSAPParam(SAPParam argSAPParam)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsSAPParamExists(argSAPParam.ProcessCode, argSAPParam.ParamNo, argSAPParam.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertSAPParam(argSAPParam, da, lstErr);
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
        //            UpdateSAPParam(argSAPParam, da, lstErr);
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

        public void SaveSAPParam(SAPParam argSAPParam, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsSAPParamExists(argSAPParam.ProcessCode, argSAPParam.ParamNo, argSAPParam.ClientCode, da) == false)
                {
                    InsertSAPParam(argSAPParam, da, lstErr);
                }
                else
                {
                    UpdateSAPParam(argSAPParam, da, lstErr);
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
        
        public void InsertSAPParam(SAPParam argSAPParam, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@ProcessCode", argSAPParam.ProcessCode);
            param[1] = new SqlParameter("@ParamNo", argSAPParam.ParamNo);
            param[2] = new SqlParameter("@ParamName", argSAPParam.ParamName);
            param[3] = new SqlParameter("@ParamDataType", argSAPParam.ParamDataType);
            param[4] = new SqlParameter("@DataTypeFormat", argSAPParam.DataTypeFormat);
            param[5] = new SqlParameter("@InputMethod", argSAPParam.InputMethod);
            param[6] = new SqlParameter("@InputValue", argSAPParam.InputValue);
            param[7] = new SqlParameter("@ParamType", argSAPParam.ParamType);
            param[8] = new SqlParameter("@Size", argSAPParam.Size);
            param[9] = new SqlParameter("@ZeroPrefix", argSAPParam.ZeroPrefix);
            param[10] = new SqlParameter("@MapTableName", argSAPParam.MapTableName);
            param[11] = new SqlParameter("@MapFieldValue", argSAPParam.MapFieldValue);
            param[12] = new SqlParameter("@ClientCode", argSAPParam.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argSAPParam.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argSAPParam.ModifiedBy);
            

            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSAPParam", param);


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
        
        public void UpdateSAPParam(SAPParam argSAPParam, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@ProcessCode", argSAPParam.ProcessCode);
            param[1] = new SqlParameter("@ParamNo", argSAPParam.ParamNo);
            param[2] = new SqlParameter("@ParamName", argSAPParam.ParamName);
            param[3] = new SqlParameter("@ParamDataType", argSAPParam.ParamDataType);
            param[4] = new SqlParameter("@DataTypeFormat", argSAPParam.DataTypeFormat);
            param[5] = new SqlParameter("@InputMethod", argSAPParam.InputMethod);
            param[6] = new SqlParameter("@InputValue", argSAPParam.InputValue);
            param[7] = new SqlParameter("@ParamType", argSAPParam.ParamType);
            param[8] = new SqlParameter("@Size", argSAPParam.Size);
            param[9] = new SqlParameter("@ZeroPrefix", argSAPParam.ZeroPrefix);
            param[10] = new SqlParameter("@MapTableName", argSAPParam.MapTableName);
            param[11] = new SqlParameter("@MapFieldValue", argSAPParam.MapFieldValue);
            param[12] = new SqlParameter("@ClientCode", argSAPParam.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argSAPParam.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argSAPParam.ModifiedBy);


            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[17].Size = 20;
            param[17].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSAPParam", param);


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

        public void DeleteSAPParam(string argProcessCode, string argParamNo, string argClientCode, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@ProcessCode", argProcessCode);
                param[1] = new SqlParameter("@ParamNo", argParamNo);
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

                int i = da.NExecuteNonQuery("Proc_DeleteSAPParam", param);


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

        }
        
        public ICollection<ErrorHandler> DeleteSAPParam(string argProcessCode, string argParamNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@ProcessCode", argProcessCode);
                param[1] = new SqlParameter("@ParamNo", argParamNo);
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

                int i = da.ExecuteNonQuery("Proc_DeleteSAPParam", param);


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

        public bool blnIsSAPParamExists(string argProcessCode, string argParamNo, string argClientCode,DataAccess da)
        {
            bool IsSAPParamExists = false;
            DataSet ds = new DataSet();
            ds = GetSAPParam(argProcessCode, argParamNo, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSAPParamExists = true;
            }
            else
            {
                IsSAPParamExists = false;
            }
            return IsSAPParamExists;
        }
    }
}