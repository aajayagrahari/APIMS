
//Created On :: 25, June, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_MM
{
    public class CharactersticValueCurrencyManager
    {
        const string CharactersticValueCurrencyTable = "CharactersticValueCurrency";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CharactersticValueCurrency objGetCharactersticValueCurrency(int argidCharactersticsValue, string argCharactersticsName, string argClientCode)
        {
            CharactersticValueCurrency argCharactersticValueCurrency = new CharactersticValueCurrency();
            DataSet DataSetToFill = new DataSet();

            if (argidCharactersticsValue <= 0)
            {
                goto ErrorHandlers;
            }

            if (argCharactersticsName.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCharactersticValueCurrency(argidCharactersticsValue, argCharactersticsName, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCharactersticValueCurrency = this.objCreateCharactersticValueCurrency((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCharactersticValueCurrency;
        }
        
        public ICollection<CharactersticValueCurrency> colGetCharactersticValueCurrency(string argClientCode)
        {
            List<CharactersticValueCurrency> lst = new List<CharactersticValueCurrency>();
            DataSet DataSetToFill = new DataSet();
            CharactersticValueCurrency tCharactersticValueCurrency = new CharactersticValueCurrency();

            DataSetToFill = this.GetCharactersticValueCurrency(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCharactersticValueCurrency(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetCharactersticValueCurrency(int argidCharactersticsValue, string argCharactersticsName, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@idCharactersticsValue", argidCharactersticsValue);
            param[1] = new SqlParameter("@CharactersticsName", argCharactersticsName);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCharactersticValueCurrency4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCharactersticValueCurrency(int argidCharactersticsValue, string argCharactersticsName, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@idCharactersticsValue", argidCharactersticsValue);
            param[1] = new SqlParameter("@CharactersticsName", argCharactersticsName);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCharactersticValueCurrency4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCharactersticValueCurrency(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            
            DataSetToFill = da.FillDataSet("SP_GetCharactersticValueCurrency",param);
            return DataSetToFill;
        }
        
        private CharactersticValueCurrency objCreateCharactersticValueCurrency(DataRow dr)
        {
            CharactersticValueCurrency tCharactersticValueCurrency = new CharactersticValueCurrency();

            tCharactersticValueCurrency.SetObjectInfo(dr);

            return tCharactersticValueCurrency;

        }

        public void SaveCharactersticValueCurrency(CharactersticValueCurrency argCharactersticValueCurrency, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCharactersticValueCurrencyExists(argCharactersticValueCurrency.idCharactersticsValue, argCharactersticValueCurrency.CharactersticsName, argCharactersticValueCurrency.ClientCode, da) == false)
                {
                    InsertCharactersticValueCurrency(argCharactersticValueCurrency, da, lstErr);
                }
                else
                {
                    UpdateCharactersticValueCurrency(argCharactersticValueCurrency, da, lstErr);
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
        
        //public ICollection<ErrorHandler> SaveCharactersticValueCurrency(CharactersticValueCurrency argCharactersticValueCurrency)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsCharactersticValueCurrencyExists(argCharactersticValueCurrency.idCharactersticsValue, argCharactersticValueCurrency.CharactersticsName, argCharactersticValueCurrency.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertCharactersticValueCurrency(argCharactersticValueCurrency, da, lstErr);
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
        //            UpdateCharactersticValueCurrency(argCharactersticValueCurrency, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
        //            da.Close_Connection();
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
        
        public void InsertCharactersticValueCurrency(CharactersticValueCurrency argCharactersticValueCurrency, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@idCharactersticsValue", argCharactersticValueCurrency.idCharactersticsValue);
            param[1] = new SqlParameter("@CharactersticsName", argCharactersticValueCurrency.CharactersticsName);
            param[2] = new SqlParameter("@CharactersticsValue", argCharactersticValueCurrency.CharactersticsValue);
            param[3] = new SqlParameter("@ClientCode", argCharactersticValueCurrency.ClientCode);
            
            param[4] = new SqlParameter("@Type", SqlDbType.Char);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[5].Size = 255;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCharactersticValueCurrency", param);
            
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
        
        public void UpdateCharactersticValueCurrency(CharactersticValueCurrency argCharactersticValueCurrency, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@idCharactersticsValue", argCharactersticValueCurrency.idCharactersticsValue);
            param[1] = new SqlParameter("@CharactersticsName", argCharactersticValueCurrency.CharactersticsName);
            param[2] = new SqlParameter("@CharactersticsValue", argCharactersticValueCurrency.CharactersticsValue);
            param[3] = new SqlParameter("@ClientCode", argCharactersticValueCurrency.ClientCode);

            param[4] = new SqlParameter("@Type", SqlDbType.Char);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[5].Size = 255;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateCharactersticValueCurrency", param);


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

        public void DeleteCharactersticValueCurrency(int argidCharactersticsValue, string argCharactersticsName, string argClientCode, int iIsDeleted, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@idCharactersticsValue", argidCharactersticsValue);
                param[1] = new SqlParameter("@CharactersticsName", argCharactersticsName);
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

                int i = da.NExecuteNonQuery("Proc_DeleteCharactersticValueCurrency", param);


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
        }

        public ICollection<ErrorHandler> DeleteCharactersticValueCurrency(int argidCharactersticsValue, string argCharactersticsName, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@idCharactersticsValue", argidCharactersticsValue);
                param[1] = new SqlParameter("@CharactersticsName", argCharactersticsName);
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

                int i = da.ExecuteNonQuery("Proc_DeleteCharactersticValueCurrency", param);


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
        
        public bool blnIsCharactersticValueCurrencyExists(int argidCharactersticsValue, string argCharactersticsName, string argClientCode, DataAccess da)
        {
            bool IsCharactersticValueCurrencyExists = false;
            DataSet ds = new DataSet();
            ds = GetCharactersticValueCurrency(argidCharactersticsValue, argCharactersticsName, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCharactersticValueCurrencyExists = true;
            }
            else
            {
                IsCharactersticValueCurrencyExists = false;
            }
            return IsCharactersticValueCurrencyExists;
        }
    }
}