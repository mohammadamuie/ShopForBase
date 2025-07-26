using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Application.Responses;
using Project.Domain.Entities;
using Project.Web.RazorShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.RazorShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index(string returnUrl)
        {
            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;

            if (_signInManager.IsSignedIn(User))
            {
                return Redirect(returnUrl);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View("index");
        }
        public IActionResult Login(string returnUrl)
        {
            returnUrl = string.IsNullOrEmpty(returnUrl) ? "/" : returnUrl;

            if (_signInManager.IsSignedIn(User))
            {
                return Redirect(returnUrl);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View("index");
        }
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginViewModel input)
        {
            var ReturnUrl = string.IsNullOrWhiteSpace(input.ReturnUrl) ? "/" : input.ReturnUrl;
            var find = await _userManager.FindByNameAsync(input.Username);
            if (find == null)
            {
                return new Response<string>(ResponseStatus.BadRequest, message: "کاربر یافت نشد").ToJsonResult();
            }

            var result = await _signInManager.PasswordSignInAsync(input.Username, input.Password, input.RememberMe, lockoutOnFailure: false);

            return result.Succeeded
                ? new Response<string>(ResponseStatus.Succeed, data: ReturnUrl).ToJsonResult()
                : new Response<string>(ResponseStatus.BadRequest, message: "کلمه عبور صحیح نمی‌باشد").ToJsonResult();
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string returnUrl = null)
        {
            await _signInManager.SignOutAsync();
            returnUrl = returnUrl ?? Url.Content("/");
            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Json((Status: 1, Message: "Logged Out"));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
