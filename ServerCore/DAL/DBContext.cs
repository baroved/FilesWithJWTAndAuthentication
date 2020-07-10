using ExtendLib.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerCore.DAL
{
    public class DBContext:DbContext
    {
        public DBContext(DbContextOptions options):base(options)
        {

        }

        public DbSet<UserAuthenticationInfo> Users { get; set; }
        public DbSet<MyFile> Files { get; set; }
    }
}
