using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Doctors.Repository;
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
    public partial class StartedExamination : Form
    {
        private readonly PatientRepository PatientRep;
        private readonly DoctorRepository DoctorRep;
        private readonly int PatientId;
        private int ExaminationId;
        private string PatientFullName;
        private Doctor ExaminingDoctor;
        public StartedExamination(int examinationId, string patientFullName, string doctorUsername)
        {
            InitializeComponent();
            PatientRep = new PatientRepository();
            DoctorRep = new DoctorRepository(doctorUsername, true);
            PatientId = PatientRep.GetPatientIdByFirstName(patientFullName.Split(' ')[0]);
            PatientFullName = patientFullName;
            ExaminationId = examinationId;
            ExaminingDoctor = DoctorRep.GetDoctorByUsername();
            
        }

        private void ShowData()
        {
            string firstQuery = "select * from MedicalRecord where id_patient = " + PatientId;

            string[] firstData = PatientRep.GetMedicalRecord(firstQuery);
            lbPatientName.Text = "Examining: " + PatientFullName;
            lbHeight.Text = "Height: " + firstData[0];
            lbWeight.Text = "Weight: " + firstData[1];
        }

        private void FillComboBoxes()
        {
            cbSpeciality.DataSource = Enum.GetValues(typeof(DoctorSpeciality));
            cbSpeciality.ValueMember = null;
            cbSpeciality.DropDownStyle = ComboBoxStyle.DropDownList;


            BindingList<Doctor> doctors = DoctorRep.GetDoctors();
            var filteredBindingList = new BindingList<Doctor>
                (doctors.Where(doc => doc.FullName != ExaminingDoctor.FullName).ToList());

            cbDoctor.ValueMember = null;
            cbDoctor.DropDownStyle = ComboBoxStyle.DropDownList;

            cbDoctor.DisplayMember = "FullName";
            cbDoctor.DataSource = filteredBindingList;
        }

        private void StartedExamination_Load(object sender, EventArgs e)
        {

            ShowData();
            FillComboBoxes();

        }

        private void lbHeight_Click(object sender, EventArgs e)
        {

        }

        private void rbSpeciality_CheckedChanged(object sender, EventArgs e)
        {
            if(rbSpeciality.Checked)
            {
                cbSpeciality.Enabled = true;
                cbDoctor.Enabled = false;
            }
            else
            {
                cbDoctor.Enabled = true;
                cbSpeciality.Enabled = false;
            }
        }
    }
}
