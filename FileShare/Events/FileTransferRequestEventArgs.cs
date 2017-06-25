using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare
{
    public class FileTransferRequestEventArgs : EventArgs
    {
        public FileTransferRequestEventArgs(Computer sender, string fileName, long fileSize)
        {
            Sender = sender;
            FileName = fileName;
            FileSize = fileSize;
            Accept = false;
        }
        public Computer Sender { get; private set; }
        public string FileName { get; private set; }
        public long FileSize { get; private set; }
        public bool Accept { get; set; }
    }
}
