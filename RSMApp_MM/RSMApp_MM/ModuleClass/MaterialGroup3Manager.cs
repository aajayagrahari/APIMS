
//Created On :: 15, May, 2012
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
    public class MaterialGroup3Manager
    {
        const string MaterialGroup3Table = "MaterialGroup3";

       //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public MaterialGroup3 objGetMaterialGroup3(string argMatGroup3Code, string argClientCode)
        {
            MaterialGroup3 argMaterialGroup3 = new MaterialGroup3();
            DataSet DataSetToFill = new DataSet();

            if (argMatGroup3Code.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetMaterialGroup3(argMatGroup3Code, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMaterialGroup3 = this.objCreateMaterialGroup3((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argMaterialGroup3;
        }

        public DataSet GetMaterialGroup3(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + MaterialGroup3Table.ToString();

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

        public ICollection<MaterialGroup3> colGetMaterialGroup3(string argClientCode)
        {
            List<MaterialGroup3> lst = new List<MaterialGroup3>();
            DataSet DataSetToFill = new DataSet();
            MaterialGroup3 tMaterialGroup3 = new MaterialGroup3();

            DataSetToFill = this.GetMaterialGroup3(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMaterialGroup3(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetMaterialGroup3(string argMatGroup3Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MatGroup3Code", argMatGroup3Code);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialGroup34ID", param);

            return DataSetToFill;
        }

        public DataSet GetMaterialGroup3(string argMatGroup3Code, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MatGroup3Code", argMatGroup3Code);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMaterialGroup34ID", param);

            return DataSetToFill;
        }

        public DataSet GetMaterialGroup3(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialGroup3", param);
            return DataSetToFill;
        }

        private MaterialGroup3 objCreateMaterialGroup3(DataRow dr)
        {
            MaterialGroup3 tMaterialGroup3 = new MaterialGroup3();

            tMaterialGroup3.SetObjectInfo(dr);

            return tMaterialGroup3;

        }

        //public ICollection<ErrorHandler> SaveMaterialGroup3(MaterialGroup3 argMaterialGroup3)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsMaterialGroup3Exists(argMaterialGroup3.MatGroup3Code, argMaterialGroup3.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertMaterialGroup3(argMaterialGroup3, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }

        //            da.COMMIT_TRANSACTION();
        //        }
        //        else
        //        {
        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            UpdateMaterialGroup3(argMaterialGroup3, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }

        //            da.COMMIT_TRANSACTION();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (da != null)
        //        {
        //            da.ROLLBACK_TRANSACTION();
        //        }
        //        objErrorHandler.Type = ErrorConstant.strAboartType;
        //        objErrorHandler.MsgId = 0;
        //        objErrorHandler.Module = ErrorConstant.strInsertModule;
        //        objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
        //        objErrorHandler.Message = ex.Message.ToString();
        //        objErrorHandler.RowNo = 0;
        //        objErrorHandler.FieldName = "";
        //        objErrorHandler.LogCode = "";
        //        lstErr.Add(objErrorHandler);
        //    }
        //    finally
        //    {
        //        if (da != null)
        //        {
        //            da.Close_Connection();
        //            da = null;
        //        }
        //    }
        //    return lstErr;
        //}


        public PartnerErrorResult_MM SaveMaterialGroup3(MaterialGroup3 argMaterialGroup3)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            PartnerErrorResult_MM errorcol = new PartnerErrorResult_MM();
            MaterialGroup3Manager objMaterialGroup3Manager = new MaterialGroup3Manager();

            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                objMaterialGroup3Manager.SaveMaterialGroup3(argMaterialGroup3, ref da, ref lstErr);

                foreach (ErrorHandler objerr in lstErr)
                {
                    if (objerr.Type == "E")
                    {
                        errorcol.colErrorHandler.Add(objerr);
                        da.ROLLBACK_TRANSACTION();
                        return errorcol;
                    }

                    if (objerr.Type == "A")
                    {
                        errorcol.colErrorHandler.Add(objerr);
                        da.ROLLBACK_TRANSACTION();
                        return errorcol;
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
                errorcol.colErrorHandler.Add(objErrorHandler);
            }
            finally
            {
                if (da != null)
                {
                    da.Close_Connection();
                    da = null;
                }
            }
            return errorcol;

        }
        public void SaveMaterialGroup3(MaterialGroup3 argMaterialGroup3, ref DataAccess da, ref List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsMaterialGroup3Exists(argMaterialGroup3.MatGroup3Code, argMaterialGroup3.ClientCode, da) == false)
                {
                    InsertMaterialGroup3(argMaterialGroup3, ref da, ref lstErr);
                }
                else
                {
                    UpdateMaterialGroup3(argMaterialGroup3, ref da, ref lstErr);
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
            MaterialGroup3 ObjMaterialGroup3 = null;
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


                        ObjMaterialGroup3 = new MaterialGroup3();

                        ObjMaterialGroup3.MatGroup3Code = Convert.ToString(drExcel["MatGroup3Code"]).Trim();
                        ObjMaterialGroup3.MatGroup3Desc = Convert.ToString(drExcel["MatGroup3Desc"]).Trim();
                        ObjMaterialGroup3.CreatedBy = Convert.ToString(argUserName);
                        ObjMaterialGroup3.ModifiedBy = Convert.ToString(argUserName);
                        ObjMaterialGroup3.ClientCode = Convert.ToString(argClientCode);

                        SaveMaterialGroup3(ObjMaterialGroup3, ref da, ref lstErr);

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
    
        public void InsertMaterialGroup3(MaterialGroup3 argMaterialGroup3, ref DataAccess da, ref List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@MatGroup3Code", argMaterialGroup3.MatGroup3Code);
            param[1] = new SqlParameter("@MatGroup3Desc", argMaterialGroup3.MatGroup3Desc);
            param[2] = new SqlParameter("@ClientCode", argMaterialGroup3.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argMaterialGroup3.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argMaterialGroup3.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertMaterialGroup3", param);


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

        public void UpdateMaterialGroup3(MaterialGroup3 argMaterialGroup3, ref DataAccess da, ref List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@MatGroup3Code", argMaterialGroup3.MatGroup3Code);
            param[1] = new SqlParameter("@MatGroup3Desc", argMaterialGroup3.MatGroup3Desc);
            param[2] = new SqlParameter("@ClientCode", argMaterialGroup3.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argMaterialGroup3.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argMaterialGroup3.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateMaterialGroup3", param);


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

        public PartnerErrorResult_MM DeleteMaterialGroup3(string argMatGroup3Code, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            PartnerErrorResult_MM errorCol = new PartnerErrorResult_MM();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@MatGroup3Code", argMatGroup3Code);
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

                int i = da.ExecuteNonQuery("Proc_DeleteMaterialGroup3", param);


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
                errorCol.colErrorHandler.Add(objErrorHandler);

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
                errorCol.colErrorHandler.Add(objErrorHandler);
            }
            return errorCol;

        }

        public bool blnIsMaterialGroup3Exists(string argMatGroup3Code, string argClientCode)
        {
            bool IsMaterialGroup3Exists = false;
            DataSet ds = new DataSet();
            ds = GetMaterialGroup3(argMatGroup3Code, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterialGroup3Exists = true;
            }
            else
            {
                IsMaterialGroup3Exists = false;
            }
            return IsMaterialGroup3Exists;
        }

        public bool blnIsMaterialGroup3Exists(string argMatGroup3Code, string argClientCode,DataAccess da)
        {
            bool IsMaterialGroup3Exists = false;
            DataSet ds = new DataSet();
            ds = GetMaterialGroup3(argMatGroup3Code, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterialGroup3Exists = true;
            }
            else
            {
                IsMaterialGroup3Exists = false;
            }
            return IsMaterialGroup3Exists;
        }
    }
}