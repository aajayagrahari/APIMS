
//Created On :: 19, June, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_MM
{
    public class CharAllowedValueManager
    {
        const string CharAllowedValueTable = "CharAllowedValue";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public CharAllowedValue objGetCharAllowedValue(int argidClass, int argidCharacteristic, int argidCharAllowedValue, string argClientCode)
        {
            CharAllowedValue argCharAllowedValue = new CharAllowedValue();
            DataSet DataSetToFill = new DataSet();

            if (argidClass <= 0)
            {
                goto ErrorHandlers;
            }

            if (argidCharacteristic <= 0)
            {
                goto ErrorHandlers;
            }

            if (argidCharAllowedValue <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCharAllowedValue(argidClass, argidCharacteristic, argidCharAllowedValue, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCharAllowedValue = this.objCreateCharAllowedValue((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCharAllowedValue;
        }


        public ICollection<CharAllowedValue> colGetCharAllowedValue(int argidClass,  string argClientCode)
        {
            List<CharAllowedValue> lst = new List<CharAllowedValue>();
            DataSet DataSetToFill = new DataSet();
            CharAllowedValue tCharAllowedValue = new CharAllowedValue();

            DataSetToFill = this.GetCharAllowedValue(argidClass, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCharAllowedValue(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetCharAllowedValue(int argidClass, int argidCharacteristic, int argidCharAllowedValue, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@idClass", argidClass);
            param[1] = new SqlParameter("@idCharacteristic", argidCharacteristic);
            param[2] = new SqlParameter("@idCharAllowedValue", argidCharAllowedValue);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCharAllowedValue4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCharAllowedValue(int argidClass, int argidCharacteristic, int argidCharAllowedValue, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@idClass", argidClass);
            param[1] = new SqlParameter("@idCharacteristic", argidCharacteristic);
            param[2] = new SqlParameter("@idCharAllowedValue", argidCharAllowedValue);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCharAllowedValue4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCharAllowedValue(int argidClass, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@idClass", argidClass);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCharAllowedValue", param);
            return DataSetToFill;
        }
        
        private CharAllowedValue objCreateCharAllowedValue(DataRow dr)
        {
            CharAllowedValue tCharAllowedValue = new CharAllowedValue();

            tCharAllowedValue.SetObjectInfo(dr);

            return tCharAllowedValue;

        }

        public void SaveCharAllowedValue(CharAllowedValue argCharAllowedValue, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCharAllowedValueExists(argCharAllowedValue.idClass, argCharAllowedValue.idCharacteristic, argCharAllowedValue.idCharAllowedValue, argCharAllowedValue.ClientCode, da) == false)
                {
                    InsertCharAllowedValue(argCharAllowedValue, da, lstErr);
                }
                else
                {
                    UpdateCharAllowedValue(argCharAllowedValue, da, lstErr);
                }
            }
            catch(Exception ex)
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

        //public ICollection<ErrorHandler> SaveCharAllowedValue(CharAllowedValue argCharAllowedValue)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsCharAllowedValueExists(argCharAllowedValue.idClass, argCharAllowedValue.idCharacteristic, argCharAllowedValue.idCharAllowedValue, argCharAllowedValue.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertCharAllowedValue(argCharAllowedValue, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            UpdateCharAllowedValue(argCharAllowedValue, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
        //        }
        //        da.COMMIT_TRANSACTION();
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
        
        public void InsertCharAllowedValue(CharAllowedValue argCharAllowedValue, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@idClass", argCharAllowedValue.idClass);
            param[1] = new SqlParameter("@idCharacteristic", argCharAllowedValue.idCharacteristic);
            param[2] = new SqlParameter("@idCharAllowedValue", argCharAllowedValue.idCharAllowedValue);
            param[3] = new SqlParameter("@CharacteristicValueChar", argCharAllowedValue.CharacteristicValueChar);
            param[4] = new SqlParameter("@CharValueNumFrom", argCharAllowedValue.CharValueNumFrom);
            param[5] = new SqlParameter("@CharValueNumTo", argCharAllowedValue.CharValueNumTo);
            param[6] = new SqlParameter("@ClientCode", argCharAllowedValue.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argCharAllowedValue.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argCharAllowedValue.ModifiedBy);
            
            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCharAllowedValue", param);


            string strMessage = Convert.ToString(param[10].Value);
            string strType = Convert.ToString(param[9].Value);
            string strRetValue = Convert.ToString(param[11].Value);


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
        
        public void UpdateCharAllowedValue(CharAllowedValue argCharAllowedValue, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@idClass", argCharAllowedValue.idClass);
            param[1] = new SqlParameter("@idCharacteristic", argCharAllowedValue.idCharacteristic);
            param[2] = new SqlParameter("@idCharAllowedValue", argCharAllowedValue.idCharAllowedValue);
            param[3] = new SqlParameter("@CharacteristicValueChar", argCharAllowedValue.CharacteristicValueChar);
            param[4] = new SqlParameter("@CharValueNumFrom", argCharAllowedValue.CharValueNumFrom);
            param[5] = new SqlParameter("@CharValueNumTo", argCharAllowedValue.CharValueNumTo);
            param[6] = new SqlParameter("@ClientCode", argCharAllowedValue.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argCharAllowedValue.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argCharAllowedValue.ModifiedBy);
            
            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;
            
            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;
            
            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCharAllowedValue", param);


            string strMessage = Convert.ToString(param[10].Value);
            string strType = Convert.ToString(param[9].Value);
            string strRetValue = Convert.ToString(param[11].Value);


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

        public int CheckAllowedValue(string argClientCode, int argidClass, string argCharacteristicsName, string argCharactersticValue, string argCharValuetype)
        {
            DataAccess da = new DataAccess();

            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@idClass", argidClass);
            param[1] = new SqlParameter("@CharacteristicsName", argCharacteristicsName);
            param[2] = new SqlParameter("@CharactersticValue", argCharactersticValue);
            param[3] = new SqlParameter("@CharValuetype", argCharValuetype);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            param[5] = new SqlParameter("@returnvalue", SqlDbType.Int);
            param[5].Direction = ParameterDirection.ReturnValue;

            int i = da.ExecuteNonQuery("SP_CheckAllowedValue", param);

            int iRetValue = Convert.ToInt32(param[5].Value);
            

            return iRetValue;
            
        }

        public ICollection<ErrorHandler> DeleteCharAllowedValue(int argidClass, int argidCharacteristic, int argidCharAllowedValue, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@idClass", argidClass);
                param[1] = new SqlParameter("@idCharacteristic", argidCharacteristic);
                param[2] = new SqlParameter("@idCharAllowedValue", argidCharAllowedValue);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCharAllowedValue", param);


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

        public void DeleteCharAllowedValue(int argidClass, int argidCharacteristic, int argidCharAllowedValue, string argClientCode, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@idClass", argidClass);
                param[1] = new SqlParameter("@idCharacteristic", argidCharacteristic);
                param[2] = new SqlParameter("@idCharAllowedValue", argidCharAllowedValue);
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
                int i = da.NExecuteNonQuery("Proc_DeleteCharAllowedValue", param);


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
        
        public bool blnIsCharAllowedValueExists(int argidClass, int argidCharacteristic, int argidCharAllowedValue, string argClientCode, DataAccess da)
        {
            bool IsCharAllowedValueExists = false;
            DataSet ds = new DataSet();
            ds = GetCharAllowedValue(argidClass, argidCharacteristic, argidCharAllowedValue, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCharAllowedValueExists = true;
            }
            else
            {
                IsCharAllowedValueExists = false;
            }
            return IsCharAllowedValueExists;
        }
    }
}