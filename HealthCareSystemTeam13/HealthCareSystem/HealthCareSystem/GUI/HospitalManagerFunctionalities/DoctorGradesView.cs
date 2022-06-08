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
    public partial class DoctorGradesView : Form
    {
        private int DoctorId { get; set; }
        private SurveyRepository SurveyRepository;
        private DoctorRepository DoctorRepository;
        public DoctorGradesView(int doctorId)
        {
            SurveyRepository = new SurveyRepository();
            DoctorRepository = new DoctorRepository();
            DoctorId = doctorId;
            InitializeComponent();
            FillLabels();
        }

        private void FillLabels()
        {
            BindingList<Doctor> doctors = DoctorRepository.GetDoctors();

            string doctorFullName = "";
            foreach (Doctor doctor in doctors)
            {
                if(doctor.ID == DoctorId)
                {
                    doctorFullName = doctor.FullName; 
                }
            }


            lblDoctor.Text += DoctorId + "  " + doctorFullName;
            List<DoctorSurvey> doctorSurveys = SurveyRepository.GetDoctorSurveys();
            double[] avgGrades = SurveyService.GetAverageDoctorGrades(doctorSurveys, DoctorId);
            
            int[] numberDoctorGrades = SurveyService.GetNumberOfDoctorGrades(doctorSurveys, true, DoctorId);
            int[] numberQualityGrades = SurveyService.GetNumberOfDoctorGrades(doctorSurveys, false, DoctorId);

            Label[] gradeLabels = { lblDoctor1, lblDoctor2, lblDoctor3, lblDoctor4, lblDoctor5 };
            Label[] qualityLabels = { lblQuality1, lblQuality2, lblQuality3, lblQuality4, lblQuality5};

            lblAvgDoctorGrade.Text += avgGrades[0].ToString();
            lblAvgQuality.Text += avgGrades[1].ToString();

            for(int i = 0; i < 5; i++)
            {
                gradeLabels[i].Text += numberDoctorGrades[i].ToString();
                qualityLabels[i].Text += numberQualityGrades[i].ToString();            
            }
            

        }

        private void DoctorGradesView_Load(object sender, EventArgs e)
        {

        }
    }
}
