
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
    public class SAPFieldManager
    {
        const string SAPFieldTable = "SAPField";

        ///GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public SAPField objGetSAPField(string argProcessCode, string argParamNo, string argFieldNo, string argClientCode)
        {
            SAPField argSAPField = new SAPField();
            DataSet DataSetToFill = new DataSet();

            if (argProcessCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argParamNo.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argFieldNo.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetSAPField(argProcessCode, argParamNo, argFieldNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSAPField = this.objCreateSAPField((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSAPField;
        }
        
        public ICollection<SAPField> colGetSAPField(string argProcessCode, string argClientCode)
        {
            List<SAPField> lst = new List<SAPField>();
            DataSet DataSetToFill = new DataSet();
            SAPField tSAPField = new SAPField();

            DataSetToFill = this.GetSAPField(argProcessCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSAPField(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetSAPField(string argProcessCode, string argParamNo, string argFieldNo, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ProcessCode", argProcessCode);
            param[1] = new SqlParameter("@ParamNo", argParamNo);
            param[2] = new SqlParameter("@FieldNo", argFieldNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSAPField4ID", param);

            return DataSetToFill;
        }

        public DataSet GetSAPField(string argProcessCode, string argParamNo, string argFieldNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ProcessCode", argProcessCode);
            param[1] = new SqlParameter("@ParamNo", argParamNo);
            param[2] = new SqlParameter("@FieldNo", argFieldNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSAPField4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetSAPField(string argProcessCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ProcessCode", argProcessCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSAPField",param);
            return DataSetToFill;
        }
        
        private SAPField objCreateSAPField(DataRow dr)
        {
            SAPField tSAPField = new SAPField();

            tSAPField.SetObjectInfo(dr);

            return tSAPField;

        }
        
        //public ICollection<ErrorHandler> SaveSAPField(SAPField argSAPField)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsSAPFieldExists(argSAPField.ProcessCode, argSAPField.ParamNo, argSAPField.FieldNo, argSAPField.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertSAPField(argSAPField, da, lstErr);
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
        //            UpdateSAPField(argSAPField, da, lstErr);
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

        public void SaveSAPField(SAPField argSAPField, DataAccess da, List<ErrorHandler> lstErr)
        {            
            try
            {
                if (blnIsSAPFieldExists(argSAPField.ProcessCode, argSAPField.ParamNo, argSAPField.FieldNo, argSAPField.ClientCode, da) == false)
                {

                   
                    InsertSAPField(argSAPField, da, lstErr);
                   
                }
                else
                {
           
                    UpdateSAPField(argSAPField, da, lstErr);
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
        
        
        public void InsertSAPField(SAPField argSAPField, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@ProcessCode", argSAPField.ProcessCode);
            param[1] = new SqlParameter("@ParamNo", argSAPField.ParamNo);
            param[2] = new SqlParameter("@FieldNo", argSAPField.FieldNo);
            param[3] = new SqlParameter("@FieldName", argSAPField.FieldName);
            param[4] = new SqlParameter("@InputMethod", argSAPField.InputMethod);
            param[5] = new SqlParameter("@InputValue", argSAPField.InputValue);
            param[6] = new SqlParameter("@ParamType", argSAPField.ParamType);
            param[7] = new SqlParameter("@Size", argSAPField.Size);
            param[8] = new SqlParameter("@ZeroPrefix", argSAPField.ZeroPrefix);
            param[9] = new SqlParameter("@MapTableName", argSAPField.MapTableName);
            param[10] = new SqlParameter("@MapFieldValue", argSAPField.MapFieldValue);
            param[11] = new SqlParameter("@ParamDataType",argSAPField.ParamDataType);
            param[12] = new SqlParameter("@DataTypeFormat", argSAPField.DataTypeFormat);
            param[13] = new SqlParameter("@ClientCode", argSAPField.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argSAPField.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argSAPField.ModifiedBy);
           

            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSAPField", param);
            
            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);


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
        
        public void UpdateSAPField(SAPField argSAPField, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@ProcessCode", argSAPField.ProcessCode);
            param[1] = new SqlParameter("@ParamNo", argSAPField.ParamNo);
            param[2] = new SqlParameter("@FieldNo", argSAPField.FieldNo);
            param[3] = new SqlParameter("@FieldName", argSAPField.FieldName);
            param[4] = new SqlParameter("@InputMethod", argSAPField.InputMethod);
            param[5] = new SqlParameter("@InputValue", argSAPField.InputValue);
            param[6] = new SqlParameter("@ParamType", argSAPField.ParamType);
            param[7] = new SqlParameter("@Size", argSAPField.Size);
            param[8] = new SqlParameter("@ZeroPrefix", argSAPField.ZeroPrefix);
            param[9] = new SqlParameter("@MapTableName", argSAPField.MapTableName);
            param[10] = new SqlParameter("@MapFieldValue", argSAPField.MapFieldValue);
            param[11] = new SqlParameter("@ParamDataType", argSAPField.ParamDataType);
            param[12] = new SqlParameter("@DataTypeFormat", argSAPField.DataTypeFormat);
            param[13] = new SqlParameter("@ClientCode", argSAPField.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argSAPField.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argSAPField.ModifiedBy);


            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSAPField", param);

            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);


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

        public ICollection<ErrorHandler> DeleteSAPField(string argProcessCode, string argParamNo, string argFieldNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@ProcessCode", argProcessCode);
                param[1] = new SqlParameter("@ParamNo", argParamNo);
                param[2] = new SqlParameter("@FieldNo", argFieldNo);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
       
                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteSAPField", param);

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

        public void DeleteSAPField(string argProcessCode, string argParamNo, string argFieldNo, string argClientCode, int iIsDeleted, DataAccess da, List<ErrorHandler> lstErr)
        {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@ProcessCode", argProcessCode);
                param[1] = new SqlParameter("@ParamNo", argParamNo);
                param[2] = new SqlParameter("@FieldNo", argFieldNo);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted",iIsDeleted);
               
                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.NExecuteNonQuery("Proc_DeleteSAPField", param);

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
        
        public bool blnIsSAPFieldExists(string argProcessCode, string argParamNo, string argFieldNo, string argClientCode, DataAccess da)
        {
            bool IsSAPFieldExists = false;
            DataSet ds = new DataSet();
            ds = GetSAPField(argProcessCode, argParamNo, argFieldNo, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSAPFieldExists = true;
            }
            else
            {
                IsSAPFieldExists = false;
            }
            return IsSAPFieldExists;
        }
    }
}