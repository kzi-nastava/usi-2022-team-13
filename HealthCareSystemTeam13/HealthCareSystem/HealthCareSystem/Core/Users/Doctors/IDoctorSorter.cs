using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Users.Doctors.Model;

namespace HealthCareSystem.Core.Users.Doctors
{
    interface IDoctorSorter
    {
        List<Doctor> SortDoctors(List<Doctor> doctors, int indicator);
        List<Doctor> SortBySpeciality(List<Doctor> doctors);
        List<Doctor> SortByRating(List<Doctor> doctors);
        List<Doctor> SortByLastName(List<Doctor> doctors);
        List<Doctor> SortByFirstName(List<Doctor> doctors);
        void Swap(List<Doctor> doctors, int j);
    }
}
