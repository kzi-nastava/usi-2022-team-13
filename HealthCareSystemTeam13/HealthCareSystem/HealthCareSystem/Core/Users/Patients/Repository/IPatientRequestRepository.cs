using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Patients.Repository
{
    interface IPatientRequestRepository
    {
        bool IsRequestChanged(string requestId);
        Dictionary<string, string> GetPatientRequest(string requestId);
        void DeleteSinglePatientRequest(string requestId);
    }
}
