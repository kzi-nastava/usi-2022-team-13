using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Rooms.HospitalEquipment.TransferHistoryOfEquipment.Model;

namespace HealthCareSystem.Core.Rooms.Repository
{
    class TransferHistoryRepository
    {
        public OleDbConnection Connection { get; set; }

        public TransferHistoryRepository()
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


        public List<TransferHistoryOfEquipment> GetTransferHistory(string query)
        {
            List<TransferHistoryOfEquipment> transferHistory = new List<TransferHistoryOfEquipment>();


            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
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
    }
}
