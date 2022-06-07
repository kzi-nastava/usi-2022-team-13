﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core.Users.Model;
using HealthCareSystem.Core.Users.Patients.Model;
using HealthCareSystem.Core.Users.Secretaries.Repository;

namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    public partial class AddPatient : Form
    {
        SecretaryRepository _secretaryRepository;
        public AddPatient()
        {
            InitializeComponent();
            _secretaryRepository = new SecretaryRepository();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            User user = new User(usernameBox.Text, passwordBox.Text, UserRole.Patients);
            _secretaryRepository.InsertSingleUser(user);
            string userID = _secretaryRepository.GetUserId(usernameBox.Text)[0];
            Patient patient = new Patient(nameBox.Text, lastNameBox.Text, Convert.ToInt32(userID), false);
            _secretaryRepository.InsertSinglePatient(patient);
            string patientID = _secretaryRepository.GetPatientId(userID)[0];
            MedicalRecord medicalRecord = new MedicalRecord(Convert.ToInt32(patientID), Convert.ToInt32(heightBox.Text), Convert.ToInt32(weightBox.Text));
            _secretaryRepository.InsertSingleMedicalRecord(medicalRecord);
            this.Close();
        }

    }
}
