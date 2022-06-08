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
        PatientRequestRepository _patientRequestRepository;
        public ExaminationRequests()
        {
            _examinationRepository = new ExaminationRepository();
            _patientRequestRepository = new PatientRequestRepository();
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
            if (_patientRequestRepository.IsRequestChanged(requestID))
            {
                _examinationRepository.UpdateExamination(requestID);
                _patientRequestRepository.DeleteSinglePatientRequest(requestID);
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
            _patientRequestRepository.DeleteSinglePatientRequest(requestID);
            MessageBox.Show("Succesfully denied request!");
        }
    }
}
