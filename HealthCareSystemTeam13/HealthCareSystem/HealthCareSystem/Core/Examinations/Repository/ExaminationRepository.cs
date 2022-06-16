using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Rooms.Repository;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Doctors.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Rooms.Model;
using HealthCareSystem.Core.Users.Patients.Model;
using HealthCareSystem.Core.Users.Patients.Repository;

namespace HealthCareSystem.Core.Examinations.Repository
{
    class ExaminationRepository : IExaminationRepository
    {
        public OleDbConnection Connection { get; set; }
        public IRoomRepository _roomRepository { get; set; }
        public DoctorRepository DoctorRep { get; set; }
        public DataTable Examinations { get; private set; }
        public DataTable RequestsPatients { get; private set; }
        public DataTable ClosestExaminations { get; private set; }
        public DataTable FinishedExaminations { get; private set; }

        public DataTable GetExaminations() 
        { return Examinations; }
        public DataTable GetRequestsPatients()
        { return RequestsPatients; }
        public DataTable GetClosestExaminations()
        { return ClosestExaminations; }
        public DataTable GetFinishedExaminations()
        { return FinishedExaminations; }

        private readonly IPatientRequestRepository _patientRequestRepository;
        public ExaminationRepository()
        {
            try
            {
                Connection = DatabaseConnection.GetConnection();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

            _roomRepository = new RoomRepository();
            DoctorRep = new DoctorRepository();
            _patientRequestRepository = new PatientRequestRepository();
        }


        public void PullExaminationForPatient(int patientId)
        {

            Examinations = new DataTable();

            string examinationsQuery = "select Examination.id, Doctors.FirstName + ' ' +Doctors.LastName as Doctor, dateOf as [Date and Time], id_room as RoomID, duration, typeOfExamination as Type from Examination left outer join Doctors  on Examination.id_doctor = Doctors.id " +
                "where id_patient = " + patientId + "";

            GUIHelpers.FillTable(Examinations, examinationsQuery, Connection);

        }
        public void PullFinishedExaminations(int patientId)
        {
            FinishedExaminations = new DataTable();

            string examinationsQuery = "select Examination.id, Doctors.FirstName + ' ' +Doctors.LastName as Doctor, Examination.dateOf as [Date and Time], Examination.id_room as RoomID, Examination.duration, typeOfExamination as Type from Examination left outer join Doctors on Examination.id_doctor = Doctors.id " +
                "where id_patient = " + patientId + " and Examination.dateOf < #" + DateTime.Now.ToString() + "#";

            GUIHelpers.FillTable(FinishedExaminations, examinationsQuery, Connection);
        }


        public void InsertExamination(int patientId, int doctorId, DateTime examinationDateTime,
            int duration, int roomId, string selectedType = "")
        {

            string insertQuery = "insert into Examination(id_doctor, id_patient, isEdited, isCancelled, isFinished, dateOf, typeOfExamination, isUrgent, id_room, duration) values(@id_doctor, @id_patient, @isEdited, @isCancelled, @isFinished, @dateOf, @typeOfExamination, @isUrgent, @id_room, @duration)";

            using (var cmd = new OleDbCommand(insertQuery, Connection))
            {
                cmd.Parameters.AddWithValue("@id_doctor", doctorId);
                cmd.Parameters.AddWithValue("@id_patient", patientId);
                cmd.Parameters.AddWithValue("@isEdited", false);
                cmd.Parameters.AddWithValue("@isCancelled", false);
                cmd.Parameters.AddWithValue("@isFinished", false);
                cmd.Parameters.AddWithValue("@dateOf", examinationDateTime.ToString());

                if (selectedType.Length == 0 || selectedType == TypeOfExamination.BasicExamination.ToString())
                {
                    cmd.Parameters.AddWithValue("@typeOfExamination", TypeOfExamination.BasicExamination.ToString());
                }
                else
                {
                    cmd.Parameters.AddWithValue("@typeOfExamination", TypeOfExamination.Operation.ToString());
                }
                cmd.Parameters.AddWithValue("@isUrgent", false);
                cmd.Parameters.AddWithValue("@id_room", roomId);
                cmd.Parameters.AddWithValue("@duration", 15);

                cmd.ExecuteNonQuery();
            }

            InsertExaminationChanges(TypeOfChange.Add, patientId);

        }

        public Dictionary<string, string> GetExamination(int examinationId)
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

            string query = "select id_doctor, dateOf, id_room, typeOfExamination from Examination where id = " + examinationId + "";


            Dictionary<string, string> row = new Dictionary<string, string>();
            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);

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
            DatabaseCommander.ExecuteNonQueries(query, Connection);
        }


        public int GetRoomIdFromExaminationId(int examinationId)
        {
           

            string query = "select id_room from Examination where id = " + examinationId + "";

            int roomNumber = -1;

            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                roomNumber = Convert.ToInt32(reader["id_room"]);
            }


            return roomNumber;
        }

        public List<DateTime> GetDateOfExaminationsForDoctor(int doctorId)
        {
            List<DateTime> dates = new List<DateTime>();
            string query = "select dateOf from Examination where id_doctor = " + doctorId;
            try
            {

                OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                    dates.Add((DateTime)reader["DateOf"]);
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

            return dates;
        }

        public void PullExaminationRequests()
        {
            RequestsPatients = new DataTable();
            var query = "select * from PatientEditRequest";
            GUIHelpers.FillTable(RequestsPatients, query, Connection);
        }

        public void PullClosestExaminations(DoctorSpeciality speciality)
        {
            ClosestExaminations = new DataTable();
            DateTime fromDateTime = DateTime.Now;
            DateTime toDateTime = DateTime.Now.AddHours(2);
            var query = "select * from (select * from Examination WHERE dateOf > #" + fromDateTime.ToString() + "# and dateOf < #" + toDateTime.ToString() + "# and id_doctor in (SELECT id FROM doctors WHERE speciality = " + speciality.ToString() + " ) ORDER BY dateOf) where rownum < 5 ";
            GUIHelpers.FillTable(ClosestExaminations, query, Connection);
        }

        public void InsertSingleExamination(Examination examination)
        {
            var query = "INSERT INTO Examination(id_doctor, id_patient, isEdited, isCancelled, isFinished, dateOf, typeOfExamination, isUrgent, id_room, duration) " +
                "VALUES(@id_doctor, @id_patient, @isEdited, @isCancelled, @isFinished, @dateOf, @typeOfExamination, @isUrgent, @id_room, @duration)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_doctor", examination.IdDoctor);
                cmd.Parameters.AddWithValue("@id_patient", examination.IdPatient);
                cmd.Parameters.AddWithValue("@isEdited", examination.IsEdited);
                cmd.Parameters.AddWithValue("@isCancelled", examination.IsCancelled);
                cmd.Parameters.AddWithValue("@isFinished", examination.IsFinished);
                cmd.Parameters.AddWithValue("@dateOf", examination.DateOf.ToString());
                cmd.Parameters.AddWithValue("@typeOfExamination", examination.TypeOfExamination.ToString());
                cmd.Parameters.AddWithValue("@isUrgent", examination.IsUrgent);
                cmd.Parameters.AddWithValue("@id_room", examination.IdRoom);
                cmd.Parameters.AddWithValue("@duration", examination.Duration);
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteSingleExamination(string requestID)
            {
                var query = "SELECT id_examination from PatientEditRequest WHERE id = " + requestID + "";
                string examinationID = DatabaseCommander.ExecuteReaderQueries(query, Connection)[0];

                query = "UPDATE Examination SET iscancelled = 1 WHERE ID = " + examinationID + "";
                using (var cmd = new OleDbCommand(query, Connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            public void UpdateExamination(string requestID)
            {
                Dictionary<string, string> information = _patientRequestRepository.GetPatientRequest(requestID);
                var query = "UPDATE Examination SET id_doctor = @id_doctor, dateOf = @dateOf, id_room = @id_room WHERE ID = @examination_id";
                using (var cmd = new OleDbCommand(query, Connection))
                {
                    cmd.Parameters.AddWithValue("@id_doctor", information["doctor_id"]);
                    cmd.Parameters.AddWithValue("@dateOf", information["dateTimeOfExamination"]);
                    cmd.Parameters.AddWithValue("@id_room", information["room_id"]);
                    cmd.Parameters.AddWithValue("@examination_id", information["examination_id"]);
                    cmd.ExecuteNonQuery();
                }
                query = "UPDATE Examination SET isedited = 1 WHERE ID = " + information["examination_id"] + "";
                using (var cmd = new OleDbCommand(query, Connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            public List<Examination> GetDoctorsEximanitonsInNextTwoHours(string doctorId)
            {
                List<Examination> examinations = new List<Examination>();
                DateTime fromDateTime = DateTime.Now;
                DateTime toDateTime = DateTime.Now.AddHours(2);
                var query = "SELECT id, id_doctor, id_patient, isEdited, isCancelled, isFinished, dateOf, typeOfExamination, isUrgent, id_room, duration FROM Examination " +
                    "WHERE dateOf > #" + fromDateTime.ToString() + "# and dateof < #" + toDateTime.ToString() + "# and id_doctor  = " + doctorId + "";

                Dictionary<string, string> row = new Dictionary<string, string>();

                OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);

                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    examinations.Add(GetExaminationFromReader(reader));
                }

                return examinations;
            }

            public List<Examination> GetRoomEximanitonsFromTo(DateTime fromDateTime, DateTime toDateTime, string roomId)
            {
                List<Examination> examinations = new List<Examination>();
                var query = "SELECT id, id_doctor, id_patient, isEdited, isCancelled, isFinished, dateOf, typeOfExamination, isUrgent, id_room, duration FROM Examination " +
                    "WHERE dateOf BETWEEN (" + fromDateTime + ", " + toDateTime + ") and id_room  = " + roomId + "";
                Dictionary<string, string> row = new Dictionary<string, string>();
                OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);

                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    examinations.Add(GetExaminationFromReader(reader));
                }

                return examinations;
            }

            public Examination GetExaminationFromReader(OleDbDataReader reader)
            {
                return new Examination((int)reader["id"], (int)reader["id_doctor"], (int)reader["id_patient"], (bool)reader["isEdited"], (bool)reader["isCancelled"], (bool)reader["isFinished"], (DateTime)reader["dateOf"],
                                                                 (TypeOfExamination)reader["typeOfExamination"], (bool)reader["isUrgent"], (int)reader["id_room"], (int)reader["duration"]);
            }
            public List<Examination> GetDoctorsExamiantions(DateTime from, DateTime to, string doctorId)
            {
                List<Examination> examinations = new List<Examination>();
                var query = "SELECT id, id_doctor, id_patient, isEdited, isCancelled, isFinished, dateOf, typeOfExamination, isUrgent, id_room, duration FROM Examination " +
                    "WHERE dateOf > #" + from.ToString() + "# and dateOf < #" + to.ToString() + "# and id_doctor  = " + doctorId + "";

                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = Connection;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = query;

                OleDbDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    examinations.Add(GetExaminationFromReader(reader));
                }

                return examinations;
            }

            public Tuple<string, DateTime> AvailableExamination(DoctorSpeciality speciality, int duration)
            {
                List<string> doctorsID = DoctorRep.GetSpecialistsIds(speciality);
                Tuple<string, DateTime> closestTimeAndDoctor = new Tuple<string, DateTime>("none", DateTime.Now.AddHours(2));

                foreach (string doctorID in doctorsID)
                {
                    List<Examination> examinations = GetDoctorsEximanitonsInNextTwoHours(doctorID);
                    examinations = examinations.OrderBy(examination => examination.DateOf).ToList();
                    TimeSpan timeSpan = TimeSpan.FromSeconds(0);

                    if (examinations.Any())
                        timeSpan = DateTime.Now - examinations[0].DateOf;
                    if (timeSpan.TotalMinutes <= duration)
                    {
                        closestTimeAndDoctor = new Tuple<string, DateTime>(doctorID, DateTime.Now);
                    }

                    for (int index = 0; index < examinations.Count() - 1; index++)
                    {
                        timeSpan = examinations[index + 1].DateOf - examinations[index].DateOf;
                        if (timeSpan.TotalMinutes <= duration)
                        {
                            if (examinations[index].DateOf.AddMinutes(examinations[index].Duration) < closestTimeAndDoctor.Item2)
                            {
                                closestTimeAndDoctor = new Tuple<string, DateTime>(doctorID, examinations[index].DateOf.AddMinutes(examinations[index].Duration));
                            }
                        }
                    }

                    if (examinations.Any())
                        timeSpan = DateTime.Now.AddHours(2) - examinations[examinations.Count - 1].DateOf;
                    if (timeSpan.TotalMinutes <= duration)
                    {
                        if (examinations[examinations.Count - 1].DateOf.AddMinutes(examinations[examinations.Count - 1].Duration) < closestTimeAndDoctor.Item2)
                        {
                            closestTimeAndDoctor = new Tuple<string, DateTime>(doctorID, examinations[examinations.Count - 1].DateOf.AddMinutes(examinations[examinations.Count - 1].Duration));
                        }
                    }
                }
                return closestTimeAndDoctor;
            }
            public void MoveExamination(int id, DateTime dateTime)
            {
                var query = "UPDATE Examination SET dateOf = " + dateTime + " WHERE ID = " + id + "";
                using (var cmd = new OleDbCommand(query, Connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            public void InsertExaminationChanges(TypeOfChange typeOfChange, int patientId = 0)
            {

                string insertQuery = "insert into PatientExaminationChanges(id_patient, typeOfChange, dateOf) values(@id_patient, @typeOfChange, @dateOf)";
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                using (var cmd = new OleDbCommand(insertQuery, Connection))
                {
                    cmd.Parameters.AddWithValue("@id_patient", patientId);
                    cmd.Parameters.AddWithValue("@typeOfChange", typeOfChange.ToString());
                    cmd.Parameters.AddWithValue("@dateOf", DateTime.Now.ToString());

                    cmd.ExecuteNonQuery();
                }


            }
        public void MoveDoctorsExaminations(DateTime from, DateTime to, int doctorId)
            {
                List<Examination> movingExaminations = GetDoctorsExamiantions(from, to, doctorId.ToString());
                foreach (Examination movingExamination in movingExaminations)
                {
                    List<Examination> examinations = GetDoctorsExamiantions(to, DateTime.Now.AddDays(30), (movingExamination.IdDoctor).ToString());
                    examinations = examinations.OrderBy(examination => examination.DateOf).ToList();
                    bool found = false;

                    for (int index = 0; index < examinations.Count() - 1; index++)
                    {
                        TimeSpan timeSpan = examinations[index + 1].DateOf - examinations[index].DateOf.AddMinutes(examinations[index].Duration);
                        if (timeSpan.TotalMinutes <= movingExamination.Duration)
                        {
                            MoveExamination(examinations[index].Id, examinations[index].DateOf.AddMinutes(examinations[index].Duration));
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        MoveExamination(examinations[examinations.Count() - 1].Id, examinations[examinations.Count() - 1].DateOf.AddMinutes(examinations[examinations.Count() - 1].Duration));
                    }
                }
            }

            public void PullExaminations(int doctorId)
            {
                Examinations = new DataTable();

                string examinationsQuery = "select Examination.id, Patients.FirstName + ' ' + Patients.LastName as Patient," +
                    " dateOf as [Date and Time], id_room as RoomID, duration as Duration, typeOfExamination as Type from Examination" +
                    " left outer join Patients  on Examination.id_patient = Patients.id " +
                    "where id_doctor = " + doctorId + "";

                GUIHelpers.FillTable(Examinations, examinationsQuery, Connection);
            }

            public List<Examination> GetExaminationsInRoom(Room room)
            {
                List<Examination> examinations = new List<Examination>();

                try
                {

                    OleDbCommand cmd = DatabaseCommander.GetCommand("select * from Examination where id_room = " + room.ID, Connection);
                    OleDbDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Examination examination = SetExaminationValues(reader);

                        examinations.Add(examination);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.ToString());
                }

                return examinations;
            }


            public Examination SetExaminationValues(OleDbDataReader reader)
            {
                Enum.TryParse<TypeOfExamination>(reader["typeOfExamination"].ToString(), out var typeOfExamination);
                int id = Convert.ToInt32(reader["id"]);
                int doctorId = Convert.ToInt32(reader["id_doctor"]);
                int patientId = Convert.ToInt32(reader["id_patient"]);
                bool isEdited = Convert.ToBoolean(reader["isEdited"]);
                bool isCancelled = Convert.ToBoolean(reader["isCancelled"]);
                bool isFinished = Convert.ToBoolean(reader["isFinished"]);
                DateTime dateOf = Convert.ToDateTime(reader["dateOf"]);
                bool isUrgent = Convert.ToBoolean(reader["isUrgent"]);
                int roomId = Convert.ToInt32(reader["id_room"]);
                int duration = Convert.ToInt32(reader["duration"]);


                Examination examination = new Examination(doctorId, patientId, isEdited, isCancelled, isFinished, dateOf, typeOfExamination,
                    isUrgent, roomId, duration);
                return examination;
            }


            public List<Examination> GetRecommendedExaminations(Doctor selectedDoctor, string startTime, string endTime, DateTime examinationFinalDate, bool isDoctorPriority)
            {
                // get taken appointments in criteria
                List<Examination> takenExaminations = GetTakenExaminations(selectedDoctor.ID, startTime, endTime, examinationFinalDate);

                DateTime startDate = TimeDateHelpers.GetMergedDateTime(DateTime.Now.AddDays(1), startTime);
                DateTime endDate = TimeDateHelpers.GetMergedDateTime(examinationFinalDate, endTime);

                // gets free examinations based on the taken ones
                var examinations = GetFreeExaminations(selectedDoctor.ID, startDate, endDate, takenExaminations);

                if (!examinations.Any())
                {
                    if (isDoctorPriority)
                    {
                        examinations = GetFreeExaminationsWithDoctorPriority(selectedDoctor.ID, startDate, endDate, takenExaminations);
                    }
                    else
                    {
                        examinations = GetFreeExaminationsWithTimespanPriority(startDate, endDate, takenExaminations);
                    }

                    if (!examinations.Any())
                    {
                        examinations = GetTopThreeExaminations(startDate, endDate, takenExaminations);

                    }
                }
                return examinations;
            }

        public List<Examination> GetFreeExaminationsWithDoctorPriority(int doctorId, DateTime startDate, DateTime endDate, List<Examination> takenExaminations)
            {
                List<Examination> examinations = new List<Examination>();

                TimeDateHelpers.MoveExaminationDateTimeBoundaries(ref startDate, ref endDate);

                int totalFoundExaminations = 0;
                int startHour = startDate.Hour;
                int startMinute = startDate.Minute;
                int endHour = endDate.Hour;
                int endMinute = endDate.Minute;
                while (startDate.CompareTo(endDate) <= 0)
                {

                    int roomId = _roomRepository.GetAvailableRoomId(startDate, takenExaminations);
                    if (roomId != 0)
                    {
                        if (DoctorRep.IsDoctorAvailableAtTime(doctorId, startDate, takenExaminations))
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
        public List<Examination> GetFreeExaminationsWithTimespanPriority(DateTime startDate, DateTime endDate, List<Examination> takenExaminations)
            {
                List<Examination> examinations = new List<Examination>();
                int startHour = startDate.Hour;
                int startMinute = startDate.Minute;
                int endHour = endDate.Hour;
                int endMinute = endDate.Minute;
                int totalFoundExaminations = 0;
                while (startDate.CompareTo(endDate) <= 0)
                {
                    var roomId = _roomRepository.GetAvailableRoomId(startDate, takenExaminations);
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

            public List<Examination> GetFreeExaminations(int doctorId, DateTime startDate, DateTime endDate, List<Examination> takenExaminations)
            {
                List<Examination> examinations = new List<Examination>();

                int startHour = startDate.Hour;
                int startMinute = startDate.Minute;
                int endHour = endDate.Hour;
                int endMinute = endDate.Minute;
                int totalFoundExaminations = 0;

                while (startDate.CompareTo(endDate) <= 0)
                {
                    var roomId = _roomRepository.GetAvailableRoomId(startDate, takenExaminations);
                    if (roomId != 0)
                    {
                        if (DoctorRep.IsDoctorAvailableAtTime(doctorId, startDate, takenExaminations))
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
        public List<Examination> GetTopThreeExaminations(DateTime startDate, DateTime endDate, List<Examination> takenExaminations)
            {
                List<Examination> examinations = new List<Examination>();

                TimeDateHelpers.MoveExaminationDateTimeBoundaries(ref startDate, ref endDate);
                int startHour = startDate.Hour;
                int startMinute = startDate.Minute;
                int endHour = endDate.Hour;
                int endMinute = endDate.Minute;
                int totalFoundExaminations = 0;

                while (startDate.CompareTo(endDate) <= 0)
                {
                    var roomId = _roomRepository.GetAvailableRoomId(startDate, takenExaminations);
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


        public List<Examination> GetTakenExaminations(int doctorId, string startTime, string endTime, DateTime examinationFinalDate)
            {
                List<Examination> examinations = new List<Examination>();

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

                return examinations;
            }

        public Examination GetExaminationValues(OleDbDataReader reader)
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

            public List<Examination> GetFinishedExaminations(int patientId)
            {

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

                OleDbCommand cmd = DatabaseCommander.GetCommand("select * from Examination where not id = " + currentExaminationId + "", Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SetExaminationValues(examinations, reader);
                }

                return examinations;
            }

            public List<Examination> GetAllExaminations()
            {
                List<Examination> examinations = new List<Examination>();

                OleDbCommand cmd = DatabaseCommander.GetCommand("select * from Examination", Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SetExaminationValues(examinations, reader);
                }

                return examinations;
            }

            public void SetExaminationValues(List<Examination> examinations, OleDbDataReader reader)
            {
                Enum.TryParse<TypeOfExamination>(reader["typeOfExamination"].ToString(), out var typeOfExamination);

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
            public void PullExaminationsByDate(DateTime date)
            {
                Examinations = new DataTable();


                string examinationsQuery = "select Examination.id, Patients.FirstName + ' ' + Patients.LastName as Patient," +
                                           " dateOf, id_room as RoomID, duration as Duration, typeOfExamination as Type from Examination" +
                                           " left outer join Patients  on Examination.id_patient = Patients.id " +
                                           "where id_doctor = " + DoctorRep.GetDoctorId() + " and Day(dateOf) = Day('" + date + "')";
                GUIHelpers.FillTable(Examinations, examinationsQuery, Connection);
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
                                           "where id_doctor = " + DoctorRep.GetDoctorId() + " and (" +
                                           "Day(dateOf) = Day('" + firstDay + "') or Day(dateOf) = Day('" + secondDay + "') or Day(dateOf) = Day('" + thirdDay + "'))";
                GUIHelpers.FillTable(Examinations, examinationsQuery, Connection);
            }

    }
}
