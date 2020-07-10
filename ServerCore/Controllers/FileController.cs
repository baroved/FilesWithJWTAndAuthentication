using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtendLib.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerCore.Infra;

namespace ServerCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFiles _filesService;
        public FileController(IFiles filesService)
        {
            _filesService = filesService;
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<bool>> Post([FromBody] MyFile fileInfo)
        {
            return await _filesService.AddFile(fileInfo);
            
        }

        [HttpGet("files/{IdUser}")]
        public async Task<ActionResult<List<MyFile>>> GetFiles(int IdUser)
        {
            return await _filesService.GetAllFiles(IdUser);
            
        }

        [HttpGet("Download/{Id}")]
        public async Task<ActionResult<MyFile>> GetFileById(int Id)
        {
            return await _filesService.GetFileById(Id);

        }


    }
}