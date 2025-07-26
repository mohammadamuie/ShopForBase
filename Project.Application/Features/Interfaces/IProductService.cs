using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.Product;
using Project.Domain.Entities.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.Features.Interfaces
{
    public interface IProductService
    {
        Task<ProductSearchResponse> GetData(string search, int page, int? category, int? countpage, int? Discount,bool? Special, bool? random);
        Task<DatatableResponse<ProductDataTable>> GetDataTable(ProductDataTableInput input, FiltersFromRequestDataTable filtersFromRequest);
        Task<ProductDTO> GetProductDTO(int Id);
        Task<ProductDTO> GetProductDetailDTO(string Url);
        Task<int> Create(UpsertProduct entity);
        Task Edit(UpsertProduct entity);
        Task Active(int id);
        Task Delete(int id);
        Task<ProductImages> AddProductGallery(IFormFile Image, int ProductId);
        Task DeleteProductGallery(int Id);
        Task<IEnumerable<SelectListItem>> GetSelectOptionData();
        Task<List<ProductDTO>> GetByTake(int take, int CategoryId);
        Task Special(int id);
        Task UpdateQuantity(int id, int count, bool isMinus);
        Task CheckQuantity(int id, int count);
    }
}
