using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Medications.Receipts.Model
{
    class Instruction
    {

        public DateTime StartTime { get; set; }

        public int TimesPerDay { get; set; }

        public string Description { get; set; }
        
        public Instruction()
        {

        }
        public Instruction(DateTime startTime, int timesPerDay)
        {
            StartTime = startTime;
            TimesPerDay = timesPerDay;
        }

        public Instruction(DateTime startTime, int timesPerDay, string description)
        {
            StartTime = startTime;
            TimesPerDay = timesPerDay;
            Description = description;

        }

    }
}
