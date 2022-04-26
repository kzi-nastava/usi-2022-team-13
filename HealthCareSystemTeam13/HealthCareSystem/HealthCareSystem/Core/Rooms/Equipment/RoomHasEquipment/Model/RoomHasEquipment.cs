using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Rooms.Equipment.RoomHasEquipment.Model
{
    class RoomHasEquipment
    {
        public int EquipmentId { get; set; }
        public int RoomId { get; set; }
        public int Quantity { get; set; }

        public RoomHasEquipment(int equipmentId, int roomId, int quantity)
        {
            this.EquipmentId = equipmentId;
            this.RoomId = roomId;
            this.Quantity = quantity;
        }
    }
}
