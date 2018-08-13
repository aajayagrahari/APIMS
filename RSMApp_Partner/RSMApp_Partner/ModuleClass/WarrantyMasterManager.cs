
//Created On :: 16, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class WarrantyMasterManager
    {
        const string WarrantyMasterTable = "WarrantyMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        WarrantyDetailManager objWarrantyDetailManager = new WarrantyDetailManager();

        public WarrantyMaster objGetWarrantyMaster(string argWarrantyCode, string argClientCode)
        {
            WarrantyMaster argWarrantyMaster = new WarrantyMaster();
            DataSet DataSetToFill = new DataSet();

            if (argWarrantyCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetWarrantyMaster(argWarrantyCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argWarrantyMaster = this.objCreateWarrantyMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argWarrantyMaster;
        }

        public ICollection<WarrantyMaster> colGetWarrantyMaster(string argClientCode)
        {
            List<WarrantyMaster> lst = new List<WarrantyMaster>();
            DataSet DataSetToFill = new DataSet();
            WarrantyMaster tWarrantyMaster = new WarrantyMaster();

            DataSetToFill = this.GetWarrantyMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateWarrantyMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetWarrantyMaster(string argWarrantyCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@WarrantyCode", argWarrantyCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetWarrantyMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetWarrantyMaster(string argWarrantyCode, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@WarrantyCode", argWarrantyCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetWarrantyMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetWarrantyMaster(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + WarrantyMasterTable.ToString();

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

                    tSQL = tSQL + " " + sWhereClauseSTR + " ClientCode = '" + argClientCode + "'  AND IsActive=0";
                }

                ds = da.FillDataSetWithSQL(tSQL);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        public DataSet GetWarrantyMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetWarrantyMaster",param);
            return DataSetToFill;
        }
       
        private WarrantyMaster objCreateWarrantyMaster(DataRow dr)
        {
            WarrantyMaster tWarrantyMaster = new WarrantyMaster();

            tWarrantyMaster.SetObjectInfo(dr);

            return tWarrantyMaster;

        }

        public ICollection<ErrorHandler> SaveWarrantyMaster(WarrantyMaster argWarrantyMaster, ICollection<WarrantyDetail> colGetWarrantyDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            string strretValue = "";
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsWarrantyMasterExists(argWarrantyMaster.WarrantyCode, argWarrantyMaster.ClientCode, da) == false)
                {
                    strretValue = InsertWarrantyMaster(argWarrantyMaster, da, lstErr);
                }
                else
                {
                    strretValue = UpdateWarrantyMaster(argWarrantyMaster, da, lstErr);
                }

                foreach (ErrorHandler objerr in lstErr)
                {
                    if (objerr.Type == "E")
                    {
                        da.ROLLBACK_TRANSACTION();
                        return lstErr;
                    }

                    if (objerr.Type == "A")
                    {
                        da.ROLLBACK_TRANSACTION();
                        return lstErr;
                    }
                }

                if (strretValue != "")
                {
                    if (colGetWarrantyDetail.Count > 0)
                    {
                        foreach (WarrantyDetail argWarrantyDetail in colGetWarrantyDetail)
                        {
                            argWarrantyDetail.WarrantyCode = Convert.ToString(strretValue);

                            if (argWarrantyDetail.IsDeleted == 0)
                            {
                                objWarrantyDetailManager.SaveWarrantyDetail(argWarrantyDetail, da, lstErr);
                            }
                            else
                            {
                                objWarrantyDetailManager.DeleteWarrantyDetail(argWarrantyDetail.WarrantyCode, argWarrantyDetail.ItemNo, argWarrantyDetail.ClientCode, argWarrantyDetail.IsDeleted);
                            }
                        }

                        foreach (ErrorHandler objerr in lstErr)
                        {
                            if (objerr.Type == "E")
                            {
                                da.ROLLBACK_TRANSACTION();
                                return lstErr;
                            }

                            if (objerr.Type == "A")
                            {
                                da.ROLLBACK_TRANSACTION();
                                return lstErr;
                            }
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

        public string InsertWarrantyMaster(WarrantyMaster argWarrantyMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@WarrantyCode", argWarrantyMaster.WarrantyCode);
            param[1] = new SqlParameter("@WarrantyName", argWarrantyMaster.WarrantyName);
            param[2] = new SqlParameter("@MatGroup1Code", argWarrantyMaster.MatGroup1Code);
            param[3] = new SqlParameter("@VendorWarTermsCode", argWarrantyMaster.VendorWarTermsCode);
            param[4] = new SqlParameter("@CustWarTermsCode", argWarrantyMaster.CustWarTermsCode);
            param[5] = new SqlParameter("@ValidFrom", argWarrantyMaster.ValidFrom);
            param[6] = new SqlParameter("@ValidTo", argWarrantyMaster.ValidTo);
            param[7] = new SqlParameter("@IsActive", argWarrantyMaster.IsActive);
            param[8] = new SqlParameter("@ClientCode", argWarrantyMaster.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argWarrantyMaster.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argWarrantyMaster.ModifiedBy);


            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertWarrantyMaster", param);


            string strMessage = Convert.ToString(param[12].Value);
            string strType = Convert.ToString(param[11].Value);
            string strRetValue = Convert.ToString(param[13].Value);


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
            return (strRetValue);

        }

        public string UpdateWarrantyMaster(WarrantyMaster argWarrantyMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];
            param[0] = new SqlParameter("@WarrantyCode", argWarrantyMaster.WarrantyCode);
            param[1] = new SqlParameter("@WarrantyName", argWarrantyMaster.WarrantyName);
            param[2] = new SqlParameter("@MatGroup1Code", argWarrantyMaster.MatGroup1Code);
            param[3] = new SqlParameter("@VendorWarTermsCode", argWarrantyMaster.VendorWarTermsCode);
            param[4] = new SqlParameter("@CustWarTermsCode", argWarrantyMaster.CustWarTermsCode);
            param[5] = new SqlParameter("@ValidFrom", argWarrantyMaster.ValidFrom);
            param[6] = new SqlParameter("@ValidTo", argWarrantyMaster.ValidTo);
            param[7] = new SqlParameter("@IsActive", argWarrantyMaster.IsActive);
            param[8] = new SqlParameter("@ClientCode", argWarrantyMaster.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argWarrantyMaster.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argWarrantyMaster.ModifiedBy);


            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateWarrantyMaster", param);


            string strMessage = Convert.ToString(param[12].Value);
            string strType = Convert.ToString(param[11].Value);
            string strRetValue = Convert.ToString(param[13].Value);


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
            return (strRetValue);

        }

        public ICollection<ErrorHandler> DeleteWarrantyMaster(string argWarrantyCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@WarrantyCode", argWarrantyCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteWarrantyMaster", param);


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

        public bool blnIsWarrantyMasterExists(string argWarrantyCode, string argClientCode)
        {
            bool IsWarrantyMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetWarrantyMaster(argWarrantyCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsWarrantyMasterExists = true;
            }
            else
            {
                IsWarrantyMasterExists = false;
            }
            return IsWarrantyMasterExists;
        }

        public bool blnIsWarrantyMasterExists(string argWarrantyCode, string argClientCode,DataAccess da)
        {
            bool IsWarrantyMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetWarrantyMaster(argWarrantyCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsWarrantyMasterExists = true;
            }
            else
            {
                IsWarrantyMasterExists = false;
            }
            return IsWarrantyMasterExists;
        }
    }
}