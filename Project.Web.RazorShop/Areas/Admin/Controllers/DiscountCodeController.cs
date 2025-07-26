using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.DiscountCode;
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
    public class DiscountCodeController : Controller
    {
        private readonly IDiscountCodeService _codeRepository;
        private readonly IIdentityUserService _identityUser;
        private readonly IProductService _productService;

        public DiscountCodeController(IDiscountCodeService codeRepository, IIdentityUserService identityUser, IProductService productService)
        {
            _codeRepository = codeRepository;
            _identityUser = identityUser;
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _identityUser.GetSelectOptionData();
            var product = await _productService.GetSelectOptionData();
            ViewData["users"] =user.Reverse();
            ViewData["products"] =product.Reverse();
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> Upsert(UpsertDiscountCode input)
        {
            if (input.Id ==null)
            {
                await _codeRepository.Create(input);
                return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
            }
            else
            {
                await _codeRepository.Edit(input);
                return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
            }
             
        }
        [HttpPost]
        public async Task<JsonResult> GetData(DiscountCodeDataTableInput input)
        {
            HttpContext.Request.GetDataFromRequest(out FiltersFromRequestDataTable filters);
            var res = await _codeRepository.GetDataTable(input, filters);
            return Json(res);
        }
    }
}
