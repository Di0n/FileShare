using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare
{
    public class Computer
    {
        public Computer() { }
        public Computer(string name, string ip)
        {
            Name = name;
            IP = ip;
        }

        public string Name { get; set; }
        public string IP { get; set; }
        public ushort Port { get { return 15600; } }
    }
}
