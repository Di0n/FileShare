using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileShare
{
    public partial class ReceiveForm : Form
    {
        private readonly Computer sender;
        private readonly long totalFileSize;
        private volatile bool cancellationRequested;
        private volatile bool transferInProgress;
        private List<int> transferTimes;
        private DateTime speedCheckTime;

        public ReceiveForm(Computer sender, long totalFileSize)
        {
            InitializeComponent();
            this.sender = sender;
            this.totalFileSize = totalFileSize;
            speedCheckTime = DateTime.Now;
            transferTimes = new List<int>();
            transferInProgress = true;
        }

        public void UpdateStatus(long totalReceived, int transferSpeed)
        {
            int percentage = (int)(100.0d * totalReceived / totalFileSize);

            this.BeginInvoke((MethodInvoker)delegate
            {
                if (percentage == 100)
                {
                    if (this.WindowState == FormWindowState.Minimized)
                        WindowFlasher.FlashWindow(this);
                }

                this.Text = percentage == 100 ? "Voltooid (100%)" : String.Format("Bezig met ontvangen ({0}%)", percentage);
                lbl_DataTransfered.Text = String.Format("{0} / {1}", Utility.ConvertFileSize(totalReceived), Utility.ConvertFileSize(totalFileSize));
                prb_TransferProgress.Value = percentage;
            });

            long bytesLeft = totalFileSize - totalReceived;

            if (transferSpeed != 0)
            {
                long estimatedTime = (bytesLeft * 8) / transferSpeed;
                TimeSpan ts = TimeSpan.FromMilliseconds(estimatedTime);

                if (ts.TotalMilliseconds != 0)
                    transferTimes.Add((int)ts.TotalMilliseconds);
                if ((DateTime.Now - speedCheckTime).TotalSeconds >= 1)
                {
                    TimeSpan timeLeft = TimeSpan.FromMilliseconds(transferTimes.Average());
                    this.BeginInvoke((MethodInvoker)delegate { lbl_EstimatedTime.Text = String.Format("Geschatte tijd: {0}", timeLeft.ToString(@"hh\h\:mm\m\:ss\s")); });
                    transferTimes.Clear();
                    speedCheckTime = DateTime.Now;
                }
            }

            if (totalReceived == totalFileSize)
            {
                transferInProgress = false;

                this.BeginInvoke((MethodInvoker)delegate
                {
                    btn_Stop.Text = "Sluiten";
                });
            }
        }

        public void CancelFileTransfer()
        {
            this.BeginInvoke((MethodInvoker)delegate
            {
                if (!this.IsDisposed)
                    this.Text = "Geannuleerd door verzender";
                transferInProgress = false;
            });
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            if (transferInProgress && !CancellationRequested)
            {
                DialogResult result = MessageBox.Show("Weet u zeker dat u de bestandsoverdracht wil annuleren?", "Annuleren", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                CancellationRequested = result == DialogResult.No ? false : true;

                if (!CancellationRequested)
                    return;

                transferInProgress = false;
                this.Close();
            }
            else if (!transferInProgress)
            {
                this.Close();
            }
        }

        private void ReceiveForm_Shown(object sender, EventArgs e)
        {
            lbl_Receiver.Text = String.Format("Afzender: {0} ({1})", this.sender.IP, this.sender.Name);
            lbl_DataTransfered.Text = String.Format("0KB / {0}KB", totalFileSize);
        }

        private void ReceiveForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (!transferInProgress || CancellationRequested)
                    return;

                DialogResult result = MessageBox.Show("Weet u zeker dat u de bestandsoverdracht wilt annuleren?", "Annuleren", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.No)
                    e.Cancel = true;
                else
                {
                    this.CancellationRequested = true;
                    transferInProgress = false;
                }
            }
        }

        public bool CancellationRequested 
        {
            get { return cancellationRequested; }
            set { cancellationRequested = value; }
        }
        public bool TransferInProgress { get { return transferInProgress; } }
    }
}
