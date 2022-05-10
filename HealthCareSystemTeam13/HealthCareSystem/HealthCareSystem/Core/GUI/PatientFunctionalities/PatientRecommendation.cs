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
        PatientRepository PatientRep;
        DoctorRepository DoctorRep;
        RoomRepository RoomRep;
        ExaminationRepository ExaminationRep;
        private DateTime ExaminationFinalDate;
        private Doctor SelectedDoctor;
        private string StartTime, EndTime;
        private bool IsDoctorPriority;

        public PatientRecommendation(string username)
        {
            Username = username;
            PatientRep = new PatientRepository(Username);
            DoctorRep = new DoctorRepository();
            RoomRep = new RoomRepository();
            ExaminationRep = new ExaminationRepository();
            IsDoctorPriority = true;

            
            InitializeComponent();
            //DataGridViewSettings();
            FillDoctorsComboBox();
        }
        private void DataGridViewSettings()
        {

            dgwRecommendations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgwRecommendations.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgwRecommendations.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgwRecommendations.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgwRecommendations.Columns[0].Width = 90;
            dgwRecommendations.Columns[3].Width = 90;
            dgwRecommendations.Columns[4].Width = 90;
            dgwRecommendations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgwRecommendations.MultiSelect = false;

        }
        private void FillDoctorsComboBox()
        {

            BindingList<Doctor> doctors = DoctorRep.GetDoctors();

            cbDoctors.ValueMember = null;
            cbDoctors.DropDownStyle = ComboBoxStyle.DropDownList;

            cbDoctors.DisplayMember = "FullName";
            cbDoctors.DataSource = doctors;

        }
        private bool CheckSelectedValues()
        {
            var regex = @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
            DateTime startDateTime = GetMergedDateTime(DateTime.Now, StartTime);
            DateTime endDateTime = GetMergedDateTime(ExaminationFinalDate, EndTime);

            if (!IsTimeValid(regex)) return false;
            else if (!IsDateValid()) return false;
            
            return true;
        }

        private bool IsDateValid()
        {
            if (ExaminationFinalDate <= DateTime.Now)
            {
                MessageBox.Show("Examination date must be after current time.");
                return false;

            }
            return true;
        }

        private bool IsTimeValid(string regex)
        {
 
            var startMatch = System.Text.RegularExpressions.Regex.Match(StartTime, regex);
            var endMatch = System.Text.RegularExpressions.Regex.Match(EndTime, regex);
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            if (!startMatch.Success || !endMatch.Success)
            {
                MessageBox.Show("Invalid time format. Please enter like: HH:MM ");
                return false;
            }
            try
            {
                start = DateTime.ParseExact(StartTime, "HH:mm", CultureInfo.InvariantCulture);
                end = DateTime.ParseExact(EndTime, "HH:mm", CultureInfo.InvariantCulture);
            }catch(Exception exception)
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
            SelectedDoctor = (Doctor)cbDoctors.SelectedValue;
            ExaminationFinalDate = dtLatestDate.Value;
            StartTime = tbStartTime.Text;
            EndTime = tbEndTime.Text;
            if (rbDoctor.Checked == true) IsDoctorPriority = true;
            else IsDoctorPriority = false;
        }
        private void PatientRecommendation_Load(object sender, EventArgs e)
        {

        }

        private static DateTime GetMergedDateTime(DateTime examinationDate, string examinationTime)
        {
            string[] examinationHourMinute = examinationTime.Split(':');
            DateTime examinationDateTime = new DateTime(examinationDate.Year, examinationDate.Month, examinationDate.Day, Convert.ToInt32(examinationHourMinute[0]), Convert.ToInt32(examinationHourMinute[1]), 0);
            return examinationDateTime;
        }

        private void rbDoctor_CheckedChanged(object sender, EventArgs e)
        {
            if (rbDoctor.Checked == true) IsDoctorPriority = true;
            else IsDoctorPriority = false;
 
        }

        private void rbTime_CheckedChanged(object sender, EventArgs e)
        {
            if (rbTime.Checked == true) IsDoctorPriority = false;
            else IsDoctorPriority = true;
        }

        private void btnAppoint_Click(object sender, EventArgs e)
        {
            if (CanAddExamination())
            {

                var selectedRow = dgwRecommendations.SelectedRows[0];

                PatientRep.InsertExamination(Username, (int)selectedRow.Cells[0].Value, (DateTime)selectedRow.Cells[2].Value, 15, (int)selectedRow.Cells[4].Value);
                MessageBox.Show("Successfully added examination!");
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            SetSelectedValues();

            bool valid = CheckSelectedValues();

            if (valid)
            {
                List<Examination> examinations = ExaminationRep.GetRecommendedExaminations(SelectedDoctor, StartTime, EndTime, ExaminationFinalDate, IsDoctorPriority);
                dgwRecommendations.DataSource = examinations;
            }
        }
        private bool CanAddExamination()
        {
            if (dgwRecommendations.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a row first.");

            }
            else if (dgwRecommendations.SelectedRows.Count == 1)
            {
                DataGridViewRow row = dgwRecommendations.SelectedRows[0];
                if (row.Cells[0].Value == null)
                {
                    MessageBox.Show("You selected an empty row.");
                }
                else
                {
                    return true;
                }
            }
            else
            {
                MessageBox.Show("Please select only 1 row.");
            }
            return false;
        }
    }
}
