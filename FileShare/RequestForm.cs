using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileShare
{
    public partial class RequestForm : Form
    {
        public RequestForm(Computer sender, long totalFileSize)
        {
            InitializeComponent();
            pb_QuestionMark.Image = SystemIcons.Question.ToBitmap();
            lbl_Request.Text = String.Format("{0} @ {1} wil een bestandsoverdracht starten.", sender.Name, sender.IP);
            lbl_TotalSize.Text = String.Format("Totale grootte: {0}", Utility.ConvertFileSize(totalFileSize));
        }

        private void OnTimeOut()
        {
            this.DialogResult = DialogResult.Ignore;
        }

        private void RequestForm_Closing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                this.DialogResult = DialogResult.No;
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            int time = int.Parse(lbl_RequestCountdown.Text);
            if (time == 0)
            {
                OnTimeOut();
            }
            else
            {
                time--;
                if (lbl_RequestCountdown.InvokeRequired)
                {
                    lbl_RequestCountdown.BeginInvoke((MethodInvoker)delegate { lbl_RequestCountdown.Text = time.ToString(); });
                }
                else
                {
                    lbl_RequestCountdown.Text = time.ToString();
                }
            }
        }
    }
}
