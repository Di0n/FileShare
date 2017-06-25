using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileShare.CustomControls
{
    class ComputerListView : CustomListView
    {
        public IEnumerable<Computer> GetAllComputers()
        {
            for (int i = 0; i < this.Items.Count; i++)
                yield return this.Items[i].Tag as Computer;
        }
    }
}
