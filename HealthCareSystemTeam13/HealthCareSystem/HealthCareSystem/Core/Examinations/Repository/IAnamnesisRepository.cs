using HealthCareSystem.Core.Examinations.Model;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Examinations.Repository
{
    interface IAnamnesisRepository
    {
        List<DoctorAnamnesis> GetAnamnesises(List<Examination> examinations);
        List<DoctorAnamnesis> GetAnamnesisesByKeyword(List<DoctorAnamnesis> anamnesises, string keyword);
        bool IsKeywordInAnamnesis(string keyword, DoctorAnamnesis anamnesis);
        DoctorAnamnesis GetDoctorAnamnesis(int examinationId);
        DoctorAnamnesis SetDoctorAnamnesisValues(OleDbDataReader reader);
        Anamnesis GetAnamnesis(int examinationId);
        Anamnesis SetAnamnesisValues(OleDbDataReader reader);
    }
}