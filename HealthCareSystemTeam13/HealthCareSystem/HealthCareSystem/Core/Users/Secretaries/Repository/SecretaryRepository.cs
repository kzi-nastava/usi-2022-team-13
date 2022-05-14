using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using HealthCareSystem.Core;
using HealthCareSystem.Core.Scripts.Repository;
using HealthCareSystem.Core.Users.Patients.Model;
using HealthCareSystem.Core.Users.Model;
using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Rooms.Model;

namespace HealthCareSystem.Core.Users.Secretaries.Repository
{
    class SecretaryRepository
    {
        public DataTable patients { get; set; }
        public DataTable blockedPatients { get; set; }
        public DataTable requestsPatients { get; set; }
        public DataTable referralLetters { get; set; }
        public DataTable closestExaminations { get; set; }
        public OleDbConnection Connection { get; set; }

        public SecretaryRepository()
        {
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

        private void FillTable(DataTable table, string query)
        {
            using (var cmd = new OleDbCommand(query, Connection))
            {
                OleDbDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
            }
        }

        public void PullPatients()
        {
            patients = new DataTable();
            var query = "select Patients.ID, Patients.firstName, Patients.lastName, Users.usrnm, Users.pass from Patients INNER JOIN Users ON Users.id = patients.user_id";
            FillTable(patients, query);
        }

        public void PullBlockedPatients()
        {
            blockedPatients = new DataTable();
            string blockedPatientsQuery = "select BlockedPatients.id, Patients.FirstName as FirstName, Patients.LastName as LastName, BlockedPatients.id_secretary as BlockedBy from BlockedPatients inner join Patients on Patients.id = BlockedPatients.id_patient";
            FillTable(blockedPatients, blockedPatientsQuery);
        }

        public void PullExaminationRequests()
        {
            requestsPatients = new DataTable();
            var query = "select * from PatientEditRequest";
            FillTable(requestsPatients, query);
        }

        public void PullReferralLetters()
        {
            referralLetters = new DataTable();
            var query = "select * from ReferralLetter";
            FillTable(referralLetters, query);
        }

        public void PullClosestExaminations(string roomId)
        {
            closestExaminations = new DataTable();
            DateTime fromDateTime = DateTime.Now;
            DateTime toDateTime = DateTime.Now.AddHours(2);
            var query = "select * from (select * from Examiantion WHERE dateOf BETWEEN (" + fromDateTime + ", " + toDateTime + ") and id_room  = " + roomId + " and isUrgent = 0 ORDER BY dateOf) where rownum < 5 ";
            FillTable(closestExaminations, query);
        }

        public void PullClosestExaminations(DoctorSpeciality speciality)
        {
            closestExaminations = new DataTable();
            DateTime fromDateTime = DateTime.Now;
            DateTime toDateTime = DateTime.Now.AddHours(2);
            var query = "select * from (select * from Examiantion WHERE dateOf BETWEEN (" + fromDateTime + ", " + toDateTime + ") and id_doctor in (SELECT id FROM doctors WHERE speciality = " + speciality + " ) ORDER BY dateOf) where rownum < 5 ";
            FillTable(closestExaminations, query);
        }

        public void InsertSingleUser(User user)
        {
            var query = "INSERT INTO users(usrnm, pass, role) VALUES(@usrnm, @pass, @role)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@usrnm", user.Username);
                cmd.Parameters.AddWithValue("@pass", user.Password);
                cmd.Parameters.AddWithValue("@role", user.Role.ToString());
                cmd.ExecuteNonQuery();
            }
        }

        public List<string> GetUserId(string username)
        {
            var query = "SELECT id FROM Users WHERE usrnm = '" + username + "'";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        public List<string> GetPatientId(string userID)
        {
            var query = "SELECT id FROM Patients WHERE user_id = " + userID + "";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        public void InsertSinglePatient(Patient patient)
        {
            var query = "INSERT INTO Patients(firstName, lastName, user_id, isBlocked) VALUES(@firstName, @LastName, @user_id, @isBlocked)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@firstName", patient.FirstName);
                cmd.Parameters.AddWithValue("@LastName", patient.LastName);
                cmd.Parameters.AddWithValue("@user_id", patient.UserId);
                cmd.Parameters.AddWithValue("@isBlocked", patient.IsBlocked);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertSingleMedicalRecord(MedicalRecord medicalRecord)
        {
            var query = "INSERT INTO MedicalRecord(id_patient, height, weight) VALUES(@id_patient, @height, @weight)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_patient", medicalRecord.IdPatient);
                cmd.Parameters.AddWithValue("@height", medicalRecord.Height);
                cmd.Parameters.AddWithValue("@weight", medicalRecord.Weight);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertSingleExamination(Examination examination)
        {
            var query = "INSERT INTO Examination(id_doctor, id_patient, isEdited, isCancelled, isFinished, dateOf, typeOfExamination, isUrgent, id_room, duration) " +
                "VALUES(@id_doctor, @id_patient, @isEdited, @isCancelled, @isFinished, @dateOf, @typeOfExamination, @isUrgent, @id_room, @duration)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_doctor", examination.IdDoctor);
                cmd.Parameters.AddWithValue("@id_patient", examination.IdPatient);
                cmd.Parameters.AddWithValue("@isEdited", examination.IsEdited);
                cmd.Parameters.AddWithValue("@isCancelled", examination.IsCancelled);
                cmd.Parameters.AddWithValue("@isFinished", examination.IsFinished);
                cmd.Parameters.AddWithValue("@dateOf", examination.DateOf.ToString());
                cmd.Parameters.AddWithValue("@typeOfExamination", examination.TypeOfExamination.ToString());
                cmd.Parameters.AddWithValue("@isUrgent", examination.IsUrgent);
                cmd.Parameters.AddWithValue("@id_room", examination.IdRoom);
                cmd.Parameters.AddWithValue("@duration", examination.Duration);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSinglePatient(string patientID)
        {
            var query = "SELECT user_id FROM Patients WHERE id = " + Convert.ToInt32(patientID) + "";
            string userID = DatabaseHelpers.ExecuteReaderQueries(query, Connection)[0];
            query = "DELETE from Patients WHERE id = " + patientID + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
            query = "DELETE from Users WHERE id = " + userID + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSingleBlockedPatient(string blockedPatientID)
        {
            var query = "SELECT id_patient FROM BlockedPatients WHERE id = " + blockedPatientID + "";
            string patientID = DatabaseHelpers.ExecuteReaderQueries(query, Connection)[0];
            query = "DELETE from BlockedPatients WHERE ID = " + blockedPatientID + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
            query = "UPDATE Patients SET isBlocked = 0 WHERE ID = " + patientID + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSinglePatientRequest(string requestID)
        {
            var query = "DELETE from PatientEditRequest WHERE ID = " + Convert.ToInt32(requestID) + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSingleExamination(string requestID)
        {
            var query = "SELECT id_examination from PatientEditRequest WHERE id = " + requestID + "";
            string examinationID = DatabaseHelpers.ExecuteReaderQueries(query, Connection)[0];
            query = "UPDATE Examination SET iscancelled = 1 WHERE ID = " + examinationID + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSingleReferralLetter(string letterID)
        {
            var query = "DELETE from ReferralLetter WHERE id = " + Convert.ToInt32(letterID) + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public Dictionary<string, string> GetPatientInformation(string patientID)
        {
            var query = "select Patients.firstName, Patients.lastName, Users.usrnm, Users.pass from Patients INNER JOIN Users ON users.id = patients.user_id WHERE patients.id = " + patientID + "";
            Dictionary<string, string> row = new Dictionary<string, string>();

            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                row["firstName"] = reader["firstName"].ToString();
                row["lastName"] = reader["lastName"].ToString();
                row["usrnm"] = reader["usrnm"].ToString();
                row["pass"] = reader["pass"].ToString();
            }
            return row;
        }

        public void UpdatePatient(string patientID, string username, string password, string name, string lastname)
        {
            var query = "SELECT user_id FROM Patients WHERE id = " + patientID + "";
            string userID = DatabaseHelpers.ExecuteReaderQueries(query, Connection)[0];

            query = "UPDATE Users SET usrnm = @usrnm, pass = @pass WHERE ID = @userID";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@usrnm", username);
                cmd.Parameters.AddWithValue("@pass", password);
                cmd.Parameters.AddWithValue("@userID", userID);
                cmd.ExecuteNonQuery();
            }

            query = "UPDATE Patients SET firstname = @firstname, lastname = @astname WHERE ID = @patientID";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@firstname", name);
                cmd.Parameters.AddWithValue("@lastname", lastname);
                cmd.Parameters.AddWithValue("@patientID", patientID);
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateExamination(string requestID)
        {
            Dictionary<string, string> information = GetPatientRequest(requestID);
            var query = "UPDATE Examination SET id_doctor = @id_doctor, dateOf = @dateOf, id_room = @id_room WHERE ID = @examination_id";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_doctor", information["doctor_id"]);
                cmd.Parameters.AddWithValue("@dateOf", information["dateTimeOfExamination"]);
                cmd.Parameters.AddWithValue("@id_room", information["room_id"]);
                cmd.Parameters.AddWithValue("@examination_id", information["examination_id"]);
                cmd.ExecuteNonQuery();
            }
            query = "UPDATE Examination SET isedited = 1 WHERE ID = " + information["examination_id"] + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void BlockSinglePatient(string patientID, string username)
        {
            string userID = GetUserId(username)[0];
            var query = "SELECT ID FROM Secretaries WHERE user_id = " + userID + "";
            string secretaryID = DatabaseHelpers.ExecuteReaderQueries(query, Connection)[0];
            query = "UPDATE Patients SET isBlocked = 1 WHERE ID = " + patientID + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
            BlockedPatient blockedPatient = new BlockedPatient(Convert.ToInt32(patientID), Convert.ToInt32(secretaryID), new DateTime(2022, 10, 12));
            InsertSingleBlockedPatient(blockedPatient);
        }

        public void InsertSingleBlockedPatient(BlockedPatient blockedPatient)
        {
            var query = "INSERT INTO BlockedPatients(id_patient, id_secretary, dateOf) VALUES(@id_patient, @id_secretary, @dateOf)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_patient", blockedPatient.PatientID);
                cmd.Parameters.AddWithValue("@id_secretary", blockedPatient.SecretaryID);
                cmd.Parameters.AddWithValue("@dateOf", blockedPatient.DateOf);
                cmd.ExecuteNonQuery();
            }
        }

        public bool CheckTypeOfChange(string requestID)
        {
            var query = "SELECT isChanged FROM PatientEditRequest WHERE id = " + requestID + "";
            return Convert.ToBoolean(DatabaseHelpers.ExecuteReaderQueries(query, Connection)[0]);
        }

        public bool ValidateID(string userID, string table)
        {
            var query = "SELECT ID FROM '" + table + "'" +
                " WHERE user_id = '" + userID + "'";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection).Any();
        }

        public Dictionary<string, string> GetPatientRequest(string requestID)
        {
            var query = "SELECT id_examination, id_doctor, dateTimeOfExamination, id_room FROM PatientEditRequest WHERE id = " + requestID + "";
            Dictionary<string, string> row = new Dictionary<string, string>();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                row["examination_id"] = reader["id_examination"].ToString();
                row["doctor_id"] = reader["id_doctor"].ToString();
                row["dateTimeOfExamination"] = reader["dateTimeOfExamination"].ToString();
                row["room_id"] = reader["id_room"].ToString();
            }
            return row;
        }

        public List<string> GetSpecialistsIds(DoctorSpeciality speciality)
        {
            var query = "SELECT ID FROM Doctors WHERE speciality = '" + speciality + "'";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        public List<string> GetOperationRooms()
        {
            var query = "SELECT ID FROM Rooms WHERE type = operation";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        public List<string> GetExaminationRooms()
        {
            var query = "SELECT ID FROM Rooms WHERE type = examination";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        public List<Examination> GetDoctorsEximanitonsInNextTwoHours(string doctorId)
        {
            List<Examination> examinations = new List<Examination>();
            DateTime fromDateTime = DateTime.Now;
            DateTime toDateTime = DateTime.Now.AddHours(2);
            var query = "SELECT id, id_doctor, id_patient, isEdited, isCancelled, isFinished, dateOf, typeOfExamination, isUrgent, id_room, duration FROM Examination " +
                "WHERE dateOf BETWEEN (" + fromDateTime + ", " + toDateTime + ") and id_doctor  = " + doctorId + "";
            Dictionary<string, string> row = new Dictionary<string, string>();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                examinations.Add(new Examination((int)reader["id"], (int)reader["id_doctor"], (int)reader["id_patient"], (bool)reader["isEdited"], (bool)reader["isCancelled"], (bool)reader["isFinished"], (DateTime)reader["dateOf"],
                                                 (TypeOfExamination)reader["typeOfExamination"], (bool)reader["isUrgent"], (int)reader["id_room"], (int)reader["duration"]));
            }

            return examinations;
        }

        public List<Examination> GetRoomEximanitonsFromTo(DateTime fromDateTime, DateTime toDateTime, string roomId)
        {
            List<Examination> examinations = new List<Examination>();
            var query = "SELECT id, id_doctor, id_patient, isEdited, isCancelled, isFinished, dateOf, typeOfExamination, isUrgent, id_room, duration FROM Examination " +
                "WHERE dateOf BETWEEN (" + fromDateTime + ", " + toDateTime + ") and id_room  = " + roomId + "";
            Dictionary<string, string> row = new Dictionary<string, string>();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                examinations.Add(new Examination((int)reader["id"], (int)reader["id_doctor"], (int)reader["id_patient"], (bool)reader["isEdited"], (bool)reader["isCancelled"], (bool)reader["isFinished"], (DateTime)reader["dateOf"],
                                                 (TypeOfExamination)reader["typeOfExamination"], (bool)reader["isUrgent"], (int)reader["id_room"], (int)reader["duration"]));
            }

            return examinations;
        }

        public List<Examination> GetDoctorsExamiantions(DateTime from, DateTime to, string doctorId)
        {
            List<Examination> examinations = new List<Examination>();
            var query = "SELECT id, id_doctor, id_patient, isEdited, isCancelled, isFinished, dateOf, typeOfExamination, isUrgent, id_room, duration FROM Examination " +
                "WHERE dateOf BETWEEN (" + from + ", " + to + ") and id_doctor  = " + doctorId + "";
            //Dictionary<string, string> row = new Dictionary<string, string>();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                examinations.Add(new Examination((int)reader["id"], (int)reader["id_doctor"], (int)reader["id_patient"], (bool)reader["isEdited"], (bool)reader["isCancelled"], (bool)reader["isFinished"], (DateTime)reader["dateOf"],
                                                 (TypeOfExamination)reader["typeOfExamination"], (bool)reader["isUrgent"], (int)reader["id_room"], (int)reader["duration"]));
            }

            return examinations;
        }

        public Tuple<string, DateTime> AvailableExamination(DoctorSpeciality speciality, int duration)
        {
            List<string> doctorsID = GetSpecialistsIds(speciality);
            Tuple<string, DateTime> closestTimeAndDoctor = new Tuple<string, DateTime>("none", DateTime.Now.AddHours(2)); 
            foreach (string doctorID in doctorsID)
            {
                List<Examination> examinations = GetDoctorsEximanitonsInNextTwoHours(doctorID);
                examinations = examinations.OrderBy(examination => examination.DateOf).ToList();
                TimeSpan timeSpan = DateTime.Now - examinations[0].DateOf;
                if (timeSpan.TotalMinutes <= duration)
                {
                    closestTimeAndDoctor = new Tuple<string, DateTime>(doctorID, DateTime.Now);
                }
                for (int index = 0; index < examinations.Count() - 1; index++)
                {
                    timeSpan = examinations[index + 1].DateOf - examinations[index].DateOf;
                    if (timeSpan.TotalMinutes <= duration)
                    {
                        if(examinations[index].DateOf.AddMinutes(examinations[index].Duration) < closestTimeAndDoctor.Item2 )
                        {
                            closestTimeAndDoctor = new Tuple<string, DateTime>(doctorID, examinations[index].DateOf.AddMinutes(examinations[index].Duration));
                        }
                    }
                }
                timeSpan = DateTime.Now.AddHours(2) - examinations[examinations.Count - 1].DateOf;
                if (timeSpan.TotalMinutes <= duration)
                {
                    if (examinations[examinations.Count - 1].DateOf.AddMinutes(examinations[examinations.Count - 1].Duration) < closestTimeAndDoctor.Item2)
                    {
                        closestTimeAndDoctor = new Tuple<string, DateTime>(doctorID, examinations[examinations.Count - 1].DateOf.AddMinutes(examinations[examinations.Count - 1].Duration));
                    }
                }
            }
            return closestTimeAndDoctor;
        }

        public int GetAvailableRoom(DateTime dateTime, int duration)
        {
            if(duration > 15)
            {
                List<string> roomsId = GetOperationRooms();
                foreach(string roomId in roomsId)
                {
                    if(IsRoomAvailable(roomId, dateTime, duration))
                    {
                        return Convert.ToInt32(roomId);
                    }    
                }
            } else
            {
                List<string> roomsId = GetExaminationRooms();
                foreach (string roomId in roomsId)
                {
                    if (IsRoomAvailable(roomId, dateTime, duration))
                    {
                        return Convert.ToInt32(roomId);
                    }
                }
            }

            return 0;
        }

        public bool IsRoomAvailable(string roomId, DateTime dateTime, int duration)
        {
            DateTime fromDateTime = DateTime.Now;
            DateTime toDateTime = DateTime.Now.AddHours(2);
            List<Examination> examinations = GetRoomEximanitonsFromTo(fromDateTime, toDateTime, roomId);
            foreach(Examination examination in examinations)
            {
                if (examination.DateOf < dateTime.AddMinutes(duration) && examination.DateOf > dateTime)
                    return false;
            }
            return true;
        }

        /* public void MoveExaminations(DateTime from, DateTime to, int doctorId, int roomId)
        {
            MoveExaminationsFromRoom(from, to, roomId);
        }
        public void MoveExaminationsFromRoom(DateTime from, DateTime to, int roomId)
        {
            List<Examination> movingExaminations = GetRoomEximanitonsFromTo(from, to, roomId.ToString());
            foreach(Examination movingExamination in movingExaminations)
            {
                List<Examination> examinations = GetDoctorsExamiantions((movingExamination.IdDoctor).ToString());
                examinations = examinations.OrderBy(examination => examination.DateOf).ToList();
                bool found = false;

                for (int index = 0; index < examinations.Count() - 1; index++)
                {
                    TimeSpan timeSpan = examinations[index + 1].DateOf - examinations[index].DateOf.AddMinutes(examinations[index].Duration);
                    if (timeSpan.TotalMinutes <= movingExamination.Duration && IsRoomAvailable((movingExamination.IdRoom).ToString(), examinations[index].DateOf.AddMinutes(examinations[index].Duration), movingExamination.Duration))
                    {
                        MoveExamination(examinations[examinations.Count() - 1].Id, examinations[index].DateOf.AddMinutes(examinations[index].Duration));
                        found = true;
                        break;
                    }
                }
                if(!found)
                { 
                    MoveExamination(examinations[examinations.Count() - 1].Id, examinations[examinations.Count() - 1].DateOf.AddMinutes(examinations[examinations.Count() - 1].Duration));
                }

            }
        }*/

        void MoveExamination(int id, DateTime dateTime)
        {
            var query = "UPDATE Examination SET dateOf = " + dateTime + " WHERE ID = " + id + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void MoveDoctorsExaminations(DateTime from, DateTime to, int doctorId)
        {
            List<Examination> movingExaminations = GetDoctorsExamiantions(from, to, doctorId.ToString());
            foreach (Examination movingExamination in movingExaminations)
            {
                List<Examination> examinations = GetDoctorsExamiantions(to, DateTime.Now.AddDays(30), (movingExamination.IdDoctor).ToString());
                examinations = examinations.OrderBy(examination => examination.DateOf).ToList();
                bool found = false;

                for (int index = 0; index < examinations.Count() - 1; index++)
                {
                    TimeSpan timeSpan = examinations[index + 1].DateOf - examinations[index].DateOf.AddMinutes(examinations[index].Duration);
                    if (timeSpan.TotalMinutes <= movingExamination.Duration )
                    {
                        MoveExamination(examinations[index].Id, examinations[index].DateOf.AddMinutes(examinations[index].Duration));
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    MoveExamination(examinations[examinations.Count() - 1].Id, examinations[examinations.Count() - 1].DateOf.AddMinutes(examinations[examinations.Count() - 1].Duration));
                }
            }
        }
    }
}