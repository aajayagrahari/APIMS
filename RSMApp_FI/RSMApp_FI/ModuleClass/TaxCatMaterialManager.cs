
//Created On :: 03, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;


namespace RSMApp_FI
{
    public class TaxCatMaterialManager
    {
        const string TaxCatMaterialTable = "TaxCatMaterial";

       // GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public TaxCatMaterial objGetTaxCatMaterial(string argTaxCategoryCode, string argTaxClassCode, string argClientCode)
        {
            TaxCatMaterial argTaxCatMaterial = new TaxCatMaterial();
            DataSet DataSetToFill = new DataSet();

            if (argTaxCategoryCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argTaxClassCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetTaxCatMaterial(argTaxCategoryCode, argTaxClassCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argTaxCatMaterial = this.objCreateTaxCatMaterial((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argTaxCatMaterial;
        }
        
        public ICollection<TaxCatMaterial> colGetTaxCatMaterial(string argClientCode)
        {
            List<TaxCatMaterial> lst = new List<TaxCatMaterial>();
            DataSet DataSetToFill = new DataSet();
            TaxCatMaterial tTaxCatMaterial = new TaxCatMaterial();

            DataSetToFill = this.GetTaxCatMaterial(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateTaxCatMaterial(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetTaxCatMaterial(string argTaxCategoryCode, string argTaxClassCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@TaxCategoryCode", argTaxCategoryCode);
            param[1] = new SqlParameter("@TaxClassCode", argTaxClassCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetTaxCatMaterial4ID", param);

            return DataSetToFill;
        }

        public DataSet GetTaxCatMaterial(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
          
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetTaxCatMaterial",param);
            return DataSetToFill;
        }

        private TaxCatMaterial objCreateTaxCatMaterial(DataRow dr)
        {
            TaxCatMaterial tTaxCatMaterial = new TaxCatMaterial();

            tTaxCatMaterial.SetObjectInfo(dr);

            return tTaxCatMaterial;

        }

        public ICollection<ErrorHandler> SaveTaxCatMaterial(TaxCatMaterial argTaxCatMaterial)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsTaxCatMaterialExists(argTaxCatMaterial.TaxCategoryCode, argTaxCatMaterial.TaxClassCode, argTaxCatMaterial.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertTaxCatMaterial(argTaxCatMaterial, da, lstErr);
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
                    UpdateTaxCatMaterial(argTaxCatMaterial, da, lstErr);
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

        public void InsertTaxCatMaterial(TaxCatMaterial argTaxCatMaterial, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@TaxCategoryCode", argTaxCatMaterial.TaxCategoryCode);
            param[1] = new SqlParameter("@TaxCategoryDesc", argTaxCatMaterial.TaxCategoryDesc);
            param[2] = new SqlParameter("@TaxClassCode", argTaxCatMaterial.TaxClassCode);
            param[3] = new SqlParameter("@TaxClassDesc", argTaxCatMaterial.TaxClassDesc);
            param[4] = new SqlParameter("@ClientCode", argTaxCatMaterial.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argTaxCatMaterial.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argTaxCatMaterial.ModifiedBy);
         

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertTaxCatMaterial", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public void UpdateTaxCatMaterial(TaxCatMaterial argTaxCatMaterial, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@TaxCategoryCode", argTaxCatMaterial.TaxCategoryCode);
            param[1] = new SqlParameter("@TaxCategoryDesc", argTaxCatMaterial.TaxCategoryDesc);
            param[2] = new SqlParameter("@TaxClassCode", argTaxCatMaterial.TaxClassCode);
            param[3] = new SqlParameter("@TaxClassDesc", argTaxCatMaterial.TaxClassDesc);
            param[4] = new SqlParameter("@ClientCode", argTaxCatMaterial.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argTaxCatMaterial.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argTaxCatMaterial.ModifiedBy);


            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateTaxCatMaterial", param);


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
            objErrorHandler.ReturnValue = strRetValue;
            lstErr.Add(objErrorHandler);

        }

        public ICollection<ErrorHandler> DeleteTaxCatMaterial(string argTaxCategoryCode, string argTaxClassCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@TaxCategoryCode", argTaxCategoryCode);
                param[1] = new SqlParameter("@TaxClassCode", argTaxClassCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteTaxCatMaterial", param);


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

        public bool blnIsTaxCatMaterialExists(string argTaxCategoryCode, string argTaxClassCode, string argClientCode)
        {
            bool IsTaxCatMaterialExists = false;
            DataSet ds = new DataSet();
            ds = GetTaxCatMaterial(argTaxCategoryCode, argTaxClassCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsTaxCatMaterialExists = true;
            }
            else
            {
                IsTaxCatMaterialExists = false;
            }
            return IsTaxCatMaterialExists;
        }
    }
}