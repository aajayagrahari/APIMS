
//Created On :: 15, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;


namespace RSMApp_MM
{
    public class ValuationAreaManager
    {
        const string ValuationAreaTable = "ValuationArea";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public ValuationArea objGetValuationArea(string argValuationAreaCode, string argClientCode)
        {
            ValuationArea argValuationArea = new ValuationArea();
            DataSet DataSetToFill = new DataSet();

            if (argValuationAreaCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetValuationArea(argValuationAreaCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argValuationArea = this.objCreateValuationArea((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argValuationArea;
        }
        
        public ValuationArea objGetValuationArea(string argClientCode)
        {
            ValuationArea argValuationArea = new ValuationArea();
            DataSet DataSetToFill = new DataSet();

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetValuationArea(argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argValuationArea = this.objCreateValuationArea((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argValuationArea;
        }
        
        public ICollection<ValuationArea> colGetValuationArea(string argClientCode)
        {
            List<ValuationArea> lst = new List<ValuationArea>();
            DataSet DataSetToFill = new DataSet();
            ValuationArea tValuationArea = new ValuationArea();

            DataSetToFill = this.GetValuationArea(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateValuationArea(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        
        
        public DataSet GetValuationArea(string argValuationAreaCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ValuationAreaCode", argValuationAreaCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetValuationArea4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetValuationArea(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetValuationArea", param);
            return DataSetToFill;
        }

        public DataSet GetValuationArea4Combo(int argValuationMode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@ValuationMode", argValuationMode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetValuationArea4Combo", param);
            return DataSetToFill;
        }

        public DataSet GetValuationArea(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + ValuationAreaTable.ToString();

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

        private ValuationArea objCreateValuationArea(DataRow dr)
        {
            ValuationArea tValuationArea = new ValuationArea();

            tValuationArea.SetObjectInfo(dr);

            return tValuationArea;

        }
        
        public ICollection<ErrorHandler> SaveValuationArea(ValuationArea argValuationArea)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsValuationAreaExists(argValuationArea.ValuationAreaCode, argValuationArea.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertValuationArea(argValuationArea, da, lstErr);
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
                    UpdateValuationArea(argValuationArea, da, lstErr);
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
        
        public void InsertValuationArea(ValuationArea argValuationArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@ValuationAreaCode", argValuationArea.ValuationAreaCode);
            param[1] = new SqlParameter("@ValAreaDesc", argValuationArea.ValAreaDesc);
            param[2] = new SqlParameter("@ClientCode", argValuationArea.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argValuationArea.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argValuationArea.ModifiedBy);
            
            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertValuationArea", param);
            
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
        
        public void UpdateValuationArea(ValuationArea argValuationArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@ValuationAreaCode", argValuationArea.ValuationAreaCode);
            param[1] = new SqlParameter("@ValAreaDesc", argValuationArea.ValAreaDesc);
            param[2] = new SqlParameter("@ClientCode", argValuationArea.ClientCode);
            param[3] = new SqlParameter("@CreatedBy", argValuationArea.CreatedBy);
            param[4] = new SqlParameter("@ModifiedBy", argValuationArea.ModifiedBy);

            param[5] = new SqlParameter("@Type", SqlDbType.Char);
            param[5].Size = 1;
            param[5].Direction = ParameterDirection.Output;

            param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[6].Size = 255;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[7].Size = 20;
            param[7].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateValuationArea", param);


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
        
        public ICollection<ErrorHandler> DeleteValuationArea(string argValuationAreaCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@ValuationAreaCode", argValuationAreaCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteValuationArea", param);


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
        
        public bool blnIsValuationAreaExists(string argValuationAreaCode, string argClientCode)
        {
            bool IsValuationAreaExists = false;
            DataSet ds = new DataSet();
            ds = GetValuationArea(argValuationAreaCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsValuationAreaExists = true;
            }
            else
            {
                IsValuationAreaExists = false;
            }
            return IsValuationAreaExists;
        }
    }
}