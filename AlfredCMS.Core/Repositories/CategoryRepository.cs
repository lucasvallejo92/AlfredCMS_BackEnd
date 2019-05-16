using AlfredCMS.Core.Models;
using AlfredCMS.Core.Repositories.Interfaces;
using AlfredCMS.Data;
using AlfredCMS.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AlfredCMS.Core.Models.Data;


namespace AlfredCMS.Core.Repositories
{
    public class CategoryRepository : IRepository<CategoryDTO>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetAsync(string identifier)
        {
            var category = await _context.Categories.Where(x => x.Slug == identifier).FirstOrDefaultAsync();
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<bool> AddAsync(CategoryDTO entity)
        {
            var allreadyExists = await _context.Categories.Where(x => x.Slug == entity.Slug).FirstOrDefaultAsync();

            if (allreadyExists != null)
            {
                return false;
            }

            _context.Categories.Add(_mapper.Map<Category>(entity));
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(string identifier, CategoryDTO entity)
        {
            var category = await _context.Categories.Where(x => x.Slug == identifier).FirstOrDefaultAsync();

            category = _mapper.Map(entity, category);

            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<ResponseType.Response> DeleteAsync(string identifier)
        {
            var category = await _context.Categories.Where(x => x.Slug == identifier).FirstOrDefaultAsync();

            if (category == null)
            {
                return ResponseType.Response.Not_Found;
            }

            var existsOnPost = await _context.Posts.Where(x => x.CategoryId == category.Id).FirstOrDefaultAsync();

            if (existsOnPost != null)
            {
                return ResponseType.Response.Cannot_Delete;
            }

            _context.Remove(category);
            await _context.SaveChangesAsync();

            return ResponseType.Response.Deleted;
        }
    }
}
