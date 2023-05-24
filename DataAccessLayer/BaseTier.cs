using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MovieTheater
{
    public class BaseTier
    {
        public SqlConnection conn;
        public SqlCommand cmd;
        public SqlDataReader reader;
        public string connectionString;
        public string query;
        public bool success;


        public BaseTier()
        {
            connectionString = ConfigurationManager.ConnectionStrings["TargetDatabase"].ToString();
        }
    }
}