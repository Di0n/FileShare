namespace FileShare
{
    partial class SendForm
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
            this.btn_CancelTransfers = new System.Windows.Forms.Button();
            this.lbl_Seperator = new System.Windows.Forms.Label();
            this.lbl_Remaining = new System.Windows.Forms.Label();
            this.lbl_Seperator_2 = new System.Windows.Forms.Label();
            this.clock1 = new FileShare.CustomControls.Clock();
            this.slv_Receivers = new FileShare.CustomControls.SendListView();
            this.clmn_SocketAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn_PCName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn_Status = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn_Percentage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // btn_CancelTransfers
            // 
            this.btn_CancelTransfers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_CancelTransfers.Location = new System.Drawing.Point(464, 281);
            this.btn_CancelTransfers.Name = "btn_CancelTransfers";
            this.btn_CancelTransfers.Size = new System.Drawing.Size(107, 30);
            this.btn_CancelTransfers.TabIndex = 1;
            this.btn_CancelTransfers.Text = "Stop overdrachten";
            this.btn_CancelTransfers.UseVisualStyleBackColor = true;
            this.btn_CancelTransfers.Click += new System.EventHandler(this.CancelTransfersBtn_Click);
            // 
            // lbl_Seperator
            // 
            this.lbl_Seperator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Seperator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Seperator.Location = new System.Drawing.Point(-1, 267);
            this.lbl_Seperator.Name = "lbl_Seperator";
            this.lbl_Seperator.Size = new System.Drawing.Size(600, 2);
            this.lbl_Seperator.TabIndex = 5;
            // 
            // lbl_Remaining
            // 
            this.lbl_Remaining.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Remaining.AutoSize = true;
            this.lbl_Remaining.Location = new System.Drawing.Point(13, 336);
            this.lbl_Remaining.Name = "lbl_Remaining";
            this.lbl_Remaining.Size = new System.Drawing.Size(68, 13);
            this.lbl_Remaining.TabIndex = 6;
            this.lbl_Remaining.Text = "Resterend: 0";
            // 
            // lbl_Seperator_2
            // 
            this.lbl_Seperator_2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Seperator_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Seperator_2.Location = new System.Drawing.Point(-1, 324);
            this.lbl_Seperator_2.Name = "lbl_Seperator_2";
            this.lbl_Seperator_2.Size = new System.Drawing.Size(600, 2);
            this.lbl_Seperator_2.TabIndex = 8;
            // 
            // clock1
            // 
            this.clock1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clock1.AutoSize = true;
            this.clock1.Location = new System.Drawing.Point(537, 336);
            this.clock1.Name = "clock1";
            this.clock1.Size = new System.Drawing.Size(34, 13);
            this.clock1.TabIndex = 11;
            this.clock1.Text = "15:58";
            // 
            // slv_Receivers
            // 
            this.slv_Receivers.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.slv_Receivers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.slv_Receivers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmn_SocketAddress,
            this.clmn_PCName,
            this.clmn_Status,
            this.clmn_Percentage});
            this.slv_Receivers.FullRowSelect = true;
            this.slv_Receivers.Location = new System.Drawing.Point(0, 0);
            this.slv_Receivers.Name = "slv_Receivers";
            this.slv_Receivers.Size = new System.Drawing.Size(584, 269);
            this.slv_Receivers.TabIndex = 10;
            this.slv_Receivers.UseCompatibleStateImageBehavior = false;
            this.slv_Receivers.View = System.Windows.Forms.View.Details;
            // 
            // clmn_SocketAddress
            // 
            this.clmn_SocketAddress.Text = "Socket Adres";
            this.clmn_SocketAddress.Width = 180;
            // 
            // clmn_PCName
            // 
            this.clmn_PCName.Text = "Naam";
            this.clmn_PCName.Width = 160;
            // 
            // clmn_Status
            // 
            this.clmn_Status.Text = "Status";
            this.clmn_Status.Width = 148;
            // 
            // clmn_Percentage
            // 
            this.clmn_Percentage.Text = "%";
            this.clmn_Percentage.Width = 35;
            // 
            // SendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.clock1);
            this.Controls.Add(this.lbl_Seperator_2);
            this.Controls.Add(this.lbl_Remaining);
            this.Controls.Add(this.lbl_Seperator);
            this.Controls.Add(this.btn_CancelTransfers);
            this.Controls.Add(this.slv_Receivers);
            this.MinimumSize = new System.Drawing.Size(500, 300);
            this.Name = "SendForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "";
            this.Text = "SendForm";
            this.Load += new System.EventHandler(this.SendForm_Load);
            this.Shown += new System.EventHandler(this.SendForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_CancelTransfers;
        private System.Windows.Forms.Label lbl_Seperator;
        private System.Windows.Forms.Label lbl_Remaining;
        private System.Windows.Forms.Label lbl_Seperator_2;
        private CustomControls.SendListView slv_Receivers;
        private System.Windows.Forms.ColumnHeader clmn_SocketAddress;
        private System.Windows.Forms.ColumnHeader clmn_PCName;
        private System.Windows.Forms.ColumnHeader clmn_Status;
        private System.Windows.Forms.ColumnHeader clmn_Percentage;
        private CustomControls.Clock clock1;
    }
}