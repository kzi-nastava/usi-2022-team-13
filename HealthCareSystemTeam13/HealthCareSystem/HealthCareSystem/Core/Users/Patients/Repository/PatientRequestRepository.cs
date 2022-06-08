using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Examinations.Repository;

namespace HealthCareSystem.Core.Users.Patients.Repository
{
    class PatientRequestRepository
    {
        public OleDbConnection Connection { get; }
        public PatientRequestRepository()
        {
            try
            {
                Connection = new OleDbConnection();

                Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../../Data/HCDb.mdb;
                Persist Security Info=False;";

                Connection.Open();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

        }
        public bool IsRequestChanged(string requestID)
        {
            var query = "SELECT isChanged FROM PatientEditRequest WHERE id = " + requestID + "";
            return Convert.ToBoolean(DatabaseCommander.ExecuteReaderQueries(query, Connection)[0]);
        }

        public Dictionary<string, string> GetPatientRequest(string requestID)
        {
            var query = "SELECT id_examination, id_doctor, dateTimeOfExamination, id_room FROM PatientEditRequest WHERE id = " + requestID + "";
            Dictionary<string, string> row = new Dictionary<string, string>();
            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
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
        public void DeleteSinglePatientRequest(string requestID)
        {
            var query = "DELETE from PatientEditRequest WHERE ID = " + Convert.ToInt32(requestID) + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }


    }
}
