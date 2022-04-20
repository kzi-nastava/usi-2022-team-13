using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace HealthCareSystem.Core.Scripts.Repository
{
    class InsertionRepository
    {
        private static OleDbConnection Connection;

        public InsertionRepository()
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


        public void ExecuteQueries()
        {

            InsertUsers();
            InsertDoctors();
            InsertManager();
            InsertPatients();
            InsertSecretaries();
            InsertSurveys();


            Connection.Close();
        }


        public void DeleteRecords()
        {
            try
            {
                Connection.Open();

            }catch(Exception exception)
            {

            }

            var query = "Delete from users";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();

            }

            Connection.Close();

        }
        public static void InsertUsers()
        {
            var query = "INSERT INTO users(usrnm, pass, role) VALUES(@usrnm, @pass, @role)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@usrnm", "markoni");
                cmd.Parameters.AddWithValue("@pass", "markoni123");
                cmd.Parameters.AddWithValue("@role", "Patient");
                cmd.ExecuteNonQuery();

            }
        }

        public static void InsertDoctors()
        {

        }
        public static void InsertPatients()
        {

        }
        public static void InsertSecretaries()
        {

        }
        public static void InsertManager()
        {

        }
        public static void InsertSurveys()
        {

        }

    }
}
