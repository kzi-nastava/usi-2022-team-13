using HealthCareSystem.Core.Examinations.Repository;
using HealthCareSystem.Core.Rooms.Repository;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Doctors.Repository;
using HealthCareSystem.Core.Users.Doctors.Service;
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
        private readonly IPatientRepository _patientRepository;
        private DoctorRepository _doctorRep;
        private RoomRepository _roomRep;
        private ExaminationRepository _examinationRep;
        private Doctor _doctorEntity;
        public string DoctorUsername;
        public readonly int ValidDate;
        public string PatientUsername;

        public InsertOrChangeExamination(int examinationId, bool isAddChoosen, string doctorUsername, int validDate)
        {
            ExaminationId = examinationId;
            IsAddChoosen = isAddChoosen;
            DoctorUsername = doctorUsername;
            ValidDate = validDate;
            _patientRepository = new PatientRepository();
            _doctorRep = new DoctorRepository(doctorUsername, true);
            _doctorRep.Username = DoctorUsername;
            _doctorEntity = _doctorRep.GetDoctorByUsername();

            _roomRep = new RoomRepository();
            _examinationRep = new ExaminationRepository();

            InitializeComponent();

            FillPatientsComboBox();
            FillTypesComboBox();
            if (!IsAddChoosen)
            {
                LoadEditData();

            }
            else
            {
                tbDuration.Text = "15";
            }


        }

        private void FillPatientsComboBox()
        {
            // Using BindingList so that i can display the name of the doctor, and have his id in the back
            BindingList<Patient> patients = _patientRepository.GetPatients();

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
                    _patientRepository.SetUsername(PatientUsername);
                    _examinationRep.InsertExamination(_patientRepository.GetPatientId(), _doctorEntity.ID, mergedTime, Duration, RoomId, selectedType);
                    MessageBox.Show("Successfully added examination!");

                }
                else
                {
                    UpdateContent(mergedTime);
                }
                this.Close();
            }
        }

        private void UpdateContent(DateTime mergedTime)
        {
            int patiendId = _patientRepository.GetPatientIdByFirstName(
                cbPatients.Text.Split(' ')[0]);

            string updateQuery = "Update Examination set id_patient = " + patiendId + "," +
                " isEdited=" + true + ", dateOf = '" + mergedTime + "', typeOfExamination = '" + cbType.SelectedItem.ToString() + "', " +
                "id_room = " + RoomId + " where id = " + ExaminationId + "";
            _patientRepository.UpdatePatientContent(updateQuery, patiendId);
            MessageBox.Show("Successfully edited examination!");

        }

        private void SetSelectedValues()
        {
            // napraviti metodu u patient repositorijumu da se preko patient entiteta nadje username iz user tabele i onda malo edit doradis i tjt
            SelectedPatient = (Patient)cbPatients.SelectedValue;
            PatientUsername = _patientRepository.GetUsernameFromPatient(SelectedPatient);
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
            else if (!DoctorService.IsDoctorAvailable(_doctorEntity.ID, GetMergedDateTime(ExaminationDate, time),
                _examinationRep.GetAllOtherExaminations(ExaminationId)))
            {

                MessageBox.Show("Doctor is not available at that time.");
                return false;

            }
            else if (!match.Success)
            {

                MessageBox.Show("Invalid time format. Please enter like: HH:MM ");
                return false;

            }
            else if (!_roomRep.IsRoomAvailable(Convert.ToInt32(tbRoomId.Text), GetMergedDateTime(ExaminationDate, time),
                _examinationRep.GetAllOtherExaminations(ExaminationId)))
            {

                int availableRoomId = _roomRep.GetAvailableRoomId(GetMergedDateTime(ExaminationDate, time), _examinationRep.GetAllOtherExaminations(ExaminationId));
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

        private void LoadEditData()
        {
            Dictionary<string, string> data = _examinationRep.GetExamination(ExaminationId);
            tbExaminationId.Text = data["id"];

            tbDuration.Text = "15";
            tbRoomId.Text = data["room_id"];
            DateTime dateParsed = DateTime.Parse(data["dateOf"]);

            dtDate.Value = dateParsed;
            tbTime.Text = dateParsed.Hour.ToString() + ":" + dateParsed.Minute.ToString();
            string patientName = GetSelectedPatient().FullName.ToString();
            cbPatients.SelectedIndex = cbPatients.FindString(patientName);
            cbType.SelectedIndex = cbType.FindStringExact(data["typeOfExamination"]);
        }

        private Patient GetSelectedPatient()
        {
            Patient patient;

            string doctorQuery = "select * from Patients inner join Examination on Patients.ID = Examination.id_patient where Examination.ID = " + ExaminationId;
            
            patient = _patientRepository.GetSelectedPatient(doctorQuery);
            return patient;
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
