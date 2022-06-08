using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Model
{
    public enum UserRole
    {
        HospitalManagers, Patients, Doctors, Secretaries
    }
    class User
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public UserRole Role { get; set; }

       
        public User(string username, string password, UserRole role)
        {
            this.Username = username;
            this.Password = password;
            this.Role = role;
            
        }

    }
}
