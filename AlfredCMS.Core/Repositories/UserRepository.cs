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
    public class UserRepository : IUserRepository<UserDTO>
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserAsync(int id)
        {
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> AddUserAsync(UserDTO entity)
        {
            var allreadyExists = await _context.Users.Where(x => x.Id == entity.Id).FirstOrDefaultAsync();

            if (allreadyExists != null)
            {
                return false;
            }

            _context.Users.Add(_mapper.Map<User>(entity));
            await _context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> UpdateUserAsync(int id, UserDTO entity)
        {
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

            user = _mapper.Map(entity, user);

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

        public async Task<ResponseType.Response> DeleteUserAsync(int id)
        {
            var user = await _context.Users.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (user == null)
            {
                return ResponseType.Response.Not_Found;
            }

            _context.Remove(user);
            await _context.SaveChangesAsync();

            return ResponseType.Response.Deleted;
        }
    }
}
