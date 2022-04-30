﻿using HealthCareSystem.Core.Users.Patients.Model;
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

namespace HealthCareSystem.Core.GUI.DoctorsFunctionalities
{
    public partial class PatientMedicalRecord : Form
    {
        private readonly PatientRepository PatientRep;
        private readonly int PatientId;
        public PatientMedicalRecord(string patientFullName)
        {
            InitializeComponent();
            PatientRep = new PatientRepository();
            PatientId = PatientRep.GetPatientIdByFirstName(patientFullName.Split(' ')[0]);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PatientMedicalRecord_Load(object sender, EventArgs e)
        {
            string query = "select * from MedicalRecord where id_patient = " + PatientId;
            string[] data = PatientRep.GetMedicalRecord(query);
            lbHeight.Text = "Height: " + data[0];
            lbWeight.Text = "Weight: " + data[1];
        }
    }
}
