using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core.GUI.SecretaryFunctionalities;

namespace HealthCareSystem.Core.GUI
{
    public partial class SecretaryView : Form
    {
        public SecretaryView()
        {
            InitializeComponent();
        }

        private void SecretaryView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        public void LoadForm(object Form)
        {
            if (this.mainPanel.Controls.Count > 0)
                this.mainPanel.Controls.RemoveAt(0);
            Form form = Form as Form;
            form.TopLevel = false;
            form.Dock = DockStyle.Fill;
            this.mainPanel.Controls.Add(form);
            this.mainPanel.Tag = form;
            form.Show();
        }


        private void blockedPatientsButton_Click(object sender, EventArgs e)
        {

        }

        private void requestsButton_Click(object sender, EventArgs e)
        {

        }

        private void patientsButton_Click(object sender, EventArgs e)
        {
            LoadForm(new PatientCRUDForm());
        }
    }
}
