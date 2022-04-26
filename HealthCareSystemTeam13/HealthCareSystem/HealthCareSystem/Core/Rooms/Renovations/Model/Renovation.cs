using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Rooms.Renovations.Model
{
    class Renovation
    {
        public int RoomId { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }

        public int SecondRoomId { get; set; }

        public Renovation(int roomId, DateTime startingDate, DateTime endingDate)
        {
            this.RoomId = roomId;
            this.StartingDate = startingDate;
            this.EndingDate = endingDate;
        }

        public Renovation(int roomId, DateTime startingDate, DateTime endingDate, int secondRoomId)
        {
            this.RoomId = roomId;
            this.StartingDate = startingDate;
            this.EndingDate = endingDate;
            this.SecondRoomId = secondRoomId;
        }
    }
}
