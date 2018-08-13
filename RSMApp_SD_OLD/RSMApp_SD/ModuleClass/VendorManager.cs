
//Created On :: 18, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using Telerik.Web.UI;

namespace RSMApp_SD
{
    public class VendorManager
    {
        const string VendorTable = "Vendor";

        ///GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Vendor objGetVendor(string argVendorCode, string argClientCode)
        {
            Vendor argVendor = new Vendor();
            DataSet DataSetToFill = new DataSet();

            if (argVendorCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetVendor(argVendorCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argVendor = this.objCreateVendor((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argVendor;
        }

        public ICollection<Vendor> colGetVendor(string argClientCode)
        {
            List<Vendor> lst = new List<Vendor>();
            DataSet DataSetToFill = new DataSet();
            Vendor tVendor = new Vendor();

            DataSetToFill = this.GetVendor(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateVendor(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBVendor, string argClientCode, int iIsDeleted)
        {
            RCBVendor.Items.Clear();
            foreach (DataRow dr in GetVendor(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["VendorCode"].ToString() + " " + dr["VendorName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["VendorCode"].ToString()
                };

                itemCollection.Attributes.Add("VendorCode", dr["VendorCode"].ToString());
                itemCollection.Attributes.Add("VendorName", dr["VendorName"].ToString());
                

                RCBVendor.Items.Add(itemCollection);
                itemCollection.DataBind();
            }

        }

        public DataSet GetVendor(string argVendorCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@VendorCode", argVendorCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetVendor4ID", param);

            return DataSetToFill;
        }

        public DataSet GetVendor(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetVendor", param);
            return DataSetToFill;
        }

        public DataSet GetVendor(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT VendorCode,(Name1 + + Name2) as VendorName FROM " + VendorTable.ToString();

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

        private Vendor objCreateVendor(DataRow dr)
        {
            Vendor tVendor = new Vendor();

            tVendor.SetObjectInfo(dr);

            return tVendor;

        }
        
        public ICollection<ErrorHandler> SaveVendor(Vendor argVendor)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsVendorExists(argVendor.VendorCode, argVendor.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertVendor(argVendor, da, lstErr);
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
                    UpdateVendor(argVendor, da, lstErr);
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

        public void InsertVendor(Vendor argVendor, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@VendorCode", argVendor.VendorCode);
            param[1] = new SqlParameter("@Name1", argVendor.Name1);
            param[2] = new SqlParameter("@Name2", argVendor.Name2);
            param[3] = new SqlParameter("@VendorAccTypeCode", argVendor.VendorAccTypeCode);
            param[4] = new SqlParameter("@Address1", argVendor.Address1);
            param[5] = new SqlParameter("@Address2", argVendor.Address2);
            param[6] = new SqlParameter("@CountryCode", argVendor.CountryCode);
            param[7] = new SqlParameter("@StateCode", argVendor.StateCode);
            param[8] = new SqlParameter("@City", argVendor.City);
            param[9] = new SqlParameter("@PinCode", argVendor.PinCode);
            param[10] = new SqlParameter("@TelNo", argVendor.TelNo);
            param[11] = new SqlParameter("@FaxNo", argVendor.FaxNo);
            param[12] = new SqlParameter("@Title", argVendor.Title);
            param[13] = new SqlParameter("@ContactPerson", argVendor.ContactPerson);
            param[14] = new SqlParameter("@PANNo", argVendor.PANNo);
            param[15] = new SqlParameter("@TINNo", argVendor.TINNo);
            param[16] = new SqlParameter("@CurrencyCode",argVendor.CurrencyCode);
            param[17] = new SqlParameter("@ClientCode", argVendor.ClientCode);
            param[18] = new SqlParameter("@CreatedBy", argVendor.CreatedBy);
            param[19] = new SqlParameter("@ModifiedBy", argVendor.ModifiedBy);

            param[20] = new SqlParameter("@Type", SqlDbType.Char);
            param[20].Size = 1;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[21].Size = 255;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[22].Size = 20;
            param[22].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertVendor", param);

            string strMessage = Convert.ToString(param[21].Value);
            string strType = Convert.ToString(param[20].Value);
            string strRetValue = Convert.ToString(param[22].Value);
            
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

        public void UpdateVendor(Vendor argVendor, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@VendorCode", argVendor.VendorCode);
            param[1] = new SqlParameter("@Name1", argVendor.Name1);
            param[2] = new SqlParameter("@Name2", argVendor.Name2);
            param[3] = new SqlParameter("@VendorAccTypeCode", argVendor.VendorAccTypeCode);
            param[4] = new SqlParameter("@Address1", argVendor.Address1);
            param[5] = new SqlParameter("@Address2", argVendor.Address2);
            param[6] = new SqlParameter("@CountryCode", argVendor.CountryCode);
            param[7] = new SqlParameter("@StateCode", argVendor.StateCode);
            param[8] = new SqlParameter("@City", argVendor.City);
            param[9] = new SqlParameter("@PinCode", argVendor.PinCode);
            param[10] = new SqlParameter("@TelNo", argVendor.TelNo);
            param[11] = new SqlParameter("@FaxNo", argVendor.FaxNo);
            param[12] = new SqlParameter("@Title", argVendor.Title);
            param[13] = new SqlParameter("@ContactPerson", argVendor.ContactPerson);
            param[14] = new SqlParameter("@PANNo", argVendor.PANNo);
            param[15] = new SqlParameter("@TINNo", argVendor.TINNo);
            param[16] = new SqlParameter("@CurrencyCode",argVendor.CurrencyCode);
            param[17] = new SqlParameter("@ClientCode", argVendor.ClientCode);
            param[18] = new SqlParameter("@CreatedBy", argVendor.CreatedBy);
            param[19] = new SqlParameter("@ModifiedBy", argVendor.ModifiedBy);

            param[20] = new SqlParameter("@Type", SqlDbType.Char);
            param[20].Size = 1;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[21].Size = 255;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[22].Size = 20;
            param[22].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateVendor", param);


            string strMessage = Convert.ToString(param[21].Value);
            string strType = Convert.ToString(param[20].Value);
            string strRetValue = Convert.ToString(param[22].Value);


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

        public ICollection<ErrorHandler> DeleteVendor(string argVendorCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@VendorCode", argVendorCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteVendor", param);

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

        public bool blnIsVendorExists(string argVendorCode, string argClientCode)
        {
            bool IsVendorExists = false;
            DataSet ds = new DataSet();
            ds = GetVendor(argVendorCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsVendorExists = true;
            }
            else
            {
                IsVendorExists = false;
            }
            return IsVendorExists;
        }
    }
}