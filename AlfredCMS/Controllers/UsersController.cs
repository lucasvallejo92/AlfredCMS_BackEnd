using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlfredCMS.Core.Models;
using AlfredCMS.Core.Models.Data;
using AlfredCMS.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AlfredCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository<UserDTO> _repository;

        public UsersController(IUserRepository<UserDTO> repository)
        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            try
            {
                var users = await _repository.GetUsersAsync();
                return Ok(users);

            } catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [AllowAnonymous]
        [HttpGet("{slug}", Name = "GetUser")]
        public async Task<ActionResult<UserDTO>> GetPost(int id)
        {
            try
            {
                var user = await _repository.GetUserAsync(id);

                if (user == null)
                {
                    return NotFound(new { message = "NOT_FOUND" });
                }

                return Ok(user);
                
            } catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddPost([FromBody] UserDTO user)
        {
            try
            {
                var addedCategory = await _repository.AddUserAsync(user);

                if (!addedCategory)
                {
                    return BadRequest(new { message = "ALLREADY_EXISTS" });
                }

                return new CreatedAtRouteResult("GetUser", new { slug = user.Id }, user);
                
            } catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{slug}")]
        public async Task<ActionResult> UpdatePost(int id, [FromBody] UserDTO user)
        {
            try
            {
                if (user.Id != id)
                {
                    return BadRequest();
                }

                var isUpdated = await _repository.UpdateUserAsync(id, user);

                if (!isUpdated)
                {
                    return BadRequest(new { message = "NOT_EXISTS" });
                }

                return Ok();
                
            } catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{slug}")]
        public async Task<ActionResult> DeletePost(int id)
        {
            try
            {
                var response = await _repository.DeleteUserAsync(id);

                if (response == ResponseType.Response.Not_Found)
                {
                    return BadRequest(new { message = "NOT_FOUND" });
                }

                return Ok(new { message = "DELETED" });
                
            } catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}