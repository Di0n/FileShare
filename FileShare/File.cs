using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare
{
    public class File
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Path { get; private set; }
        public long Size { get; private set; }
        public DateTime DateModified { get; private set; }

        public File(string fullName)
        {
            FileInfo info = new FileInfo(fullName);
            Name = info.Name;
            Path = fullName;
            Type = info.Extension.ToUpper();
            Size = info.Length;
            DateModified = info.LastWriteTimeUtc;
        }

        public Icon GetIcon()
        {
            Icon result = null;

            try
            {
                result = Icon.ExtractAssociatedIcon(Path);
            }
            catch (ArgumentException)
            {
                result = SystemIcons.WinLogo; // if icon.ExtractAssociatedIcon() failed, set default icon.
            }

            return result;
        }

        public bool IsTempFile
        {
            get
            {
                return Path.StartsWith(System.IO.Path.GetTempPath());
            }
        }
    }
}
