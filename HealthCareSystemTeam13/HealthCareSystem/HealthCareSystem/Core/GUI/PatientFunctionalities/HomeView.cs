using HealthCareSystem.Core.Users.Patients.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    public partial class HomeView : Form
    {
        public string Username { get; set; }
        private PatientRepository PatientRep;
        public HomeView(string username)
        {
            this.Username = username;
            PatientRep = new PatientRepository(this.Username);
            InitializeComponent();
        }

        private void HomeView_Load(object sender, EventArgs e)
        {
            int patientId = PatientRep.GetPatientId();
            string name = DatabaseHelpers.ExecuteReaderQueries("select firstName + ' ' + lastName from Patients where id = " + patientId + "", PatientRep.Connection)[0];
            lbName.Text = name;
        }
    }
}
