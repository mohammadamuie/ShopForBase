using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Interfaces
{
    public interface IPagesService
    {
        Task ActivePage(int Id);
        Task AddPage(UpsertPages input);
        Task DisablePage(int Id);
        Task<PagesDTO> GetByIdPage(int Id);
        Task<PagesDTO> GetByUrlPage(string url);
        Task<DatatableResponse<PagesDTO>> GetDataTable(CategoryDataTableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<List<PagesDTO>> GetPageList();
        Task UpdatePage(UpsertPages input);
    }
}
