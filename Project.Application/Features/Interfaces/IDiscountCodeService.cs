using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.DiscountCode;
using Project.Application.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Interfaces
{
    public interface IDiscountCodeService
    {
        Task Create(UpsertDiscountCode entity);
        Task Edit(UpsertDiscountCode entity);
        Task<DiscountCodeSearchResponse> GetData(int page, int? countpage);
        Task<DatatableResponse<DiscountCodeDataTable>> GetDataTable(DiscountCodeDataTableInput input, FiltersFromRequestDataTable filtersFromRequest);
    }
}
