using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core.GUI.PatientFunctionalities;

namespace HealthCareSystem.Core.GUI
{
    public partial class PatientView : Form
    {
        public string Username { get; set; }
        public LoginForm SuperForm;
        public PatientView(string username, LoginForm superForm)
        {
            Username = username;
            SuperForm = superForm;
            InitializeComponent();

        }

        private void LoadForm(object Form)
        {
            if(this.pnlPatient.Controls.Count > 0)
            {
                this.pnlPatient.Controls.RemoveAt(0);

            }
            Form selectedButton = Form as Form;
            selectedButton.TopLevel = false;
            selectedButton.Dock = DockStyle.Fill;
            this.pnlPatient.Controls.Add(selectedButton);
            this.pnlPatient.Tag = selectedButton;
            selectedButton.Show();

        }



        private void PatientView_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Are you sure?", "Exit?", MessageBoxButtons.YesNo);

            if(exit == DialogResult.Yes) Application.Exit();
        }

        private void btnExaminations_Click(object sender, EventArgs e)
        {
            LoadForm(new PatientExaminations(Username));
        }

        private void PatientView_Load(object sender, EventArgs e)
        {

        }

    }
}
