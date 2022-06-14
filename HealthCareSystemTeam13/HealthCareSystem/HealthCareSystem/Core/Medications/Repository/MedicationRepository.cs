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
        public DataTable Medications { get; private set; }

        public MedicationRepository()
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
        public int GetMedicationNotificationTime(int patientId)
        {
            string query = "select notificationTime from Patients where id = " + patientId + "";
            int hoursBefore = Convert.ToInt32(DatabaseCommander.ExecuteReaderQueries(query, Connection)[0]);

            return hoursBefore;
        }

        public void SetMedicationNotificationTime(int newTime, int patientId)
        {
            string query = "Update Patients set notificationTime = " + newTime + " where id = " + patientId + "";
            DatabaseCommander.ExecuteNonQueries(query, Connection);
        }

        public void InsertConnectionOfReceiptAndMedication(int receiptId, int medicationId)
        {

            string query = "insert into ReceiptMedications (id_receipt, id_medication)" +
                           " values (@id_receipt, @id_medication)";

            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_receipt", receiptId);
                cmd.Parameters.AddWithValue("@id_medication", medicationId);

                cmd.ExecuteNonQuery();
            }

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

            return medications;

        }

        public void UpdateMedication(string query)
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            DatabaseCommander.ExecuteNonQueries(query, Connection);
        }

        public void InsertRejectedMedication(string reasonForDenying, int medicationId, int doctorId)
        {

            string insertQuery = "insert into RejectedMedications (id_medication, id_doctor, description)" +
                                 " values (@id_medication, @id_doctor, @description)";

            using (var cmd = new OleDbCommand(insertQuery, Connection))
            {
                cmd.Parameters.AddWithValue("@id_medication", medicationId);
                cmd.Parameters.AddWithValue("@id_doctor", doctorId);
                cmd.Parameters.AddWithValue("@description", reasonForDenying.ToString());
                cmd.ExecuteNonQuery();
            }

        }

        public void PullMedicine()
        {
            Medicine = new DataTable();

            string medicineQuery = "select id, nameOfMedication as Name" +
                " from Medications where status = 'Approved'";

            GUIHelpers.FillTable(Medicine, medicineQuery, Connection);
        }

        public void PullMedications()
        {
            Medications = new DataTable();
            string medicationsQuery = "select m.ID, m.nameOfMedication, m.status, r.id_doctor, r.description" +
                " from medications m left join RejectedMedications r on m.Id = r.id_medication";
            GUIHelpers.FillTable(Medications, medicationsQuery, Connection);
        }
        public Medication GetSelectedMedication(string query)
        {

            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);

            Medication medication = new Medication();

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Enum.TryParse<MedicationStatus>(reader["status"].ToString(), out var medicationState);
                medication = new Medication(Convert.ToInt32(reader["id"]), reader["nameOfMedication"].ToString(), medicationState);
            }

            return medication;
        }
        public void InsertMedicationContainsIngredient(int medicationId, int ingredientId)
        {
            var insertQuery = "INSERT INTO medicationContainsIngredient(id_medication, id_ingredient) VALUES(@id_medication, @id_ingredient)";
            using (var cmd = new OleDbCommand(insertQuery, Connection))
            {
                cmd.Parameters.AddWithValue("@id_medication", medicationId);
                cmd.Parameters.AddWithValue("@id_ingredient", ingredientId);
                cmd.ExecuteNonQuery();

            }
        }
        public bool DoesMedicationExists(string name)
        {
            string query = "select * from Medications where nameOfMedication='" + name + "'";
            Medication medication = GetSelectedMedication(query);

            if (medication.Name == name) return false;
            return true;
        }
        public void InsertMedication(string ingredientName)
        {
            var insertQuery = "INSERT INTO medications(nameOfMedication, status) VALUES(@nameOfMedication, @status)";
            using (var cmd = new OleDbCommand(insertQuery, Connection))
            {
                cmd.Parameters.AddWithValue("@nameOfMedication", ingredientName);
                cmd.Parameters.AddWithValue("@status", MedicationStatus.InProgress.ToString());
                cmd.ExecuteNonQuery();

            }
        }
        public List<int> GetAlergicMedicationsIds(int patientId)
        {

            List<int> alergicMedicationIds = new List<int>();

            string query = "" +
                           "select MedicationContainsIngredient.id_medication" +
                           " from MedicationContainsIngredient inner join PatientAlergicTo " +
                           "on MedicationContainsIngredient.id_ingredient = PatientAlergicTo.id_ingredient " +
                           " where PatientAlergicTo.id_patient = " + patientId;

            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                alergicMedicationIds.Add(Convert.ToInt32
                    (reader["id_medication"]));
            }
            return alergicMedicationIds;
        }

    }
}
