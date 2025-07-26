using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Parbad;
using Parbad.Abstraction;
using Parbad.Gateway.Mellat;
using Parbad.Gateway.ParbadVirtual;
using Parbad.Gateway.ZarinPal;
using Parbad.Storage.Abstractions.Models;
using Project.Application.Contracts.Persistence;
using Project.Application.DTOs.Payment;
using Project.Application.Exceptions;
using Project.Application.Features.Interfaces;
using Project.Application.Hubs;
using Project.Domain.Entities;
using Project.Domain.Entities.News;
using System;
using System.Linq;
using System.Threading.Tasks;
using Payment = Project.Domain.Entities.Payment;

namespace Project.Application.Features.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IFactorService _factorService;
        private readonly IOnlinePayment _onlinePayment;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IProductService _productService;
        private readonly IPurchaseRequestRepository _purchaseRequestRepository;
        private readonly string _baseUrl;
        private readonly string _successUrl;
        private readonly string _failedUrl;
        private readonly IHubContext<ChatHub> _chatHub;

        public PaymentService(IFactorService factorService, IOnlinePayment onlinePayment, IPaymentRepository paymentRepository, IMapper mapper, IProductService productService, IPurchaseRequestRepository purchaseRequestRepository, IHubContext<ChatHub> chatHub)
        {
            _factorService = factorService;
            _onlinePayment = onlinePayment;
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _productService = productService;
            _purchaseRequestRepository = purchaseRequestRepository;
            _baseUrl = "https://localhost:44360/";
            _successUrl = "cart/successpayment/";
            _failedUrl = "failedpayment/";
            _chatHub = chatHub;
        }
        private string GenerateUrl(bool isSuccess, int paymentId, string price, string trackingNumber, int itemId, int type)
        {
            var url = _baseUrl;
            if (isSuccess)
                url += _successUrl;
            else
                url += _failedUrl;

            url += "p=" + paymentId + "&&item=" + itemId + "&&type=" + type + "&&price=" + price + "&&trackingNumber=" + trackingNumber;

            return url;
        }


        public async Task<IPaymentRequestResult> PayFactor(int factorId)
        {
            var findFactor = await _factorService.GetById(factorId);

            if (findFactor == null)
            {
                throw new NotFoundException("فاکتور مورد نظر پیدا نشد");
            }

            if (findFactor.Status == Domain.Enums.FactorStatus.Completed)
            {
                throw new BadRequestException("این فاکتور از قبل پرداخت شده است");
            }

            var callbackUrl = "https://localhost:44321/billing/verify";

            IPaymentRequestResult result = await _onlinePayment.RequestAsync(invoice =>
            {
                invoice
                    .SetZarinPalData("پرداخت فاکتور")
                    .SetTrackingNumber(DateTime.Now.Ticks)
                    .SetAmount(new Money((decimal)findFactor.FinalAmount))
                    .SetCallbackUrl(callbackUrl)
                    //.UseParbadVirtual();
                    .UseZarinPal();
            });

            var createPayment = new CreatePayment
            {
                Amount = findFactor.FinalAmount,
                FactorId = findFactor.Id,
                TrackingNumber = result.TrackingNumber,
                Message = result.Message
            };

            var payment = _mapper.Map<Domain.Entities.Payment>(createPayment);

            payment = await _paymentRepository.Add(payment);

            return result.IsSucceed ? result : throw new BadRequestException("پرداخت ناموفق - " + result.Message);

        }

        public async Task<IPaymentFetchResult> VerifyPayment()
        {
            IPaymentFetchResult invoice = await _onlinePayment.FetchAsync();

            if (invoice.IsAlreadyVerified)
            {
                return invoice;
            }

            var findPayment = await _paymentRepository.GetAllQueryable()
                .FirstOrDefaultAsync(f => f.IsActive == true && f.TrackingNumber == invoice.TrackingNumber);

            if (findPayment == null)
            {
                throw new BadRequestException("اطلاعات پرداخت پیدا نشد");
            }

            if (invoice.IsSucceed && invoice.Amount == findPayment.Amount)
            {
                //await _factorService.ExecuteFactor(findPayment.FactorId);

                IPaymentVerifyResult result = await _onlinePayment.VerifyAsync(invoice);

                findPayment.IsPaid = true;
                findPayment.TransactionCode = result.TransactionCode;
                findPayment.Message = result.Message;
                findPayment.AdditionalData = JsonConvert.SerializeObject(result.AdditionalData);
            }
            else
            {
                findPayment.IsPaid = false;
                findPayment.Message = invoice.Message;
                findPayment.AdditionalData = JsonConvert.SerializeObject(invoice.AdditionalData);
            }

            await _paymentRepository.Update(findPayment);

            return invoice;
        }

        public async Task<string> CartPurchaseRequestVerify()
        {
            var invoice = await _onlinePayment.FetchAsync();
            var result = await _onlinePayment.VerifyAsync(invoice);

            var payment = _paymentRepository.GetAllQueryable().FirstOrDefault(x => x.TrackingNumber == result.TrackingNumber);

            var purchaseRequest = _purchaseRequestRepository.GetAllQueryable().Include(x => x.User).Include(x => x.CartItems).FirstOrDefault(x => x.Id == payment.PurchaseRequestId);

            if (result.IsSucceed == true)
            {
                purchaseRequest.PurchaseRequestStatus = Domain.Enums.PurchaseRequestStatus.PaymentCompeleted_WaitForAdminConfirmation;
                purchaseRequest.IsPaid = true;
                purchaseRequest.PaidPrice = payment.Amount;
                payment.IsPaid = true;
                payment.TransactionCode = result.TransactionCode;
                await _paymentRepository.Update(payment);
                await _purchaseRequestRepository.Update(purchaseRequest);

                var url = GenerateUrl(true,
                    payment.Id,
                    payment.Amount.ToString(),
                    payment.TrackingNumber.ToString(),
                    purchaseRequest.Id,
                1);
             
                await _chatHub.Clients.All.SendAsync("ReceiveMessage", true);

                //await _smsSender.SendWithPatternAsync(purchaseRequest.Customer.Phone
                //  , "zdfl0hynved2lrk", JsonConvert.SerializeObject(new Dictionary<string, string>
                //  {
                //      ["phone"] = purchaseRequest.Customer.Phone,
                //      ["code"] = purchaseRequest.Code,
                //      ["date"] = purchaseRequest.CreatedAt.ToShortPersianDateTimeString(),
                //  }));

                return url;
            }
            else
            {
                foreach (var item in purchaseRequest.CartItems)
                {
                    await _productService.UpdateQuantity(item.ProductId, item.Count, false);
                }
                purchaseRequest.PurchaseRequestStatus = Domain.Enums.PurchaseRequestStatus.NoPayment;
                payment.IsPaid = false;
                payment.TransactionCode = result.TransactionCode;
                await _paymentRepository.Update(payment);
                await _purchaseRequestRepository.Update(purchaseRequest);

                var url = GenerateUrl(false,
                   payment.Id,
                   payment.Amount.ToString(),
                   payment.TrackingNumber.ToString(),
                   purchaseRequest.Id,
                   1);
                return url;
            }
        }


        public async Task<IPaymentRequestResult> CreatePaymentForCartPurchaseRequest(string callBackUrl, double price, PurchaseRequest request)
        {
            var payment = new Payment
            {
                Amount = price/* * 10*/,
                PurchaseRequestId = request.Id
            };

            IPaymentRequestResult result = await _onlinePayment.RequestAsync(invoice =>
            {
                invoice
                    .SetZarinPalData($"پرداخت مبلغ نهایی سفارش کد {request.Id}")
                    .SetTrackingNumber(DateTime.Now.Ticks)
                    .SetAmount(new Money((decimal)payment.Amount))
                    .SetCallbackUrl(callBackUrl)
                    .UseZarinPal();
            });

            payment.TrackingNumber = result.TrackingNumber;
            payment.Message = result.Message;

            await _paymentRepository.Add(payment);

            return result.IsSucceed ? result : throw new BadRequestException("پرداخت ناموفق - " + result.Message);
        }


    }
}
