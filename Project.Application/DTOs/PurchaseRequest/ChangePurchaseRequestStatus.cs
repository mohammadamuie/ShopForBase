using Project.Application.Filters;
using Project.Domain.Enums;

namespace Project.Application.DTOs.PurchaseRequest
{
    public class ChangePurchaseRequestStatus
    {

        public int? Id { get; set; }
        public PurchaseRequestStatus Status { get; set; }
        public string Value { get; set; }

    }
}
