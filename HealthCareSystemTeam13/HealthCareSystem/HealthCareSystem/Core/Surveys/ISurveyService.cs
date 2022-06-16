using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Surveys.HospitalSurveys.Model;
using HealthCareSystem.Core.Users.Doctors.Model;

namespace HealthCareSystem.Core.Surveys
{
    interface ISurveyService
    {
        void SetNumberOfGradesForQuality(List<DoctorSurvey> doctorSurveys, int doctorId, int[] numberOfGrades);
        void SetNumberOfGradesForGrade(List<DoctorSurvey> doctorSurveys, int doctorId, int[] numberOfGrades);
        int[] GetNumberOfDoctorGrades(List<DoctorSurvey> doctorSurveys, bool isDoctorGrade, int doctorId);
        double[] GetAverageDoctorGrades(List<DoctorSurvey> doctorSurveys, int doctorId);
        Dictionary<Doctor, double> GetDoctorsAndAverages(List<DoctorSurvey> doctorSurveys, BindingList<Doctor> doctors);
        double SumAllHospitalGrades(List<HospitalSurvey> hospitalSurveys, bool isHygene);
        int[] GetNumberOfHospitalGrades(List<HospitalSurvey> hospitalSurveys, bool isHygene);
    }
}
