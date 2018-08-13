
//Created On :: 26, September, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;


namespace RSMApp_FI
{
    public class ProfitCenterManager
    {
        const string ProfitCenterTable = "ProfitCenter";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public ProfitCenter objGetProfitCenter(string argProfitCenterCode, string argClientCode)
        {
            ProfitCenter argProfitCenter = new ProfitCenter();
            DataSet DataSetToFill = new DataSet();

            if (argProfitCenterCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetProfitCenter(argProfitCenterCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argProfitCenter = this.objCreateProfitCenter((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argProfitCenter;
        }
        
        public ICollection<ProfitCenter> colGetProfitCenter(string argClientCode)
        {
            List<ProfitCenter> lst = new List<ProfitCenter>();
            DataSet DataSetToFill = new DataSet();
            ProfitCenter tProfitCenter = new ProfitCenter();

            DataSetToFill = this.GetProfitCenter(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateProfitCenter(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

       
        
        public DataSet GetProfitCenter(string argProfitCenterCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ProfitCenterCode", argProfitCenterCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetProfitCenter4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetProfitCenter(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetProfitCenter",param);
            return DataSetToFill;
        }

        public DataSet GetProfitCenter(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + ProfitCenterTable.ToString();

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
        
        private ProfitCenter objCreateProfitCenter(DataRow dr)
        {
            ProfitCenter tProfitCenter = new ProfitCenter();

            tProfitCenter.SetObjectInfo(dr);

            return tProfitCenter;

        }
        
        public ICollection<ErrorHandler> SaveProfitCenter(ProfitCenter argProfitCenter)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsProfitCenterExists(argProfitCenter.ProfitCenterCode, argProfitCenter.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertProfitCenter(argProfitCenter, da, lstErr);
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
                    UpdateProfitCenter(argProfitCenter, da, lstErr);
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
        
        public void InsertProfitCenter(ProfitCenter argProfitCenter, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[31];
            param[0] = new SqlParameter("@ProfitCenterCode", argProfitCenter.ProfitCenterCode);
            param[1] = new SqlParameter("@PCName", argProfitCenter.PCName);
            param[2] = new SqlParameter("@PCDecription", argProfitCenter.PCDecription);
            param[3] = new SqlParameter("@COACode", argProfitCenter.COACode);
            param[4] = new SqlParameter("@PCStatus", argProfitCenter.PCStatus);
            param[5] = new SqlParameter("@ValidFrom", argProfitCenter.ValidFrom);
            param[6] = new SqlParameter("@ValidTo", argProfitCenter.ValidTo);
            param[7] = new SqlParameter("@ContactPerson", argProfitCenter.ContactPerson);
            param[8] = new SqlParameter("@Department", argProfitCenter.Department);
            param[9] = new SqlParameter("@PCGroup", argProfitCenter.PCGroup);
            param[10] = new SqlParameter("@TelePhone1", argProfitCenter.TelePhone1);
            param[11] = new SqlParameter("@TelePhone2", argProfitCenter.TelePhone2);
            param[12] = new SqlParameter("@MobileNo", argProfitCenter.MobileNo);
            param[13] = new SqlParameter("@EmailID", argProfitCenter.EmailID);
            param[14] = new SqlParameter("@Title", argProfitCenter.Title);
            param[15] = new SqlParameter("@Name1", argProfitCenter.Name1);
            param[16] = new SqlParameter("@Name2", argProfitCenter.Name2);
            param[17] = new SqlParameter("@Name3", argProfitCenter.Name3);
            param[18] = new SqlParameter("@Name4", argProfitCenter.Name4);
            param[19] = new SqlParameter("@Address1", argProfitCenter.Address1);
            param[20] = new SqlParameter("@Address2", argProfitCenter.Address2);
            param[21] = new SqlParameter("@CountryCode", argProfitCenter.CountryCode);
            param[22] = new SqlParameter("@StateCode", argProfitCenter.StateCode);
            param[23] = new SqlParameter("@City", argProfitCenter.City);
            param[24] = new SqlParameter("@ZipCode", argProfitCenter.ZipCode);
            param[25] = new SqlParameter("@ClientCode", argProfitCenter.ClientCode);
            param[26] = new SqlParameter("@CreatedBy", argProfitCenter.CreatedBy);
            param[27] = new SqlParameter("@ModifiedBy", argProfitCenter.ModifiedBy);
       
            param[28] = new SqlParameter("@Type", SqlDbType.Char);
            param[28].Size = 1;
            param[28].Direction = ParameterDirection.Output;

            param[29] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[29].Size = 255;
            param[29].Direction = ParameterDirection.Output;

            param[30] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[30].Size = 20;
            param[30].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertProfitCenter", param);


            string strMessage = Convert.ToString(param[29].Value);
            string strType = Convert.ToString(param[28].Value);
            string strRetValue = Convert.ToString(param[30].Value);


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
        
        public void UpdateProfitCenter(ProfitCenter argProfitCenter, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[31];
            param[0] = new SqlParameter("@ProfitCenterCode", argProfitCenter.ProfitCenterCode);
            param[1] = new SqlParameter("@PCName", argProfitCenter.PCName);
            param[2] = new SqlParameter("@PCDecription", argProfitCenter.PCDecription);
            param[3] = new SqlParameter("@COACode", argProfitCenter.COACode);
            param[4] = new SqlParameter("@PCStatus", argProfitCenter.PCStatus);
            param[5] = new SqlParameter("@ValidFrom", argProfitCenter.ValidFrom);
            param[6] = new SqlParameter("@ValidTo", argProfitCenter.ValidTo);
            param[7] = new SqlParameter("@ContactPerson", argProfitCenter.ContactPerson);
            param[8] = new SqlParameter("@Department", argProfitCenter.Department);
            param[9] = new SqlParameter("@PCGroup", argProfitCenter.PCGroup);
            param[10] = new SqlParameter("@TelePhone1", argProfitCenter.TelePhone1);
            param[11] = new SqlParameter("@TelePhone2", argProfitCenter.TelePhone2);
            param[12] = new SqlParameter("@MobileNo", argProfitCenter.MobileNo);
            param[13] = new SqlParameter("@EmailID", argProfitCenter.EmailID);
            param[14] = new SqlParameter("@Title", argProfitCenter.Title);
            param[15] = new SqlParameter("@Name1", argProfitCenter.Name1);
            param[16] = new SqlParameter("@Name2", argProfitCenter.Name2);
            param[17] = new SqlParameter("@Name3", argProfitCenter.Name3);
            param[18] = new SqlParameter("@Name4", argProfitCenter.Name4);
            param[19] = new SqlParameter("@Address1", argProfitCenter.Address1);
            param[20] = new SqlParameter("@Address2", argProfitCenter.Address2);
            param[21] = new SqlParameter("@CountryCode", argProfitCenter.CountryCode);
            param[22] = new SqlParameter("@StateCode", argProfitCenter.StateCode);
            param[23] = new SqlParameter("@City", argProfitCenter.City);
            param[24] = new SqlParameter("@ZipCode", argProfitCenter.ZipCode);
            param[25] = new SqlParameter("@ClientCode", argProfitCenter.ClientCode);
            param[26] = new SqlParameter("@CreatedBy", argProfitCenter.CreatedBy);
            param[27] = new SqlParameter("@ModifiedBy", argProfitCenter.ModifiedBy);

            param[28] = new SqlParameter("@Type", SqlDbType.Char);
            param[28].Size = 1;
            param[28].Direction = ParameterDirection.Output;

            param[29] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[29].Size = 255;
            param[29].Direction = ParameterDirection.Output;

            param[30] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[30].Size = 20;
            param[30].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateProfitCenter", param);


            string strMessage = Convert.ToString(param[29].Value);
            string strType = Convert.ToString(param[28].Value);
            string strRetValue = Convert.ToString(param[30].Value);


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

        public ICollection<ErrorHandler> DeleteProfitCenter(string argProfitCenterCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@ProfitCenterCode", argProfitCenterCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteProfitCenter", param);


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
        
        public bool blnIsProfitCenterExists(string argProfitCenterCode, string argClientCode)
        {
            bool IsProfitCenterExists = false;
            DataSet ds = new DataSet();
            ds = GetProfitCenter(argProfitCenterCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsProfitCenterExists = true;
            }
            else
            {
                IsProfitCenterExists = false;
            }
            return IsProfitCenterExists;
        }
    }
}