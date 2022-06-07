using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Rooms.Renovations.Model;

namespace HealthCareSystem.Core.Rooms.Repository
{
    class RenovationRepository
    {
        public OleDbConnection Connection { get; }
        public DataTable Renovations { get; private set; }

        public RenovationRepository()
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
        public void PullRenovations()
        {
            Renovations = new DataTable();
            string renovationsQuery = "select * from renovations";
            GUIHelpers.FillTable(Renovations, renovationsQuery, Connection);
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


        public List<Renovation> GetRenovations(string query)
        {
            List<Renovation> renovations = new List<Renovation>();


            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                OleDbCommand cmd = DatabaseCommander.GetCommand(query, Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {

                    Renovation.TypeOfRenovation typeOfRenovation;
                    Enum.TryParse<Renovation.TypeOfRenovation>(reader["renovationType"].ToString(), out typeOfRenovation);

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


    }
}
