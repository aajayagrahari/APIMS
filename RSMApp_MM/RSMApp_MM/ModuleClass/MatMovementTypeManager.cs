
//Created On :: 16, May, 2012
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
    public class MatMovementTypeManager
    {
        const string MatMovementTypeTable = "MatMovementType";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public MatMovementType objGetMatMovementType(string argMatMovementCode, string argClientCode)
        {
            MatMovementType argMatMovementType = new MatMovementType();
            DataSet DataSetToFill = new DataSet();

            if (argMatMovementCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetMatMovementType(argMatMovementCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMatMovementType = this.objCreateMatMovementType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argMatMovementType;
        }

        public ICollection<MatMovementType> colGetMatMovementType(string argClientCode)
        {
            List<MatMovementType> lst = new List<MatMovementType>();
            DataSet DataSetToFill = new DataSet();
            MatMovementType tMatMovementType = new MatMovementType();

            DataSetToFill = this.GetMatMovementType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMatMovementType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
    
        public DataSet GetMatMovementType(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + MatMovementTypeTable.ToString();

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

        public DataSet GetMatMovementType(string argMatMovementCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MatMovementCode", argMatMovementCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMatMovementType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMatMovementType(string argMatMovementCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MatMovementCode", argMatMovementCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMatMovementType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMatMovementType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetMatMovementType",param);
            return DataSetToFill;
        }

        private MatMovementType objCreateMatMovementType(DataRow dr)
        {
            MatMovementType tMatMovementType = new MatMovementType();

            tMatMovementType.SetObjectInfo(dr);

            return tMatMovementType;

        }

        public ICollection<ErrorHandler> SaveMatMovementType(MatMovementType argMatMovementType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsMatMovementTypeExists(argMatMovementType.MatMovementCode, argMatMovementType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertMatMovementType(argMatMovementType, da, lstErr);
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
                    UpdateMatMovementType(argMatMovementType, da, lstErr);
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

        public void SaveMatMovementType(MatMovementType argMatMovementType, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsMatMovementTypeExists(argMatMovementType.MatMovementCode, argMatMovementType.ClientCode, da) == false)
                {
                    InsertMatMovementType(argMatMovementType, da, lstErr);
                }
                else
                {
                    UpdateMatMovementType(argMatMovementType, da, lstErr);
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
            MatMovementType ObjMatMovementType = null;
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


                        ObjMatMovementType = new MatMovementType();

                        ObjMatMovementType.MatMovementCode = Convert.ToString(drExcel["MatMovementCode"]).Trim();
                        ObjMatMovementType.MatMovementDesc = Convert.ToString(drExcel["MatMovementDesc"]).Trim();
                        ObjMatMovementType.IsMAPUpdated = Convert.ToInt32(drExcel["IsMAPUpdated"]);
                        ObjMatMovementType.OperationType = Convert.ToString(drExcel["OperationType"]);
                        ObjMatMovementType.CreatedBy = Convert.ToString(argUserName);
                        ObjMatMovementType.ModifiedBy = Convert.ToString(argUserName);
                        ObjMatMovementType.ClientCode = Convert.ToString(argClientCode);

                        SaveMatMovementType(ObjMatMovementType, da, lstErr);

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
   
        public void InsertMatMovementType(MatMovementType argMatMovementType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@MatMovementCode", argMatMovementType.MatMovementCode);
            param[1] = new SqlParameter("@MatMovementDesc", argMatMovementType.MatMovementDesc);
            param[2] = new SqlParameter("@OperationType", argMatMovementType.OperationType);
            param[3] = new SqlParameter("@IsMAPUpdated", argMatMovementType.IsMAPUpdated);
            param[4] = new SqlParameter("@ClientCode", argMatMovementType.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argMatMovementType.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argMatMovementType.ModifiedBy);
            
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertMatMovementType", param);


            string strMessage = Convert.ToString(param[8].Value);
            string strType = Convert.ToString(param[7].Value);
            string strRetValue = Convert.ToString(param[9].Value);


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

        public void UpdateMatMovementType(MatMovementType argMatMovementType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@MatMovementCode", argMatMovementType.MatMovementCode);
            param[1] = new SqlParameter("@MatMovementDesc", argMatMovementType.MatMovementDesc);
            param[2] = new SqlParameter("@OperationType", argMatMovementType.OperationType);
            param[3] = new SqlParameter("@IsMAPUpdated", argMatMovementType.IsMAPUpdated);
            param[4] = new SqlParameter("@ClientCode", argMatMovementType.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argMatMovementType.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argMatMovementType.ModifiedBy);
            
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateMatMovementType", param);


            string strMessage = Convert.ToString(param[8].Value);
            string strType = Convert.ToString(param[7].Value);
            string strRetValue = Convert.ToString(param[9].Value);


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

        public ICollection<ErrorHandler> DeleteMatMovementType(string argMatMovementCode, string argClientCode,int IisDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@MatMovementCode", argMatMovementCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted", IisDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteMatMovementType", param);


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

        public bool blnIsMatMovementTypeExists(string argMatMovementCode, string argClientCode)
        {
            bool IsMatMovementTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetMatMovementType(argMatMovementCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMatMovementTypeExists = true;
            }
            else
            {
                IsMatMovementTypeExists = false;
            }
            return IsMatMovementTypeExists;
        }

        public bool blnIsMatMovementTypeExists(string argMatMovementCode, string argClientCode,DataAccess da)
        {
            bool IsMatMovementTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetMatMovementType(argMatMovementCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMatMovementTypeExists = true;
            }
            else
            {
                IsMatMovementTypeExists = false;
            }
            return IsMatMovementTypeExists;
        }
    }

}