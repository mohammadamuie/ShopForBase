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
    public class UserInformationRepository : GenericRepository<UserInformation>, IUserInformationRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public UserInformationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserInformation> GetByUserIdNoTraking(string UserId)
        {
            return await _dbContext.UserInformation.AsNoTracking().FirstOrDefaultAsync(w => w.UserId== UserId);
        }


        public async Task<UserInformation> GetByIdNoTraking(int? UserId)
        {
            return await _dbContext.UserInformation.AsNoTracking().FirstOrDefaultAsync(w => w.Id == UserId);
        }
    }
}
