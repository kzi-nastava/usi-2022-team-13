using HealthCareSystem.Core.Rooms.DynamicEqipmentRequests.Model;
using HealthCareSystem.Core.Rooms.HospitalEquipment.RoomHasEquipment.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Rooms.Repository
{
    interface IEquipmentRepository
    {
        void PullEquipmentInWarehouse();
        void PullDynamicEquipment();
        void PullTransferDynamicEquipment(int equipmentId);
        
        void InsertSingleDynamicEquipmentRequest(DynamicEquipmentRequest request);

        void UpdateSigleDynamicEquipment(DynamicEquipmentRequest request);

        void UpdateSigleDynamicEquipment(int amount, int roomHasEquipmentID);

        void UpdateSigleDynamicEquipment(int amount, RoomHasEquipment roomHasEquipment);

        void DeleteSingleDynamicEquipmentRequest(int requestID);

        List<DynamicEquipmentRequest> GetDeliveredDynamicEquipmentRequest();

        void CheckDynamicEquipmentRequests();

        void PullEquipment();

        DataTable GetEquipmentDataTable();

        DataTable GetEquipmentInWarehouseDataTable();

        DataTable GetDynamicEquipmentDataTable();

        DataTable GetTransferDynamicEquipmentDataTable();

    }
}
