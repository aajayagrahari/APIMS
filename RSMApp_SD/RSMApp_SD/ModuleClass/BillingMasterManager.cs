
//Created On :: 17, September, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class BillingMasterManager
    {
        const string BillingMasterTable = "BillingMaster";
        BillingDetailManager objBillingDetailManager = new BillingDetailManager();
        BillingPartnerManager objBillingPartnerManager = new BillingPartnerManager();

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public BillingMaster objGetBillingMaster(string argBillingDocCode, string argClientCode)
        {
            BillingMaster argBillingMaster = new BillingMaster();
            DataSet DataSetToFill = new DataSet();

            if (argBillingDocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetBillingMaster(argBillingDocCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argBillingMaster = this.objCreateBillingMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argBillingMaster;
        }

        public ICollection<BillingMaster> colGetBillingMaster(string argDOCType, string argClientCode)
        {
            List<BillingMaster> lst = new List<BillingMaster>();
            DataSet DataSetToFill = new DataSet();
            BillingMaster tBillingMaster = new BillingMaster();

            DataSetToFill = this.GetBillingMaster(argDOCType, argClientCode, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"));

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateBillingMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetBillingMaster(string argBillingDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@BillingDocCode", argBillingDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBillingMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetBillingMaster(string argBillingDocCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@BillingDocCode", argBillingDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetBillingMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetBillingMaster(string argDocType, string argClientCode, DateTime argStartDate, DateTime argEndDate)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@DocType", argDocType);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            param[2] = new SqlParameter("@StartDate", argStartDate);
            param[3] = new SqlParameter("@EndDate", argEndDate);

            DataSetToFill = da.FillDataSet("SP_GetBillingMaster", param);
            return DataSetToFill;
        }

        
        private BillingMaster objCreateBillingMaster(DataRow dr)
        {
            BillingMaster tBillingMaster = new BillingMaster();

            tBillingMaster.SetObjectInfo(dr);

            return tBillingMaster;

        }
        
        //public ICollection<ErrorHandler> SaveBillingMaster(BillingMaster argBillingMaster)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsBillingMasterExists(argBillingMaster.BillingDocCode, argBillingMaster.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertBillingMaster(argBillingMaster, da, lstErr);
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
        //            UpdateBillingMaster(argBillingMaster, da, lstErr);
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

        public ICollection<ErrorHandler> SaveBillingMaster(BillingMaster argBillingMaster, DataTable dtBillingPartner, ICollection<BillingDetail> colBillingDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();

            string strretValue = "";
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsBillingMasterExists(argBillingMaster.BillingDocCode, argBillingMaster.ClientCode, da) == false)
                {
                    strretValue = InsertBillingMaster(argBillingMaster, da, lstErr);
                }
                else
                {
                    strretValue = UpdateBillingMaster(argBillingMaster, da, lstErr);
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
                    if (dtBillingPartner.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtBillingPartner.Rows)
                        {
                            if (Convert.ToInt32(dr["IsDeleted"]) == 0)
                            {

                                BillingPartner objBillingPartner = new BillingPartner();

                                objBillingPartner.BillingDocCode = strretValue.Trim();
                                objBillingPartner.PFunctionCode = Convert.ToString(dr["PFunctionCode"]).Trim();
                                objBillingPartner.CustomerCode = Convert.ToString(dr["CustomerCode"]).Trim();
                                objBillingPartner.PartnerType = Convert.ToString(dr["PartnerType"]).Trim();
                                objBillingPartner.ClientCode = Convert.ToString(dr["ClientCode"]).Trim();
                                objBillingPartner.CreatedBy = Convert.ToString(dr["CreatedBy"]).Trim();
                                objBillingPartner.ModifiedBy = Convert.ToString(dr["ModifiedBy"]).Trim();

                                objBillingPartnerManager.SaveBillingPartner(objBillingPartner, da, lstErr);
                            }
                            else
                            {
                                objBillingPartnerManager.DeleteBillingPartner(strretValue.Trim(), Convert.ToString(dr["PFunctionCode"]).Trim(), Convert.ToString(dr["ClientCode"]).Trim(), 1, da, lstErr);
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

                    if (colBillingDetail.Count > 0)
                    {
                        foreach (BillingDetail argBillingDetail in colBillingDetail)
                        {
                            argBillingDetail.BillingDocCode = Convert.ToString(strretValue);
                            if (argBillingDetail.IsDeleted == 0)
                            {
                                objBillingDetailManager.SaveBillingDetail(argBillingDetail, da, lstErr);
                            }
                            else
                            {
                                objBillingDetailManager.DeleteBillingDetail(argBillingDetail.BillingDocCode, argBillingDetail.ItemNo, 1, argBillingDetail.ClientCode, da, lstErr);
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
                //}
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

        public string InsertBillingMaster(BillingMaster argBillingMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@BillingDocCode", argBillingMaster.BillingDocCode);
            param[1] = new SqlParameter("@BillingDocTypeCode", argBillingMaster.BillingDocTypeCode);
            param[2] = new SqlParameter("@BillingDate", argBillingMaster.BillingDate);
            param[3] = new SqlParameter("@CustomerCode", argBillingMaster.CustomerCode);
            param[4] = new SqlParameter("@BillToParty", argBillingMaster.BillToParty);
            param[5] = new SqlParameter("@TotalQty", argBillingMaster.TotalQty);
            param[6] = new SqlParameter("@GrossAmt", argBillingMaster.GrossAmt);
            param[7] = new SqlParameter("@NetAmt", argBillingMaster.NetAmt);
            param[8] = new SqlParameter("@SAPTranID", argBillingMaster.SAPTranID);
            param[9] = new SqlParameter("@IsSAPPosted", argBillingMaster.IsSAPPosted);
            param[10] = new SqlParameter("@ClientCode", argBillingMaster.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argBillingMaster.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argBillingMaster.ModifiedBy);


            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertBillingMaster", param);


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
            return strRetValue;

        }
        
        public string UpdateBillingMaster(BillingMaster argBillingMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[16];
            param[0] = new SqlParameter("@BillingDocCode", argBillingMaster.BillingDocCode);
            param[1] = new SqlParameter("@BillingDocTypeCode", argBillingMaster.BillingDocTypeCode);
            param[2] = new SqlParameter("@BillingDate", argBillingMaster.BillingDate);
            param[3] = new SqlParameter("@CustomerCode", argBillingMaster.CustomerCode);
            param[4] = new SqlParameter("@BillToParty", argBillingMaster.BillToParty);
            param[5] = new SqlParameter("@TotalQty", argBillingMaster.TotalQty);
            param[6] = new SqlParameter("@GrossAmt", argBillingMaster.GrossAmt);
            param[7] = new SqlParameter("@NetAmt", argBillingMaster.NetAmt);
            param[8] = new SqlParameter("@SAPTranID", argBillingMaster.SAPTranID);
            param[9] = new SqlParameter("@IsSAPPosted", argBillingMaster.IsSAPPosted);
            param[10] = new SqlParameter("@ClientCode", argBillingMaster.ClientCode);
            param[11] = new SqlParameter("@CreatedBy", argBillingMaster.CreatedBy);
            param[12] = new SqlParameter("@ModifiedBy", argBillingMaster.ModifiedBy);


            param[13] = new SqlParameter("@Type", SqlDbType.Char);
            param[13].Size = 1;
            param[13].Direction = ParameterDirection.Output;

            param[14] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[14].Size = 255;
            param[14].Direction = ParameterDirection.Output;

            param[15] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[15].Size = 20;
            param[15].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateBillingMaster", param);


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
            return strRetValue;

        }
        
        public ICollection<ErrorHandler> DeleteBillingMaster(string argBillingDocCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@BillingDocCode", argBillingDocCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteBillingMaster", param);


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
        
        public bool blnIsBillingMasterExists(string argBillingDocCode, string argClientCode)
        {
            bool IsBillingMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetBillingMaster(argBillingDocCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsBillingMasterExists = true;
            }
            else
            {
                IsBillingMasterExists = false;
            }
            return IsBillingMasterExists;
        }

        public bool blnIsBillingMasterExists(string argBillingDocCode, string argClientCode, DataAccess da)
        {
            bool IsBillingMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetBillingMaster(argBillingDocCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsBillingMasterExists = true;
            }
            else
            {
                IsBillingMasterExists = false;
            }
            return IsBillingMasterExists;
        }
    }
}