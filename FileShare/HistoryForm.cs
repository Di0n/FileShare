using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileShare
{
    public partial class HistoryForm : Form
    {
        DatabaseHandler dbh;

        public HistoryForm()
        {
            InitializeComponent();

            dbh = new DatabaseHandler();
        }

        private async void HistoryForm_Load(object sender, EventArgs e)
        {
            DataTable dataSet = null;
            dataSet = await dbh.GetHistory();

            ListViewItem lvi;
            string transferType, time, fileSize, address, port, pcName, transferCount;
            int duration;

            string formattedSender, formattedReceiver, formattedTransferType, formattedDuration, formattedFileSize;

            for (int i = 0; i < dataSet.Rows.Count; i++)
            {
                // Saving all data in regular strings first
                transferType = dataSet.Rows[i][0].ToString();
                time = dataSet.Rows[i][1].ToString();
                duration = int.Parse(dataSet.Rows[i][2].ToString());
                fileSize = dataSet.Rows[i][3].ToString();
                address = dataSet.Rows[i][4].ToString();
                port = dataSet.Rows[i][5].ToString();
                pcName = dataSet.Rows[i][6].ToString();
                transferCount = dataSet.Rows[i][7].ToString();

                // Formatting all items to neat strings
                long sizeBytes;
                long.TryParse(fileSize, out sizeBytes);
                formattedFileSize = Utility.ConvertFileSize(sizeBytes);

                TimeSpan t = TimeSpan.FromSeconds(duration);
                formattedDuration = string.Format("{0:D2}h {1:D2}m {2:D2}s", t.Hours, t.Minutes, t.Seconds);

                switch (transferType)
                {
                    case "u":
                        formattedTransferType = "Upload";
                        formattedSender = "Ik";
                        formattedReceiver = pcName + " @ " + address + ":" + port;
                        break;
                    case "d":
                        formattedTransferType = "Download";
                        formattedSender = pcName + " @ " + address + ":" + port;
                        formattedReceiver = "Ik";
                        break;
                    default:
                        formattedTransferType = "Transfer type onbekend";
                        formattedSender = "";
                        formattedReceiver = "";
                        break;
                }

                // Building the list view item
                lvi = new ListViewItem(formattedTransferType);
                lvi.SubItems.Add(time);
                lvi.SubItems.Add(formattedDuration);
                lvi.SubItems.Add(formattedFileSize);
                lvi.SubItems.Add(formattedSender);
                lvi.SubItems.Add(formattedReceiver);
                lvi.SubItems.Add(transferCount);

                lv_HistoryData.Items.Add(lvi);
            }
        }


    }
}
