using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Users.Doctors.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using HealthCareSystem.Core.Users.Patients.Model;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Users.Doctors.Service;
using HealthCareSystem.Core.Medications.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.Model;

namespace HealthCareSystem.Core.Users.Doctors.Repository
{
    class DoctorRepository
    {
        public OleDbConnection Connection { get; set; }

        public string Username { get; set; }
        public DataTable Examinations { get; set; }
        public DataTable Medicine { get; set; }

        public DoctorRepository(string username = "", bool calledFromDoctor = false)
        {
            if (username.Length > 0) Username = username;
            try
            {
                Connection = new OleDbConnection();

                Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../../Data/HCDb.mdb;
                Persist Security Info=False;";

                if (calledFromDoctor)
                {
                    Connection.Open();
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

        }


        public void PullExaminations()
        {
            Examinations = new DataTable();

            string examinationsQuery = "select Examination.id, Patients.FirstName + ' ' + Patients.LastName as Patient," +
                " dateOf as [Date and Time], id_room as RoomID, duration as Duration, typeOfExamination as Type from Examination" +
                " left outer join Patients  on Examination.id_patient = Patients.id " +
                "where id_doctor = " + GetDoctorId() + "";

            FillTable(Examinations, examinationsQuery);
        }

        public void PullMedicine()
        {
            Medicine = new DataTable();

            string medicineQuery = "select id, nameOfMedication as Name" +
                " from Medications where status = 'Approved'";

            FillTable(Medicine, medicineQuery);
        }

        private void FillTable(DataTable table, string query)
        {

            using (var cmd = new OleDbCommand(query, Connection))
            {
                OleDbDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
            }
        }

        public int GetDoctorId()
        {
            string userId = DatabaseHelpers.ExecuteReaderQueries("select id from users where usrnm = '" + Username + "'", Connection)[0];

            int doctorId = Convert.ToInt32(DatabaseHelpers.ExecuteReaderQueries("select id from doctors where user_id = " + Convert.ToInt32(userId) + "", Connection)[0]);
            Console.WriteLine(doctorId);
            return doctorId;
        }

        public Doctor GetDoctorByUsername()
        {
            string userId = DatabaseHelpers.ExecuteReaderQueries("select id from users where usrnm = '" + Username + "'", Connection)[0];

            Doctor doctor = new Doctor();

            OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from doctors where user_id = " + Convert.ToInt32(userId) + "", Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                doctor = GetDoctorFromReader(reader);
            }
            return doctor;

        }

        private static Doctor GetDoctorFromReader(OleDbDataReader reader)
        {
            Doctor doctor;
            DoctorSpeciality speciality;
            Enum.TryParse<DoctorSpeciality>(reader["speciality"].ToString(), out speciality);

            doctor = new Doctor(Convert.ToInt32(reader["id"]), reader["firstName"].ToString(), reader["lastName"].ToString(),
                Convert.ToInt32(reader["user_id"]), speciality);
            return doctor;
        }


        public Doctor GetAvailableDoctor(DateTime examinationDateTime, List<Examination> examinations)
        {
            BindingList<Doctor> doctors = GetDoctors();
            foreach (Doctor doctor in doctors)
            {
                if (DoctorService.IsDoctorAvailable(doctor.ID, examinationDateTime, examinations)) return doctor;
            }
            return null;

        }
        public BindingList<Doctor> GetDoctors()
        {
            BindingList<Doctor> doctors = new BindingList<Doctor>();
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }
            try
            {

                OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from doctors", Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    doctors.Add(GetDoctorFromReader(reader));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();

            return doctors;
        }


        public Patient GetSelectedPatient(string query)
        {
            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);

            Patient patient = new Patient();

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                patient = SetPatientValues(reader);
            }
            return patient;
        }

        public List<int> GetAlergicMedicationsIds(int patientId)
        {

            List<int> alergicMedicationIds = new List<int>();

            string query = "" +
                "select MedicationContainsIngredient.id_medication" +
                " from MedicationContainsIngredient inner join PatientAlergicTo " +
                "on MedicationContainsIngredient.id_ingredient = PatientAlergicTo.id_ingredient " +
                " where PatientAlergicTo.id_patient = " + patientId;

            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                alergicMedicationIds.Add(Convert.ToInt32
                    (reader["id_medication"]));
            }



            return alergicMedicationIds;
        }

        public void InsertInstruction(string description)
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

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

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();
        }

        public void InsertReceipt(int doctorId, int patientId, DateTime dateOf)
        {
            int instructionId = GetLastCreatedInstructionId();

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

        public int GetLastReceiptId()
        {
            int lastCreatedReceiptnId = 0;
            string query = "select top 1 ID from Receipt order by id desc";
            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lastCreatedReceiptnId = Convert.ToInt32(reader["ID"]);
            }

            return lastCreatedReceiptnId;
        }

        public int GetLastCreatedInstructionId()
        {
            int lastCreatedInstructionId = 0;
            string query = "select top 1 ID from Instructions order by id desc";
            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lastCreatedInstructionId = Convert.ToInt32(reader["ID"]);
            }

            return lastCreatedInstructionId;
        }

        public String GetMedicationNameById(int medicationId)
        {
            string medicationName = "";
            string query = "select nameOfMedication from Medications where ID = " + medicationId;
            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                medicationName = reader["nameOfMedication"].ToString();
            }

            return medicationName;
        }

        private static Patient SetPatientValues(OleDbDataReader reader)
        {
            return new Patient(
            Convert.ToInt32(reader["Patients.ID"]),
            reader["firstName"].ToString(), reader["lastName"].ToString(),
            Convert.ToInt32(reader["user_id"]), false
            );
        }

        public void CancelExamination(int examinationId)
        {
            string query = "delete from Examination where id = " + examinationId + "";
            DatabaseHelpers.ExecuteNonQueries(query, Connection);
        }

        public void PullExaminationsByDate(DateTime date)
        {
            Examinations = new DataTable();


            string examinationsQuery = "select Examination.id, Patients.FirstName + ' ' + Patients.LastName as Patient," +
            " dateOf, id_room as RoomID, duration as Duration, typeOfExamination as Type from Examination" +
            " left outer join Patients  on Examination.id_patient = Patients.id " +
            "where id_doctor = " + GetDoctorId() + " and Day(dateOf) = Day('" + date + "')";
            FillTable(Examinations, examinationsQuery);
        }

        public void PullExaminationsThreeDays()
        {
            Examinations = new DataTable();
            DateTime firstDay = DateTime.Today.AddDays(1);
            DateTime secondDay = DateTime.Today.AddDays(2);
            DateTime thirdDay = DateTime.Today.AddDays(3);


            string examinationsQuery = "select Examination.id, Patients.FirstName + ' ' + Patients.LastName as Patient," +
            " dateOf, id_room as RoomID, duration as Duration, typeOfExamination as Type from Examination" +
            " left outer join Patients  on Examination.id_patient = Patients.id " +
            "where id_doctor = " + GetDoctorId() + " and (" +
            "Day(dateOf) = Day('" + firstDay + "') or Day(dateOf) = Day('" + secondDay + "') or Day(dateOf) = Day('" + thirdDay + "'))";
            FillTable(Examinations, examinationsQuery);
        }

        public void InsertReferral(ReferralLetter referralLetter, int option)
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

            string query = "INSERT INTO ReferralLetter" +
                "(id_doctor, id_patient, id_forwarded_doctor, typeOfExamination, speciality) " +
                "VALUES (@id_doctor, @id_patient," +
                " @id_forwarded_doctor, @typeOfExamination, @speciality)";

            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_doctor", referralLetter.CurrentDoctorID);
                cmd.Parameters.AddWithValue("@id_patient", referralLetter.CurrentPatientID);

                if (option == 1)
                    cmd.Parameters.AddWithValue("@id_forwarded_doctor", referralLetter.ForwardedDoctorID);
                else if (option == 2)
                    cmd.Parameters.AddWithValue("@id_forwarded_doctor", DBNull.Value);
                Console.WriteLine(referralLetter.ExaminationType);
                cmd.Parameters.AddWithValue("@typeOfExamination", referralLetter.ExaminationType);
                if (option == 2)
                    cmd.Parameters.AddWithValue("@speciality", referralLetter.Speciality);
                else if (option == 1)
                    cmd.Parameters.AddWithValue("@id_forwarded_doctor", DBNull.Value);

                cmd.ExecuteNonQuery();
            }

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();
        }
        public List<Doctor> GetDoctorsWithAverageRating()
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            string query = "select dr.id as DoctorID, dr.firstName as FirstName, dr.lastName as LastName, dr.user_id as UserId, dr.speciality as Speciality, avg(ds.doctorGrade) as Rating  from Doctors as dr left outer join DoctorSurveys ds on dr.id = ds.id_doctor group by dr.id, dr.firstName, dr.lastName, dr.user_id, dr.speciality";

            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();
            List<Doctor> doctors = new List<Doctor>();
            while (reader.Read())
            {
                SetDoctorValuesWithRating(doctors, reader);
            }

            if (Connection.State == ConnectionState.Open) Connection.Close();
            return doctors;
        }
        private static void SetDoctorValuesWithRating(List<Doctor> doctors, OleDbDataReader reader)
        {
            DoctorSpeciality speciality;
            Enum.TryParse<DoctorSpeciality>(reader["Speciality"].ToString(), out speciality);

            doctors.Add(new Doctor(
                Convert.ToInt32(reader["DoctorID"]),
                reader["FirstName"].ToString(),
                reader["LastName"].ToString(),
                Convert.ToInt32(reader["UserId"]),
                speciality,
                Convert.ToDouble(reader["Rating"])
                ));

        }
        public int GetDoctorIdByFullName(string firstName, string lastName)
        {
            string query = "select id from Doctors where firstName = '" + firstName + "' and lastName = '" + lastName + "'";
            int doctorId = Convert.ToInt32(DatabaseHelpers.ExecuteReaderQueries(query, Connection)[0]);
            return doctorId;
        }

        public BindingList<Medication> GetMedications()
        {
            BindingList<Medication> medications = new BindingList<Medication>();
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }
            try
            {

                OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from Medications", Connection);
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
            DatabaseHelpers.ExecuteNonQueries(query, Connection);
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

        public int GetRoomIdFromExaminationId(int examinationId)
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

            string query = "select id_room from Examination where id = " + examinationId + "";

            int roomNumber = -1;

            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                roomNumber = Convert.ToInt32(reader["id_room"]);
            }

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();

            return roomNumber;
        }

        public List<Equipment> GetEquipmentFromRoomId(int roomId)
        {

            List<Equipment> equipment = new List<Equipment>();

            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }
            try
            {
                string query = "select id_equipment, amount, Equipment.nameOf from RoomHasEquipment, Equipment" +
                    " where Equipment.id = id_equipment and RoomHasEquipment.id_room = " + roomId;

                OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Equipment equipmentEntity = new Equipment(reader["nameOf"].ToString(),
                        Convert.ToInt32(reader["id_equipment"]), Convert.ToInt32(reader["amount"]));
                    equipment.Add(equipmentEntity);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();

            return equipment;
        }

        public void UpdateAmountOfEquipmentInTheRoom(int amount, int roomId, int equipmentId)
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();

            string updateQuery = "update RoomHasEquipment set amount = " + amount +
                " where id_room = " + roomId + " and id_equipment = " + equipmentId;

            DatabaseHelpers.ExecuteNonQueries(updateQuery, Connection);
        }

        public List<DateTime> GetDateOfExaminationsForDoctor()
        {
            List<DateTime> dates = new List<DateTime>();
            string query = "select dateOf where id_doctor = " + GetDoctorId();


            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }
            try
            {

                OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    dates.Add((DateTime)reader["DateOf"]);
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();




            return dates;
        }




    }
}
