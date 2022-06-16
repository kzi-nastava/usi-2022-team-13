using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Medications.Repository
{
    interface IInstructionRepository
    {
        void InsertInstruction(string description);
        Dictionary<int, DateTime> GetMedicationInstructions(int patientId);
        int GetLastCreatedInstructionId();

    }
}
