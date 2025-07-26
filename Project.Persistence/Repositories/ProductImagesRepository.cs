using Microsoft.EntityFrameworkCore;
using Project.Application.Contracts.Persistence;
using Project.Domain.Entities.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories
{
   public class ProductImagesRepository : GenericRepository<ProductImages>, IProductImagesRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductImagesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task Remove(int Id)
        {
            var model=await _dbContext.ProductImages.AsNoTracking().FirstOrDefaultAsync(w => w.Id == Id);
            _dbContext.ProductImages.Remove(model);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<List<ProductImages>> GetByProductId(int Id)
        {
            return await _dbContext.ProductImages.AsNoTracking().Where(w => w.ProductId == Id).ToListAsync();
        }
    }
}
