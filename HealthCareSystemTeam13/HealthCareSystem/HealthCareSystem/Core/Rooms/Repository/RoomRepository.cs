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
using static HealthCareSystem.Core.Rooms.HospitalEquipment.Model.Equipment;
using HealthCareSystem.Core.Rooms.HospitalEquipment.RoomHasEquipment.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.TransferHistoryOfEquipment.Model;
using HealthCareSystem.Core.Rooms.Renovations.Model;
using static HealthCareSystem.Core.Rooms.Renovations.Model.Renovation;
using HealthCareSystem.Core.Ingredients.Model;

namespace HealthCareSystem.Core.Rooms.Repository
{
    class RoomRepository
    {
        public OleDbConnection Connection { get; set; }
        public DataTable Rooms { get; set; }
        public DataTable Equipment { get; set; }
        public DataTable Renovations { get; set; }
        public DataTable Ingredients { get; set; }

 
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


        public void PullIngredients()
        {
            Ingredients = new DataTable();
            string ingredientsQuery = "select * from ingredients";
            FillTable(Ingredients, ingredientsQuery);
        }

        public void PullRenovations()
        {
            Renovations = new DataTable();
            string renovationsQuery = "select * from renovations";
            FillTable(Renovations, renovationsQuery);
        }

        public void PullEquipment()
        {
            Equipment = new DataTable();
            string equipmentQuery = "select rhe.id_room as 'Room id', r.type as 'Room type', rhe.id_equipment as 'Equipment id', e.nameOf as 'Equipment name', e.type as 'Equipment type', rhe.amount as 'Amount' " +
                                "from Equipment e, Rooms r, RoomHasEquipment rhe " +
                                "where rhe.id_room = r.ID and rhe.id_equipment = e.ID";
            FillTable(Equipment, equipmentQuery);

        }

        public void PullFoundRows(string search, string amount, string roomType, string equipmentType)
        {
            Equipment = new DataTable();
            string equipmentQuery = "select rhe.id_room as 'Room id', r.type as 'Room type', rhe.id_equipment as 'Equipment id', e.nameOf as 'Equipment name', e.type as 'Equipment type', rhe.amount as 'Amount' " +
                                "from Equipment e, Rooms r, RoomHasEquipment rhe " +
                                "where rhe.id_room = r.ID and rhe.id_equipment = e.ID and (e.nameOf like '%" + search + "%' or e.type like '%" + search + "%')";

            if (amount != "Any")
            {
                if(amount == "10+")
                {
                    equipmentQuery += " and rhe.amount > 10";
                }
                else
                {
                    equipmentQuery += " and rhe.amount > 1 and rhe.amount <= 10";
                }
            }

            if(roomType != "Any")
            {
                equipmentQuery += " and r.type like '" + roomType + "'";
            }

            if (equipmentType != "Any")
            {
                equipmentQuery += " and e.type like '" + equipmentType + "'";
            }

            FillTable(Equipment, equipmentQuery);
        }

        private string AddFilters(string equipmentQuery)
        {


            return equipmentQuery;
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
                if (Connection.State == ConnectionState.Closed) Connection.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
            }
            Connection.Close();
        }

        public void RemoveRoom(int roomId)
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            string query = "delete from Rooms where id = " + roomId + "";
            DatabaseHelpers.ExecuteNonQueries(query, Connection);
        }

        public void RemoveIngredient(int ingredientId)
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            string query = "delete from ingredients where id = " + ingredientId + "";
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

        public void InsertIngredient(string ingredientName)
        {
            var insertQuery = "INSERT INTO ingredients(nameOfIngredient) VALUES(@nameOfIngredient)";
            using (var cmd = new OleDbCommand(insertQuery, Connection))
            {
                cmd.Parameters.AddWithValue("@nameOfIngredient", ingredientName);
                cmd.ExecuteNonQuery();

            }
        }

        public void InsertRenovation(Renovation renovation)
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            var query = "INSERT INTO Renovations(id_room, dateOfStart, dateOfFinish, id_other_room, renovationType) VALUES(@id_room, @startingDate, @ending_date, @id_other_room, @renovationType)";

            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_room", renovation.RoomId);
                cmd.Parameters.AddWithValue("@startingDate", renovation.StartingDate.ToString());
                cmd.Parameters.AddWithValue("@ending_date", renovation.EndingDate.ToString());
                if (renovation.SecondRoomId == -1)
                {
                    cmd.Parameters.Add("@id_other_room", OleDbType.Integer).Value = DBNull.Value;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@id_other_room", renovation.SecondRoomId);
                }

                cmd.Parameters.AddWithValue("@renovationType", renovation.Type.ToString());
                cmd.ExecuteNonQuery();
            }

            Connection.Close();
        }


        public void InsertTransferHistoryOfEquipment(TransferHistoryOfEquipment transferHistoryOfEquipment)
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            var query = "INSERT INTO EquipmentTransferHistory(id_original_room, id_new_room, dateOfChange, isExecuted, amount, id_equipment) " +
                "VALUES(@first_room_id, @second_room_id, @transferDate, @isExecuted, @amount, @id_equipment)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@first_room_id", transferHistoryOfEquipment.FirstRoomId);
                cmd.Parameters.AddWithValue("@second_room_id", transferHistoryOfEquipment.SecondRoomId);
                cmd.Parameters.AddWithValue("@transferDate", transferHistoryOfEquipment.TransferDate.ToString());
                cmd.Parameters.AddWithValue("@isExecuted", transferHistoryOfEquipment.IsExecuted);
                cmd.Parameters.AddWithValue("@amount", transferHistoryOfEquipment.Amount);
                cmd.Parameters.AddWithValue("@id_equpment", transferHistoryOfEquipment.EquipmentId);
                cmd.ExecuteNonQuery();
            }

            Connection.Close();
        }


        public Room GetSelectedRoom(string query)
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();
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

        public Ingredient GetSelectedIngredient(string query)
        {
            
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);

            Ingredient ingredient = new Ingredient();

            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                ingredient = new Ingredient(Convert.ToInt32(reader["id"]), reader["nameOfIngredient"].ToString());
            }
            return ingredient;
        }



        public void UpdateContent(string query)
        {
            if (Connection.State == ConnectionState.Closed) Connection.Open();
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

        public List<Equipment> GetEquipment(string query)
        {
            List<Equipment> equipment = new List<Equipment>();


            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    EquipmentType typeOfEquipment;
                    Enum.TryParse<EquipmentType>(reader["type"].ToString(), out typeOfEquipment);

                    equipment.Add(new Equipment(Convert.ToInt32(reader["id"]), reader["nameOf"].ToString(), typeOfEquipment));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            Connection.Close();

            return equipment;
        }

        public List<Renovation> GetRenovations(string query)
        {
            List<Renovation> renovations = new List<Renovation>();


            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    TypeOfRenovation typeOfRenovation;
                    Enum.TryParse<TypeOfRenovation>(reader["renovationType"].ToString(), out typeOfRenovation);

                    try
                    {
                        renovations.Add(new Renovation(Convert.ToInt32(reader["id_room"]), Convert.ToDateTime(reader["dateOfStart"]), Convert.ToDateTime(reader["dateOfFinish"]), Convert.ToInt32(reader["id_other_room"]), typeOfRenovation));
                    }
                    catch (Exception)
                    {
                        renovations.Add(new Renovation(Convert.ToInt32(reader["id_room"]), Convert.ToDateTime(reader["dateOfStart"]), Convert.ToDateTime(reader["dateOfFinish"]), -1, typeOfRenovation));
                    }


                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            Connection.Close();

            return renovations;
        }

        public List<RoomHasEquipment> GetEquipmentInRoom(string query)
        {
            List<RoomHasEquipment> equipmentInRoom = new List<RoomHasEquipment>();


            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
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

        public List<TransferHistoryOfEquipment> GetTransferHistory(string query)
        {
            List<TransferHistoryOfEquipment> transferHistory = new List<TransferHistoryOfEquipment>();


            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    transferHistory.Add(new TransferHistoryOfEquipment(Convert.ToInt32(reader["id_original_room"]), Convert.ToInt32(reader["id_new_room"]), Convert.ToDateTime(reader["dateOfChange"]), 
                        Convert.ToBoolean(reader["isExecuted"]), Convert.ToInt32(reader["amount"]), Convert.ToInt32(reader["id_equipment"])));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            Connection.Close();

            return transferHistory;
        }


        public List<Room> GetRooms(string query)
        {
            List<Room> rooms = new List<Room>();


            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                OleDbCommand cmd = DatabaseHelpers.GetCommand(query, Connection);
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

        public Ingredient GetIngredient(int id)
        {
            string query = "select * from ingredients where id=" + id;
            Ingredient ingredient = GetSelectedIngredient(query);
            return ingredient;
        }

        public bool DoesIngredientExists(string name)
        {
            string query = "select * from Ingredients where nameOfIngredient='" + name + "'";
            Ingredient ingredient = GetSelectedIngredient(query);

            if (ingredient == null) return false;
            return true;
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
        public List<string> GetOperationRooms()
        {
            var query = "SELECT ID FROM Rooms WHERE type = operation";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        public List<string> GetExaminationRooms()
        {
            var query = "SELECT ID FROM Rooms WHERE type = examination";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }
    }
}
