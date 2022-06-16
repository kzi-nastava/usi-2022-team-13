using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Users.Doctors.Model;

namespace HealthCareSystem.Core.Users.Doctors.Repository
{
    interface IDoctorRepository
    {
        Doctor GetSelectedDoctor(string query);
        bool IsDoctorAvailableAtTime(int doctorId, DateTime startDate, List<Examination> takenExaminations);
        List<string> GetSpecialistsIds(DoctorSpeciality speciality);
        BindingList<Doctor> GetDoctors();
        List<Doctor> GetDoctorsWithAverageRating();
        void SetDoctorValuesWithRating(List<Doctor> doctors, OleDbDataReader reader);
        int GetDoctorIdByFullName(string firstName, string lastName);
        Doctor GetDoctorByUsername();
        Doctor GetDoctorFromReader(OleDbDataReader reader);
        Doctor GetAvailableDoctor(DateTime examinationDateTime, List<Examination> examinations);
        int GetDoctorId();
        string GetUsername();
        void SetUsername(string username);

    }
}
