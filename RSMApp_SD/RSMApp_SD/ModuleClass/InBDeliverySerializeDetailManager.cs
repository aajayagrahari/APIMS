
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
    public class InBDeliverySerializeDetailManager
    {
        const string InBDeliverySerializeDetailTable = "InBDeliverySerializeDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public InBDeliverySerializeDetail objGetInBDeliverySerializeDetail(string argInBDeliveryDocCode, string argItemNo, string argSerialNo, string argClientCode)
        {
            InBDeliverySerializeDetail argInBDeliverySerializeDetail = new InBDeliverySerializeDetail();
            DataSet DataSetToFill = new DataSet();

            if (argInBDeliveryDocCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argItemNo.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argSerialNo.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetInBDeliverySerializeDetail(argInBDeliveryDocCode, argItemNo, argSerialNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argInBDeliverySerializeDetail = this.objCreateInBDeliverySerializeDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argInBDeliverySerializeDetail;
        }

        public ICollection<InBDeliverySerializeDetail> colGetInBDeliverySerializeDetail(string argInBDeliveryDocCode, string argClientCode)
        {
            List<InBDeliverySerializeDetail> lst = new List<InBDeliverySerializeDetail>();
            DataSet DataSetToFill = new DataSet();
            InBDeliverySerializeDetail tInBDeliverySerializeDetail = new InBDeliverySerializeDetail();

            DataSetToFill = this.GetInBDeliverySerializeDetail(argInBDeliveryDocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateInBDeliverySerializeDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<InBDeliverySerializeDetail> colGetInBDeliverySerializeDetail(string argInBDeliveryDocCode, string argClientCode, List<InBDeliverySerializeDetail> lst)
        {
            //List<DeliverySerializeDetail> lst = new List<DeliverySerializeDetail>();
            DataSet DataSetToFill = new DataSet();
            InBDeliverySerializeDetail tDeliverySerializeDetail = new InBDeliverySerializeDetail();

            DataSetToFill = this.GetInBDeliverySerializeDetail(argInBDeliveryDocCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateInBDeliverySerializeDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetInBDeliverySerializeDetail(string argInBDeliveryDocCode, string argItemNo, string argSerialNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@SerialNo", argSerialNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetInBDeliverySerializeDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetInBDeliverySerializeDetail(string argInBDeliveryDocCode, string argItemNo, string argSerialNo, string argClientCode, DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@SerialNo", argSerialNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetInBDeliverySerializeDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetInBDeliverySerializeDetail(string argInBDeliveryDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDocCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetInBDeliverySerializeDetail", param);
            return DataSetToFill;
        }

        private InBDeliverySerializeDetail objCreateInBDeliverySerializeDetail(DataRow dr)
        {
            InBDeliverySerializeDetail tInBDeliverySerializeDetail = new InBDeliverySerializeDetail();

            tInBDeliverySerializeDetail.SetObjectInfo(dr);

            return tInBDeliverySerializeDetail;

        }

        public ICollection<ErrorHandler> SaveInBDeliverySerializeDetail(InBDeliverySerializeDetail argInBDeliverySerializeDetail)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsInBDeliverySerializeDetailExists(argInBDeliverySerializeDetail.InBDeliveryDocCode, argInBDeliverySerializeDetail.ItemNo, argInBDeliverySerializeDetail.SerialNo, argInBDeliverySerializeDetail.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertInBDeliverySerializeDetail(argInBDeliverySerializeDetail, da, lstErr);
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
                    UpdateInBDeliverySerializeDetail(argInBDeliverySerializeDetail, da, lstErr);
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

        public void SaveInBDeliverySerializeDetail(InBDeliverySerializeDetail argInBDeliverySerializeDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsInBDeliverySerializeDetailExists(argInBDeliverySerializeDetail.InBDeliveryDocCode, argInBDeliverySerializeDetail.ItemNo, argInBDeliverySerializeDetail.SerialNo, argInBDeliverySerializeDetail.ClientCode,da) == false)
                {
                    InsertInBDeliverySerializeDetail(argInBDeliverySerializeDetail, da, lstErr);
                }
                else
                {
                    UpdateInBDeliverySerializeDetail(argInBDeliverySerializeDetail, da, lstErr);
                }
            }
            catch (Exception ex)
            {

                objErrorHandler.Type = ErrorConstant.strAboartType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strInsertModule;
                objErrorHandler.ModulePart = ErrorConstant.strDetailModule;
                objErrorHandler.Message = ex.Message.ToString();
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";
                lstErr.Add(objErrorHandler);

            }
        }

        public void InsertInBDeliverySerializeDetail(InBDeliverySerializeDetail argInBDeliverySerializeDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliverySerializeDetail.InBDeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argInBDeliverySerializeDetail.ItemNo);
            param[2] = new SqlParameter("@POItemNo", argInBDeliverySerializeDetail.POItemNo);
            param[3] = new SqlParameter("@DLItemNo", argInBDeliverySerializeDetail.DLItemNo);
            param[4] = new SqlParameter("@SerialNo", argInBDeliverySerializeDetail.SerialNo);
            param[5] = new SqlParameter("@ClientCode", argInBDeliverySerializeDetail.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argInBDeliverySerializeDetail.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argInBDeliverySerializeDetail.ModifiedBy);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertInBDeliverySerializeDetail", param);


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

        public void UpdateInBDeliverySerializeDetail(InBDeliverySerializeDetail argInBDeliverySerializeDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliverySerializeDetail.InBDeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argInBDeliverySerializeDetail.ItemNo);
            param[2] = new SqlParameter("@POItemNo", argInBDeliverySerializeDetail.POItemNo);
            param[3] = new SqlParameter("@DLItemNo", argInBDeliverySerializeDetail.DLItemNo);
            param[4] = new SqlParameter("@SerialNo", argInBDeliverySerializeDetail.SerialNo);
            param[5] = new SqlParameter("@ClientCode", argInBDeliverySerializeDetail.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argInBDeliverySerializeDetail.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argInBDeliverySerializeDetail.ModifiedBy);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            
            int i = da.NExecuteNonQuery("Proc_UpdateInBDeliverySerializeDetail", param);


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

        public ICollection<ErrorHandler> DeleteInBDeliverySerializeDetail(string argInBDeliveryDocCode, string argItemNo, string argSerialNo, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDocCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@SerialNo", argSerialNo);
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

                int i = da.ExecuteNonQuery("Proc_DeleteInBDeliverySerializeDetail", param);


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

        public ICollection<ErrorHandler> DeleteInBDeliverySerializeDetail(string argInBDeliveryDocCode, string argItemNo, string argSerialNo, string argClientCode, int iIsDeleted, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@InBDeliveryDocCode", argInBDeliveryDocCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@SerialNo", argSerialNo);
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

                int i = da.NExecuteNonQuery("Proc_DeleteInBDeliverySerializeDetail", param);


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

        public bool blnIsInBDeliverySerializeDetailExists(string argInBDeliveryDocCode, string argItemNo, string argSerialNo, string argClientCode)
        {
            bool IsInBDeliverySerializeDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetInBDeliverySerializeDetail(argInBDeliveryDocCode, argItemNo, argSerialNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsInBDeliverySerializeDetailExists = true;
            }
            else
            {
                IsInBDeliverySerializeDetailExists = false;
            }
            return IsInBDeliverySerializeDetailExists;
        }

        public bool blnIsInBDeliverySerializeDetailExists(string argInBDeliveryDocCode, string argItemNo, string argSerialNo, string argClientCode, DataAccess da)
        {
            bool IsInBDeliverySerializeDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetInBDeliverySerializeDetail(argInBDeliveryDocCode, argItemNo, argSerialNo, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsInBDeliverySerializeDetailExists = true;
            }
            else
            {
                IsInBDeliverySerializeDetailExists = false;
            }
            return IsInBDeliverySerializeDetailExists;
        }
    
    }
}