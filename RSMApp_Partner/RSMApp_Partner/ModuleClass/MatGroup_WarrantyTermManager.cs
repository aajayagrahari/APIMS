
//Created On :: 13, December, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class MatGroup_WarrantyTermManager
    {
        const string MatGroup_WarrantyTermTable = "MatGroup_WarrantyTerm";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public MatGroup_WarrantyTerm objGetMatGroup_WarrantyTerm(string argMaterialCode,string argMatGroup1Code, string argClientCode)
        {
            MatGroup_WarrantyTerm argMatGroup_WarrantyTerm = new MatGroup_WarrantyTerm();
            DataSet DataSetToFill = new DataSet();

            if (argMatGroup1Code.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetMatGroup_WarrantyTerm(argMaterialCode,argMatGroup1Code, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMatGroup_WarrantyTerm = this.objCreateMatGroup_WarrantyTerm((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argMatGroup_WarrantyTerm;
        }

        public ICollection<MatGroup_WarrantyTerm> colGetMatGroup_WarrantyTerm(string argClientCode)
        {
            List<MatGroup_WarrantyTerm> lst = new List<MatGroup_WarrantyTerm>();
            DataSet DataSetToFill = new DataSet();
            MatGroup_WarrantyTerm tMatGroup_WarrantyTerm = new MatGroup_WarrantyTerm();

            DataSetToFill = this.GetMatGroup_WarrantyTerm(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMatGroup_WarrantyTerm(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetMatGroup_WarrantyTerm(string argMaterialCode, string argMatGroup1Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMatGroup_WarrantyTerm4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMatGroup_WarrantyTerm(string argMaterialCode,  string argMatGroup1Code, string argClientCode, DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMatGroup_WarrantyTerm4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMatGroup_WarrantyTerm(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMatGroup_WarrantyTerm",param);
            return DataSetToFill;
        }

        private MatGroup_WarrantyTerm objCreateMatGroup_WarrantyTerm(DataRow dr)
        {
            MatGroup_WarrantyTerm tMatGroup_WarrantyTerm = new MatGroup_WarrantyTerm();

            tMatGroup_WarrantyTerm.SetObjectInfo(dr);

            return tMatGroup_WarrantyTerm;

        }

        public ICollection<ErrorHandler> SaveMatGroup_WarrantyTerm(MatGroup_WarrantyTerm argMatGroup_WarrantyTerm)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsMatGroup_WarrantyTermExists("", argMatGroup_WarrantyTerm.MatGroup1Code, argMatGroup_WarrantyTerm.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertMatGroup_WarrantyTerm(argMatGroup_WarrantyTerm, da, lstErr);
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
                    UpdateMatGroup_WarrantyTerm(argMatGroup_WarrantyTerm, da, lstErr);
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

        public ICollection<ErrorHandler> SaveMatGroup_WarrantyTerm(ICollection<MatGroup_WarrantyTerm> colGetMatGroup_WarrantyTerm)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (MatGroup_WarrantyTerm argMatGroup_WarrantyTerm in colGetMatGroup_WarrantyTerm)
                {

                    if (argMatGroup_WarrantyTerm.IsDeleted == 0)
                    {

                        if (blnIsMatGroup_WarrantyTermExists("", argMatGroup_WarrantyTerm.MatGroup1Code, argMatGroup_WarrantyTerm.ClientCode, da) == false)
                        {
                            InsertMatGroup_WarrantyTerm(argMatGroup_WarrantyTerm, da, lstErr);
                        }
                        else
                        {
                            UpdateMatGroup_WarrantyTerm(argMatGroup_WarrantyTerm, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMatGroup_WarrantyTerm(argMatGroup_WarrantyTerm.MatGroup1Code,argMatGroup_WarrantyTerm.MaterialCode,argMatGroup_WarrantyTerm.MastMaterialCode ,argMatGroup_WarrantyTerm.ClientCode, argMatGroup_WarrantyTerm.IsDeleted);

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

        public void InsertMatGroup_WarrantyTerm(MatGroup_WarrantyTerm argMatGroup_WarrantyTerm, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup_WarrantyTerm.MatGroup1Code);
            param[1] = new SqlParameter("@MaterialCode", argMatGroup_WarrantyTerm.MaterialCode);
            param[2] = new SqlParameter("@MastMaterialCode", argMatGroup_WarrantyTerm.MastMaterialCode);
            param[3] = new SqlParameter("@VendorWarTermsCode", argMatGroup_WarrantyTerm.VendorWarTermsCode);
            param[4] = new SqlParameter("@CustWarTermsCode", argMatGroup_WarrantyTerm.CustWarTermsCode);
            param[5] = new SqlParameter("@ClientCode", argMatGroup_WarrantyTerm.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argMatGroup_WarrantyTerm.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argMatGroup_WarrantyTerm.ModifiedBy);
    

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertMatGroup_WarrantyTerm", param);


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

        public void UpdateMatGroup_WarrantyTerm(MatGroup_WarrantyTerm argMatGroup_WarrantyTerm, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup_WarrantyTerm.MatGroup1Code);
            param[1] = new SqlParameter("@MaterialCode", argMatGroup_WarrantyTerm.MaterialCode);
            param[2] = new SqlParameter("@MastMaterialCode", argMatGroup_WarrantyTerm.MastMaterialCode);
            param[3] = new SqlParameter("@VendorWarTermsCode", argMatGroup_WarrantyTerm.VendorWarTermsCode);
            param[4] = new SqlParameter("@CustWarTermsCode", argMatGroup_WarrantyTerm.CustWarTermsCode);
            param[5] = new SqlParameter("@ClientCode", argMatGroup_WarrantyTerm.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argMatGroup_WarrantyTerm.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argMatGroup_WarrantyTerm.ModifiedBy);


            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateMatGroup_WarrantyTerm", param);


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

        public ICollection<ErrorHandler> DeleteMatGroup_WarrantyTerm(string argMatGroup1Code, string argMaterialCode,string argMastMaterialCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
                param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
                param[2] = new SqlParameter("@MastMaterialCode", argMastMaterialCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteMatGroup_WarrantyTerm", param);


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

        public bool blnIsMatGroup_WarrantyTermExists(string argMaterialCode, string argMatGroup1Code, string argClientCode)
        {
            bool IsMatGroup_WarrantyTermExists = false;
            DataSet ds = new DataSet();
            ds = GetMatGroup_WarrantyTerm(argMaterialCode, argMatGroup1Code, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMatGroup_WarrantyTermExists = true;
            }
            else
            {
                IsMatGroup_WarrantyTermExists = false;
            }
            return IsMatGroup_WarrantyTermExists;
        }

        public bool blnIsMatGroup_WarrantyTermExists(string argMaterialCode,string argMatGroup1Code, string argClientCode,DataAccess da)
        {
            bool IsMatGroup_WarrantyTermExists = false;
            DataSet ds = new DataSet();
            ds = GetMatGroup_WarrantyTerm(argMaterialCode, argMatGroup1Code, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMatGroup_WarrantyTermExists = true;
            }
            else
            {
                IsMatGroup_WarrantyTermExists = false;
            }
            return IsMatGroup_WarrantyTermExists;
        }
    }
}