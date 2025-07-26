using Project.Domain.Entities.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Contracts.Persistence
{
    public interface IProductImagesRepository : IGenericRepository<ProductImages>
    {
        Task Remove(int Id);
        Task<List<ProductImages>> GetByProductId(int Id);
    }
}
