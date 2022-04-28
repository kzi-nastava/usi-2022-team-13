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

namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    public partial class PatientExaminations : Form
    {
        public string Username { get; set; }
        PatientRepository patientRepository;
        public PatientExaminations(string username)
        {
            Username = username;
            patientRepository = new PatientRepository(Username);
            patientRepository.PullExaminations();
       
            InitializeComponent();
            FillDataGridView();

        }

        private void FillDataGridView()
        {

            dgwExaminations.DataSource = patientRepository.examinations;
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
        private void dgwExaminations_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

            if (CanChangeExamination())
            {
                if (IsValidDate())
                {
                    int examinationId = (int)dgwExaminations.SelectedRows[0].Cells[0].Value;
                    AddEditExamination addEditView = new AddEditExamination(examinationId, false, Username, this);

                    addEditView.ShowDialog();

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            if (CanChangeExamination())
            {
                DialogResult wantToCancel = MessageBox.Show("Are you sure?", "Cancel Examination", MessageBoxButtons.YesNo);

                if (wantToCancel == DialogResult.Yes)
                {
                    if (IsValidDate())
                    {
                        patientRepository.CancelExamination((int)dgwExaminations.SelectedRows[0].Cells[0].Value);
                        MessageBox.Show("Succesfully canceled examination!");
                        RefreshDataGridView();

                    }
                }
            }

        }

        private bool IsValidDate()
        {
            DateTime examinationDate = (DateTime)dgwExaminations.SelectedRows[0].Cells[2].Value;


            if (examinationDate.CompareTo(DateTime.Now) < 0)
            {
                MessageBox.Show("This examination date/time has expired.");
                return false;
            }
            else if ((examinationDate - DateTime.Now).TotalDays < 1)
            {
                MessageBox.Show("You can only edit/delete an examination atleast 1 day before it starts");
                return false;
            }

            return true;

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
            patientRepository.PullExaminations();
            dgwExaminations.DataSource = patientRepository.examinations;
            dgwExaminations.Refresh();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEditExamination addEditView = new AddEditExamination((int)dgwExaminations.SelectedRows[0].Cells[0].Value, true, Username, this);

            addEditView.ShowDialog();
            

        }

        private void PatientExaminations_Load(object sender, EventArgs e)
        {

        }
    }

}