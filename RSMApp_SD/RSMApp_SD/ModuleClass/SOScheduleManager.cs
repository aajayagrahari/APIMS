
//Created On :: 01, June, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class SOScheduleManager
    {
        const string SOScheduleTable = "SOSchedule";

        // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public SOSchedule objGetSOSchedule(string argSODocCode, string argClientCode)
        {
            SOSchedule argSOSchedule = new SOSchedule();
            DataSet DataSetToFill = new DataSet();

            if (argSODocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

           
            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetSOSchedule(argSODocCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSOSchedule = this.objCreateSOSchedule((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSOSchedule;
        }

        public ICollection<SOSchedule> colGetSOSchedule(string argClientCode, string argSODocCode, List<SOSchedule> lst)
        {
            //List<SOSchedule> lst = new List<SOSchedule>();
            DataSet DataSetToFill = new DataSet();
            SOSchedule tSOSchedule = new SOSchedule();

            DataSetToFill = this.GetSOSchedule(argClientCode, argSODocCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSOSchedule(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        //public ICollection<SOSchedule> colGetSOSchedule(string argClientCode, string argSODocCode, List<SOSchedule> lst)
        //{
        //    DataSet DataSetToFill = new DataSet();
        //    SOSchedule tSOSchedule = new SOSchedule();

        //    DataSetToFill = this.GetSOSchedule(argClientCode, argSODocCode);

        //    if (DataSetToFill != null)
        //    {
        //        foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
        //        {
        //            lst.Add(objCreateSOSchedule(dr));
        //        }
        //    }
        //    goto Finish;

        //Finish:
        //    DataSetToFill = null;


        //    return lst;
        //}




        public DataSet GetSOSchedule(string argSODocCode, string argItemNo, string argSOSItemNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SODocCode", argSODocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@SOSItemNo", argSOSItemNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSOSchedule4ID", param);

            return DataSetToFill;
        }

        public DataSet GetSOSchedule(string argSODocCode, string argItemNo, string argSOSItemNo, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SODocCode", argSODocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@SOSItemNo", argSOSItemNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSOSchedule4ID", param);

            return DataSetToFill;
        }



        public DataSet GetSOSchedule(string argClientCode, string argSODocCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@SODocCode", argSODocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSOSchedule", param);

            return DataSetToFill;
        }


        private SOSchedule objCreateSOSchedule(DataRow dr)
        {
            SOSchedule tSOSchedule = new SOSchedule();

            tSOSchedule.SetObjectInfo(dr);

            return tSOSchedule;

        }


        public void SaveSOSchedule(SOSchedule argSOSchedule, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsSOScheduleExists(argSOSchedule.SODocCode, argSOSchedule.ItemNo, argSOSchedule.SOSItemNo, argSOSchedule.ClientCode, da) == false)
                {
                    InsertSOSchedule(argSOSchedule, da, lstErr);
                }
                else
                {
                    UpdateSOSchedule(argSOSchedule, da, lstErr);
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

        //public ICollection<ErrorHandler> SaveSOSchedule(SOSchedule argSOSchedule)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsSOScheduleExists(argSOSchedule.SODocCode, argSOSchedule.ItemNo, argSOSchedule.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertSOSchedule(argSOSchedule, da, lstErr);
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
        //            UpdateSOSchedule(argSOSchedule, da, lstErr);
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


        public string InsertSOSchedule(SOSchedule argSOSchedule, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@SOSItemNo",argSOSchedule.SOSItemNo);
            param[1] = new SqlParameter("@SODocCode", argSOSchedule.SODocCode);
            param[2] = new SqlParameter("@ItemNo", argSOSchedule.ItemNo);
            param[3] = new SqlParameter("@ClientCode", argSOSchedule.ClientCode);
            param[4] = new SqlParameter("@DeliveryDate", argSOSchedule.DeliveryDate);
            param[5] = new SqlParameter("@SchQuantity", argSOSchedule.SchQuantity);
            param[6] = new SqlParameter("@DeliveryQuantity", argSOSchedule.DeliveryQuantity);
            param[7] = new SqlParameter("@RoundedQty", argSOSchedule.RoundedQty);
            param[8] = new SqlParameter("@ConfirmedQty", argSOSchedule.ConfirmedQty);
            param[9] = new SqlParameter("@SchStatus", argSOSchedule.SchStatus);
            param[10] = new SqlParameter("@CreatedBy", argSOSchedule.CreatedBy);
            param[11] = new SqlParameter("@ModifiedBy", argSOSchedule.ModifiedBy);

            param[12] = new SqlParameter("@Type", SqlDbType.Char);
            param[12].Size = 1;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[13].Size = 255;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[14].Size = 20;
            param[14].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSOSchedule", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

            return strRetValue;

        }


        public string UpdateSOSchedule(SOSchedule argSOSchedule, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[15];
            param[0] = new SqlParameter("@SOSItemNo", argSOSchedule.SOSItemNo);
            param[1] = new SqlParameter("@SODocCode", argSOSchedule.SODocCode);
            param[2] = new SqlParameter("@ItemNo", argSOSchedule.ItemNo);
            param[3] = new SqlParameter("@ClientCode", argSOSchedule.ClientCode);
            param[4] = new SqlParameter("@DeliveryDate", argSOSchedule.DeliveryDate);
            param[5] = new SqlParameter("@SchQuantity", argSOSchedule.SchQuantity);
            param[6] = new SqlParameter("@DeliveryQuantity", argSOSchedule.DeliveryQuantity);
            param[7] = new SqlParameter("@RoundedQty", argSOSchedule.RoundedQty);
            param[8] = new SqlParameter("@ConfirmedQty", argSOSchedule.ConfirmedQty);
            param[9] = new SqlParameter("@SchStatus", argSOSchedule.SchStatus);
            param[10] = new SqlParameter("@CreatedBy", argSOSchedule.CreatedBy);
            param[11] = new SqlParameter("@ModifiedBy", argSOSchedule.ModifiedBy);

            param[12] = new SqlParameter("@Type", SqlDbType.Char);
            param[12].Size = 1;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[13].Size = 255;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[14].Size = 20;
            param[14].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSOSchedule", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);
            return strRetValue;

        }


        public ICollection<ErrorHandler> DeleteSOSchedule(string argSOSItemNo,string argSODocCode, string argItemNo, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@SOSItemNo",argSOSItemNo);
                param[1] = new SqlParameter("@SODocCode", argSODocCode);
                param[2] = new SqlParameter("@ItemNo", argItemNo);
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

                int i = da.ExecuteNonQuery("Proc_DeleteSOSchedule", param);


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


        public bool blnIsSOScheduleExists( string argSODocCode, string argItemNo, string argSOSItemNo, string argClientCode, DataAccess da)
        {
            bool IsSOScheduleExists = false;
            DataSet ds = new DataSet();
            ds = GetSOSchedule(argSODocCode, argItemNo, argSOSItemNo, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSOScheduleExists = true;
            }
            else
            {
                IsSOScheduleExists = false;
            }
            return IsSOScheduleExists;
        }
    }
}