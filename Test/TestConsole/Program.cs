using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Threading;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //Student student = new Student();
            //student.Moving += (s) => Console.WriteLine(s);
            //student.Move(7);

            Action<string> Moving = null;
            Moving = (s) => Console.WriteLine(s);
            Action<int> Move = (distance) =>
            {
                for (int i = 1; i <= distance; i++)
                {
                    Thread.Sleep(1000);
                    if (Moving != null)
                        Moving(string.Format("Идет перемещение... пройдено километров: {0}", i));
                }
            };
            Move(4);
            //Console.ReadLine();
            #region Работа с БД
            //DB db = new DB();
            //DataTable table = new DataTable();
            //MySqlDataAdapter adapter = new MySqlDataAdapter();
            //MySqlCommand command = new MySqlCommand("SELECT * FROM weather2021", db.getConnection());
            ////command.Parameters.Add("@d", MySqlDbType.Date).Value = "2021-08-01";

            //adapter.SelectCommand = command;
            //adapter.Fill(table);

            //string result = table.Rows[1]["temperature"].ToString();

            //Console.WriteLine(result);

            ////conn.Close();
            ////WorkWithDataBase.CloseConnection();

            ////db.closeConnection();
            //Console.ReadLine(); 
            #endregion
        }

        static void student_Moving(string message)
        {
            Console.WriteLine(message);
        }
    }
}
