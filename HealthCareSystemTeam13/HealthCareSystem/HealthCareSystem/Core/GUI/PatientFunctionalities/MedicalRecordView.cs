using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Examinations.Repository;
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
    public partial class MedicalRecordView : Form
    {
        public string Username { get; set; }
        private PatientRepository PatientRep;
        private ExaminationRepository ExaminationRep;
        private int PatientId;
        private List<DoctorAnamnesis> anamnesises;
        public MedicalRecordView(string username)
        {
            Username = username;
            PatientRep = new PatientRepository(Username);
            ExaminationRep = new ExaminationRepository();
            InitializeComponent();
        }

        private void MedicalRecordView_Load(object sender, EventArgs e)
        {
            SetValues();
        }
        private void SetValues()
        {
            PatientId = PatientRep.GetPatientId();
            
            SetTextValues();
            SetDgwExaminations();
            SetListBoxDiseases();
            SetDgwAnamnesis();
        }
        private void SetTextValues()
        {
            Dictionary<string, string> information = PatientRep.GetPatientNameAndMedicalStats(PatientId);
            lbHeight.Text = information["height"] + " cm";
            lbWeight.Text = information["weight"] + " kg";
            lbName.Text = information["firstName"] + " " + information["lastName"];
        }
        private void SetDgwExaminations()
        {
            PatientRep.PullPastExaminations();
            dgwExaminations.DataSource = PatientRep.examinations;
            Helpers.DataGridViewSettings(dgwExaminations);
            dgwExaminations.Font = new Font("Lucida Bright", 10);

        }
        private void SetDgwAnamnesis()
        {
            List<Examination> examinations = ExaminationRep.GetFinishedExaminations(PatientId);

            anamnesises = PatientRep.GetAnamnesises(examinations);

            dgwAnamnesis.DataSource = anamnesises;

            Helpers.DataGridViewSettings(dgwAnamnesis);
            dgwAnamnesis.Font = new Font("Lucida Bright", 10);
        }

        private void SetListBoxDiseases()
        {
            
            int medicalRecordId = Convert.ToInt32(DatabaseHelpers.ExecuteReaderQueries("select id from MedicalRecord where id_patient = " + PatientId + "", PatientRep.Connection)[0]);
            List<string> diseases = DatabaseHelpers.ExecuteReaderQueries("select nameOfDisease from DiseaseHistory where id_medicalRecord = " + medicalRecordId + "", PatientRep.Connection);
            lbDiseases.DataSource = diseases;

        }

        private void btnShowAnamnesis_Click(object sender, EventArgs e)
        {
            if (Helpers.IsDgwRowSelected(dgwExaminations))
            {
                int examinationId = (int)dgwExaminations.SelectedRows[0].Cells[0].Value;
                AnamnesisView anamnesisView = new AnamnesisView(examinationId);
                anamnesisView.ShowDialog();
            }
        }

        private void btnShowAnamnesisInSearch_Click(object sender, EventArgs e)
        {
            if (Helpers.IsDgwRowSelected(dgwAnamnesis))
            {
                int examinationId = (int)dgwAnamnesis.SelectedRows[0].Cells[0].Value;
                AnamnesisView anamnesisView = new AnamnesisView(examinationId);
                anamnesisView.ShowDialog();
            }
        }

        private void btnSearchAnamnesis_Click(object sender, EventArgs e)
        {
            string keyword = tbAnamnesis.Text;
            if (keyword.Trim() != "") dgwAnamnesis.DataSource = PatientRep.GetAnamnesisesByKeyword(anamnesises, keyword);
            else MessageBox.Show("No input.");
        }

        private void btnSortByDoctor_Click(object sender, EventArgs e)
        {
            // sort by doctor names and set new datasource to sorted list

        }

        private void btnSortBySpeciality_Click(object sender, EventArgs e)
        {
            // sort by speciality and set new datasource to sorted list

        }

        private void btnSortByDate_Click(object sender, EventArgs e)
        {
            // sort by date and set new datasource to sorted list

        }
    }
}
