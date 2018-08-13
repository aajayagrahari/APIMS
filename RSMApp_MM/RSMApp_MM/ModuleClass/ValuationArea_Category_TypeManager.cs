
//Created On :: 12, September, 2012
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
    public class ValuationArea_Category_TypeManager
    {
        const string ValuationArea_Category_TypeTable = "ValuationArea_Category_Type";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public ValuationArea_Category_Type objGetValuationArea_Category_Type(string argValCatCode, string argValTypeCode, string argValAreaCode, string argClientCode)
        {
            ValuationArea_Category_Type argValuationArea_Category_Type = new ValuationArea_Category_Type();
            DataSet DataSetToFill = new DataSet();

            if (argValCatCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argValTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argValAreaCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetValuationArea_Category_Type(argValCatCode, argValTypeCode, argValAreaCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argValuationArea_Category_Type = this.objCreateValuationArea_Category_Type((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argValuationArea_Category_Type;
        }
        
        public ICollection<ValuationArea_Category_Type> colGetValuationArea_Category_Type(string argClientCode)
        {
            List<ValuationArea_Category_Type> lst = new List<ValuationArea_Category_Type>();
            DataSet DataSetToFill = new DataSet();
            ValuationArea_Category_Type tValuationArea_Category_Type = new ValuationArea_Category_Type();

            DataSetToFill = this.GetValuationArea_Category_Type(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateValuationArea_Category_Type(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<ValuationArea_Category_Type> colGetValuationArea_Category_Type(DataTable dt, string argUserName, string clientCode)
        {
            List<ValuationArea_Category_Type> lst = new List<ValuationArea_Category_Type>();
            ValuationArea_Category_Type objValuationArea_Category_Type = null;
            foreach (DataRow dr in dt.Rows)
            {
                objValuationArea_Category_Type = new ValuationArea_Category_Type();
                objValuationArea_Category_Type.PlantCode = Convert.ToString(dr["PlantCode"]).Trim();
                objValuationArea_Category_Type.ValCatCode = Convert.ToString(dr["ValCatCode"]).Trim();
                objValuationArea_Category_Type.ValTypeCode = Convert.ToString(dr["ValTypeCode"]).Trim();
                objValuationArea_Category_Type.ValAreaCode = Convert.ToString(dr["ValAreaCode"]).Trim();
                objValuationArea_Category_Type.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objValuationArea_Category_Type.ModifiedBy = Convert.ToString(argUserName).Trim();
                objValuationArea_Category_Type.CreatedBy = Convert.ToString(argUserName).Trim();
                objValuationArea_Category_Type.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objValuationArea_Category_Type);
            }
            return lst;
        }
        
        public DataSet GetValuationArea_Category_Type(string argValCatCode, string argValTypeCode, string argValAreaCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ValCatCode", argValCatCode);
            param[1] = new SqlParameter("@ValTypeCode", argValTypeCode);
            param[2] = new SqlParameter("@ValAreaCode", argValAreaCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetValuationArea_Category_Type4ID", param);

            return DataSetToFill;
        }

        public DataSet GetValuationArea_Category_Type(string argValCatCode, string argValTypeCode, string argValAreaCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@ValCatCode", argValCatCode);
            param[1] = new SqlParameter("@ValTypeCode", argValTypeCode);
            param[2] = new SqlParameter("@ValAreaCode", argValAreaCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetValuationArea_Category_Type4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetValuationArea_Category_Type(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetValuationArea_Category_Type",param);
            return DataSetToFill;
        }
        
        private ValuationArea_Category_Type objCreateValuationArea_Category_Type(DataRow dr)
        {
            ValuationArea_Category_Type tValuationArea_Category_Type = new ValuationArea_Category_Type();

            tValuationArea_Category_Type.SetObjectInfo(dr);

            return tValuationArea_Category_Type;

        }
        
        public ICollection<ErrorHandler> SaveValuationArea_Category_Type(ValuationArea_Category_Type argValuationArea_Category_Type)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsValuationArea_Category_TypeExists(argValuationArea_Category_Type.ValCatCode, argValuationArea_Category_Type.ValTypeCode, argValuationArea_Category_Type.ValAreaCode, argValuationArea_Category_Type.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertValuationArea_Category_Type(argValuationArea_Category_Type, da, lstErr);
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
                    UpdateValuationArea_Category_Type(argValuationArea_Category_Type, da, lstErr);
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

        public ICollection<ErrorHandler> SaveValuationArea_Category_Type(ICollection<ValuationArea_Category_Type> colGetValuationArea_Category_Type, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (ValuationArea_Category_Type argValuationArea_Category_Type in colGetValuationArea_Category_Type)
                {
                    if (argValuationArea_Category_Type.IsDeleted == 0)
                    {
                        if (blnIsValuationArea_Category_TypeExists(argValuationArea_Category_Type.ValCatCode, argValuationArea_Category_Type.ValTypeCode, argValuationArea_Category_Type.ValAreaCode, argValuationArea_Category_Type.ClientCode, da) == false)
                        {
                            InsertValuationArea_Category_Type(argValuationArea_Category_Type, da, lstErr);
                        }
                        else
                        {
                            UpdateValuationArea_Category_Type(argValuationArea_Category_Type, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteValuationArea_Category_Type(argValuationArea_Category_Type.ValCatCode, argValuationArea_Category_Type.ValTypeCode, argValuationArea_Category_Type.ValAreaCode, argValuationArea_Category_Type.ClientCode, argValuationArea_Category_Type.IsDeleted);
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
                    SaveValuationArea_Category_Type(colGetValuationArea_Category_Type(dtExcel, argUserName, argClientCode), lstErr);

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
        
        public void InsertValuationArea_Category_Type(ValuationArea_Category_Type argValuationArea_Category_Type, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@PlantCode", argValuationArea_Category_Type.PlantCode);
            param[1] = new SqlParameter("@ValCatCode", argValuationArea_Category_Type.ValCatCode);
            param[2] = new SqlParameter("@ValTypeCode", argValuationArea_Category_Type.ValTypeCode);
            param[3] = new SqlParameter("@ValAreaCode", argValuationArea_Category_Type.ValAreaCode);
            param[4] = new SqlParameter("@ClientCode", argValuationArea_Category_Type.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argValuationArea_Category_Type.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argValuationArea_Category_Type.ModifiedBy);
           

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertValuationArea_Category_Type", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }
        
        public void UpdateValuationArea_Category_Type(ValuationArea_Category_Type argValuationArea_Category_Type, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@PlantCode", argValuationArea_Category_Type.PlantCode);
            param[1] = new SqlParameter("@ValCatCode", argValuationArea_Category_Type.ValCatCode);
            param[2] = new SqlParameter("@ValTypeCode", argValuationArea_Category_Type.ValTypeCode);
            param[3] = new SqlParameter("@ValAreaCode", argValuationArea_Category_Type.ValAreaCode);
            param[4] = new SqlParameter("@ClientCode", argValuationArea_Category_Type.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argValuationArea_Category_Type.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argValuationArea_Category_Type.ModifiedBy);


            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateValuationArea_Category_Type", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }
        
        public ICollection<ErrorHandler> DeleteValuationArea_Category_Type(string argValCatCode, string argValTypeCode, string argValAreaCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@ValCatCode", argValCatCode);
                param[1] = new SqlParameter("@ValTypeCode", argValTypeCode);
                param[2] = new SqlParameter("@ValAreaCode", argValAreaCode);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted",iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteValuationArea_Category_Type", param);


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
        
        public bool blnIsValuationArea_Category_TypeExists(string argValCatCode, string argValTypeCode, string argValAreaCode, string argClientCode)
        {
            bool IsValuationArea_Category_TypeExists = false;
            DataSet ds = new DataSet();
            ds = GetValuationArea_Category_Type(argValCatCode, argValTypeCode, argValAreaCode, argClientCode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsValuationArea_Category_TypeExists = true;
            }
            else
            {
                IsValuationArea_Category_TypeExists = false;
            }
            return IsValuationArea_Category_TypeExists;
        }

        public bool blnIsValuationArea_Category_TypeExists(string argValCatCode, string argValTypeCode, string argValAreaCode, string argClientCode, DataAccess da)
        {
            bool IsValuationArea_Category_TypeExists = false;
            DataSet ds = new DataSet();
            ds = GetValuationArea_Category_Type(argValCatCode, argValTypeCode, argValAreaCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsValuationArea_Category_TypeExists = true;
            }
            else
            {
                IsValuationArea_Category_TypeExists = false;
            }
            return IsValuationArea_Category_TypeExists;
        }
    }
}