using System;
using System.Data.OleDb;

namespace HealthCareSystem.Core.Rooms.Repository
{
    public class EquipmentRepository
    {
        public OleDbConnection Connection { get; set; }

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
        }
    }
}