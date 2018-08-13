
//Created On :: 26, September, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;


namespace RSMApp_Comman
{
    public class CalculationTypeMasterManager
    {
        const string CalculationTypeMasterTable = "CalculationTypeMaster";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public CalculationTypeMaster objGetCalculationTypeMaster(string argCalculationTypeCode, string argClientCode)
        {
            CalculationTypeMaster argCalculationTypeMaster = new CalculationTypeMaster();
            DataSet DataSetToFill = new DataSet();

            if (argCalculationTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCalculationTypeMaster(argCalculationTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCalculationTypeMaster = this.objCreateCalculationTypeMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCalculationTypeMaster;
        }


        public ICollection<CalculationTypeMaster> colGetCalculationTypeMaster(string argClientCode)
        {
            List<CalculationTypeMaster> lst = new List<CalculationTypeMaster>();
            DataSet DataSetToFill = new DataSet();
            CalculationTypeMaster tCalculationTypeMaster = new CalculationTypeMaster();

            DataSetToFill = this.GetCalculationTypeMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCalculationTypeMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetCalculationTypeMaster(string argCalculationTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CalculationTypeCode", argCalculationTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCalculationTypeMaster4ID", param);

            return DataSetToFill;
        }


        public DataSet GetCalculationTypeMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCalculationTypeMaster", param);
            return DataSetToFill;
        }


        public DataSet GetCalculationTypeMaster(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + CalculationTypeMasterTable.ToString();

                if (iIsDeleted >= 0)
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


        private CalculationTypeMaster objCreateCalculationTypeMaster(DataRow dr)
        {
            CalculationTypeMaster tCalculationTypeMaster = new CalculationTypeMaster();

            tCalculationTypeMaster.SetObjectInfo(dr);

            return tCalculationTypeMaster;

        }


        public ICollection<ErrorHandler> SaveCalculationTypeMaster(CalculationTypeMaster argCalculationTypeMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCalculationTypeMasterExists(argCalculationTypeMaster.CalculationTypeCode, argCalculationTypeMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCalculationTypeMaster(argCalculationTypeMaster, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
                    }
                }
                else
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    UpdateCalculationTypeMaster(argCalculationTypeMaster, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
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
            return lstErr;
        }


        public void InsertCalculationTypeMaster(CalculationTypeMaster argCalculationTypeMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@CalculationTypeCode", argCalculationTypeMaster.CalculationTypeCode);
            param[1] = new SqlParameter("@CalculationTypeDesc", argCalculationTypeMaster.CalculationTypeDesc);
            param[2] = new SqlParameter("@ClientCode", argCalculationTypeMaster.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argCalculationTypeMaster.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argCalculationTypeMaster.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCalculationTypeMaster", param);


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


        public void UpdateCalculationTypeMaster(CalculationTypeMaster argCalculationTypeMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@CalculationTypeCode", argCalculationTypeMaster.CalculationTypeCode);
            param[1] = new SqlParameter("@CalculationTypeDesc", argCalculationTypeMaster.CalculationTypeDesc);
            param[2] = new SqlParameter("@ClientCode", argCalculationTypeMaster.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argCalculationTypeMaster.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argCalculationTypeMaster.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCalculationTypeMaster", param);


            string strMessage = Convert.ToString(param[6].Value);
            string strType = Convert.ToString(param[5].Value);
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


        public ICollection<ErrorHandler> DeleteCalculationTypeMaster(string argCalculationTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@CalculationTypeCode", argCalculationTypeCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);

                param[2] = new SqlParameter("@Type", SqlDbType.Char);
                param[2].Size = 1;
                param[2].Direction = ParameterDirection.Output;
                param[3] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[3].Size = 255;
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[4].Size = 20;
                param[4].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteCalculationTypeMaster", param);


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


        public bool blnIsCalculationTypeMasterExists(string argCalculationTypeCode, string argClientCode)
        {
            bool IsCalculationTypeMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetCalculationTypeMaster(argCalculationTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCalculationTypeMasterExists = true;
            }
            else
            {
                IsCalculationTypeMasterExists = false;
            }
            return IsCalculationTypeMasterExists;
        }
    }
}