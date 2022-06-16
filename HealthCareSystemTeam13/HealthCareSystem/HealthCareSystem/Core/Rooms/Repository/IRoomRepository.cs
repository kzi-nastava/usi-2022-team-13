using HealthCareSystem.Core.Examinations.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.RoomHasEquipment.Model;
using HealthCareSystem.Core.Rooms.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Rooms.Repository
{
    interface IRoomRepository
    {
        List<Equipment> GetEquipmentFromRoomId(int roomId);

        void UpdateAmountOfEquipmentInTheRoom(int amount, int roomId, int equipmentId);

        int GetAvailableRoom(DateTime dateTime, int duration);

        bool IsRoomAvailable(string roomId, DateTime dateTime, int duration);

        int GetWarehouseId();

        void PullRooms();

        void RemoveRoom(int roomId);

        Room GetSelectedRoom(string query);

        bool IsRoomAvailable(int roomId, DateTime examinationTime, List<Examination> examinations);

        List<Room> GetRooms();

        List<Room> GetRooms(string query);

        Room GetRoom(int id);

        bool DoesRoomHaveFutureExaminations(Room room);

        int GetAvailableRoomId(DateTime examinationDateTime, List<Examination> examinations);

        void SetRoomsToAvailable(Dictionary<int, bool> availableRooms, List<Room> rooms);

        void EliminateUnavailableRooms(DateTime examinationDateTime, List<Examination> examinations, Dictionary<int, bool> availableRooms, List<Room> rooms);

        List<string> GetOperationRooms();

        List<string> GetExaminationRooms();

        void PullFoundRows(string search, string amount, string roomType, string equipmentType);

        List<RoomHasEquipment> GetEquipmentInRoom(string query);

        DataTable GetEquipmentDataTable();

        DataTable GetRoomsDataTable();

        void InsertRoom(TypeOfRoom roomType);


    }
}
