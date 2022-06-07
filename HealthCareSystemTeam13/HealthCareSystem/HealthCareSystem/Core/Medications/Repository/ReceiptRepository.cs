using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Medications.Repository
{
    class ReceiptRepository
    {
        public OleDbConnection Connection { get; set; }
        private InstructionRepository _instructionRepository;

        public ReceiptRepository()
        {
            try
            {
                Connection = new OleDbConnection();

                Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../../Data/HCDb.mdb;
                    Persist Security Info=False;";
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

            _instructionRepository = new InstructionRepository();
        }
        public void InsertReceipt(int doctorId, int patientId, DateTime dateOf)
        {
            int instructionId = _instructionRepository.GetLastCreatedInstructionId();

            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

            string query = "insert into Receipt (id_instructions, id_doctor, id_patient, dateOf)" +
                           " values (@id_instructions, @id_doctor, @id_patient, @dateOf )";

            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_instructions", instructionId);
                cmd.Parameters.AddWithValue("@id_doctor", doctorId);
                cmd.Parameters.AddWithValue("@id_patient", patientId);
                cmd.Parameters.AddWithValue("@dateOf", dateOf.ToString());

                cmd.ExecuteNonQuery();
            }

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();

        }

        public int GetLastReceiptId()
        {
            int lastCreatedReceiptnId = 0;
            string query = "select top 1 ID from Receipt order by id desc";
            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lastCreatedReceiptnId = Convert.ToInt32(reader["ID"]);
            }

            return lastCreatedReceiptnId;
        }
    }
}
