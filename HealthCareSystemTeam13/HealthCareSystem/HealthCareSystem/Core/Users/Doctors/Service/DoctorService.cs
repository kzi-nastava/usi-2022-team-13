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
                if (IsKeywordInDoctor(keyword, doctor))
                {
                    selectedDoctors.Add(doctor);
                }
            }

            return selectedDoctors;
        }

        private static bool IsKeywordInDoctor(string keyword, Doctor doctor)
        {
            return doctor.FirstName.ToLower().Contains(keyword) || doctor.LastName.ToLower().Contains(keyword) || doctor.Speciality.ToString().ToLower().Contains(keyword);
        }

        public static List<Doctor> SortDoctors(List<Doctor> doctors, int indicator)
        {
            // indicator = 0 -> sort by rating, = 1 -> sort by firstName,
            // = 2 -> sort by lastName,  = 3 sort by Speciality 

            if (indicator == 1) return SortByRating(doctors);
            else if (indicator == 2) return SortByFirstName(doctors);
            else if (indicator == 3) return SortByLastName(doctors);
            else return SortBySpeciality(doctors);

        }

        private static List<Doctor> SortBySpeciality(List<Doctor> doctors)
        {
            for (int i = 0; i < doctors.Count() - 1; i++)
            {
                for (int j = 0; j < doctors.Count() - i - 1; j++)
                {
                    if (doctors[j].Speciality.ToString().CompareTo(doctors[j + 1].Speciality.ToString()) > 0)
                        Swap(doctors, j);
                }
            }
            return doctors;
        }

        private static List<Doctor> SortByFirstName(List<Doctor> doctors)
        {
            for (int i = 0; i < doctors.Count() - 1; i++)
            {
                for (int j = 0; j < doctors.Count() - i - 1; j++)
                {
                    if (doctors[j].FirstName.CompareTo(doctors[j + 1].FirstName) > 0)
                        Swap(doctors, j);
                }
            }
            return doctors;
        }

        private static List<Doctor> SortByLastName(List<Doctor> doctors)
        {
            for (int i = 0; i < doctors.Count() - 1; i++)
            {
                for (int j = 0; j < doctors.Count() - i - 1; j++)
                {
                    if (doctors[j].LastName.CompareTo(doctors[j + 1].LastName) > 0)
                        Swap(doctors, j);
                }
            }
            return doctors;
        }

        private static List<Doctor> SortByRating(List<Doctor> doctors)
        {
            for (int i = 0; i < doctors.Count() - 1; i++)
            {
                for (int j = 0; j < doctors.Count() - i - 1; j++)
                {
                    if (doctors[j].AverageRating > doctors[j + 1].AverageRating)
                        Swap(doctors, j);
                }
            }
            return doctors;
        }
        private static void Swap(List<Doctor> doctors, int j)
        {
            Doctor temp = doctors[j];
            doctors[j] = doctors[j + 1];
            doctors[j + 1] = temp;
        }
    }
}
