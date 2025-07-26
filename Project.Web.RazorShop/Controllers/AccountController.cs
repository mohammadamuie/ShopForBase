using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Application.DTOs.User;
using Project.Application.Features.Interfaces;
using Project.Application.Responses;
using Project.Domain.Entities;
using Project.Web.RazorShop.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Web.RazorShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IIdentityUserService _identityUserService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IIdentityUserService identityUserService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _identityUserService = identityUserService;
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
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInUser input)
        {
            await _identityUserService.SignIn(input);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }

        public async Task<IActionResult> Verify(string Phonenumber)
        {
            var expireSeconds = await _identityUserService.GetUserExpireCodeTime(Phonenumber);
            var model = new RegisterCode()
            {
                PhoneNumber = Phonenumber,
                ExpireSecondCode = expireSeconds
            };
            return View(model);
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

        [HttpPost]
        public async Task<IActionResult> Verify(RegisterCode input)
        {
            await _identityUserService.Verify(input);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }
    }
}
