namespace Project.Application.DTOs.Payment
{
    public class CreatePayment
    {
        public double Amount { get; set; }
        public int FactorId { get; set; }
        public long TrackingNumber { get; set; }
        public string Message { get; set; }
    }
}
