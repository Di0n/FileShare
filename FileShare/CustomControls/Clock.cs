using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _Timer = System.Timers.Timer;
namespace FileShare.CustomControls
{
    class Clock : Label
    {
        _Timer updateTimer;

        public Clock()
        {
            updateTimer = new _Timer((60 - DateTime.Now.Second) * 1000);
            updateTimer.AutoReset = true;
            updateTimer.Elapsed += UpdateTimer_Elapsed;
            updateTimer.Start();

            this.VisibleChanged += Clock_Initialized;

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (updateTimer != null)
                    updateTimer.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Clock_Initialized(object sender, EventArgs e)
        {
                SetCurrentTime();
                this.VisibleChanged -= Clock_Initialized;
        }

        private void UpdateTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            updateTimer.Interval = (60 - DateTime.Now.Second) * 1000;
            SetCurrentTime();
        }

        private void SetCurrentTime()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke((MethodInvoker)delegate() { this.Text = DateTime.Now.ToShortTimeString(); });
            }
            else
            {
                this.Text = DateTime.Now.ToShortTimeString();
            }
        }
        
    }
}
