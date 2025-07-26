using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.News;
using Project.Application.DTOs.NewsCategory;
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
    public class NewsController : Controller
    {
        private readonly INewsCategoryService _newsCategoryService;
        private readonly INewsService _newsService;
        public NewsController(INewsCategoryService newsCategoryService, INewsService newsService)
        {
            _newsCategoryService = newsCategoryService;
            _newsService = newsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetData(NewsDatatableInput input)
        {
            HttpContext.Request.GetDataFromRequest(out FiltersFromRequestDataTable filters);
            var res = await _newsService.Datatable(input, filters);
            return Json(res);
        }
        public async Task<IActionResult> Create()
        {
            var selmodel = await _newsCategoryService.GetAll();
            selmodel = selmodel.Select(w => new NewsCategoryDTO()
            {
                Id = w.Id,
                Name = w.Name 
            }).ToList();
            var Categories = new SelectList(selmodel, "Id", "Name").Append(new SelectListItem
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

            var selmodel = await _newsCategoryService.GetAll();
            selmodel = selmodel.Select(w => new NewsCategoryDTO()
            {
                Id = w.Id,
                Name = w.Name 
            }).ToList();
            var Categories = new SelectList(selmodel, "Id", "Name").Append(new SelectListItem
            {
                Value = "0",
                Text = "دسته بندی را انتخاب کنید",
                Selected = true,
                Disabled = false,
                Group = null
            });
            ViewData["Categories"] = Categories.Reverse();

            var model = await _newsService.GetEditNews(Id);
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> Upsert(UpsertNews input)
        {
            if (input.Id > 0 && input.Id != null)
            {
                await _newsService.Edit(input);
                return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
            }
            await _newsService.Create(input);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
        [HttpPost]
        public async Task<JsonResult> DisableNews(int Id)
        {
            await _newsService.ActiveNews(Id, false);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
        [HttpPost]
        public async Task<JsonResult> ActivateNews(int Id)
        {
            await _newsService.ActiveNews(Id, true);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
    }
}
