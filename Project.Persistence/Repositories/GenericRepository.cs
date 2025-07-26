using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Project.Application.Contracts.Persistence;
using Project.Domain.Entities.Base;

namespace Project.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> Get(int id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(f => (f as BaseEntity).Id == id);
        }

        public async Task<T> GetNoTracking(int id)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(f => (f as BaseEntity).IsActive == true && (f as BaseEntity).Id == id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dbContext.Set<T>().Where(w => (w as BaseEntity).IsActive == true).ToListAsync();
        }

        public IQueryable<T> GetAllQueryable()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        public async Task<T> Add(T entity)
        {
            await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var find = await Get(id);
            (find as BaseEntity).IsActive = false;
            //_dbContext.Set<T>().Update(find);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Recover(int id)
        {
            var find = await Get(id);
            (find as BaseEntity).IsActive = true;
            //_dbContext.Set<T>().Update(find);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<bool> Exist(int id)
        {
            var entity = await Get(id);
            return entity != null;
        }
    }
}