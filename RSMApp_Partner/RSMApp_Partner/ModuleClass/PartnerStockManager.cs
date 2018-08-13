
//Created On :: 16, October, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Partner
{
    public class PartnerStockManager
    {
        const string PartnerStockTable = "PartnerStock";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public PartnerStock objGetPartnerStock(string argPartnerCode, string argStoreCode, string argMaterialCode, string argClientCode)
        {
            PartnerStock argPartnerStock = new PartnerStock();
            DataSet DataSetToFill = new DataSet();

            if (argPartnerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argStoreCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argMaterialCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPartnerStock(argPartnerCode, argStoreCode, argMaterialCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartnerStock = this.objCreatePartnerStock((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPartnerStock;
        }

        public ICollection<PartnerStock> colGetPartnerStock(string argPartnerCode, string argClientCode)
        {
            List<PartnerStock> lst = new List<PartnerStock>();
            DataSet DataSetToFill = new DataSet();
            PartnerStock tPartnerStock = new PartnerStock();

            DataSetToFill = this.GetPartnerStock(argPartnerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePartnerStock(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetPartnerStock(string argPartnerCode, string argStoreCode, string argMaterialCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@StoreCode", argStoreCode);
            param[2] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerStock4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerStock(string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerStock", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerStock4CL(string argSerialNo, string argMatGroup1Code, string argStoreCode, string argStockIndicator, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@StoreCode", argStoreCode);
            param[3] = new SqlParameter("@StockIndicator", argStockIndicator);
            param[4] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[5] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerStock4CL", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerStock4Consumption(string argStoreCode, string argStockIndicator, string argPartnerEmployeeCode, string argMaterialCode, string argConMatGroup1Code, string argMRevisionCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@StoreCode", argStoreCode);
            param[1] = new SqlParameter("@StockIndicator", argStockIndicator);
            param[2] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeCode);
            param[3] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[4] = new SqlParameter("@ConMatGroup1Code", argConMatGroup1Code);
            param[5] = new SqlParameter("@MRevisionCode", argMRevisionCode);
            param[6] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[7] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_GetMatList4PartsConsumption", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerStock4Consumption(string argStoreCode, string argStockIndicator, string argPartnerEmployeeCode, string argMaterialCode, string argConMatGroup1Code, string argRepairTypeCode, string argMRevisionCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@StoreCode", argStoreCode);
            param[1] = new SqlParameter("@StockIndicator", argStockIndicator);
            param[2] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeCode);
            param[3] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[4] = new SqlParameter("@ConMatGroup1Code", argConMatGroup1Code);
            param[5] = new SqlParameter("@RepairTypeCode", argRepairTypeCode);            
            param[6] = new SqlParameter("@MRevisionCode", argMRevisionCode);
            param[7] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[8] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_GetSpareList4PartsConsumption", param);

            return DataSetToFill;
        }


        public DataSet GetMaterialList4PartsOrder(string argMaterialCode, string argConMatGroup1Code, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@ConMatGroup1Code", argConMatGroup1Code);
            param[2] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_GetMatList4PartsOrder", param);

            return DataSetToFill;
        }

        public DataSet GetMaterialList4GM(string argMaterialCode, string argFrmStoreCode, string argFrmStockInd, string argFrmPartnerEmployeeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@FrmStoreCode", argFrmStoreCode);
            param[2] = new SqlParameter("@FrmStockInd", argFrmStockInd);
            param[3] = new SqlParameter("@FrmPartnerEmployeeCode", argFrmPartnerEmployeeCode);
            param[4] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[5] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_GetMaterialList4GM", param);

            return DataSetToFill;
        }
        
        public DataSet GetSerialMatList4GM(string argSerialNo, string argMaterialCode, string argMatGroup1Code,  string argFrmStoreCode, string argFrmStockInd, string argFrmPartnerEmployeeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[3] = new SqlParameter("@FrmStoreCode", argFrmStoreCode);
            param[4] = new SqlParameter("@FrmStockInd", argFrmStockInd);
            param[5] = new SqlParameter("@FrmPartnerEmployeeCode", argFrmPartnerEmployeeCode);
            param[6] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[7] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_GetSerialMaterialList4GM", param);

            return DataSetToFill;
        }

        public DataSet GetSerialMatList4Cannibalize(string argSerialNo, string argMaterialCode, string argMatGroup1Code,  string argFrmStoreCode, string argFrmStockInd, string argFrmPartnerEmployeeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[3] = new SqlParameter("@FrmStoreCode", argFrmStoreCode);
            param[4] = new SqlParameter("@FrmStockInd", argFrmStockInd);
            param[5] = new SqlParameter("@FrmPartnerEmployeeCode", argFrmPartnerEmployeeCode);
            param[6] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[7] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_GetSerialMaterialList4Cannibalize", param);

            return DataSetToFill;
        }

        

        public DataSet GetSerialMatList4CN(string argSerialNo, string argMaterialCode, string argMatGroup1Code, string argFrmStoreCode, string argFrmStockInd, string argFrmPartnerEmployeeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@SerialNo", argSerialNo);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[3] = new SqlParameter("@FrmStoreCode", argFrmStoreCode);
            param[4] = new SqlParameter("@FrmStockInd", argFrmStockInd);
            param[5] = new SqlParameter("@FrmPartnerEmployeeCode", argFrmPartnerEmployeeCode);
            param[6] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[7] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_GetSerialMaterialList4CN", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerStock4Report(string argMatGroup1Code, string argStoreCode, string argPartnerEmployeeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@StoreCode", argStoreCode);
            param[2] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeCode);
            param[3] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetPartnerStock", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerSerializeStock4Report(string argMatGroup1Code, string argStoreCode, string argPartnerEmployeeCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@StoreCode", argStoreCode);
            param[2] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeCode);
            param[3] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetPartnerSerializeStock", param);

            return DataSetToFill;
        }

        public DataSet GetMatGroup14Repair(string argPartnerEmployeeCode, string argPartnerCode, string argClientCode)
        {

            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];            
            param[0] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialGroup14Repair", param);

            return DataSetToFill;            
        }

        public DataSet GetMatGroup14SerialStock(string argPartnerEmployeeCode, string argPartnerCode, string argStoreCode, string argStockIndicatore, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@StoreCode", argStoreCode);
            param[3] = new SqlParameter("@StockIndicatore", argStockIndicatore);            
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialGroup14SerialStock", param);

            return DataSetToFill;      
        }

        public DataSet GetMatGroup14Stock(string argPartnerEmployeeCode, string argPartnerCode, string argStoreCode, string argStockIndicatore, int argIsSerialize, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@StoreCode", argStoreCode);
            param[3] = new SqlParameter("@StockIndicatore", argStockIndicatore);
            param[4] = new SqlParameter("@IsSerialize", argIsSerialize);
            param[5] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialGroup14Stock", param);

            return DataSetToFill;
        }

        public DataSet GetMaterialFromStock(string argMatGroup1Code, string argPartnerEmployeeCode, string argPartnerCode, string argStoreCode, string argStockIndicatore, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[6];
            param[0] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@StoreCode", argStoreCode);
            param[3] = new SqlParameter("@StockIndicatore", argStockIndicatore);
            param[4] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[5] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialFromStock", param);

            return DataSetToFill;
        }

        public DataSet GetMaterial4GM(string argMatGroup1Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial4GM", param);

            return DataSetToFill;
        }
             
        /* Spare List */

        public DataSet GetSparePartMatGroup(string argMastMaterialCode, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MastMaterialCode", argMastMaterialCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_GetSparePartMatGroup", param);

            return DataSetToFill;
        }


        public DataSet GetSparePartList(string argPartnerTypeCode, string argMastMaterialCode, string argMatGroup1Code, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];


            param[0] = new SqlParameter("@PartnerTypeCode", argPartnerTypeCode);
            param[1] = new SqlParameter("@MastMaterialCode", argMastMaterialCode);
            param[2] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[3] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_GetSparePartList", param);

            return DataSetToFill;
        }


        /*-------------------------*/


        private PartnerStock objCreatePartnerStock(DataRow dr)
        {
            PartnerStock tPartnerStock = new PartnerStock();

            tPartnerStock.SetObjectInfo(dr);

            return tPartnerStock;

        }

        public ICollection<ErrorHandler> SavePartnerStock(PartnerStock argPartnerStock)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartnerStockExists(argPartnerStock.PartnerCode, argPartnerStock.StoreCode, argPartnerStock.MaterialCode, argPartnerStock.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartnerStock(argPartnerStock, da, lstErr);
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
                    UpdatePartnerStock(argPartnerStock, da, lstErr);
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

        public void InsertPartnerStock(PartnerStock argPartnerStock, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@PartnerCode", argPartnerStock.PartnerCode);
            param[1] = new SqlParameter("@StoreCode", argPartnerStock.StoreCode);
            param[2] = new SqlParameter("@PartnerEmployeeCode", argPartnerStock.PartnerEmployeeCode);
            param[3] = new SqlParameter("@MaterialCode", argPartnerStock.MaterialCode);
            param[4] = new SqlParameter("@UnrestrictedStock", argPartnerStock.UnrestrictedStock);
            param[5] = new SqlParameter("@RestrictedStock", argPartnerStock.RestrictedStock);
            param[6] = new SqlParameter("@BlockedStock", argPartnerStock.BlockedStock);
            param[7] = new SqlParameter("@ReturnedStock", argPartnerStock.ReturnedStock);
            param[8] = new SqlParameter("@QCStock", argPartnerStock.QCStock);
            param[9] = new SqlParameter("@MAP", argPartnerStock.MAP);
            param[10] = new SqlParameter("@OnSiteStock", argPartnerStock.OnSiteStock);
            param[11] = new SqlParameter("@UOMCode", argPartnerStock.UOMCode);
            param[12] = new SqlParameter("@ClientCode", argPartnerStock.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argPartnerStock.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argPartnerStock.ModifiedBy);
 

            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[16].Size = 20;
            param[16].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPartnerStock", param);


            string strMessage = Convert.ToString(param[16].Value);
            string strType = Convert.ToString(param[15].Value);
            string strRetValue = Convert.ToString(param[17].Value);


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

        public void UpdatePartnerStock(PartnerStock argPartnerStock, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[18];
            param[0] = new SqlParameter("@PartnerCode", argPartnerStock.PartnerCode);
            param[1] = new SqlParameter("@StoreCode", argPartnerStock.StoreCode);
            param[2] = new SqlParameter("@PartnerEmployeeCode", argPartnerStock.PartnerEmployeeCode);
            param[3] = new SqlParameter("@MaterialCode", argPartnerStock.MaterialCode);
            param[4] = new SqlParameter("@UnrestrictedStock", argPartnerStock.UnrestrictedStock);
            param[5] = new SqlParameter("@RestrictedStock", argPartnerStock.RestrictedStock);
            param[6] = new SqlParameter("@BlockedStock", argPartnerStock.BlockedStock);
            param[7] = new SqlParameter("@ReturnedStock", argPartnerStock.ReturnedStock);
            param[8] = new SqlParameter("@QCStock", argPartnerStock.QCStock);
            param[9] = new SqlParameter("@MAP", argPartnerStock.MAP);
            param[10] = new SqlParameter("@OnSiteStock", argPartnerStock.OnSiteStock);
            param[11] = new SqlParameter("@UOMCode", argPartnerStock.UOMCode);
            param[12] = new SqlParameter("@ClientCode", argPartnerStock.ClientCode);
            param[13] = new SqlParameter("@CreatedBy", argPartnerStock.CreatedBy);
            param[14] = new SqlParameter("@ModifiedBy", argPartnerStock.ModifiedBy);


            param[15] = new SqlParameter("@Type", SqlDbType.Char);
            param[15].Size = 1;
            param[15].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[16].Size = 255;
            param[16].Direction = ParameterDirection.Output;

            param[16] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[16].Size = 20;
            param[16].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePartnerStock", param);


            string strMessage = Convert.ToString(param[16].Value);
            string strType = Convert.ToString(param[15].Value);
            string strRetValue = Convert.ToString(param[17].Value);


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

        public ICollection<ErrorHandler> DeletePartnerStock(string argPartnerCode, string argStoreCode, string argMaterialCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
                param[1] = new SqlParameter("@StoreCode", argStoreCode);
                param[2] = new SqlParameter("@MaterialCode", argMaterialCode);
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

                int i = da.ExecuteNonQuery("Proc_DeletePartnerStock", param);


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

        public bool blnIsPartnerStockExists(string argPartnerCode, string argStoreCode, string argMaterialCode, string argClientCode)
        {
            bool IsPartnerStockExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerStock(argPartnerCode, argStoreCode, argMaterialCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerStockExists = true;
            }
            else
            {
                IsPartnerStockExists = false;
            }
            return IsPartnerStockExists;
        }




        //---Ashutosh

        public void SaveBulkData(DataSet dsExcel, string argTableName)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.BulkCopy(dsExcel, argTableName);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataSet GetPartnerSerializeStock4SerialNo(string argSearchType, string argMatGroup1Code, string argSerialNo, string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@SearchType", argSearchType);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@SerialNo", argSerialNo);
            param[3] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SL_QuickSearch4SerialNo", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerStock4Report(string argSearchType, string argMaterialCode, string argMatGroup1Code, string argStoreCode, string argPartnerCode, string argPartnerEmployeeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@SearchType", argSearchType);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[3] = new SqlParameter("@StoreCode", argStoreCode);
            param[4] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[5] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeCode);
            param[6] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetPartnerStock", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerSerializeStock4Report(string argMatGroup1Code, string argStoreCode, string argPartnerEmployeeCode, string argPartnerCode, string argSearchType, string argMaterialCode, string argSerialNo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@StoreCode", argStoreCode);
            param[2] = new SqlParameter("@PartnerEmployeeCode", argPartnerEmployeeCode);
            param[3] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[4] = new SqlParameter("@SearchType", argSearchType);
            param[5] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[6] = new SqlParameter("@SerialNo", argSerialNo);
            param[7] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetPartnerSerializeStock", param);

            return DataSetToFill;
        }

        public DataSet GetMaterialList4Report(string argMaterialTypeCode, string argMatGroup1Code, string argSearchType, string argMaterialCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@MaterialTypeCode", argMaterialTypeCode);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[2] = new SqlParameter("@SearchType", argSearchType);
            param[3] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("RP_GetMaterialList4Report", param);

            return DataSetToFill;
        }

        //------


    }
}