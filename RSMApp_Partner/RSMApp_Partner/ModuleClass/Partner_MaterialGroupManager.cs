
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
    public class Partner_MaterialGroupManager
    {
        const string Partner_MaterialGroupTable = "Partner_MaterialGroup";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Partner_MaterialGroup objGetPartner_MaterialGroup(string argPartnerCode, string argMatGroup1Code, string argClientCode)
        {
            Partner_MaterialGroup argPartner_MaterialGroup = new Partner_MaterialGroup();
            DataSet DataSetToFill = new DataSet();

            if (argPartnerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argMatGroup1Code.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPartner_MaterialGroup(argPartnerCode, argMatGroup1Code, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartner_MaterialGroup = this.objCreatePartner_MaterialGroup((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPartner_MaterialGroup;
        }

        public ICollection<Partner_MaterialGroup> colGetPartner_MaterialGroup(string argPartnerCode, string argClientCode)
        {
            List<Partner_MaterialGroup> lst = new List<Partner_MaterialGroup>();
            DataSet DataSetToFill = new DataSet();
            Partner_MaterialGroup tPartner_MaterialGroup = new Partner_MaterialGroup();

            DataSetToFill = this.GetPartner_MaterialGroup(argPartnerCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePartner_MaterialGroup(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<Partner_MaterialGroup> colGetPartner_MaterialGroup(DataTable dt, string argUserName, string clientCode)
        {
            List<Partner_MaterialGroup> lst = new List<Partner_MaterialGroup>();
            Partner_MaterialGroup objPartner_MaterialGroup = null;
            foreach (DataRow dr in dt.Rows)
            {
                objPartner_MaterialGroup = new Partner_MaterialGroup();
                objPartner_MaterialGroup.PartnerTypeCode = Convert.ToString(dr["PartnerTypeCode"]).Trim();
                objPartner_MaterialGroup.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]).Trim();
                objPartner_MaterialGroup.IsAllowed = Convert.ToInt32(dr["IsAllowed"]);
                objPartner_MaterialGroup.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objPartner_MaterialGroup.ModifiedBy = Convert.ToString(argUserName).Trim();
                objPartner_MaterialGroup.CreatedBy = Convert.ToString(argUserName).Trim();
                objPartner_MaterialGroup.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objPartner_MaterialGroup);
            }
            return lst;
        }

        public DataSet GetPartner_MaterialGroup(string argPartnerTypeCode, string argMatGroup1Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PartnerTypeCode", argPartnerTypeCode);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartner_MaterialGroup4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPartner_MaterialGroup(string argPartnerTypeCode, string argMatGroup1Code, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PartnerTypeCode", argPartnerTypeCode);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPartner_MaterialGroup4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPartner_MaterialGroup4PartnerType(string argPartnerTypeCode, string argClientCode)
        {

            DataSet DataSetToFill = new DataSet();
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerTypeCode", argPartnerTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartner_MaterialGroup4PartnerType", param);

            return DataSetToFill;
        }

        public DataSet GetPartner_MaterialGroup(string argPartnerCode, string argClientCode)
        {

            DataSet DataSetToFill = new DataSet();
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartner_MaterialGroup", param);

            return DataSetToFill;
        }

        private Partner_MaterialGroup objCreatePartner_MaterialGroup(DataRow dr)
        {
            Partner_MaterialGroup tPartner_MaterialGroup = new Partner_MaterialGroup();
            tPartner_MaterialGroup.SetObjectInfo(dr);
            return tPartner_MaterialGroup;
        }

        public ICollection<ErrorHandler> SavePartner_MaterialGroup(Partner_MaterialGroup argPartner_MaterialGroup)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartner_MaterialGroupExists(argPartner_MaterialGroup.PartnerTypeCode, argPartner_MaterialGroup.MatGroup1Code, argPartner_MaterialGroup.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartner_MaterialGroup(argPartner_MaterialGroup, da, lstErr);
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
                    UpdatePartner_MaterialGroup(argPartner_MaterialGroup, da, lstErr);
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

        public ICollection<ErrorHandler> SavePartner_MaterialGroup(ICollection<Partner_MaterialGroup> colGetPartner_MaterialGroup)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Partner_MaterialGroup argPartner_MaterialGroup in colGetPartner_MaterialGroup)
                {

                    if (argPartner_MaterialGroup.IsDeleted == 0)
                    {

                        if (blnIsPartner_MaterialGroupExists(argPartner_MaterialGroup.PartnerTypeCode, argPartner_MaterialGroup.MatGroup1Code, argPartner_MaterialGroup.ClientCode,da) == false)
                        {
                            InsertPartner_MaterialGroup(argPartner_MaterialGroup, da, lstErr);
                        }
                        else
                        {
                            UpdatePartner_MaterialGroup(argPartner_MaterialGroup, da, lstErr);
                        }
                    }
                    else
                    {
                        DeletePartner_MaterialGroup(argPartner_MaterialGroup.PartnerTypeCode, argPartner_MaterialGroup.MatGroup1Code, argPartner_MaterialGroup.ClientCode, argPartner_MaterialGroup.IsDeleted);

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

        public ICollection<ErrorHandler> SavePartner_MaterialGroup(ICollection<Partner_MaterialGroup> colGetPartner_MaterialGroup, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Partner_MaterialGroup argPartner_MaterialGroup in colGetPartner_MaterialGroup)
                {
                    if (argPartner_MaterialGroup.IsDeleted == 0)
                    {
                        if (blnIsPartner_MaterialGroupExists(argPartner_MaterialGroup.PartnerTypeCode, argPartner_MaterialGroup.MatGroup1Code, argPartner_MaterialGroup.ClientCode, da) == false)
                        {
                            InsertPartner_MaterialGroup(argPartner_MaterialGroup, da, lstErr);
                        }
                        else
                        {
                            UpdatePartner_MaterialGroup(argPartner_MaterialGroup, da, lstErr);
                        }
                    }
                    else
                    {
                        DeletePartner_MaterialGroup(argPartner_MaterialGroup.PartnerTypeCode, argPartner_MaterialGroup.MatGroup1Code, argPartner_MaterialGroup.ClientCode, argPartner_MaterialGroup.IsDeleted);
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
                    SavePartner_MaterialGroup(colGetPartner_MaterialGroup(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertPartner_MaterialGroup(Partner_MaterialGroup argPartner_MaterialGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@PartnerTypeCode", argPartner_MaterialGroup.PartnerTypeCode);
            param[1] = new SqlParameter("@MatGroup1Code", argPartner_MaterialGroup.MatGroup1Code);
            param[2] = new SqlParameter("@IsAllowed", argPartner_MaterialGroup.IsAllowed);
            param[3] = new SqlParameter("@ClientCode", argPartner_MaterialGroup.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argPartner_MaterialGroup.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argPartner_MaterialGroup.ModifiedBy);
      
            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;
            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPartner_MaterialGroup", param);


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

        public void UpdatePartner_MaterialGroup(Partner_MaterialGroup argPartner_MaterialGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@PartnerTypeCode", argPartner_MaterialGroup.PartnerTypeCode);
            param[1] = new SqlParameter("@MatGroup1Code", argPartner_MaterialGroup.MatGroup1Code);
            param[2] = new SqlParameter("@IsAllowed", argPartner_MaterialGroup.IsAllowed);
            param[3] = new SqlParameter("@ClientCode", argPartner_MaterialGroup.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argPartner_MaterialGroup.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argPartner_MaterialGroup.ModifiedBy);

            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdatePartner_MaterialGroup", param);


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

        public ICollection<ErrorHandler> DeletePartner_MaterialGroup(string argPartnerCode, string argMatGroup1Code, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@PartnerTypeCode", argPartnerCode);
                param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
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

                int i = da.ExecuteNonQuery("Proc_DeletePartner_MaterialGroup", param);


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

        public bool blnIsPartner_MaterialGroupExists(string argPartnerTypeCode, string argMatGroup1Code, string argClientCode)
        {
            bool IsPartner_MaterialGroupExists = false;
            DataSet ds = new DataSet();
            ds = GetPartner_MaterialGroup(argPartnerTypeCode, argMatGroup1Code, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartner_MaterialGroupExists = true;
            }
            else
            {
                IsPartner_MaterialGroupExists = false;
            }
            return IsPartner_MaterialGroupExists;
        }

        public bool blnIsPartner_MaterialGroupExists(string argPartnerTypeCode, string argMatGroup1Code, string argClientCode, DataAccess da)
        {
            bool IsPartner_MaterialGroupExists = false;
            DataSet ds = new DataSet();
            ds = GetPartner_MaterialGroup(argPartnerTypeCode, argMatGroup1Code, argClientCode, da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartner_MaterialGroupExists = true;
            }
            else
            {
                IsPartner_MaterialGroupExists = false;
            }
            return IsPartner_MaterialGroupExists;
        }
    }
}