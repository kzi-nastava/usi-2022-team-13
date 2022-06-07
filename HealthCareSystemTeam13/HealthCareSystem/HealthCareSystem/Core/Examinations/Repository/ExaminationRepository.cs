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

                Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../../Data/HCDb.mdb;
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

            OleDbCommand cmd = DatabaseCommander.GetCommand("select * from Examination where id_patient = " + patientId + " and dateOf < #" + DateTime.Now.ToString() + "#", Connection);
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

            OleDbCommand cmd = DatabaseCommander.GetCommand("select * from Examination where not id = "+currentExaminationId+"", Connection);
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

            OleDbCommand cmd = DatabaseCommander.GetCommand("select * from Examination", Connection);
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


        public Anamnesis GetAnamnesis(int examinationId)
        {
            if (Connection.State == System.Data.ConnectionState.Closed) Connection.Open();

            OleDbCommand cmd = DatabaseCommander.GetCommand("select * from Anamnesises where id_examination = " + examinationId + "", Connection);
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

            DateTime startDate = TimeDateHelpers.GetMergedDateTime(DateTime.Now.AddDays(1), startTime);
            DateTime endDate = TimeDateHelpers.GetMergedDateTime(examinationFinalDate, endTime);

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

            TimeDateHelpers.MoveExaminationDateTimeBoundaries(ref startDate, ref endDate);

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
                    if (DoctorRep.IsDoctorAvailableAtTime(doctorId, startDate, takenExaminations, this))
                    {
                        examinations.Add(new Examination(doctorId, startDate, TypeOfExamination.BasicExamination, roomId));
                        totalFoundExaminations++;
                        if (totalFoundExaminations > 4) break;
                    }
                }
                startDate = TimeDateHelpers.GetNewStartDate(startDate, startHour, startMinute, endHour, endMinute);
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
                startDate = TimeDateHelpers.GetNewStartDate(startDate, startHour, startMinute, endHour, endMinute);
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
                    if (DoctorRep.IsDoctorAvailableAtTime(doctorId, startDate, takenExaminations, this))
                    {
                        examinations.Add(new Examination(doctorId, startDate, TypeOfExamination.BasicExamination, roomId));
                        totalFoundExaminations++;
                        if (totalFoundExaminations > 4) break;
                    }
                }
                startDate = TimeDateHelpers.GetNewStartDate(startDate, startHour, startMinute, endHour, endMinute);
            }

            return examinations;

        }
        private List<Examination> GetTopThreeExaminations(DateTime startDate, DateTime endDate, List<Examination> takenExaminations)
        {
            List<Examination> examinations = new List<Examination>();

            TimeDateHelpers.MoveExaminationDateTimeBoundaries(ref startDate, ref endDate);
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
                startDate = TimeDateHelpers.GetNewStartDate(startDate, startHour, startMinute, endHour, endMinute);
            }
            return examinations.GetRange(0, 3);
        }


        private List<Examination> GetTakenExaminations(int doctorId, string startTime, string endTime, DateTime examinationFinalDate)
        {
            List<Examination> examinations = new List<Examination>();
            if(Connection.State == System.Data.ConnectionState.Closed) Connection.Open();

            DateTime start = TimeDateHelpers.GetMergedDateTime(DateTime.Now, startTime);
            DateTime end = TimeDateHelpers.GetMergedDateTime(examinationFinalDate, endTime);

            string query = "select * from examination where id_doctor = " + doctorId + " and dateOf between #" + start.ToString() + "# and #" + end.ToString() + "#";

            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
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
            Enum.TryParse<TypeOfExamination>(reader["typeOfExamination"].ToString(), out var typeOfExamination);

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
