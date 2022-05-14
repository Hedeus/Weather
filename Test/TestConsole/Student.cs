using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestConsole
{
    //public delegate void ShowMessage(string message);
    internal class Student
    {
        public void Move(int distance)
        {
            for (int i = 1; i <= distance; i++)
            {
                Thread.Sleep(1000);
                if (Moving != null)
                    Moving(string.Format("Идет перемещение... пройдено километров: {0}", i));
            }
        }
        public event Action<string> Moving;

    }
}
