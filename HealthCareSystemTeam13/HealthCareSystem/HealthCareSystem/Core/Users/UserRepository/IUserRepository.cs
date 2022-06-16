using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthCareSystem.Core.Users.Model;

namespace HealthCareSystem.Core.Users
{
    interface IUserRepository
    {
        void InsertSingleUser(User user);
        List<string> GetUserId(string username);
    }
}
