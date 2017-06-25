namespace FileShare
{
    partial class FileForm
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
            this.ms_MenuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmi_File = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_SelectFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_UploadFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_ExitApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_History = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_TopToolStrip = new System.Windows.Forms.ToolStrip();
            this.tsb_BrowseFiles = new System.Windows.Forms.ToolStripButton();
            this.tsb_UploadFiles = new System.Windows.Forms.ToolStripButton();
            this.lbl_Seperator_2 = new System.Windows.Forms.Label();
            this.lbl_Seperator_1 = new System.Windows.Forms.Label();
            this.lbl_Objects = new System.Windows.Forms.Label();
            this.lbl_Seperator_3 = new System.Windows.Forms.Label();
            this.lbl_Seperator_4 = new System.Windows.Forms.Label();
            this.lbl_Seperator_5 = new System.Windows.Forms.Label();
            this.lbl_TotalFileSize = new System.Windows.Forms.Label();
            this.il_FileIconList = new System.Windows.Forms.ImageList(this.components);
            this.cms_FileListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_RemoveFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.ni_NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.cms_TrayIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_OpenFileShare = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_ExitFileShare = new System.Windows.Forms.ToolStripMenuItem();
            this.clk_Time = new FileShare.CustomControls.Clock();
            this.flv_FileList = new FileShare.CustomControls.FileListView();
            this.clmn_FileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn_FileType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn_Size = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn_FileModDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ms_MenuStrip.SuspendLayout();
            this.ts_TopToolStrip.SuspendLayout();
            this.cms_FileListMenu.SuspendLayout();
            this.cms_TrayIconMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // ms_MenuStrip
            // 
            this.ms_MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_File,
            this.tsmi_Help,
            this.tsmi_History});
            this.ms_MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.ms_MenuStrip.Name = "ms_MenuStrip";
            this.ms_MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ms_MenuStrip.Size = new System.Drawing.Size(584, 24);
            this.ms_MenuStrip.TabIndex = 0;
            this.ms_MenuStrip.Text = "menuStrip1";
            // 
            // tsmi_File
            // 
            this.tsmi_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_SelectFile,
            this.tsmi_UploadFile,
            this.toolStripSeparator1,
            this.tsmi_ExitApplication});
            this.tsmi_File.Name = "tsmi_File";
            this.tsmi_File.Size = new System.Drawing.Size(61, 20);
            this.tsmi_File.Text = "Bestand";
            // 
            // tsmi_SelectFile
            // 
            this.tsmi_SelectFile.Name = "tsmi_SelectFile";
            this.tsmi_SelectFile.Size = new System.Drawing.Size(128, 22);
            this.tsmi_SelectFile.Text = "Selecteren";
            this.tsmi_SelectFile.Click += new System.EventHandler(this.BrowseFiles_Click);
            // 
            // tsmi_UploadFile
            // 
            this.tsmi_UploadFile.Name = "tsmi_UploadFile";
            this.tsmi_UploadFile.Size = new System.Drawing.Size(128, 22);
            this.tsmi_UploadFile.Text = "Uploaden";
            this.tsmi_UploadFile.Click += new System.EventHandler(this.UploadFiles_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(125, 6);
            // 
            // tsmi_ExitApplication
            // 
            this.tsmi_ExitApplication.Name = "tsmi_ExitApplication";
            this.tsmi_ExitApplication.Size = new System.Drawing.Size(128, 22);
            this.tsmi_ExitApplication.Text = "Afsluiten";
            this.tsmi_ExitApplication.Click += new System.EventHandler(this.ExitApplication_Click);
            // 
            // tsmi_Help
            // 
            this.tsmi_Help.Name = "tsmi_Help";
            this.tsmi_Help.Size = new System.Drawing.Size(44, 20);
            this.tsmi_Help.Text = "Help";
            this.tsmi_Help.Click += new System.EventHandler(this.HelpItem_Click);
            // 
            // tsmi_History
            // 
            this.tsmi_History.Name = "tsmi_History";
            this.tsmi_History.Size = new System.Drawing.Size(88, 20);
            this.tsmi_History.Text = "Geschiedenis";
            this.tsmi_History.Click += new System.EventHandler(this.HistoryItem_Click);
            // 
            // ts_TopToolStrip
            // 
            this.ts_TopToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsb_BrowseFiles,
            this.tsb_UploadFiles});
            this.ts_TopToolStrip.Location = new System.Drawing.Point(0, 24);
            this.ts_TopToolStrip.Name = "ts_TopToolStrip";
            this.ts_TopToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.ts_TopToolStrip.Size = new System.Drawing.Size(584, 55);
            this.ts_TopToolStrip.TabIndex = 1;
            this.ts_TopToolStrip.Text = "toolStrip1";
            // 
            // tsb_BrowseFiles
            // 
            this.tsb_BrowseFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_BrowseFiles.Image = global::FileShare.Properties.Resources.Files_48;
            this.tsb_BrowseFiles.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_BrowseFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_BrowseFiles.Name = "tsb_BrowseFiles";
            this.tsb_BrowseFiles.Size = new System.Drawing.Size(52, 52);
            this.tsb_BrowseFiles.Text = "Selecteer bestanden.";
            this.tsb_BrowseFiles.Click += new System.EventHandler(this.BrowseFiles_Click);
            // 
            // tsb_UploadFiles
            // 
            this.tsb_UploadFiles.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_UploadFiles.Image = global::FileShare.Properties.Resources.Upload_48;
            this.tsb_UploadFiles.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsb_UploadFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_UploadFiles.Name = "tsb_UploadFiles";
            this.tsb_UploadFiles.Size = new System.Drawing.Size(52, 52);
            this.tsb_UploadFiles.Text = "Verzend bestanden.";
            this.tsb_UploadFiles.Click += new System.EventHandler(this.UploadFiles_Click);
            // 
            // lbl_Seperator_2
            // 
            this.lbl_Seperator_2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Seperator_2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Seperator_2.Location = new System.Drawing.Point(0, 77);
            this.lbl_Seperator_2.Name = "lbl_Seperator_2";
            this.lbl_Seperator_2.Size = new System.Drawing.Size(600, 2);
            this.lbl_Seperator_2.TabIndex = 3;
            // 
            // lbl_Seperator_1
            // 
            this.lbl_Seperator_1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Seperator_1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Seperator_1.Location = new System.Drawing.Point(0, 22);
            this.lbl_Seperator_1.Name = "lbl_Seperator_1";
            this.lbl_Seperator_1.Size = new System.Drawing.Size(600, 2);
            this.lbl_Seperator_1.TabIndex = 4;
            // 
            // lbl_Objects
            // 
            this.lbl_Objects.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Objects.AutoSize = true;
            this.lbl_Objects.Location = new System.Drawing.Point(12, 339);
            this.lbl_Objects.Name = "lbl_Objects";
            this.lbl_Objects.Size = new System.Drawing.Size(70, 13);
            this.lbl_Objects.TabIndex = 6;
            this.lbl_Objects.Text = "Bestanden: 0";
            // 
            // lbl_Seperator_3
            // 
            this.lbl_Seperator_3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Seperator_3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Seperator_3.Location = new System.Drawing.Point(0, 330);
            this.lbl_Seperator_3.Name = "lbl_Seperator_3";
            this.lbl_Seperator_3.Size = new System.Drawing.Size(600, 2);
            this.lbl_Seperator_3.TabIndex = 7;
            // 
            // lbl_Seperator_4
            // 
            this.lbl_Seperator_4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Seperator_4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Seperator_4.Location = new System.Drawing.Point(110, 330);
            this.lbl_Seperator_4.Name = "lbl_Seperator_4";
            this.lbl_Seperator_4.Size = new System.Drawing.Size(2, 40);
            this.lbl_Seperator_4.TabIndex = 8;
            // 
            // lbl_Seperator_5
            // 
            this.lbl_Seperator_5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_Seperator_5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_Seperator_5.Location = new System.Drawing.Point(204, 330);
            this.lbl_Seperator_5.Name = "lbl_Seperator_5";
            this.lbl_Seperator_5.Size = new System.Drawing.Size(2, 40);
            this.lbl_Seperator_5.TabIndex = 9;
            // 
            // lbl_TotalFileSize
            // 
            this.lbl_TotalFileSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_TotalFileSize.AutoSize = true;
            this.lbl_TotalFileSize.Location = new System.Drawing.Point(118, 339);
            this.lbl_TotalFileSize.Name = "lbl_TotalFileSize";
            this.lbl_TotalFileSize.Size = new System.Drawing.Size(38, 13);
            this.lbl_TotalFileSize.TabIndex = 10;
            this.lbl_TotalFileSize.Text = "0,00 B";
            // 
            // il_FileIconList
            // 
            this.il_FileIconList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.il_FileIconList.ImageSize = new System.Drawing.Size(16, 16);
            this.il_FileIconList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // cms_FileListMenu
            // 
            this.cms_FileListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_RemoveFiles});
            this.cms_FileListMenu.Name = "cms_FileListMenu";
            this.cms_FileListMenu.Size = new System.Drawing.Size(196, 26);
            this.cms_FileListMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.FileListMenu_ItemClicked);
            // 
            // tsmi_RemoveFiles
            // 
            this.tsmi_RemoveFiles.Name = "tsmi_RemoveFiles";
            this.tsmi_RemoveFiles.Size = new System.Drawing.Size(195, 22);
            this.tsmi_RemoveFiles.Text = "Remove selected file(s)";
            // 
            // ni_NotifyIcon
            // 
            this.ni_NotifyIcon.ContextMenuStrip = this.cms_TrayIconMenu;
            this.ni_NotifyIcon.Text = "FileShare";
            this.ni_NotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.NotifyIcon_MouseClick);
            // 
            // cms_TrayIconMenu
            // 
            this.cms_TrayIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_OpenFileShare,
            this.tsmi_ExitFileShare});
            this.cms_TrayIconMenu.Name = "cms_TrayIconMenu";
            this.cms_TrayIconMenu.Size = new System.Drawing.Size(122, 48);
            // 
            // tsmi_OpenFileShare
            // 
            this.tsmi_OpenFileShare.Name = "tsmi_OpenFileShare";
            this.tsmi_OpenFileShare.Size = new System.Drawing.Size(121, 22);
            this.tsmi_OpenFileShare.Text = "Open";
            this.tsmi_OpenFileShare.Click += new System.EventHandler(this.OpenFileShare_Click);
            // 
            // tsmi_ExitFileShare
            // 
            this.tsmi_ExitFileShare.Name = "tsmi_ExitFileShare";
            this.tsmi_ExitFileShare.Size = new System.Drawing.Size(121, 22);
            this.tsmi_ExitFileShare.Text = "Afsluiten";
            this.tsmi_ExitFileShare.Click += new System.EventHandler(this.ExitFileShare_Click);
            // 
            // clk_Time
            // 
            this.clk_Time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clk_Time.AutoSize = true;
            this.clk_Time.Location = new System.Drawing.Point(533, 339);
            this.clk_Time.Name = "clk_Time";
            this.clk_Time.Size = new System.Drawing.Size(34, 13);
            this.clk_Time.TabIndex = 13;
            this.clk_Time.Text = "11:04";
            // 
            // flv_FileList
            // 
            this.flv_FileList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flv_FileList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.flv_FileList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmn_FileName,
            this.clmn_FileType,
            this.clmn_Size,
            this.clmn_FileModDate});
            this.flv_FileList.FullRowSelect = true;
            this.flv_FileList.Location = new System.Drawing.Point(0, 77);
            this.flv_FileList.Name = "flv_FileList";
            this.flv_FileList.Size = new System.Drawing.Size(584, 255);
            this.flv_FileList.SmallImageList = this.il_FileIconList;
            this.flv_FileList.TabIndex = 12;
            this.flv_FileList.UseCompatibleStateImageBehavior = false;
            this.flv_FileList.View = System.Windows.Forms.View.Details;
            this.flv_FileList.FileListChanged += new FileShare.CustomControls.FileListView.FileListChangedEventHandler(this.FileListChanged);
            this.flv_FileList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FileList_MouseClick);
            // 
            // clmn_FileName
            // 
            this.clmn_FileName.Text = "Naam";
            this.clmn_FileName.Width = 120;
            // 
            // clmn_FileType
            // 
            this.clmn_FileType.Text = "Type";
            // 
            // clmn_Size
            // 
            this.clmn_Size.Text = "Grootte";
            this.clmn_Size.Width = 120;
            // 
            // clmn_FileModDate
            // 
            this.clmn_FileModDate.Text = "Gewijzigd op";
            this.clmn_FileModDate.Width = 160;
            // 
            // FileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.clk_Time);
            this.Controls.Add(this.lbl_TotalFileSize);
            this.Controls.Add(this.lbl_Seperator_3);
            this.Controls.Add(this.lbl_Objects);
            this.Controls.Add(this.lbl_Seperator_1);
            this.Controls.Add(this.lbl_Seperator_2);
            this.Controls.Add(this.ts_TopToolStrip);
            this.Controls.Add(this.ms_MenuStrip);
            this.Controls.Add(this.lbl_Seperator_4);
            this.Controls.Add(this.lbl_Seperator_5);
            this.Controls.Add(this.flv_FileList);
            this.MainMenuStrip = this.ms_MenuStrip;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "FileForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bestanden selecteren";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FileForm_FormClosing);
            this.ms_MenuStrip.ResumeLayout(false);
            this.ms_MenuStrip.PerformLayout();
            this.ts_TopToolStrip.ResumeLayout(false);
            this.ts_TopToolStrip.PerformLayout();
            this.cms_FileListMenu.ResumeLayout(false);
            this.cms_TrayIconMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip ms_MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem tsmi_File;
        private System.Windows.Forms.ToolStripMenuItem tsmi_Help;
        private System.Windows.Forms.ToolStripMenuItem tsmi_History;
        private System.Windows.Forms.ToolStrip ts_TopToolStrip;
        private System.Windows.Forms.ToolStripButton tsb_BrowseFiles;
        private System.Windows.Forms.Label lbl_Seperator_2;
        private System.Windows.Forms.Label lbl_Seperator_1;
        private System.Windows.Forms.ToolStripButton tsb_UploadFiles;
        private System.Windows.Forms.Label lbl_Objects;
        private System.Windows.Forms.Label lbl_Seperator_3;
        private System.Windows.Forms.Label lbl_Seperator_4;
        private System.Windows.Forms.Label lbl_Seperator_5;
        private System.Windows.Forms.Label lbl_TotalFileSize;
        private System.Windows.Forms.ToolStripMenuItem tsmi_SelectFile;
        private System.Windows.Forms.ToolStripMenuItem tsmi_UploadFile;
        private System.Windows.Forms.ImageList il_FileIconList;
        private CustomControls.FileListView flv_FileList;
        private System.Windows.Forms.ColumnHeader clmn_FileName;
        private System.Windows.Forms.ColumnHeader clmn_FileType;
        private System.Windows.Forms.ColumnHeader clmn_Size;
        private System.Windows.Forms.ColumnHeader clmn_FileModDate;
        private System.Windows.Forms.ContextMenuStrip cms_FileListMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_RemoveFiles;
        private CustomControls.Clock clk_Time;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ExitApplication;
        private System.Windows.Forms.NotifyIcon ni_NotifyIcon;
        private System.Windows.Forms.ContextMenuStrip cms_TrayIconMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_OpenFileShare;
        private System.Windows.Forms.ToolStripMenuItem tsmi_ExitFileShare;
    }
}

