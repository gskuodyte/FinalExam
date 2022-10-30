using HumanRegistrationSystem_Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanRegistrationSystem_BL
{
    public interface IJwtService
    {
        string GetJwtToken(UserAccount userAccount);
    }
}
