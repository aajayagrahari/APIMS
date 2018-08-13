
//Created On :: 06, June, 2012
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using RSMApp_Connection;
using RSMApp_ErrorResult;

namespace RSMApp_SAP
{
    public class DBTableStructureManager
    {
        const string DBTableStructure = "DBTableStructure";

        public DataSet GetDBTableStructure()
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            DataSetToFill = da.FillDataSet("Sp_GetDBTableNames");
            return DataSetToFill;
        }

        public DataSet GetDBTableFields(string strTableName)
        {
            DataAccess da = new DataAccess();
            DataSet DataSetToFill = new DataSet();

            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@TableName", strTableName);

            DataSetToFill = da.FillDataSet("Sp_GetDBTableFileds", param);
            return DataSetToFill;
        }


       
        
       
       
    }
}