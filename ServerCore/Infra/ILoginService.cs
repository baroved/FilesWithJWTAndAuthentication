using ExtendLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerCore.Infra
{
    public interface ILoginService
    {
        Task<UserAuthenticationInfo> Login(UserAuthenticationInfo userInfo);
        Task<bool> Register(UserAuthenticationInfo userInfo);
        Task<UserAuthenticationInfo> GetUserByID(int id);
    }
}
