using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare
{
    class FileTransferProgressEventArgs : EventArgs
    {
        public FileTransferProgressEventArgs(long bytesTransfered, long totalBytes, int transferSpeed)
        {
            BytesTransfered = bytesTransfered;
            TotalBytes = totalBytes;
            TransferSpeed = transferSpeed;
            Cancel = false;
        }

        public long BytesTransfered { get; private set; }
        public long TotalBytes { get; private set; }
        public int TransferSpeed { get; private set; } // Speed in kbps
        public bool Cancel { get; set; }
    }
}
