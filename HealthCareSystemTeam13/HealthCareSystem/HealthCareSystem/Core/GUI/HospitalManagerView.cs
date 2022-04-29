using HealthCareSystem.Core.GUI.HospitalManagerFunctionalities;
using HealthCareSystem.Core.Rooms.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCareSystem.Core.GUI
{
    public partial class HospitalManagerView : Form
    {
        public string Username { get; set; }
        public LoginForm SuperForm;
        public HospitalManagerView(string username, LoginForm superForm)
        {
            Username = username;
            SuperForm = superForm;
            InitializeComponent();
        }


        private void LoadForm(object Form)
        {
           
            Form selectedButton = Form as Form;
            selectedButton.TopLevel = false;
            selectedButton.Dock = DockStyle.Fill;
            this.pnlHospitalManager.Controls.Add(selectedButton);
            this.pnlHospitalManager.Tag = selectedButton;
            selectedButton.Show();
        }

        private void HospitalManagerView_FormClosing(object sender, FormClosingEventArgs e)
        {
            SuperForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Are you sure?", "Logout?", MessageBoxButtons.YesNo);

            if (exit == DialogResult.Yes) { SuperForm.Show(); this.Close(); }
        }

        private void btnRooms_Click(object sender, EventArgs e)
        {
            LoadForm(new HospitalRooms());
        }

        private void HospitalManagerView_Load(object sender, EventArgs e)
        {

        }
    }
}
