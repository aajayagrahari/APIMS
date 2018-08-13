
//Created On :: 15, May, 2012
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
    public class VendorAccTypeManager
    {
        const string VendorAccTypeTable = "VendorAccType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public VendorAccType objGetVendorAccType(string argVendorAccTypeCode)
        {
            VendorAccType argVendorAccType = new VendorAccType();
            DataSet DataSetToFill = new DataSet();

            if (argVendorAccTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetVendorAccType(argVendorAccTypeCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argVendorAccType = this.objCreateVendorAccType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argVendorAccType;
        }

        public ICollection<VendorAccType> colGetVendorAccType(string argClientCode)
        {
            List<VendorAccType> lst = new List<VendorAccType>();
            DataSet DataSetToFill = new DataSet();
            VendorAccType tVendorAccType = new VendorAccType();

            DataSetToFill = this.GetVendorAccType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateVendorAccType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBVendorAccType, string argClientCode, int iIsDeleted)
        {
            RCBVendorAccType.Items.Clear();
            foreach (DataRow dr in GetVendorAccType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["VendorAccTypeCode"].ToString() + " " + dr["VendorAccTypeDesc"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["VendorAccTypeCode"].ToString()
                };

                itemCollection.Attributes.Add("VendorAccTypeCode", dr["VendorAccTypeCode"].ToString());
                itemCollection.Attributes.Add("VendorAccTypeDesc", dr["VendorAccTypeDesc"].ToString());

                RCBVendorAccType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public DataSet GetVendorAccType(string argVendorAccTypeCode,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@VendorAccTypeCode", argVendorAccTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetVendorAccType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetVendorAccType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetVendorAccType",param);
            return DataSetToFill;
        }

        private VendorAccType objCreateVendorAccType(DataRow dr)
        {
            VendorAccType tVendorAccType = new VendorAccType();

            tVendorAccType.SetObjectInfo(dr);

            return tVendorAccType;

        }

        public DataSet GetVendorAccType(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + VendorAccTypeTable.ToString();

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

        public ICollection<ErrorHandler> SaveVendorAccType(VendorAccType argVendorAccType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsVendorAccTypeExists(argVendorAccType.VendorAccTypeCode, argVendorAccType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertVendorAccType(argVendorAccType, da, lstErr);
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
                    UpdateVendorAccType(argVendorAccType, da, lstErr);
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

        public void InsertVendorAccType(VendorAccType argVendorAccType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@VendorAccTypeCode", argVendorAccType.VendorAccTypeCode);
            param[1] = new SqlParameter("@VendorAccTypeDesc", argVendorAccType.VendorAccTypeDesc);
            param[2] = new SqlParameter("@NumRange", argVendorAccType.NumRange);
            param[3] = new SqlParameter("@RangeFrom", argVendorAccType.RangeFrom);
            param[4] = new SqlParameter("@RangeTo", argVendorAccType.RangeTo);
            param[5] = new SqlParameter("@ClientCode", argVendorAccType.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argVendorAccType.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argVendorAccType.ModifiedBy);
            
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertVendorAccType", param);


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

        }

        public void UpdateVendorAccType(VendorAccType argVendorAccType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@VendorAccTypeCode", argVendorAccType.VendorAccTypeCode);
            param[1] = new SqlParameter("@VendorAccTypeDesc", argVendorAccType.VendorAccTypeDesc);
            param[2] = new SqlParameter("@NumRange", argVendorAccType.NumRange);
            param[3] = new SqlParameter("@RangeFrom", argVendorAccType.RangeFrom);
            param[4] = new SqlParameter("@RangeTo", argVendorAccType.RangeTo);
            param[5] = new SqlParameter("@ClientCode", argVendorAccType.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argVendorAccType.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argVendorAccType.ModifiedBy);
            
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateVendorAccType", param);


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

        }

        public ICollection<ErrorHandler> DeleteVendorAccType(string argVendorAccTypeCode,string argClientCode,int IisDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@VendorAccTypeCode", argVendorAccTypeCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted", IisDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteVendorAccType", param);


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

        public bool blnIsVendorAccTypeExists(string argVendorAccTypeCode,string argClientCode)
        {
            bool IsVendorAccTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetVendorAccType(argVendorAccTypeCode,argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsVendorAccTypeExists = true;
            }
            else
            {
                IsVendorAccTypeExists = false;
            }
            return IsVendorAccTypeExists;
        }
    }
}