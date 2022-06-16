using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Users.Doctors.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Doctors.Service
{
    class DoctorService:IDoctorService
    {
        public bool IsDoctorAvailable(int doctorID, DateTime ExaminationDateTime, List<Examination> examinations)
        {
            for (int i = 0; i < examinations.Count(); i++)
            {
                TimeSpan difference = ExaminationDateTime.Subtract(examinations[i].DateOf);

                if (Math.Abs(difference.TotalMinutes) < 15 && doctorID == examinations[i].IdDoctor)
                    return false;
            }
            return true;
        }
        public List<Doctor> GetDoctorsByKeyword(List<Doctor> doctors, string keyword)
        {
            List<Doctor> selectedDoctors = new List<Doctor>();
            foreach (Doctor doctor in doctors)
            {
                if (IsKeywordInDoctor(keyword, doctor))
                    selectedDoctors.Add(doctor);

            }

            return selectedDoctors;
        }

        public bool IsKeywordInDoctor(string keyword, Doctor doctor)
        {
            return doctor.FirstName.ToLower().Contains(keyword) || doctor.LastName.ToLower().Contains(keyword) || doctor.Speciality.ToString().ToLower().Contains(keyword);
        }
    }
}
