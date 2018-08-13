
//Created On :: 17, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using RSMApp_Organization;
using System.Data.OleDb;


namespace RSMApp_MM
{
    public class MaterialMasterManager
    {
        const string MaterialMasterTable = "MaterialMaster";

        //GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public MaterialMaster objGetMaterialMaster(string argMaterialCode, string argClientCode)
        {
            MaterialMaster argMaterialMaster = new MaterialMaster();
            DataSet DataSetToFill = new DataSet();

            if (argMaterialCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetMaterialMaster(argMaterialCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }
            argMaterialMaster = this.objCreateMaterialMaster((DataRow)DataSetToFill.Tables[0].Rows[0]);
            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;

            return argMaterialMaster;
        }
        
        public ICollection<MaterialMaster> colGetMaterialMaster(string argClientCode)
        {
            List<MaterialMaster> lst = new List<MaterialMaster>();
            DataSet DataSetToFill = new DataSet();
            MaterialMaster tMaterialMaster = new MaterialMaster();
            DataSetToFill = this.GetMaterialMaster(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateMaterialMaster(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }

        public DataSet GetMaterialMaster(string argPrefix, string argMaterialTypeCode, string argMatGroup1Code, string argMatGroup2Code, string argSalesOrganizationCode, string argDistChannelCode, string argDivisionCode,  string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[8];

            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@MaterialTypeCode", argMaterialTypeCode.Trim());
            param[2] = new SqlParameter("@MatGroup1Code", argMatGroup1Code.Trim());
            param[3] = new SqlParameter("@MatGroup2Code", argMatGroup2Code.Trim());
            param[4] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode.Trim());
            param[5] = new SqlParameter("@DistChannelCode", argDistChannelCode.Trim());
            param[6] = new SqlParameter("@DivisionCode", argDivisionCode.Trim());
            param[7] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial4Combo", param);
            return DataSetToFill;
        }

        public DataSet GetMaterialMaster(string argPrefix, string argMaterialTypeCode, string argMatGroup1Code, string argMatGroup2Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@MaterialTypeCode", argMaterialTypeCode.Trim());
            param[2] = new SqlParameter("@MatGroup1Code", argMatGroup1Code.Trim());
            param[3] = new SqlParameter("@MatGroup2Code", argMatGroup2Code.Trim());          
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialAgainstType4Combo", param);
            return DataSetToFill;
        }
                     
        public DataSet GetMaterialMaster4BOM(string argPrefix, string argMaterialTypeCode, string argMatGroup1Code, string argMatGroup2Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@MaterialTypeCode", argMaterialTypeCode.Trim());
            param[2] = new SqlParameter("@MatGroup1Code", argMatGroup1Code.Trim());
            param[3] = new SqlParameter("@MatGroup2Code", argMatGroup2Code.Trim());          
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial4BOM", param);
            return DataSetToFill;
        }

        public DataSet GetMaterialMaster(string argPrefix, string argMaterialTypeCode, string argMatGroup1Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@MaterialTypeCode", argMaterialTypeCode.Trim());
            param[2] = new SqlParameter("@MatGroup1Code", argMatGroup1Code.Trim());
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial4Type", param);
            return DataSetToFill;
        }
                
        public DataSet GetMaterialMaster(int iIsDeleted, string argClientCode)
        {
            DataSet ds = new DataSet();
            DataAccess da = new DataAccess();
            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {
                tSQL = "SELECT * FROM " + MaterialMasterTable.ToString();

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
        
        public DataSet GetMaterialMaster(string argMaterialCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialMaster4ID", param);
            return DataSetToFill;
        }

        public DataSet GetMaterialMaster4Sales(string argMaterialCode, string argSalesOrganizationCode, string argDistChannelCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];

            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@SalesOrganizationCode", argSalesOrganizationCode);
            param[2] = new SqlParameter("@DistChannelCode", argDistChannelCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterialMaster4Sales", param);
            return DataSetToFill;
        }

        public DataSet GetMaterialMaster(string argMaterialCode, string argClientCode,DataAccess da)
        {
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetMaterialMaster4ID", param);
            return DataSetToFill;
        }
        
        public DataSet GetMaterialMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetMaterialMaster", param);
            return DataSetToFill;
        }

        public DataSet GetMaterialMaster4Search(string argPrefix,string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
            DataSetToFill = da.FillDataSet("SP_GetMaterial4Search", param);
            return DataSetToFill;
        }

        public DataSet GetMaterialMaster4MatGroup(string argMatGroup1Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MatGroup1Code", argMatGroup1Code);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial4MatGroup", param);

            return DataSetToFill;
        }

        //-- Added by Ashutosh  date 16/05/2013

        public DataSet GetMaterialMaster4Consumption(string argPrefix, string argMaterialTypeCode, string argMatGroup1Code, string argMatGroup2Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@MaterialTypeCode", argMaterialTypeCode.Trim());
            param[2] = new SqlParameter("@MatGroup1Code", argMatGroup1Code.Trim());
            param[3] = new SqlParameter("@MatGroup2Code", argMatGroup2Code.Trim());
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial4Consumption", param);
            return DataSetToFill;
        }
        public DataSet GetMaterialMaster4Consumable(string argPrefix, string argMaterialTypeCode, string argMatGroup1Code, string argMatGroup2Code, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@MaterialTypeCode", argMaterialTypeCode.Trim());
            param[2] = new SqlParameter("@MatGroup1Code", argMatGroup1Code.Trim());
            param[3] = new SqlParameter("@MatGroup2Code", argMatGroup2Code.Trim());
            param[4] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetMaterial4Consumable", param);
            return DataSetToFill;
        }

        private MaterialMaster objCreateMaterialMaster(DataRow dr)
        {
            MaterialMaster tMaterialMaster = new MaterialMaster();
            tMaterialMaster.SetObjectInfo(dr);
            return tMaterialMaster;
        }

        //public ICollection<ErrorHandler> SaveMaterialMaster(MaterialMaster argMaterialMaster)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsMaterialMasterExists(argMaterialMaster.MaterialCode, argMaterialMaster.ClientCode) == false)
        //        {
        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertMaterialMaster(argMaterialMaster, da, lstErr);
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
        //            UpdateMaterialMaster(argMaterialMaster, da, lstErr);
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
        //        objErrorHandler.ReturnValue = "";
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

        public PartnerErrorResult_MM SaveMaterialMaster(MaterialMaster argMaterialMaster)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            PartnerErrorResult_MM errorcol = new PartnerErrorResult_MM();
            DataAccess da = new DataAccess();
            try
            {
                
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    SaveMaterialMaster(argMaterialMaster, ref da, ref lstErr);
                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            errorcol.colErrorHandler.Add(objerr);
                            da.ROLLBACK_TRANSACTION();
                            return errorcol;
                        }

                        if (objerr.Type == "A")
                        {
                            errorcol.colErrorHandler.Add(objerr);
                            da.ROLLBACK_TRANSACTION();
                            return errorcol;
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
                errorcol.colErrorHandler.Add(objErrorHandler);
            }
            finally
            {
                if (da != null)
                {
                    da.Close_Connection();
                    da = null;
                }
            }
            return errorcol;
        }

        /*************/
        public void SaveMaterialMaster(MaterialMaster argMaterialMaster, ref DataAccess da, ref List<ErrorHandler> lstErr)
        {
            try
            {
                if (blnIsMaterialMasterExists(argMaterialMaster.MaterialCode, argMaterialMaster.ClientCode, da) == false)
                {
                    InsertMaterialMaster(argMaterialMaster, da, lstErr);
                }
                else
                {
                    UpdateMaterialMaster(argMaterialMaster, da, lstErr);
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
            MaterialMaster ObjMaterialMaster = null;
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
                        ObjMaterialMaster = new MaterialMaster();
                        ObjMaterialMaster.MaterialCode = Convert.ToString(drExcel["MaterialCode"]).Trim();
                        ObjMaterialMaster.MatDesc = Convert.ToString(drExcel["MatDesc"]).Trim();
                        ObjMaterialMaster.MaterialTypeCode = Convert.ToString(drExcel["MaterialTypeCode"]).Trim();
                        ObjMaterialMaster.ExtMatGroupCode = Convert.ToString(drExcel["ExtMatGroupCode"]).Trim();
                        ObjMaterialMaster.MatGroup1Code = Convert.ToString(drExcel["MatGroup1Code"]).Trim();
                        ObjMaterialMaster.MatGroup2Code = Convert.ToString(drExcel["MatGroup2Code"]).Trim();
                        ObjMaterialMaster.MatGroup3Code = Convert.ToString(drExcel["MatGroup3Code"]).Trim();
                        ObjMaterialMaster.ValClassType = Convert.ToString(drExcel["ValClassType"]).Trim();
                        ObjMaterialMaster.DivisionCode = Convert.ToString(drExcel["DivisionCode"]).Trim();
                        ObjMaterialMaster.PriceControl = Convert.ToString(drExcel["PriceControl"]).Trim();
                        ObjMaterialMaster.UOMCode = Convert.ToString(drExcel["UOMCode"]).Trim();
                        ObjMaterialMaster.PriceUnit = Convert.ToInt32(drExcel["PriceUnit"]);
                        ObjMaterialMaster.IsSerialize = Convert.ToInt32(drExcel["IsSerialize"]);
                        ObjMaterialMaster.IsBOM = Convert.ToInt32(drExcel["IsBOM"]);
                        ObjMaterialMaster.GrossWeight = Convert.ToInt32(drExcel["GrossWeight"]);
                        ObjMaterialMaster.NetWeight = Convert.ToInt32(drExcel["NetWeight"]);
                        ObjMaterialMaster.MatSize = Convert.ToInt32(drExcel["MatSize"]);
                        ObjMaterialMaster.WeightUOM = Convert.ToString(drExcel["WeightUOM"]).Trim();
                        ObjMaterialMaster.MatVolume = Convert.ToInt32(drExcel["MatVolume"]);
                        ObjMaterialMaster.PurchaseUnit = Convert.ToInt32(drExcel["PurchaseUnit"]);
                        ObjMaterialMaster.IsBatchWise = Convert.ToInt32(drExcel["IsBatchWise"]);
                        ObjMaterialMaster.VolumeUOM = Convert.ToString(drExcel["VolumeUOM"]).Trim();
                        ObjMaterialMaster.MaterialHierarchy = Convert.ToString(drExcel["MaterialHierarchy"]).Trim();
                        ObjMaterialMaster.ItemCatGroupCode = Convert.ToString(drExcel["ItemCatGroupCode"]).Trim();
                        ObjMaterialMaster.OldMaterialCode = Convert.ToString(drExcel["OldMaterialCode"]).Trim();
                        ObjMaterialMaster.SNProfileCode = Convert.ToString(drExcel["SNProfileCode"]).Trim();
                        ObjMaterialMaster.IsBOMExplodeApp = Convert.ToInt32(drExcel["IsBOMExplodeApp"]);
                        ObjMaterialMaster.BaseWarrantyOn = Convert.ToString(drExcel["BaseWarrantyOn"]).Trim();
                        ObjMaterialMaster.CreatedBy = Convert.ToString(argUserName);
                        ObjMaterialMaster.ModifiedBy = Convert.ToString(argUserName);
                        ObjMaterialMaster.ClientCode = Convert.ToString(argClientCode);
                        
                        SaveMaterialMaster(ObjMaterialMaster, ref da, ref lstErr);

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
        /**********/
                
        public void InsertMaterialMaster(MaterialMaster argMaterialMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[34];

            param[0] = new SqlParameter("@MaterialCode", argMaterialMaster.MaterialCode);
            param[1] = new SqlParameter("@MatDesc", argMaterialMaster.MatDesc);
            param[2] = new SqlParameter("@MaterialTypeCode", argMaterialMaster.MaterialTypeCode);
            param[3] = new SqlParameter("@ExtMatGroupCode", argMaterialMaster.ExtMatGroupCode);
            param[4] = new SqlParameter("@MatGroup1Code", argMaterialMaster.MatGroup1Code);
            param[5] = new SqlParameter("@MatGroup2Code", argMaterialMaster.MatGroup2Code);
            param[6] = new SqlParameter("@MatGroup3Code", argMaterialMaster.MatGroup3Code);
            param[7] = new SqlParameter("@DivisionCode", argMaterialMaster.DivisionCode);
            param[8] = new SqlParameter("@ValClassType", argMaterialMaster.ValClassType);
            param[9] = new SqlParameter("@ItemCatGroupCode", argMaterialMaster.ItemCatGroupCode);
            param[10] = new SqlParameter("@PriceControl", argMaterialMaster.PriceControl);
            param[11] = new SqlParameter("@UOMCode", argMaterialMaster.UOMCode);
            param[12] = new SqlParameter("@PriceUnit", argMaterialMaster.PriceUnit);
            param[13] = new SqlParameter("@PurchaseUnit", argMaterialMaster.PurchaseUnit);
            param[14] = new SqlParameter("@IsSerialize", argMaterialMaster.IsSerialize);
            param[15] = new SqlParameter("@IsBOM", argMaterialMaster.IsBOM);
            param[16] = new SqlParameter("@GrossWeight", argMaterialMaster.GrossWeight);
            param[17] = new SqlParameter("@NetWeight", argMaterialMaster.NetWeight);
            param[18] = new SqlParameter("@MatSize", argMaterialMaster.MatSize);
            param[19] = new SqlParameter("@WeightUOM", argMaterialMaster.WeightUOM);
            param[20] = new SqlParameter("@MatVolume", argMaterialMaster.MatVolume);
            param[21] = new SqlParameter("@IsBatchWise", argMaterialMaster.IsBatchWise);
            param[22] = new SqlParameter("@VolumeUOM", argMaterialMaster.VolumeUOM);
            param[23] = new SqlParameter("@MaterialHierarchy", argMaterialMaster.MaterialHierarchy);
            param[24] = new SqlParameter("@OldMaterialCode", argMaterialMaster.OldMaterialCode);
            param[25] = new SqlParameter("@SNProfileCode", argMaterialMaster.SNProfileCode);
            param[26] = new SqlParameter("@IsBOMExplodeApp", argMaterialMaster.IsBOMExplodeApp);
            param[27] = new SqlParameter("@BaseWarrantyOn", argMaterialMaster.BaseWarrantyOn);
            param[28] = new SqlParameter("@ClientCode", argMaterialMaster.ClientCode);
            param[29] = new SqlParameter("@CreatedBy", argMaterialMaster.CreatedBy);
            param[30] = new SqlParameter("@ModifiedBy", argMaterialMaster.ModifiedBy);
            
            param[31] = new SqlParameter("@Type", SqlDbType.Char);
            param[31].Size = 1;
            param[31].Direction = ParameterDirection.Output;

            param[32] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[32].Size = 255;
            param[32].Direction = ParameterDirection.Output;

            param[33] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[33].Size = 20;
            param[33].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertMaterialMaster", param);

            string strMessage = Convert.ToString(param[32].Value);
            string strType = Convert.ToString(param[31].Value);
            string strRetValue = Convert.ToString(param[33].Value);

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
        
        public void UpdateMaterialMaster(MaterialMaster argMaterialMaster, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[34];

            param[0] = new SqlParameter("@MaterialCode", argMaterialMaster.MaterialCode);
            param[1] = new SqlParameter("@MatDesc", argMaterialMaster.MatDesc);
            param[2] = new SqlParameter("@MaterialTypeCode", argMaterialMaster.MaterialTypeCode);
            param[3] = new SqlParameter("@ExtMatGroupCode", argMaterialMaster.ExtMatGroupCode);
            param[4] = new SqlParameter("@MatGroup1Code", argMaterialMaster.MatGroup1Code);
            param[5] = new SqlParameter("@MatGroup2Code", argMaterialMaster.MatGroup2Code);
            param[6] = new SqlParameter("@MatGroup3Code", argMaterialMaster.MatGroup3Code);
            param[7] = new SqlParameter("@DivisionCode", argMaterialMaster.DivisionCode);
            param[8] = new SqlParameter("@ValClassType", argMaterialMaster.ValClassType);
            param[9] = new SqlParameter("@ItemCatGroupCode", argMaterialMaster.ItemCatGroupCode);
            param[10] = new SqlParameter("@PriceControl", argMaterialMaster.PriceControl);
            param[11] = new SqlParameter("@UOMCode", argMaterialMaster.UOMCode);
            param[12] = new SqlParameter("@PriceUnit", argMaterialMaster.PriceUnit);
            param[13] = new SqlParameter("@PurchaseUnit", argMaterialMaster.PurchaseUnit);
            param[14] = new SqlParameter("@IsSerialize", argMaterialMaster.IsSerialize);
            param[15] = new SqlParameter("@IsBOM", argMaterialMaster.IsBOM);
            param[16] = new SqlParameter("@GrossWeight", argMaterialMaster.GrossWeight);
            param[17] = new SqlParameter("@NetWeight", argMaterialMaster.NetWeight);
            param[18] = new SqlParameter("@MatSize", argMaterialMaster.MatSize);
            param[19] = new SqlParameter("@WeightUOM", argMaterialMaster.WeightUOM);
            param[20] = new SqlParameter("@MatVolume", argMaterialMaster.MatVolume);
            param[21] = new SqlParameter("@IsBatchWise", argMaterialMaster.IsBatchWise);
            param[22] = new SqlParameter("@VolumeUOM", argMaterialMaster.VolumeUOM);
            param[23] = new SqlParameter("@MaterialHierarchy", argMaterialMaster.MaterialHierarchy);
            param[24] = new SqlParameter("@OldMaterialCode", argMaterialMaster.OldMaterialCode);
            param[25] = new SqlParameter("@SNProfileCode", argMaterialMaster.SNProfileCode);
            param[26] = new SqlParameter("@IsBOMExplodeApp", argMaterialMaster.IsBOMExplodeApp);
            param[27] = new SqlParameter("@BaseWarrantyOn", argMaterialMaster.BaseWarrantyOn);
            param[28] = new SqlParameter("@ClientCode", argMaterialMaster.ClientCode);
            param[29] = new SqlParameter("@CreatedBy", argMaterialMaster.CreatedBy);
            param[30] = new SqlParameter("@ModifiedBy", argMaterialMaster.ModifiedBy);

            param[31] = new SqlParameter("@Type", SqlDbType.Char);
            param[31].Size = 1;
            param[31].Direction = ParameterDirection.Output;

            param[32] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[32].Size = 255;
            param[32].Direction = ParameterDirection.Output;

            param[33] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[33].Size = 20;
            param[33].Direction = ParameterDirection.Output;
            int i = da.NExecuteNonQuery("Proc_UpdateMaterialMaster", param);
            
            string strMessage = Convert.ToString(param[32].Value);
            string strType = Convert.ToString(param[31].Value);
            string strRetValue = Convert.ToString(param[33].Value);

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
        
        public ICollection<ErrorHandler> DeleteMaterialMaster(string argMaterialCode, string argClientCode, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {
                SqlParameter[] param = new SqlParameter[6];
                param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
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

                int i = da.ExecuteNonQuery("Proc_DeleteMaterialMaster", param);

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
        
        public bool blnIsMaterialMasterExists(string argMaterialCode, string argClientCode)
        {
            bool IsMaterialMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterialMaster(argMaterialCode, argClientCode);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterialMasterExists = true;
            }
            else
            {
                IsMaterialMasterExists = false;
            }
            return IsMaterialMasterExists;
        }

        public bool blnIsMaterialMasterExists(string argMaterialCode, string argClientCode,DataAccess da)
        {
            bool IsMaterialMasterExists = false;
            DataSet ds = new DataSet();
            ds = GetMaterialMaster(argMaterialCode, argClientCode,da);
            
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsMaterialMasterExists = true;
            }
            else
            {
                IsMaterialMasterExists = false;
            }
            return IsMaterialMasterExists;
        }
    }
}