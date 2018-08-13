
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
    public class CharactersticValueCharManager
    {
        const string CharactersticValueCharTable = "CharactersticValueChar";

       //  GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CharactersticValueChar objGetCharactersticValueChar(int argidCharactersticsValue, string argCharactersticsName, string argClientCode)
        {
            CharactersticValueChar argCharactersticValueChar = new CharactersticValueChar();
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

            DataSetToFill = this.GetCharactersticValueChar(argidCharactersticsValue, argCharactersticsName, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCharactersticValueChar = this.objCreateCharactersticValueChar((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCharactersticValueChar;
        }
        
        public ICollection<CharactersticValueChar> colGetCharactersticValueChar(string argClientCode)
        {
            List<CharactersticValueChar> lst = new List<CharactersticValueChar>();
            DataSet DataSetToFill = new DataSet();
            CharactersticValueChar tCharactersticValueChar = new CharactersticValueChar();

            DataSetToFill = this.GetCharactersticValueChar(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCharactersticValueChar(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetCharactersticValueChar(int argidCharactersticsValue, string argCharactersticsName, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@idCharactersticsValue", argidCharactersticsValue);
            param[1] = new SqlParameter("@CharactersticsName", argCharactersticsName);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCharactersticValueChar4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCharactersticValueChar(int argidCharactersticsValue, string argCharactersticsName, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@idCharactersticsValue", argidCharactersticsValue);
            param[1] = new SqlParameter("@CharactersticsName", argCharactersticsName);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCharactersticValueChar4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCharactersticValueChar(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);


            DataSetToFill = da.FillDataSet("SP_GetCharactersticValueChar",param);
            return DataSetToFill;
        }
        
        private CharactersticValueChar objCreateCharactersticValueChar(DataRow dr)
        {
            CharactersticValueChar tCharactersticValueChar = new CharactersticValueChar();

            tCharactersticValueChar.SetObjectInfo(dr);

            return tCharactersticValueChar;

        }

        public void SaveCharactersticValueChar(CharactersticValueChar argCharactersticValueChar, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCharactersticValueCharExists(argCharactersticValueChar.idCharactersticsValue, argCharactersticValueChar.CharactersticsName, argCharactersticValueChar.ClientCode, da) == false)
                {
                    InsertCharactersticValueChar(argCharactersticValueChar, da, lstErr);
                }
                else
                {
                    UpdateCharactersticValueChar(argCharactersticValueChar, da, lstErr);
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

        //public ICollection<ErrorHandler> SaveCharactersticValueChar(CharactersticValueChar argCharactersticValueChar)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsCharactersticValueCharExists(argCharactersticValueChar.idCharactersticsValue, argCharactersticValueChar.CharactersticsName, argCharactersticValueChar.ClientCode, da) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertCharactersticValueChar(argCharactersticValueChar, da, lstErr);
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
        //            UpdateCharactersticValueChar(argCharactersticValueChar, da, lstErr);
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
        
        public void InsertCharactersticValueChar(CharactersticValueChar argCharactersticValueChar, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@idCharactersticsValue", argCharactersticValueChar.idCharactersticsValue);
            param[1] = new SqlParameter("@CharactersticsName", argCharactersticValueChar.CharactersticsName);
            param[2] = new SqlParameter("@CharactersticsValue", argCharactersticValueChar.CharactersticsValue);
            param[3] = new SqlParameter("@ClientCode", argCharactersticValueChar.ClientCode);
            
            param[4] = new SqlParameter("@Type", SqlDbType.Char);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[5].Size = 255;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCharactersticValueChar", param);
            
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
        
        public void UpdateCharactersticValueChar(CharactersticValueChar argCharactersticValueChar, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@idCharactersticsValue", argCharactersticValueChar.idCharactersticsValue);
            param[1] = new SqlParameter("@CharactersticsName", argCharactersticValueChar.CharactersticsName);
            param[2] = new SqlParameter("@CharactersticsValue", argCharactersticValueChar.CharactersticsValue);
            param[3] = new SqlParameter("@ClientCode", argCharactersticValueChar.ClientCode);
            
            param[4] = new SqlParameter("@Type", SqlDbType.Char);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[5].Size = 255;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCharactersticValueChar", param);
            
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

        public void DeleteCharactersticValueChar(int argidCharactersticsValue, string argCharactersticsName, string argClientCode, int iIsDeleted, DataAccess da, List<ErrorHandler> lstErr)
        {
           // DataAccess da = new DataAccess();
           // List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
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

                int i = da.NExecuteNonQuery("Proc_DeleteCharactersticValueChar", param);

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

        public ICollection<ErrorHandler> DeleteCharactersticValueChar(int argidCharactersticsValue, string argCharactersticsName, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
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

                int i = da.ExecuteNonQuery("Proc_DeleteCharactersticValueChar", param);
                
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
        
        public bool blnIsCharactersticValueCharExists(int argidCharactersticsValue, string argCharactersticsName, string argClientCode, DataAccess da)
        {
            bool IsCharactersticValueCharExists = false;
            DataSet ds = new DataSet();
            ds = GetCharactersticValueChar(argidCharactersticsValue, argCharactersticsName, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCharactersticValueCharExists = true;
            }
            else
            {
                IsCharactersticValueCharExists = false;
            }
            return IsCharactersticValueCharExists;
        }
    }
}