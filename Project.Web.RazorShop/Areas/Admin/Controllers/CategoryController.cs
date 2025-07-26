using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs.Category;
using Project.Application.DTOs.DataTable;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Features.Services;
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
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var select = await _categoryService.GetSelectOptionData();
            var categories = new SelectList(select, "Id", "Name").Append(new SelectListItem
            {
                Value = "0",
                Text = "ندارد",
                Selected = true,
                Disabled = false,
                Group = null
            });
            ViewData["categories"] = categories.Reverse();

            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetData(CategoryDataTableInput input)
        {
            var select = await _categoryService.GetSelectOptionData();
            var categories = new SelectList(select, "Id", "Name").Append(new SelectListItem
            {
                Value = "0",
                Text = "ندارد",
                Selected = true,
                Disabled = false,
                Group = null
            });
            ViewData["categories"] = categories.Reverse();

            Request.GetDataFromRequest(out FiltersFromRequestDataTable filtersFromRequest);
            var res = await _categoryService.GetDataTable(input, filtersFromRequest);
            return Json(res);
        }


        [HttpPost]
        public async Task<JsonResult> Upsert(UpsertCategory input)
        {
            if (input.Id > 0 && input.Id != null)
            {
                await _categoryService.Edit(input);
                return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
            }
            await _categoryService.Create(input);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }

        
        [HttpPost]
        public async Task<IActionResult> Activate(int Id)
        {
            await _categoryService.ChangeIsActive(Id,true);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
        [HttpPost]
        public async Task<IActionResult> Disable(int Id)
        {
            await _categoryService.ChangeIsActive(Id, false);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
        [HttpPost]
        public async Task<JsonResult> Special(int Id)
        {
            await _categoryService.Special(Id);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
    }
}
