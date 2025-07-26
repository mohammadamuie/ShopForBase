using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.News;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Interfaces
{
    public interface INewsService
    {
        Task<DatatableResponse<NewsDTO>> Datatable(NewsDatatableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task Create(UpsertNews create);
        Task Edit(UpsertNews edit);
        Task ActiveNews(int Id, bool isActive);
        Task<UpsertNews> GetEditNews(int Id);
        Task<List<NewsDTO>> GetNewsData(string search, int CurrentPage, int PageSize, int? CategoryId, string Tags);
        Task<NewsDTO> GetNewsDetail(string Url);
        Task<List<NewsDTO>> GetNewsDataByTake(int? CategoryId, int Take);
        Task<int> GetCount();
    }
}
