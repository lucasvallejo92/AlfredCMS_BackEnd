using AlfredCMS.Core.Models.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlfredCMS.Core.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(string identifier);
        Task<bool> AddAsync(T entity);
        Task<bool> UpdateAsync(string identifier, T entity);
        Task<ResponseType.Response> DeleteAsync(string identifier);
    }
}
