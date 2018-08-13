
//Created On :: 21, July, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SD
{
    public class DeliverySerializeDetailManager
    {
        const string DeliverySerializeDetailTable = "DeliverySerializeDetail";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public DeliverySerializeDetail objGetDeliverySerializeDetail(string argDeliveryDocCode, string argItemNo, string argSerialNo, string argClientCode)
        {
            DeliverySerializeDetail argDeliverySerializeDetail = new DeliverySerializeDetail();
            DataSet DataSetToFill = new DataSet();

            if (argDeliveryDocCode.Trim() == "")
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

            DataSetToFill = this.GetDeliverySerializeDetail(argDeliveryDocCode, argItemNo, argSerialNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argDeliverySerializeDetail = this.objCreateDeliverySerializeDetail((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argDeliverySerializeDetail;
        }

        public ICollection<DeliverySerializeDetail> colGetDeliverySerializeDetail(string argDeliveryDocCode, string argClientCode)
        {
            List<DeliverySerializeDetail> lst = new List<DeliverySerializeDetail>();
            DataSet DataSetToFill = new DataSet();
            DeliverySerializeDetail tDeliverySerializeDetail = new DeliverySerializeDetail();

            DataSetToFill = this.GetDeliverySerializeDetail(argDeliveryDocCode,  argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDeliverySerializeDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<DeliverySerializeDetail> colGetDeliverySerializeDetail(string argDeliveryDocCode, string argClientCode, List<DeliverySerializeDetail> lst)
        {
            //List<DeliverySerializeDetail> lst = new List<DeliverySerializeDetail>();
            DataSet DataSetToFill = new DataSet();
            DeliverySerializeDetail tDeliverySerializeDetail = new DeliverySerializeDetail();

            DataSetToFill = this.GetDeliverySerializeDetail(argDeliveryDocCode,  argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateDeliverySerializeDetail(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }
        
        public DataSet GetDeliverySerializeDetail(string argDeliveryDocCode, string argItemNo, string argSerialNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@SerialNo", argSerialNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDeliverySerializeDetail4ID", param);

            return DataSetToFill;
        }

        public DataSet GetDeliverySerializeDetail(string argDeliveryDocCode, string argItemNo, string argSerialNo, string argClientCode, DataAccess da)
        {
          //  DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argItemNo);
            param[2] = new SqlParameter("@SerialNo", argSerialNo);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetDeliverySerializeDetail4ID", param);

            return DataSetToFill;
        }
        
        public DataSet GetDeliverySerializeDetail(string argDeliveryDocCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];

            param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);   
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetDeliverySerializeDetail", param);
            return DataSetToFill;
        }
        
        private DeliverySerializeDetail objCreateDeliverySerializeDetail(DataRow dr)
        {
            DeliverySerializeDetail tDeliverySerializeDetail = new DeliverySerializeDetail();

            tDeliverySerializeDetail.SetObjectInfo(dr);

            return tDeliverySerializeDetail;

        }

        public void SaveDeliverySerializeDetail(DeliverySerializeDetail argDeliverySerializeDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsDeliverySerializeDetailExists(argDeliverySerializeDetail.DeliveryDocCode, argDeliverySerializeDetail.ItemNo, argDeliverySerializeDetail.SerialNo, argDeliverySerializeDetail.ClientCode, da) == false)
                {
                    InsertDeliverySerializeDetail(argDeliverySerializeDetail, da, lstErr);
                }
                else
                {
                    UpdateDeliverySerializeDetail(argDeliverySerializeDetail, da, lstErr);
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

        //public ICollection<ErrorHandler> SaveDeliverySerializeDetail(DeliverySerializeDetail argDeliverySerializeDetail)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsDeliverySerializeDetailExists(argDeliverySerializeDetail.DeliveryDocCode, argDeliverySerializeDetail.ItemNo, argDeliverySerializeDetail.SerialNo, argDeliverySerializeDetail.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertDeliverySerializeDetail(argDeliverySerializeDetail, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
        //        }
        //        else
        //        {
        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            UpdateDeliverySerializeDetail(argDeliverySerializeDetail, da, lstErr);
        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }
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
        
        public void InsertDeliverySerializeDetail(DeliverySerializeDetail argDeliverySerializeDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliverySerializeDetail.DeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argDeliverySerializeDetail.ItemNo);
            param[2] = new SqlParameter("@SOItemNo", argDeliverySerializeDetail.SOItemNo);
            param[3] = new SqlParameter("@SerialNo", argDeliverySerializeDetail.SerialNo);
            param[4] = new SqlParameter("@SerialNo2", argDeliverySerializeDetail.SerialNo2);
            param[5] = new SqlParameter("@ClientCode", argDeliverySerializeDetail.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argDeliverySerializeDetail.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argDeliverySerializeDetail.ModifiedBy);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertDeliverySerializeDetail", param);


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
        
        public void UpdateDeliverySerializeDetail(DeliverySerializeDetail argDeliverySerializeDetail, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@DeliveryDocCode", argDeliverySerializeDetail.DeliveryDocCode);
            param[1] = new SqlParameter("@ItemNo", argDeliverySerializeDetail.ItemNo);
            param[2] = new SqlParameter("@SOItemNo", argDeliverySerializeDetail.SOItemNo);
            param[3] = new SqlParameter("@SerialNo", argDeliverySerializeDetail.SerialNo);
            param[4] = new SqlParameter("@SerialNo2", argDeliverySerializeDetail.SerialNo2);
            param[5] = new SqlParameter("@ClientCode", argDeliverySerializeDetail.ClientCode);
            param[6] = new SqlParameter("@CreatedBy", argDeliverySerializeDetail.CreatedBy);
            param[7] = new SqlParameter("@ModifiedBy", argDeliverySerializeDetail.ModifiedBy);
            
            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateDeliverySerializeDetail", param);


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

        public void DeleteDeliverySerializeDetail(string argDeliveryDocCode, string argItemNo, string argSerialNo, string argClientCode, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@SerialNo", argSerialNo);
                param[3] = new SqlParameter("@ClientCode", argClientCode);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.NExecuteNonQuery("Proc_DeleteDeliverySerializeDetail", param);
                
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
        }

        public ICollection<ErrorHandler> DeleteDeliverySerializeDetail(string argDeliveryDocCode, string argItemNo, string argSerialNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            
            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@DeliveryDocCode", argDeliveryDocCode);
                param[1] = new SqlParameter("@ItemNo", argItemNo);
                param[2] = new SqlParameter("@SerialNo", argSerialNo);
                param[3] = new SqlParameter("@ClientCode", argClientCode);

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;
                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;
                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteDeliverySerializeDetail", param);


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
        
        public bool blnIsDeliverySerializeDetailExists(string argDeliveryDocCode, string argItemNo, string argSerialNo, string argClientCode, DataAccess da)
        {
            bool IsDeliverySerializeDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetDeliverySerializeDetail(argDeliveryDocCode, argItemNo, argSerialNo, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDeliverySerializeDetailExists = true;
            }
            else
            {
                IsDeliverySerializeDetailExists = false;
            }
            return IsDeliverySerializeDetailExists;
        }

        public bool blnIsDeliverySerializeDetailExists(string argDeliveryDocCode, string argItemNo, string argSerialNo, string argClientCode)
        {
            bool IsDeliverySerializeDetailExists = false;
            DataSet ds = new DataSet();
            ds = GetDeliverySerializeDetail(argDeliveryDocCode, argItemNo, argSerialNo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDeliverySerializeDetailExists = true;
            }
            else
            {
                IsDeliverySerializeDetailExists = false;
            }
            return IsDeliverySerializeDetailExists;
        }

        public DataTable BulkCopy(string argExcelPath, string argQuery, string strTableName, string argFileExt)
        {
            DataTable dtExcel = null;
            string xConnStr = "";
            string strSheetName = "";

            DataTable dtTableSchema = new DataTable();
            DataSet dsExcel = new DataSet();
            OleDbConnection objXConn = null;
            OleDbDataAdapter objDataAdapter = new OleDbDataAdapter();

            if (argFileExt.ToString() == ".xls")
            {
                xConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;" +
               "Data Source=" + argExcelPath.Trim() + ";" +
               "Extended Properties=Excel 8.0";
            }
            else
            {
                xConnStr = "Provider=Microsoft.ACE.OLEDB.12.0;" +
               "Data Source=" + argExcelPath.Trim() + ";" +
               "Extended Properties=Excel 12.0";
            }

            try
            {

                objXConn = new OleDbConnection(xConnStr);
                objXConn.Open();

                dtTableSchema = objXConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                if (argFileExt.ToString() == ".xls")
                {
                    strSheetName = Convert.ToString(dtTableSchema.Rows[0]["TABLE_NAME"]);
                }
                else
                {

                    strSheetName = Convert.ToString(dtTableSchema.Rows[0]["TABLE_NAME"]);
                    if (strSheetName.IndexOf(@"_xlnm#_FilterDatabase") >= 0)
                    {
                        strSheetName = Convert.ToString(dtTableSchema.Rows[1]["TABLE_NAME"]);


                    }

                }

                argQuery = argQuery + " [" + strSheetName + "]";

                OleDbCommand objCommand = new OleDbCommand(argQuery, objXConn);

                objDataAdapter.SelectCommand = objCommand;
                objDataAdapter.Fill(dsExcel);

                dtExcel = dsExcel.Tables[0];



            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
               objXConn.Close();
            }

            return dtExcel;

        }
    }
}