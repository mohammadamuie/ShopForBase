using Project.Application.DTOs.Category;
using Project.Application.DTOs.DataTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Interfaces
{
    public interface ICategoryService
    {
        Task<List<LandingCategoryMenuItem>> GetCategoryMenuItemAsync();
        Task Create(UpsertCategory entity);
        Task Edit(UpsertCategory entity);
        Task ChangeIsActive(int id, bool IsActive);
        Task<List<SelectCategory>> GetSelectOptionData();
        Task<List<SelectCategory>> GetSelectOptionData2();
        Task<DatatableResponse<CategoryDataTable>> GetDataTable(CategoryDataTableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task Special(int id);
        Task<List<CategoryDTO>> GetSpecial();
    }
}
