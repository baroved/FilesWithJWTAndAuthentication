using ExtendLib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerCore.Infra
{
    public interface IFiles
    {
        Task<bool> AddFile(MyFile fileInfo);
        Task<List<MyFile>> GetAllFiles(int idUser);
        Task<MyFile> GetFileById(int id);


    }
}
