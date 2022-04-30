using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core;
using HealthCareSystem.Core.Examinations.Repository;
using HealthCareSystem.Core.Rooms.Repository;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Doctors.Repository;
<<<<<<< HEAD
using HealthCareSystem.Core.Users.Patients.Model;
=======
>>>>>>> feature/Doctor
using HealthCareSystem.Core.Users.Patients.Repository;

namespace HealthCareSystem.Core.GUI.PatientFunctionalities
{
    public partial class AddEditExamination : Form
    {
        public int ExaminationId { get; set; }
        public DateTime ExaminationDate { get; set; }
        private Doctor SelectedDoctor { get; set; }
        private int roomId { get; set; }
        private int duration { get; set; }
        public bool IsAddChoosen { get; set; }
        private PatientRepository PatientRep;
        private DoctorRepository DoctorRep;
        private RoomRepository RoomRep;
        private ExaminationRepository ExaminationRep;
        private string selectedTime;
        private string PatientUsername;
        private int ValidDate;

        public AddEditExamination(int examinationId, bool isAddChoosen, string patientUsername, int validDate)
        {
            ExaminationId = examinationId;
            IsAddChoosen = isAddChoosen;
            PatientUsername = patientUsername;
            ValidDate = validDate;
            PatientRep = new PatientRepository(patientUsername);
            DoctorRep = new DoctorRepository();
            RoomRep = new RoomRepository();
            ExaminationRep = new ExaminationRepository();


            InitializeComponent();

            FillDoctorsComboBox();
            if (!IsAddChoosen)
            {
                LoadEditData();

            }
            else
            {
                tbDuration.Text = "15";
            }

           
        }
        private void LoadEditData()
        {
            Dictionary<string, string> data = PatientRep.GetExamination(ExaminationId);
            tbExaminationId.Text = data["id"];

            tbDuration.Text = "15";
            tbRoomId.Text = data["room_id"];
            DateTime dateParsed = DateTime.Parse(data["dateOf"]);
            
            dtDate.Value = dateParsed;
            tbTime.Text = dateParsed.Hour.ToString() + ":" + dateParsed.Minute.ToString();
            cbDoctors.SelectedIndex = cbDoctors.FindStringExact(GetSelectedDoctor().FullName);

        }

        private void FillDoctorsComboBox()
        {
            // Using BindingList so that i can display the name of the doctor, and have his id in the back
            BindingList<Doctor> doctors = DoctorRep.GetDoctors();

            cbDoctors.ValueMember = null;
            cbDoctors.DropDownStyle = ComboBoxStyle.DropDownList;
            
            cbDoctors.DisplayMember = "FullName";
            cbDoctors.DataSource = doctors;
            
        }

        private Doctor GetSelectedDoctor()
        {
            Doctor doctor;
            string doctorQuery = "select * from Doctors inner join Examination on Doctors.id = Examination.id_doctor where Examination.id = " + ExaminationId + "";
            doctor = PatientRep.GetSelectedDoctor(doctorQuery);

            return doctor;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            SetSelectedValues();
            if (tbRoomId.Text == "") tbRoomId.Text = "0";

            bool isValid = CheckSelectedValues();
            
            if (isValid)
            {
                // check for block
                string time = tbTime.Text;
                DateTime mergedTime = GetMergedDateTime(ExaminationDate, time);
                if (IsAddChoosen)
                {
                    
                    PatientRep.InsertExamination(PatientUsername, SelectedDoctor.ID, mergedTime, duration, roomId);
                    MessageBox.Show("Successfully added examination!");
                    
                }
                else
                {

                    if (ValidDate == 1)
                    {
                        UpdateContent(mergedTime);
                    }
                    else
                    {
                        PatientRep.SendExaminationEditRequest(ExaminationId, DateTime.Now, true, SelectedDoctor.ID, mergedTime, roomId);
<<<<<<< HEAD
                        PatientRep.InsertExaminationChanges(TypeOfChange.Edit);
=======

>>>>>>> feature/Doctor
                        MessageBox.Show("Wait for a secretary to aproove this request.");
                    }
                }
                DatabaseHelpers.BlockSpamPatients(PatientUsername, PatientRep.Connection);
                this.Close();
            } 
        }

        private void UpdateContent(DateTime mergedTime)
        {
            string updateQuery = "Update Examination set id_doctor = " + SelectedDoctor.ID + ", isEdited=" + true + ", dateOf = '" + mergedTime + "', id_room = " + roomId + " where id = " + ExaminationId + "";
            PatientRep.UpdateContent(updateQuery);
            MessageBox.Show("Successfully edited examination!");

        }

        private bool CheckSelectedValues()
        {
            var regex = @"^([0-1]?[0-9]|2[0-3]):[0-5][0-9]$";
            string time = tbTime.Text;

            var match = System.Text.RegularExpressions.Regex.Match(tbTime.Text, regex);
            if (ExaminationDate <= DateTime.Now)
            {

                MessageBox.Show("Examination date must be after current time.");
                return false;

            }
            else if (!DoctorRep.IsDoctorAvailable(SelectedDoctor, GetMergedDateTime(ExaminationDate, time), ExaminationRep.GetAllOtherExaminations(ExaminationId)))
            {

                MessageBox.Show("Doctor is not available at that time.");
                return false;

            }
            else if (!match.Success)
            {

                MessageBox.Show("Invalid time format. Please enter like: HH:MM ");
                return false;

            }
            else if (!RoomRep.isRoomAvailable(Convert.ToInt32(tbRoomId.Text), GetMergedDateTime(ExaminationDate, time), ExaminationRep.GetAllOtherExaminations(ExaminationId)))
            {

                int availableRoomId = RoomRep.GetAvailableRoomId(GetMergedDateTime(ExaminationDate, time), ExaminationRep.GetAllOtherExaminations(ExaminationId));
                if (availableRoomId == 0)
                {
                    MessageBox.Show("No available rooms at this date/time.");
                    return false;
                }
                roomId = availableRoomId;

            }



            return true;
        }

        private void SetSelectedValues()
        {
            SelectedDoctor = (Doctor)cbDoctors.SelectedValue;
            if (tbRoomId.Text != "") { roomId = Convert.ToInt32(tbRoomId.Text); }
            else { roomId = 0; }
            duration = Convert.ToInt32(tbDuration.Text);
            ExaminationDate = dtDate.Value;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tbRoomId_TextChanged(object sender, EventArgs e)
        {

        }

        private void AddEditExamination_Load(object sender, EventArgs e)
        {

        }
        private static DateTime GetMergedDateTime(DateTime examinationDate, string examinationTime)
        {
            string[] examinationHourMinute = examinationTime.Split(':');
            DateTime examinationDateTime = new DateTime(examinationDate.Year, examinationDate.Month, examinationDate.Day, Convert.ToInt32(examinationHourMinute[0]), Convert.ToInt32(examinationHourMinute[1]), 0);
            return examinationDateTime;
        }

        private void AddEditExamination_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
    }
}
