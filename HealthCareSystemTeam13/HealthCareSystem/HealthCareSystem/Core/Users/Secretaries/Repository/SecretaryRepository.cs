using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using HealthCareSystem.Core;
using HealthCareSystem.Core.Scripts.Repository;
using HealthCareSystem.Core.Users.Patients.Model;

namespace HealthCareSystem.Core.Users.Patients.Repository
{
    class SecretaryRepository
    {
        public DataTable patients { get; set; }
        public DataTable blockedPatients { get; set; }
        public OleDbConnection Connection { get; set; }

        public SecretaryRepository()
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

        private void FillTable(DataTable table, string query)
        {

            using (var cmd = new OleDbCommand(query, Connection))
            {
                OleDbDataReader reader = cmd.ExecuteReader();
                table.Load(reader);
            }
        }
    
        public void PullPatients()
        {
            patients = new DataTable();
            string patientsQuery = "select Patients.id, Patients.FirstName as First Name, Patients.LastName as Last Name, Patients.isBlocked as Blocked from Patients";
            FillTable(patients, patientsQuery);
        }

        public void PullBlockedPatients()
        {
            blockedPatients = new DataTable();
            string blockedPatientsQuery = "select BlockedPatients.id, Patients.FirstName as First Name, Patients.LastName as Last Name, BlockedPatients.secretary_id as Blocked by from BlockedPatients" + 
            " left join Patients on Patients.id = BlockedPatients.id_patient group by Patients.id";
            FillTable(blockedPatients, blockedPatientsQuery);
        }
    }
}