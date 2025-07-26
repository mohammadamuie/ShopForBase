using Project.Application.DTOs.DataTable;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Contracts.Persistence
{
    public interface IDiscountCodeRepository:IGenericRepository<DiscountCode>
    {
        Task CheckCodeAsync(string Code);
    }
}
