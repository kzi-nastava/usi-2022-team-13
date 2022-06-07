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
        private DoctorRepository DoctorRep;

        public RequestDaysOff(string doctorUsername)
        {
            InitializeComponent();
            DoctorRep = new DoctorRepository(doctorUsername, true);
            DoctorRep.Username = doctorUsername;
            

        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStart.Value;
            DateTime endDate = dtpEnd.Value;
            int dayDifference = startDate.DayOfYear - DateTime.Now.DayOfYear;
            if(dayDifference <= 2)
            {
                MessageBox.Show("You can't request days off that start less then two days from today!");
                return;
            }

            List<DateTime> examinationDates = DoctorRep.GetDateOfExaminationsForDoctor();

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

            DoctorRep.InsertDaysOff(startDate, endDate, reasonForDaysOff, isUrgent, DoctorRep.GetDoctorId());




        }
    }
}
