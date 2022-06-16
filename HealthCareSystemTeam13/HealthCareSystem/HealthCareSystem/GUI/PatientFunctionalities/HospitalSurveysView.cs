using HealthCareSystem.Core.Surveys.HospitalSurveys.Model;
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
    public partial class HospitalSurveysView : Form
    {
        public string Username { get; set; }
        private readonly SurveyRepository _surveyRepository;
        private HospitalSurvey _hospitalSurvey;
        private readonly IPatientRepository _patientRepository;
        public HospitalSurveysView(string username)
        {
            this.Username = username;
            _surveyRepository = new SurveyRepository();
            _patientRepository = new PatientRepository(username);
            InitializeComponent();
        }

        private void HospitalSurveysView_Load(object sender, EventArgs e)
        {
            SetInitialValues();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SetValuesForSurvey();
            SendSurvey();
            MessageBox.Show("Succesfully added a survey! Thanks for your help :)");
            this.Close();
        }
        private void SetInitialValues()
        {
            cbQuality.SelectedIndex = 0;
            cbHygiene.SelectedIndex = 0;
            rbReccomendNo.Checked = true;
            rbSatisfiedNo.Checked = true;
        }
        private void SetValuesForSurvey()
        {
            int quality = Convert.ToInt32(cbQuality.SelectedItem);
            int hygiene = Convert.ToInt32(cbHygiene.SelectedItem);
            int satisfied = rbSatisfiedNo.Checked == true ? 0 : 1;
            int recommend = rbReccomendNo.Checked == true ? 0 : 1;
            string comment = rtbComment.Text;
             _hospitalSurvey = new HospitalSurvey(quality, hygiene, satisfied, recommend, comment);
        }
        private void SendSurvey()
        {
            _surveyRepository.AddHospitalSurvey(this._hospitalSurvey, _patientRepository.GetPatientId());
        }

        private void btnSend_MouseEnter(object sender, EventArgs e)
        {
            btnSend.ForeColor = Color.Black;
            btnSend.BackColor = Color.White;
        }

        private void btnCancel_MouseEnter(object sender, EventArgs e)
        {
            btnSend.ForeColor = Color.Black;
            btnSend.BackColor = Color.White;
        }

        private void btnSend_MouseLeave(object sender, EventArgs e)
        {
            btnSend.ForeColor = Color.White;
            btnSend.BackColor = Color.Transparent;
        }

        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            btnSend.ForeColor = Color.White;
            btnSend.BackColor = Color.Transparent;
        }
    }
}
