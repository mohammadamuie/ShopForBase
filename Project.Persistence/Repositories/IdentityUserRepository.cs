using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories
{
    public class IdentityUserRepository : GenericRepository<ApplicationUser>, IIdentityUserRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public IdentityUserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ApplicationUser> GetByUserNameNoTraking(string UserName)
        {
            return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(w => w.UserName== UserName);
        }
    }
}
