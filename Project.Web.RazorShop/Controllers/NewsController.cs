using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.News;
using Project.Application.Features.Interfaces;
using Project.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;
using static Humanizer.On;

namespace Project.Web.RazorShop.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsCategoryService _categoryService;
        private readonly INewsService _newsService;

        public NewsController(INewsCategoryService categoryService, INewsService newsService)
        {
            _categoryService = categoryService;
            _newsService = newsService;
        }

        public async Task<IActionResult> Index(string search, int? CategoryId, string Tags, int countpage = 5, int Page = 1)
        {
            var news = await _newsService.GetNewsData(search, Page, countpage, CategoryId, Tags);
            var randomnews = await _newsService.GetNewsDataByTake(null, 4);
            var category = await _categoryService.GetAll();
            var model = new NewsListDTO()
            {
                Size = countpage,
                CurrentPage = Page,
                TotalCount = await _newsService.GetCount(),
                News = news,
                NewsCategories = category,
                RandomNews = randomnews
            };
            return View(model);
        }
        public async Task<IActionResult> Detail(string Url)
        {
            var news = await _newsService.GetNewsDetail(Url);
            var randomnews = await _newsService.GetNewsDataByTake(null, 4);
            var model = new NewsDetailDTO()
            {
                News = news,
                RandomNews = randomnews
            };
            return View(model);
        }
    }
}
