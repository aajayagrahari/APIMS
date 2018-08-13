
//Created On :: 04, June, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class SOPriceConditionManager
    {
        const string SOPriceConditionTable = "SOPriceCondition";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public SOPriceCondition objGetSOPriceCondition(string argSODocCode, string argItemNo, string argConditionType, string argClientCode)
        {
            SOPriceCondition argSOPriceCondition = new SOPriceCondition();
            DataSet DataSetToFill = new DataSet();

            if (argSODocCode.Trim() == "")
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

            DataSetToFill = this.GetSOPriceCondition(argSODocCode, argItemNo, argConditionType, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSOPriceCondition = this.objCreateSOPriceCondition((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSOPriceCondition;
        }

        public ICollection<SOPriceCondition> colGetSOPriceCondition(string argSODocCode, string argClientCode, List<SOPriceCondition> lst)
        {
           // List<SOPriceCondition> lst = new List<SOPriceCondition>();
            DataSet DataSetToFill = new DataSet();
            SOPriceCondition tSOPriceCondition = new SOPriceCondition();

            DataSetToFill = this.GetSOPriceCondition(argSODocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSOPriceCondition(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<SOPriceCondition> colGetSOPriceCondition(string argSODocCode, string argClientCode)
        {
            List<SOPriceCondition> lst = new List<SOPriceCondition>();
            DataSet DataSetToFill = new DataSet();
            SOPriceCondition tSOPriceCondition = new SOPriceCondition();

            DataSetToFill = this.GetSOPriceCondition(argSODocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSOPriceCondition(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetSOPriceConditionExists(string argSODocCode, string argItemNo, string argConditionType, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SODocCode", argSODocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ConditionType", argConditionType);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSOPriceCondition4ID", param);

            return DataSetToFill;
        }

        public DataSet GetSOPriceCondition(string argSODocCode, string argItemNo, string argConditionType, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SODocCode", argSODocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@ConditionType", argConditionType);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSOPriceCondition4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetSOPriceCondition(string argClientCode, string argSODocCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            param[1] = new SqlParameter("@SODocCode", argSODocCode);

            DataSetToFill = da.FillDataSet("SP_GetSOPriceCondition",param);
            return DataSetToFill;
        }
        
        private SOPriceCondition objCreateSOPriceCondition(DataRow dr)
        {
            SOPriceCondition tSOPriceCondition = new SOPriceCondition();

            tSOPriceCondition.SetObjectInfo(dr);

            return tSOPriceCondition;

        }

        public void SaveSOPriceCondition(SOPriceCondition argSOPriceCondition, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsSOPriceConditionExists(argSOPriceCondition.SODocCode, argSOPriceCondition.ItemNo, argSOPriceCondition.CalculationType, argSOPriceCondition.ClientCode, da) == false)
                {
                    InsertSOPriceCondition(argSOPriceCondition, da, lstErr);
                }
                else
                {
                    UpdateSOPriceCondition(argSOPriceCondition, da, lstErr);
                }
            }
            catch(Exception ex)
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
        
        //public ICollection<ErrorHandler> SaveSOPriceCondition(SOPriceCondition argSOPriceCondition)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsSOPriceConditionExists(argSOPriceCondition.SODocCode, argSOPriceCondition.ItemNo, argSOPriceCondition.ConditionType, argSOPriceCondition.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertSOPriceCondition(argSOPriceCondition, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
        //            da.COMMIT_TRANSACTION();
        //        }
        //        else
        //        {
        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            UpdateSOPriceCondition(argSOPriceCondition, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
        //            da.COMMIT_TRANSACTION();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        if (da != null)
        //        {
        //            da.ROLLBACK_TRANSACTION();
        //        }
        //        objErrorHandler.Type = ErrorConstant.strAboartType;
        //        objErrorHandler.MsgId = 0;
        //        objErrorHandler.Module = ErrorConstant.strInsertModule;
        //        objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
        //        objErrorHandler.Message = ex.Message.ToString();
        //        objErrorHandler.RowNo = 0;
        //        objErrorHandler.FieldName = "";
        //        objErrorHandler.LogCode = "";
        //        lstErr.Add(objErrorHandler);
        //    }
        //    finally
        //    {
        //        if (da != null)
        //        {
        //            da.Close_Connection();
        //            da = null;
        //        }
        //    }
        //    return lstErr;
        //}
        
        public void InsertSOPriceCondition(SOPriceCondition argSOPriceCondition, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@SODocCode", argSOPriceCondition.SODocCode);
            param[1] = new SqlParameter("@ItemNo", argSOPriceCondition.ItemNo);
            param[2] = new SqlParameter("@ConditionType", argSOPriceCondition.ConditionType);
            param[3] = new SqlParameter("@CalculationType", argSOPriceCondition.CalculationType);
            param[4] = new SqlParameter("@Amount", argSOPriceCondition.Amount);
            param[5] = new SqlParameter("@Currency", argSOPriceCondition.Currency);
            param[6] = new SqlParameter("@PerUnit", argSOPriceCondition.PerUnit);
            param[7] = new SqlParameter("@UOMCode", argSOPriceCondition.UOMCode);
            param[8] = new SqlParameter("@ConditionValue", argSOPriceCondition.ConditionValue);
            param[9] = new SqlParameter("@SOLevel", argSOPriceCondition.SOLevel);
            param[10] = new SqlParameter("@ClientCode", argSOPriceCondition.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argSOPriceCondition.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argSOPriceCondition.ModifiedBy);
            
            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSOPriceCondition", param);


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
        
        public void UpdateSOPriceCondition(SOPriceCondition argSOPriceCondition, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@SODocCode", argSOPriceCondition.SODocCode);
            param[1] = new SqlParameter("@ItemNo", argSOPriceCondition.ItemNo);
            param[2] = new SqlParameter("@ConditionType", argSOPriceCondition.ConditionType);
            param[3] = new SqlParameter("@CalculationType", argSOPriceCondition.CalculationType);
            param[4] = new SqlParameter("@Amount", argSOPriceCondition.Amount);
            param[5] = new SqlParameter("@Currency", argSOPriceCondition.Currency);
            param[6] = new SqlParameter("@PerUnit", argSOPriceCondition.PerUnit);
            param[7] = new SqlParameter("@UOMCode", argSOPriceCondition.UOMCode);
            param[8] = new SqlParameter("@ConditionValue", argSOPriceCondition.ConditionValue);
            param[9] = new SqlParameter("@SOLevel", argSOPriceCondition.SOLevel);
            param[10] = new SqlParameter("@ClientCode", argSOPriceCondition.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argSOPriceCondition.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argSOPriceCondition.ModifiedBy);

            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSOPriceCondition", param);


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
        
        public ICollection<ErrorHandler> DeleteSOPriceCondition(string argSODocCode, string argItemNo, string argConditionType, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@SODocCode", argSODocCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@ConditionType", argConditionType);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted",iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteSOPriceCondition", param);


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
        
        public bool blnIsSOPriceConditionExists(string argSODocCode, string argItemNo, string argConditionType, string argClientCode, DataAccess da)
        {
            bool IsSOPriceConditionExists = false;
            DataSet ds = new DataSet();
            ds = GetSOPriceConditionExists(argSODocCode, argItemNo, argConditionType, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSOPriceConditionExists = true;
            }
            else
            {
                IsSOPriceConditionExists = false;
            }
            return IsSOPriceConditionExists;
        }

    }
}