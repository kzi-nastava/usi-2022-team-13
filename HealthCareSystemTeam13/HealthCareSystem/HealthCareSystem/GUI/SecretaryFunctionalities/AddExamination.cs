using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Examinations.Repository;
using HealthCareSystem.Core.Rooms.Repository;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Doctors.Repository;
using HealthCareSystem.Core.Users.Doctors.Service;
using HealthCareSystem.Core.Users.Secretaries.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core.Users.Doctors;

namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    partial class AddExamination : Form
    {
        private IRoomRepository _roomRepository;
        private IExaminationRepository _examinationRepository;
 
        private ReferralLetter ChosenReferralLetter;
        private IDoctorService _doctorService;

        public AddExamination(ReferralLetter referralLetter)
        {
            InitializeComponent();
            ChosenReferralLetter = referralLetter;
            _roomRepository = new RoomRepository();
            _examinationRepository = new ExaminationRepository();
            _doctorService = new DoctorService();


        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (CheckSelectedValues())
            {
                Examination examination = new Examination(ChosenReferralLetter.ForwardedDoctorID, ChosenReferralLetter.CurrentPatientID, false, false, false, TimeDateHelpers.GetMergedDateTime(dateTimeBox.Value, timeBox.Text), ChosenReferralLetter.ExaminationType, false, Convert.ToInt32(roomIdBox.Text), Convert.ToInt32(durationBox.Text));
                _examinationRepository.InsertSingleExamination(examination);
            }
        }

        private bool CheckSelectedValues()
        {
            var regex = @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
            string time = timeBox.Text;

            List<Examination> otherExaminations = _examinationRepository.GetAllExaminations();
            DateTime mergedExaminationTime = TimeDateHelpers.GetMergedDateTime(dateTimeBox.Value, time);
            var match = System.Text.RegularExpressions.Regex.Match(timeBox.Text, regex);

            if (dateTimeBox.Value <= DateTime.Now)
            {

                MessageBox.Show("Examination date must be after current time.");
                return false;

            }
            else if (!_doctorService.IsDoctorAvailable(ChosenReferralLetter.ForwardedDoctorID, mergedExaminationTime, otherExaminations))
            {

                MessageBox.Show("Doctor is not available at that time.");
                return false;

            }
            else if (!match.Success)
            {

                MessageBox.Show("Invalid time format. Please enter like: HH:MM ");
                return false;

            }
            else if (!_roomRepository.IsRoomAvailable(Convert.ToInt32(roomIdBox.Text), mergedExaminationTime, otherExaminations))
            {

                int availableRoomId = _roomRepository.GetAvailableRoomId(mergedExaminationTime, otherExaminations);
                if (availableRoomId == 0)
                {
                    MessageBox.Show("No available rooms at this date/time.");
                    return false;
                }

            }

            return true;
        }

    }
}
