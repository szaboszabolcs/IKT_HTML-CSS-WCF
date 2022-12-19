using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Service_WCF.AdatbazisKezelese
{
    public class Kapcsolat
    {
        protected Kapcsolat() { }

        public static MySqlConnection connection
        {
            get
            {
                MySqlConnection connection = new MySqlConnection();
                string connectionString = "SERVER=localhost;" + "DATABASE=login;" + "UID=root;" + "PASSWORD=;" + "SSL MODE=none;";
                connection.ConnectionString = connectionString;
                return connection;
            }
        }
    }
}