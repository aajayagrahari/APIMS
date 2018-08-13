
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
    public class MaterialGroup1Manager
    {
        const string MaterialGroup1Table = "MaterialGroup1";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public MaterialGroup1 objGetMaterialGroup1(string argMatGroup1Code, string argClientCode)
        {
            MaterialGroup1 argMaterialGroup1 = new MaterialGroup1();
            DataSet DataSetToFill = new DataSet();

            if (argMatGroup1Code.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetMaterialGroup1(argMatGroup1Code, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMaterialGroup1 = this.objCreateMaterialGroup1((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argMaterialGroup1;
        }
               
        public DataSet GetMaterialGroup14Combo(string argPrefix, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialGroup14Combo", param);

            return DataSetToFill;
        }
                
        public DataSet GetMaterialGroup1(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + MaterialGroup1Table.ToString();

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

        public DataSet GetMaterialGroup14MaterialType(string strMaterialTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialTypeCode", strMaterialTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialGroup14MatType", param);
            return DataSetToFill;
        }
        
        public ICollection<MaterialGroup1> colGetMaterialGroup1(string argClientCode)
        {
            List<MaterialGroup1> lst = new List<MaterialGroup1>();
            DataSet DataSetToFill = new DataSet();
            MaterialGroup1 tMaterialGroup1 = new MaterialGroup1();

            DataSetToFill = this.GetMaterialGroup1(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMaterialGroup1(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetMaterialGroup1(string argMatGroup1Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialGroup14ID", param);

            return DataSetToFill;
        }

        public DataSet GetMaterialGroup1(string argMatGroup1Code, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMaterialGroup14ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetMaterialGroup1(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialGroup1", param);
            return DataSetToFill;
        }

       
        
        private MaterialGroup1 objCreateMaterialGroup1(DataRow dr)
        {
            MaterialGroup1 tMaterialGroup1 = new MaterialGroup1();

            tMaterialGroup1.SetObjectInfo(dr);

            return tMaterialGroup1;

        }

        //public ICollection<ErrorHandler> SaveMaterialGroup1(MaterialGroup1 argMaterialGroup1)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsMaterialGroup1Exists(argMaterialGroup1.MatGroup1Code, argMaterialGroup1.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertMaterialGroup1(argMaterialGroup1, da, lstErr);
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
        //            UpdateMaterialGroup1(argMaterialGroup1, da, lstErr);
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

        public PartnerErrorResult_MM SaveMaterialGroup1(MaterialGroup1 argMaterialGroup1)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            PartnerErrorResult_MM errorcol = new PartnerErrorResult_MM();
            MaterialGroup1Manager objMaterialGroup1Manager = new MaterialGroup1Manager();

            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                objMaterialGroup1Manager.SaveMaterialGroup1(argMaterialGroup1, ref da, ref lstErr);

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
      
        public void SaveMaterialGroup1(MaterialGroup1 argMaterialGroup1, ref DataAccess da, ref List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsMaterialGroup1Exists(argMaterialGroup1.MatGroup1Code, argMaterialGroup1.ClientCode, da) == false)
                {
                    InsertMaterialGroup1(argMaterialGroup1, da, lstErr);
                }
                else
                {
                    UpdateMaterialGroup1(argMaterialGroup1, da, lstErr);
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
            MaterialGroup1 ObjMaterialGroup1 = null;
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


                        ObjMaterialGroup1 = new MaterialGroup1();

                        ObjMaterialGroup1.MatGroup1Code = Convert.ToString(drExcel["MatGroup1Code"]).Trim();
                        ObjMaterialGroup1.MatGroup1Desc = Convert.ToString(drExcel["MatGroup1Desc"]).Trim();
                        ObjMaterialGroup1.CreatedBy = Convert.ToString(argUserName);
                        ObjMaterialGroup1.ModifiedBy = Convert.ToString(argUserName);
                        ObjMaterialGroup1.ClientCode = Convert.ToString(argClientCode);

                        SaveMaterialGroup1(ObjMaterialGroup1, ref da, ref lstErr);

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
           
        public void InsertMaterialGroup1(MaterialGroup1 argMaterialGroup1, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@MatGroup1Code", argMaterialGroup1.MatGroup1Code);
            param[1] = new SqlParameter("@MatGroup1Desc", argMaterialGroup1.MatGroup1Desc);
            param[2] = new SqlParameter("@ClientCode", argMaterialGroup1.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argMaterialGroup1.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argMaterialGroup1.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertMaterialGroup1", param);


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
        
        public void UpdateMaterialGroup1(MaterialGroup1 argMaterialGroup1, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@MatGroup1Code", argMaterialGroup1.MatGroup1Code);
            param[1] = new SqlParameter("@MatGroup1Desc", argMaterialGroup1.MatGroup1Desc);
            param[2] = new SqlParameter("@ClientCode", argMaterialGroup1.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argMaterialGroup1.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argMaterialGroup1.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateMaterialGroup1", param);


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
        
        public PartnerErrorResult_MM DeleteMaterialGroup1(string argMatGroup1Code, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            PartnerErrorResult_MM errorCol = new PartnerErrorResult_MM();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
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

                int i = da.ExecuteNonQuery("Proc_DeleteMaterialGroup1", param);


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
        
        public bool blnIsMaterialGroup1Exists(string argMatGroup1Code, string argClientCode)
        {
            bool IsMaterialGroup1Exists = false;
            DataSet ds = new DataSet();
            ds = GetMaterialGroup1(argMatGroup1Code, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterialGroup1Exists = true;
            }
            else
            {
                IsMaterialGroup1Exists = false;
            }
            return IsMaterialGroup1Exists;
        }

        public bool blnIsMaterialGroup1Exists(string argMatGroup1Code, string argClientCode,DataAccess da)
        {
            bool IsMaterialGroup1Exists = false;
            DataSet ds = new DataSet();
            ds = GetMaterialGroup1(argMatGroup1Code, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterialGroup1Exists = true;
            }
            else
            {
                IsMaterialGroup1Exists = false;
            }
            return IsMaterialGroup1Exists;
        }

    }
}