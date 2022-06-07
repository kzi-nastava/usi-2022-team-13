using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core.Users.Secretaries.Repository;
using HealthCareSystem.Core.GUI.SecretaryFunctionalities;
using HealthCareSystem.Core.Users.Patients.Repository;

namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    public partial class BlockedPatientsForm : Form
    {
        PatientRepository _patientRepository;
        string Username;
        public BlockedPatientsForm(string username)
        {
            _patientRepository = new PatientRepository();
            _patientRepository.PullBlockedPatients();
            InitializeComponent();
            FillDataGridView();
            this.Username = username;
        }

        private void FillDataGridView()
        {
            blockedPatientsDataGrid.DataSource = _patientRepository.BlockedPatients;
            DataGridViewSettings();
        }

        private void DataGridViewSettings()
        {
            blockedPatientsDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            blockedPatientsDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            blockedPatientsDataGrid.MultiSelect = false;
        }

        private void unblockButton_Click(object sender, EventArgs e)
        {
            UnblockPatient unblockPatient = new UnblockPatient();
            unblockPatient.ShowDialog();
        }
    }
}
