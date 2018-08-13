
//Created On :: 15, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;


namespace RSMApp_MM
{
    public class ExtMaterialGroupManager
    {
        const string ExtMaterialGroupTable = "ExtMaterialGroup";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public ExtMaterialGroup objGetExtMaterialGroup(string argExtMatGroupCode, string argClientCode)
        {
            ExtMaterialGroup argExtMaterialGroup = new ExtMaterialGroup();
            DataSet DataSetToFill = new DataSet();

            if (argExtMatGroupCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetExtMaterialGroup(argExtMatGroupCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argExtMaterialGroup = this.objCreateExtMaterialGroup((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argExtMaterialGroup;
        }
               
        public ICollection<ExtMaterialGroup> colGetExtMaterialGroup(string argClientCode)
        {
            List<ExtMaterialGroup> lst = new List<ExtMaterialGroup>();
            DataSet DataSetToFill = new DataSet();
            ExtMaterialGroup tExtMaterialGroup = new ExtMaterialGroup();

            DataSetToFill = this.GetExtMaterialGroup(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateExtMaterialGroup(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
  
        public DataSet GetExtMatGroup4Combo(string argPrefix, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetExtMatGroup4Combo", param);

            return DataSetToFill;
        }

        public DataSet GetExtMaterialGroup(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + ExtMaterialGroupTable.ToString();

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
        
        public DataSet GetExtMaterialGroup(string argExtMatGroupCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ExtMatGroupCode", argExtMatGroupCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetExtMaterialGroup4ID", param);

            return DataSetToFill;
        }

        public DataSet GetExtMaterialGroup(string argExtMatGroupCode, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ExtMatGroupCode", argExtMatGroupCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetExtMaterialGroup4ID", param);

            return DataSetToFill;
        }

        public DataSet GetExtMaterialGroup(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetExtMaterialGroup", param);
            return DataSetToFill;
        }

        private ExtMaterialGroup objCreateExtMaterialGroup(DataRow dr)
        {
            ExtMaterialGroup tExtMaterialGroup = new ExtMaterialGroup();

            tExtMaterialGroup.SetObjectInfo(dr);

            return tExtMaterialGroup;

        }

        public ICollection<ErrorHandler> SaveExtMaterialGroup(ExtMaterialGroup argExtMaterialGroup)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsExtMaterialGroupExists(argExtMaterialGroup.ExtMatGroupCode, argExtMaterialGroup.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertExtMaterialGroup(argExtMaterialGroup, da, lstErr);
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
                    UpdateExtMaterialGroup(argExtMaterialGroup, da, lstErr);
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

        public void SaveExtMaterialGroup(ExtMaterialGroup argExtMaterialGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsExtMaterialGroupExists(argExtMaterialGroup.ExtMatGroupCode, argExtMaterialGroup.ClientCode, da) == false)
                {
                    InsertExtMaterialGroup(argExtMaterialGroup, da, lstErr);
                }
                else
                {
                    UpdateExtMaterialGroup(argExtMaterialGroup, da, lstErr);
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
            ExtMaterialGroup ObjExtMaterialGroup = null;
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


                        ObjExtMaterialGroup = new ExtMaterialGroup();

                        ObjExtMaterialGroup.ExtMatGroupCode = Convert.ToString(drExcel["ExtMatGroupCode"]).Trim();
                        ObjExtMaterialGroup.ExtMatGroupDesc = Convert.ToString(drExcel["ExtMatGroupDesc"]).Trim();
                        ObjExtMaterialGroup.CreatedBy = Convert.ToString(argUserName);
                        ObjExtMaterialGroup.ModifiedBy = Convert.ToString(argUserName);
                        ObjExtMaterialGroup.ClientCode = Convert.ToString(argClientCode);

                        SaveExtMaterialGroup(ObjExtMaterialGroup, da, lstErr);

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

        public void InsertExtMaterialGroup(ExtMaterialGroup argExtMaterialGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@ExtMatGroupCode", argExtMaterialGroup.ExtMatGroupCode);
            param[1] = new SqlParameter("@ExtMatGroupDesc", argExtMaterialGroup.ExtMatGroupDesc);
            param[2] = new SqlParameter("@ClientCode", argExtMaterialGroup.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argExtMaterialGroup.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argExtMaterialGroup.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertExtMaterialGroup", param);


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

        public void UpdateExtMaterialGroup(ExtMaterialGroup argExtMaterialGroup, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@ExtMatGroupCode", argExtMaterialGroup.ExtMatGroupCode);
            param[1] = new SqlParameter("@ExtMatGroupDesc", argExtMaterialGroup.ExtMatGroupDesc);
            param[2] = new SqlParameter("@ClientCode", argExtMaterialGroup.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argExtMaterialGroup.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argExtMaterialGroup.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateExtMaterialGroup", param);


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

        public ICollection<ErrorHandler> DeleteExtMaterialGroup(string argExtMatGroupCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@ExtMatGroupCode", argExtMatGroupCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteExtMaterialGroup", param);


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

        public bool blnIsExtMaterialGroupExists(string argExtMatGroupCode, string argClientCode)
        {
            bool IsExtMaterialGroupExists = false;
            DataSet ds = new DataSet();
            ds = GetExtMaterialGroup(argExtMatGroupCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsExtMaterialGroupExists = true;
            }
            else
            {
                IsExtMaterialGroupExists = false;
            }
            return IsExtMaterialGroupExists;
        }

        public bool blnIsExtMaterialGroupExists(string argExtMatGroupCode, string argClientCode,DataAccess da)
        {
            bool IsExtMaterialGroupExists = false;
            DataSet ds = new DataSet();
            ds = GetExtMaterialGroup(argExtMatGroupCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsExtMaterialGroupExists = true;
            }
            else
            {
                IsExtMaterialGroupExists = false;
            }
            return IsExtMaterialGroupExists;
        }
    }
}