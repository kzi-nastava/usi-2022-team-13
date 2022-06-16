using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCareSystem.Core.Users.Secretaries
{
    interface ISecretaryRepository
    {
        List<string> GetSecretaryId(string userId);
    }
}
