using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    internal class MovingEventArgs : EventArgs
    {
        public MovingEventArgs(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
