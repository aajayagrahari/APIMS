
//Created On :: 19, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class MatGroup_DefectTypeManager
    {
        const string MaterialGroup_DefectTypeTable = "MatGroup_DefectType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public MatGroup_DefectType objGetMatGroup_DefectType(string argMatGroup1Code, string argDefectTypeCode, string argClientCode)
        {
            MatGroup_DefectType argMaterialGroup_DefectType = new MatGroup_DefectType();
            DataSet DataSetToFill = new DataSet();

            if (argMatGroup1Code.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argDefectTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetMatGroup_DefectType(argMatGroup1Code, argDefectTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMaterialGroup_DefectType = this.objCreateMatGroup_DefectType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argMaterialGroup_DefectType;
        }

        public ICollection<MatGroup_DefectType> colGetMatGroup_DefectType(string argMaterialGroup1Code, string argClientCode)
        {
            List<MatGroup_DefectType> lst = new List<MatGroup_DefectType>();
            DataSet DataSetToFill = new DataSet();
            MatGroup_DefectType tMaterialGroup_DefectType = new MatGroup_DefectType();

            DataSetToFill = this.GetMatGroup_DefectType(argMaterialGroup1Code, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMatGroup_DefectType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetMatGroup_DefectType(string argMatGroup1Code, string argDefectTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@DefectTypeCode", argDefectTypeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMatGroup_DefectType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMatGroup_DefectType(string argMatGroup1Code, string argDefectTypeCode, string argClientCode, DataAccess da)
        {

            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@DefectTypeCode", argDefectTypeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMatGroup_DefectType4ID", param);

            return DataSetToFill;
        }

        public DataSet GetMatGroup_DefectType(string argMatGroup1Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMatGroup_DefectType", param);

            return DataSetToFill;
        }

        private MatGroup_DefectType objCreateMatGroup_DefectType(DataRow dr)
        {
            MatGroup_DefectType tMaterialGroup_DefectType = new MatGroup_DefectType();

            tMaterialGroup_DefectType.SetObjectInfo(dr);

            return tMaterialGroup_DefectType;

        }

        public ICollection<ErrorHandler> SaveMatGroup_DefectType(MatGroup_DefectType argMaterialGroup_DefectType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsMatGroup_DefectTypeExists(argMaterialGroup_DefectType.MatGroup1Code, argMaterialGroup_DefectType.DefectTypeCode, argMaterialGroup_DefectType.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertMatGroup_DefectType(argMaterialGroup_DefectType, da, lstErr);
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
                    UpdateMatGroup_DefectType(argMaterialGroup_DefectType, da, lstErr);
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

        public ICollection<ErrorHandler> SaveMatGroup_DefectType(ICollection<MatGroup_DefectType> colGetMatGroup_DefectType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (MatGroup_DefectType argMatGroup_DefectType in colGetMatGroup_DefectType)
                {
                    if (argMatGroup_DefectType.IsDeleted == 0)
                    {
                        if (blnIsMatGroup_DefectTypeExists(argMatGroup_DefectType.MatGroup1Code, argMatGroup_DefectType.DefectTypeCode, argMatGroup_DefectType.ClientCode, da) == false)
                        {
                            InsertMatGroup_DefectType(argMatGroup_DefectType, da, lstErr);
                        }
                        else
                        {
                            UpdateMatGroup_DefectType(argMatGroup_DefectType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMatGroup_DefectType(argMatGroup_DefectType.MatGroup1Code, argMatGroup_DefectType.DefectTypeCode, argMatGroup_DefectType.ClientCode, argMatGroup_DefectType.IsDeleted);
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

        public void InsertMatGroup_DefectType(MatGroup_DefectType argMaterialGroup_DefectType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@MatGroup1Code", argMaterialGroup_DefectType.MatGroup1Code);
            param[1] = new SqlParameter("@DefectTypeCode", argMaterialGroup_DefectType.DefectTypeCode);
            param[2] = new SqlParameter("@MaterialCode", argMaterialGroup_DefectType.MaterialCode);
            param[3] = new SqlParameter("@ClientCode", argMaterialGroup_DefectType.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argMaterialGroup_DefectType.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argMaterialGroup_DefectType.ModifiedBy);

            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertMatGroup_DefectType", param);


            string strMessage = Convert.ToString(param[7].Value);
            string strType = Convert.ToString(param[6].Value);
            string strRetValue = Convert.ToString(param[8].Value);


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

        public void UpdateMatGroup_DefectType(MatGroup_DefectType argMaterialGroup_DefectType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@MatGroup1Code", argMaterialGroup_DefectType.MatGroup1Code);
            param[1] = new SqlParameter("@DefectTypeCode", argMaterialGroup_DefectType.DefectTypeCode);
            param[2] = new SqlParameter("@MaterialCode", argMaterialGroup_DefectType.MaterialCode);
            param[3] = new SqlParameter("@ClientCode", argMaterialGroup_DefectType.ClientCode);
            param[4] = new SqlParameter("@CreatedBy", argMaterialGroup_DefectType.CreatedBy);
            param[5] = new SqlParameter("@ModifiedBy", argMaterialGroup_DefectType.ModifiedBy);

            param[6] = new SqlParameter("@Type", SqlDbType.Char);
            param[6].Size = 1;
            param[6].Direction = ParameterDirection.Output;

            param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[7].Size = 255;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[8].Size = 20;
            param[8].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateMatGroup_DefectType", param);


            string strMessage = Convert.ToString(param[7].Value);
            string strType = Convert.ToString(param[6].Value);
            string strRetValue = Convert.ToString(param[8].Value);


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

        public ICollection<ErrorHandler> DeleteMatGroup_DefectType(string argMatGroup1Code, string argDefectTypeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
                param[1] = new SqlParameter("@DefectTypeCode", argDefectTypeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteMatGroup_DefectType", param);


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

        public bool blnIsMatGroup_DefectTypeExists(string argMatGroup1Code, string argDefectTypeCode, string argClientCode)
        {
            bool IsMaterialGroup_DefectTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetMatGroup_DefectType(argMatGroup1Code, argDefectTypeCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterialGroup_DefectTypeExists = true;
            }
            else
            {
                IsMaterialGroup_DefectTypeExists = false;
            }
            return IsMaterialGroup_DefectTypeExists;
        }

        public bool blnIsMatGroup_DefectTypeExists(string argMatGroup1Code, string argDefectTypeCode, string argClientCode, DataAccess da)
        {
            bool IsMaterialGroup_DefectTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetMatGroup_DefectType(argMatGroup1Code, argDefectTypeCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterialGroup_DefectTypeExists = true;
            }
            else
            {
                IsMaterialGroup_DefectTypeExists = false;
            }
            return IsMaterialGroup_DefectTypeExists;
        }
    }
}