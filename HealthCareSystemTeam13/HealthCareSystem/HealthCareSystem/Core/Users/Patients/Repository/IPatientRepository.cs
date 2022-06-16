using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Users.Patients.Model;

namespace HealthCareSystem.Core.Users.Patients.Repository
{
    interface IPatientRepository
    {
        int GetPatientId();
        Dictionary<string, string> GetPatientNameAndMedicalStats(int patientId);
        int GetPatientIdByFirstName(string firstName);
        void UpdatePatientContent(string query, int patiendId = 0);
        int GetPatientId(string patientUsername);
        BindingList<Patient> GetPatients();
        string GetUsernameFromPatient(Patient patient);
        bool IsPatientBlocked(string patientUsername);
        void BlockSpamPatients(string patientUsername);
        void BlockPatient(int patientId);
        void PullPatients();
        void PullBlockedPatients();
        DataTable GetPatientsTable();
        DataTable GetBlockedPatientsTable();
        void InsertSinglePatient(Patient patient);
        List<string> GetPatientIdByUserId(string userID);
        void InsertSingleBlockedPatient(BlockedPatient blockedPatient);
        void DeleteSinglePatient(string patientID);
        void DeleteSingleBlockedPatient(string blockedPatientID);
        Dictionary<string, string> GetPatientInformation(string patientID);
        void UpdatePatient(string patientID, string username, string password, string name, string lastname);
        void BlockSinglePatient(string patientID, string username);
        Patient GetSelectedPatient(string query);
        Patient SetPatientValues(OleDbDataReader reader);
        string GetUsername();
        void SetUsername(string username);

    }
}
