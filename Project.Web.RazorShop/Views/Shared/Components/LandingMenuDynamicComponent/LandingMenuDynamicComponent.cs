using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Application.Features.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.RazorShop.Views.Shared.Components.LandingMenuDynamicComponent
{
    public class LandingMenuDynamicComponent : ViewComponent
    {
        private readonly IMenuItemService _menuItemService;

        public LandingMenuDynamicComponent(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var items = await _menuItemService.GetMenuItemAsync();
            return View(nameof(LandingMenuDynamicComponent), items);
        }

    }

}
