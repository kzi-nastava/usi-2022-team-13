using HealthCareSystem.Core.Examinations.Repository;
using HealthCareSystem.Core.Rooms.Repository;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Doctors.Repository;
using HealthCareSystem.Core.Users.Patients.Model;
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

namespace HealthCareSystem.Core.GUI.DoctorsFunctionalities
{
    public partial class InsertOrChangeExamination : Form
    {
        public int ExaminationId { get; set; }
        public DateTime ExaminationDate { get; set; }
        private Patient SelectedPatient { get; set; }

        private int RoomId { get; set; }
        private int Duration { get; set; }
        public bool IsAddChoosen { get; set; }
        private PatientRepository PatientRep;
        private DoctorRepository DoctorRep;
        private RoomRepository RoomRep;
        private ExaminationRepository ExaminationRep;
        private Doctor DoctorEntity;
        private string DoctorUsername;
        private readonly int ValidDate;
        private string PatientUsername;

        public InsertOrChangeExamination(int examinationId, bool isAddChoosen, string doctorUsername, int validDate)
        {
            ExaminationId = examinationId;
            IsAddChoosen = isAddChoosen;
            DoctorUsername = doctorUsername;
            ValidDate = validDate;
            PatientRep = new PatientRepository();
            DoctorRep = new DoctorRepository(doctorUsername, true);
            DoctorRep.Username = DoctorUsername;
            DoctorEntity = DoctorRep.GetDoctorByUsername();

            RoomRep = new RoomRepository();
            ExaminationRep = new ExaminationRepository();

            InitializeComponent();

            FillPatientsComboBox();
            FillTypesComboBox();
            if (!IsAddChoosen)
            {
                //LoadEditData();

            }
            else
            {
                tbDuration.Text = "15";
            }


        }

        private void FillPatientsComboBox()
        {
            // Using BindingList so that i can display the name of the doctor, and have his id in the back
            BindingList<Patient> patients = PatientRep.GetPatients();

            cbPatients.ValueMember = null;
            cbPatients.DropDownStyle = ComboBoxStyle.DropDownList;

            cbPatients.DisplayMember = "FullName";
            cbPatients.DataSource = patients;
        }

        private void FillTypesComboBox()
        {
            BindingList<string> typeNames = new BindingList<string>{"BasicExamination", "Operation" };

            cbType.ValueMember = null;
            cbType.DropDownStyle = ComboBoxStyle.DropDownList;

            cbType.DataSource = typeNames;
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
                string selectedType = cbType.SelectedItem.ToString();
                if (IsAddChoosen)
                {
                    
                    PatientRep.InsertExamination(PatientUsername, DoctorEntity.ID, mergedTime, Duration, RoomId, selectedType);
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
                        PatientRep.SendExaminationEditRequest(ExaminationId, DateTime.Now, true, DoctorEntity.ID, mergedTime, RoomId);

                        MessageBox.Show("Wait for a secretary to aproove this request.");
                    }
                }
                this.Close();
            }
        }

        private void UpdateContent(DateTime mergedTime)
        {
            string updateQuery = "Update Examination set id_doctor = " + DoctorEntity.ID + "," +
                " isEdited=" + true + ", dateOf = '" + mergedTime + "', id_room = " + RoomId + " where id = " + ExaminationId + "";
            PatientRep.UpdateContent(updateQuery);
            MessageBox.Show("Successfully edited examination!");

        }

        private void SetSelectedValues()
        {
            // napraviti metodu u patient repositorijumu da se preko patient entiteta nadje username iz user tabele i onda malo edit doradis i tjt
            SelectedPatient = (Patient)cbPatients.SelectedValue;
            PatientUsername = PatientRep.GetUsernameFromEntity(SelectedPatient);
            if (tbRoomId.Text != "") { RoomId = Convert.ToInt32(tbRoomId.Text); }
            else { RoomId = 0; }
            Duration = Convert.ToInt32(tbDuration.Text);
            ExaminationDate = dtDate.Value;
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
            else if (!DoctorRep.IsDoctorAvailable(DoctorEntity, GetMergedDateTime(ExaminationDate, time), ExaminationRep.GetAllOtherExaminations(ExaminationId)))
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
                RoomId = availableRoomId;

            }



            return true;
        }

        private static DateTime GetMergedDateTime(DateTime examinationDate, string examinationTime)
        {
            string[] examinationHourMinute = examinationTime.Split(':');
            DateTime examinationDateTime = new DateTime(examinationDate.Year, examinationDate.Month, examinationDate.Day, Convert.ToInt32(examinationHourMinute[0]), Convert.ToInt32(examinationHourMinute[1]), 0);
            return examinationDateTime;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
