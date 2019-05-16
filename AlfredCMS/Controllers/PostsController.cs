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
    public class PostsController : ControllerBase
    {
        private readonly IRepository<PostDTO> _repository;

        public PostsController(IRepository<PostDTO> repository)

        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDTO>>> GetPosts()
        {
            var posts = await _repository.GetAllAsync();
            return Ok(posts);
        }

        [AllowAnonymous]
        [HttpGet("{slug}", Name = "GetPost")]
        public async Task<ActionResult<PostDTO>> GetPost(string slug)
        {
            var post = await _repository.GetAsync(slug);

            if (post == null)
            {
                return NotFound(slug);
            }

            return Ok(post);
        }

        [HttpPost]
        public async Task<ActionResult<PostDTO>> AddPost([FromBody] PostDTO post)
        {
            var addedCategory = await _repository.AddAsync(post);

            if (!addedCategory)
            {
                return BadRequest(new { message = "ALLREADY_EXISTS" });
            }

            return new CreatedAtRouteResult("GetPost", new { slug = post.Slug }, post);
        }

        [HttpPut("{slug}")]
        public async Task<ActionResult> UpdatePost(string slug, [FromBody] PostDTO post)
        {
            if (post.Slug != slug)
            {
                return BadRequest();
            }

            var isUpdated = await _repository.UpdateAsync(slug, post);

            if (!isUpdated)
            {
                return BadRequest(new { message = "NOT_EXISTS" });
            }

            return Ok();
        }

        [HttpDelete("{slug}")]
        public async Task<ActionResult> DeletePost(string slug)
        {
            var response = await _repository.DeleteAsync(slug);

            if (response == ResponseType.Response.Not_Found)
            {
                return BadRequest(new { message = "NOT_FOUND" });
            }

            return Ok(new { message = "DELETED" });
        }
    }
}