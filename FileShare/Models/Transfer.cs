using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare
{
    public class Transfer
    {
        public Transfer() { }
        public Transfer(string transferType, DateTime time, int duration, long fileSize)
        {
            TransferType = transferType;
            this.Time = time;
            Duration = duration;
            FileSize = fileSize;
        }

        public string TransferType { get; set; }
        public DateTime Time { get; set; }
        public int Duration { get; set; }
        public long FileSize { get; set; }
    }
}
