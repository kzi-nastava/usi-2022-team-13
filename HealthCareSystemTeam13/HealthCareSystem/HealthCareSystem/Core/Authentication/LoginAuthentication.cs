using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using HealthCareSystem.Core.Scripts.Repository;
using HealthCareSystem.Core.Users.Model;
using HealthCareSystem.Core.Authentication;
using HealthCareSystem.Core;

namespace HealthCareSystem.Core.Authentication
{
    class LoginAuthentication
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public LoginForm Login { get; set; }
        private static OleDbConnection Connection;
        public LoginAuthentication(string username, string password, LoginForm loginForm)
        {
            this.Username = username;
            this.Password = password;
            this.Login = loginForm;

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

        public void OpenViewForm()
        {
            User user = GetUser(Username, Password);

            Connection.Close();
            if (user == null)
            {
                MessageBox.Show("Invalid Input. Try Again!");
            }

            else
            {

                switch (user.Role)
                {
                    case UserRole.Doctors:
                        DoctorView doctorView = new DoctorView();
                        this.Login.Hide();
                        doctorView.ShowDialog();
                        break;
                    case UserRole.HospitalManagers:
                        HospitalManagerView managerView = new HospitalManagerView();
                        this.Login.Hide();
                        managerView.ShowDialog();
                        break;
                    case UserRole.Patients:
                        break;
                    case UserRole.Secretaries:
                        break;
                }
            }
        }

        private static User GetUser(string username, string password)
        {

            bool isInvalid = IsInvalidInput(username, password);

            if (isInvalid) return null;

            string query = "select role from users where usrnm = '" + username + "' and pass = '" + password + "'";

            List<string> roles = DatabaseHelpers.ExecuteReaderQueries(query, Connection);
            

            if(roles.Count() == 0)
            {

                return null;
            }
            else
            {
                UserRole role;
                Enum.TryParse<UserRole>(roles[0], out role);
                return new User(username, password, role);
            }

        }
        private static bool IsInvalidInput(string username, string password)
        {
            return (username == "" || password == "" || username.Contains(" "));

        }
    }
}
