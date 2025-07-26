using DNTPersianUtils.Core.IranCities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.NewsCategory;
using Project.Application.DTOs.User;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Features.Services;
using Project.Application.Helpers;
using Project.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.RazorShop.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize(Roles = PublicHelper.Roles.Customer)]
    public class HomeController : Controller
    {
        private readonly IIdentityUserService _userService;
        private readonly IDiscountCodeService _discountCodeService;
        public HomeController(IIdentityUserService userService, IDiscountCodeService discountCodeService)
        {
            _userService = userService;
            _discountCodeService = discountCodeService;
        }

        public async Task<IActionResult> Index(int? countpage = 9, int page = 1)
        {
            
            var profile = await _userService.GetProfile();
            var discountCode = await _discountCodeService.GetData(page, countpage);
            var model = new UserDashboardDTO()
            {
                Profile = profile,
                DiscountCode=discountCode
            };

            ViewBag.Provinces = Iran.Provinces.OrderBy(i => i.ProvinceName);
            ViewBag.Cities = Iran.Cities.OrderBy(i => i.CityName);

            return View(model);
        }
        public async Task<IActionResult> Profile()
        {

            var profile = await _userService.GetProfile();
            var model = new UserDashboardDTO()
            {
                Profile = profile
            };

            ViewBag.Provinces = Iran.Provinces.OrderBy(i => i.ProvinceName);
            ViewBag.Cities = Iran.Cities.OrderBy(i => i.CityName);

            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> SendCode()
        {
            var response = await _userService.SendCode();
            return new Response<string>(response).ToJsonResult();
        }

        [HttpPost]
        public async Task<JsonResult> Upsert(EditProfile input)
        {
            await _userService.EditProfile(input);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }

        public IActionResult GetProvinceCities(string provinceName)
        {
            var list = Iran.Cities.Where(x => x.ProvinceName == provinceName).ToList();
            return Json(new SelectList(list, "CityName", "CityName"));
        }
    }
}
