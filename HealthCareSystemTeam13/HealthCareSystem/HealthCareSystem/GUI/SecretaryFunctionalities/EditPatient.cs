using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core.Users.Patients.Repository;
using HealthCareSystem.Core.Users.Secretaries.Repository;

namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    public partial class EditPatient : Form
    {
        PatientRepository _patientRepository;
        public EditPatient()
        {
            InitializeComponent();
            _patientRepository = new PatientRepository();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            string patientID = patientIdBox.Text;
            Dictionary<string, string> information = _patientRepository.GetPatientInformation(patientID);
            string name = information["firstName"];
            string lastname = information["lastName"];
            string username = information["usrnm"];
            string password = information["pass"];
            if (nameBox.Text != "") { name = nameBox.Text; }
            if (lastNameBox.Text != "") { lastname = lastNameBox.Text; }
            if (usernameBox.Text != "") { username = usernameBox.Text; }
            if (passwordBox.Text != "") { password = passwordBox.Text; }
            _patientRepository.UpdatePatient(patientID, username, password, name, lastname);
            this.Close();
        }
    }
}
