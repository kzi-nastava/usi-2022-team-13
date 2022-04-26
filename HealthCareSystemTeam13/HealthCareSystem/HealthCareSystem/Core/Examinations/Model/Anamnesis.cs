using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Examinations.Model
{

    class Anamnesis
    {
        public int ExaminationID { get; set; }

        public string Notice { get; set; }

        public string Conclusions{ get; set; }

        public DateTime DateOf { get; set; }

        public Anamnesis()
        {

        }
        public Anamnesis(int examinationID, string notice, string conclusion, DateTime dateOf)
        {
            this.ExaminationID = examinationID;
            this.Notice = notice;
            this.Conclusion = conclusion;
            this.DateOf = dateOf;
        }
    }
}
