using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Users.Doctors.Model;

namespace HealthCareSystem.Core.Users.Doctors
{
    interface IDoctorService
    {
        bool IsDoctorAvailable(int doctorID, DateTime ExaminationDateTime, List<Examination> examinations);
        List<Doctor> GetDoctorsByKeyword(List<Doctor> doctors, string keyword);
        bool IsKeywordInDoctor(string keyword, Doctor doctor);
    }
}
