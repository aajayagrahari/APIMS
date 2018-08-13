
//Created On :: 09, October, 2012
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
    public class PartnerMasterManager
    {
        const string PartnerMasterTable = "PartnerMaster";

        ErrorHandler objErrorHandler = new ErrorHandler();

        public PartnerMaster objGetPartnerMaster(string argPartnerCode, string argClientCode)
        {
            PartnerMaster argPartnerMaster = new PartnerMaster();
            DataSet DataSetToFill = new DataSet();

            if (argPartnerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetPartnerMaster(argPartnerCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argPartnerMaster = this.objCreatePartnerMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argPartnerMaster;
        }

        public ICollection<PartnerMaster> colGetPartnerMaster(string argClientCode)
        {
            List<PartnerMaster> lst = new List<PartnerMaster>();
            DataSet DataSetToFill = new DataSet();
            PartnerMaster tPartnerMaster = new PartnerMaster();

            DataSetToFill = this.GetPartnerMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreatePartnerMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetPartnerMaster(string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerMaster4ID", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerMaster(string argPartnerCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetPartnerMaster4ID", param);
            return DataSetToFill;
        }

        public DataSet GetPartnerMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartnerMaster", param);
            return DataSetToFill;
        }

        public DataSet GetPartner4Combo(string argPrefix, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartner4Combo", param);

            return DataSetToFill;
        }

        public DataSet GetPartner4AssignPartnerCombo(string argPartnerCode, string argPrefix, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@Prefix", argPrefix);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetPartner4AssignPartnerCombo", param);

            return DataSetToFill;
        }

        public DataSet GetPartner4GM(string argPartnerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetAsgPartner4Partner", param);

            return DataSetToFill;
        }

        public DataSet GetPartnerMaster(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + PartnerMasterTable.ToString();

                if (iIsDeleted > -1)
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " IsDeleted = " + iIsDeleted;
                }

                if (argClientCode != "")
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " ClientCode = '" + argClientCode + "'";
                }
               
                ds = da.FillDataSetWithSQL(tSQL);

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ds;
        }

        private PartnerMaster objCreatePartnerMaster(DataRow dr)
        {
            PartnerMaster tPartnerMaster = new PartnerMaster();

            tPartnerMaster.SetObjectInfo(dr);

            return tPartnerMaster;

        }

        public ICollection<ErrorHandler> SavePartnerMaster(PartnerMaster argPartnerMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsPartnerMasterExists(argPartnerMaster.PartnerCode, argPartnerMaster.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertPartnerMaster(argPartnerMaster, da, lstErr);
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
                    UpdatePartnerMaster(argPartnerMaster, da, lstErr);
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

        public void SavePartnerMaster(PartnerMaster argPartnerMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsPartnerMasterExists(argPartnerMaster.PartnerCode, argPartnerMaster.ClientCode, da) == false)
                {
                    InsertPartnerMaster(argPartnerMaster, da, lstErr);
                }
                else
                {
                    UpdatePartnerMaster(argPartnerMaster, da, lstErr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
            PartnerMaster ObjPartnerMaster = null;
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

                    foreach (DataRow drExcel in dtExcel.Rows)
                    {
                        ObjPartnerMaster = new PartnerMaster();
                        ObjPartnerMaster.PartnerCode = Convert.ToString(drExcel["PartnerCode"]).Trim();
                        ObjPartnerMaster.ShortCode = Convert.ToString(drExcel["ShortCode"]).Trim();
                        ObjPartnerMaster.PartnerTypeCode = Convert.ToString(drExcel["PartnerTypeCode"]).Trim();
                        ObjPartnerMaster.Title = Convert.ToString(drExcel["Title"]).Trim();
                        ObjPartnerMaster.PartnerName = Convert.ToString(drExcel["PartnerName"]).Trim();
                        ObjPartnerMaster.Address1 = Convert.ToString(drExcel["Address1"]).Trim();
                        ObjPartnerMaster.Address2 = Convert.ToString(drExcel["Address2"]).Trim();
                        ObjPartnerMaster.CountryCode = Convert.ToString(drExcel["CountryCode"]).Trim();
                        ObjPartnerMaster.StateCode = Convert.ToString(drExcel["StateCode"]).Trim();
                        ObjPartnerMaster.City = Convert.ToString(drExcel["City"]).Trim();
                        ObjPartnerMaster.PinCode = Convert.ToString(drExcel["PinCode"]).Trim();
                        ObjPartnerMaster.TelNo = Convert.ToString(drExcel["TelNo"]).Trim();
                        ObjPartnerMaster.FaxNo = Convert.ToString(drExcel["FaxNo"]).Trim();
                        ObjPartnerMaster.MobileNo = Convert.ToString(drExcel["MobileNo"]).Trim();
                        ObjPartnerMaster.EmailID = Convert.ToString(drExcel["EmailID"]).Trim();
                        ObjPartnerMaster.ContactPerson = Convert.ToString(drExcel["ContactPerson"]).Trim();
                        ObjPartnerMaster.PrintName = Convert.ToString(drExcel["PrintName"]).Trim();
                        ObjPartnerMaster.CSTTIN = Convert.ToString(drExcel["CSTTIN"]).Trim();
                        ObjPartnerMaster.VATTIN = Convert.ToString(drExcel["VATTIN"]).Trim();
                        ObjPartnerMaster.PANNo = Convert.ToString(drExcel["PANNo"]).Trim();
                        ObjPartnerMaster.ServiceTaxNo = Convert.ToString(drExcel["ServiceTaxNo"]).Trim();
                        ObjPartnerMaster.DefaultPlantCode = Convert.ToString(drExcel["DefaultPlantCode"]).Trim();
                        ObjPartnerMaster.AnnualSales = Convert.ToInt32(drExcel["AnnualSales"]);
                        ObjPartnerMaster.AnnualSalesCurrency = Convert.ToString(drExcel["AnnualSalesCurrency"]).Trim();
                        ObjPartnerMaster.TotalEmployees = Convert.ToInt32(drExcel["TotalEmployees"]);
                        ObjPartnerMaster.TransportZoneCode = Convert.ToString(drExcel["TransportZoneCode"]).Trim();
                        ObjPartnerMaster.CustomerClassCode = Convert.ToString(drExcel["CustomerClassCode"]).Trim();
                        ObjPartnerMaster.IndustryName = Convert.ToString(drExcel["IndustryName"]).Trim();
                        ObjPartnerMaster.IsBlocked = Convert.ToInt32(drExcel["IsBlocked"]);
                        ObjPartnerMaster.CreatedBy = Convert.ToString(argUserName);
                        ObjPartnerMaster.ModifiedBy = Convert.ToString(argUserName);
                        ObjPartnerMaster.ClientCode = Convert.ToString(argClientCode);
                        SavePartnerMaster(ObjPartnerMaster, da, lstErr);

                        foreach (ErrorHandler objerr in lstErr)
                        {
                            if (objerr.Type == "E")
                            {
                                da.ROLLBACK_TRANSACTION();
                                break;
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

        public void InsertPartnerMaster(PartnerMaster argPartnerMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[36];

            param[0] = new SqlParameter("@PartnerCode", argPartnerMaster.PartnerCode);
            param[1] = new SqlParameter("@PartnerTypeCode", argPartnerMaster.PartnerTypeCode);
            param[2] = new SqlParameter("@ShortCode",argPartnerMaster.ShortCode);
            param[3] = new SqlParameter("@Title", argPartnerMaster.Title);
            param[4] = new SqlParameter("@PartnerName", argPartnerMaster.PartnerName);
            param[5] = new SqlParameter("@Address1", argPartnerMaster.Address1);
            param[6] = new SqlParameter("@Address2", argPartnerMaster.Address2);
            param[7] = new SqlParameter("@CountryCode", argPartnerMaster.CountryCode);
            param[8] = new SqlParameter("@StateCode", argPartnerMaster.StateCode);
            param[9] = new SqlParameter("@City", argPartnerMaster.City);
            param[10] = new SqlParameter("@PinCode", argPartnerMaster.PinCode);
            param[11] = new SqlParameter("@TelNo", argPartnerMaster.TelNo);
            param[12] = new SqlParameter("@FaxNo", argPartnerMaster.FaxNo);
            param[13] = new SqlParameter("@MobileNo", argPartnerMaster.MobileNo);
            param[14] = new SqlParameter("@EmailID", argPartnerMaster.EmailID);
            param[15] = new SqlParameter("@ContactPerson", argPartnerMaster.ContactPerson);
            param[16] = new SqlParameter("@PrintName", argPartnerMaster.PrintName);
            param[17] = new SqlParameter("@CSTTIN", argPartnerMaster.CSTTIN);
            param[18] = new SqlParameter("@VATTIN", argPartnerMaster.VATTIN);
            param[19] = new SqlParameter("@PANNo", argPartnerMaster.PANNo);
            param[20] = new SqlParameter("@ServiceTaxNo", argPartnerMaster.ServiceTaxNo);
            param[21] = new SqlParameter("@DefaultPlantCode", argPartnerMaster.DefaultPlantCode);
            param[22] = new SqlParameter("@AnnualSales", argPartnerMaster.AnnualSales);
            param[23] = new SqlParameter("@AnnualSalesCurrency", argPartnerMaster.AnnualSalesCurrency);
            param[24] = new SqlParameter("@TotalEmployees", argPartnerMaster.TotalEmployees);
            param[25] = new SqlParameter("@TransportZoneCode", argPartnerMaster.TransportZoneCode);
            param[26] = new SqlParameter("@CustomerClassCode", argPartnerMaster.CustomerClassCode);
            param[27] = new SqlParameter("@IndustryName", argPartnerMaster.IndustryName);
            param[28] = new SqlParameter("@CompanyOwned", argPartnerMaster.CompanyOwned);
            param[29] = new SqlParameter("@IsBlocked", argPartnerMaster.IsBlocked);
            param[30] = new SqlParameter("@ClientCode", argPartnerMaster.ClientCode);
            param[31] = new SqlParameter("@CreatedBy", argPartnerMaster.CreatedBy);
            param[32] = new SqlParameter("@ModifiedBy", argPartnerMaster.ModifiedBy);
            
            param[33] = new SqlParameter("@Type", SqlDbType.Char);
            param[33].Size = 1;
            param[33].Direction = ParameterDirection.Output;

            param[34] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[34].Size = 255;
            param[34].Direction = ParameterDirection.Output;

            param[35] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[35].Size = 20;
            param[35].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertPartnerMaster", param);
            
            string strMessage = Convert.ToString(param[34].Value);
            string strType = Convert.ToString(param[33].Value);
            string strRetValue = Convert.ToString(param[35].Value);


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

        public void UpdatePartnerMaster(PartnerMaster argPartnerMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[36];

            param[0] = new SqlParameter("@PartnerCode", argPartnerMaster.PartnerCode);
            param[1] = new SqlParameter("@PartnerTypeCode", argPartnerMaster.PartnerTypeCode);
            param[2] = new SqlParameter("@ShortCode", argPartnerMaster.ShortCode);
            param[3] = new SqlParameter("@Title", argPartnerMaster.Title);
            param[4] = new SqlParameter("@PartnerName", argPartnerMaster.PartnerName);
            param[5] = new SqlParameter("@Address1", argPartnerMaster.Address1);
            param[6] = new SqlParameter("@Address2", argPartnerMaster.Address2);
            param[7] = new SqlParameter("@CountryCode", argPartnerMaster.CountryCode);
            param[8] = new SqlParameter("@StateCode", argPartnerMaster.StateCode);
            param[9] = new SqlParameter("@City", argPartnerMaster.City);
            param[10] = new SqlParameter("@PinCode", argPartnerMaster.PinCode);
            param[11] = new SqlParameter("@TelNo", argPartnerMaster.TelNo);
            param[12] = new SqlParameter("@FaxNo", argPartnerMaster.FaxNo);
            param[13] = new SqlParameter("@MobileNo", argPartnerMaster.MobileNo);
            param[14] = new SqlParameter("@EmailID", argPartnerMaster.EmailID);
            param[15] = new SqlParameter("@ContactPerson", argPartnerMaster.ContactPerson);
            param[16] = new SqlParameter("@PrintName", argPartnerMaster.PrintName);
            param[17] = new SqlParameter("@CSTTIN", argPartnerMaster.CSTTIN);
            param[18] = new SqlParameter("@VATTIN", argPartnerMaster.VATTIN);
            param[19] = new SqlParameter("@PANNo", argPartnerMaster.PANNo);
            param[20] = new SqlParameter("@ServiceTaxNo", argPartnerMaster.ServiceTaxNo);
            param[21] = new SqlParameter("@DefaultPlantCode", argPartnerMaster.DefaultPlantCode);
            param[22] = new SqlParameter("@AnnualSales", argPartnerMaster.AnnualSales);
            param[23] = new SqlParameter("@AnnualSalesCurrency", argPartnerMaster.AnnualSalesCurrency);
            param[24] = new SqlParameter("@TotalEmployees", argPartnerMaster.TotalEmployees);
            param[25] = new SqlParameter("@TransportZoneCode", argPartnerMaster.TransportZoneCode);
            param[26] = new SqlParameter("@CustomerClassCode", argPartnerMaster.CustomerClassCode);
            param[27] = new SqlParameter("@IndustryName", argPartnerMaster.IndustryName);
            param[28] = new SqlParameter("@CompanyOwned", argPartnerMaster.CompanyOwned);
            param[29] = new SqlParameter("@IsBlocked", argPartnerMaster.IsBlocked);
            param[30] = new SqlParameter("@ClientCode", argPartnerMaster.ClientCode);
            param[31] = new SqlParameter("@CreatedBy", argPartnerMaster.CreatedBy);
            param[32] = new SqlParameter("@ModifiedBy", argPartnerMaster.ModifiedBy);

            param[33] = new SqlParameter("@Type", SqlDbType.Char);
            param[33].Size = 1;
            param[33].Direction = ParameterDirection.Output;

            param[34] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[34].Size = 255;
            param[34].Direction = ParameterDirection.Output;

            param[35] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[35].Size = 20;
            param[35].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdatePartnerMaster", param);
            
            string strMessage = Convert.ToString(param[34].Value);
            string strType = Convert.ToString(param[33].Value);
            string strRetValue = Convert.ToString(param[35].Value);
            
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

        public ICollection<ErrorHandler> DeletePartnerMaster(string argPartnerCode, string argClientCode,int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@PartnerCode", argPartnerCode);
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

                int i = da.ExecuteNonQuery("Proc_DeletePartnerMaster", param);


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

        public bool blnIsPartnerMasterExists(string argPartnerCode, string argClientCode)
        {
            bool IsPartnerMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerMaster(argPartnerCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerMasterExists = true;
            }
            else
            {
                IsPartnerMasterExists = false;
            }
            return IsPartnerMasterExists;
        }

        public bool blnIsPartnerMasterExists(string argPartnerCode, string argClientCode, DataAccess da)
        {
            bool IsPartnerMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetPartnerMaster(argPartnerCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsPartnerMasterExists = true;
            }
            else
            {
                IsPartnerMasterExists = false;
            }
            return IsPartnerMasterExists;
        }
    }
}