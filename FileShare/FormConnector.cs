using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileShare
{
    class FormConnector
    {
        private Form        ownerForm;
        private List<Form>  connectedForms;

        public FormConnector(Form ownerForm)
        {
            this.ownerForm = ownerForm;
            this.ownerForm.LocationChanged += (o, e) => OwnerForm_StateChanged();
            this.ownerForm.SizeChanged += (o, e) => OwnerForm_StateChanged();
            connectedForms = new List<Form>();
        }

        public void ConnectForm(Form form)
        {
            if (!this.connectedForms.Contains(form))
            {
                this.connectedForms.Add(form);
            }
        }

        public void RemoveForm(Form form)
        {
            this.connectedForms.Remove(form);
        }

        public void RemoveAllForms()
        {
            this.connectedForms.Clear();
        }

        private void OwnerForm_StateChanged()
        {
            connectedForms.ForEach(f =>
                {
                    if (f != null && f.Visible == true)
                    {
                        if (Properties.Settings.Default.IsWindows10)
                            f.Location = new System.Drawing.Point(ownerForm.Location.X + ownerForm.Width - 15, ownerForm.Location.Y);
                        else
                            f.Location = new System.Drawing.Point(ownerForm.Location.X + ownerForm.Width, ownerForm.Location.Y);
                    }
                });
        }
    }
}
