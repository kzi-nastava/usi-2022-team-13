using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Surveys
{
    class DoctorSurvey
    {
        public int Grade { get; set; }
        public int Quality { get; set; }
        public bool WouldRecommend { get; set; }
        public string Comment { get; set; }

        public DoctorSurvey(int grade, int quality, bool wouldRecommend, string comment)
        {
            this.Grade = grade;
            this.Quality = quality;
            this.WouldRecommend = wouldRecommend;
            this.Comment = comment;
        }
        public DoctorSurvey()
        {

        }
    }
}
