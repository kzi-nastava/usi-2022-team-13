﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using HealthCareSystem.Core;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Rooms.Model;
using HealthCareSystem.Core.Scripts.Repository;
using HealthCareSystem.Core.Users.Patients.Model;
<<<<<<< HEAD
=======
using System.ComponentModel;
>>>>>>> feature/Doctor

namespace HealthCareSystem.Core.Users.Patients.Repository
{
    class PatientRepository
    {
        public string Username { get; set; }
        public DataTable examinations { get; set; }
        public OleDbConnection Connection { get; set; }
<<<<<<< HEAD
        public PatientRepository(string username) { 
            Username = username;
=======
        public PatientRepository(string username = "") { 
            if(username.Length > 0) Username = username;
>>>>>>> feature/Doctor
            try
            {
                Connection = new OleDbConnection();

                Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=HCDb.mdb;
                Persist Security Info=False;";

                Connection.Open();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

        
        }
        private int GetPatientId()
        {
            string userId = DatabaseHelpers.ExecuteReaderQueries("select id from users where usrnm = '" + Username + "'", Connection)[0];

            int patientId = Convert.ToInt32(DatabaseHelpers.ExecuteReaderQueries("select id from patients where user_id = " + Convert.ToInt32(userId) + "", Connection)[0]);

            return patientId;
        }

<<<<<<< HEAD
        public void PullExaminations()
        {
            examinations = new DataTable();
   
=======
        public int GetPatientIdByFirstName(string firstName)
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

            int patientId = Convert.ToInt32(
                DatabaseHelpers.ExecuteReaderQueries("select id from Patients where firstName = '" + firstName + "'", Connection)[0]);

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();

            return patientId;
        }

        public void PullExaminations()
        {
            examinations = new DataTable();
    
>>>>>>> feature/Doctor
            string examinationsQuery = "select Examination.id, Doctors.FirstName + ' ' +Doctors.LastName as Doctor, dateOf as [Date and Time], id_room as RoomID, duration, typeOfExamination as Type from Examination left outer join Doctors  on Examination.id_doctor = Doctors.id " +
                "where id_patient = " + GetPatientId() +"";

            FillTable(examinations, examinationsQuery);


        }
<<<<<<< HEAD
        public void UpdateContent(string query)
        {
            
            DatabaseHelpers.ExecuteNonQueries(query, Connection);
            InsertExaminationChanges(TypeOfChange.Edit);
=======
        public void UpdateContent(string query, int patiendId = 0)
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

            DatabaseHelpers.ExecuteNonQueries(query, Connection);
            if(patiendId > 0) InsertExaminationChanges(TypeOfChange.Edit, patiendId);
            else InsertExaminationChanges(TypeOfChange.Edit);

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();
>>>>>>> feature/Doctor

        }

        public void SendExaminationEditRequest(int examinationId, DateTime currentTime, bool isEdit, int doctorId, DateTime newDateTime, int roomId)
        {
            string query = "insert into PatientEditRequest (id_examination, dateOf, isChanged, isDeleted, id_doctor, dateTimeOfExamination, id_room) VALUES(@id_examination, @dateOf, @isChanged, @isDeleted, @id_doctor, @dateTimeOfExamination, @id_room)";

            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_examination", examinationId);
                cmd.Parameters.AddWithValue("@dateOf", currentTime.ToString());
                if (isEdit)
                {
                    cmd.Parameters.AddWithValue("@isChanged", true);
                    cmd.Parameters.AddWithValue("@isDeleted", false);
                    cmd.Parameters.AddWithValue("@id_doctor", doctorId);
                    cmd.Parameters.AddWithValue("@dateTimeOfExamination", newDateTime.ToString());
                    cmd.Parameters.AddWithValue("@id_room", roomId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@isChanged", false);
                    cmd.Parameters.AddWithValue("@isDeleted", true);
                    cmd.Parameters.AddWithValue("@id_doctor", DBNull.Value);
                    cmd.Parameters.AddWithValue("@dateTimeOfExamination", DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_room", DBNull.Value);
                }

                cmd.ExecuteNonQuery();

            }

        }

<<<<<<< HEAD
        public void InsertExaminationChanges(TypeOfChange typeOfChange)
        {
            int patientId = GetPatientId(Username);

=======
        public void InsertExaminationChanges(TypeOfChange typeOfChange, int parsedPatientId = 0)
        {
            int patientId;
            if (parsedPatientId == 0) { patientId = GetPatientId(Username); }
            else { patientId = parsedPatientId; }
>>>>>>> feature/Doctor
            string insertQuery = "insert into PatientExaminationChanges(id_patient, typeOfChange, dateOf) values(@id_patient, @typeOfChange, @dateOf)";

            using (var cmd = new OleDbCommand(insertQuery, Connection))
            {
                cmd.Parameters.AddWithValue("@id_patient", patientId);
                cmd.Parameters.AddWithValue("@typeOfChange", typeOfChange.ToString());
                cmd.Parameters.AddWithValue("@dateOf", DateTime.Now.ToString());

                cmd.ExecuteNonQuery();
            }
<<<<<<< HEAD
           
        }

        public void InsertExamination(string patientUsername, int doctorId, DateTime examinationDateTime, int duration, int roomId)
        {
=======

        }

        public void InsertExamination(string patientUsername, int doctorId, DateTime examinationDateTime,
            int duration, int roomId, string selectedType="")
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }
             
>>>>>>> feature/Doctor
            int patientId = GetPatientId(patientUsername);

            string insertQuery = "insert into Examination(id_doctor, id_patient, isEdited, isCancelled, isFinished, dateOf, typeOfExamination, isUrgent, id_room, duration) values(@id_doctor, @id_patient, @isEdited, @isCancelled, @isFinished, @dateOf, @typeOfExamination, @isUrgent, @id_room, @duration)";

            using (var cmd = new OleDbCommand(insertQuery, Connection))
            {
                cmd.Parameters.AddWithValue("@id_doctor", doctorId);
                cmd.Parameters.AddWithValue("@id_patient", patientId);
                cmd.Parameters.AddWithValue("@isEdited", false);
                cmd.Parameters.AddWithValue("@isCancelled", false);
                cmd.Parameters.AddWithValue("@isFinished", false);
                cmd.Parameters.AddWithValue("@dateOf", examinationDateTime.ToString());
<<<<<<< HEAD
                cmd.Parameters.AddWithValue("@typeOfExamination", TypeOfExamination.BasicExamination.ToString());
=======
                if(selectedType.Length == 0 || selectedType == TypeOfExamination.BasicExamination.ToString())
                {
                    cmd.Parameters.AddWithValue("@typeOfExamination", TypeOfExamination.BasicExamination.ToString());
                } else
                {
                    cmd.Parameters.AddWithValue("@typeOfExamination", TypeOfExamination.Operation.ToString());
                }
>>>>>>> feature/Doctor
                cmd.Parameters.AddWithValue("@isUrgent", false);
                cmd.Parameters.AddWithValue("@id_room", roomId);
                cmd.Parameters.AddWithValue("@duration", 15);

                cmd.ExecuteNonQuery();
            }
<<<<<<< HEAD
            InsertExaminationChanges(TypeOfChange.Add);
=======
            Username = patientUsername;
            InsertExaminationChanges(TypeOfChange.Add);
            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();
>>>>>>> feature/Doctor
        }

        private int GetPatientId(string patientUsername)
        {
            string patientIdQuery = "select Patients.id from Patients inner join Users on Patients.user_id = Users.id where Users.usrnm = '" + patientUsername + "'";
<<<<<<< HEAD
=======
            Console.WriteLine(patientIdQuery);
>>>>>>> feature/Doctor
            int patientId = Convert.ToInt32(DatabaseHelpers.ExecuteReaderQueries(patientIdQuery, Connection)[0]);
            return patientId;
        }

        public Dictionary<string, string> GetExamination(int examinationId)
        {
<<<<<<< HEAD
            string query = "select id_doctor, dateOf, id_room from Examination where id = " + examinationId + "";
=======
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

            string query = "select id_doctor, dateOf, id_room, typeOfExamination from Examination where id = " + examinationId + "";
>>>>>>> feature/Doctor

            Dictionary<string, string> row = new Dictionary<string, string>();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                row["id"] = examinationId.ToString();
                row["doctor_id"] = reader["id_doctor"].ToString();
                row["dateOf"] = reader["dateOf"].ToString();
                row["room_id"] = reader["id_room"].ToString();
<<<<<<< HEAD
            }
=======
                row["typeOfExamination"] = reader["typeOfExamination"].ToString();
            }

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();

>>>>>>> feature/Doctor
            return row;
        }

        public void CancelExamination(int examinationId)
        {
            string query = "delete from Examination where id = " + examinationId + "";
            DatabaseHelpers.ExecuteNonQueries(query, Connection);
        }

        private void FillTable(DataTable table, string query)
        {

            using (var cmd = new OleDbCommand(query, Connection))
            {
                OleDbDataReader reader = cmd.ExecuteReader();
                table.Load(reader);


            }
        }

        public Doctor GetSelectedDoctor(string query)
        {
            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
            
            Doctor doctor = new Doctor();

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                DoctorSpeciality speciality;
                Enum.TryParse<DoctorSpeciality>(reader["speciality"].ToString(), out speciality);
                doctor = new Doctor(reader["firstName"].ToString(), reader["lastName"].ToString(), Convert.ToInt32(reader["user_id"]), speciality);

            }
            return doctor;
        }

        public static List<Doctor> GetDoctorsForExaminations(OleDbConnection connection)
        {
            List<Doctor> doctors = new List<Doctor>();

            OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from doctors", connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DoctorSpeciality speciality;
                Enum.TryParse<DoctorSpeciality>(reader["speciality"].ToString(), out speciality);

                doctors.Add(new Doctor(reader["firstName"].ToString(), reader["lastName"].ToString(), Convert.ToInt32(reader["user_id"]), speciality));
            }

            return doctors;
        }
<<<<<<< HEAD
        
=======

        public BindingList<Patient> GetPatients()
        {
            BindingList<Patient> patients = new BindingList<Patient>();
            try
            {
                

                OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from Patients", Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    patients.Add(new Patient(
                        Convert.ToInt32(reader["ID"]), reader["firstName"].ToString(),
                        reader["lastName"].ToString(), Convert.ToInt32(reader["user_id"]), 
                        Convert.ToBoolean(reader["isBlocked"])
                    ));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            Connection.Close();

            return patients;
        }

        public string GetUsernameFromEntity(Patient patient)
        {
            Connection.Open();
            // string patientIdQuery = "select Patients.id from Patients inner join Users on Patients.user_id = Users.id where Users.usrnm = '" + patientUsername + "'";
            string query = "select Users.usrnm from Users inner join " +
                "Patients on Patients.user_id = Users.id where Patients.id = " + patient.ID ;
            
            string patientUsername = DatabaseHelpers.ExecuteReaderQueries(query, Connection)[0];
            Connection.Close();
            return patientUsername;
        }

        public string[] GetMedicalRecord(string query)
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            Console.WriteLine(query);

            string[] data = new string[2];

            while (reader.Read())
            {
                string a = reader["weight"].ToString();
                string b = reader["height"].ToString();
                data[0] = a;
                data[1] = b;
            }

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();

            return data;
        }
>>>>>>> feature/Doctor

    }
}
