using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Medications.Model
{
    class Receipt
    {
        public int DoctorId { get; set; }
        public int InstructionId { get; set; }
        public int PatientId { get; set; }
        public DateTime DateOfHandout { get; set; }

        public Receipt(int doctorId, int instructionId, int patientId, DateTime dateOfHandout)
        {
            DoctorId = doctorId;
            InstructionId = instructionId;
            PatientId = patientId;
            DateOfHandout = dateOfHandout;
        }
    }
}
