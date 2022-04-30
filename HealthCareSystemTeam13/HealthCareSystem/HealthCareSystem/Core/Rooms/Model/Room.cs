using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Rooms.Model
{
    public enum TypeOfRoom
    {
        Warehouse, OperationRoom, ExaminationRoom, DayRoom, DeliveryRoom, IntensiveCareUnit, NurseryRoom 
    }
    class Room
    {
        public int ID;
        public TypeOfRoom Type { get; set; }
        public Room(TypeOfRoom type, int id)
        {
            ID = id;
            this.Type = type;
        }

        public Room(TypeOfRoom type)
        {
            ID = 0;
<<<<<<< HEAD
=======
            this.Type = type;
        }
        public Room(TypeOfRoom type, int id)
        {
            ID = id;
>>>>>>> feature/Doctor
            this.Type = type;
        }

        public Room()
        {
        }
    }
}
