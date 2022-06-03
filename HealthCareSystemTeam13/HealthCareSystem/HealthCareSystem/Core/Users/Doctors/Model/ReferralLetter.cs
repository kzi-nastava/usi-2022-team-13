using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Users.Patients.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Doctors.Model
{
    class ReferralLetter
    {
        public int CurrentDoctorID { get; set; }
        public int CurrentPatientID { get; set; }
        public int ForwardedDoctorID { get; set; }
        public TypeOfExamination ExaminationType { get; set; }
        public DoctorSpeciality Speciality { get; set; }
        public ReferralLetter(int currentDoctorID, int currentPatientID, int forwardedDoctorID, TypeOfExamination examinationType, DoctorSpeciality speciality)
        {
            CurrentDoctorID = currentDoctorID;
            CurrentPatientID = currentPatientID;
            ForwardedDoctorID = forwardedDoctorID;
            ExaminationType = examinationType;
            Speciality = speciality;
        }
        public ReferralLetter(int currentDoctorID,
            int currentPatientID, int forwardedDoctorID, TypeOfExamination examinationType)
        {
            CurrentDoctorID = currentDoctorID;
            CurrentPatientID = currentPatientID;
            ForwardedDoctorID = forwardedDoctorID;
            ExaminationType = examinationType;
        }

        public ReferralLetter(int currentDoctorID,
            int currentPatientID, TypeOfExamination examinationType, DoctorSpeciality speciality)
        {
            CurrentDoctorID = currentDoctorID;
            CurrentPatientID = currentPatientID;
            ExaminationType = examinationType;
            Speciality = speciality;
        }



    }
}
