using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.News;
using Project.Application.DTOs.NewsCategory;
using Project.Application.DTOs.PurchaseRequest;
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
    public class PurchaseRequestController : Controller
    {
        private readonly IPurchaseRequestService _purchaseRequestService;

        public PurchaseRequestController(IPurchaseRequestService purchaseRequestService)
        {
            _purchaseRequestService = purchaseRequestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Detail(int Id)
        {
            var model = await _purchaseRequestService.Detail(Id);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> GetData(NewsDatatableInput input)
        {
            HttpContext.Request.GetDataFromRequest(out FiltersFromRequestDataTable filters);
            var res = await _purchaseRequestService.Datatable(input, filters);
            return Json(res);
        }

        [HttpPost]
        public async Task<JsonResult> ChangeStatus([FromForm] ChangePurchaseRequestStatus input)
        {
            await _purchaseRequestService.ChangeStatus(input);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
    }
}
