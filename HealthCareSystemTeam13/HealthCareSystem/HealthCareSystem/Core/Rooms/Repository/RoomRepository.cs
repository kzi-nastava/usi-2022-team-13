using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Rooms.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace HealthCareSystem.Core.Rooms.Repository
{
    class RoomRepository
    {
        public OleDbConnection Connection { get; set; }
        public DataTable Rooms { get; set; }

        public RoomRepository()
        {
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

        }

        public void PullRooms()
        {
            Rooms = new DataTable();
            string roomsQuery = "select id, type as 'Room type' from Rooms";
            FillTable(Rooms, roomsQuery);
        }
        private void FillTable(DataTable table, string query)
        {
            using (var cmd = new OleDbCommand(query, Connection))
            {
                OleDbDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
            }
        }

        public void RemoveRoom(int roomId)
        {
            string query = "delete from Rooms where id = " + roomId + "";
            DatabaseHelpers.ExecuteNonQueries(query, Connection);
        }

        public void InsertRoom(TypeOfRoom roomType)
        {
            var insertQuery = "INSERT INTO rooms(type) VALUES(@type)";
            using (var cmd = new OleDbCommand(insertQuery, Connection))
            {
                cmd.Parameters.AddWithValue("@type", roomType.ToString());
                cmd.ExecuteNonQuery();

            }
        }

        public Room GetSelectedRoom(string query)
        {
            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);

            Room room = new Room();

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                TypeOfRoom roomType;
                Enum.TryParse<TypeOfRoom>(reader["type"].ToString(), out roomType);
                room = new Room(roomType, Convert.ToInt32(reader["id"]));

            }
            return room;
        }

        public void UpdateContent(string query)
        {
            DatabaseHelpers.ExecuteNonQueries(query, Connection);
        }

        public bool IsRoomAvailable(int roomId, DateTime examinationTime, List<Examination> examinations)
        {
            // if patient is adding an examination
            if (roomId == 0) return false;

            for(int i = 0; i < examinations.Count(); i++)
            {
                TimeSpan difference = examinationTime.Subtract(examinations[i].DateOf);
                if(Math.Abs(difference.TotalMinutes) < 15 && roomId == examinations[i].IdRoom)
                {
                    return false;
                }
            }

            return true;
        }
        public List<Room> GetRooms()
        {
            List<Room> rooms = new List<Room>();

            try
            {
                if(Connection.State == ConnectionState.Closed) Connection.Open();

                OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from rooms", Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TypeOfRoom typeOfRoom;
                    Enum.TryParse<TypeOfRoom>(reader["type"].ToString(), out typeOfRoom);

                    rooms.Add(new Room(typeOfRoom, Convert.ToInt32(reader["id"])));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            Connection.Close();

            return rooms;
        }

        public Room GetRoom(int id)
        {
            string query = "select * from Rooms where id=" + id;
            Room room = GetSelectedRoom(query);
            return room;
        }
        
        public bool DoesRoomHaveFutureExaminations(Room room)
        {
            
            List<Examination> examinations = GetExaminationsInRoom(room);
            Console.WriteLine(examinations.Count);
            foreach (Examination examination in examinations)
            {
                if(DateTime.Compare(DateTime.Now, examination.DateOf) < 0)
                {
                    return true;
                }
            }
            
            return false;
        }

        public List<Examination> GetExaminationsInRoom(Room room)
        {
            List<Examination> examinations = new List<Examination>();

            try
            {
                if(Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }
                
                OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from Examination where id_room = " + room.ID, Connection);
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
            
            if(Connection.State == ConnectionState.Closed)
                {
                    Connection.Open();
                }

            return examinations;

        }

        private static Examination SetExaminationValues(OleDbDataReader reader)
        {
            TypeOfExamination typeOfExamination;
            Enum.TryParse<TypeOfExamination>(reader["typeOfExamination"].ToString(), out typeOfExamination);
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

        public int GetAvailableRoomId(DateTime examinationDateTime, List<Examination> examinations)
        {

            Dictionary<int, bool> availableRooms = new Dictionary<int, bool>();
            List<Room> rooms = GetRooms();

            // set all to true at first, then eliminate the rest
            SetRoomsToAvailable(availableRooms, rooms);

            // elimination
            EliminateUnavailableRooms(examinationDateTime, examinations, availableRooms, rooms);

            // return first free room id
            foreach (KeyValuePair<int, bool> room in availableRooms)
            {
                if (room.Value == true) return room.Key;
            }

            return 0;
        }

        private static void SetRoomsToAvailable(Dictionary<int, bool> availableRooms, List<Room> rooms)
        {
            for (int i = 0; i < rooms.Count(); i++)
            {
                if (rooms[i].Type == TypeOfRoom.ExaminationRoom)
                    availableRooms.Add(rooms[i].ID, true);
            }
        }

        private static void EliminateUnavailableRooms(DateTime examinationDateTime, List<Examination> examinations, Dictionary<int, bool> availableRooms, List<Room> rooms)
        {
            for (int i = 0; i < examinations.Count(); i++)
            {
                TimeSpan difference = examinationDateTime.Subtract(examinations[i].DateOf);
                for (int j = 0; j < rooms.Count(); j++)
                {
                    if (Math.Abs(difference.TotalMinutes) < 15 && rooms[i].ID == examinations[i].IdRoom)
                    {
                        availableRooms[rooms[i].ID] = false;
                    }
                }
            }
        }
    }
}
