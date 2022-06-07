using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Rooms.HospitalEquipment.RoomHasEquipment.Model
{
    public class RoomHasEquipment
    {
        public int Id { get; set; }
        public int EquipmentId { get; set; }
        public int RoomId { get; set; }
        public int Quantity { get; set; }

        public RoomHasEquipment(int equipmentId, int roomId, int quantity)
        {
            this.EquipmentId = equipmentId;
            this.RoomId = roomId;
            this.Quantity = quantity;
        }
        public RoomHasEquipment(int id, int equipmentId, int roomId, int quantity)
        {
            this.Id = id;
            this.EquipmentId = equipmentId;
            this.RoomId = roomId;
            this.Quantity = quantity;
        }
    }
}
