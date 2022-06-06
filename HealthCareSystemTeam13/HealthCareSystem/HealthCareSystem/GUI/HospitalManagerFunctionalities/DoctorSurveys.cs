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
    public partial class DoctorSurveys : Form
    {
        private SurveyRepository SurveyRepository;
        public DoctorSurveys()
        {
            SurveyRepository = new SurveyRepository();
            SurveyRepository.PullDoctorSurveys();
            InitializeComponent();
            FillDataGridView();
        }

        private void DataGridViewSettings()
        {
            dgwDoctorSurveys.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgwDoctorSurveys.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgwDoctorSurveys.MultiSelect = false;
        }

        public void RefreshDataGridView()
        {
            SurveyRepository.PullDoctorSurveys();
            dgwDoctorSurveys.DataSource = SurveyRepository.DoctorSurveys;
            dgwDoctorSurveys.Refresh();
        }

        private void FillDataGridView()
        {
            dgwDoctorSurveys.DataSource = SurveyRepository.DoctorSurveys;
            DataGridViewSettings();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }
    }
}
