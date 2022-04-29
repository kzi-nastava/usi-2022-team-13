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



        public bool isRoomAvailable(int roomId, DateTime examinationTime, List<Examination> examinations)
        {
            // if patients is adding an examination
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
                Connection.Open();

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
        

        public int GetAvailableRoomId(DateTime examinationDateTime, List<Examination> examinations)
        {

            Dictionary<int, bool> availableRooms = new Dictionary<int, bool>();
            List<Room> rooms = GetRooms();

            // set all to true at first, then eliminate the rest
            for(int i = 0; i < rooms.Count(); i++)
            {
                if(rooms[i].Type == TypeOfRoom.ExaminationRoom)
                    availableRooms.Add(rooms[i].ID, true);
            }
            
            // elimination
            for(int i = 0; i < examinations.Count(); i++)
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

            // return first free room id
            foreach(KeyValuePair<int, bool> room in availableRooms)
            {
                if (room.Value == true) return room.Key;
            }

            return 0;
        }
    }
}
