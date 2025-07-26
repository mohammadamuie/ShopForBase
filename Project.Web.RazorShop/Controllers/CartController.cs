using DNTPersianUtils.Core.IranCities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using Parbad;
using Project.Application.DTOs.CartItem;
using Project.Application.DTOs.HomePage;
using Project.Application.DTOs.Product;
using Project.Application.DTOs.PurchaseRequest;
using Project.Application.DTOs.ShoppingCart;
using Project.Application.DTOs.User;
using Project.Application.Exceptions;
using Project.Application.Features.Interfaces;
using Project.Application.Features.Services;
using Project.Application.Hubs;
using Project.Application.Responses;
using Project.Domain.Entities.News;
using Project.Domain.Entities.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.Web.RazorShop.Controllers
{
    public class CartController : Controller
    {

        private readonly IShoppingCartService _shoppingCartService;
        private readonly IProductService _productService;
        private readonly IIdentityUserService _userService;
        private readonly ISettingService _settingService;
        private readonly IPurchaseRequestService _purchaseRequestService;
        private readonly IPaymentService _paymentService;
        private readonly IHubContext<ChatHub> _chatHub;

        public CartController(IShoppingCartService shoppingCartService, IProductService productService, IIdentityUserService userService, ISettingService settingService, IPurchaseRequestService purchaseRequestService, IPaymentService paymentService, IHubContext<ChatHub> chatHub)
        {
            _shoppingCartService = shoppingCartService;
            _productService = productService;
            _userService = userService;
            _settingService = settingService;
            _purchaseRequestService = purchaseRequestService;
            _paymentService = paymentService;
            _chatHub = chatHub;
        }

        public class CartInfo
        {
            public int Id { get; set; }
            public int Quantity { get; set; }
            public string Key { get; set; }
        }
        private List<CartInfo> ReadFromCookie()
        {
            // اینجا می‌توانید از HttpContext.Request.Cookies یا دیگر روش‌های خواندن کوکی در ASP.NET Core استفاده کنید
            // مثال:
            if (HttpContext.Request.Cookies.TryGetValue("manoShoppingCart", out var cartCookie))
            {
                try
                {
                    // بازگشت اطلاعات به عنوان شیء CartInfo
                    return JsonConvert.DeserializeObject<List<CartInfo>>(cartCookie);
                }
                catch (System.Exception e)
                {
                    //حذف کامل کوکی بعد از خطا
                    //Response.Cookies.Delete("manoShoppingCart");

                    //HttpContext.Response.Cookies.Append("manoShoppingCart", "", new CookieOptions
                    //{
                    //    Expires = DateTime.Now.AddDays(-1) // تنظیم تاریخ انقضای گذشته برای حذف کوکی
                    //});

                    return null;
                }

            }
            return null;
        }

        public async Task<IActionResult> Index()
        {
            var cartItem = ReadFromCookie();
            List<ProductDTO> products = new List<ProductDTO>();
            if (cartItem != null)
            {
                foreach (var item in cartItem)
                {
                    var getproduct = await _productService.GetProductDTO(item.Id);
                    if (!getproduct.IsPrice)
                    {
                        if (item.Key != null)
                        {
                            if (getproduct.ProductType != null)
                            {
                                getproduct.ProductType = getproduct.ProductType.Where(w => w.Type == item.Key).ToList();
                                if (getproduct.ProductType.First() != null)
                                {
                                    getproduct.price = getproduct.ProductType.First().Typeprice;
                                }
                            }

                        }
                    }

                    getproduct.CartQuantity = item.Quantity;
                    products.Add(getproduct);
                }
            }
            var model = new ShoppingCartDTO()
            {
                Products = products
            };


            return View(model);
        }

        public async Task<IActionResult> SuccessPayment(int p, string price, string trackingNumber, int item, int type)
        {
            return View();
        }

        public async Task<IActionResult> Checkout()
        {
            var user = await _userService.CurrentLoginDTO();
            if (user == null)
            {
                return LocalRedirect(Url.Content("/customer"));
            }
            var cartItem = ReadFromCookie();
            if (cartItem == null)
            {
                return LocalRedirect(Url.Content("/"));
            }
            var profile = await _userService.GetProfile();
            var sendPrice = await _settingService.GetSendPrice();
            var model = new CheckoutDTO()
            {
                Profile = profile,
                SendPrice = sendPrice
            };
            foreach (var item in cartItem)
            {
                var getproduct = await _productService.GetProductDTO(item.Id);
                if (!getproduct.IsPrice)
                {
                    if (item.Key != null)
                    {
                        if (getproduct.ProductType != null)
                        {
                            getproduct.ProductType = getproduct.ProductType.Where(w => w.Type == item.Key).ToList();
                            if (getproduct.ProductType.First() != null)
                            {
                                getproduct.price = getproduct.ProductType.First().Typeprice;
                            }
                        }

                    }
                }
                model.TotalPrice += (item.Quantity * getproduct.price.Value);
            }
            ViewBag.Provinces = Iran.Provinces.OrderBy(i => i.ProvinceName);
            ViewBag.Cities = Iran.Cities.OrderBy(i => i.CityName);

            return View(model);
        }

        [HttpPost("Submit")]
        public async Task<IActionResult> Submit()
        {

            try
            {
                var cartItem = ReadFromCookie();
                if (cartItem == null)
                {
                    return LocalRedirect(Url.Content("/"));
                }

                if (cartItem.Count() < 1)
                {
                    throw new BadRequestException("سبد خرید خالی میباشد!!");
                }
                var model = new SubmitPurchaseRequestDTO()
                {
                    Items = cartItem.Select(w => new CartItemFromUserDTO()
                    {
                        id = w.Id,
                        key = w.Key,
                        quantity = w.Quantity,
                    }).ToList(),
                };
                var data = await _purchaseRequestService.Submit(model);
                return new Response<PurchaseRequestResultDTO>(data).ToJsonResult();
            }
            catch (Exception)
            {

                throw new BadRequestException("سبد خرید خالی میباشد!!");
            }
        }

        [Route("CartPurchaseRequestVerify")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> CartPurchaseRequestVerify()
        {
            var data = await _paymentService.CartPurchaseRequestVerify();
            await _chatHub.Clients.All.SendAsync("ReceiveMessage", true);

            return Redirect(data);
        }


        [HttpPost]
        public async Task<JsonResult> Upsert(EditProfileWithOutPhoneNumber input)
        {
            await _userService.EditProfileWithOutPhoneNumber(input);
            return new Response<string>(ResponseStatus.Succeed).ToJsonResult();
        }

        public IActionResult GetProvinceCities(string provinceName)
        {
            var list = Iran.Cities.Where(x => x.ProvinceName == provinceName).ToList();
            return Json(new SelectList(list, "CityName", "CityName"));
        }
    }
}
