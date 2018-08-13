
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
    public class ClassCharacteristicManager
    {
        const string ClassCharacteristicTable = "ClassCharacteristic";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public ClassCharacteristic objGetClassCharacteristic(int argidClass, int argidCharacteristic, string argClientCode)
        {
            ClassCharacteristic argClassCharacteristic = new ClassCharacteristic();
            DataSet DataSetToFill = new DataSet();

            if (argidClass <= 0)
            {
                goto ErrorHandlers;
            }

            if (argidCharacteristic <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetClassCharacteristic(argidClass, argidCharacteristic, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argClassCharacteristic = this.objCreateClassCharacteristic((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argClassCharacteristic;
        }


        public ICollection<ClassCharacteristic> colGetClassCharacteristic(int argidClass, string argClientCode)
        {
            List<ClassCharacteristic> lst = new List<ClassCharacteristic>();
            DataSet DataSetToFill = new DataSet();
            ClassCharacteristic tClassCharacteristic = new ClassCharacteristic();

            DataSetToFill = this.GetClassCharacteristic(argidClass, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateClassCharacteristic(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetClassCharacteristic(int argidClass, int argidCharacteristic, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@idClass", argidClass);
            param[1] = new SqlParameter("@idCharacteristic", argidCharacteristic);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetClassCharacteristic4ID", param);

            return DataSetToFill;
        }

        public DataSet GetClassCharacteristic(int argidClass, int argidCharacteristic, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@idClass", argidClass);
            param[1] = new SqlParameter("@idCharacteristic", argidCharacteristic);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetClassCharacteristic4ID", param);

            return DataSetToFill;
        }


        public DataSet GetClassCharacteristic(int argidClass, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();


            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@idClass", argidClass);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetClassCharacteristic", param);
            return DataSetToFill;
        }


        private ClassCharacteristic objCreateClassCharacteristic(DataRow dr)
        {
            ClassCharacteristic tClassCharacteristic = new ClassCharacteristic();

            tClassCharacteristic.SetObjectInfo(dr);

            return tClassCharacteristic;

        }

        public string SaveClassCharacteristic(ClassCharacteristic argClassCharacteristic, DataAccess da, List<ErrorHandler> lstErr)
        {
            string strRetValue = "";
            try
            {
                if (blnIsClassCharacteristicExists(argClassCharacteristic.idClass, argClassCharacteristic.idCharacteristic, argClassCharacteristic.ClientCode, da) == false)
                {
                    strRetValue = InsertClassCharacteristic(argClassCharacteristic, da, lstErr);
                }
                else
                {
                    strRetValue = UpdateClassCharacteristic(argClassCharacteristic, da, lstErr);
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

            return strRetValue;
        }


        //public ICollection<ErrorHandler> SaveClassCharacteristic(ClassCharacteristic argClassCharacteristic)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsClassCharacteristicExists(argClassCharacteristic.idClass, argClassCharacteristic.idCharacteristic, argClassCharacteristic.ClientCode, da) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertClassCharacteristic(argClassCharacteristic, da, lstErr);
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
        //            UpdateClassCharacteristic(argClassCharacteristic, da, lstErr);
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
        
        public string InsertClassCharacteristic(ClassCharacteristic argClassCharacteristic, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@idClass", argClassCharacteristic.idClass);
            param[1] = new SqlParameter("@idCharacteristic", argClassCharacteristic.idCharacteristic);
            param[2] = new SqlParameter("@ClassType", argClassCharacteristic.ClassType);
            param[3] = new SqlParameter("@ClassName", argClassCharacteristic.ClassName);
            param[4] = new SqlParameter("@CharacteristicName", argClassCharacteristic.CharacteristicName);
            param[5] = new SqlParameter("@CharValuetype", argClassCharacteristic.CharValuetype);
            param[6] = new SqlParameter("@ClientCode", argClassCharacteristic.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argClassCharacteristic.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argClassCharacteristic.ModifiedBy);
         
            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertClassCharacteristic", param);


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

            return strRetValue.Trim();
        }

        public string UpdateClassCharacteristic(ClassCharacteristic argClassCharacteristic, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@idClass", argClassCharacteristic.idClass);
            param[1] = new SqlParameter("@idCharacteristic", argClassCharacteristic.idCharacteristic);
            param[2] = new SqlParameter("@ClassType", argClassCharacteristic.ClassType);
            param[3] = new SqlParameter("@ClassName", argClassCharacteristic.ClassName);
            param[4] = new SqlParameter("@CharacteristicName", argClassCharacteristic.CharacteristicName);
            param[5] = new SqlParameter("@CharValuetype", argClassCharacteristic.CharValuetype);
            param[6] = new SqlParameter("@ClientCode", argClassCharacteristic.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argClassCharacteristic.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argClassCharacteristic.ModifiedBy);
            
            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateClassCharacteristic", param);


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

            return strRetValue.Trim();
        }
        
        public ICollection<ErrorHandler> DeleteClassCharacteristic(int argidClass, int argidCharacteristic, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@idClass", argidClass);
                param[1] = new SqlParameter("@idCharacteristic", argidCharacteristic);
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
                int i = da.ExecuteNonQuery("Proc_DeleteClassCharacteristic", param);


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


        public void DeleteClassCharacteristic(int argidClass, int argidCharacteristic, string argClientCode, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@idClass", argidClass);
                param[1] = new SqlParameter("@idCharacteristic", argidCharacteristic);
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
                int i = da.NExecuteNonQuery("Proc_DeleteClassCharacteristic", param);


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

        public bool blnIsClassCharacteristicExists(int argidClass, int argidCharacteristic, string argClientCode, DataAccess da)
        {
            bool IsClassCharacteristicExists = false;
            DataSet ds = new DataSet();
            ds = GetClassCharacteristic(argidClass, argidCharacteristic, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsClassCharacteristicExists = true;
            }
            else
            {
                IsClassCharacteristicExists = false;
            }
            return IsClassCharacteristicExists;
        }
    }
}