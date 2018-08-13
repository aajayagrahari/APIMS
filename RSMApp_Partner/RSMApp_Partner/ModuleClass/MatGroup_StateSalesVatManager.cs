
//Created On :: 29, November, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class MatGroup_StateSalesVatManager
    {
        const string MatGroup_StateSalesVatTable = "MatGroup_StateSalesVat";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public MatGroup_StateSalesVat objGetMatGroup_StateSalesVat(int argVatID, string argClientCode)
        {
            MatGroup_StateSalesVat argMatGroup_StateSalesVat = new MatGroup_StateSalesVat();
            DataSet DataSetToFill = new DataSet();

            if (argVatID <= 0)
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetMatGroup_StateSalesVat(argVatID, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMatGroup_StateSalesVat = this.objCreateMatGroup_StateSalesVat((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argMatGroup_StateSalesVat;
        }
        
        public ICollection<MatGroup_StateSalesVat> colGetMatGroup_StateSalesVat(string argClientCode)
        {
            List<MatGroup_StateSalesVat> lst = new List<MatGroup_StateSalesVat>();
            DataSet DataSetToFill = new DataSet();
            MatGroup_StateSalesVat tMatGroup_StateSalesVat = new MatGroup_StateSalesVat();

            DataSetToFill = this.GetMatGroup_StateSalesVat(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMatGroup_StateSalesVat(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetMatGroup_StateSalesVat(int argVatID, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@VatID", argVatID);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMatGroup_StateSalesVat4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetMatGroup_StateSalesVat(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMatGroup_StateSalesVat", param);
            return DataSetToFill;
        }

        public DataSet GetStateSalesVat4Repair(string argMatGroup1Code, string argStateCode, DateTime argCurDate, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@StateCode", argStateCode);
            param[2] = new SqlParameter("@CurDate", argCurDate);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSalesVat4Repair", param);
            return DataSetToFill;
        }

        private MatGroup_StateSalesVat objCreateMatGroup_StateSalesVat(DataRow dr)
        {
            MatGroup_StateSalesVat tMatGroup_StateSalesVat = new MatGroup_StateSalesVat();

            tMatGroup_StateSalesVat.SetObjectInfo(dr);

            return tMatGroup_StateSalesVat;

        }
        
        public ICollection<ErrorHandler> SaveMatGroup_StateSalesVat(MatGroup_StateSalesVat argMatGroup_StateSalesVat)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsMatGroup_StateSalesVatExists(argMatGroup_StateSalesVat.VatID, argMatGroup_StateSalesVat.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertMatGroup_StateSalesVat(argMatGroup_StateSalesVat, da, lstErr);
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
                    UpdateMatGroup_StateSalesVat(argMatGroup_StateSalesVat, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
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


        public void InsertMatGroup_StateSalesVat(MatGroup_StateSalesVat argMatGroup_StateSalesVat, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@VatID", argMatGroup_StateSalesVat.VatID);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup_StateSalesVat.MatGroup1Code);
            param[2] = new SqlParameter("@StateCode", argMatGroup_StateSalesVat.StateCode);
            param[3] = new SqlParameter("@VatPer", argMatGroup_StateSalesVat.VatPer);
            param[4] = new SqlParameter("@ValidFrom", argMatGroup_StateSalesVat.ValidFrom);
            param[5] = new SqlParameter("@ValidTo", argMatGroup_StateSalesVat.ValidTo);
            param[6] = new SqlParameter("@ClientCode", argMatGroup_StateSalesVat.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argMatGroup_StateSalesVat.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argMatGroup_StateSalesVat.ModifiedBy);
           
            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertMatGroup_StateSalesVat", param);


            string strMessage = Convert.ToString(param[10].Value);
            string strType = Convert.ToString(param[9].Value);
            string strRetValue = Convert.ToString(param[11].Value);


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


        public void UpdateMatGroup_StateSalesVat(MatGroup_StateSalesVat argMatGroup_StateSalesVat, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@VatID", argMatGroup_StateSalesVat.VatID);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup_StateSalesVat.MatGroup1Code);
            param[2] = new SqlParameter("@StateCode", argMatGroup_StateSalesVat.StateCode);
            param[3] = new SqlParameter("@VatPer", argMatGroup_StateSalesVat.VatPer);
            param[4] = new SqlParameter("@ValidFrom", argMatGroup_StateSalesVat.ValidFrom);
            param[5] = new SqlParameter("@ValidTo", argMatGroup_StateSalesVat.ValidTo);
            param[6] = new SqlParameter("@ClientCode", argMatGroup_StateSalesVat.ClientCode);
            param[7] = new SqlParameter("@CreatedBy", argMatGroup_StateSalesVat.CreatedBy);
            param[8] = new SqlParameter("@ModifiedBy", argMatGroup_StateSalesVat.ModifiedBy);
            
            param[9] = new SqlParameter("@Type", SqlDbType.Char);
            param[9].Size = 1;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[10].Size = 255;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[11].Size = 20;
            param[11].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateMatGroup_StateSalesVat", param);


            string strMessage = Convert.ToString(param[10].Value);
            string strType = Convert.ToString(param[9].Value);
            string strRetValue = Convert.ToString(param[11].Value);


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


        public ICollection<ErrorHandler> DeleteMatGroup_StateSalesVat(int argVatID, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@VatID", argVatID);
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
                int i = da.ExecuteNonQuery("Proc_DeleteMatGroup_StateSalesVat", param);


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


        public bool blnIsMatGroup_StateSalesVatExists(int argVatID, string argClientCode)
        {
            bool IsMatGroup_StateSalesVatExists = false;
            DataSet ds = new DataSet();
            ds = GetMatGroup_StateSalesVat(argVatID, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMatGroup_StateSalesVatExists = true;
            }
            else
            {
                IsMatGroup_StateSalesVatExists = false;
            }
            return IsMatGroup_StateSalesVatExists;
        }
    }
}