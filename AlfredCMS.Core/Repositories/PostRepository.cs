using AlfredCMS.Core.Models;
using AlfredCMS.Core.Models.Data;
using AlfredCMS.Core.Repositories.Interfaces;
using AlfredCMS.Data;
using AlfredCMS.Data.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlfredCMS.Core.Repositories
{
    public class PostRepository: IRepository<PostDTO>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PostRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PostDTO>> GetAllAsync()
        {
            var posts = await _context.Posts.Include(x => x.Category).Include(x => x.User).ToListAsync();
            return _mapper.Map<IEnumerable<PostDTO>>(posts);
        }

        public async Task<PostDTO> GetAsync(string identifier)
        {
            var post = await _context.Posts.Where(x => x.Slug == identifier).FirstOrDefaultAsync();
            return _mapper.Map<PostDTO>(post);
        }

        public async Task<bool> AddAsync(PostDTO entity)
        {
            var allreadyExists = await _context.Posts.Where(x => x.Slug == entity.Slug).FirstOrDefaultAsync();

            if (allreadyExists != null)
            {
                return false;
            }

            _context.Posts.Add(_mapper.Map<Posts>(entity));
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAsync(string identifier, PostDTO entity)
        {
            var post = await _context.Posts.Where(x => x.Slug == identifier).FirstOrDefaultAsync();

            post = _mapper.Map(entity, post);

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
            var post = await _context.Posts.Where(x => x.Slug == identifier).FirstOrDefaultAsync();

            if (post == null)
            {
                return ResponseType.Response.Not_Found;
            }

            _context.Remove(post);
            await _context.SaveChangesAsync();

            return ResponseType.Response.Deleted;
        }
    }
}
