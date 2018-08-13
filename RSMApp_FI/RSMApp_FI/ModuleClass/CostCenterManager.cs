
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
    public class CostCenterManager
    {
        const string CostCenterTable = "CostCenter";

       //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public CostCenter objGetCostCenter(string argCostCenterCode, string argClientCode)
        {
            CostCenter argCostCenter = new CostCenter();
            DataSet DataSetToFill = new DataSet();

            if (argCostCenterCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetCostCenter(argCostCenterCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argCostCenter = this.objCreateCostCenter((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argCostCenter;
        }
        
        public ICollection<CostCenter> colGetCostCenter(string argClientCode)
        {
            List<CostCenter> lst = new List<CostCenter>();
            DataSet DataSetToFill = new DataSet();
            CostCenter tCostCenter = new CostCenter();

            DataSetToFill = this.GetCostCenter(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCostCenter(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

      
        
        public DataSet GetCostCenter(string argCostCenterCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CostCenterCode", argCostCenterCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCostCenter4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetCostCenter(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCostCenter",param);
            return DataSetToFill;
        }
        
        public DataSet GetCostCenter(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + CostCenterTable.ToString();

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
        
        private CostCenter objCreateCostCenter(DataRow dr)
        {
            CostCenter tCostCenter = new CostCenter();

            tCostCenter.SetObjectInfo(dr);

            return tCostCenter;

        }
        
        public ICollection<ErrorHandler> SaveCostCenter(CostCenter argCostCenter)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsCostCenterExists(argCostCenter.CostCenterCode, argCostCenter.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertCostCenter(argCostCenter, da, lstErr);
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
                    UpdateCostCenter(argCostCenter, da, lstErr);
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
        
        public void InsertCostCenter(CostCenter argCostCenter, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[35];
            param[0] = new SqlParameter("@CostCenterCode", argCostCenter.CostCenterCode);
            param[1] = new SqlParameter("@CCName", argCostCenter.CCName);
            param[2] = new SqlParameter("@CCDescription", argCostCenter.CCDescription);
            param[3] = new SqlParameter("@COACode", argCostCenter.COACode);
            param[4] = new SqlParameter("@ValidFrom", argCostCenter.ValidFrom);
            param[5] = new SqlParameter("@ValidTo", argCostCenter.ValidTo);
            param[6] = new SqlParameter("@ContactPerson", argCostCenter.ContactPerson);
            param[7] = new SqlParameter("@Department", argCostCenter.Department);
            param[8] = new SqlParameter("@CCCategory", argCostCenter.CCCategory);
            param[9] = new SqlParameter("@CCGroup", argCostCenter.CCGroup);
            param[10] = new SqlParameter("@CompanyCode", argCostCenter.CompanyCode);
            param[11] = new SqlParameter("@BussinessAreaCode", argCostCenter.BussinessAreaCode);
            param[12] = new SqlParameter("@CurrencyCode", argCostCenter.CurrencyCode);
            param[13] = new SqlParameter("@ProfitCenterCode", argCostCenter.ProfitCenterCode);
            param[14] = new SqlParameter("@TelePhone1", argCostCenter.TelePhone1);
            param[15] = new SqlParameter("@TelePhone2", argCostCenter.TelePhone2);
            param[16] = new SqlParameter("@MobileNo", argCostCenter.MobileNo);
            param[17] = new SqlParameter("@EmailID", argCostCenter.EmailID);
            param[18] = new SqlParameter("@Title", argCostCenter.Title);
            param[19] = new SqlParameter("@Name1", argCostCenter.Name1);
            param[20] = new SqlParameter("@Name2", argCostCenter.Name2);
            param[21] = new SqlParameter("@Name3", argCostCenter.Name3);
            param[22] = new SqlParameter("@Name4", argCostCenter.Name4);
            param[23] = new SqlParameter("@Address1", argCostCenter.Address1);
            param[24] = new SqlParameter("@Address2", argCostCenter.Address2);
            param[25] = new SqlParameter("@CountryCode", argCostCenter.CountryCode);
            param[26] = new SqlParameter("@StateCode", argCostCenter.StateCode);
            param[27] = new SqlParameter("@City", argCostCenter.City);
            param[28] = new SqlParameter("@ZipCode", argCostCenter.ZipCode);
            param[29] = new SqlParameter("@ClientCode", argCostCenter.ClientCode);
            param[30] = new SqlParameter("@CreatedBy", argCostCenter.CreatedBy);
            param[31] = new SqlParameter("@ModifiedBy", argCostCenter.ModifiedBy);
   

            param[32] = new SqlParameter("@Type", SqlDbType.Char);
            param[32].Size = 1;
            param[32].Direction = ParameterDirection.Output;

            param[33] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[33].Size = 255;
            param[33].Direction = ParameterDirection.Output;

            param[34] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[34].Size = 20;
            param[34].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCostCenter", param);


            string strMessage = Convert.ToString(param[33].Value);
            string strType = Convert.ToString(param[32].Value);
            string strRetValue = Convert.ToString(param[34].Value);


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

        public void UpdateCostCenter(CostCenter argCostCenter, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[35];
            param[0] = new SqlParameter("@CostCenterCode", argCostCenter.CostCenterCode);
            param[1] = new SqlParameter("@CCName", argCostCenter.CCName);
            param[2] = new SqlParameter("@CCDescription", argCostCenter.CCDescription);
            param[3] = new SqlParameter("@COACode", argCostCenter.COACode);
            param[4] = new SqlParameter("@ValidFrom", argCostCenter.ValidFrom);
            param[5] = new SqlParameter("@ValidTo", argCostCenter.ValidTo);
            param[6] = new SqlParameter("@ContactPerson", argCostCenter.ContactPerson);
            param[7] = new SqlParameter("@Department", argCostCenter.Department);
            param[8] = new SqlParameter("@CCCategory", argCostCenter.CCCategory);
            param[9] = new SqlParameter("@CCGroup", argCostCenter.CCGroup);
            param[10] = new SqlParameter("@CompanyCode", argCostCenter.CompanyCode);
            param[11] = new SqlParameter("@BussinessAreaCode", argCostCenter.BussinessAreaCode);
            param[12] = new SqlParameter("@CurrencyCode", argCostCenter.CurrencyCode);
            param[13] = new SqlParameter("@ProfitCenterCode", argCostCenter.ProfitCenterCode);
            param[14] = new SqlParameter("@TelePhone1", argCostCenter.TelePhone1);
            param[15] = new SqlParameter("@TelePhone2", argCostCenter.TelePhone2);
            param[16] = new SqlParameter("@MobileNo", argCostCenter.MobileNo);
            param[17] = new SqlParameter("@EmailID", argCostCenter.EmailID);
            param[18] = new SqlParameter("@Title", argCostCenter.Title);
            param[19] = new SqlParameter("@Name1", argCostCenter.Name1);
            param[20] = new SqlParameter("@Name2", argCostCenter.Name2);
            param[21] = new SqlParameter("@Name3", argCostCenter.Name3);
            param[22] = new SqlParameter("@Name4", argCostCenter.Name4);
            param[23] = new SqlParameter("@Address1", argCostCenter.Address1);
            param[24] = new SqlParameter("@Address2", argCostCenter.Address2);
            param[25] = new SqlParameter("@CountryCode", argCostCenter.CountryCode);
            param[26] = new SqlParameter("@StateCode", argCostCenter.StateCode);
            param[27] = new SqlParameter("@City", argCostCenter.City);
            param[28] = new SqlParameter("@ZipCode", argCostCenter.ZipCode);
            param[29] = new SqlParameter("@ClientCode", argCostCenter.ClientCode);
            param[30] = new SqlParameter("@CreatedBy", argCostCenter.CreatedBy);
            param[31] = new SqlParameter("@ModifiedBy", argCostCenter.ModifiedBy);


            param[32] = new SqlParameter("@Type", SqlDbType.Char);
            param[32].Size = 1;
            param[32].Direction = ParameterDirection.Output;

            param[33] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[33].Size = 255;
            param[33].Direction = ParameterDirection.Output;

            param[34] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[34].Size = 20;
            param[34].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateCostCenter", param);


            string strMessage = Convert.ToString(param[33].Value);
            string strType = Convert.ToString(param[32].Value);
            string strRetValue = Convert.ToString(param[34].Value);


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
        
        public ICollection<ErrorHandler> DeleteCostCenter(string argCostCenterCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@CostCenterCode", argCostCenterCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteCostCenter", param);


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
        
        public bool blnIsCostCenterExists(string argCostCenterCode, string argClientCode)
        {
            bool IsCostCenterExists = false;
            DataSet ds = new DataSet();
            ds = GetCostCenter(argCostCenterCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCostCenterExists = true;
            }
            else
            {
                IsCostCenterExists = false;
            }
            return IsCostCenterExists;
        }
    }
}