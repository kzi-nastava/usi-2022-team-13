using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Patients
{
    interface IPatientService
    {
        List<int> GetHoursForNotifications(Dictionary<int, DateTime> instructions, int notificationAlertTime);
    }
}
