using Project.Domain.Entities.Base;
using Project.Domain.Enums;

namespace Project.Domain.Entities
{
    public class Factor : BaseEntity
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public double Price { get; set; }
        public double TaxAmount { get; set; }
        public string DiscountCode { get; set; }
        public double DiscountAmount { get; set; }
        public double FinalAmount { get; set; }
        public FactorStatus Status { get; set; } = FactorStatus.Pending;
        public FactorType Type { get; set; } = FactorType.Subscription;
        public int TypeId { get; set; } = 0;
    }
}