using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare
{
    public class ResolveIPException : Exception
    {
        public ResolveIPException() { }
        public ResolveIPException(string message) : base(message) { }
        public ResolveIPException(string message, Exception inner) : base(message, inner) { }
    }
}
