using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_Comman
{
    public class xCountryManager
    {
        const string argTableName = "Country";
        const string srgModule = "Country";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();

        
        public DataSet GetCountry(string strCountryCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@CountryCode", strCountryCode);
            DataSetToFill = da.FillDataSet("SP_GetCountry4ID", param);
                        
            return DataSetToFill;
        }

        public DataSet GetCountry()
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            DataSetToFill = da.FillDataSet("SP_GetCountry");

            return DataSetToFill;
        }

        public DataSet GetCountryExists(string strCountryCode)
        {
            DataSet dsCountry = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + argTableName.ToString();

                if (strCountryCode != "")
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " CountryCode = '" + strCountryCode + "'";
                }


                dsCountry = da.FillDataSetWithSQL(tSQL);
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return dsCountry;
        }
        
        public DataSet GetCountryDuplication(string strCountryName)
        {
            DataSet dsCountry = new DataSet();
            DataAccess da = new DataAccess();

            string tSQL = "";
            bool IsWhereClauseAttached = false;
            string sWhereClauseSTR = "";

            try
            {

                tSQL = "SELECT * FROM " + argTableName.ToString();

                if (strCountryName != "")
                {
                    sWhereClauseSTR = IsWhereClauseAttached == true ? "AND" : "WHERE";
                    IsWhereClauseAttached = true;

                    tSQL = tSQL + " " + sWhereClauseSTR + " CountryName = '" + strCountryName + "'";
                }


                dsCountry = da.FillDataSetWithSQL(tSQL);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return dsCountry;
        }

        public xCountry ObjCreateCountry(DataRow dr)
        {
            xCountry objCountry = new xCountry ();
            objCountry.SetObjectInfo(dr);
            return objCountry;
        }

        public xCountry objGetCountry(string strCountryCode)
        {
            xCountry objCountryMaster = new xCountry();
            DataSet DataSetToFill = new DataSet();

            if (strCountryCode != "" || strCountryCode != null)
            {
                goto ErrorHandler;
            }

            DataSetToFill = this.GetCountry(strCountryCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            objCountryMaster = ObjCreateCountry((DataRow)DataSetToFill.Tables[0].Rows[0]);

        ErrorHandler:

        Finish:
            DataSetToFill = null;

            return objCountryMaster;
        }

        public ICollection<xCountry> colGetCountry()
        {
            List<xCountry> lst = new List<xCountry>();
                        
            DataSet DataSetToFill = new DataSet();

            DataSetToFill = this.GetCountry();

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(ObjCreateCountry(dr));
                }
            }

            return lst;
        }

        public ICollection<ErrorHandler> SaveCountry(xCountry objCountry)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            /*Data Access Layer for Sql Connection */
            DataAccess da = new DataAccess();

            string strMSG = "";

            try
            {

                if (blnIsCountryExists(objCountry.CountryCode) == false)
                {

                    if (blnSaveCountryBusinessrules(objCountry.CountryName) == true)
                    {
                        /* Error Handler */
                        objErrorHandler.Type = ErrorConstant.strErrType;
                        objErrorHandler.MsgId = 0;
                        objErrorHandler.Module = ErrorConstant.strCheckDuplModule;
                        objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                        objErrorHandler.Message = "Country with same name exists.";
                        objErrorHandler.RowNo = 0;
                        objErrorHandler.FieldName = "Country Name";
                        objErrorHandler.LogCode = "";

                        lstErr.Add(objErrorHandler);

                        return lstErr;
                    }

                    strMSG = "Connection Created";
                    /* Open and Begin Transaction */
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();

                    InsertCountry(objCountry, da, lstErr);
                    strMSG = "Insert";
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

                    /* Open and Begin Transaction */
                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();

                    UpdateCountry(objCountry, da, lstErr);

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
                    /* Rollback and Close Transaction */
                    da.ROLLBACK_TRANSACTION();
                }
                
                /* Error Handler */
                objErrorHandler.Type = ErrorConstant.strAboartType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strInsertModule;
                objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                objErrorHandler.Message =  strMSG + " " + ex.Message.ToString();
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";

                lstErr.Add(objErrorHandler);

                /* ----------------------- */

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

        //public string SaveCountry(Country objCountry)
        //{
        //    string strRetValue = "";

        //    if (blnIsCountryExists(objCountry.CountryCode) == false)
        //    {
        //        if (CheckDuplication(objCountry.CountryCode, objCountry.CountryName) == false)
        //        {
        //            strRetValue = InsertCountry(objCountry);
        //        }
        //    }
        //    else
        //    {
        //        if (CheckDuplication(objCountry.CountryCode, objCountry.CountryName) == false)
        //        {
        //            strRetValue = UpdateCountry(objCountry);
        //        }
        //    }

        //    return strRetValue;
        //}

        public void InsertCountry(xCountry objCountry, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[5];

            param[0] = new SqlParameter("@CountryCode", objCountry.CountryCode);
            param[1] = new SqlParameter("@CountryName", objCountry.CountryName);

            param[2] = new SqlParameter("@Type", SqlDbType.Char);
            param[2].Size = 1;
            param[2].Direction = ParameterDirection.Output; 

            param[3] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[3].Size = 255;
            param[3].Direction = ParameterDirection.Output;

            param[4] = new SqlParameter("@NewCountryCode", SqlDbType.VarChar);
            param[4].Size = 20;
            param[4].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertCountry", param);

            string strType = Convert.ToString(param[2].Value);
            string strMessage = Convert.ToString(param[3].Value);            
            string strCountryCode = Convert.ToString(param[4].Value);

            
            objErrorHandler.Type = strType;
            objErrorHandler.MsgId = 0;
            objErrorHandler.Module = ErrorConstant.strInsertModule;
            objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
            objErrorHandler.Message = strCountryCode + " " + strMessage.ToString();
            objErrorHandler.RowNo = 0;
            objErrorHandler.FieldName = "";
            objErrorHandler.LogCode = "";

            lstErr.Add(objErrorHandler);

        }

        public void UpdateCountry(xCountry objCountry, DataAccess da, List<ErrorHandler> lstErr)
        {
            //DataAccess da = new DataAccess();
            SqlParameter[] param = new SqlParameter[3];

            param[0] = new SqlParameter("@CountryCode", objCountry.CountryCode);
            param[1] = new SqlParameter("@CountryName", objCountry.CountryName);

            param[2] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[2].Direction = ParameterDirection.ReturnValue;

            
            int i = da.NExecuteNonQuery("Proc_UpdateCountry", param);
            
            string iRetValue = Convert.ToString(param[2].Value);

            string[] strResult = objGlobalFunction.SpliteSQLReturnMsg(iRetValue);
            if (strResult[0].ToString() == "S")
            {
                /* Error Handler */
                objErrorHandler.Type = ErrorConstant.strSuccessType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strInsertModule;
                objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                objErrorHandler.Message = strResult[1].ToString();
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";

                lstErr.Add(objErrorHandler);

                //da.COMMIT_TRANSACTION();
                //da.Close_Connection();
            }
            else
            {
                /* Error Handler */
                objErrorHandler.Type = ErrorConstant.strErrType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strInsertModule;
                objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                objErrorHandler.Message = strResult[1].ToString();
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";

                lstErr.Add(objErrorHandler);

                //da.ROLLBACK_TRANSACTION();
                //da.Close_Connection();
            }
            
        }

        public ICollection<ErrorHandler> DeletetblCountry(string argCountryCode)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] sparam = new SqlParameter[2];            
                sparam[0] = new SqlParameter("@CountryCode", argCountryCode);
                sparam[1] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                sparam[1].Direction = ParameterDirection.ReturnValue;

                int i = da.ExecuteNonQuery("Proc_DeleteCountry", sparam);
                string iRetValue = Convert.ToString(sparam[1].Value);
                
                string[] strResult = objGlobalFunction.SpliteSQLReturnMsg(iRetValue);
                if (strResult[0].ToString() == "S")
                {
                    /* Error Handler */
                    objErrorHandler.Type = ErrorConstant.strSuccessType;
                    objErrorHandler.MsgId = 0;
                    objErrorHandler.Module = ErrorConstant.strInsertModule;
                    objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                    objErrorHandler.Message = strResult[1].ToString();
                    objErrorHandler.RowNo = 0;
                    objErrorHandler.FieldName = "";
                    objErrorHandler.LogCode = "";

                    lstErr.Add(objErrorHandler);

                    //da.COMMIT_TRANSACTION();
                    //da.Close_Connection();

                }
                else
                {
                    /* Error Handler */
                    objErrorHandler.Type = ErrorConstant.strErrType;
                    objErrorHandler.MsgId = 0;
                    objErrorHandler.Module = ErrorConstant.strInsertModule;
                    objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                    objErrorHandler.Message = strResult[1].ToString();
                    objErrorHandler.RowNo = 0;
                    objErrorHandler.FieldName = "";
                    objErrorHandler.LogCode = "";

                    lstErr.Add(objErrorHandler);

                    //da.ROLLBACK_TRANSACTION();
                    //da.Close_Connection();
                }
            }
            catch(Exception ex)
            {
                /* Error Handler */
                objErrorHandler.Type = ErrorConstant.strAboartType;
                objErrorHandler.MsgId = 0;
                objErrorHandler.Module = ErrorConstant.strInsertModule;
                objErrorHandler.ModulePart = ErrorConstant.strMasterModule;
                objErrorHandler.Message = ex.Message.ToString();
                objErrorHandler.RowNo = 0;
                objErrorHandler.FieldName = "";
                objErrorHandler.LogCode = "";

                lstErr.Add(objErrorHandler);

                /* ----------------------- */
            }

            return lstErr;
        }

        public bool blnIsCountryExists(string strCountryCode)
        {
            bool IsCountryExists = false;
            DataSet ds = new DataSet();

            ds = GetCountryExists(strCountryCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsCountryExists = true;
            }
            else
            {
                IsCountryExists = false;
            }

            return IsCountryExists;
        }

        public bool blnSaveCountryBusinessrules(string strCountryName)
        {
            bool IsDuplicate = false;           

            DataSet ds = new DataSet();
            ds = GetCountryDuplication(strCountryName);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsDuplicate = true;
            }
            else
            {
                IsDuplicate = false;
            }




            return IsDuplicate;


        }
        
    }
}
