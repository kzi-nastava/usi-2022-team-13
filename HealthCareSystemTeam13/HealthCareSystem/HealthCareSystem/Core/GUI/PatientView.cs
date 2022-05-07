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
using HealthCareSystem.Core.Users.Patients.Repository;

namespace HealthCareSystem.Core.GUI
{
    public partial class PatientView : Form
    {
        public string Username { get; set; }
        public LoginForm SuperForm;
        private PatientRepository PatientRep;
        public PatientView(string username, LoginForm superForm)
        {
            Username = username;
            SuperForm = superForm;
            PatientRep = new PatientRepository(username);
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
            SuperForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Are you sure?", "Logout?", MessageBoxButtons.YesNo);

            if (exit == DialogResult.Yes) { SuperForm.Show(); this.Close(); }
        }

        private void btnExaminations_Click(object sender, EventArgs e)
        {
            if (!PatientRep.IsPatientBlocked(Username))
            {
                LoadForm(new PatientExaminations(Username));
            }
            else
            {
                MessageBox.Show("You are blocked!");
                SuperForm.Show(); this.Close();
            }
        }

        private void PatientView_Load(object sender, EventArgs e)
        {

        }

    }
}
