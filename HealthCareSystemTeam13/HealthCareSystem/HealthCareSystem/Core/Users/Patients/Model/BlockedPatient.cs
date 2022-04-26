using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Patients.Model
{
    class BlockedPatient
    {

        public int PatientID { get; set; }
        public int SecretaryID { get; set; }
        public DateTime DateOf { get; set; }
        
        public BlockedPatient()
        {

        }
        public BlockedPatient(int patientID, int secretaryID, DateTime dateOf)
        {
            this.PatientID = patientID;
            this.SecretaryID = secretaryID;
            this.DateOf = dateOf;
        }

    }
}
