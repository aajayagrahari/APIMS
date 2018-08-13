
//Created On :: 26, November, 2012
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
    public class MatGroup_RepairTypeManager
    {
        const string MatGroup_RepairTypeTable = "MatGroup_RepairType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public MatGroup_RepairType objGetMatGroup_RepairType(string argMatGroup1Code, string argMaterialCode, string argDefectTypeCode, string argRepairTypeCode, string argClientCode)
        {
            MatGroup_RepairType argMatGroup_RepairType = new MatGroup_RepairType();
            DataSet DataSetToFill = new DataSet();

            if (argMatGroup1Code.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argDefectTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argRepairTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetMatGroup_RepairType(argMatGroup1Code, argMaterialCode, argDefectTypeCode, argRepairTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMatGroup_RepairType = this.objCreateMatGroup_RepairType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argMatGroup_RepairType;
        }

        public ICollection<MatGroup_RepairType> colGetMatGroup_RepairType(string argMatGroup1Code,string argClientCode)
        {
            List<MatGroup_RepairType> lst = new List<MatGroup_RepairType>();
            DataSet DataSetToFill = new DataSet();
            MatGroup_RepairType tMatGroup_RepairType = new MatGroup_RepairType();

            DataSetToFill = this.GetMatGroup_RepairType(argMatGroup1Code,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMatGroup_RepairType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<MatGroup_RepairType> colGetMatGroup_RepairType(DataTable dt, string argUserName, string clientCode)
        {
            List<MatGroup_RepairType> lst = new List<MatGroup_RepairType>();
            MatGroup_RepairType objMatGroup_RepairType = null;
            foreach (DataRow dr in dt.Rows)
            {
                objMatGroup_RepairType = new MatGroup_RepairType();
                objMatGroup_RepairType.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]).Trim();
                objMatGroup_RepairType.MaterialCode = Convert.ToString(dr["MaterialCode"]).Trim();
                objMatGroup_RepairType.DefectTypeCode = Convert.ToString(dr["DefectTypeCode"]).Trim();
                objMatGroup_RepairType.RepairTypeCode = Convert.ToString(dr["RepairTypeCode"]).Trim();
                objMatGroup_RepairType.ServiceLevel = Convert.ToString(dr["ServiceLevel"]).Trim();
                objMatGroup_RepairType.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objMatGroup_RepairType.ModifiedBy = Convert.ToString(argUserName).Trim();
                objMatGroup_RepairType.CreatedBy = Convert.ToString(argUserName).Trim();
                objMatGroup_RepairType.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objMatGroup_RepairType);
            }
            return lst;
        }

        public DataSet GetMatGroup_RepairType(string argMatGroup1Code, string argMaterialCode,  string argDefectTypeCode, string argRepairTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@DefectTypeCode", argDefectTypeCode);
            param[3] = new SqlParameter("@RepairTypeCode", argRepairTypeCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMatGroup_RepairType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMatGroup_RepairType(string argMatGroup1Code, string argMaterialCode, string argDefectTypeCode, string argRepairTypeCode, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@DefectTypeCode", argDefectTypeCode);
            param[3] = new SqlParameter("@RepairTypeCode", argRepairTypeCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMatGroup_RepairType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMatGroup_RepairType4Call(string argMatGroup1Code, string argMaterialCode, string argRepairTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);  
            param[2] = new SqlParameter("@RepairTypeCode", argRepairTypeCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMatGroup_RepairType4Call", param);

            return DataSetToFill;
        }

        public DataSet GetMatGroup_RepairType(string argMatGroup1Code,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMatGroup_RepairType",param);
            return DataSetToFill;
        }

        private MatGroup_RepairType objCreateMatGroup_RepairType(DataRow dr)
        {
            MatGroup_RepairType tMatGroup_RepairType = new MatGroup_RepairType();

            tMatGroup_RepairType.SetObjectInfo(dr);

            return tMatGroup_RepairType;

        }

        public ICollection<ErrorHandler> SaveMatGroup_RepairType(MatGroup_RepairType argMatGroup_RepairType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsMatGroup_RepairTypeExists(argMatGroup_RepairType.MatGroup1Code, argMatGroup_RepairType.MaterialCode, argMatGroup_RepairType.DefectTypeCode, argMatGroup_RepairType.RepairTypeCode, argMatGroup_RepairType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertMatGroup_RepairType(argMatGroup_RepairType, da, lstErr);
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
                    UpdateMatGroup_RepairType(argMatGroup_RepairType, da, lstErr);
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

        public ICollection<ErrorHandler> SaveMatGroup_RepairType(ICollection<MatGroup_RepairType> colGetMatGroup_RepairType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (MatGroup_RepairType argMatGroup_RepairType in colGetMatGroup_RepairType)
                {

                    if (argMatGroup_RepairType.IsDeleted == 0)
                    {

                        if (blnIsMatGroup_RepairTypeExists(argMatGroup_RepairType.MatGroup1Code, argMatGroup_RepairType.MaterialCode,argMatGroup_RepairType.DefectTypeCode,argMatGroup_RepairType.RepairTypeCode,argMatGroup_RepairType.ClientCode, da) == false)
                        {
                            InsertMatGroup_RepairType(argMatGroup_RepairType, da, lstErr);
                        }
                        else
                        {
                            UpdateMatGroup_RepairType(argMatGroup_RepairType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMatGroup_RepairType(argMatGroup_RepairType.MatGroup1Code, argMatGroup_RepairType.DefectTypeCode, argMatGroup_RepairType.RepairTypeCode, argMatGroup_RepairType.ClientCode, argMatGroup_RepairType.IsDeleted);

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

        public ICollection<ErrorHandler> SaveMatGroup_RepairType(ICollection<MatGroup_RepairType> colGetMatGroup_RepairType, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (MatGroup_RepairType argMatGroup_RepairType in colGetMatGroup_RepairType)
                {
                    if (argMatGroup_RepairType.IsDeleted == 0)
                    {
                        if (blnIsMatGroup_RepairTypeExists(argMatGroup_RepairType.MatGroup1Code, argMatGroup_RepairType.MaterialCode,argMatGroup_RepairType.DefectTypeCode,argMatGroup_RepairType.RepairTypeCode,argMatGroup_RepairType.ClientCode, da) == false)
                        {
                            InsertMatGroup_RepairType(argMatGroup_RepairType, da, lstErr);
                        }
                        else
                        {
                            UpdateMatGroup_RepairType(argMatGroup_RepairType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMatGroup_RepairType(argMatGroup_RepairType.MatGroup1Code, argMatGroup_RepairType.DefectTypeCode, argMatGroup_RepairType.RepairTypeCode, argMatGroup_RepairType.ClientCode, argMatGroup_RepairType.IsDeleted);
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
                    SaveMatGroup_RepairType(colGetMatGroup_RepairType(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertMatGroup_RepairType(MatGroup_RepairType argMatGroup_RepairType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup_RepairType.MatGroup1Code);
            param[1] = new SqlParameter("@MaterialCode", argMatGroup_RepairType.MaterialCode);
            param[2] = new SqlParameter("@DefectTypeCode", argMatGroup_RepairType.DefectTypeCode);
            param[3] = new SqlParameter("@RepairTypeCode", argMatGroup_RepairType.RepairTypeCode);
            param[4] = new SqlParameter("@ServiceLevel", argMatGroup_RepairType.ServiceLevel);
            param[5] = new SqlParameter("@ClientCode", argMatGroup_RepairType.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argMatGroup_RepairType.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argMatGroup_RepairType.ModifiedBy);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertMatGroup_RepairType", param);


            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
            string strRetValue = Convert.ToString(param[10].Value);


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

        public void UpdateMatGroup_RepairType(MatGroup_RepairType argMatGroup_RepairType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup_RepairType.MatGroup1Code);
            param[1] = new SqlParameter("@MaterialCode", argMatGroup_RepairType.MaterialCode);
            param[2] = new SqlParameter("@DefectTypeCode", argMatGroup_RepairType.DefectTypeCode);
            param[3] = new SqlParameter("@RepairTypeCode", argMatGroup_RepairType.RepairTypeCode);
            param[4] = new SqlParameter("@ServiceLevel", argMatGroup_RepairType.ServiceLevel);
            param[5] = new SqlParameter("@ClientCode", argMatGroup_RepairType.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argMatGroup_RepairType.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argMatGroup_RepairType.ModifiedBy);
        
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateMatGroup_RepairType", param);


            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
            string strRetValue = Convert.ToString(param[10].Value);


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

        public ICollection<ErrorHandler> DeleteMatGroup_RepairType(string argMatGroup1Code, string argDefectTypeCode, string argRepairTypeCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
                param[1] = new SqlParameter("@DefectTypeCode", argDefectTypeCode);
                param[2] = new SqlParameter("@RepairTypeCode", argRepairTypeCode);
                param[3] = new SqlParameter("@IsDeleted", iIsDeleted);
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

                int i = da.ExecuteNonQuery("Proc_DeleteMatGroup_RepairType", param);


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

        public bool blnIsMatGroup_RepairTypeExists(string argMatGroup1Code, string argMaterialCode, string argDefectTypeCode, string argRepairTypeCode, string argClientCode)
        {
            bool IsMatGroup_RepairTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetMatGroup_RepairType(argMatGroup1Code, argMaterialCode, argDefectTypeCode, argRepairTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMatGroup_RepairTypeExists = true;
            }
            else
            {
                IsMatGroup_RepairTypeExists = false;
            }
            return IsMatGroup_RepairTypeExists;
        }

        public bool blnIsMatGroup_RepairTypeExists(string argMatGroup1Code, string argMaterialCode, string argDefectTypeCode, string argRepairTypeCode, string argClientCode,DataAccess da)
        {
            bool IsMatGroup_RepairTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetMatGroup_RepairType(argMatGroup1Code, argMaterialCode, argDefectTypeCode, argRepairTypeCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMatGroup_RepairTypeExists = true;
            }
            else
            {
                IsMatGroup_RepairTypeExists = false;
            }
            return IsMatGroup_RepairTypeExists;
        }
    }
}