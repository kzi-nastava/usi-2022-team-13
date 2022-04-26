using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Rooms.DynamicEqipmentRequests.Model
{
    class DynamicEquipmentRequest
    {
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }

        public DynamicEquipmentRequest(int equipmentId, int quantity)
        {
            this.EquipmentId = equipmentId;
            this.Quantity = quantity;
        }

    }
}
