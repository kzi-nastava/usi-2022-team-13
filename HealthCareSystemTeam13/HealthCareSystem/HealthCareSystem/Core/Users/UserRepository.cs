using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Users.Model;

namespace HealthCareSystem.Core.Users
{
    class UserRepository
    {
        public OleDbConnection Connection { get; set; }

        public UserRepository()
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
        public void InsertSingleUser(User user)
        {
            var query = "INSERT INTO users(usrnm, pass, role) VALUES(@usrnm, @pass, @role)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@usrnm", user.Username);
                cmd.Parameters.AddWithValue("@pass", user.Password);
                cmd.Parameters.AddWithValue("@role", user.Role.ToString());
                cmd.ExecuteNonQuery();
            }
        }

        public List<string> GetUserId(string username)
        {
            var query = "SELECT id FROM Users WHERE usrnm = '" + username + "'";
            return DatabaseCommander.ExecuteReaderQueries(query, Connection);
        }

    }
}
