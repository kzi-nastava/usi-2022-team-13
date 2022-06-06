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
            FillTable(HospitalSurveys, hospitalSurveysQuery);
        }

        public void PullDoctorSurveys()
        {
            DoctorSurveys = new DataTable();
            string doctorSurveysQuery = "select * from doctorSurveys";
            FillTable(DoctorSurveys, doctorSurveysQuery);
        }

        private void FillTable(DataTable table, string query)
        {

            using (var cmd = new OleDbCommand(query, Connection))
            {
                if (Connection.State == ConnectionState.Closed) Connection.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
            }
            Connection.Close();
        }
    }
}
