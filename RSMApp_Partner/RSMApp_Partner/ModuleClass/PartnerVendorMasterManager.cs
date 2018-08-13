
//Created On :: 23, December, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class PartnerVendorMasterManager
    {
        const string PartnerVendorMasterTable = "PartnerVendorMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public PartnerVendorMaster objGetPartnerVendorMaster(string argVendorCode, string argPartnerCode, string argClientCode)
        {
            PartnerVendorMaster argPartnerVendorMaster = new PartnerVendorMaster();
            DataSet DataSetToFill = new DataSet();

            if (argVendorCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argPartnerCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            DataSetToFill = this.GetPartnerVendorMaster(argVendorCode, argPartnerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartnerVendorMaster = this.objCreatePartnerVendorMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandler:

        Finish:
            DataSetToFill = null;


            return argPartnerVendorMaster;
        }


        public ICollection<PartnerVendorMaster> colGetPartnerVendorMaster(string argPartnerCode, string argClientCode)
        {
            List<PartnerVendorMaster> lst = new List<PartnerVendorMaster>();
            DataSet DataSetToFill = new DataSet();
            PartnerVendorMaster tPartnerVendorMaster = new PartnerVendorMaster();

            DataSetToFill = this.GetPartnerVendorMaster(argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePartnerVendorMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetPartnerVendorMaster(string argVendorCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@VendorCode", argVendorCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerVendorMaster4ID", param);

            return DataSetToFill;
        }


        public DataSet GetPartnerVendorMaster(string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerVendorMaster", param);
            return DataSetToFill;
        }


        private PartnerVendorMaster objCreatePartnerVendorMaster(DataRow dr)
        {
            PartnerVendorMaster tPartnerVendorMaster = new PartnerVendorMaster();

            tPartnerVendorMaster.SetObjectInfo(dr);

            return tPartnerVendorMaster;

        }


        public ICollection<ErrorHandler> SavePartnerVendorMaster(PartnerVendorMaster argPartnerVendorMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartnerVendorMasterExists(argPartnerVendorMaster.VendorCode, argPartnerVendorMaster.PartnerCode, argPartnerVendorMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartnerVendorMaster(argPartnerVendorMaster, da, lstErr);
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
                    UpdatePartnerVendorMaster(argPartnerVendorMaster, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
                        }
                    }
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


        public void InsertPartnerVendorMaster(PartnerVendorMaster argPartnerVendorMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[24];
            param[0] = new SqlParameter("@VendorCode", argPartnerVendorMaster.VendorCode);
            param[1] = new SqlParameter("@VendorTypeCode", argPartnerVendorMaster.VendorTypeCode);
            param[2] = new SqlParameter("@VendorName", argPartnerVendorMaster.VendorName);
            param[3] = new SqlParameter("@Address1", argPartnerVendorMaster.Address1);
            param[4] = new SqlParameter("@Address2", argPartnerVendorMaster.Address2);
            param[5] = new SqlParameter("@CountryCode", argPartnerVendorMaster.CountryCode);
            param[6] = new SqlParameter("@StateCode", argPartnerVendorMaster.StateCode);
            param[7] = new SqlParameter("@City", argPartnerVendorMaster.City);
            param[8] = new SqlParameter("@PinCode", argPartnerVendorMaster.PinCode);
            param[9] = new SqlParameter("@TelNo", argPartnerVendorMaster.TelNo);
            param[10] = new SqlParameter("@FaxNo", argPartnerVendorMaster.FaxNo);
            param[11] = new SqlParameter("@MobileNo", argPartnerVendorMaster.MobileNo);
            param[12] = new SqlParameter("@EmailID", argPartnerVendorMaster.EmailID);
            param[13] = new SqlParameter("@ContactPerson", argPartnerVendorMaster.ContactPerson);
            param[14] = new SqlParameter("@PartnerCode", argPartnerVendorMaster.PartnerCode);
            param[15] = new SqlParameter("@ClientCode", argPartnerVendorMaster.ClientCode);
            param[16] = new SqlParameter("@CreatedBy", argPartnerVendorMaster.CreatedBy);
            param[17] = new SqlParameter("@ModifiedBy", argPartnerVendorMaster.ModifiedBy);
            param[18] = new SqlParameter("@CreatedDate", argPartnerVendorMaster.CreatedDate);
            param[19] = new SqlParameter("@ModifiedDate", argPartnerVendorMaster.ModifiedDate);
            param[20] = new SqlParameter("@IsDeleted", argPartnerVendorMaster.IsDeleted);

            param[21] = new SqlParameter("@Type", SqlDbType.Char);
            param[21].Size = 1;
            param[21].Direction = ParameterDirection.Output;
            param[22] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[22].Size = 255;
            param[22].Direction = ParameterDirection.Output;
            param[23] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[23].Size = 20;
            param[23].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertPartnerVendorMaster", param);


            string strMessage = Convert.ToString(param[22].Value);
            string strType = Convert.ToString(param[21].Value);
            string strRetValue = Convert.ToString(param[23].Value);


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


        public void UpdatePartnerVendorMaster(PartnerVendorMaster argPartnerVendorMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[24];
            param[0] = new SqlParameter("@VendorCode", argPartnerVendorMaster.VendorCode);
            param[1] = new SqlParameter("@VendorTypeCode", argPartnerVendorMaster.VendorTypeCode);
            param[2] = new SqlParameter("@VendorName", argPartnerVendorMaster.VendorName);
            param[3] = new SqlParameter("@Address1", argPartnerVendorMaster.Address1);
            param[4] = new SqlParameter("@Address2", argPartnerVendorMaster.Address2);
            param[5] = new SqlParameter("@CountryCode", argPartnerVendorMaster.CountryCode);
            param[6] = new SqlParameter("@StateCode", argPartnerVendorMaster.StateCode);
            param[7] = new SqlParameter("@City", argPartnerVendorMaster.City);
            param[8] = new SqlParameter("@PinCode", argPartnerVendorMaster.PinCode);
            param[9] = new SqlParameter("@TelNo", argPartnerVendorMaster.TelNo);
            param[10] = new SqlParameter("@FaxNo", argPartnerVendorMaster.FaxNo);
            param[11] = new SqlParameter("@MobileNo", argPartnerVendorMaster.MobileNo);
            param[12] = new SqlParameter("@EmailID", argPartnerVendorMaster.EmailID);
            param[13] = new SqlParameter("@ContactPerson", argPartnerVendorMaster.ContactPerson);
            param[14] = new SqlParameter("@PartnerCode", argPartnerVendorMaster.PartnerCode);
            param[15] = new SqlParameter("@ClientCode", argPartnerVendorMaster.ClientCode);
            param[16] = new SqlParameter("@CreatedBy", argPartnerVendorMaster.CreatedBy);
            param[17] = new SqlParameter("@ModifiedBy", argPartnerVendorMaster.ModifiedBy);
            param[18] = new SqlParameter("@CreatedDate", argPartnerVendorMaster.CreatedDate);
            param[19] = new SqlParameter("@ModifiedDate", argPartnerVendorMaster.ModifiedDate);
            param[20] = new SqlParameter("@IsDeleted", argPartnerVendorMaster.IsDeleted);

            param[21] = new SqlParameter("@Type", SqlDbType.Char);
            param[21].Size = 1;
            param[21].Direction = ParameterDirection.Output;
            param[22] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[22].Size = 255;
            param[22].Direction = ParameterDirection.Output;
            param[23] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[23].Size = 20;
            param[23].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdatePartnerVendorMaster", param);


            string strMessage = Convert.ToString(param[22].Value);
            string strType = Convert.ToString(param[21].Value);
            string strRetValue = Convert.ToString(param[23].Value);


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


        public ICollection<ErrorHandler> DeletePartnerVendorMaster(string argVendorCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@VendorCode", argVendorCode);
                param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
                param[2] = new SqlParameter("@ClientCode", argClientCode);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;
                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeletePartnerVendorMaster", param);


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


        public bool blnIsPartnerVendorMasterExists(string argVendorCode, string argPartnerCode, string argClientCode)
        {
            bool IsPartnerVendorMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerVendorMaster(argVendorCode, argPartnerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerVendorMasterExists = true;
            }
            else
            {
                IsPartnerVendorMasterExists = false;
            }
            return IsPartnerVendorMasterExists;
        }
    }
}