
//Created On :: 18, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_SD
{
    public class VendorManager
    {
        const string VendorTable = "Vendor";

        ///GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Vendor objGetVendor(string argVendorCode, string argClientCode)
        {
            Vendor argVendor = new Vendor();
            DataSet DataSetToFill = new DataSet();

            if (argVendorCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetVendor(argVendorCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argVendor = this.objCreateVendor((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argVendor;
        }

        public ICollection<Vendor> colGetVendor(string argClientCode)
        {
            List<Vendor> lst = new List<Vendor>();
            DataSet DataSetToFill = new DataSet();
            Vendor tVendor = new Vendor();

            DataSetToFill = this.GetVendor(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateVendor(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public DataSet GetVendor(string argVendorCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@VendorCode", argVendorCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetVendor4ID", param);

            return DataSetToFill;
        }

        public DataSet GetVendor(string argVendorCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@VendorCode", argVendorCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetVendor4ID", param);

            return DataSetToFill;
        }

        public DataSet GetVendor(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetVendor", param);
            return DataSetToFill;
        }

        public DataSet GetVendor4Combo(string argPrefix, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetVendor4Combo", param);

            return DataSetToFill;
        }

        public DataSet GetVendor4PO(string argPrefix, string argPurchaseOrgCode,string argCompanyCode ,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@PurchaseOrgCode", argPurchaseOrgCode);
            param[2] = new SqlParameter("@CompanyCode", argCompanyCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetVendor4PO", param);

            return DataSetToFill;
        }

        public DataSet GetVendor(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT VendorCode,(Name1 + + Name2) as VendorName,CurrencyCode, City FROM " + VendorTable.ToString();

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

        private Vendor objCreateVendor(DataRow dr)
        {
            Vendor tVendor = new Vendor();

            tVendor.SetObjectInfo(dr);

            return tVendor;

        }
        
        public ICollection<ErrorHandler> SaveVendor(Vendor argVendor)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsVendorExists(argVendor.VendorCode, argVendor.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertVendor(argVendor, da, lstErr);
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
                    UpdateVendor(argVendor, da, lstErr);
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

        public void SaveVendor(Vendor argVendor, DataAccess da, List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsVendorExists(argVendor.VendorCode, argVendor.ClientCode, da) == false)
                {
                    InsertVendor(argVendor, da, lstErr);
                }
                else
                {
                    UpdateVendor(argVendor, da, lstErr);
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
            Vendor ObjVendor = null;
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
                        ObjVendor = new Vendor();
                        ObjVendor.VendorCode = Convert.ToString(drExcel["VendorCode"]).Trim();
                        ObjVendor.Name1 = Convert.ToString(drExcel["Name1"]).Trim();
                        ObjVendor.Name2 = Convert.ToString(drExcel["Name2"]).Trim();
                        ObjVendor.VendorAccTypeCode = Convert.ToString(drExcel["VendorAccTypeCode"]).Trim();
                        ObjVendor.Address1 = Convert.ToString(drExcel["Address1"]).Trim();
                        ObjVendor.Address2 = Convert.ToString(drExcel["Address2"]).Trim();
                        ObjVendor.CountryCode = Convert.ToString(drExcel["CountryCode"]).Trim();
                        ObjVendor.StateCode = Convert.ToString(drExcel["StateCode"]).Trim();
                        ObjVendor.City = Convert.ToString(drExcel["City"]).Trim();
                        ObjVendor.PinCode = Convert.ToString(drExcel["PinCode"]).Trim();
                        ObjVendor.TelNo = Convert.ToString(drExcel["TelNo"]).Trim();
                        ObjVendor.MobileNo = Convert.ToString(drExcel["MobileNo"]).Trim();
                        ObjVendor.FaxNo = Convert.ToString(drExcel["FaxNo"]).Trim();
                        ObjVendor.Title = Convert.ToString(drExcel["Title"]).Trim();
                        ObjVendor.ContactPerson = Convert.ToString(drExcel["ContactPerson"]).Trim();
                        ObjVendor.PANNo = Convert.ToString(drExcel["PANNo"]).Trim();
                        ObjVendor.TINNo = Convert.ToString(drExcel["TINNo"]).Trim();
                        ObjVendor.VATNo = Convert.ToString(drExcel["VATNo"]).Trim();
                        ObjVendor.CurrencyCode = Convert.ToString(drExcel["CurrencyCode"]).Trim();
                        ObjVendor.TypeOfBusiness = Convert.ToString(drExcel["TypeOfBusiness"]).Trim();
                        ObjVendor.TypeOfIndustry = Convert.ToString(drExcel["TypeOfIndustry"]).Trim();
                        ObjVendor.IndustryName = Convert.ToString(drExcel["IndustryName"]).Trim();
                        ObjVendor.EMailID = Convert.ToString(drExcel["EMailID"]).Trim();
                        ObjVendor.MappedCode = Convert.ToString(drExcel["MappedCode"]).Trim();
                        ObjVendor.MappedAs = Convert.ToString(drExcel["MappedAs"]).Trim();
                        ObjVendor.CreatedBy = Convert.ToString(argUserName);
                        ObjVendor.ModifiedBy = Convert.ToString(argUserName);
                        ObjVendor.ClientCode = Convert.ToString(argClientCode);
                        SaveVendor(ObjVendor, da, lstErr);

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

        public void InsertVendor(Vendor argVendor, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[31];
            param[0] = new SqlParameter("@VendorCode", argVendor.VendorCode);
            param[1] = new SqlParameter("@Name1", argVendor.Name1);
            param[2] = new SqlParameter("@Name2", argVendor.Name2);
            param[3] = new SqlParameter("@VendorAccTypeCode", argVendor.VendorAccTypeCode);
            param[4] = new SqlParameter("@Address1", argVendor.Address1);
            param[5] = new SqlParameter("@Address2", argVendor.Address2);
            param[6] = new SqlParameter("@CountryCode", argVendor.CountryCode);
            param[7] = new SqlParameter("@StateCode", argVendor.StateCode);
            param[8] = new SqlParameter("@City", argVendor.City);
            param[9] = new SqlParameter("@PinCode", argVendor.PinCode);
            param[10] = new SqlParameter("@TelNo", argVendor.TelNo);
            param[11] = new SqlParameter("@MobileNo",argVendor.MobileNo);
            param[12] = new SqlParameter("@EmailID",argVendor.EMailID);
            param[13] = new SqlParameter("@FaxNo", argVendor.FaxNo);
            param[14] = new SqlParameter("@Title", argVendor.Title);
            param[15] = new SqlParameter("@ContactPerson", argVendor.ContactPerson);
            param[16] = new SqlParameter("@PANNo", argVendor.PANNo);
            param[17] = new SqlParameter("@TINNo", argVendor.TINNo);
            param[18] = new SqlParameter("@VATNo", argVendor.VATNo);
            param[19] = new SqlParameter("@MappedAs", argVendor.MappedAs);
            param[20] = new SqlParameter("@MappedCode", argVendor.MappedCode);
            param[21] = new SqlParameter("@TypeOfBusiness", argVendor.TypeOfBusiness);
            param[22] = new SqlParameter("@TypeOfIndustry", argVendor.TypeOfIndustry);
            param[23] = new SqlParameter("@IndustryName", argVendor.IndustryName);
            param[24] = new SqlParameter("@CurrencyCode",argVendor.CurrencyCode);
            param[25] = new SqlParameter("@ClientCode", argVendor.ClientCode);
            param[26] = new SqlParameter("@CreatedBy", argVendor.CreatedBy);
            param[27] = new SqlParameter("@ModifiedBy", argVendor.ModifiedBy);

            param[28] = new SqlParameter("@Type", SqlDbType.Char);
            param[28].Size = 1;
            param[28].Direction = ParameterDirection.Output;

            param[29] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[29].Size = 255;
            param[29].Direction = ParameterDirection.Output;

            param[30] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[30].Size = 20;
            param[30].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertVendor", param);

            string strMessage = Convert.ToString(param[29].Value);
            string strType = Convert.ToString(param[28].Value);
            string strRetValue = Convert.ToString(param[30].Value);
            
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

        public void UpdateVendor(Vendor argVendor, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[31];
            param[0] = new SqlParameter("@VendorCode", argVendor.VendorCode);
            param[1] = new SqlParameter("@Name1", argVendor.Name1);
            param[2] = new SqlParameter("@Name2", argVendor.Name2);
            param[3] = new SqlParameter("@VendorAccTypeCode", argVendor.VendorAccTypeCode);
            param[4] = new SqlParameter("@Address1", argVendor.Address1);
            param[5] = new SqlParameter("@Address2", argVendor.Address2);
            param[6] = new SqlParameter("@CountryCode", argVendor.CountryCode);
            param[7] = new SqlParameter("@StateCode", argVendor.StateCode);
            param[8] = new SqlParameter("@City", argVendor.City);
            param[9] = new SqlParameter("@PinCode", argVendor.PinCode);
            param[10] = new SqlParameter("@TelNo", argVendor.TelNo);
            param[11] = new SqlParameter("@MobileNo", argVendor.MobileNo);
            param[12] = new SqlParameter("@EmailID", argVendor.EMailID);
            param[13] = new SqlParameter("@FaxNo", argVendor.FaxNo);
            param[14] = new SqlParameter("@Title", argVendor.Title);
            param[15] = new SqlParameter("@ContactPerson", argVendor.ContactPerson);
            param[16] = new SqlParameter("@PANNo", argVendor.PANNo);
            param[17] = new SqlParameter("@TINNo", argVendor.TINNo);
            param[18] = new SqlParameter("@VATNo", argVendor.VATNo);
            param[19] = new SqlParameter("@MappedAs", argVendor.MappedAs);
            param[20] = new SqlParameter("@MappedCode", argVendor.MappedCode);
            param[21] = new SqlParameter("@TypeOfBusiness", argVendor.TypeOfBusiness);
            param[22] = new SqlParameter("@TypeOfIndustry", argVendor.TypeOfIndustry);
            param[23] = new SqlParameter("@IndustryName", argVendor.IndustryName);
            param[24] = new SqlParameter("@CurrencyCode", argVendor.CurrencyCode);
            param[25] = new SqlParameter("@ClientCode", argVendor.ClientCode);
            param[26] = new SqlParameter("@CreatedBy", argVendor.CreatedBy);
            param[27] = new SqlParameter("@ModifiedBy", argVendor.ModifiedBy);

            param[28] = new SqlParameter("@Type", SqlDbType.Char);
            param[28].Size = 1;
            param[28].Direction = ParameterDirection.Output;

            param[29] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[29].Size = 255;
            param[29].Direction = ParameterDirection.Output;

            param[30] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[30].Size = 20;
            param[30].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateVendor", param);


            string strMessage = Convert.ToString(param[29].Value);
            string strType = Convert.ToString(param[28].Value);
            string strRetValue = Convert.ToString(param[30].Value);


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

        public ICollection<ErrorHandler> DeleteVendor(string argVendorCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@VendorCode", argVendorCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteVendor", param);

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

        public bool blnIsVendorExists(string argVendorCode, string argClientCode)
        {
            bool IsVendorExists = false;
            DataSet ds = new DataSet();
            ds = GetVendor(argVendorCode, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsVendorExists = true;
            }
            else
            {
                IsVendorExists = false;
            }
            return IsVendorExists;
        }

        public bool blnIsVendorExists(string argVendorCode, string argClientCode, DataAccess da)
        {
            bool IsVendorExists = false;
            DataSet ds = new DataSet();
            ds = GetVendor(argVendorCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsVendorExists = true;
            }
            else
            {
                IsVendorExists = false;
            }
            return IsVendorExists;
        }
    }
}