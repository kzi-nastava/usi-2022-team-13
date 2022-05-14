using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.GUI.DoctorsFunctionalities;
using HealthCareSystem.Core.Users.Doctors.Repository;
using HealthCareSystem.Core.Users.Patients.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCareSystem
{
    public partial class DoctorView : Form
    {
        public string Username { get; set; }
        public LoginForm AuthForm;
        private readonly DoctorRepository DoctorRep;
        public DoctorView(string username, LoginForm authForm)
        {
            Username = username;
            AuthForm = authForm;
            DoctorRep = new DoctorRepository(username, true);
            DoctorRep.PullExaminations();
            InitializeComponent();

            FillDataGridView();

        }

        private void FillDataGridView()
        {

            dgwExaminations.DataSource = DoctorRep.examinations;
            DataGridViewSettings();
        }

        private void DataGridViewSettings()
        {
            dgwExaminations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgwExaminations.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgwExaminations.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgwExaminations.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgwExaminations.Columns[0].Width = 90;
            dgwExaminations.Columns[3].Width = 90;
            dgwExaminations.Columns[4].Width = 90;
            dgwExaminations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgwExaminations.MultiSelect = false;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            InsertOrChangeExamination InsertOrChangeExaminationView = new InsertOrChangeExamination(
                (int)dgwExaminations.SelectedRows[0].Cells[0].Value, true, Username, 1);

            InsertOrChangeExaminationView.ShowDialog();


        }

        private void DoctorView_FormClosing(object sender, FormClosingEventArgs e)
        {
            AuthForm.Show();
        }

        private void DoctorView_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CanChangeExamination())
            {
                DialogResult wantToCancel = MessageBox.Show("Are you sure?", "Cancel Examination", MessageBoxButtons.YesNo);

                if (wantToCancel == DialogResult.Yes)
                {
                        DoctorRep.CancelExamination((int)dgwExaminations.SelectedRows[0].Cells[0].Value);
                        // DoctorRep.InsertExaminationChanges(TypeOfChange.Delete);
                        MessageBox.Show("Succesfully canceled examination!");
                        RefreshDataGridView();
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        public void RefreshDataGridView()
        {
            DoctorRep.PullExaminations();
            dgwExaminations.DataSource = DoctorRep.examinations;
            dgwExaminations.Refresh();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (CanChangeExamination())
            {
                int validDate = IsValidDate();
                if (validDate != 0)
                {
                    int examinationId = (int)dgwExaminations.SelectedRows[0].Cells[0].Value;
                    InsertOrChangeExamination insertOrChangeExaminationForm = 
                        new InsertOrChangeExamination(examinationId, false, Username, validDate);

                    insertOrChangeExaminationForm.ShowDialog();

                }
            }
        }

        private int IsValidDate()
        {

            // 0 invalid
            // 1 valid for direct change

            DateTime examinationDate = (DateTime)dgwExaminations.SelectedRows[0].Cells[2].Value;


            if (examinationDate.CompareTo(DateTime.Now) < 0)
            {
                MessageBox.Show("This examination date/time has expired.");
                return 0;
            }

            return 1;

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

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Are you sure?", "Logout?", MessageBoxButtons.YesNo);

            if (exit == DialogResult.Yes) { AuthForm.Show(); this.Close(); }
        }

        private void btnShowDay_Click(object sender, EventArgs e)
        {
            DateTime date = dtDate.Value;
            DoctorRep.PullExaminationsByDate(date);
            dgwExaminations.DataSource = DoctorRep.examinations;
            dgwExaminations.Refresh();
        }

        private void btnShowNextThreeDays_Click(object sender, EventArgs e)
        {
            DoctorRep.PullExaminationsThreeDays();
            dgwExaminations.DataSource = DoctorRep.examinations;
            dgwExaminations.Refresh();
        }

        private void btnMedicalRecord_Click(object sender, EventArgs e)
        {
            if (CanChangeExamination())
            {
                string patientFullName = dgwExaminations.SelectedRows[0].Cells[1].Value.ToString();
                PatientMedicalRecord patientMedicalRecordForm = new PatientMedicalRecord(patientFullName);
                patientMedicalRecordForm.ShowDialog();
            }
        }

        private void btnStartExamination_Click(object sender, EventArgs e)
        {
            if (CanChangeExamination())
            {
                int validDate = IsValidDate();
                if (validDate != 0)
                {
                    int examinationId = (int)dgwExaminations.SelectedRows[0].Cells[0].Value;
                    string patientFullName = dgwExaminations.SelectedRows[0].Cells[1].Value.ToString();
                    StartedExamination startedExaminationForm = new 
                        StartedExamination(examinationId, patientFullName, Username);


                    startedExaminationForm.ShowDialog();

                }
            }
        }
    }
}
