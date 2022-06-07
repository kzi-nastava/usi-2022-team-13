using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Doctors.Model
{
    enum StateOfRequest
    {
        Waiting, Accepted, Declined
    }
    class DaysOffRequest
    {
        public int Id { get; private set; }
        public int DoctorId { get; private set; }
        public DateTime DateFrom { get; private set; }
        public DateTime DateTo { get; private set; }
        public string ReasonOf { get; private set; }
        public bool IsUrgent { get; private set; }
        public StateOfRequest StateOfTheRequest { get; private set; }

        public DaysOffRequest(int id, int doctorId, DateTime dateFrom, DateTime dateTo,
            string reasonOf, bool isUrgent, StateOfRequest stateOfTheRequest)
        {
            Id = id;
            DoctorId = doctorId;
            DateFrom = dateFrom;
            DateTo = dateTo;
            ReasonOf = reasonOf;
            IsUrgent = isUrgent;
            StateOfTheRequest = stateOfTheRequest;
        }
    }
}
