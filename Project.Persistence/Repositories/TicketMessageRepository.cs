using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class TicketMessageRepository : GenericRepository<TicketMessage>, ITicketMessageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TicketMessageRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}