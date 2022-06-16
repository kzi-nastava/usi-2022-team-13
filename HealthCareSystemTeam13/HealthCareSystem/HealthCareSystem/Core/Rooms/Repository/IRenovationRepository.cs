using HealthCareSystem.Core.Rooms.Renovations.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Rooms.Repository
{
    interface IRenovationRepository
    {
        void PullRenovations();
        void InsertRenovation(Renovation renovation);

        List<Renovation> GetRenovations(string query);

        DataTable GetRenovationsDataTable();
    }
}
