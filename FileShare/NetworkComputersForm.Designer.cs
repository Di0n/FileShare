namespace FileShare
{
    partial class NetworkComputersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkComputersForm));
            this.pb_RefreshAnimation = new System.Windows.Forms.PictureBox();
            this.pb_Refresh = new System.Windows.Forms.PictureBox();
            this.pb_Add = new System.Windows.Forms.PictureBox();
            this.lv_NetworkComputers = new FileShare.CustomControls.CustomListView();
            this.clmn_PCName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.pb_RefreshAnimation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Refresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Add)).BeginInit();
            this.SuspendLayout();
            // 
            // pb_RefreshAnimation
            // 
            this.pb_RefreshAnimation.BackColor = System.Drawing.Color.Transparent;
            this.pb_RefreshAnimation.Image = global::FileShare.Properties.Resources.Refresh_Animation_46;
            this.pb_RefreshAnimation.Location = new System.Drawing.Point(65, 115);
            this.pb_RefreshAnimation.Name = "pb_RefreshAnimation";
            this.pb_RefreshAnimation.Size = new System.Drawing.Size(46, 46);
            this.pb_RefreshAnimation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pb_RefreshAnimation.TabIndex = 4;
            this.pb_RefreshAnimation.TabStop = false;
            this.pb_RefreshAnimation.Visible = false;
            // 
            // pb_Refresh
            // 
            this.pb_Refresh.Image = global::FileShare.Properties.Resources.Command_Refresh_32;
            this.pb_Refresh.Location = new System.Drawing.Point(142, 269);
            this.pb_Refresh.Name = "pb_Refresh";
            this.pb_Refresh.Size = new System.Drawing.Size(30, 30);
            this.pb_Refresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pb_Refresh.TabIndex = 2;
            this.pb_Refresh.TabStop = false;
            this.pb_Refresh.Click += new System.EventHandler(this.RefreshBtn_Click);
            // 
            // pb_Add
            // 
            this.pb_Add.Image = global::FileShare.Properties.Resources.Add_New_32;
            this.pb_Add.Location = new System.Drawing.Point(12, 269);
            this.pb_Add.Name = "pb_Add";
            this.pb_Add.Size = new System.Drawing.Size(30, 30);
            this.pb_Add.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pb_Add.TabIndex = 1;
            this.pb_Add.TabStop = false;
            this.pb_Add.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // lv_NetworkComputers
            // 
            this.lv_NetworkComputers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmn_PCName});
            this.lv_NetworkComputers.Location = new System.Drawing.Point(12, 12);
            this.lv_NetworkComputers.MultiSelect = false;
            this.lv_NetworkComputers.Name = "lv_NetworkComputers";
            this.lv_NetworkComputers.Size = new System.Drawing.Size(160, 251);
            this.lv_NetworkComputers.TabIndex = 3;
            this.lv_NetworkComputers.UseCompatibleStateImageBehavior = false;
            this.lv_NetworkComputers.View = System.Windows.Forms.View.Details;
            this.lv_NetworkComputers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.NetworkComputersLv_DoubleClicked);
            // 
            // clmn_PCName
            // 
            this.clmn_PCName.Text = "PC Naam";
            this.clmn_PCName.Width = 120;
            // 
            // NetworkComputersForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(184, 311);
            this.ControlBox = false;
            this.Controls.Add(this.pb_RefreshAnimation);
            this.Controls.Add(this.lv_NetworkComputers);
            this.Controls.Add(this.pb_Refresh);
            this.Controls.Add(this.pb_Add);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NetworkComputersForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Tag = "FormExtension";
            this.Text = "NetwerkPC\'s";
            this.Load += new System.EventHandler(this.NetworkComputersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pb_RefreshAnimation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Refresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pb_Add)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pb_Add;
        private System.Windows.Forms.PictureBox pb_Refresh;
        private CustomControls.CustomListView lv_NetworkComputers;
        private System.Windows.Forms.ColumnHeader clmn_PCName;
        private System.Windows.Forms.PictureBox pb_RefreshAnimation;
    }
}