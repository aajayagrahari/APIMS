
//Created On :: 12, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Authorization
{
    public class ProfileManager
    {
        const string ProfileTable = "Profile";

       //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        ProfileDetailManager objProfileDetailManager = new ProfileDetailManager();

        public Profile objGetProfile(string argProfileCode, string argClientCode)
        {
            Profile argProfile = new Profile();
            DataSet DataSetToFill = new DataSet();

            if (argProfileCode == null)
            {
                goto ErrorHandlers;
            }

            if (argProfileCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetProfile(argProfileCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argProfile = this.objCreateProfile((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argProfile;
        }
        
        public ICollection<Profile> colGetProfile(string argClientCode)
        {
            List<Profile> lst = new List<Profile>();
            DataSet DataSetToFill = new DataSet();
            Profile tProfile = new Profile();

            DataSetToFill = this.GetProfile(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateProfile(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetProfile(string argProfileCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ProfileCode", argProfileCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetProfile4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetProfile(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetProfile", param);
            return DataSetToFill;
        }

        public DataSet GetProfile(int iIsDeleted)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + ProfileTable.ToString();

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
        
        private Profile objCreateProfile(DataRow dr)
        {
            Profile tProfile = new Profile();

            tProfile.SetObjectInfo(dr);

            return tProfile;
        }

        public ICollection<ErrorHandler> SaveProfile(Profile argProfile, ICollection<ProfileDetail> colProfileDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            string strretValue = "";
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsProfileExists(argProfile.ProfileCode, argProfile.ClientCode) == false)
                {                   
                   strretValue =  InsertProfile(argProfile, da, lstErr);
                }
                else
                {
                    strretValue =  UpdateProfile(argProfile, da, lstErr);
                }

                foreach (ErrorHandler objerr in lstErr)
                {
                    if (objerr.Type == "E")
                    {
                        da.ROLLBACK_TRANSACTION();
                        return lstErr;
                    }
                }

                if (strretValue == argProfile.ProfileCode)
                {
                    if (colProfileDetail.Count > 0)
                    {
                        foreach (ProfileDetail argProfileDetail in colProfileDetail)
                        {
                            if (argProfileDetail.IsDeleted == 0)
                            {
                                objProfileDetailManager.SaveProfileDetail(argProfileDetail, da, lstErr);
                            }
                            else
                            {
                                objProfileDetailManager.DeleteProfileDetail(argProfileDetail.ProfileCode, argProfileDetail.ItemNo, argProfileDetail.ClientCode, da, lstErr);
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
        
        public ICollection<ErrorHandler> SaveProfile(Profile argProfile)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsProfileExists(argProfile.ProfileCode, argProfile.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertProfile(argProfile, da, lstErr);
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
                    UpdateProfile(argProfile, da, lstErr);
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

        public string InsertProfile(Profile argProfile, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@ProfileCode", argProfile.ProfileCode);
            param[1] = new SqlParameter("@ProfileDesc", argProfile.ProfileDesc);
            param[2] = new SqlParameter("@TranType", argProfile.TranType);
            param[3] = new SqlParameter("@Modules", argProfile.Modules);
            param[4] = new SqlParameter("@Activity", argProfile.Activity);
            param[5] = new SqlParameter("@ClientCode", argProfile.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argProfile.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argProfile.ModifiedBy);
            
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertProfile", param);


            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
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

            return strRetValue;
        }

        public string UpdateProfile(Profile argProfile, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@ProfileCode", argProfile.ProfileCode);
            param[1] = new SqlParameter("@ProfileDesc", argProfile.ProfileDesc);
            param[2] = new SqlParameter("@TranType", argProfile.TranType);
            param[3] = new SqlParameter("@Modules", argProfile.Modules);
            param[4] = new SqlParameter("@Activity", argProfile.Activity);
            param[5] = new SqlParameter("@ClientCode", argProfile.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argProfile.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argProfile.ModifiedBy);
            
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;
            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;
            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateProfile", param);


            string strMessage = Convert.ToString(param[9].Value);
            string strType = Convert.ToString(param[8].Value);
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

            return strRetValue;
        }

        public ICollection<ErrorHandler> DeleteProfile(string argProfileCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@ProfileCode", argProfileCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteProfile", param);


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

        public bool blnIsProfileExists(string argProfileCode, string argClientCode)
        {
            bool IsProfileExists = false;
            DataSet ds = new DataSet();
            ds = GetProfile(argProfileCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsProfileExists = true;
            }
            else
            {
                IsProfileExists = false;
            }
            return IsProfileExists;
        }





    }
}