using ExtendLib.Model;
using ServerCore.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerCore.Repository
{
    public class FileRepository
    {
        private readonly DBContext context;
        public FileRepository(DBContext _context)
        {
            context = _context;
        }
        public Task<bool> AddFile(MyFile newFile)
        {
            if (newFile != null)
            {
                context.Files.Add(newFile);
                context.SaveChanges();
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<List<MyFile>> GetAllFiles(int idUser)
        {
            return Task.FromResult(context.Files.Where(id => id.User.Id == idUser)
                .ToList());
            
        }

        public  Task<MyFile> GetFileById(int id)
        {
            return Task.FromResult(context.Files.Where(a => a.Id == id).FirstOrDefault());
        }
            
    }
}
