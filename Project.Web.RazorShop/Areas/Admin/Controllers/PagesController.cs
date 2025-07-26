using DNTPersianUtils.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.Pages;
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
    public class PagesController : Controller
    {
        private readonly IPagesService _pagesService;

        public PagesController(IPagesService pagesService)
        {
            _pagesService = pagesService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetData(CategoryDataTableInput input)
        {

            Request.GetDataFromRequest(out FiltersFromRequestDataTable filtersFromRequest);
            var res = await _pagesService.GetDataTable(input, filtersFromRequest);
            return Json(res);
        }



        [HttpPost]
        public async Task<IActionResult> Disable(int Id)
        {
             await _pagesService.DisablePage(Id);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
        [HttpPost]
        public async Task<IActionResult> Activate(int Id)
        {
            await _pagesService.ActivePage(Id);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UpsertPages input)
        {
            await _pagesService.AddPage(input);

            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }

        public async Task<IActionResult> Update(int Id)
        {
            var respose = await _pagesService.GetByIdPage(Id);
            
            return View(respose);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpsertPages input)
        {
            
            await _pagesService.UpdatePage(input);

            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
    }
}
