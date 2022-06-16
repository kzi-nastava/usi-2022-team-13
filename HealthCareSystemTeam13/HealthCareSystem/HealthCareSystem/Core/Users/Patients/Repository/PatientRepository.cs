using System;
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
using System.ComponentModel;
using HealthCareSystem.Core.Examinations.Repository;
using HealthCareSystem.Core.Surveys.HospitalSurveys.Model;

namespace HealthCareSystem.Core.Users.Patients.Repository
{
    class PatientRepository
    {
        public string Username { get; set; }
        public OleDbConnection Connection { get; set; }
        public DataTable BlockedPatients { get; private set; }
        public DataTable Patients { get; private set; }

        private readonly IExaminationRepository _examinationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IExaminationChangesRepository _examinationChangesRepository;


        public PatientRepository(string username = "") { 
            if(username.Length > 0) Username = username;
            try
            {
                Connection = DatabaseConnection.GetConnection();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            _examinationRepository = new ExaminationRepository();
            _userRepository = new UserRepository();
            _examinationChangesRepository = new ExaminationChangesRepository();


        }
        public int GetPatientId()
        {
            string userId = DatabaseCommander.ExecuteReaderQueries("select id from users where usrnm = '" + Username + "'", Connection)[0];

            int patientId = Convert.ToInt32(DatabaseCommander.ExecuteReaderQueries("select id from patients where user_id = " + Convert.ToInt32(userId) + "", Connection)[0]);

            return patientId;
        }
        public Dictionary<string, string> GetPatientNameAndMedicalStats(int patientId)
        {
            var query = "select Patients.firstName, Patients.lastName, MedicalRecord.height, MedicalRecord.weight from Patients inner join MedicalRecord on Patients.ID = MedicalRecord.id_patient WHERE patients.id = " + patientId + "";
            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();
            Dictionary<string, string> patientInfo = new Dictionary<string, string>();
            while (reader.Read())
            {
                patientInfo["firstName"] = reader["firstName"].ToString();
                patientInfo["lastName"] = reader["lastName"].ToString();
                patientInfo["height"] = reader["height"].ToString();
                patientInfo["weight"] = reader["weight"].ToString();

            }
            return patientInfo;
        }
        public int GetPatientIdByFirstName(string firstName)
        {

            int patientId = Convert.ToInt32(
                DatabaseCommander.ExecuteReaderQueries("select id from Patients where firstName = '" + firstName + "'", Connection)[0]);

            return patientId;
        }

        public void UpdatePatientContent(string query, int patiendId = 0)
        {

            DatabaseCommander.ExecuteNonQueries(query, Connection);
            if (patiendId > 0) _examinationRepository.InsertExaminationChanges(TypeOfChange.Edit, patiendId);
            else _examinationRepository.InsertExaminationChanges(TypeOfChange.Edit);


        }

        private int GetPatientId(string patientUsername)
        {
            string patientIdQuery = "select Patients.id from Patients inner join Users on Patients.user_id = Users.id where Users.usrnm = '" + patientUsername + "'";
            int patientId = Convert.ToInt32(DatabaseCommander.ExecuteReaderQueries(patientIdQuery, Connection)[0]);
            return patientId;
        }

        public BindingList<Patient> GetPatients()
        {

            BindingList<Patient> patients = new BindingList<Patient>();
            try
            {
                OleDbCommand cmd = DatabaseCommander.GetCommand("select * from Patients", Connection);
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

            return patients;
        }

        public string GetUsernameFromPatient(Patient patient)
        {

            string query = "select Users.usrnm from Users inner join " +
                           "Patients on Patients.user_id = Users.id where Patients.id = " + patient.ID;

            string patientUsername = DatabaseCommander.ExecuteReaderQueries(query, Connection)[0];
            return patientUsername;
        }

        public bool IsPatientBlocked(string patientUsername)
        {

            List<string> userIds = DatabaseCommander.ExecuteReaderQueries("select id from users where usrnm= '" + patientUsername + "'", Connection);

            List<string> blockedPatients = DatabaseCommander.ExecuteReaderQueries("select isBlocked from Patients where user_id = " + Convert.ToInt32(userIds[0]) + "", Connection);

            return blockedPatients[0] == "True";
        }
        public void BlockSpamPatients(string patientUsername)
        {
            int patientId = GetPatientId(patientUsername);
            
            List<ExaminationChange> changes = _examinationChangesRepository.GetExaminationChanges(patientId);
            int adds = 0, edits = 0;

            for (int i = 0; i < changes.Count(); i++)
            {

                if (DateTime.Now.Subtract(changes[i].DateOfChange).TotalDays <= 30)
                {
                    if (changes[i].Change == TypeOfChange.Add) adds++;
                    else if (changes[i].Change == TypeOfChange.Edit || changes[i].Change == TypeOfChange.Delete) edits++;
                }
            }

            if (adds > 8 || edits > 4)
            {
                BlockPatient(patientId);
            }

        }
        private void BlockPatient(int patientId)
        {
            string query = "Update Patients set isBlocked = " + true + " where id = " + patientId + "";
            DatabaseCommander.ExecuteNonQueries(query, Connection);


            query = "INSERT INTO BlockedPatients(id_patient, id_secretary, dateOf) VALUES(@id_patient, @id_secretary, @dateOf)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_patient", patientId);
                cmd.Parameters.AddWithValue("@id_secretary", DBNull.Value);
                cmd.Parameters.AddWithValue("@dateOf", DateTime.Now.ToString());
                cmd.ExecuteNonQuery();
            }

        }

        public void PullPatients()
        {
            Patients = new DataTable();
            var query = "select Patients.ID, Patients.firstName, Patients.lastName, Users.usrnm, Users.pass from Patients INNER JOIN Users ON Users.id = patients.user_id";
            GUIHelpers.FillTable(Patients, query, Connection);
        }

        public void PullBlockedPatients()
        {
            BlockedPatients = new DataTable();
            string blockedPatientsQuery = "select BlockedPatients.id, Patients.FirstName as FirstName, Patients.LastName as LastName, BlockedPatients.id_secretary as BlockedBy from BlockedPatients inner join Patients on Patients.id = BlockedPatients.id_patient";
            GUIHelpers.FillTable(BlockedPatients, blockedPatientsQuery, Connection);
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
        public List<string> GetPatientIdByUserId(string userID)
        {
            var query = "SELECT id FROM Patients WHERE user_id = " + userID + "";
            return DatabaseCommander.ExecuteReaderQueries(query, Connection);
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
        public void DeleteSinglePatient(string patientID)
        {
            var query = "SELECT user_id FROM Patients WHERE id = " + Convert.ToInt32(patientID) + "";
            string userID = DatabaseCommander.ExecuteReaderQueries(query, Connection)[0];

            query = "DELETE from Users WHERE id = " + userID + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSingleBlockedPatient(string blockedPatientID)
        {
            var query = "SELECT id_patient FROM BlockedPatients WHERE id = " + blockedPatientID + "";
            string patientID = DatabaseCommander.ExecuteReaderQueries(query, Connection)[0];

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

   
        public Dictionary<string, string> GetPatientInformation(string patientID)
        {
            var query = "select Patients.firstName, Patients.lastName, Users.usrnm, Users.pass from Patients INNER JOIN Users ON users.id = patients.user_id WHERE patients.id = " + patientID + "";
            Dictionary<string, string> row = new Dictionary<string, string>();

            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);

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
            string userID = DatabaseCommander.ExecuteReaderQueries(query, Connection)[0];

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
        public void BlockSinglePatient(string patientID, string username)
        {
            string userID = _userRepository.GetUserId(username)[0];
            var query = "SELECT ID FROM Secretaries WHERE user_id = " + userID + "";
            string secretaryID = DatabaseCommander.ExecuteReaderQueries(query, Connection)[0];

            query = "UPDATE Patients SET isBlocked = 1 WHERE ID = " + patientID + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }

            BlockedPatient blockedPatient = new BlockedPatient(Convert.ToInt32(patientID), Convert.ToInt32(secretaryID), new DateTime(2022, 10, 12));
            InsertSingleBlockedPatient(blockedPatient);
        }

       

        public Patient GetSelectedPatient(string query)
        {

            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);

            Patient patient = new Patient();

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                patient = SetPatientValues(reader);
            }

            return patient;
        }

        private static Patient SetPatientValues(OleDbDataReader reader)
        {
            return new Patient(
            Convert.ToInt32(reader["Patients.ID"]),
            reader["firstName"].ToString(), reader["lastName"].ToString(),
            Convert.ToInt32(reader["user_id"]), false
            );
        }

    }
}
