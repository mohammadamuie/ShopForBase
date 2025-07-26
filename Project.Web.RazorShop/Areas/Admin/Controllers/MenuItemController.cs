using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.MenuItem;
using Project.Application.DTOs.Pages;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Helpers;
using Project.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.RazorShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = PublicHelper.Roles.Administrator)]
    public class MenuItemController : Controller
    {
        private readonly IMenuItemService _menuItemService;
        private readonly IPagesService _pagesService;
        public MenuItemController(IMenuItemService menuItemService, IPagesService pagesService)
        {
            _menuItemService = menuItemService;
            _pagesService = pagesService;
        }

        public async Task<IActionResult> Index()
        {
            var Pages = new SelectList(await _pagesService.GetPageList(), "Url", "Title").Append(new SelectListItem
            {
                Value = "#",
                Text = "ندارد",
                Selected = true,
                Disabled = false,
                Group = null
            });
            ViewData["AllPages"] = Pages.Reverse();
            var items = new SelectList(await _menuItemService.GetMenuItemList(), "Id", "Name").Append(new SelectListItem
            {
                Value = "0",
                Text = "ندارد",
                Selected = true,
                Disabled = false,
                Group = null
            });
            ViewData["AllItems"] = items.Reverse();
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetData(CategoryDataTableInput input)
        {

            Request.GetDataFromRequest(out FiltersFromRequestDataTable filtersFromRequest);
            var res = await _menuItemService.GetDataTable(input, filtersFromRequest);
            return Json(res);
        }


        [HttpPost]
        public async Task<IActionResult> Disable(int Id)
        {
            await _menuItemService.Disable(Id);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
        [HttpPost]
        public async Task<IActionResult> Activate(int Id)
        {
            await _menuItemService.Active(Id);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UpsertMenuItem input)
        {
            await _menuItemService.Create(input);

            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpsertMenuItem input)
        {
            await _menuItemService.Edit(input);

            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
    }
}
