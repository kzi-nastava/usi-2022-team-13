using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.HospitalManagers
{
    class HospitalManager
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int UserId { get; set; }

        public HospitalManager()
        {

        }
        public HospitalManager(string firstName, string lastName, int userId)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.UserId = userId;
        }
    }
}
