using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Autorization
{
    public class Data
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;port=3307;username=root;password=root;database=users");
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            connection.Open();
        }
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
        public MySqlConnection GetConnection()
        {
            return connection;
        }

    }
}
