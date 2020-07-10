using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExtendLib.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerCore.Infra;

namespace ServerCore.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IFiles _filesService;
        private readonly ILoginService _userService;
        public UserController(ILoginService userService, IFiles filesService)
        {
            _userService = userService;
            _filesService = filesService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("Login")]
        public async Task<ActionResult<UserAuthenticationInfo>> Post([FromBody] UserAuthenticationInfo authInfo)
        {
            var user = await _userService.Login(authInfo);
            return user;
        }

        [HttpPost()]
        public async Task<ActionResult<bool>> RegisterPost([FromBody] UserAuthenticationInfo authInfo)
        {
            var check = await _userService.Register(authInfo);
            return check;
        }

        [HttpGet("getuser/{id}")]
        public async Task<ActionResult<UserAuthenticationInfo>> GetUserByID(int id)
        {
            var check = await _userService.GetUserByID(id);
            return check;
        }



        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}