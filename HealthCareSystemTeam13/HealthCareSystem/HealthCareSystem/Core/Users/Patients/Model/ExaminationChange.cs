using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Patients.Model
{
    public enum TypeOfChange
    {
        Add, Edit, Delete
    }
    class ExaminationChange
    {
        public int PatientId { get; set; }
        public TypeOfChange Change { get; set; }
        public DateTime DateOfChange { get; set; }

        public ExaminationChange(int patientId, TypeOfChange change, DateTime date)
        {
            PatientId = patientId;
            Change = change;
            DateOfChange = date;
        }
    }
}
