using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SubItem = System.Windows.Forms.ListViewItem.ListViewSubItem;

namespace FileShare.CustomControls
{
    class SendListView : CustomListView
    {
        public SendListView()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true); // These 2 settings make sure there is no blinking when rapidly updating the listview.
            this.SetStyle(ControlStyles.EnableNotifyMessage, true);
        }

        public void AddReceivers(List<Computer> receivers)
        {
            this.BeginUpdate();
            foreach (Computer receiver in receivers)
            {
                if (this.Items.ContainsKey(receiver.IP)) continue;

                ListViewItem item = new ListViewItem(receiver.IP + ':' + receiver.Port);
                item.Name = receiver.IP;

                item.SubItems.Add(new SubItem(item, receiver.Name) { Name = "clmn_PCName" });
                item.SubItems.Add(new SubItem(item, "Wachten") { Name = "clmn_Status" });
                item.SubItems.Add(new SubItem(item, "0") { Name = "clmn_Percentage" });
                item.Tag = receiver;

                this.Items.Add(item);
            }
            this.EndUpdate();
        }

        protected override void OnNotifyMessage(Message m)
        {
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }
        }
    }
}
