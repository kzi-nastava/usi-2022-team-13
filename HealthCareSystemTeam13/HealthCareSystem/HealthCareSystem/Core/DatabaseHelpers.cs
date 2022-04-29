using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Patients.Model;

namespace HealthCareSystem.Core
{
    class DatabaseHelpers
    {
       
        public DatabaseHelpers()
        {
            
        }

        public static void ExecuteNonQueries(string query, OleDbConnection connection)
        {
            using (var cmd = new OleDbCommand(query, connection))
            {
                cmd.ExecuteNonQuery();

            }
        }
        public static List<string> ExecuteReaderQueries(string query, OleDbConnection connection)
        {
            Console.WriteLine(query);

            List<string> data = new List<string>();
            OleDbCommand cmd = GetCommand(query, connection);

            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                data.Add(reader[0].ToString());
            }

            return data;
        }

        public static OleDbCommand GetCommand(string query, OleDbConnection connection)
        {
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = connection;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            return cmd;
        }

        public static bool IsPatientBlocked(string patientUsername, OleDbConnection connection)
        {

            List<string> userIds = ExecuteReaderQueries("select id from users where usrnm= '" + patientUsername + "'", connection);

            List<string>  blockedPatients = ExecuteReaderQueries("select isBlocked from Patients where user_id = " + Convert.ToInt32(userIds[0]) + "", connection);
  
            return blockedPatients[0] == "True";
        }
        public static void BlockSpamPatients(string patientUsername, OleDbConnection connection)
        {
            int patientId = GetPatientId(patientUsername, connection);
            List<ExaminationChange> changes = GetExaminationChanges(patientId, connection);
            int adds = 0, edits = 0;

            for(int i = 0; i < changes.Count(); i++)
            {

                if (DateTime.Now.Subtract(changes[i].DateOfChange).TotalDays <= 30)
                {
                    if (changes[i].Change == TypeOfChange.Add) adds++;
                    else if (changes[i].Change == TypeOfChange.Edit || changes[i].Change == TypeOfChange.Delete) edits++;
                }
            }
            
            if(adds > 8 || edits > 4)
            {
                BlockPatient(patientId, connection);
            }

        }
        private static void BlockPatient(int patientId, OleDbConnection connection)
        {
            string query = "Update Patients set isBlocked = " + true + " where id = " + patientId + "";
            ExecuteNonQueries(query, connection);


            query = "INSERT INTO BlockedPatients(id_patient, id_secretary, dateOf) VALUES(@id_patient, @id_secretary, @dateOf)";
            using (var cmd = new OleDbCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id_patient", patientId);
                cmd.Parameters.AddWithValue("@id_secretary", DBNull.Value);
                cmd.Parameters.AddWithValue("@dateOf", DateTime.Now.ToString());
                cmd.ExecuteNonQuery();
            }

        }

        private static List<ExaminationChange> GetExaminationChanges(int patientId, OleDbConnection connection)
        {
            List<ExaminationChange> changes = new List<ExaminationChange>();
            string query = "select * from PatientExaminationChanges where id_patient = " + patientId + "";
            OleDbCommand cmd = GetCommand(query, connection);

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TypeOfChange typeOfChange;
                Enum.TryParse<TypeOfChange>(reader["typeOfChange"].ToString(), out typeOfChange);
                changes.Add(new ExaminationChange(patientId, typeOfChange, (DateTime)reader["dateOf"]));

            }
            return changes;
        }
        private static int GetPatientId(string patientUsername, OleDbConnection connection)
        {
            string patientIdQuery = "select Patients.id from Patients inner join Users on Patients.user_id = Users.id where Users.usrnm = '" + patientUsername + "'";
            int patientId = Convert.ToInt32(ExecuteReaderQueries(patientIdQuery, connection)[0]);
            return patientId;
        }

    }
}
