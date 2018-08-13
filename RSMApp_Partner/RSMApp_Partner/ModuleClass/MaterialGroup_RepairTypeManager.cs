
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
    public class MaterialGroup_RepairTypeManager
    {
        const string MaterialGroup_RepairTypeTable = "MaterialGroup_RepairType";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public MaterialGroup_RepairType objGetMaterialGroup_RepairType(string argMaterialGroup1Code, string argDefectTypeCode, string argRepairTypeCode, string argClientCode)
        {
            MaterialGroup_RepairType argMaterialGroup_RepairType = new MaterialGroup_RepairType();
            DataSet DataSetToFill = new DataSet();

            if (argMaterialGroup1Code.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argDefectTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argRepairTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetMaterialGroup_RepairType(argMaterialGroup1Code, argDefectTypeCode, argRepairTypeCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argMaterialGroup_RepairType = this.objCreateMaterialGroup_RepairType((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argMaterialGroup_RepairType;
        }

        public ICollection<MaterialGroup_RepairType> colGetMaterialGroup_RepairType(string argMaterialGroup1Code, string argClientCode)
        {
            List<MaterialGroup_RepairType> lst = new List<MaterialGroup_RepairType>();
            DataSet DataSetToFill = new DataSet();
            MaterialGroup_RepairType tMaterialGroup_RepairType = new MaterialGroup_RepairType();

            DataSetToFill = this.GetMaterialGroup_RepairType(argMaterialGroup1Code,argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMaterialGroup_RepairType(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetMaterialGroup_RepairType(string argMatGroup1Code, string argDefectTypeCode, string argRepairTypeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@DefectTypeCode", argDefectTypeCode);
            param[2] = new SqlParameter("@RepairTypeCode", argRepairTypeCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMatGroup_RepairType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetMaterialGroup_RepairType(string argMatGroup1Code, string argDefectTypeCode, string argRepairTypeCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@DefectTypeCode", argDefectTypeCode);
            param[2] = new SqlParameter("@RepairTypeCode", argRepairTypeCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMatGroup_RepairType4ID", param);
            return DataSetToFill;
        }

        public DataSet GetMaterialGroup_RepairType(string argMatGroup1Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMatGroup_RepairType", param);
            return DataSetToFill;
        }

        private MaterialGroup_RepairType objCreateMaterialGroup_RepairType(DataRow dr)
        {
            MaterialGroup_RepairType tMaterialGroup_RepairType = new MaterialGroup_RepairType();
            tMaterialGroup_RepairType.SetObjectInfo(dr);
            return tMaterialGroup_RepairType;
        }

        public ICollection<ErrorHandler> SaveMaterialGroup_RepairType(MaterialGroup_RepairType argMaterialGroup_RepairType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsMaterialGroup_RepairTypeExists(argMaterialGroup_RepairType.MatGroup1Code, argMaterialGroup_RepairType.DefectTypeCode, argMaterialGroup_RepairType.RepairTypeCode, argMaterialGroup_RepairType.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertMaterialGroup_RepairType(argMaterialGroup_RepairType, da, lstErr);
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
                    UpdateMaterialGroup_RepairType(argMaterialGroup_RepairType, da, lstErr);
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

        public ICollection<ErrorHandler> SaveMaterialGroup_RepairType(ICollection<MaterialGroup_RepairType> colGetMaterialGroup_RepairType)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (MaterialGroup_RepairType argMaterialGroup_RepairType in colGetMaterialGroup_RepairType)
                {
                    if (argMaterialGroup_RepairType.IsDeleted == 0)
                    {
                        if (blnIsMaterialGroup_RepairTypeExists(argMaterialGroup_RepairType.MatGroup1Code, argMaterialGroup_RepairType.DefectTypeCode, argMaterialGroup_RepairType.RepairTypeCode, argMaterialGroup_RepairType.ClientCode, da) == false)
                        {
                            InsertMaterialGroup_RepairType(argMaterialGroup_RepairType, da, lstErr);
                        }
                        else
                        {
                            UpdateMaterialGroup_RepairType(argMaterialGroup_RepairType, da, lstErr);
                        }
                    }
                    else
                    {
                        DeleteMaterialGroup_RepairType(argMaterialGroup_RepairType.MatGroup1Code, argMaterialGroup_RepairType.RepairTypeCode, argMaterialGroup_RepairType.DefectTypeCode, argMaterialGroup_RepairType.ClientCode, argMaterialGroup_RepairType.IsDeleted);
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

        public void InsertMaterialGroup_RepairType(MaterialGroup_RepairType argMaterialGroup_RepairType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@MatGroup1Code", argMaterialGroup_RepairType.MatGroup1Code);
            param[1] = new SqlParameter("@DefectTypeCode", argMaterialGroup_RepairType.DefectTypeCode);
            param[2] = new SqlParameter("@RepairTypeCode", argMaterialGroup_RepairType.RepairTypeCode);
            param[3] = new SqlParameter("@ServiceLevel", argMaterialGroup_RepairType.ServiceLevel);
            param[4] = new SqlParameter("@MaterialCode", argMaterialGroup_RepairType.MaterialCode);
            param[5] = new SqlParameter("@ClientCode", argMaterialGroup_RepairType.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argMaterialGroup_RepairType.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argMaterialGroup_RepairType.ModifiedBy);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertMatGroup_RepairType", param);

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

        public void UpdateMaterialGroup_RepairType(MaterialGroup_RepairType argMaterialGroup_RepairType, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@MatGroup1Code", argMaterialGroup_RepairType.MatGroup1Code);
            param[1] = new SqlParameter("@DefectTypeCode", argMaterialGroup_RepairType.DefectTypeCode);
            param[2] = new SqlParameter("@RepairTypeCode", argMaterialGroup_RepairType.RepairTypeCode);
            param[3] = new SqlParameter("@ServiceLevel", argMaterialGroup_RepairType.ServiceLevel);
            param[4] = new SqlParameter("@MaterialCode", argMaterialGroup_RepairType.MaterialCode);
            param[5] = new SqlParameter("@ClientCode", argMaterialGroup_RepairType.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argMaterialGroup_RepairType.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argMaterialGroup_RepairType.ModifiedBy);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateMatGroup_RepairType", param);

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

        public ICollection<ErrorHandler> DeleteMaterialGroup_RepairType(string argMatGroup1Code, string argRepairTypeCode, string argDefectTypeCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
                param[1] = new SqlParameter("@DefectTypeCode", argDefectTypeCode);
                param[2] = new SqlParameter("@RepairTypeCode", argRepairTypeCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteMatGroup_RepairType", param);

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

        public bool blnIsMaterialGroup_RepairTypeExists(string argMaterialGroup1Code, string argDefectTypeCode, string argRepairTypeCode, string argClientCode)
        {
            bool IsMaterialGroup_RepairTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterialGroup_RepairType(argMaterialGroup1Code, argDefectTypeCode, argRepairTypeCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterialGroup_RepairTypeExists = true;
            }
            else
            {
                IsMaterialGroup_RepairTypeExists = false;
            }
            return IsMaterialGroup_RepairTypeExists;
        }

        public bool blnIsMaterialGroup_RepairTypeExists(string argMaterialGroup1Code, string argDefectTypeCode, string argRepairTypeCode, string argClientCode, DataAccess da)
        {
            bool IsMaterialGroup_RepairTypeExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterialGroup_RepairType(argMaterialGroup1Code, argDefectTypeCode, argRepairTypeCode, argClientCode, da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterialGroup_RepairTypeExists = true;
            }
            else
            {
                IsMaterialGroup_RepairTypeExists = false;
            }
            return IsMaterialGroup_RepairTypeExists;
        }
    }
}