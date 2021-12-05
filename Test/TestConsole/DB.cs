using MySql.Data.MySqlClient;

namespace TestConsole
{
    class DB
    {
        MySqlConnection connection = new MySqlConnection("server=localhost;uid=root;pwd=1h9e8d7;database=weather;");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public MySqlConnection getConnection()
        {
            return connection;
        }
    }
}
