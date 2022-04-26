using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Patients.Model
{
    class DiseaseHistory
    {
        public int MedicalRecordId { get; set; }
        public string Name { get; set; }
        public DiseaseHistory(int recordId, string name)
        {
            MedicalRecordId = recordId;
            Name = name;
        }

    }
}
