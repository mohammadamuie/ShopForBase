using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Contracts.Persistence
{
    public interface IIdentityUserRepository : IGenericRepository<ApplicationUser>
    {
        Task<ApplicationUser> GetByUserNameNoTraking(string UserName);
    }
}
