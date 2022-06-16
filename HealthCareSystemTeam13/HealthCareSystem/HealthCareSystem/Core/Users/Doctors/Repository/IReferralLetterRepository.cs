using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Users.Doctors.Model;

namespace HealthCareSystem.Core.Users.Doctors.Repository
{
    interface IReferralLetterRepository
    {
        void InsertReferral(ReferralLetter referralLetter, int option);
        void PullReferralLetters();
        void DeleteSingleReferralLetter(string letterID);
        DataTable GetReferalLetters();
    }
}
