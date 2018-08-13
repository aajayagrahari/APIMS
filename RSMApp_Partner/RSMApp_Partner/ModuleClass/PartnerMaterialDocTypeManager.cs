
//Created On :: 19, October, 2012
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
    public class PartnerMaterialDocTypeManager
    {
        const string PartnerMaterialDocTypeTable = "PartnerMaterialDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public PartnerMaterialDocType objGetPartnerMaterialDocType(string argMaterialDocTypeCode, string argClientCode)
        {
            PartnerMaterialDocType argPartnerMaterialDocType = new PartnerMaterialDocType();
            DataSet DataSetToFill = new DataSet();

            if (argMaterialDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPartnerMaterialDocType(argMaterialDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartnerMaterialDocType = this.objCreatePartnerMaterialDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPartnerMaterialDocType;
        }

        public ICollection<PartnerMaterialDocType> colGetPartnerMaterialDocType(string argClientCode)
        {
            List<PartnerMaterialDocType> lst = new List<PartnerMaterialDocType>();
            DataSet DataSetToFill = new DataSet();
            PartnerMaterialDocType tPartnerMaterialDocType = new PartnerMaterialDocType();

            DataSetToFill = this.GetPartnerMaterialDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePartnerMaterialDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetPartnerMaterialDocType(string argMaterialDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialDocTypeCode", argMaterialDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerMaterialDocType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerMaterialDocType(string argMaterialDocTypeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialDocTypeCode", argMaterialDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPartnerMaterialDocType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerMaterialDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerMaterialDocType", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerMaterialDocType(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + PartnerMaterialDocTypeTable.ToString();

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

        private PartnerMaterialDocType objCreatePartnerMaterialDocType(DataRow dr)
        {
            PartnerMaterialDocType tPartnerMaterialDocType = new PartnerMaterialDocType();
            tPartnerMaterialDocType.SetObjectInfo(dr);
            return tPartnerMaterialDocType;
        }

        public ICollection<ErrorHandler> SavePartnerMaterialDocType(PartnerMaterialDocType argPartnerMaterialDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartnerMaterialDocTypeExists(argPartnerMaterialDocType.MaterialDocTypeCode, argPartnerMaterialDocType.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartnerMaterialDocType(argPartnerMaterialDocType, da, lstErr);
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
                    UpdatePartnerMaterialDocType(argPartnerMaterialDocType, da, lstErr);
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

        public void SavePartnerMaterialDocType(PartnerMaterialDocType argPartnerMaterialDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsPartnerMaterialDocTypeExists(argPartnerMaterialDocType.MaterialDocTypeCode, argPartnerMaterialDocType.ClientCode, da) == false)
                {
                    InsertPartnerMaterialDocType(argPartnerMaterialDocType, da, lstErr);
                }
                else
                {
                    UpdatePartnerMaterialDocType(argPartnerMaterialDocType, da, lstErr);
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
            PartnerMaterialDocType ObjPartnerMaterialDocType = null;
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
                        ObjPartnerMaterialDocType = new PartnerMaterialDocType();
                        ObjPartnerMaterialDocType.MaterialDocTypeCode = Convert.ToString(drExcel["MaterialDocTypeCode"]).Trim();
                        ObjPartnerMaterialDocType.MaterialDocTypeDesc = Convert.ToString(drExcel["MaterialDocTypeDesc"]).Trim();
                        ObjPartnerMaterialDocType.FromPartner = Convert.ToString(drExcel["FromPartner"]).Trim();
                        ObjPartnerMaterialDocType.FromStore = Convert.ToString(drExcel["FromStore"]).Trim();
                        ObjPartnerMaterialDocType.FromEmployee = Convert.ToString(drExcel["FromEmployee"]).Trim();
                        ObjPartnerMaterialDocType.FromPlant = Convert.ToString(drExcel["FromPlant"]).Trim();
                        ObjPartnerMaterialDocType.ToPartner = Convert.ToString(drExcel["ToPartner"]).Trim();
                        ObjPartnerMaterialDocType.ToStore = Convert.ToString(drExcel["ToStore"]).Trim();
                        ObjPartnerMaterialDocType.ToEmployee = Convert.ToString(drExcel["ToEmployee"]).Trim();
                        ObjPartnerMaterialDocType.ToPlant = Convert.ToString(drExcel["ToPlant"]).Trim();
                        ObjPartnerMaterialDocType.ToMaterialCode = Convert.ToString(drExcel["ToMaterialCode"]).Trim();
                        ObjPartnerMaterialDocType.AllowedFromStock = Convert.ToString(drExcel["AllowedFromStock"]).Trim();
                        ObjPartnerMaterialDocType.AllowedToStock = Convert.ToString(drExcel["AllowedToStock"]).Trim();
                        ObjPartnerMaterialDocType.CreatedBy = Convert.ToString(argUserName);
                        ObjPartnerMaterialDocType.ModifiedBy = Convert.ToString(argUserName);
                        ObjPartnerMaterialDocType.ClientCode = Convert.ToString(argClientCode);
                        SavePartnerMaterialDocType(ObjPartnerMaterialDocType, da, lstErr);

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

        public void InsertPartnerMaterialDocType(PartnerMaterialDocType argPartnerMaterialDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@MaterialDocTypeCode", argPartnerMaterialDocType.MaterialDocTypeCode);
            param[1] = new SqlParameter("@MaterialDocTypeDesc", argPartnerMaterialDocType.MaterialDocTypeDesc);
            param[2] = new SqlParameter("@FromPartner", argPartnerMaterialDocType.FromPartner);
            param[3] = new SqlParameter("@FromStore", argPartnerMaterialDocType.FromStore);
            param[4] = new SqlParameter("@FromEmployee", argPartnerMaterialDocType.FromEmployee);
            param[5] = new SqlParameter("@FromPlant", argPartnerMaterialDocType.FromPlant);
            param[6] = new SqlParameter("@ToPartner", argPartnerMaterialDocType.ToPartner);
            param[7] = new SqlParameter("@ToStore", argPartnerMaterialDocType.ToStore);
            param[8] = new SqlParameter("@ToEmployee", argPartnerMaterialDocType.ToEmployee);
            param[9] = new SqlParameter("@ToPlant", argPartnerMaterialDocType.ToPlant);
            param[10] = new SqlParameter("@ToMaterialCode", argPartnerMaterialDocType.ToMaterialCode);
            param[11] = new SqlParameter("@AllowedFromStock", argPartnerMaterialDocType.AllowedFromStock);
            param[12] = new SqlParameter("@AllowedToStock", argPartnerMaterialDocType.AllowedToStock);
            param[13] = new SqlParameter("@ClientCode", argPartnerMaterialDocType.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argPartnerMaterialDocType.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argPartnerMaterialDocType.ModifiedBy);

            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPartnerMaterialDocType", param);


            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);


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

        public void UpdatePartnerMaterialDocType(PartnerMaterialDocType argPartnerMaterialDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@MaterialDocTypeCode", argPartnerMaterialDocType.MaterialDocTypeCode);
            param[1] = new SqlParameter("@MaterialDocTypeDesc", argPartnerMaterialDocType.MaterialDocTypeDesc);
            param[2] = new SqlParameter("@FromPartner", argPartnerMaterialDocType.FromPartner);
            param[3] = new SqlParameter("@FromStore", argPartnerMaterialDocType.FromStore);
            param[4] = new SqlParameter("@FromEmployee", argPartnerMaterialDocType.FromEmployee);
            param[5] = new SqlParameter("@FromPlant", argPartnerMaterialDocType.FromPlant);
            param[6] = new SqlParameter("@ToPartner", argPartnerMaterialDocType.ToPartner);
            param[7] = new SqlParameter("@ToStore", argPartnerMaterialDocType.ToStore);
            param[8] = new SqlParameter("@ToEmployee", argPartnerMaterialDocType.ToEmployee);
            param[9] = new SqlParameter("@ToPlant", argPartnerMaterialDocType.ToPlant);
            param[10] = new SqlParameter("@ToMaterialCode", argPartnerMaterialDocType.ToMaterialCode);
            param[11] = new SqlParameter("@AllowedFromStock", argPartnerMaterialDocType.AllowedFromStock);
            param[12] = new SqlParameter("@AllowedToStock", argPartnerMaterialDocType.AllowedToStock);
            param[13] = new SqlParameter("@ClientCode", argPartnerMaterialDocType.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argPartnerMaterialDocType.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argPartnerMaterialDocType.ModifiedBy);

            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePartnerMaterialDocType", param);


            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);


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

        public ICollection<ErrorHandler> DeletePartnerMaterialDocType(string argMaterialDocTypeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@MaterialDocTypeCode", argMaterialDocTypeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeletePartnerMaterialDocType", param);


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

        public bool blnIsPartnerMaterialDocTypeExists(string argMaterialDocTypeCode, string argClientCode)
        {
            bool IsPartnerMaterialDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerMaterialDocType(argMaterialDocTypeCode, argClientCode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerMaterialDocTypeExists = true;
            }
            else
            {
                IsPartnerMaterialDocTypeExists = false;
            }
            return IsPartnerMaterialDocTypeExists;
        }

        public bool blnIsPartnerMaterialDocTypeExists(string argMaterialDocTypeCode, string argClientCode, DataAccess da)
        {
            bool IsPartnerMaterialDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerMaterialDocType(argMaterialDocTypeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerMaterialDocTypeExists = true;
            }
            else
            {
                IsPartnerMaterialDocTypeExists = false;
            }
            return IsPartnerMaterialDocTypeExists;
        }
    }
}