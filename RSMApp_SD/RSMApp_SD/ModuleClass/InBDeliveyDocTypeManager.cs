
//Created On :: 17, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class InBDeliveyDocTypeManager
    {
        const string InBDeliveyDocTypeTable = "InBDeliveyDocType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public InBDeliveyDocType objGetInBDeliveyDocType(string argInBDeliveryDocTypeCode, string argClientCode)
        {
            InBDeliveyDocType argInBDeliveyDocType = new InBDeliveyDocType();
            DataSet DataSetToFill = new DataSet();

            if (argInBDeliveryDocTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetInBDeliveyDocType(argInBDeliveryDocTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argInBDeliveyDocType = this.objCreateInBDeliveyDocType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argInBDeliveyDocType;
        }

        public ICollection<InBDeliveyDocType> colGetInBDeliveyDocType(string argClientCode)
        {
            List<InBDeliveyDocType> lst = new List<InBDeliveyDocType>();
            DataSet DataSetToFill = new DataSet();
            InBDeliveyDocType tInBDeliveyDocType = new InBDeliveyDocType();

            DataSetToFill = this.GetInBDeliveyDocType(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateInBDeliveyDocType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetInBDeliveyDocType(string argInBDeliveryDocTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@InBDeliveryDocTypeCode", argInBDeliveryDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetInBDeliveyDocType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetInBDeliveyDocType(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + InBDeliveyDocTypeTable.ToString();

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

        public DataSet GetInBDeliveyDocType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetInBDeliveyDocType",param);

            return DataSetToFill;
        }

        private InBDeliveyDocType objCreateInBDeliveyDocType(DataRow dr)
        {
            InBDeliveyDocType tInBDeliveyDocType = new InBDeliveyDocType();

            tInBDeliveyDocType.SetObjectInfo(dr);

            return tInBDeliveyDocType;

        }

        public ICollection<ErrorHandler> SaveInBDeliveyDocType(InBDeliveyDocType argInBDeliveyDocType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsInBDeliveyDocTypeExists(argInBDeliveyDocType.InBDeliveryDocTypeCode, argInBDeliveyDocType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertInBDeliveyDocType(argInBDeliveyDocType, da, lstErr);
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
                    UpdateInBDeliveyDocType(argInBDeliveyDocType, da, lstErr);
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

        public void InsertInBDeliveyDocType(InBDeliveyDocType argInBDeliveyDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@InBDeliveryDocTypeCode", argInBDeliveyDocType.InBDeliveryDocTypeCode);
            param[1] = new SqlParameter("@InBDeliveryTypeDesc", argInBDeliveyDocType.InBDeliveryTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argInBDeliveyDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argInBDeliveyDocType.NumRange);
            param[4] = new SqlParameter("@SaveMode", argInBDeliveyDocType.SaveMode);
            param[5] = new SqlParameter("@ClientCode", argInBDeliveyDocType.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argInBDeliveyDocType.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argInBDeliveyDocType.ModifiedBy);
 

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertInBDeliveyDocType", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public void UpdateInBDeliveyDocType(InBDeliveyDocType argInBDeliveyDocType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@InBDeliveryDocTypeCode", argInBDeliveyDocType.InBDeliveryDocTypeCode);
            param[1] = new SqlParameter("@InBDeliveryTypeDesc", argInBDeliveyDocType.InBDeliveryTypeDesc);
            param[2] = new SqlParameter("@ItemNoIncr", argInBDeliveyDocType.ItemNoIncr);
            param[3] = new SqlParameter("@NumRange", argInBDeliveyDocType.NumRange);
            param[4] = new SqlParameter("@SaveMode", argInBDeliveyDocType.SaveMode);
            param[5] = new SqlParameter("@ClientCode", argInBDeliveyDocType.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argInBDeliveyDocType.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argInBDeliveyDocType.ModifiedBy);


            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateInBDeliveyDocType", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public ICollection<ErrorHandler> DeleteInBDeliveyDocType(string argInBDeliveryDocTypeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@InBDeliveryDocTypeCode", argInBDeliveryDocTypeCode);
                param[1] = new SqlParameter("@IsDeleted", iIsDeleted);
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

                int i = da.ExecuteNonQuery("Proc_DeleteInBDeliveyDocType", param);


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

        public bool blnIsInBDeliveyDocTypeExists(string argInBDeliveryDocTypeCode, string argClientCode)
        {
            bool IsInBDeliveyDocTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetInBDeliveyDocType(argInBDeliveryDocTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsInBDeliveyDocTypeExists = true;
            }
            else
            {
                IsInBDeliveyDocTypeExists = false;
            }
            return IsInBDeliveyDocTypeExists;
        }
    }
}