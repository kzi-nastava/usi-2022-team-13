using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Patients.Model;

namespace HealthCareSystem.Core
{
    class DatabaseCommander
    {
       
        public static void ExecuteNonQueries(string query, OleDbConnection connection)
        {
            if (connection.State == ConnectionState.Closed) connection.Open();

            using (var cmd = new OleDbCommand(query, connection))
            {
                cmd.ExecuteNonQuery();

            }
        }
        public static List<string> ExecuteReaderQueries(string query, OleDbConnection connection)
        {
            if (connection.State == ConnectionState.Closed) connection.Open();

            List<string> data = new List<string>();
            OleDbCommand cmd = GetCommand(query, connection);

            OleDbDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                data.Add(reader[0].ToString());
            }

            return data;
        }

        public static OleDbCommand GetCommand(string query, OleDbConnection connection)
        {
            if(connection.State == ConnectionState.Closed)connection.Open();
            OleDbCommand cmd = new OleDbCommand();

            cmd.Connection = connection;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            return cmd;
        }

    }
}
