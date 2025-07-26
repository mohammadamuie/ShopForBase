using Project.Domain.Entities.Base;
using Project.Domain.Entities.News;

namespace Project.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public double Amount { get; set; }
        public int? FactorId { get; set; }
        public Factor Factor { get; set; }
        public int? PurchaseRequestId { get; set; }
        public PurchaseRequest PurchaseRequest { get; set; }
        public bool IsPaid { get; set; }
        public long TrackingNumber { get; set; }
        public string TransactionCode { get; set; }
        public string AdditionalData { get; set; }
        public string Message { get; set; }
    }
}