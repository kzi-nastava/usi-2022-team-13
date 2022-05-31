using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using HealthCareSystem.Core;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Rooms.Model;
using HealthCareSystem.Core.Scripts.Repository;
using HealthCareSystem.Core.Users.Patients.Model;
using System.ComponentModel;
using HealthCareSystem.Core.Examinations.Repository;
using HealthCareSystem.Core.Surveys.HospitalSurveys.Model;

namespace HealthCareSystem.Core.Users.Patients.Repository
{
    class PatientRepository
    {
        public string Username { get; set; }
        public DataTable Examinations { get; set; }
        public OleDbConnection Connection { get; set; }
        private ExaminationRepository ExaminationRep;


        public PatientRepository(string username = "") { 
            if(username.Length > 0) Username = username;
            try
            {
                Connection = new OleDbConnection();

                Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=HCDb.mdb;
                Persist Security Info=False;";

                Connection.Open();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            ExaminationRep = new ExaminationRepository();
        
        }
        public int GetPatientId()
        {
            string userId = DatabaseHelpers.ExecuteReaderQueries("select id from users where usrnm = '" + Username + "'", Connection)[0];

            int patientId = Convert.ToInt32(DatabaseHelpers.ExecuteReaderQueries("select id from patients where user_id = " + Convert.ToInt32(userId) + "", Connection)[0]);

            return patientId;
        }

        internal void AddDoctorSurvey(int doctorId, int patientId, int rating, int quality, bool wouldReccomend, string comment)
        {
            string query = "insert into DoctorSurveys(id_doctor, id_patient, doctorGrade, quality, wouldRecommend, comment) values("+doctorId+", "+patientId+", "+rating+", "+quality+", "+ wouldReccomend + ", '"+comment+"')";

            DatabaseHelpers.ExecuteNonQueries(query, Connection);
        }

        public Dictionary<string, string> GetPatientNameAndMedicalStats(int patientId)
        {
            var query = "select Patients.firstName, Patients.lastName, MedicalRecord.height, MedicalRecord.weight from Patients inner join MedicalRecord on Patients.ID = MedicalRecord.id_patient WHERE patients.id = " + patientId + "";
            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();
            Dictionary<string, string> patientInfo = new Dictionary<string, string>();
            while (reader.Read())
            {
                patientInfo["firstName"] = reader["firstName"].ToString();
                patientInfo["lastName"] = reader["lastName"].ToString();
                patientInfo["height"] = reader["height"].ToString();
                patientInfo["weight"] = reader["weight"].ToString();

            }
            return patientInfo;
        }


        internal List<DoctorAnamnesis> GetAnamnesises(List<Examination> examinations)
        {
            List<DoctorAnamnesis> anamnesises = new List<DoctorAnamnesis>();

            foreach (Examination examination in examinations)
            {
                DoctorAnamnesis anamnesis = ExaminationRep.GetDoctorAnamnesis(examination.Id);
                if (anamnesis != null) anamnesises.Add(anamnesis);
            }


            return anamnesises;
        }

        
        public List<DoctorAnamnesis> GetAnamnesisesByKeyword(List<DoctorAnamnesis> anamnesises, string keyword)
        {
            List<DoctorAnamnesis> filteredAnamnesises = new List<DoctorAnamnesis>();
            foreach(DoctorAnamnesis anamnesis in anamnesises)
            {
                if (IsKeywordInAnamnesis(keyword, anamnesis)) 
                    filteredAnamnesises.Add(anamnesis);
            }
            return filteredAnamnesises;
        }

        private static bool IsKeywordInAnamnesis(string keyword, DoctorAnamnesis anamnesis)
        {
            return anamnesis.Notice.ToLower().Contains(keyword.ToLower()) || anamnesis.Conclusions.ToLower().Contains(keyword.ToLower());
        }

        public int GetPatientIdByFirstName(string firstName)
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

            int patientId = Convert.ToInt32(
                DatabaseHelpers.ExecuteReaderQueries("select id from Patients where firstName = '" + firstName + "'", Connection)[0]);

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();

            return patientId;
        }


        public void PullExaminationForPatient()
        {

            Examinations = new DataTable();
    
            string examinationsQuery = "select Examination.id, Doctors.FirstName + ' ' +Doctors.LastName as Doctor, dateOf as [Date and Time], id_room as RoomID, duration, typeOfExamination as Type from Examination left outer join Doctors  on Examination.id_doctor = Doctors.id " +
                "where id_patient = " + GetPatientId() +"";

            FillTable(Examinations, examinationsQuery);

        }
        public void PullFinishedExaminations()
        {
            Examinations = new DataTable();

            string examinationsQuery = "select Examination.id, Doctors.FirstName + ' ' +Doctors.LastName as Doctor, Examination.dateOf as [Date and Time], Examination.id_room as RoomID, Examination.duration, typeOfExamination as Type from Examination left outer join Doctors on Examination.id_doctor = Doctors.id " +
                "where id_patient = " + GetPatientId() + " and Examination.dateOf < #"+DateTime.Now.ToString()+"#";

            FillTable(Examinations, examinationsQuery);
        }

        public void UpdateContent(string query, int patiendId = 0)
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

            DatabaseHelpers.ExecuteNonQueries(query, Connection);
            if(patiendId > 0) InsertExaminationChanges(TypeOfChange.Edit, patiendId);
            else InsertExaminationChanges(TypeOfChange.Edit);

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();

        }

        public void SendExaminationEditRequest(int examinationId, DateTime currentTime, bool isEdit, int doctorId, DateTime newDateTime, int roomId)
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            string query = "insert into PatientEditRequest (id_examination, dateOf, isChanged, isDeleted, id_doctor, dateTimeOfExamination, id_room) VALUES(@id_examination, @dateOf, @isChanged, @isDeleted, @id_doctor, @dateTimeOfExamination, @id_room)";

            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_examination", examinationId);
                cmd.Parameters.AddWithValue("@dateOf", currentTime.ToString());
                if (isEdit)
                {
                    cmd.Parameters.AddWithValue("@isChanged", true);
                    cmd.Parameters.AddWithValue("@isDeleted", false);
                    cmd.Parameters.AddWithValue("@id_doctor", doctorId);
                    cmd.Parameters.AddWithValue("@dateTimeOfExamination", newDateTime.ToString());
                    cmd.Parameters.AddWithValue("@id_room", roomId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@isChanged", false);
                    cmd.Parameters.AddWithValue("@isDeleted", true);
                    cmd.Parameters.AddWithValue("@id_doctor", DBNull.Value);
                    cmd.Parameters.AddWithValue("@dateTimeOfExamination", DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_room", DBNull.Value);
                }

                cmd.ExecuteNonQuery();

            }

        }


        public void InsertExaminationChanges(TypeOfChange typeOfChange, int parsedPatientId = 0)
        {
            int patientId;
            if (parsedPatientId == 0) { patientId = GetPatientId(Username); }
            else { patientId = parsedPatientId; }
            string insertQuery = "insert into PatientExaminationChanges(id_patient, typeOfChange, dateOf) values(@id_patient, @typeOfChange, @dateOf)";

            using (var cmd = new OleDbCommand(insertQuery, Connection))
            {
                cmd.Parameters.AddWithValue("@id_patient", patientId);
                cmd.Parameters.AddWithValue("@typeOfChange", typeOfChange.ToString());
                cmd.Parameters.AddWithValue("@dateOf", DateTime.Now.ToString());

                cmd.ExecuteNonQuery();
            }

           
        }

        public void InsertExamination(string patientUsername, int doctorId, DateTime examinationDateTime,
            int duration, int roomId, string selectedType="")
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }
             

            int patientId = GetPatientId(patientUsername);

            string insertQuery = "insert into Examination(id_doctor, id_patient, isEdited, isCancelled, isFinished, dateOf, typeOfExamination, isUrgent, id_room, duration) values(@id_doctor, @id_patient, @isEdited, @isCancelled, @isFinished, @dateOf, @typeOfExamination, @isUrgent, @id_room, @duration)";

            using (var cmd = new OleDbCommand(insertQuery, Connection))
            {
                cmd.Parameters.AddWithValue("@id_doctor", doctorId);
                cmd.Parameters.AddWithValue("@id_patient", patientId);
                cmd.Parameters.AddWithValue("@isEdited", false);
                cmd.Parameters.AddWithValue("@isCancelled", false);
                cmd.Parameters.AddWithValue("@isFinished", false);
                cmd.Parameters.AddWithValue("@dateOf", examinationDateTime.ToString());
                
                if(selectedType.Length == 0 || selectedType == TypeOfExamination.BasicExamination.ToString())
                {
                    cmd.Parameters.AddWithValue("@typeOfExamination", TypeOfExamination.BasicExamination.ToString());
                } else
                {
                    cmd.Parameters.AddWithValue("@typeOfExamination", TypeOfExamination.Operation.ToString());
                }
                cmd.Parameters.AddWithValue("@isUrgent", false);
                cmd.Parameters.AddWithValue("@id_room", roomId);
                cmd.Parameters.AddWithValue("@duration", 15);

                cmd.ExecuteNonQuery();
            }

            Username = patientUsername;
            InsertExaminationChanges(TypeOfChange.Add);
            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();

        }



        private int GetPatientId(string patientUsername)
        {
            string patientIdQuery = "select Patients.id from Patients inner join Users on Patients.user_id = Users.id where Users.usrnm = '" + patientUsername + "'";
            int patientId = Convert.ToInt32(DatabaseHelpers.ExecuteReaderQueries(patientIdQuery, Connection)[0]);
            return patientId;
        }

        public Dictionary<string, string> GetExamination(int examinationId)
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

            string query = "select id_doctor, dateOf, id_room, typeOfExamination from Examination where id = " + examinationId + "";


            Dictionary<string, string> row = new Dictionary<string, string>();
            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                row["id"] = examinationId.ToString();
                row["doctor_id"] = reader["id_doctor"].ToString();
                row["dateOf"] = reader["dateOf"].ToString();
                row["room_id"] = reader["id_room"].ToString();
                row["typeOfExamination"] = reader["typeOfExamination"].ToString();
            }

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();

            return row;
        }

        public void CancelExamination(int examinationId)
        {
            string query = "delete from Examination where id = " + examinationId + "";
            DatabaseHelpers.ExecuteNonQueries(query, Connection);
        }

        private void FillTable(DataTable table, string query)
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            using (var cmd = new OleDbCommand(query, Connection))
            {
                OleDbDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
            }
            if (Connection.State == ConnectionState.Open) Connection.Close();

        }

        public Doctor GetSelectedDoctor(string query)
        {
            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
            
            Doctor doctor = new Doctor();

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                DoctorSpeciality speciality;
                Enum.TryParse<DoctorSpeciality>(reader["speciality"].ToString(), out speciality);
                doctor = new Doctor(reader["firstName"].ToString(), reader["lastName"].ToString(), Convert.ToInt32(reader["user_id"]), speciality);

            }
            return doctor;
        }

        public static List<Doctor> GetDoctorsForExaminations(OleDbConnection connection)
        {
            List<Doctor> doctors = new List<Doctor>();

            OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from doctors", connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                DoctorSpeciality speciality;
                Enum.TryParse<DoctorSpeciality>(reader["speciality"].ToString(), out speciality);

                doctors.Add(new Doctor(reader["firstName"].ToString(), reader["lastName"].ToString(), Convert.ToInt32(reader["user_id"]), speciality));
            }

            return doctors;
        }

        public BindingList<Patient> GetPatients()
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();

            BindingList<Patient> patients = new BindingList<Patient>();
            try
            {
                OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from Patients", Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    patients.Add(new Patient(
                        Convert.ToInt32(reader["ID"]), reader["firstName"].ToString(),
                        reader["lastName"].ToString(), Convert.ToInt32(reader["user_id"]), 
                        Convert.ToBoolean(reader["isBlocked"])
                    ));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            if(Connection.State == ConnectionState.Open) Connection.Close();

            return patients;
        }

        public string GetUsernameFromPatient(Patient patient)
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();

            string query = "select Users.usrnm from Users inner join " +
                "Patients on Patients.user_id = Users.id where Patients.id = " + patient.ID ;
            
            string patientUsername = DatabaseHelpers.ExecuteReaderQueries(query, Connection)[0];
            if (Connection.State == ConnectionState.Open) Connection.Close();
            return patientUsername;
        }

        public string[] GetMedicalRecord(string query)
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            string[] data = new string[2];

            while (reader.Read())
            {
                data[0] = reader["weight"].ToString();
                data[1] = reader["height"].ToString();
            }

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();

            return data;
        }

        public bool IsPatientBlocked(string patientUsername)
        {
            if (Connection.State == System.Data.ConnectionState.Closed) Connection.Open();

            List<string> userIds = DatabaseHelpers.ExecuteReaderQueries("select id from users where usrnm= '" + patientUsername + "'", Connection);

            List<string> blockedPatients = DatabaseHelpers.ExecuteReaderQueries("select isBlocked from Patients where user_id = " + Convert.ToInt32(userIds[0]) + "", Connection);

            if (Connection.State == ConnectionState.Open) Connection.Close();

            return blockedPatients[0] == "True";
        }
        public void BlockSpamPatients(string patientUsername)
        {
            int patientId = GetPatientId(patientUsername);
            List<ExaminationChange> changes = GetExaminationChanges(patientId);
            int adds = 0, edits = 0;

            for (int i = 0; i < changes.Count(); i++)
            {

                if (DateTime.Now.Subtract(changes[i].DateOfChange).TotalDays <= 30)
                {
                    if (changes[i].Change == TypeOfChange.Add) adds++;
                    else if (changes[i].Change == TypeOfChange.Edit || changes[i].Change == TypeOfChange.Delete) edits++;
                }
            }

            if (adds > 8 || edits > 4)
            {
                BlockPatient(patientId);
            }

        }
        private void BlockPatient(int patientId)
        {
            string query = "Update Patients set isBlocked = " + true + " where id = " + patientId + "";
            DatabaseHelpers.ExecuteNonQueries(query, Connection);


            query = "INSERT INTO BlockedPatients(id_patient, id_secretary, dateOf) VALUES(@id_patient, @id_secretary, @dateOf)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_patient", patientId);
                cmd.Parameters.AddWithValue("@id_secretary", DBNull.Value);
                cmd.Parameters.AddWithValue("@dateOf", DateTime.Now.ToString());
                cmd.ExecuteNonQuery();
            }

        }

        private List<ExaminationChange> GetExaminationChanges(int patientId)
        {
            List<ExaminationChange> changes = new List<ExaminationChange>();
            string query = "select * from PatientExaminationChanges where id_patient = " + patientId + "";
            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TypeOfChange typeOfChange;
                Enum.TryParse<TypeOfChange>(reader["typeOfChange"].ToString(), out typeOfChange);
                changes.Add(new ExaminationChange(patientId, typeOfChange, (DateTime)reader["dateOf"]));

            }
            return changes;
        }
        public Dictionary<int, DateTime> GetMedicationInstructions()
        {
            Dictionary<int, DateTime> instructions = new Dictionary<int, DateTime>();
            int patientId = GetPatientId();
            string query = "select ins.startTime as startTime, ins.timesPerDay as perDay from Receipt as r inner join Instructions as ins on r.id_instructions = ins.id where r.id_patient = " + patientId + "";

            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
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
            int hoursBefore = Convert.ToInt32(DatabaseHelpers.ExecuteReaderQueries(query, Connection)[0]);

            return hoursBefore;
        }
       public void SetMedicationNotificationTime(int newTime)
        {
            int patientId = GetPatientId();
            string query = "Update Patients set notificationTime = " + newTime + " where id = " + patientId + "";
            DatabaseHelpers.ExecuteNonQueries(query, Connection);
        }

        public void AddHospitalSurvey(HospitalSurvey survey)
        {
            int patientId = GetPatientId();
            bool isSatisfied = survey.Happiness == 1 ? true : false;
            bool wouldReccomend = survey.WouldRecommend == 1 ? true : false;
            string query = "insert into HospitalSurveys(quality, higyene, isSatisfied, wouldRecomend, comment, id_patient) values(" + survey.QualityOfService + ", " + survey.Cleanliness + ", " + isSatisfied + ", " + wouldReccomend + ", '" + survey.Comment + "', "+patientId+")";
            DatabaseHelpers.ExecuteNonQueries(query, Connection);
        }
    }
}
