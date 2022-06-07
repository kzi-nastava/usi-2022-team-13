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
using HealthCareSystem.Core.GUI.PatientFunctionalities;
using HealthCareSystem.Core.Users.Patients.Model;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Doctors.Repository;

namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    public partial class PatientExaminations : Form
    {
        public string Username { get; set; }
        private PatientRepository _patientRepository;
        private DoctorRepository _doctorRepository;
        public PatientExaminations(string username)
        {
            Username = username;
            _patientRepository = new PatientRepository(Username);
            _patientRepository.PullExaminationForPatient();
            _doctorRepository = new DoctorRepository();
            InitializeComponent();
            FillDataGridView();

        }

        private void FillDataGridView()
        {

            dgwExaminations.DataSource = _patientRepository.Examinations;
            GUIHelpers.DataGridViewSettings(dgwExaminations);
        }
        
        private void dgwExaminations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (!_patientRepository.IsPatientBlocked(Username))
            {
                if (CanChangeExamination())
                {
                    int validDate = IsValidDate();
                    if (validDate != 0)
                        EditExamination(validDate);
                }
            }
            else
            {
                MessageBox.Show("You are blocked");
            }

        }

        private void EditExamination(int validDate)
        {
            int examinationId = (int)dgwExaminations.SelectedRows[0].Cells[0].Value;

            AddEditExamination addEditView = new AddEditExamination(examinationId, false, Username, validDate);

            addEditView.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            if (!_patientRepository.IsPatientBlocked(Username))
            {
                if (CanChangeExamination())
                {
                    DialogResult wantToCancel = MessageBox.Show("Are you sure?", "Cancel Examination", MessageBoxButtons.YesNo);

                    if (wantToCancel == DialogResult.Yes)
                    {
                        int validDate = IsValidDate();
                        if (validDate == 1)
                            CancelExamination();
                        else if (validDate == 2)
                            CancelExaminationWithRequest();
                    }
                }
            }
            else
                MessageBox.Show("You are blocked!");

        }

        private void CancelExaminationWithRequest()
        {
            _patientRepository.SendExaminationEditRequest((int)dgwExaminations.SelectedRows[0].Cells[0].Value, DateTime.Now,
                false, 0, DateTime.Now, 0);
            MessageBox.Show("Wait for a secretary to aproove this request.");
        }

        private void CancelExamination()
        {
            _patientRepository.CancelExamination((int)dgwExaminations.SelectedRows[0].Cells[0].Value);
            _patientRepository.InsertExaminationChanges(TypeOfChange.Delete);
            _patientRepository.BlockSpamPatients(Username);
            MessageBox.Show("Succesfully canceled examination!");
            RefreshDataGridView();
        }

        private int IsValidDate()
        {

            // 0 invalid
            // 1 valid for direct change
            // 2 valid for secretary request

            DateTime examinationDate = (DateTime)dgwExaminations.SelectedRows[0].Cells[2].Value;


            if (examinationDate.CompareTo(DateTime.Now) < 0)
            {
                MessageBox.Show("This examination date/time has expired.");
                return 0;
            }
            else if ((examinationDate - DateTime.Now).TotalDays < 2)
            {
                return 2;
            }

            return 1;

        }
        private bool CanRateDoctor()
        {
            if (GUIHelpers.IsDgwRowSelected(dgwExaminations))
            {
                DateTime examinationDate = (DateTime)dgwExaminations.SelectedRows[0].Cells[2].Value;

                if (examinationDate.CompareTo(DateTime.Now) < 0)
                {

                    return true;
                }
                MessageBox.Show("Your examination hasn't been finished.");
            }
            else
                MessageBox.Show("Please select an examination first.");
            return false;
        }


        private bool CanChangeExamination()
        {
            if (dgwExaminations.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row first.");

            }
            else if (dgwExaminations.SelectedRows.Count == 1)
            {
                DataGridViewRow row = dgwExaminations.SelectedRows[0];
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("You selected an empty row.");
                }
                else
                {
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Please select only 1 row.");
            }
            return false;
        }

        public void RefreshDataGridView()
        {
            _patientRepository.PullExaminationForPatient();
            dgwExaminations.DataSource = _patientRepository.Examinations;
            dgwExaminations.Refresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!_patientRepository.IsPatientBlocked(Username))
            {
                
                AddEditExamination addEditView = new AddEditExamination(0, true, Username, 1);

                addEditView.ShowDialog();
            }
            else
            {
                MessageBox.Show("You are blocked!");
            }


        }

        private void PatientExaminations_Load(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!_patientRepository.IsPatientBlocked(Username))
            {
                RefreshDataGridView();

            }
            else
            {
                MessageBox.Show("You are blocked!");
            }

            RefreshDataGridView();
        }

        private void btnRateDoctor_Click(object sender, EventArgs e)
        {
            if (!_patientRepository.IsPatientBlocked(Username))
            {
                if (CanRateDoctor())
                {
                    string selectedDoctorName = dgwExaminations.SelectedRows[0].Cells[1].Value.ToString();
                    int patientId = _patientRepository.GetPatientId();
                    var name = selectedDoctorName.Split(' ');
                    int doctorId = _doctorRepository.GetDoctorIdByFullName(name[0], name[1]);
                    DoctorSurveyView surveyView = new DoctorSurveyView(patientId, doctorId);
                    surveyView.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("You are blocked!");
            }
        }
    }

}