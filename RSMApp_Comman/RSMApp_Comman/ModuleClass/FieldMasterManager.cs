
//Created On :: 17, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Comman
{
    public class FieldMasterManager
    {
        const string FieldMasterTable = "FieldMaster";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public FieldMaster objGetFieldMaster(string argFieldName)
        {
            FieldMaster argFieldMaster = new FieldMaster();
            DataSet DataSetToFill = new DataSet();

            if (argFieldName.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetFieldMaster(argFieldName);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argFieldMaster = this.objCreateFieldMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argFieldMaster;
        }

        public ICollection<FieldMaster> colGetFieldMaster()
        {
            List<FieldMaster> lst = new List<FieldMaster>();
            DataSet DataSetToFill = new DataSet();
            FieldMaster tFieldMaster = new FieldMaster();

            DataSetToFill = this.GetFieldMaster();

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateFieldMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

       

        public DataSet GetFieldMaster(int iIsDeleted)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + FieldMasterTable.ToString();

                if (iIsDeleted >= 0)
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " IsDeleted = " + iIsDeleted;
                }


                ds = da.FillDataSetWithSQL(tSQL);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet GetFieldMaster(string argFieldName)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@FieldName", argFieldName);

            DataSetToFill = da.FillDataSet("SP_GetFieldMaster4ID", param);

            return DataSetToFill;
        }


        public DataSet GetFieldMaster()
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            DataSetToFill = da.FillDataSet("SP_GetFieldMaster");
            return DataSetToFill;
        }


        private FieldMaster objCreateFieldMaster(DataRow dr)
        {
            FieldMaster tFieldMaster = new FieldMaster();

            tFieldMaster.SetObjectInfo(dr);

            return tFieldMaster;

        }


        public ICollection<ErrorHandler> SaveFieldMaster(FieldMaster argFieldMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsFieldMasterExists(argFieldMaster.FieldName) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertFieldMaster(argFieldMaster, da, lstErr);
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
                    UpdateFieldMaster(argFieldMaster, da, lstErr);
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


        public void InsertFieldMaster(FieldMaster argFieldMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@FieldName", argFieldMaster.FieldName);
            param[1] = new SqlParameter("@CreatedBy", argFieldMaster.CreatedBy);
            param[2] = new SqlParameter("@ModifiedBy", argFieldMaster.ModifiedBy);
            
            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;

            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertFieldMaster", param);


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


        public void UpdateFieldMaster(FieldMaster argFieldMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@FieldName", argFieldMaster.FieldName);
            param[1] = new SqlParameter("@CreatedBy", argFieldMaster.CreatedBy);
            param[2] = new SqlParameter("@ModifiedBy", argFieldMaster.ModifiedBy);
            
            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;

            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateFieldMaster", param);


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


        public ICollection<ErrorHandler> DeleteFieldMaster(string argFieldName,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@FieldName", argFieldName);
                param[1] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[2] = new SqlParameter("@Type", SqlDbType.Char);
                param[2].Size = 1;
                param[2].Direction = ParameterDirection.Output;

                param[3] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[3].Size = 255;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[4].Size = 20;
                param[4].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteFieldMaster", param);


                string strMessage = Convert.ToString(param[3].Value);
                string strType = Convert.ToString(param[2].Value);
                string strRetValue = Convert.ToString(param[4].Value);


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


        public bool blnIsFieldMasterExists(string argFieldName)
        {
            bool IsFieldMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetFieldMaster(argFieldName);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsFieldMasterExists = true;
            }
            else
            {
                IsFieldMasterExists = false;
            }
            return IsFieldMasterExists;
        }
    }
}