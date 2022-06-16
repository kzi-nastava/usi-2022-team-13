using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.GUI.DoctorsFunctionalities;
using HealthCareSystem.Core.Users.Patients.Model;

namespace HealthCareSystem.Core.Users.Patients.Repository
{
    class MedicalRecordRepository: IMedicalRecordRepository
    {
        public OleDbConnection Connection { get; set; }

        public MedicalRecordRepository()
        {
            try
            {
                Connection = DatabaseConnection.GetConnection();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }
        public string[] GetMedicalRecord(string query)
        {

            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            string[] data = new string[2];

            while (reader.Read())
            {
                data[0] = reader["weight"].ToString();
                data[1] = reader["height"].ToString();
            }

            return data;
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

    }
}
