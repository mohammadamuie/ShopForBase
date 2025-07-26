using Project.Application.DTOs.Base;
using DNTPersianUtils.Core;
using Project.Application.DTOs.CartItem;
using Project.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Application.DTOs.User;
using Project.Application.DTOs.DataTable;

namespace Project.Application.DTOs.PurchaseRequest
{
    public class PurchaseRequestFactorDTO : BaseDTO
    {
        public string Code { get; set; }
        public double FinalPrice { get; set; }
        public float Discount { get; set; }
        public double PaidPrice { get; set; }
        public double SendPrice { get; set; }
        public double DistancePrice { get; set; }
        public ICollection<CartItemDTO> CartItems { get; set; }
        public UserDataTable User { get; set; }
        public UserProfile UserProfile { get; set; }
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
