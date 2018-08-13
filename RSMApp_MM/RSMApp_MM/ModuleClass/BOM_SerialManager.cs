
//Created On :: 21, July, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_MM
{
    public class BOM_SerialManager
    {
        const string BOM_SerialTable = "BOM_Serial";

        // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public BOM_Serial objGetBOM_Serial(string argMaterialCode, string argMRevisionCode, string argSerialFrom, string argClientCode)
        {
            BOM_Serial argBOM_Serial = new BOM_Serial();
            DataSet DataSetToFill = new DataSet();

            if (argMaterialCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argMRevisionCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetBOM_Serial(argMaterialCode, argMRevisionCode,argSerialFrom, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argBOM_Serial = this.objCreateBOM_Serial((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argBOM_Serial;
        }

        public ICollection<BOM_Serial> colGetBOM_Serial(string argMaterialCode, string argMRevisionCode, string argClientCode)
        {
            List<BOM_Serial> lst = new List<BOM_Serial>();
            DataSet DataSetToFill = new DataSet();
            BOM_Serial tBOM_Serial = new BOM_Serial();

            DataSetToFill = this.GetBOM_Serial(argMaterialCode, argMRevisionCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateBOM_Serial(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetBOM_Serial(string argMaterialCode, string argMRevisionCode, string argSerialFrom,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@MRevisionCode", argMRevisionCode);
            param[2] = new SqlParameter("@SerialFrom", argSerialFrom);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBOM_Serial4ID", param);

            return DataSetToFill;
        }

        public DataSet GetBOM_Serial(string argMaterialCode, string argMRevisionCode, string argSerialFrom, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@MRevisionCode", argMRevisionCode);
            param[2] = new SqlParameter("@SerialFrom", argSerialFrom);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetBOM_Serial4ID", param);

            return DataSetToFill;
        }

        public DataSet GetBOM_Serial(string argMaterialCode,string argMRevisionCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

 
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@MRevisionCode", argMRevisionCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBOM_Serial",param);
            return DataSetToFill;
        }

        private BOM_Serial objCreateBOM_Serial(DataRow dr)
        {
            BOM_Serial tBOM_Serial = new BOM_Serial();

            tBOM_Serial.SetObjectInfo(dr);

            return tBOM_Serial;

        }

        public void SaveBOM_Serial(BOM_Serial argBOM_Serial, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsBOM_SerialExists(argBOM_Serial.MaterialCode, argBOM_Serial.MRevisionCode, argBOM_Serial.SerialFrom, argBOM_Serial.ClientCode, da) == false)
                {
                    InsertBOM_Serial(argBOM_Serial, da, lstErr);
                }
                else
                {
                    UpdateBOM_Serial(argBOM_Serial, da, lstErr);
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

        //public ICollection<ErrorHandler> SaveBOM_Serial(BOM_Serial argBOM_Serial)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsBOM_SerialExists(argBOM_Serial.MaterialCode, argBOM_Serial.MRevisionCode, argBOM_Serial.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertBOM_Serial(argBOM_Serial, da, lstErr);
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
        //            UpdateBOM_Serial(argBOM_Serial, da, lstErr);
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

        public void InsertBOM_Serial(BOM_Serial argBOM_Serial, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@MaterialCode", argBOM_Serial.MaterialCode);
            param[1] = new SqlParameter("@MRevisionCode", argBOM_Serial.MRevisionCode);
            param[2] = new SqlParameter("@SerialFrom", argBOM_Serial.SerialFrom);
            param[3] = new SqlParameter("@SerialTo", argBOM_Serial.SerialTo);
            param[4] = new SqlParameter("@BatchDate", argBOM_Serial.BatchDate);
            param[5] = new SqlParameter("@ClientCode", argBOM_Serial.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argBOM_Serial.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argBOM_Serial.ModifiedBy);
         
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertBOM_Serial", param);
            
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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);
        }

        public void UpdateBOM_Serial(BOM_Serial argBOM_Serial, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@MaterialCode", argBOM_Serial.MaterialCode);
            param[1] = new SqlParameter("@MRevisionCode", argBOM_Serial.MRevisionCode);
            param[2] = new SqlParameter("@SerialFrom", argBOM_Serial.SerialFrom);
            param[3] = new SqlParameter("@SerialTo", argBOM_Serial.SerialTo);
            param[4] = new SqlParameter("@BatchDate", argBOM_Serial.BatchDate);
            param[5] = new SqlParameter("@ClientCode", argBOM_Serial.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argBOM_Serial.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argBOM_Serial.ModifiedBy);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateBOM_Serial", param);

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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);
        }

        public PartnerErrorResult_MM DeleteBOM_Serial(string argMaterialCode, string argMRevisionCode, string argClientCode, int IisDeleted)
        {
            DataAccess da = new DataAccess();
            PartnerErrorResult_MM errcol = new PartnerErrorResult_MM();

            try
            {
                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
                param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
                param[2] = new SqlParameter("@MRevisionCode", argMRevisionCode);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted",IisDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteBOM_Serial", param);
                
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
                errcol.colErrorHandler.Add(objErrorHandler);
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
                errcol.colErrorHandler.Add(objErrorHandler);
            }
            return errcol;
        }

        public bool blnIsBOM_SerialExists(string argMaterialCode, string argMRevisionCode, string argSerialFrom, string argClientCode,DataAccess da)
        {
            bool IsBOM_SerialExists = false;
            DataSet ds = new DataSet();
            ds = GetBOM_Serial(argMaterialCode, argMRevisionCode, argSerialFrom, argClientCode,da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsBOM_SerialExists = true;
            }
            else
            {
                IsBOM_SerialExists = false;
            }
            return IsBOM_SerialExists;
        }
    }
}