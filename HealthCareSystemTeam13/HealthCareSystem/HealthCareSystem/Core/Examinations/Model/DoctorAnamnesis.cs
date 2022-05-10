using HealthCareSystem.Core.Users.Doctors.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Examinations.Model
{
    class DoctorAnamnesis
    {
        public int ExaminationId { get; set; }
        public string Notice { get; set; }
        public string Conclusions { get; set; }
        public DateTime DateOfExamination { get; set; }
        public string DoctorName { get; set; }
        public string DoctorSpeciality { get; set; }

        public DoctorAnamnesis(int examinationId, string notice, string conclusions, DateTime dateOfExamination, string doctorName, string doctorSpeciality)
        {
            ExaminationId = examinationId;
            Notice = notice;
            Conclusions = conclusions;
            DateOfExamination = dateOfExamination;
            DoctorName = doctorName;
            DoctorSpeciality = doctorSpeciality;
        }

     
    }
}
