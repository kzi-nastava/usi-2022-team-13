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
    class DoctorService
    {
        public static bool IsDoctorAvailable(int doctorID, DateTime ExaminationDateTime, List<Examination> examinations)
        {
            for (int i = 0; i < examinations.Count(); i++)
            {
                TimeSpan difference = ExaminationDateTime.Subtract(examinations[i].DateOf);
                Console.WriteLine(ExaminationDateTime.ToString());

                if (Math.Abs(difference.TotalMinutes) < 15 && doctorID == examinations[i].IdDoctor)
                {
                    return false;
                }
            }

            return true;
        }
        public static List<Doctor> GetDoctorsByKeyword(List<Doctor> doctors, string keyword)
        {
            List<Doctor> selectedDoctors = new List<Doctor>();
            foreach (Doctor doctor in doctors)
            {
                Console.WriteLine(keyword);
                if (doctor.FirstName.ToLower().Contains(keyword) || doctor.LastName.ToLower().Contains(keyword) || doctor.Speciality.ToString().ToLower().Contains(keyword))
                {
                    selectedDoctors.Add(doctor);
                }
            }

            return selectedDoctors;
        }
    }
}
