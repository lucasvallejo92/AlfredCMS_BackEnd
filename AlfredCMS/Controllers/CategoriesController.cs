using System.Collections.Generic;
using System.Threading.Tasks;
using AlfredCMS.Core.Models;
using AlfredCMS.Core.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AlfredCMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepository<CategoryDTO> _repository;

        public CategoriesController(IRepository<CategoryDTO> repository)

        {
            _repository = repository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _repository.GetAll();
            return Ok(categories);
        }

        [AllowAnonymous]
        [HttpGet("{slug}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> GetCategory(string slug)
        {
            var category = await _repository.Get(slug);

            if (category == null)
            {
                return NotFound(slug);
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> AddCategory([FromBody] CategoryDTO category)
        {
            var addedCategory = await _repository.Add(category);

            if (!addedCategory)
            {
                return BadRequest(new { message = "ALLREADY_EXISTS" });
            }

            return new CreatedAtRouteResult("GetCategory", new { slug = category.Slug }, category);
        }

        [HttpPut("{slug}")]
        public async Task<ActionResult> UpdateCategory(string slug, [FromBody] CategoryDTO category)
        {
            if (category.Slug != slug)
            {
                return BadRequest();
            }

            var isUpdated = await _repository.Update(slug, category);

            if (!isUpdated)
            {
                return BadRequest(new { message = "NOT_EXISTS" });
            }

            return Ok();
        }

        [HttpDelete("{slug}")]
        public async Task<ActionResult> DeleteCategory(string slug)
        {
            var response = await _repository.Delete(slug);

            if (response == "NOT_FOUND")
            {
                return BadRequest(new { message = "NOT_FOUND" });
            }
            else if (response == "CANNOT_DELETE")
            {
                return BadRequest(new { message = "CANNOT_DELETE" });
            }

            return Ok(new { message = "DELETED" });
        }
    }
}