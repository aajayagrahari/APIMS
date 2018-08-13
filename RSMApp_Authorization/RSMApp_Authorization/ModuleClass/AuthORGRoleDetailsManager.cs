
//Created On :: 24, July, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Authorization
{
    public class AuthORGRoleDetailsManager
    {
        const string AuthORGRoleDetailsTable = "AuthORGRoleDetails";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public AuthORGRoleDetails objGetAuthORGRoleDetails(string argAuthOrgcode, int ISerialNo, string argFieldName, string argClientCode)
        {
            AuthORGRoleDetails argAuthORGRoleDetails = new AuthORGRoleDetails();
            DataSet DataSetToFill = new DataSet();

            if (argAuthOrgcode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argFieldName.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetAuthORGRoleDetails(argAuthOrgcode, ISerialNo, argFieldName, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argAuthORGRoleDetails = this.objCreateAuthORGRoleDetails((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argAuthORGRoleDetails;
        }
        
        public ICollection<AuthORGRoleDetails> colGetAuthORGRoleDetails(string argAuthOrgcode, string argClientCode)
        {
            List<AuthORGRoleDetails> lst = new List<AuthORGRoleDetails>();
            DataSet DataSetToFill = new DataSet();
            AuthORGRoleDetails tAuthORGRoleDetails = new AuthORGRoleDetails();

            DataSetToFill = this.GetAuthORGRoleDetails(argAuthOrgcode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateAuthORGRoleDetails(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetAuthORGRoleDetails(string argAuthOrgcode, int ISerialNo, string argFieldName, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@AuthOrgcode", argAuthOrgcode);
            param[1] = new SqlParameter("@SerialNo", ISerialNo);
            param[2] = new SqlParameter("@FieldName", argFieldName);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAuthORGRoleDetails4ID", param);

            return DataSetToFill;
        }

        public DataSet GetAuthORGRoleDetails(string argAuthOrgcode, int ISerialNo, string argFieldName, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@AuthOrgcode", argAuthOrgcode);
            param[1] = new SqlParameter("@SerialNo", ISerialNo);
            param[2] = new SqlParameter("@FieldName", argFieldName);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetAuthORGRoleDetails4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetAuthORGRoleDetails(string argAuthOrgcode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AuthOrgcode", argAuthOrgcode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAuthORGRoleDetails", param);
            return DataSetToFill;
        }
        
        private AuthORGRoleDetails objCreateAuthORGRoleDetails(DataRow dr)
        {
            AuthORGRoleDetails tAuthORGRoleDetails = new AuthORGRoleDetails();

            tAuthORGRoleDetails.SetObjectInfo(dr);

            return tAuthORGRoleDetails;

        }

        public void SaveAuthORGRoleDetails(AuthORGRoleDetails argAuthORGRoleDetails, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsAuthORGRoleDetailsExists(argAuthORGRoleDetails.AuthOrgcode,argAuthORGRoleDetails.SerialNo, argAuthORGRoleDetails.FieldName, argAuthORGRoleDetails.ClientCode, da) == false)
                {
                    InsertAuthORGRoleDetails(argAuthORGRoleDetails, da, lstErr);
                }
                else
                {
                    UpdateAuthORGRoleDetails(argAuthORGRoleDetails, da, lstErr);
                }
            }
            catch(Exception ex)
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

        //public ICollection<ErrorHandler> SaveAuthORGRoleDetails(AuthORGRoleDetails argAuthORGRoleDetails)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsAuthORGRoleDetailsExists(argAuthORGRoleDetails.AuthOrgcode, argAuthORGRoleDetails.FieldName, argAuthORGRoleDetails.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertAuthORGRoleDetails(argAuthORGRoleDetails, da, lstErr);
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
        //            UpdateAuthORGRoleDetails(argAuthORGRoleDetails, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
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
        
        public void InsertAuthORGRoleDetails(AuthORGRoleDetails argAuthORGRoleDetails, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@AuthOrgcode", argAuthORGRoleDetails.AuthOrgcode);
            param[1] = new SqlParameter("@SerialNo", argAuthORGRoleDetails.SerialNo);
            param[2] = new SqlParameter("@FieldName", argAuthORGRoleDetails.FieldName);
            param[3] = new SqlParameter("@FieldValueFrom", argAuthORGRoleDetails.FieldValueFrom);
            param[4] = new SqlParameter("@FieldValueTo", argAuthORGRoleDetails.FieldValueTo);
            param[5] = new SqlParameter("@ClientCode", argAuthORGRoleDetails.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argAuthORGRoleDetails.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argAuthORGRoleDetails.ModifiedBy);
            
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertAuthORGRoleDetails", param);


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
        
        public void UpdateAuthORGRoleDetails(AuthORGRoleDetails argAuthORGRoleDetails, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@AuthOrgcode", argAuthORGRoleDetails.AuthOrgcode);
            param[1] = new SqlParameter("@SerialNo", argAuthORGRoleDetails.SerialNo);
            param[2] = new SqlParameter("@FieldName", argAuthORGRoleDetails.FieldName);
            param[3] = new SqlParameter("@FieldValueFrom", argAuthORGRoleDetails.FieldValueFrom);
            param[4] = new SqlParameter("@FieldValueTo", argAuthORGRoleDetails.FieldValueTo);
            param[5] = new SqlParameter("@ClientCode", argAuthORGRoleDetails.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argAuthORGRoleDetails.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argAuthORGRoleDetails.ModifiedBy);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;


            int i = da.NExecuteNonQuery("Proc_UpdateAuthORGRoleDetails", param);

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
        
        public ICollection<ErrorHandler> DeleteAuthORGRoleDetails(string argAuthOrgcode, string argFieldName, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];

                param[0] = new SqlParameter("@AuthOrgcode", argAuthOrgcode);
                param[1] = new SqlParameter("@FieldName", argFieldName);
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

                int i = da.ExecuteNonQuery("Proc_DeleteAuthORGRoleDetails", param);

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
        
        public bool blnIsAuthORGRoleDetailsExists(string argAuthOrgcode, int ISerialNo, string argFieldName, string argClientCode, DataAccess da)
        {
            bool IsAuthORGRoleDetailsExists = false;
            DataSet ds = new DataSet();
            ds = GetAuthORGRoleDetails(argAuthOrgcode, ISerialNo, argFieldName, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAuthORGRoleDetailsExists = true;
            }
            else
            {
                IsAuthORGRoleDetailsExists = false;
            }
            return IsAuthORGRoleDetailsExists;
        }
    }
}