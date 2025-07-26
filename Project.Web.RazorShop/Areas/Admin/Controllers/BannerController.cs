using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs.BakeryCategory;
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
    public class BannerController : Controller
    {
        private readonly IBannerService _bannerService;

        public BannerController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetData(CategoryDataTableInput input)
        {
            
            Request.GetDataFromRequest(out FiltersFromRequestDataTable filtersFromRequest);
            var res = await _bannerService.GetDataTable(input, filtersFromRequest);
            return Json(res);
        }


        [HttpPost]
        public async Task<JsonResult> Upsert(UpsertBanner input)
        {
            if (input.Id > 0 && input.Id != null)
            {
                await _bannerService.Edit(input);
                return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
            }
            await _bannerService.Create(input);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }

        
        [HttpPost]
        public async Task<IActionResult> Activate(int Id)
        {
            await _bannerService.ChangeIsActive(Id,true);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
        [HttpPost]
        public async Task<IActionResult> Disable(int Id)
        {
            await _bannerService.ChangeIsActive(Id, false);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
        
    }
}
