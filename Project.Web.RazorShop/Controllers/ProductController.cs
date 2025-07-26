using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Product;
using Project.Application.Features.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.RazorShop.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Search(string search,int? category, int? Discount, bool? Special, bool? random, int? countpage = 9, int page = 1)
        {
            var response = await _productService.GetData(search, page, category, countpage, Discount, Special,random);
            return View(response);
        }

        public async Task<IActionResult> Details(string Url)
        {
            var detail = await _productService.GetProductDetailDTO(Url);
            var productList = await _productService.GetByTake(8, detail.CategoryId);

            var model = new ProductDetailDTO()
            {
                Product = detail,
                ProductList = productList
            };
            return View(model);
        }
     
        [HttpGet]
        public async Task<IActionResult> GetData(string search, int? category, int? Discount, bool? Special, bool? random, int? countpage = 9, int page = 1)
        {
            var response = await _productService.GetData(search, page, category, countpage, Discount, Special, random);

            return Json(response);
        }

    }
}
