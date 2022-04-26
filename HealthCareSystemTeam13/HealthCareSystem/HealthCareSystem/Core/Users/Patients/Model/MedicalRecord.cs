using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Patients.Model
{
    class MedicalRecord
    {
        public int Weight { get; set; }
        public int Height { get; set; }
        public int IdPatient { get; set; }

        public MedicalRecord()
        {

        }
        public MedicalRecord(int idPatient, int weight, int height)
        {
            this.IdPatient = idPatient;
            this.Weight = weight;
            this.Height = height;
        }
    }
}
