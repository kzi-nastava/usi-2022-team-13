using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Examinations.Repository;
using HealthCareSystem.Core.Rooms.Repository;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Doctors.Repository;
using HealthCareSystem.Core.Users.Patients.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    public partial class PatientRecommendation : Form
    {

        public string Username { get; set; }
        private readonly PatientRepository _patientRepository;
        private readonly DoctorRepository _doctorRepository;
        private readonly IExaminationRepository _examinationRepository;
        private DateTime _examinationFinalDate;
        private Doctor _selectedDoctor;
        private string _startTime, _endTime;
        private bool _isDoctorPriority;

        public PatientRecommendation(string username)
        {
            Username = username;
            _patientRepository = new PatientRepository(Username);
            _doctorRepository = new DoctorRepository();
            _examinationRepository = new ExaminationRepository();
            _isDoctorPriority = true;

            
            InitializeComponent();
            
            FillDoctorsComboBox();
        }
        
        private void FillDoctorsComboBox()
        {

            BindingList<Doctor> doctors = _doctorRepository.GetDoctors();

            cbDoctors.ValueMember = null;
            cbDoctors.DropDownStyle = ComboBoxStyle.DropDownList;

            cbDoctors.DisplayMember = "FullName";
            cbDoctors.DataSource = doctors;

        }
        private bool CheckSelectedValues()
        {
            var regex = @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
            DateTime startDateTime = TimeDateHelpers.GetMergedDateTime(DateTime.Now, _startTime);
            DateTime endDateTime = TimeDateHelpers.GetMergedDateTime(_examinationFinalDate, _endTime);

            if (!IsTimeValid(regex)) return false;
            else if (!TimeDateHelpers.IsValidExaminationDate(_examinationFinalDate))
            {
                MessageBox.Show("Examination date must be after current time.");
                return false;
            }
            
            return true;
        }

        

        private bool IsTimeValid(string regex)
        {
 
            var startMatch = System.Text.RegularExpressions.Regex.Match(_startTime, regex);
            var endMatch = System.Text.RegularExpressions.Regex.Match(_endTime, regex);
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            if (!startMatch.Success || !endMatch.Success)
            {
                MessageBox.Show("Invalid time format. Please enter like: HH:MM ");
                return false;
            }
            try
            {
                start = DateTime.ParseExact(_startTime, "HH:mm", CultureInfo.InvariantCulture);
                end = DateTime.ParseExact(_endTime, "HH:mm", CultureInfo.InvariantCulture);
            }catch(Exception)
            {
                MessageBox.Show("Time Format Must be HH:mm !");
                return false;
            }
            if(start.CompareTo(end) > 0)
            {
                MessageBox.Show("Start time is after end time. Please change it.");
                return false;
            }
            return true;
        }

        private void SetSelectedValues()
        {
            _selectedDoctor = (Doctor)cbDoctors.SelectedValue;
            _examinationFinalDate = dtLatestDate.Value;
            _startTime = tbStartTime.Text;
            _endTime = tbEndTime.Text;
            if (rbDoctor.Checked == true) _isDoctorPriority = true;
            else _isDoctorPriority = false;
        }

        private void rbDoctor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDoctor.Checked == true) _isDoctorPriority = true;
            else _isDoctorPriority = false;
 
        }

        private void rbTime_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTime.Checked == true) _isDoctorPriority = false;
            else _isDoctorPriority = true;
        }

        private void btnAppoint_Click(object sender, EventArgs e)
        {
            if (CanAddExamination())
            {
                var selectedRow = dgwRecommendations.SelectedRows[0];

                _examinationRepository.InsertExamination(_patientRepository.GetPatientId(), (int)selectedRow.Cells[0].Value, (DateTime)selectedRow.Cells[2].Value, 15, (int)selectedRow.Cells[4].Value);
                MessageBox.Show("Successfully added examination!");
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            SetSelectedValues();

            if (CheckSelectedValues())
            {
                List<Examination> examinations = _examinationRepository.GetRecommendedExaminations(_selectedDoctor, _startTime, _endTime, _examinationFinalDate, _isDoctorPriority);
                dgwRecommendations.DataSource = examinations;
            }
        }
        private bool CanAddExamination()
        {
            return GUIHelpers.IsDgwRowSelected(dgwRecommendations);
            
        }
    }
}
