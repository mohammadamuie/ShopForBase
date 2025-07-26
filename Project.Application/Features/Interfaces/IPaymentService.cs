using Parbad;
using Project.Domain.Entities;
using Project.Domain.Entities.News;
using System.Threading.Tasks;

namespace Project.Application.Features.Interfaces
{
    public interface IPaymentService
    {
        Task<IPaymentRequestResult> PayFactor(int factorId);
        Task<IPaymentFetchResult> VerifyPayment();
        Task<IPaymentRequestResult> CreatePaymentForCartPurchaseRequest(string callBackUrl, double price, PurchaseRequest request);
        Task<string> CartPurchaseRequestVerify();
    }
}
