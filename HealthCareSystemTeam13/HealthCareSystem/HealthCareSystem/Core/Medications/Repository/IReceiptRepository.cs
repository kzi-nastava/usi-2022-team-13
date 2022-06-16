using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Medications.Repository
{
    interface IReceiptRepository
    {
        void InsertReceipt(int doctorId, int patientId, DateTime dateOf);
        int GetLastReceiptId();
    }
}
