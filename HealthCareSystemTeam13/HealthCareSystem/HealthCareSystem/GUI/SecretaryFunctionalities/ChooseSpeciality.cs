using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Users.Doctors.Model;
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
using HealthCareSystem.Core.Examinations.Repository;
using HealthCareSystem.Core.Rooms.Repository;

namespace HealthCareSystem.Core.GUI.SecretaryFunctionalities
{
    public partial class ChooseSpeciality : Form
    {
        private int PatientID;
        ExaminationRepository _examinationRepository;
        RoomRepository _roomRepository;
        public ChooseSpeciality(int patientID)
        {
            InitializeComponent();
            FillSpecialityComboBox();
            PatientID = patientID;
            _examinationRepository = new ExaminationRepository();
            _roomRepository = new RoomRepository();
        }

        public void FillSpecialityComboBox()
        {
            List<DoctorSpeciality> specialities = Enum.GetValues(typeof(DoctorSpeciality)).Cast<DoctorSpeciality>().ToList();
            specialityComboBox.ValueMember = null;
            specialityComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            specialityComboBox.DisplayMember = "Speciality";
            specialityComboBox.DataSource = specialities;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            Tuple<string, DateTime> availableTimeAndDoctor =  _examinationRepository.AvailableExamination((DoctorSpeciality)specialityComboBox.SelectedValue, Convert.ToInt32(durationBox.Text)) ;
            if(availableTimeAndDoctor.Item1 == "none")
            {
                UrgentExaminations urgentExaminations = new UrgentExaminations(PatientID, (DoctorSpeciality)specialityComboBox.SelectedValue, Convert.ToInt32(durationBox.Text));
                urgentExaminations.ShowDialog();
            } 
            else
            {
                TypeOfExamination typeOfExamiantion = TypeOfExamination.BasicExamination;
                if(Convert.ToInt32(durationBox.Text) > 15)
                {
                    typeOfExamiantion = TypeOfExamination.Operation;
                }
                int roomId = _roomRepository.GetAvailableRoom(availableTimeAndDoctor.Item2, Convert.ToInt32(durationBox.Text));
                if(roomId == 0)
                {
                    UrgentExaminations urgentExaminations = new UrgentExaminations(PatientID, (DoctorSpeciality)specialityComboBox.SelectedValue, Convert.ToInt32(durationBox.Text));
                    urgentExaminations.ShowDialog();
                }    
                _examinationRepository.InsertSingleExamination(new Examination(Convert.ToInt32(availableTimeAndDoctor.Item1), PatientID, false, false, false, availableTimeAndDoctor.Item2, typeOfExamiantion, true, roomId, Convert.ToInt32(durationBox.Text)));
            }
        }
    }
}
