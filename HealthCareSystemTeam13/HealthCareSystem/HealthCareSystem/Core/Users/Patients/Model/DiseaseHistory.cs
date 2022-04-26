using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Patients.Model
{
    class DiseaseHistory
    {
        public int IdMedicalRecord { get; set; }
        public String NameOfDisease { get; set; }
        
        public DiseaseHistory()
        {

        }
        public DiseaseHistory(int idMedicalRecord, String nameOfDisease)
        {
            this.IdMedicalRecord = idMedicalRecord;
            this.NameOfDisease = nameOfDisease;
        }
    }
}
