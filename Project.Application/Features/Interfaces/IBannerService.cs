using Project.Application.DTOs.BakeryCategory;
using Project.Application.DTOs.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Interfaces
{
    public interface IBannerService
    {
        Task ChangeIsActive(int id, bool IsActive);
        Task<BannerDTO> Create(UpsertBanner input);
        Task<DatatableResponse<BannerDTO>> Datatable(DatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<bool> Delete(int id);
        Task<BannerDTO> Edit(UpsertBanner input);
        Task<DatatableResponse<BannerDTO>> GetDataTable(CategoryDataTableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<UpsertBanner> GetToEdit(int id);
        Task<List<BannerDTO>> GetToList();
        Task UnAvailable(int id);
    }
}
