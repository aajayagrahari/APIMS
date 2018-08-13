using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWM.Data
{
    public interface IDataAccessLayer
    {
        DataSet FillDataSet(string spName, SqlParameter[] sqlparam);
    }
}
