using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories
{
    class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IQueryable<Category>> CategoryWhere(IQueryable<Category> data, bool IsActive)
        {
            return data.Where(w =>
                  w.IsActive == IsActive &&
                 (w.Parent != null ? w.Parent.IsActive == IsActive : w.IsActive == IsActive) &&
                 (w.Parent.Parent != null ? w.Parent.Parent.IsActive == IsActive : w.IsActive == IsActive));
        }
    }
}
