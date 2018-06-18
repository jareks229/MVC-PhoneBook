using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Warsztaty_3.Repositories
{
    public class ActiveRecord
    {
        public int Id { get; protected set; }

        public static string ConnectionString = "Integrated Security=SSPI;" + "Initial Catalog=Persons;" +
                                                 "Data Source = DESKTOP-OO9IBF3\\SQLEXPRESS";

        public SqlConnection Connection;

        public static SqlConnection GetOpenConnection()
        {
            var connection = new SqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }

        public void Open()
        {
            Connection = GetOpenConnection();
        }

        public SqlCommand GetCommand(string query)
        {
            return new SqlCommand(query, Connection);
        }

        public void Close()
        {
            Connection.Close();
        }


    }
}
