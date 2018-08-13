
//Created On :: 18, December, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_Partner
{
    public class PartnerMatPriceListManager
    {
        const string PartnerMatPriceListTable = "PartnerMatPriceList";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public PartnerMatPriceList objGetPartnerMatPriceList(int argPriceListID, string argPartnerTypeCode, string argMaterialCode,string argUOMCode,string argValidFrom,string argValidTo, string argClientCode)
        {
            PartnerMatPriceList argPartnerMatPriceList = new PartnerMatPriceList();
            DataSet DataSetToFill = new DataSet();

            if (argPriceListID <= 0)
            {
                goto ErrorHandlers;
            }

            if (argPartnerTypeCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argMaterialCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            if (argUOMCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argValidFrom.Trim() == "")
            {
                goto ErrorHandlers;
            }
            if (argValidTo.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPartnerMatPriceList(argPriceListID, argPartnerTypeCode, argMaterialCode, argUOMCode, argValidFrom, argValidTo ,argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartnerMatPriceList = this.objCreatePartnerMatPriceList((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPartnerMatPriceList;
        }

        public ICollection<PartnerMatPriceList> colGetPartnerMatPriceList(string argClientCode)
        {
            List<PartnerMatPriceList> lst = new List<PartnerMatPriceList>();
            DataSet DataSetToFill = new DataSet();
            PartnerMatPriceList tPartnerMatPriceList = new PartnerMatPriceList();

            DataSetToFill = this.GetPartnerMatPriceList(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePartnerMatPriceList(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<PartnerMatPriceList> colGetPartnerMatPriceList(DataTable dt, string argUserName, string clientCode)
        {
            List<PartnerMatPriceList> lst = new List<PartnerMatPriceList>();
            PartnerMatPriceList objPartnerMatPriceList = null;
            foreach (DataRow dr in dt.Rows)
            {
                objPartnerMatPriceList = new PartnerMatPriceList();
                objPartnerMatPriceList.PartnerTypeCode = Convert.ToString(dr["PartnerTypeCode"]).Trim();
                objPartnerMatPriceList.MaterialCode = Convert.ToString(dr["MaterialCode"]).Trim();
                objPartnerMatPriceList.Price = Convert.ToDouble(dr["Price"]);
                objPartnerMatPriceList.UOMCode = Convert.ToString(dr["UOMCode"]).Trim();
                objPartnerMatPriceList.ValidFrom = Convert.ToString(dr["ValidFrom"]).Trim();
                objPartnerMatPriceList.ValidTo = Convert.ToString(dr["ValidTo"]).Trim();
                objPartnerMatPriceList.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objPartnerMatPriceList.ModifiedBy = Convert.ToString(argUserName).Trim();
                objPartnerMatPriceList.CreatedBy = Convert.ToString(argUserName).Trim();
                objPartnerMatPriceList.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objPartnerMatPriceList);
            }
            return lst;
        }

        public DataSet GetPartnerMatPriceList(int argPriceListID, string argPartnerTypeCode, string argMaterialCode, string argUOMCode ,string argValidFrom, string argValidTo, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@PriceListID", argPriceListID);
            param[1] = new SqlParameter("@PartnerTypeCode", argPartnerTypeCode);
            param[2] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[3] = new SqlParameter("@UOMCode", argUOMCode);
            param[4] = new SqlParameter("@ValidFrom", argValidFrom);
            param[5] = new SqlParameter("@ValidTo", argValidTo);
            param[6] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerMatPriceList4ID", param);

            return DataSetToFill;
        }
        public DataSet GetPartnerMatPriceList(int argPriceListID, string argPartnerTypeCode, string argMaterialCode, string argUOMCode ,string argValidFrom, string argValidTo, string argClientCode,DataAccess da)
        {
            
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[7];
            param[0] = new SqlParameter("@PriceListID", argPriceListID);
            param[1] = new SqlParameter("@PartnerTypeCode", argPartnerTypeCode);
            param[2] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[3] = new SqlParameter("@UOMCode", argUOMCode);
            param[4] = new SqlParameter("@ValidFrom", argValidFrom);
            param[5] = new SqlParameter("@ValidTo", argValidTo);
            param[6] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPartnerMatPriceList4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerMatPriceList(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerMatPriceList", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerPriceList4Material(string argMaterialCode, string argPartnerCode, string argClientCode, DateTime argCurrtDate)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);
            param[3] = new SqlParameter("@CurDate", argCurrtDate);

            DataSetToFill = da.FillDataSet("SP_GetPartnerPriceList4Material", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerPriceList4EndUser(string argMaterialCode, string argPartnerCode, string argClientCode, DateTime argCurrtDate)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);
            param[3] = new SqlParameter("@CurDate", argCurrtDate);

            DataSetToFill = da.FillDataSet("SP_GetPartnerPriceList4EndUser", param);

            return DataSetToFill;
        }

        private PartnerMatPriceList objCreatePartnerMatPriceList(DataRow dr)
        {
            PartnerMatPriceList tPartnerMatPriceList = new PartnerMatPriceList();

            tPartnerMatPriceList.SetObjectInfo(dr);

            return tPartnerMatPriceList;

        }

        public ICollection<ErrorHandler> SavePartnerMatPriceList(PartnerMatPriceList argPartnerMatPriceList)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartnerMatPriceListExists(argPartnerMatPriceList.PriceListID, argPartnerMatPriceList.PartnerTypeCode, argPartnerMatPriceList.MaterialCode,argPartnerMatPriceList.UOMCode, argPartnerMatPriceList.ValidFrom,argPartnerMatPriceList.ValidTo ,argPartnerMatPriceList.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartnerMatPriceList(argPartnerMatPriceList, da, lstErr);
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
                    UpdatePartnerMatPriceList(argPartnerMatPriceList, da, lstErr);
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

        public ICollection<ErrorHandler> SavePartnerMatPriceList(ICollection<PartnerMatPriceList> colGetPartnerMatPriceList, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (PartnerMatPriceList argPartnerMatPriceList in colGetPartnerMatPriceList)
                {
                    if (argPartnerMatPriceList.IsDeleted == 0)
                    {
                        if (blnIsPartnerMatPriceListExists(argPartnerMatPriceList.PriceListID,argPartnerMatPriceList.PartnerTypeCode,argPartnerMatPriceList.MaterialCode,argPartnerMatPriceList.UOMCode,argPartnerMatPriceList.ValidFrom,argPartnerMatPriceList.ValidTo, argPartnerMatPriceList.ClientCode, da) == false)
                        {
                            InsertPartnerMatPriceList(argPartnerMatPriceList, da, lstErr);
                        }
                        else
                        {
                            UpdatePartnerMatPriceList(argPartnerMatPriceList, da, lstErr);
                        }
                    }
                    else
                    {
                        DeletePartnerMatPriceList(argPartnerMatPriceList.PriceListID, argPartnerMatPriceList.ClientCode, argPartnerMatPriceList.IsDeleted);
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

        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
            string xConnStr = "";
            string strSheetName = "";
            DataSet dsExcel = new DataSet();
            DataTable dtTableSchema = new DataTable();
            OleDbConnection objXConn = null;
            OleDbDataAdapter objDataAdapter = new OleDbDataAdapter();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

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

                /*****************************************/
                DataAccess da = new DataAccess();
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                try
                {
                    SavePartnerMatPriceList(colGetPartnerMatPriceList(dtExcel, argUserName, argClientCode), lstErr);

                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            break;
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objXConn.Close();
            }
            return lstErr;
        }


        public void InsertPartnerMatPriceList(PartnerMatPriceList argPartnerMatPriceList, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@PriceListID", argPartnerMatPriceList.PriceListID);
            param[1] = new SqlParameter("@PartnerTypeCode", argPartnerMatPriceList.PartnerTypeCode);
            param[2] = new SqlParameter("@MaterialCode", argPartnerMatPriceList.MaterialCode);
            param[3] = new SqlParameter("@Price", argPartnerMatPriceList.Price);
            param[4] = new SqlParameter("@UOMCode", argPartnerMatPriceList.UOMCode);
            param[5] = new SqlParameter("@ValidFrom", argPartnerMatPriceList.ValidFrom);
            param[6] = new SqlParameter("@ValidTo", argPartnerMatPriceList.ValidTo);
            param[7] = new SqlParameter("@ClientCode", argPartnerMatPriceList.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argPartnerMatPriceList.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argPartnerMatPriceList.ModifiedBy);

            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPartnerMatPriceList", param);


            string strMessage = Convert.ToString(param[11].Value);
            string strType = Convert.ToString(param[10].Value);
            string strRetValue = Convert.ToString(param[12].Value);


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

        public void UpdatePartnerMatPriceList(PartnerMatPriceList argPartnerMatPriceList, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[13];
            param[0] = new SqlParameter("@PriceListID", argPartnerMatPriceList.PriceListID);
            param[1] = new SqlParameter("@PartnerTypeCode", argPartnerMatPriceList.PartnerTypeCode);
            param[2] = new SqlParameter("@MaterialCode", argPartnerMatPriceList.MaterialCode);
            param[3] = new SqlParameter("@Price", argPartnerMatPriceList.Price);
            param[4] = new SqlParameter("@UOMCode", argPartnerMatPriceList.UOMCode);
            param[5] = new SqlParameter("@ValidFrom", argPartnerMatPriceList.ValidFrom);
            param[6] = new SqlParameter("@ValidTo", argPartnerMatPriceList.ValidTo);
            param[7] = new SqlParameter("@ClientCode", argPartnerMatPriceList.ClientCode);
            param[8] = new SqlParameter("@CreatedBy", argPartnerMatPriceList.CreatedBy);
            param[9] = new SqlParameter("@ModifiedBy", argPartnerMatPriceList.ModifiedBy);

            param[10] = new SqlParameter("@Type", SqlDbType.Char);
            param[10].Size = 1;
            param[10].Direction = ParameterDirection.Output;

            param[11] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[11].Size = 255;
            param[11].Direction = ParameterDirection.Output;

            param[12] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[12].Size = 20;
            param[12].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePartnerMatPriceList", param);

            string strMessage = Convert.ToString(param[11].Value);
            string strType = Convert.ToString(param[10].Value);
            string strRetValue = Convert.ToString(param[12].Value);

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

        public ICollection<ErrorHandler> DeletePartnerMatPriceList(int argPriceListID, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];

                param[0] = new SqlParameter("@PriceListID", argPriceListID);
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

                int i = da.ExecuteNonQuery("Proc_DeletePartnerMatPriceList", param);


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

        public bool blnIsPartnerMatPriceListExists(int argPriceListID, string argPartnerTypeCode, string argMaterialCode, string argUOMCode, string argValidFrom, string argValidTo, string argClientCode)
        {
            bool IsPartnerMatPriceListExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerMatPriceList(argPriceListID, argPartnerTypeCode, argMaterialCode, argUOMCode,argValidFrom,argValidTo, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerMatPriceListExists = true;
            }
            else
            {
                IsPartnerMatPriceListExists = false;
            }
            return IsPartnerMatPriceListExists;
        }

        public bool blnIsPartnerMatPriceListExists(int argPriceListID, string argPartnerTypeCode, string argMaterialCode, string argUOMCode, string argValidFrom, string argValidTo, string argClientCode, DataAccess da)
        {
            bool IsPartnerMatPriceListExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerMatPriceList(argPriceListID, argPartnerTypeCode, argMaterialCode,argUOMCode,argValidFrom, argValidTo, argClientCode,da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerMatPriceListExists = true;
            }
            else
            {
                IsPartnerMatPriceListExists = false;
            }
            return IsPartnerMatPriceListExists;
        }
    }
}