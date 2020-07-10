using ExtendLib.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientLib.Infra
{
    public interface IFilesService
    {
        Task<List<MyFile>> GetAllFiles(int idUser);
        Task AddFile(MyFile data,string token);
        Task<MyFile> GetFileById(int id);
    }
}
