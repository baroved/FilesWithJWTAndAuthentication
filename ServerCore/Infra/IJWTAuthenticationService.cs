using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerCore.Infra
{

    public interface IJWTAuthenticationService
    {
        Task<string> GenerateToken(string userId);
    }
}

