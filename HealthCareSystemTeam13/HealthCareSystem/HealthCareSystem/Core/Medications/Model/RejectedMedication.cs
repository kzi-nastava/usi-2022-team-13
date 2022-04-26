using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Medications.Model
{
    class RejectedMedication
    {
       
        public int MedicationID { get; set; }
        public int DoctorID { get; set; }
        public string Description { get; set; }
       
        public RejectedMedication()
        {

        }
        public RejectedMedication(int medicationID, int doctorID, string description)
        {
            this.MedicationID = medicationID;
            this.DoctorID = doctorID;
            this.Description = description;
        }
    }
}
