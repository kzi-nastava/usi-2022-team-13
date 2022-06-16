using HealthCareSystem.Core.Surveys.HospitalSurveys.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Surveys
{
    interface ISurveyRepository
    {
        void PullHospitalSurveys();
        void PullDoctorSurveys();
        List<HospitalSurvey> GetHospitalSurveys();
        List<DoctorSurvey> GetDoctorSurveys();
        void AddDoctorSurvey(int doctorId, int patientId, int rating, int quality, bool wouldReccomend, string comment);
        void AddHospitalSurvey(HospitalSurvey survey, int patientId);
        DataTable GetDoctorsSurveys();
        DataTable GetHospitalsSurveys();
    }
}
