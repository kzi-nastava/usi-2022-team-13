using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Users.Doctors.Model;

namespace HealthCareSystem.Core.Users.Doctors
{
    class DoctorSorter
    {
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
                    if (doctors[j].Speciality.ToString().CompareTo(doctors[j + 1].Speciality.ToString()) > 0) Swap(doctors, j);
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
                    if (doctors[j].FirstName.CompareTo(doctors[j + 1].FirstName) > 0) Swap(doctors, j);
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
                    if (doctors[j].LastName.CompareTo(doctors[j + 1].LastName) > 0) Swap(doctors, j);
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
                    if (doctors[j].AverageRating > doctors[j + 1].AverageRating) Swap(doctors, j);
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
