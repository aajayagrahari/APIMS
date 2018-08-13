
//Created On :: 14, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class SOType_SalesAreaManager
    {
        const string SOType_SalesAreaTable = "SOType_SalesArea";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public SOType_SalesArea objGetSOType_SalesArea(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argSOTypeCode, string argClientCode)
        {
            SOType_SalesArea argSOType_SalesArea = new SOType_SalesArea();
            DataSet DataSetToFill = new DataSet();

            if (argSalesOrganizationCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argDistChannelCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argDivisionCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argSOTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetSOType_SalesArea(argSalesOrganizationCode, argDistChannelCode, argDivisionCode, argSOTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSOType_SalesArea = this.objCreateSOType_SalesArea((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSOType_SalesArea;
        }


        public ICollection<SOType_SalesArea> colGetSOType_SalesArea(string argClientCode)
        {
            List<SOType_SalesArea> lst = new List<SOType_SalesArea>();
            DataSet DataSetToFill = new DataSet();
            SOType_SalesArea tSOType_SalesArea = new SOType_SalesArea();

            DataSetToFill = this.GetSOType_SalesArea(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSOType_SalesArea(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetSOType_SalesArea(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argSOTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[1] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[3] = new SqlParameter("@SOTypeCode", argSOTypeCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSOType_SalesArea4ID", param);

            return DataSetToFill;
        }


        public DataSet GetSOType_SalesArea(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetSOType_SalesArea", param);
            return DataSetToFill;
        }


        private SOType_SalesArea objCreateSOType_SalesArea(DataRow dr)
        {
            SOType_SalesArea tSOType_SalesArea = new SOType_SalesArea();

            tSOType_SalesArea.SetObjectInfo(dr);

            return tSOType_SalesArea;

        }


        public ICollection<ErrorHandler> SaveSOType_SalesArea(SOType_SalesArea argSOType_SalesArea)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsSOType_SalesAreaExists(argSOType_SalesArea.SalesOrganizationCode, argSOType_SalesArea.DistChannelCode, argSOType_SalesArea.DivisionCode, argSOType_SalesArea.SOTypeCode, argSOType_SalesArea.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertSOType_SalesArea(argSOType_SalesArea, da, lstErr);
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
                    UpdateSOType_SalesArea(argSOType_SalesArea, da, lstErr);
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


        public void InsertSOType_SalesArea(SOType_SalesArea argSOType_SalesArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSOType_SalesArea.SalesOrganizationCode);
            param[1] = new SqlParameter("@DistChannelCode", argSOType_SalesArea.DistChannelCode);
            param[2] = new SqlParameter("@DivisionCode", argSOType_SalesArea.DivisionCode);
            param[3] = new SqlParameter("@SOTypeCode", argSOType_SalesArea.SOTypeCode);
            param[4] = new SqlParameter("@ClientCode", argSOType_SalesArea.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argSOType_SalesArea.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argSOType_SalesArea.ModifiedBy);
            
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertSOType_SalesArea", param);


            string strMessage = Convert.ToString(param[8].Value);
            string strType = Convert.ToString(param[7].Value);
            string strRetValue = Convert.ToString(param[9].Value);


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


        public void UpdateSOType_SalesArea(SOType_SalesArea argSOType_SalesArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@SalesOrganizationCode", argSOType_SalesArea.SalesOrganizationCode);
            param[1] = new SqlParameter("@DistChannelCode", argSOType_SalesArea.DistChannelCode);
            param[2] = new SqlParameter("@DivisionCode", argSOType_SalesArea.DivisionCode);
            param[3] = new SqlParameter("@SOTypeCode", argSOType_SalesArea.SOTypeCode);
            param[4] = new SqlParameter("@ClientCode", argSOType_SalesArea.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argSOType_SalesArea.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argSOType_SalesArea.ModifiedBy);
            
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSOType_SalesArea", param);


            string strMessage = Convert.ToString(param[8].Value);
            string strType = Convert.ToString(param[7].Value);
            string strRetValue = Convert.ToString(param[9].Value);


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


        public ICollection<ErrorHandler> DeleteSOType_SalesArea(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argSOTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
                param[1] = new SqlParameter("@DistChannelCode", argDistChannelCode);
                param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
                param[3] = new SqlParameter("@SOTypeCode", argSOTypeCode);
                param[4] = new SqlParameter("@ClientCode", argClientCode);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;
                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;
                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteSOType_SalesArea", param);


                string strMessage = Convert.ToString(param[6].Value);
                string strType = Convert.ToString(param[5].Value);
                string strRetValue = Convert.ToString(param[7].Value);


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


        public bool blnIsSOType_SalesAreaExists(string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode, string argSOTypeCode, string argClientCode)
        {
            bool IsSOType_SalesAreaExists = false;
            DataSet ds = new DataSet();
            ds = GetSOType_SalesArea(argSalesOrganizationCode, argDistChannelCode, argDivisionCode, argSOTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSOType_SalesAreaExists = true;
            }
            else
            {
                IsSOType_SalesAreaExists = false;
            }
            return IsSOType_SalesAreaExists;
        }
    }
}