
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
    public class TransportZoneManager
    {
        const string TransportZoneTable = "TransportZone";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public TransportZone objGetTransportZone(string argTransportZoneCode,string argClientCode)
        {
            TransportZone argTransportZone = new TransportZone();
            DataSet DataSetToFill = new DataSet();

            if (argTransportZoneCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetTransportZone(argTransportZoneCode,argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argTransportZone = this.objCreateTransportZone((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argTransportZone;
        }

        public ICollection<TransportZone> colGetTransportZone(string argClientCode)
        {
            List<TransportZone> lst = new List<TransportZone>();
            DataSet DataSetToFill = new DataSet();
            TransportZone tTransportZone = new TransportZone();

            DataSetToFill = this.GetTransportZone(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateTransportZone(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

       

        public DataSet GetTransportZone(string argTransportZoneCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@TransportZoneCode", argTransportZoneCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
           
            DataSetToFill = da.FillDataSet("SP_GetTransportZone4ID", param);

            return DataSetToFill;
        }

        public DataSet GetTransportZone(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetTransportZone",param);
            return DataSetToFill;
        }

        public DataSet GetTransportZone(int iIsDeleted,string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + TransportZoneTable.ToString();

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

        private TransportZone objCreateTransportZone(DataRow dr)
        {
            TransportZone tTransportZone = new TransportZone();

            tTransportZone.SetObjectInfo(dr);

            return tTransportZone;

        }

        public ICollection<ErrorHandler> SaveTransportZone(TransportZone argTransportZone)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsTransportZoneExists(argTransportZone.TransportZoneCode,argTransportZone.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertTransportZone(argTransportZone, da, lstErr);
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
                    UpdateTransportZone(argTransportZone, da, lstErr);
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

        public void InsertTransportZone(TransportZone argTransportZone, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@TransportZoneCode", argTransportZone.TransportZoneCode);
            param[1] = new SqlParameter("@TransportZoneDesc", argTransportZone.TransportZoneDesc);
            param[2] = new SqlParameter("@ClientCode",argTransportZone.ClientCode);
       
            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;

            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertTransportZone", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public void UpdateTransportZone(TransportZone argTransportZone, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@TransportZoneCode", argTransportZone.TransportZoneCode);
            param[1] = new SqlParameter("@TransportZoneDesc", argTransportZone.TransportZoneDesc);
            param[2] = new SqlParameter("@ClientCode", argTransportZone.ClientCode);

            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;

            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateTransportZone", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public ICollection<ErrorHandler> DeleteTransportZone(string argTransportZoneCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@TransportZoneCode", argTransportZoneCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteTransportZone", param);


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
               // objErrorHandler.ReturnValue = strRetValue;
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

        public bool blnIsTransportZoneExists(string argTransportZoneCode,string argClientCode)
        {
            bool IsTransportZoneExists = false;
            DataSet ds = new DataSet();
            ds = GetTransportZone(argTransportZoneCode,argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsTransportZoneExists = true;
            }
            else
            {
                IsTransportZoneExists = false;
            }
            return IsTransportZoneExists;
        }
    }
}