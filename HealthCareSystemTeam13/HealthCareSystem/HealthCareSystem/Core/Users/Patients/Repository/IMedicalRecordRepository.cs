using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Users.Patients.Model;

namespace HealthCareSystem.Core.Users.Patients.Repository
{
    interface IMedicalRecordRepository
    {
        string[] GetMedicalRecord(string query);
        void InsertSingleMedicalRecord(MedicalRecord medicalRecord);

    }
}
