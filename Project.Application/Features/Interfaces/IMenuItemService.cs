using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.MenuItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Interfaces
{
    public interface IMenuItemService
    {
        Task Active(int Id);
        Task Create(UpsertMenuItem model);
        Task Disable(int Id);
        Task Edit(UpsertMenuItem model);
        Task<DatatableResponse<MenuItemDTO>> GetDataTable(CategoryDataTableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<List<MenuItemDTO>> GetMenuItemAsync();
        Task<List<MenuItemDTO>> GetMenuItemList();
    }
}
