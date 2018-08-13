
//Created On :: 06, June, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SAP
{
    public class SAPProcessManager
    {
        const string SAPProcessTable = "SAPProcess";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        SAPParamManager ObjSAPParamManager = new SAPParamManager();
        SAPFieldManager objSAPFieldManager = new SAPFieldManager();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public SAPProcess objGetSAPProcess(string argProcessCode, string argClientCode)
        {
            SAPProcess argSAPProcess = new SAPProcess();
            DataSet DataSetToFill = new DataSet();

            if (argProcessCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetSAPProcess(argProcessCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSAPProcess = this.objCreateSAPProcess((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSAPProcess;
        }
        
        public ICollection<SAPProcess> colGetSAPProcess(string argClientCode)
        {
            List<SAPProcess> lst = new List<SAPProcess>();
            DataSet DataSetToFill = new DataSet();
            SAPProcess tSAPProcess = new SAPProcess();

            DataSetToFill = this.GetSAPProcess(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSAPProcess(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetSAPProcess(string argProcessCode, string argClientCode,DataAccess da)
        {
           // DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ProcessCode", argProcessCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSAPProcess4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetSAPProcess(string argProcessCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ProcessCode", argProcessCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSAPProcess4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetSAPProcess(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            
            DataSetToFill = da.FillDataSet("SP_GetSAPProcess",param);
            return DataSetToFill;
        }
        
        private SAPProcess objCreateSAPProcess(DataRow dr)
        {
            SAPProcess tSAPProcess = new SAPProcess();

            tSAPProcess.SetObjectInfo(dr);

            return tSAPProcess;

        }

        public ICollection<ErrorHandler> SaveSAPProcess(SAPProcess argSAPProcess, ICollection<SAPParam> colSAPParam,ICollection<SAPField> ColSAPField)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            string strretValue = "";
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsSAPProcessExists(argSAPProcess.ProcessCode, argSAPProcess.ClientCode, da) == false)
                {
                    strretValue = InsertSAPProcess(argSAPProcess, da, lstErr);
                }
                else
                {
                    strretValue = UpdateSAPProcess(argSAPProcess, da, lstErr);
                }

                foreach (ErrorHandler objerr in lstErr)
                {
                    if (objerr.Type == "E")
                    {
                        da.ROLLBACK_TRANSACTION();
                        return lstErr;
                    }

                    if (objerr.Type == "A")
                    {
                        da.ROLLBACK_TRANSACTION();
                        return lstErr;
                    }
                }

                if (strretValue != "")
                {
                    if (colSAPParam.Count > 0)
                    {
                        foreach (SAPParam argSAPParam in colSAPParam)
                        {
                            argSAPParam.ProcessCode = Convert.ToString(strretValue);

                            if (argSAPParam.IsDeleted == 0)
                            {

                                ObjSAPParamManager.SaveSAPParam(argSAPParam, da, lstErr);
                            }
                            else
                            {
                                ObjSAPParamManager.DeleteSAPParam(argSAPParam.ProcessCode, argSAPParam.ParamNo, argSAPParam.ClientCode, da, lstErr);
                            }

                            if (ColSAPField.Count  > 0 )
                            {
                                foreach (SAPField argSAPField in ColSAPField)
                                {
                                    if (argSAPParam.ProcessCode == argSAPField.ProcessCode && argSAPParam.ParamNo == argSAPField.ParamNo)
                                    {
                                        if (argSAPField.IsDeleted == 0)
                                        {
                                            objSAPFieldManager.SaveSAPField(argSAPField, da, lstErr);
                                        }
                                        else
                                        {
                                            objSAPFieldManager.DeleteSAPField(argSAPField.ProcessCode, argSAPField.ParamNo, argSAPField.FieldNo, argSAPField.ClientCode, argSAPField.IsDeleted, da, lstErr);
                                        }
                                    }
                                }
                            }
                        }

                        foreach (ErrorHandler objerr in lstErr)
                        {
                            if (objerr.Type == "E")
                            {
                                da.ROLLBACK_TRANSACTION();
                                return lstErr;
                            }

                            if (objerr.Type == "A")
                            {
                                da.ROLLBACK_TRANSACTION();
                                return lstErr;
                            }
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
        
        public ICollection<ErrorHandler> SaveSAPProcess(SAPProcess argSAPProcess)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsSAPProcessExists(argSAPProcess.ProcessCode, argSAPProcess.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertSAPProcess(argSAPProcess, da, lstErr);
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
                    UpdateSAPProcess(argSAPProcess, da, lstErr);
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
        
        public string InsertSAPProcess(SAPProcess argSAPProcess, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@ProcessCode", argSAPProcess.ProcessCode);
            param[1] = new SqlParameter("@ProcessName", argSAPProcess.ProcessName);
            param[2] = new SqlParameter("@Activity", argSAPProcess.Activity);
            param[3] = new SqlParameter("@BAPIName", argSAPProcess.BAPIName);
            param[4] = new SqlParameter("@ClientCode", argSAPProcess.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argSAPProcess.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argSAPProcess.ModifiedBy);
            
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSAPProcess", param);


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

            return strRetValue;

        }
        
        public string UpdateSAPProcess(SAPProcess argSAPProcess, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@ProcessCode", argSAPProcess.ProcessCode);
            param[1] = new SqlParameter("@ProcessName", argSAPProcess.ProcessName);
            param[2] = new SqlParameter("@Activity", argSAPProcess.Activity);
            param[3] = new SqlParameter("@BAPIName", argSAPProcess.BAPIName);
            param[4] = new SqlParameter("@ClientCode", argSAPProcess.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argSAPProcess.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argSAPProcess.ModifiedBy);

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSAPProcess", param);


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

            return strRetValue;

        }
        
        public ICollection<ErrorHandler> DeleteSAPProcess(string argProcessCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@ProcessCode", argProcessCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted",iIsDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteSAPProcess", param);


                string strMessage = Convert.ToString(param[4].Value);
                string strType = Convert.ToString(param[3].Value);
                string strRetValue = Convert.ToString(param[5].Value);


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
        
        public bool blnIsSAPProcessExists(string argProcessCode, string argClientCode)
        {
            bool IsSAPProcessExists = false;
            DataSet ds = new DataSet();
            ds = GetSAPProcess(argProcessCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSAPProcessExists = true;
            }
            else
            {
                IsSAPProcessExists = false;
            }
            return IsSAPProcessExists;
        }

        public bool blnIsSAPProcessExists(string argProcessCode, string argClientCode, DataAccess da)
        {
            bool IsSAPProcessExists = false;
            DataSet ds = new DataSet();
            ds = GetSAPProcess(argProcessCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSAPProcessExists = true;
            }
            else
            {
                IsSAPProcessExists = false;
            }
            return IsSAPProcessExists;
        }
    }
}