
//Created On :: 23, August, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using Telerik.Web.UI;
using System.Data.OleDb;

namespace RSMApp_Organization
{
    public class Plant_ShippingPointManager
    {
        const string Plant_ShippingPointTable = "Plant_ShippingPoint";

         // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Plant_ShippingPoint objGetPlant_ShippingPoint(string argPlantCode, string argShippingPointCode, string argClientCode)
        {
            Plant_ShippingPoint argPlant_ShippingPoint = new Plant_ShippingPoint();
            DataSet DataSetToFill = new DataSet();

            if (argPlantCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argShippingPointCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPlant_ShippingPoint(argPlantCode, argShippingPointCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPlant_ShippingPoint = this.objCreatePlant_ShippingPoint((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPlant_ShippingPoint;
        }
       
        public ICollection<Plant_ShippingPoint> colGetPlant_ShippingPoint(string argClientCode)
        {
            List<Plant_ShippingPoint> lst = new List<Plant_ShippingPoint>();
            DataSet DataSetToFill = new DataSet();
            Plant_ShippingPoint tPlant_ShippingPoint = new Plant_ShippingPoint();

            DataSetToFill = this.GetPlant_ShippingPoint(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePlant_ShippingPoint(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<Plant_ShippingPoint> colGetPlant_ShippingPoint(DataTable dt, string argUserName, string clientCode)
        {
            List<Plant_ShippingPoint> lst = new List<Plant_ShippingPoint>();
            Plant_ShippingPoint objPlant_ShippingPoint = null;
            foreach (DataRow dr in dt.Rows)
            {
                objPlant_ShippingPoint = new Plant_ShippingPoint();
                objPlant_ShippingPoint.PlantCode = Convert.ToString(dr["PlantCode"]).Trim();
                objPlant_ShippingPoint.ShippingPointCode = Convert.ToString(dr["ShippingPointCode"]).Trim();
                objPlant_ShippingPoint.CreatedBy = Convert.ToString(argUserName);
                objPlant_ShippingPoint.ModifiedBy = Convert.ToString(argUserName);
                objPlant_ShippingPoint.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objPlant_ShippingPoint);
            }
            return lst;
        }
        
        public DataSet GetPlant_ShippingPoint(string argPlantCode, string argShippingPointCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PlantCode", argPlantCode);
            param[1] = new SqlParameter("@ShippingPointCode", argShippingPointCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPlant_ShippingPoint4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPlant_ShippingPoint(string argPlantCode, string argShippingPointCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PlantCode", argPlantCode);
            param[1] = new SqlParameter("@ShippingPointCode", argShippingPointCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPlant_ShippingPoint4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetPlant_ShippingPoint(string argPlantCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PlantCode", argPlantCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPlant_ShippingPoint4Combo", param);

            return DataSetToFill;
        }

        public DataSet GetPlant_ShippingPoint(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPlant_ShippingPoint",param);
            return DataSetToFill;
        }
        
        private Plant_ShippingPoint objCreatePlant_ShippingPoint(DataRow dr)
        {
            Plant_ShippingPoint tPlant_ShippingPoint = new Plant_ShippingPoint();

            tPlant_ShippingPoint.SetObjectInfo(dr);

            return tPlant_ShippingPoint;

        }
        
        public ICollection<ErrorHandler> SavePlant_ShippingPoint(Plant_ShippingPoint argPlant_ShippingPoint)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPlant_ShippingPointExists(argPlant_ShippingPoint.PlantCode, argPlant_ShippingPoint.ShippingPointCode, argPlant_ShippingPoint.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPlant_ShippingPoint(argPlant_ShippingPoint, da, lstErr);
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
                    UpdatePlant_ShippingPoint(argPlant_ShippingPoint, da, lstErr);
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

        /*************/
        public ICollection<ErrorHandler> SavePlant_ShippingPoint(ICollection<Plant_ShippingPoint> colGetPlant_ShippingPoint, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Plant_ShippingPoint argPlant_ShippingPoint in colGetPlant_ShippingPoint)
                {
                    if (argPlant_ShippingPoint.IsDeleted == 0)
                    {
                        if (blnIsPlant_ShippingPointExists(argPlant_ShippingPoint.PlantCode, argPlant_ShippingPoint.ShippingPointCode, argPlant_ShippingPoint.ClientCode, da) == false)
                        {
                            InsertPlant_ShippingPoint(argPlant_ShippingPoint, da, lstErr);
                        }
                        else
                        {
                            UpdatePlant_ShippingPoint(argPlant_ShippingPoint, da, lstErr);
                        }
                    }
                    else
                    {
                        DeletePlant_ShippingPoint(argPlant_ShippingPoint.PlantCode, argPlant_ShippingPoint.ShippingPointCode, argPlant_ShippingPoint.ClientCode, argPlant_ShippingPoint.IsDeleted);
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
                    SavePlant_ShippingPoint(colGetPlant_ShippingPoint(dtExcel, argUserName, argClientCode), lstErr);

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
        /**********/
        
        public void InsertPlant_ShippingPoint(Plant_ShippingPoint argPlant_ShippingPoint, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@PlantCode", argPlant_ShippingPoint.PlantCode);
            param[1] = new SqlParameter("@ShippingPointCode", argPlant_ShippingPoint.ShippingPointCode);
            param[2] = new SqlParameter("@ClientCode", argPlant_ShippingPoint.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argPlant_ShippingPoint.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argPlant_ShippingPoint.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPlant_ShippingPoint", param);
            
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
        
        public void UpdatePlant_ShippingPoint(Plant_ShippingPoint argPlant_ShippingPoint, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@PlantCode", argPlant_ShippingPoint.PlantCode);
            param[1] = new SqlParameter("@ShippingPointCode", argPlant_ShippingPoint.ShippingPointCode);
            param[2] = new SqlParameter("@ClientCode", argPlant_ShippingPoint.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argPlant_ShippingPoint.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argPlant_ShippingPoint.ModifiedBy);

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePlant_ShippingPoint", param);


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
        
        public ICollection<ErrorHandler> DeletePlant_ShippingPoint(string argPlantCode, string argShippingPointCode, string argClientCode,int iIsdeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@PlantCode", argPlantCode);
                param[1] = new SqlParameter("@ShippingPointCode", argShippingPointCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("IsDeleted",iIsdeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeletePlant_ShippingPoint", param);
                
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
        
        public bool blnIsPlant_ShippingPointExists(string argPlantCode, string argShippingPointCode, string argClientCode)
        {
            bool IsPlant_ShippingPointExists = false;
            DataSet ds = new DataSet();
            ds = GetPlant_ShippingPoint(argPlantCode, argShippingPointCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPlant_ShippingPointExists = true;
            }
            else
            {
                IsPlant_ShippingPointExists = false;
            }
            return IsPlant_ShippingPointExists;
        }

        public bool blnIsPlant_ShippingPointExists(string argPlantCode, string argShippingPointCode, string argClientCode, DataAccess da)
        {
            bool IsPlant_ShippingPointExists = false;
            DataSet ds = new DataSet();
            ds = GetPlant_ShippingPoint(argPlantCode, argShippingPointCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPlant_ShippingPointExists = true;
            }
            else
            {
                IsPlant_ShippingPointExists = false;
            }
            return IsPlant_ShippingPointExists;
        }
    }
}