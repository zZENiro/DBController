using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ConnectionToDB
{
    public static class ConnectionManager
    {
        private static string _query = "";
        public static string Query
        {
            get
            {
                return _query;
            }
            set
            {
                _query = value;
            }
        }

        private static string _connStr = "";
        public static string ConnectionString
        {
            get
            {
                return _connStr;
            }
            set
            {
                _connStr = value;
            }
        }

        private static SqlConnection _connection;
        //private static MySqlConnection _mySqlConnection;

        public enum SqlTypeConnection
        {
            MySql = 0,
            MSSql = 1
        }

        // непонятно как делать, если процедура может при вызове требовать
        // вводить параметры. Знать заранее мы не можем, какую процедуру
        // выберет пользователь D^:
        public static DataSet EmitStoredProcedure()
        {
            using (_connection = new SqlConnection(_connStr))
            {
                try
                {
                    _connection.Open();
                    SqlCommand _comm = new SqlCommand(_query, _connection);
                    _comm.CommandType = CommandType.StoredProcedure;
                    _comm.ExecuteNonQuery();

                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return EmitStoredProcedure();
                }
            }

            return new DataSet();
        }

        public static DataSet Connect(SqlTypeConnection type)
        {
            switch (type)
            {
                case SqlTypeConnection.MSSql:
                    {
                        return MSSqlConnection();
                    }

                case SqlTypeConnection.MySql:
                    {
                        return MySqlConnection();
                    }

                default:
                    return new DataSet();
            }
        }

        private static DataSet MySqlConnection()
        {
            DataSet ds = new DataSet();



            return ds;
        }

        private static DataSet MSSqlConnection()
        {
            DataSet ds;

            using (_connection = new SqlConnection(_connStr))
            {
                try
                {
                    _connection.Open();

                    SqlCommand _cmd = new SqlCommand(_query, _connection);
                    _cmd.ExecuteNonQuery();

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(_query, _connection);

                    ds = new DataSet();
                    dataAdapter.Fill(ds);
                    return ds;
                }
                catch (SqlException ex)
                {
                    // if query isn't valid then show message of exception and return empty DataSet to current statement (_currState) of Main()
                    Console.WriteLine(ex.Message);
                    return new DataSet();
                }
            }
        }
    }
}
