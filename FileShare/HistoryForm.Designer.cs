namespace FileShare
{
    partial class HistoryForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HistoryForm));
            this.lv_HistoryData = new System.Windows.Forms.ListView();
            this.clmn_Type = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn_Time = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn_Duration = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn_DataSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn_Sender = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn_Receiver = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnTransferCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lv_HistoryData
            // 
            this.lv_HistoryData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lv_HistoryData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lv_HistoryData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmn_Type,
            this.clmn_Time,
            this.clmn_Duration,
            this.clmn_DataSize,
            this.clmn_Sender,
            this.clmn_Receiver,
            this.clmnTransferCount});
            this.lv_HistoryData.Location = new System.Drawing.Point(0, 0);
            this.lv_HistoryData.Name = "lv_HistoryData";
            this.lv_HistoryData.Size = new System.Drawing.Size(747, 365);
            this.lv_HistoryData.TabIndex = 0;
            this.lv_HistoryData.UseCompatibleStateImageBehavior = false;
            this.lv_HistoryData.View = System.Windows.Forms.View.Details;
            // 
            // clmn_Type
            // 
            this.clmn_Type.Text = "Type";
            // 
            // clmn_Time
            // 
            this.clmn_Time.Text = "Tijd";
            this.clmn_Time.Width = 120;
            // 
            // clmn_Duration
            // 
            this.clmn_Duration.Text = "Duur";
            this.clmn_Duration.Width = 80;
            // 
            // clmn_DataSize
            // 
            this.clmn_DataSize.Text = "Data grootte";
            this.clmn_DataSize.Width = 80;
            // 
            // clmn_Sender
            // 
            this.clmn_Sender.Text = "Afzender";
            this.clmn_Sender.Width = 150;
            // 
            // clmn_Receiver
            // 
            this.clmn_Receiver.Text = "Ontvanger";
            this.clmn_Receiver.Width = 150;
            // 
            // clmnTransferCount
            // 
            this.clmnTransferCount.Text = "Aantal transfers";
            this.clmnTransferCount.Width = 100;
            // 
            // HistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 361);
            this.Controls.Add(this.lv_HistoryData);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "HistoryForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Geschiedenis";
            this.Load += new System.EventHandler(this.HistoryForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lv_HistoryData;
        private System.Windows.Forms.ColumnHeader clmn_Type;
        private System.Windows.Forms.ColumnHeader clmn_Time;
        private System.Windows.Forms.ColumnHeader clmn_Duration;
        private System.Windows.Forms.ColumnHeader clmn_DataSize;
        private System.Windows.Forms.ColumnHeader clmn_Sender;
        private System.Windows.Forms.ColumnHeader clmn_Receiver;
        private System.Windows.Forms.ColumnHeader clmnTransferCount;
    }
}