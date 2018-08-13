
//Created On :: 12, July, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class DeliveryMasterManager
    {
        const string DeliveryMasterTable = "DeliveryMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();

        ErrorHandler objErrorHandler = new ErrorHandler();
        DeliveryPartnerManager objDeliveryPartnerManager=new DeliveryPartnerManager();
        DeliveryDetailManager objDeliveryDetailManager = new DeliveryDetailManager();
        DeliverySerializeDetailManager objDeliverySerializeManager = new DeliverySerializeDetailManager();

        public DeliveryMaster objGetDeliveryMaster(string argDeliveryDocCode, string argClientCode)
        {
            DeliveryMaster argDeliveryMaster = new DeliveryMaster();
            DataSet DataSetToFill = new DataSet();

            if (argDeliveryDocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetDeliveryMaster(argDeliveryDocCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argDeliveryMaster = this.objCreateDeliveryMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argDeliveryMaster;
        }
        
        public ICollection<DeliveryMaster> colGetDeliveryMaster(string argDOCType, string argClientCode)
        {
            List<DeliveryMaster> lst = new List<DeliveryMaster>();
            DataSet DataSetToFill = new DataSet();
            DeliveryMaster tDeliveryMaster = new DeliveryMaster();

            DataSetToFill = this.GetDeliveryMaster( argDOCType, argClientCode, Convert.ToDateTime("1900-01-01"),  Convert.ToDateTime("1900-01-01"));

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDeliveryMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetDeliveryMaster(string argDeliveryDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDeliveryMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetDeliveryMaster(string argDeliveryDocCode, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetDeliveryMaster4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetDeliveryMaster(string argDocType, string argClientCode, DateTime argStartDate, DateTime argEndDate)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@DocType", argDocType);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            param[2] = new SqlParameter("@StartDate", argStartDate);
            param[3] = new SqlParameter("@EndDate", argEndDate);

            DataSetToFill = da.FillDataSet("SP_GetDeliveryMaster",param);
            return DataSetToFill;
        }

        public DataSet GetDeliveryMaster4BD(string argClientCode, string argBillingDocTypeCode, string argToDocType)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@BillingDocTypeCode", argBillingDocTypeCode);
            param[1] = new SqlParameter("@ToDocType", argToDocType);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDeliveryMaster4BD", param);

            return DataSetToFill;
        }

        private DeliveryMaster objCreateDeliveryMaster(DataRow dr)
        {
            DeliveryMaster tDeliveryMaster = new DeliveryMaster();

            tDeliveryMaster.SetObjectInfo(dr);

            return tDeliveryMaster;

        }

        public ICollection<ErrorHandler> SaveDeliveryMaster(DeliveryMaster argDeliveryMaster, DataTable dtDeliveryPartner, ICollection<DeliveryDetail> colDeliveryDetail, ICollection<DeliverySerializeDetail> colDeliverySerialize)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();

            string strretValue = "";
            try
            {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();

                    if (blnIsDeliveryMasterExists(argDeliveryMaster.DeliveryDocCode, argDeliveryMaster.ClientCode, da) == false)
                    {
                        strretValue = InsertDeliveryMaster(argDeliveryMaster, da, lstErr);
                    }
                    else
                    {
                        strretValue = UpdateDeliveryMaster(argDeliveryMaster, da, lstErr);
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
                        if (dtDeliveryPartner.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtDeliveryPartner.Rows)
                            {
                                if (Convert.ToInt32(dr["IsDeleted"]) == 0)
                                {

                                    DeliveryPartner objDeliveryPartner = new DeliveryPartner();

                                    objDeliveryPartner.DeliveryDocCode = strretValue.Trim();
                                    objDeliveryPartner.PFunctionCode = Convert.ToString(dr["PFunctionCode"]).Trim();
                                    objDeliveryPartner.CustomerCode = Convert.ToString(dr["CustomerCode"]).Trim();
                                    objDeliveryPartner.PartnerType = Convert.ToString(dr["PartnerType"]).Trim();
                                    objDeliveryPartner.ClientCode = Convert.ToString(dr["ClientCode"]).Trim();
                                    objDeliveryPartner.CreatedBy = Convert.ToString(dr["CreatedBy"]).Trim();
                                    objDeliveryPartner.ModifiedBy = Convert.ToString(dr["ModifiedBy"]).Trim();

                                    objDeliveryPartnerManager.SaveDeliveryPartner(objDeliveryPartner, da, lstErr);
                                }
                                else
                                {
                                    objDeliveryPartnerManager.DeleteDeliveryPartner(strretValue.Trim(),Convert.ToString(dr["PFunctionCode"]).Trim(),Convert.ToString(dr["ClientCode"]).Trim(), 1, da, lstErr);
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

                        if (colDeliveryDetail.Count > 0)
                        {
                            foreach (DeliveryDetail argDeliveryDetail in colDeliveryDetail)
                            {
                                argDeliveryDetail.DeliveryDocCode = Convert.ToString(strretValue);
                                if (argDeliveryDetail.IsDeleted == 0)
                                {
                                    objDeliveryDetailManager.SaveDeliveryDetail(argDeliveryDetail, da, lstErr);
                                }
                                else
                                {
                                    objDeliveryDetailManager.DeleteDeliveryDetail(argDeliveryDetail.DeliveryDocCode, argDeliveryDetail.ItemNo, 1, argDeliveryDetail.ClientCode, da, lstErr);
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

                        if (colDeliverySerialize.Count > 0)
                        {
                            foreach (DeliverySerializeDetail argDeliverySerialize in colDeliverySerialize)
                            {
                                argDeliverySerialize.DeliveryDocCode = strretValue.Trim();
                                if (argDeliverySerialize.IsDeleted == 0)
                                {
                                    objDeliverySerializeManager.SaveDeliverySerializeDetail(argDeliverySerialize, da, lstErr);
                                }
                                else
                                {
                                    objDeliverySerializeManager.DeleteDeliverySerializeDetail(argDeliverySerialize.DeliveryDocCode,argDeliverySerialize.ItemNo, argDeliverySerialize.SerialNo, argDeliverySerialize.ClientCode, da, lstErr);
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
        
        //public ICollection<ErrorHandler> SaveDeliveryMaster(DeliveryMaster argDeliveryMaster)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsDeliveryMasterExists(argDeliveryMaster.DeliveryDocCode, argDeliveryMaster.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertDeliveryMaster(argDeliveryMaster, da, lstErr);
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
        //            UpdateDeliveryMaster(argDeliveryMaster, da, lstErr);
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
        
        public string InsertDeliveryMaster(DeliveryMaster argDeliveryMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryMaster.DeliveryDocCode);
            param[1] = new SqlParameter("@DeliveryDocTypeCode", argDeliveryMaster.DeliveryDocTypeCode);
            param[2] = new SqlParameter("@DeliveryDate", argDeliveryMaster.DeliveryDate);
            param[3] = new SqlParameter("@CustomerCode", argDeliveryMaster.CustomerCode);
            param[4] = new SqlParameter("@ShipToParty", argDeliveryMaster.ShipToParty);
            param[5] = new SqlParameter("@DeliveryStatus", argDeliveryMaster.DeliveryStatus);
            param[6] = new SqlParameter("@PostingDate", argDeliveryMaster.PostingDate);
            param[7] = new SqlParameter("@TotalQty", argDeliveryMaster.TotalQty);
            param[8] = new SqlParameter("@TotalAmt", argDeliveryMaster.TotalAmt);
            param[9] = new SqlParameter("@SAPTranID", argDeliveryMaster.SAPTranID);
            param[10] = new SqlParameter("@IsSAPPosted", argDeliveryMaster.IsSAPPosted);
            param[11] = new SqlParameter("@IsPGIDone", argDeliveryMaster.IsPGIDone);
            param[12] = new SqlParameter("@ShippingPointCode", argDeliveryMaster.ShippingPointCode);
            param[13] = new SqlParameter("@ClientCode", argDeliveryMaster.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argDeliveryMaster.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argDeliveryMaster.ModifiedBy);
            
            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertDeliveryMaster", param);
            
            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);


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
        
        public string UpdateDeliveryMaster(DeliveryMaster argDeliveryMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[19];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryMaster.DeliveryDocCode);
            param[1] = new SqlParameter("@DeliveryDocTypeCode", argDeliveryMaster.DeliveryDocTypeCode);
            param[2] = new SqlParameter("@DeliveryDate", argDeliveryMaster.DeliveryDate);
            param[3] = new SqlParameter("@CustomerCode", argDeliveryMaster.CustomerCode);
            param[4] = new SqlParameter("@ShipToParty", argDeliveryMaster.ShipToParty);
            param[5] = new SqlParameter("@DeliveryStatus", argDeliveryMaster.DeliveryStatus);
            param[6] = new SqlParameter("@PostingDate", argDeliveryMaster.PostingDate);
            param[7] = new SqlParameter("@TotalQty", argDeliveryMaster.TotalQty);
            param[8] = new SqlParameter("@TotalAmt", argDeliveryMaster.TotalAmt);
            param[9] = new SqlParameter("@SAPTranID", argDeliveryMaster.SAPTranID);
            param[10] = new SqlParameter("@IsSAPPosted", argDeliveryMaster.IsSAPPosted);
            param[11] = new SqlParameter("@IsPGIDone", argDeliveryMaster.IsPGIDone);
            param[12] = new SqlParameter("@ShippingPointCode", argDeliveryMaster.ShippingPointCode);
            param[13] = new SqlParameter("@ClientCode", argDeliveryMaster.ClientCode);
            param[14] = new SqlParameter("@CreatedBy", argDeliveryMaster.CreatedBy);
            param[15] = new SqlParameter("@ModifiedBy", argDeliveryMaster.ModifiedBy);

            param[16] = new SqlParameter("@Type", SqlDbType.Char);
            param[16].Size = 1;
            param[16].Direction = ParameterDirection.Output;

            param[17] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[17].Size = 255;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[18].Size = 20;
            param[18].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateDeliveryMaster", param);
            
            string strMessage = Convert.ToString(param[17].Value);
            string strType = Convert.ToString(param[16].Value);
            string strRetValue = Convert.ToString(param[18].Value);
            
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

        public void UpdateDeliveryMasterIsEdit(string argDeliveryDocCode,int argIsEdit,string argClientCode)
        {
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@IsEdit", argIsEdit);
            param[2] = new SqlParameter("@ClientCode", argClientCode);
            
            int i = da.ExecuteNonQuery("Proc_UpdateDeliveryMasterIsEdit", param);

        }

        public string UpdateDeliveryGI(string argDeliveryDocCode, string argClientCode, string argCreatedBy)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            param[2] = new SqlParameter("@UserName", argCreatedBy);

            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;

            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;

            int i = da.ExecuteNonQuery("SL_UpdateDeliveryGI", param);

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
            return strRetValue;

        }

        public string UpdateDeliveryPGI(string argDeliveryDocCode, string argClientCode, string argCreatedBy)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            param[2] = new SqlParameter("@UserName", argCreatedBy);

            param[3] = new SqlParameter("@Type", SqlDbType.Char);
            param[3].Size = 1;
            param[3].Direction = ParameterDirection.Output;

            param[4] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[4].Size = 255;
            param[4].Direction = ParameterDirection.Output;

            param[5] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[5].Size = 20;
            param[5].Direction = ParameterDirection.Output;

            int i = da.ExecuteNonQuery("SL_UpdateDeliveryPGI", param);

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
            return strRetValue;

        }

        public string UpdateDeliveryMaster(string argDeliveryDocCode, string argClientCode, string argIsSAPPosted, string argSAPTranID)
        {
            DataAccess da = new DataAccess();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            param[2] = new SqlParameter("@IsSAPPosted", argIsSAPPosted);
            param[3] = new SqlParameter("@SAPTranID", argSAPTranID);

            param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[4].Direction = ParameterDirection.ReturnValue;

            int i = da.ExecuteNonQuery("SAP_UpdateDeliveryMaster", param);

            string strRetValue = Convert.ToString(param[4].Value);

            return strRetValue;

        }

        public ICollection<ErrorHandler> DeleteDeliveryMaster(string argDeliveryDocCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteDeliveryMaster", param);
                
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
        
        public bool blnIsDeliveryMasterExists(string argDeliveryDocCode, string argClientCode)
        {
            bool IsDeliveryMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetDeliveryMaster(argDeliveryDocCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDeliveryMasterExists = true;
            }
            else
            {
                IsDeliveryMasterExists = false;
            }
            return IsDeliveryMasterExists;
        }

        public bool blnIsDeliveryMasterExists(string argDeliveryDocCode, string argClientCode,DataAccess da )
          {
            bool IsDeliveryMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetDeliveryMaster(argDeliveryDocCode, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDeliveryMasterExists = true;
            }
            else
            {
                IsDeliveryMasterExists = false;
            }
            return IsDeliveryMasterExists;
         }
    }
}