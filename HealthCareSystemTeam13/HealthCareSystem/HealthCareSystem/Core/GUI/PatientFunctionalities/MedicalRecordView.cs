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
        private int PatientId;
        public MedicalRecordView(string username)
        {
            Username = username;
            PatientRep = new PatientRepository(Username);
            InitializeComponent();
        }

        private void MedicalRecordView_Load(object sender, EventArgs e)
        {
            SetValues();
        }
        private void SetValues()
        {
            PatientId = PatientRep.GetPatientId();
            Dictionary<string, string> information = PatientRep.GetPatientNameAndMedicalStats(PatientId);
            lbHeight.Text = information["height"] + " cm";
            lbWeight.Text = information["weight"] + " kg";
            lbName.Text = information["firstName"] + " " + information["lastName"];
            PatientRep.PullPastExaminations();
            dgwExaminations.DataSource = PatientRep.examinations;
            Helpers.DataGridViewSettings(dgwExaminations);
            dgwExaminations.Font = new Font("Lucida Bright", 10);


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
        
    }
}
