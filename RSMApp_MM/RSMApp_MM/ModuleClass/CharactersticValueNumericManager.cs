
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
    public class CharactersticValueNumericManager
    {
        const string CharactersticValueNumericTable = "CharactersticValueNumeric";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CharactersticValueNumeric objGetCharactersticValueNumeric(int argidCharactersticsValue, string argCharactersticsName, string argClientCode)
        {
            CharactersticValueNumeric argCharactersticValueNumeric = new CharactersticValueNumeric();
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

            DataSetToFill = this.GetCharactersticValueNumeric(argidCharactersticsValue, argCharactersticsName, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCharactersticValueNumeric = this.objCreateCharactersticValueNumeric((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCharactersticValueNumeric;
        }
        
        public ICollection<CharactersticValueNumeric> colGetCharactersticValueNumeric(string argClientCode)
        {
            List<CharactersticValueNumeric> lst = new List<CharactersticValueNumeric>();
            DataSet DataSetToFill = new DataSet();
            CharactersticValueNumeric tCharactersticValueNumeric = new CharactersticValueNumeric();

            DataSetToFill = this.GetCharactersticValueNumeric(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCharactersticValueNumeric(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetCharactersticValueNumeric(int argidCharactersticsValue, string argCharactersticsName, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@idCharactersticsValue", argidCharactersticsValue);
            param[1] = new SqlParameter("@CharactersticsName", argCharactersticsName);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCharactersticValueNumeric4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCharactersticValueNumeric(int argidCharactersticsValue, string argCharactersticsName, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@idCharactersticsValue", argidCharactersticsValue);
            param[1] = new SqlParameter("@CharactersticsName", argCharactersticsName);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCharactersticValueNumeric4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCharactersticValueNumeric(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
          
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCharactersticValueNumeric",param);
            return DataSetToFill;
        }
        
        private CharactersticValueNumeric objCreateCharactersticValueNumeric(DataRow dr)
        {
            CharactersticValueNumeric tCharactersticValueNumeric = new CharactersticValueNumeric();

            tCharactersticValueNumeric.SetObjectInfo(dr);

            return tCharactersticValueNumeric;

        }
        
        //public ICollection<ErrorHandler> SaveCharactersticValueNumeric(CharactersticValueNumeric argCharactersticValueNumeric)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsCharactersticValueNumericExists(argCharactersticValueNumeric.idCharactersticsValue, argCharactersticValueNumeric.CharactersticsName, argCharactersticValueNumeric.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertCharactersticValueNumeric(argCharactersticValueNumeric, da, lstErr);
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
        //            UpdateCharactersticValueNumeric(argCharactersticValueNumeric, da, lstErr);
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

        public void SaveCharactersticValueNumeric(CharactersticValueNumeric argCharactersticValueNumeric, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsCharactersticValueNumericExists(argCharactersticValueNumeric.idCharactersticsValue, argCharactersticValueNumeric.CharactersticsName, argCharactersticValueNumeric.ClientCode, da) == false)
                {
                    InsertCharactersticValueNumeric(argCharactersticValueNumeric, da, lstErr);
                }
                else
                {
                    UpdateCharactersticValueNumeric(argCharactersticValueNumeric, da, lstErr);
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
        
        public void InsertCharactersticValueNumeric(CharactersticValueNumeric argCharactersticValueNumeric, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@idCharactersticsValue", argCharactersticValueNumeric.idCharactersticsValue);
            param[1] = new SqlParameter("@CharactersticsName", argCharactersticValueNumeric.CharactersticsName);
            param[2] = new SqlParameter("@CharactersticsValue", argCharactersticValueNumeric.CharactersticsValue);
            param[3] = new SqlParameter("@ClientCode", argCharactersticValueNumeric.ClientCode);
            
            param[4] = new SqlParameter("@Type", SqlDbType.Char);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[5].Size = 255;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCharactersticValueNumeric", param);
            
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
        
        public void UpdateCharactersticValueNumeric(CharactersticValueNumeric argCharactersticValueNumeric, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@idCharactersticsValue", argCharactersticValueNumeric.idCharactersticsValue);
            param[1] = new SqlParameter("@CharactersticsName", argCharactersticValueNumeric.CharactersticsName);
            param[2] = new SqlParameter("@CharactersticsValue", argCharactersticValueNumeric.CharactersticsValue);
            param[3] = new SqlParameter("@ClientCode", argCharactersticValueNumeric.ClientCode);
            
            param[4] = new SqlParameter("@Type", SqlDbType.Char);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[5].Size = 255;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCharactersticValueNumeric", param);
            
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
        
        public ICollection<ErrorHandler> DeleteCharactersticValueNumeric(int argidCharactersticsValue, string argCharactersticsName, string argClientCode,int iIsDeleted)
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

                int i = da.ExecuteNonQuery("Proc_DeleteCharactersticValueNumeric", param);
                
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

        public void DeleteCharactersticValueNumeric(int argidCharactersticsValue, string argCharactersticsName, string argClientCode, int iIsDeleted, DataAccess da, List<ErrorHandler> lstErr)
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

                int i = da.NExecuteNonQuery("Proc_DeleteCharactersticValueNumeric", param);

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

        public bool blnIsCharactersticValueNumericExists(int argidCharactersticsValue, string argCharactersticsName, string argClientCode, DataAccess da)
        {
            bool IsCharactersticValueNumericExists = false;
            DataSet ds = new DataSet();
            ds = GetCharactersticValueNumeric(argidCharactersticsValue, argCharactersticsName, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCharactersticValueNumericExists = true;
            }
            else
            {
                IsCharactersticValueNumericExists = false;
            }
            return IsCharactersticValueNumericExists;
        }
    }
}