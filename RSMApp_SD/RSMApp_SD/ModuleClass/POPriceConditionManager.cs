
//Created On :: 16, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class POPriceConditionManager
    {
        const string POPriceConditionTable = "POPriceCondition";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();


        public POPriceCondition objGetPOPriceCondition(string argPODocCode, string argItemNo, string argConditionType, string argClientCode)
        {
            POPriceCondition argPOPriceCondition = new POPriceCondition();
            DataSet DataSetToFill = new DataSet();

            if (argPODocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argItemNo.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argConditionType.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPOPriceCondition(argPODocCode, argItemNo, argConditionType, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPOPriceCondition = this.objCreatePOPriceCondition((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPOPriceCondition;
        }


        public ICollection<POPriceCondition> colGetPOPriceCondition(string argPODocCode, string argClientCode, List<POPriceCondition> lst)
        {
           
            DataSet DataSetToFill = new DataSet();
            POPriceCondition tPOPriceCondition = new POPriceCondition();

            DataSetToFill = this.GetPOPriceCondition(argPODocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePOPriceCondition(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


        return lst;
        }


        public DataSet GetPOPriceCondition(string argPODocCode, string argItemNo, string argConditionType, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@PODocCode", argPODocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ConditionType", argConditionType);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPOPriceCondition4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPOPriceCondition(string argPODocCode, string argItemNo, string argConditionType, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@PODocCode", argPODocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ConditionType", argConditionType);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPOPriceCondition4ID", param);

            return DataSetToFill;
        }


        public DataSet GetPOPriceCondition(string argPODocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
      
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PODocCode", argPODocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPOPriceCondition",param);
            return DataSetToFill;
        }


        private POPriceCondition objCreatePOPriceCondition(DataRow dr)
        {
            POPriceCondition tPOPriceCondition = new POPriceCondition();

            tPOPriceCondition.SetObjectInfo(dr);

            return tPOPriceCondition;

        }


        public ICollection<ErrorHandler> SavePOPriceCondition(POPriceCondition argPOPriceCondition)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPOPriceConditionExists(argPOPriceCondition.PODocCode, argPOPriceCondition.ItemNo, argPOPriceCondition.ConditionType, argPOPriceCondition.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPOPriceCondition(argPOPriceCondition, da, lstErr);
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
                    UpdatePOPriceCondition(argPOPriceCondition, da, lstErr);
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

        public void SavePOPriceCondition(POPriceCondition argPOPriceCondition, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsPOPriceConditionExists(argPOPriceCondition.PODocCode, argPOPriceCondition.ItemNo, argPOPriceCondition.CalculationType, argPOPriceCondition.ClientCode, da) == false)
                {
                    InsertPOPriceCondition(argPOPriceCondition, da, lstErr);
                }
                else
                {
                    UpdatePOPriceCondition(argPOPriceCondition, da, lstErr);
                }
            }
            catch (Exception ex)
            {
                objErrorHandler.Type = ErrorConstant.strAboartType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strInsertModule;
                objErrorHandler.ModulePart = ErrorConstant.strDetailModule;
                objErrorHandler.Message = ex.Message.ToString();
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";
                lstErr.Add(objErrorHandler);
            }
        }
        public void InsertPOPriceCondition(POPriceCondition argPOPriceCondition, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@PODocCode", argPOPriceCondition.PODocCode);
            param[1] = new SqlParameter("@ItemNo", argPOPriceCondition.ItemNo);
            param[2] = new SqlParameter("@ConditionType", argPOPriceCondition.ConditionType);
            param[3] = new SqlParameter("@CalculationType", argPOPriceCondition.CalculationType);
            param[4] = new SqlParameter("@Amount", argPOPriceCondition.Amount);
            param[5] = new SqlParameter("@Currency", argPOPriceCondition.Currency);
            param[6] = new SqlParameter("@PerUnit", argPOPriceCondition.PerUnit);
            param[7] = new SqlParameter("@UOMCode", argPOPriceCondition.UOMCode);
            param[8] = new SqlParameter("@ConditionValue", argPOPriceCondition.ConditionValue);
            param[9] = new SqlParameter("@POLevel", argPOPriceCondition.POLevel);
            param[10] = new SqlParameter("@ClientCode", argPOPriceCondition.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argPOPriceCondition.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argPOPriceCondition.ModifiedBy);


            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_InsertPOPriceCondition", param);


            string strMessage = Convert.ToString(param[14].Value);
            string strType = Convert.ToString(param[13].Value);
            string strRetValue = Convert.ToString(param[15].Value);


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


        public void UpdatePOPriceCondition(POPriceCondition argPOPriceCondition, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@PODocCode", argPOPriceCondition.PODocCode);
            param[1] = new SqlParameter("@ItemNo", argPOPriceCondition.ItemNo);
            param[2] = new SqlParameter("@ConditionType", argPOPriceCondition.ConditionType);
            param[3] = new SqlParameter("@CalculationType", argPOPriceCondition.CalculationType);
            param[4] = new SqlParameter("@Amount", argPOPriceCondition.Amount);
            param[5] = new SqlParameter("@Currency", argPOPriceCondition.Currency);
            param[6] = new SqlParameter("@PerUnit", argPOPriceCondition.PerUnit);
            param[7] = new SqlParameter("@UOMCode", argPOPriceCondition.UOMCode);
            param[8] = new SqlParameter("@ConditionValue", argPOPriceCondition.ConditionValue);
            param[9] = new SqlParameter("@POLevel", argPOPriceCondition.POLevel);
            param[10] = new SqlParameter("@ClientCode", argPOPriceCondition.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argPOPriceCondition.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argPOPriceCondition.ModifiedBy);


            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePOPriceCondition", param);


            string strMessage = Convert.ToString(param[14].Value);
            string strType = Convert.ToString(param[13].Value);
            string strRetValue = Convert.ToString(param[15].Value);


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


        public ICollection<ErrorHandler> DeletePOPriceCondition(string argPODocCode, string argItemNo, string argConditionType, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@PODocCode", argPODocCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@ConditionType", argConditionType);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeletePOPriceCondition", param);


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


        public bool blnIsPOPriceConditionExists(string argPODocCode, string argItemNo, string argConditionType, string argClientCode)
        {
            bool IsPOPriceConditionExists = false;
            DataSet ds = new DataSet();
            ds = GetPOPriceCondition(argPODocCode, argItemNo, argConditionType, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPOPriceConditionExists = true;
            }
            else
            {
                IsPOPriceConditionExists = false;
            }
            return IsPOPriceConditionExists;
        }

        public bool blnIsPOPriceConditionExists(string argPODocCode, string argItemNo, string argConditionType, string argClientCode,DataAccess da)
        {
            bool IsPOPriceConditionExists = false;
            DataSet ds = new DataSet();
            ds = GetPOPriceCondition(argPODocCode, argItemNo, argConditionType, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPOPriceConditionExists = true;
            }
            else
            {
                IsPOPriceConditionExists = false;
            }
            return IsPOPriceConditionExists;
        }
    }
}