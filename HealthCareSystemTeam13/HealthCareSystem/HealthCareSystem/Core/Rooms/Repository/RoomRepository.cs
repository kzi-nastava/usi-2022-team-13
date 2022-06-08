using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Rooms.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HealthCareSystem.Core.Examinations.Repository;
using static HealthCareSystem.Core.Rooms.HospitalEquipment.Model.Equipment;
using HealthCareSystem.Core.Rooms.HospitalEquipment.RoomHasEquipment.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.TransferHistoryOfEquipment.Model;
using HealthCareSystem.Core.Rooms.Renovations.Model;
using static HealthCareSystem.Core.Rooms.Renovations.Model.Renovation;
using HealthCareSystem.Core.Ingredients.Model;
using HealthCareSystem.Core.Medications.Model;
using HealthCareSystem.Core.Medications.Repository;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace HealthCareSystem.Core.Rooms.Repository
{
    class RoomRepository
    {
        public OleDbConnection Connection { get; set; }
        public DataTable Rooms { get; set; }
        public DataTable Equipment { get; set; }
        private ExaminationRepository _examinationRepository;


        public RoomRepository(int indicator = 1)
        {
            try
            {
                Connection = new OleDbConnection();

                Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../../Data/HCDb.mdb;
                Persist Security Info=False;";

                Connection.Open();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            if(indicator == 0)
                _examinationRepository = new ExaminationRepository();
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

                OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
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

            DatabaseCommander.ExecuteNonQueries(updateQuery, Connection);
        }

        public int GetAvailableRoom(DateTime dateTime, int duration)
        {
            if (duration > 15)
            {
                List<string> roomsId = GetOperationRooms();
                foreach (string roomId in roomsId)
                {
                    if (IsRoomAvailable(roomId, dateTime, duration))
                    {
                        return Convert.ToInt32(roomId);
                    }
                }
            }
            else
            {
                List<string> roomsId = GetExaminationRooms();
                foreach (string roomId in roomsId)
                {
                    if (IsRoomAvailable(roomId, dateTime, duration))
                    {
                        return Convert.ToInt32(roomId);
                    }
                }
            }

            return 0;
        }

        public bool IsRoomAvailable(string roomId, DateTime dateTime, int duration)
        {
            DateTime fromDateTime = DateTime.Now;
            DateTime toDateTime = DateTime.Now.AddHours(2);
            List<Examination> examinations = _examinationRepository.GetRoomEximanitonsFromTo(fromDateTime, toDateTime, roomId);

            foreach (Examination examination in examinations)
            {
                if (examination.DateOf < dateTime.AddMinutes(duration) && examination.DateOf > dateTime)
                    return false;
            }
            return true;
        }

        public int GetWarehouseId()
        {
            var query = "SELECT id FROM Rooms WHERE type = 'Warehouse'";
            return Convert.ToInt32(DatabaseCommander.ExecuteReaderQueries(query, Connection)[0]);
        }

        public void PullRooms()
        {
            Rooms = new DataTable();
            string roomsQuery = "select id, type as 'Room type' from Rooms";

            GUIHelpers.FillTable(Rooms, roomsQuery, Connection);
        }


        public void RemoveRoom(int roomId)
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            string query = "delete from Rooms where id = " + roomId + "";
            DatabaseCommander.ExecuteNonQueries(query, Connection);
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
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);

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


        public bool IsRoomAvailable(int roomId, DateTime examinationTime, List<Examination> examinations)
        {
            // if patient is adding an examination
            if (roomId == 0) return false;

            for (int i = 0; i < examinations.Count(); i++)
            {
                TimeSpan difference = examinationTime.Subtract(examinations[i].DateOf);
                if (Math.Abs(difference.TotalMinutes) < 15 && roomId == examinations[i].IdRoom)
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
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                OleDbCommand cmd = DatabaseCommander.GetCommand("select * from rooms", Connection);
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
        public List<Room> GetRooms(string query)
        {
            List<Room> rooms = new List<Room>();


            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
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
            _examinationRepository = new ExaminationRepository();
            List<Examination> examinations = _examinationRepository.GetExaminationsInRoom(room);
            foreach (Examination examination in examinations)
            {
                if (DateTime.Compare(DateTime.Now, examination.DateOf) < 0)
                {
                    return true;
                }
            }

            return false;
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


        private void SetRoomsToAvailable(Dictionary<int, bool> availableRooms, List<Room> rooms)
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



        public List<string> GetOperationRooms()
        {
            var query = "SELECT ID FROM Rooms WHERE type = operation";
            return DatabaseCommander.ExecuteReaderQueries(query, Connection);
        }

        public List<string> GetExaminationRooms()
        {
            var query = "SELECT ID FROM Rooms WHERE type = examination";
            return DatabaseCommander.ExecuteReaderQueries(query, Connection);
        }

        public void PullFoundRows(string search, string amount, string roomType, string equipmentType)
        {
            Equipment = new DataTable();
            string equipmentQuery = "select rhe.id_room as 'Room id', r.type as 'Room type', rhe.id_equipment as 'Equipment id', e.nameOf as 'Equipment name', e.type as 'Equipment type', rhe.amount as 'Amount' " +
                                    "from Equipment e, Rooms r, RoomHasEquipment rhe " +
                                    "where rhe.id_room = r.ID and rhe.id_equipment = e.ID and (e.nameOf like '%" + search + "%' or e.type like '%" + search + "%')";

            if (amount != "Any")
            {
                if (amount == "10+")
                {
                    equipmentQuery += " and rhe.amount > 10";
                }
                else
                {
                    equipmentQuery += " and rhe.amount > 1 and rhe.amount <= 10";
                }
            }

            if (roomType != "Any")
            {
                equipmentQuery += " and r.type like '" + roomType + "'";
            }

            if (equipmentType != "Any")
            {
                equipmentQuery += " and e.type like '" + equipmentType + "'";
            }

            GUIHelpers.FillTable(Equipment, equipmentQuery, Connection);
        }
        public List<RoomHasEquipment> GetEquipmentInRoom(string query)
        {
            List<RoomHasEquipment> equipmentInRoom = new List<RoomHasEquipment>();


            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    equipmentInRoom.Add(new RoomHasEquipment(Convert.ToInt32(reader["id_equipment"]), Convert.ToInt32(reader["id_room"]), Convert.ToInt32(reader["amount"])));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            Connection.Close();

            return equipmentInRoom;
        }

    }
}
