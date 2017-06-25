namespace FileShare
{
    partial class RequestForm
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
            this.components = new System.ComponentModel.Container();
            this.lbl_Request = new System.Windows.Forms.Label();
            this.lbl_RequestCountdown = new System.Windows.Forms.Label();
            this.btn_No = new System.Windows.Forms.Button();
            this.pb_QuestionMark = new System.Windows.Forms.PictureBox();
            this.lbl_TotalSize = new System.Windows.Forms.Label();
            this.lbl_MenuBackground = new System.Windows.Forms.Label();
            this.lbl_Background = new System.Windows.Forms.Label();
            this.btn_Yes = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pb_QuestionMark)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Request
            // 
            this.lbl_Request.AutoSize = true;
            this.lbl_Request.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_Request.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Request.Location = new System.Drawing.Point(63, 16);
            this.lbl_Request.Name = "lbl_Request";
            this.lbl_Request.Size = new System.Drawing.Size(285, 13);
            this.lbl_Request.TabIndex = 0;
            this.lbl_Request.Text = "Naam-PC @ 127.0.0.1 wil een bestandsoverdracht starten.";
            // 
            // lbl_RequestCountdown
            // 
            this.lbl_RequestCountdown.AutoSize = true;
            this.lbl_RequestCountdown.BackColor = System.Drawing.SystemColors.MenuBar;
            this.lbl_RequestCountdown.Location = new System.Drawing.Point(9, 111);
            this.lbl_RequestCountdown.Name = "lbl_RequestCountdown";
            this.lbl_RequestCountdown.Size = new System.Drawing.Size(19, 13);
            this.lbl_RequestCountdown.TabIndex = 2;
            this.lbl_RequestCountdown.Text = "10";
            // 
            // btn_No
            // 
            this.btn_No.DialogResult = System.Windows.Forms.DialogResult.No;
            this.btn_No.Location = new System.Drawing.Point(332, 96);
            this.btn_No.Name = "btn_No";
            this.btn_No.Size = new System.Drawing.Size(90, 28);
            this.btn_No.TabIndex = 3;
            this.btn_No.Text = "Nee";
            this.btn_No.UseVisualStyleBackColor = true;
            // 
            // pb_QuestionMark
            // 
            this.pb_QuestionMark.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pb_QuestionMark.Location = new System.Drawing.Point(25, 16);
            this.pb_QuestionMark.Name = "pb_QuestionMark";
            this.pb_QuestionMark.Size = new System.Drawing.Size(32, 32);
            this.pb_QuestionMark.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pb_QuestionMark.TabIndex = 5;
            this.pb_QuestionMark.TabStop = false;
            // 
            // lbl_TotalSize
            // 
            this.lbl_TotalSize.AutoSize = true;
            this.lbl_TotalSize.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_TotalSize.Location = new System.Drawing.Point(63, 35);
            this.lbl_TotalSize.Name = "lbl_TotalSize";
            this.lbl_TotalSize.Size = new System.Drawing.Size(102, 13);
            this.lbl_TotalSize.TabIndex = 6;
            this.lbl_TotalSize.Text = "Totale grootte: 0 KB";
            // 
            // lbl_MenuBackground
            // 
            this.lbl_MenuBackground.BackColor = System.Drawing.SystemColors.MenuBar;
            this.lbl_MenuBackground.Location = new System.Drawing.Point(-2, 85);
            this.lbl_MenuBackground.Name = "lbl_MenuBackground";
            this.lbl_MenuBackground.Size = new System.Drawing.Size(440, 50);
            this.lbl_MenuBackground.TabIndex = 7;
            // 
            // lbl_Background
            // 
            this.lbl_Background.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.lbl_Background.Location = new System.Drawing.Point(1, -2);
            this.lbl_Background.Name = "lbl_Background";
            this.lbl_Background.Size = new System.Drawing.Size(437, 94);
            this.lbl_Background.TabIndex = 8;
            // 
            // btn_Yes
            // 
            this.btn_Yes.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.btn_Yes.Location = new System.Drawing.Point(236, 96);
            this.btn_Yes.Name = "btn_Yes";
            this.btn_Yes.Size = new System.Drawing.Size(90, 28);
            this.btn_Yes.TabIndex = 9;
            this.btn_Yes.Text = "Ja";
            this.btn_Yes.UseVisualStyleBackColor = true;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // RequestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 136);
            this.Controls.Add(this.btn_Yes);
            this.Controls.Add(this.lbl_TotalSize);
            this.Controls.Add(this.pb_QuestionMark);
            this.Controls.Add(this.btn_No);
            this.Controls.Add(this.lbl_RequestCountdown);
            this.Controls.Add(this.lbl_Request);
            this.Controls.Add(this.lbl_MenuBackground);
            this.Controls.Add(this.lbl_Background);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RequestForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bestandsoverdracht aanvraag";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RequestForm_Closing);
            ((System.ComponentModel.ISupportInitialize)(this.pb_QuestionMark)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Request;
        private System.Windows.Forms.Label lbl_RequestCountdown;
        private System.Windows.Forms.Button btn_No;
        private System.Windows.Forms.PictureBox pb_QuestionMark;
        private System.Windows.Forms.Label lbl_TotalSize;
        private System.Windows.Forms.Label lbl_MenuBackground;
        private System.Windows.Forms.Label lbl_Background;
        private System.Windows.Forms.Button btn_Yes;
        private System.Windows.Forms.Timer timer;
    }
}