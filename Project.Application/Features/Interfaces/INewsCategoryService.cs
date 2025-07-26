using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.NewsCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Interfaces
{
    public interface INewsCategoryService
    {
        Task<DatatableResponse<NewsCategoryDTO>> Datatable(NewsCategoryDatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<List<NewsCategoryDTO>> GetAll();
        Task ActiveCategory(int Id, bool IsActive);
        Task<UpsertNewsCategory> GetToEdit(int id);
        Task Edit(UpsertNewsCategory edit);
        Task Create(UpsertNewsCategory create);
    }
}
