﻿using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Users.Doctors.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using HealthCareSystem.Core.Users.Patients.Model;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Examinations.Repository;
using HealthCareSystem.Core.Users.Doctors.Service;
using HealthCareSystem.Core.Medications.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.Model;

namespace HealthCareSystem.Core.Users.Doctors.Repository
{
    class DoctorRepository:IDoctorRepository
    {
        public OleDbConnection Connection { get; set; }

        public string Username { get; set; }
        private IDoctorService _doctorService;


        public DoctorRepository(string username = "", bool calledFromDoctor = false)
        {
            if (username.Length > 0) Username = username;
            try
            {
                Connection = DatabaseConnection.GetConnection();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

            _doctorService = new DoctorService();

        }
        public Doctor GetSelectedDoctor(string query)
        {

            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);

            Doctor doctor = new Doctor();

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Enum.TryParse<DoctorSpeciality>(reader["speciality"].ToString(), out var speciality);
                doctor = new Doctor(reader["firstName"].ToString(), reader["lastName"].ToString(), Convert.ToInt32(reader["user_id"]), speciality);

            }
            return doctor;
        }

        public bool IsDoctorAvailableAtTime(int doctorId, DateTime startDate, List<Examination> takenExaminations)
        {
            bool isAvailable = true;
            foreach (Examination takenExam in takenExaminations)
            {
                if (!IsValidTimeAndDoctor(startDate, takenExam, doctorId))
                    isAvailable = false;
            }
            return isAvailable;
        }

        private bool IsDoctorEqual(int doctorId, Examination takenExamination)
        {
            return doctorId == takenExamination.IdDoctor;
        }

        private bool IsValidTimeAndDoctor(DateTime startDate, Examination takenExamination, int doctorId)
        {

            return !(!TimeDateHelpers.IsValidTime(startDate, takenExamination) && IsDoctorEqual(doctorId, takenExamination));
        }

        public List<string> GetSpecialistsIds(DoctorSpeciality speciality)
        {
            var query = "SELECT ID FROM Doctors WHERE speciality = '" + speciality.ToString() + "'";
            return DatabaseCommander.ExecuteReaderQueries(query, Connection);
        }

        public string GetUsername()
        {
            return Username;
        }
        public BindingList<Doctor> GetDoctors()
        {
            BindingList<Doctor> doctors = new BindingList<Doctor>();
            try
            {

                OleDbCommand cmd = DatabaseCommander.GetCommand("select * from doctors", Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    doctors.Add(GetDoctorFromReader(reader));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            

            return doctors;
        }
        public List<Doctor> GetDoctorsWithAverageRating()
        {
            string query = "select dr.id as DoctorID, dr.firstName as FirstName, dr.lastName as LastName, dr.user_id as UserId, dr.speciality as Speciality, avg(ds.doctorGrade) as Rating  from Doctors as dr left outer join DoctorSurveys ds on dr.id = ds.id_doctor group by dr.id, dr.firstName, dr.lastName, dr.user_id, dr.speciality";

            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();
            List<Doctor> doctors = new List<Doctor>();
            while (reader.Read())
            {
                SetDoctorValuesWithRating(doctors, reader);
            }

            
            return doctors;
        }

        public void SetDoctorValuesWithRating(List<Doctor> doctors, OleDbDataReader reader)
        {
            DoctorSpeciality speciality;
            Enum.TryParse<DoctorSpeciality>(reader["Speciality"].ToString(), out speciality);

            doctors.Add(new Doctor(
                Convert.ToInt32(reader["DoctorID"]),
                reader["FirstName"].ToString(),
                reader["LastName"].ToString(),
                Convert.ToInt32(reader["UserId"]),
                speciality,
                Convert.ToDouble(reader["Rating"])
                ));

        }
        public int GetDoctorIdByFullName(string firstName, string lastName)
        {
            string query = "select id from Doctors where firstName = '" + firstName + "' and lastName = '" + lastName + "'";
            int doctorId = Convert.ToInt32(DatabaseCommander.ExecuteReaderQueries(query, Connection)[0]);
            return doctorId;
        }

        public void SetUsername(string username)
        {
            Username = username;
        }
        public Doctor GetDoctorByUsername()
        {
            string userId = DatabaseCommander.ExecuteReaderQueries("select id from users where usrnm = '" + Username + "'", Connection)[0];

            Doctor doctor = new Doctor();

            OleDbCommand cmd = DatabaseCommander.GetCommand("select * from doctors where user_id = " + Convert.ToInt32(userId) + "", Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                doctor = GetDoctorFromReader(reader);
            }
            return doctor;

        }
        public Doctor GetDoctorFromReader(OleDbDataReader reader)
        {
            Enum.TryParse<DoctorSpeciality>(reader["speciality"].ToString(), out var speciality);

            var doctor = new Doctor(Convert.ToInt32(reader["id"]), reader["firstName"].ToString(), reader["lastName"].ToString(),
                Convert.ToInt32(reader["user_id"]), speciality);
            return doctor;
        }
        public Doctor GetAvailableDoctor(DateTime examinationDateTime, List<Examination> examinations)
        {
            BindingList<Doctor> doctors = GetDoctors();
            foreach (Doctor doctor in doctors)
            {
                if (_doctorService.IsDoctorAvailable(doctor.ID, examinationDateTime, examinations)) return doctor;
            }
            return null;

        }

        public int GetDoctorId()
        {
            string userId = DatabaseCommander.ExecuteReaderQueries("select id from users where usrnm = '" + Username + "'", Connection)[0];

            int doctorId = Convert.ToInt32(DatabaseCommander.ExecuteReaderQueries("select id from doctors where user_id = " + Convert.ToInt32(userId) + "", Connection)[0]);
            return doctorId;

        }


    }
}
