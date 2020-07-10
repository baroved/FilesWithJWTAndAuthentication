using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExtendLib.Model
{
    public class UserAuthenticationInfo
    {
        [Key]
        public int Id { get; set; }
        
        public string UserName { get; set; }
        
        public string Password { get; set; }
        
        public string EmailAddress { get; set; }

        public string UserToken { get; set; }


    }
}
