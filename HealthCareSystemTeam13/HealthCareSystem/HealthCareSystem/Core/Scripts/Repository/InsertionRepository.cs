using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using HealthCareSystem.Core.Users.Model;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.HospitalManagers;

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
            InsertManagers();
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

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            finally
            {
                // In here we need to make a list of table names
                // But in an opposite order of insertion so that the deletion
                // goes from the last inserted to first
                var query = "Delete from users";
                using (var cmd = new OleDbCommand(query, Connection))
                {
                    cmd.ExecuteNonQuery();

                }

                Connection.Close();
            }
        }


        private static List<String> GetUserIds(UserRole role)
        {
            List<String> ids = new List<String>();

            var query = "select ID from Users where role='" + role.ToString() + "'";

            return ExecuteReaderQueries(ids, query);
        }

        private static List<string> ExecuteReaderQueries(List<string> data, string query)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = Connection;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = query;
            OleDbDataReader idsReader = cmd.ExecuteReader();
            while (idsReader.Read())
            {
                data.Add(idsReader[0].ToString());
            }


            return data;
        }

        private static List<User> GetUsers()
        {
            List<User> users = new List<User>();

            users.Add(new User("markomarkovic", "marko123", UserRole.HospitalManagers));
            users.Add(new User("mirkobreskvica", "mirko123", UserRole.Doctors));
            users.Add(new User("marinaadamovic", "marina123", UserRole.Doctors));

            return users;
        }

        private static List<Doctor> GetDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            List<String> userIds = GetUserIds(UserRole.Doctors);
            
            doctors.Add(new Doctor("Mirko", "Breskvica", Convert.ToInt32(userIds[0]), DoctorSpeciality.BasicPractice));
            doctors.Add(new Doctor("Marina", "Adamovic", Convert.ToInt32(userIds[1]), DoctorSpeciality.Dermatology));


            return doctors;
        }
        private static List<HospitalManager> GetHospitalManagers()
        {
            List<HospitalManager> hospitalManager = new List<HospitalManager>();
            List<String> userIds = GetUserIds(UserRole.HospitalManagers);

            hospitalManager.Add(new HospitalManager("Marko", "Markovic", Convert.ToInt32(userIds[0])));

            return hospitalManager;
        }

        private static void InsertSingleUser(User user)
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

        private static void InsertSingleDoctor(Doctor doctor)
        {
            var query = "INSERT INTO Doctors(firstName, lastName, user_id, speciality) VALUES(@firstName, @LastName, @user_id, @speciality)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@firstName", doctor.FirstName);
                cmd.Parameters.AddWithValue("@LastName", doctor.LastName);
                cmd.Parameters.AddWithValue("@user_id", doctor.UserId);
                cmd.Parameters.AddWithValue("@speciality", doctor.Speciality.ToString());
                cmd.ExecuteNonQuery();

            }
        }
        private static void InsertSingleHospitalManager(HospitalManager hospitalManager)
        {
            var query = "INSERT INTO HospitalManagers(firstName, lastName, user_id) VALUES(@firstName, @LastName, @user_id)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@firstName", hospitalManager.FirstName);
                cmd.Parameters.AddWithValue("@LastName", hospitalManager.LastName);
                cmd.Parameters.AddWithValue("@user_id", hospitalManager.UserId);
                cmd.ExecuteNonQuery();

            }
        }

        private static void InsertUsers()
        {
            List<User> users = GetUsers();

            foreach(User user in users)
            {
                InsertSingleUser(user);
            }
  
        }

        private static void InsertDoctors()
        {
            List<Doctor> doctors = GetDoctors();

            foreach (Doctor doctor in doctors)
            {
                InsertSingleDoctor(doctor);
            }
        }
        private static void InsertPatients()
        {

        }
        private static void InsertSecretaries()
        {

        }
        private static void InsertManagers()
        {
            List<HospitalManager> managers = GetHospitalManagers();

            foreach (HospitalManager manager in managers)
            {
                InsertSingleHospitalManager(manager);
            }

        }
        private static void InsertSurveys()
        {

        }

    }
}
