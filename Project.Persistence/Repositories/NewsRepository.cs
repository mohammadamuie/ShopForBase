using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Domain.Entities.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories
{
    public class NewsRepository : GenericRepository<News>, INewsRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public NewsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<News> GetByUrlNoTraking(string Url)
        {
            return await _dbContext.News.AsNoTracking().Include(w=>w.Category).FirstOrDefaultAsync(w => w.Url == Url);
        }
    }
}
