using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;

namespace Project.Persistence.Repositories
{
    public class UserRepository : GenericRepository<JWTUser>, IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<JWTUser> GetByPhone(string phone)
        {
            return await _dbContext.JWTUsers.FirstOrDefaultAsync(f => f.Phone == phone);
        }

        public async Task<JWTUser> GetNoTracking(int id)
        {
            return await _dbContext.JWTUsers.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<JWTUser> GetWithRelations(int id)
        {
            var user = await Get(id);


            return user;
        }

    }
}