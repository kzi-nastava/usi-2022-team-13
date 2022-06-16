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
using HealthCareSystem.Core.GUI;
using HealthCareSystem.Core.Users.Patients.Repository;

namespace HealthCareSystem.Core.Authentication
{
    class LoginAuthentication
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public LoginForm Login { get; set; }
        private readonly IPatientRepository _patientRepository;
        private static OleDbConnection _connection;

        public LoginAuthentication(string username, string password, LoginForm loginForm)
        {
            this.Username = username;
            this.Password = password;
            this.Login = loginForm;
            _patientRepository = new PatientRepository();
            try
            {
                _connection = DatabaseConnection.GetConnection();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

        }

        public void OpenViewForm()
        {
            User user = GetUser(Username, Password);

            if (user == null)
            {
                MessageBox.Show("Invalid Input. Try Again!");
            }

            else
            {

                switch (user.Role)
                {
                    case UserRole.Doctors:
                        DoctorView doctorView = new DoctorView(Username, Login);
                        this.Login.Hide();
                        doctorView.ShowDialog();
                        break;
                    case UserRole.HospitalManagers:
                        HospitalManagerView managerView = new HospitalManagerView(Username, Login);
                        this.Login.Hide();
                        managerView.ShowDialog();
                        break;
                    case UserRole.Patients:
                        bool isBlocked = _patientRepository.IsPatientBlocked(Username);
                        if (!isBlocked)
                        {
                            PatientView patientView = new PatientView(Username, Login);
                            this.Login.Hide();
                            patientView.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Patient is blocked!");
                        }
                        break;
                    case UserRole.Secretaries:
                        SecretaryView secretacyView = new SecretaryView(Username, Login);
                        this.Login.Hide();
                        secretacyView.ShowDialog();
                        break;
                }
                _connection.Close();
            }
        }

        private static User GetUser(string username, string password)
        {

            bool isInvalid = IsInvalidInput(username, password);
            if (isInvalid) return null;

            string query = "select role from users where usrnm = '" + username + "' and pass = '" + password + "'";
            List<string> roles = DatabaseCommander.ExecuteReaderQueries(query, _connection);

            if (!roles.Any()) return null;
            else
            {
                Enum.TryParse<UserRole>(roles[0], out var role);
                return new User(username, password, role);
            }

        }
        private static bool IsInvalidInput(string username, string password)
        {
            return (username == "" || password == "" || username.Contains(" "));

        }
    }
}
