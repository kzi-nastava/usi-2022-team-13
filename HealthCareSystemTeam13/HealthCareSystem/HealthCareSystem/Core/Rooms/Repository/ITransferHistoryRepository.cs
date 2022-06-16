using HealthCareSystem.Core.Rooms.HospitalEquipment.TransferHistoryOfEquipment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Rooms.Repository
{
    interface ITransferHistoryRepository
    {
        void InsertTransferHistoryOfEquipment(TransferHistoryOfEquipment transferHistoryOfEquipment);

        List<TransferHistoryOfEquipment> GetTransferHistory(string query);
    }
}
