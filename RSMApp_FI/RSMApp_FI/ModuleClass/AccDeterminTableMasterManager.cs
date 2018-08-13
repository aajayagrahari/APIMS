
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
    public class AccDeterminTableMasterManager
    {
        const string AccDeterminTableMasterTable = "AccDeterminTableMaster";
        AccDeterminTableDetailManager objAccDeterminTableDetailManager = new AccDeterminTableDetailManager();

       //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public AccDeterminTableMaster objGetAccDeterminTableMaster(string argAccDMTableCode, string argClientCode)
        {
            AccDeterminTableMaster argAccDeterminTableMaster = new AccDeterminTableMaster();
            DataSet DataSetToFill = new DataSet();

            if (argAccDMTableCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetAccDeterminTableMaster(argAccDMTableCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argAccDeterminTableMaster = this.objCreateAccDeterminTableMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argAccDeterminTableMaster;
        }


        public ICollection<AccDeterminTableMaster> colGetAccDeterminTableMaster(string argClientCode)
        {
            List<AccDeterminTableMaster> lst = new List<AccDeterminTableMaster>();
            DataSet DataSetToFill = new DataSet();
            AccDeterminTableMaster tAccDeterminTableMaster = new AccDeterminTableMaster();

            DataSetToFill = this.GetAccDeterminTableMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateAccDeterminTableMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetAccDeterminTableMaster(string argAccDMTableCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AccDMTableCode", argAccDMTableCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAccDeterminTableMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetAccDeterminTableMaster(string argAccDMTableCode, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@AccDMTableCode", argAccDMTableCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetAccDeterminTableMaster4ID", param);

            return DataSetToFill;
        }


        public DataSet GetAccDeterminTableMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAccDeterminTableMaster",param);
            return DataSetToFill;
        }


        private AccDeterminTableMaster objCreateAccDeterminTableMaster(DataRow dr)
        {
            AccDeterminTableMaster tAccDeterminTableMaster = new AccDeterminTableMaster();

            tAccDeterminTableMaster.SetObjectInfo(dr);

            return tAccDeterminTableMaster;

        }


        public ICollection<ErrorHandler> SaveAccDeterminTableMaster(AccDeterminTableMaster argAccDeterminTableMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsAccDeterminTableMasterExists(argAccDeterminTableMaster.AccDMTableCode, argAccDeterminTableMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertAccDeterminTableMaster(argAccDeterminTableMaster, da, lstErr);
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
                    UpdateAccDeterminTableMaster(argAccDeterminTableMaster, da, lstErr);
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

        public ICollection<ErrorHandler> SaveAccDeterminTableMaster(AccDeterminTableMaster argAccDeterminTableMaster, DataTable dtAccDeterminTable_Detail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            string strretValue = "";
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsAccDeterminTableMasterExists(argAccDeterminTableMaster.AccDMTableCode, argAccDeterminTableMaster.ClientCode, da) == false)
                {
                    strretValue = InsertAccDeterminTableMaster(argAccDeterminTableMaster, da, lstErr);
                }
                else
                {
                    strretValue = UpdateAccDeterminTableMaster(argAccDeterminTableMaster, da, lstErr);
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
                    if (dtAccDeterminTable_Detail.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtAccDeterminTable_Detail.Rows)
                        {
                            if (Convert.ToInt32(dr["IsDeleted"]) == 0)
                            {
                                AccDeterminTableDetail objAccDeterminTableDetail = new AccDeterminTableDetail();

                                objAccDeterminTableDetail.AccDMTableCode = strretValue.Trim();
                                objAccDeterminTableDetail.FieldName = Convert.ToString(dr["FieldName"]).Trim();
                                objAccDeterminTableDetail.MasterTableName = Convert.ToString(dr["MasterTableName"]).Trim();
                                objAccDeterminTableDetail.MasterTableField = Convert.ToString(dr["MasterTableField"]).Trim();
                                objAccDeterminTableDetail.ClientCode = Convert.ToString(dr["ClientCode"]).Trim();
                                objAccDeterminTableDetail.CreatedBy = Convert.ToString(dr["CreatedBy"]).Trim();
                                objAccDeterminTableDetail.ModifiedBy = Convert.ToString(dr["ModifiedBy"]).Trim();

                                objAccDeterminTableDetailManager.SaveAccDeterminTableDetail(objAccDeterminTableDetail, da, lstErr);
                            }
                            else
                            {
                                objAccDeterminTableDetailManager.DeleteAccDeterminTableDetail(strretValue.ToString().Trim(), Convert.ToString(dr["FieldName"]).Trim(), Convert.ToString(dr["ClientCode"]).Trim(), 1, da);
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



                    //}
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

        public string InsertAccDeterminTableMaster(AccDeterminTableMaster argAccDeterminTableMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@AccDMTableCode", argAccDeterminTableMaster.AccDMTableCode);
            param[1] = new SqlParameter("@TableDescription", argAccDeterminTableMaster.TableDescription);
            param[2] = new SqlParameter("@TableSequence", argAccDeterminTableMaster.TableSequence);
            param[3] = new SqlParameter("@TableType", argAccDeterminTableMaster.TableType);
            param[4] = new SqlParameter("@ClientCode", argAccDeterminTableMaster.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argAccDeterminTableMaster.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argAccDeterminTableMaster.ModifiedBy);
   
            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertAccDeterminTableMaster", param);


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


        public string UpdateAccDeterminTableMaster(AccDeterminTableMaster argAccDeterminTableMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[10];
            param[0] = new SqlParameter("@AccDMTableCode", argAccDeterminTableMaster.AccDMTableCode);
            param[1] = new SqlParameter("@TableDescription", argAccDeterminTableMaster.TableDescription);
            param[2] = new SqlParameter("@TableSequence", argAccDeterminTableMaster.TableSequence);
            param[3] = new SqlParameter("@TableType", argAccDeterminTableMaster.TableType);
            param[4] = new SqlParameter("@ClientCode", argAccDeterminTableMaster.ClientCode);
            param[5] = new SqlParameter("@CreatedBy", argAccDeterminTableMaster.CreatedBy);
            param[6] = new SqlParameter("@ModifiedBy", argAccDeterminTableMaster.ModifiedBy);

            param[7] = new SqlParameter("@Type", SqlDbType.Char);
            param[7].Size = 1;
            param[7].Direction = ParameterDirection.Output;

            param[8] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[8].Size = 255;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[9].Size = 20;
            param[9].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateAccDeterminTableMaster", param);


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


        public ICollection<ErrorHandler> DeleteAccDeterminTableMaster(string argAccDMTableCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@AccDMTableCode", argAccDMTableCode);
                param[1] = new SqlParameter("@ClientCode", argClientCode);
                param[2] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[3] = new SqlParameter("@Type", SqlDbType.Char);
                param[3].Size = 1;
                param[3].Direction = ParameterDirection.Output;

                param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[4].Size = 255;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[5].Size = 20;
                param[5].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteAccDeterminTableMaster", param);


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


        public bool blnIsAccDeterminTableMasterExists(string argAccDMTableCode, string argClientCode)
        {
            bool IsAccDeterminTableMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetAccDeterminTableMaster(argAccDMTableCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAccDeterminTableMasterExists = true;
            }
            else
            {
                IsAccDeterminTableMasterExists = false;
            }
            return IsAccDeterminTableMasterExists;
        }

        public bool blnIsAccDeterminTableMasterExists(string argAccDMTableCode, string argClientCode,DataAccess da)
        {
            bool IsAccDeterminTableMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetAccDeterminTableMaster(argAccDMTableCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsAccDeterminTableMasterExists = true;
            }
            else
            {
                IsAccDeterminTableMasterExists = false;
            }
            return IsAccDeterminTableMasterExists;
        }
    }
}