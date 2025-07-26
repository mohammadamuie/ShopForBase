using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(int id);
        Task Recover(int id);
        Task<bool> Exist(int id);
        System.Linq.IQueryable<T> GetAllQueryable();
        Task<T> GetNoTracking(int id);
    }
}
