using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Application.Features.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.RazorShop.Views.Shared.Components.LandingMenuMobileComponent
{
    public class LandingMenuMobileComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public LandingMenuMobileComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _categoryService.GetCategoryMenuItemAsync();
            return View(nameof(LandingMenuMobileComponent), items);
        }

    }

}
