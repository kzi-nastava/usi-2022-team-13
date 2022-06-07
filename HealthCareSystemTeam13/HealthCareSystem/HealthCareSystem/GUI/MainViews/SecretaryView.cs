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
using HealthCareSystem.GUI.SecretaryFunctionalities;

namespace HealthCareSystem.Core.GUI
{
    public partial class SecretaryView : Form
    {
        string Username;
        public LoginForm SuperForm;
        public SecretaryView(string username, LoginForm superForm)
        {
            InitializeComponent();
            SuperForm = superForm;
            this.Username = username;
        }

        private void SecretaryView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SuperForm.Show();
        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Are you sure?", "Logout?", MessageBoxButtons.YesNo);

            if (exit == DialogResult.Yes) { SuperForm.Show(); this.Close(); }
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
            LoadForm(new BlockedPatientsForm(Username));
        }

        private void requestsButton_Click(object sender, EventArgs e)
        {
            LoadForm(new ExaminationRequests());
        }

        private void patientsButton_Click(object sender, EventArgs e)
        {
            LoadForm(new PatientCRUDForm(Username));
        }

        private void lettersButton_Click(object sender, EventArgs e)
        {
            LoadForm(new ReferralLettersForm());
        }

        private void requestsDynamicButton_Click(object sender, EventArgs e)
        {
            LoadForm(new OutOfStockForm(Username));
        }

        private void outOfStockButton_Click(object sender, EventArgs e)
        {
            LoadForm(new OutOfStockInRoomsForm());
        }

        private void daysOffButton_Click(object sender, EventArgs e)
        {
            LoadForm(new DaysOffRequestsForm(Username));
        }
    }
}
