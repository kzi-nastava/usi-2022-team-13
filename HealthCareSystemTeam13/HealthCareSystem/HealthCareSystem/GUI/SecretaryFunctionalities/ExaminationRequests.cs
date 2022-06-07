using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core.Examinations.Repository;
using HealthCareSystem.Core.Users.Patients.Repository;
using HealthCareSystem.Core.Users.Secretaries.Repository;

namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    public partial class ExaminationRequests : Form
    {
        ExaminationRepository _examinationRepository;
        PatientRepository _patientRepository;
        public ExaminationRequests()
        {
            _examinationRepository = new ExaminationRepository();
            _patientRepository = new PatientRepository();
            _examinationRepository.PullExaminationRequests();
            InitializeComponent();
            FillDataGridView();
        }

        private void FillDataGridView()
        {
            requestsDataGrid.DataSource = _examinationRepository.RequestsPatients;
            DataGridViewSettings();
        }
        private void DataGridViewSettings()
        {
            requestsDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            requestsDataGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            requestsDataGrid.MultiSelect = false;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            string requestID = requestIdBox.Text;
            if (_patientRepository.IsRequestChanged(requestID))
            {
                _examinationRepository.UpdateExamination(requestID);
                _patientRepository.DeleteSinglePatientRequest(requestID);
            } 
            else
            {
                _examinationRepository.DeleteSingleExamination(requestID);
            }    

            MessageBox.Show("Succesfully accepted request!");
        }

        private void denyButton_Click(object sender, EventArgs e)
        {
            string requestID = requestIdBox.Text;
            _patientRepository.DeleteSinglePatientRequest(requestID);
            MessageBox.Show("Succesfully denied request!");
        }
    }
}
