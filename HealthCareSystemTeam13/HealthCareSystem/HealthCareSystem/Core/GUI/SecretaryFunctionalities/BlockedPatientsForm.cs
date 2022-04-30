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

namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    public partial class BlockedPatientsForm : Form
    {
        SecretaryRepository secretaryRepository;
        string Username;
        public BlockedPatientsForm(string username)
        {
            secretaryRepository = new SecretaryRepository();
            secretaryRepository.PullBlockedPatients();
            InitializeComponent();
            FillDataGridView();
            this.Username = username;
        }

        private void FillDataGridView()
        {
            blockedPatientsDataGrid.DataSource = secretaryRepository.blockedPatients;
            DataGridViewSettings();
        }

        private void DataGridViewSettings()
        {
            blockedPatientsDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //patientsDataGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //patientsDataGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //patientsDataGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //patientsDataGrid.Columns[0].Width = 90;
            // patientsDataGrid.Columns[3].Width = 90;
            // patientsDataGrid.Columns[4].Width = 90;
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
