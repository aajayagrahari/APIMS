
//Created On :: 05, May, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;


namespace RSMApp_Comman
{
    public class ModulePageManager
    {
        const string ModulePageTable = "ModulePage";

        GlobalFunction objGlobalFunction = new GlobalFunction();
        ErrorHandler objErrorHandler = new ErrorHandler();
        
        public ModulePage objGetModulePage(int iIdModulePage, string argClientCode)
        {
            ModulePage argModulePage = new ModulePage();
            DataSet DataSetToFill = new DataSet();

            if (argClientCode.Trim() == "")
            {
                goto ErrorHandlers;
            }

            DataSetToFill = this.GetModulePage4ID(iIdModulePage, argClientCode);

            if (DataSetToFill.Tables[0].Rows.Count <= 0)
            {
                goto Finish;
            }

            argModulePage = this.objCreateModulePage((DataRow)DataSetToFill.Tables[0].Rows[0]);

            goto Finish;

        ErrorHandlers:

        Finish:
            DataSetToFill = null;


            return argModulePage;
        }
        
        public ICollection<ModulePage> colGetModulePage(string argClientCode)
        {
            List<ModulePage> lst = new List<ModulePage>();
            DataSet DataSetToFill = new DataSet();
            ModulePage tModulePage = new ModulePage();

            DataSetToFill = this.GetModulePage(argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateModulePage(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;


            return lst;
        }

        public ICollection<ModulePage> colGetModulePage4Menu(string argParentModule, string argClientCode, List<ModulePage> lst)
        {
            //List<ModulePage> lst = new List<ModulePage>();
            DataSet DataSetToFill = new DataSet();
            ModulePage tModulePage = new ModulePage();

            DataSetToFill = this.GetModulePage(argParentModule, argClientCode);

            if (DataSetToFill != null)
            {
                foreach (DataRow dr in DataSetToFill.Tables[0].Rows)
                {
                    lst.Add(objCreateModulePage4Menu(dr));
                }
            }
            goto Finish;

        Finish:
            DataSetToFill = null;

            return lst;
        }
        
      
        
        public DataSet GetModule(string argTransType, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ModuleType", argTransType);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetModule4TType", param);

            return DataSetToFill;              
        }

        public DataSet GetMainMenu(string argTransType, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ModuleType", argTransType);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetMainMenu", param);

            return DataSetToFill;              
        }


        

        public DataSet GetModuleType(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetModuleType", param);

            return DataSetToFill;
        }

        public DataSet GetModulePage4ID(int iIdModulePage, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@idModulePage", iIdModulePage);
            param[1] = new SqlParameter("@ClientCode", argClientCode);
       
            DataSetToFill = da.FillDataSet("SP_GetModulePage4ID", param);

            return DataSetToFill;
        }

        public DataSet GetModulePage4ID(int iIdModulePage, string argClientCode, DataAccess da)
        {
            //DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@idModulePage", iIdModulePage);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.NFillDataSet("SP_GetModulePage4ID", param);

            return DataSetToFill;
        }

        public DataSet GetModulePage4PID(int IidPModulePage, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@idPModulePage", IidPModulePage);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetModulePage4PID", param);

            return DataSetToFill;
        }
                  
        public DataSet GetModulePage(string argParentModule, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ParentModule", argParentModule);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetModulePage4Parent", param);

            return DataSetToFill;
        }
                
        public DataSet GetModulePage(string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("SP_GetModulePage", param);
            return DataSetToFill;
        }

        public DataSet GetModule4Parent(string argParentModule, string argClientCode)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@ParentModule", argParentModule);
            param[1] = new SqlParameter("@ClientCode", argClientCode);

            DataSetToFill = da.FillDataSet("Sp_GetModule4Parent", param);

            return DataSetToFill;
        }
                                 
        private ModulePage objCreateModulePage(DataRow dr)
        {
            ModulePage tModulePage = new ModulePage();

            tModulePage.SetObjectInfo(dr);

            return tModulePage;
        }

        private ModulePage objCreateModulePage4Menu(DataRow dr)
        {
            ModulePage tModulePage = new ModulePage();

            tModulePage.SetObjectInfo2(dr);

            return tModulePage;
        }
        
        public ICollection<ErrorHandler> SaveModulePage(ModulePage argModulePage)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                if (blnIsModulePageExists(argModulePage.idModulePage, argModulePage.ClientCode) == false)
                {

                    da.Open_Connection();
                    da.BEGIN_TRANSACTION();
                    InsertModulePage(argModulePage, da, lstErr);
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
                    UpdateModulePage(argModulePage, da, lstErr);
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

        public ICollection<ErrorHandler> SaveModulePage(ICollection<ModulePage> colModulePage)
        {
            List<ErrorHandler> lstErr = new List<ErrorHandler>();
            DataAccess da = new DataAccess();
            try
            {
                da.Open_Connection();
                da.BEGIN_TRANSACTION();

                foreach (ModulePage argModulePage in colModulePage)
                {

                    if (blnIsModulePageExists(argModulePage.idModulePage, argModulePage.ClientCode, da) == false)
                    {
                        if (argModulePage.IsDeleted == 0)
                        {
                            InsertModulePage(argModulePage, da, lstErr);
                        }
                    }
                    else
                    {
                        if (argModulePage.IsDeleted == 0)
                        {

                            UpdateModulePage(argModulePage, da, lstErr);
                        }
                        else
                        {
                            DeleteModulePage(argModulePage.Module, argModulePage.ModuleType, argModulePage.ParentModule, argModulePage.ClientCode, argModulePage.IsDeleted, da, lstErr);
                        }
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
        
        public void InsertModulePage(ModulePage argModulePage, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@idModulePage", argModulePage.idModulePage);
            param[1] = new SqlParameter("@ModuleType", argModulePage.ModuleType);
            param[2] = new SqlParameter("@Module", argModulePage.Module);
            param[3] = new SqlParameter("@ParentModule", argModulePage.ParentModule);
            param[4] = new SqlParameter("@parentModuleNode", argModulePage.ParentModuleNode);
            param[5] = new SqlParameter("@idPModulePage", argModulePage.idPModulePage);
            param[6] = new SqlParameter("@PageURL",argModulePage.PageURL);
            param[7] = new SqlParameter("@ClientCode", argModulePage.ClientCode);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_InsertModulePage", param);


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
            lstErr.Add(objErrorHandler);

        }
        
        public void UpdateModulePage(ModulePage argModulePage, DataAccess da, List<ErrorHandler> lstErr)
        {
            SqlParameter[] param = new SqlParameter[11];
            param[0] = new SqlParameter("@idModulePage", argModulePage.idModulePage);
            param[1] = new SqlParameter("@ModuleType", argModulePage.ModuleType);
            param[2] = new SqlParameter("@Module", argModulePage.Module);
            param[3] = new SqlParameter("@ParentModule", argModulePage.ParentModule);
            param[4] = new SqlParameter("@parentModuleNode", argModulePage.ParentModuleNode);
            param[5] = new SqlParameter("@idPModulePage", argModulePage.idPModulePage);
            param[6] = new SqlParameter("@PageURL", argModulePage.PageURL);
            param[7] = new SqlParameter("@ClientCode", argModulePage.ClientCode);

            param[8] = new SqlParameter("@Type", SqlDbType.Char);
            param[8].Size = 1;
            param[8].Direction = ParameterDirection.Output;

            param[9] = new SqlParameter("@Message", SqlDbType.VarChar);
            param[9].Size = 255;
            param[9].Direction = ParameterDirection.Output;

            param[10] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
            param[10].Size = 20;
            param[10].Direction = ParameterDirection.Output;

            int i = da.NExecuteNonQuery("Proc_UpdateModulePage", param);


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
            lstErr.Add(objErrorHandler);

        }
        
        public void DeleteModulePage(string argModule, string argModuleType, string argParentModule, string argClientCode, int iIsDeleted, DataAccess da, List<ErrorHandler> lstErr)
        {       
            try
            {

                SqlParameter[] param = new SqlParameter[8];
                param[0] = new SqlParameter("@ModuleType", argModuleType);
                param[1] = new SqlParameter("@Module", argModule);
                param[2] = new SqlParameter("@ParentModule", argParentModule);
                param[3] = new SqlParameter("@IsDeleted", iIsDeleted);
                param[4] = new SqlParameter("@ClientCode", argClientCode);
                
                param[5] = new SqlParameter("@Type", SqlDbType.Char);
                param[5].Size = 1;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[6].Size = 255;
                param[6].Direction = ParameterDirection.Output;

                param[7] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[7].Size = 20;
                param[7].Direction = ParameterDirection.Output;

                int i = da.NExecuteNonQuery("Proc_DeleteModulePage", param);


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
          

        }
        

        public ICollection<ErrorHandler> DeleteModulePage(string argModule,string argModuleType,string argParentModule, int iIsDeleted)
        {
            DataAccess da = new DataAccess();
            List<ErrorHandler> lstErr = new List<ErrorHandler>();

            try
            {

                SqlParameter[] param = new SqlParameter[7];
                param[0] = new SqlParameter("@ModuleType", argModuleType);
                param[1] = new SqlParameter("@Module", argModule);
                param[2] = new SqlParameter("@ParentModule", argParentModule);
                param[3] = new SqlParameter("@IsDeleted", iIsDeleted);
                

                param[4] = new SqlParameter("@Type", SqlDbType.Char);
                param[4].Size = 1;
                param[4].Direction = ParameterDirection.Output;

                param[5] = new SqlParameter("@Message", SqlDbType.VarChar);
                param[5].Size = 255;
                param[5].Direction = ParameterDirection.Output;

                param[6] = new SqlParameter("@returnvalue", SqlDbType.VarChar);
                param[6].Size = 20;
                param[6].Direction = ParameterDirection.Output;

                int i = da.ExecuteNonQuery("Proc_DeleteModulePage", param);


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
        
        public bool blnIsModulePageExists(int iIdModule, string argClientCode)
        {
            bool IsModulePageExists = false;
            DataSet ds = new DataSet();
            ds = GetModulePage4ID(iIdModule, argClientCode);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsModulePageExists = true;
            }
            else
            {
                IsModulePageExists = false;
            }
            return IsModulePageExists;
        }

        public bool blnIsModulePageExists(int iIdModule, string argClientCode, DataAccess da)
        {
            bool IsModulePageExists = false;
            DataSet ds = new DataSet();
            ds = GetModulePage4ID(iIdModule, argClientCode, da);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsModulePageExists = true;
            }
            else
            {
                IsModulePageExists = false;
            }
            return IsModulePageExists;
        }
    }
}