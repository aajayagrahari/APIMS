
//Created On :: 13, June, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data.OleDb;

namespace RSMApp_MM
{
    public class BillsofMaterialManager
    {
        const string BillsofMaterialTable = "BillsofMaterial";
        BOM_SerialManager objBOM_SerialManager = new BOM_SerialManager();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public BillsofMaterial objGetBillsofMaterial(string argClientCode)
        {
            BillsofMaterial argBillsofMaterial = new BillsofMaterial();
            DataSet DataSetToFill = new DataSet();
            
            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetBillsofMaterialMaster(argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argBillsofMaterial = this.objCreateBillsofMaterial((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argBillsofMaterial;
        }

        public BillsofMaterial objGetBillsofMaterial(string argMastMaterialCode, string argMRevisionCode, string argClientCode)
        {
            BillsofMaterial argBillsofMaterial = new BillsofMaterial();
            DataSet DataSetToFill = new DataSet();

            if (argMastMaterialCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetBillsofMaterial(argMastMaterialCode, argMRevisionCode, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argBillsofMaterial = this.objCreateBillsofMaterial((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argBillsofMaterial;
        }
        
        public ICollection<BillsofMaterial> colGetBillsofMaterial(string argClientCode)
        {
            List<BillsofMaterial> lst = new List<BillsofMaterial>();
            DataSet DataSetToFill = new DataSet();
            BillsofMaterial tBillsofMaterial = new BillsofMaterial();

            DataSetToFill = this.GetBillsofMaterial(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateBillsofMaterial(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<BillsofMaterial> colGetBillsofMaterial(string argMaterialCode, string argMRevesionCode, string argClientCode, List<BillsofMaterial> lst)
        {
            //List<BillsofMaterial> lst = new List<BillsofMaterial>();
            DataSet DataSetToFill = new DataSet();
            BillsofMaterial tBillsofMaterial = new BillsofMaterial();

            DataSetToFill = this.GetBillsofMaterial(argMaterialCode, argMRevesionCode, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateBillsofMaterial(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

       
        public DataSet GetBillsofMaterialMaster(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBillsofMaterialMaster", param);

            return DataSetToFill;
        }
        
        public DataSet GetBillsofMaterial(string argMastMaterialCode, string argMRevisionCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MastMaterialCode", argMastMaterialCode);
            param[1] = new SqlParameter("@MRevisionCode", argMRevisionCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBillsofMaterial4ID", param);

            return DataSetToFill;
        }

        public DataSet GetBOM4Parent(string argMastMaterialCode,string argMaterialCode , string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MastMaterialCode", argMastMaterialCode);
            param[1] = new SqlParameter("@MaterialCode",argMaterialCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBOM4Parent", param);

            return DataSetToFill;
        }


        public DataSet GetBOM4Product(string argPrefix,string argMatGroup1CodeCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@Prefix", argPrefix);
            param[1] = new SqlParameter("@MatGroup1Code", argMatGroup1CodeCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBOM4Product", param);

            return DataSetToFill;
        }
        
        
        public DataSet GetBillsofMaterialExists(string argMastMaterialCode, string argMaterialCode,  string argMRevisionCode,string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MastMaterialCode", argMastMaterialCode);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@MRevisionCode",argMRevisionCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetBillsofMaterialExists", param);

            return DataSetToFill;
        }

        public DataSet GetBillsofMaterialExists(string argMastMaterialCode, string argMaterialCode, string argMRevisionCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MastMaterialCode", argMastMaterialCode);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@MRevisionCode", argMRevisionCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBillsofMaterialExists", param);

            return DataSetToFill;
        }

        public DataSet GetBillsofMaterialExists(string argMastMaterialCode, string argMaterialCode, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();
            SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@MastMaterialCode", argMastMaterialCode);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetBillsofMaterialExists", param);

            return DataSetToFill;
        }
                
        public DataSet GetBillsofMaterial(string argclientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argclientCode);
            DataSetToFill = da.FillDataSet("SP_GetBillsofMaterial",param);
            return DataSetToFill;
        }

        public DataSet GetRevisionCode(string argMaterialCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetRevisionCode", param);

            return DataSetToFill;
        }

        public DataSet GetBOM4ItemRec(string argMastMaterialCode, string argMaterialCode, string argMRevisionCode, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[4];
            param[0] = new SqlParameter("@MastMaterialCode", argMastMaterialCode);
            param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[2] = new SqlParameter("@MRevisionCode", argMRevisionCode);
            param[3] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetBillsofMaterial4ItemRec", param);

            return DataSetToFill;
        }

        //---- added by Ashutosh  (16-05-2013)
        public DataSet GetMastMaterial4Material(string argMaterialCode,string argclientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@MaterialCode", argMaterialCode);
            param[1] = new SqlParameter("@ClientCode", argclientCode);

            DataSetToFill = da.FillDataSet("SP_GetMastMaterial4Material", param);
            return DataSetToFill;
        }
        //----END
                
        private BillsofMaterial objCreateBillsofMaterial(DataRow dr)
        {
            BillsofMaterial tBillsofMaterial = new BillsofMaterial();
            tBillsofMaterial.SetObjectInfo(dr);
            return tBillsofMaterial;
        }
        
        //public ICollection<ErrorHandler> SaveBillsofMaterial(ICollection<BillsofMaterial> colBillsofMaterial, DataTable dtSerialNo)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        da.Open_Connection();
        //        da.BEGIN_TRANSACTION();
        //        string strReturnVal = "";
 
        //        foreach(BillsofMaterial argBillsofMaterial in colBillsofMaterial)
        //        {
        //            if (blnIsBillsofMaterialExists(argBillsofMaterial.MastMaterialCode, argBillsofMaterial.MaterialCode, argBillsofMaterial.MRevisionCode, argBillsofMaterial.ClientCode, da) == false)
        //            {
        //                strReturnVal=InsertBillsofMaterial(argBillsofMaterial, da, lstErr);
        //            }
        //            else
        //            {
        //                strReturnVal=UpdateBillsofMaterial(argBillsofMaterial, da, lstErr);
        //            }
        //        }

        //        foreach (ErrorHandler objerr in lstErr)
        //        {
        //            if (objerr.Type == "E")
        //            {
        //                da.ROLLBACK_TRANSACTION();
        //                return lstErr;
        //            }

        //            if (objerr.Type == "A")
        //            {
        //                da.ROLLBACK_TRANSACTION();
        //                return lstErr;
        //            }
        //        }
        //        if (strReturnVal != "")
        //        {
        //            foreach (DataRow dr in dtSerialNo.Rows)
        //            {
        //                BOM_Serial objBOM_Serial = new BOM_Serial();

        //                objBOM_Serial.MaterialCode = Convert.ToString(dr["MaterialCode"]).Trim();
        //                objBOM_Serial.MRevisionCode = Convert.ToString(dr["MRevisionCode"]).Trim();
        //                objBOM_Serial.SerialFrom = Convert.ToString(dr["SerialFrom"]).Trim();
        //                objBOM_Serial.SerialTo = Convert.ToString(dr["SerialTo"]).Trim();
        //                objBOM_Serial.ClientCode = Convert.ToString(dr["ClientCode"]).Trim();
        //                objBOM_Serial.CreatedBy = Convert.ToString(dr["CreatedBy"]).Trim();
        //                objBOM_Serial.ModifiedBy = Convert.ToString(dr["ModifiedBy"]).Trim();

        //                objBOM_SerialManager.SaveBOM_Serial(objBOM_Serial, da, lstErr);
                        

        //            }

        //            foreach (ErrorHandler objerr in lstErr)
        //            {
        //                if (objerr.Type == "E")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }

        //                if (objerr.Type == "A")
        //                {
        //                    da.ROLLBACK_TRANSACTION();
        //                    return lstErr;
        //                }
        //            }

        //        }

        //        da.COMMIT_TRANSACTION();
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

        ///*************/
        //public void SaveBillsofMaterial(BillsofMaterial argBillsofMaterial, DataAccess da, List<ErrorHandler> lstErr)
        //{
        //    try
        //    {
        //        if (blnIsBillsofMaterialExists(argBillsofMaterial.MastMaterialCode,argBillsofMaterial.MaterialCode,argBillsofMaterial.MRevisionCode ,argBillsofMaterial.ClientCode, da) == false)
        //        {
        //            InsertBillsofMaterial(argBillsofMaterial, da, lstErr);
        //        }
        //        else
        //        {
        //            UpdateBillsofMaterial(argBillsofMaterial, da, lstErr);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public ICollection<ErrorHandler> BulkInsert(string argExcelPath, string argQuery, string strTableName, string argFileExt, string argUserName, string argClientCode)
        {
            DataTable dtExcel = null;
            BillsofMaterial ObjBillsofMaterial = null;
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
                        ObjBillsofMaterial = new BillsofMaterial();
                        ObjBillsofMaterial.MastMaterialCode = Convert.ToString(drExcel["MastMaterialCode"]);
                        ObjBillsofMaterial.MaterialCode = Convert.ToString(drExcel["MaterialCode"]);
                        ObjBillsofMaterial.MRevisionCode = Convert.ToString(drExcel["MRevisionCode"]);
                        ObjBillsofMaterial.DRevisionCode = Convert.ToString(drExcel["DRevisionCode"]);
                        ObjBillsofMaterial.ValidFrom = Convert.ToString(drExcel["ValidFrom"]);
                        ObjBillsofMaterial.ValidTo = Convert.ToString(drExcel["ValidTo"]);
                        ObjBillsofMaterial.IsSerialBatch = Convert.ToInt32(drExcel["IsSerialBatch"]);
                        ObjBillsofMaterial.Quantity = Convert.ToInt32(drExcel["Quantity"]);
                        ObjBillsofMaterial.IsServiceRec = Convert.ToInt32(drExcel["IsServiceRec"]);
                        ObjBillsofMaterial.CreatedBy = Convert.ToString(argUserName);
                        ObjBillsofMaterial.ModifiedBy = Convert.ToString(argUserName);
                        ObjBillsofMaterial.ClientCode = Convert.ToString(argClientCode);

                        //SaveBillsofMaterial(ObjBillsofMaterial, ref da, ref lstErr);

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

        public PartnerErrorResult_MM SaveBillsofMaterial(BillsofMaterialCol ColBillsofMaterial, DataTable dtSerialNo)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            PartnerErrorResult_MM errorcol = new PartnerErrorResult_MM();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();
                string strReturnVal = "";

                foreach (BillsofMaterial argBillsofMaterial in ColBillsofMaterial.colBillsofMaterial)
                {
                    if (argBillsofMaterial.IsDeleted == 0)
                    {
                        if (blnIsBillsofMaterialExists(argBillsofMaterial.MastMaterialCode, argBillsofMaterial.MaterialCode, argBillsofMaterial.MRevisionCode, argBillsofMaterial.ClientCode, da) == false)
                        {
                            strReturnVal = InsertBillsofMaterial(argBillsofMaterial, ref da, ref lstErr);
                        }
                        else
                        {
                            strReturnVal = UpdateBillsofMaterial(argBillsofMaterial, ref da, ref lstErr);
                        }
                    }
                    else
                    {
                        DeleteBillsofMaterial(argBillsofMaterial.MastMaterialCode, argBillsofMaterial.MaterialCode, argBillsofMaterial.MRevisionCode, argBillsofMaterial.ClientCode, argBillsofMaterial.IsDeleted, ref da, ref lstErr);
                    }
                }

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

                if (strReturnVal != "")
                {
                    foreach (DataRow dr in dtSerialNo.Rows)
                    {
                        BOM_Serial objBOM_Serial = new BOM_Serial();
                        objBOM_Serial.MaterialCode = Convert.ToString(dr["MaterialCode"]).Trim();
                        objBOM_Serial.MRevisionCode = Convert.ToString(dr["MRevisionCode"]).Trim();
                        objBOM_Serial.SerialFrom = Convert.ToString(dr["SerialFrom"]).Trim();
                        objBOM_Serial.SerialTo = Convert.ToString(dr["SerialTo"]).Trim();
                        objBOM_Serial.BatchDate = Convert.ToDateTime(dr["BatchDate"]).ToString("yyyy-MM-dd");
                        objBOM_Serial.ClientCode = Convert.ToString(dr["ClientCode"]).Trim();
                        objBOM_Serial.CreatedBy = Convert.ToString(dr["CreatedBy"]).Trim();
                        objBOM_Serial.ModifiedBy = Convert.ToString(dr["ModifiedBy"]).Trim();
                        objBOM_Serial.IsDeleted = Convert.ToInt32(dr["IsDeleted"]);

                        if (objBOM_Serial.IsDeleted == 0)
                        {
                            objBOM_SerialManager.SaveBOM_Serial(objBOM_Serial, da, lstErr);
                        }
                        else
                        {
                            objBOM_SerialManager.DeleteBOM_Serial(objBOM_Serial.MaterialCode, objBOM_Serial.MRevisionCode, objBOM_Serial.ClientCode, objBOM_Serial.IsDeleted);
                        }
                    }

                    foreach (ErrorHandler objerr in lstErr)
                    {
                        if (objerr.Type == "E")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return errorcol;
                        }

                        if (objerr.Type == "A")
                        {
                            da.ROLLBACK_TRANSACTION();
                            return errorcol;
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

        //public ICollection<ErrorHandler> SaveBillsofMaterial(BillsofMaterial argBillsofMaterial)
        //{
        //    List<ErrorHandler> lstErr = new List<ErrorHandler>();
        //    DataAccess da = new DataAccess();
        //    try
        //    {
        //        if (blnIsBillsofMaterialExists(argBillsofMaterial.BOMCode, argBillsofMaterial.MastMaterialCode, argBillsofMaterial.ClientCode) == false)
        //        {

        //            da.Open_Connection();
        //            da.BEGIN_TRANSACTION();
        //            InsertBillsofMaterial(argBillsofMaterial, da, lstErr);
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
        //            UpdateBillsofMaterial(argBillsofMaterial, da, lstErr);
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
        
        public string InsertBillsofMaterial(BillsofMaterial argBillsofMaterial, ref DataAccess da, ref List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@MastMaterialCode", argBillsofMaterial.MastMaterialCode);
            param[1] = new SqlParameter("@MaterialCode", argBillsofMaterial.MaterialCode);
            param[2] = new SqlParameter("@MRevisionCode",argBillsofMaterial.MRevisionCode);
            param[3] = new SqlParameter("@DRevisionCode", argBillsofMaterial.DRevisionCode);
            param[4] = new SqlParameter("@ValidFrom",argBillsofMaterial.ValidFrom);
            param[5] = new SqlParameter("@ValidTo",argBillsofMaterial.ValidTo);
            param[6] = new SqlParameter("@IsSerialBatch",argBillsofMaterial.IsSerialBatch);
            param[7] = new SqlParameter("@Quantity", argBillsofMaterial.Quantity);
            param[8] = new SqlParameter("@IsServiceRec", argBillsofMaterial.IsServiceRec);
            param[9] = new SqlParameter("@IsWarrantyApp", argBillsofMaterial.IsWarrantyApp);
            param[10] = new SqlParameter("@IsExtWarrantyApp", argBillsofMaterial.IsExtWarrantyApp);
            param[11] = new SqlParameter("@BaseWarrantyOn", argBillsofMaterial.BaseWarrantyOn);
            param[12] = new SqlParameter("@IsDOARec", argBillsofMaterial.IsDOARec);
            param[13] = new SqlParameter("@IsAssignTech", argBillsofMaterial.IsAssignTech);
            param[14] = new SqlParameter("@ClientCode", argBillsofMaterial.ClientCode);
            param[15] = new SqlParameter("@CreatedBy", argBillsofMaterial.CreatedBy);
            param[16] = new SqlParameter("@ModifiedBy", argBillsofMaterial.ModifiedBy);
            
            param[17] = new SqlParameter("@Type", SqlDbType.Char);
            param[17].Size = 1;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[18].Size = 255;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[19].Size = 20;
            param[19].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertBillsofMaterial", param);
            
            string strMessage = Convert.ToString(param[18].Value);
            string strType = Convert.ToString(param[17].Value);
            string strRetValue = Convert.ToString(param[19].Value);

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
        
        public string UpdateBillsofMaterial(BillsofMaterial argBillsofMaterial, ref DataAccess da, ref List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[20];
            param[0] = new SqlParameter("@MastMaterialCode", argBillsofMaterial.MastMaterialCode);
            param[1] = new SqlParameter("@MaterialCode", argBillsofMaterial.MaterialCode);
            param[2] = new SqlParameter("@MRevisionCode", argBillsofMaterial.MRevisionCode);
            param[3] = new SqlParameter("@DRevisionCode", argBillsofMaterial.DRevisionCode);
            param[4] = new SqlParameter("@ValidFrom", argBillsofMaterial.ValidFrom);
            param[5] = new SqlParameter("@ValidTo", argBillsofMaterial.ValidTo);
            param[6] = new SqlParameter("@IsSerialBatch", argBillsofMaterial.IsSerialBatch);
            param[7] = new SqlParameter("@Quantity", argBillsofMaterial.Quantity);
            param[8] = new SqlParameter("@IsServiceRec", argBillsofMaterial.IsServiceRec);
            param[9] = new SqlParameter("@IsWarrantyApp", argBillsofMaterial.IsWarrantyApp);
            param[10] = new SqlParameter("@IsExtWarrantyApp", argBillsofMaterial.IsExtWarrantyApp);
            param[11] = new SqlParameter("@BaseWarrantyOn", argBillsofMaterial.BaseWarrantyOn);
            param[12] = new SqlParameter("@IsDOARec", argBillsofMaterial.IsDOARec);
            param[13] = new SqlParameter("@IsAssignTech", argBillsofMaterial.IsAssignTech);
            param[14] = new SqlParameter("@ClientCode", argBillsofMaterial.ClientCode);
            param[15] = new SqlParameter("@CreatedBy", argBillsofMaterial.CreatedBy);
            param[16] = new SqlParameter("@ModifiedBy", argBillsofMaterial.ModifiedBy);

            param[17] = new SqlParameter("@Type", SqlDbType.Char);
            param[17].Size = 1;
            param[17].Direction = ParameterDirection.Output;

            param[18] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[18].Size = 255;
            param[18].Direction = ParameterDirection.Output;

            param[19] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[19].Size = 20;
            param[19].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateBillsofMaterial", param);

            string strMessage = Convert.ToString(param[18].Value);
            string strType = Convert.ToString(param[17].Value);
            string strRetValue = Convert.ToString(param[19].Value);

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

        public List<ErrorHandler> DeleteBillsofMaterial(string argMastMaterialCode, string argMaterialCode, string argMRevisionCode, string argClientCode, int iIsDeleted, ref DataAccess da, ref List<ErrorHandler> lstErr)
        {
            try
            {
                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@MastMaterialCode", argMastMaterialCode);
                param[1] = new SqlParameter("@MaterialCode", argMaterialCode);
                param[2] = new SqlParameter("@MRevisionCode", argMRevisionCode);
                param[3] = new SqlParameter("@ClientCode", argClientCode);
                param[4] = new SqlParameter("@IsDeleted",iIsDeleted);

                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.NExecuteNonQuery("Proc_DeleteBillsofMaterial", param);
                
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
        
        public bool blnIsBillsofMaterialExists(string argMastMaterialCode, string argMaterialCode, string argMRevisionCode, string argClientCode, DataAccess da)
        {
            bool IsBillsofMaterialExists = false;
            DataSet ds = new DataSet();
            ds = GetBillsofMaterialExists(argMastMaterialCode, argMaterialCode,argMRevisionCode, argClientCode, da);

            if (ds.Tables[0].Rows.Count > 0)
            {
                IsBillsofMaterialExists = true;
            }
            else
            {
                IsBillsofMaterialExists = false;
            }
            return IsBillsofMaterialExists;
        }
    }
}