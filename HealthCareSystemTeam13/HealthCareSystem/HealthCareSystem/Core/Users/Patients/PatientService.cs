using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Patients.Service
{
    class PatientService: IPatientService
    {
        public List<int> GetHoursForNotifications(Dictionary<int, DateTime> instructions, int notificationAlertTime)
        {
            List<int> atHours = new List<int>();
            int timesPerDay = instructions.Keys.First();
            int startHour = 8;
            int incremental = 23 / timesPerDay;

            while (startHour < 24 && timesPerDay > 0)
            {
                atHours.Add(startHour - notificationAlertTime);
                startHour += incremental;
                timesPerDay -= 1;
            }

            return atHours;
        }
    }
}
