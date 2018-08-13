
//Created On :: 23, February, 2013
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
    public class PartnerPricingProcedureManager
    {
        const string PartnerPricingProcedureTable = "PartnerPricingProcedure";

       //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public PartnerPricingProcedure objGetPartnerPricingProcedure(string argProcedureType, int argLineItemNo, string argClientCode)
        {
            PartnerPricingProcedure argPartnerPricingProcedure = new PartnerPricingProcedure();
            DataSet DataSetToFill = new DataSet();

            if (argProcedureType.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argLineItemNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPartnerPricingProcedure(argProcedureType, argLineItemNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartnerPricingProcedure = this.objCreatePartnerPricingProcedure((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPartnerPricingProcedure;
        }


        public ICollection<PartnerPricingProcedure> colGetPartnerPricingProcedure(string argClientCode)
        {
            List<PartnerPricingProcedure> lst = new List<PartnerPricingProcedure>();
            DataSet DataSetToFill = new DataSet();
            PartnerPricingProcedure tPartnerPricingProcedure = new PartnerPricingProcedure();

            DataSetToFill = this.GetPartnerPricingProcedure(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePartnerPricingProcedure(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public ICollection<PartnerPricingProcedure> colGetPartnerPricingProcedure(DataTable dt, string argUserName, string clientCode)
        {
            List<PartnerPricingProcedure> lst = new List<PartnerPricingProcedure>();
            PartnerPricingProcedure objPartnerPricingProcedure = null;
            foreach (DataRow dr in dt.Rows)
            {
                objPartnerPricingProcedure = new PartnerPricingProcedure();
                objPartnerPricingProcedure.ProcedureType = Convert.ToString(dr["ProcedureType"]).Trim();
                objPartnerPricingProcedure.LineItemNo = Convert.ToInt32(dr["LineItemNo"]);
                objPartnerPricingProcedure.PricingDescription = Convert.ToString(dr["PricingDescription"]).Trim();
                objPartnerPricingProcedure.CalculationType = Convert.ToString(dr["CalculationType"]).Trim();
                objPartnerPricingProcedure.CalculationValue = Convert.ToDouble(dr["CalculationValue"]);
                objPartnerPricingProcedure.CalculationOn = Convert.ToString(dr["CalculationON"]).Trim();
                objPartnerPricingProcedure.CalcLineItemNo = Convert.ToInt32(dr["CalcLineItemNo"]);
                objPartnerPricingProcedure.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objPartnerPricingProcedure.ModifiedBy = Convert.ToString(argUserName).Trim();
                objPartnerPricingProcedure.CreatedBy = Convert.ToString(argUserName).Trim();
                objPartnerPricingProcedure.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objPartnerPricingProcedure);
            }
            return lst;
        }


        public DataSet GetPartnerPricingProcedure(string argProcedureType, int argLineItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ProcedureType", argProcedureType);
            param[1] = new SqlParameter("@LineItemNo", argLineItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerPricingProcedure4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerPricingProcedure(string argProcedureType, int argLineItemNo, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@ProcedureType", argProcedureType);
            param[1] = new SqlParameter("@LineItemNo", argLineItemNo);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPartnerPricingProcedure4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerPricingProcedure(string argProcedureType, string argClientCode)
        {
            DataSet DataSetToFill = new DataSet();
            DataAccess da = new DataAccess();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ProcedureType", argProcedureType);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerPricingProcedure4ProcedureType", param);

            return DataSetToFill;
        }



        public DataSet GetPartnerPricingProcedure(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerPricingProcedure",param);
            return DataSetToFill;
        }
        public DataSet GetPartnerPricingProcedure(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + PartnerPricingProcedureTable.ToString();

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


        private PartnerPricingProcedure objCreatePartnerPricingProcedure(DataRow dr)
        {
            PartnerPricingProcedure tPartnerPricingProcedure = new PartnerPricingProcedure();

            tPartnerPricingProcedure.SetObjectInfo(dr);

            return tPartnerPricingProcedure;

        }


        public ICollection<ErrorHandler> SavePartnerPricingProcedure(PartnerPricingProcedure argPartnerPricingProcedure)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartnerPricingProcedureExists(argPartnerPricingProcedure.ProcedureType, argPartnerPricingProcedure.LineItemNo, argPartnerPricingProcedure.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartnerPricingProcedure(argPartnerPricingProcedure, da, lstErr);
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
                    UpdatePartnerPricingProcedure(argPartnerPricingProcedure, da, lstErr);
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

        public ICollection<ErrorHandler> SavePartnerPricingProcedure(ICollection<PartnerPricingProcedure> colGetPartnerPricingProcedure)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (PartnerPricingProcedure argPartnerPricingProcedure in colGetPartnerPricingProcedure)
                {

                    if (argPartnerPricingProcedure.IsDeleted == 0)
                    {

                        if (blnIsPartnerPricingProcedureExists(argPartnerPricingProcedure.ProcedureType, argPartnerPricingProcedure.LineItemNo, argPartnerPricingProcedure.ClientCode, da) == false)
                        {
                            InsertPartnerPricingProcedure(argPartnerPricingProcedure, da, lstErr);
                        }
                        else
                        {
                            UpdatePartnerPricingProcedure(argPartnerPricingProcedure, da, lstErr);
                        }
                    }
                    else
                    {
                        DeletePartnerPricingProcedure(argPartnerPricingProcedure.ProcedureType, argPartnerPricingProcedure.LineItemNo, argPartnerPricingProcedure.ClientCode, argPartnerPricingProcedure.IsDeleted);

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

        public ICollection<ErrorHandler> SavePartnerPricingProcedure(ICollection<PartnerPricingProcedure> colGetPartnerPricingProcedure, List<ErrorHandler> lstErr)
        {
        
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (PartnerPricingProcedure argPartnerPricingProcedure in colGetPartnerPricingProcedure)
                {

                    if (argPartnerPricingProcedure.IsDeleted == 0)
                    {

                        if (blnIsPartnerPricingProcedureExists(argPartnerPricingProcedure.ProcedureType, argPartnerPricingProcedure.LineItemNo, argPartnerPricingProcedure.ClientCode, da) == false)
                        {
                            InsertPartnerPricingProcedure(argPartnerPricingProcedure, da, lstErr);
                        }
                        else
                        {
                            UpdatePartnerPricingProcedure(argPartnerPricingProcedure, da, lstErr);
                        }
                    }
                    else
                    {
                        DeletePartnerPricingProcedure(argPartnerPricingProcedure.ProcedureType, argPartnerPricingProcedure.LineItemNo, argPartnerPricingProcedure.ClientCode, argPartnerPricingProcedure.IsDeleted);

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
                    SavePartnerPricingProcedure(colGetPartnerPricingProcedure(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertPartnerPricingProcedure(PartnerPricingProcedure argPartnerPricingProcedure, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@ProcedureType", argPartnerPricingProcedure.ProcedureType);
            param[1] = new SqlParameter("@LineItemNo", argPartnerPricingProcedure.LineItemNo);
            param[2] = new SqlParameter("@PricingDescription", argPartnerPricingProcedure.PricingDescription);
            param[3] = new SqlParameter("@CalculationType", argPartnerPricingProcedure.CalculationType);
            param[4] = new SqlParameter("@CalculationValue", argPartnerPricingProcedure.CalculationValue);
            param[5] = new SqlParameter("@CalculationOn", argPartnerPricingProcedure.CalculationOn);
            param[6] = new SqlParameter("@CalcLineItemNo", argPartnerPricingProcedure.CalcLineItemNo);
            param[7] = new SqlParameter("@ClientCode", argPartnerPricingProcedure.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argPartnerPricingProcedure.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argPartnerPricingProcedure.ModifiedBy);
     
            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;
            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;
            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertPartnerPricingProcedure", param);


            string strMessage = Convert.ToString(param[11].Value);
            string strType = Convert.ToString(param[10].Value);
            string strRetValue = Convert.ToString(param[12].Value);


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

        public void UpdatePartnerPricingProcedure(PartnerPricingProcedure argPartnerPricingProcedure, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@ProcedureType", argPartnerPricingProcedure.ProcedureType);
            param[1] = new SqlParameter("@LineItemNo", argPartnerPricingProcedure.LineItemNo);
            param[2] = new SqlParameter("@PricingDescription", argPartnerPricingProcedure.PricingDescription);
            param[3] = new SqlParameter("@CalculationType", argPartnerPricingProcedure.CalculationType);
            param[4] = new SqlParameter("@CalculationValue", argPartnerPricingProcedure.CalculationValue);
            param[5] = new SqlParameter("@CalculationOn", argPartnerPricingProcedure.CalculationOn);
            param[6] = new SqlParameter("@CalcLineItemNo", argPartnerPricingProcedure.CalcLineItemNo);
            param[7] = new SqlParameter("@ClientCode", argPartnerPricingProcedure.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argPartnerPricingProcedure.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argPartnerPricingProcedure.ModifiedBy);

            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;
            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;
            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePartnerPricingProcedure", param);


            string strMessage = Convert.ToString(param[11].Value);
            string strType = Convert.ToString(param[10].Value);
            string strRetValue = Convert.ToString(param[12].Value);


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


        public ICollection<ErrorHandler> DeletePartnerPricingProcedure(string argProcedureType, int argLineItemNo, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@ProcedureType", argProcedureType);
                param[1] = new SqlParameter("@LineItemNo", argLineItemNo);
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

                int i = da.ExecuteNonQuery("Proc_DeletePartnerPricingProcedure", param);


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


        public bool blnIsPartnerPricingProcedureExists(string argProcedureType, int argLineItemNo, string argClientCode)
        {
            bool IsPartnerPricingProcedureExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerPricingProcedure(argProcedureType, argLineItemNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerPricingProcedureExists = true;
            }
            else
            {
                IsPartnerPricingProcedureExists = false;
            }
            return IsPartnerPricingProcedureExists;
        }

        public bool blnIsPartnerPricingProcedureExists(string argProcedureType, int argLineItemNo, string argClientCode,DataAccess da)
        {
            bool IsPartnerPricingProcedureExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerPricingProcedure(argProcedureType, argLineItemNo, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerPricingProcedureExists = true;
            }
            else
            {
                IsPartnerPricingProcedureExists = false;
            }
            return IsPartnerPricingProcedureExists;
        }
    }
}