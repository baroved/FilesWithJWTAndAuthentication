using ExtendLib.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientLib.Infra
{
    public interface IUserService
    {
        Task<UserAuthenticationInfo> Login(UserAuthenticationInfo authInfo);
        Task<bool> Register(UserAuthenticationInfo authInfo);

        Task<UserAuthenticationInfo> GetUserByID(int id);
    }
}
