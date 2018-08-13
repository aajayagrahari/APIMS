using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using System.Configuration;

namespace HRMS_Connection
{
    public class DataAccess
    {
        SqlDataAdapter da;
        SqlCommand cmd;

        SqlConnection con;
        SqlTransaction Transaction;

        #region Private Function for Connection


        private void Make_Connection()
        {
            string sConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            if (sConnectionString == null)
                throw new Exception("Error in Config File.Please Check");
            con = new SqlConnection(sConnectionString);
            cmd = new SqlCommand();
            cmd.CommandTimeout = 50000;
            //cmd.CommandTimeout = 120;	
            cmd.Connection = con;
        }
        #endregion

        # region Private Method for Parameters
        private void Add_Parameter_In_Command(SqlParameter[] oParameterArray)
        {
            if (cmd != null)
            {
                cmd.Parameters.Clear();
                if (oParameterArray != null)
                {
                    foreach (SqlParameter Param in oParameterArray)
                    {
                        cmd.Parameters.Add(Param);
                    }
                }
            }
        }
        #endregion

        #region Public Method for Opening Connection
        public void Open_Connection()
        {
            try
            {
                if (con == null)
                    Make_Connection();

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
            }
            catch (Exception ex)
            {

                //MessageBox.Show( "Please Contect Your Administrator.", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
                throw ex;
            }

        }
        #endregion

        #region Public Method For Closeing Connection
        public void Close_Connection()
        {
            try
            {
                if (con != null)
                {
                    if (con.State == ConnectionState.Open)
                    {
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Reader With ParaMeter
        public SqlDataReader ExecuteReader(string spName, SqlParameter[] Oparam)
        {
            SqlDataReader dr;
            try
            {
                Open_Connection();
                Add_Parameter_In_Command(Oparam);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;
        }

        #endregion

        #region Reader Without ParaMeter
        public SqlDataReader ExecuteReader(string spName)
        {
            SqlDataReader dr;
            try
            {
                Open_Connection();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dr;
        }

        #endregion

        #region Dataset Methods With Parameter
        public DataSet FillDataSet(string spName, SqlParameter[] sqlparam)
        {

            try
            {
                Open_Connection();
                DataSet DataSetToFill = new DataSet();
                if (sqlparam != null)
                    Add_Parameter_In_Command(sqlparam);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                da = new SqlDataAdapter(cmd);
                da.Fill(DataSetToFill);
                return DataSetToFill;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());

            }
            finally
            {
                Close_Connection();
                da.Dispose();
            }

        }
        #endregion

        #region Dataset Methods withOut Parameter
        public DataSet FillDataSet(string spName)
        {
            try
            {
                Open_Connection();
                DataSet DataSetToFill = new DataSet();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                da = new SqlDataAdapter(cmd);
                da.Fill(DataSetToFill, spName);
                return DataSetToFill;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                Close_Connection();
                da.Dispose();
            }
        }
        #endregion

        #region  ExecuteNonQuery

        public int ExecuteNonQuery(string spName, SqlParameter[] sqlparam)
        {
            int retValue = 0;
            try
            {
                Open_Connection();
                Add_Parameter_In_Command(sqlparam);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                retValue = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                Close_Connection();
            }
            return retValue;
        }

        #endregion

        #region function void execute non query without parameters
        public int ExecuteNonQuery(String spname)
        {
            int retval;
            try
            {
                Open_Connection();
                cmd.CommandText = spname;
                cmd.CommandType = CommandType.StoredProcedure;
                retval = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close_Connection();
            }
            return retval;
        }

        public int ExecuteNonQueryWithSQL(string tSQL)
        {
            int iRetValue;
            try
            {
                Open_Connection();
                cmd.CommandText = tSQL;
                cmd.CommandType = CommandType.Text;
                iRetValue = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close_Connection();
            }
            return iRetValue;
        }

        public int nExecute(string tSQL)
        {
            int iRetValue;
            try
            {
                //cmd = new SqlCommand();
                //cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = tSQL;
                iRetValue = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return iRetValue;
        }


        #endregion

        #region Dataset Methods With SQL
        public DataSet FillDataSetWithSQL(string SQL)
        {
            try
            {
                Open_Connection();
                DataSet DataSetToFill = new DataSet();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL;
                da = new SqlDataAdapter(cmd);
                da.Fill(DataSetToFill, SQL);
                Close_Connection();
                da.Dispose();
                return DataSetToFill;
            }


            catch (Exception ex)
            {
                throw ex;


            }

        }

        public DataTable FillDataSetWithSQLSchema(string SQL)
        {

            try
            {
                Open_Connection();
                DataTable table = new DataTable();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL;
                da = new SqlDataAdapter(cmd);
                table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                da.FillSchema(table, SchemaType.Source);
                Close_Connection();
                da.Dispose();
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region GetCountRows()

        public int GetRowCount(string SQL)
        {
            int iRowCount = 0;
            try
            {
                Open_Connection();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL;
                iRowCount = (int)cmd.ExecuteScalar();
                Close_Connection();
                da.Dispose();
                return iRowCount;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        #endregion

        #region NEW Execute Methods

        public SqlDataReader NExecuteReader(string spName, SqlParameter[] Oparam)
        {
            SqlDataReader dr;
            try
            {

                Add_Parameter_In_Command(Oparam);
                //cmd = new SqlCommand();
                //cmd.Connection = con;
                cmd.Transaction = Transaction;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                dr = cmd.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return dr;
        }

        public int NExecuteNonQuery(string spName, SqlParameter[] sqlparam)
        {
            int retValue = 0;
            try
            {
                //BEGIN_TRANSACTION();
                //Open_Connection();
                //cmd = new SqlCommand();
                //cmd.Connection = con;
                cmd.Transaction = Transaction;
                Add_Parameter_In_Command(sqlparam);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                retValue = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            //finally
            //{
            //    Close_Connection();
            //}
            return retValue;
        }

        public DataSet NFillDataSetWithSQL(string SQL)
        {
            try
            {
                //Open_Connection();
                DataSet DataSetToFill = new DataSet();
                // cmd = new SqlCommand();
                //cmd.Connection = con;
                cmd.Transaction = Transaction;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = SQL;

                da = new SqlDataAdapter(cmd);
                da.Fill(DataSetToFill, SQL);
                //Close_Connection();
                //da.Dispose();
                return DataSetToFill;
            }


            catch (Exception ex)
            {
                throw ex;


            }

        }

        public DataSet NFillDataSet(string spName, SqlParameter[] sqlparam)
        {
            try
            {
                //Open_Connection();
                DataSet DataSetToFill = new DataSet();
                // cmd = new SqlCommand();
                // cmd.Connection = con;
                cmd.Transaction = Transaction;
                if (sqlparam != null)
                    Add_Parameter_In_Command(sqlparam);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = spName;
                da = new SqlDataAdapter(cmd);
                da.Fill(DataSetToFill);
                return DataSetToFill;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                //Close_Connection();
                //da.Dispose();
            }
        }

        public void BEGIN_TRANSACTION()
        {
            if (con.State == ConnectionState.Open)
            {
                Transaction = con.BeginTransaction();
            }
            //string tSQL;
            //tSQL = "Begin Transaction";
            //nExecute(tSQL);
        }

        public void COMMIT_TRANSACTION()
        {
            if (con.State == ConnectionState.Open)
            {
                Transaction.Commit();
            }
            //string tSQL;
            //tSQL = "Commit Transaction";
            //nExecute(tSQL);
        }

        public void ROLLBACK_TRANSACTION()
        {
            if (con.State == ConnectionState.Open)
            {
                Transaction.Rollback();
            }
            //string tSQL;
            //tSQL = "Commit Transaction";
            //nExecute(tSQL);
        }

        #endregion

        public void BulkCopy(DataSet dsExcel, string argTableName)
        {
            try
            {
                if (dsExcel != null)
                {
                    if (dsExcel.Tables.Count > 0)
                    {
                        Open_Connection();
                        SqlBulkCopy bulkcopy = new SqlBulkCopy(con);
                        DataTable dtbulk = dsExcel.Tables[0];

                        bulkcopy.DestinationTableName = argTableName;
                        bulkcopy.WriteToServer(dtbulk);

                        bulkcopy.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Close_Connection();
            }
        }

    }
}
