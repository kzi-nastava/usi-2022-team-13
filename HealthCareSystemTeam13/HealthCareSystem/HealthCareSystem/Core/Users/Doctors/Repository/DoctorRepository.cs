using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Users.Doctors.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Doctors.Repository
{
    class DoctorRepository
    {
        public OleDbConnection Connection { get; set; }
        public DoctorRepository()
        {
            try
            {
                Connection = new OleDbConnection();

                Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=HCDb.mdb;
                Persist Security Info=False;";
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }


        }


        public bool IsDoctorAvailable(Doctor doctor, DateTime ExaminationDateTime, List<Examination> examinations)
        {
            for (int i = 0; i < examinations.Count(); i++)
            {
                TimeSpan difference = ExaminationDateTime.Subtract(examinations[i].DateOf);
                Console.WriteLine(ExaminationDateTime.ToString());

                if (Math.Abs(difference.TotalMinutes) < 15 && doctor.ID == examinations[i].IdDoctor)
                {
                    return false;
                }
            }

            return true;
        }

        public BindingList<Doctor> GetDoctors()
        {
            BindingList<Doctor> doctors = new BindingList<Doctor>();
            try
            {
                Connection.Open();

                OleDbCommand cmd = DatabaseHelpers.GetCommand("select * from doctors", Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DoctorSpeciality speciality;
                    Enum.TryParse<DoctorSpeciality>(reader["speciality"].ToString(), out speciality);

                    doctors.Add(new Doctor(Convert.ToInt32(reader["ID"]), reader["firstName"].ToString(), reader["lastName"].ToString(), Convert.ToInt32(reader["user_id"]), speciality));
                }
            }catch(Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            Connection.Close();

            return doctors;
        }
    }
}
