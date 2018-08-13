
//Created On :: 19, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class InBDeliveryMasterManager
    {
        const string InBDeliveryMasterTable = "InBDeliveryMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        InBDeliveryDetailManager   objInBDeliveryDetailManager=new InBDeliveryDetailManager();
        InBDeliverySerializeDetailManager objInBDeliverySerializeDetailManager = new InBDeliverySerializeDetailManager();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public InBDeliveryMaster objGetInBDeliveryMaster(string argInBDeliveryDocCode, string argClientCode)
        {
            InBDeliveryMaster argInBDeliveryMaster = new InBDeliveryMaster();
            DataSet DataSetToFill = new DataSet();

            if (argInBDeliveryDocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetInBDeliveryMaster(argInBDeliveryDocCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argInBDeliveryMaster = this.objCreateInBDeliveryMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argInBDeliveryMaster;
        }


        public ICollection<InBDeliveryMaster> colGetInBDeliveryMaster(string argInBDeliveryDocTypeCode,string argClientCode)
        {
            List<InBDeliveryMaster> lst = new List<InBDeliveryMaster>();
            DataSet DataSetToFill = new DataSet();
            InBDeliveryMaster tInBDeliveryMaster = new InBDeliveryMaster();

            DataSetToFill = this.GetInBDeliveryMasterList(argInBDeliveryDocTypeCode,argClientCode, Convert.ToDateTime("1900-01-01"), Convert.ToDateTime("1900-01-01"));

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateInBDeliveryMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetInBDeliveryMaster(string argInBDeliveryDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetInBDeliveryMaster4ID", param);

            return DataSetToFill;
        }



        public DataSet GetInBDeliveryMaster(string argInBDeliveryDocCode, string argClientCode, DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetInBDeliveryMaster4ID", param);

            return DataSetToFill;
        }


        public DataSet GetInBDeliveryMasterList(string argInBDeliveryDocTypeCode, string argClientCode, DateTime argStartDate, DateTime argEndDate)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@InBDeliveryDocTypeCode", argInBDeliveryDocTypeCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            param[2] = new SqlParameter("@StartDate", argStartDate);
            param[3] = new SqlParameter("@EndDate", argEndDate);

            DataSetToFill = da.FillDataSet("SP_GetInBDeliveryMaster", param);
            return DataSetToFill;
        }


        private InBDeliveryMaster objCreateInBDeliveryMaster(DataRow dr)
        {
            InBDeliveryMaster tInBDeliveryMaster = new InBDeliveryMaster();

            tInBDeliveryMaster.SetObjectInfo(dr);

            return tInBDeliveryMaster;

        }


        public ICollection<ErrorHandler> SaveInBDeliveryMaster(InBDeliveryMaster argInBDeliveryMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsInBDeliveryMasterExists(argInBDeliveryMaster.InBDeliveryDocCode, argInBDeliveryMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertInBDeliveryMaster(argInBDeliveryMaster, da, lstErr);
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
                    UpdateInBDeliveryMaster(argInBDeliveryMaster, da, lstErr);
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

        public ICollection<ErrorHandler> SaveInBDeliveryMaster(InBDeliveryMaster argInBDeliveryMaster, ICollection<InBDeliveryDetail> colGetInBDeliveryDetail, ICollection<InBDeliverySerializeDetail> colGetInBDeliverySerializeDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();

            string strretValue = "";
            try
            {
                // if (blnCheckBasicRules(argSalesOrder, lstErr) == true)
                // {

                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                if (blnIsInBDeliveryMasterExists(argInBDeliveryMaster.InBDeliveryDocCode, argInBDeliveryMaster.ClientCode,da) == false)
                {
                    strretValue = InsertInBDeliveryMaster(argInBDeliveryMaster, da, lstErr);
                }
                else
                {
                    strretValue = UpdateInBDeliveryMaster(argInBDeliveryMaster, da, lstErr);
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
                    if (colGetInBDeliveryDetail.Count > 0)
                    {
                        foreach (InBDeliveryDetail argInBDeliveryDetail in colGetInBDeliveryDetail)
                        {
                            argInBDeliveryDetail.InBDeliveryDocCode = Convert.ToString(strretValue);

                            if (argInBDeliveryDetail.IsDeleted == 0)
                            {

                                objInBDeliveryDetailManager.SaveInBDeliveryDetail(argInBDeliveryDetail, da, lstErr);
                            }
                            else
                            {
                                objInBDeliveryDetailManager.DeleteInBDeliveryDetail(argInBDeliveryDetail.InBDeliveryDocCode, argInBDeliveryDetail.ItemNo, argInBDeliveryDetail.ClientCode, 0, da, lstErr);
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

                    if (colGetInBDeliverySerializeDetail.Count > 0)
                    {
                        foreach (InBDeliverySerializeDetail argInBDeliverySerializeDetail in colGetInBDeliverySerializeDetail)
                        {
                            argInBDeliverySerializeDetail.InBDeliveryDocCode = strretValue.Trim();
                            if (argInBDeliverySerializeDetail.IsDeleted == 0)
                            {
                                objInBDeliverySerializeDetailManager.SaveInBDeliverySerializeDetail(argInBDeliverySerializeDetail, da, lstErr);
                            }
                            else
                            {
                                objInBDeliverySerializeDetailManager.DeleteInBDeliverySerializeDetail(argInBDeliverySerializeDetail.InBDeliveryDocCode, argInBDeliverySerializeDetail.ItemNo, argInBDeliverySerializeDetail.SerialNo, argInBDeliverySerializeDetail.ClientCode,0, da, lstErr);
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

        public string  InsertInBDeliveryMaster(InBDeliveryMaster argInBDeliveryMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[21];
            param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryMaster.InBDeliveryDocCode);
            param[1] = new SqlParameter("@InBDeliveryDocTypeCode", argInBDeliveryMaster.InBDeliveryDocTypeCode);
            param[2] = new SqlParameter("@ReceiptDate", argInBDeliveryMaster.ReceiptDate);
            param[3] = new SqlParameter("@VendorCode", argInBDeliveryMaster.VendorCode);
            param[4] = new SqlParameter("@SourcePlantCode", argInBDeliveryMaster.SourcePlantCode);
            param[5] = new SqlParameter("@ReceiptStatus", argInBDeliveryMaster.ReceiptStatus);
            param[6] = new SqlParameter("@PostingDate", argInBDeliveryMaster.PostingDate);
            param[7] = new SqlParameter("@TotalQty", argInBDeliveryMaster.TotalQty);
            param[8] = new SqlParameter("@TotalAmt", argInBDeliveryMaster.TotalAmt);
            param[9] = new SqlParameter("@SAPTranID", argInBDeliveryMaster.SAPTranID);
            param[10] = new SqlParameter("@IsSAPPosted", argInBDeliveryMaster.IsSAPPosted);
            param[11] = new SqlParameter("@IsPGRDone", argInBDeliveryMaster.IsPGRDone);
            param[12] = new SqlParameter("@MovementCode", argInBDeliveryMaster.MovementCode);
            param[13] = new SqlParameter("@MovementDate", argInBDeliveryMaster.MovementDate);
            param[14] = new SqlParameter("@PlantCode", argInBDeliveryMaster.PlantCode);
            param[15] = new SqlParameter("@ClientCode", argInBDeliveryMaster.ClientCode);
            param[16] = new SqlParameter("@CreatedBy", argInBDeliveryMaster.CreatedBy);
            param[17] = new SqlParameter("@ModifiedBy", argInBDeliveryMaster.ModifiedBy);

            param[18] = new SqlParameter("@Type", SqlDbType.Char);
            param[18].Size = 1;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[19].Size = 255;
            param[19].Direction = ParameterDirection.Output;

            param[20] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[20].Size = 20;
            param[20].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertInBDeliveryMaster", param);


            string strMessage = Convert.ToString(param[19].Value);
            string strType = Convert.ToString(param[18].Value);
            string strRetValue = Convert.ToString(param[20].Value);


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
            return(strRetValue);

        }


        public string UpdateInBDeliveryMaster(InBDeliveryMaster argInBDeliveryMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[21];
            param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryMaster.InBDeliveryDocCode);
            param[1] = new SqlParameter("@InBDeliveryDocTypeCode", argInBDeliveryMaster.InBDeliveryDocTypeCode);
            param[2] = new SqlParameter("@ReceiptDate", argInBDeliveryMaster.ReceiptDate);
            param[3] = new SqlParameter("@VendorCode", argInBDeliveryMaster.VendorCode);
            param[4] = new SqlParameter("@SourcePlantCode", argInBDeliveryMaster.SourcePlantCode);
            param[5] = new SqlParameter("@ReceiptStatus", argInBDeliveryMaster.ReceiptStatus);
            param[6] = new SqlParameter("@PostingDate", argInBDeliveryMaster.PostingDate);
            param[7] = new SqlParameter("@TotalQty", argInBDeliveryMaster.TotalQty);
            param[8] = new SqlParameter("@TotalAmt", argInBDeliveryMaster.TotalAmt);
            param[9] = new SqlParameter("@SAPTranID", argInBDeliveryMaster.SAPTranID);
            param[10] = new SqlParameter("@IsSAPPosted", argInBDeliveryMaster.IsSAPPosted);
            param[11] = new SqlParameter("@IsPGRDone", argInBDeliveryMaster.IsPGRDone);
            param[12] = new SqlParameter("@MovementCode", argInBDeliveryMaster.MovementCode);
            param[13] = new SqlParameter("@MovementDate", argInBDeliveryMaster.MovementDate);
            param[14] = new SqlParameter("@PlantCode", argInBDeliveryMaster.PlantCode);
            param[15] = new SqlParameter("@ClientCode", argInBDeliveryMaster.ClientCode);
            param[16] = new SqlParameter("@CreatedBy", argInBDeliveryMaster.CreatedBy);
            param[17] = new SqlParameter("@ModifiedBy", argInBDeliveryMaster.ModifiedBy);
  
            param[18] = new SqlParameter("@Type", SqlDbType.Char);
            param[18].Size = 1;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[19].Size = 255;
            param[19].Direction = ParameterDirection.Output;

            param[20] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[20].Size = 20;
            param[20].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateInBDeliveryMaster", param);


            string strMessage = Convert.ToString(param[19].Value);
            string strType = Convert.ToString(param[18].Value);
            string strRetValue = Convert.ToString(param[20].Value);


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
            return(strRetValue);

        }


        public ICollection<ErrorHandler> DeleteInBDeliveryMaster(string argInBDeliveryDocCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDocCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteInBDeliveryMaster", param);


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


        public bool blnIsInBDeliveryMasterExists(string argInBDeliveryDocCode, string argClientCode)
        {
            bool IsInBDeliveryMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetInBDeliveryMaster(argInBDeliveryDocCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsInBDeliveryMasterExists = true;
            }
            else
            {
                IsInBDeliveryMasterExists = false;
            }
            return IsInBDeliveryMasterExists;
        }

        public bool blnIsInBDeliveryMasterExists(string argInBDeliveryDocCode, string argClientCode, DataAccess da)
        {
            bool IsInBDeliveryMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetInBDeliveryMaster(argInBDeliveryDocCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsInBDeliveryMasterExists = true;
            }
            else
            {
                IsInBDeliveryMasterExists = false;
            }
            return IsInBDeliveryMasterExists;
        }
    }
}