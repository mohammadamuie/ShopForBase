using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Application.Exceptions;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories
{
    public class DiscountCodeRepository : GenericRepository<DiscountCode>, IDiscountCodeRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public DiscountCodeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CheckCodeAsync(string Code)
        {
            var getCode = await _dbContext.DiscountCodes.FirstOrDefaultAsync(w => w.Code == Code);
            if (getCode != null)
            {
                throw new BadRequestException("کد وارد شده از قبل وجود دارد!");
            }
        }
    }
}
