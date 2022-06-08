using HealthCareSystem.Core.Surveys.HospitalSurveys.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Surveys.Repository
{
    class SurveyRepository
    {
        public DataTable HospitalSurveys { get; set; }
        public DataTable DoctorSurveys { get; set; }
        public OleDbConnection Connection { get; set; }
        public SurveyRepository()
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

        public void PullHospitalSurveys()
        {
            HospitalSurveys = new DataTable();
            string hospitalSurveysQuery = "select * from hospitalSurveys";
            GUIHelpers.FillTable(HospitalSurveys, hospitalSurveysQuery, Connection);
        }

        public void PullDoctorSurveys()
        {
            DoctorSurveys = new DataTable();
            string doctorSurveysQuery = "select * from doctorSurveys";
            GUIHelpers.FillTable(DoctorSurveys, doctorSurveysQuery, Connection);
        }

        public List<HospitalSurvey> GetHospitalSurveys()
        {
            List<HospitalSurvey> hospitalSurveys = new List<HospitalSurvey>();

            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                OleDbCommand cmd = DatabaseCommander.GetCommand("select * from hospitalSurveys", Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    hospitalSurveys.Add(new HospitalSurvey(Convert.ToInt32(reader["quality"]), Convert.ToInt32(reader["higyene"]), Convert.ToInt32(reader["isSatisfied"]), 
                        Convert.ToInt32(reader["wouldRecomend"]), reader["comment"].ToString()));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            Connection.Close();

            return hospitalSurveys;
        }

        public List<DoctorSurvey> GetDoctorSurveys()
        {
            List<DoctorSurvey> doctorSurveys = new List<DoctorSurvey>();

            try
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();

                OleDbCommand cmd = DatabaseCommander.GetCommand("select * from doctorSurveys", Connection);
                OleDbDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    doctorSurveys.Add(new DoctorSurvey(Convert.ToInt32(reader["id_doctor"]), Convert.ToInt32(reader["doctorGrade"]), Convert.ToInt32(reader["quality"]), Convert.ToBoolean(reader["wouldRecommend"]), reader["comment"].ToString()));
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            Connection.Close();

            return doctorSurveys;
        }

        internal void AddDoctorSurvey(int doctorId, int patientId, int rating, int quality, bool wouldReccomend, string comment)
        {
            string query = "insert into DoctorSurveys(id_doctor, id_patient, doctorGrade, quality, wouldRecommend, comment) values(" + doctorId + ", " + patientId + ", " + rating + ", " + quality + ", " + wouldReccomend + ", '" + comment + "')";

            DatabaseCommander.ExecuteNonQueries(query, Connection);
        }

        public void AddHospitalSurvey(HospitalSurvey survey, int patientId)
        {
            bool isSatisfied = survey.Happiness == 1 ? true : false;
            bool wouldReccomend = survey.WouldRecommend == 1 ? true : false;
            string query = "insert into HospitalSurveys(quality, higyene, isSatisfied, wouldRecomend, comment, id_patient) values(" + survey.QualityOfService + ", " + survey.Cleanliness + ", " + isSatisfied + ", " + wouldReccomend + ", '" + survey.Comment + "', " + patientId + ")";
            DatabaseCommander.ExecuteNonQueries(query, Connection);
        }

    }

    
}
