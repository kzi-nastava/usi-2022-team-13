using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core.Users.Secretaries.Repository;

namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    public partial class EditPatient : Form
    {
        SecretaryRepository secretaryRepository;
        public EditPatient()
        {
            InitializeComponent();
            secretaryRepository = new SecretaryRepository();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            string patientID = patientIdBox.Text;
            List<String> information = secretaryRepository.GetPatientInformation(patientID);
            string name = information[0];
            Console.WriteLine(information[0]);
            string lastname = information[1];
            string username = information[2];
            string password = information[3];
            if (nameBox.Text != "") { name = nameBox.Text; }
            if (lastNameBox.Text != "") { lastname = lastNameBox.Text; }
            if (usernameBox.Text != "") { username = usernameBox.Text; }
            if (passwordBox.Text != "") { password = passwordBox.Text; }
            Console.WriteLine(patientID, username, password, name, lastname);
            secretaryRepository.UpdatePatient(patientID, username, password, name, lastname);
            this.Close();
        }
    }
}
