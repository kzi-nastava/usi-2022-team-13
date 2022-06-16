using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Users.Patients.Model;

namespace HealthCareSystem.Core.Examinations.Repository
{
    class ExaminationChangesRepository
    {
        public OleDbConnection Connection { get; set; }
        public ExaminationChangesRepository()
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
        public List<ExaminationChange> GetExaminationChanges(int patientId)
        {
            List<ExaminationChange> changes = new List<ExaminationChange>();
            string query = "select * from PatientExaminationChanges where id_patient = " + patientId + "";

            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Enum.TryParse<TypeOfChange>(reader["typeOfChange"].ToString(), out var typeOfChange);
                changes.Add(new ExaminationChange(patientId, typeOfChange, (DateTime)reader["dateOf"]));

            }
            return changes;
        }
    }
}
