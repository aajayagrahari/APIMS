
//Created On :: 05, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_FI
{
    public class AccDeterminTableDetailManager
    {
        const string AccDeterminTableDetailTable = "AccDeterminTableDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public AccDeterminTableDetail objGetAccDeterminTableDetail(string argAccDMTableCode, string argFieldName, string argClientCode)
        {
            AccDeterminTableDetail argAccDeterminTableDetail = new AccDeterminTableDetail();
            DataSet DataSetToFill = new DataSet();

            if (argAccDMTableCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argFieldName.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetAccDeterminTableDetail(argAccDMTableCode, argFieldName, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argAccDeterminTableDetail = this.objCreateAccDeterminTableDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argAccDeterminTableDetail;
        }


        public ICollection<AccDeterminTableDetail> colGetAccDeterminTableDetail(string argAccDMTableCode, string argClientCode)
        {
            List<AccDeterminTableDetail> lst = new List<AccDeterminTableDetail>();
            DataSet DataSetToFill = new DataSet();
            AccDeterminTableDetail tAccDeterminTableDetail = new AccDeterminTableDetail();

            DataSetToFill = this.GetAccDeterminTableDetail(argAccDMTableCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateAccDeterminTableDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetAccDeterminTableDetail(string argAccDMTableCode, string argFieldName, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@AccDMTableCode", argAccDMTableCode);
            param[1] = new SqlParameter("@FieldName", argFieldName);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAccDeterminTableDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetAccDeterminTableDetail(string argAccDMTableCode, string argFieldName, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@AccDMTableCode", argAccDMTableCode);
            param[1] = new SqlParameter("@FieldName", argFieldName);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetAccDeterminTableDetail4ID", param);

            return DataSetToFill;
        }


        public DataSet GetAccDeterminTableDetail(string argAccDMTableCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AccDMTableCode", argAccDMTableCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAccDeterminTableDetail",param);
            return DataSetToFill;
        }


        private AccDeterminTableDetail objCreateAccDeterminTableDetail(DataRow dr)
        {
            AccDeterminTableDetail tAccDeterminTableDetail = new AccDeterminTableDetail();

            tAccDeterminTableDetail.SetObjectInfo(dr);

            return tAccDeterminTableDetail;

        }


        public void SaveAccDeterminTableDetail(AccDeterminTableDetail argAccDeterminTableDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
         
           try
            {
                if (blnIsAccDeterminTableDetailExists(argAccDeterminTableDetail.AccDMTableCode, argAccDeterminTableDetail.FieldName, argAccDeterminTableDetail.ClientCode,da) == false)
                {

                    InsertAccDeterminTableDetail(argAccDeterminTableDetail, da, lstErr);
                }
                else
                {
                    InsertAccDeterminTableDetail(argAccDeterminTableDetail, da, lstErr);
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
           
        }


        public void InsertAccDeterminTableDetail(AccDeterminTableDetail argAccDeterminTableDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@AccDMTableCode", argAccDeterminTableDetail.AccDMTableCode);
            param[1] = new SqlParameter("@FieldName", argAccDeterminTableDetail.FieldName);
            param[2] = new SqlParameter("@MasterTableName", argAccDeterminTableDetail.MasterTableName);
            param[3] = new SqlParameter("@MasterTableField", argAccDeterminTableDetail.MasterTableField);
            param[4] = new SqlParameter("@ClientCode", argAccDeterminTableDetail.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argAccDeterminTableDetail.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argAccDeterminTableDetail.ModifiedBy);

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertAccDeterminTableDetail", param);


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


        public void UpdateAccDeterminTableDetail(AccDeterminTableDetail argAccDeterminTableDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@AccDMTableCode", argAccDeterminTableDetail.AccDMTableCode);
            param[1] = new SqlParameter("@FieldName", argAccDeterminTableDetail.FieldName);
            param[2] = new SqlParameter("@MasterTableName", argAccDeterminTableDetail.MasterTableName);
            param[3] = new SqlParameter("@MasterTableField", argAccDeterminTableDetail.MasterTableField);
            param[4] = new SqlParameter("@ClientCode", argAccDeterminTableDetail.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argAccDeterminTableDetail.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argAccDeterminTableDetail.ModifiedBy);

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateAccDeterminTableDetail", param);


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


        public ICollection<ErrorHandler> DeleteAccDeterminTableDetail(string argAccDMTableCode, string argFieldName, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@AccDMTableCode", argAccDMTableCode);
                param[1] = new SqlParameter("@FieldName", argFieldName);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteAccDeterminTableDetail", param);


                string strMessage = Convert.ToString(param[5].Value);
                string strType = Convert.ToString(param[4].Value);
                string strRetValue = Convert.ToString(param[6].Value);


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

        public ICollection<ErrorHandler> DeleteAccDeterminTableDetail(string argAccDMTableCode, string argFieldName, string argClientCode, int iIsDeleted,DataAccess da)
        {
           
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@AccDMTableCode", argAccDMTableCode);
                param[1] = new SqlParameter("@FieldName", argFieldName);
                param[2] = new SqlParameter("@ClientCode", argClientCode);
                param[3] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;
                int i = da.NExecuteNonQuery("Proc_DeleteAccDeterminTableDetail", param);


                string strMessage = Convert.ToString(param[5].Value);
                string strType = Convert.ToString(param[4].Value);
                string strRetValue = Convert.ToString(param[6].Value);


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

        public bool blnIsAccDeterminTableDetailExists(string argAccDMTableCode, string argFieldName, string argClientCode)
        {
            bool IsAccDeterminTableDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetAccDeterminTableDetail(argAccDMTableCode, argFieldName, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAccDeterminTableDetailExists = true;
            }
            else
            {
                IsAccDeterminTableDetailExists = false;
            }
            return IsAccDeterminTableDetailExists;
        }
        public bool blnIsAccDeterminTableDetailExists(string argAccDMTableCode, string argFieldName, string argClientCode,DataAccess da)
        {
            bool IsAccDeterminTableDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetAccDeterminTableDetail(argAccDMTableCode, argFieldName, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAccDeterminTableDetailExists = true;
            }
            else
            {
                IsAccDeterminTableDetailExists = false;
            }
            return IsAccDeterminTableDetailExists;
        }

    }
}