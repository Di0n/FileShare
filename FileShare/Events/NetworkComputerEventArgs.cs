using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileShare
{
    public class NetworkComputerEventArgs : EventArgs
    {
        public NetworkComputerEventArgs(Computer computer)
        {
            Data = computer;
        }

        public Computer Data { private set; get; }
    }
}
