using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories
{
    public class BannerRepository : GenericRepository<Banner>, IBannerRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public BannerRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
