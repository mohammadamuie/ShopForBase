using Project.Domain.Enums;

namespace Project.Application.DTOs.Factor
{
    public class CreateFactor
    {
        public string UserId { get; set; }
        public double Price { get; set; }
        public string DiscountCode { get; set; }
        public double DiscountAmount { get; set; }
        public FactorType Type { get; set; }
        public int TypeId { get; set; } = 0;
    }
}
