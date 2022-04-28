using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using HealthCareSystem.Core;

namespace HealthCareSystem.Core.Users.Patients.Repository
{
    class PatientRepository
    {
        public string Username { get; set; }
        public DataTable examinations { get; set; }
        public OleDbConnection Connection { get; set; }
        public PatientRepository(string username) { 
            Username = username;
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
        private int GetPatientId()
        {
            string userId = DatabaseHelpers.ExecuteReaderQueries("select id from users where usrnm = '" + Username + "'", Connection)[0];

            int patientId = Convert.ToInt32(DatabaseHelpers.ExecuteReaderQueries("select id from patients where user_id = " + Convert.ToInt32(userId) + "", Connection)[0]);

            return patientId;
        }

        public void PullExaminations()
        {
            examinations = new DataTable();
   
            string examinationsQuery = "select Examination.id, Doctors.FirstName + ' ' +Doctors.LastName as Doctor, dateOf as [Date and Time], id_room as RoomID, duration from Examination left outer join Doctors  on Examination.id_doctor = Doctors.id " +
                "where id_patient = " + GetPatientId() +"";

            FillTable(examinations, examinationsQuery);


        }
        
        public List<string> GetExamination(int examinationId)
        {
            string query = "select id_doctor, dateOf, id_room from Examination where id = " + examinationId + "";
            return DatabaseHelpers.ExecuteReaderQueries(query, Connection);
        }

        public void CancelExamination(int examinationId)
        {
            string query = "delete from Examination where id = " + examinationId + "";
            DatabaseHelpers.ExecuteNonQueries(query, Connection);
        }

        private void FillTable(DataTable table, string query)
        {

            using (var cmd = new OleDbCommand(query, Connection))
            {
                OleDbDataReader reader = cmd.ExecuteReader();
                table.Load(reader);


            }
        }

        
    }
}
