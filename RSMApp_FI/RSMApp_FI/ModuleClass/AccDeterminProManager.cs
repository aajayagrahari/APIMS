
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
    public class AccDeterminProManager
    {
        const string AccDeterminProTable = "AccDeterminPro";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public AccDeterminPro objGetAccDeterminPro(int argAccDeterminProCode, string argAccDMTableCode, string argClientCode)
        {
            AccDeterminPro argAccDeterminPro = new AccDeterminPro();
            DataSet DataSetToFill = new DataSet();

            if (argAccDeterminProCode <= 0)
            {
                goto ErrorHandlers;
            }

            if (argAccDMTableCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetAccDeterminPro(argAccDeterminProCode, argAccDMTableCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argAccDeterminPro = this.objCreateAccDeterminPro((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argAccDeterminPro;
        }

        public ICollection<AccDeterminPro> colGetAccDeterminPro(string argAccDMTableCode, string argClientCode)
        {
            List<AccDeterminPro> lst = new List<AccDeterminPro>();
            DataSet DataSetToFill = new DataSet();
            AccDeterminPro tAccDeterminPro = new AccDeterminPro();

            DataSetToFill = this.GetAccDeterminPro(argAccDMTableCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateAccDeterminPro(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetAccDeterminPro(int argAccDeterminProCode, string argAccDMTableCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@AccDeterminProCode", argAccDeterminProCode);
            param[1] = new SqlParameter("@AccDMTableCode", argAccDMTableCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAccDeterminPro4ID", param);

            return DataSetToFill;
        }
       
        public DataSet GetAccDeterminPro(int argAccDeterminProCode, string argAccDMTableCode, string argClientCode,DataAccess da)
        {
        
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@AccDeterminProCode", argAccDeterminProCode);
            param[1] = new SqlParameter("@AccDMTableCode", argAccDMTableCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetAccDeterminPro4ID", param);

            return DataSetToFill;
        }

        public DataSet GetAccDeterminPro(string argAccDMTableCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@AccDMTableCode", argAccDMTableCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAccDeterminPro",param);
            return DataSetToFill;
        }
        
        private AccDeterminPro objCreateAccDeterminPro(DataRow dr)
        {
            AccDeterminPro tAccDeterminPro = new AccDeterminPro();

            tAccDeterminPro.SetObjectInfo(dr);

            return tAccDeterminPro;

        }

        //public ICollection<ErrorHandler> SaveAccDeterminPro(AccDeterminPro argAccDeterminPro)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsAccDeterminProExists(argAccDeterminPro.AccDeterminProCode, argAccDeterminPro.AccDMTableCode, argAccDeterminPro.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertAccDeterminPro(argAccDeterminPro, da, lstErr);
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
        //            UpdateAccDeterminPro(argAccDeterminPro, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
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

        public ICollection<ErrorHandler> SaveAccDeterminPro(ICollection<AccDeterminPro> colGetAccDeterminPro)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (AccDeterminPro argAccDeterminPro in colGetAccDeterminPro)
                {
                    if (argAccDeterminPro.IsDeleted == 0)
                    {

                        if (blnIsAccDeterminProExists(argAccDeterminPro.AccDeterminProCode, argAccDeterminPro.AccDMTableCode, argAccDeterminPro.ClientCode,da) == false)
                        {
                            InsertAccDeterminPro(argAccDeterminPro, da, lstErr);
                        }
                        else
                        {
                            UpdateAccDeterminPro(argAccDeterminPro, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteAccDeterminPro(argAccDeterminPro.AccDeterminProCode, argAccDeterminPro.AccDMTableCode, argAccDeterminPro.ClientCode, argAccDeterminPro.IsDeleted);
                    }
                }

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
        
        public void InsertAccDeterminPro(AccDeterminPro argAccDeterminPro, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@AccDeterminProCode", argAccDeterminPro.AccDeterminProCode);
            param[1] = new SqlParameter("@AccDMTableCode", argAccDeterminPro.AccDMTableCode);
            param[2] = new SqlParameter("@CustGroupCode", argAccDeterminPro.CustGroupCode);
            param[3] = new SqlParameter("@MatGroup1Code", argAccDeterminPro.MatGroup1Code);
            param[4] = new SqlParameter("@AccKeyCode", argAccDeterminPro.AccKeyCode);
            param[5] = new SqlParameter("@SalesOrganizationCode", argAccDeterminPro.SalesOrganizationCode);
            param[6] = new SqlParameter("@DistChannelCode", argAccDeterminPro.DistChannelCode);
            param[7] = new SqlParameter("@PlantCode", argAccDeterminPro.PlantCode);
            param[8] = new SqlParameter("@AccAssignGroupCodeCust", argAccDeterminPro.AccAssignGroupCodeCust);
            param[9] = new SqlParameter("@AccAssignGroupCodeMat", argAccDeterminPro.AccAssignGroupCodeMat);
            param[10] = new SqlParameter("@SOTypeCode", argAccDeterminPro.SOTypeCode);
            param[11] = new SqlParameter("@DeliveryDocTypeCode", argAccDeterminPro.DeliveryDocTypeCode);
            param[12] = new SqlParameter("@BillingDocTypeCode", argAccDeterminPro.BillingDocTypeCode);
            param[13] = new SqlParameter("@AccountDocTypeCode", argAccDeterminPro.AccountDocTypeCode);
            param[14] = new SqlParameter("@PlantStateCode", argAccDeterminPro.PlantStateCode);
            param[15] = new SqlParameter("@ChartACCode", argAccDeterminPro.ChartACCode);
            param[16] = new SqlParameter("@GLCode", argAccDeterminPro.GLCode);
            param[17] = new SqlParameter("@ClientCode", argAccDeterminPro.ClientCode);
            param[18] = new SqlParameter("@CreatedBy", argAccDeterminPro.CreatedBy);
            param[19] = new SqlParameter("@ModifiedBy", argAccDeterminPro.ModifiedBy);
        
            param[20] = new SqlParameter("@Type", SqlDbType.Char);
            param[20].Size = 1;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[21].Size = 255;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[22].Size = 20;
            param[22].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertAccDeterminPro", param);


            string strMessage = Convert.ToString(param[21].Value);
            string strType = Convert.ToString(param[20].Value);
            string strRetValue = Convert.ToString(param[22].Value);


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

        public void UpdateAccDeterminPro(AccDeterminPro argAccDeterminPro, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[23];
            param[0] = new SqlParameter("@AccDeterminProCode", argAccDeterminPro.AccDeterminProCode);
            param[1] = new SqlParameter("@AccDMTableCode", argAccDeterminPro.AccDMTableCode);
            param[2] = new SqlParameter("@CustGroupCode", argAccDeterminPro.CustGroupCode);
            param[3] = new SqlParameter("@MatGroup1Code", argAccDeterminPro.MatGroup1Code);
            param[4] = new SqlParameter("@AccKeyCode", argAccDeterminPro.AccKeyCode);
            param[5] = new SqlParameter("@SalesOrganizationCode", argAccDeterminPro.SalesOrganizationCode);
            param[6] = new SqlParameter("@DistChannelCode", argAccDeterminPro.DistChannelCode);
            param[7] = new SqlParameter("@PlantCode", argAccDeterminPro.PlantCode);
            param[8] = new SqlParameter("@AccAssignGroupCodeCust", argAccDeterminPro.AccAssignGroupCodeCust);
            param[9] = new SqlParameter("@AccAssignGroupCodeMat", argAccDeterminPro.AccAssignGroupCodeMat);
            param[10] = new SqlParameter("@SOTypeCode", argAccDeterminPro.SOTypeCode);
            param[11] = new SqlParameter("@DeliveryDocTypeCode", argAccDeterminPro.DeliveryDocTypeCode);
            param[12] = new SqlParameter("@BillingDocTypeCode", argAccDeterminPro.BillingDocTypeCode);
            param[13] = new SqlParameter("@AccountDocTypeCode", argAccDeterminPro.AccountDocTypeCode);
            param[14] = new SqlParameter("@PlantStateCode", argAccDeterminPro.PlantStateCode);
            param[15] = new SqlParameter("@ChartACCode", argAccDeterminPro.ChartACCode);
            param[16] = new SqlParameter("@GLCode", argAccDeterminPro.GLCode);
            param[17] = new SqlParameter("@ClientCode", argAccDeterminPro.ClientCode);
            param[18] = new SqlParameter("@CreatedBy", argAccDeterminPro.CreatedBy);
            param[19] = new SqlParameter("@ModifiedBy", argAccDeterminPro.ModifiedBy);

            param[20] = new SqlParameter("@Type", SqlDbType.Char);
            param[20].Size = 1;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[21].Size = 255;
            param[21].Direction = ParameterDirection.Output;

            param[22] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[22].Size = 20;
            param[22].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateAccDeterminPro", param);


            string strMessage = Convert.ToString(param[21].Value);
            string strType = Convert.ToString(param[20].Value);
            string strRetValue = Convert.ToString(param[22].Value);


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

        public ICollection<ErrorHandler> DeleteAccDeterminPro(int argAccDeterminProCode, string argAccDMTableCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@AccDeterminProCode", argAccDeterminProCode);
                param[1] = new SqlParameter("@AccDMTableCode", argAccDMTableCode);
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
                int i = da.ExecuteNonQuery("Proc_DeleteAccDeterminPro", param);


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

        public bool blnIsAccDeterminProExists(int argAccDeterminProCode, string argAccDMTableCode, string argClientCode)
        {
            bool IsAccDeterminProExists = false;
            DataSet ds = new DataSet();
            ds = GetAccDeterminPro(argAccDeterminProCode, argAccDMTableCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAccDeterminProExists = true;
            }
            else
            {
                IsAccDeterminProExists = false;
            }
            return IsAccDeterminProExists;
        }

        public bool blnIsAccDeterminProExists(int argAccDeterminProCode, string argAccDMTableCode, string argClientCode,DataAccess da)
        {
            bool IsAccDeterminProExists = false;
            DataSet ds = new DataSet();
            ds = GetAccDeterminPro(argAccDeterminProCode, argAccDMTableCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAccDeterminProExists = true;
            }
            else
            {
                IsAccDeterminProExists = false;
            }
            return IsAccDeterminProExists;
        }
    }
}