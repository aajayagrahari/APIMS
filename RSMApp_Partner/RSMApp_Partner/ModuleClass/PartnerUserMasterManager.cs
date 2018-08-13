
//Created On :: 09, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_Partner
{
    public class PartnerUserMasterManager
    {
        const string PartnerUserMasterTable = "PartnerUserMaster";
        
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public PartnerUserMaster objGetPartnerUserMaster(string argPartnerUserName, string argPartnerCode, string argClientCode)
        {
            PartnerUserMaster argPartnerUserMaster = new PartnerUserMaster();
            DataSet DataSetToFill = new DataSet();

            if (argPartnerUserName.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argPartnerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPartnerUserMaster(argPartnerUserName, argPartnerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartnerUserMaster = this.objCreatePartnerUserMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPartnerUserMaster;
        }
        
        public ICollection<PartnerUserMaster> colGetPartnerUserMaster(string argClientCode)
        {
            List<PartnerUserMaster> lst = new List<PartnerUserMaster>();
            DataSet DataSetToFill = new DataSet();
            PartnerUserMaster tPartnerUserMaster = new PartnerUserMaster();

            DataSetToFill = this.GetPartnerUserMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePartnerUserMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetPartnerUserMaster(string argPartnerUserName, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PartnerUserName", argPartnerUserName);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerUserMaster4ID", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerUserMaster(string argPartnerUserName, string argPartnerCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PartnerUserName", argPartnerUserName);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPartnerUserMaster4ID", param);
            return DataSetToFill;
        }
        
        public DataSet GetPartnerUserMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerUserMaster", param);
            return DataSetToFill;
        }
        
        public DataSet CheckPartnerUserLogin(string argPartnerUserName, string argPartnerUserPassword, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PartnerUserName", argPartnerUserName);
            param[1] = new SqlParameter("@PartnerUserPassword", argPartnerUserPassword);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_CheckPartnerUserLogin", param);

            return DataSetToFill;
        }
        
        private PartnerUserMaster objCreatePartnerUserMaster(DataRow dr)
        {
            PartnerUserMaster tPartnerUserMaster = new PartnerUserMaster();
            tPartnerUserMaster.SetObjectInfo(dr);
            return tPartnerUserMaster;
        }
        
        public ICollection<ErrorHandler> SavePartnerUserMaster(PartnerUserMaster argPartnerUserMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartnerUserMasterExists(argPartnerUserMaster.PartnerUserName, argPartnerUserMaster.PartnerCode, argPartnerUserMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartnerUserMaster(argPartnerUserMaster, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
                    }
                    da.COMMIT_TRANSACTION();
                }
                else
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    UpdatePartnerUserMaster(argPartnerUserMaster, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
                    }
                }

                da.COMMIT_TRANSACTION();
            }
            catch (Exception ex)
            {
                if (da != null)
                {
                    da.ROLLBACK_TRANSACTION();
                }
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
            finally
            {
                if (da != null)
                {
                    da.Close_Connection();
                    da = null;
                }
            }
            return lstErr;
        }

        public void SavePartnerUserMaster(PartnerUserMaster argPartnerUserMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsPartnerUserMasterExists(argPartnerUserMaster.PartnerUserName, argPartnerUserMaster.PartnerCode, argPartnerUserMaster.ClientCode, da) == false)
                {
                    InsertPartnerUserMaster(argPartnerUserMaster, da, lstErr);
                }
                else
                {
                    UpdatePartnerUserMaster(argPartnerUserMaster, da, lstErr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
            PartnerUserMaster ObjPartnerUserMaster = null;
            string xConnStr = "";
            string strSheetName = "";
            DataSet dsExcel = new DataSet();
            DataTable dtTableSchema = new DataTable();
            OleDbConnection objXConn = null;
            OleDbDataAdapter objDataAdapter = new OleDbDataAdapter();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

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

                /*****************************************/
                DataAccess da = new DataAccess();
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                try
                {

                    foreach (DataRow drExcel in dtExcel.Rows)
                    {
                        ObjPartnerUserMaster = new PartnerUserMaster();
                        ObjPartnerUserMaster.PartnerUserName = Convert.ToString(drExcel["PartnerUserName"]).Trim();
                        ObjPartnerUserMaster.PartnerUserPassword = Convert.ToString(drExcel["PartnerUserPassword"]).Trim();
                        ObjPartnerUserMaster.PartnerCode = Convert.ToString(drExcel["PartnerCode"]).Trim();
                        ObjPartnerUserMaster.PartnerEmployeeCode = Convert.ToString(drExcel["PartnerEmployeeCode"]).Trim();
                        ObjPartnerUserMaster.FirstName = Convert.ToString(drExcel["FirstName"]).Trim();
                        ObjPartnerUserMaster.LastName = Convert.ToString(drExcel["LastName"]).Trim();
                        ObjPartnerUserMaster.Gender = Convert.ToString(drExcel["Gender"]).Trim();
                        ObjPartnerUserMaster.Address1 = Convert.ToString(drExcel["Address1"]).Trim();
                        ObjPartnerUserMaster.Address2 = Convert.ToString(drExcel["Address2"]).Trim();
                        ObjPartnerUserMaster.Phone1 = Convert.ToString(drExcel["Phone1"]).Trim();
                        ObjPartnerUserMaster.Phone2 = Convert.ToString(drExcel["Phone2"]).Trim();
                        ObjPartnerUserMaster.EmailID = Convert.ToString(drExcel["EmailID"]).Trim();
                        ObjPartnerUserMaster.CountryCode = Convert.ToString(drExcel["CountryCode"]).Trim();
                        ObjPartnerUserMaster.StateCode = Convert.ToString(drExcel["StateCode"]).Trim();
                        ObjPartnerUserMaster.City = Convert.ToString(drExcel["City"]).Trim();
                        ObjPartnerUserMaster.IsBlocked = Convert.ToInt32(drExcel["IsBlocked"]);
                        ObjPartnerUserMaster.CreatedBy = Convert.ToString(argUserName);
                        ObjPartnerUserMaster.ModifiedBy = Convert.ToString(argUserName);
                        ObjPartnerUserMaster.ClientCode = Convert.ToString(argClientCode);
                        SavePartnerUserMaster(ObjPartnerUserMaster, da, lstErr);

                        foreach (ErrorHandler objerr in lstErr)
                        {
                            if (objerr.Type == "E")
                            {
                                da.ROLLBACK_TRANSACTION();
                                break;
                            }
                        }
                    }
                    da.COMMIT_TRANSACTION();
                }
                catch (Exception ex)
                {
                    if (da != null)
                    {
                        da.ROLLBACK_TRANSACTION();
                    }
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
                finally
                {
                    if (da != null)
                    {
                        da.Close_Connection();
                        da = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objXConn.Close();
            }
            return lstErr;
        }
        
        public void InsertPartnerUserMaster(PartnerUserMaster argPartnerUserMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[22];

            param[0] = new SqlParameter("@PartnerUserName", argPartnerUserMaster.PartnerUserName);
            param[1] = new SqlParameter("@PartnerUserPassword", argPartnerUserMaster.PartnerUserPassword);
            param[2] = new SqlParameter("@PartnerCode", argPartnerUserMaster.PartnerCode);
            param[3] = new SqlParameter("@PartnerEmployeeCode", argPartnerUserMaster.PartnerEmployeeCode);
            param[4] = new SqlParameter("@FirstName", argPartnerUserMaster.FirstName);
            param[5] = new SqlParameter("@LastName", argPartnerUserMaster.LastName);
            param[6] = new SqlParameter("@Gender", argPartnerUserMaster.Gender);
            param[7] = new SqlParameter("@Address1", argPartnerUserMaster.Address1);
            param[8] = new SqlParameter("@Address2", argPartnerUserMaster.Address2);
            param[9] = new SqlParameter("@Phone1", argPartnerUserMaster.Phone1);
            param[10] = new SqlParameter("@Phone2", argPartnerUserMaster.Phone2);
            param[11] = new SqlParameter("@EmailID", argPartnerUserMaster.EmailID);
            param[12] = new SqlParameter("@CountryCode", argPartnerUserMaster.CountryCode);
            param[13] = new SqlParameter("@StateCode", argPartnerUserMaster.StateCode);
            param[14] = new SqlParameter("@City", argPartnerUserMaster.City);
            param[15] = new SqlParameter("@IsBlocked", argPartnerUserMaster.IsBlocked);
            param[16] = new SqlParameter("@ClientCode", argPartnerUserMaster.ClientCode);
            param[17] = new SqlParameter("@CreatedBy", argPartnerUserMaster.CreatedBy);
            param[18] = new SqlParameter("@ModifiedBy", argPartnerUserMaster.ModifiedBy);
            
            param[19] = new SqlParameter("@Type", SqlDbType.Char);
            param[19].Size = 1;
            param[19].Direction = ParameterDirection.Output;

            param[20] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[20].Size = 255;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[21].Size = 20;
            param[21].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPartnerUserMaster", param);


            string strMessage = Convert.ToString(param[20].Value);
            string strType = Convert.ToString(param[19].Value);
            string strRetValue = Convert.ToString(param[21].Value);


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
        
        public void UpdatePartnerUserMaster(PartnerUserMaster argPartnerUserMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[22];

            param[0] = new SqlParameter("@PartnerUserName", argPartnerUserMaster.PartnerUserName);
            param[1] = new SqlParameter("@PartnerUserPassword", argPartnerUserMaster.PartnerUserPassword);
            param[2] = new SqlParameter("@PartnerCode", argPartnerUserMaster.PartnerCode);
            param[3] = new SqlParameter("@PartnerEmployeeCode", argPartnerUserMaster.PartnerEmployeeCode);
            param[4] = new SqlParameter("@FirstName", argPartnerUserMaster.FirstName);
            param[5] = new SqlParameter("@LastName", argPartnerUserMaster.LastName);
            param[6] = new SqlParameter("@Gender", argPartnerUserMaster.Gender);
            param[7] = new SqlParameter("@Address1", argPartnerUserMaster.Address1);
            param[8] = new SqlParameter("@Address2", argPartnerUserMaster.Address2);
            param[9] = new SqlParameter("@Phone1", argPartnerUserMaster.Phone1);
            param[10] = new SqlParameter("@Phone2", argPartnerUserMaster.Phone2);
            param[11] = new SqlParameter("@EmailID", argPartnerUserMaster.EmailID);
            param[12] = new SqlParameter("@CountryCode", argPartnerUserMaster.CountryCode);
            param[13] = new SqlParameter("@StateCode", argPartnerUserMaster.StateCode);
            param[14] = new SqlParameter("@City", argPartnerUserMaster.City);
            param[15] = new SqlParameter("@IsBlocked", argPartnerUserMaster.IsBlocked);
            param[16] = new SqlParameter("@ClientCode", argPartnerUserMaster.ClientCode);
            param[17] = new SqlParameter("@CreatedBy", argPartnerUserMaster.CreatedBy);
            param[18] = new SqlParameter("@ModifiedBy", argPartnerUserMaster.ModifiedBy);

            param[19] = new SqlParameter("@Type", SqlDbType.Char);
            param[19].Size = 1;
            param[19].Direction = ParameterDirection.Output;

            param[20] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[20].Size = 255;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[21].Size = 20;
            param[21].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePartnerUserMaster", param);


            string strMessage = Convert.ToString(param[20].Value);
            string strType = Convert.ToString(param[19].Value);
            string strRetValue = Convert.ToString(param[21].Value);


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

        public string UpdatePartnerUserMasterPassword(PartnerUserMaster argPartnerUserMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[7];

            param[0] = new SqlParameter("@PartnerUserName", argPartnerUserMaster.PartnerUserName);
            param[1] = new SqlParameter("@PartnerUserPassword", argPartnerUserMaster.PartnerUserPassword);
            param[2] = new SqlParameter("@PartnerCode", argPartnerUserMaster.PartnerCode);
            param[3] = new SqlParameter("@ClientCode", argPartnerUserMaster.ClientCode);

            param[4] = new SqlParameter("@Type", SqlDbType.Char);
            param[4].Size = 1;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[5].Size = 255;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[6].Size = 20;
            param[6].Direction = ParameterDirection.Output;

            int i = da.ExecuteNonQuery("SL_UpdatePartnerUserPassword", param);


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

            return strRetValue;

        }

        public ICollection<ErrorHandler> DeletePartnerUserMaster(string argPartnerUserName, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@PartnerUserName", argPartnerUserName);
                param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
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
                int i = da.ExecuteNonQuery("Proc_DeletePartnerUserMaster", param);


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
        
        public bool blnIsPartnerUserMasterExists(string argPartnerUserName, string argPartnerCode, string argClientCode)
        {
            bool IsPartnerUserMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerUserMaster(argPartnerUserName, argPartnerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerUserMasterExists = true;
            }
            else
            {
                IsPartnerUserMasterExists = false;
            }
            return IsPartnerUserMasterExists;
        }

        public bool blnIsPartnerUserMasterExists(string argPartnerUserName, string argPartnerCode, string argClientCode, DataAccess da)
        {
            bool IsPartnerUserMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerUserMaster(argPartnerUserName, argPartnerCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerUserMasterExists = true;
            }
            else
            {
                IsPartnerUserMasterExists = false;
            }
            return IsPartnerUserMasterExists;
        }
    }
}