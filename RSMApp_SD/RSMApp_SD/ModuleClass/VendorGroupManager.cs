
//Created On :: 08, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_SD
{
    public class VendorGroupManager
    {
        const string VendorGroupTable = "VendorGroup";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public VendorGroup objGetVendorGroup(string argVendGroupCode, string argClientCode)
        {
            VendorGroup argVendorGroup = new VendorGroup();
            DataSet DataSetToFill = new DataSet();

            if (argVendGroupCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetVendorGroup(argVendGroupCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argVendorGroup = this.objCreateVendorGroup((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argVendorGroup;
        }

        public ICollection<VendorGroup> colGetVendorGroup(string argClientCode)
        {
            List<VendorGroup> lst = new List<VendorGroup>();
            DataSet DataSetToFill = new DataSet();
            VendorGroup tVendorGroup = new VendorGroup();

            DataSetToFill = this.GetVendorGroup(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateVendorGroup(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetVendorGroup(string argVendGroupCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@VendGroupCode", argVendGroupCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetVendorGroup4ID", param);

            return DataSetToFill;
        }

        public DataSet GetVendorGroup(string argVendGroupCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@VendGroupCode", argVendGroupCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetVendorGroup4ID", param);

            return DataSetToFill;
        }

        public DataSet GetVendorGroup(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetVendorGroup",param);
            return DataSetToFill;
        }

        public DataSet GetVendorGroup(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + VendorGroupTable.ToString();

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

        private VendorGroup objCreateVendorGroup(DataRow dr)
        {
            VendorGroup tVendorGroup = new VendorGroup();

            tVendorGroup.SetObjectInfo(dr);

            return tVendorGroup;

        }

        public ICollection<ErrorHandler> SaveVendorGroup(VendorGroup argVendorGroup)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsVendorGroupExists(argVendorGroup.VendGroupCode, argVendorGroup.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertVendorGroup(argVendorGroup, da, lstErr);
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
                    UpdateVendorGroup(argVendorGroup, da, lstErr);
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

        public void SaveVendorGroup(VendorGroup argVendorGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsVendorGroupExists(argVendorGroup.VendGroupCode, argVendorGroup.ClientCode, da) == false)
                {
                    InsertVendorGroup(argVendorGroup, da, lstErr);
                }
                else
                {
                    UpdateVendorGroup(argVendorGroup, da, lstErr);
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
            VendorGroup ObjVendorGroup = null;
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
                        ObjVendorGroup = new VendorGroup();
                        ObjVendorGroup.VendGroupCode = Convert.ToString(drExcel["VendGroupCode"]).Trim();
                        ObjVendorGroup.VendGroupDesc = Convert.ToString(drExcel["VendGroupDesc"]).Trim();
                        ObjVendorGroup.CreatedBy = Convert.ToString(argUserName);
                        ObjVendorGroup.ModifiedBy = Convert.ToString(argUserName);
                        ObjVendorGroup.ClientCode = Convert.ToString(argClientCode);
                        SaveVendorGroup(ObjVendorGroup, da, lstErr);

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

        public void InsertVendorGroup(VendorGroup argVendorGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@VendGroupCode", argVendorGroup.VendGroupCode);
            param[1] = new SqlParameter("@VendGroupDesc", argVendorGroup.VendGroupDesc);
            param[2] = new SqlParameter("@ClientCode", argVendorGroup.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argVendorGroup.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argVendorGroup.ModifiedBy);
         

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertVendorGroup", param);


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

        public void UpdateVendorGroup(VendorGroup argVendorGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@VendGroupCode", argVendorGroup.VendGroupCode);
            param[1] = new SqlParameter("@VendGroupDesc", argVendorGroup.VendGroupDesc);
            param[2] = new SqlParameter("@ClientCode", argVendorGroup.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argVendorGroup.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argVendorGroup.ModifiedBy);
         
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateVendorGroup", param);

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

        public ICollection<ErrorHandler> DeleteVendorGroup(string argVendGroupCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@VendGroupCode", argVendGroupCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteVendorGroup", param);


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

        public bool blnIsVendorGroupExists(string argVendGroupCode, string argClientCode)
        {
            bool IsVendorGroupExists = false;
            DataSet ds = new DataSet();
            ds = GetVendorGroup(argVendGroupCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsVendorGroupExists = true;
            }
            else
            {
                IsVendorGroupExists = false;
            }
            return IsVendorGroupExists;
        }

        public bool blnIsVendorGroupExists(string argVendGroupCode, string argClientCode, DataAccess da)
        {
            bool IsVendorGroupExists = false;
            DataSet ds = new DataSet();
            ds = GetVendorGroup(argVendGroupCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsVendorGroupExists = true;
            }
            else
            {
                IsVendorGroupExists = false;
            }
            return IsVendorGroupExists;
        }
    }
}