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

namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    public partial class ExaminationRequests : Form
    {
        SecretaryRepository _secretaryRepository;

        public ExaminationRequests()
        {
            _secretaryRepository = new SecretaryRepository();
            _secretaryRepository.PullExaminationRequests();
            InitializeComponent();
            FillDataGridView();
        }

        private void FillDataGridView()
        {
            requestsDataGrid.DataSource = _secretaryRepository.RequestsPatients;
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
            if (_secretaryRepository.IsRequestChanged(requestID))
            {
                _secretaryRepository.UpdateExamination(requestID);
                _secretaryRepository.DeleteSinglePatientRequest(requestID);
            } 
            else
            {
                _secretaryRepository.DeleteSingleExamination(requestID);
            }    

            MessageBox.Show("Succesfully accepted request!");
        }

        private void denyButton_Click(object sender, EventArgs e)
        {
            string requestID = requestIdBox.Text;
            _secretaryRepository.DeleteSinglePatientRequest(requestID);
            MessageBox.Show("Succesfully denied request!");
        }
    }
}
