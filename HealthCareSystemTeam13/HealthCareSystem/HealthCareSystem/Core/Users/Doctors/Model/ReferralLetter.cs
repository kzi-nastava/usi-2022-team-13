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
        public Doctor CurrentDoctor { get; set; }
        public Patient CurrentPatient { get; set; }
        public Doctor ForwardedDoctor { get; set; }
        public TypeOfExamination ExaminationType { get; set; }
        public ReferralLetter(Doctor currentDoctor, Patient currentPatient, Doctor forwardedDoctor, TypeOfExamination examinationType)
        {
            CurrentDoctor = currentDoctor;
            CurrentPatient = currentPatient;
            ForwardedDoctor = forwardedDoctor;
            ExaminationType = examinationType;
        }


    }
}
