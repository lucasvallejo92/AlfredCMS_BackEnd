using AlfredCMS.Core.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlfredCMS.Core.Repositories.Interfaces
{
    public interface IUserRepository<T> where T: class
    {
        Task<IEnumerable<T>> GetUsersAsync();
        Task<T> GetUserAsync(int id);
        Task<bool> AddUserAsync(T entity);
        Task<bool> UpdateUserAsync(int id, T entity);
        Task<ResponseType.Response> DeleteUserAsync(int id);
    }
}
