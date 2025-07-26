using Project.Application.Contracts.Persistence;
using Project.Domain.Entities.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories
{
    public class NewsCategoryRepository : GenericRepository<NewsCategory>, INewsCategoryRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public NewsCategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }
}
