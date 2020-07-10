using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ExtendLib.Model
{
    public class MyFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Source { get; set; }
        public int UserId { get; set; }
        public UserAuthenticationInfo User { get; set; }
    }
}
