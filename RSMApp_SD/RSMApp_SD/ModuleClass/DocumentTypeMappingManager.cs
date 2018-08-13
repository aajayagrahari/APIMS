
//Created On :: 12, September, 2012
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
    public class DocumentTypeMappingManager
    {
        const string DocumentTypeMappingTable = "DocumentTypeMapping";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public DocumentTypeMapping objGetDocumentTypeMapping(string argFromDocType, string argFrmDocTypeCode, string argToDocType, string argToDocTypeCode, string argClientCode)
        {
            DocumentTypeMapping argDocumentTypeMapping = new DocumentTypeMapping();
            DataSet DataSetToFill = new DataSet();

            if (argFromDocType.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argFrmDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argToDocType.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argToDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetDocumentTypeMapping(argFromDocType, argFrmDocTypeCode, argToDocType, argToDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argDocumentTypeMapping = this.objCreateDocumentTypeMapping((DataRow)DataSetToFill.Tables[0].Rows[0]);
            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argDocumentTypeMapping;
        }

        public ICollection<DocumentTypeMapping> colGetDocumentTypeMapping(string argClientCode)
        {
            List<DocumentTypeMapping> lst = new List<DocumentTypeMapping>();
            DataSet DataSetToFill = new DataSet();
            DocumentTypeMapping tDocumentTypeMapping = new DocumentTypeMapping();
            DataSetToFill = this.GetDocumentTypeMapping(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDocumentTypeMapping(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<DocumentTypeMapping> colGetDocumentTypeMapping(DataTable dt, string argUserName, string clientCode)
        {
            List<DocumentTypeMapping> lst = new List<DocumentTypeMapping>();
            DocumentTypeMapping objDocumentTypeMapping = null;
            foreach (DataRow dr in dt.Rows)
            {
                objDocumentTypeMapping = new DocumentTypeMapping();
                objDocumentTypeMapping.FromDocType = Convert.ToString(dr["FromDocType"]).Trim();
                objDocumentTypeMapping.FrmDocTypeCode = Convert.ToString(dr["FrmDocTypeCode"]).Trim();
                objDocumentTypeMapping.FrmDocTypeDesc = Convert.ToString(dr["FrmDocTypeDesc"]).Trim();
                objDocumentTypeMapping.ToDocType = Convert.ToString(dr["ToDocType"]).Trim();
                objDocumentTypeMapping.ToDocTypeCode = Convert.ToString(dr["ToDocTypeCode"]).Trim();
                objDocumentTypeMapping.ToDocTypeDesc = Convert.ToString(dr["ToDocTypeDesc"]).Trim();
                objDocumentTypeMapping.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objDocumentTypeMapping.ModifiedBy = Convert.ToString(argUserName).Trim();
                objDocumentTypeMapping.CreatedBy = Convert.ToString(argUserName).Trim();
                objDocumentTypeMapping.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objDocumentTypeMapping);
            }
            return lst;
        }

        public DataSet GetDocumentTypeMapping(string argFromDocType, string argFrmDocTypeCode, string argToDocType, string argToDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@FromDocType", argFromDocType);
            param[1] = new SqlParameter("@FrmDocTypeCode", argFrmDocTypeCode);
            param[2] = new SqlParameter("@ToDocType", argToDocType);
            param[3] = new SqlParameter("@ToDocTypeCode", argToDocTypeCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDocumentTypeMapping4ID", param);
            return DataSetToFill;
        }

        public DataSet GetDocumentTypeMapping(string argFromDocType, string argFrmDocTypeCode, string argToDocType, string argToDocTypeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@FromDocType", argFromDocType);
            param[1] = new SqlParameter("@FrmDocTypeCode", argFrmDocTypeCode);
            param[2] = new SqlParameter("@ToDocType", argToDocType);
            param[3] = new SqlParameter("@ToDocTypeCode", argToDocTypeCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetDocumentTypeMapping4ID", param);
            return DataSetToFill;
        }

        public DataSet GetDocumentTypeMapping(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDocumentTypeMapping", param);
            return DataSetToFill;
        }

        private DocumentTypeMapping objCreateDocumentTypeMapping(DataRow dr)
        {
            DocumentTypeMapping tDocumentTypeMapping = new DocumentTypeMapping();
            tDocumentTypeMapping.SetObjectInfo(dr);
            return tDocumentTypeMapping;
        }

        public ICollection<ErrorHandler> SaveDocumentTypeMapping(DocumentTypeMapping argDocumentTypeMapping)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsDocumentTypeMappingExists(argDocumentTypeMapping.FromDocType, argDocumentTypeMapping.FrmDocTypeCode, argDocumentTypeMapping.ToDocType, argDocumentTypeMapping.ToDocTypeCode, argDocumentTypeMapping.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertDocumentTypeMapping(argDocumentTypeMapping, da, lstErr);
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
                    UpdateDocumentTypeMapping(argDocumentTypeMapping, da, lstErr);
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

        public ICollection<ErrorHandler> SaveDocumentTypeMapping(ICollection<DocumentTypeMapping> colGetDocumentTypeMapping, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (DocumentTypeMapping argDocumentTypeMapping in colGetDocumentTypeMapping)
                {
                    if (argDocumentTypeMapping.IsDeleted == 0)
                    {
                        if (blnIsDocumentTypeMappingExists(argDocumentTypeMapping.FromDocType, argDocumentTypeMapping.FrmDocTypeCode, argDocumentTypeMapping.ToDocType, argDocumentTypeMapping.ToDocTypeCode, argDocumentTypeMapping.ClientCode, da) == false)
                        {
                            InsertDocumentTypeMapping(argDocumentTypeMapping, da, lstErr);
                        }
                        else
                        {
                            UpdateDocumentTypeMapping(argDocumentTypeMapping, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteDocumentTypeMapping(argDocumentTypeMapping.FromDocType, argDocumentTypeMapping.FrmDocTypeCode, argDocumentTypeMapping.ToDocType, argDocumentTypeMapping.ToDocTypeCode, argDocumentTypeMapping.ClientCode);
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
                    SaveDocumentTypeMapping(colGetDocumentTypeMapping(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertDocumentTypeMapping(DocumentTypeMapping argDocumentTypeMapping, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@FromDocType", argDocumentTypeMapping.FromDocType);
            param[1] = new SqlParameter("@FrmDocTypeCode", argDocumentTypeMapping.FrmDocTypeCode);
            param[2] = new SqlParameter("@FrmDocTypeDesc", argDocumentTypeMapping.FrmDocTypeDesc);
            param[3] = new SqlParameter("@ToDocType", argDocumentTypeMapping.ToDocType);
            param[4] = new SqlParameter("@ToDocTypeCode", argDocumentTypeMapping.ToDocTypeCode);
            param[5] = new SqlParameter("@ToDocTypeDesc", argDocumentTypeMapping.ToDocTypeDesc);
            param[6] = new SqlParameter("@ClientCode", argDocumentTypeMapping.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argDocumentTypeMapping.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argDocumentTypeMapping.ModifiedBy);
            
            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertDocumentTypeMapping", param);

            string strMessage = Convert.ToString(param[10].Value);
            string strType = Convert.ToString(param[9].Value);
            string strRetValue = Convert.ToString(param[11].Value);

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

        public void UpdateDocumentTypeMapping(DocumentTypeMapping argDocumentTypeMapping, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@FromDocType", argDocumentTypeMapping.FromDocType);
            param[1] = new SqlParameter("@FrmDocTypeCode", argDocumentTypeMapping.FrmDocTypeCode);
            param[2] = new SqlParameter("@FrmDocTypeDesc", argDocumentTypeMapping.FrmDocTypeDesc);
            param[3] = new SqlParameter("@ToDocType", argDocumentTypeMapping.ToDocType);
            param[4] = new SqlParameter("@ToDocTypeCode", argDocumentTypeMapping.ToDocTypeCode);
            param[5] = new SqlParameter("@ToDocTypeDesc", argDocumentTypeMapping.ToDocTypeDesc);
            param[6] = new SqlParameter("@ClientCode", argDocumentTypeMapping.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argDocumentTypeMapping.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argDocumentTypeMapping.ModifiedBy);
            
            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateDocumentTypeMapping", param);

            string strMessage = Convert.ToString(param[10].Value);
            string strType = Convert.ToString(param[9].Value);
            string strRetValue = Convert.ToString(param[11].Value);

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

        public ICollection<ErrorHandler> DeleteDocumentTypeMapping(string argFromDocType, string argFrmDocTypeCode, string argToDocType, string argToDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@FromDocType", argFromDocType);
                param[1] = new SqlParameter("@FrmDocTypeCode", argFrmDocTypeCode);
                param[2] = new SqlParameter("@ToDocType", argToDocType);
                param[3] = new SqlParameter("@ToDocTypeCode", argToDocTypeCode);
                param[4] = new SqlParameter("@ClientCode", argClientCode);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;
                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;
                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteDocumentTypeMapping", param);

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

        public bool blnIsDocumentTypeMappingExists(string argFromDocType, string argFrmDocTypeCode, string argToDocType, string argToDocTypeCode, string argClientCode)
        {
            bool IsDocumentTypeMappingExists = false;
            DataSet ds = new DataSet();
            ds = GetDocumentTypeMapping(argFromDocType, argFrmDocTypeCode, argToDocType, argToDocTypeCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDocumentTypeMappingExists = true;
            }
            else
            {
                IsDocumentTypeMappingExists = false;
            }
            return IsDocumentTypeMappingExists;
        }

        public bool blnIsDocumentTypeMappingExists(string argFromDocType, string argFrmDocTypeCode, string argToDocType, string argToDocTypeCode, string argClientCode, DataAccess da)
        {
            bool IsDocumentTypeMappingExists = false;
            DataSet ds = new DataSet();
            ds = GetDocumentTypeMapping(argFromDocType, argFrmDocTypeCode, argToDocType, argToDocTypeCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDocumentTypeMappingExists = true;
            }
            else
            {
                IsDocumentTypeMappingExists = false;
            }
            return IsDocumentTypeMappingExists;
        }
    }
}