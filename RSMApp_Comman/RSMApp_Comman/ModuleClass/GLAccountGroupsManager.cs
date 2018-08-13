
//Created On :: 07, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using Telerik.Web.UI;

namespace RSMApp_Comman
{
    public class GLAccountGroupsManager
    {
        const string GLAccountGroupsTable = "GLAccountGroups";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public GLAccountGroups objGetGLAccountGroups(string argChartACCode, string argActGroup)
        {
            GLAccountGroups argGLAccountGroups = new GLAccountGroups();
            DataSet DataSetToFill = new DataSet();

            if (argChartACCode.Trim() == "")
            {
                goto ErrorHandler;
            }

            if (argActGroup.Trim() == "")
            {
                goto ErrorHandler;
            }

            DataSetToFill = this.GetGLAccountGroups(argChartACCode, argActGroup);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argGLAccountGroups = this.objCreateGLAccountGroups((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandler:

        Finish:
            DataSetToFill = null;


            return argGLAccountGroups;
        }


        public ICollection<GLAccountGroups> colGetGLAccountGroups()
        {
            List<GLAccountGroups> lst = new List<GLAccountGroups>();
            DataSet DataSetToFill = new DataSet();
            GLAccountGroups tGLAccountGroups = new GLAccountGroups();

            DataSetToFill = this.GetGLAccountGroups();

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateGLAccountGroups(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public void FillCombobox(Telerik.Web.UI.RadComboBox RCBGLAccountGroup, string argChartACCode,string argClientCode, int iIsDeleted)
        {
            RCBGLAccountGroup.Items.Clear();
            foreach (DataRow dr in GetGLAccountGroups(argChartACCode,iIsDeleted,argClientCode).Tables[0].Rows)
            {
                String itemText = dr["ActGroup"].ToString() + " " + dr["GroupName"].ToString();
                var itemCollection = new RadComboBoxItem
                {
                    Text = itemText,
                    Value = dr["ActGroup"].ToString()
                };

                itemCollection.Attributes.Add("ActGroup", dr["ActGroup"].ToString());
                itemCollection.Attributes.Add("GroupName", dr["GroupName"].ToString());

                RCBGLAccountGroup.Items.Add(itemCollection);
                itemCollection.DataBind();
            }
        }


        public DataSet GetGLAccountGroups(string argChartACCode, string argActGroup)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ChartACCode", argChartACCode);
            param[1] = new SqlParameter("@ActGroup", argActGroup);

            DataSetToFill = da.FillDataSet("SP_GetGLAccountGroups4ID", param);

            return DataSetToFill;
        }


        public DataSet GetGLAccountGroups(string argChartACCode, int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + GLAccountGroupsTable.ToString();

                if (iIsDeleted >= 0)
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " IsDeleted = " + iIsDeleted;
                }
                if (argChartACCode != "")
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " ChartACCode = '" + argChartACCode + "'";
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


        public DataSet GetGLAccountGroups()
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            DataSetToFill = da.FillDataSet("SP_GetGLAccountGroups");
            return DataSetToFill;
        }


        private GLAccountGroups objCreateGLAccountGroups(DataRow dr)
        {
            GLAccountGroups tGLAccountGroups = new GLAccountGroups();

            tGLAccountGroups.SetObjectInfo(dr);

            return tGLAccountGroups;

        }


        public ICollection<ErrorHandler> SaveGLAccountGroups(GLAccountGroups argGLAccountGroups)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsGLAccountGroupsExists(argGLAccountGroups.ChartACCode, argGLAccountGroups.ActGroup) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertGLAccountGroups(argGLAccountGroups, da, lstErr);
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
                    UpdateGLAccountGroups(argGLAccountGroups, da, lstErr);
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


        public void InsertGLAccountGroups(GLAccountGroups argGLAccountGroups, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@ChartACCode", argGLAccountGroups.ChartACCode);
            param[1] = new SqlParameter("@ActGroup", argGLAccountGroups.ActGroup);
            param[2] = new SqlParameter("@GroupName", argGLAccountGroups.GroupName);
            param[3] = new SqlParameter("@FromSRNO", argGLAccountGroups.FromSRNO);
            param[4] = new SqlParameter("@ToSRNO", argGLAccountGroups.ToSRNO);
            param[5] = new SqlParameter("@ClientCode", argGLAccountGroups.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argGLAccountGroups.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argGLAccountGroups.ModifiedBy);
            

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertGLAccountGroups", param);


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


        public void UpdateGLAccountGroups(GLAccountGroups argGLAccountGroups, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@ChartACCode", argGLAccountGroups.ChartACCode);
            param[1] = new SqlParameter("@ActGroup", argGLAccountGroups.ActGroup);
            param[2] = new SqlParameter("@GroupName", argGLAccountGroups.GroupName);
            param[3] = new SqlParameter("@FromSRNO", argGLAccountGroups.FromSRNO);
            param[4] = new SqlParameter("@ToSRNO", argGLAccountGroups.ToSRNO);
            param[5] = new SqlParameter("@ClientCode", argGLAccountGroups.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argGLAccountGroups.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argGLAccountGroups.ModifiedBy);
            

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateGLAccountGroups", param);


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


        public ICollection<ErrorHandler> DeleteGLAccountGroups(string argChartACCode, string argActGroup, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@ChartACCode", argChartACCode);
                param[1] = new SqlParameter("@ActGroup", argActGroup);
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

                int i = da.ExecuteNonQuery("Proc_DeleteGLAccountGroups", param);


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


        public bool blnIsGLAccountGroupsExists(string argChartACCode, string argActGroup)
        {
            bool IsGLAccountGroupsExists = false;
            DataSet ds = new DataSet();
            ds = GetGLAccountGroups(argChartACCode, argActGroup);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsGLAccountGroupsExists = true;
            }
            else
            {
                IsGLAccountGroupsExists = false;
            }
            return IsGLAccountGroupsExists;
        }
    }
}