using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Doctors.Repository
{
    interface IDaysOffRepository
    {
        void InsertDaysOff(DateTime startDate, DateTime endDate, string reasonForDaysOff, bool isUrgent, int doctorId);
        void PullDaysOffRequests();
        DataTable GetDaysOfRequests();
        void ManageDaysOffRequest(string username, int requestId, bool approved, string comment = "");
        void PullRequestsForDaysOff(int doctorId);
        DataTable GetRequestsForDaysOff();

    }
}
