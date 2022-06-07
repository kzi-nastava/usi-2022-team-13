using HealthCareSystem.Core.Surveys;
using HealthCareSystem.Core.Surveys.Repository;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Doctors.Repository;
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
        private DoctorRepository DoctorRepository;
        public DoctorSurveys()
        {
            SurveyRepository = new SurveyRepository();
            DoctorRepository = new DoctorRepository();
            SurveyRepository.PullDoctorSurveys();
            InitializeComponent();
            FillDataGridView();
            FillLabels();
        }
        private void FillLabels()
        {
            List<DoctorSurvey> doctorSurveys = SurveyRepository.GetDoctorSurveys();
            BindingList<Doctor> doctors = DoctorRepository.GetDoctors();
            Dictionary<Doctor, double> rankings = SurveyService.GetDoctorsAndAverages(doctorSurveys, doctors);
            Label[] labelsBest = { lblBest1, lblBest2, lblBest3};
            Label[] labelsWorst = { lblWorst1, lblWorst2, lblWorst3};


            for(int i = 0; i < 3; i++)
            {
                labelsBest[i].Text += rankings.OrderBy(key => key.Value).ElementAt(i).Key.FullName + " " + rankings.OrderByDescending(key => key.Value).ElementAt(i).Value;
            }

            for (int i = 0; i < 3; i++)
            {
                labelsWorst[i].Text += rankings.OrderBy(key => key.Value).ElementAt(i).Key.FullName + " " + rankings.OrderBy(key => key.Value).ElementAt(i).Value;
            }
          
        }

        private void DataGridViewSettings()
        {
            dgwDoctorSurveys.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgwDoctorSurveys.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgwDoctorSurveys.MultiSelect = false;
            dgwDoctorSurveys.ReadOnly = true;
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

        private void btnGrades_Click(object sender, EventArgs e)
        {
            DoctorGradesView doctorGradesView = new DoctorGradesView((int)dgwDoctorSurveys.SelectedRows[0].Cells[1].Value);

            doctorGradesView.ShowDialog();
        }
    }
}
