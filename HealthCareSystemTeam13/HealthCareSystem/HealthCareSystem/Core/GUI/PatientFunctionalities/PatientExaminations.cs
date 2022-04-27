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

namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    public partial class PatientExaminations : Form
    {
        public string Username { get; set; }
        public PatientExaminations(string username)
        {
            Username = username;
            InitializeComponent();
            FillDataGridView();
            
        }

        private void FillDataGridView()
        {
            PatientRepository patientRepository = new PatientRepository(Username);

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
            bool canEdit = CanChangeExamination();

            if (canEdit)
            {
                Console.WriteLine(dgwExaminations.SelectedCells[0].Value.ToString());
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool canDelete = CanChangeExamination();

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

    }
