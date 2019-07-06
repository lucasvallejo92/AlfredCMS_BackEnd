using System;
using System.Threading.Tasks;
using AlfredCMS.Core.Models.Auth;
using AlfredCMS.Core.Models.Data;
using AlfredCMS.Core.Repositories.Interfaces.Auth;
using AlfredCMS.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace AlfredCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        
        private readonly IAuthInterface<UserData> _repository;
        public AuthController(IAuthInterface<UserData> repository)
        {
            _repository = repository;
        }

        [HttpPost("signin")]
        public async Task<ActionResult> GetToken([FromBody] UserData credentials)
        {
            try
            {
                var user = await _repository.AuthorizeAsync(credentials);
                
                if (user == ResponseType.Response.Not_Found)
                {
                    return Unauthorized();
                }
                
                var token = TokenCreator.CreateToken();
                return Ok(token);
                
            } catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}