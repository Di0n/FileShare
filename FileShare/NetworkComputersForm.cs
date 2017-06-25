using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileShare
{
    public partial class NetworkComputersForm : Form
    {
        public delegate void NetworkComputerAddRequestEventHandler(object source, NetworkComputerEventArgs e);
        public event NetworkComputerAddRequestEventHandler NetworkComputerAddRequest;

        public NetworkComputersForm()
        {
            InitializeComponent();
            pb_RefreshAnimation.Parent = lv_NetworkComputers;

            int x = ((pb_RefreshAnimation.Parent.ClientSize.Width / 2) - (pb_RefreshAnimation.Width / 2));
            int y = ((pb_RefreshAnimation.Parent.ClientSize.Height / 2) - (pb_RefreshAnimation.Height / 2));

            pb_RefreshAnimation.Location = new Point(x, y);
        }

        private void AddComputersToList(List<Computer> computers)
        {
            lv_NetworkComputers.BeginUpdate();
            computers.ForEach(c =>
            {
                if (!lv_NetworkComputers.Items.ContainsKey(c.IP))
                {
                    ListViewItem item = new ListViewItem(c.Name);
                    item.Name = c.IP;
                    item.Tag = c;
                    lv_NetworkComputers.Items.Add(item);
                }
            });
            lv_NetworkComputers.EndUpdate();
        }

        private Task<List<Computer>> GetLocalComputers()
        {
            return Task.Run<List<Computer>>(async () =>
                {
                    List<Computer> comp = new List<Computer>();
                    string ownIP = await Utility.GetLocalIPAddressAsync();

                    using (DirectoryEntry root = new DirectoryEntry("WinNT:"))
                        foreach (DirectoryEntry computers in root.Children)
                            foreach (DirectoryEntry computer in computers.Children)
                            {
                                if (this.IsDisposed) return null;
                                if (computer.Name != "Schema" && computer.SchemaClassName == "Computer")
                                {
                                    try
                                    {
                                        string ip = await ResolveHostnameToAddress(computer.Name);

                                        if (ip != ownIP)
                                            comp.Add(new Computer() { Name = computer.Name, IP = ip }); // Exclude own pc
                                    }
                                    catch (ResolveIPException)
                                    {
                                        // try again
                                        continue;
                                    }
                                    catch (SocketException sx)
                                    {
                                        if (sx.SocketErrorCode == SocketError.HostNotFound)
                                        {
                                            continue;
                                        }
                                    }
                                }
                            }
                    return comp;
                });
        }

        /// <summary>
        /// Resolves the hostname into a local ip address
        /// </summary>
        /// <param name="hostName"></param>
        /// <returns></returns>
        /// <exception cref="ResolveIPException">Thrown when there was an error resolving.</exception>
        async Task<string> ResolveHostnameToAddress(string hostName)
        {
            IPAddress[] ips = await Dns.GetHostAddressesAsync(hostName);
            for (int i = 0; i < ips.Length; i++)
            {
                if (ips[i].AddressFamily == AddressFamily.InterNetwork)
                {
                    return ips[i].ToString();
                }
            }
            throw new ResolveIPException("Failed to resolve host: " + hostName);
        }

        // *** EVENTS *** \\\
        private async void NetworkComputersForm_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.IsWindows10)
                this.Location = new Point(this.Owner.Location.X + this.Owner.Size.Width - 15, this.Owner.Location.Y);
            else
                this.Location = new Point(this.Owner.Location.X + this.Owner.Size.Width, this.Owner.Location.Y);

            ToggleRefreshAnimation = true;
            List<Computer> computers = await GetLocalComputers();
            ToggleRefreshAnimation = false;

            if (computers != null) AddComputersToList(computers);
        }

        private void NetworkComputersLv_DoubleClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (lv_NetworkComputers.SelectedItems.Count == 1)
                {
                    OnNetworkComputerAddRequest((lv_NetworkComputers.SelectedItems[0].Tag as Computer));
                }
            }
        }

        private async void RefreshBtn_Click(object sender, EventArgs e)
        {
            ToggleRefreshAnimation = true;
            List<Computer> computers = await GetLocalComputers();
            ToggleRefreshAnimation = false;

            if (computers != null) AddComputersToList(computers);
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            if (lv_NetworkComputers.SelectedItems.Count == 1)
            {
                OnNetworkComputerAddRequest(lv_NetworkComputers.SelectedItems[0].Tag as Computer);
            }
        }

        private void OnNetworkComputerAddRequest(Computer comp)
        {
            if (NetworkComputerAddRequest != null) NetworkComputerAddRequest(this, new NetworkComputerEventArgs(comp));
        }

        private bool toggleRefreshAnimationStatus;
        private bool ToggleRefreshAnimation
        {
            get { return toggleRefreshAnimationStatus; }
            set
            {
                if (!this.IsDisposed)
                {
                    pb_RefreshAnimation.Visible = value;
                    toggleRefreshAnimationStatus = value;
                }
            }
        }
    }
}
