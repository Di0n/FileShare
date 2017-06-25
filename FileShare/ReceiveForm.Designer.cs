namespace FileShare
{
    partial class ReceiveForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceiveForm));
            this.lbl_Receiver = new System.Windows.Forms.Label();
            this.lbl_DataTransfered = new System.Windows.Forms.Label();
            this.prb_TransferProgress = new System.Windows.Forms.ProgressBar();
            this.lbl_EstimatedTime = new System.Windows.Forms.Label();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_Receiver
            // 
            this.lbl_Receiver.AutoSize = true;
            this.lbl_Receiver.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Receiver.Location = new System.Drawing.Point(9, 9);
            this.lbl_Receiver.Name = "lbl_Receiver";
            this.lbl_Receiver.Size = new System.Drawing.Size(212, 18);
            this.lbl_Receiver.TabIndex = 0;
            this.lbl_Receiver.Text = "Afzender: 127.0.0.1 (Unnamed)\r\n";
            // 
            // lbl_DataTransfered
            // 
            this.lbl_DataTransfered.AutoSize = true;
            this.lbl_DataTransfered.Location = new System.Drawing.Point(9, 43);
            this.lbl_DataTransfered.Name = "lbl_DataTransfered";
            this.lbl_DataTransfered.Size = new System.Drawing.Size(88, 13);
            this.lbl_DataTransfered.TabIndex = 1;
            this.lbl_DataTransfered.Text = "500KB / 1000KB";
            // 
            // prb_TransferProgress
            // 
            this.prb_TransferProgress.Location = new System.Drawing.Point(12, 59);
            this.prb_TransferProgress.Name = "prb_TransferProgress";
            this.prb_TransferProgress.Size = new System.Drawing.Size(360, 30);
            this.prb_TransferProgress.TabIndex = 2;
            // 
            // lbl_EstimatedTime
            // 
            this.lbl_EstimatedTime.AutoSize = true;
            this.lbl_EstimatedTime.Location = new System.Drawing.Point(9, 102);
            this.lbl_EstimatedTime.Name = "lbl_EstimatedTime";
            this.lbl_EstimatedTime.Size = new System.Drawing.Size(139, 13);
            this.lbl_EstimatedTime.TabIndex = 3;
            this.lbl_EstimatedTime.Text = "Geschatte tijd: 00h:00m:00s";
            // 
            // btn_Stop
            // 
            this.btn_Stop.Location = new System.Drawing.Point(297, 119);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(75, 30);
            this.btn_Stop.TabIndex = 4;
            this.btn_Stop.Text = "Stop";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.StopBtn_Click);
            // 
            // ReceiveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.lbl_EstimatedTime);
            this.Controls.Add(this.prb_TransferProgress);
            this.Controls.Add(this.lbl_DataTransfered);
            this.Controls.Add(this.lbl_Receiver);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "ReceiveForm";
            this.Tag = "";
            this.Text = "Bezig met ontvangen van bestand(en)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ReceiveForm_Closing);
            this.Shown += new System.EventHandler(this.ReceiveForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Receiver;
        private System.Windows.Forms.Label lbl_DataTransfered;
        private System.Windows.Forms.ProgressBar prb_TransferProgress;
        private System.Windows.Forms.Label lbl_EstimatedTime;
        private System.Windows.Forms.Button btn_Stop;
    }
}