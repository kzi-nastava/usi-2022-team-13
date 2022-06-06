using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Surveys.HospitalSurveys.Model
{
    class HospitalSurvey
    {
        public int QualityOfService { get; set; }
        public int Cleanliness { get; set; }
        public int Happiness { get; set; }
        public int WouldRecommend { get; set; }
        public string Comment { get; set; }

        public HospitalSurvey(int qualityOfService, int cleanliness, int happiness, int wouldRecommend, string comment )
        {
            this.QualityOfService = qualityOfService;
            this.Cleanliness = cleanliness;
            this.Happiness = happiness;
            this.WouldRecommend = wouldRecommend;
            this.Comment = comment;
        }
        public HospitalSurvey()
        {

        }
    }
}
