
//Created On :: 18, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using RSMApp_Organization;
using System.Data.OleDb;

namespace RSMApp_SD
{
    public class Customer_SalesAreaManager
    {
        const string Customer_SalesAreaTable = "Customer_SalesArea";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        Customer_PartnerFunctionManager objCustomer_PartnerFunctionManager = new Customer_PartnerFunctionManager();
        Customer_TaxManager objCustomer_TaxManager = new Customer_TaxManager();
        ErrorHandler objErrorHandler = new ErrorHandler();

        public Customer_SalesArea objGetCustomer_SalesArea(string argCustomerCode, string argSalesOrganizationCode, string argDivisionCode, string argDistChannelCode, string argClientCode)
        {
            Customer_SalesArea argCustomer_SalesArea = new Customer_SalesArea();
            DataSet DataSetToFill = new DataSet();

            if (argCustomerCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argSalesOrganizationCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argDivisionCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argDistChannelCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }
            DataSetToFill = this.GetCustomer_SalesArea(argCustomerCode, argSalesOrganizationCode, argDivisionCode, argDistChannelCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argCustomer_SalesArea = this.objCreateCustomer_SalesArea((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argCustomer_SalesArea;
        }

        public ICollection<Customer_SalesArea> colGetCustomer_SalesArea(string argCustomerCode, string argClientCode)
        {
            List<Customer_SalesArea> lst = new List<Customer_SalesArea>();
            DataSet DataSetToFill = new DataSet();
            Customer_SalesArea tCustomer_SalesArea = new Customer_SalesArea();
            DataSetToFill = this.GetCustomer_SalesArea(argCustomerCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateCustomer_SalesArea(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public ICollection<Customer_SalesArea> colGetCustomer_SalesArea(DataTable dt, string argUserName, string clientCode)
        {
            List<Customer_SalesArea> lst = new List<Customer_SalesArea>();
            Customer_SalesArea objCustomer_SalesArea = null;
            foreach (DataRow dr in dt.Rows)
            {
                objCustomer_SalesArea = new Customer_SalesArea();
                objCustomer_SalesArea.CustomerCode = Convert.ToString(dr["CustomerCode"]).Trim();
                objCustomer_SalesArea.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]).Trim();
                objCustomer_SalesArea.DivisionCode = Convert.ToString(dr["DivisionCode"]).Trim();
                objCustomer_SalesArea.DistChannelCode = Convert.ToString(dr["DistChannelCode"]).Trim();
                objCustomer_SalesArea.SalesDistrictCode = Convert.ToString(dr["SalesDistrictCode"]).Trim();
                objCustomer_SalesArea.SalesRegionCode = Convert.ToString(dr["SalesRegionCode"]).Trim();
                objCustomer_SalesArea.SalesOfficeCode = Convert.ToString(dr["SalesOfficeCode"]).Trim();
                objCustomer_SalesArea.SalesGroupCode = Convert.ToString(dr["SalesGroupCode"]).Trim();
                objCustomer_SalesArea.DlvPriorityCode = Convert.ToString(dr["DlvPriorityCode"]).Trim();
                objCustomer_SalesArea.CurrencyCode = Convert.ToString(dr["CurrencyCode"]).Trim();
                objCustomer_SalesArea.CustGroupCode = Convert.ToString(dr["CustGroupCode"]).Trim();
                objCustomer_SalesArea.AccAssignGroupCode = Convert.ToString(dr["AccAssignGroupCode"]).Trim();
                objCustomer_SalesArea.CrContAreaCode = Convert.ToString(dr["CrContAreaCode"]).Trim();
                objCustomer_SalesArea.PaymentTermsCode = Convert.ToString(dr["PaymentTermsCode"]).Trim();
                objCustomer_SalesArea.IncoTermsCode = Convert.ToString(dr["IncoTermsCode"]).Trim();
                objCustomer_SalesArea.DefaultPlantCode = Convert.ToString(dr["DefaultPlantCode"]).Trim();
                objCustomer_SalesArea.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objCustomer_SalesArea.ModifiedBy = Convert.ToString(argUserName).Trim();
                objCustomer_SalesArea.CreatedBy = Convert.ToString(argUserName).Trim();
                objCustomer_SalesArea.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objCustomer_SalesArea);
            }
            return lst;
        }

        public ICollection<Customer_Tax> colGetCustomer_Tax(DataTable dt, string argUserName, string clientCode)
        {
            List<Customer_Tax> lst = new List<Customer_Tax>();
            Customer_Tax objCustomer_Tax = null;
            foreach (DataRow dr in dt.Rows)
            {
                objCustomer_Tax = new Customer_Tax();
                objCustomer_Tax.CustomerCode = Convert.ToString(dr["CustomerCode"]).Trim();
                objCustomer_Tax.CountryCode = Convert.ToString(dr["CountryCode"]).Trim();
                objCustomer_Tax.TaxCategoryCode = Convert.ToString(dr["TaxCategoryCode"]).Trim();
                objCustomer_Tax.TaxClassCode = Convert.ToString(dr["TaxClassCode"]).Trim();
                objCustomer_Tax.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objCustomer_Tax.ModifiedBy = Convert.ToString(argUserName).Trim();
                objCustomer_Tax.CreatedBy = Convert.ToString(argUserName).Trim();
                objCustomer_Tax.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objCustomer_Tax);
            }
            return lst;
        }

        public ICollection<Customer_PartnerFunction> colGetCustomer_PartnerFunction(DataTable dt, string argUserName, string clientCode)
        {
            List<Customer_PartnerFunction> lst = new List<Customer_PartnerFunction>();
            Customer_PartnerFunction objCustomer_PartnerFunction = null;
            foreach (DataRow dr in dt.Rows)
            {
                objCustomer_PartnerFunction = new Customer_PartnerFunction();
                objCustomer_PartnerFunction.CustomerCode = Convert.ToString(dr["CustomerCode"]).Trim();
                objCustomer_PartnerFunction.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]).Trim();
                objCustomer_PartnerFunction.DivisionCode = Convert.ToString(dr["DivisionCode"]).Trim();
                objCustomer_PartnerFunction.DistChannelCode = Convert.ToString(dr["DistChannelCode"]).Trim();
                objCustomer_PartnerFunction.PFunctionCode = Convert.ToString(dr["PFunctionCode"]).Trim();
                objCustomer_PartnerFunction.PartnerCounter = Convert.ToInt32(dr["PartnerCounter"]);
                objCustomer_PartnerFunction.PartnerTable = Convert.ToString(dr["PartnerTable"]).Trim();
                objCustomer_PartnerFunction.PartnerCode = Convert.ToString(dr["PartnerCode"]).Trim();
                objCustomer_PartnerFunction.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);
                objCustomer_PartnerFunction.ModifiedBy = Convert.ToString(argUserName).Trim();
                objCustomer_PartnerFunction.CreatedBy = Convert.ToString(argUserName).Trim();
                objCustomer_PartnerFunction.ClientCode = Convert.ToString(clientCode).Trim();
                lst.Add(objCustomer_PartnerFunction);
            }
            return lst;
        }

        public DataSet GetCustomer_SalesArea(string argCustomerCode, string argSalesOrganizationCode, string argDivisionCode, string argDistChannelCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[3] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCustomer_SalesArea4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCustomer_SalesArea(string argCustomerCode, string argSalesOrganizationCode, string argDivisionCode, string argDistChannelCode, string argClientCode, DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[3] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetCustomer_SalesArea4ID", param);
            return DataSetToFill;
        }

        public DataSet GetCustomer_SalesArea4Combo(string argPrefix, string argSalesOrganizationCode, string argDivisionCode, string argDistChannelCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
            param[3] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCustomer_SalesArea4Combo", param);
            return DataSetToFill;
        }

        public DataSet GetCustomer_SalesArea(string argCustomerCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetCustomer_SalesArea", param);
            return DataSetToFill;
        }

        private Customer_SalesArea objCreateCustomer_SalesArea(DataRow dr)
        {
            Customer_SalesArea tCustomer_SalesArea = new Customer_SalesArea();
            tCustomer_SalesArea.SetObjectInfo(dr);
            return tCustomer_SalesArea;
        }

        //public ICollection<ErrorHandler> SaveCustomer_SalesArea(Customer_SalesArea argCustomer_SalesArea)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    SalesAreaManager ObjSalesAreaManager = new SalesAreaManager();
        //    try
        //    {
        //        if (ObjSalesAreaManager.blnIsSalesAreaExists(argCustomer_SalesArea.SalesOrganizationCode, argCustomer_SalesArea.DistChannelCode, argCustomer_SalesArea.DivisionCode, argCustomer_SalesArea.ClientCode) == true)
        //        {

        //            if (blnIsCustomer_SalesAreaExists(argCustomer_SalesArea.CustomerCode, argCustomer_SalesArea.SalesOrganizationCode, argCustomer_SalesArea.DivisionCode, argCustomer_SalesArea.DistChannelCode, argCustomer_SalesArea.ClientCode) == false)
        //            {

        //                da.Open_Connection();
        //                da.BEGIN_TRANSACTION();
        //                InsertCustomer_SalesArea(argCustomer_SalesArea, da, lstErr);
        //                foreach (ErrorHandler objerr in lstErr)
        //                {
        //                    if (objerr.Type == "E")
        //                    {
        //                        da.ROLLBACK_TRANSACTION();
        //                        return lstErr;
        //                    }
        //                }
        //                da.COMMIT_TRANSACTION();
        //            }
        //            else
        //            {
        //                da.Open_Connection();
        //                da.BEGIN_TRANSACTION();
        //                UpdateCustomer_SalesArea(argCustomer_SalesArea, da, lstErr);
        //                foreach (ErrorHandler objerr in lstErr)
        //                {
        //                    if (objerr.Type == "E")
        //                    {
        //                        da.ROLLBACK_TRANSACTION();
        //                        return lstErr;
        //                    }
        //                }
        //                da.COMMIT_TRANSACTION();
        //            }
        //        }
        //        else
        //        {
        //            objErrorHandler.Type = "E";
        //            objErrorHandler.MsgId = 0;
        //            objErrorHandler.Module = ErrorConstant.strInsertModule;
        //            objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
        //            objErrorHandler.Message = "Sales Area does not exists.";
        //            objErrorHandler.RowNo = 0;
        //            objErrorHandler.FieldName = "";
        //            objErrorHandler.LogCode = "";
        //            lstErr.Add(objErrorHandler);
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

        public ICollection<ErrorHandler> SaveCustomer_SalesArea(ICollection<Customer_SalesArea> colGetCustomer_SalesArea, DataTable dtCustomer_Tax, DataTable dtCustomer_Partner)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();
                string strretValue = "";
                SalesAreaManager ObjSalesAreaManager = new SalesAreaManager();

                foreach (Customer_SalesArea argCustomer_SalesArea in colGetCustomer_SalesArea)
                {
                    if (ObjSalesAreaManager.blnIsSalesAreaExists(argCustomer_SalesArea.SalesOrganizationCode, argCustomer_SalesArea.DistChannelCode, argCustomer_SalesArea.DivisionCode, argCustomer_SalesArea.ClientCode) == true)
                    {

                        if (blnIsCustomer_SalesAreaExists(argCustomer_SalesArea.CustomerCode, argCustomer_SalesArea.SalesOrganizationCode, argCustomer_SalesArea.DivisionCode, argCustomer_SalesArea.DistChannelCode, argCustomer_SalesArea.ClientCode, da) == false)
                        {
                            strretValue = InsertCustomer_SalesArea(argCustomer_SalesArea, da, lstErr);
                        }
                        else
                        {
                            strretValue = UpdateCustomer_SalesArea(argCustomer_SalesArea, da, lstErr);
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
                    else
                    {
                        objErrorHandler.Type = "E";
                        objErrorHandler.MsgId = 0;
                        objErrorHandler.Module = ErrorConstant.strInsertModule;
                        objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                        objErrorHandler.Message = "Sales Area does not exists.";
                        objErrorHandler.RowNo = 0;
                        objErrorHandler.FieldName = "";
                        objErrorHandler.LogCode = "";
                        lstErr.Add(objErrorHandler);
                    }
                }

                if (strretValue != "")
                {
                    if (dtCustomer_Tax.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCustomer_Tax.Rows)
                        {
                            if (Convert.ToInt32(dr["IsDeleted"]) == 0)
                            {
                                Customer_Tax objCustomer_Tax = new Customer_Tax();
                                objCustomer_Tax.CustomerCode = strretValue.Trim();
                                objCustomer_Tax.CountryCode = Convert.ToString(dr["CountryCode"]).Trim();
                                objCustomer_Tax.TaxCategoryCode = Convert.ToString(dr["TaxCategoryCode"]).Trim();
                                objCustomer_Tax.TaxClassCode = Convert.ToString(dr["TaxClassCode"]).Trim();
                                objCustomer_Tax.ClientCode = Convert.ToString(dr["ClientCode"]).Trim();
                                objCustomer_Tax.CreatedBy = Convert.ToString(dr["CreatedBy"]).Trim();
                                objCustomer_Tax.ModifiedBy = Convert.ToString(dr["ModifiedBy"]).Trim();
                                objCustomer_TaxManager.SaveCustomer_Tax(objCustomer_Tax, da, lstErr);
                            }
                            else
                            {
                                objCustomer_TaxManager.DeleteCustomer_Tax(strretValue.ToString().Trim(), Convert.ToString(dr["CountryCode"]).Trim(), Convert.ToString(dr["TaxCategoryCode"]).Trim(), Convert.ToString(dr["TaxClassCode"]).Trim(), Convert.ToString(dr["ClientCode"]).Trim(), 1, da);
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

                    if (dtCustomer_Partner.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtCustomer_Partner.Rows)
                        {
                            if (Convert.ToInt32(dr["IsDeleted"]) == 0)
                            {
                                Customer_PartnerFunction objCustomer_Partner = new Customer_PartnerFunction();
                                objCustomer_Partner.CustomerCode = strretValue.Trim();
                                objCustomer_Partner.SalesOrganizationCode = Convert.ToString(dr["SalesOrganizationCode"]).Trim();
                                objCustomer_Partner.DivisionCode = Convert.ToString(dr["DivisionCode"]).Trim();
                                objCustomer_Partner.DistChannelCode = Convert.ToString(dr["DistChannelCode"]).Trim();
                                objCustomer_Partner.PFunctionCode = Convert.ToString(dr["PFunctionCode"]).Trim();
                                objCustomer_Partner.PartnerCounter = Convert.ToInt32(dr["PartnerCounter"]);
                                objCustomer_Partner.PartnerTable = Convert.ToString(dr["PartnerTable"]).Trim();
                                objCustomer_Partner.PartnerCode = Convert.ToString(dr["PartnerCode"]).Trim();
                                objCustomer_Partner.ClientCode = Convert.ToString(dr["ClientCode"]).Trim();
                                objCustomer_Partner.CreatedBy = Convert.ToString(dr["CreatedBy"]).Trim();
                                objCustomer_Partner.ModifiedBy = Convert.ToString(dr["ModifiedBy"]).Trim();
                                objCustomer_PartnerFunctionManager.SaveCustomer_PartnerFunction(objCustomer_Partner, da, lstErr);
                            }
                            else
                            {
                                objCustomer_PartnerFunctionManager.DeleteCustomer_PartnerFunction(strretValue.ToString().Trim(), Convert.ToString(dr["PFunctionCode"]).Trim(), Convert.ToString(dr["ClientCode"]).Trim(), 1, da);
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

        public ICollection<ErrorHandler> SaveCustomer_SalesArea(ICollection<Customer_SalesArea> colGetCustomer_SalesArea, ICollection<Customer_Tax> colGetCustomer_Tax, ICollection<Customer_PartnerFunction> colGetCustomer_PartnerFunction, List<ErrorHandler> lstErr)
        {
            DataAccess da = new DataAccess();
            try
            {
                string strretValue = "";
                da.Open_Connection();
                da.BEGIN_TRANSACTION();
                Customer_TaxManager ObjCustomer_TaxManager = new Customer_TaxManager();
                Customer_PartnerFunctionManager ObjCustomer_PartnerFunctionManager = new Customer_PartnerFunctionManager();

                foreach (Customer_SalesArea argCustomer_SalesArea in colGetCustomer_SalesArea)
                {
                    if (argCustomer_SalesArea.IsDeleted == 0)
                    {
                        if (blnIsCustomer_SalesAreaExists(argCustomer_SalesArea.CustomerCode, argCustomer_SalesArea.SalesOrganizationCode, argCustomer_SalesArea.DivisionCode, argCustomer_SalesArea.DistChannelCode, argCustomer_SalesArea.ClientCode, da) == false)
                        {
                           strretValue = InsertCustomer_SalesArea(argCustomer_SalesArea, da, lstErr);
                        }
                        else
                        {
                           strretValue = UpdateCustomer_SalesArea(argCustomer_SalesArea, da, lstErr);
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
                            if (colGetCustomer_Tax.Count > 0)
                            {
                                ObjCustomer_TaxManager.SaveCustomer_Tax(colGetCustomer_Tax, lstErr);
                            }

                            if (colGetCustomer_PartnerFunction.Count > 0)
                            {
                                ObjCustomer_PartnerFunctionManager.SaveCustomer_PartnerFunction(colGetCustomer_PartnerFunction, lstErr);
                            }
                        }
                    }
                    else
                    {
                        DeleteCustomer_SalesArea(argCustomer_SalesArea.CustomerCode, argCustomer_SalesArea.SalesOrganizationCode, argCustomer_SalesArea.DivisionCode, argCustomer_SalesArea.DistChannelCode, argCustomer_SalesArea.ClientCode, argCustomer_SalesArea.IsDeleted);
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

        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery4Customer_Tax, string argQuery4Customer_SalesArea, string argQuery4Customer_PartnerFunction, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel4Customer_Tax = null;
            DataTable dtExcel4Customer_SalesArea = null;
            DataTable dtExcel4Customer_PartnerFunction = null;
            string xConnStr = "";
            string strSheetName = "";
            DataSet dsExcel4Customer_Tax = new DataSet();
            DataSet dsExcel4Customer_SalesArea = new DataSet();
            DataSet dsExcel4Customer_PartnerFunction = new DataSet();
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
                argQuery4Customer_Tax = argQuery4Customer_Tax + " [" + strSheetName + "]";
                OleDbCommand objCommand4Customer_Tax = new OleDbCommand(argQuery4Customer_Tax, objXConn);
                objDataAdapter.SelectCommand = objCommand4Customer_Tax;
                objDataAdapter.Fill(dsExcel4Customer_Tax);
                dtExcel4Customer_Tax = dsExcel4Customer_Tax.Tables[0];

                argQuery4Customer_SalesArea = argQuery4Customer_SalesArea + " [" + strSheetName + "]";
                OleDbCommand objCommand4Customer_SalesArea = new OleDbCommand(argQuery4Customer_SalesArea, objXConn);
                objDataAdapter.SelectCommand = objCommand4Customer_SalesArea;
                objDataAdapter.Fill(dsExcel4Customer_SalesArea);
                dtExcel4Customer_SalesArea = dsExcel4Customer_SalesArea.Tables[0];

                argQuery4Customer_PartnerFunction = argQuery4Customer_PartnerFunction + " [" + strSheetName + "]";
                OleDbCommand objCommand4Customer_PartnerFunction = new OleDbCommand(argQuery4Customer_PartnerFunction, objXConn);
                objDataAdapter.SelectCommand = objCommand4Customer_PartnerFunction;
                objDataAdapter.Fill(dsExcel4Customer_PartnerFunction);
                dtExcel4Customer_PartnerFunction = dsExcel4Customer_PartnerFunction.Tables[0];
                
                /*****************************************/
                DataAccess da = new DataAccess();
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                try
                {
                    SaveCustomer_SalesArea(colGetCustomer_SalesArea(dtExcel4Customer_SalesArea, argUserName, argClientCode), colGetCustomer_Tax(dtExcel4Customer_Tax, argUserName, argClientCode), colGetCustomer_PartnerFunction(dtExcel4Customer_PartnerFunction, argUserName, argClientCode), lstErr);

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

        public string InsertCustomer_SalesArea(Customer_SalesArea argCustomer_SalesArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[22];
            param[0] = new SqlParameter("@CustomerCode", argCustomer_SalesArea.CustomerCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argCustomer_SalesArea.SalesOrganizationCode);
            param[2] = new SqlParameter("@DivisionCode", argCustomer_SalesArea.DivisionCode);
            param[3] = new SqlParameter("@DistChannelCode", argCustomer_SalesArea.DistChannelCode);
            param[4] = new SqlParameter("@SalesDistrictCode", argCustomer_SalesArea.SalesDistrictCode);
            param[5] = new SqlParameter("@SalesRegionCode", argCustomer_SalesArea.SalesRegionCode);
            param[6] = new SqlParameter("@SalesOfficeCode", argCustomer_SalesArea.SalesOfficeCode);
            param[7] = new SqlParameter("@SalesGroupCode", argCustomer_SalesArea.SalesGroupCode);
            param[8] = new SqlParameter("@CustGroupCode", argCustomer_SalesArea.CustGroupCode);
            param[9] = new SqlParameter("@CurrencyCode", argCustomer_SalesArea.CurrencyCode);
            param[10] = new SqlParameter("@DlvPriorityCode", argCustomer_SalesArea.DlvPriorityCode);
            param[11] = new SqlParameter("@DefaultPlantCode", argCustomer_SalesArea.DefaultPlantCode);
            param[12] = new SqlParameter("@IncoTermsCode", argCustomer_SalesArea.IncoTermsCode);
            param[13] = new SqlParameter("@PaymentTermsCode", argCustomer_SalesArea.PaymentTermsCode);
            param[14] = new SqlParameter("@CrContAreaCode", argCustomer_SalesArea.CrContAreaCode);
            param[15] = new SqlParameter("@AccAssignGroupCode", argCustomer_SalesArea.AccAssignGroupCode);
            param[16] = new SqlParameter("@ClientCode", argCustomer_SalesArea.ClientCode);
            param[17] = new SqlParameter("@CreatedBy", argCustomer_SalesArea.CreatedBy);
            param[18] = new SqlParameter("@ModifiedBy", argCustomer_SalesArea.ModifiedBy);

            param[19] = new SqlParameter("@Type", SqlDbType.Char);
            param[19].Size = 1;
            param[19].Direction = ParameterDirection.Output;

            param[20] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[20].Size = 255;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[21].Size = 20;
            param[21].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCustomer_SalesArea", param);

            string strMessage = Convert.ToString(param[20].Value);
            string strType = Convert.ToString(param[19].Value);
            string strRetValue = Convert.ToString(param[21].Value);

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

        public string UpdateCustomer_SalesArea(Customer_SalesArea argCustomer_SalesArea, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[22];
            param[0] = new SqlParameter("@CustomerCode", argCustomer_SalesArea.CustomerCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argCustomer_SalesArea.SalesOrganizationCode);
            param[2] = new SqlParameter("@DivisionCode", argCustomer_SalesArea.DivisionCode);
            param[3] = new SqlParameter("@DistChannelCode", argCustomer_SalesArea.DistChannelCode);
            param[4] = new SqlParameter("@SalesDistrictCode", argCustomer_SalesArea.SalesDistrictCode);
            param[5] = new SqlParameter("@SalesRegionCode", argCustomer_SalesArea.SalesRegionCode);
            param[6] = new SqlParameter("@SalesOfficeCode", argCustomer_SalesArea.SalesOfficeCode);
            param[7] = new SqlParameter("@SalesGroupCode", argCustomer_SalesArea.SalesGroupCode);
            param[8] = new SqlParameter("@CustGroupCode", argCustomer_SalesArea.CustGroupCode);
            param[9] = new SqlParameter("@CurrencyCode", argCustomer_SalesArea.CurrencyCode);
            param[10] = new SqlParameter("@DlvPriorityCode", argCustomer_SalesArea.DlvPriorityCode);
            param[11] = new SqlParameter("@DefaultPlantCode", argCustomer_SalesArea.DefaultPlantCode);
            param[12] = new SqlParameter("@IncoTermsCode", argCustomer_SalesArea.IncoTermsCode);
            param[13] = new SqlParameter("@PaymentTermsCode", argCustomer_SalesArea.PaymentTermsCode);
            param[14] = new SqlParameter("@CrContAreaCode", argCustomer_SalesArea.CrContAreaCode);
            param[15] = new SqlParameter("@AccAssignGroupCode", argCustomer_SalesArea.AccAssignGroupCode);
            param[16] = new SqlParameter("@ClientCode", argCustomer_SalesArea.ClientCode);
            param[17] = new SqlParameter("@CreatedBy", argCustomer_SalesArea.CreatedBy);
            param[18] = new SqlParameter("@ModifiedBy", argCustomer_SalesArea.ModifiedBy);

            param[19] = new SqlParameter("@Type", SqlDbType.Char);
            param[19].Size = 1;
            param[19].Direction = ParameterDirection.Output;

            param[20] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[20].Size = 255;
            param[20].Direction = ParameterDirection.Output;

            param[21] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[21].Size = 20;
            param[21].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateCustomer_SalesArea", param);

            string strMessage = Convert.ToString(param[20].Value);
            string strType = Convert.ToString(param[19].Value);
            string strRetValue = Convert.ToString(param[21].Value);

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

        public ICollection<ErrorHandler> DeleteCustomer_SalesArea(string argCustomerCode, string argSalesOrganizationCode, string argDivisionCode, string argDistChannelCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[9];
                param[0] = new SqlParameter("@CustomerCode", argCustomerCode);
                param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
                param[2] = new SqlParameter("@DivisionCode", argDivisionCode);
                param[3] = new SqlParameter("@DistChannelCode", argDistChannelCode);
                param[4] = new SqlParameter("@ClientCode", argClientCode);
                param[5] = new SqlParameter("@IsDeleted", iIsDeleted);

                param[6] = new SqlParameter("@Type", SqlDbType.Char);
                param[6].Size = 1;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[7].Size = 255;
                param[7].Direction = ParameterDirection.Output;

                param[8] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[8].Size = 20;
                param[8].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteCustomer_SalesArea", param);

                string strMessage = Convert.ToString(param[7].Value);
                string strType = Convert.ToString(param[6].Value);
                string strRetValue = Convert.ToString(param[8].Value);

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

        public bool blnIsCustomer_SalesAreaExists(string argCustomerCode, string argSalesOrganizationCode, string argDivisionCode, string argDistChannelCode, string argClientCode)
        {
            bool IsCustomer_SalesAreaExists = false;
            DataSet ds = new DataSet();
            ds = GetCustomer_SalesArea(argCustomerCode, argSalesOrganizationCode, argDivisionCode, argDistChannelCode, argClientCode);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCustomer_SalesAreaExists = true;
            }
            else
            {
                IsCustomer_SalesAreaExists = false;
            }
            return IsCustomer_SalesAreaExists;
        }

        public bool blnIsCustomer_SalesAreaExists(string argCustomerCode, string argSalesOrganizationCode, string argDivisionCode, string argDistChannelCode, string argClientCode, DataAccess da)
        {
            bool IsCustomer_SalesAreaExists = false;
            DataSet ds = new DataSet();
            ds = GetCustomer_SalesArea(argCustomerCode, argSalesOrganizationCode, argDivisionCode, argDistChannelCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCustomer_SalesAreaExists = true;
            }
            else
            {
                IsCustomer_SalesAreaExists = false;
            }
            return IsCustomer_SalesAreaExists;
        }
    }
}