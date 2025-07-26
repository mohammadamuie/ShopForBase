using Project.Domain.Entities.Base;
using Project.Domain.Enums;
using System.Collections.Generic;

namespace Project.Domain.Entities.News
{
    public class PurchaseRequest : BaseEntity
    {
        public string Code { get; set; }
        public double FinalPrice { get; set; }
        public float Discount { get; set; }
        public double PaidPrice { get; set; }
        public double SendPrice { get; set; }
        public double DistancePrice { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        public string Adress { get; set; }
        public string Postalcode { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public bool IsPaid { get; set; }
        public bool IsOnlinePayment { get; set; }
        public PurchaseRequestStatus PurchaseRequestStatus { get; set; }
        public string AddressAdmin { get; set; }
        public string PhoneNumberAdmin { get; set; }
        public string PostKey { get; set; }

    }
}
