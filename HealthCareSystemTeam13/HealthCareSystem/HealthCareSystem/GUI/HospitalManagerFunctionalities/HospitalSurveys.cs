using HealthCareSystem.Core.Surveys;
using HealthCareSystem.Core.Surveys.HospitalSurveys.Model;
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
            FillGradeLabels();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDataGridView();
        }

        private void FillGradeLabels()
        {
            List<HospitalSurvey> hospitalSurveys = SurveyRepository.GetHospitalSurveys();
            int[] numberOfHygeneGrades = SurveyService.GetNumberOfHospitalGrades(hospitalSurveys, true);
            int[] numberOfQualityGrades = SurveyService.GetNumberOfHospitalGrades(hospitalSurveys, false);
            Label[] hygene = { lblHygene1, lblHygene2, lblHygene3, lblHygene4, lblHygene5 };
            Label[] quality = { lblQuality1, lblQuality2, lblQuality3, lblQuality4, lblQuality5 };

            double hygeneSum = SurveyService.SumAllHospitalGrades(hospitalSurveys, true);
            int countHygene = 0;
            double qualitySum = SurveyService.SumAllHospitalGrades(hospitalSurveys, false);
            int countQuality = 0;

            for (int i = 0; i < 5; i++)
            {
                hygene[i].Text += numberOfHygeneGrades[i].ToString();
                quality[i].Text += numberOfQualityGrades[i].ToString();
                countHygene += numberOfHygeneGrades[i];
                countQuality += numberOfQualityGrades[i];
            }

            lblHygeneAvg.Text += (hygeneSum / countHygene).ToString();
            lblQualityAvg.Text += (qualitySum / countQuality).ToString();

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
