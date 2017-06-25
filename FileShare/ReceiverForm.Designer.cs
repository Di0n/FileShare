namespace FileShare
{
    partial class ReceiverForm
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
            this.tb_AddressBox = new System.Windows.Forms.TextBox();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Send = new System.Windows.Forms.Button();
            this.cms_ReceiverMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_RemoveComputer = new System.Windows.Forms.ToolStripMenuItem();
            this.clv_ReceiverList = new FileShare.CustomControls.ComputerListView();
            this.clmn_SocketAddress = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmn_PCName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cms_ReceiverMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // tb_AddressBox
            // 
            this.tb_AddressBox.Location = new System.Drawing.Point(12, 239);
            this.tb_AddressBox.Name = "tb_AddressBox";
            this.tb_AddressBox.Size = new System.Drawing.Size(279, 20);
            this.tb_AddressBox.TabIndex = 1;
            // 
            // btn_Add
            // 
            this.btn_Add.Location = new System.Drawing.Point(297, 237);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(75, 23);
            this.btn_Add.TabIndex = 2;
            this.btn_Add.Text = "Voeg toe";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // btn_Send
            // 
            this.btn_Send.Location = new System.Drawing.Point(12, 265);
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Size = new System.Drawing.Size(360, 34);
            this.btn_Send.TabIndex = 3;
            this.btn_Send.Text = "Verstuur bestanden";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.Send_Click);
            // 
            // cms_ReceiverMenu
            // 
            this.cms_ReceiverMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_RemoveComputer});
            this.cms_ReceiverMenu.Name = "cms_ReceiverMenu";
            this.cms_ReceiverMenu.Size = new System.Drawing.Size(235, 26);
            this.cms_ReceiverMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.ReceiverList_RemoveSelectedItems);
            // 
            // tsmi_RemoveComputer
            // 
            this.tsmi_RemoveComputer.Name = "tsmi_RemoveComputer";
            this.tsmi_RemoveComputer.Size = new System.Drawing.Size(234, 22);
            this.tsmi_RemoveComputer.Text = "Remove selected computer(s).";
            // 
            // clv_ReceiverList
            // 
            this.clv_ReceiverList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmn_SocketAddress,
            this.clmn_PCName});
            this.clv_ReceiverList.FullRowSelect = true;
            this.clv_ReceiverList.Location = new System.Drawing.Point(12, 12);
            this.clv_ReceiverList.Name = "clv_ReceiverList";
            this.clv_ReceiverList.Size = new System.Drawing.Size(360, 219);
            this.clv_ReceiverList.TabIndex = 5;
            this.clv_ReceiverList.UseCompatibleStateImageBehavior = false;
            this.clv_ReceiverList.View = System.Windows.Forms.View.Details;
            this.clv_ReceiverList.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ReceiverList_MouseClick);
            // 
            // clmn_SocketAddress
            // 
            this.clmn_SocketAddress.Text = "Socket Adres";
            this.clmn_SocketAddress.Width = 160;
            // 
            // clmn_PCName
            // 
            this.clmn_PCName.Text = "Naam";
            this.clmn_PCName.Width = 120;
            // 
            // ReceiverForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(384, 311);
            this.Controls.Add(this.clv_ReceiverList);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.btn_Add);
            this.Controls.Add(this.tb_AddressBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ReceiverForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Tag = "";
            this.Text = "Versturen";
            this.Load += new System.EventHandler(this.ReceiverForm_Load);
            this.Shown += new System.EventHandler(this.ReceiverForm_Shown);
            this.cms_ReceiverMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tb_AddressBox;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Send;
        private CustomControls.ComputerListView clv_ReceiverList;
        private System.Windows.Forms.ColumnHeader clmn_SocketAddress;
        private System.Windows.Forms.ColumnHeader clmn_PCName;
        private System.Windows.Forms.ContextMenuStrip cms_ReceiverMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmi_RemoveComputer;
    }
}