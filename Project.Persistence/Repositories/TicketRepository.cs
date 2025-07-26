using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories
{
    public class TicketRepository : GenericRepository<Ticket>, ITicketRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TicketRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Ticket> GetWithUser(int id)
        {
            return await _dbContext.Tickets
                .Where(w => w.IsActive == true && w.Id == id)
                .Include(i => i.User)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}