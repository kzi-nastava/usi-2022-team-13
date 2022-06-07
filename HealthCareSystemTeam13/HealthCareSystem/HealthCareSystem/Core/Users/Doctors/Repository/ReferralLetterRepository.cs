using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Users.Doctors.Model;

namespace HealthCareSystem.Core.Users.Doctors.Repository
{
    class ReferralLetterRepository
    {
        public OleDbConnection Connection { get; set; }
        public DataTable ReferralLetters { get; private set; }

        public ReferralLetterRepository()
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
        public void InsertReferral(ReferralLetter referralLetter, int option)
        {
            int checkState = 0;
            if (Connection.State == ConnectionState.Closed) { Connection.Open(); checkState = 1; }

            string query = "INSERT INTO ReferralLetter" +
                "(id_doctor, id_patient, id_forwarded_doctor, typeOfExamination, speciality) " +
                "VALUES (@id_doctor, @id_patient," +
                " @id_forwarded_doctor, @typeOfExamination, @speciality)";

            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_doctor", referralLetter.CurrentDoctorID);
                cmd.Parameters.AddWithValue("@id_patient", referralLetter.CurrentPatientID);

                if (option == 1)
                    cmd.Parameters.AddWithValue("@id_forwarded_doctor", referralLetter.ForwardedDoctorID);
                else if (option == 2)
                    cmd.Parameters.AddWithValue("@id_forwarded_doctor", DBNull.Value);
                Console.WriteLine(referralLetter.ExaminationType);
                cmd.Parameters.AddWithValue("@typeOfExamination", referralLetter.ExaminationType);
                if (option == 2)
                    cmd.Parameters.AddWithValue("@speciality", referralLetter.Speciality);
                else if (option == 1)
                    cmd.Parameters.AddWithValue("@id_forwarded_doctor", DBNull.Value);

                cmd.ExecuteNonQuery();
            }

            if (Connection.State == ConnectionState.Open && checkState == 1) Connection.Close();
        }

        public void PullReferralLetters()
        {
            ReferralLetters = new DataTable();
            var query = "select * from ReferralLetter";
            GUIHelpers.FillTable(ReferralLetters, query, Connection);
        }

        public void DeleteSingleReferralLetter(string letterID)
        {
            var query = "DELETE from ReferralLetter WHERE id = " + Convert.ToInt32(letterID) + "";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.ExecuteNonQuery();
            }
        }

    }
}
