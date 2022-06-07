using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Rooms.HospitalEquipment.Model
{
    class Equipment
    {
        public enum EquipmentType
        {
            Static, Dynamic
        }

        public String Name { get; set; }

        public int Amount { get; set; }
        public string NameAndAmount
        {
            get
            {
                return Name + " | " + Amount + " left in the room";
            }
        }

        public int ID { get; set; }
        public EquipmentType Type { get; set; }

        public Equipment(string name, EquipmentType type)
        {
            ID = 0;
            this.Name = name;
            this.Type = type;
        }
        public Equipment(int id, string name, EquipmentType type)
        {
            this.ID = id;
            this.Name = name;
            this.Type = type;
        }

        public Equipment(string name, int id, int amount)
        {
            Name = name;
            ID = id;
            Amount = amount;
        }

        public override string ToString()
        {
            return Name + " (Equipment type: " + Type.ToString() + ")";
        }
    }
}
