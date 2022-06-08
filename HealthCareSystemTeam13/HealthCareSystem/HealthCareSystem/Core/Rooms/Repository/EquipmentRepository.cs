using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using HealthCareSystem.Core.Rooms.DynamicEqipmentRequests.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.RoomHasEquipment.Model;

namespace HealthCareSystem.Core.Rooms.Repository
{
    public class EquipmentRepository
    {
        public OleDbConnection Connection { get; set; }
        public DataTable EquipmentInWarehouse { get; private set; }
        public DataTable DynamicEquipment { get; private set; }
        public DataTable TransferDynamicEquipment { get; private set; }
        public DataTable Equipment { get; private set; }
        private RoomRepository _roomRepository;

        public EquipmentRepository()
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

            _roomRepository = new RoomRepository();
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

        public void PullEquipmentInWarehouse()
        {
            EquipmentInWarehouse = new DataTable();
            var warehouseId = _roomRepository.GetWarehouseId();
            var query = "select * from Equipment where type = 'Dynamic' and id not in (select id_equipment from RoomHasEquipment where id_room = " + warehouseId + " and amount > 0)";
            GUIHelpers.FillTable(EquipmentInWarehouse, query, Connection);
        }

        public void PullDynamicEquipment()
        {
            DynamicEquipment = new DataTable();
            var query = "select * from RoomHasEquipment where amount < 6";
            GUIHelpers.FillTable(DynamicEquipment, query, Connection);
        }

        public void PullTransferDynamicEquipment(int equipmentId)
        {
            TransferDynamicEquipment = new DataTable();
            var query = "select * from RoomHasEquipment where id_equipment = " + equipmentId + "";
            GUIHelpers.FillTable(TransferDynamicEquipment, query, Connection);
        }
        public void InsertSingleDynamicEquipmentRequest(DynamicEquipmentRequest request)
        {
            var query = "INSERT INTO RequestForDinamicEquipment(id_equipment, amount, dateOf, id_secretary) VALUES(@id_equipment, @amount, @dateOf, @id_secretary)";
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_equipment", request.EquipmentId);
                cmd.Parameters.AddWithValue("@amount", request.Quantity);
                cmd.Parameters.AddWithValue("@dateOf", request.Date.ToString());
                cmd.Parameters.AddWithValue("@id_secretary", request.SecretaryId);
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateSigleDynamicEquipment(DynamicEquipmentRequest request)
        {
            int warehouseId = _roomRepository.GetWarehouseId();
            var amounts = DatabaseCommander.ExecuteReaderQueries("select amount from RoomHasEquipment " +
               "where id_room = " + warehouseId + " and id_equipment = " + request.EquipmentId, Connection);
            string query;
            if (amounts.Count != 0)
            {
                query = "Update RoomHasEquipment SET amount = " + (Convert.ToInt32(amounts[0]) + request.Quantity) + " WHERE id_room =" + warehouseId + " and id_equipment = " + request.EquipmentId;
            }
            else
            {
                query = "Insert into RoomHasEquipment(id_room, id_equipment, amount) values(" + _roomRepository.GetWarehouseId() + ", " + request.EquipmentId + ", " + request.Quantity + ")";
            }
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateSigleDynamicEquipment(int amount, int roomHasEquipmentID)
        {
            var query = "Update RoomHasEquipment SET amount = amount - " + amount + " WHERE id = " + roomHasEquipmentID.ToString();
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateSigleDynamicEquipment(int amount, RoomHasEquipment roomHasEquipment)
        {
            var query = "Update RoomHasEquipment SET amount = amount + " + amount + " WHERE id = " + roomHasEquipment.Id.ToString();
            if(Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
        public void DeleteSingleDynamicEquipmentRequest(int requestID)
        {
            var query = "DELETE from RequestForDinamicEquipment WHERE id = " + requestID + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }
        private static DynamicEquipmentRequest GetDynamicEquipmentRequestsFromReader(OleDbDataReader reader)
        {
            return new DynamicEquipmentRequest((int)reader["id_equipment"], (int)reader["amount"], (DateTime)reader["dateOf"], (int)reader["id_secretary"], (int)reader["id"]);
        }

        public List<DynamicEquipmentRequest> GetDeliveredDynamicEquipmentRequest()
        {
            List<DynamicEquipmentRequest> requests = new List<DynamicEquipmentRequest>();
            var query = "SELECT id, id_equipment, amount, dateOf, id_secretary FROM RequestForDinamicEquipment where dateOf < #" + (DateTime.Now.AddDays(-1)).ToString() + "#";
            OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);

            if(Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
            OleDbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                requests.Add(GetDynamicEquipmentRequestsFromReader(reader));
            }

            return requests;
        }
        public void CheckDynamicEquipmentRequests()
        {
            List<DynamicEquipmentRequest> requests = GetDeliveredDynamicEquipmentRequest();
            foreach (DynamicEquipmentRequest request in requests)
            {
                UpdateSigleDynamicEquipment(request);
                DeleteSingleDynamicEquipmentRequest(request.ID);
            }
        }

        public void PullEquipment()
        {
            Equipment = new DataTable();
            string equipmentQuery = "select rhe.id_room as 'Room id', r.type as 'Room type', rhe.id_equipment as 'Equipment id', e.nameOf as 'Equipment name', e.type as 'Equipment type', rhe.amount as 'Amount' " +
                                "from Equipment e, Rooms r, RoomHasEquipment rhe " +
                                "where rhe.id_room = r.ID and rhe.id_equipment = e.ID";
            GUIHelpers.FillTable(Equipment, equipmentQuery, Connection);

        }

        public List<Equipment> GetEquipment(string query)
        {
            List<Equipment> equipment = new List<Equipment>();


            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Equipment.EquipmentType typeOfEquipment;
                    Enum.TryParse<Equipment.EquipmentType>(reader["type"].ToString(), out typeOfEquipment);

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
    }
}