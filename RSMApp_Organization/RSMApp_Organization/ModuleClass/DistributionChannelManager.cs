
//Created On :: 07, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using Telerik.Web.UI;
using System.Data.OleDb;

namespace RSMApp_Organization
{
    public class DistributionChannelManager
    {
        const string DistributionChannelTable = "DistributionChannel";
        ErrorHandler objErrorHandler = new ErrorHandler();

        public DistributionChannel objGetDistributionChannel(string argDistChannelCode, string argClientCode)
        {
            DistributionChannel argDistributionChannel = new DistributionChannel();
            DataSet DataSetToFill = new DataSet();

            if (argDistChannelCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetDistributionChannel(argDistChannelCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argDistributionChannel = this.objCreateDistributionChannel((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argDistributionChannel;
        }

        public ICollection<DistributionChannel> colGetDistributionChannel(string argClientCode)
        {
            List<DistributionChannel> lst = new List<DistributionChannel>();
            DataSet DataSetToFill = new DataSet();
            DistributionChannel tDistributionChannel = new DistributionChannel();
            DataSetToFill = this.GetDistributionChannel(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDistributionChannel(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }
        
        public DataSet GetDistributionChannel(string argDistChannelCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDistributionChannel4ID", param);
            return DataSetToFill;
        }

        /******/
        public DataSet GetDistributionChannel(string argDistChannelCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetDistributionChannel4ID", param);
            return DataSetToFill;
        }
        /*****/

        public DataSet GetDistributionChannel4SalesOrg(string argSalesOrganizationCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetDistChannel4SalesOrg", param);
            return DataSetToFill;
        }
        
        public DataSet GetDistributionChannel(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + DistributionChannelTable.ToString();

                if (iIsDeleted > -1)
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;
                    tSQL = tSQL + " " + sWhereClauseSTR + " IsDeleted = " + iIsDeleted;
                }

                if (argClientCode != "")
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;
                    tSQL = tSQL + " " + sWhereClauseSTR + " ClientCode = '" + argClientCode + "'";
                }
                ds = da.FillDataSetWithSQL(tSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public DataSet GetDistributionChannel(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDistributionChannel", param);
            return DataSetToFill;
        }
        
        public DataSet GetDistChannel4Combo(string argPrefix, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetDistributionChannel4Combo", param);
            return DataSetToFill;
        }
        
        private DistributionChannel objCreateDistributionChannel(DataRow dr)
        {
            DistributionChannel tDistributionChannel = new DistributionChannel();
            tDistributionChannel.SetObjectInfo(dr);
            return tDistributionChannel;
        }
        
        public ICollection<ErrorHandler> SaveDistributionChannel(DistributionChannel argDistributionChannel)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsDistributionChannelExists(argDistributionChannel.DistChannelCode, argDistributionChannel.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertDistributionChannel(argDistributionChannel, da, lstErr);
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
                    UpdateDistributionChannel(argDistributionChannel, da, lstErr);
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

        /*************/
        public void SaveDistributionChannel(DistributionChannel argDistributionChannel, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsDistributionChannelExists(argDistributionChannel.DistChannelCode, argDistributionChannel.ClientCode, da) == false)
                {
                    InsertDistributionChannel(argDistributionChannel, da, lstErr);
                }
                else
                {
                    UpdateDistributionChannel(argDistributionChannel, da, lstErr);
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
            DistributionChannel ObjDistributionChannel = null;
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
                        ObjDistributionChannel = new DistributionChannel();
                        ObjDistributionChannel.DistChannelCode = Convert.ToString(drExcel["DistChannelCode"]).Trim();
                        ObjDistributionChannel.DistChannel = Convert.ToString(drExcel["DistChannel"]).Trim();
                        ObjDistributionChannel.CreatedBy = Convert.ToString(argUserName);
                        ObjDistributionChannel.ModifiedBy = Convert.ToString(argUserName);
                        ObjDistributionChannel.ClientCode = Convert.ToString(argClientCode);
                        SaveDistributionChannel(ObjDistributionChannel, da, lstErr);

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
        /*************/

        public void InsertDistributionChannel(DistributionChannel argDistributionChannel, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@DistChannelCode", argDistributionChannel.DistChannelCode);
            param[1] = new SqlParameter("@DistChannel", argDistributionChannel.DistChannel);
            param[2] = new SqlParameter("@ClientCode", argDistributionChannel.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argDistributionChannel.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argDistributionChannel.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertDistributionChannel", param);

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
            lstErr.Add(objErrorHandler);
        }
        
        public void UpdateDistributionChannel(DistributionChannel argDistributionChannel, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@DistChannelCode", argDistributionChannel.DistChannelCode);
            param[1] = new SqlParameter("@DistChannel", argDistributionChannel.DistChannel);
            param[2] = new SqlParameter("@ClientCode", argDistributionChannel.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argDistributionChannel.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argDistributionChannel.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateDistributionChannel", param);

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
            lstErr.Add(objErrorHandler);
        }
        
        public ICollection<ErrorHandler> DeleteDistributionChannel(string argDistChannelCode, int iIsDeleted, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@DistChannelCode", argDistChannelCode);
                param[1] = new SqlParameter("@IsDeleted", iIsDeleted);
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

                int i = da.ExecuteNonQuery("Proc_DeleteDistributionChannel", param);

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
        
        public bool blnIsDistributionChannelExists(string argDistChannelCode, string argClientCode)
        {
            bool IsDistributionChannelExists = false;
            DataSet ds = new DataSet();
            ds = GetDistributionChannel(argDistChannelCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDistributionChannelExists = true;
            }
            else
            {
                IsDistributionChannelExists = false;
            }
            return IsDistributionChannelExists;
        }

        /*****/
        public bool blnIsDistributionChannelExists(string argDistChannelCode, string argClientCode, DataAccess da)
        {
            bool IsDistributionChannelExists = false;
            DataSet ds = new DataSet();
            ds = GetDistributionChannel(argDistChannelCode.Trim(), argClientCode.Trim(), da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDistributionChannelExists = true;
            }
            else
            {
                IsDistributionChannelExists = false;
            }
            return IsDistributionChannelExists;
        }
        /*****/
    }
}