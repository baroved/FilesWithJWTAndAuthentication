using ClientLib.Infra;
using ExtendLib.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClientLib.Services
{

    public class FilesService : IFilesService
    {
        private readonly IHttpService _httpService;
        public FilesService(IHttpService httpService)
        {
            _httpService = httpService;
        }

        public async Task AddFile(MyFile newFile,string token)
        {
            await _httpService.PostAsync<MyFile, string>("/file", newFile,token);
        }


        public async Task<List<MyFile>> GetAllFiles(int idUser)
        {
            return await _httpService.GetAsync<List<MyFile>>($"/file/files/{idUser}");
        }
        public async Task<MyFile> GetFileById(int id)
        {
            return await _httpService.GetAsync<MyFile>($"/file/Download/{id}");
        }
    }
}
