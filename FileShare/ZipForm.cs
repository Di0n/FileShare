using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileShare
{
    public partial class ZipForm : Form
    {
        private List<File>      files;
        private volatile bool   cancelZip;
        private long            totalFileSize;

        public ZipForm(List<File> files)
        {
            InitializeComponent();
            cancelZip = false;
            this.files = files;
        }

        private Task<File> ZipFiles(List<File> files)
        {
            return Task.Run<File>(() =>
                {
                    using (ZipFile zipFile = new ZipFile())
                    {
                        totalFileSize = 0;
                        for (int i = 0; i < files.Count; i++)
                        {
                            zipFile.AddFile(files[i].Path, "");
                            totalFileSize += files[i].Size;
                        }
                        zipFile.SaveProgress += ZipFile_SaveProgress;

                        string appTempDir = Path.GetTempPath() + "FileShare\\";

                        if (!Directory.Exists(appTempDir))
                        {
                            DirectoryInfo dirInfo = Directory.CreateDirectory(appTempDir);
                            if (!dirInfo.Exists)
                                throw new DirectoryNotFoundException("Failed to create program temp folder.");
                        }

                        string tempFileName = Path.GetRandomFileName();
                        string fullName = appTempDir + tempFileName;

                        zipFile.Save(fullName);

                        if (!cancelZip)
                            return new File(fullName);
                        else
                            return null;
                    }
                });
        }
       
        private long totalBytesTransfered = 0;
        private void ZipFile_SaveProgress(object sender, SaveProgressEventArgs e)
        {
            if (cancelZip)
            {
                e.Cancel = true;
                return;
            }

            totalBytesTransfered = e.BytesTransferred - totalBytesTransfered;
            int totalPercentage = (int)(100.0d * totalBytesTransfered / totalFileSize);

            if (totalPercentage > 0 && totalPercentage < 101)
            {
                if (prb_ZipProgress.InvokeRequired)
                    prb_ZipProgress.BeginInvoke((MethodInvoker)delegate() { this.prb_ZipProgress.Value = totalPercentage; });
            }
        }

        private async void ZipForm_Shown(object sender, EventArgs e)
        {
                ZippedFile = await ZipFiles(files);
                if (cancelZip)
                    this.DialogResult = DialogResult.Cancel;
                else
                    this.DialogResult = DialogResult.OK;
        }

        private void CancelZip_Click(object sender, EventArgs e)
        {
            cancelZip = true;
        }

        public File ZippedFile { get; private set; }
    }
}
