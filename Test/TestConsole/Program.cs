using MySql.Data.MySqlClient;
using System;
using System.Data;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            

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
