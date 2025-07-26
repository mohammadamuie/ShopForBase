using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.DataTable;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.RazorShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = PublicHelper.Roles.Administrator)]
    public class UserController : Controller
    {
        private readonly IIdentityUserService _identityUserService;

        public UserController(IIdentityUserService identityUserService)
        {
            _identityUserService = identityUserService;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetDataTable(UserDataTableInput input)
        {
            Request.GetDataFromRequest(out FiltersFromRequestDataTable filtersFromRequest);
            var response = await _identityUserService.GetDataTable(input, filtersFromRequest);
            return new JsonResult(response);
        }

        public async Task<IActionResult> Profile(string UserName)
        {
            var response = await _identityUserService.GetProfile(UserName);
            return View(response);
        }
    }
}
