using HealthCareSystem.Core.Examinations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Examinations.Controller
{
    class ExaminationSorter:IExaminationSorter
    {
        public List<DoctorAnamnesis> SortAnamnesises(List<DoctorAnamnesis> anamnesises, int indicator = 0)
        {
            // indicator = 0 -> sort by date, = 1 -> sort by doctor, = 2 -> sort by speciality
            if (indicator == 0) return SortByDate(anamnesises);
            if (indicator == 1) return SortByDoctor(anamnesises);
            return SortBySpeciality(anamnesises);

        }

        public List<DoctorAnamnesis> SortBySpeciality(List<DoctorAnamnesis> anamnesises)
        {
            for (int i = 0; i < anamnesises.Count() - 1; i++)
            {
                for (int j = 0; j < anamnesises.Count() - i - 1; j++)
                {
                    if (anamnesises[j].DoctorSpeciality.CompareTo(anamnesises[j + 1].DoctorSpeciality) > 0)
                    {
                        Swap(anamnesises, j);

                    }
                }
            }
            return anamnesises;
        }

        public List<DoctorAnamnesis> SortByDoctor(List<DoctorAnamnesis> anamnesises)
        {
            for (int i = 0; i < anamnesises.Count() - 1; i++)
            {
                for (int j = 0; j < anamnesises.Count() - i - 1; j++)
                {
                    if (anamnesises[j].DoctorName.CompareTo(anamnesises[j + 1].DoctorName) > 0)
                    {
                        Swap(anamnesises, j);

                    }
                }
            }
            return anamnesises;
        }

        public List<DoctorAnamnesis> SortByDate(List<DoctorAnamnesis> anamnesises)
        {
            for (int i = 0; i < anamnesises.Count() - 1; i++)
            {
                for (int j = 0; j < anamnesises.Count() - i - 1; j++)
                {
                    if (anamnesises[j].DateOfExamination > anamnesises[j + 1].DateOfExamination)
                    {
                        Swap(anamnesises, j);

                    }
                }
            }
            return anamnesises;

        }

        public void Swap(List<DoctorAnamnesis> anamnesises, int j)
        {
            DoctorAnamnesis temp = anamnesises[j];
            anamnesises[j] = anamnesises[j + 1];
            anamnesises[j + 1] = temp;
        }
    }
}


