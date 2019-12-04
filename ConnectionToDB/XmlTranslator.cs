using System;
using System.Data;
using System.Data.SqlClient;

namespace ConnectionToDB
{
    public static class XmlTranslator
    {
        private static SqlConnection _conn = null;

        public static string @Path {
            get
            {
                return _path;
            }
            set
            {
                _path = value;
            }
        }
        private static string @_path = "";

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
        private static string _query = "";

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
        private static string _connStr = "";

        public static bool GetXml()
        {
            using (_conn = new SqlConnection(_connStr))
            {
                try
                {
                    _conn.Open();

                    SqlDataAdapter adapter = new SqlDataAdapter(_query, _conn);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds);
                    ds.WriteXml(@_path);
                    return true;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
    }
}
