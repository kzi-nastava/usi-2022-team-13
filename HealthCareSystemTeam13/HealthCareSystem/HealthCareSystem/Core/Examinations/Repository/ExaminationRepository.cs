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
        OleDbConnection Connection;
        RoomRepository RoomRep;
        DoctorRepository DoctorRep;
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

                anamnesis = new DoctorAnamnesis(Convert.ToInt32(
                                            reader["ExaminationId"]),
                                            reader["Notice"].ToString(),
                                            reader["Conclusions"].ToString(),
                                            (DateTime)reader["DateOfExamination"],
                                            reader["Doctor"].ToString(),
                                            reader["Speciality"].ToString()
                                            ) ;
            }

            return anamnesis;
        }
        public Anamnesis GetAnamnesis(int examinationId)
        {
            if (Connection.State == System.Data.ConnectionState.Closed) Connection.Open();

            OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from Anamnesises where id_examination = " + examinationId + "", Connection);
            Anamnesis anamnesis = null;

            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                anamnesis = new Anamnesis(Convert.ToInt32(reader["id_examination"]),
                                            reader["notice"].ToString(),
                                            reader["conclusions"].ToString(),
                                            (DateTime)reader["dateOf"]);
            }

            return anamnesis;
        }



        public List<Examination> GetRecommendedExaminations(Doctor selectedDoctor, string startTime, string endTime, DateTime examinationFinalDate, bool isDoctorPriority)
        {
            List<Examination> examinations;

            // get taken appointments in criteria
            List<Examination> takenExaminations = GetTakenExaminations(selectedDoctor.ID, startTime, endTime, examinationFinalDate);

            // gets free examinations based on the taken ones
            examinations = GetFreeExaminations(selectedDoctor.ID, examinationFinalDate, startTime, endTime, isDoctorPriority, false, takenExaminations);

            if (examinations.Count() == 0)
            {
                examinations = GetFreeExaminations(selectedDoctor.ID, examinationFinalDate, startTime, endTime, isDoctorPriority, true, takenExaminations);

                if(examinations.Count() == 0)
                {
                    // find top 3 that fit

                }
            }


            return examinations;
        }

        private List<Examination> GetFreeExaminations(int doctorId, DateTime examinationFinalDate, string startTime, string endTime, bool isDoctorPriority, bool isPriorityUsed, List<Examination> takenExaminations, bool isTopThree = false)
        {
            List<Examination> examinations = new List<Examination>();

            // setting dates and times
            DateTime startDate = Helpers.GetMergedDateTime(DateTime.Now.AddDays(1), startTime);
            DateTime endDate = Helpers.GetMergedDateTime(examinationFinalDate, endTime);
            int startHour = startDate.Hour;
            int startMinute = startDate.Minute;
            int endHour = endDate.Hour;
            int endMinute = endDate.Minute;
            int totalFoundExaminations = 0;
            int roomId;

            if (isPriorityUsed && isDoctorPriority)
            {
                startDate = startDate.AddHours(-4);
                endDate = endDate.AddDays(2).AddHours(4);
            } else if (isTopThree)
            {
                endDate = endDate.AddDays(2);
                startHour = startDate.AddHours(-2).Hour;
                endHour = endDate.AddHours(2).Hour;

            }

            while (startDate.CompareTo(endDate) <= 0)
            {
                bool isExaminationFound = true;
                roomId = RoomRep.GetAvailableRoomId(startDate, takenExaminations);
                if (roomId != 0)
                {
                    if (!isPriorityUsed || (isPriorityUsed && isDoctorPriority))
                    {
                        foreach (Examination takenExam in takenExaminations)
                        {

                            if (!IsValidTimeDoctor(startDate, takenExam, doctorId))
                            {
                                isExaminationFound = false;
                                continue;
                            }
                            else totalFoundExaminations++;

                        }
                    }
                    else if ((isPriorityUsed && !isDoctorPriority) || isTopThree)
                    {
                        Doctor availableDoctor = DoctorRep.GetAvailableDoctor(startDate, examinations);
                        if (availableDoctor != null)
                        {
                            doctorId = availableDoctor.ID;
                            roomId = RoomRep.GetAvailableRoomId(startDate, takenExaminations);
                            if (roomId > 0) { totalFoundExaminations++; }

                        }
                        else { isExaminationFound = false; }

                    }

                    if (isExaminationFound)
                    {
                        examinations.Add(new Examination(doctorId, startDate, TypeOfExamination.BasicExamination, roomId));
                        totalFoundExaminations++;
                        if (totalFoundExaminations > 4) break;
                    }
                }
                startDate = GetNewStartDate(startDate, startHour, startMinute, endHour, endMinute);
            }
            if (isTopThree) return examinations.GetRange(0, 3);
  
            return examinations;

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
        private bool IsValidTimeDoctor(DateTime startDate, Examination takenExamination, int doctorId)
        {

            return !(!IsValidTime(startDate, takenExamination) && IsDoctorEqual(doctorId, takenExamination));
        }

        private List<Examination> GetTakenExaminations(int doctorId, string startTime, string endTime, DateTime examinationFinalDate)
        {
            List<Examination> examinations = new List<Examination>();
            Connection.Open();

            DateTime start = Helpers.GetMergedDateTime(DateTime.Now, startTime);
            DateTime end = Helpers.GetMergedDateTime(examinationFinalDate, endTime);

            string query = "select * from examination where id_doctor = " + doctorId + " and dateOf between #" + start.ToString() + "# and #" + end.ToString() + "#";
            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TypeOfExamination typeOfExamination;
                Enum.TryParse<TypeOfExamination>(reader["typeOfExamination"].ToString(), out typeOfExamination);

                examinations.Add(new Examination(
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
            Connection.Close();

            return examinations;
        }
    }
}
