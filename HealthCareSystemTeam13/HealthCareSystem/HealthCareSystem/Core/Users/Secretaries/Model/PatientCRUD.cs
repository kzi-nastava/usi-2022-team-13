using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using HealthCareSystem.Core.Scripts.Repository;
using HealthCareSystem.Core.Users.Model;
using HealthCareSystem.Core.Authentication;
using HealthCareSystem.Core;
using HealthCareSystem.Core.GUI;


namespace HealthCareSystem.Core.Users.Secretaries.Model
{
    class PatientCRUD
    {
        public PatientCRUDForm PatientsForm { get; set; }
        private static OleDbConnection Connection;
        public PatientCRUD(PatientCRUDForm patientCRUDForm)
        {

            this.PatientsForm = patientCRUDForm;

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

        public static void LoadPatientsData() 
        { 

        }
    }
}
