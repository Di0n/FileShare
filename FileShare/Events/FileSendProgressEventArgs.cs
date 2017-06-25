using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare
{
    class FileSendProgressEventArgs : EventArgs
    {
        public FileSendProgressEventArgs(Computer receiver, long totalSize, long sent)
        {
            Receiver = receiver;
            TotalSize = totalSize;
            Sent = sent;
        }

        public Computer Receiver { get; private set; }
        public long TotalSize { get; private set; }
        public long Sent { get; private set; }
    }
}
