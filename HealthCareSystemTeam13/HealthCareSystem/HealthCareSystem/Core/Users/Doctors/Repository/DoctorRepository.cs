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

namespace HealthCareSystem.Core.Users.Doctors.Repository
{
    class DoctorRepository
    {
        public OleDbConnection Connection { get; set; }
  

        public string Username { get; set; }
        public DataTable examinations { get; set; }
        public DataTable medicine { get; set; }

        public DoctorRepository(string username="", bool calledFromDoctor=false)
        {
            if(username.Length > 0) Username = username;
            try
            {
                Connection = new OleDbConnection();

                Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=HCDb.mdb;
                Persist Security Info=False;";

                if(calledFromDoctor)
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
            examinations = new DataTable();

            string examinationsQuery = "select Examination.id, Patients.FirstName + ' ' + Patients.LastName as Patient," +
                " dateOf as [Date and Time], id_room as RoomID, duration as Duration, typeOfExamination as Type from Examination" +
                " left outer join Patients  on Examination.id_patient = Patients.id " +
                "where id_doctor = " + GetDoctorId() + "";

            FillTable(examinations, examinationsQuery);
        }

        public void PullMedicine()
        {
            medicine = new DataTable();

            string medicineQuery = "select id, nameOfMedication as Name" +
                " from Medications ";

            FillTable(medicine, medicineQuery);
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
                DoctorSpeciality speciality;
                Enum.TryParse<DoctorSpeciality>(reader["speciality"].ToString(), out speciality);

                doctor = new Doctor(Convert.ToInt32(reader["id"]), reader["firstName"].ToString(), reader["lastName"].ToString(),
                    Convert.ToInt32(reader["user_id"]), speciality);
            }
            return doctor;

        }


        public bool IsDoctorAvailable(Doctor doctor, DateTime ExaminationDateTime, List<Examination> examinations)
        {
            for (int i = 0; i < examinations.Count(); i++)
            {
                TimeSpan difference = ExaminationDateTime.Subtract(examinations[i].DateOf);
                Console.WriteLine(ExaminationDateTime.ToString());

                if (Math.Abs(difference.TotalMinutes) < 15 && doctor.ID == examinations[i].IdDoctor)
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsDoctorAvailable(int doctorID, DateTime ExaminationDateTime, List<Examination> examinations)
        {
            for (int i = 0; i < examinations.Count(); i++)
            {
                TimeSpan difference = ExaminationDateTime.Subtract(examinations[i].DateOf);
                Console.WriteLine(ExaminationDateTime.ToString());

                if (Math.Abs(difference.TotalMinutes) < 15 && doctorID == examinations[i].IdDoctor)
                {
                    return false;
                }
            }

            return true;
        }
        public Doctor GetAvailableDoctor(DateTime examinationDateTime, List<Examination> examinations)
        {
            BindingList<Doctor> doctors = GetDoctors();
            foreach(Doctor doctor in doctors)
            {
                if (IsDoctorAvailable(doctor, examinationDateTime, examinations)) return doctor;
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
                    DoctorSpeciality speciality;
                    Enum.TryParse<DoctorSpeciality>(reader["speciality"].ToString(), out speciality);

                    doctors.Add(new Doctor(Convert.ToInt32(reader["ID"]), reader["firstName"].ToString(), reader["lastName"].ToString(), Convert.ToInt32(reader["user_id"]), speciality));
                }
            }catch(Exception exception)
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
            examinations = new DataTable();


            string examinationsQuery = "select Examination.id, Patients.FirstName + ' ' + Patients.LastName as Patient," +
            " dateOf, id_room as RoomID, duration as Duration, typeOfExamination as Type from Examination" +
            " left outer join Patients  on Examination.id_patient = Patients.id " +
            "where id_doctor = " + GetDoctorId() + " and Day(dateOf) = Day('" + date + "')";
            FillTable(examinations, examinationsQuery);
        }

        public void PullExaminationsThreeDays()
        {
            examinations = new DataTable();
            DateTime firstDay = DateTime.Today.AddDays(1);
            DateTime secondDay = DateTime.Today.AddDays(2);
            DateTime thirdDay = DateTime.Today.AddDays(3);


            string examinationsQuery = "select Examination.id, Patients.FirstName + ' ' + Patients.LastName as Patient," +
            " dateOf, id_room as RoomID, duration as Duration, typeOfExamination as Type from Examination" +
            " left outer join Patients  on Examination.id_patient = Patients.id " +
            "where id_doctor = " + GetDoctorId() + " and (" +
            "Day(dateOf) = Day('" + firstDay + "') or Day(dateOf) = Day('" + secondDay + "') or Day(dateOf) = Day('" + thirdDay + "'))";
            FillTable(examinations, examinationsQuery);
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
                if(option == 1)
                    cmd.Parameters.AddWithValue("@id_forwarded_doctor", referralLetter.ForwardedDoctorID);
                else if (option == 2)
                    cmd.Parameters.AddWithValue("@id_forwarded_doctor", DBNull.Value);
                Console.WriteLine(referralLetter.ExaminationType);
                cmd.Parameters.AddWithValue("@typeOfExamination", referralLetter.ExaminationType);
                if(option == 2)
                    cmd.Parameters.AddWithValue("@speciality", referralLetter.Speciality);
                else if (option == 1)
                    cmd.Parameters.AddWithValue("@id_forwarded_doctor", DBNull.Value);
                cmd.ExecuteNonQuery();
            }

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();
        }

    }
}
