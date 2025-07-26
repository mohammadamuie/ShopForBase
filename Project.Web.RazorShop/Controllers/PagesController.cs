using Microsoft.AspNetCore.Mvc;
using Project.Application.Features.Interfaces;
using System.Threading.Tasks;

namespace Project.Web.RazorShop.Controllers
{
    public class PagesController : Controller
    {
        private readonly IPagesService _pagesService;

        public PagesController(IPagesService pagesService)
        {
            _pagesService = pagesService;
        }
        public async Task<IActionResult> Index(string Url)
        {
            if (string.IsNullOrWhiteSpace(Url))
            {
                return NotFound();
            }
            var model =await _pagesService.GetByUrlPage(Url);
            return View(model);
        }
    }
}
