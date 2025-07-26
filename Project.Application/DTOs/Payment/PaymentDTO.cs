using Project.Application.DTOs.Base;

namespace Project.Application.DTOs.Payment
{
    public class PaymentDTO : BaseDTO
    {
        public double Amount { get; set; }
        public int FactorId { get; set; }
        public bool IsPaid { get; set; }
        public long TrackingNumber { get; set; }
        public string TransactionCode { get; set; }
        public string AdditionalData { get; set; }
        public string Message { get; set; }
    }
}
