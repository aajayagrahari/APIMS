
//Created On :: 05, March, 2013
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
    public class AsgPartnerToPartnerManager
    {
        const string AsgPartnerToPartnerTable = "AsgPartnerToPartner";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        

        public AsgPartnerToPartner objGetAsgPartnerToPartner(string argAsgPartnerCode, string argPartnerCode, string argClientCode)
        {
            AsgPartnerToPartner argAsgPartnerToPartner = new AsgPartnerToPartner();
            DataSet DataSetToFill = new DataSet();

            if (argAsgPartnerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argPartnerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetAsgPartnerToPartner(argAsgPartnerCode, argPartnerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argAsgPartnerToPartner = this.objCreateAsgPartnerToPartner((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argAsgPartnerToPartner;
        }


        public ICollection<AsgPartnerToPartner> colGetAsgPartnerToPartner(string argAsgPartnerCode,string argClientCode)
        {
            List<AsgPartnerToPartner> lst = new List<AsgPartnerToPartner>();
            DataSet DataSetToFill = new DataSet();
            AsgPartnerToPartner tAsgPartnerToPartner = new AsgPartnerToPartner();

            DataSetToFill = this.GetAsgPartnerToPartner(argAsgPartnerCode,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateAsgPartnerToPartner(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetAsgPartnerToPartner(string argAsgPartnerCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@AsgPartnerCode", argAsgPartnerCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAsgPartnerToPartner4ID", param);

            return DataSetToFill;
        }

        public DataSet GetAsgPartnerToPartner(string argAsgPartnerCode, string argPartnerCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@AsgPartnerCode", argAsgPartnerCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetAsgPartnerToPartner4ID", param);

            return DataSetToFill;
        }


        public DataSet GetAsgPartnerToPartner(string argAsgPartnerCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AsgPartnerCode", argAsgPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAsgPartnerToPartner",param);
            return DataSetToFill;
        }


        private AsgPartnerToPartner objCreateAsgPartnerToPartner(DataRow dr)
        {
            AsgPartnerToPartner tAsgPartnerToPartner = new AsgPartnerToPartner();

            tAsgPartnerToPartner.SetObjectInfo(dr);

            return tAsgPartnerToPartner;

        }


        public ICollection<ErrorHandler> SaveAsgPartnerToPartner(AsgPartnerToPartner argAsgPartnerToPartner)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsAsgPartnerToPartnerExists(argAsgPartnerToPartner.AsgPartnerCode, argAsgPartnerToPartner.PartnerCode, argAsgPartnerToPartner.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertAsgPartnerToPartner(argAsgPartnerToPartner, da, lstErr);
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
                    UpdateAsgPartnerToPartner(argAsgPartnerToPartner, da, lstErr);
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

        public ICollection<ErrorHandler> SaveAsgPartnerToPartner(ICollection<AsgPartnerToPartner> colGetAsgPartnerToPartner)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (AsgPartnerToPartner argAsgPartnerToPartner in colGetAsgPartnerToPartner)
                {

                    if (argAsgPartnerToPartner.IsDeleted == 0)
                    {

                        if (blnIsAsgPartnerToPartnerExists(argAsgPartnerToPartner.AsgPartnerCode, argAsgPartnerToPartner.PartnerCode, argAsgPartnerToPartner.ClientCode, da) == false)
                        {
                            InsertAsgPartnerToPartner(argAsgPartnerToPartner, da, lstErr);
                        }
                        else
                        {
                            UpdateAsgPartnerToPartner(argAsgPartnerToPartner, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteAsgPartnerToPartner(argAsgPartnerToPartner.AsgPartnerCode, argAsgPartnerToPartner.PartnerCode, argAsgPartnerToPartner.ClientCode, argAsgPartnerToPartner.IsDeleted);

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

        public void SaveAsgPartnerToPartner(AsgPartnerToPartner argAsgPartnerToPartner, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsAsgPartnerToPartnerExists(argAsgPartnerToPartner.AsgPartnerCode, argAsgPartnerToPartner.PartnerCode, argAsgPartnerToPartner.ClientCode, da) == false)
                {
                    InsertAsgPartnerToPartner(argAsgPartnerToPartner, da, lstErr);
                }
                else
                {
                    UpdateAsgPartnerToPartner(argAsgPartnerToPartner, da, lstErr);
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
            AsgPartnerToPartner ObjAsgPartnerToPartner = null;
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
                        ObjAsgPartnerToPartner = new AsgPartnerToPartner();
                        ObjAsgPartnerToPartner.AsgPartnerCode = Convert.ToString(drExcel["AsgPartnerCode"]).Trim();
                        ObjAsgPartnerToPartner.PartnerCode = Convert.ToString(drExcel["PartnerCode"]).Trim();
                        ObjAsgPartnerToPartner.CreatedBy = Convert.ToString(argUserName);
                        ObjAsgPartnerToPartner.ModifiedBy = Convert.ToString(argUserName);
                        ObjAsgPartnerToPartner.ClientCode = Convert.ToString(argClientCode);
                        SaveAsgPartnerToPartner(ObjAsgPartnerToPartner, da, lstErr);

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



        public void InsertAsgPartnerToPartner(AsgPartnerToPartner argAsgPartnerToPartner, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@AsgPartnerCode", argAsgPartnerToPartner.AsgPartnerCode);
            param[1] = new SqlParameter("@PartnerCode", argAsgPartnerToPartner.PartnerCode);
            param[2] = new SqlParameter("@ClientCode", argAsgPartnerToPartner.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argAsgPartnerToPartner.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argAsgPartnerToPartner.ModifiedBy);
      
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertAsgPartnerToPartner", param);


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


        public void UpdateAsgPartnerToPartner(AsgPartnerToPartner argAsgPartnerToPartner, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@AsgPartnerCode", argAsgPartnerToPartner.AsgPartnerCode);
            param[1] = new SqlParameter("@PartnerCode", argAsgPartnerToPartner.PartnerCode);
            param[2] = new SqlParameter("@ClientCode", argAsgPartnerToPartner.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argAsgPartnerToPartner.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argAsgPartnerToPartner.ModifiedBy);

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateAsgPartnerToPartner", param);


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


        public ICollection<ErrorHandler> DeleteAsgPartnerToPartner(string argAsgPartnerCode, string argPartnerCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@AsgPartnerCode", argAsgPartnerCode);
                param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteAsgPartnerToPartner", param);


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


        public bool blnIsAsgPartnerToPartnerExists(string argAsgPartnerCode, string argPartnerCode, string argClientCode)
        {
            bool IsAsgPartnerToPartnerExists = false;
            DataSet ds = new DataSet();
            ds = GetAsgPartnerToPartner(argAsgPartnerCode, argPartnerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAsgPartnerToPartnerExists = true;
            }
            else
            {
                IsAsgPartnerToPartnerExists = false;
            }
            return IsAsgPartnerToPartnerExists;
        }

        public bool blnIsAsgPartnerToPartnerExists(string argAsgPartnerCode, string argPartnerCode, string argClientCode,DataAccess da)
        {
            bool IsAsgPartnerToPartnerExists = false;
            DataSet ds = new DataSet();
            ds = GetAsgPartnerToPartner(argAsgPartnerCode, argPartnerCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAsgPartnerToPartnerExists = true;
            }
            else
            {
                IsAsgPartnerToPartnerExists = false;
            }
            return IsAsgPartnerToPartnerExists;
        }
    }
}