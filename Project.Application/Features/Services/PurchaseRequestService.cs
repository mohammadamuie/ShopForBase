using AutoMapper;
using FluentValidation.Validators;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Project.Application.Contracts.Infrastructure;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Datatable;
using Project.Application.DTOs.DataTable;
using Project.Application.DTOs.News;
using Project.Application.DTOs.Product;
using Project.Application.DTOs.PurchaseRequest;
using Project.Application.Exceptions;
using Project.Application.Extensions;
using Project.Application.Features.Interfaces;
using Project.Application.Helpers;
using Project.Application.Responses;
using Project.Domain.Entities.News;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace Project.Application.Features.Services
{
    public class PurchaseRequestService : IPurchaseRequestService
    {
        private readonly IPurchaseRequestRepository _purchaseRequestRepository;
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;
        private readonly IIdentityUserService _identityUserService;
        private readonly IProductRepository _productRepository;
        private readonly IProductService _productService;
        private readonly ISettingService _settingService;
        private readonly IPaymentService _paymentService;
        private readonly ISmsSender _smsSender;
        public PurchaseRequestService(IPurchaseRequestRepository purchaseRequestRepository, ICartItemRepository cartItemRepository, IMapper mapper, IIdentityUserService identityUserService, IProductRepository productRepository, IProductService productService, ISettingService settingService, IPaymentService paymentService, ISmsSender smsSender)
        {
            _purchaseRequestRepository = purchaseRequestRepository;
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
            _identityUserService = identityUserService;
            _productRepository = productRepository;
            _productService = productService;
            _settingService = settingService;
            _paymentService = paymentService;
            _smsSender = smsSender;
        }


        public async Task<DatatableResponse<PurchaseRequestFactorDTO>> Datatable(NewsDatatableInput input, FiltersFromRequestDataTable filtersFromRequest)
        {
            var data = _purchaseRequestRepository.GetAllQueryable()
                .Include(w => w.CartItems)
                .Include(x => x.CartItems).ThenInclude(x => x.Product)
                .Include(w => w.User)
                .Where(w => (input.IsToday == true ? w.CreatedAt.Date == DateTime.Now.Date : true) && (input.Status != null ? w.PurchaseRequestStatus == input.Status : true))
                .OrderByDescending(w => w.Id)
                .AsNoTracking();

            var totalRecords = await data.CountAsync();

            if (!string.IsNullOrWhiteSpace(filtersFromRequest.SearchValue))
            {

            }

            if (input.Id.HasValue && input.Id.Value > 0)
            {
                data = data.Where(w => w.Id == input.Id);
            }

            return await data.ToDataTableAsync<PurchaseRequest, PurchaseRequestFactorDTO>(totalRecords, filtersFromRequest, _mapper);
        }
        public async Task<PurchaseRequestFactorDTO> Detail(int Id)
        {
            var data = await _purchaseRequestRepository.GetAllQueryable()
                .Include(w => w.CartItems)
                .Include(x => x.CartItems).ThenInclude(x => x.Product).ThenInclude(w => w.Category)
                .Include(w => w.User)
                .FirstOrDefaultAsync(w => w.Id == Id);
            if (data == null)
            {
                throw new NotFoundException();
            }
            var model = _mapper.Map<PurchaseRequestFactorDTO>(data);
            model.UserProfile = await _identityUserService.GetProfile(model.User.UserName);
            return model;
        }

        public async Task<PurchaseRequestResultDTO> Submit(SubmitPurchaseRequestDTO input)
        {
            var user = await _identityUserService.CurrentLoginDTO();

            var customer = await _identityUserService.GetProfile();

            var request = new PurchaseRequest
            {
                UserId = user.Id
            };



            var list = new List<CartItem>();

            foreach (var item in input.Items)
            {

                var prodctId = item.id.HasValue ? item.id.Value : 0;
                var product = await _productService.GetProductDTO(prodctId);
                if (!product.IsPrice)
                {
                    if (item.key != null)
                    {
                        if (product.ProductType != null)
                        {
                            product.ProductType = product.ProductType.Where(w => w.Type == item.key).ToList();
                            if (product.ProductType.First() != null)
                            {
                                product.price = product.ProductType.First().Typeprice;
                            }
                        }

                    }
                }
                await _productService.CheckQuantity(prodctId, item.quantity);

                await _productService.UpdateQuantity(prodctId, item.quantity, true);
                list.Add(new CartItem
                {
                    ProductId = prodctId,
                    Count = item.quantity,
                    Price = product.price.Value,
                    Type = item.key
                });

                request.FinalPrice += (product.price.Value * item.quantity);
            }

            request.CartItems = list;




            request.Adress = customer.Adress;
            request.Postalcode = customer.Postalcode;
            request.Province = customer.Province;
            request.City = customer.City;
            request.DistancePrice = request.SendPrice;
            request.SendPrice = await _settingService.GetSendPrice();
            request.PaidPrice = request.FinalPrice;
            request.FinalPrice = request.SendPrice + request.FinalPrice;




            await _purchaseRequestRepository.Add(request);



            var random = new Random();
            request.Code = "C-" + random.Next(10, 99).ToString() + request.Id.ToString();


            await _purchaseRequestRepository.Update(request);


            var result = new PurchaseRequestResultDTO
            {
                HasPayment = false,
                RequestId = request.Id
            };
            var paymentPrice = request.FinalPrice;

            request.Discount = 0;

            await _purchaseRequestRepository.Update(request);

            var paymentResult = await _paymentService.CreatePaymentForCartPurchaseRequest("https://localhost:44360/CartPurchaseRequestVerify", paymentPrice, request);

            result.HasPayment = true;
            result.Payment = paymentResult;



            return result;
        }

        public async Task ChangeStatus(ChangePurchaseRequestStatus input)
        {
            var find = await _purchaseRequestRepository.GetAllQueryable().Include(w => w.User).Include(x => x.CartItems).FirstOrDefaultAsync(w => w.Id == input.Id.Value);

            switch (input.Status)
            {
                case PurchaseRequestStatus.PaymentCompeleted_WaitForAdminConfirmation:
                    find.PurchaseRequestStatus = input.Status;
                    break;
                case PurchaseRequestStatus.AcceptedByAdmin_CakeIsBeingBaked:
                    find.PurchaseRequestStatus = input.Status;
                    break;
                case PurchaseRequestStatus.preparing:
                    find.PurchaseRequestStatus = input.Status;
                    find.PostKey = input.Value;
                    await _smsSender.SendWithPatternAsync(find.User.PhoneNumber, "coemirlc0cyfu2u", JsonConvert.SerializeObject(new Dictionary<string, string>
                    {
                        ["status"] = find.PurchaseRequestStatus.GetDisplayAttributeFrom(),
                    }));
                    break;
                case PurchaseRequestStatus.Delivered:
                    find.PurchaseRequestStatus = input.Status;
                    await _smsSender.SendWithPatternAsync(find.User.PhoneNumber, "coemirlc0cyfu2u", JsonConvert.SerializeObject(new Dictionary<string, string>
                    {
                        ["status"] = find.PurchaseRequestStatus.GetDisplayAttributeFrom(),
                    }));
                    break;
                case PurchaseRequestStatus.Cancelled:
                    find.PurchaseRequestStatus = input.Status;
                    break;
                case PurchaseRequestStatus.CancelledByAdmin:
                    find.PurchaseRequestStatus = input.Status;
                    break;
                case PurchaseRequestStatus.NoPayment:
                    find.PurchaseRequestStatus = input.Status;
                    break;
                case PurchaseRequestStatus.Archived:
                    find.PurchaseRequestStatus = input.Status;
                    break;
                default:
                    break;
            }

            await _purchaseRequestRepository.Update(find);


        }
    }
}
