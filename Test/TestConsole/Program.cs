using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //WorkWithDataBase.OpenConnection("root", "1h9e8d7");
            //string connstring = "server=localhost;uid=root;pwd=1h9e8d7;database=weather;";

            //MySqlConnection conn = new MySqlConnection(connstring); ;

            //MySqlConnection conn = new MySqlConnection(connstring);
            //try
            //{
            //    //WorkWithDataBase.OpenConnection(connstring);
            //    //conn.Open();
            //}
            //catch (MySqlException ex)
            //{
            //    switch (ex.Number)
            //    {
            //        case 0:
            //            Console.WriteLine("Cannot connect to server.  Contact administrator");
            //            break;
            //        case 1045:
            //            Console.WriteLine("Invalid username/password, please try again");
            //            break;
            //    }
            //    // Console.WriteLine(ex.Source);
            //}

            DB db = new DB();
            DataTable table = new DataTable();
            MySqlDataAdapter adapter = new MySqlDataAdapter();

            //string sql = "SELECT temperature FROM weather.weather2021 WHERE date='2021-08-01';";
            //db.openConnection();

            //MySqlCommand command = new MySqlCommand("SELECT `temperature` FROM weather.weather2021 WHERE `date`=@d", db.getConnection());
            MySqlCommand command = new MySqlCommand("SELECT * FROM weather2021", db.getConnection());
           //command.Parameters.Add("@d", MySqlDbType.Date).Value = "2021-08-01";

            adapter.SelectCommand = command;
            adapter.Fill(table);

            string result = table.Rows[1]["temperature"].ToString();

            //foreach(DataRow row in table.Rows)
            //{
            //    foreach(DataColumn column in table.Columns)
            //    {
            //        Console.WriteLine(row[column]);
            //    }
            //}     

            Console.WriteLine(result);

            //conn.Close();
            //WorkWithDataBase.CloseConnection();

            //db.closeConnection();
            Console.ReadLine();
        }
    }
}
