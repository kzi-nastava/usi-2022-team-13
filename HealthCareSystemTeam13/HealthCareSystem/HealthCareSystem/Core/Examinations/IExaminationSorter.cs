using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Examinations.Model;

namespace HealthCareSystem.Core.Examinations
{
    interface IExaminationSorter
    {
        List<DoctorAnamnesis> SortAnamnesises(List<DoctorAnamnesis> anamnesises, int indicator = 0);
        List<DoctorAnamnesis> SortBySpeciality(List<DoctorAnamnesis> anamnesises);
        List<DoctorAnamnesis> SortByDoctor(List<DoctorAnamnesis> anamnesises);
        List<DoctorAnamnesis> SortByDate(List<DoctorAnamnesis> anamnesises);
        void Swap(List<DoctorAnamnesis> anamnesises, int j);

    }
}
