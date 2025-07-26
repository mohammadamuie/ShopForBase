using Project.Application.DTOs.CartItem;
using Project.Application.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Application.DTOs.PurchaseRequest
{
    public class SubmitPurchaseRequestDTO
    {
        [MustHaveOneElement(ErrorMessage = "سبد خرید خالی میباشد")]
        public List<CartItemFromUserDTO> Items { get; set; }
    }
}
