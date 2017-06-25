using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileShare
{
    public partial class ReceiverForm : Form
    {
        private NetworkComputersForm computersForm;
        private FormConnector con;

        public ReceiverForm()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel; // Set standard Dialogresult to cancel.
        }

        private void AddReceiver(Computer receiver)
        {
            if (!clv_ReceiverList.Items.ContainsKey(receiver.IP))
            {
                
                ListViewItem item = new ListViewItem(receiver.IP + ':' + receiver.Port);
                item.Name = receiver.IP;
                item.SubItems.Add(receiver.Name);
                item.Tag = receiver;

                clv_ReceiverList.BeginUpdate();
                clv_ReceiverList.Items.Add(item);
                clv_ReceiverList.EndUpdate();
            }
        }

        private void NetworkComputerAddRequest(object source, NetworkComputerEventArgs e)
        {
            AddReceiver(e.Data);
        }

        private void Send_Click(object sender, EventArgs e)
        {
            if (clv_ReceiverList.Items.Count == 0)
            {
                MessageBox.Show("Voeg tenminste één ontvanger toe.");
                return;
            }
            else
            {
                DialogResult result = MessageBox.Show(this, "Weet u zeker dat u de bestanden wilt versturen?", "Verstuur bestanden?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    Receivers = clv_ReceiverList.GetAllComputers().ToList();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            
        }

        private void ReceiverList_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (clv_ReceiverList.FocusedItem.Bounds.Contains(e.Location))
                    cms_ReceiverMenu.Show(clv_ReceiverList, e.Location);
            }
        }

        private void ReceiverForm_Shown(object sender, EventArgs e)
        {
            computersForm.Show(this);
            this.Focus();
        }

        private void ReceiverForm_Load(object sender, EventArgs e)
        {
            con = new FormConnector(this);
            computersForm = new NetworkComputersForm();
            computersForm.NetworkComputerAddRequest += NetworkComputerAddRequest;
            con.ConnectForm(computersForm);
            this.Location = this.Owner.Location;
        }

        public List<Computer> Receivers { get; private set; }

        private async void Add_Click(object sender, EventArgs e)
        {
            IPAddress ipAddress;
            string text = tb_AddressBox.Text;
            if (IPAddress.TryParse(text, out ipAddress))
            {
                bool isLocalIP = false;
                try
                {
                    isLocalIP = await Utility.IsLocalAddressAsync(ipAddress);
                }
                catch (SocketException) 
                {
                    MessageBox.Show("Er ging iets fout.\nCheck uw internet verbinding en probeer later opnieuw.", "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (isLocalIP)
                {
                    MessageBox.Show("Ongeldig IP adres.", "Ongeldig IP adres", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                AddReceiver(new Computer() { IP = ipAddress.ToString(), Name = "" });
                tb_AddressBox.Clear();
            }
            else
            {
                MessageBox.Show("Ongeldig IP adres.", "Ongeldig IP adres", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ReceiverList_RemoveSelectedItems(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == tsmi_RemoveComputer)
            {
                clv_ReceiverList.BeginUpdate();
                for (int i = 0; i < clv_ReceiverList.SelectedItems.Count; i++)
                {
                    clv_ReceiverList.Items.Remove(clv_ReceiverList.SelectedItems[i]);
                }
                clv_ReceiverList.EndUpdate();
            }
        }
    }
}