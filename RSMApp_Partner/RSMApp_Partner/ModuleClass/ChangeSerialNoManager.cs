
//Created On :: 30, December, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class ChangeSerialNoManager
    {
        const string ChangeSerialNoTable = "ChangeSerialNo";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public ChangeSerialNo objGetChangeSerialNo(int argidChangeSerialNo, string argClientCode)
        {
            ChangeSerialNo argChangeSerialNo = new ChangeSerialNo();
            DataSet DataSetToFill = new DataSet();

            if (argidChangeSerialNo <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetChangeSerialNo(argidChangeSerialNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argChangeSerialNo = this.objCreateChangeSerialNo((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argChangeSerialNo;
        }


        public ICollection<ChangeSerialNo> colGetChangeSerialNo(string argPartnerCode, string argClientCode)
        {
            List<ChangeSerialNo> lst = new List<ChangeSerialNo>();
            DataSet DataSetToFill = new DataSet();
            ChangeSerialNo tChangeSerialNo = new ChangeSerialNo();

            DataSetToFill = this.GetChangeSerialNoList(argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateChangeSerialNo(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetChangeSerialNo(int argidChangeSerialNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@idChangeSerialNo", argidChangeSerialNo);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetChangeSerialNo4ID", param);

            return DataSetToFill;
        }


        public DataSet GetChangeSerialNoList(string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetChangeSerialNo",param);
            return DataSetToFill;
        }


        private ChangeSerialNo objCreateChangeSerialNo(DataRow dr)
        {
            ChangeSerialNo tChangeSerialNo = new ChangeSerialNo();

            tChangeSerialNo.SetObjectInfo(dr);

            return tChangeSerialNo;

        }


        public PartnerErrorResult SaveChangeSerialNo(ChangeSerialNo argChangeSerialNo)
        {
            PartnerErrorResult errorresult = new PartnerErrorResult();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsChangeSerialNoExists(argChangeSerialNo.idChangeSerialNo, argChangeSerialNo.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertChangeSerialNo(argChangeSerialNo, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            errorresult.colErrorHandler.Add(objerr);
                            da.ROLLBACK_TRANSACTION();
                            return errorresult;
                        }
                    }
                }
                else
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    UpdateChangeSerialNo(argChangeSerialNo, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            errorresult.colErrorHandler.Add(objerr);
                            da.ROLLBACK_TRANSACTION();
                            return errorresult;
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
                errorresult.colErrorHandler.Add(objErrorHandler);
            }
            finally
            {
                if (da != null)
                {
                    da.Close_Connection();
                    da = null;
                }
            }
            return errorresult;
        }



        public void InsertChangeSerialNo(ChangeSerialNo argChangeSerialNo, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@idChangeSerialNo", argChangeSerialNo.idChangeSerialNo);
            param[1] = new SqlParameter("@ChangeDate", argChangeSerialNo.ChangeDate);
            param[2] = new SqlParameter("@SerialNo", argChangeSerialNo.SerialNo);
            param[3] = new SqlParameter("@MaterialCode", argChangeSerialNo.MaterialCode);
            param[4] = new SqlParameter("@MatGroup1Code", argChangeSerialNo.MatGroup1Code);
            param[5] = new SqlParameter("@NewSerialNo", argChangeSerialNo.NewSerialNo);
            param[6] = new SqlParameter("@PartnerCode", argChangeSerialNo.PartnerCode);
            param[7] = new SqlParameter("@ClientCode", argChangeSerialNo.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argChangeSerialNo.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argChangeSerialNo.ModifiedBy);
           
            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertChangeSerialNo", param);


            string strMessage = Convert.ToString(param[11].Value);
            string strType = Convert.ToString(param[10].Value);
            string strRetValue = Convert.ToString(param[12].Value);


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


        public void UpdateChangeSerialNo(ChangeSerialNo argChangeSerialNo, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@idChangeSerialNo", argChangeSerialNo.idChangeSerialNo);
            param[1] = new SqlParameter("@ChangeDate", argChangeSerialNo.ChangeDate);
            param[2] = new SqlParameter("@SerialNo", argChangeSerialNo.SerialNo);
            param[3] = new SqlParameter("@MaterialCode", argChangeSerialNo.MaterialCode);
            param[4] = new SqlParameter("@MatGroup1Code", argChangeSerialNo.MatGroup1Code);
            param[5] = new SqlParameter("@NewSerialNo", argChangeSerialNo.NewSerialNo);
            param[6] = new SqlParameter("@PartnerCode", argChangeSerialNo.PartnerCode);
            param[7] = new SqlParameter("@ClientCode", argChangeSerialNo.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argChangeSerialNo.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argChangeSerialNo.ModifiedBy);

            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateChangeSerialNo", param);


            string strMessage = Convert.ToString(param[11].Value);
            string strType = Convert.ToString(param[10].Value);
            string strRetValue = Convert.ToString(param[12].Value);


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


        public ICollection<ErrorHandler> DeleteChangeSerialNo(int argidChangeSerialNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@idChangeSerialNo", argidChangeSerialNo);
                param[1] = new SqlParameter("@ClientCode", argClientCode);

                param[2] = new SqlParameter("@Type", SqlDbType.Char);
                param[2].Size = 1;
                param[2].Direction = ParameterDirection.Output;
                param[3] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[3].Size = 255;
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[4].Size = 20;
                param[4].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteChangeSerialNo", param);


                string strMessage = Convert.ToString(param[3].Value);
                string strType = Convert.ToString(param[2].Value);
                string strRetValue = Convert.ToString(param[4].Value);


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


        public bool blnIsChangeSerialNoExists(int argidChangeSerialNo, string argClientCode)
        {
            bool IsChangeSerialNoExists = false;
            DataSet ds = new DataSet();
            ds = GetChangeSerialNo(argidChangeSerialNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsChangeSerialNoExists = true;
            }
            else
            {
                IsChangeSerialNoExists = false;
            }
            return IsChangeSerialNoExists;
        }
    }
}