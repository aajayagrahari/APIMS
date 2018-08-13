
//Created On :: 15, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_Organization
{
    public class Company_plantManager
    {
        const string Company_plantTable = "Company_plant";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Company_plant objGetCompany_plant(string argCompanyCode, string argPlantCode, string argClientCode)
        {
            Company_plant argCompany_plant = new Company_plant();
            DataSet DataSetToFill = new DataSet();

            if (argCompanyCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argPlantCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCompany_plant(argCompanyCode, argPlantCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCompany_plant = this.objCreateCompany_plant((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCompany_plant;
        }

        public ICollection<Company_plant> colGetCompany_plant(string argClientCode)
        {
            List<Company_plant> lst = new List<Company_plant>();
            DataSet DataSetToFill = new DataSet();
            Company_plant tCompany_plant = new Company_plant();

            DataSetToFill = this.GetCompany_plant(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCompany_plant(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<Company_plant> colGetCompany_plant(DataTable dt, string argUserName, string clientCode)
        {
            List<Company_plant> lst = new List<Company_plant>();
            Company_plant objCompany_plant = null;
            foreach (DataRow dr in dt.Rows)
            {
                objCompany_plant = new Company_plant();
                objCompany_plant.CompanyCode = Convert.ToString(dr["CompanyCode"]).Trim();
                objCompany_plant.PlantCode = Convert.ToString(dr["PlantCode"]).Trim();
                objCompany_plant.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objCompany_plant.ModifiedBy = Convert.ToString(argUserName).Trim();
                objCompany_plant.CreatedBy = Convert.ToString(argUserName).Trim();
                objCompany_plant.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objCompany_plant);
            }
            return lst;
        }

        public DataSet GetCompany_plant(string argCompanyCode, string argPlantCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[1] = new SqlParameter("@PlantCode", argPlantCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCompany_plant4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCompany_plant(string argCompanyCode, string argPlantCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[1] = new SqlParameter("@PlantCode", argPlantCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCompany_plant4ID", param);

            return DataSetToFill;
        }

        public DataSet GetCompany_plant(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetCompany_plant",param);
            return DataSetToFill;
        }

        private Company_plant objCreateCompany_plant(DataRow dr)
        {
            Company_plant tCompany_plant = new Company_plant();

            tCompany_plant.SetObjectInfo(dr);

            return tCompany_plant;

        }

        public ICollection<ErrorHandler> SaveCompany_plant(Company_plant argCompany_plant)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCompany_plantExists(argCompany_plant.CompanyCode, argCompany_plant.PlantCode, argCompany_plant.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCompany_plant(argCompany_plant, da, lstErr);
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
                    UpdateCompany_plant(argCompany_plant, da, lstErr);
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

        public ICollection<ErrorHandler> SaveCompany_plant(ICollection<Company_plant> colGetCompany_plant, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Company_plant argCompany_plant in colGetCompany_plant)
                {
                    if (argCompany_plant.IsDeleted == 0)
                    {
                        if (blnIsCompany_plantExists(argCompany_plant.CompanyCode, argCompany_plant.PlantCode, argCompany_plant.ClientCode, da) == false)
                        {
                            InsertCompany_plant(argCompany_plant, da, lstErr);
                        }
                        else
                        {
                            UpdateCompany_plant(argCompany_plant, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteCompany_plant(argCompany_plant.CompanyCode, argCompany_plant.PlantCode, argCompany_plant.ClientCode, argCompany_plant.IsDeleted);
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
                    SaveCompany_plant(colGetCompany_plant(dtExcel, argUserName, argClientCode), lstErr);

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

        public void InsertCompany_plant(Company_plant argCompany_plant, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@CompanyCode", argCompany_plant.CompanyCode);
            param[1] = new SqlParameter("@PlantCode", argCompany_plant.PlantCode);
            param[2] = new SqlParameter("@ClientCode", argCompany_plant.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argCompany_plant.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argCompany_plant.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCompany_plant", param);


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

        public void UpdateCompany_plant(Company_plant argCompany_plant, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@CompanyCode", argCompany_plant.CompanyCode);
            param[1] = new SqlParameter("@PlantCode", argCompany_plant.PlantCode);
            param[2] = new SqlParameter("@ClientCode", argCompany_plant.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argCompany_plant.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argCompany_plant.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCompany_plant", param);


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

        public ICollection<ErrorHandler> DeleteCompany_plant(string argCompanyCode, string argPlantCode, string argClientCode,int IisDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@CompanyCode", argCompanyCode);
                param[1] = new SqlParameter("@PlantCode", argPlantCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("IsDeleted", IisDeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteCompany_plant", param);


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

        public bool blnIsCompany_plantExists(string argCompanyCode, string argPlantCode, string argClientCode)
        {
            bool IsCompany_plantExists = false;
            DataSet ds = new DataSet();
            ds = GetCompany_plant(argCompanyCode, argPlantCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCompany_plantExists = true;
            }
            else
            {
                IsCompany_plantExists = false;
            }
            return IsCompany_plantExists;
        }

        public bool blnIsCompany_plantExists(string argCompanyCode, string argPlantCode, string argClientCode, DataAccess da)
        {
            bool IsCompany_plantExists = false;
            DataSet ds = new DataSet();
            ds = GetCompany_plant(argCompanyCode, argPlantCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCompany_plantExists = true;
            }
            else
            {
                IsCompany_plantExists = false;
            }
            return IsCompany_plantExists;
        }
    }
}