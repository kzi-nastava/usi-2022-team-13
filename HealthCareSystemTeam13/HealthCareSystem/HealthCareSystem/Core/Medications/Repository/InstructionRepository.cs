using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Medications.Repository
{
    class InstructionRepository
    {
        public OleDbConnection Connection { get; set; }

        public InstructionRepository()
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
        public void InsertInstruction(string description)
        {

            DateTime startTime = DateTime.Now;
            int timesPerDay = 2;

            string query = "insert into Instructions (startTime, timesPerDay, description) " +
                           "values (@startTime, @timesPerDay, @description )";

            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@startTime", startTime.ToString());
                cmd.Parameters.AddWithValue("@timesPerDay", timesPerDay);
                cmd.Parameters.AddWithValue("@description", description);
                cmd.ExecuteNonQuery();
            }

        }
        public Dictionary<int, DateTime> GetMedicationInstructions(int patientId)
        {
            Dictionary<int, DateTime> instructions = new Dictionary<int, DateTime>();

            string query = "select ins.startTime as startTime, ins.timesPerDay as perDay from Receipt as r inner join Instructions as ins on r.id_instructions = ins.id where r.id_patient = " + patientId + "";

            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                instructions.Add(Convert.ToInt32(reader["perDay"]), (DateTime)reader["startTime"]);
            }

            return instructions;
        }

        public int GetLastCreatedInstructionId()
        {
            int lastCreatedInstructionId = 0;
            string query = "select top 1 ID from Instructions order by id desc";
            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lastCreatedInstructionId = Convert.ToInt32(reader["ID"]);
            }

            return lastCreatedInstructionId;
        }
    }
}
