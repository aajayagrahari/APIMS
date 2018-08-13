
//Created On :: 05, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;


namespace RSMApp_Comman
{
    public class ClientManager
    {
        const string ClientTable = "Client";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Client objGetClient(string argClientCode)
        {
            Client argClient = new Client();
            DataSet DataSetToFill = new DataSet();

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            DataSetToFill = this.GetClient(argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argClient = this.objCreateClient((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandler:

        Finish:
            DataSetToFill = null;


            return argClient;
        }
        
        public ICollection<Client> colGetClient()
        {
            List<Client> lst = new List<Client>();
            DataSet DataSetToFill = new DataSet();
            Client tClient = new Client();

            DataSetToFill = this.GetClient();

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateClient(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        

        public DataSet GetClient4Exsist(string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.NFillDataSet("SP_GetClient4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetClient(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetClient4ID", param);

            return DataSetToFill;
        }

        public DataSet GetClient(int iIsDeleted)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + ClientTable.ToString();

                if (iIsDeleted > -1)
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
        
        public DataSet GetClient()
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            DataSetToFill = da.FillDataSet("SP_GetClient");
            return DataSetToFill;
        }

        private Client objCreateClient(DataRow dr)
        {
            Client tClient = new Client();

            tClient.SetObjectInfo(dr);

            return tClient;

        }
        
        public ICollection<ErrorHandler> SaveClient(Client argClient)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsClientExists(argClient.ClientCode, da) == false)
                {                   
                    InsertClient(argClient, da, lstErr);
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
                    //da.Open_Connection();
                    //da.BEGIN_TRANSACTION();
                    UpdateClient(argClient, da, lstErr);
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
        
        public void InsertClient(Client argClient, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@ClientCode", argClient.ClientCode);
            param[1] = new SqlParameter("@ClientName", argClient.ClientName);
            param[2] = new SqlParameter("@ClientDescription", argClient.ClientDescription);
            param[3] = new SqlParameter("@LicenceCode", argClient.LicenceCode);
            param[4] = new SqlParameter("@ValuationMode", argClient.ValuationMode);
            param[5] = new SqlParameter("@LicencePeriod", argClient.LicencePeriod);
                        
            param[6] = new SqlParameter("@CreatedBy", argClient.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argClient.ModifiedBy);
            

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertClient", param);


            string strType = Convert.ToString(param[8].Value);
            string strMessage = Convert.ToString(param[9].Value);            
            string strRetValue = Convert.ToString(param[10].Value);


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
        
        public void UpdateClient(Client argClient, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@ClientCode", argClient.ClientCode);
            param[1] = new SqlParameter("@ClientName", argClient.ClientName);
            param[2] = new SqlParameter("@ClientDescription", argClient.ClientDescription);
            param[3] = new SqlParameter("@LicenceCode", argClient.LicenceCode);
            param[4] = new SqlParameter("@ValuationMode", argClient.ValuationMode);
            param[5] = new SqlParameter("@LicencePeriod", argClient.LicencePeriod);

            param[6] = new SqlParameter("@CreatedBy", argClient.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argClient.ModifiedBy);


            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateClient", param);

            string strType = Convert.ToString(param[8].Value);
            string strMessage = Convert.ToString(param[9].Value);            
            string strRetValue = Convert.ToString(param[10].Value);

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

        public ICollection<ErrorHandler> DeleteClient(string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[4];
                param[0] = new SqlParameter("@ClientCode", argClientCode);

                param[1] = new SqlParameter("@Type", SqlDbType.Char);
                param[1].Size = 1;
                param[1].Direction = ParameterDirection.Output;

                param[2] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[2].Size = 255;
                param[2].Direction = ParameterDirection.Output;

                param[3] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[3].Size = 20;
                param[3].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteClient", param);


                string strMessage = Convert.ToString(param[2].Value);
                string strType = Convert.ToString(param[1].Value);
                string strRetValue = Convert.ToString(param[3].Value);


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
        
        public bool blnIsClientExists(string argClientCode, DataAccess da)
        {
            bool IsClientExists = false;
            DataSet ds = new DataSet();
            ds = GetClient4Exsist(argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsClientExists = true;
            }
            else
            {
                IsClientExists = false;
            }
            return IsClientExists;
        }
    
    }
}