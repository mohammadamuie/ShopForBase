using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class FactorRepository : GenericRepository<Factor>, IFactorRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public FactorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}