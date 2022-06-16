using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Users.Doctors.Model;
using HealthCareSystem.Core.Users.Secretaries.Repository;

namespace HealthCareSystem.Core.Users.Doctors.Repository
{
    class DaysOffRepository:IDaysOffRepository
    {
        public OleDbConnection Connection { get; set; }
        public DataTable DaysOffRequests { get; private set; }
        public DataTable RequestsForDaysOff { get; private set; }
        private readonly SecretaryRepository _secretaryRepository;

        public DaysOffRepository()
        {
            try
            {
                Connection = DatabaseConnection.GetConnection();

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }

            _secretaryRepository = new SecretaryRepository();
        }

        public void InsertDaysOff(DateTime startDate, DateTime endDate, string reasonForDaysOff, bool isUrgent, int doctorId)
        {


            string query = "insert into DoctorRequestDaysOf (dateFrom, dateTo, reasonOf, isUrgent, id_doctor, stateOfRequest) " +
                "values (@dateFrom, @dateTo, @reasonOf, @isUrgent, @doctor_id, @stateOfRequest)";


            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@dateFrom", startDate);
                cmd.Parameters.AddWithValue("@dateTo", endDate);
                cmd.Parameters.AddWithValue("@reasonOf", reasonForDaysOff);
                cmd.Parameters.AddWithValue("@isUrgent", isUrgent);
                cmd.Parameters.AddWithValue("@id_doctor", doctorId);
                if (!isUrgent)
                    cmd.Parameters.AddWithValue("@stateOfRequest", StateOfRequest.Waiting.ToString());
                else
                    cmd.Parameters.AddWithValue("@stateOfRequest", StateOfRequest.Accepted.ToString());

                cmd.ExecuteNonQuery();
            }

        }

        public void PullDaysOffRequests()
        {
            DaysOffRequests = new DataTable();
            var query = "select * from DoctorRequestDaysOf df where df.id not in (select mdf.id_request from ManagementOfDaysOfRequests mdf)";
            GUIHelpers.FillTable(DaysOffRequests, query, Connection);
        }

        public DataTable GetDaysOfRequests()
        {
            return DaysOffRequests;
        }

        public void ManageDaysOffRequest(string username, int requestId, bool approved, string comment = "")
        {
            var query = "INSERT INTO ManagementOfDaysOfRequests(id_request, id_secretary, isapproved, comment) VALUES(@id_request, @id_secretary, @isapproved, @comment)";
            using (var cmd = new OleDbCommand(query, Connection))
            {
                cmd.Parameters.AddWithValue("@id_request", requestId);
                cmd.Parameters.AddWithValue("@id_secretary", _secretaryRepository.GetSecretaryId(username)[0]);
                cmd.Parameters.AddWithValue("@isapproved", approved.ToString());
                cmd.Parameters.AddWithValue("@comment", comment);
                cmd.ExecuteNonQuery();
            }
        }

        public void PullRequestsForDaysOff(int doctorId)
        {
            RequestsForDaysOff = new DataTable();

            string requestsForDaysOffQuery = "select DateFrom, DateTo, reasonOf, stateOfRequest, isUrgent from DoctorRequestDaysOf " +
                "where id_doctor = " + doctorId + "";

            GUIHelpers.FillTable(RequestsForDaysOff, requestsForDaysOffQuery, Connection);
        }
    }
}
