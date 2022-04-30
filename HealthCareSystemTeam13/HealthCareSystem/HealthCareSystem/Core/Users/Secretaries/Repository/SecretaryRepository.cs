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

namespace HealthCareSystem.Core.Users.Secretaries.Repository
{
    class SecretaryRepository
    {
        public DataTable patients { get; set; }
        public DataTable blockedPatients { get; set; }
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
            var query = "select Patients.ID, Patients.firstName, Patients.lastName, Users.usrnm, Users.pass, MedicalRecord.height, MedicalRecord.weight from Patients INNER JOIN Users ON Users.id = patients.user_id INNER JOIN MedicalRecord ON Patients.id = MedicalRecord.id_patient";
            FillTable(patients, query);
        }

        public void PullBlockedPatients()
        {
            blockedPatients = new DataTable();
            string blockedPatientsQuery = "select BlockedPatients.id, Patients.FirstName as FirstName, Patients.LastName as LastName, BlockedPatients.id_secretary as BlockedBy from BlockedPatients inner join Patients on Patients.id = BlockedPatients.id_patient";
            FillTable(blockedPatients, blockedPatientsQuery);
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
        public void DeleteSinglePatient(string patientID)
        {
            var query = "SELECT user_id FROM Patients WHERE id = " + patientID + "";
            string userID = DatabaseHelpers.ExecuteReaderQueries(query, Connection)[0];
            query = "DELETE from MedicalRecord WHERE id_patient = " + patientID + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
            query = "DELETE from BlockedPatients WHERE id_patient = " + patientID + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
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

        public List<String> GetPatientInformation(string patientID)
        {
            var query = "select Patients.firstName, Patients.lastName, Users.usrnm, Users.pass from Patients INNER JOIN Users ON users.id = patients.user_id WHERE patients.id = " + patientID + "";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        public void UpdatePatient(string patientID, string username, string password, string name, string lastname)
        {
            var query = "SELECT user_id FROM Patients WHERE id = " + patientID + "";
            string userID = DatabaseHelpers.ExecuteReaderQueries(query, Connection)[0];
            query = "UPDATE Users SET usrnm = " + username + ", pswrd = " + password + " WHERE ID = " + userID + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
            query = "UPDATE Patients SET name = " + name + ", lastname = " + lastname + " WHERE ID = " + patientID + "";
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
            BlockedPatient blockedPatient = new BlockedPatient(Convert.ToInt32(patientID), Convert.ToInt32(secretaryID), DateTime.Now);
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

        public bool ValidateID(string userID, string table)
        {
            var query = "SELECT ID FROM '" + table + "'" +
                " WHERE user_id = '" + userID + "'";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection).Any();
        }
    }
}