using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Medications.Model;

namespace HealthCareSystem.Core.Medications.Repository
{
    class MedicationRepository
    {
        public OleDbConnection Connection { get; set; }
        public DataTable Medicine { get; private set; }

        public MedicationRepository()
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
        }
        public Dictionary<int, DateTime> GetMedicationInstructions()
        {
            Dictionary<int, DateTime> instructions = new Dictionary<int, DateTime>();
            int patientId = GetPatientId();
            string query = "select ins.startTime as startTime, ins.timesPerDay as perDay from Receipt as r inner join Instructions as ins on r.id_instructions = ins.id where r.id_patient = " + patientId + "";

            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                instructions.Add(Convert.ToInt32(reader["perDay"]), (DateTime)reader["startTime"]);
            }

            return instructions;
        }
        public int GetMedicationNotificationTime()
        {
            int patientId = GetPatientId();
            string query = "select notificationTime from Patients where id = " + patientId + "";
            int hoursBefore = Convert.ToInt32(DatabaseCommander.ExecuteReaderQueries(query, Connection)[0]);

            return hoursBefore;
        }

        public void SetMedicationNotificationTime(int newTime)
        {
            int patientId = GetPatientId();
            string query = "Update Patients set notificationTime = " + newTime + " where id = " + patientId + "";
            DatabaseCommander.ExecuteNonQueries(query, Connection);
        }

        public void InsertConnectionOfReceiptAndMedication(int receiptId, int medicationId)
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

            string query = "insert into ReceiptMedications (id_receipt, id_medication)" +
                " values (@id_receipt, @id_medication)";

            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_receipt", receiptId);
                cmd.Parameters.AddWithValue("@id_medication", medicationId);

                cmd.ExecuteNonQuery();

            }

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();
        }

        public String GetMedicationNameById(int medicationId)
        {
            string medicationName = "";
            string query = "select nameOfMedication from Medications where ID = " + medicationId;
            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                medicationName = reader["nameOfMedication"].ToString();
            }

            return medicationName;
        }

        public BindingList<Medication> GetMedications()
        {
            BindingList<Medication> medications = new BindingList<Medication>();
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }
            try
            {

                OleDbCommand cmd = DatabaseCommander.GetCommand("select * from Medications", Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    MedicationStatus status;
                    Enum.TryParse<MedicationStatus>(reader["status"].ToString(), out status);

                    Medication medication = new Medication(Convert.ToInt32(reader["id"]), reader["nameOfMedication"].ToString(), status);
                    if (reader["status"].ToString() == "InProgress")
                    {
                        medications.Add(medication);

                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();

            return medications;

        }

        public void UpdateMedication(string query)
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            DatabaseCommander.ExecuteNonQueries(query, Connection);
        }

        public void InsertRejectedMedication(string reasonForDenying, int medicationId, int doctorId)
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

            string insertQuery = "insert into RejectedMedications (id_medication, id_doctor, description)" +
            " values (@id_medication, @id_doctor, @description)";

            using (var cmd = new OleDbCommand(insertQuery, Connection))
            {
                cmd.Parameters.AddWithValue("@id_medication", medicationId);
                cmd.Parameters.AddWithValue("@id_doctor", doctorId);
                cmd.Parameters.AddWithValue("@description", reasonForDenying.ToString());
                cmd.ExecuteNonQuery();
            }

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();

        }

        public void PullMedicine()
        {
            Medicine = new DataTable();

            string medicineQuery = "select id, nameOfMedication as Name" +
                " from Medications where status = 'Approved'";

            FillTable(Medicine, medicineQuery);
        }


    }
}
