using System.Collections.Generic;
using System.Threading.Tasks;

namespace AlfredCMS.Core.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(string slug);
        Task<bool> Add(T entity);
        Task<bool> Update(string slug, T entity);
        Task<string> Delete(string slug);
    }
}
