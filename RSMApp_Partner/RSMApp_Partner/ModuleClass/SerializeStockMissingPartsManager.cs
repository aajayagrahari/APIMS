
//Created On :: 15, December, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class SerializeStockMissingPartsManager
    {
        const string SerializeStockMissingPartsTable = "SerializeStockMissingParts";
        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public SerializeStockMissingParts objGetSerializeStockMissingParts(string argSerialNo, string argClientCode)
        {
            SerializeStockMissingParts argSerializeStockMissingParts = new SerializeStockMissingParts();
            DataSet DataSetToFill = new DataSet();

            if (argSerialNo.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetSerializeStockMissingParts(argSerialNo, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argSerializeStockMissingParts = this.objCreateSerializeStockMissingParts((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argSerializeStockMissingParts;
        }


        public ICollection<SerializeStockMissingParts> colGetSerializeStockMissingParts(string argSerialNo, string argClientCode)
        {
            List<SerializeStockMissingParts> lst = new List<SerializeStockMissingParts>();
            DataSet DataSetToFill = new DataSet();
            SerializeStockMissingParts tSerializeStockMissingParts = new SerializeStockMissingParts();

            DataSetToFill = this.GetSerializeStockMissingParts(argSerialNo, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateSerializeStockMissingParts(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }


        public DataSet GetSerializeStockMissingParts(string argSerialNo, string argMaterialCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSerializeStockMissingParts4ID", param);

            return DataSetToFill;
        }

        public DataSet GetSerializeStockMissingParts(string argSerialNo, string argMaterialCode, string argClientCode, DataAccess da)
        {            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetSerializeStockMissingParts4ID", param);

            return DataSetToFill;
        }


        public DataSet GetSerializeStockMissingParts(string argSerialNo,  string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSerializeStockMissingParts");
            return DataSetToFill;
        }
        
        public DataSet GetSerializeStockMissingParts4Repair(string argSerialNo, string argMastMaterialCode, string argMaterialCode,  string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@MastMaterialCode", argMastMaterialCode);
            param[2] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetSerializeStockMissingParts4Repair", param);
            return DataSetToFill;
        }

        private SerializeStockMissingParts objCreateSerializeStockMissingParts(DataRow dr)
        {
            SerializeStockMissingParts tSerializeStockMissingParts = new SerializeStockMissingParts();

            tSerializeStockMissingParts.SetObjectInfo(dr);

            return tSerializeStockMissingParts;

        }


        public ICollection<ErrorHandler> SaveSerializeStockMissingParts(SerializeStockMissingParts argSerializeStockMissingParts)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsSerializeStockMissingPartsExists(argSerializeStockMissingParts.SerialNo,  argSerializeStockMissingParts.MaterialCode,  argSerializeStockMissingParts.ClientCode) == false)
                {
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertSerializeStockMissingParts(argSerializeStockMissingParts, da, lstErr);
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
                    UpdateSerializeStockMissingParts(argSerializeStockMissingParts, da, lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return lstErr;
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

        public void SaveSerializeStockMissingParts(SerializeStockMissingParts argSerializeStockMissingParts, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsSerializeStockMissingPartsExists(argSerializeStockMissingParts.SerialNo, argSerializeStockMissingParts.MaterialCode, argSerializeStockMissingParts.ClientCode, da) == false)
                {
                    InsertSerializeStockMissingParts(argSerializeStockMissingParts, da, lstErr);
                }
                else
                {
                    UpdateSerializeStockMissingParts(argSerializeStockMissingParts, da, lstErr);
                }
            }
            catch(Exception ex)
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

        public void InsertSerializeStockMissingParts(SerializeStockMissingParts argSerializeStockMissingParts, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];

            param[0] = new SqlParameter("@SerialNo", argSerializeStockMissingParts.SerialNo);
            param[1] = new SqlParameter("@MastMaterialCode", argSerializeStockMissingParts.MastMaterialCode);
            param[2] = new SqlParameter("@MaterialCode", argSerializeStockMissingParts.MaterialCode);
            param[3] = new SqlParameter("@MatDesc", argSerializeStockMissingParts.MatDesc);
            param[4] = new SqlParameter("@MatGroup1Code", argSerializeStockMissingParts.MatGroup1Code);
            param[5] = new SqlParameter("@PartnerCode", argSerializeStockMissingParts.PartnerCode);
            param[6] = new SqlParameter("@PlantCode", argSerializeStockMissingParts.PlantCode);
            param[7] = new SqlParameter("@DocType", argSerializeStockMissingParts.DocType);
            param[8] = new SqlParameter("@ClientCode", argSerializeStockMissingParts.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argSerializeStockMissingParts.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argSerializeStockMissingParts.ModifiedBy);          

            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertSerializeStockMissingParts", param);


            string strMessage = Convert.ToString(param[12].Value);
            string strType = Convert.ToString(param[11].Value);
            string strRetValue = Convert.ToString(param[13].Value);


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
        
        public void UpdateSerializeStockMissingParts(SerializeStockMissingParts argSerializeStockMissingParts, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[14];

            param[0] = new SqlParameter("@SerialNo", argSerializeStockMissingParts.SerialNo);
            param[1] = new SqlParameter("@MastMaterialCode", argSerializeStockMissingParts.MastMaterialCode);
            param[2] = new SqlParameter("@MaterialCode", argSerializeStockMissingParts.MaterialCode);
            param[3] = new SqlParameter("@MatDesc", argSerializeStockMissingParts.MatDesc);
            param[4] = new SqlParameter("@MatGroup1Code", argSerializeStockMissingParts.MatGroup1Code);
            param[5] = new SqlParameter("@PartnerCode", argSerializeStockMissingParts.PartnerCode);
            param[6] = new SqlParameter("@PlantCode", argSerializeStockMissingParts.PlantCode);
            param[7] = new SqlParameter("@DocType", argSerializeStockMissingParts.DocType);
            param[8] = new SqlParameter("@ClientCode", argSerializeStockMissingParts.ClientCode);
            param[9] = new SqlParameter("@CreatedBy", argSerializeStockMissingParts.CreatedBy);
            param[10] = new SqlParameter("@ModifiedBy", argSerializeStockMissingParts.ModifiedBy);

            param[11] = new SqlParameter("@Type", SqlDbType.Char);
            param[11].Size = 1;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[12].Size = 255;
            param[12].Direction = ParameterDirection.Output;

            param[13] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[13].Size = 20;
            param[13].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateSerializeStockMissingParts", param);


            string strMessage = Convert.ToString(param[12].Value);
            string strType = Convert.ToString(param[11].Value);
            string strRetValue = Convert.ToString(param[13].Value);


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
        
        public ICollection<ErrorHandler> DeleteSerializeStockMissingParts(string argSerialNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[5];
                param[0] = new SqlParameter("@SerialNo", argSerialNo);
                param[1] = new SqlParameter("@ClientCode", argClientCode);

                param[2] = new SqlParameter("@Type", SqlDbType.Char);
                param[2].Size = 1;
                param[2].Direction = ParameterDirection.Output;
                param[3] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[3].Size = 255;
                param[3].Direction = ParameterDirection.Output;
                param[4] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[4].Size = 20;
                param[4].Direction = ParameterDirection.Output;
                int i = da.ExecuteNonQuery("Proc_DeleteSerializeStockMissingParts", param);


                string strMessage = Convert.ToString(param[3].Value);
                string strType = Convert.ToString(param[2].Value);
                string strRetValue = Convert.ToString(param[4].Value);


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
        
        public bool blnIsSerializeStockMissingPartsExists(string argSerialNo, string argMaterialCode, string argClientCode)
        {
            bool IsSerializeStockMissingPartsExists = false;
            DataSet ds = new DataSet();
            ds = GetSerializeStockMissingParts(argSerialNo,argMaterialCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSerializeStockMissingPartsExists = true;
            }
            else
            {
                IsSerializeStockMissingPartsExists = false;
            }
            return IsSerializeStockMissingPartsExists;
        }

        public bool blnIsSerializeStockMissingPartsExists(string argSerialNo, string argMaterialCode,  string argClientCode, DataAccess da)
        {
            bool IsSerializeStockMissingPartsExists = false;
            DataSet ds = new DataSet();
            ds = GetSerializeStockMissingParts(argSerialNo, argMaterialCode, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsSerializeStockMissingPartsExists = true;
            }
            else
            {
                IsSerializeStockMissingPartsExists = false;
            }
            return IsSerializeStockMissingPartsExists;
        }
    }
}