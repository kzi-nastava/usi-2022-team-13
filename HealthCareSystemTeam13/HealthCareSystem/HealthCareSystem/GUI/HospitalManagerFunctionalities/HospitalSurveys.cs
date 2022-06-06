using HealthCareSystem.Core.Surveys.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCareSystem.GUI.HospitalManagerFunctionalities
{
    public partial class HospitalSurveys : Form
    {
        private SurveyRepository SurveyRepository;
        public HospitalSurveys()
        {
            SurveyRepository = new SurveyRepository();
            SurveyRepository.PullHospitalSurveys();
            InitializeComponent();
            FillDataGridView();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void DataGridViewSettings()
        {
            dgwHospitalSurveys.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgwHospitalSurveys.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgwHospitalSurveys.MultiSelect = false;
            dgwHospitalSurveys.ReadOnly = true;
        }

        public void RefreshDataGridView()
        {
            SurveyRepository.PullHospitalSurveys();
            dgwHospitalSurveys.DataSource = SurveyRepository.HospitalSurveys;
            dgwHospitalSurveys.Refresh();
        }

        private void FillDataGridView()
        {
            dgwHospitalSurveys.DataSource = SurveyRepository.HospitalSurveys;
            DataGridViewSettings();
        }

    }
}
