using ExtendLib.Model;
using ServerCore.Infra;
using ServerCore.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace ServerCore.Services
{
    public class Files : IFiles
    {
        FileRepository _fileRepo;
        public Files(FileRepository fileRepo)
        {
            _fileRepo = fileRepo;
        }
        public async Task<bool> AddFile(MyFile fileInfo)
        {
          return await _fileRepo.AddFile(fileInfo);
        }

        public async Task<List<MyFile>> GetAllFiles(int idUser)
        {
             return await  _fileRepo.GetAllFiles(idUser);

        }
        public async Task<MyFile> GetFileById(int id)
        {
            return await _fileRepo.GetFileById(id);

        }
    }
}
