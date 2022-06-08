using HealthCareSystem.Core.Examinations.Repository;
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

namespace HealthCareSystem.GUI.DoctorsFunctionalities
{
    public partial class RequestDaysOff : Form
    {
        private DoctorRepository _doctorRep;
        private ExaminationRepository _examinationRep;
        private DaysOffRepository _daysOffRep;

        public RequestDaysOff(string doctorUsername)
        {
            
            _doctorRep = new DoctorRepository(doctorUsername, true);
            _examinationRep = new ExaminationRepository();
            _doctorRep.Username = doctorUsername;
            _daysOffRep = new DaysOffRepository();
            _daysOffRep.PullRequestsForDaysOff(_doctorRep.GetDoctorId());

            InitializeComponent();

            FillDataGridView();


        }

        private void FillDataGridView()
        {

            dgwRequests.DataSource = _daysOffRep.RequestsForDaysOff;
            DataGridViewSettings();
        }

        private void DataGridViewSettings()
        {
            dgwRequests.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgwRequests.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgwRequests.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgwRequests.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgwRequests.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgwRequests.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgwRequests.Columns[0].Width = 90;
            dgwRequests.Columns[1].Width = 90;
            dgwRequests.Columns[2].Width = 90;
            dgwRequests.Columns[3].Width = 90;
            dgwRequests.Columns[4].Width = 90;
            dgwRequests.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgwRequests.MultiSelect = false;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStart.Value;
            DateTime endDate = dtpEnd.Value;

            if(rtbReasonForRequest.Text == "")
            {
                MessageBox.Show("You need to enter a reason for wanting to take days off!");
                return;
            }

            if(endDate.DayOfYear < startDate.DayOfYear)
            {
                MessageBox.Show("Starting date can't be after ending date!");
                return;
            }

            int dayDifference = startDate.DayOfYear - DateTime.Now.DayOfYear;
            if(dayDifference <= 2)
            {
                MessageBox.Show("You can't request days off that start less then two days from today!");
                return;
            }

            List<DateTime> examinationDates = _examinationRep.GetDateOfExaminationsForDoctor(_doctorRep.GetDoctorId());

            bool isUrgent = cbUrgent.Checked;

            if(isUrgent && endDate.DayOfYear - startDate.DayOfYear > 5)
            {
                MessageBox.Show("Urgent request for days off can last 5 days at most!");
                return;
            }


            foreach (var examinationDate in examinationDates)
            {
                if(examinationDate.DayOfYear >= startDate.DayOfYear && examinationDate.DayOfYear <= endDate.DayOfYear)
                {
                    MessageBox.Show("You have an examination scheduled at that time period!");
                    return;
                } 
            }

            String reasonForDaysOff = rtbReasonForRequest.Text;

            _daysOffRep.InsertDaysOff(startDate, endDate, reasonForDaysOff, isUrgent, _doctorRep.GetDoctorId());

            MessageBox.Show("Successfully created a request for taking days off!");

            _daysOffRep.PullRequestsForDaysOff(_doctorRep.GetDoctorId());
            FillDataGridView();



        }
    }
}
