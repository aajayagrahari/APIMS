
//Created On :: 14, May, 2012
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
    public class SOTypeManager
    {
        const string SOTypeTable = "SOType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public SOType objGetSOType(string argSOTypeCode, string argClientCode)
        {
            SOType argSOType = new SOType();
            DataSet DataSetToFill = new DataSet();

            if (argSOTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetSOType(argSOTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSOType = this.objCreateSOType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSOType;
        }


        public ICollection<SOType> colGetSOType(string argClientCode)
        {
            List<SOType> lst = new List<SOType>();
            DataSet DataSetToFill = new DataSet();
            SOType tSOType = new SOType();

            DataSetToFill = this.GetSOType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSOType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBSOType, string argClientCode, int iIsDeleted)
        {
            RCBSOType.Items.Clear();
            foreach (DataRow dr in GetSOType(iIsDeleted, argClientCode).Tables[0].Rows)
            {
                String itemText = dr["SOTypeCode"].ToString() + " " + dr["SOCategoryCode"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["SOTypeCode"].ToString()
                };

                itemCollection.Attributes.Add("SOTypeCode", dr["SOTypeCode"].ToString());
                itemCollection.Attributes.Add("SOCategoryCode", dr["SOCategoryCode"].ToString());

                RCBSOType.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }

        public DataSet GetSOType(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + SOTypeTable.ToString();

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


        public DataSet GetSOType(string argSOTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSOType4ID", param);

            return DataSetToFill;
        }


        public DataSet GetSOType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSOType", param);
            return DataSetToFill;
        }


        private SOType objCreateSOType(DataRow dr)
        {
            SOType tSOType = new SOType();

            tSOType.SetObjectInfo(dr);

            return tSOType;

        }


        public ICollection<ErrorHandler> SaveSOType(SOType argSOType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsSOTypeExists(argSOType.SOTypeCode, argSOType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertSOType(argSOType, da, lstErr);
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
                    UpdateSOType(argSOType, da, lstErr);
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


        public void InsertSOType(SOType argSOType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@SOTypeCode", argSOType.SOTypeCode);
            param[1] = new SqlParameter("@SOCategoryCode", argSOType.SOCategoryCode);
            param[2] = new SqlParameter("@SOTypeDesc", argSOType.SOTypeDesc);
            param[3] = new SqlParameter("@NumRange", argSOType.NumRange);
            param[4] = new SqlParameter("@RangeFrom", argSOType.RangeFrom);
            param[5] = new SqlParameter("@RangeTo", argSOType.RangeTo);
            param[6] = new SqlParameter("@ItemCategoryCode", argSOType.ItemCategoryCode);
            param[7] = new SqlParameter("@CRLimitCheckType", argSOType.CRLimitCheckType);
            param[8] = new SqlParameter("@ItemNoIncr", argSOType.ItemNoIncr);
            param[9] = new SqlParameter("@ClientCode", argSOType.ClientCode);
            param[10] = new SqlParameter("@CreatedBy", argSOType.CreatedBy);
            param[11] = new SqlParameter("@ModifiedBy", argSOType.ModifiedBy);
            
            param[12] = new SqlParameter("@Type", SqlDbType.Char);
            param[12].Size = 1;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[13].Size = 255;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[14].Size = 20;
            param[14].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSOType", param);


            string strMessage = Convert.ToString(param[13].Value);
            string strType = Convert.ToString(param[12].Value);
            string strRetValue = Convert.ToString(param[14].Value);


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


        public void UpdateSOType(SOType argSOType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@SOTypeCode", argSOType.SOTypeCode);
            param[1] = new SqlParameter("@SOCategoryCode", argSOType.SOCategoryCode);
            param[2] = new SqlParameter("@SOTypeDesc", argSOType.SOTypeDesc);
            param[3] = new SqlParameter("@NumRange", argSOType.NumRange);
            param[4] = new SqlParameter("@RangeFrom", argSOType.RangeFrom);
            param[5] = new SqlParameter("@RangeTo", argSOType.RangeTo);
            param[6] = new SqlParameter("@ItemCategoryCode", argSOType.ItemCategoryCode);
            param[7] = new SqlParameter("@CRLimitCheckType", argSOType.CRLimitCheckType);
            param[8] = new SqlParameter("@ItemNoIncr", argSOType.ItemNoIncr);
            param[9] = new SqlParameter("@ClientCode", argSOType.ClientCode);
            param[10] = new SqlParameter("@CreatedBy", argSOType.CreatedBy);
            param[11] = new SqlParameter("@ModifiedBy", argSOType.ModifiedBy);
            
            param[12] = new SqlParameter("@Type", SqlDbType.Char);
            param[12].Size = 1;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[13].Size = 255;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[14].Size = 20;
            param[14].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSOType", param);


            string strMessage = Convert.ToString(param[13].Value);
            string strType = Convert.ToString(param[12].Value);
            string strRetValue = Convert.ToString(param[14].Value);


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


        public ICollection<ErrorHandler> DeleteSOType(string argSOTypeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@SOTypeCode", argSOTypeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteSOType", param);


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


        public bool blnIsSOTypeExists(string argSOTypeCode, string argClientCode)
        {
            bool IsSOTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetSOType(argSOTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSOTypeExists = true;
            }
            else
            {
                IsSOTypeExists = false;
            }
            return IsSOTypeExists;
        }
    }
}