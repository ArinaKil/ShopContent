using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ShopContent.Classes
{
    public class Connection
    {
        private static readonly string config = "server=student.permaviat.ru;Trusted_Connection=No;DataBase=ShopContent;User=;PWD=;";
        public static SqlConnection OpenConnection()
        {
            SqlConnection connection = new SqlConnection(config);
            connection.Open();
            return connection;
        }
        public static SqlDataReader Query(string SQL, SqlConnection connection)
        {
            SqlCommand command = new SqlCommand(SQL, connection);
            return command.ExecuteReader();
        }
        public static void CloseConnection(SqlConnection connection)
        {
            connection.Close();
            SqlConnection.ClearPool(connection);
        }
    }
}
