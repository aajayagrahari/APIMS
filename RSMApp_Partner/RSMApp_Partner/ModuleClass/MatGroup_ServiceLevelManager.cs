
//Created On :: 29, November, 2012
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
    public class MatGroup_ServiceLevelManager
    {
        const string MatGroup_ServiceLevelTable = "MatGroup_ServiceLevel";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public MatGroup_ServiceLevel objGetMatGroup_ServiceLevel(int argLabourChrgID, string argClientCode)
        {
            MatGroup_ServiceLevel argMatGroup_ServiceLevel = new MatGroup_ServiceLevel();
            DataSet DataSetToFill = new DataSet();

            if (argLabourChrgID <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetMatGroup_ServiceLevel(argLabourChrgID, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMatGroup_ServiceLevel = this.objCreateMatGroup_ServiceLevel((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argMatGroup_ServiceLevel;
        }
        
        public ICollection<MatGroup_ServiceLevel> colGetMatGroup_ServiceLevel(string argClientCode)
        {
            List<MatGroup_ServiceLevel> lst = new List<MatGroup_ServiceLevel>();
            DataSet DataSetToFill = new DataSet();
            MatGroup_ServiceLevel tMatGroup_ServiceLevel = new MatGroup_ServiceLevel();

            DataSetToFill = this.GetMatGroup_ServiceLevel(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMatGroup_ServiceLevel(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<MatGroup_ServiceLevel> colGetMatGroup_ServiceLevel(DataTable dt, string argUserName, string clientCode)
        {
            List<MatGroup_ServiceLevel> lst = new List<MatGroup_ServiceLevel>();
            MatGroup_ServiceLevel objMatGroup_ServiceLevel = null;
            foreach (DataRow dr in dt.Rows)
            {
                objMatGroup_ServiceLevel = new MatGroup_ServiceLevel();
                objMatGroup_ServiceLevel.MatGroup1Code = Convert.ToString(dr["MatGroup1Code"]).Trim();
                objMatGroup_ServiceLevel.ServiceLevel = Convert.ToString(dr["ServiceLevel"]).Trim();
                objMatGroup_ServiceLevel.UnderWarLbChrg = Convert.ToDouble(dr["UnderWarLbChrg"]);
                objMatGroup_ServiceLevel.OutWarLbChrg = Convert.ToDouble(dr["OutWarLbChrg"]);
                objMatGroup_ServiceLevel.ValidFrom = Convert.ToString(dr["ValidFrom"]).Trim();
                objMatGroup_ServiceLevel.ValidTo = Convert.ToString(dr["ValidTo"]).Trim();
                objMatGroup_ServiceLevel.MaterialCode = Convert.ToString(dr["MaterialCode"].ToString().Trim());
                objMatGroup_ServiceLevel.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objMatGroup_ServiceLevel.ModifiedBy = Convert.ToString(argUserName).Trim();
                objMatGroup_ServiceLevel.CreatedBy = Convert.ToString(argUserName).Trim();
                objMatGroup_ServiceLevel.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objMatGroup_ServiceLevel);
            }
            return lst;
        }
        
        public DataSet GetMatGroup_ServiceLevel(int argLabourChrgID, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@LabourChrgID", argLabourChrgID);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMatGroup_ServiceLevel4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMatGroup_ServiceLevel(int argLabourChrgID, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@LabourChrgID", argLabourChrgID);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMatGroup_ServiceLevel4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetMatGroup_ServiceLevel(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMatGroup_ServiceLevel", param);
            return DataSetToFill;
        }

        public DataSet GetServiceLevelLbChrg4Repair(string argMatGroup1Code, string argMaterialCode, string argServiceLevel, DateTime argCurDate, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@ServiceLevel", argServiceLevel);
            param[3] = new SqlParameter("@CurDate", argCurDate);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetServiceLevelLbChrg4Repair", param);
            return DataSetToFill;
        }
        
        private MatGroup_ServiceLevel objCreateMatGroup_ServiceLevel(DataRow dr)
        {
            MatGroup_ServiceLevel tMatGroup_ServiceLevel = new MatGroup_ServiceLevel();

            tMatGroup_ServiceLevel.SetObjectInfo(dr);

            return tMatGroup_ServiceLevel;

        }
        
        public ICollection<ErrorHandler> SaveMatGroup_ServiceLevel(MatGroup_ServiceLevel argMatGroup_ServiceLevel)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsMatGroup_ServiceLevelExists(argMatGroup_ServiceLevel.LabourChrgID, argMatGroup_ServiceLevel.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertMatGroup_ServiceLevel(argMatGroup_ServiceLevel, da, lstErr);
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
                    UpdateMatGroup_ServiceLevel(argMatGroup_ServiceLevel, da, lstErr);
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

        public ICollection<ErrorHandler> SaveMatGroup_ServiceLevel(ICollection<MatGroup_ServiceLevel> colGetMatGroup_ServiceLevel, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (MatGroup_ServiceLevel argMatGroup_ServiceLevel in colGetMatGroup_ServiceLevel)
                {
                    if (argMatGroup_ServiceLevel.IsDeleted == 0)
                    {
                        if (blnIsMatGroup_ServiceLevelExists(argMatGroup_ServiceLevel.LabourChrgID, argMatGroup_ServiceLevel.ClientCode, da) == false)
                        {
                            InsertMatGroup_ServiceLevel(argMatGroup_ServiceLevel, da, lstErr);
                        }
                        else
                        {
                            UpdateMatGroup_ServiceLevel(argMatGroup_ServiceLevel, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMatGroup_ServiceLevel(argMatGroup_ServiceLevel.LabourChrgID, argMatGroup_ServiceLevel.ClientCode, argMatGroup_ServiceLevel.IsDeleted);
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
                    SaveMatGroup_ServiceLevel(colGetMatGroup_ServiceLevel(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertMatGroup_ServiceLevel(MatGroup_ServiceLevel argMatGroup_ServiceLevel, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];

            param[0] = new SqlParameter("@LabourChrgID", argMatGroup_ServiceLevel.LabourChrgID);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup_ServiceLevel.MatGroup1Code);
            param[2] = new SqlParameter("@MaterialCode", argMatGroup_ServiceLevel.MaterialCode);
            param[3] = new SqlParameter("@ServiceLevel", argMatGroup_ServiceLevel.ServiceLevel);
            param[4] = new SqlParameter("@UnderWarLbChrg", argMatGroup_ServiceLevel.UnderWarLbChrg);
            param[5] = new SqlParameter("@OutWarLbChrg", argMatGroup_ServiceLevel.OutWarLbChrg);
            param[6] = new SqlParameter("@ValidFrom", argMatGroup_ServiceLevel.ValidFrom);
            param[7] = new SqlParameter("@ValidTo", argMatGroup_ServiceLevel.ValidTo);
            param[8] = new SqlParameter("@ClientCode", argMatGroup_ServiceLevel.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argMatGroup_ServiceLevel.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argMatGroup_ServiceLevel.ModifiedBy);
           
            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertMatGroup_ServiceLevel", param);


            string strMessage = Convert.ToString(param[12].Value);
            string strType = Convert.ToString(param[11].Value);
            string strRetValue = Convert.ToString(param[13].Value);


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
        
        public void UpdateMatGroup_ServiceLevel(MatGroup_ServiceLevel argMatGroup_ServiceLevel, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];

            param[0] = new SqlParameter("@LabourChrgID", argMatGroup_ServiceLevel.LabourChrgID);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup_ServiceLevel.MatGroup1Code);
            param[2] = new SqlParameter("@MaterialCode", argMatGroup_ServiceLevel.MaterialCode);
            param[3] = new SqlParameter("@ServiceLevel", argMatGroup_ServiceLevel.ServiceLevel);
            param[4] = new SqlParameter("@UnderWarLbChrg", argMatGroup_ServiceLevel.UnderWarLbChrg);
            param[5] = new SqlParameter("@OutWarLbChrg", argMatGroup_ServiceLevel.OutWarLbChrg);
            param[6] = new SqlParameter("@ValidFrom", argMatGroup_ServiceLevel.ValidFrom);
            param[7] = new SqlParameter("@ValidTo", argMatGroup_ServiceLevel.ValidTo);
            param[8] = new SqlParameter("@ClientCode", argMatGroup_ServiceLevel.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argMatGroup_ServiceLevel.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argMatGroup_ServiceLevel.ModifiedBy);

            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateMatGroup_ServiceLevel", param);


            string strMessage = Convert.ToString(param[12].Value);
            string strType = Convert.ToString(param[11].Value);
            string strRetValue = Convert.ToString(param[13].Value);


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
        
        public ICollection<ErrorHandler> DeleteMatGroup_ServiceLevel(int argLabourChrgID, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@LabourChrgID", argLabourChrgID);
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
                int i = da.ExecuteNonQuery("Proc_DeleteMatGroup_ServiceLevel", param);


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
        
        public bool blnIsMatGroup_ServiceLevelExists(int argLabourChrgID, string argClientCode)
        {
            bool IsMatGroup_ServiceLevelExists = false;
            DataSet ds = new DataSet();
            ds = GetMatGroup_ServiceLevel(argLabourChrgID, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMatGroup_ServiceLevelExists = true;
            }
            else
            {
                IsMatGroup_ServiceLevelExists = false;
            }
            return IsMatGroup_ServiceLevelExists;
        }

        public bool blnIsMatGroup_ServiceLevelExists(int argLabourChrgID, string argClientCode,DataAccess da)
        {
            bool IsMatGroup_ServiceLevelExists = false;
            DataSet ds = new DataSet();
            ds = GetMatGroup_ServiceLevel(argLabourChrgID, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMatGroup_ServiceLevelExists = true;
            }
            else
            {
                IsMatGroup_ServiceLevelExists = false;
            }
            return IsMatGroup_ServiceLevelExists;
        }
    }
}