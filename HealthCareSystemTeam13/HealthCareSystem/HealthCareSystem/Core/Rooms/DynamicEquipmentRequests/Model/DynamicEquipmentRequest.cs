using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Rooms.DynamicEqipmentRequests.Model
{
    class DynamicEquipmentRequest
    {
        public int ID { get; set; }
        public int EquipmentId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public int SecretaryId { get; set; }
        public DynamicEquipmentRequest(int equipmentId, int quantity, DateTime date, int seceratryId)
        {
            this.EquipmentId = equipmentId;
            this.Quantity = quantity;
            this.Date = date;
            this.SecretaryId = seceratryId;
        }
        public DynamicEquipmentRequest(int equipmentId, int quantity, DateTime date, int seceratryId, int id)
        {
            this.EquipmentId = equipmentId;
            this.Quantity = quantity;
            this.Date = date;
            this.SecretaryId = seceratryId;
            this.ID = id;
        }
    }
}
