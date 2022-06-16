using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Examinations.Repository
{
    interface IExaminationEditRequestRepository
    {
        void SendExaminationEditRequest(int examinationId, DateTime currentTime, bool isEdit, int doctorId, DateTime newDateTime, int roomId);
    }
}
