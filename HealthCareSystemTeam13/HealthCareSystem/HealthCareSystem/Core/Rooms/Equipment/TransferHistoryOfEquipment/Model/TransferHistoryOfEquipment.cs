using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Rooms.Equipment.TransferHistoryOfEquipment.Model
{
    class TransferHistoryOfEquipment
    {
        public int FirstRoomId { get; set; }
        public int SecondRoomId { get; set; }

        public DateTime TransferDate { get; set; }

        public TransferHistoryOfEquipment(int firstRoomId, int secondRoomId, DateTime transferDate)
        {
            this.FirstRoomId = firstRoomId;
            this.SecondRoomId = secondRoomId;
            this.TransferDate = transferDate;
        }
    }
}
