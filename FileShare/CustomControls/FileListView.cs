using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileShare.CustomControls
{
    class FileListView : CustomListView
    {
        public delegate void FileListChangedEventHandler(object source, EventArgs e);
        public event FileListChangedEventHandler FileListChanged;
        public async Task AddFiles(List<File> files)
        {
            this.BeginUpdate();

            foreach (File file in files)
            {
                if (this.Items.ContainsKey(file.Path)) continue; // File already in list.

                Icon fileIcon = null;

                ListViewItem item = new ListViewItem(file.Name, 0);
                item.Name = file.Path;

                string[] subItems = new string[3]
                {
                    file.Type, 
                    file.Size.ToString(),
                    file.DateModified.ToString()
                };

                item.SubItems.AddRange(subItems);

                if (file.Type == ".EXE")
                {
                    if (!this.SmallImageList.Images.ContainsKey(file.Path))
                        this.SmallImageList.Images.Add(file.Path, await Task.Run<Icon>(()=> file.GetIcon()));

                    item.ImageKey = file.Path;
                }
                else
                {
                    if (!this.SmallImageList.Images.ContainsKey(file.Type))
                        this.SmallImageList.Images.Add(file.Type, await Task.Run<Icon>(() => file.GetIcon()));

                    item.ImageKey = file.Type;
                }

                item.Tag = file;

                this.Items.Add(item);
                if (fileIcon != null) fileIcon.Dispose();

                TotalFileSize += file.Size;
            }
            this.EndUpdate();
            OnFileListChange();
        }

        public IEnumerable<File> GetFiles()
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                yield return this.Items[i].Tag as File;
            }
        }

        public void RemoveSelectedFiles()
        {
            this.BeginUpdate();
            foreach (ListViewItem item in this.SelectedItems)
            {
                File file = item.Tag as File;

                if (file.Type == ".EXE")
                    this.SmallImageList.Images.RemoveByKey(file.Path);

                TotalFileSize -= file.Size;
                item.Remove();
            }
            this.EndUpdate();
            OnFileListChange();
        }

        public void ClearAllFiles()
        {
            this.BeginUpdate();
            this.Items.Clear();
            this.EndUpdate();

            this.SmallImageList.Images.Clear();

            TotalFileSize = 0;

            OnFileListChange();
        }
     
        private void OnFileListChange()
        {
            if (FileListChanged != null) FileListChanged(this, EventArgs.Empty);
        }

        public long TotalFileSize { get; private set; }
    }
}
