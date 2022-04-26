using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Rooms.Equipment.Model
{
    class Equipment
    {
        public enum EquipmentType
        {
            Static, Dynamic
        }

        public String Name { get; set; }

        public EquipmentType Type { get; set; }

        public Equipment(string name, EquipmentType type)
        {
            this.Name = name;
            this.Type = type;
        }
    }
}
