using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
namespace HealthCareSystem.Core
{
    class DatabaseHelpers
    {
        public static void ExecuteNonQueries(string query, OleDbConnection connection)
        {
            using (var cmd = new OleDbCommand(query, connection))
            {
                cmd.ExecuteNonQuery();

            }
        }
        public static List<string> ExecuteReaderQueries(string query, OleDbConnection connection)
        {
            Console.WriteLine(query);

            List<string> data = new List<string>();
            OleDbCommand cmd = new OleDbCommand();
           
            cmd.Connection = connection;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            
            OleDbDataReader queryData = cmd.ExecuteReader();

            while (queryData.Read())
            {
                data.Add(queryData[0].ToString());
            }

            return data;
        }
        public static bool IsPatientBlocked(string patientUsername, OleDbConnection connection)
        {

            List<string> userIds = ExecuteReaderQueries("select id from users where usrnm= '" + patientUsername + "'", connection);

            List<string>  blockedPatients = ExecuteReaderQueries("select isBlocked from Patients where user_id = " + Convert.ToInt32(userIds[0]) + "", connection);
  
            return blockedPatients[0] == "True";
        }

    }
}
