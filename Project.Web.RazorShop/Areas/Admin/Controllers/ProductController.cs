using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.Product;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Helpers;
using Project.Application.Responses;
using Project.Domain.Entities.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.RazorShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = PublicHelper.Roles.Administrator)]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create()
        {
            var data = await _categoryService.GetSelectOptionData2();
            var Categories = new SelectList(data, "Id", "Name").Append(new SelectListItem
            {
                Value = "0",
                Text = "دسته بندی را انتخاب کنید",
                Selected = true,
                Disabled = false,
                Group = null
            });
            ViewData["Categories"] = Categories.Reverse();

            return View();
        }
        public async Task<IActionResult> Edit(int Id)
        {
            var data = await _categoryService.GetSelectOptionData2();
            var Categories = new SelectList(data, "Id", "Name").Append(new SelectListItem
            {
                Value = "0",
                Text = "دسته بندی را انتخاب کنید",
                Selected = true,
                Disabled = false,
                Group = null
            });
            ViewData["Categories"] = Categories.Reverse();

            var response = await _productService.GetProductDTO(Id);
            return View(response);
        }

        [HttpPost]
        public async Task<JsonResult> Special(int Id)
        {
             await _productService.Special(Id);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }

        [HttpPost]
        public async Task<JsonResult> Activate(int Id)
        {
             await _productService.Active(Id);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }

        [HttpPost]
        public async Task<JsonResult> Disable(int Id)
        {
            await _productService.Delete(Id);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }

        [HttpPost]
        public async Task<JsonResult> UpsertProduct(UpsertProduct input)
        {
            if (input.Id > 0 && input.Id != null)
            {
                await _productService.Edit(input);
                return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
            }

           var response= await _productService.Create(input);
            return new Response<int>(response).ToJsonResult();
        }

        [HttpPost]
        public async Task<JsonResult> GetData(ProductDataTableInput input)
        {
            HttpContext.Request.GetDataFromRequest(out FiltersFromRequestDataTable filters);
            var res = await _productService.GetDataTable(input, filters);
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult> AddImageGalery(IFormFile Image, int ProductId)
        {
            
           var model= await _productService.AddProductGallery(Image, ProductId);
            return new Response<ProductImages> (model).ToJsonResult();
        }
        [HttpPost]
        public async Task<IActionResult> DeleteImageGalery(int Id)
        {
            await _productService.DeleteProductGallery(Id);

            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }

    }
}
