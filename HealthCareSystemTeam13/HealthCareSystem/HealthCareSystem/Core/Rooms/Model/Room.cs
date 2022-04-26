using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Rooms.Model
{
    public enum TypeOfRoom
    {
        Warehouse, OperationRoom, ExaminationRoom, DayRoom, DeliveryRoom, IntensiveCareUnit, NurseryRoom, 
    }
    class Room
    {
        public TypeOfRoom Type { get; set; }
        public Room(TypeOfRoom type)
        {
            this.Type = type;
        }
    }
}
