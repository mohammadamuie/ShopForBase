using System.Threading.Tasks;
using Project.Domain.Entities;

namespace Project.Application.Contracts.Persistence
{
    public interface IUserRepository : IGenericRepository<JWTUser>
    {
        Task<JWTUser> GetByPhone(string phone);
        Task<JWTUser> GetNoTracking(int id);
        Task<JWTUser> GetWithRelations(int id);
    }
}
