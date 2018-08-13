
//Created On :: 16, October, 2012
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
    public class Partner_ServiceLevelManager
    {
        const string Partner_ServiceLevelTable = "Partner_ServiceLevel";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Partner_ServiceLevel objGetPartner_ServiceLevel(string argPartnerCode, string argMatGroup1Code, string argServiceLevel, string argClientCode)
        {
            Partner_ServiceLevel argPartner_ServiceLevel = new Partner_ServiceLevel();
            DataSet DataSetToFill = new DataSet();

            if (argPartnerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argMatGroup1Code.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argServiceLevel.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPartner_ServiceLevel(argPartnerCode, argMatGroup1Code, argServiceLevel, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartner_ServiceLevel = this.objCreatePartner_ServiceLevel((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPartner_ServiceLevel;
        }

        public ICollection<Partner_ServiceLevel> colGetPartner_ServiceLevel(string argPartnerCode, string argClientCode)
        {
            List<Partner_ServiceLevel> lst = new List<Partner_ServiceLevel>();
            DataSet DataSetToFill = new DataSet();
            Partner_ServiceLevel tPartner_ServiceLevel = new Partner_ServiceLevel();

            DataSetToFill = this.GetPartner_ServiceLevel(argPartnerCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePartner_ServiceLevel(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<Partner_ServiceLevel> colGetPartner_ServiceLevel(DataTable dt, string argUserName, string clientCode)
        {
            List<Partner_ServiceLevel> lst = new List<Partner_ServiceLevel>();
            Partner_ServiceLevel objPartner_ServiceLevel = null;
            foreach (DataRow dr in dt.Rows)
            {
                objPartner_ServiceLevel = new Partner_ServiceLevel();
                objPartner_ServiceLevel.PartnerCode = Convert.ToString(dr["PartnerCode"]).Trim();
                objPartner_ServiceLevel.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]).Trim();
                objPartner_ServiceLevel.ServiceLevel = Convert.ToString(dr["ServiceLevel"]).Trim();
                objPartner_ServiceLevel.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objPartner_ServiceLevel.ModifiedBy = Convert.ToString(argUserName).Trim();
                objPartner_ServiceLevel.CreatedBy = Convert.ToString(argUserName).Trim();
                objPartner_ServiceLevel.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objPartner_ServiceLevel);
            }
            return lst;
        }

        public DataSet GetPartner_ServiceLevel(string argPartnerCode, string argMatGroup1Code, string argServiceLevel, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@ServiceLevel", argServiceLevel);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartner_ServiceLevel4ID", param);
            return DataSetToFill;
        }

        public DataSet GetPartner_ServiceLevel(string argPartnerCode, string argMatGroup1Code, string argServiceLevel, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@ServiceLevel", argServiceLevel);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPartner_ServiceLevel4ID", param);
            return DataSetToFill;
        }

        public DataSet GetPartner_ServiceLevel(string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartner_ServiceLevel", param);
            return DataSetToFill;
        }

        private Partner_ServiceLevel objCreatePartner_ServiceLevel(DataRow dr)
        {
            Partner_ServiceLevel tPartner_ServiceLevel = new Partner_ServiceLevel();
            tPartner_ServiceLevel.SetObjectInfo(dr);
            return tPartner_ServiceLevel;
        }

        public ICollection<ErrorHandler> SavePartner_ServiceLevel(Partner_ServiceLevel argPartner_ServiceLevel)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartner_ServiceLevelExists(argPartner_ServiceLevel.PartnerCode, argPartner_ServiceLevel.MatGroup1Code, argPartner_ServiceLevel.ServiceLevel, argPartner_ServiceLevel.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartner_ServiceLevel(argPartner_ServiceLevel, da, lstErr);
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
                    UpdatePartner_ServiceLevel(argPartner_ServiceLevel, da, lstErr);
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

        public ICollection<ErrorHandler> SavePartner_ServiceLevel(ICollection<Partner_ServiceLevel> colGetPartner_ServiceLevel)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Partner_ServiceLevel argPartner_ServiceLevel in colGetPartner_ServiceLevel)
                {

                    if (argPartner_ServiceLevel.IsDeleted == 0)
                    {

                        if (blnIsPartner_ServiceLevelExists(argPartner_ServiceLevel.PartnerCode, argPartner_ServiceLevel.MatGroup1Code, argPartner_ServiceLevel.ServiceLevel, argPartner_ServiceLevel.ClientCode,da) == false)
                        {
                            InsertPartner_ServiceLevel(argPartner_ServiceLevel, da, lstErr);
                        }
                        else
                        {
                            UpdatePartner_ServiceLevel(argPartner_ServiceLevel, da, lstErr);
                        }
                    }
                    else
                    {
                        DeletePartner_ServiceLevel(argPartner_ServiceLevel.PartnerCode, argPartner_ServiceLevel.MatGroup1Code, argPartner_ServiceLevel.ServiceLevel, argPartner_ServiceLevel.ClientCode, argPartner_ServiceLevel.IsDeleted);

                    }

                }

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

        public ICollection<ErrorHandler> SavePartner_ServiceLevel(ICollection<Partner_ServiceLevel> colGetPartner_ServiceLevel, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Partner_ServiceLevel argPartner_ServiceLevel in colGetPartner_ServiceLevel)
                {
                    if (argPartner_ServiceLevel.IsDeleted == 0)
                    {
                        if (blnIsPartner_ServiceLevelExists(argPartner_ServiceLevel.PartnerCode, argPartner_ServiceLevel.MatGroup1Code, argPartner_ServiceLevel.ServiceLevel, argPartner_ServiceLevel.ClientCode, da) == false)
                        {
                            InsertPartner_ServiceLevel(argPartner_ServiceLevel, da, lstErr);
                        }
                        else
                        {
                            UpdatePartner_ServiceLevel(argPartner_ServiceLevel, da, lstErr);
                        }
                    }
                    else
                    {
                        DeletePartner_ServiceLevel(argPartner_ServiceLevel.PartnerCode, argPartner_ServiceLevel.MatGroup1Code, argPartner_ServiceLevel.ServiceLevel, argPartner_ServiceLevel.ClientCode, argPartner_ServiceLevel.IsDeleted);
                    }
                }

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

        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
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
                    SavePartner_ServiceLevel(colGetPartner_ServiceLevel(dtExcel, argUserName, argClientCode), lstErr);

                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            break;
                        }
                    }
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

        public void InsertPartner_ServiceLevel(Partner_ServiceLevel argPartner_ServiceLevel, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@PartnerCode", argPartner_ServiceLevel.PartnerCode);
            param[1] = new SqlParameter("@MatGroup1Code", argPartner_ServiceLevel.MatGroup1Code);
            param[2] = new SqlParameter("@ServiceLevel", argPartner_ServiceLevel.ServiceLevel);
            param[3] = new SqlParameter("@ClientCode", argPartner_ServiceLevel.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argPartner_ServiceLevel.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argPartner_ServiceLevel.ModifiedBy);


            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPartner_ServiceLevel", param);


            string strMessage = Convert.ToString(param[7].Value);
            string strType = Convert.ToString(param[6].Value);
            string strRetValue = Convert.ToString(param[8].Value);


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
        
        public void UpdatePartner_ServiceLevel(Partner_ServiceLevel argPartner_ServiceLevel, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@PartnerCode", argPartner_ServiceLevel.PartnerCode);
            param[1] = new SqlParameter("@MatGroup1Code", argPartner_ServiceLevel.MatGroup1Code);
            param[2] = new SqlParameter("@ServiceLevel", argPartner_ServiceLevel.ServiceLevel);
            param[3] = new SqlParameter("@ClientCode", argPartner_ServiceLevel.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argPartner_ServiceLevel.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argPartner_ServiceLevel.ModifiedBy);


            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePartner_ServiceLevel", param);


            string strMessage = Convert.ToString(param[7].Value);
            string strType = Convert.ToString(param[6].Value);
            string strRetValue = Convert.ToString(param[8].Value);


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
        
        public ICollection<ErrorHandler> DeletePartner_ServiceLevel(string argPartnerCode, string argMatGroup1Code, string argServiceLevel, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
                param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
                param[2] = new SqlParameter("@ServiceLevel", argServiceLevel);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeletePartner_ServiceLevel", param);


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
        
        public bool blnIsPartner_ServiceLevelExists(string argPartnerCode, string argMatGroup1Code, string argServiceLevel, string argClientCode)
        {
            bool IsPartner_ServiceLevelExists = false;
            DataSet ds = new DataSet();
            ds = GetPartner_ServiceLevel(argPartnerCode, argMatGroup1Code, argServiceLevel, argClientCode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartner_ServiceLevelExists = true;
            }
            else
            {
                IsPartner_ServiceLevelExists = false;
            }
            return IsPartner_ServiceLevelExists;
        }

        public bool blnIsPartner_ServiceLevelExists(string argPartnerCode, string argMatGroup1Code, string argServiceLevel, string argClientCode,DataAccess da)
        {
            bool IsPartner_ServiceLevelExists = false;
            DataSet ds = new DataSet();
            ds = GetPartner_ServiceLevel(argPartnerCode, argMatGroup1Code, argServiceLevel, argClientCode,da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartner_ServiceLevelExists = true;
            }
            else
            {
                IsPartner_ServiceLevelExists = false;
            }
            return IsPartner_ServiceLevelExists;
        }
    }
}