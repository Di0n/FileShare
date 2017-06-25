using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileShare.CustomControls
{
    class CustomListView : ListView
    {
        public CustomListView()
        {
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        }
        public IEnumerable<ListViewItem> GetAllSelectedListViewItems()
        {
            for (int i = 0; i < this.SelectedItems.Count; i++)
                yield return this.SelectedItems[i];
        }

        public IEnumerable<ListViewItem> GetAllListViewItems()
        {
            for (int i = 0; i < this.Items.Count; i++)
                yield return this.Items[i];
        }
    }
}
