using HealthCareSystem.Core.Users.Patients.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core.Surveys.Repository;

namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    public partial class DoctorSurveyView : Form
    {
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        private SurveyRepository _surveyRepository;
        public DoctorSurveyView(int patientId, int doctorId)
        {
            this.PatientId = patientId;
            this.DoctorId = doctorId;
            _surveyRepository = new SurveyRepository();
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DoctorSurveyView_Load(object sender, EventArgs e)
        {
            SetInitialValues();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendSurvey();
            MessageBox.Show("Succesfully added a survey! Thanks for your help :)");
            this.Close();
        }
        private void SetInitialValues()
        {
            cbRating.SelectedIndex = 0;
            cbQuality.SelectedIndex = 0;
            rbReccomendNo.Checked = true;
        }
        private void SendSurvey()
        {
            int rating = Convert.ToInt32(cbRating.SelectedItem);
            int quality = Convert.ToInt32(cbQuality.SelectedItem);

            bool reccomend = rbReccomendNo.Checked == true ? false : true;
            string comment = rtbComment.Text;

            _surveyRepository.AddDoctorSurvey(DoctorId, PatientId, rating, quality, reccomend, comment);
        }
    }
}
