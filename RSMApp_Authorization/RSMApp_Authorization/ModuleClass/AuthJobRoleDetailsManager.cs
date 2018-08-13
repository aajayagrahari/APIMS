
//Created On :: 24, July, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Authorization
{
    public class AuthJobRoleDetailsManager
    {
        const string AuthJobRoleDetailsTable = "AuthJobRoleDetails";

      // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public AuthJobRoleDetails objGetAuthJobRoleDetails(string argAuthJobRoleCode, string argModule, string argClientCode)
        {
            AuthJobRoleDetails argAuthJobRoleDetails = new AuthJobRoleDetails();
            DataSet DataSetToFill = new DataSet();

            if (argAuthJobRoleCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argModule.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetAuthJobRoleDetails(argAuthJobRoleCode, argModule, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argAuthJobRoleDetails = this.objCreateAuthJobRoleDetails((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argAuthJobRoleDetails;
        }


        public ICollection<AuthJobRoleDetails> colGetAuthJobRoleDetails(string argAuthJobRoleCode, string argClientCode)
        {
            List<AuthJobRoleDetails> lst = new List<AuthJobRoleDetails>();
            DataSet DataSetToFill = new DataSet();
            AuthJobRoleDetails tAuthJobRoleDetails = new AuthJobRoleDetails();

            DataSetToFill = this.GetAuthJobRoleDetails(argAuthJobRoleCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateAuthJobRoleDetails(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetAuthJobRoleDetails(string argAuthJobRoleCode, string argModule, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@AuthJobRoleCode", argAuthJobRoleCode);
            param[1] = new SqlParameter("@Module", argModule);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetAuthJobRoleDetails4ID", param);

            return DataSetToFill;
        }

        public DataSet GetAuthJobRoleDetails(string argAuthJobRoleCode, string argModule, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@AuthJobRoleCode", argAuthJobRoleCode);
            param[1] = new SqlParameter("@Module", argModule);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAuthJobRoleDetails4ID", param);

            return DataSetToFill;
        }


        public DataSet GetAuthJobRoleDetails(string argAuthJobRoleCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AuthJobRoleCode", argAuthJobRoleCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAuthJobRoleDetails", param);
            return DataSetToFill;
        }

        public DataSet GetAuthJobRoleDetails4Copy(string argAuthJobRoleCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AuthJobRoleCode", argAuthJobRoleCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetAuthJobRoleDetails4Copy", param);
            return DataSetToFill;
        }

        private AuthJobRoleDetails objCreateAuthJobRoleDetails(DataRow dr)
        {
            AuthJobRoleDetails tAuthJobRoleDetails = new AuthJobRoleDetails();

            tAuthJobRoleDetails.SetObjectInfo(dr);

            return tAuthJobRoleDetails;

        }

        public void SaveAuthJobRoleDetails(AuthJobRoleDetails argAuthJobRoleDetails, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsAuthJobRoleDetailsExists(argAuthJobRoleDetails.AuthJobRoleCode, argAuthJobRoleDetails.Module, argAuthJobRoleDetails.ClientCode, da) == false)
                {
                    InsertAuthJobRoleDetails(argAuthJobRoleDetails, da, lstErr);
                }
                else
                {
                    UpdateAuthJobRoleDetails(argAuthJobRoleDetails, da, lstErr);
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


        //public ICollection<ErrorHandler> SaveAuthJobRoleDetails(AuthJobRoleDetails argAuthJobRoleDetails)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsAuthJobRoleDetailsExists(argAuthJobRoleDetails.AuthJobRoleCode, argAuthJobRoleDetails.Module, argAuthJobRoleDetails.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertAuthJobRoleDetails(argAuthJobRoleDetails, da, lstErr);
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
        //            UpdateAuthJobRoleDetails(argAuthJobRoleDetails, da, lstErr);
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


        public void InsertAuthJobRoleDetails(AuthJobRoleDetails argAuthJobRoleDetails, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@AuthJobRoleCode", argAuthJobRoleDetails.AuthJobRoleCode);
            param[1] = new SqlParameter("@ModuleType", argAuthJobRoleDetails.ModuleType);
            param[2] = new SqlParameter("@Module", argAuthJobRoleDetails.Module);
            param[3] = new SqlParameter("@CreateAllowed", argAuthJobRoleDetails.CreateAllowed);
            param[4] = new SqlParameter("@ModifiedAllowed", argAuthJobRoleDetails.ModifiedAllowed);
            param[5] = new SqlParameter("@DeleteAllowed", argAuthJobRoleDetails.DeleteAllowed);
            param[6] = new SqlParameter("@PrintAllowed", argAuthJobRoleDetails.PrintAllowed);
            param[7] = new SqlParameter("@DisplayAllowed", argAuthJobRoleDetails.DisplayAllowed);
            param[8] = new SqlParameter("@ClientCode", argAuthJobRoleDetails.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argAuthJobRoleDetails.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argAuthJobRoleDetails.ModifiedBy);
   
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertAuthJobRoleDetails", param);


            string strMessage = Convert.ToString(param[12].Value);
            string strType = Convert.ToString(param[11].Value);
            string strRetValue = Convert.ToString(param[13].Value);


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


        public void UpdateAuthJobRoleDetails(AuthJobRoleDetails argAuthJobRoleDetails, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@AuthJobRoleCode", argAuthJobRoleDetails.AuthJobRoleCode);
            param[1] = new SqlParameter("@ModuleType", argAuthJobRoleDetails.ModuleType);
            param[2] = new SqlParameter("@Module", argAuthJobRoleDetails.Module);
            param[3] = new SqlParameter("@CreateAllowed", argAuthJobRoleDetails.CreateAllowed);
            param[4] = new SqlParameter("@ModifiedAllowed", argAuthJobRoleDetails.ModifiedAllowed);
            param[5] = new SqlParameter("@DeleteAllowed", argAuthJobRoleDetails.DeleteAllowed);
            param[6] = new SqlParameter("@PrintAllowed", argAuthJobRoleDetails.PrintAllowed);
            param[7] = new SqlParameter("@DisplayAllowed", argAuthJobRoleDetails.DisplayAllowed);
            param[8] = new SqlParameter("@ClientCode", argAuthJobRoleDetails.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argAuthJobRoleDetails.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argAuthJobRoleDetails.ModifiedBy);
            
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateAuthJobRoleDetails", param);
            
            string strMessage = Convert.ToString(param[12].Value);
            string strType = Convert.ToString(param[11].Value);
            string strRetValue = Convert.ToString(param[13].Value);


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


        public ICollection<ErrorHandler> DeleteAuthJobRoleDetails(string argAuthJobRoleCode, string argModule, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@AuthJobRoleCode", argAuthJobRoleCode);
                param[1] = new SqlParameter("@Module", argModule);
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

                int i = da.ExecuteNonQuery("Proc_DeleteAuthJobRoleDetails", param);


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


        public bool blnIsAuthJobRoleDetailsExists(string argAuthJobRoleCode, string argModule, string argClientCode, DataAccess da)
        {
            bool IsAuthJobRoleDetailsExists = false;
            DataSet ds = new DataSet();
            ds = GetAuthJobRoleDetails(argAuthJobRoleCode, argModule, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAuthJobRoleDetailsExists = true;
            }
            else
            {
                IsAuthJobRoleDetailsExists = false;
            }
            return IsAuthJobRoleDetailsExists;
        }


        public DataTable BulkCopy(string argExcelPath, string argQuery, string strTableName, string argFileExt)
        {
            DataTable dtExcel = null;
            string xConnStr = "";
            string strSheetName = "";

            DataTable dtTableSchema = new DataTable();
            DataSet dsExcel = new DataSet();
            OleDbConnection objXConn = null;
            OleDbDataAdapter objDataAdapter = new OleDbDataAdapter();

            if (argFileExt.ToString() == ".xls")
            {
                xConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" +
               "Data Source=" + argExcelPath.Trim() + ";" +
               "Extended Properties=Excel 8.0";
            }
            else
            {
                xConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
               "Data Source=" + argExcelPath.Trim() + ";" +
               "Extended Properties=Excel 12.0";
            }

            try
            {

                objXConn = new OleDbConnection(xConnStr);
                objXConn.Open();

                dtTableSchema = objXConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (argFileExt.ToString() == ".xls")
                {
                    strSheetName = Convert.ToString(dtTableSchema.Rows[0]["TABLE_NAME"]);
                }
                else
                {

                    strSheetName = Convert.ToString(dtTableSchema.Rows[0]["TABLE_NAME"]);
                    if (strSheetName.IndexOf(@"_xlnm#_FilterDatabase") >= 0)
                    {
                        strSheetName = Convert.ToString(dtTableSchema.Rows[1]["TABLE_NAME"]);


                    }

                }

                argQuery = argQuery + " [" + strSheetName + "]";

                OleDbCommand objCommand = new OleDbCommand(argQuery, objXConn);

                objDataAdapter.SelectCommand = objCommand;
                objDataAdapter.Fill(dsExcel);

                dtExcel = dsExcel.Tables[0];



            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objXConn.Close();
            }

            return dtExcel;

        }
    }
}