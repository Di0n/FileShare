namespace FileShare
{
    partial class ZipForm
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
            this.lbl_ZipText = new System.Windows.Forms.Label();
            this.prb_ZipProgress = new System.Windows.Forms.ProgressBar();
            this.btn_CancelZip = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_ZipText
            // 
            this.lbl_ZipText.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ZipText.Location = new System.Drawing.Point(12, 9);
            this.lbl_ZipText.Name = "lbl_ZipText";
            this.lbl_ZipText.Size = new System.Drawing.Size(360, 23);
            this.lbl_ZipText.TabIndex = 0;
            this.lbl_ZipText.Text = "Bezig met bestanden te zippen...";
            this.lbl_ZipText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // prb_ZipProgress
            // 
            this.prb_ZipProgress.Location = new System.Drawing.Point(12, 54);
            this.prb_ZipProgress.Name = "prb_ZipProgress";
            this.prb_ZipProgress.Size = new System.Drawing.Size(360, 30);
            this.prb_ZipProgress.TabIndex = 1;
            // 
            // btn_CancelZip
            // 
            this.btn_CancelZip.Location = new System.Drawing.Point(297, 119);
            this.btn_CancelZip.Name = "btn_CancelZip";
            this.btn_CancelZip.Size = new System.Drawing.Size(75, 30);
            this.btn_CancelZip.TabIndex = 2;
            this.btn_CancelZip.Text = "Annuleren";
            this.btn_CancelZip.UseVisualStyleBackColor = true;
            this.btn_CancelZip.Click += new System.EventHandler(this.CancelZip_Click);
            // 
            // ZipForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 161);
            this.Controls.Add(this.btn_CancelZip);
            this.Controls.Add(this.prb_ZipProgress);
            this.Controls.Add(this.lbl_ZipText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "ZipForm";
            this.Tag = "";
            this.Text = "Bezig met zippen...";
            this.Shown += new System.EventHandler(this.ZipForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_ZipText;
        private System.Windows.Forms.ProgressBar prb_ZipProgress;
        private System.Windows.Forms.Button btn_CancelZip;
    }
}