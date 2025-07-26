using Project.Domain.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Application.Contracts.Persistence
{
    public interface ITicketRepository : IGenericRepository<Ticket>
    {
        Task<Ticket> GetWithUser(int id);
    }
}
