using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.Setting;
using Project.Application.Features.Interfaces;
using Project.Application.Helpers;
using Project.Application.Responses;
using System.Threading.Tasks;

namespace Project.Web.RazorShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = PublicHelper.Roles.Administrator)]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        public async Task<IActionResult> Index()
        {
            var model=await _settingService.GetSetting();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpsertSetting input)
        {
            await _settingService.UpdateSettings(input);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
    }
}
