
//Created On :: 03, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;


namespace RSMApp_Comman
{
    public class IndustryMasterManager
    {
        const string IndustryMasterTable = "IndustryMaster";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public IndustryMaster objGetIndustryMaster(string argIndustryName,string argClientCode)
        {
            IndustryMaster argIndustryMaster = new IndustryMaster();
            DataSet DataSetToFill = new DataSet();

            if (argIndustryName.Trim() == "")
            {
                goto ErrorHandlers;
            }
            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetIndustryMaster(argIndustryName,argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argIndustryMaster = this.objCreateIndustryMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argIndustryMaster;
        }


        public ICollection<IndustryMaster> colGetIndustryMaster(string argClientCode)
        {
            List<IndustryMaster> lst = new List<IndustryMaster>();
            DataSet DataSetToFill = new DataSet();
            IndustryMaster tIndustryMaster = new IndustryMaster();

            DataSetToFill = this.GetIndustryMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateIndustryMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

      


        public DataSet GetIndustryMaster(string argIndustryName,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@IndustryName", argIndustryName);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetIndustryMaster4ID", param);

            return DataSetToFill;
        }


        public DataSet GetIndustryMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];

            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetIndustryMaster",param);
            return DataSetToFill;
        }

        public DataSet GetIndustryMaster(int iIsDeleted,string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + IndustryMasterTable.ToString();

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


        private IndustryMaster objCreateIndustryMaster(DataRow dr)
        {
            IndustryMaster tIndustryMaster = new IndustryMaster();

            tIndustryMaster.SetObjectInfo(dr);

            return tIndustryMaster;

        }


        public ICollection<ErrorHandler> SaveIndustryMaster(IndustryMaster argIndustryMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsIndustryMasterExists(argIndustryMaster.IndustryName,argIndustryMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertIndustryMaster(argIndustryMaster, da, lstErr);
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
                    UpdateIndustryMaster(argIndustryMaster, da, lstErr);
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


        public void InsertIndustryMaster(IndustryMaster argIndustryMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@IndustryName", argIndustryMaster.IndustryName);
            param[1] = new SqlParameter("@ClientCode", argIndustryMaster.ClientCode);

            param[2] = new SqlParameter("@Type", SqlDbType.Char);
            param[2].Size = 1;
            param[2].Direction = ParameterDirection.Output;

            param[3] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[3].Size = 255;
            param[3].Direction = ParameterDirection.Output;

            param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[4].Size = 20;
            param[4].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertIndustryMaster", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }


        public void UpdateIndustryMaster(IndustryMaster argIndustryMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@IndustryName", argIndustryMaster.IndustryName);
            param[1] = new SqlParameter("@ClientCode", argIndustryMaster.ClientCode);

            param[2] = new SqlParameter("@Type", SqlDbType.Char);
            param[2].Size = 1;
            param[2].Direction = ParameterDirection.Output;

            param[3] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[3].Size = 255;
            param[3].Direction = ParameterDirection.Output;

            param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[4].Size = 20;
            param[4].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateIndustryMaster", param);


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


        public ICollection<ErrorHandler> DeleteIndustryMaster(string argIndustryName,string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@IndustryName", argIndustryName);
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

                int i = da.ExecuteNonQuery("Proc_DeleteIndustryMaster", param);


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


        public bool blnIsIndustryMasterExists(string argIndustryName,string argClientCode)
        {
            bool IsIndustryMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetIndustryMaster(argIndustryName,argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsIndustryMasterExists = true;
            }
            else
            {
                IsIndustryMasterExists = false;
            }
            return IsIndustryMasterExists;
        }
    }
}