using ClientLib.Infra;
using ExtendLib.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientLib.Services
{
    
        public class UserService : IUserService
        {
            private readonly IHttpService _httpService;
            public UserService(IHttpService httpService)
            {
                _httpService = httpService;
            }

        public async Task<UserAuthenticationInfo> GetUserByID(int id)
        {
            return await _httpService.GetAsync<UserAuthenticationInfo>($"/user/getuser/{id}");
            
        }

        public async Task<UserAuthenticationInfo> Login(UserAuthenticationInfo authInfo)
            {
                return await _httpService.PostAsync<UserAuthenticationInfo, UserAuthenticationInfo>("/user/Login", authInfo);
                
            }

        public async Task<bool> Register(UserAuthenticationInfo authInfo)
        {
            return await _httpService.PostAsync<UserAuthenticationInfo, bool>("/user", authInfo);
            
        }
    }
} 

