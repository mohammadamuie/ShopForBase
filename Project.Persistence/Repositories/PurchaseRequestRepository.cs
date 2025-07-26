using Project.Application.Contracts.Persistence;
using Project.Domain.Entities.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories
{
    public class PurchaseRequestRepository : GenericRepository<PurchaseRequest>, IPurchaseRequestRepository
    {
        public PurchaseRequestRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
