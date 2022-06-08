using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Rooms.HospitalEquipment.TransferHistoryOfEquipment.Model
{
    class TransferHistoryOfEquipment
    {
        public int FirstRoomId { get; set; }
        public int SecondRoomId { get; set; }

        public DateTime TransferDate { get; set; }

        public bool IsExecuted { get; set; }

        public int Amount { get; set; }
        public int EquipmentId { get; set; }
        public TransferHistoryOfEquipment(int firstRoomId, int secondRoomId, DateTime transferDate, bool isExecuted, int amount, int equipmentId)
        {
            this.FirstRoomId = firstRoomId;
            this.SecondRoomId = secondRoomId;
            this.TransferDate = transferDate;
            this.IsExecuted = isExecuted;
            this.Amount = amount;
            this.EquipmentId = equipmentId;
        }
    }
}
