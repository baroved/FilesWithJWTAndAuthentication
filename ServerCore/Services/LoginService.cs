using ExtendLib.Model;
using ServerCore.DAL;
using ServerCore.Infra;
using ServerCore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerCore.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserRipository _userRipository;
        private readonly IJWTAuthenticationService _jwtService;
        public LoginService(IJWTAuthenticationService jwtService, UserRipository userRipository)
        {
            _userRipository = userRipository;
            _jwtService = jwtService;
        }

        public async Task<UserAuthenticationInfo> GetUserByID(int id)
        {
            return await _userRipository.GetUserByID(id);
            
        }

        public async Task<UserAuthenticationInfo> Login(UserAuthenticationInfo userInfo)
        {
            int idtemp;
            if ( await _userRipository.Login(userInfo, out idtemp))
            {
                userInfo.Id = idtemp;
                userInfo.UserToken = await _jwtService.GenerateToken(userInfo.UserName);
            }
            else
                userInfo.UserToken = string.Empty;

            return userInfo;
        }


        public async Task<bool> Register(UserAuthenticationInfo userInfo)
        {
            if ( await _userRipository.Register(userInfo))
                return true;
            return false;

        }
    }
}

