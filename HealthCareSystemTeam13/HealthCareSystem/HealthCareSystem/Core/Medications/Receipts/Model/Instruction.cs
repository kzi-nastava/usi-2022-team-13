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
        
        public Instruction()
        {

        }
        public Instruction(DateTime startTime, int timesPerDay)
        {
            this.StartTime = startTime;
            this.TimesPerDay = timesPerDay;
        }

    }
}
