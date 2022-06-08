using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Examinations.Model;

namespace HealthCareSystem.Core
{
    class TimeDateHelpers
    {
        public static DateTime GetMergedDateTime(DateTime examinationDate, string examinationTime)
        {
            string[] examinationHourMinute = examinationTime.Split(':');
            DateTime examinationDateTime = new DateTime(examinationDate.Year, examinationDate.Month, examinationDate.Day, Convert.ToInt32(examinationHourMinute[0]), Convert.ToInt32(examinationHourMinute[1]), 0);
            return examinationDateTime;
        }

        public static DateTime GetNewStartDate(DateTime startDate, int startHour, int startMinute, int endHour, int endMinute)
        {
            startDate = startDate.AddMinutes(15);
            if (startDate.Hour > endHour)
            {
                startDate = new DateTime(startDate.Year, startDate.Month, startDate.Day, startHour, startMinute, 0).AddDays(1);
            }
            return startDate;
        }

        public static void MoveExaminationDateTimeBoundaries(ref DateTime startDate, ref DateTime endDate)
        {
            startDate = startDate.AddHours(-4);
            endDate = endDate.AddDays(2).AddHours(4);
        }

        public static bool IsValidTime(DateTime startDate, Examination takenExamination)
        {
            TimeSpan difference = startDate.Subtract(takenExamination.DateOf);
            if (Math.Abs(difference.TotalMinutes) < 15) return false;
            return true;
        }
        public static bool IsValidExaminationDate(DateTime examinationDate)
        {
            return examinationDate > DateTime.Now;
        }
    }
}
