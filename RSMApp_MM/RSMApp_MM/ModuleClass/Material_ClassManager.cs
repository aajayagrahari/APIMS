
//Created On :: 23, June, 2012
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
    public class Material_ClassManager
    {
        const string Material_ClassTable = "Material_Class";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public Material_Class objGetMaterial_Class(string argMaterialCode, int argidClass, string argClientCode)
        {
            Material_Class argMaterial_Class = new Material_Class();
            DataSet DataSetToFill = new DataSet();

            if (argMaterialCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argidClass <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetMaterial_Class(argMaterialCode, argidClass, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMaterial_Class = this.objCreateMaterial_Class((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argMaterial_Class;
        }
        
        public ICollection<Material_Class> colGetMaterial_Class(string argClientCode)
        {
            List<Material_Class> lst = new List<Material_Class>();
            DataSet DataSetToFill = new DataSet();
            Material_Class tMaterial_Class = new Material_Class();

            DataSetToFill = this.GetMaterial_Class(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMaterial_Class(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<Material_Class> colGetMaterial_Class(DataTable dt, string argUserName, string clientCode)
        {
            List<Material_Class> lst = new List<Material_Class>();
            Material_Class objMaterial_Class = null;
            foreach (DataRow dr in dt.Rows)
            {
                objMaterial_Class = new Material_Class();
                objMaterial_Class.MaterialCode = Convert.ToString(dr["MaterialCode"]).Trim();
                objMaterial_Class.idClass = Convert.ToInt32(dr["idClass"]);
                objMaterial_Class.ClassType = Convert.ToString(dr["ClassType"]).Trim();
                objMaterial_Class.ClassName = Convert.ToString(dr["ClassName"]).Trim();
                objMaterial_Class.MCStatus = Convert.ToString(dr["MCStatus"]).Trim();
                objMaterial_Class.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objMaterial_Class.ModifiedBy = Convert.ToString(argUserName).Trim();
                objMaterial_Class.CreatedBy = Convert.ToString(argUserName).Trim();
                objMaterial_Class.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objMaterial_Class);
            }
            return lst;
        }
        
        public DataSet GetMaterial_Class(string argMaterialCode, int argidClass, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@idClass", argidClass);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial_Class4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMaterial_Class(string argMaterialCode, int argidClass, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@idClass", argidClass);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMaterial_Class4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetMaterial_Class(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial_Class",param);
            return DataSetToFill;
        }

        public DataSet GetMaterial_Class(string argMaterialCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialCode",argMaterialCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial_Class4Material", param);
            return DataSetToFill;
        }
        
        private Material_Class objCreateMaterial_Class(DataRow dr)
        {
            Material_Class tMaterial_Class = new Material_Class();

            tMaterial_Class.SetObjectInfo(dr);

            return tMaterial_Class;

        }
        
        public ICollection<ErrorHandler> SaveMaterial_Class(Material_Class argMaterial_Class)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsMaterial_ClassExists(argMaterial_Class.MaterialCode, argMaterial_Class.idClass, argMaterial_Class.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertMaterial_Class(argMaterial_Class, da, lstErr);
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
                    UpdateMaterial_Class(argMaterial_Class, da, lstErr);
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

        public ICollection<ErrorHandler> SaveMaterial_Class(ICollection<Material_Class> colGetMaterial_Class)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Material_Class argMaterial_Class in colGetMaterial_Class)
                {
                    if (argMaterial_Class.IsDeleted == 0)
                    {
                        if (blnIsMaterial_ClassExists(argMaterial_Class.MaterialCode, argMaterial_Class.idClass, argMaterial_Class.ClientCode, da) == false)
                        {
                            InsertMaterial_Class(argMaterial_Class, da, lstErr);
                        }
                        else
                        {
                            UpdateMaterial_Class(argMaterial_Class, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMaterial_Class(argMaterial_Class.MaterialCode, argMaterial_Class.idClass, argMaterial_Class.ClientCode, argMaterial_Class.IsDeleted);
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

        public ICollection<ErrorHandler> SaveMaterial_Class(ICollection<Material_Class> colGetMaterial_Class, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (Material_Class argMaterial_Class in colGetMaterial_Class)
                {
                    if (argMaterial_Class.IsDeleted == 0)
                    {
                        if (blnIsMaterial_ClassExists(argMaterial_Class.MaterialCode, argMaterial_Class.idClass, argMaterial_Class.ClientCode, da) == false)
                        {
                            InsertMaterial_Class(argMaterial_Class, da, lstErr);
                        }
                        else
                        {
                            UpdateMaterial_Class(argMaterial_Class, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMaterial_Class(argMaterial_Class.MaterialCode, argMaterial_Class.idClass, argMaterial_Class.ClientCode, argMaterial_Class.IsDeleted);
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
                    SaveMaterial_Class(colGetMaterial_Class(dtExcel, argUserName, argClientCode), lstErr);

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
        
        public void InsertMaterial_Class(Material_Class argMaterial_Class, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@MaterialCode", argMaterial_Class.MaterialCode);
            param[1] = new SqlParameter("@idClass", argMaterial_Class.idClass);
            param[2] = new SqlParameter("@ClassType", argMaterial_Class.ClassType);
            param[3] = new SqlParameter("@ClassName", argMaterial_Class.ClassName);
            param[4] = new SqlParameter("@MCStatus", argMaterial_Class.MCStatus);
            param[5] = new SqlParameter("@ClientCode", argMaterial_Class.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argMaterial_Class.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argMaterial_Class.ModifiedBy);
            
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertMaterial_Class", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }
        
        public void UpdateMaterial_Class(Material_Class argMaterial_Class, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@MaterialCode", argMaterial_Class.MaterialCode);
            param[1] = new SqlParameter("@idClass", argMaterial_Class.idClass);
            param[2] = new SqlParameter("@ClassType", argMaterial_Class.ClassType);
            param[3] = new SqlParameter("@ClassName", argMaterial_Class.ClassName);
            param[4] = new SqlParameter("@MCStatus", argMaterial_Class.MCStatus);
            param[5] = new SqlParameter("@ClientCode", argMaterial_Class.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argMaterial_Class.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argMaterial_Class.ModifiedBy);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateMaterial_Class", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }
        
        public ICollection<ErrorHandler> DeleteMaterial_Class(string argMaterialCode, int argidClass, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
                param[1] = new SqlParameter("@idClass", argidClass);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("IsDeleted",iIsDeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteMaterial_Class", param);


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
        
        public bool blnIsMaterial_ClassExists(string argMaterialCode, int argidClass, string argClientCode)
        {
            bool IsMaterial_ClassExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterial_Class(argMaterialCode, argidClass, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterial_ClassExists = true;
            }
            else
            {
                IsMaterial_ClassExists = false;
            }
            return IsMaterial_ClassExists;
        }

        public bool blnIsMaterial_ClassExists(string argMaterialCode, int argidClass, string argClientCode, DataAccess da)
        {
            bool IsMaterial_ClassExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterial_Class(argMaterialCode, argidClass, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterial_ClassExists = true;
            }
            else
            {
                IsMaterial_ClassExists = false;
            }
            return IsMaterial_ClassExists;
        }
    }
}