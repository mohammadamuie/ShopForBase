using Project.Application.Contracts.Persistence;
using Project.Domain.Entities;
using Project.Domain.Entities.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence.Repositories
{
    public class SettingRepository : GenericRepository<Setting>, ISettingRepository
    {
        public SettingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
