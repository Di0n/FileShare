using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using FileShare.Properties;
using System.IO;
using System.Net.Sockets;

namespace FileShare
{
    public partial class FileForm : Form
    {
        private ReceiveForm receiveForm;

        public FileForm()
        {
            InitializeComponent();
            Task receive = new Task(new Action(RunFileReceiveServer), TaskCreationOptions.LongRunning);
            receive.ConfigureAwait(false);
            receive.Start();

            ni_NotifyIcon.Icon = Resources.DeY;
        }

        private void RunFileReceiveServer()
        {
            using (FileReceiver receiver = new FileReceiver())
            {
                receiver.FileTransferRequest += FileReceiver_FileTransferRequest;
                receiver.FileTransferProgressChanged += FileReceiver_ProgressChanged;
                receiver.FileTransferCancelled += FileReceiver_TransferCancelled;

                while (true) // Keep running the filereceiver
                {
                    try
                    {
                        receiver.Run();
                    }
                    catch (SocketException) { }
                    Task.Delay(5000).Wait(); // Wait 5 seconds before restarting the receiver.
                }
            }
        }



        private List<string> BrowseFileDialog()
        {
            List<string> files = new List<string>();

            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.Multiselect = true;
                fd.Title = "Selecteer bestanden";
                fd.SupportMultiDottedExtensions = true;
                fd.AddExtension = true;
                fd.CheckFileExists = true;
                fd.CheckPathExists = true;

                DialogResult result = fd.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    files = fd.FileNames.ToList();
                }
            }

            return files;
        }

        private List<Computer> SelectReceivers()
        {
            DialogResult recvFormResult;
            using (ReceiverForm rf = new ReceiverForm())
            {
                recvFormResult = rf.ShowDialog(this);
                if (recvFormResult == DialogResult.OK)
                    return rf.Receivers;
            }
            return null;
        }

        private void StartFileUpload(File file, List<Computer> computers)
        {
            using (SendForm sf = new SendForm(file, computers))
            {
                sf.ShowDialog(this);
            }
        }

        private File StartFileZip(List<File> files)
        {
            using (ZipForm zf = new ZipForm(files))
            {
                DialogResult result;
                try
                {
                    result = zf.ShowDialog(this);
                    if (result == DialogResult.OK)
                        return zf.ZippedFile;
                    else if (result == DialogResult.Cancel)
                        return null;
                }
                catch (Ionic.Zip.ZipException) { }
                catch (System.IO.IOException) { }
                catch (Exception) { }
            }

            MessageBox.Show("Er ging iets fout tijdens het zippen van de bestanden.", "Zip fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }

        // *** EVENTS *** \\
        private void FileReceiver_FileTransferRequest(object sender, FileTransferRequestEventArgs args)
        {
            if (receiveForm != null && !receiveForm.IsDisposed)
            {
                if (receiveForm.TransferInProgress) // Don't accept transfers if one is in progress.
                {
                    args.Accept = false;
                    return;
                }
            }
            using (RequestForm requestForm = new RequestForm(args.Sender, args.FileSize))
            {
                DialogResult result = requestForm.ShowDialog();
                if (result != DialogResult.Yes)
                {
                    args.Accept = false;
                    return;
                }
            }

            FormCollection openForms = Application.OpenForms;
            for (int i = 0; i < openForms.Count; i++)
            {
                Form form = openForms[i];

                if ((form.Tag as string) != "FormExtension" && form != this || ((form.Tag as string) != "FormExtension" && openForms.Count == 1))
                {
                    form.BeginInvoke((MethodInvoker)delegate
                    {
                        using (receiveForm = new ReceiveForm(args.Sender, args.FileSize))
                        {
                            receiveForm.ShowDialog(form);
                        }
                    });
                    break;
                }
            }
            args.Accept = true;
        }

        private async void FileReceiver_ProgressChanged(object sender, FileTransferProgressEventArgs args)
        {
            if (receiveForm != null)
            {
                if (receiveForm.CancellationRequested) args.Cancel = true;
                else await receiveForm.UpdateStatus(args.BytesTransfered, args.TransferSpeed);
            }
        }

        private void FileReceiver_TransferCancelled(object sender, EventArgs args)
        {
            if (receiveForm != null)
            {
                receiveForm.CancelFileTransfer();
            }
        }

        private void FileListChanged(object source, EventArgs e)
        {
            lbl_Objects.Text = String.Format("Bestanden: {0}", flv_FileList.Items.Count);
            lbl_TotalFileSize.Text = Utility.ConvertFileSize(flv_FileList.TotalFileSize);
        }

        private async void BrowseFiles_Click(object sender, EventArgs e)
        {
            List<string> fileNames = BrowseFileDialog();
            if (fileNames.Count > 0)
            {
                List<File> files = new List<File>(fileNames.Count);
                fileNames.ForEach(f => files.Add(new File(f)));
                await flv_FileList.AddFiles(files);
            }
        }

        private void HelpItem_Click(object sender, EventArgs e)
        {
            using (HelpForm hf = new HelpForm())
                hf.ShowDialog(this);
        }

        private void HistoryItem_Click(object sender, EventArgs e)
        {
            using (HistoryForm hf = new HistoryForm())
                hf.ShowDialog(this);
        }

        private void UploadFiles_Click(object sender, EventArgs e)
        {
            if (flv_FileList.Items.Count == 0)
            {
                MessageBox.Show("Geen bestanden geselecteerd.");
                return;
            }

            FormVisible = false;
            List<Computer> computers = SelectReceivers();

            if (computers == null)
            {
                FormVisible = true;
                return;
            }

            List<File> files = flv_FileList.GetFiles().ToList();

            File zipFile = null;
            if (flv_FileList.Items.Count == 1 && (flv_FileList.Items[0].Tag as File).Type == ".ZIP")
                zipFile = (File)flv_FileList.Items[0].Tag;
            else
                zipFile = StartFileZip(files);

            if (zipFile == null)
            {
                FormVisible = true;
                return;
            }

            StartFileUpload(zipFile, computers);

            if (zipFile.IsTempFile)
            {
                if (System.IO.File.Exists(zipFile.Path))
                    try
                    {
                        System.IO.File.Delete(zipFile.Path);
                    }
                    catch (IOException) { }
            }

            flv_FileList.ClearAllFiles();

            FormVisible = true;
        }

        private void FileList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (flv_FileList.FocusedItem.Bounds.Contains(e.Location))
                    cms_FileListMenu.Show(flv_FileList, e.Location);
            }
        }

        private void FileListMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == tsmi_RemoveFiles)
            {
                flv_FileList.RemoveSelectedFiles();
            }
        }

        private void ExitApplication_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FileForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                ni_NotifyIcon.Visible = true;
                this.Hide();
            }
            else if (this.WindowState != FormWindowState.Minimized)
            {
                ni_NotifyIcon.Visible = false;
            }

        }

        private void OpenFileShare_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            ni_NotifyIcon.Visible = false;
        }

        private void ExitFileShare_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Weet je zeker dat je het programma af wilt sluiten?", "FileShare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void NotifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                ni_NotifyIcon.Visible = false;
            }
        }

        private void FileForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.Visible = false;
                ni_NotifyIcon.Visible = true;
                e.Cancel = true;
            }
        }

        // Properties
        private bool formVisible;
        private bool FormVisible
        {
            get { return formVisible; }
            set
            {
                formVisible = value;
                this.Visible = value;
                this.ShowInTaskbar = value;
            }
        }
    }
}
