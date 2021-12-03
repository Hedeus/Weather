using System.Collections.Generic;
using System.Data.SqlClient;

namespace Weather.Data
{
    static class WorkWithDataBase
    {
        public static string connectString;
        static SqlConnection myConnection;
        static string SQLServerName = "MySQL80";
        static string dataBaseName = "weather";


        #region Перегруженные методы подключения к БД
        
        public static void OpenConnection()
        {
            connectString = "Data Source=.\\" + SQLServerName + ";Initial Catalog=" + dataBaseName + ";Integrated Security=true;";
            myConnection = new SqlConnection(connectString);
            myConnection.Open();
        }

        public static void OpenConnection(string login, string password)
        {
            connectString = "Data Source=.\\" + SQLServerName + ";Initial Catalog=" + dataBaseName + ";User ID=" + login + ";Password=" + password + ";";
            myConnection = new SqlConnection(connectString);
            myConnection.Open();
        }
                
        public static void OpenConnection(string SQLServerName, string dataBaseName, string login, string password)
        {
            connectString = "Data Source=.\\" + SQLServerName + ";Initial Catalog=" + dataBaseName + ";Integrated Security=true;";
            myConnection = new SqlConnection(connectString);
            myConnection.Open();
        }

        public static void OpenConnection(string userConnectionString)
        {
            connectString = userConnectionString;
            myConnection = new SqlConnection(connectString);
            myConnection.Open();
        }

        #endregion

        #region Методы отвечающие за запросы к БД

        #region Перегруженные методы SELECT

        public static List<string[]> ExecuteQuery(string query, int col)
        {
            SqlCommand command = new SqlCommand(query, myConnection);

            SqlDataReader reader = command.ExecuteReader();

            List<string[]> response = new List<string[]>();

            while(reader.Read())
            {
                response.Add(new string[col]);

                for (int i = 0; i < col; i++)
                    response[response.Count - 1][i] = reader[i].ToString();
            }

            reader.Close();
            if (response.Count != 0)
                return response;
            else
                return null;
        }

        public static string ExecuteQuery(string query)
        {
            SqlCommand command = new SqlCommand(query, myConnection);

            SqlDataReader reader = command.ExecuteReader();

            string response = null;

            while(reader.Read())
            {
                response = reader[0].ToString();
            }
            reader.Close();
            return response;
        }

        #endregion

        #region запросы без ответа (DELETE, INSERT и т.п.)

        public static void ExecuteQueryWithoutResponse(string query)
        {
            SqlCommand command = new SqlCommand(query, myConnection);

            command.ExecuteNonQuery();
        }

        #endregion

        #endregion

        #region Закрытие соединения с БД

        public static void CloseConnection()
        {
            myConnection.Close();
        }

        #endregion
    }
}
