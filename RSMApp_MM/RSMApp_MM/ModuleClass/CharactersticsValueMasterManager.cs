
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
    public class CharactersticsValueMasterManager
    {
        const string CharactersticsValueMasterTable = "CharactersticsValueMaster";

        // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        //public CharactersticsValueMaster objGetCharactersticsValueMaster(int argidCharactersticsValue,  string argClientCode)
        //{
        //    CharactersticsValueMaster argCharactersticsValueMaster = new CharactersticsValueMaster();
        //    DataSet DataSetToFill = new DataSet();

        //    if (argidCharactersticsValue <= 0)
        //    {
        //        goto ErrorHandlers;
        //    }

        //    if (argClientCode.Trim() == "")
        //    {
        //        goto ErrorHandlers;
        //    }

        //    DataSetToFill = this.GetCharactersticsValueMaster(argidCharactersticsValue, argClientCode);

        //    if (DataSetToFill.Tables[0].Rows.Count <= 0)
        //    {
        //        goto Finish;
        //    }

        //    argCharactersticsValueMaster = this.objCreateCharactersticsValueMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

        //    goto Finish;

        //ErrorHandlers:

        //Finish:
        //    DataSetToFill = null;


        //    return argCharactersticsValueMaster;
        //}
        
        public ICollection<CharactersticsValueMaster> colGetCharactersticsValueMaster(string argClientCode)
        {
            List<CharactersticsValueMaster> lst = new List<CharactersticsValueMaster>();
            DataSet DataSetToFill = new DataSet();
            CharactersticsValueMaster tCharactersticsValueMaster = new CharactersticsValueMaster();

            DataSetToFill = this.GetCharactersticsValueMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCharactersticsValueMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;
            
            return lst;
        }

        public ICollection<CharactersticsValueMaster> colGetCharactersticsValue4Class(string argClientCode, string argMaterilCode, string argClassType, List<CharactersticsValueMaster> lst)
        {
            //List<CharactersticsValueMaster> lst = new List<CharactersticsValueMaster>();
            DataSet DataSetToFill = new DataSet();
            CharactersticsValueMaster tCharactersticsValueMaster = new CharactersticsValueMaster();

            ClassMasterManager objClassMasterManager = new ClassMasterManager();

            DataSetToFill = objClassMasterManager.GetClassMaster4Type(argClassType, argMaterilCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCharactersticsValue(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<CharactersticsValueMaster> colGetCharactersticsValue(string argClientCode, string argObjectKey, string argObjecTable, List<CharactersticsValueMaster> lst)
        {
           // List<CharactersticsValueMaster> lst = new List<CharactersticsValueMaster>();
            DataSet DataSetToFill = new DataSet();
            CharactersticsValueMaster tCharactersticsValueMaster = new CharactersticsValueMaster();

            DataSetToFill = this.GetCharactersticsValue(argClientCode, argObjectKey, argObjecTable);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCharactersticsValue(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetCharactersticsValueMaster(string argObjectKey, string argObjectTable, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ObjectKey", argObjectKey);
            param[1] = new SqlParameter("@ObjectTable", argObjectTable);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCharactersticsValueMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCharactersticsValueMaster(string argObjectKey, string argObjectTable, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ObjectKey", argObjectKey);
            param[1] = new SqlParameter("@ObjectTable", argObjectTable);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCharactersticsValueMaster4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCharactersticsValueMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCharactersticsValueMaster",param);
            return DataSetToFill;
        }

        public DataSet GetCharactersticsValue(string argClientCode, string argObjectKey, string argObjectTable)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            param[1] = new SqlParameter("@ObjectKey", argObjectKey);
            param[2] = new SqlParameter("@ObjectTable", argObjectTable);

            DataSetToFill = da.FillDataSet("SP_GetCharactersticValue4Object", param);
            return DataSetToFill;
        }
        
        private CharactersticsValueMaster objCreateCharactersticsValueMaster(DataRow dr)
        {
            CharactersticsValueMaster tCharactersticsValueMaster = new CharactersticsValueMaster();

            tCharactersticsValueMaster.SetObjectInfo(dr);

            return tCharactersticsValueMaster;

        }

        private CharactersticsValueMaster objCreateCharactersticsValue(DataRow dr)
        {
            CharactersticsValueMaster tCharactersticsValueMaster = new CharactersticsValueMaster();

            tCharactersticsValueMaster.SetObjectInfo2(dr);

            return tCharactersticsValueMaster;

        }

        public void SaveCharactersticsValueMaster(CharactersticsValueMaster argCharactersticsValueMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            int strRetValue = -1;

            try
            {
                    if (blnIsCharactersticsValueMasterExists(argCharactersticsValueMaster.ObjectKey, argCharactersticsValueMaster.ObjectTable, argCharactersticsValueMaster.ClientCode, da) == false)
                    {
                       strRetValue =  InsertCharactersticsValueMaster(argCharactersticsValueMaster, da, lstErr);
                    }
                    else
                    {
                        DataSet ds = new DataSet();
                        ds = GetCharactersticsValueMaster(argCharactersticsValueMaster.ObjectKey, argCharactersticsValueMaster.ObjectTable, argCharactersticsValueMaster.ClientCode, da);

                        if (ds != null)
                        {
                            strRetValue = Convert.ToInt32(ds.Tables[0].Rows[0]["idCharactersticsValue"]);
                        }
                       //strRetValue =  UpdateCharactersticsValueMaster(argCharactersticsValueMaster, da, lstErr);
                    }

                    if (strRetValue > 0)
                    {
                         
                        if (argCharactersticsValueMaster.CharValuetype.Trim() == "CHAR")
                        {

                            CharactersticValueChar argCharactersticValueChar = new CharactersticValueChar();
                            CharactersticValueCharManager argCharactersticValueCharManager = new CharactersticValueCharManager();

                            argCharactersticValueChar.idCharactersticsValue = strRetValue;
                            argCharactersticValueChar.CharactersticsName = argCharactersticsValueMaster.CharactersticsName;
                            argCharactersticValueChar.CharactersticsValue = argCharactersticsValueMaster.CharactersticsValue;
                            argCharactersticValueChar.ClientCode = argCharactersticsValueMaster.ClientCode;
                            
                            argCharactersticValueCharManager.SaveCharactersticValueChar(argCharactersticValueChar, da, lstErr);

                        }
                        else if (argCharactersticsValueMaster.CharValuetype.Trim() == "NUM")
                        {
                            CharactersticValueNumeric argCharactersticValueNumeric = new CharactersticValueNumeric();
                            CharactersticValueNumericManager argCharactersticValueNumericManager = new CharactersticValueNumericManager();

                            argCharactersticValueNumeric.idCharactersticsValue = strRetValue;
                            argCharactersticValueNumeric.CharactersticsName = argCharactersticsValueMaster.CharactersticsName;
                            argCharactersticValueNumeric.CharactersticsValue = Convert.ToDouble(argCharactersticsValueMaster.CharactersticsValue.Trim());
                            argCharactersticValueNumeric.ClientCode = argCharactersticsValueMaster.ClientCode;

                            argCharactersticValueNumericManager.SaveCharactersticValueNumeric(argCharactersticValueNumeric, da, lstErr);
                        }
                        else if (argCharactersticsValueMaster.CharValuetype.Trim() == "CURRENCY")
                        {
                            CharactersticValueCurrency argCharactersticValueCurrency = new CharactersticValueCurrency();
                            CharactersticValueCurrencyManager argCharactersticValueCurrencyManager = new CharactersticValueCurrencyManager();

                            argCharactersticValueCurrency.idCharactersticsValue = strRetValue;
                            argCharactersticValueCurrency.CharactersticsName = argCharactersticsValueMaster.CharactersticsName;
                            argCharactersticValueCurrency.CharactersticsValue = Convert.ToInt32(argCharactersticsValueMaster.CharactersticsValue.Trim());
                            argCharactersticValueCurrency.ClientCode = argCharactersticsValueMaster.ClientCode;

                            argCharactersticValueCurrencyManager.SaveCharactersticValueCurrency(argCharactersticValueCurrency, da, lstErr);
                        }

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
        
        public int InsertCharactersticsValueMaster(CharactersticsValueMaster argCharactersticsValueMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@idCharactersticsValue", argCharactersticsValueMaster.idCharactersticsValue);
            param[1] = new SqlParameter("@ObjectKey", argCharactersticsValueMaster.ObjectKey);
            param[2] = new SqlParameter("@ObjectTable", argCharactersticsValueMaster.ObjectTable);
            param[3] = new SqlParameter("@idClass", argCharactersticsValueMaster.idClass);
            param[4] = new SqlParameter("@ClassType", argCharactersticsValueMaster.ClassType);
            param[5] = new SqlParameter("@ClassName", argCharactersticsValueMaster.ClassName);
            param[6] = new SqlParameter("@ClientCode", argCharactersticsValueMaster.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argCharactersticsValueMaster.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argCharactersticsValueMaster.ModifiedBy);
            
            
            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCharactersticsValueMaster", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

            int iretvalue = Convert.ToInt32(strRetValue.Trim());

            return iretvalue;

        }
        
        public int UpdateCharactersticsValueMaster(CharactersticsValueMaster argCharactersticsValueMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@idCharactersticsValue", argCharactersticsValueMaster.idCharactersticsValue);
            param[1] = new SqlParameter("@ObjectKey", argCharactersticsValueMaster.ObjectKey);
            param[2] = new SqlParameter("@ObjectTable", argCharactersticsValueMaster.ObjectTable);
            param[3] = new SqlParameter("@idClass", argCharactersticsValueMaster.idClass);
            param[4] = new SqlParameter("@ClassType", argCharactersticsValueMaster.ClassType);
            param[5] = new SqlParameter("@ClassName", argCharactersticsValueMaster.ClassName);
            param[6] = new SqlParameter("@ClientCode", argCharactersticsValueMaster.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argCharactersticsValueMaster.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argCharactersticsValueMaster.ModifiedBy);


            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCharactersticsValueMaster", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

            int iretvalue = Convert.ToInt32(strRetValue.Trim());

            return iretvalue;

        }
        
        public ICollection<ErrorHandler> DeleteCharactersticsValueMaster(int argidCharactersticsValue, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@idCharactersticsValue", argidCharactersticsValue);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted",iIsDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteCharactersticsValueMaster", param);

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

        public bool blnIsCharactersticsValueMasterExists(string argObjectKey, string argObjectTable, string argClientCode, DataAccess da)
        {
            bool IsCharactersticsValueMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetCharactersticsValueMaster(argObjectKey, argObjectTable,  argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCharactersticsValueMasterExists = true;
            }
            else
            {
                IsCharactersticsValueMasterExists = false;
            }
            return IsCharactersticsValueMasterExists;
        }
    }
}