using ExtendLib.Model;
using ServerCore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerCore.Repository
{
    public class UserRipository
    {
        private readonly DBContext context;
        public UserRipository(DBContext _context)
        {
            context = _context;
        }
        public Task<bool> Login(UserAuthenticationInfo userInfo,out int id)
        {
            id = default(int);
            var selectedUser = context.Users.SingleOrDefault(
        u => u.Password == userInfo.Password
        && u.UserName == userInfo.UserName);

            if (selectedUser != null)
            {
                id = selectedUser.Id;
                return Task.FromResult(true);

            }
            
            return Task.FromResult(false);


        }
        public Task<UserAuthenticationInfo> GetUserByID(int id)
        {
            return Task.FromResult(context.Users.Where(a => a.Id == id).FirstOrDefault());
        }


        public Task<bool> Register(UserAuthenticationInfo userInfo)
        {

            var checkUserName = context.Users.Where(a => a.UserName == userInfo.UserName).FirstOrDefault();
            if (checkUserName != null)
                return Task.FromResult(false);
            else
                if (userInfo.UserName == "" || userInfo.Password == "" || userInfo.EmailAddress == "")
                return Task.FromResult(false);
            else
            {
                context.Users.Add(new UserAuthenticationInfo { UserName = userInfo.UserName, Password = userInfo.Password, EmailAddress = userInfo.EmailAddress });
                context.SaveChangesAsync();
                return Task.FromResult(true);
            }

        }


    }
}
