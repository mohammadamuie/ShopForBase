using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Contracts.Persistence
{
    public interface IUserInformationRepository:IGenericRepository<UserInformation>
    {
        Task<UserInformation> GetByUserIdNoTraking(string UserId);
        Task<UserInformation> GetByIdNoTraking(int? UserId);
    }
}
