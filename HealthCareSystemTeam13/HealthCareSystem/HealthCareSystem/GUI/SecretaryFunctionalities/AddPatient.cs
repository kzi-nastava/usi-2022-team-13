using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core.Users;
using HealthCareSystem.Core.Users.Model;
using HealthCareSystem.Core.Users.Patients.Model;
using HealthCareSystem.Core.Users.Patients.Repository;
using HealthCareSystem.Core.Users.Secretaries.Repository;

namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    public partial class AddPatient : Form
    {
        readonly MedicalRecordRepository _medicalRecordRepository;
        readonly IUserRepository _userRepository;
        private readonly IPatientRepository _patientRepository;
        public AddPatient()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
            _patientRepository = new PatientRepository();
            _medicalRecordRepository = new MedicalRecordRepository();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            User user = new User(usernameBox.Text, passwordBox.Text, UserRole.Patients);
            _userRepository.InsertSingleUser(user);
            string userID = _userRepository.GetUserId(usernameBox.Text)[0];
            Patient patient = new Patient(nameBox.Text, lastNameBox.Text, Convert.ToInt32(userID), false);
            _patientRepository.InsertSinglePatient(patient);
            string patientID = _patientRepository.GetPatientIdByUserId(userID)[0];
            MedicalRecord medicalRecord = new MedicalRecord(Convert.ToInt32(patientID), Convert.ToInt32(heightBox.Text), Convert.ToInt32(weightBox.Text));
            _medicalRecordRepository.InsertSingleMedicalRecord(medicalRecord);
            this.Close();
        }

    }
}
