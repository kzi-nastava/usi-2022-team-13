using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Doctors.Model
{
    enum DoctorSpeciality
    {
        Anesthesiology, Dermatology, Radiology, Neurology, Ophthalmology, Pathology, Pediatrics, Surgery, Urology, BasicPractice, Psychiatry
    }
    class Doctor
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }

        public int UserId { get; set; }
        public DoctorSpeciality Speciality { get; set; }

        public Doctor()
        {

        }
        public Doctor(string firstName, string lastName, int userId, DoctorSpeciality speciality)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserId = userId;
            this.Speciality = speciality;
            this.FullName = FirstName + " " + LastName;
            this.ID = 0;
        }
        public Doctor(int id, string firstName, string lastName, int userId, DoctorSpeciality speciality)
        {
            this.ID = id;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserId = userId;
            this.Speciality = speciality;
            this.FullName = FirstName + " " + LastName;
        }

    }
}
