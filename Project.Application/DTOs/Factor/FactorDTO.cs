using Project.Application.DTOs.Base;
using Project.Domain.Enums;

namespace Project.Application.DTOs.Factor
{
    public class FactorDTO : BaseDTO
    {
        public string UserId { get; set; }
        public string User { get; set; }
        public double Price { get; set; }
        public double TaxAmount { get; set; }
        public string DiscountCode { get; set; }
        public double DiscountAmount { get; set; }
        public double FinalAmount { get; set; }
        public FactorStatus Status { get; set; }
        public string StatusString { get; set; }
        public FactorType Type { get; set; }
        public string TypeString { get; set; }
        public int TypeId { get; set; } = 0;
    }
}
