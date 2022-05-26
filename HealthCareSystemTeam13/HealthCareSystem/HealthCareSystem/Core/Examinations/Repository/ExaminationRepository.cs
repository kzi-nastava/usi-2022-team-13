using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Rooms.Repository;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Doctors.Repository;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Examinations.Repository
{
    class ExaminationRepository
    {
        public OleDbConnection Connection { get; set; }
        public RoomRepository RoomRep { get; set; }
        public DoctorRepository DoctorRep { get; set; }
        public ExaminationRepository()
        {
            try
            {
                Connection = new OleDbConnection();

                Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=HCDb.mdb;
                Persist Security Info=False;";

                

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            RoomRep = new RoomRepository();
            DoctorRep = new DoctorRepository();
        }
        public List<Examination> GetFinishedExaminations(int patientId)
        {
            if (Connection.State == System.Data.ConnectionState.Closed) Connection.Open();

            List<Examination> examinations = new List<Examination>();

            OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from Examination where id_patient = " + patientId + " and dateOf < #" + DateTime.Now.ToString() + "#", Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                SetExaminationValues(examinations, reader);
            }

            return examinations;
        }
        public List<Examination> GetAllOtherExaminations(int currentExaminationId)
        {
            List<Examination> examinations = new List<Examination>();
            Connection.Open();

            OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from Examination where not id = "+currentExaminationId+"", Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                SetExaminationValues(examinations, reader);
            }
            Connection.Close();

            return examinations;
        }

        public List<Examination> GetAllExaminations()
        {
            List<Examination> examinations = new List<Examination>();
            Connection.Open();

            OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from Examination", Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                SetExaminationValues(examinations, reader);
            }
            Connection.Close();

            return examinations;
        }

        private static void SetExaminationValues(List<Examination> examinations, OleDbDataReader reader)
        {
            TypeOfExamination typeOfExamination;
            Enum.TryParse<TypeOfExamination>(reader["typeOfExamination"].ToString(), out typeOfExamination);

            examinations.Add(new Examination(
                Convert.ToInt32(reader["ID"]),
                Convert.ToInt32(reader["id_doctor"]),
                Convert.ToInt32(reader["id_patient"]),
                false,
                false,
                false,
                (DateTime)reader["dateOf"],
                typeOfExamination,
                false,
                Convert.ToInt32(reader["id_room"]),
                Convert.ToInt32(reader["duration"])
                ));
        }

        public DoctorAnamnesis GetDoctorAnamnesis(int examinationId)
        {
            if (Connection.State == System.Data.ConnectionState.Closed) Connection.Open();

            string query = "select a.id_examination as ExaminationId, a.notice as Notice, a.conclusions as Conclusions, e.dateOf as DateOfExamination, d.firstName + ' ' + d.lastName as Doctor, d.speciality as Speciality from (Anamnesises a inner join Examination e on a.id_examination = e.id) inner join doctors d on e.id_doctor = d.id where e.id = " + examinationId + "";

            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
            DoctorAnamnesis anamnesis = null;

            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                anamnesis = SetDoctorAnamnesisValues(reader);
            }

            return anamnesis;
        }

        private static DoctorAnamnesis SetDoctorAnamnesisValues(OleDbDataReader reader)
        {
            return new DoctorAnamnesis(Convert.ToInt32(
                                                        reader["ExaminationId"]),
                                                        reader["Notice"].ToString(),
                                                        reader["Conclusions"].ToString(),
                                                        (DateTime)reader["DateOfExamination"],
                                                        reader["Doctor"].ToString(),
                                                        reader["Speciality"].ToString()
                                                        );
        }

        public Anamnesis GetAnamnesis(int examinationId)
        {
            if (Connection.State == System.Data.ConnectionState.Closed) Connection.Open();

            OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from Anamnesises where id_examination = " + examinationId + "", Connection);
            Anamnesis anamnesis = null;

            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                anamnesis = SetAnamnesisValues(reader);
            }

            return anamnesis;
        }

        private static Anamnesis SetAnamnesisValues(OleDbDataReader reader)
        {
            return new Anamnesis(Convert.ToInt32(reader["id_examination"]),
                                                        reader["notice"].ToString(),
                                                        reader["conclusions"].ToString(),
                                                        (DateTime)reader["dateOf"]);
        }

        
        public List<Examination> GetRecommendedExaminations(Doctor selectedDoctor, string startTime, string endTime, DateTime examinationFinalDate, bool isDoctorPriority)
        {
            List<Examination> examinations;

            // get taken appointments in criteria
            List<Examination> takenExaminations = GetTakenExaminations(selectedDoctor.ID, startTime, endTime, examinationFinalDate);

            DateTime startDate = Helpers.GetMergedDateTime(DateTime.Now.AddDays(1), startTime);
            DateTime endDate = Helpers.GetMergedDateTime(examinationFinalDate, endTime);

            // gets free examinations based on the taken ones
            examinations = GetFreeExaminations(selectedDoctor.ID, startDate, endDate, takenExaminations);

            if (examinations.Count() == 0)
            {
                if (isDoctorPriority)
                {
                    examinations = GetFreeExaminationsWithDoctorPriority(selectedDoctor.ID, startDate, endDate, takenExaminations);
                }
                else
                {
                    examinations = GetFreeExaminationsWithTimespanPriority(startDate, endDate, takenExaminations);
                }
                
                if(examinations.Count() == 0)
                {
                    examinations = GetTopThreeExaminations(startDate, endDate, takenExaminations);

                }
            }
            return examinations;
        }

        private List<Examination> GetFreeExaminationsWithDoctorPriority(int doctorId, DateTime startDate, DateTime endDate, List<Examination> takenExaminations)
        {
            List<Examination> examinations = new List<Examination>();

            MoveExaminationDateTimeBoundaries(ref startDate, ref endDate);

            int totalFoundExaminations = 0;
            int roomId;
            int startHour = startDate.Hour;
            int startMinute = startDate.Minute;
            int endHour = endDate.Hour;
            int endMinute = endDate.Minute;
            while (startDate.CompareTo(endDate) <= 0)
            {
               
                roomId = RoomRep.GetAvailableRoomId(startDate, takenExaminations);
                if (roomId != 0)
                {
                    if (IsDoctorAvailableAtTime(doctorId, startDate, takenExaminations))
                    {
                        examinations.Add(new Examination(doctorId, startDate, TypeOfExamination.BasicExamination, roomId));
                        totalFoundExaminations++;
                        if (totalFoundExaminations > 4) break;
                    }
                }
                startDate = GetNewStartDate(startDate, startHour, startMinute, endHour, endMinute);
            }


            return examinations;
        }
        private List<Examination> GetFreeExaminationsWithTimespanPriority(DateTime startDate, DateTime endDate, List<Examination> takenExaminations)
        {
            List<Examination> examinations = new List<Examination>();
            int startHour = startDate.Hour;
            int startMinute = startDate.Minute;
            int endHour = endDate.Hour;
            int endMinute = endDate.Minute;
            int totalFoundExaminations = 0;
            int roomId;
            while (startDate.CompareTo(endDate) <= 0)
            {
                roomId = RoomRep.GetAvailableRoomId(startDate, takenExaminations);
                if (roomId != 0)
                {
                    Doctor availableDoctor = DoctorRep.GetAvailableDoctor(startDate, takenExaminations);

                    if (availableDoctor != null)
                    {
                        examinations.Add(new Examination(availableDoctor.ID, startDate, TypeOfExamination.BasicExamination, roomId));
                        totalFoundExaminations++;
                        if (totalFoundExaminations > 4) break;
                    }
                }
                startDate = GetNewStartDate(startDate, startHour, startMinute, endHour, endMinute);
            }

            return examinations;
        }

        private List<Examination> GetFreeExaminations(int doctorId, DateTime startDate, DateTime endDate, List<Examination> takenExaminations)
        {
            List<Examination> examinations = new List<Examination>();

            int startHour = startDate.Hour;
            int startMinute = startDate.Minute;
            int endHour = endDate.Hour;
            int endMinute = endDate.Minute;
            int totalFoundExaminations = 0;
            int roomId;

            while (startDate.CompareTo(endDate) <= 0)
            {
                roomId = RoomRep.GetAvailableRoomId(startDate, takenExaminations);
                if (roomId != 0)
                {
                    if (IsDoctorAvailableAtTime(doctorId, startDate, takenExaminations))
                    {
                        examinations.Add(new Examination(doctorId, startDate, TypeOfExamination.BasicExamination, roomId));
                        totalFoundExaminations++;
                        if (totalFoundExaminations > 4) break;
                    }
                }
                startDate = GetNewStartDate(startDate, startHour, startMinute, endHour, endMinute);
            }

            return examinations;

        }
        private List<Examination> GetTopThreeExaminations(DateTime startDate, DateTime endDate, List<Examination> takenExaminations)
        {
            List<Examination> examinations = new List<Examination>();

            MoveExaminationDateTimeBoundaries(ref startDate, ref endDate);
            int startHour = startDate.Hour;
            int startMinute = startDate.Minute;
            int endHour = endDate.Hour;
            int endMinute = endDate.Minute;
            int totalFoundExaminations = 0;
            int roomId;
            
            while (startDate.CompareTo(endDate) <= 0)
            {
                roomId = RoomRep.GetAvailableRoomId(startDate, takenExaminations);
                if (roomId != 0)
                {
                    Doctor availableDoctor = DoctorRep.GetAvailableDoctor(startDate, takenExaminations);
                    if (availableDoctor != null)
                    {
                        examinations.Add(new Examination(availableDoctor.ID, startDate, TypeOfExamination.BasicExamination, roomId));
                        totalFoundExaminations++;
                        if (totalFoundExaminations > 4) break;
                    }
                }
                startDate = GetNewStartDate(startDate, startHour, startMinute, endHour, endMinute);
            }
            return examinations.GetRange(0, 3);
        }

        private bool IsDoctorAvailableAtTime(int doctorId, DateTime startDate, List<Examination> takenExaminations)
        {
            bool isAvailable = true;
            foreach (Examination takenExam in takenExaminations)
            {
                if (!IsValidTimeAndDoctor(startDate, takenExam, doctorId)) 
                    isAvailable = false;  
            }
            return isAvailable;
        }

      
        private static void MoveExaminationDateTimeBoundaries(ref DateTime startDate, ref DateTime endDate)
        {
            startDate = startDate.AddHours(-4);
            endDate = endDate.AddDays(2).AddHours(4);
        }
       
        private static DateTime GetNewStartDate(DateTime startDate, int startHour, int startMinute, int endHour, int endMinute)
        {
            startDate = startDate.AddMinutes(15);
            if (startDate.Hour > endHour)
            {
                startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startHour, startMinute, 0).AddDays(1);
            }
            return startDate;
        }

        private bool IsDoctorEqual(int doctorId, Examination takenExamination)
        {
            return doctorId == takenExamination.IdDoctor;
        }
        private bool IsValidTime(DateTime startDate, Examination takenExamination)
        {
            TimeSpan difference = startDate.Subtract(takenExamination.DateOf);
            if (Math.Abs(difference.TotalMinutes) < 15) return false;
            return true;
        }
        private bool IsValidTimeAndDoctor(DateTime startDate, Examination takenExamination, int doctorId)
        {

            return !(!IsValidTime(startDate, takenExamination) && IsDoctorEqual(doctorId, takenExamination));
        }

        private List<Examination> GetTakenExaminations(int doctorId, string startTime, string endTime, DateTime examinationFinalDate)
        {
            List<Examination> examinations = new List<Examination>();
            if(Connection.State == System.Data.ConnectionState.Closed) Connection.Open();

            DateTime start = Helpers.GetMergedDateTime(DateTime.Now, startTime);
            DateTime end = Helpers.GetMergedDateTime(examinationFinalDate, endTime);

            string query = "select * from examination where id_doctor = " + doctorId + " and dateOf between #" + start.ToString() + "# and #" + end.ToString() + "#";

            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Examination examination = GetExaminationValues(reader);
                examinations.Add(examination);
            }
            if(Connection.State == System.Data.ConnectionState.Open) Connection.Close();

            return examinations;
        }

        private static Examination GetExaminationValues(OleDbDataReader reader)
        {
            TypeOfExamination typeOfExamination;
            Enum.TryParse<TypeOfExamination>(reader["typeOfExamination"].ToString(), out typeOfExamination);

            Examination examination = new Examination(
                Convert.ToInt32(reader["id_doctor"]),
                Convert.ToInt32(reader["id_patient"]),
                false,
                false,
                false,
                (DateTime)reader["dateOf"],
                typeOfExamination,
                false,
                Convert.ToInt32(reader["id_room"]),
                Convert.ToInt32(reader["duration"])
                );
            return examination;
        }
    }
}
