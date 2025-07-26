using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Project.Application.DTOs.HomePage;
using Project.Application.Features.Interfaces;
using Project.Application.Features.Services;
using Project.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.RazorShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly INewsService _newsService;
        private readonly ICategoryService _categoryService;
        private readonly ISettingService _settingService;
        private readonly IBannerService _bannerService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public HomeController(IProductService productService, INewsService newsService, ICategoryService categoryService, IHttpContextAccessor httpContextAccessor, ISettingService settingService, IBannerService bannerService)
        {
            _productService = productService;
            _newsService = newsService;
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
            _settingService = settingService;
            _bannerService = bannerService;
        }



        public async Task<IActionResult> Index()
        {
            var randomnews = await _newsService.GetNewsDataByTake(null, 4);
            var setting = await _settingService.GetSetting();
            
            var model = new HomeDTO()
            {
                SpecialProducts = await _productService.GetData(null, 1, null, 10, null, true, null),
                LastProducts = await _productService.GetData(null, 1, null, 8, null, null, null),
                RandomNews = randomnews,
                RandomProducts = await _productService.GetData(null, 1, null, 8, null, null, true),
                SpecialCategories = await _categoryService.GetSpecial(),
                Setting = setting,
                Banner= await _bannerService.GetToList()
        };

            return View(model);
        }
    }
}
