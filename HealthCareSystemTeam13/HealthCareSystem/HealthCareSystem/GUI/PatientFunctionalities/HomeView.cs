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
        private readonly IPatientRepository _patientRepository;
        public HomeView(string username)
        {
            this.Username = username;
            _patientRepository = new PatientRepository(this.Username);
            InitializeComponent();
        }

        private void HomeView_Load(object sender, EventArgs e)
        {
            int patientId = _patientRepository.GetPatientId();
            string name = DatabaseCommander.ExecuteReaderQueries("select firstName + ' ' + lastName from Patients where id = " + patientId + "", DatabaseConnection.GetConnection())[0];
            lbName.Text = name;
        }
    }
}
