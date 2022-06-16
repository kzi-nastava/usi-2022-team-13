using HealthCareSystem.Core.Users.Patients.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Examinations.Repository
{
    interface IExaminationChangesRepository
    {
        List<ExaminationChange> GetExaminationChanges(int patientId);
    }
}
