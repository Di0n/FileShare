using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SubItem = System.Windows.Forms.ListViewItem.ListViewSubItem;

namespace FileShare
{
    public partial class SendForm : Form
    {
        private readonly File file;
        private readonly List<Computer> receivers;
        private ListViewItem currentReceiver;
        private bool cancelAllTransfers;
        private CancellationTokenSource cancelTokenSource;

        public SendForm(File file, List<Computer> receivers)
        {
            this.file = file;
            this.receivers = receivers;
            InitializeComponent();
        }

        /// <summary>
        /// Cancels all ongoing transfers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelTransfersBtn_Click(object sender, EventArgs e)
        {
            cancelAllTransfers = true;
            if (cancelTokenSource != null)
            {
                try { cancelTokenSource.Cancel(); }
                catch (ObjectDisposedException) { }
            }
        }

        /// <summary>
        /// Gets called when the form loads, adds the receivers to the list.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendForm_Load(object sender, EventArgs e)
        {
            this.Location = this.Owner.Location;
            slv_Receivers.AddReceivers(receivers);
        }

        /// <summary>
        /// Gets called when the form gets shown, starts the send operations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref=""></exception>
        private async void SendForm_Shown(object sender, EventArgs e)
        {
            Queue<ListViewItem> receiverQueue = new Queue<ListViewItem>(slv_Receivers.GetAllListViewItems());
            while (receiverQueue.Count > 0 && !cancelAllTransfers)
            {
                using (FileSender fileSender = new FileSender())
                using (cancelTokenSource = new CancellationTokenSource())
                {
                    fileSender.FileSendProgress += FileSender_SendProgress;

                    CancellationToken cancelToken = cancelTokenSource.Token;

                    lbl_Remaining.Text = String.Format("Resterend: {0}", receiverQueue.Count);

                    currentReceiver = receiverQueue.Dequeue();
                    Computer recv = (Computer)currentReceiver.Tag;

                    SubItem status = currentReceiver.SubItems[currentReceiver.SubItems.IndexOfKey("clmn_Status")];

                    status.Text = "Verbinden";

                    try { await fileSender.ConnectAsync(recv, cancelToken); }
                    catch (SocketException)
                    {
                        currentReceiver.BackColor = Color.Red;
                        status.Text = "Verbinden mislukt";
                        continue;
                    }
                    catch (TaskCanceledException)
                    {
                        currentReceiver.BackColor = Color.Yellow;
                        status.Text = "Verbinden afgebroken";
                        continue;
                    }

                    status.Text = "Wachten op antwoord";

                    FileSender.RequestResponse requestResponse = null;

                    Task<FileSender.RequestResponse> requestFileSend = fileSender.RequestFileSendAsync(file, cancelToken);
                    try
                    {
                        requestResponse = await requestFileSend;
                    }
                    catch (SocketException)
                    {
                        currentReceiver.BackColor = Color.Red;
                        status.Text = "Geen antwoord";
                    }
                    catch (TaskCanceledException)
                    {
                        currentReceiver.BackColor = Color.Yellow;
                        status.Text = "Aanvraag afgebroken";
                    }

                    if (requestFileSend.Exception != null)
                    {
                        continue;
                    }

                    if (!requestResponse.RequestAccepted)
                    {
                        currentReceiver.BackColor = Color.Gray;
                        status.Text = "Verzoek afgewezen";
                        continue;
                    }

                    currentReceiver.BackColor = Color.LightBlue;
                    status.Text = "Gestart";

                    Stopwatch transferTimer = new Stopwatch();

                    transferTimer.Start();

                    Task sendFile = fileSender.SendFileAsync(file, cancelToken);
                    try
                    {
                        await sendFile;
                        transferTimer.Stop();
                    }
                    catch (SocketException)
                    {
                        status.Text = "Verzenden mislukt";
                        currentReceiver.BackColor = Color.Red;
                    }
                    catch (System.IO.IOException ex)
                    {
                        SocketException sx = ex.InnerException as SocketException;
                        if (sx != null)
                            if (sx.SocketErrorCode == SocketError.ConnectionReset)
                            {
                                status.Text = "Geannuleerd";
                                currentReceiver.BackColor = Color.Yellow;
                                continue;
                            }
                        status.Text = "Verzenden mislukt";
                        currentReceiver.BackColor = Color.Red;
                        continue;
                    }
                    catch (TaskCanceledException) {/* if cancellation was requested proceed */}

                    fileSender.Close();

                    if (cancelToken.IsCancellationRequested)
                    {
                        currentReceiver.BackColor = Color.Yellow;
                        status.Text = "Afgebroken";
                    }
                    else if (sendFile.IsCompleted)
                    {
                        currentReceiver.BackColor = Color.LightGreen;
                        status.Text = "Voltooid";

                        Transfer dbTransfer = new Transfer("u", DateTime.Now, (int)TimeSpan.FromMilliseconds(transferTimer.ElapsedMilliseconds).TotalSeconds, file.Size);
                        Computer dbComputer = new Computer(requestResponse.ComputerName, recv.IP);

                        await AddTransferToDBAsync(dbTransfer, dbComputer);
                    }
                }
            }

            cancelTokenSource = null;
            lbl_Remaining.Text = "Resterend: 0";
        }

        /// <summary>
        /// Adds a transfer to the database.
        /// </summary>
        /// <param name="transfer"></param>
        /// <param name="computer"></param>
        /// <returns></returns>
        private async Task AddTransferToDBAsync(Transfer transfer, Computer computer)
        {
            using (DatabaseHandler dbh = new DatabaseHandler())
            {
                try
                {
                    await dbh.AddTransfer(transfer, computer);

                }
                catch (System.Data.SqlClient.SqlException)
                {

                }
            }
        }

        /// <summary>
        /// Gets called whenever FileSender makes progress sending a file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void FileSender_SendProgress(object sender, FileSendProgressEventArgs args)
        {
            if (currentReceiver == null) return;

            SubItem percentageColumn = currentReceiver.SubItems[currentReceiver.SubItems.IndexOfKey("clmn_Percentage")];
            if (percentageColumn != null)
            {
                int percentage = ((int)(100.0d * args.Sent / args.TotalSize));

                if (percentage < 0 || percentage > 100) return;
                if (slv_Receivers.InvokeRequired)
                {
                    slv_Receivers.BeginInvoke((MethodInvoker)delegate()
                    {
                        percentageColumn.Text = percentage.ToString();
                    });
                }
                else
                {
                    percentageColumn.Text = percentage.ToString();
                }
            }
        }
    }
}
