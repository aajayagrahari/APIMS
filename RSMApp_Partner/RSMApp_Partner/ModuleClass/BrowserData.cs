using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RSMApp_Connection;
using RSMApp_ErrorResult;
using System.Data;

namespace RSMApp_Partner
{
    public class BrowserData
    {
        
        public int GetRowCount(string tSQL)
        {
            DataAccess da = new DataAccess();
            int iretValue = da.GetRowCount(tSQL);

            return iretValue;

        }

        public DataTable FillDataSetWithSQLSchema(string tSQL)
        {
            DataAccess da = new DataAccess();
            DataTable dt = new DataTable();
            dt = da.FillDataSetWithSQLSchema(tSQL);

            return dt;
        }

        public DataSet FillDataSetWithSQL(string tSQL)
        {
            DataAccess da = new DataAccess();
            DataSet ds = new DataSet();
            ds = da.FillDataSetWithSQL(tSQL);
            return ds;
        }
    }
}
